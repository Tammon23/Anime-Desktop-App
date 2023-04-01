using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyAnimeList;
using MyAnimeList.ResponseObjects.General;

namespace GUI.Models;

public class AnimeSearch : MALConnector
{
    private AnimeSearch(Node searchResult) : base(searchResult)
    {
    }
    
    // Change to enum this class
    public static async Task<IEnumerable<AnimeSearch>> SearchAsync(string searchString, int offset = 0, int limit = 100,
        string fields = "")
    {
        if (!ClientInitialized)
        {
            await MALRequestClient.Init();
            ClientInitialized = true;
        }

        var r = await Anime.GetAnime(searchString);

        return r == null ? new List<AnimeSearch>() : r.Data.Select(node => new AnimeSearch(node.Node)).ToList();
    }
}