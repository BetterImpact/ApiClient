using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolunteerSquared.ApiClient.Models
{
    public class UsersFilterModelEnterprise : UsersFilterModelBase
    {
        #region Organization Ids

        public List<int> OrganizationIds { get; set; }

        internal string OrganizationIdsString
        {
            get
            {
                return string.Join(",", OrganizationIds);
            }
        }

        internal bool HasOrganizationIds
        {
            get
            {
                return OrganizationIds != null && OrganizationIds.Any();
            }
        }

        #endregion
    }
}
