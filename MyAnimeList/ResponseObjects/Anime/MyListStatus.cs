using System;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class MyListStatus
    {
        public MyListStatus(StatusEnum status, int score, int numEpisodesWatched, bool isRewatching, DateTime updatedAt)
        {
            this.Status = status;
            this.Score = score;
            this.NumEpisodesWatched = numEpisodesWatched;
            this.IsRewatching = isRewatching;
            this.UpdatedAt = updatedAt;
        }

        [DataMember]
        public StatusEnum? Status { get; }

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
            return $"Status: {Status}, "
                   + $"Score: {Score}, "
                   + $"Number of Epsiodes Watched: {NumEpisodesWatched}, "
                   + $"Is Rewatching: {IsRewatching}, "
                   + $"Updated At: {UpdatedAt}"
                ;
        }
    }
}