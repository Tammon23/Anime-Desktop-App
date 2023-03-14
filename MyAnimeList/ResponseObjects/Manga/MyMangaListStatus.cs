using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Manga;

[DataContract]
public class MyMangaListStatus
{
    public MyMangaListStatus(MangaStatusEnum status, bool isRereading, int numVolumesRead, int numChaptersRead,
        int score, DateTime updatedAt)
    {
        Status = status;
        IsRereading = isRereading;
        NumVolumesRead = numVolumesRead;
        NumChaptersRead = numChaptersRead;
        Score = score;
        UpdatedAt = updatedAt;
    }

    [DataMember] public MangaStatusEnum Status { get; }

    [DataMember(Name = "is_rereading")] public bool IsRereading { get; }

    [DataMember(Name = "num_volumes_read")]
    public int NumVolumesRead { get; }

    [DataMember(Name = "num_chapters_read")]
    public int NumChaptersRead { get; }

    [DataMember] public int Score { get; }

    [DataMember(Name = "updated_at")] public DateTime UpdatedAt { get; }

    public override string ToString()
    {
        return $"Status: {Status}, "
               + $"Is Rereading: {IsRereading}, "
               + $"Number of Volumes read: {NumVolumesRead}, "
               + $"Number of Chapters read: {NumChaptersRead}, "
               + $"Score: {Score}, "
               + $"Updated At: {UpdatedAt}"
            ;
    }
}