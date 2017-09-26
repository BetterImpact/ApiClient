using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VolunteerSquared.ApiClient.Tests
{
    [TestClass]
    public class tests
    {
        [TestMethod]
        public void TestListEnterpriseUsers()
        {
            var client = new ApiClient("http://localhost/VolunteerSquaredWeb4/", "entapi", "123456");

            var users = client.ListEnterpriseUsers(new Models.UsersFilterModel() { PageSize = 25 } );

            Console.WriteLine(users.Header.TotalItemCount);
            Console.WriteLine(users.Users.Count);
        }

        [TestMethod]
        public void TestGetEnterpriseUser()
        {
            var client = new ApiClient("http://localhost/VolunteerSquaredWeb4/", "entapi", "123456");

            var user = client.GetEnterpriseUser(1);
        }

        [TestMethod]
        public void TestListOrganizationUsers()
        {
            var client = new ApiClient("http://localhost/VolunteerSquaredWeb4/", "orgapi", "123456");

            var users = client.ListEnterpriseUsers(new Models.UsersFilterModel() { PageSize = 25 });

            Console.WriteLine(users.Header.TotalItemCount);
            Console.WriteLine(users.Users.Count);
        }

        [TestMethod]
        public void TestGetOrganizationUser()
        {
            var client = new ApiClient("http://localhost/VolunteerSquaredWeb4/", "orgapi", "123456");

            var user = client.GetEnterpriseUser(1);
        }
    }
}
