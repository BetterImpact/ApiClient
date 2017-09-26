using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("check_box")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class UserCustomFieldCheckBox : UserCustomFieldBase
    {
        public UserCustomFieldCheckBox()
        {
        }

        public UserCustomFieldCheckBox(bool value)
        {
            this.Value = value;
        }

        [JsonProperty("value")]
        public bool Value { get; set; }
    }
}
