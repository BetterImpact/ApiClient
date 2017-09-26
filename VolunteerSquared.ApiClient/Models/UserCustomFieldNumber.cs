using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("number")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldNumber : UserCustomFieldBase
    {
        public UserCustomFieldNumber()
        {
        }

        public UserCustomFieldNumber(double value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public double Value { get; set; }
    }
}