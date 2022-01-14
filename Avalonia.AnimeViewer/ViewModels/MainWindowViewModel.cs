using System;
using System.IO;
using System.Net;
using ReactiveUI;

namespace Avalonia.AnimeViewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        private string? _animeSearchText;
        private bool _isBusy;
        private string _imageLink = "https://cdn.myanimelist.net/images/anime/1988/119437.jpg";


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
                DownloadImage(AnimeTitle);
                System.Diagnostics.Debug.WriteLine(AnimeTitle);
            }
        }

        private Avalonia.Media.Imaging.Bitmap _imageBitmap = null;
        public Avalonia.Media.Imaging.Bitmap ImageBitmap
        {
            get => _imageBitmap;
            set => this.RaiseAndSetIfChanged(ref _imageBitmap, value);
        }

        public MainWindowViewModel()
        {
            DownloadImage(AnimeTitle);
        }

        public void DownloadImage(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadDataAsync(new Uri(url));
                client.DownloadDataCompleted += DownloadComplete;
            }
        }

        private void DownloadComplete(object sender, DownloadDataCompletedEventArgs e)
        {
            try
            {
                byte[] bytes = e.Result;

                Stream stream = new MemoryStream(bytes);

                var image = new Avalonia.Media.Imaging.Bitmap(stream);
                ImageBitmap = image;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                ImageBitmap = null; // Could not download...
            }
            
        }
    }
}