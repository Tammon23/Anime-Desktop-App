using AnimeScraper.Codebase.Helper;

namespace AnimeScraper.Codebase.Providers;

public class GoGoAnime : IProvider
{
    private const ProviderEnum Provider = ProviderEnum.GOGOANIME;
    
    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        var uri = Provider.BuildSearchPath().AddParameters(new {keyword = query});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri, -1)).Item1;
        
        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes("//p[@class='name']/a");

        if (htmlNodes == null) yield break;
        
        foreach (var node in htmlNodes)
        { 
            yield return new SearchableAnime(node.Attributes["title"].Value, Provider.GetBaseUri().TrimEnd('/') + node.Attributes["href"].Value);
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

    public ProviderEnum GetProvider()
    {
        return Provider;
    }
}