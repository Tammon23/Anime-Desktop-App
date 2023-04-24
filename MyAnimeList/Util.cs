using System.ComponentModel;
using System.Reflection;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;
using MyAnimeList.ResponseObjects.Manga;
using MyAnimeList.ResponseObjects.User.MyAnimeList.ResponseObjects.User;

namespace MyAnimeList;

public class Util
{
    public static string AnimeRankingTypeToString(AnimeRankingTypeEnum? rankingType)
    {
        return rankingType switch
        {
            AnimeRankingTypeEnum.Airing => "airing",
            AnimeRankingTypeEnum.All => "all",
            AnimeRankingTypeEnum.Favorite => "favorite",
            AnimeRankingTypeEnum.Movie => "movie",
            AnimeRankingTypeEnum.Ova => "ova",
            AnimeRankingTypeEnum.Special => "special",
            AnimeRankingTypeEnum.Tv => "tv",
            AnimeRankingTypeEnum.Upcoming => "upcoming",
            AnimeRankingTypeEnum.ByPopularity => "bypopularity",
            _ => "all"
        };
    }

    public static string SeasonToString(SeasonEnum? season)
    {
        return season switch
        {
            SeasonEnum.Winter => "winter",
            SeasonEnum.Spring => "spring",
            SeasonEnum.Summer => "summer",
            SeasonEnum.Fall => "fall",
            _ => "winter"
        };
    }

    public static string StatusToString(AnimeStatusEnum? status)
    {
        return status switch
        {
            AnimeStatusEnum.Completed => "completed",
            AnimeStatusEnum.Dropped => "dropped",
            AnimeStatusEnum.Watching => "watching",
            AnimeStatusEnum.OnHold => "on_hold",
            AnimeStatusEnum.PlanToWatch => "plan_to_watch",
            _ => "plan_to_watch"
        };
    }

    public static string SortToString(SortEnum? status)
    {
        return status switch
        {
            SortEnum.ListScore => "list_score",
            SortEnum.ListUpdatedAt => "list_updated_at",
            SortEnum.AnimeTitle => "anime_title",
            SortEnum.AnimeStartDate => "anime_start_date",
            SortEnum.AnimeId => "anime_id ",
            _ => "anime_title"
        };
    }

    public static string MangaRankingTypeToString(MangaRankingTypeEnum? status)
    {
        return status switch
        {
            MangaRankingTypeEnum.All => "all",
            MangaRankingTypeEnum.Doujin => "doujin",
            MangaRankingTypeEnum.Favorite => "favorite",
            MangaRankingTypeEnum.Manga => "manga",
            MangaRankingTypeEnum.Manhua => "manhua",
            MangaRankingTypeEnum.Manhwa => "manhwa",
            MangaRankingTypeEnum.Novels => "novels",
            MangaRankingTypeEnum.ByPopularity => "bypopularity",
            MangaRankingTypeEnum.OneShots => "oneshot",
            _ => "all"
        };
    }

    public static string NsfwToString(NsfwEnum? nsfw)
    {
        return nsfw switch
        {
            NsfwEnum.White => "white",
            NsfwEnum.Black => "black",
            NsfwEnum.Gray => "gray",
            _ => "unknown"
        };
    }

    public static string RatingToString(RatingEnum? rating)
    {
        return rating switch
        {
            RatingEnum.G => "g",
            RatingEnum.Pg => "pg",
            RatingEnum.Pg13 => "pg_13",
            RatingEnum.R => "r",
            RatingEnum.RPlus => "r+",
            RatingEnum.Rx => "rx",
            _ => "unknown"
        };
    }

    public static string MediaTypeToString(AnimeMediaType? mediaType)
    {
        return mediaType switch
        {
            AnimeMediaType.Tv => "TV",
            AnimeMediaType.Ova => "OVA",
            AnimeMediaType.Movie => "Movie",
            AnimeMediaType.Special => "Special",
            AnimeMediaType.Ona => "ONA",
            AnimeMediaType.Music => "Music",
            _ => "Unknown"
        };
    }

    public static string AiringStatusToString(AnimeAiringStatusEnum? airingStatus)
    {
        return airingStatus switch
        {
            AnimeAiringStatusEnum.FinishedAiring => "Finished Airing",
            AnimeAiringStatusEnum.CurrentlyAiring => "Currently Airing",
            AnimeAiringStatusEnum.NotYetAired => "Not Yet Aired",
            _ => "Unknown"
        };
    }
    
    public static string GetEnumDescription(Enum value)
    {
        return
            value
                .GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description
            ?? value.ToString();        
    }
    
    
}