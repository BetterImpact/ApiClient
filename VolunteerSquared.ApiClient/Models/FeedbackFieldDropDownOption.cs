using Newtonsoft.Json;
using System;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable]
    public class FeedbackFieldDropDownOption
    {
        [JsonProperty("feedback_field_option_id")]
        public int FeedbackFieldOptionId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;
    }
}