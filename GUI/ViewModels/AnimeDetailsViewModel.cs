using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using GUI.Models;
using MyAnimeList;
using MyAnimeList.FieldManager;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;
using ReactiveUI;

namespace GUI.ViewModels;

public class AnimeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    private static bool _clientInitialized;
    private IReadOnlyCollection<Node> AnimeRecommendations { get; init; }
    private IReadOnlyCollection<Node> AnimeRelated { get; init; }
    private IReadOnlyCollection<Node> MangaRelated { get; init; }
    
    public AnimeDetails? Details { get; private init; } // add ^ in view if does not work as is

    public string UrlPathSegment => "Details_Page";
    public IScreen HostScreen { get; }

    public string? Art => Details?.MainPicture.Large ?? Details?.MainPicture.Medium;

    private AnimeDetailsViewModel(IScreen screen)
    {
        HostScreen = screen;
    }

    public AnimeDetailsViewModel()
    {
        throw new System.NotImplementedException();
    }

    public static async Task<AnimeDetailsViewModel> CreateAsync(IScreen screen, int id)
    {
        var details = await GetDetails(id);
        List<Node> animeRecommendations = new();
        List<Node> relatedAnime = new();
        List<Node> relatedManga = new();
        
        if (details != null)
        {
            animeRecommendations.AddRange(details.Recommendations.Select(recAnime => new NodePresentation(recAnime.Node, screen)
            {
                UseLargePicture = true // eventually will be migrated to the settings plane
            }));
            
            relatedAnime.AddRange(details.RelatedAnime.Select(relAnime => new NodePresentation(relAnime.Node, screen)
            {
                UseLargePicture = true
            }));
            
            relatedManga.AddRange(details.RelatedManga.Select(relManga => new NodePresentation(relManga.Node, screen)
            {
                UseLargePicture = true
            }));
        }

        var result = new AnimeDetailsViewModel(screen)
        {
            Details = details,
            AnimeRecommendations = animeRecommendations,
            AnimeRelated = relatedAnime,
            MangaRelated = relatedManga
        };

        return result;
    }
    

    
    public static async Task<AnimeDetails?> GetDetails(int id)
    {
        if (!_clientInitialized)
        {
            await MALRequestClient.Init();
            _clientInitialized = true;
        }

        var fieldsToSearchFor = new FieldSelector();
        fieldsToSearchFor.AddAllFields();
        
        var result = await Anime.GetAnimeDetails(id, fieldsToSearchFor);
        Debug.WriteLine($">>> {result.MyListStatus}");
        return result;
    }
}