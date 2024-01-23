using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Script.Serialization;

namespace Muneris
{
    public static class Program
    {
        private static string Serialize(object input) => new JavaScriptSerializer().Serialize(input);

        public static void Main()
        {
            //SimphonyTransactionServicesGen2ApiTest();
            //BusinessIntelligenceApiTest();
            //ConfigurationAndContentApiTest();

            Console.ReadKey(true);
        }

        static void SimphonyTransactionServicesGen2ApiTest()
        {
            // Review the online documentation for more information
            // https://docs.oracle.com/en/industries/food-beverage/simphony/omsstsg2api


            var username = "";
            var password = "";
            var clientId = "";
            var orgName = "";
            var locRef = "";
            var rvcRef = "";

            var authBaseUrl = "https://mte4-ohra-idm.oracleindustry.com";
            var apiBaseUrl = "https://mte4-sts.oraclecloud.com";

            var authInfo = OracleAuthHelper.GetAuthInfo(authBaseUrl, clientId, username, password, orgName);

            //StsApi_GetOrganizations(apiUrl, authInfo.AccessToken);
            //StsApi_GetLocations(apiBaseUrl, authInfo.AccessToken, orgName);
            //StsApi_GetRevenueCenters(apiUrl, authInfo.AccessToken, orgName, locRef);

            //StsApi_GetTenders(apiUrl, authInfo.AccessToken, orgName, locRef, rvcRef);
            //StsApi_GetMenuSummary(apiBaseUrl, authInfo.AccessToken, orgName, locRef, rvcRef);
            //StsApi_GetMenuDetails(apiBaseUrl, authInfo.AccessToken, orgName, locRef, rvcRef);

            //StsApi_PostCalculateTotals(apiUrl, authInfo.AccessToken, orgName, locRef, rvcRef);
            //StsApi_PostNewOrder(apiUrl, authInfo.AccessToken, orgName, locRef, rvcRef);
            //StsApi_PostNewRound(apiUrl, authInfo.AccessToken, orgName, locRef, rvcRef, "7475cf17fd2644a182d339bca4cea25000037113");

            //StsApi_GetChecks(apiUrl, authInfo.AccessToken, orgName, locRef, rvcRef);
            //StsApi_GetCheck(apiUrl, authInfo.AccessToken, orgName, locRef, rvcRef, "7475cf17fd2644a182d339bca4cea25000037113");
            //StsApi_GetCheckPrint(apiUrl, authInfo.AccessToken, orgName, locRef, rvcRef, "7475cf17fd2644a182d339bca4cea25000037113");

            //StsApi_NotificationsDeleteSubscriptions(apiUrl, authInfo.AccessToken);
            //StsApi_NotificationsCreateSubscription(apiUrl, authInfo.AccessToken, orgName);
            //StsApi_NotificationsCreateSubscription(apiUrl, authInfo.AccessToken, orgName, locRef, rvcRef);
            //StsApi_NotificationsGetSubscriptions(apiUrl, authInfo.AccessToken);
            //StsApi_NotificationsMessageTypes(apiUrl, authInfo.AccessToken);
            //StsApi_NotificationsDeleteSubscriber(apiUrl, authInfo.AccessToken);
            //StsApi_NotificationsCreateSubscriber(apiUrl, authInfo.AccessToken);
        }

        static void BusinessIntelligenceApiTest()
        {
            // Review the online documentation for more information
            // https://docs.oracle.com/en/industries/food-beverage/back-office/20.1/biapi

            var username = "";
            var password = "";
            var clientId = "";
            var orgName = "";

            var authBaseUrl = "";
            var apiBaseUrl = "";

            var authInfo = OracleAuthHelper.GetAuthInfo(authBaseUrl, clientId, username, password, orgName);

            BiApi_GetLocations(apiBaseUrl, authInfo.AccessToken, orgName);
        }

        static void ConfigurationAndContentApiTest()
        {
            // Review the online documentation for more information
            // https://docs.oracle.com/en/industries/food-beverage/simphony/ccapi

            var username = "";
            var password = "";
            var clientId = "";
            var orgShortName = "";

            var authBaseUrl = "";
            var apiBaseUrl = "";

            var authInfo = OracleAuthHelper.GetAuthInfo(authBaseUrl, clientId, username, password, orgShortName);

            CcApi_GetHierarchy(apiBaseUrl, authInfo.AccessToken);
        }



        private static void StsApi_NotificationsDeleteSubscriptions(string apiBaseUrl, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var url = $"{apiBaseUrl}/api/v1/notifications/subscriptions";
            var data = client.DeleteAsync(url).GetAwaiter().GetResult();
        }
        private static void StsApi_NotificationsGetSubscriptions(string apiBaseUrl, string accessToken)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");

            var url = $"{apiBaseUrl}/api/v1/notifications/subscriptions";
            var data = client.DownloadString(url);
            Console.WriteLine(data);
        }
        private static void StsApi_NotificationsCreateSubscription(string apiBaseUrl, string accessToken, string orgName)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            var body = new
            {
                callbackUri = "https://ent3wqoqbegii.x.pipedream.net",
                messageType = new { id = "CheckNotification" },
                postOfficeOptions = new { PostOfficeType = "PushOnePostOffice" },
                orgShortName = orgName,
            };

            var content = Serialize(body);
            var url = $"{apiBaseUrl}/api/v1/notifications/subscriptions";
            var data = client.UploadString(url, content);
        }
        private static void StsApi_NotificationsCreateSubscription(string apiBaseUrl, string accessToken, string orgName, string locRef, string rvcRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            var body = new
            {
                callbackUri = $"https://ent3wqoqbegii.x.pipedream.net/{orgName}/{locRef}/{rvcRef}",
                messageType = new { id = "CheckNotification" },
                postOfficeOptions = new { PostOfficeType = "PushOnePostOffice" },
                orgShortName = orgName,
                locRef = locRef,
                rvcRef = rvcRef
            };

            var content = Serialize(body);
            var url = $"{apiBaseUrl}/api/v1/notifications/subscriptions";
            var data = client.UploadString(url, content);
        }
        private static void StsApi_NotificationsDeleteSubscriber(string apiBaseUrl, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var url = $"{apiBaseUrl}/api/v1/notifications/registration";
            var data = client.DeleteAsync(url).GetAwaiter().GetResult();
        }
        private static void StsApi_NotificationsCreateSubscriber(string apiBaseUrl, string accessToken)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            var key = Convert.ToBase64String(new HMACSHA256().Key); // Convert HMAC key to Base64 for use in HTTP body

            var body = new
            {
                hmacKey = key,
                keyId = Guid.NewGuid(),
                keyType = "hmac-sha256",
            };

            var content = Serialize(body);
            var url = $"{apiBaseUrl}/api/v1/notifications/registration";
            var data = client.UploadString(url, "PUT", content);
        }
        private static void StsApi_NotificationsMessageTypes(string apiBaseUrl, string accessToken)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");

            var url = $"{apiBaseUrl}/api/v1/notifications/discovery";
            var data = client.DownloadString(url);
            Console.WriteLine(data);
        }

        private static void StsApi_PostNewRound(string apiBaseUrl, string accessToken, string orgName, string locRef, string rvcRef, string checkRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("Simphony-OrgShortName", orgName);
            client.Headers.Add("Simphony-LocRef", locRef);
            client.Headers.Add("Simphony-RvcRef", rvcRef);

            var body = new
            {
                Header = new
                {
                    idempotencyId = Guid.NewGuid().ToString("N"),

                    checkEmployeeRef = 999,
                    orderTypeRef = 1,

                    orgShortName = orgName,
                    locRef = locRef,
                    rvcRef = rvcRef,
                    checkRef = checkRef,
                },

                menuItems = new[]
                {
                    new
                    {
                        menuItemId = 10100002,
                    }
                },

                tenders = new[]
                {
                    new
                    {
                        tenderId = 1001,
                    }
                }
            };


            var content = Serialize(body);
            var url = $"{apiBaseUrl}/api/v1/checks/{checkRef}/round";
            var data = client.UploadString(url, content);
        }
        private static void StsApi_PostNewOrder(string apiBaseUrl, string accessToken, string orgName, string locRef, string rvcRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("Simphony-OrgShortName", orgName);
            client.Headers.Add("Simphony-LocRef", locRef);
            client.Headers.Add("Simphony-RvcRef", rvcRef);

            var body = new
            {
                Header = new
                {
                    idempotencyId = Guid.NewGuid().ToString("N"),

                    checkEmployeeRef = 999,
                    orderTypeRef = 1,

                    orgShortName = orgName,
                    locRef = locRef,
                    rvcRef = rvcRef,
                },

                menuItems = new[]
                {
                    new
                    {
                        menuItemId = 10100001,
                    }
                },

                tenders = new[]
                {
                    new
                    {
                        tenderId = 1001,
                    }
                }
            };


            var content = Serialize(body);
            var url = $"{apiBaseUrl}/api/v1/checks";
            var data = client.UploadString(url, content);
        }
        private static void StsApi_PostCalculateTotals(string apiBaseUrl, string accessToken, string orgName, string locRef, string rvcRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            client.Headers.Add("Simphony-OrgShortName", orgName);
            client.Headers.Add("Simphony-LocRef", locRef);
            client.Headers.Add("Simphony-RvcRef", rvcRef);

            var body = new
            {
                Header = new
                {
                    idempotencyId = Guid.NewGuid().ToString("N"),

                    checkEmployeeRef = 999,
                    orderTypeRef = 1,

                    orgShortName = orgName,
                    locRef = locRef,
                    rvcRef = rvcRef,
                },

                menuItems = new[]
                {
                    new
                    {
                        menuItemId = 10100001,
                    }
                }
            };


            var content = Serialize(body);
            var url = $"{apiBaseUrl}/api/v1/checks/calculator";
            var data = client.UploadString(url, content);
        }

        private static void StsApi_GetCheck(string apiBaseUrl, string accessToken, string orgName, string lofRef, string rvcRef, string checkRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add("Simphony-OrgShortName", orgName);
            client.Headers.Add("Simphony-LocRef", lofRef);
            client.Headers.Add("Simphony-RvcRef", rvcRef);

            var url = $"{apiBaseUrl}/api/v1/checks/{checkRef}";
            var data = client.DownloadString(url);
        }
        private static void StsApi_GetCheckPrint(string apiBaseUrl, string accessToken, string orgName, string lofRef, string rvcRef, string checkRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add("Simphony-OrgShortName", orgName);
            client.Headers.Add("Simphony-LocRef", lofRef);
            client.Headers.Add("Simphony-RvcRef", rvcRef);

            var url = $"{apiBaseUrl}/api/v1/checks/{checkRef}/printed";
            var data = client.DownloadString(url);
        }
        private static void StsApi_GetChecks(string apiBaseUrl, string accessToken, string orgName, string lofRef, string rvcRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add("Simphony-OrgShortName", orgName);
            client.Headers.Add("Simphony-LocRef", lofRef);
            client.Headers.Add("Simphony-RvcRef", rvcRef);

            var url = $"{apiBaseUrl}/api/v1/checks";
            var data = client.DownloadString(url);
        }
        private static void StsApi_GetTenders(string apiBaseUrl, string accessToken, string orgName, string lofRef, string rvcRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");

            client.QueryString.Add("OrgShortName", orgName);
            client.QueryString.Add("LocRef", lofRef);
            client.QueryString.Add("RvcRef", rvcRef);

            var url = $"{apiBaseUrl}/api/v1/tenders/collection";
            var data = client.DownloadString(url);
            Console.WriteLine(data);
        }
        private static void StsApi_GetMenuSummary(string apiBaseUrl, string accessToken, string orgName, string lofRef, string rvcRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");

            client.QueryString.Add("OrgShortName", orgName);
            client.QueryString.Add("LocRef", lofRef);

            var url = $"{apiBaseUrl}/api/v1/menus/summary";
            var data = client.DownloadString(url);
            Console.WriteLine(data);
        }
        private static void StsApi_GetMenuDetails(string apiBaseUrl, string accessToken, string orgName, string lofRef, string rvcRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");

            client.Headers.Add("Simphony-OrgShortName", orgName);
            client.Headers.Add("Simphony-LocRef", lofRef);
            client.Headers.Add("Simphony-RvcRef", rvcRef);

            var url = $"{apiBaseUrl}/api/v1/menus/{orgName}:{lofRef}:{rvcRef}";
            var data = client.DownloadString(url);
            Console.WriteLine(data);
        }
        private static void StsApi_GetRevenueCenters(string apiBaseUrl, string accessToken, string orgName, string locRef)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            var url = $"{apiBaseUrl}/api/v1/organizations/{orgName}/locations/{locRef}/revenueCenters";
            var data = client.DownloadString(url);
            Console.WriteLine(data);
        }
        private static void StsApi_GetLocations(string apiBaseUrl, string accessToken, string orgName)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            var url = $"{apiBaseUrl}/api/v1/organizations/{orgName}/locations";
            var data = client.DownloadString(url);
            Console.WriteLine(data);
        }
        private static void StsApi_GetOrganizations(string apiBaseUrl, string accessToken)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            var url = $"{apiBaseUrl}/api/v1/organizations";
            var data = client.DownloadString(url);
            Console.WriteLine(data);
        }

        static void BiApi_GetLocations(string apiBaseUrl, string accessToken, string orgName)
        {
            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            var url = $"{apiBaseUrl}/bi/v1/{orgName}/getLocationDimensions";
            var data = client.UploadString(url, "");
            Console.WriteLine(data);
        }

        static void CcApi_GetHierarchy(string apiBaseUrl, string accessToken)
        {
            var body = new
            {
                searchCriteria = "where equals(hierType,RVC)",
                languages = "",
                include = ""
            };

            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add("Content-Type", "application/json");
            var content = Serialize(body);
            var url = $"{apiBaseUrl}/config/sim/v1/hierarchy/getHierarchy";
            var data = client.UploadString(url, content);
            Console.WriteLine(data);
        }
        static void CcApi_GetLocations(string apiBaseUrl, string accessToken)
        {
            var body = new
            {
                searchCriteria = "",
                languages = "",
                include = "",
                offset = 0,
                limit = 100,
                orderby = ""
            };

            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, $"Bearer {accessToken}");
            client.Headers.Add("Content-Type", "application/json");
            var content = Serialize(body);
            var url = $"{apiBaseUrl}/config/sim/v1/hierarchy/getlocations";
            var data = client.UploadString(url, content);
            Console.WriteLine(data);
        }
    }
}
