namespace AnimeScraper.Codebase.Helpers;

public class Anime : SearchableAnime
{
    public ProviderEnum ProviderType;
    private IProvider _provider;
    private Dictionary<int, StreamUrlManager> episodes;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="name">The name/title of the anime</param>
    /// <param name="animeHomePageUrl">The url to the provider for the anime</param>
    /// <param name="provider">The provider that this anime entry is associated with</param>
    public Anime(string name, string animeHomePageUrl, ProviderEnum provider) : base(name, animeHomePageUrl)
    {
        ProviderType = provider;
    }

    /// <summary>
    /// Used to get urls in a page format
    /// </summary>
    /// <param name="batchSize">The number of episodes to get on each page</param>
    /// <param name="runUntilLast">Whether the method should keep yielding pages with results until no more pages can
    /// be yielded, defaults to true</param>
    /// <param name="lastEpisode">The last url the method should try and get (only relevant when runUntilLast = false)</param>
    public void GetEpisodeUrls(int batchSize, bool runUntilLast = true, uint lastEpisode = 0){}
    
    /// <summary>
    /// Used to get episode urls within a range
    /// </summary>
    /// <param name="first">The start episode number</param>
    /// <param name="last">The last episode number. If the last episode exceeds the maximum episodes available,
    /// then the maximum is returned</param>
    public void GetEpisodeUrls(int first, int last){}
    
}