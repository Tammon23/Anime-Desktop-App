using AnimeScraper.Codebase.Helper;

namespace AnimeScraper.Codebase.Providers;

public class AnimeOut : IProvider
{
    private ProviderEnum Provider = ProviderEnum.ANIMEOUT;
    
    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        var uri = Provider.BuildSearchPath().AddParameters(new { s = query});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri)).Item1;
        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes(@"//h3[@class=""post-title entry-title""]/a");

        if (htmlNodes == null) yield break;
        
        foreach (var node in htmlNodes)
        { 
            yield return new SearchableAnime(node.InnerText, node.Attributes["href"].Value);
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