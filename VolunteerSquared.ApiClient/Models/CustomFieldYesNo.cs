using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("custom_field_yes_no")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class CustomFieldYesNo : CustomFieldBase
    {

    }
}
