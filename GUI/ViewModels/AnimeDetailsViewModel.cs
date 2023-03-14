using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using MyAnimeList;
using MyAnimeList.FieldManager;
using MyAnimeList.ResponseObjects.Anime;
using ReactiveUI;

namespace GUI.ViewModels;

public class AnimeDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    private static bool _clientInitialized;

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
        var result = new AnimeDetailsViewModel(screen)
        {
            Details = await GetDetails(id)
        };
        return result;
    }
    
    public AnimeDetails? Details { get; private init; } // add ^ in view if does not work as is

    public string UrlPathSegment => "Details_Page";
    public IScreen HostScreen { get; }

    public string? Art => Details?.MainPicture.Large ?? Details?.MainPicture.Medium;
    
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