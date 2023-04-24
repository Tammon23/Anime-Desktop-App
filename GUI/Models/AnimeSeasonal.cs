using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyAnimeList;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;

namespace GUI.Models;

public class AnimeSeasonal : MALConnector
{
    private AnimeSeasonal(Node seasonalAnime) : base(seasonalAnime)
    {
    }
    
    // Change to enum this class
    public static async Task<IEnumerable<AnimeSeasonal>> GetSeasonalAnime(int year, SeasonEnum season, int offset = 0, int limit = 100,
        string fields = "")
    {
        var r = await Anime.GetSeasonalAnime(year, season, offset, limit, fields);

        return r == null ? new List<AnimeSeasonal>() : r.Data.Select(node => new AnimeSeasonal(node.Node)).ToList();
    }
}