using System.Diagnostics.Tracing;
using System.Text;

namespace AnimeScraper.Codebase.Helpers.ContentControl;

public class Anime : SearchableAnime
{
    public ProviderEnum ProviderType;
    private readonly Dictionary<int, StreamUrlManager> _episodes = new();

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

    public int GetNumberSavedEpisodes() => _episodes.Count;
    public Dictionary<int, StreamUrlManager> GetEpisodesDict() => _episodes;

    /// <summary>
    /// Adds a specified episode to the anime index
    /// </summary>
    /// <param name="episodeNumber">The episode's number</param>
    /// <param name="episode">The <see cref="StreamUrlManager"/> of the episode</param>
    public void AddEpisode(int episodeNumber, StreamUrlManager episode)
    {
        _episodes[episodeNumber] = episode;
    }

    /// <summary>
    /// Gets the specified episode of an anime if it's saved to the object
    /// </summary>
    /// <param name="episodeNumber">The episode to look for</param>
    /// <returns>A <see cref="StreamUrlManager"/> of the episode of it exists in this context, null otherwise</returns>
    public StreamUrlManager? GetEpisode(int episodeNumber)
    {
        return _episodes.ContainsKey(episodeNumber) ? _episodes[episodeNumber] : null;
    }

    /// <summary>
    /// Gets the episodes numbers saved in the instance
    /// </summary>
    /// <returns>A list of episode numbers</returns>
    public List<int> GetEpisodeNumbers()
    {
        var episodeNumbers = new List<int>();
        foreach (var (episodeNumber, _) in _episodes)
        {
            episodeNumbers.Add(episodeNumber);
        }

        episodeNumbers.Sort();
        return episodeNumbers;
    }

    public override string ToString()
    {
        if (_episodes.Count == 0)
            return " { Empty } ";
        
        var sb = new StringBuilder();
        var size = 0;
        
        sb.Append(" { ");
        foreach (var (episode, manager) in _episodes)
        {
            size += 1;
            if (size == _episodes.Count)
                sb.Append($"<Episode: {episode}, Manager:{manager}> ");
            else
                sb.Append($"<Episode: {episode}, Manager:{manager}>, ");

        }
        sb.Append("} ");
        
        return sb.ToString();
        }
}