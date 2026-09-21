using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using VolunteerSquared.ApiClient.Models;

namespace VolunteerSquared.ApiClient
{
    public static class RequestHelper
    {
        private class RequireObjectPropertiesContractResolver : DefaultContractResolver
        {
            protected override JsonObjectContract CreateObjectContract(Type objectType)
            {
                var contract = base.CreateObjectContract(objectType);

                contract.ItemRequired = Required.AllowNull; // Require all properties to be present, but allow null values

                return contract;
            }
        }

        public static T ExecuteRequest<T> (IRestClient client, IRestRequest request) where T : new()
        {
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw ErrorFrom(response);
            }
            else
            {
                var settings = new JsonSerializerSettings();

#if DEBUG
                //if we're in debug mode, be strict about missing members in the json, and also missing members in objects
                settings.ContractResolver = new RequireObjectPropertiesContractResolver();
                settings.MissingMemberHandling = MissingMemberHandling.Error;
#endif

                return JsonConvert.DeserializeObject<T>(response.Content, settings);
            }
        }

        public static string ExecuteFileDownloadRequest(IRestClient client, IRestRequest request, string savePath)
        {
            return ExecuteFileDownloadRequest(client, request, savePath, null);
        }

        /// <summary>
        /// Downloads whatever the request points at and saves it to disk.
        /// </summary>
        /// <param name="savePath">Either a folder (the name the server sends us is used) or a complete file name.</param>
        /// <param name="defaultFileName">
        /// The name to fall back on when savePath is a folder and the response does not tell us what the file is called.
        /// Not every endpoint sends a Content-Disposition header, the time clock qr code images being one example.
        /// </param>
        /// <returns>The complete path of the file that was written.</returns>
        public static string ExecuteFileDownloadRequest(IRestClient client, IRestRequest request, string savePath, string defaultFileName)
        {
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw ErrorFrom(response);
            }
            else
            {
                if (Directory.Exists(savePath))
                {
                    var fileName = GetFileNameFromResponse(response) ?? defaultFileName;

                    if (string.IsNullOrEmpty(fileName))
                    {
                        throw new ApiException("The response did not tell us what this file is called. Pass a complete file path instead of a folder.", response.StatusCode);
                    }

                    var completeFilePath = Path.Combine(savePath, fileName);
                    var counter = 0;

                    while (File.Exists(completeFilePath))
                    {
                        counter++;

                        completeFilePath = Path.Combine(savePath, String.Format("{0} ({1}){2}", Path.GetFileNameWithoutExtension(fileName), counter, Path.GetExtension(fileName)));
                    }

                    response.RawBytes.SaveAs(completeFilePath);

                    return completeFilePath;
                }
                else
                {
                    response.RawBytes.SaveAs(savePath);

                    return savePath;
                }
            }
        }

        /// <summary>
        /// Turns whatever came back into something worth reading. A request that never reached the api, or one that
        /// answered with something other than the usual error json, has no error message for us to pass on, so say
        /// what actually happened instead of failing while trying to read one.
        /// </summary>
        private static ApiException ErrorFrom(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                //a bad url, a name that does not resolve, a timeout, a certificate problem, and so on. there is no
                //response uri to report when the request never got off the ground, so fall back to what was asked for.
                var target = response.ResponseUri != null ? response.ResponseUri.ToString() : null;

                if (string.IsNullOrEmpty(target) && response.Request != null)
                {
                    target = response.Request.Resource;
                }

                return new ApiException(string.Format("The request to {0} did not complete ({1}): {2}", target, response.ResponseStatus, response.ErrorMessage), response.StatusCode);
            }

            if (!string.IsNullOrEmpty(response.Content))
            {
                try
                {
                    var error = JsonConvert.DeserializeObject<ApiError>(response.Content);

                    if (error != null && !string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        return new ApiException(error.ErrorMessage, response.StatusCode);
                    }
                }
                catch (JsonException)
                {
                    //not the error json we were expecting, an html error page for instance. fall through and hand
                    //back what we were given.
                }
            }

            return new ApiException(string.Format("The api returned {0} ({1}). {2}", (int)response.StatusCode, response.StatusDescription, response.Content), response.StatusCode);
        }

        /// <summary>
        /// Digs the file name out of the Content-Disposition header, if the response has one. The name is reduced to a
        /// plain file name so that a name chosen on the server can never write outside of the folder we were asked to
        /// save into.
        /// </summary>
        private static string GetFileNameFromResponse(IRestResponse response)
        {
            var header = response.Headers.FirstOrDefault(h => string.Equals(h.Name, "Content-Disposition", StringComparison.OrdinalIgnoreCase));

            if (header == null)
            {
                return null;
            }

            ContentDispositionHeaderValue contentDisposition;

            if (!ContentDispositionHeaderValue.TryParse(header.Value.ToString(), out contentDisposition))
            {
                return null;
            }

            //FileNameStar carries the utf8 encoded name when the server sends one. FileName comes back with the quotes
            //still attached, and quotes are not legal in a windows file name, so they have to come off.
            var fileName = !string.IsNullOrEmpty(contentDisposition.FileNameStar)
                ? contentDisposition.FileNameStar
                : (contentDisposition.FileName ?? string.Empty).Trim('"');

            //throw away any folder information that came along for the ride.
            var lastSeparator = fileName.LastIndexOfAny(new[] { '/', '\\', ':' });

            if (lastSeparator >= 0)
            {
                fileName = fileName.Substring(lastSeparator + 1);
            }

            foreach (var invalidCharacter in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidCharacter, '_');
            }

            //a name made up of nothing but dots and spaces is not a usable file name.
            return fileName.Trim('.', ' ').Length == 0 ? null : fileName;
        }
    }
}
