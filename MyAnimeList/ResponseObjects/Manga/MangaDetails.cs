using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Manga;

[DataContract]
public class MangaDetails
{
    public MangaDetails(
        int id,
        string title,
        Picture mainPicture,
        AlternativeTitles alternativeTitles,
        DateTime startDate,
        DateTime endDate,
        string synopsis,
        double mean,
        int rank,
        int popularity,
        int numListUsers,
        int numScoringUsers,
        string nsfw,
        DateTime createdAt,
        DateTime updatedAt,
        string mediaType,
        string status,
        List<Genre>? genres,
        MyMangaListStatus myListStatus,
        int numVolumes,
        int numChapters,
        List<Author>? authors,
        List<Picture>? pictures,
        string background,
        List<RelatedAnime>? relatedAnime,
        List<RelatedManga>? relatedManga,
        List<Recommendation>? recommendations,
        List<Serialization>? serialization
    )
    {
        Id = id;
        Title = title;
        MainPicture = mainPicture;
        AlternativeTitles = alternativeTitles;
        StartDate = startDate;
        EndDate = endDate;
        Synopsis = synopsis;
        Mean = mean;
        Rank = rank;
        Popularity = popularity;
        NumListUsers = numListUsers;
        NumScoringUsers = numScoringUsers;
        Nsfw = nsfw;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        MediaType = mediaType;
        Status = status;
        Genres = genres;
        MyListStatus = myListStatus;
        NumVolumes = numVolumes;
        NumChapters = numChapters;
        Authors = authors;
        Pictures = pictures;
        Background = background;
        RelatedAnime = relatedAnime;
        RelatedManga = relatedManga;
        Recommendations = recommendations;
        Serialization = serialization;
    }

    [DataMember] public int Id { get; }

    [DataMember] public string Title { get; }

    [DataMember(Name = "main_picture")] public Picture MainPicture { get; }

    [DataMember(Name = "alternative_titles")]
    public AlternativeTitles AlternativeTitles { get; }

    [DataMember(Name = "start_date")] public DateTime StartDate { get; }

    [DataMember(Name = "end_date")] public DateTime EndDate { get; }

    [DataMember] public string Synopsis { get; }

    [DataMember] public double Mean { get; }

    [DataMember] public int Rank { get; }

    [DataMember] public int Popularity { get; }

    [DataMember(Name = "num_list_users")] public int NumListUsers { get; }

    [DataMember(Name = "num_scoring_users")]
    public int NumScoringUsers { get; }

    [DataMember] public string Nsfw { get; }

    [DataMember(Name = "created_at")] public DateTime CreatedAt { get; }

    [DataMember(Name = "updated_at")] public DateTime UpdatedAt { get; }

    [DataMember(Name = "media_type")] public string MediaType { get; }

    [DataMember] public string Status { get; }

    [DataMember(Name = "genres")] public IReadOnlyList<Genre>? Genres { get; }

    [DataMember(Name = "my_list_status")] public MyMangaListStatus MyListStatus { get; }

    [DataMember(Name = "num_volumes")] public int NumVolumes { get; }

    [DataMember(Name = "num_chapters")] public int NumChapters { get; }

    [DataMember] public IReadOnlyList<Author>? Authors { get; }

    [DataMember] public IReadOnlyList<Picture>? Pictures { get; }

    [DataMember] public string Background { get; }

    [DataMember(Name = "related_anime")] public IReadOnlyList<RelatedAnime>? RelatedAnime { get; }

    [DataMember(Name = "related_manga")] public IReadOnlyList<RelatedManga>? RelatedManga { get; }

    [DataMember] public IReadOnlyList<Recommendation>? Recommendations { get; }

    [DataMember] public IReadOnlyList<Serialization>? Serialization { get; }

    public override string ToString()
    {
        return $"Id: {Id}, "
               + $"Title: {Title}, "
               + $"Main Picture: {MainPicture}, "
               + $"Alt Titles: {AlternativeTitles}, "
               + $"Start Date: {StartDate}, "
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
               + $"Media Type: {MediaType}"
               + $"Status: {Status}, "
               + $"Genres: {(Genres != null ? string.Join(" | ", Genres) : "N/A")}, "
               + $"My List Status: {MyListStatus}, "
               + $"Number of Volumes: {NumVolumes}, "
               + $"Number of Chapters: {NumChapters}, "
               + $"Authors: {(Authors != null ? "(" + string.Join(" | ", Authors) + ")" : "N/A")}, "
               + $"Pictures: {(Pictures != null ? string.Join(" | ", Pictures) : "N/A")}, "
               + $"Background: {Background}, "
               + $"Related Anime: {(RelatedAnime != null ? string.Join(" | ", RelatedAnime) : "N/A")}, "
               + $"Related Manga: {(RelatedManga != null ? string.Join(" | ", RelatedManga) : "N/A")}, "
               + $"Recommendations: {(Recommendations != null ? string.Join(" | ", Recommendations) : "N/A")}, "
               + $"Serialization: {(Serialization != null ? string.Join(" | ", Serialization) : "N/A")}"
            ;
    }
}