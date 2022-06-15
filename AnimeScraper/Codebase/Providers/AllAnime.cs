using System.Net;
using AnimeScraper.Codebase.Helpers;
using Newtonsoft.Json.Linq;

namespace AnimeScraper.Codebase.Providers;

public class AllAnime : IProvider
{
    private const ProviderEnum Provider = ProviderEnum.ALLANIME;
    
    public async IAsyncEnumerable<SearchableAnime> Search(string query)
    {
        const ProviderEnum provider = ProviderEnum.ALLANIME;
        var uri = provider.BuildSearchPath().AddParameters(new
        {
            variables = $@"{{""search"":{{""allowAdult"":true,""query"":""{query.Replace(@"""", @"\""")}""}},""translationType"":""sub""}}",
            extensions = @"{""persistedQuery"":{""version"":1,""sha256Hash"":""9343797cc3d9e3f444e2d3b7db9a84d759b816a4d84512ea72d079f85bb96e98""}}"
        });

        var response = await ClientManager.GetClient().GetAsync(uri);
        
        if(response.StatusCode != HttpStatusCode.OK)
            yield break;

        dynamic responseJson  = JObject.Parse(await response.Content.ReadAsStringAsync());

        var content = responseJson.data?.shows?.edges;
        
        if (content == null) yield break;
        var found = false;
        
        foreach (var node in content)
        {
            if (node == null) continue;
            foreach (var item in node.availableEpisodes)
            {
                if (item.Value <= 0) continue;
                found = true;
                break;
            }
                
            if (!found) continue;
                    
            yield return new SearchableAnime(node.name.Value, provider.GetBaseUri() + $"anime/{node._id}");
            found = false;
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