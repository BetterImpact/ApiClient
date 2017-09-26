using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class UserOrganizationMembership
    {
        [JsonProperty("organization_member_id")]
        public int OrganizationMemberId { get; set; }
        [JsonProperty("organization_id")]
        public int OrganizationId { get; set; }
        [JsonProperty("organization_name")]
        public string OrganizationName { get; set; }
        [JsonProperty("date_created")]
        public System.DateTime DateCreated { get; set; }
        [JsonProperty("date_updated")]
        public System.DateTime DateUpdated { get; set; }

        [JsonProperty("is_administrator")]
        public bool IsAdministrator { get; set; }
        [JsonProperty("administrator_status")]
        public string AdministratorStatus { get; set; }
        [JsonProperty("administrator_type")]
        public string AdministratorType { get; set; }

        [JsonProperty("is_client")]
        public bool IsClient { get; set; }
        [JsonProperty("client_status")]
        public string ClientStatus { get; set; }
        [JsonProperty("client_date_joined")]
        public System.DateTime? ClientDateJoined { get; set; }
        [JsonProperty("client_last_status_change")]
        public System.DateTime? ClientLastStatusChange { get; set; }

        [JsonProperty("is_donor")]
        public bool IsDonor { get; set; }
        [JsonProperty("donor_status")]
        public string DonorStatus { get; set; }

        [JsonProperty("is_member")]
        public bool IsMember { get; set; }
        [JsonProperty("member_status")]
        public string MemberStatus { get; set; }

        [JsonProperty("is_volunteer")]
        public bool IsVolunteer { get; set; }
        [JsonProperty("volunteer_status")]
        public string VolunteerStatus { get; set; }
        [JsonProperty("volunteer_inactive_status_reason")]
        public string VolunteerInactiveStatusReason { get; set; }
        [JsonProperty("volunteer_archived_status_reason")]
        public string VolunteerArchivedStatusReason { get; set; }
        [JsonProperty("volunteer_last_status_change")]
        public System.DateTime? VolunteerLastStatusChange { get; set; }
        [JsonProperty("volunteer_notes")]
        public string VolunteerNotes { get; set; }
        [JsonProperty("volunteer_application_form")]
        public int? VolunteerApplicationForm { get; set; }
        [JsonProperty("volunteer_date_joined")]
        public System.DateTime? VolunteerDateJoined { get; set; }

        [JsonProperty("volunteer_total_hours")]
        public double VolunteerTotalHours { get; set; }
    }
}
