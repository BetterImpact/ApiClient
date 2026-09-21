using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VolunteerSquared.ApiClient;
using VolunteerSquared.ApiClient.Models;

namespace VolunteerSquared.ApiClientTests
{
    /// <summary>
    /// One test per endpoint, run against a real account. Credentials come from TestConfiguration, and the ids the
    /// tests need are looked up from the account rather than hard coded, so this runs against any account.
    /// </summary>
    public class ApiSmokeTests
    {
        private Client organizationClient;
        private Client enterpriseClient;

        private Client OrganizationClient
        {
            get { return organizationClient ?? (organizationClient = TestConfiguration.CreateOrganizationClient()); }
        }

        private Client EnterpriseClient
        {
            get { return enterpriseClient ?? (enterpriseClient = TestConfiguration.CreateEnterpriseClient()); }
        }

        #region Enterprise

        [Test]
        public void CanGetSingleEnterpriseUser()
        {
            var result = EnterpriseClient.GetEnterpriseUser(SomeEnterpriseUserIds(1).First());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Memberships);
            Assert.IsNotNull(result.CustomFields);
            Assert.IsNotNull(result.Qualifications);
            Assert.IsNotNull(result.BackgroundCheckResults);
        }

        [Test]
        public void CanListEnterpriseUsers()
        {
            var result = EnterpriseClient.ListEnterpriseUsers();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListEnterpriseUsersWithFilters()
        {
            var result = EnterpriseClient.ListEnterpriseUsers(new UsersFilterModelEnterprise() { PageNumber = 0, PageSize = 10, IncludeCustomFields = true, IncludeMemberships = true, IncludeQualifications = true, IncludeVerifiedVolunteersBackgroundCheckResults = true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListEnterpriseUsersByIdList()
        {
            var result = EnterpriseClient.GetEnterpriseUsersByIdList(SomeEnterpriseUserIds(3));

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListEnterpriseTimelogEntries()
        {
            var result = EnterpriseClient.ListEnterpriseTimelogEntries();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanListEnterpriseTimelogEntriesWithFilter()
        {
            //filter by people we already know have entries, otherwise there is nothing to assert on.
            var userIds = SomeEnterpriseTimelogEntries(5).Select(entry => entry.UserId).Distinct().ToList();

            var result = EnterpriseClient.ListEnterpriseTimelogEntries(new TimelogFilterModelEnterprise() { FilterUserIds = userIds, IncludeRecordedFeedbackFields = true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
            Assert.IsTrue(result.TimelogEntries.All(entry => userIds.Contains(entry.UserId)), "The user id filter let somebody else through.");
        }

        [Test]
        public void CanGetSingleEnterpriseTimelogEntry()
        {
            var result = EnterpriseClient.GetEnterpriseTimelogEntry(SomeEnterpriseTimelogEntries(1).First().TimelogEntryId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void CanGetEnterpriseTimelogEntriesByIdList()
        {
            var result = EnterpriseClient.GetEnterpriseTimelogEntriesByIdList(SomeEnterpriseTimelogEntries(3).Select(entry => entry.TimelogEntryId).ToList());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanLookupEnterpriseOrganizations()
        {
            var result = EnterpriseClient.LookupEnterpriseOrganizations();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Organizations);
            Assert.IsNotEmpty(result.Organizations);
        }

        [Test]
        public void CanLookupEnterpriseActivityReportGroups()
        {
            var result = EnterpriseClient.LookupEnterpriseActivityReportGroups();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActivityReportGroups);
            Assert.IsNotEmpty(result.ActivityReportGroups);
        }

        [Test]
        public void CanLookupEnterpriseQualifications()
        {
            var result = EnterpriseClient.LookupEnterpriseQualifications();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Qualifications);
            Assert.IsNotEmpty(result.Qualifications);
        }

        [Test]
        public void CanLookupEnterpriseFeedbackFields()
        {
            var result = EnterpriseClient.LookupEnterpriseFeedbackFields();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.FeedbackFields);
            Assert.IsNotEmpty(result.FeedbackFields);
        }

        [Test]
        public void CanLookupEnterpriseCustomFields()
        {
            var result = EnterpriseClient.LookupEnterpriseCustomFields();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CustomFields);
            Assert.IsNotEmpty(result.CustomFields);
        }

        [Test]
        public void CanDownloadEnterpriseUserTimeClockQRCode()
        {
            var user = EnterpriseClient.ListEnterpriseUsers(new UsersFilterModelEnterprise() { PageSize = 25 }).Users.FirstOrDefault(u => !string.IsNullOrEmpty(u.TimeClockQRCodeUrl));

            //single sign on users do not get a qr code, so an account that only uses sso has nothing to test with.
            if (user == null)
            {
                Assert.Ignore("None of the users on the first page have a time clock qr code.");
            }

            //the qr code response does not name the file, so a complete path has to be given.
            var savePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("n") + ".png");

            try
            {
                var result = EnterpriseClient.DownloadUserTimeClockQRCode(user, savePath);

                Assert.AreEqual(savePath, result);
                Assert.IsTrue(File.Exists(result));
                Assert.Greater(new FileInfo(result).Length, 0);
            }
            finally
            {
                File.Delete(savePath);
            }
        }

        #endregion

        #region Organization

        [Test]
        public void CanGetSingleOrganizationUser()
        {
            var result = OrganizationClient.GetOrganizationUser(SomeOrganizationUserIds(1).First());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Memberships);
            Assert.IsNotNull(result.CustomFields);
            Assert.IsNotNull(result.Qualifications);
            Assert.IsNotNull(result.BackgroundCheckResults);
        }

        [Test]
        public void CanListOrganizationUsers()
        {
            var result = OrganizationClient.ListOrganizationUsers();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListOrganizationUsersWithFilters()
        {
            var result = OrganizationClient.ListOrganizationUsers(new UsersFilterModelOrganization() { PageNumber = 0, PageSize = 10, IncludeCustomFields = true, IncludeMemberships = true, IncludeQualifications = true, IncludeVerifiedVolunteersBackgroundCheckResults = true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListOrganizationUsersByIdList()
        {
            var result = OrganizationClient.GetOrganizationUsersByIdList(SomeOrganizationUserIds(3));

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Users);
            Assert.IsNotEmpty(result.Users);
        }

        [Test]
        public void CanListOrganizationTimelogEntries()
        {
            var result = OrganizationClient.ListOrganizationTimelogEntries();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanListOrganizationTimelogEntriesWithFilter()
        {
            //filter by people we already know have entries, otherwise there is nothing to assert on.
            var userIds = SomeOrganizationTimelogEntries(5).Select(entry => entry.UserId).Distinct().ToList();

            var result = OrganizationClient.ListOrganizationTimelogEntries(new TimelogFilterModelOrganization() { FilterUserIds = userIds, IncludeRecordedFeedbackFields = true });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
            Assert.IsTrue(result.TimelogEntries.All(entry => userIds.Contains(entry.UserId)), "The user id filter let somebody else through.");
        }

        [Test]
        public void CanGetSingleOrganizationTimelogEntry()
        {
            var result = OrganizationClient.GetOrganizationTimelogEntry(SomeOrganizationTimelogEntries(1).First().TimelogEntryId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void CanGetOrganizationTimelogEntriesByIdList()
        {
            var result = OrganizationClient.GetOrganizationTimelogEntriesByIdList(SomeOrganizationTimelogEntries(3).Select(entry => entry.TimelogEntryId).ToList());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TimelogEntries);
            Assert.IsNotEmpty(result.TimelogEntries);
        }

        [Test]
        public void CanLookupOrganizationActivityCategories()
        {
            var result = OrganizationClient.LookupOrganizationActivityCategories();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ActivityCategories);
            Assert.IsNotEmpty(result.ActivityCategories);
        }

        [Test]
        public void CanLookupOrganizationQualifications()
        {
            var result = OrganizationClient.LookupOrganizationQualifications();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Qualifications);
            Assert.IsNotEmpty(result.Qualifications);
        }

        [Test]
        public void CanLookupOrganizationFeedbackFields()
        {
            var result = OrganizationClient.LookupOrganizationFeedbackFields();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.FeedbackFields);
            Assert.IsNotEmpty(result.FeedbackFields);
        }

        [Test]
        public void CanLookupOrganizationCustomFields()
        {
            var result = OrganizationClient.LookupOrganizationCustomFields();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CustomFields);
            Assert.IsNotEmpty(result.CustomFields);
        }

        [Test]
        public void CanDownloadOrganizationUserTimeClockQRCode()
        {
            var user = OrganizationClient.ListOrganizationUsers(new UsersFilterModelOrganization() { PageSize = 25 }).Users.FirstOrDefault(u => !string.IsNullOrEmpty(u.TimeClockQRCodeUrl));

            //single sign on users do not get a qr code, so an account that only uses sso has nothing to test with.
            if (user == null)
            {
                Assert.Ignore("None of the users on the first page have a time clock qr code.");
            }

            //the qr code response does not name the file, so a complete path has to be given.
            var savePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("n") + ".png");

            try
            {
                var result = OrganizationClient.DownloadUserTimeClockQRCode(user, savePath);

                Assert.AreEqual(savePath, result);
                Assert.IsTrue(File.Exists(result));
                Assert.Greater(new FileInfo(result).Length, 0);
            }
            finally
            {
                File.Delete(savePath);
            }
        }

        #endregion

        #region Finding something in the account to test with

        private List<int> SomeOrganizationUserIds(int howMany)
        {
            var users = OrganizationClient.ListOrganizationUsers(BareUserList(howMany)).Users;

            if (!users.Any())
            {
                Assert.Ignore("The organization has no users to test with.");
            }

            return users.Select(user => user.UserId).ToList();
        }

        private List<int> SomeEnterpriseUserIds(int howMany)
        {
            var users = EnterpriseClient.ListEnterpriseUsers(BareEnterpriseUserList(howMany)).Users;

            if (!users.Any())
            {
                Assert.Ignore("The enterprise has no users to test with.");
            }

            return users.Select(user => user.UserId).ToList();
        }

        private List<Timelog> SomeOrganizationTimelogEntries(int howMany)
        {
            var entries = OrganizationClient.ListOrganizationTimelogEntries(new TimelogFilterModelOrganization() { PageSize = howMany }).TimelogEntries;

            if (!entries.Any())
            {
                Assert.Ignore("The organization has no timelog entries to test with.");
            }

            return entries.ToList();
        }

        private List<Timelog> SomeEnterpriseTimelogEntries(int howMany)
        {
            var entries = EnterpriseClient.ListEnterpriseTimelogEntries(new TimelogFilterModelEnterprise() { PageSize = howMany }).TimelogEntries;

            if (!entries.Any())
            {
                Assert.Ignore("The enterprise has no timelog entries to test with.");
            }

            return entries.ToList();
        }

        private static UsersFilterModelOrganization BareUserList(int howMany)
        {
            //we only want ids here, so leave the expensive parts of the user record out.
            return new UsersFilterModelOrganization()
            {
                PageSize = howMany,
                IncludeCustomFields = false,
                IncludeMemberships = false,
                IncludeQualifications = false,
                IncludeVerifiedVolunteersBackgroundCheckResults = false
            };
        }

        private static UsersFilterModelEnterprise BareEnterpriseUserList(int howMany)
        {
            return new UsersFilterModelEnterprise()
            {
                PageSize = howMany,
                IncludeCustomFields = false,
                IncludeMemberships = false,
                IncludeQualifications = false,
                IncludeVerifiedVolunteersBackgroundCheckResults = false
            };
        }

        #endregion
    }
}
