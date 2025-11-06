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
    [DisplayName("recorded_feedback_field_long_text")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class RecordedFeedbackFieldLongText : RecordedFeedbackFieldBase
    {
        public RecordedFeedbackFieldLongText() { }

        public RecordedFeedbackFieldLongText(string value) {
            Value = value;
        }

        [JsonProperty("value")]
        public string Value {  get; set; }
    }
}
