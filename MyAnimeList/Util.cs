using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.User;
using MyAnimeList.ResponseObjects.User.MyAnimeList.ResponseObjects.User;

namespace MyAnimeList
{
    public class Util
    {
        public static string RankingTypeToString(RankingTypeEnum? rankingType) => rankingType switch
        {
            RankingTypeEnum.Airing       => "airing",
            RankingTypeEnum.All          => "all",
            RankingTypeEnum.Favorite     => "favorite",
            RankingTypeEnum.Movie        => "movie",
            RankingTypeEnum.Ova          => "ova",
            RankingTypeEnum.Special      => "special",
            RankingTypeEnum.Tv           => "tv",
            RankingTypeEnum.Upcoming     => "upcoming",
            RankingTypeEnum.ByPopularity => "bypopularity",
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

        public static string StatusToString(StatusEnum? status) => status switch
        {
            StatusEnum.Completed   => "completed",
            StatusEnum.Dropped     => "dropped",
            StatusEnum.Watching    => "watching",
            StatusEnum.OnHold      => "on_hold",
            StatusEnum.PlanToWatch => "plan_to_watch",
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
    }
}

