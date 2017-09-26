using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    public class ApiError
    {
        [JsonProperty("error_message")]
        public string ErrorMessage = String.Empty;
    }
}
