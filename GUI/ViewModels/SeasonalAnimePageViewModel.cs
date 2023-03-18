using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using GUI.Models;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;
using ReactiveUI;
using AnimeSeasonal = GUI.Models.AnimeSeasonal;

namespace GUI.ViewModels;

public class SeasonalAnimePageViewModel : ReactiveObject, IRoutableViewModel
{
    private int _year;
    private int _selectedSeason;
    
    public ObservableCollection<Node> QueriedAnimeSeasonal { get; } = new();
    public int MaximumYear { get; set; }
    
    /*
    public int Year { get; set; }
    public int SelectedSeason { get; set; }
    */
    
    public int Year
    {
        get => _year;
        set => this.RaiseAndSetIfChanged(ref _year, value);
    }

    public int SelectedSeason
    {
        get => _selectedSeason; 
        set => this.RaiseAndSetIfChanged(ref _selectedSeason, value);
    }
    
    public SeasonalAnimePageViewModel(IScreen screen)
    {
        HostScreen = screen;
        var time = DateTime.Now;

        Year = time.Year;
        MaximumYear = Year;
        SelectedSeason = time.Month switch
        {
            <= 3 => 0,
            > 3 and <= 6 => 1,
            > 6 and <= 9 => 2,
            _ => 3
        };
        
        this.WhenAnyValue(x => x.Year, x => x.SelectedSeason)
            .Throttle(TimeSpan.FromSeconds(1), RxApp.MainThreadScheduler)
            .SelectMany(GetSeasonalAnime)
            .Subscribe();
    }

    // Reference to IScreen that owns the routable view model
    public IScreen HostScreen { get; }

    // Unique identifier for the routable view model
    public string? UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);

    private async Task<Unit> GetSeasonalAnime((int, int) _, CancellationToken cancel)
    {
        QueriedAnimeSeasonal.Clear();
        
        var selectedSeason = SelectedSeason switch
        {
            0 => SeasonEnum.Winter,
            1 => SeasonEnum.Spring,
            2 => SeasonEnum.Summer,
            _ => SeasonEnum.Fall
        };
        
        // searching for the anime using the provided search term
        var animeSeasonal = await AnimeSeasonal.GetSeasonalAnime(Year, selectedSeason);

        foreach (var anime in animeSeasonal)
        {
            var node = new NodePresentation(anime.ContentNode, HostScreen)
            {
                UseLargePicture = true // eventually will be migrated to the settings plane
            };
            QueriedAnimeSeasonal.Add(node);
        }
        
        return Unit.Default;
    }
}