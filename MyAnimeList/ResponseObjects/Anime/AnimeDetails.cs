using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;
using MyAnimeList.ResponseObjects.Manga;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class AnimeDetails : AnimeNode
    {
        // details
        public AnimeDetails(
            int id,
            string title,
            Picture? mainPicture,
            AlternativeTitles? alternativeTitles,
            DateTime? startDate,
            DateTime? endDate,
            string? synopsis,
            double? mean,
            int? rank,
            int? popularity,
            int? numListUsers,
            int? numScoringUsers,
            NsfwEnum? nsfw,
            DateTime? createdAt,
            DateTime? updatedAt,
            string? mediaType,
            string? status,
            List<Genre> genres,
            MyAnimeListStatus? myListStatus,
            int? numEpisodes,
            StartSeason? startSeason,
            Broadcast? broadcast,
            string? source,
            int? averageEpisodeDuration,
            RatingEnum? rating,
            List<Picture> pictures,
            string? background,
            List<RelatedAnime> relatedAnime,
            List<RelatedManga> relatedManga,
            List<Recommendation> recommendations,
            List<Studio> studios,
            Statistics? statistics
        ) : base(id, title, mainPicture, alternativeTitles, startDate, endDate, synopsis, mean, rank, popularity, numListUsers, numScoringUsers, nsfw, genres, createdAt, updatedAt, mediaType, status, myListStatus, numEpisodes, startSeason, broadcast, averageEpisodeDuration, source, rating, studios)
        {

            this.Pictures = pictures;
            this.Background = background;
            this.RelatedAnime = relatedAnime;
            this.RelatedManga = relatedManga;
            this.Recommendations = recommendations;
            this.Statistics = statistics;
        }
        
        [DataMember]
        public IReadOnlyList<Picture> Pictures { get; }

        [DataMember]
        public string? Background { get; }

        [DataMember(Name ="related_anime")]
        public IReadOnlyList<RelatedAnime> RelatedAnime { get; }

        [DataMember(Name ="related_manga")]
        public IReadOnlyList<RelatedManga> RelatedManga { get; }

        [DataMember]
        public IReadOnlyList<Recommendation> Recommendations { get; }
        
        [DataMember]
        public Statistics? Statistics { get; }

        public override string ToString()
        {
            return base.ToString()
                   + $"Pictures: {(Pictures.Count > 0  ? string.Join(" | ", Pictures) : "")}, "
                   + $"Background: {Background}, "
                   + $"Related Anime: {(RelatedAnime.Count > 0  ? string.Join(" | ", RelatedAnime) : "")}, "
                   + $"Related Manga: {(RelatedManga.Count > 0  ? string.Join(" | ", RelatedManga) : "")}, "
                   + $"Recommendations: {(Recommendations.Count > 0  ? string.Join(" | ", Recommendations) : "")}, "
                   + $"Statistics: {Statistics}"
                ;
        }
    }
}