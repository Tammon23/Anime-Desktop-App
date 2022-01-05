using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum
{
    [DataContract]
    public class Category
    {
        public Category(string title, List<Board>? boards)
        {
            this.Title = title;
            this.Boards = boards;
        }

        [DataMember]
        public string Title { get; }

        [DataMember]
        public IReadOnlyList<Board>? Boards { get; }

        public override string ToString()
        {
            return $"Title: {Title}, "
                    + $"Boards: {((Boards != null) ? string.Join(" | ", Boards) : "N/A")}"
                ;
        }
    }
}