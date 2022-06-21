using System.Text.RegularExpressions;
using AnimeScraper.Codebase.Helpers.Exceptions;

namespace AnimeScraper.Codebase.Helpers;

public class Helper
{
    public Helper() {}

    public Regex ConstructSiteBasedRegex(string SiteUrl, string Extra = "", string ExtraRegex = "")
    {
        Regex rx = new Regex("");
        return rx;
    }

    public string AppendProtocol(string uri, string protocol = "https")
    {
        string result = "";
        return result;
    }

    /// <summary>
    /// Checks if the provided episode is within a correct range
    /// </summary>
    /// <param name="episode">The episode number to check</param>
    /// <param name="throwOnNegative1">Determines if -1 should throw an exception, default true</param>
    /// <exception cref="EpisodeOutOfRangeException"></exception>
    public static void ThrowIfBadEpisodeNumber(int episode, bool throwOnNegative1=true)
    {
        if (throwOnNegative1)
        {
            if (episode <= 0)
                throw new EpisodeOutOfRangeException(
                    "First episode index should not be 0, if you think this is incorrect, submit an issue here " +
                    "https://github.com/Tammon23/Anime-Desktop-App");
        }
        else if (episode <= 0 && episode != -1)
        {
            throw new EpisodeOutOfRangeException(
                "First episode index should not be 0, if you think this is incorrect, submit an issue here " +
                "https://github.com/Tammon23/Anime-Desktop-App");
        }
    }
    
    /// <summary>
    /// Checks if the provided range is a valid range
    /// </summary>
    /// <param name="lower">The first value of the range</param>
    /// <param name="upper">The last value of the range</param>
    /// <param name="continuousLowerRange">If set to true, then when param "upper" is set to -1, it is treated
    /// as a range with no upper bound</param>
    /// <exception cref="InvalidRangeException"></exception>
    public static void ThrowIfBadEpisodeRange(int lower, int upper, bool continuousLowerRange=false)
    {
        if (continuousLowerRange)
        {
            if (lower > upper && upper != -1)
                throw new InvalidRangeException("Invalid range provided");
        }
        else if (lower > upper)
        {
            throw new InvalidRangeException("Invalid range provided");
        }

    }
    
    public static bool IsValidUrl(string url, ProviderEnum provider)
    {
        return url.StartsWith(provider.GetBaseUri());
    }

    public static double CalculatePage(int episode, float episodesPerPage)
    {
        return Math.Ceiling((episode - 1) / episodesPerPage);
    }

    /*public static IProvider GetFromProviderEnum(ProviderEnum provider)
    {
        switch (provider)
        {
            case ProviderEnum.ALLANIME:
                return AllAnime.GetProvider();
            case ProviderEnum.NINEANIME:
                break;
            case ProviderEnum.ANIMEKAIZOKU:
                break;
            case ProviderEnum.ANIMEOUT:
                break;
            case ProviderEnum.ANIMEPAHE:
                break;
            case ProviderEnum.ANIMEONSEN:
                break;
            case ProviderEnum.ANIMEXIN:
                break;
            case ProviderEnum.ANIMIXPLAY:
                break;
            case ProviderEnum.ANIMTIME:
                break;
            case ProviderEnum.CRUNCHYROLL:
                break;
            case ProviderEnum.KAWAIIFU:
                break;
            case ProviderEnum.GOGOANIME:
                break;
            case ProviderEnum.HAHO:
                break;
            case ProviderEnum.HENTAISTREAM:
                break;
            case ProviderEnum.KAMYROLL_API:
                break;
            case ProviderEnum.TENSHI:
                break;
            case ProviderEnum.NYAASI:
                break;
            case ProviderEnum.TWIST:
                break;
            case ProviderEnum.ZORO:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(provider), provider, null);
        }
    }*/

}