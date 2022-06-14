namespace AnimeScraper.Codebase.Helper;

public interface ISearcher
{
    /// <summary>
    /// Used to asynchronously search for anime, and return a <see cref="SearchableAnime"/>
    /// </summary>
    /// <param name="query">The content to search for, i.e., Naruto, or Claymore</param>
    /// <returns>A asynchronously enumerable of <see cref="SearchableAnime"/>s</returns>
    public IAsyncEnumerable<SearchableAnime> Search(string query);
}