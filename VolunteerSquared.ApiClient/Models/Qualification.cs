using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable]
    public class Qualification
    {
        [JsonProperty("qualification_id")]
        public int QualificationId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("expires")]
        public bool Expires { get; set; }
        [JsonProperty("match_type")]
        public string MatchType { get; set; }
        [JsonProperty("display_order")]
        public int DisplayOrder { get; set; }
        [JsonProperty("belongs_to_enterprise")]
        public bool BelongsToEnterprise { get; set; }

        [JsonProperty("qualification_category_id")]
        public int? QualificationCategoryId { get; set; }
        [JsonProperty("qualification_category_name")]
        public string QualificationCategoryName { get; set; }
        [JsonProperty("qualification_category_display_order")]
        public int? QualificationCategoryDisplayOrder { get; set; }

        [JsonProperty("options")]
        public IList<QualificationOption> Options { get; set; }
    }
}
