using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("short_text")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldShortText : UserCustomFieldBase
    {
        public UserCustomFieldShortText()
        {
        }

        public UserCustomFieldShortText(string value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}