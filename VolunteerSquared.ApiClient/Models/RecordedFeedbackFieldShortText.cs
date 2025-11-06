using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [DisplayName("recorded_feedback_field_short_text")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class RecordedFeedbackFieldShortText : RecordedFeedbackFieldBase
    {
        public RecordedFeedbackFieldShortText() { }

        public RecordedFeedbackFieldShortText(string value) {
            Value = value;
        }

        [JsonProperty("value")]
        public string Value {  get; set; }
    }
}
