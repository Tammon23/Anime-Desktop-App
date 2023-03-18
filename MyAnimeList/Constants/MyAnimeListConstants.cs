namespace MyAnimeList.Constants;

public class MyAnimeListConstants
{
    // TODO eventually change below to my github io website to avoid "port in use" problems
    // remember to change the url in the mal api account settings
    public const string RedirectUrl = "http://localhost:44406/"; 
    public const string APIUrl = "https://myanimelist.net/";

    public const string AuthUrl =
        "{0}v1/oauth2/authorize?response_type=code&client_id={1}&code_challenge={2}&code_challenge_method=plain";

    public const string ClientId = "23c477235d19c5349899187d13f5af36";
}
