using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using MyAnimeList.ResponseObjects.Anime;

namespace MyAnimeList.ResponseObjects.User
{
    [DataContract]
    public class AnimeListStatus
    {
        public AnimeListStatus(StatusEnum status,
            int? score = null,
            int? numWatchedEpisodes = null,
            bool? isRewatching = null,
            DateTime? updatedAt = null,
            int? priority = null,
            int? numTimesRewatched = null,
            int? rewatchValue = null,
            List<string>? tags = null,
            string? comments = null)
        {
            this.Status = status;
            this.Score = score;
            this.NumWatchedEpisodes = numWatchedEpisodes;
            this.IsRewatching = isRewatching;
            this.UpdatedAt = updatedAt;
            this.Priority = priority;
            this.NumTimesRewatched = numTimesRewatched;
            this.RewatchValue = rewatchValue;
            this.Tags = tags;
            this.Comments = comments;
        }

        [DataMember]
        public StatusEnum Status { get; }

        [DataMember]
        public int? Score { get; }

        [DataMember(Name = "num_watched_episodes")]
        public int? NumWatchedEpisodes { get; }

        [DataMember(Name = "is_rewatching")]
        public bool? IsRewatching { get; }

        [DataMember(Name = "updated_at")]
        public DateTime? UpdatedAt { get; }

        [DataMember]
        public int? Priority { get; }

        [DataMember(Name ="num_times_rewatched")]
        public int? NumTimesRewatched { get; }

        [DataMember(Name ="rewatch_value")]
        public int? RewatchValue { get; }

        [DataMember]
        public List<string>? Tags { get; }

        [DataMember]
        public string? Comments { get; }

        public FormUrlEncodedContent ToFormUrlEncodedContent()
        {
            Dictionary<string, string> form = new();

            if (Status != null)             form.Add("status", Util.StatusToString(Status));
            if (Score != null)              form.Add("score", Score.ToString());
            if (NumWatchedEpisodes != null) form.Add("num_watched_episodes", NumWatchedEpisodes.ToString());
            if (IsRewatching != null)       form.Add("is_rewatching", IsRewatching.ToString());
            if (Priority != null)           form.Add("priority", Priority.ToString());
            if (NumTimesRewatched != null)  form.Add("num_times_rewatched", NumTimesRewatched.ToString());
            if (Tags != null)               form.Add("tags", JsonSerializer.Serialize(Tags));                                  // <<<< May need redoing
            if (Comments != null)           form.Add("comments", Comments);
            
            return new FormUrlEncodedContent(form);
        }
        
        public override string ToString() =>
            $"Status: {Status}, "
            + $"Score: {Score}, " 
            + $"Number of Watched Episodes: {NumWatchedEpisodes}, "
            + $"Is Rewatching: {IsRewatching}, "
            + $"Updated At: {UpdatedAt}, "
            + $"Priority: {Priority}, "
            + $"Number of Times Rewatched: {NumTimesRewatched}, "
            + $"Rewatch Value: {RewatchValue}, "
            + $"Tags: {((Tags != null) ? string.Join(" | ", Tags) : "N/A")}, "                    
            + $"Comments: {Comments}";
    }
}
