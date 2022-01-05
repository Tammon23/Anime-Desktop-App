using System;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum
{
    [DataContract]
    public class ForumTopicDatum
    {
        public ForumTopicDatum(int id,
            string title,
            DateTime createdAt,
            User.User createdBy,
            int numberOfPosts,
            DateTime lastPostCreatedAt,
            User.User lastPostCreatedBy,
            bool isLocked)
        {
            this.Id = id;
            this.Title = title;
            this.CreatedAt = createdAt;
            this.CreatedBy = createdBy;
            this.NumberOfPosts = numberOfPosts;
            this.LastPostCreatedAt = lastPostCreatedAt;
            this.LastPostCreatedBy = lastPostCreatedBy;
            this.IsLocked = isLocked;
        }

        [DataMember]
        public int Id { get; }

        [DataMember]
        public string Title { get; }

        [DataMember(Name="created_at")]
        public DateTime CreatedAt { get; }

        [DataMember(Name="created_by")]
        public User.User CreatedBy { get; }

        [DataMember(Name="number_of_posts")]
        public int NumberOfPosts { get; }

        [DataMember(Name="last_post_created_at")]
        public DateTime LastPostCreatedAt { get; }

        [DataMember(Name="last_post_created_by")]
        public User.User LastPostCreatedBy { get; }

        [DataMember(Name="is_locked")]
        public bool IsLocked { get; }

        public override string ToString()
        {
            return $"Id: {Id}, "
                   + $"Title: {Title}, "
                   + $"Created At: {CreatedAt}, "
                   + $"Created By: {CreatedBy}, " 
                   + $"Number of Posts: {NumberOfPosts}, "
                   + $"Last Post Created At: {LastPostCreatedAt}, "
                   + $"Last Post Created By: {LastPostCreatedBy}, "
                   + $"IsLocked: {IsLocked}"
                ;
        }
    }
}