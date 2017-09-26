using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    [Serializable()]
    public class ListUsersModel
    {
        [JsonProperty("header")]
        public HeaderModel Header { get; set; }

        [JsonProperty("users")]
        public IList<User> Users { get; set; }
    }
}
