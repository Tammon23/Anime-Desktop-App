using System.Threading.Tasks;
using MyAnimeList.ResponseObjects.Anime;


namespace MyAnimeList.Anime
{
    public partial class Anime
    {
        public Anime()
        {
            MALRequestClient.Init();
        }
        
        public async Task<AnimeList?> GetAnimeList(string searchString, int limit=4)
        {   
            var r = await MALRequestClient.Get<AnimeList>($"v2/anime?q={searchString}&limit={limit}");
            return r;
        }
    }
    
    
}
