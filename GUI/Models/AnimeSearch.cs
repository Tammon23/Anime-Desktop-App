using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MyAnimeList;
using MyAnimeList.ResponseObjects.General;

namespace GUI.Models;

public class AnimeSearch
{
    private static HttpClient _sHttpClient = new();
    public static bool ClientInitialized;

    public AnimeSearch(Node searchResult)
    {
        SearchResult = searchResult;
    }


    public Node SearchResult { get; set; }

    private string CachePath => $"./Cache/Anime/{SearchResult.Id}-Art";

    public async Task SaveAsync()
    {
        if (!Directory.Exists("./Cache")) Directory.CreateDirectory("./Cache");

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
        if (!Directory.Exists("./Cache")) Directory.CreateDirectory("./Cache");

        var results = new List<AnimeSearch>();

        foreach (var file in Directory.EnumerateFiles("./Cache"))
        {
            if (!string.IsNullOrWhiteSpace(new DirectoryInfo(file).Extension)) continue;

            await using var fs = File.OpenRead(file);
            results.Add(await LoadFromStreamAsync(fs).ConfigureAwait(false));
        }

        return results;
    }

    // Change to enum this class
    public static async Task<IEnumerable<AnimeSearch>> SearchAsync(string searchString, int offset = 0, int limit = 100,
        string fields = "")
    {
        if (!ClientInitialized)
        {
            await MALRequestClient.Init();
            ClientInitialized = true;
        }

        var r = await Anime.GetAnime(searchString);

        return r == null ? new List<AnimeSearch>() : r.Data.Select(node => new AnimeSearch(node.Node)).ToList();
    }
}