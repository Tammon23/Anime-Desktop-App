using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using AnimeScraper.Codebase.Helpers;
using AnimeScraper.Codebase.Helpers.ContentControl;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using static System.String;

namespace AnimeScraper.Codebase.Providers;

public class Animepahe : IProvider
{
    private static readonly Animepahe Instance = new ();
    private Animepahe(){}

    public static Animepahe GetInstance()
    {
        return Instance;
    }

    private const ProviderEnum Provider = ProviderEnum.ANIMEPAHE;

    /// <summary>
    /// Gets the home page url of the anime
    /// </summary>
    /// <param name="query">The anime to query</param>
    /// <returns>A <see cref="SearchableAnime"/> object with the name of the found anime and the homepage url</returns>
    /// <example>Query: claymore, result Name: Claymore, AnimeHomePageUrl: https://animepahe.com/anime/908871f8-67f9-8624-044e-c181215dacaf</example>
    public async IAsyncEnumerable<SearchableAnime> Search(string query) 
    {
        var uri = Provider.BuildSearchPath().AddParameters(new { q = query, m = "search"});
        var responseJson = await ClientManager.LoadFromGetWebJson(uri);
        if(responseJson == null)
            yield break;
        
        var content = responseJson.data;
        if(content == null) yield break;
        
        foreach (var node in content)
        {
            yield return new SearchableAnime(node.title.Value, Provider.BuildContentPath() + node.session.Value);
        }
    }

    /// <summary>
    /// Used to grab the mrl of a particular anime episode. Optionally, a user can update their anime object to save that mrl
    /// and related qualities
    /// </summary>
    /// <param name="homePageUrl">The home page url of the anime</param>
    /// <param name="episode">The episode number of the anime</param>
    /// <param name="name">The name of the anime (parameter only used when anime parameter is null</param>
    /// <param name="anime">The anime object to update instead making a new one</param>
    /// <returns>An anime object with the specified episode urls, null if failure. If failure the failure reason
    /// is provided as the second item of the tuple. <b>Note. Both items of tuple will not be null</b></returns>
    public async Task<Tuple<Anime?, string?>> GrabAnimeEpisode(string homePageUrl, int episode, string name = "unknown", Anime? anime = null)
    {
        anime ??= new Anime(name, homePageUrl, GetProviderType());

        var urls = new StreamUrlManager();
        
        var episodeId = await GetEpisodeId(homePageUrl, episode);
        if (episodeId == null) return Tuple.Create<Anime?, string?>(null, "Unable to find episode session id");
        
        var providerUrls = await GetKwikUrl(episodeId);

        if (providerUrls == null) return Tuple.Create<Anime?, string?>(null, "Unable to find episode provider url");

        foreach (var (quality, url) in providerUrls)
        {
            var m3u8 = await GetM3U8MRL(url);
            if(m3u8 == null) continue;
            urls.AddStreamUrl(m3u8, quality, new Dictionary<AnimeUrlOptionsEnum, string>{{AnimeUrlOptionsEnum.Host, new Uri(url).Host},
                {AnimeUrlOptionsEnum.Referrer, "https://kwik.cx/"}});

        }

        if (urls.GetAllStreamUrls().Count == 0)
            return Tuple.Create<Anime?, string?>(null, "Unable to find mrl from provider url");
        
        anime.AddEpisode(episode, urls);

        return new Tuple<Anime?, string?>(anime, null);
    }

    /// <summary>
    /// Used to grab the mrl of a particular anime episode. Optionally, a user can update their anime object to save that mrl
    /// and related qualities
    /// </summary>
    /// <param name="searchableAnime"><see cref="SearchableAnime"/></param>
    /// <param name="episode">The episode number of the anime</param>
    /// <param name="anime">The anime object to update instead making a new one</param>
    /// <returns>An anime object with the specified episode urls, null if failure. If failure the failure reason
    /// is provided as the second item of the tuple. <b>Note. Both items of tuple will not be null</b></returns>
    public async Task<Tuple<Anime?, string?>> GrabAnimeEpisode(SearchableAnime searchableAnime, int episode, Anime? anime = null)
        => await GrabAnimeEpisode(searchableAnime.AnimeHomePageUrl, episode, searchableAnime.Name, anime);
    
    /// <summary>
    /// Used to grab the mrl of a range of anime episodes. Optionally, a user can update their anime object to save that mrl
    /// and related qualities
    /// </summary>
    /// <param name="homePageUrl">The home page url of the anime</param>
    /// <param name="firstEpisode">The first episode number of the anime of the range</param>
    /// <param name="lastEpisode">The last episode number of the anime of the range</param>
    /// <param name="name">The name of the anime (parameter only used when anime parameter is null</param>
    /// <param name="anime">The anime object to update instead making a new one</param>
    /// <returns>An anime object with the specified episode urls, null if failure. If failure the failure reason
    /// is provided as the second item of the tuple</returns>
    public async Task<Tuple<Anime?, string?>> GrabAnimeEpisodeRange(string homePageUrl, int firstEpisode, int lastEpisode = -1,
        string name = "unknown", Anime? anime = null)
    {
        anime ??= new Anime(name, homePageUrl, GetProviderType());
        var (episodeIds, failureReason) = await GetEpisodeIdRange(homePageUrl, firstEpisode, lastEpisode);
        if (episodeIds == null) return Tuple.Create<Anime?, string?>(null, failureReason);

        var someFailuresFound = false;
        foreach (var (episode, episodeId) in episodeIds)
        {
            
            var providerUrls = await GetKwikUrl(episodeId);
            if (providerUrls == null)
            {
                someFailuresFound = true;
                continue;
            }
            
            var urls = new StreamUrlManager();
            foreach (var (quality, url) in providerUrls)
            {
                var m3u8 = await GetM3U8MRL(url);
                if(m3u8 == null) continue;
                urls.AddStreamUrl(m3u8, quality, new Dictionary<AnimeUrlOptionsEnum, string>{{AnimeUrlOptionsEnum.Host, new Uri(url).Host},
                    {AnimeUrlOptionsEnum.Referrer, "https://kwik.cx/"}});
            }
            
            if (urls.GetAllStreamUrls().Count == 0)
                continue;

            anime.AddEpisode(episode, urls);
        }

        return anime.GetNumberSavedEpisodes() == 0 
            ? Tuple.Create<Anime?, string?>(null, "Could not find the providers or mrls of the providers of any episode") 
            : Tuple.Create<Anime?, string?>(anime, someFailuresFound ? "Could not find some of the providers or mrls of the providers of some episodes" : null);
    }

    /// <summary>
    /// Used to grab the mrl of a range of anime episodes. Optionally, a user can update their anime object to save that mrl
    /// and related qualities
    /// </summary>
    /// <param name="searchableAnime"><see cref="SearchableAnime"/></param>
    /// <param name="firstEpisode">The first episode number of the anime of the range</param>
    /// <param name="lastEpisode">The last episode number of the anime of the range</param>
    /// <param name="anime">The anime object to update instead making a new one</param>
    /// <returns>An anime object with the specified episode urls, null if failure. If failure the failure reason
    /// is provided as the second item of the tuple</returns>
    public async Task<Tuple<Anime?, string?>> GrabAnimeEpisodeRange(SearchableAnime searchableAnime, int firstEpisode,
        int lastEpisode = -1, Anime? anime = null) => await GrabAnimeEpisodeRange(searchableAnime.AnimeHomePageUrl,
        firstEpisode, lastEpisode, searchableAnime.Name, anime);

    /// <summary>
    /// Takes in a home url of an anime, and tries to get the session id of a provide episode
    /// </summary>
    /// <param name="homePageUrl">The home page url of the anime</param>
    /// <param name="episode">The episode to search for</param>
    /// <returns>The session id of the anime episode</returns>
    /// <example>HomePageUrl: https://animepahe.com/anime/908871f8-67f9-8624-044e-c181215dacaf, Episode: 1, result a4990fad8c8ba1272e020adbce0f53950983a4759f288f3045da4d47225fd296</example>
    private static async Task<string?> GetEpisodeId(string homePageUrl, int episode)
    {
        Helper.ThrowIfBadEpisodeNumber(episode);
        
        if (!Helper.IsValidUrl(homePageUrl, Provider))
            return null;
        
        var parsed = homePageUrl.TrimEnd('/').Split("/");
        if (parsed.Length != 5 || parsed[3] != "anime")
            return null;
        var id = parsed.Last();
        
        var animepaheAPI = $"https://animepahe.com/api?m=release&id={id}&sort=episode_asc&page=1";
        var responseJson = await ClientManager.LoadFromGetWebJson(animepaheAPI);
        if (responseJson == null || responseJson.total < episode)
            return null;

        if (responseJson.per_page >= episode) 
            return responseJson.data[episode-1].session;

        
        // if the episode doesnt belong on this page, query the correct page instead
        var perPageObjects = responseJson.per_page.ToObject<int>();
        var page = Helper.CalculatePage(episode, perPageObjects);
        var uri = $"https://animepahe.com/api?m=release&id={id}&sort=episode_asc&page={page}";
        var rj = await ClientManager.LoadFromGetWebJson(uri);
        return rj == null ? null : (string?) rj.data[(episode - 1) % perPageObjects].session;
    }

    /// <summary>
    /// Takes in a home url of an anime, and tries to get the session id of episodes between a range
    /// </summary>
    /// <param name="homePageUrl">The home page url of the anime</param>
    /// <param name="first">The first episode to search for</param>
    /// <param name="last">The last episode to search for (-1 for all episodes after first)</param>
    /// <returns>A tuple, where the first value is a dictionary of each queried episode and their respective IDs, and
    /// the second value is a failure reason. One of the 2 tuple values will always be null</returns>
    /// <example>HomePageUrl: https://animepahe.com/anime/c56dfd21-efb2-dc95-a661-f6555685e958, first: 1, last: 2, result {(1, sessionIdforep1), (1, sessionIdforep2)}</example>
    private static async Task<Tuple<Dictionary<int, string>?, string?>> GetEpisodeIdRange(string homePageUrl, int first, int last = -1)
    {
        Helper.ThrowIfBadEpisodeNumber(first);
        Helper.ThrowIfBadEpisodeNumber(last, false);
        Helper.ThrowIfBadEpisodeRange(first, last, true);
        
        if (!Helper.IsValidUrl(homePageUrl, Provider))
            return Tuple.Create<Dictionary<int, string>?, string?>(null, "Invalid homePageUrl Provided");

        var parsed = homePageUrl.TrimEnd('/').Split("/");
        if (parsed.Length != 5 || parsed[3] != "anime")
            return Tuple.Create<Dictionary<int, string>?, string?>(null, "Unable to parse anime id from homePageUrl");
        
        var id = parsed.Last();
        
        // searching the first page of json responses for the episodes of the provided anime
        var animepaheAPI = $"https://animepahe.com/api?m=release&id={id}&sort=episode_asc&page=1";
        var responseJson = await ClientManager.LoadFromGetWebJson(animepaheAPI);
        if (responseJson == null)
            return Tuple.Create<Dictionary<int, string>?, string?>(null, "Something went wrong during the request");

        if (first > (int) responseJson.total.Value)
            return Tuple.Create<Dictionary<int, string>?, string?>(null, "First episode ID to search for exceeds available episodes");
        
        var sessions = new Dictionary<int, string>();
        var nextEpisode = first;
        if (last == -1) last = (int)responseJson.total.Value;

        var responsesPerPage = (int)responseJson.per_page.Value;

        // the first episode we are looking for is on the first page of the response
        if (responsesPerPage >= first)
        {
            while (nextEpisode <= last && nextEpisode < responsesPerPage)
            {
                sessions.Add(nextEpisode, responseJson.data[nextEpisode-1].session.Value);
                nextEpisode++;
            }
        }
        var page = Helper.CalculatePage(nextEpisode, responsesPerPage);

        while (nextEpisode <= last)
        {
            // try scraping the next json from the next page
            var pageUrl = $"https://animepahe.com/api?m=release&id={id}&sort=episode_asc&page={page}";
            var responseJsonNextPage = await ClientManager.LoadFromGetWebJson(pageUrl);
            
            // if something goes wrong that page, abort and return what we have if we have something else return null 
            if (responseJsonNextPage == null)
            {
                return sessions.Count == 0 
                    ? Tuple.Create<Dictionary<int, string>?, string?>(null, "Something went wrong during the request")
                    : Tuple.Create<Dictionary<int, string>?, string?>(sessions, null);
            }
            
            // if all goes right up until this point, parse as many episode as you need from the json
            while (nextEpisode <= last && nextEpisode < responsesPerPage * page)
            {
                sessions.Add(nextEpisode, responseJsonNextPage.data[(nextEpisode - 1) % responsesPerPage].session.Value);
                nextEpisode++;
            }

            page++;

            // move onto the next page...
        }

        return Tuple.Create<Dictionary<int, string>?, string?>(sessions, null);
    }

    /// <summary>
    /// Used to retrieve the url of the content, from the content provider that animepahe uses
    /// </summary>
    /// <param name="episodeId">The session id of the episode</param>
    /// <returns>A dictionary where the key is the quality, and the value is the kwik url, null if failed to make initial request</returns>
    private static async Task<Dictionary<string, string>?> GetKwikUrl(string episodeId)
    {
        var responseJson = await ClientManager.LoadFromGetWebJson($"https://animepahe.com/api?m=links&id={episodeId}&p=kwik");
        if (responseJson == null)
            return null;

        var content = new Dictionary<string, string>();
        foreach (JObject v in responseJson.data!)
        foreach (KeyValuePair<string, JToken> item in v)
                content.Add(item.Key, ((dynamic) item.Value).kwik.Value);

        return content;
    }

    private static async Task<string?> GetM3U8MRL(string kwikUrl)
    { 
        var requestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(kwikUrl),
            Headers =
            {
                {HttpRequestHeader.Referer.ToString(), Provider.GetBaseUri()}
            }
        };
        
        var htmlDoc = (await ClientManager.LoadFromGetWeb(requestMessage)).Item1;
        if (htmlDoc == null) return null;
        var host = GetHost(ref htmlDoc);
        return ConstructMRL(host ?? "", ref htmlDoc);  /// --> should return a new anime object
    }
    
    /// <summary>
    /// Tries to find a hint of what the host of the mrl is by using the fact the page preconnects to the host
    /// </summary>
    /// <param name="htmlDoc">The document to parse</param>
    /// <returns>The string that could be the host, null if nothing found</returns>
    /// <example>Might find this tag &lt;link rel="preconnect" href="//na-051.files.nextcdn.org"> and extracts the host
    /// <b>na-051.files.nextcdn.org</b></example>
    private static string? GetHost(ref HtmlDocument htmlDoc)
    {
        var node = htmlDoc.DocumentNode.SelectSingleNode("//link[@rel='preconnect']");
        var host = node.Attributes["href"].Value;
        if (host != null && host.StartsWith("//"))
            return host.TrimStart('/');
        return host;
    }

    /// <summary>
    /// This is a hacky method to decipher the stream source from a given eval string. This is done to avoid running any
    /// javascript
    /// </summary>
    /// <param name="host">The potential host of the media</param>
    /// <param name="htmlDoc">The document to parse through</param>
    /// <example>With the host as <i>na-051.files.nextcdn.org</i>, the regexs formed are
    /// m3u8\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|org\|nextcdn\|files\|051\|na and
    /// na\|051\|files\|nextcdn\|org\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|m3u8</example>
    private static string? ConstructMRL(string host, ref HtmlDocument htmlDoc)
    {
        var node = htmlDoc.DocumentNode.SelectSingleNode("//script[starts-with(text(), 'eval')]");
        var plyr = node?.InnerText;

        if (plyr == null) return null;

        var formattedHost = host.Split(new[] {"-", "."}, StringSplitOptions.None);
        var formattedHostReverse = formattedHost.Reverse();

        var forwardRegex = new StringBuilder();
        foreach (var v in formattedHost)
        {
            forwardRegex.Append(v);
            forwardRegex.Append(@"\|");
        }
        forwardRegex.Append(@"[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|m3u8");

        var backwardRegex = new StringBuilder();
        backwardRegex.Append(@"m3u8\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+\|[a-zA-Z0-9]+");
        foreach (var v in formattedHostReverse)
        {
            backwardRegex.Append(@"\|");
            backwardRegex.Append(v);
        }
        
        var rxForward = new Regex(forwardRegex.ToString(), RegexOptions.Compiled);
        var rxBackward= new Regex(backwardRegex.ToString(), RegexOptions.Compiled);

        var matchForward = rxForward.Match(plyr).Value;
        var matchBackward = rxBackward.Match(plyr).Value;

        if (matchForward.Length != 0) return CreateLinkFromMessy(matchForward, true);
        return matchBackward.Length == 0 ? null : CreateLinkFromMessy(matchBackward, false);
    }

    /// <summary>
    /// Creates the formatted mrl link from a obfuscated interpretation
    /// </summary>
    /// <param name="messy">The obfuscated interpretation</param>
    /// <param name="isForward">Whether the forward regex was used or the backward one
    /// (dictates if the obfuscated is forwards or not)</param>
    /// <returns>A formatted link</returns>
    /// <example>Messy: m3u8|uwu|7e9321dffff0b274c95c99dee8b248594f00d31acebaec3e7e594a0d80109f0a|06|stream|org|nextcdn|files|051|na,
    /// isForward: false,
    /// Returns: https://na-051.files.nextcdn.org/stream/06/7e9321dffff0b274c95c99dee8b248594f00d31acebaec3e7e594a0d80109f0a/uwu.m3u8 </example>
    private static string CreateLinkFromMessy(string messy, bool isForward)
    {
        var linkSegments = messy.Split("|");
        return Format(isForward ? "https://{0}-{1}.{2}.{3}.{4}/{5}/{6}/{7}/{8}.{9}" : "https://{9}-{8}.{7}.{6}.{5}/{4}/{3}/{2}/{1}.{0}", linkSegments);
    }

    
    public ProviderEnum GetProviderType()
    {
        return Provider;
    }
}