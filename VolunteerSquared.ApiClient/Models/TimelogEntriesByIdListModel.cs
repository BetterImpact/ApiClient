using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class TimelogEntriesByIdListModel
    {
        [JsonProperty("timelog_entries")]
        public IList<Timelog> TimelogEntries { get; set; }
    }
}
