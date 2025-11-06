using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable]
    public class QualificationOption
    {
        [JsonProperty("qualification_option_id")]
        public int QualificationOptionId { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("rank")]
        public int Rank { get; set; }
    }
}
