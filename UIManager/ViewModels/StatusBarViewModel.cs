using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReactiveUI;
using UIManager.Models;
using UIManager.Util;

namespace UIManager.ViewModels;

public class StatusBarViewModel : ViewModelBase
{
    public string? _animeSearchText;
    private bool _isBusy;
    private CancellationTokenSource? _cancellationTokenSource;
    
    public StatusBarViewModel()
    {
        /*_sessionContext = sessionContext;
        sessionContext.PropertyChanged += OnSessionContextPropertyChanged;
        this.WhenAnyValue(x => x.AnimeSearchText)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoAnimeSearch!);*/
    }

    
    public string? AnimeSearchText
    {
        get => _animeSearchText;
        set => this.RaiseAndSetIfChanged(ref _animeSearchText, value);
    }
    
    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }

    private ObservableCollection<ContentCardViewModel> SearchForAnimeResult { get; } = new();

    private async void DoAnimeSearch(string searchTerm)
    {
        IsBusy = true;
        SearchForAnimeResult.Clear();

        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        var animeSearchResults = await AnimeSearch.SearchAsync(searchTerm);
            
        foreach (var anime in animeSearchResults)
        {
            var vm = new ContentCardViewModel(anime);
            SearchForAnimeResult.Add(vm);
        }

        if (!_cancellationTokenSource.IsCancellationRequested)
        {
            LoadCovers(_cancellationTokenSource.Token);
        }
            
        IsBusy = false;
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