using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolunteerSquared.ApiClient;
using VolunteerSquared.ApiClient.Models;

namespace ExampleEnterpriseConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initialize API client. Base URL shouldnt change, you will have to insert your own keys.
            var client = new ApiClient("https://api.betterimpact.com/", "YOUR_API_USERNAME_HERE", "YOUR_API_PASSWORD_HERE");

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
            var resultingCustomFieldFileName = client.DownloadFileUserCustomField(singleUser.CustomFields[12345] as UserCustomFieldFile, @"c:\fileCustomFields\");
        }
    }
}
