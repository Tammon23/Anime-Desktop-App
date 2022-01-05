using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum
{
    [DataContract]
    public class Subboard
    {
        public Subboard(int id, string title)
        {
            this.Id = id;
            this.Title = title;
        }

        [DataMember]
        public int Id { get; }

        [DataMember]
        public string Title { get; }

        public override string ToString()
        {
            return $"Id: {Id}, "
                    + $"Title: {Title}"
                ;
        }
    }
}