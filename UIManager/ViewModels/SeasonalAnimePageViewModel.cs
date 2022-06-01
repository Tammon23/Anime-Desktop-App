using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Avalonia.Media.Imaging;
using UIManager.Models;

namespace UIManager.ViewModels;

public class SeasonalAnimePageViewModel : ViewModelBase
{
    private Bitmap? _art;
    private readonly AnimeSearch _anime;
    private CancellationTokenSource? _cancellationTokenSource;
    public ObservableCollection<AnimeListPageViewModel> SearchForAnimeResult { get; } = new();
    private string _animeSearchText;
    private bool _isBusy;

    public SeasonalAnimePageViewModel()
    {
        this.WhenAnyValue(x => x.AnimeSearchText)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoAnimeSearch!);
    }
    
    
    public SeasonalAnimePageViewModel(AnimeSearch anime)
    {
        _anime = anime;
    }

    public string AnimeTitle => _anime.SearchResult.Title;

    public Bitmap? Art
    {
        get => _art;
        private set => this.RaiseAndSetIfChanged(ref _art, value);
    }

    public bool IsBusy => _isBusy;

    public string AnimeSearchText
    {
        get => _animeSearchText;
        set => this.RaiseAndSetIfChanged(ref _animeSearchText, value);
    }

    public async Task LoadCover()
    {
        await using var imageStream = await _anime.LoadAnimeArtLargeBitmapAsync();
        Art = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
    }

    public static async Task<IEnumerable<AnimeListPageViewModel>> LoadCached()
    {
        return (await AnimeSearch.LoadCachedAsync()).Select(x => new AnimeListPageViewModel(x));
    }

    public async Task SaveToDiskAsync()
    {
        await _anime.SaveAsync();

        if (Art != null)
        {
            await Task.Run(() =>
            {
                using var fs = _anime.SaveAnimeArtLargeBitmapStream();
                Art.Save(fs);
            });
        }
    }
    
    private async void DoAnimeSearch(string searchTerm)
    {
        _isBusy = true;
        SearchForAnimeResult.Clear();

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        var animeSearchResults = await AnimeSearch.SearchAsync(searchTerm);
            
        foreach (var anime in animeSearchResults)
        {
            var vm = new AnimeListPageViewModel(anime);
            SearchForAnimeResult.Add(vm);
        }

        if (!_cancellationTokenSource.IsCancellationRequested)
        {
            LoadCovers(_cancellationTokenSource.Token);
        }
            
        _isBusy = false;
    }
    
    private async void LoadCovers(CancellationToken cancellationToken)
    {
        foreach (var anime in SearchForAnimeResult.ToList())
        {
            await anime.LoadCover();

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
        }
    }
    
    
}