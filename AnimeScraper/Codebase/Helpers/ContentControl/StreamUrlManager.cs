using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using static System.Int16;

namespace AnimeScraper.Codebase.Helpers;

public class StreamUrlManager
{
    private readonly List<StreamUrl> _mrls;
    public readonly SubDubTitle Type;
    public StreamUrlManager(SubDubTitle type = SubDubTitle.Subbed, params StreamUrl[] mrls)
    {
        Type = type;
        _mrls = mrls.ToList();
    }

    public StreamUrlManager(SubDubTitle type = SubDubTitle.Subbed)
    {
        Type = type;
        _mrls = new List<StreamUrl>();
    }

    /// <summary>
    /// Adds a new url to the manager
    /// </summary>
    /// <param name="mrl">The mrl to be added</param>
    public void AddStreamUrl(StreamUrl mrl)
    {
        _mrls.Add(mrl);
    }

    /// <summary>
    /// Adds a new url to the manager
    /// </summary>
    /// <param name="mrl">The uri that holds the multi media of the stream</param>
    /// <param name="quality">The quality of the url</param>
    /// <param name="options">The required options needed for the url to work properly</param>
    public void AddStreamUrl(string mrl, string quality, Dictionary<AnimeUrlOptionsEnum, string>? options = null)
    {
        _mrls.Add(new StreamUrl(mrl, quality, options));
    }
    
    /// <summary>
    /// Used to get the qualities 
    /// </summary>
    /// <returns></returns>
    public List<string> GetQualities()
    {
        return _mrls.Select(mrl => mrl.Quality).ToList();
    }

    /// <summary>
    /// Used to get mrl with the best quality (not a particularly good algorithm)
    /// </summary>
    /// <returns>The <see cref="StreamUrl"/> with the best quality</returns>
    public StreamUrl GetBest()
    {
        var best = MinValue;
        StreamUrl? bestMrl = null;
        
        foreach (var mrl in _mrls)
        {
            try
            {
                var quality = Parse(mrl.Quality.Replace("p", string.Empty));
                if (best < quality)
                {
                    best = quality;
                    bestMrl = mrl;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        return bestMrl ?? _mrls.Last();
    }

    /// <summary>
    /// Used to get mrl with the worst quality (not a particularly good algorithm)
    /// </summary>
    /// <returns>The <see cref="StreamUrl"/> with the worst quality</returns>
    public StreamUrl GetWorst()
    {
        var worst = MaxValue;
        StreamUrl? worstMrl = null;
        
        foreach (var mrl in _mrls)
        {
            try
            {
                var quality = Parse(mrl.Quality.Replace("p", string.Empty));
                if (worst > quality)
                {
                    worst = quality;
                    worstMrl = mrl;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        return worstMrl ?? _mrls.First();
    }

    /// <summary>
    /// Retrieves all the stream urls that the manager is responsible for
    /// </summary>
    /// <returns>A list of <see cref="StreamUrl"/>s</returns>
    public List<StreamUrl> GetAllStreamUrls()
    {
        return _mrls;
    }

    /// <summary>
    /// Using a regex pattern, formed from the provided quality, tries to get all mrls that match it 
    /// </summary>
    /// <param name="quality">The quality to search for</param>
    /// <returns>A list of matched <see cref="StreamUrl"/>s</returns>
    public List<StreamUrl> GetStreamUrlByQuality(string quality)
    {
        var result = new List<StreamUrl>();
        var qualityRegex = new Regex("[a-zA-Z]*" + quality + "[a-zA-Z]*");

        result.AddRange(_mrls.Where(mrl => qualityRegex.IsMatch(mrl.Quality)));

        return result;
    }
    
    /// <summary>
    /// Using a regex pattern, tries to get all mrls that match it 
    /// </summary>
    /// <param name="quality">The quality regex to search for</param>
    /// <returns>A list of matched <see cref="StreamUrl"/>s</returns>
    public List<StreamUrl> GetStreamUrlByQuality(Regex quality)
    {
        var result = new List<StreamUrl>();
        result.AddRange(_mrls.Where(mrl => quality.IsMatch(mrl.Quality)));

        return result;
    }

    public override string ToString()
    {
        if (_mrls.Count == 0)
            return " { } ";

        var size = 0;
        var sb = new StringBuilder();
        sb.Append(" { ");

        foreach (var mrl in _mrls)
        {
            size++;
            if(size == _mrls.Count)
                sb.Append($"<Type: {(Type == SubDubTitle.Dubbed ? "Dubbed" : "Subbed")} Mrl: {mrl}> ");
            else
                sb.Append($"<Type: {(Type == SubDubTitle.Dubbed ? "Dubbed" : "Subbed")} Mrl: {mrl}>, ");
        }

        sb.Append(" }");
        return sb.ToString();
    }
}