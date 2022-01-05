using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MyAnimeList.Exceptions;
using MyAnimeList.ResponseObjects.Forum;

namespace MyAnimeList
{
    public class Forum
    {
        /// <summary>
        /// Gets the forum boards
        /// </summary>
        /// <returns>ForumBoard object or null if failure</returns>
        public async Task<ForumBoard?> GetForumBoards()
        {
            var r = await MALRequestClient.Get<ForumBoard>(
                "v2/forum/boards"
            );
            
            return r;
        }
        
         // ----> requires testing
        /// <summary>
        /// Gets more detail related to a given forum topic
        /// </summary>
        /// <param name="topic_id">The ID of the forum topic</param>
        /// <param name="offset">The amount of results to skip over when retrieving results</param>
        /// <param name="limit">The max amount of results</param>
        /// <returns>ForumTopicDetail object or null if failure</returns>
        /// <exception cref="LimitOutOfRangeException">Limit must be between 0 and 100</exception>
        public async Task<ForumTopicDetail> GetForumTopicDetail(int topic_id, int limit = 100, int offset=0)
        {
            if (limit is > 100 or < 0)
            {
                throw new LimitOutOfRangeException("Limit must be between 0 and 100");
            }

            var r = await MALRequestClient.Get<ForumTopicDetail>(
                $"v2/forum/topic/{topic_id}?offset={offset}&limit={limit}"
            );

            return r;
        }
        
        public async Task<ForumTopic> GetForumTopic(int? boardId = null,
            int? subboardId = null,
            string? q = null,
            string? topicUserName = null,
            string? userName = null,
            string sort = "recent",
            int limit = 100,
            int offset = 0
            )
        {
            if (limit is > 100 or < 0)
            {
                throw new LimitOutOfRangeException("Limit must be between 0 and 100");
            }

            var r = await MALRequestClient.Get<ForumTopicDetail>(
                $"v2/forum/topics?q={q}&subboard_id={subboardId}&limit={limit}"
            );
            return r;
        }
    }
}