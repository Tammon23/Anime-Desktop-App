using System;
using System.IO;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using GUI.ViewModels;
using MyAnimeList.ResponseObjects.General;
using ReactiveUI;

namespace GUI.Models;

// A slightly more advanced version of Node which allows
// for the art to be requested and converted to a Bitmap object
public class NodePresentation : Node
{
    private static readonly HttpClient SHttpClient = new();

    public NodePresentation(Node node, IScreen screen) : base(node.Id, node.Title, node.MainPicture)
    {
        
        GoDetailsPage = ReactiveCommand.CreateFromTask(
            async () =>
            {
                var model = await AnimeDetailsViewModel.CreateAsync(screen, Id);
                screen.Router.Navigate.Execute(model);
            });
    }

    public bool UseLargePicture { get; set; } = true;
    private string CachePath => $"./Cache/Anime/{Id}-Art";

    /*public ReactiveCommand<Unit, IRoutableViewModel> GoDetailsPage { get; }*/
    public ReactiveCommand<Unit, Unit> GoDetailsPage { get; }
    public Task<Bitmap> Art => LoadArt();
    /*public string Art => MainPicture.Large;*/
    private async Task<Bitmap> LoadArt()
    {
        Stream imageStream;
        if (MainPicture != null)
        {
            imageStream = UseLargePicture ? await LoadAnimeArtLargeBitmapAsync() : await LoadAnimeArtMediumBitmapAsync();
        }
        else
        {
            var assests = AvaloniaLocator.Current.GetService<IAssetLoader>();
            imageStream = assests.Open(new Uri("avares://GUI/Assets/SampleImages/image_not_found_image.png"));
        }

        return await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
    }
    
    /// <summary>
    ///     Functions from https://docs.avaloniaui.net/tutorials/music-store-app/searching-for-albums
    /// </summary>
    /// <returns></returns>
    private async Task<Stream> LoadAnimeArtLargeBitmapAsync()
    {
        if (File.Exists(CachePath + "-Large.bmp")) return File.OpenRead(CachePath + "-Large.bmp");

        var data = await SHttpClient.GetByteArrayAsync(MainPicture.Large);
        return new MemoryStream(data);
    }

    private async Task<Stream> LoadAnimeArtMediumBitmapAsync()
    {
        if (File.Exists(CachePath + "-Medium.bmp")) return File.OpenRead(CachePath + "-Medium.bmp");

        var data = await SHttpClient.GetByteArrayAsync(MainPicture.Medium);
        return new MemoryStream(data);
    }
}