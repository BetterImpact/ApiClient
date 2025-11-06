using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class ActivityReportGroupsLookupModel
    {
        [JsonProperty("activity_report_groups")]
        public IList<ActivityReportGroup> ActivityReportGroups { get; set; }
    }
}
