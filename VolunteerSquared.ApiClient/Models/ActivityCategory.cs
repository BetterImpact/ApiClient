using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class ActivityCategory
    {
        [JsonProperty("activity_category_id")]
        public int ActivityCategoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
