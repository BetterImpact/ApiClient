using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using VolunteerSquared.ApiClient.Models;

namespace VolunteerSquared.ApiClient
{
    public static class RequestHelper
    {
        public static T ExecuteRequest<T> (IRestClient client, IRestRequest request) where T : new()
        {
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApiException(JsonConvert.DeserializeObject<ApiError>(response.Content).ErrorMessage, response.StatusCode);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
        }

        public static string ExecuteFileDownloadRequest(IRestClient client, IRestRequest request, string savePath)
        {
            var response = client.Execute(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApiException(JsonConvert.DeserializeObject<ApiError>(response.Content).ErrorMessage, response.StatusCode);
            }
            else
            {
                if (Directory.Exists(savePath))
                {
                    var fileName = ContentDispositionHeaderValue.Parse(response.Headers.First(h => h.Name == "Content-Disposition").Value.ToString()).FileName;
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
    }
}
