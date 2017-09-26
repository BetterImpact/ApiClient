using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient
{
    class ApiException: Exception
    {
        public readonly HttpStatusCode StatusCode;

        public ApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
