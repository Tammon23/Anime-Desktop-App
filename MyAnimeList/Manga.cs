using System.Threading.Tasks;
using MyAnimeList.Exceptions;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.Forum;

namespace MyAnimeList
{
    public class Manga
    {
        public async Task<AnimeList?> GetMangaList(string searchString, int offset=0, int limit=100, string fields = "")
        {
            if (limit is > 100 or < 0)
            {
                throw new LimitOutOfRangeException("Limit must be between 0 and 100");
            }
            
            var r = await MALRequestClient.Get<AnimeList>(
                $"v2/manga?offset={offset}&q={searchString}&limit={limit}&fields={fields}");
            return r;
        }    
    }
}