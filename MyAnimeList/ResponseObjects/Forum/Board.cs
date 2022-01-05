using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum
{
    [DataContract]
    public class Board
    {
        public Board(int id, string title, string description, List<Subboard>? subboards)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Subboards = subboards;
        }

        [DataMember]
        public int Id { get; }

        [DataMember]
        public string Title { get; }

        [DataMember]
        public string Description { get; }

        [DataMember]
        public IReadOnlyList<Subboard>? Subboards { get; }

        public override string ToString()
        {
            return $"Id: {Id}, "
                   + $"Title: {Title}, "
                   + $"Description: {Description}, "
                   + $"Subboards: {((Subboards != null) ? string.Join(" | ", Subboards) : "N/A")}"
                ;
        }
    }
}