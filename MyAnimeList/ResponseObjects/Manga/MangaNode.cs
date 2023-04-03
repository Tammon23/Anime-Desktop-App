using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Manga;

public class MangaNode : Node
{
    public MangaNode(
        int id,
        string? title,
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
        List<Genre> genres,
        DateTime? createdAt,
        DateTime? updatedAt,
        string? mediaType,
        MangaPublishedStatusEnum? status,
        MyMangaListStatus? myListStatus,
        int numVolumes,
        int numChapters,
        List<Author> authors
    ) : base(id, title ?? "", mainPicture ?? new Picture("", ""))

    {
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
        NumOfVolumes = numVolumes;
        NumOfChapters = numChapters;
        Authors = authors;
    }

    [DataMember(Name = "alternative_titles")]
    public AlternativeTitles? AlternativeTitles { get; }

    [DataMember(Name = "start_date")] public DateTime? StartDate { get; }

    [DataMember(Name = "end_date")] public DateTime? EndDate { get; }

    [DataMember] public string? Synopsis { get; }

    [DataMember] public double? Mean { get; }

    [DataMember] public int? Rank { get; }

    [DataMember] public int? Popularity { get; }

    [DataMember(Name = "num_list_users")] public int? NumListUsers { get; }

    [DataMember(Name = "num_scoring_users")]
    public int? NumScoringUsers { get; }

    [DataMember] public NsfwEnum? Nsfw { get; }

    [DataMember(Name = "created_at")] public DateTime? CreatedAt { get; }

    [DataMember(Name = "updated_at")] public DateTime? UpdatedAt { get; }

    [DataMember(Name = "media_type")] public string? MediaType { get; }

    [DataMember] public MangaPublishedStatusEnum? Status { get; }

    [DataMember] public IReadOnlyList<Genre>? Genres { get; }

    [DataMember(Name = "my_list_status")] public MyMangaListStatus? MyListStatus { get; }

    [DataMember(Name = "num_volumes")] public int? NumOfVolumes { get; }
    [DataMember(Name = "num_chapters")] public int? NumOfChapters { get; }
    [DataMember] public IReadOnlyList<Author>? Authors { get; }

    public override string ToString()
    {
        return base.ToString()
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
               + $"Status: {Status}, "
               + $"Genres: {(Genres is {Count: > 0} ? string.Join(" | ", Genres) : "")}, "
               + $"My List Status: {MyListStatus}, "
               + $"Number of Volumes: {NumOfVolumes}, "
               + $"Number of Chapters: {NumOfChapters}, "
               + $"Authros: {(Authors is {Count: > 0} ? string.Join(" | ", Authors) : "")}, "
            ;
    }

}
