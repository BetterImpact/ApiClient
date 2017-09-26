using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("long_text")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldLongText : UserCustomFieldBase
    {
        public UserCustomFieldLongText()
        {
        }

        public UserCustomFieldLongText(string value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}