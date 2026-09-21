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
    public class Client
    {
        private RestClient RestClient;

        public Client(string baseUrl, string username, string password)
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
            var request = new RestRequest(Routes.V1EnterpriseUserList, Method.GET);
            ApplyUsersFilterModelToRequest(request, filterModel);
            ApplyEnterpriseSpecificUsersFilterModelToRequest(request, filterModel);

            return RequestHelper.ExecuteRequest<ListUsersModel>(RestClient, request);
        }

        public User GetEnterpriseUser(int id)
        {
            var request = new RestRequest(Routes.V1EnterpriseUser, Method.GET);
            request.AddUrlSegment("id", id.ToString());

            return RequestHelper.ExecuteRequest<User>(RestClient, request);
        }

        public UsersByIdListModel GetEnterpriseUsersByIdList(List<int> ids)
        {
            var request = new RestRequest(Routes.V1EnterpriseUserListByIdList, Method.GET);
            request.AddQueryParameter("ids", string.Join(",", ids));

            return RequestHelper.ExecuteRequest<UsersByIdListModel>(RestClient, request);
        }

        public ListTimelogEntriesModel ListEnterpriseTimelogEntries()
        {
            return ListEnterpriseTimelogEntries(new TimelogFilterModelEnterprise());
        }

        public ListTimelogEntriesModel ListEnterpriseTimelogEntries(TimelogFilterModelEnterprise filterModel)
        {
            var request = new RestRequest(Routes.V1EnterpriseTimelogEntriesList, Method.GET);
            ApplyTimelogFilterModelToRequest(request, filterModel);
            ApplyEnterpriseSpecificTimelogFilterModelToRequest(request, filterModel);

            return RequestHelper.ExecuteRequest<ListTimelogEntriesModel>(RestClient, request);
        }

        public Timelog GetEnterpriseTimelogEntry(int id)
        {
            var request = new RestRequest(Routes.V1EnterpriseTimelogEntry, Method.GET);
            request.AddUrlSegment("id", id.ToString());

            return RequestHelper.ExecuteRequest<Timelog>(RestClient, request);
        }

        public TimelogEntriesByIdListModel GetEnterpriseTimelogEntriesByIdList(List<int> ids)
        {
            var request = new RestRequest(Routes.V1EnterpriseTimelogEntriesListByIdList, Method.GET);
            request.AddQueryParameter("ids", string.Join(",", ids));

            return RequestHelper.ExecuteRequest<TimelogEntriesByIdListModel>(RestClient, request);
        }

        public OrganizationsLookupModel LookupEnterpriseOrganizations()
        {
            var request = new RestRequest(Routes.V1EnterpriseLookupOrganizations, Method.GET);

            return RequestHelper.ExecuteRequest<OrganizationsLookupModel>(RestClient, request);
        }

        public ActivityReportGroupsLookupModel LookupEnterpriseActivityReportGroups()
        {
            var request = new RestRequest(Routes.V1EnterpriseLookupActivityReportGroups, Method.GET);

            return RequestHelper.ExecuteRequest<ActivityReportGroupsLookupModel>(RestClient, request);
        }

        public QualificationsLookupModel LookupEnterpriseQualifications()
        {
            var request = new RestRequest(Routes.V1EnterpriseLookupQualifications, Method.GET);

            return RequestHelper.ExecuteRequest<QualificationsLookupModel>(RestClient, request);
        }

        public FeedbackFieldsLookupModel LookupEnterpriseFeedbackFields()
        {
            var request = new RestRequest(Routes.V1EnterpriseLookupFeedbackFields, Method.GET);

            return RequestHelper.ExecuteRequest<FeedbackFieldsLookupModel>(RestClient, request);
        }

        public CustomFieldsLookupModel LookupEnterpriseCustomFields()
        {
            var request = new RestRequest(Routes.V1EnterpriseLookupCustomFields, Method.GET);

            return RequestHelper.ExecuteRequest<CustomFieldsLookupModel>(RestClient, request);
        }

        #endregion

        #region Organization

        public ListUsersModel ListOrganizationUsers()
        {
            return ListOrganizationUsers(new UsersFilterModelOrganization());
        }

        public ListUsersModel ListOrganizationUsers(UsersFilterModelOrganization filterModel)
        {
            var request = new RestRequest(Routes.V1OrganizationUserList, Method.GET);
            ApplyUsersFilterModelToRequest(request, filterModel);
            ApplyOrganizationSpecificUsersFilterModelToRequest(request, filterModel);

            return RequestHelper.ExecuteRequest<ListUsersModel>(RestClient, request);
        }

        public User GetOrganizationUser(int id)
        {
            var request = new RestRequest(Routes.V1OrganizationUser, Method.GET);
            request.AddUrlSegment("id", id.ToString());

            return RequestHelper.ExecuteRequest<User>(RestClient, request);
        }

        public UsersByIdListModel GetOrganizationUsersByIdList(List<int> ids)
        {
            var request = new RestRequest(Routes.V1OrganizationUserListByIdList, Method.GET);
            request.AddQueryParameter("ids", string.Join(",", ids));

            return RequestHelper.ExecuteRequest<UsersByIdListModel>(RestClient, request);
        }

        public ListTimelogEntriesModel ListOrganizationTimelogEntries()
        {
            return ListOrganizationTimelogEntries(new TimelogFilterModelOrganization());
        }

        public ListTimelogEntriesModel ListOrganizationTimelogEntries(TimelogFilterModelOrganization filterModel)
        {
            var request = new RestRequest(Routes.V1OrganizationTimelogEntriesList, Method.GET);
            ApplyTimelogFilterModelToRequest(request, filterModel);
            ApplyOrganizationSpecificTimelogFilterModelToRequest(request, filterModel);

            return RequestHelper.ExecuteRequest<ListTimelogEntriesModel>(RestClient, request);
        }

        public Timelog GetOrganizationTimelogEntry(int id)
        {
            var request = new RestRequest(Routes.V1OrganizationTimelogEntry, Method.GET);
            request.AddUrlSegment("id", id.ToString());

            return RequestHelper.ExecuteRequest<Timelog>(RestClient, request);
        }

        public TimelogEntriesByIdListModel GetOrganizationTimelogEntriesByIdList(List<int> ids)
        {
            var request = new RestRequest(Routes.V1OrganizationTimelogEntriesListByIdList, Method.GET);
            request.AddQueryParameter("ids", string.Join(",", ids));

            return RequestHelper.ExecuteRequest<TimelogEntriesByIdListModel>(RestClient, request);
        }

        public ActivityCategoriesLookupModel LookupOrganizationActivityCategories()
        {
            var request = new RestRequest(Routes.V1OrganizationLookupActivityCategories, Method.GET);

            return RequestHelper.ExecuteRequest<ActivityCategoriesLookupModel>(RestClient, request);
        }

        public QualificationsLookupModel LookupOrganizationQualifications()
        {
            var request = new RestRequest(Routes.V1OrganizationLookupQualifications, Method.GET);

            return RequestHelper.ExecuteRequest<QualificationsLookupModel>(RestClient, request);
        }

        public FeedbackFieldsLookupModel LookupOrganizationFeedbackFields()
        {
            var request = new RestRequest(Routes.V1OrganizationLookupFeedbackFields, Method.GET);

            return RequestHelper.ExecuteRequest<FeedbackFieldsLookupModel>(RestClient, request);
        }

        public CustomFieldsLookupModel LookupOrganizationCustomFields()
        {
            var request = new RestRequest(Routes.V1OrganizationLookupCustomFields, Method.GET);

            return RequestHelper.ExecuteRequest<CustomFieldsLookupModel>(RestClient, request);
        }
        #endregion

        #region General

        public string DownloadFileUserCustomField(UserCustomFieldFile fileModel, string savePath)
        {
            var request = new RestRequest(fileModel.Value, Method.GET);

            return RequestHelper.ExecuteFileDownloadRequest(RestClient, request, savePath);
        }

        public string DownloadSignedDocumentUserCustomField(UserCustomFieldSignedDocument signedDocumentModel, string savePath)
        {
            var request = new RestRequest(signedDocumentModel.Value, Method.GET);

            return RequestHelper.ExecuteFileDownloadRequest(RestClient, request, savePath);
        }

        /// <summary>
        /// Downloads the time clock qr code image for a user.
        /// </summary>
        /// <param name="savePath">
        /// A complete file path, or a folder. The qr code endpoint does not tell us what the image is called, so when a
        /// folder is given the file is named after the user id.
        /// </param>
        /// <remarks>
        /// Users who sign in with single sign on do not get a qr code, because they cannot use it to sign in to the
        /// time clock. Their TimeClockQRCodeUrl comes back empty, so check it before calling this.
        /// </remarks>
        public string DownloadUserTimeClockQRCode(User user, string savePath)
        {
            if (string.IsNullOrEmpty(user.TimeClockQRCodeUrl))
            {
                throw new ArgumentException("This user does not have a time clock qr code.", nameof(user));
            }

            var request = new RestRequest(user.TimeClockQRCodeUrl, Method.GET);

            return RequestHelper.ExecuteFileDownloadRequest(RestClient, request, savePath, string.Format("timeclock-qr-code-{0}.png", user.UserId));
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

        /// <summary>
        /// Formats a date the way the api wants it: iso 8601, with a timezone on it.
        /// </summary>
        /// <remarks>
        /// Always sent as UTC. The api rejects a date with no timezone at all, and its worked_from and worked_to
        /// filters currently fail on a date carrying an offset rather than a "Z", so UTC is the one form every
        /// filter accepts. A date with no Kind set is taken to be local time, which is what .net does everywhere
        /// else.
        /// </remarks>
        private static string FormatDateForApi(DateTime date)
        {
            return date.ToUniversalTime().ToString("o", CultureInfo.InvariantCulture);
        }

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
                request.AddQueryParameter("updated_since", FormatDateForApi(filterModel.UpdatedSince.Value));
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

        private void ApplyTimelogFilterModelToRequest(IRestRequest request, TimelogFilterModelBase filterModel)
        {
            if (filterModel.PageNumber > 0)
            {
                request.AddQueryParameter("page_number", filterModel.PageNumber.ToString());
            }

            if (filterModel.PageSize > 0)
            {
                request.AddQueryParameter("page_size", filterModel.PageSize.ToString());
            }

            request.AddQueryParameter("include_recorded_feedback_fields", filterModel.IncludeRecordedFeedbackFields.ToString());
            request.AddQueryParameter("approved", filterModel.FilterApprovedStatusString);

            if (filterModel.HasFilterUserIds)
            {
                request.AddQueryParameter("user_ids", filterModel.FilterUserIdsString);
            }

            if (filterModel.HasFilterActivityIds)
            {
                request.AddQueryParameter("activity_ids", filterModel.FilterActivityIdsString);
            }

            if (filterModel.HasFilterActivityCategoryIds)
            {
                request.AddQueryParameter("activity_category_ids", filterModel.FilterActivityCategoryIdsString);
            }

            if (filterModel.HasFilterActivityReportGroupIds)
            {
                request.AddQueryParameter("activity_report_group_ids", filterModel.FilterActivityReportGroupIdsString);
            }

            if (filterModel.UpdatedSince.HasValue)
            {
                request.AddQueryParameter("updated_since", FormatDateForApi(filterModel.UpdatedSince.Value));
            }

            if (filterModel.CreatedFrom.HasValue)
            {
                request.AddQueryParameter("created_from", FormatDateForApi(filterModel.CreatedFrom.Value));
            }

            if (filterModel.CreatedTo.HasValue)
            {
                request.AddQueryParameter("created_to", FormatDateForApi(filterModel.CreatedTo.Value));
            }

            if (filterModel.WorkedFrom.HasValue)
            {
                request.AddQueryParameter("worked_from", FormatDateForApi(filterModel.WorkedFrom.Value));
            }

            if (filterModel.WorkedTo.HasValue)
            {
                request.AddQueryParameter("worked_to", FormatDateForApi(filterModel.WorkedTo.Value));
            }
        }

        private void ApplyEnterpriseSpecificTimelogFilterModelToRequest(IRestRequest request, TimelogFilterModelEnterprise filterModel)
        {
            if (filterModel.HasOrganizationIds)
            {
                request.AddQueryParameter("organization_ids", filterModel.OrganizationIdsString);
            }
        }

        private void ApplyOrganizationSpecificTimelogFilterModelToRequest(IRestRequest request, TimelogFilterModelOrganization filterModel)
        {
            //no org specific stuff currently.
        }
    }
}