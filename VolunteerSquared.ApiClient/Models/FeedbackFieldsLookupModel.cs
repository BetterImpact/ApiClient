using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class FeedbackFieldsLookupModel
    {
        [JsonProperty("feedback_fields")]
        public IList<FeedbackFieldBase> FeedbackFields { get; set; }
    }
}
