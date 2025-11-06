using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSquared.ApiClient.Models;

namespace VolunteerSquared.ApiClient
{
    public class Routes
    {
        public const string V1EnterpriseUser = "v1/enterprise/users/{id}";
        public const string V1EnterpriseUserList = "v1/enterprise/users";
        public const string V1EnterpriseUserListByIdList = "v1/enterprise/by_id_list/users";
        public const string V1EnterpriseUserCustomFieldFile = "v1/enterprise/users/{userId}/custom_fields/{id}/file";
        public const string V1EnterpriseUserCustomFieldSignedDocument = "v1/enterprise/users/{userId}/custom_fields/{id}/signed_document";
        public const string V1EnterpriseTimelogEntry = "v1/enterprise/timelog_entries/{id}";
        public const string V1EnterpriseTimelogEntriesList = "v1/enterprise/timelog_entries";
        public const string V1EnterpriseTimelogEntriesListByIdList = "v1/enterprise/by_id_list/timelog_entries";
        public const string V1EnterpriseLookupOrganizations = "v1/enterprise/look_up/organizations";
        public const string V1EnterpriseLookupActivityReportGroups = "v1/enterprise/look_up/activity_report_groups";
        public const string V1EnterpriseLookupQualifications = "v1/enterprise/look_up/qualifications";
        public const string V1EnterpriseLookupCustomFields = "v1/enterprise/look_up/custom_fields";
        public const string V1EnterpriseLookupFeedbackFields = "v1/enterprise/look_up/feedback_fields";

        public const string V1OrganizationUser = "v1/organization/users/{id}";
        public const string V1OrganizationUserList = "v1/organization/users";
        public const string V1OrganizationUserListByIdList = "v1/organization/by_id_list/users";
        public const string V1OrganizationUserCustomFieldFile = "v1/organization/users/{userId}/custom_fields/{id}/file";
        public const string V1OrganizationUserCustomFieldSignedDocument = "v1/organization/users/{userId}/custom_fields/{id}/signed_document";
        public const string V1OrganizationTimelogEntry = "v1/organization/timelog_entries/{id}";
        public const string V1OrganizationTimelogEntriesList = "v1/organization/timelog_entries";
        public const string V1OrganizationTimelogEntriesListByIdList = "v1/organization/by_id_list/timelog_entries";
        public const string V1OrganizationLookupActivityCategories = "v1/organization/look_up/activity_categories";
        public const string V1OrganizationLookupQualifications = "v1/organization/look_up/qualifications";
        public const string V1OrganizationLookupCustomFields = "v1/organization/look_up/custom_fields";
        public const string V1OrganizationLookupFeedbackFields = "v1/organization/look_up/feedback_fields";

        //there are 2 more "virtual" routes that we use to retrieve files, but they come as prepopulated urls in the file custom field object.
    }
}
