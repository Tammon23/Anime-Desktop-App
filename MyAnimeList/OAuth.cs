using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace MyAnimeList
{
    public class OAuth
    {
        private static HttpClient? _oAuthMyAnimeListHttpClient;
        private const string ClientId = "23c477235d19c5349899187d13f5af36";
        private static string? _codeVerifier;
        private static string? _codeChallenge;
        private static AuthorizationResponse? _tokenRefreshInfo;
        
        public static void Init()
        {
            _codeVerifier = GenerateNonce();
            
            // MAL only supports OAuth 2.0 code challenge method plain
            // meaning the code verifier and code challenge should be equal
            _codeChallenge = _codeVerifier; 
            
            if (_oAuthMyAnimeListHttpClient == null)
            {
                _oAuthMyAnimeListHttpClient = HttpClientFactory.Create();
                _oAuthMyAnimeListHttpClient.DefaultRequestHeaders.Accept.Clear();
                _oAuthMyAnimeListHttpClient.BaseAddress = new Uri("https://myanimelist.net/");
            }
        }

        public static void Login()
        {
            
        }

        /// <summary>
        ///     This is the URL required to link a user's account to the app
        ///     Clicking it will open a "MAL Desktop wants to access your MyAnimeList account" page
        ///     After clicking allow the next step will require the current link of the page
        ///     see https://myanimelist.net/blog.php?eid=835707 or
        ///     https://myanimelist.net/apiconfig/references/authorization
        ///     for explanation
        /// </summary>
        public static string GetUserAuthUrl()
        {
            return _oAuthMyAnimeListHttpClient == null 
                ? "Failed: HttpClient not initialized or base address not set" 
                : $"{_oAuthMyAnimeListHttpClient.BaseAddress}v1/oauth2/authorize?response_type=code&client_id={ClientId}&code_challenge={_codeChallenge}&code_challenge_method=plain";
        }
        
        /// <summary>
        ///     Exchanging authorization code for refresh and access tokens
        /// </summary>
        /// <param name="authorizationUrl"></param>
        /// <returns></returns>
        public static async Task InitUserAccessToken(string authorizationUrl)
        {
            if (_oAuthMyAnimeListHttpClient == null || _codeVerifier == null) return;

            var authUrl = new Uri(authorizationUrl);
            string code = HttpUtility.ParseQueryString(authUrl.Query).Get("code");
            string state = HttpUtility.ParseQueryString(authUrl.Query).Get("state");
            
            // preparing a http form to get the token type, expires in, access token and refresh token
            var parameters = new Dictionary<string, string>();
            parameters.Add("client_id", ClientId);
            parameters.Add("code", code);
            parameters.Add("code_verifier", _codeVerifier);
            parameters.Add("grant_type", "authorization_code");

            using var content = new FormUrlEncodedContent(parameters);
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                
            var response = await _oAuthMyAnimeListHttpClient.PostAsync("v1/oauth2/token", content);
                
            if (response.StatusCode == HttpStatusCode.OK)
            {
                _tokenRefreshInfo = await response.Content.ReadAsAsync<AuthorizationResponse>();
            }
        }
        
        /// <summary>
        /// Writes the given object instance to a xml file. Author: https://www.youtube.com/watch?v=jbwjbbc5PjI
        /// </summary>
        private static void SaveTokenToFile()
        {
            if (_tokenRefreshInfo == null) return;
            
            const string filePath = "TOKENS.xml";
            
            var serializer = new XmlSerializer(typeof(AuthorizationResponse));
            using TextWriter tw = new StreamWriter(filePath);
            serializer.Serialize(tw, _tokenRefreshInfo);
            
            _tokenRefreshInfo = null;
        }

        /// <summary>
        /// Reads an object instance from a xml file. Author: https://www.youtube.com/watch?v=jbwjbbc5PjI
        /// </summary>
        /// <returns>Returns a new instance of the object read from the xml file.</returns>
        private static void LoadTokenFromFile()
        {
            const string filePath = "TOKENS.xml";
            var deserializer = new XmlSerializer(typeof(AuthorizationResponse));

            using (TextReader tr = new StreamReader(filePath))
            {
                _tokenRefreshInfo = (AuthorizationResponse)deserializer.Deserialize(tr);
            }
        }

        /// <summary>
        /// Used to refresh the access token for the client
        /// </summary>
        public static async Task RefreshToken()
        {
            
            if (_oAuthMyAnimeListHttpClient == null) return;
            
            // loading refresh tokens from file, if loading failed wont refresh
            LoadTokenFromFile();
            if (_tokenRefreshInfo == null) return;
            
            // preparing a http form to get the token type, expires in, access token and refresh token
            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "refresh_token");
            parameters.Add("refresh_token", _tokenRefreshInfo.refresh_token);

            using var content = new FormUrlEncodedContent(parameters);
            
            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            
            var response = await _oAuthMyAnimeListHttpClient.PostAsync("v1/oauth2/token", content);
            
            if (response.StatusCode == HttpStatusCode.OK)
            {
                _tokenRefreshInfo = await response.Content.ReadAsAsync<AuthorizationResponse>();
            }
            
            // saving the new token
            SaveTokenToFile();
            
        }
        
        
        /// <summary>
        ///     https://stackoverflow.com/a/65220376
        /// </summary>
        private static string GenerateNonce()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz123456789";
            var random = new Random();
            var nonce = new char[128];
            for (var i = 0; i < nonce.Length; i++) nonce[i] = chars[random.Next(chars.Length)];

            return new string(nonce);
        }

        /// <summary>
        ///     Unused since only supported OAuth code challenge method is plain
        ///     May Change in the future
        ///     https://stackoverflow.com/a/65220376
        /// </summary>
        /// <param name="codeVerifier"></param>
        /// <returns></returns>
        private static string GenerateCodeChallenge(string codeVerifier)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                var b64Hash = Convert.ToBase64String(hash);
                var code = Regex.Replace(b64Hash, "\\+", "-");
                code = Regex.Replace(code, "\\/", "_");
                code = Regex.Replace(code, "=+$", "");
                return code;
            }
        }
    }
    
    public class AuthorizationResponse
    {
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }

        public override string ToString()
        {
            return $"{token_type}, {expires_in}, {access_token}, {refresh_token}";
        }
    }
}