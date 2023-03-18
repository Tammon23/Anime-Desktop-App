#define DEBUG

using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using MyAnimeList.Constants;
using MyAnimeList.Exceptions;

namespace MyAnimeList;

public class OAuth
{
    private static HttpClient? _oAuthMyAnimeListHttpClient;
    private static string? _codeVerifier;
    private static string? _codeChallenge;
    private static AuthorizationResponse? _tokenRefreshInfo;

    private static HttpListener? _authHttpListener;
    private static Process? _browserProcess;

    private static string? _token;
    private const string FilePath = "TOKENS.xml";

    /// <summary>
    /// Used to manage when we should generate a new token, load a token from file,
    /// or refresh a saved token
    /// </summary>
    public static async Task Init()
    {
        if (LoadTokenFromFile())
        {
            
        }
        else
        {
            await GenerateToken();
        }
    }
    
    private static async Task GenerateToken()
    {
        _codeVerifier = GenerateNonce();

        // MAL only supports OAuth 2.0 code challenge method plain
        // meaning the code verifier and code challenge should be equal
        _codeChallenge = _codeVerifier;

        if (_oAuthMyAnimeListHttpClient == null)
        {
            _oAuthMyAnimeListHttpClient = HttpClientFactory.Create();
            _oAuthMyAnimeListHttpClient.DefaultRequestHeaders.Accept.Clear();
            _oAuthMyAnimeListHttpClient.BaseAddress = new Uri(MyAnimeListConstants.APIUrl);
        }

        if (_authHttpListener == null)
        {
            _authHttpListener = new HttpListener();
        }
        else
        {
            if (_authHttpListener.IsListening)
            {
                _authHttpListener.Stop();
                _authHttpListener.Abort();
            }

            _authHttpListener = new HttpListener();
        }

        try
        {
            _authHttpListener = new HttpListener();
            _authHttpListener.Prefixes.Add(MyAnimeListConstants.RedirectUrl);
            _authHttpListener.Start();

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex}");
        }

        // if the browser process variable has yet to be set, create a new one or if it has
        // been set, refresh the current one
        if (_browserProcess == null)
            _browserProcess = new Process();
        else
            _browserProcess.Close();

        // This is the URL required to link a user's account to the app
        // see https://myanimelist.net/blog.php?eid=835707 or
        // https://myanimelist.net/apiconfig/references/authorization
        _browserProcess.StartInfo.FileName = string.Format(MyAnimeListConstants.AuthUrl, MyAnimeListConstants.APIUrl,
            MyAnimeListConstants.ClientId, _codeChallenge);
        _browserProcess.StartInfo.UseShellExecute = true;
        _browserProcess.StartInfo.Verb = "";
        _browserProcess.Start();


        var context = await _authHttpListener.GetContextAsync();

        var code = context.Request.QueryString.Get("code");
        var state = context.Request.QueryString.Get("state");

        if (code == null)
            throw new AuthenticationUrlIncomplete("Code variable missing from authentication URL");

        /*if (state == null)
            throw new AuthenticationUrlIncomplete("State variable missing from authentication URL");*/

        await InitUserAccessToken(code, state);

        _authHttpListener.Stop();

        // Lastly, save the token to a file for later use
        SaveTokenToFile();
    }

    public static void Login()
    {
        // determines when to refresh the token or generate a new one
    }


    /// <summary>
    ///     Exchanging authorization code for refresh and access tokens
    /// </summary>
    /// <param name="code">The user's Authorisation Code received during the previous step</param>
    /// <param name="state">A string which can be used to maintain state between the request and callback.
    /// It is later returned by the MAL servers to the API client</param>
    /// <returns></returns>
    private static async Task InitUserAccessToken(string code, string? state)
    {
        if (_oAuthMyAnimeListHttpClient == null || _codeVerifier == null) return;

        // preparing a http form to get the token type, expires in, access token and refresh token
        var parameters = new Dictionary<string, string>();
        parameters.Add("client_id", MyAnimeListConstants.ClientId);
        /*parameters.Add("client_secret", "");*/
        parameters.Add("grant_type", "authorization_code");
        parameters.Add("code", code);
        parameters.Add("code_verifier", _codeVerifier);

        using var content = new FormUrlEncodedContent(parameters);
        content.Headers.Clear();
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

        var response = await _oAuthMyAnimeListHttpClient.PostAsync("v1/oauth2/token", content);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            _tokenRefreshInfo = await response.Content.ReadAsAsync<AuthorizationResponse>();
            _tokenRefreshInfo.ExpiresAt = DateTime.UtcNow.AddSeconds(_tokenRefreshInfo.expires_in);

        }
    }

    /// <summary>
    ///     Writes the given object instance to a xml file. Author: https://www.youtube.com/watch?v=jbwjbbc5PjI
    /// </summary>
    private static void SaveTokenToFile()
    {
#if DEBUG
        Console.WriteLine("Saving token to file");
#endif

        if (_tokenRefreshInfo == null) return;

        _token = _tokenRefreshInfo.access_token;

        var serializer = new XmlSerializer(typeof(AuthorizationResponse));
        using TextWriter tw = new StreamWriter(FilePath);
        serializer.Serialize(tw, _tokenRefreshInfo);

        _tokenRefreshInfo = null;

#if DEBUG
        Console.WriteLine("Token successfully saved to file");
#endif
    }

    /// <summary>
    ///     Reads an object instance from a xml file. Author: https://www.youtube.com/watch?v=jbwjbbc5PjI
    /// </summary>
    /// <returns>Returns a new instance of the object read from the xml file.</returns>
    private static bool LoadTokenFromFile()
    {
        if (!File.Exists(FilePath))
            return false;
        
        var deserializer = new XmlSerializer(typeof(AuthorizationResponse));

        using TextReader tr = new StreamReader(FilePath);
        _tokenRefreshInfo = (AuthorizationResponse) deserializer.Deserialize(tr);
        _token = _tokenRefreshInfo.access_token;
        
        return true;
    }
    

    /// <summary>
    ///     Used to refresh the access token for the client
    /// </summary>
    public static async Task<bool> RefreshToken()
    {
        if (_oAuthMyAnimeListHttpClient == null) return false;

        // loading refresh tokens from file, if loading failed wont refresh
        LoadTokenFromFile();
        if (_tokenRefreshInfo == null) return false;

        // preparing a http form to get the token type, expires in, access token and refresh token
        var parameters = new Dictionary<string, string>();
        parameters.Add("grant_type", "refresh_token");
        parameters.Add("refresh_token", _tokenRefreshInfo.refresh_token);

        using var content = new FormUrlEncodedContent(parameters);

        content.Headers.Clear();
        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

        var response = await _oAuthMyAnimeListHttpClient.PostAsync("v1/oauth2/token", content);

        if (response.StatusCode == HttpStatusCode.OK)
            _tokenRefreshInfo = await response.Content.ReadAsAsync<AuthorizationResponse>();

        // saving the new token
        SaveTokenToFile();

        return response.StatusCode == HttpStatusCode.OK;
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

    public static string? GetToken()
    {
        return _token;
    }
}

/*[Serializable]*/
public class AuthorizationResponse
{
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public string access_token { get; set; }
    public string refresh_token { get; set; }
    public DateTime ExpiresAt { get; set; }
    
    /*[OnDeserialized]
    private void OnDeserializedMethod(StreamingContext context)
    {
        ExpiresAt = DateTime.UtcNow.AddSeconds(expires_in);
    }*/

    public override string ToString()
    {
        return $"{token_type}, {expires_in}, {access_token}, {refresh_token}";
    }
}