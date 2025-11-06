using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class ActivityReportGroup
    {
        [JsonProperty("activity_report_group_id")]
        public int ActivityReportGroupId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
