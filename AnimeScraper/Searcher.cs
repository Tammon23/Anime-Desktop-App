using System.Net;
using AnimeScraper.Codebase;
using AnimeScraper.Codebase.Helper;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace AnimeScraper;

public static class Searcher
{

    #region Providers search field

    public static async IAsyncEnumerable<SearchableAnime> Search9Anime(string query)
    {
        const ProviderEnum provider = ProviderEnum.NINEANIME;
        var uri = provider.BuildSearchPath().AddParameters(new {keyword = query, sort = "views:desc"});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri)).Item1;
        
        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes("//*[@class='anime-list']//*[@class='name']");

        if (htmlNodes == null) yield break;
        
        foreach (var node in htmlNodes)
        { 
            yield return new SearchableAnime(node.InnerText, provider.GetBaseUri().TrimEnd('/') + node.Attributes["href"].Value);
        }
    }

    public static async IAsyncEnumerable<SearchableAnime> SearchAnimeKaizoku(string query)
    {
        const ProviderEnum provider = ProviderEnum.ANIMEKAIZOKU;
        var uri = provider.BuildSearchPath().AddParameters(new {s = query});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri)).Item1;

        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes("//*[@class='post-title']");

        if (htmlNodes == null) yield break;
        
        foreach (var node in htmlNodes)
        { 
            yield return new SearchableAnime(node.InnerText, 
                provider.GetBaseUri() + node.SelectNodes("//a")[0].Attributes["href"].Value);
        }
    }

    public static async IAsyncEnumerable<SearchableAnime> SearchAllAnime(string query)
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
    
    public static async IAsyncEnumerable<SearchableAnime> SearchAnimepahe(string query)
    {
        const ProviderEnum provider = ProviderEnum.ANIMEPAHE;
        var uri = provider.BuildSearchPath().AddParameters(new { q = query, m = "search"});

        var response = await ClientManager.GetClient().GetAsync(uri);
        if(response.StatusCode != HttpStatusCode.OK)
            yield break;

        dynamic responseJson  = JObject.Parse(await response.Content.ReadAsStringAsync()); // try dynamic if var doesnt work

        var content = responseJson.data;
        if(content == null) yield break;
        
        foreach (var node in content)
        {
            yield return new SearchableAnime(node.title.Value, provider.BuildContentPath() + node.session.Value);
        }
    }
    
    // not working
    public static async IAsyncEnumerable<SearchableAnime> SearchAnimeOut(string query)
    {
        const ProviderEnum provider = ProviderEnum.ANIMEOUT;
        var uri = provider.BuildSearchPath().AddParameters(new { s = query});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri)).Item1;
        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes(@"//h3[@class=""post-title entry-title""]/a");

        if (htmlNodes == null) yield break;
        
        foreach (var node in htmlNodes)
        { 
            yield return new SearchableAnime(node.InnerText, node.Attributes["href"].Value);
        }
    }

    public static async IAsyncEnumerable<SearchableAnime> SearchAnimixPlay(string query)
    {
        const ProviderEnum provider = ProviderEnum.ANIMIXPLAY;
        var uri = provider.BuildSearchPath();

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
            yield return new SearchableAnime(node.InnerText, provider.GetBaseUri().TrimEnd('/') + node.Attributes["href"].Value);
        }
    }
    
    public static async IAsyncEnumerable<SearchableAnime> SearchGoGoAnime(string query)
    {
        const ProviderEnum provider = ProviderEnum.GOGOANIME;
        var uri = provider.BuildSearchPath().AddParameters(new {keyword = query});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri, -1)).Item1;
        
        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes("//p[@class='name']/a");

        if (htmlNodes == null) yield break;
        
        foreach (var node in htmlNodes)
        { 
            yield return new SearchableAnime(node.Attributes["title"].Value, provider.GetBaseUri().TrimEnd('/') + node.Attributes["href"].Value);
        }
    }
    
    public static async IAsyncEnumerable<SearchableAnime> SearchKawaiifu(string query)
    {
        const ProviderEnum provider = ProviderEnum.KAWAIIFU;
        var uri = provider.BuildSearchPath().AddParameters(new {keyword = query});

        var htmlDoc = (await ClientManager.LoadFromGetWeb(uri)).Item1;
        
        var htmlNodes = htmlDoc?.DocumentNode.SelectNodes("//*[@class='info']/h4/a[last()]");
        
        if (htmlNodes == null) yield break; 
        
        foreach (var node in htmlNodes)
        {
            yield return new SearchableAnime(node.InnerText.Trim(), node.Attributes["href"].Value);
        }
    }
    #endregion

    public static IAsyncEnumerable<SearchableAnime?> Search(ProviderEnum provider, string query)
    {
        return provider switch
        {
            ProviderEnum.NINEANIME    => Search9Anime(query),
            ProviderEnum.ANIMEKAIZOKU => SearchAnimeKaizoku(query),
            ProviderEnum.ALLANIME     => SearchAllAnime(query),
            ProviderEnum.ANIMEPAHE    => SearchAnimepahe(query),
            ProviderEnum.ANIMEOUT     => SearchAnimeOut(query),
            ProviderEnum.ANIMIXPLAY   => SearchAnimixPlay(query),
            ProviderEnum.GOGOANIME    => SearchGoGoAnime(query),
            ProviderEnum.KAWAIIFU     => SearchKawaiifu(query),
            _ => Search9Anime(query)
        };
    }
}

public class SearchableAnime
{
    private readonly string _name;
    private readonly string _animeUrl;

    public SearchableAnime(string name, string animeUrl)
    {
        _name = name;
        _animeUrl = animeUrl;
    }

    public string Name => _name;
    public string AnimeUrl => _animeUrl;

    public override string ToString()
    {
        return $"Name: {_name} - AnimeUrl: {_animeUrl}";
    }
}
