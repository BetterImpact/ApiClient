using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSquared.ApiClient.Serialization;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    [JsonConverter(typeof(PolymorphicClassConverter))]
    //this has to be here because otherwise the deserialization process tries to instantiate this class, which doesnt work.
    public abstract class RecordedFeedbackFieldBase
    {
        [JsonProperty("feedback_field_id")]
        public int FeedbackFieldId { get; set; }

        [JsonProperty("feedback_field_name")]
        public string FeedbackFieldName { get; set; }
    }
}
