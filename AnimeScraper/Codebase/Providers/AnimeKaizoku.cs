using AnimeScraper.Codebase.Helper;

namespace AnimeScraper.Codebase.Providers;

public class AnimeKaizoku : IProvider
{
    private const ProviderEnum Provider = ProviderEnum.ANIMEKAIZOKU;
    
    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        const ProviderEnum Provider = ProviderEnum.ANIMEKAIZOKU;
        var uri = Provider.BuildSearchPath().AddParameters(new {s = query});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri)).Item1;

        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes("//*[@class='post-title']");

        if (htmlNodes == null) yield break;
        
        foreach (var node in htmlNodes)
        { 
            yield return new SearchableAnime(node.InnerText, 
                Provider.GetBaseUri() + node.SelectNodes("//a")[0].Attributes["href"].Value);
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