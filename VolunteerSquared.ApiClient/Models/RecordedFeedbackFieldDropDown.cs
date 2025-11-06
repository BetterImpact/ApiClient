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
    [DisplayName("recorded_feedback_field_drop_down")]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    public class RecordedFeedbackFieldDropDown : RecordedFeedbackFieldBase
    {
        public RecordedFeedbackFieldDropDown() { }

        public RecordedFeedbackFieldDropDown(string value, int valueId) {
            Value = value;
            ValueId = valueId;
        }

        [JsonProperty("value")]
        public string Value {  get; set; }

        [JsonProperty("value_id")]
        public int ValueId { get; set; }
    }
}
