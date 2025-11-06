using Newtonsoft.Json;
using System;
using System.ComponentModel;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable]
    [DisplayName("feedback_field_short_text")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class FeedbackFieldShortText : FeedbackFieldBase
    {

    }
}