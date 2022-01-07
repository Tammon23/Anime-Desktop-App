using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class AnimeDetails
    {
        public AnimeDetails(
            int id,
            string title,
            MainPicture? mainPicture,
            AlternativeTitles? alternativeTitles,
            DateTime? startDate,
            DateTime? endDate,
            string? synopsis,
            double? mean,
            int? rank,
            int? popularity,
            int? numListUsers,
            int? numScoringUsers,
            string? nsfw,
            DateTime? createdAt,
            DateTime? updatedAt,
            string? mediaType,
            string? status,
            List<Genre> genres,
            MyListStatus? myListStatus,
            int? numEpisodes,
            StartSeason? startSeason,
            Broadcast? broadcast,
            string? source,
            int? averageEpisodeDuration,
            string? rating,
            List<Picture> pictures,
            string? background,
            List<RelatedAnime> relatedAnime,
            List<object> relatedManga,
            List<Recommendation> recommendations,
            List<Studio> studios,
            Statistics? statistics
        )
        {
            this.Id = id;
            this.Title = title;
            this.MainPicture = mainPicture;
            this.AlternativeTitles = alternativeTitles;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Synopsis = synopsis;
            this.Mean = mean;
            this.Rank = rank;
            this.Popularity = popularity;
            this.NumListUsers = numListUsers;
            this.NumScoringUsers = numScoringUsers;
            this.Nsfw = nsfw;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
            this.MediaType = mediaType;
            this.Status = status;
            this.Genres = genres;
            this.MyListStatus = myListStatus;
            this.NumEpisodes = numEpisodes;
            this.StartSeason = startSeason;
            this.Broadcast = broadcast;
            this.Source = source;
            this.AverageEpisodeDuration = averageEpisodeDuration;
            this.Rating = rating;
            this.Pictures = pictures;
            this.Background = background;
            this.RelatedAnime = relatedAnime;
            this.RelatedManga = relatedManga;
            this.Recommendations = recommendations;
            this.Studios = studios;
            this.Statistics = statistics;
        }

        [DataMember]
        public int Id { get; }

        [DataMember]
        public string Title { get; }

        [DataMember(Name = "main_picture")]
        public MainPicture? MainPicture { get; }

        [DataMember(Name = "alternative_titles")]
        public AlternativeTitles? AlternativeTitles { get; }

        [DataMember(Name = "start_date")]
        public DateTime? StartDate { get; }

        [DataMember(Name = "end_date")]
        public DateTime? EndDate { get; }

        [DataMember]
        public string? Synopsis { get; }

        [DataMember]
        public double? Mean { get; }

        [DataMember]
        public int? Rank { get; }

        [DataMember]
        public int? Popularity { get; }

        [DataMember(Name ="num_list_users")]
        public int? NumListUsers { get; }

        [DataMember(Name ="num_scoring_users")]
        public int? NumScoringUsers { get; }

        [DataMember]
        public string? Nsfw { get; }

        [DataMember(Name ="created_at")]
        public DateTime? CreatedAt { get; }

        [DataMember(Name ="updated_at")]
        public DateTime? UpdatedAt { get; }

        [DataMember(Name ="media_type")]
        public string? MediaType { get; }

        [DataMember]
        public string? Status { get; }

        [DataMember]
        public IReadOnlyList<Genre>? Genres { get; }

        [DataMember(Name ="my_list_status")]
        public MyListStatus? MyListStatus { get; }

        [DataMember(Name ="num_episodes")]
        public int? NumEpisodes { get; }

        [DataMember(Name ="start_season")]
        public StartSeason? StartSeason { get; }

        [DataMember]
        public Broadcast? Broadcast { get; }

        [DataMember]
        public string? Source { get; }

        [DataMember(Name ="average_episode_duration")]
        public int? AverageEpisodeDuration { get; }

        [DataMember]
        public string? Rating { get; }

        [DataMember]
        public IReadOnlyList<Picture>? Pictures { get; }

        [DataMember]
        public string? Background { get; }

        [DataMember(Name ="related_anime")]
        public IReadOnlyList<RelatedAnime> RelatedAnime { get; }

        [DataMember(Name ="related_manga")]
        public IReadOnlyList<object>? RelatedManga { get; }

        [DataMember]
        public IReadOnlyList<Recommendation>? Recommendations { get; }

        [DataMember]
        public IReadOnlyList<Studio>? Studios { get; }

        [DataMember]
        public Statistics? Statistics { get; }

        public override string ToString()
        {
            return $"Id: {Id}, "
                   + $"Title: {Title}, "
                   + $"Main Picture: {MainPicture}, "
                   + $"Alt Titles: {AlternativeTitles}, "
                   + $"Start Date: {EndDate}, "
                   + $"End Date: {EndDate}, "
                   + $"Synopsis: {Synopsis}, "
                   + $"Mean: {Mean}, "
                   + $"Rank: {Rank}, "
                   + $"Popularity: {Popularity}, "
                   + $"Number of List Users: {NumListUsers}, "
                   + $"Number of Scoring Users: {NumScoringUsers}, "
                   + $"NSFW: {Nsfw}, "
                   + $"Created At: {CreatedAt}, "
                   + $"Updated At: {UpdatedAt}, "
                   + $"Status: {Status}, "
                   + $"Genres: {(Genres != null && Genres.Count > 0  ? string.Join(" | ", Genres) : "")}, "
                   + $"My List Status: {MyListStatus}, "
                   + $"Number of Episodes: {NumEpisodes}, "
                   + $"Start Season: {StartSeason}, "
                   + $"Broadcast: {Broadcast}, "
                   + $"Source: {Source}, "
                   + $"Average Episode Duration: {AverageEpisodeDuration}, "
                   + $"Rating: {Rating}, "
                   + $"Pictures: {(Pictures != null && Pictures.Count > 0  ? string.Join(" | ", Pictures) : "")}, "
                   + $"Background: {Background}, "
                   + $"Related Anime: {(RelatedAnime != null && RelatedAnime.Count > 0  ? string.Join(" | ", RelatedAnime) : "")}, "
                   + $"Related Manga: {(RelatedManga != null && RelatedManga.Count > 0  ? string.Join(" | ", RelatedManga) : "")}, "
                   + $"Recommendations: {(Recommendations != null && Recommendations.Count > 0  ? string.Join(" | ", Recommendations) : "")}, "
                   + $"Studios: {(Studios != null && Studios.Count > 0  ? string.Join(" | ", Studios) : "")}, "
                   + $"Statistics: {Statistics}"
                ;
        }
    }
}