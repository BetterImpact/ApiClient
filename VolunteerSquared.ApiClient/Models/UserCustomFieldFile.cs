using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("file")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldFile : UserCustomFieldBase
    {
        public UserCustomFieldFile()
        {
        }

        public UserCustomFieldFile(string value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
