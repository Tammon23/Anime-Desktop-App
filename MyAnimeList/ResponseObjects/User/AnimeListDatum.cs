using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Anime;

namespace MyAnimeList.ResponseObjects.User
{
    [DataContract]
    public class AnimeListDatum
    {
        public AnimeListDatum(Node node, ListStatus listStatus)
        {
            this.Node = node;
            this.ListStatus = listStatus;
        }

        [DataMember]
        public Node Node { get; }

        [DataMember(Name = "list_status")]
        public ListStatus ListStatus { get; }

        public override string ToString()
        {
            return $"Node: {Node}, "
                    + $"List Status: {ListStatus}"
                ;
        }
    }
}