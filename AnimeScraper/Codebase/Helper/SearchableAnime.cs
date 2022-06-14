namespace AnimeScraper.Codebase.Helper;

/// <summary>
/// Content container that holds an anime name, and related url  
/// </summary>
public class SearchableAnime
{
    public SearchableAnime(string name, string animeUrl)
    {
        Name = name;
        AnimeUrl = animeUrl;
    }

    public string Name { get; }

    public string AnimeUrl { get; }

    public override string ToString()
    {
        return $"Name: {Name}, AnimeUrl: {AnimeUrl}";
    }
}