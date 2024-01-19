using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Muneris
{
    public class AuthInfo
    {
        public string ClientId { get; set; }
        public long Timestamp { get; set; }
        public string AuthBaseUrl { get; set; }

        public string CodeVerifier { get; set; }
        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
    }

    public static class OracleAuthHelper
    {
        private static string Serialize(object input) => new JavaScriptSerializer().Serialize(input);
        private static T Deserialize<T>(string input) => new JavaScriptSerializer().Deserialize<T>(input);
        private static long CurrentTimestamp() => (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

        public static AuthInfo GetAuthInfo(string authBaseUrl, string clientId, string username, string password, string orgName)
        {
            var authInfo = new AuthInfo
            {
                ClientId = clientId,
                Timestamp = CurrentTimestamp(),
                AuthBaseUrl = authBaseUrl,
                CodeVerifier = GenerateCodeVerifyer()
            };

            var codeChallenge = GenerateCodeChallenge(authInfo.CodeVerifier);
            AuthorizeWithOpenId(authBaseUrl, clientId, codeChallenge);

            var authCode = SignIn(authBaseUrl, clientId, codeChallenge, username, password, orgName);
            var tokenResponse = GetTokens(authBaseUrl, clientId, authInfo.CodeVerifier, authCode);

            authInfo.RefreshToken = tokenResponse.refresh_token;
            authInfo.AccessToken = tokenResponse.access_token;
            authInfo.ExpiresIn = tokenResponse.expires_in;

            return authInfo;
        }

        public static AuthInfo RefreshTokens(AuthInfo authInfo)
        {
            var client = new WebClient();

            var content = new NameValueCollection
            {
                { "client_id", authInfo.ClientId },
                { "code_verifier", authInfo.CodeVerifier },
                { "redirect_uri", "apiaccount://callback" },
                { "scope", "openid" },

                { "grant_type", "refresh_token" },
                { "refresh_token", authInfo.RefreshToken },
            };

            var url = $"{authInfo.AuthBaseUrl}/oidc-provider/v1/oauth2/token";
            var data = client.UploadValues(url, content);
            var response = Deserialize<TokenResponse>(Encoding.UTF8.GetString(data));

            authInfo.Timestamp = CurrentTimestamp();
            authInfo.ExpiresIn = response.expires_in;
            authInfo.RefreshToken = response.refresh_token;
            authInfo.AccessToken = response.access_token;

            return authInfo;
        }

        private static TokenResponse GetTokens(string authBaseUrl, string clientId, string codeVerifier, string authCode)
        {
            var client = new WebClient();

            var content = new NameValueCollection
            {
                { "client_id", clientId },
                { "code_verifier", codeVerifier },
                { "redirect_uri", "apiaccount://callback" },
                { "scope", "openid" },

                { "grant_type", "authorization_code" },
                { "code", authCode },
            };

            var url = $"{authBaseUrl}/oidc-provider/v1/oauth2/token";
            var data = client.UploadValues(url, content);

            return Deserialize<TokenResponse>(Encoding.UTF8.GetString(data));
        }

        private static string SignIn(string authBaseUrl, string clientId, string codeChallenge, string username, string password, string orgName)
        {
            var client = new WebClient();

            var cookies = new Dictionary<string, string>()
            {
                { "client_id", clientId },
                { "code_challenge", codeChallenge },
                { "code_challenge_method", "S256" },
                { "redirect_uri", "apiaccount://callback" },
                { "response_type", "code" },
            };

            client.Headers.Add(HttpRequestHeader.Cookie, string.Join(";", cookies.Select(x => $"{x.Key}={x.Value}")));

            var content = new NameValueCollection
            {
                { "username", username },
                { "password", password },
                { "orgname", orgName },
            };

            var url = $"{authBaseUrl}/oidc-provider/v1/oauth2/signin";
            var data = client.UploadValues(url, content);
            var response = Deserialize<SignInResponse>(Encoding.UTF8.GetString(data));

            if (response.nextOp != "redirect")
                throw new AuthenticationException(response.nextOp);

            var uri = new Uri(response.redirectUrl);
            return HttpUtility.ParseQueryString(uri.Query).Get(0);
        }

        private static void AuthorizeWithOpenId(string authBaseUrl, string clientId, string codeChallenge)
        {
            var client = new WebClient();
            client.QueryString.Add("client_id", clientId);
            client.QueryString.Add("code_challenge", codeChallenge);
            client.QueryString.Add("code_challenge_method", "S256");
            client.QueryString.Add("redirect_uri", "apiaccount://callback");
            client.QueryString.Add("response_type", "code");
            client.QueryString.Add("scope", "openid");

            var url = $"{authBaseUrl}/oidc-provider/v1/oauth2/authorize";
            client.DownloadString(url); // the call returns a html page, which we don't need
        }

        private static string GenerateCodeChallenge(string codeVerifier)
        {
            var verifierBytes = Encoding.ASCII.GetBytes(codeVerifier);
            var hashValue = SHA256.Create().ComputeHash(verifierBytes);
            return Convert.ToBase64String(hashValue);
        }

        private static string GenerateCodeVerifyer()
        {
            var rndBytes = new byte[32];
            new Random().NextBytes(rndBytes);
            return Convert.ToBase64String(rndBytes);
        }

        private class SignInResponse
        {
            // ReSharper disable InconsistentNaming
            public string nextOp { get; set; }
            public bool success { get; set; }
            public string redirectUrl { get; set; }
        }


        private class TokenResponse
        {
            // ReSharper disable InconsistentNaming
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string id_token { get; set; }
            public string refresh_token { get; set; }
        }
    }
}