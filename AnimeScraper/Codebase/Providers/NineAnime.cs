using AnimeScraper.Codebase.Helper;

namespace AnimeScraper.Codebase.Providers;

public class NineAnime : IProvider
{
    private const ProviderEnum Provider = ProviderEnum.NINEANIME;
    
    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        var uri = Provider.BuildSearchPath().AddParameters(new {keyword = query, sort = "views:desc"});

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

    public ProviderEnum GetProvider()
    {
        return Provider;
    }
}