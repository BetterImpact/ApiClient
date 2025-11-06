using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("custom_field_drop_down")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class CustomFieldDropDown : CustomFieldBase
    {
        [JsonProperty("options")]
        public List<CustomFieldDropDownOption> Options { get; set; }
    }
}