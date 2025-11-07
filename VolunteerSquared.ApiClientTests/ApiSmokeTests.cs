using NUnit.Framework;
using VolunteerSquared.ApiClient.Models;
using VolunteerSquared.ApiClient;
using System.Collections.Generic;

namespace VolunteerSquared.ApiClientTests
{
    public class ApiSmokeTests
    {
        private const string ApiBaseUrl = "https://api.betterimpact.com/";
        
        private const string OrgApiUsername = "YOUR_ORGANIZATION_API_USERNAME_HERE";
        private const string OrgApiPassword = "YOUR_ORGANIZATION_API_PASSWORD_HERE";
        private Client orgClient;

        private const string EntApiUsername = "YOUR_ENTERPRISE_API_USERNAME_HERE";
        private const string EntApiPassword = "YOUR_ENTERPRISE_API_PASSWORD_HERE";
        private Client entClient;

        [SetUp]
        public void Setup()
        {
            orgClient = new Client(ApiBaseUrl, OrgApiUsername, OrgApiPassword);
            entClient = new Client(ApiBaseUrl, EntApiUsername, EntApiPassword);
        }

        [Test]
        public void CanGetSingleEnterpriseUser()
        {
            var result = entClient.GetEnterpriseUser(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Memberships);
            Assert.IsNotNull(result.CustomFields);
            Assert.IsNotNull(result.Qualifications);
            Assert.IsNotNull(result.BackgroundCheckResults);
        }

        [Test]
        public void CanListEnterpriseUsers()
        {
            var result = entClient.ListEnterpriseUsers();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListEnterpriseUsersWithFilters()
        {
            var result = entClient.ListEnterpriseUsers(new UsersFilterModelEnterprise() { PageNumber = 2, IncludeCustomFields=true, IncludeMemberships=true, IncludeQualifications=true, IncludeVerifiedVolunteersBackgroundCheckResults=true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListEnterpriseUsersByIdList()
        {
            var result = entClient.GetEnterpriseUsersByIdList(new List<int> { 1, 2, 3 });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListEnterpriseTimelogEntries()
        {
            var result = entClient.ListEnterpriseTimelogEntries();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanListEnterpriseTimelogEntriesWithFilter()
        {
            var result = entClient.ListEnterpriseTimelogEntries(new TimelogFilterModelEnterprise() { FilterUserIds = new List<int> { 1,2 }, IncludeRecordedFeedbackFields=true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanGetSingleEnterpriseTimelogEntry()
        {
            var result = entClient.GetEnterpriseTimelogEntry(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public void CanGetEnterpriseTimelogEntriesByIdList()
        {
            var result = entClient.GetEnterpriseTimelogEntriesByIdList(new List<int> { 1, 2, 3 });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanLookupEnterpriseOrganizations()
        {
            var result = entClient.LookupEnterpriseOrganizations();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Organizations);
            Assert.IsNotEmpty(result.Organizations);
        }

        [Test]
        public void CanLookupEnterpriseActivityReportGroups()
        {
            var result = entClient.LookupEnterpriseActivityReportGroups();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActivityReportGroups);
            Assert.IsNotEmpty(result.ActivityReportGroups);
        }

        [Test]
        public void CanLookupEnterpriseQualifications()
        {
            var result = entClient.LookupEnterpriseQualifications();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Qualifications);
            Assert.IsNotEmpty(result.Qualifications);
        }

        [Test]
        public void CanLookupEnterpriseFeedbackFields()
        {
            var result = entClient.LookupEnterpriseFeedbackFields();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.FeedbackFields);
            Assert.IsNotEmpty(result.FeedbackFields);
        }

        [Test]
        public void CanLookupEnterpriseCustomFields()
        {
            var result = entClient.LookupEnterpriseCustomFields();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CustomFields);
            Assert.IsNotEmpty(result.CustomFields);
        }




        [Test]
        public void CanGetSingleOrganizationUser()
        {
            var result = orgClient.GetOrganizationUser(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Memberships);
            Assert.IsNotNull(result.CustomFields);
            Assert.IsNotNull(result.Qualifications);
            Assert.IsNotNull(result.BackgroundCheckResults);
        }

        [Test]
        public void CanListOrganizationUsers()
        {
            var result = orgClient.ListOrganizationUsers();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListOrganizationUsersWithFilters()
        {
            var result = orgClient.ListOrganizationUsers(new UsersFilterModelOrganization() { PageNumber = 2, IncludeCustomFields=true, IncludeMemberships=true, IncludeQualifications=true, IncludeVerifiedVolunteersBackgroundCheckResults=true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListOrganizationUsersByIdList()
        {
            var result = orgClient.GetOrganizationUsersByIdList(new List<int> { 1, 2, 3 });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListOrganizationTimelogEntries()
        {
            var result = orgClient.ListOrganizationTimelogEntries();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanListOrganizationTimelogEntriesWithFilter()
        {
            var result = orgClient.ListOrganizationTimelogEntries(new TimelogFilterModelOrganization() { FilterUserIds = new List<int> { 1, 2 }, IncludeRecordedFeedbackFields = true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanGetSingleOrganizationTimelogEntry()
        {
            var result = orgClient.GetOrganizationTimelogEntry(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public void CanGetOrganizationTimelogEntriesByIdList()
        {
            var result = orgClient.GetOrganizationTimelogEntriesByIdList(new List<int> { 1, 2, 3 });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanLookupOrganizationActivityCategories()
        {
            var result = orgClient.LookupOrganizationActivityCategories();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActivityCategories);
            Assert.IsNotEmpty(result.ActivityCategories);
        }

        [Test]
        public void CanLookupOrganizationQualifications()
        {
            var result = orgClient.LookupOrganizationQualifications();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Qualifications);
            Assert.IsNotEmpty(result.Qualifications);
        }

        [Test]
        public void CanLookupOrganizationFeedbackFields()
        {
            var result = orgClient.LookupOrganizationFeedbackFields();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.FeedbackFields);
            Assert.IsNotEmpty(result.FeedbackFields);
        }

        [Test]
        public void CanLookupOrganizationCustomFields()
        {
            var result = orgClient.LookupOrganizationCustomFields();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CustomFields);
            Assert.IsNotEmpty(result.CustomFields);
        }
    }
}