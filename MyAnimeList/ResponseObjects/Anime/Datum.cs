using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class Datum
    {
        public Datum(Node node)
        {
            Node = node;
        }

        [DataMember]
        public Node Node { get; }

        public override string ToString()
        {
            return $"Node: {Node}";
        }
    }
}