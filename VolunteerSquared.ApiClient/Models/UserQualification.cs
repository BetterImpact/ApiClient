using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class UserQualification
    {
        [JsonProperty("qualification_id")]
        public int QualificationId { get; set; }
        [JsonProperty("qualification_name")]
        public string QualificationName { get; set; }
        [JsonProperty("qualification_expires")]
        public bool QualificationExpires { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("value_id")]
        public int ValueId { get; set; }
        [JsonProperty("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        //TODO: more fields (permissions, displays on, expiry, etc.)
    }
}
