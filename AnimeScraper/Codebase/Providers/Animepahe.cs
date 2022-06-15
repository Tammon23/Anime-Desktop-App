using System.Net;
using AnimeScraper.Codebase.Helpers;
using Newtonsoft.Json.Linq;

namespace AnimeScraper.Codebase.Providers;

public class Animepahe : IProvider
{
    private const ProviderEnum Provider = ProviderEnum.ANIMEPAHE;
    
    


    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        var uri = Provider.BuildSearchPath().AddParameters(new { q = query, m = "search"});

        var response = await ClientManager.GetClient().GetAsync(uri);
        if(response.StatusCode != HttpStatusCode.OK)
            yield break;

        dynamic responseJson  = JObject.Parse(await response.Content.ReadAsStringAsync());
        var content = responseJson.data;
        if(content == null) yield break;
        
        foreach (var node in content)
        {
            yield return new SearchableAnime(node.title.Value, Provider.BuildContentPath() + node.session.Value);
        }
    }
    
    public Task GetHtml()
    {
        throw new NotImplementedException();
    }

    public void GetStream(string uri)
    {
        throw new NotImplementedException();
        
    }
    public ProviderEnum GetProviderType()
    {
        return Provider;
    }
}