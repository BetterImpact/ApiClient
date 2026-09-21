using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VolunteerSquared.ApiClient;
using VolunteerSquared.ApiClient.Models;

namespace ExampleOrganizationConsumer
{
    /// <summary>
    /// Worked examples of the jobs people most often want to automate against the organization api. Each one takes a
    /// filter so you decide which people it runs for, and each one writes what it is doing to the console.
    /// </summary>
    static class Examples
    {
        //the api will not hand out more than 250 users or timelog entries at a time, and asking for the largest page
        //we are allowed keeps the number of round trips down.
        private const int MaxPageSize = 250;

        //the user ids we filter timelog entries by end up on the query string, so ask about a sensible number of
        //people at a time instead of building one enormous url.
        private const int UsersPerTimelogRequest = 100;

        private static readonly char[] InvalidFileNameCharacters = Path.GetInvalidFileNameChars();

        #region Example 1: download the custom field files for a group of users

        /// <summary>
        /// Downloads every file and signed document custom field belonging to a group of users, into one folder per
        /// user.
        /// </summary>
        /// <param name="whichUsers">Decides who is in the group, for example every accepted volunteer.</param>
        public static void DownloadCustomFieldFilesForAGroupOfUsers(Client client, UsersFilterModelOrganization whichUsers, string destinationFolder)
        {
            //the download urls hang off the custom fields, so they have to be part of the response.
            whichUsers.IncludeCustomFields = true;

            Directory.CreateDirectory(destinationFolder);

            var usersWithFiles = 0;
            var filesDownloaded = 0;

            foreach (var user in EnumerateUsers(client, whichUsers))
            {
                if (user.CustomFields == null)
                {
                    continue;
                }

                //file and signed document custom fields are only sent for users who actually have something
                //uploaded, so anything that turns up here is something we can download.
                var files = user.CustomFields.OfType<UserCustomFieldFile>().ToList();
                var signedDocuments = user.CustomFields.OfType<UserCustomFieldSignedDocument>().ToList();

                if (!files.Any() && !signedDocuments.Any())
                {
                    continue;
                }

                usersWithFiles++;

                //a folder per user keeps identically named uploads from different people apart. the user id goes in
                //the folder name because two volunteers can easily share a name.
                var userFolder = Path.Combine(destinationFolder, MakeFileNameSafe(string.Format("{0} ({1})", DisplayNameFor(user), user.UserId)));

                Directory.CreateDirectory(userFolder);

                Console.WriteLine("{0} ({1}):", DisplayNameFor(user), user.UserId);

                foreach (var file in files)
                {
                    //handing the client a folder rather than a file name lets it keep the name the file was uploaded
                    //with. if that name is already taken it adds a counter instead of overwriting anything.
                    if (TryDownload(() => client.DownloadFileUserCustomField(file, userFolder), file.CustomFieldName))
                    {
                        filesDownloaded++;
                    }
                }

                foreach (var signedDocument in signedDocuments)
                {
                    if (TryDownload(() => client.DownloadSignedDocumentUserCustomField(signedDocument, userFolder), signedDocument.CustomFieldName))
                    {
                        filesDownloaded++;
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Downloaded {0} file(s) for {1} user(s) into {2}.", filesDownloaded, usersWithFiles, destinationFolder);
        }

        #endregion

        #region Example 2: download time clock qr codes named after the volunteer

        /// <summary>
        /// Downloads the time clock qr code of everyone in the group, saving each one as first-lastname.png.
        /// </summary>
        public static void DownloadTimeClockQRCodes(Client client, UsersFilterModelOrganization whichUsers, string destinationFolder)
        {
            //none of the extras are needed here, and leaving them out makes each page considerably smaller.
            whichUsers.IncludeCustomFields = false;
            whichUsers.IncludeQualifications = false;
            whichUsers.IncludeMemberships = false;
            whichUsers.IncludeVerifiedVolunteersBackgroundCheckResults = false;

            Directory.CreateDirectory(destinationFolder);

            //two volunteers really can be called the same thing, so remember the names we have used and add a counter
            //when one repeats rather than quietly overwriting the first person's code.
            var namesAlreadyUsed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var downloaded = 0;
            var skipped = 0;

            foreach (var user in EnumerateUsers(client, whichUsers))
            {
                if (string.IsNullOrEmpty(user.TimeClockQRCodeUrl))
                {
                    //single sign on profiles cannot use a qr code to sign in to the time clock, so the api does not
                    //give us a url for them.
                    Console.WriteLine("  no qr code for {0} ({1})", DisplayNameFor(user), user.UserId);

                    skipped++;

                    continue;
                }

                //the qr code endpoint does not tell us what the image is called, so we pass a complete file path.
                var fileName = MakeUnique(namesAlreadyUsed, FileNameFor(user)) + ".png";

                Console.WriteLine("  saved {0}", client.DownloadUserTimeClockQRCode(user, Path.Combine(destinationFolder, fileName)));

                downloaded++;
            }

            Console.WriteLine();
            Console.WriteLine("Downloaded {0} qr code(s) into {1}, skipped {2} user(s) without one.", downloaded, destinationFolder, skipped);
        }

        #endregion

        #region Example 3: list users together with the hours they worked

        /// <summary>
        /// Lists a group of users with the hours they worked in a date range, by paging through the users and then
        /// asking for the timelog entries of those specific people.
        /// </summary>
        public static void ListUsersAndTheirHours(Client client, UsersFilterModelOrganization whichUsers, DateTime workedFrom, DateTime workedTo)
        {
            //the membership carries the running hours total the system keeps for each volunteer, which is worth
            //having next to the total we are about to add up ourselves.
            whichUsers.IncludeMemberships = true;
            whichUsers.IncludeCustomFields = false;
            whichUsers.IncludeQualifications = false;
            whichUsers.IncludeVerifiedVolunteersBackgroundCheckResults = false;

            //page through the users first, so we know who we are reporting on.
            var users = EnumerateUsers(client, whichUsers).ToList();

            var hoursInRange = users.ToDictionary(user => user.UserId, user => 0d);
            var entriesInRange = users.ToDictionary(user => user.UserId, user => 0);

            //rather than pulling every timelog entry in the account and throwing most of them away, ask for the
            //entries of these specific people, worked inside this specific date range.
            foreach (var batchOfUserIds in InBatchesOf(users.Select(user => user.UserId), UsersPerTimelogRequest))
            {
                var whichEntries = new TimelogFilterModelOrganization
                {
                    FilterUserIds = batchOfUserIds,
                    WorkedFrom = workedFrom,
                    WorkedTo = workedTo,
                    FilterApprovedStatus = ApprovedStatus.ApprovedOnly,
                    IncludeRecordedFeedbackFields = false
                };

                foreach (var entry in EnumerateTimelogEntries(client, whichEntries))
                {
                    //entries can only come back for the users we asked about, but a profile that changed while we
                    //were paging is not worth crashing over.
                    if (!hoursInRange.ContainsKey(entry.UserId))
                    {
                        continue;
                    }

                    hoursInRange[entry.UserId] += entry.HoursWorked;
                    entriesInRange[entry.UserId]++;
                }
            }

            Console.WriteLine();
            Console.WriteLine("Approved hours worked between {0:d} and {1:d}", workedFrom, workedTo);
            Console.WriteLine();
            Console.WriteLine("{0,-32} {1,9} {2,8} {3,15}", "Name", "Hours", "Entries", "Lifetime hours");
            Console.WriteLine(new string('-', 67));

            foreach (var user in users.OrderByDescending(user => hoursInRange[user.UserId]).ThenBy(user => user.LastName))
            {
                Console.WriteLine("{0,-32} {1,9:0.00} {2,8} {3,15:0.00}",
                    Truncate(DisplayNameFor(user), 32),
                    hoursInRange[user.UserId],
                    entriesInRange[user.UserId],
                    LifetimeHoursFor(user));
            }

            Console.WriteLine(new string('-', 67));
            Console.WriteLine("{0,-32} {1,9:0.00} {2,8}", string.Format("{0} user(s)", users.Count), hoursInRange.Values.Sum(), entriesInRange.Values.Sum());
        }

        #endregion

        #region Paging

        /// <summary>
        /// Walks every page of a user list and hands the users back one at a time.
        /// </summary>
        /// <remarks>
        /// Page numbers are zero based. Every page comes with a header that says whether there is another one, and
        /// asking for a page past the end is an error rather than an empty page, so has_next_page is what decides
        /// when to stop. The filter passed in is reused for every page, only the paging fields change.
        /// </remarks>
        private static IEnumerable<User> EnumerateUsers(Client client, UsersFilterModelOrganization whichUsers)
        {
            whichUsers.PageSize = MaxPageSize;
            whichUsers.PageNumber = 0;

            while (true)
            {
                var page = client.ListOrganizationUsers(whichUsers);

                Console.WriteLine("Users page {0} of {1} ({2} user(s) in total).", page.Header.PageNumber + 1, page.Header.PageCount, page.Header.TotalItemCount);

                foreach (var user in page.Users)
                {
                    yield return user;
                }

                if (!page.Header.HasNextPage)
                {
                    yield break;
                }

                whichUsers.PageNumber++;
            }
        }

        /// <summary>
        /// The same walk, over the pages of a timelog entry list.
        /// </summary>
        private static IEnumerable<Timelog> EnumerateTimelogEntries(Client client, TimelogFilterModelOrganization whichEntries)
        {
            whichEntries.PageSize = MaxPageSize;
            whichEntries.PageNumber = 0;

            while (true)
            {
                var page = client.ListOrganizationTimelogEntries(whichEntries);

                Console.WriteLine("Timelog page {0} of {1} ({2} entries in total).", page.Header.PageNumber + 1, page.Header.PageCount, page.Header.TotalItemCount);

                foreach (var entry in page.TimelogEntries)
                {
                    yield return entry;
                }

                if (!page.Header.HasNextPage)
                {
                    yield break;
                }

                whichEntries.PageNumber++;
            }
        }

        #endregion

        #region Odds and ends

        private static bool TryDownload(Func<string> download, string description)
        {
            try
            {
                Console.WriteLine("  saved {0} to {1}", description, download());

                return true;
            }
            catch (ApiException ex)
            {
                //one file we cannot have is no reason to abandon the rest of the run.
                Console.WriteLine("  could not download {0}: {1}", description, ex.Message);

                return false;
            }
        }

        private static IEnumerable<List<T>> InBatchesOf<T>(IEnumerable<T> items, int batchSize)
        {
            var batch = new List<T>(batchSize);

            foreach (var item in items)
            {
                batch.Add(item);

                if (batch.Count == batchSize)
                {
                    yield return batch;

                    batch = new List<T>(batchSize);
                }
            }

            if (batch.Any())
            {
                yield return batch;
            }
        }

        private static double LifetimeHoursFor(User user)
        {
            return user.Memberships == null ? 0d : user.Memberships.Sum(membership => membership.VolunteerTotalHours);
        }

        private static string DisplayNameFor(User user)
        {
            if (user.IsGroup && !string.IsNullOrEmpty(user.GroupName))
            {
                return user.GroupName;
            }

            var name = string.Join(" ", new[] { user.FirstName, user.LastName }.Where(part => !string.IsNullOrEmpty(part)));

            return name.Length == 0 ? string.Format("user {0}", user.UserId) : name;
        }

        /// <summary>
        /// first-lastname, with anything a file system would object to taken out.
        /// </summary>
        private static string FileNameFor(User user)
        {
            var name = user.IsGroup && !string.IsNullOrEmpty(user.GroupName)
                ? user.GroupName
                : string.Join("-", new[] { user.FirstName, user.LastName }.Where(part => !string.IsNullOrEmpty(part)));

            name = MakeFileNameSafe(name);

            //a profile with no usable name still needs somewhere to go.
            return name.Length == 0 ? string.Format("user-{0}", user.UserId) : name;
        }

        private static string MakeFileNameSafe(string name)
        {
            var safe = new string((name ?? string.Empty).Where(character => Array.IndexOf(InvalidFileNameCharacters, character) < 0).ToArray());

            return safe.Trim(' ', '.');
        }

        private static string MakeUnique(HashSet<string> namesAlreadyUsed, string name)
        {
            var candidate = name;
            var counter = 1;

            while (!namesAlreadyUsed.Add(candidate))
            {
                counter++;

                candidate = string.Format("{0} ({1})", name, counter);
            }

            return candidate;
        }

        private static string Truncate(string value, int maximumLength)
        {
            return value.Length <= maximumLength ? value : value.Substring(0, maximumLength - 1) + "...";
        }

        #endregion
    }
}
