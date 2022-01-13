using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MyAnimeList;
using MyAnimeList.ResponseObjects.General;
using Datum = MyAnimeList.ResponseObjects.Anime.Datum;

namespace Avalonia.AnimeViewer.Models;

public class AnimeSearch
{
    private static HttpClient s_httpClient = new();
    
    public AnimeSearch(string searchString, int offset=0, int limit=100, string fields = "")
    {
        SearchString = searchString;
        Offset = offset;
        Limit = limit;
        Fields = fields;
    }

    public string SearchString { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public string Fields { get; set; }
    
    public Node SearchResult { get; set; }
    
    private string CachePath => $"./Cache/Anime/{SearchResult.Id}-Art";
    
    /// <summary>
    /// Functions from https://docs.avaloniaui.net/tutorials/music-store-app/searching-for-albums
    /// </summary>
    /// <returns></returns>
    public async Task<Stream> LoadAnimeArtLargeBitmapAsync()
    {
        if (File.Exists(CachePath + "-Large.bmp"))
        {
            return File.OpenRead((CachePath + "-Large.bmp"));
        }
        else
        {
            var data = await s_httpClient.GetByteArrayAsync(SearchResult.MainPicture.Large);
            return new MemoryStream(data);
        }
        
    }
    
    public async Task<Stream> LoadAnimeArtMediumBitmapAsync()
    {
        if (File.Exists(CachePath + "-Medium.bmp"))
        {
            return File.OpenRead((CachePath + "-Medium.bmp"));
        }
        else
        {
            var data = await s_httpClient.GetByteArrayAsync(SearchResult.MainPicture.Medium);
            return new MemoryStream(data);
        }
    }

    public async Task SaveAsync()
    {
        if (!Directory.Exists("./Cache"))
        {
            Directory.CreateDirectory("./Cache");
        }

        using (var fs = File.OpenWrite(CachePath))
        {
            await SaveToStreamAsync(this, fs);
        }
    }

    public Stream SaveAnimeArtLargeBitmapStream()
    {
        return File.OpenWrite(CachePath + "-Large.bmp");
    }
    
    public Stream SaveAnimeArtMediumBitmapStream()
    {
        return File.OpenWrite(CachePath + "-Medium.bmp");
    }

    private static async Task SaveToStreamAsync(AnimeSearch data, Stream stream)
    {
        await JsonSerializer.SerializeAsync(stream, data).ConfigureAwait(false);
    }

    public static async Task<AnimeSearch> LoadFromStreamAsync(Stream stream)
    {
        return (await JsonSerializer.DeserializeAsync<AnimeSearch>(stream).ConfigureAwait(false))!;
    }

    public static async Task<IEnumerable<AnimeSearch>> LoadCachedAsync()
    {
        if (!Directory.Exists("./Cache"))
        {
            Directory.CreateDirectory("./Cache");
        }

        var results = new List<AnimeSearch>();

        foreach (var file in Directory.EnumerateFiles("./Cache"))
        {
            if (!string.IsNullOrWhiteSpace(new DirectoryInfo(file).Extension)) continue;

            await using var fs = File.OpenRead(file);
            results.Add(await AnimeSearch.LoadFromStreamAsync(fs).ConfigureAwait(false));
        }

        return results;
    }

    // Change to enum this class
    public static async Task<IReadOnlyList<Datum>> SearchAsync(string searchString, int offset=0, int limit=100, string fields = "")
    {
        var r = await Anime.GetAnime(searchString);
        return r.Data;
    }
}