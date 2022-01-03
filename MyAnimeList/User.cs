using System.Net.Http;
using System.Threading.Tasks;
using MyAnimeList.ResponseObjects.User;

namespace MyAnimeList
{
    public class User
    {
        public User()
        {
        }
        
        public async Task<AnimeListStatus?> UpdateAnimeListStatus(int animeId, FormUrlEncodedContent contentToUpdate)
        {
            var r = await MALRequestClient.Patch<AnimeListStatus>(
                $"v2/anime/{animeId}/my_list_status", contentToUpdate);
            return r;
        }
    }
}