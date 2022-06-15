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

    public static void ThrowIfBadEpisodeNumber(uint episode)
    {
        if (episode == 0)
            throw new EpisodeOutOfRangeException(
                "First episode index should not be 0, if you think this is incorrect, submit an issue here " +
                "https://github.com/Tammon23/Anime-Desktop-App");
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