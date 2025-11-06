using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class CustomFieldsLookupModel
    {
        [JsonProperty("custom_fields")]
        public IList<CustomFieldBase> CustomFields { get; set; }
    }
}
