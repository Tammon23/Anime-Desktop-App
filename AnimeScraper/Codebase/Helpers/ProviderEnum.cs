namespace AnimeScraper.Codebase.Helpers;

public enum ProviderEnum
{
    [Provider("https://9anime.to/", "filter", "", "https://9anime.id/")]
    NINEANIME,
    [Provider("https://allanime.site/", "graphql")]
    ALLANIME,
    [Provider("https://animekaizoku.com/")]
    ANIMEKAIZOKU,
    [Provider("https://animeout.xyz/")]
    ANIMEOUT,
    [Provider("https://animepahe.com/", "api", "anime/")]
    ANIMEPAHE,
    [Provider("https://animeonsen.xyz/")]
    ANIMEONSEN,
    [Provider("https://animexin.xyz/")]
    ANIMEXIN,
    [Provider("https://animixplay.to/", "https://cachecow.eu/api/search")]
    ANIMIXPLAY,
    [Provider("https://animtime.com/")]
    ANIMTIME,
    [Provider("http://www.crunchyroll.com/", "ajax/?req=RpcApiSearch_GetSearchCandidates")]
    CRUNCHYROLL,
    [Provider("https://kawaiifu.com/", "search-movie")]
    KAWAIIFU,
    [Provider("https://gogoanime.cm/", "search.html")]
    GOGOANIME,
    [Provider("https://haho.moe/")]
    HAHO,
    [Provider("https://hentaistream.moe/")]
    HENTAISTREAM,
    [Provider("https://api-kamyroll.herokuapp.com/")]
    KAMYROLL_API,
    [Provider("https://tenshi.moe/", "anime")]
    TENSHI,
    [Provider("https://nyaa.si/")]
    NYAASI,
    [Provider("https://twist.moe/", "https://api.twist.moe/api/anime", "a/")]
    TWIST,
    [Provider("https://zoro.to/", "search")]
    ZORO
}
