using System;
using System.Net.Http;
using System.Threading.Tasks;
using MyAnimeList.Exceptions;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.User;
using MyAnimeList.ResponseObjects.User.MyAnimeList.ResponseObjects.User;

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
        
        /// <summary>
        /// Gets the user's anime list
        /// </summary>
        /// <param name="username">Username of the anime list, defaults to @me</param>
        /// <param name="status">Filters returned anime list by these statuses. To return all anime, don't specify this field (null)</param>
        /// <param name="sort">The method to stor by (anime id under development)</param>
        /// <param name="offset">The amount of results to skip over when retrieving results</param>
        /// <param name="limit">The max amount of results</param>
        /// <returns>AnimeListStatus object or null if failure</returns>
        /// <exception cref="LimitOutOfRangeException">Limit must be between 0 and 1000 inclusively</exception>
        
        public async Task<UserAnimeList?> GetUserAnimeList(string username = "@me",
            StatusEnum? status = null,
            SortEnum? sort = null,
            int offset = 0,
            int limit = 4)
        {
            if (limit is > 1000 or < 0)
            {
                throw new LimitOutOfRangeException("Limit must be between 0 and 1000");
            }

            var r = await MALRequestClient.Get<UserAnimeList>(
                $"v2/users/{username}/animelist?fields=list_status&offset={offset}&limit={limit}" 
                    + $"{(status == null ? "" : "&status=" + Util.StatusToString(status))}"
                    + $"{(sort == null ? "" : "&sort=" + Util.SortToString(sort))}",
                username == "@me"
            );
            
            return r;
        }
        
        /// <summary>
        /// Removes an anime from the users anime list 
        /// </summary>
        /// <param name="animeId">The ID of the anime to be removed</param>
        /// <returns>True if the delete was successful, False if the anime ID is invalid, null remaining cases</returns>
        public async Task<bool?> DeleteAnimeListItem(int animeId)
        {
            var r = await MALRequestClient.Delete(
                $"v2/anime/{animeId}/my_list_status");
            return r;
        }
    }
}