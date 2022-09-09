using System.Text;

namespace AnimeScraper.Codebase.Helpers.ContentControl;

public class Anime : SearchableAnime
{
    public ProviderEnum ProviderType;
    private readonly Dictionary<int, StreamUrlManager> _subbedEpisodes = new();
    private readonly Dictionary<int, StreamUrlManager> _dubbedEpisodes = new();

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

    public int GetNumberSavedEpisodes() => _subbedEpisodes.Count + _dubbedEpisodes.Count;
    public int GetNumberSubbedSavedEpisodes() => _subbedEpisodes.Count;
    public int GetNumberDubbedSavedEpisodes() => _dubbedEpisodes.Count;
    
    public Dictionary<int, StreamUrlManager> GetEpisodesDict() => _subbedEpisodes.Union(_dubbedEpisodes).ToDictionary (k => k.Key, v => v.Value);
    public Dictionary<int, StreamUrlManager> GetSubbedEpisodesDict() => _subbedEpisodes;
    public Dictionary<int, StreamUrlManager> GetDubbedEpisodesDict() => _dubbedEpisodes;

    /// <summary>
    /// Adds a specified episode to the anime index
    /// </summary>
    /// <param name="episodeNumber">The episode's number</param>
    /// <param name="episode">The <see cref="StreamUrlManager"/> of the episode</param>
    public void AddEpisode(int episodeNumber, StreamUrlManager episode)
    {
        if (episode.Type == SubDubTitle.Dubbed)
        {
            _dubbedEpisodes[episodeNumber] = episode;
        }
        else
        {
            _subbedEpisodes[episodeNumber] = episode;
        }
    }

    /// <summary>
    /// Gets the specified subbed episode of an anime if it's saved to the object
    /// </summary>
    /// <param name="episodeNumber">The episode to look for</param>
    /// <returns>A <see cref="StreamUrlManager"/> of the episode of it exists in this context, null otherwise</returns>
    public StreamUrlManager? GetSubbedEpisode(int episodeNumber)
    {
        return _subbedEpisodes.ContainsKey(episodeNumber) ? _subbedEpisodes[episodeNumber] : null;
    }
    
    /// <summary>
    /// Gets the specified dubbed episode of an anime if it's saved to the object
    /// </summary>
    /// <param name="episodeNumber">The episode to look for</param>
    /// <returns>A <see cref="StreamUrlManager"/> of the episode of it exists in this context, null otherwise</returns>
    public StreamUrlManager? GetDubbedEpisode(int episodeNumber)
    {
        return _dubbedEpisodes.ContainsKey(episodeNumber) ? _dubbedEpisodes[episodeNumber] : null;
    }

    /// <summary>
    /// Gets the subbed episodes numbers saved in the instance
    /// </summary>
    /// <returns>A list of episode numbers</returns>
    public List<int> GetSubbedEpisodeNumbers()
    {
        var episodeNumbers = new List<int>();
        foreach (var (episodeNumber, _) in _subbedEpisodes)
        {
            episodeNumbers.Add(episodeNumber);
        }

        episodeNumbers.Sort();
        return episodeNumbers;
    }

    
    /// <summary>
    /// Gets the dubbed episodes numbers saved in the instance
    /// </summary>
    /// <returns>A list of episode numbers</returns>
    public List<int> GetDubbedEpisodeNumbers()
    {
        var episodeNumbers = new List<int>();
        foreach (var (episodeNumber, _) in _dubbedEpisodes)
        {
            episodeNumbers.Add(episodeNumber);
        }

        episodeNumbers.Sort();
        return episodeNumbers;
    }
    
    public override string ToString()
    {
        if (_subbedEpisodes.Count == 0 && _dubbedEpisodes.Count == 0)
            return " { Empty } ";
        
        var sb = new StringBuilder();
        var size = 0;
        
        sb.Append(" { ");
        foreach (var (episode, manager) in _subbedEpisodes)
        {
            size += 1;
            if (size == _subbedEpisodes.Count)
                sb.Append($"<Subbed Episode: {episode}, Manager:{manager}> ");
            else
                sb.Append($"<Subbed Episode: {episode}, Manager:{manager}>, ");

        }
        
        foreach (var (episode, manager) in _dubbedEpisodes)
        {
            size += 1;
            if (size == _subbedEpisodes.Count)
                sb.Append($"<Dubbed Episode: {episode}, Manager:{manager}> ");
            else
                sb.Append($"<Dubbed Episode: {episode}, Manager:{manager}>, ");

        }
        sb.Append("} ");
        
        return sb.ToString();
        }
}