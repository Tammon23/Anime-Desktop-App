using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using Avalonia.AnimeViewer.Models;
using ReactiveUI;

namespace Avalonia.AnimeViewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        private string? _animeSearchText;
        private bool _isBusy;
        private string _imageLink = "https://cdn.myanimelist.net/images/anime/1988/119437.jpg";
        private CancellationTokenSource? _cancellationTokenSource;

        public MainWindowViewModel()
        {
            this.WhenAnyValue(x => x.AnimeSearchText)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Throttle(TimeSpan.FromMilliseconds(400))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(DoAnimeSearch!);

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

        public string AnimeTitle
        {
            get => _imageLink;
            set {
                this.RaiseAndSetIfChanged(ref _imageLink, value);
                System.Diagnostics.Debug.WriteLine(AnimeTitle);
            }
        }

        public ObservableCollection<AnimeSearchViewModel> SearchForAnimeResult { get; } = new();

        private async void DoAnimeSearch(string searchTerm)
        {
            IsBusy = true;
            SearchForAnimeResult.Clear();
            
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            var animeSearchResults = await AnimeSearch.SearchAsync(searchTerm);

            foreach (var anime in animeSearchResults)
            {
                var vm = new AnimeSearchViewModel(anime);
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

        private Avalonia.Media.Imaging.Bitmap _imageBitmap = null;
        public Avalonia.Media.Imaging.Bitmap ImageBitmap
        {
            get => _imageBitmap;
            set => this.RaiseAndSetIfChanged(ref _imageBitmap, value);
        }
    }
}