using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("custom_field_number")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class CustomFieldNumber : CustomFieldBase
    {

    }
}