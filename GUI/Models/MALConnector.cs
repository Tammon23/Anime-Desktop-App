using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MyAnimeList;
using MyAnimeList.ResponseObjects.General;

namespace GUI.Models;

public class MALConnector
{
    private static HttpClient _sHttpClient = new();
    
    public MALConnector(Node node)
    {
        ContentNode = node;
    }

    public Node ContentNode { get; set; }

    private string CachePath => $"./Cache/Anime/{ContentNode.Id}-Art";

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

    private static async Task SaveToStreamAsync(MALConnector data, Stream stream)
    {
        await JsonSerializer.SerializeAsync(stream, data).ConfigureAwait(false);
    }

    public static async Task<MALConnector> LoadFromStreamAsync(Stream stream)
    {
        return (await JsonSerializer.DeserializeAsync<MALConnector>(stream).ConfigureAwait(false))!;
    }

    public static async Task<IEnumerable<MALConnector>> LoadCachedAsync()
    {
        if (!Directory.Exists("./Cache")) Directory.CreateDirectory("./Cache");

        var results = new List<MALConnector>();

        foreach (var file in Directory.EnumerateFiles("./Cache"))
        {
            if (!string.IsNullOrWhiteSpace(new DirectoryInfo(file).Extension)) continue;

            await using var fs = File.OpenRead(file);
            results.Add(await LoadFromStreamAsync(fs).ConfigureAwait(false));
        }

        return results;
    }
}