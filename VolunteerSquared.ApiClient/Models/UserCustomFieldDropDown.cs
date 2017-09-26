using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("drop_down")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldDropDown : UserCustomFieldBase
    {
        public UserCustomFieldDropDown()
        {
        }

        public UserCustomFieldDropDown(string value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("value_id")]
        public int ValueId { get; set; }
    }
}