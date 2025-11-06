using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class ActivityCategoriesLookupModel
    {
        [JsonProperty("activity_categories")]
        public IList<ActivityCategory> ActivityCategories { get; set; }
    }
}
