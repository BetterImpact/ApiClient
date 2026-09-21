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
    /// Tests for the behaviour the worked examples depend on: paging, the query parameters that narrow a request
    /// down, and how downloaded files get their names. These use the organization api, the enterprise side of each
    /// of these shares the same code.
    /// </summary>
    public class ApiClientBehaviourTests
    {
        private Client client;

        private Client Client
        {
            get { return client ?? (client = TestConfiguration.CreateOrganizationClient()); }
        }

        #region Paging

        [Test]
        public void PagingThroughUsersVisitsEveryUserOnce()
        {
            //a page size of one so that even a small account gets paged through properly, and a limit on how many
            //pages we walk so that this stays quick against a large one.
            const int pagesToVisit = 5;

            var whichUsers = BareUserList(1);

            var userIdsSeen = new List<int>();
            var totalItemCount = 0;
            var pagesVisited = 0;

            for (var pageNumber = 0; pageNumber < pagesToVisit; pageNumber++)
            {
                whichUsers.PageNumber = pageNumber;

                var page = Client.ListOrganizationUsers(whichUsers);

                pagesVisited++;
                totalItemCount = page.Header.TotalItemCount;

                //the api is zero based on the way in and on the way out.
                Assert.AreEqual(pageNumber, page.Header.PageNumber, "The header did not come back with the page number that was asked for.");
                Assert.AreEqual(pageNumber == 0, page.Header.IsFirstPage);

                userIdsSeen.AddRange(page.Users.Select(user => user.UserId));

                if (!page.Header.HasNextPage)
                {
                    Assert.IsTrue(page.Header.IsLastPage, "The last page did not say it was the last page.");

                    break;
                }
            }

            if (totalItemCount == 0)
            {
                Assert.Ignore("The organization has no users to page through.");
            }

            Assert.AreEqual(userIdsSeen.Count, userIdsSeen.Distinct().Count(), "The same user came back on more than one page.");
            Assert.AreEqual(Math.Min(totalItemCount, pagesVisited), userIdsSeen.Count, "A page did not hold the number of users it should have.");
        }

        [Test]
        public void PagingPastTheEndIsAnError()
        {
            //this is why the examples stop on has_next_page rather than waiting for an empty page.
            var firstPage = Client.ListOrganizationUsers(BareUserList(1));

            if (firstPage.Header.TotalItemCount == 0)
            {
                Assert.Ignore("The organization has no users to page through.");
            }

            var whichUsers = BareUserList(1);

            whichUsers.PageNumber = firstPage.Header.PageCount + 10;

            Assert.Throws<ApiException>(() => Client.ListOrganizationUsers(whichUsers));
        }

        #endregion

        #region Filters

        [Test]
        public void TheApprovedFilterIsSentToTheApi()
        {
            //the client used to carry this filter without ever putting it on the request.
            var approvedOnly = Client.ListOrganizationTimelogEntries(new TimelogFilterModelOrganization() { PageSize = 50, FilterApprovedStatus = ApprovedStatus.ApprovedOnly });
            var unapprovedOnly = Client.ListOrganizationTimelogEntries(new TimelogFilterModelOrganization() { PageSize = 50, FilterApprovedStatus = ApprovedStatus.UnapprovedOnly });
            var everything = Client.ListOrganizationTimelogEntries(new TimelogFilterModelOrganization() { PageSize = 50, FilterApprovedStatus = ApprovedStatus.DontFilter });

            Assert.IsTrue(approvedOnly.TimelogEntries.All(entry => entry.Approved), "An unapproved entry came back from an approved only request.");
            Assert.IsTrue(unapprovedOnly.TimelogEntries.All(entry => !entry.Approved), "An approved entry came back from an unapproved only request.");

            Assert.AreEqual(everything.Header.TotalItemCount,
                approvedOnly.Header.TotalItemCount + unapprovedOnly.Header.TotalItemCount,
                "Approved plus unapproved should account for every entry.");
        }

        [Test]
        public void TheWorkedDateFilterIsSentToTheApi()
        {
            var workedFrom = DateTime.Today.AddDays(-90);
            var workedTo = DateTime.Today;

            var result = Client.ListOrganizationTimelogEntries(new TimelogFilterModelOrganization() { PageSize = 50, WorkedFrom = workedFrom, WorkedTo = workedTo });

            if (!result.TimelogEntries.Any())
            {
                Assert.Ignore("No timelog entries in the last 90 days to check the date filter against.");
            }

            //a day of grace at each end, because the api works in the account's time zone and we are asking in ours.
            Assert.IsTrue(result.TimelogEntries.All(entry => entry.DateWorked >= workedFrom.AddDays(-1) && entry.DateWorked <= workedTo.AddDays(1)),
                "An entry came back from outside the date range that was asked for.");
        }

        #endregion

        #region Downloads

        [Test]
        public void DownloadingAFileCustomFieldIntoAFolderUsesTheNameFromTheServer()
        {
            var file = FindAFileCustomField(4);

            if (file == null)
            {
                Assert.Ignore("No user near the front of the list has a file custom field uploaded.");
            }

            var folder = CreateTemporaryFolder();

            try
            {
                var result = Client.DownloadFileUserCustomField(file, folder);

                Assert.IsTrue(File.Exists(result), "Nothing was written to disk.");
                Assert.Greater(new FileInfo(result).Length, 0);

                var fileName = Path.GetFileName(result);

                //the name used to come straight out of the Content-Disposition header with its quotes still
                //attached, and windows will not accept those in a file name.
                Assert.IsFalse(fileName.Contains("\""), "The file name still has the quotes from the Content-Disposition header: " + fileName);
                Assert.AreEqual(-1, fileName.IndexOfAny(Path.GetInvalidFileNameChars()), "The file name contains characters that are not legal in one: " + fileName);
                Assert.AreEqual(folder, Path.GetDirectoryName(result), "The file was written somewhere other than the folder it was asked for.");
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        [Test]
        public void DownloadingAQRCodeIntoAFolderFallsBackToADefaultName()
        {
            var user = Client.ListOrganizationUsers(BareUserList(25)).Users.FirstOrDefault(u => !string.IsNullOrEmpty(u.TimeClockQRCodeUrl));

            if (user == null)
            {
                Assert.Ignore("None of the users on the first page have a time clock qr code.");
            }

            var folder = CreateTemporaryFolder();

            try
            {
                //the qr code response has no Content-Disposition header, so the client has to name the file itself.
                var result = Client.DownloadUserTimeClockQRCode(user, folder);

                Assert.AreEqual(string.Format("timeclock-qr-code-{0}.png", user.UserId), Path.GetFileName(result));

                //and what came back should actually be a png.
                CollectionAssert.AreEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, File.ReadAllBytes(result).Take(4).ToArray(), "The downloaded file does not start like a png.");
            }
            finally
            {
                Directory.Delete(folder, true);
            }
        }

        [Test]
        public void AUserWithNoQRCodeIsRefusedRatherThanDownloaded()
        {
            var user = new User() { UserId = 1, TimeClockQRCodeUrl = string.Empty };

            //nothing leaves the machine here, the missing url is caught before a request is built, so this one
            //needs no credentials.
            var offlineClient = new Client("https://example.invalid/", "unused", "unused");

            Assert.Throws<ArgumentException>(() => offlineClient.DownloadUserTimeClockQRCode(user, Path.GetTempPath()));
        }

        #endregion

        #region Finding something in the account to test with

        private UserCustomFieldFile FindAFileCustomField(int pagesToLookThrough)
        {
            var whichUsers = new UsersFilterModelOrganization()
            {
                PageSize = 50,
                IncludeCustomFields = true,
                IncludeMemberships = false,
                IncludeQualifications = false,
                IncludeVerifiedVolunteersBackgroundCheckResults = false
            };

            for (var pageNumber = 0; pageNumber < pagesToLookThrough; pageNumber++)
            {
                whichUsers.PageNumber = pageNumber;

                var page = Client.ListOrganizationUsers(whichUsers);

                var file = page.Users
                    .Where(user => user.CustomFields != null)
                    .SelectMany(user => user.CustomFields)
                    .OfType<UserCustomFieldFile>()
                    .FirstOrDefault(field => !string.IsNullOrEmpty(field.Value));

                if (file != null)
                {
                    return file;
                }

                if (!page.Header.HasNextPage)
                {
                    break;
                }
            }

            return null;
        }

        private static UsersFilterModelOrganization BareUserList(int pageSize)
        {
            return new UsersFilterModelOrganization()
            {
                PageSize = pageSize,
                IncludeCustomFields = false,
                IncludeMemberships = false,
                IncludeQualifications = false,
                IncludeVerifiedVolunteersBackgroundCheckResults = false
            };
        }

        private static string CreateTemporaryFolder()
        {
            var folder = Path.Combine(Path.GetTempPath(), "VolunteerSquaredApiClientTests", Guid.NewGuid().ToString("n"));

            Directory.CreateDirectory(folder);

            return folder;
        }

        #endregion
    }
}
