using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("custom_field_check_box")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class CustomFieldCheckBox : CustomFieldBase
    {
        
    }
}
