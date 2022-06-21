using System.Text;

namespace AnimeScraper.Codebase.Helpers;

/// <summary>
/// A class that holds the stream links of an anime as well as the requires requests options
/// </summary>
public class StreamUrl
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mrl">The uri that holds the multi media of the stream</param>
    /// <param name="quality">The quality of the url</param>
    /// <param name="options">The required options needed for the url to work properly</param>
    public StreamUrl(string mrl, string quality, Dictionary<AnimeUrlOptionsEnum, string>? options = null)
    {
        MRL = mrl;
        Quality = quality;
        Options = options;
    }
    
    public string MRL { get;  }
    public string Quality { get;  }
    public Dictionary<AnimeUrlOptionsEnum, string>? Options { get; }

    public override string ToString()
    {
        return $" {{ Mrl: {MRL}, Quality: {Quality}, OptionCount: {Options.Count} }} ";
    }
}
 