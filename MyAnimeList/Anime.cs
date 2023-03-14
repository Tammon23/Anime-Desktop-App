using MyAnimeList.Exceptions;
using MyAnimeList.FieldManager;
using MyAnimeList.ResponseObjects.Anime;

namespace MyAnimeList;

public class Anime
{
    /// <summary>
    ///     Used to search for anime
    /// </summary>
    /// <param name="searchString">The search phrase used to find a anime result</param>
    /// <param name="offset">The amount of results to skip over when retrieving results</param>
    /// <param name="limit">The max amount of results</param>
    /// <param name="fields">The fields that you want returned. *Does not default to all*</param>
    /// <returns>AnimeList object or null if failure</returns>
    public static async Task<AnimeList?> GetAnime(string searchString, int offset = 0, int limit = 100,
        string fields = "")
    {
        if (limit is > 100 or < 0) throw new LimitOutOfRangeException("Limit must be between 0 and 100");

        var r = await MALRequestClient.Get<AnimeList>(
            $"v2/anime?offset={offset}&q={searchString}&limit={limit}&fields={fields}");
        return r;
    }

    /// <summary>
    ///     Used to get additional details of the anime
    /// </summary>
    /// <param name="id">The Id of the anime</param>
    /// <param name="fields">The fields that you want returned. *Does not default to all*</param>
    /// <returns>AnimeDetails object or null if failure</returns>
    public static async Task<AnimeDetails?> GetAnimeDetails(int id, string fields = "")
    {
        var r = await MALRequestClient.Get<AnimeDetails>(
            $"v2/anime/{id}?fields={fields}");
        return r;
    }
    
    /// <summary>
    ///     Overload for <see cref="GetAnimeDetails(int,string)"/>
    /// </summary>
    /// <param name="id">The Id of the anime</param>
    /// <param name="fields">The fields that you want returned. *Does not default to all*</param>
    /// <returns>AnimeDetails object or null if failure</returns>
    public static async Task<AnimeDetails?> GetAnimeDetails(int id, FieldSelector fields)
    {
        return await GetAnimeDetails(id, fields.GetFieldsAsString());
    }

    /// <summary>
    ///     Used to get the ranking of anime based on the ranking type
    /// </summary>
    /// <param name="animeRankingType">The ranking type to rank the anime by</param>
    /// <param name="offset">The amount of results to skip over when retrieving results</param>
    /// <param name="limit">The max amount of results</param>
    /// <param name="fields">The fields that you want returned. *Does not default to all*</param>
    /// <returns>AnimeRanking object or null if failure</returns>
    public static async Task<AnimeRanking?> GetAnimeRanking(AnimeRankingTypeEnum animeRankingType, int offset = 0,
        int limit = 100,
        string fields = "")
    {
        if (limit is > 500 or < 0) throw new LimitOutOfRangeException("Limit must be between 0 and 500");

        var ranking = Util.AnimeRankingTypeToString(animeRankingType);

        var r = await MALRequestClient.Get<AnimeRanking>(
            $"v2/anime/ranking?ranking_type={ranking}&offset={offset}&limit={limit}&fields={fields}");

        return r;
    }

    /// <summary>
    ///     Gets the seasonal anime of a provided year and season
    /// </summary>
    /// <param name="year">The year of the season to check</param>
    /// <param name="season">The season to check</param>
    /// <param name="offset">The amount of results to skip over when retrieving results</param>
    /// <param name="limit">The max amount of results</param>
    /// <param name="fields">The fields that you want returned. *Does not default to all*</param>
    /// <returns>AnimeSeasonal object or null if failure</returns>
    public static async Task<AnimeSeasonal?> GetSeasonalAnime(int year, SeasonEnum season, int offset = 0,
        int limit = 100,
        string fields = "")
    {
        if (limit is > 500 or < 0) throw new LimitOutOfRangeException("Limit must be between 0 and 500");
        var searchSeason = Util.SeasonToString(season);

        var r = await MALRequestClient.Get<AnimeSeasonal>(
            $"v2/anime/season/{year}/{searchSeason}?offset={offset}&limit={limit}&fields={fields}");
        return r;
    }

    /// <summary>
    ///     Used to get suggested anime for the authorized user
    /// </summary>
    /// <param name="offset">The amount of results to skip over when retrieving results</param>
    /// <param name="limit">The max amount of results</param>
    /// <param name="fields">The fields that you want returned. *Does not default to all*</param>
    /// <returns>AnimeSuggestions object or null if failure</returns>
    public static async Task<AnimeSuggestions?> GetAnimeSuggestions(int offset = 0, int limit = 100,
        string fields = "")
    {
        if (limit is > 100 or < 0) throw new LimitOutOfRangeException("Limit must be between 0 and 100");

        var r = await MALRequestClient.Get<AnimeSuggestions>(
            $"v2/anime/suggestions?offset={offset}&limit={limit}&fields={fields}", true);
        return r;
    }
}