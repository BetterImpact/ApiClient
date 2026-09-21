using NUnit.Framework;
using System;
using VolunteerSquared.ApiClient;

namespace VolunteerSquared.ApiClientTests
{
    /// <summary>
    /// Where the tests get the api url and credentials from. Nothing is hard coded, so that keys never end up in
    /// source control.
    /// </summary>
    /// <remarks>
    /// Values are looked for in this order:
    ///   1. test run parameters, which come from a runsettings file: dotnet test --settings local.runsettings
    ///   2. environment variables
    /// Copy local.runsettings.example to local.runsettings and fill it in. That file name is gitignored.
    /// When the credentials for a scope are not configured the tests for that scope report themselves as ignored
    /// rather than failing with an authentication error.
    /// </remarks>
    static class TestConfiguration
    {
        public static string ApiBaseUrl
        {
            get { return Read("ApiBaseUrl", "VS_API_BASE_URL") ?? "https://api.betterimpact.com/"; }
        }

        public static Client CreateOrganizationClient()
        {
            return CreateClient("organization", "OrganizationApiUsername", "VS_ORG_API_USERNAME", "OrganizationApiPassword", "VS_ORG_API_PASSWORD");
        }

        public static Client CreateEnterpriseClient()
        {
            return CreateClient("enterprise", "EnterpriseApiUsername", "VS_ENT_API_USERNAME", "EnterpriseApiPassword", "VS_ENT_API_PASSWORD");
        }

        private static Client CreateClient(string scope, string usernameParameter, string usernameVariable, string passwordParameter, string passwordVariable)
        {
            var username = Read(usernameParameter, usernameVariable);
            var password = Read(passwordParameter, passwordVariable);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Assert.Ignore(string.Format("No {0} api credentials configured. Set {1} and {2} in a runsettings file, or the {3} and {4} environment variables.",
                    scope, usernameParameter, passwordParameter, usernameVariable, passwordVariable));
            }

            return new Client(ApiBaseUrl, username, password);
        }

        private static string Read(string testRunParameterName, string environmentVariableName)
        {
            var fromTestRunParameters = TestContext.Parameters.Exists(testRunParameterName)
                ? TestContext.Parameters[testRunParameterName]
                : null;

            if (!string.IsNullOrEmpty(fromTestRunParameters))
            {
                return fromTestRunParameters;
            }

            var fromEnvironment = Environment.GetEnvironmentVariable(environmentVariableName);

            return string.IsNullOrEmpty(fromEnvironment) ? null : fromEnvironment;
        }
    }
}
