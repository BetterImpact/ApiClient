using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSquared.ApiClient.Models;

namespace VolunteerSquared.ApiClient
{
    public class ApiClient
    {
        private RestClient RestClient;

        public ApiClient(string baseUrl, string username, string password)
        {
            RestClient = new RestClient(baseUrl);
            RestClient.Authenticator = new HttpBasicAuthenticator(username, password);
        }

        #region Enterprise

        public ListUsersModel ListEnterpriseUsers()
        {
            return ListEnterpriseUsers(new UsersFilterModelEnterprise());
        }

        public ListUsersModel ListEnterpriseUsers(UsersFilterModelEnterprise filterModel)
        {
            var request = new RestRequest(Routes.ListEnterpriseUsers, Method.GET);
            ApplyUsersFilterModelToRequest(request, filterModel);
            ApplyEnterpriseSpecificUsersFilterModelToRequest(request, filterModel);

            return RequestHelper.ExecuteRequest<ListUsersModel>(RestClient, request);
        }

        public User GetEnterpriseUser(int id)
        {
            var request = new RestRequest(Routes.GetEnterpriseUser, Method.GET);
            request.AddUrlSegment("id", id.ToString());

            return RequestHelper.ExecuteRequest<User>(RestClient, request);
        }

        #endregion

        #region Organization

        public ListUsersModel ListOrganizationUsers()
        {
            return ListOrganizationUsers(new UsersFilterModelOrganization());
        }

        public ListUsersModel ListOrganizationUsers(UsersFilterModelOrganization filterModel)
        {
            var request = new RestRequest(Routes.ListOrganizationUsers, Method.GET);
            ApplyUsersFilterModelToRequest(request, filterModel);
            ApplyOrganizationSpecificUsersFilterModelToRequest(request, filterModel);

            return RequestHelper.ExecuteRequest<ListUsersModel>(RestClient, request);
        }

        public User GetOrganizationUser(int id)
        {
            var request = new RestRequest(Routes.GetOrganizationUser, Method.GET);
            request.AddUrlSegment("id", id.ToString());

            return RequestHelper.ExecuteRequest<User>(RestClient, request);
        }

        #endregion

        #region General

        public string DownloadFileUserCustomField(UserCustomFieldFile fileModel, string savePath)
        {
            var request = new RestRequest(fileModel.Value, Method.GET);

            return RequestHelper.ExecuteFileDownloadRequest(RestClient, request, savePath);
        }

        public string DownloadUserPhoto(User user, string savePath)
        {
            var request = new RestRequest(user.PhotoUrlOriginal, Method.GET);

            return RequestHelper.ExecuteFileDownloadRequest(RestClient, request, savePath);
        }

        public string DownloadUserPhotoScaled(User user, string savePath)
        {
            var request = new RestRequest(user.PhotoUrlScaled, Method.GET);

            return RequestHelper.ExecuteFileDownloadRequest(RestClient, request, savePath);
        }

        #endregion

        private void ApplyUsersFilterModelToRequest(IRestRequest request, UsersFilterModelBase filterModel)
        {
            if(filterModel.PageNumber > 0)
            {
                request.AddQueryParameter("page_number", filterModel.PageNumber.ToString());
            }

            if (filterModel.PageSize > 0)
            {
                request.AddQueryParameter("page_size", filterModel.PageSize.ToString());
            }

            request.AddQueryParameter("include_custom_fields", filterModel.IncludeCustomFields.ToString());
            request.AddQueryParameter("include_qualifications", filterModel.IncludeQualifications.ToString());
            request.AddQueryParameter("include_memberships", filterModel.IncludeMemberships.ToString());
            request.AddQueryParameter("include_verified_volunteers_background_check_results", filterModel.IncludeVerifiedVolunteersBackgroundCheckResults.ToString());

            if (filterModel.UpdatedSince.HasValue)
            {
                request.AddQueryParameter("updated_since", filterModel.UpdatedSince.Value.ToString("o", CultureInfo.CurrentCulture));
            }

            if (filterModel.HasModules)
            {
                request.AddQueryParameter("modules", filterModel.Modules);
            }

            if (filterModel.HasVolunteerStatus)
            {
                request.AddQueryParameter("volunteer_status", filterModel.VolunteerStatus);
            }

            if (filterModel.HasMemberStatus)
            {
                request.AddQueryParameter("member_status", filterModel.MemberStatus);
            }

            if (filterModel.HasDonorStatus)
            {
                request.AddQueryParameter("donor_status", filterModel.DonorStatus);
            }

            if (filterModel.HasClientStatus)
            {
                request.AddQueryParameter("client_status", filterModel.ClientStatus);
            }

            if (filterModel.HasAdminStatus)
            {
                request.AddQueryParameter("admin_status", filterModel.AdminStatus);
            }
        }

        private void ApplyEnterpriseSpecificUsersFilterModelToRequest(IRestRequest request, UsersFilterModelEnterprise filterModel) {
            if (filterModel.HasOrganizationIds) {
                request.AddQueryParameter("organization_ids", filterModel.OrganizationIdsString);
            }
        }

        private void ApplyOrganizationSpecificUsersFilterModelToRequest(IRestRequest request, UsersFilterModelOrganization filterModel)
        {
            //no org specific stuff currently.
        }
    }
}