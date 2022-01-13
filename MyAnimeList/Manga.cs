using System.Threading.Tasks;
using MyAnimeList.Exceptions;
using MyAnimeList.ResponseObjects.Manga;

namespace MyAnimeList
{
    public class Manga
    {
        /// <summary>
        /// Used to search for manga
        /// </summary>
        /// <param name="searchString">The search phrase used to find a manga result</param>
        /// <param name="offset">The amount of results to skip over when retrieving results</param>
        /// <param name="limit">The max amount of results</param>
        /// <param name="fields">The fields that you want returned. *Does not default to all*</param>
        /// <returns>MangaList object or null if failure</returns>
        public static async Task<MangaList?> GetMangaList(string searchString, int offset=0, int limit=100, string fields = "")
        {
            if (limit is > 100 or < 0)
            {
                throw new LimitOutOfRangeException("Limit must be between 0 and 100");
            }
            
            var r = await MALRequestClient.Get<MangaList>(
                $"v2/manga?offset={offset}&q={searchString}&limit={limit}&fields={fields}");
            return r;
        }
        
        /// <summary>
        /// Used to get additional details of the manga
        /// </summary>
        /// <param name="id">The Id of the manga</param>
        /// <param name="fields">The fields that you want returned. *Does not default to all*</param>
        /// <returns>AnimeDetails object or null if failure</returns>
        public static async Task<MangaDetails?> GetMangaDetails(int id, string fields = "")
        {
            var r = await MALRequestClient.Get<MangaDetails>(
                $"v2/manga/{id}?fields={fields}");
            return r;
        }
        
        public static async Task<MangaRanking?> GetAnimeRanking(MangaRankingTypeEnum rankingType, int offset = 0, int limit = 100,
            string fields = "")
        {
            if (limit is > 500 or < 0)
            {
                throw new LimitOutOfRangeException("Limit must be between 0 and 500");
            }
            
            string ranking = Util.MangaRankingTypeToString(rankingType);

            var r = await MALRequestClient.Get<MangaRanking>(
                $"v2/anime/ranking?ranking_type={ranking}&offset={offset}&limit={limit}&fields={fields}");

            return r;
        }
    }
}