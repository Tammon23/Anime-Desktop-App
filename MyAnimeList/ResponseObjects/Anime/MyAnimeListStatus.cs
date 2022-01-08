using System;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class MyAnimeListStatus
    {
        public MyAnimeListStatus(AnimeStatusEnum animeStatus, int score, int numEpisodesWatched, bool isRewatching, DateTime updatedAt)
        {
            this.AnimeStatus = animeStatus;
            this.Score = score;
            this.NumEpisodesWatched = numEpisodesWatched;
            this.IsRewatching = isRewatching;
            this.UpdatedAt = updatedAt;
        }

        [DataMember]
        public AnimeStatusEnum? AnimeStatus { get; }

        [DataMember]
        public int Score { get; }

        [DataMember(Name = "num_episodes_watched")]
        public int NumEpisodesWatched { get; }

        [DataMember(Name = "is_rewatching")]
        public bool IsRewatching { get; }

        [DataMember(Name = "updated_at")]
        public DateTime UpdatedAt { get; }
        
        public override string ToString()
        {
            return $"Status: {AnimeStatus}, "
                   + $"Score: {Score}, "
                   + $"Number of Episodes Watched: {NumEpisodesWatched}, "
                   + $"Is Rewatching: {IsRewatching}, "
                   + $"Updated At: {UpdatedAt}"
                ;
        }
    }
}