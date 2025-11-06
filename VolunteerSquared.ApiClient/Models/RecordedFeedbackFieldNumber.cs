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
    [DisplayName("recorded_feedback_field_number")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class RecordedFeedbackFieldNumber : RecordedFeedbackFieldBase
    {
        public RecordedFeedbackFieldNumber() { }

        public RecordedFeedbackFieldNumber(double value) {
            Value = value;
        }

        [JsonProperty("value")]
        public double Value {  get; set; }
    }
}
