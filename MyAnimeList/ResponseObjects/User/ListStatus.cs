using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Anime;

namespace MyAnimeList.ResponseObjects.User;

[DataContract]
public class ListStatus
{
    public ListStatus(AnimeStatusEnum animeStatus, int score, int numEpisodesWatched, bool isRewatching,
        DateTime updatedAt, DateTime startDate)
    {
        AnimeStatus = animeStatus;
        Score = score;
        NumEpisodesWatched = numEpisodesWatched;
        IsRewatching = isRewatching;
        UpdatedAt = updatedAt;
        StartDate = startDate;
    }

    [DataMember] public AnimeStatusEnum AnimeStatus { get; }

    [DataMember] public int Score { get; }

    [DataMember(Name = "num_episodes_watched")]
    public int NumEpisodesWatched { get; }

    [DataMember(Name = "is_rewatching")] public bool IsRewatching { get; }

    [DataMember(Name = "updated_at")] public DateTime UpdatedAt { get; }

    [DataMember(Name = "start_date")] public DateTime StartDate { get; }


    public override string ToString()
    {
        return $"Status: {AnimeStatus}, "
               + $"Score: {Score}, "
               + $"Number of Episodes Watched: {NumEpisodesWatched}, "
               + $"Is Rewatching: {IsRewatching}, "
               + $"Updated At: {UpdatedAt}, "
               + $"Start Date: {StartDate}";
    }
}