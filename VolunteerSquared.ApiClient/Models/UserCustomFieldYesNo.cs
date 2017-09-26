using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("yes_no")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldYesNo : UserCustomFieldBase
    {
        public UserCustomFieldYesNo()
        {
        }

        public UserCustomFieldYesNo(bool value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public bool Value { get; set; }
    }
}
