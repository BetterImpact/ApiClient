using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class OrganizationsLookupModel
    {
        [JsonProperty("organizations")]
        public IList<Organization> Organizations { get; set; }
    }
}
