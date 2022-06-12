using System.Net;
using HtmlAgilityPack;

namespace AnimeScraper.Codebase;

public static class ClientManager
{
    private static readonly HttpClient Client;
    static ClientManager()
    {
        var handler = new HttpClientHandler
        {
            AllowAutoRedirect = false
        };


        Client = HttpClientFactory.Create(handler);
        
        Client.DefaultRequestHeaders.Clear();
        Client.Timeout = TimeSpan.FromSeconds(30);
        Client.DefaultRequestHeaders.Add("Accept", "*/*");
        Client.DefaultRequestHeaders.Add("Connection", "keep-alive");
        Client.DefaultRequestHeaders.Add("Referer", "https://google.com/");
        Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (animdlc#/1.0.0)");

        AppDomain.CurrentDomain.ProcessExit += StaticCleanUp;
    }

    private static void StaticCleanUp(object? sender, EventArgs e)
    {
        Client.Dispose();
    }

    public static HttpClient GetClient()
    {
        return Client;
    }

    /// <summary>
    /// Allows for an insecure redirect. Redirects from Https -> Http are not ignored
    /// </summary>
    /// <param name="uri">The uri the web request is made to</param>
    /// <param name="maxRedirects">The maximum number of redirects the client is allowed to make, negative for infinite</param>
    /// <returns>A new <see cref="HtmlDocument"/> of the requested web page
    /// (null if failure), and the <see cref="HttpResponseMessage"/> returned</returns>
    public static async Task<Tuple<HtmlDocument?, HttpResponseMessage?>> LoadFromGetWeb(Uri uri, int maxRedirects)
    {
        var nextUri = uri;
        while (maxRedirects-- != 0)
        {
            var result = await LoadFromGetWeb(nextUri);
            var response = result.Item2;

            if (response?.StatusCode is not (HttpStatusCode.MovedPermanently
                or HttpStatusCode.Found
                or HttpStatusCode.SeeOther
                or HttpStatusCode.TemporaryRedirect
                or (HttpStatusCode) 308)) return result;
            
            nextUri = response.Headers.Location;
            if (nextUri == null) return result;
        }

        return Tuple.Create<HtmlDocument?, HttpResponseMessage?>(null, null);
    }
    
    /// <summary>
    /// Makes an async get request to the specified uri
    /// </summary>
    /// <param name="uri">The uri the web request is made to</param>
    /// <returns>A new <see cref="HtmlDocument"/> of the requested web page
    /// (null if failure), and the <see cref="HttpResponseMessage"/> returned</returns>
    public static async Task<Tuple<HtmlDocument?, HttpResponseMessage?>> LoadFromGetWeb(Uri uri)
    {
        var response = await Client.GetAsync(uri);
        if (response.StatusCode != HttpStatusCode.OK)
            return Tuple.Create<HtmlDocument?, HttpResponseMessage?>(null, response);
        
        HtmlDocument htmlDocument = new()
        {
            OptionAutoCloseOnEnd = false,
            OptionFixNestedTags = true
        };
        
        var responseHtml = await response.Content.ReadAsStringAsync();
        htmlDocument.LoadHtml(responseHtml);
        return Tuple.Create<HtmlDocument?, HttpResponseMessage?>(htmlDocument, response);

    }
    
    public static async Task<HtmlDocument?> LoadFromPostWeb(Uri uri, object content)
    {
        var postFormContent = new FormUrlEncodedContent(content.GetType().GetProperties().ToDictionary
        (
            propInfo => propInfo.Name,
            propInfo =>
            {
                var value = propInfo.GetValue(content, null);
                return value?.ToString();
            }
        ));
        
        var response = await Client.PostAsync(uri, postFormContent);
        if (response.StatusCode != HttpStatusCode.OK)
            return null;
        
        HtmlDocument htmlDocument = new()
        {
            OptionAutoCloseOnEnd = false,
            OptionFixNestedTags = true
        };
        
        var responseHtml = await response.Content.ReadAsStringAsync();
        htmlDocument.LoadHtml(responseHtml);
        return htmlDocument;
    }
}