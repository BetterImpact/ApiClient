using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class CustomFieldDropDownOption
    {
        [JsonProperty("custom_field_option_id")]
        public int CustomFieldOptionId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}