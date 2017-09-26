using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient
{
    public class Routes
    {
        public const string ListEnterpriseUsers = "v1/enterprise/users";
        public const string GetEnterpriseUser = "v1/enterprise/users/{id}";

        public const string ListOrganizationUsers = "v1/organization/users";
        public const string GetOrganizationUser = "v1/organization/users/{id}";

        //there are 2 more "virtual" routes that we use to retrieve files, but they come as prepopulated urls in the file custom field object.
    }
}
