using AnimeScraper.Codebase.Helpers;

namespace AnimeScraper.Codebase.Providers;

public class Kawaiifu : IProvider
{
    private const ProviderEnum Provider = ProviderEnum.KAWAIIFU;
    
    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        var uri = Provider.BuildSearchPath().AddParameters(new {keyword = query});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri)).Item1;
        
        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes("//*[@class='info']/h4/a[last()]");
        
        if (htmlNodes == null) yield break; 
        
        foreach (var node in htmlNodes)
        {
            yield return new SearchableAnime(node.InnerText.Trim(), node.Attributes["href"].Value);
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