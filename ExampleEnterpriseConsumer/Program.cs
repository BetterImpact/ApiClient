using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VolunteerSquared.ApiClient;
using VolunteerSquared.ApiClient.Models;

namespace ExampleEnterpriseConsumer
{
    class Program
    {
        //Base URL shouldnt change, you will have to insert your own keys. Setting the VS_API_USERNAME and
        //VS_API_PASSWORD environment variables before running keeps your keys out of source control.
        private static readonly string ApiBaseUrl = Environment.GetEnvironmentVariable("VS_API_BASE_URL") ?? "https://api.betterimpact.com/";
        private static readonly string ApiUsername = Environment.GetEnvironmentVariable("VS_API_USERNAME") ?? "YOUR_API_USERNAME_HERE";
        private static readonly string ApiPassword = Environment.GetEnvironmentVariable("VS_API_PASSWORD") ?? "YOUR_API_PASSWORD_HERE";

        //Where the examples put the files they download.
        private static readonly string DownloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "VolunteerSquaredApiExamples");

        //Which organizations the examples look at. Leave it empty for the whole enterprise, or use
        //LookupEnterpriseOrganizations to find the ids you want.
        private static readonly List<int> OrganizationIds = new List<int>();

        static void Main(string[] args)
        {
            //Initialize API client.
            var client = new Client(ApiBaseUrl, ApiUsername, ApiPassword);

            //Each of these is a complete job rather than a single call. They are built out of the individual calls
            //shown in TheBasics below. Run the ones you are interested in and comment out the rest.
            //Every one of them is handed its own filter, because they page through it and turn off the parts of the
            //user record they do not need.

            //1. Download every file and signed document custom field belonging to a group of users.
            Examples.DownloadCustomFieldFilesForAGroupOfUsers(client, AcceptedVolunteers(), Path.Combine(DownloadFolder, "CustomFieldFiles"));

            //2. Download the time clock qr code of each of those volunteers, named first-lastname.png.
            Examples.DownloadTimeClockQRCodes(client, AcceptedVolunteers(), Path.Combine(DownloadFolder, "QRCodes"));

            //3. List those volunteers together with the hours they have worked so far this year.
            Examples.ListUsersAndTheirHours(client, AcceptedVolunteers(), new DateTime(DateTime.Today.Year, 1, 1), DateTime.Today, OrganizationIds);
        }

        /// <summary>
        /// The group of people the examples above run for. Any of the filters on the model can be used to describe a
        /// different group, and leaving them all alone gives you everybody in the enterprise.
        /// </summary>
        private static UsersFilterModelEnterprise AcceptedVolunteers()
        {
            return new UsersFilterModelEnterprise
            {
                OrganizationIds = OrganizationIds,
                VolunteerModule = true,
                VolunteerStatusAccepted = true
            };
        }

        /// <summary>
        /// The individual calls the examples are built out of. The ids here are made up, put your own in before
        /// calling this.
        /// </summary>
        private static void TheBasics(Client client)
        {
            //Get a page of users, further pages can be accessed by changing the appropriate filter in the filter model.
            var users = client.ListEnterpriseUsers(new UsersFilterModelEnterprise() { PageSize = 25 /*you can put more filters in here.*/ });

            //get a single user, by Id
            var singleUser = client.GetEnterpriseUser(12345);

            //download a users photo. The resulting file name is returned (depending on your file system, the file may have to be renamed.)
            //the file path can include a file name as well, or it can be just a path. This library handles both. If you specify a file name and there
            //is a file named the same currently on your hard drive, it will be overwriten. If you do not specify a file name, the library will use the name on our system,
            //and it will rename the file as needed. Both methods will return the final filename that was used. You may also use DownloadUserPhotoScaled to get the scaled photo.
            var resultingFileName = client.DownloadUserPhoto(singleUser, @"c:\photos\");

            //this downloads a file custom field for a user. The same file naming rules apply here as with photos.
            //custom fields come back as a list, so pick the one you want out of it by its custom field id.
            var fileCustomField = singleUser.CustomFields.OfType<UserCustomFieldFile>().First(field => field.CustomFieldId == 12345);
            var resultingCustomFieldFileName = client.DownloadFileUserCustomField(fileCustomField, @"c:\fileCustomFields\");

            //the time clock qr code does not come with a name, so give it a complete file path rather than a folder.
            var resultingQRCodeFileName = client.DownloadUserTimeClockQRCode(singleUser, @"c:\qrcodes\volunteer.png");
        }
    }
}
