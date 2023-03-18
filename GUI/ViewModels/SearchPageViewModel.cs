using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using GUI.Models;
using MyAnimeList.ResponseObjects.General;
using ReactiveUI;

namespace GUI.ViewModels;

public sealed class SearchPageViewModel : ReactiveObject, IRoutableViewModel
{
    public static readonly SearchPageViewModel Instance = new();
    private bool _isBusy;
    private string? _lastSearchTerm;

    /*public SearchPageViewModel(IAdvancedScreen screen)
    {
        HostScreen = screen;
        AnimeSearchText = screen.SearchBoxTextObservable;
        ButtonIsExecuting = screen.ExecuteSearch.IsExecuting;

        // https://stackoverflow.com/questions/20669067/how-to-obtain-string-out-of-iobservablestring-reactive-extensions
        // https://intellitect.com/blog/getting-started-model-view-viewmodel-mvvm-pattern-using-windows-presentation-framework-wpf/
        // https://stackoverflow.com/questions/50177352/is-there-a-way-to-track-when-reactive-command-finished-its-execution
    }*/

    public ObservableCollection<Node> AnimeSearchResult { get; } = new();

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }

    public IObservable<bool>? ButtonIsExecuting { get; set; }
    public IObservable<string>? AnimeSearchText { get; set; }


    // Reference to IScreen that owns the routable view model
    public IScreen? HostScreen { get; set; }

    // Unique identifier for the routable view model
    public string UrlPathSegment => "SearchPageViewModel";

    public async Task DoSearch(string searchTerm)
    {
        
        Debug.WriteLine($">>> Search Term: {searchTerm}");
        // showing the progress bar
        _isBusy = true;
        this.RaisePropertyChanged(nameof(IsBusy));

        AnimeSearchResult.Clear();

        // searching for the anime using the provided search term
        var animeSearchResults = await AnimeSearch.SearchAsync(searchTerm);

        foreach (var anime in animeSearchResults)
        {
            var node = new NodePresentation(anime.ContentNode, HostScreen)
            {
                UseLargePicture = true // eventually will be migrated to the settings plane
            };
            AnimeSearchResult.Add(node);
        }

        // hiding the progress bar
        _isBusy = false;
        this.RaisePropertyChanged(nameof(IsBusy));

        _lastSearchTerm = searchTerm;
    }
}