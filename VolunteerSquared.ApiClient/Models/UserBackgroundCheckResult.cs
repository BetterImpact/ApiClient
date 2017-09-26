using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class UserBackgroundCheckResult
    {
        [JsonProperty("result_type_id")]
        public int ResultTypeId { get; set; }
        [JsonProperty("result_type_name")]
        public string ResultTypeName { get; set; }
        [JsonProperty("result_type_expires")]
        public bool ResultTypeExpires { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("needs_review_reason")]
        public string NeedsReviewReason { get; set; }
        [JsonProperty("effective_date")]
        public DateTime? EffectiveDate { get; set; }
        [JsonProperty("expiry_date")]
        public DateTime? ExpiryDate { get; set; }
    }
}
