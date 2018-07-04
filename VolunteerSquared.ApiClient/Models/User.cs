using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class User
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("legal_first_name")]
        public string LegalFirstName { get; set; }
        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("suffix")]
        public string Suffix { get; set; }
        [JsonProperty("address_line_1")]
        public string Address1 { get; set; }
        [JsonProperty("address_line_2")]
        public string Address2 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("zip_code")]
        public string PostalCode { get; set; }
        [JsonProperty("state")]
        public string Province { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("email_address")]
        public string EmailAddress { get; set; }
        [JsonProperty("secondary_email_address")]
        public string SecondaryEmailAddress { get; set; }
        [JsonProperty("mobile_email_address")]
        public string MobileEmailAddress { get; set; }
        [JsonProperty("home_phone")]
        public string HomePhone { get; set; }
        [JsonProperty("work_phone")]
        public string WorkPhone { get; set; }
        [JsonProperty("work_phone_ext")]
        public string WorkPhoneExt { get; set; }
        [JsonProperty("cell_phone")]
        public string CellPhone { get; set; }
        [JsonProperty("phone_preference")]
        public string PhonePreference { get; set; }
        [JsonProperty("twitter_username")]
        public string TwitterUsername { get; set; }
        [JsonProperty("linkedin_profile_url")]
        public string LinkedInProfileUrl { get; set; }
        [JsonProperty("instagram_username")]
        public string InstagramUserName { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("birthday")]
        public System.DateTime? Birthday { get; set; }
        [JsonProperty("date_created")]
        public System.DateTime DateCreated { get; set; }
        [JsonProperty("date_updated")]
        public System.DateTime DateUpdated { get; set; }
        [JsonProperty("region")]
        public string Language { get; set; }
        [JsonProperty("is_group")]
        public bool IsGroup { get; set; }
        [JsonProperty("group_name")]
        public string GroupName { get; set; }
        [JsonProperty("photo_url_scaled")]
        public string PhotoUrlScaled { get; set; }
        [JsonProperty("photo_url_original")]
        public string PhotoUrlOriginal { get; set; }

        [JsonProperty("memberships")]
        public IList<UserOrganizationMembership> Memberships { get; set; }
        [JsonProperty("custom_fields")]
        public IList<UserCustomFieldBase> CustomFields { get; set; }
        [JsonProperty("qualifications")]
        public IList<UserQualification> Qualifications { get; set; }
        [JsonProperty("background_check_results")]
        public IList<UserBackgroundCheckResult> BackgroundCheckResults { get; set; }
    }
}
