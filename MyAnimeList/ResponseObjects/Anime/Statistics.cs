using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class Statistics
    {
        public Statistics(Status status, int numListUsers)
        {
            this.Status = status;
            this.NumListUsers = numListUsers;
        }

        [DataMember]
        public Status Status { get; }

        [DataMember(Name = "num_list_users")]
        public int NumListUsers { get; }
        
        public override string ToString()
        {
            return $"Status: {Status}, "
                   + $"Number of List Users: {NumListUsers}"
                ;
        }
    }

}