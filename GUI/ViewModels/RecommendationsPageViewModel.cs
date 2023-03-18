using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GUI.Models;
using MyAnimeList.ResponseObjects.General;
using ReactiveUI;

namespace GUI.ViewModels;

public class RecommendationsPageViewModel : ReactiveObject, IRoutableViewModel
{
    public ObservableCollection<Node> QueriedAnimeRecommendations { get; } = new();

    public RecommendationsPageViewModel(IScreen screen)
    {
        HostScreen = screen;
        GetRecommended();
    }

    // Reference to IScreen that owns the routable view model
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    
    public async Task GetRecommended()
    {
        QueriedAnimeRecommendations.Clear();

        // searching for the anime using the provided search term
        var animeRecommendations = await AnimeRecommendations.GetRecommendedAsync();

        foreach (var anime in animeRecommendations)
        {
            var node = new NodePresentation(anime.ContentNode, HostScreen)
            {
                UseLargePicture = true // eventually will be migrated to the settings plane
            };
            QueriedAnimeRecommendations.Add(node);
        }
    }

}