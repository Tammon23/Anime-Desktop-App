using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.Manga;
using MyAnimeList.ResponseObjects.User.MyAnimeList.ResponseObjects.User;


namespace MyAnimeList
{
    public class Util
    {
        public static string AnimeRankingTypeToString(AnimeRankingTypeEnum? rankingType) => rankingType switch
        {
            AnimeRankingTypeEnum.Airing       => "airing",
            AnimeRankingTypeEnum.All          => "all",
            AnimeRankingTypeEnum.Favorite     => "favorite",
            AnimeRankingTypeEnum.Movie        => "movie",
            AnimeRankingTypeEnum.Ova          => "ova",
            AnimeRankingTypeEnum.Special      => "special",
            AnimeRankingTypeEnum.Tv           => "tv",
            AnimeRankingTypeEnum.Upcoming     => "upcoming",
            AnimeRankingTypeEnum.ByPopularity => "bypopularity",
            _                            => "all"
        };

        public static string SeasonToString(SeasonEnum? season) => season switch
        {
            SeasonEnum.Winter => "winter",
            SeasonEnum.Spring => "spring",
            SeasonEnum.Summer => "summer",
            SeasonEnum.Fall   => "fall",
            _                 => "winter"
        };

        public static string StatusToString(AnimeStatusEnum? status) => status switch
        {
            AnimeStatusEnum.Completed   => "completed",
            AnimeStatusEnum.Dropped     => "dropped",
            AnimeStatusEnum.Watching    => "watching",
            AnimeStatusEnum.OnHold      => "on_hold",
            AnimeStatusEnum.PlanToWatch => "plan_to_watch",
            _                      => "plan_to_watch"
        };
        
        public static string SortToString(SortEnum? status) => status switch
        {
            SortEnum.ListScore      => "list_score",
            SortEnum.ListUpdatedAt  => "list_updated_at",
            SortEnum.AnimeTitle     => "anime_title",
            SortEnum.AnimeStartDate => "anime_start_date",
            SortEnum.AnimeId        => "anime_id ",
            _                       => "anime_title"
        };
        
        public static string MangaRankingTypeToString(MangaRankingTypeEnum? status) => status switch
        {
            MangaRankingTypeEnum.All          => "all",
            MangaRankingTypeEnum.Doujin       => "doujin",
            MangaRankingTypeEnum.Favorite     => "favorite",
            MangaRankingTypeEnum.Manga        => "manga",
            MangaRankingTypeEnum.Manhua       => "manhua",
            MangaRankingTypeEnum.Manhwa       => "manhwa",
            MangaRankingTypeEnum.Novels       => "novels",
            MangaRankingTypeEnum.ByPopularity => "bypopularity",
            MangaRankingTypeEnum.OneShots     => "oneshot",
            _                                 => "all"
        };
    }
}

