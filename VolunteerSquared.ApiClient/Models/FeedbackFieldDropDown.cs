using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable]
    [DisplayName("feedback_field_drop_down")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class FeedbackFieldDropDown : FeedbackFieldBase
    {
        [JsonProperty("options")]
        public List<FeedbackFieldDropDownOption> Options { get; set; } = new List<FeedbackFieldDropDownOption>();
    }
}