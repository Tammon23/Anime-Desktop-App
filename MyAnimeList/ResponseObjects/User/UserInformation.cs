using System;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.User
{
    [DataContract]
    public class UserInformation : User
    {
        public UserInformation(int id, string name, string location, DateTime joinedAt, AnimeStatistics animeStatistics)
            : base(id, name)
        {
            this.Location = location;
            this.JoinedAt = joinedAt;
            this.AnimeStatistics = animeStatistics;
        }
        
        [DataMember]
        public string Location { get; }

        [DataMember(Name="joined_at")]
        public DateTime JoinedAt { get; }

        [DataMember(Name="anime_statistics")]
        public AnimeStatistics AnimeStatistics { get; }

        public override string ToString()
        {
            return base.ToString()
                   + $"Location: {Location}, "
                   + $"Joined At: {JoinedAt}, "
                   + $"Anime Statistics: {AnimeStatistics} stats is null {AnimeStatistics == null}"
                ;
        }
    }
}