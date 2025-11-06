using Newtonsoft.Json;
using System;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public abstract class FeedbackFieldBase
    {
        [JsonProperty("feedback_field_id")]
        public int FeedbackFieldId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("prompt")]
        public string Prompt { get; set; } = string.Empty;
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("required")]
        public bool Required { get; set; }
        [JsonProperty("belongs_to_enterprise")]
        public bool BelongsToEnterprise { get; set; }
    }
}