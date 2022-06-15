using System.Net;
using AnimeScraper.Codebase.Helpers;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace AnimeScraper.Codebase.Providers;

public class AnimixPlay : IProvider
{
    private const ProviderEnum Provider = ProviderEnum.ANIMIXPLAY;
    
    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        var uri = Provider.BuildSearchPath();

        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("qfast", query)
        });

        var response = await ClientManager.GetClient().PostAsync(uri, formContent);

        if (response.StatusCode != HttpStatusCode.OK)
            yield break;
        
        dynamic responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
        var content = responseJson.result.Value;
        if (content == null) yield break;

        HtmlDocument htmlDoc = new();
        htmlDoc.LoadHtml(content);

        var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//*[@class='name']/a");

        if (htmlNodes == null)
            yield break;
        
        foreach (var node in htmlNodes)
        { 
            yield return new SearchableAnime(node.InnerText, Provider.GetBaseUri().TrimEnd('/') + node.Attributes["href"].Value);
        }
    }

    public void GetStream(string uri)
    {
        throw new NotImplementedException();
    }

    public Task GetHtml()
    {
        throw new NotImplementedException();
    }

    public ProviderEnum GetProviderType()
    {
        return Provider;
    }
}