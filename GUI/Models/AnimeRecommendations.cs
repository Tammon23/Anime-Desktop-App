using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MyAnimeList;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;

namespace GUI.Models;

public class AnimeRecommendations : MALConnector
{
    
    public AnimeRecommendations(Node suggestedAnime) : base(suggestedAnime)
    {
    }
    
    public static async Task<IEnumerable<AnimeRecommendations>> GetRecommendedAsync(int offset = 0, int limit = 100, string fields = "")
    {
        if (!ClientAuthInitialized)
        {
            await MALRequestClient.Init(requireAuthInit:true);
            ClientAuthInitialized = true;
        }
        
        var r = await Anime.GetAnimeSuggestions();

        return r == null ? new List<AnimeRecommendations>() : r.Data.Select(node => new AnimeRecommendations(node.Node)).ToList();
    }
}