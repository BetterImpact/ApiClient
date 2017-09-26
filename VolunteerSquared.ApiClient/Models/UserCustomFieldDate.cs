using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("date")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldDate : UserCustomFieldBase
    {
        public UserCustomFieldDate()
        {
        }

        public UserCustomFieldDate(DateTime value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public DateTime Value { get; set; }
    }
}