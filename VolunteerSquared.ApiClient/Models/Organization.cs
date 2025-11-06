using Newtonsoft.Json;
using System;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable]
    public class Organization
    {
        [JsonProperty("organization_id")]
        public int OrganizationId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }
        [JsonProperty("date_updated")]
        public DateTime DateUpdated { get; set; }

        [JsonProperty("has_administrator_module")]
        public bool HasAdministratorModule { get; set; }
        [JsonProperty("has_client_module")]
        public bool HasClientModule { get; set; }
        [JsonProperty("has_donor_module")]
        public bool HasDonorModule { get; set; }
        [JsonProperty("has_member_module")]
        public bool HasMemberModule { get; set; }
        [JsonProperty("has_volunteer_module")]
        public bool HasVolunteerModule { get; set; }

        [JsonProperty("enterprise_region_id")]
        public int? EnterpriseRegionId { get; set; }
        [JsonProperty("enterprise_region_name")]
        public string EnterpriseRegionName { get; set; }
    }
}
