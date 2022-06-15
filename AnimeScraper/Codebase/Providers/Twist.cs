using System.Net;
using AnimeScraper.Codebase.Helpers;
using Newtonsoft.Json.Linq;

namespace AnimeScraper.Codebase.Providers;

public class Twist : IProvider
{
    private const ProviderEnum Provider = ProviderEnum.TWIST;

    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        var uri = Provider.BuildSearchPath();
        HttpResponseMessage response;
        
        using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri))
        {
            requestMessage.Headers.Add("x-access-token", "0df14814b9e590a1f26d3071a4ed7974");
            response = await ClientManager.GetClient().SendAsync(requestMessage);
        }
        
        if(response.StatusCode != HttpStatusCode.OK)
            yield break;

        dynamic responseJson  = JObject.Parse(await response.Content.ReadAsStringAsync());

        var content = responseJson;
        
        
        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri)).Item1;
        
        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes("//*[@class='anime-list']//*[@class='name']");

        if (htmlNodes == null) yield break;
        
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