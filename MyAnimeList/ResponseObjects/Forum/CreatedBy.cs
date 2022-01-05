using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum
{
    [DataContract]
    public class CreatedBy
    {
        public CreatedBy(string id, string name, string forumAvator)
        {
            this.Id = id;
            this.Name = name;
            this.ForumAvator = forumAvator;
        }

        [DataMember]
        public string Id { get; }

        [DataMember]
        public string Name { get; }

        [DataMember(Name="forum_avator")]
        public string ForumAvator { get; }

        public override string ToString()
        {
            return $"Id: {Id}, "
                   + $"Name: {Name}, "
                   + $"Forum Avator: {ForumAvator}"
                ;
        }
    }
}