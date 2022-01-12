using System;
using System.IO;
using System.Net;
using ReactiveUI;

namespace Avalonia.AnimeViewer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string chessboardUrl = "https://cdn.myanimelist.net/images/anime/1988/119437.jpg";
        public string ChessboardUrl
        {
            get => chessboardUrl;
            set {
                this.RaiseAndSetIfChanged(ref chessboardUrl, value);
                DownloadImage(ChessboardUrl);
                System.Diagnostics.Debug.WriteLine(ChessboardUrl);
            }
        }

        private Avalonia.Media.Imaging.Bitmap chessboard = null;
        public Avalonia.Media.Imaging.Bitmap Chessboard
        {
            get => chessboard;
            set => this.RaiseAndSetIfChanged(ref chessboard, value);
        }

        public MainWindowViewModel()
        {
            DownloadImage(ChessboardUrl);
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
                Chessboard = image;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Chessboard = null; // Could not download...
            }
            
        }
    }
}