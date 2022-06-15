namespace AnimeScraper.Codebase.Helpers;

/// <summary>
/// Content container that holds an anime name, and related url  
/// </summary>
public class SearchableAnime
{
    public SearchableAnime(string name, string animeHomePageUrl)
    {
        Name = name;
        AnimeHomePageUrl = animeHomePageUrl;
    }

    public string Name { get; }

    public string AnimeHomePageUrl { get; }

    public override string ToString()
    {
        return $"Name: {Name}, AnimeHomePageUrl: {AnimeHomePageUrl}";
    }
}