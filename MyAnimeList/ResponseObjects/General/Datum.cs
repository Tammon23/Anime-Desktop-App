using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General
{
    public class Datum
    {
        public Datum(Node node)
        {
            this.Node = node;
        }

        [DataMember]
        public Node Node { get; }

        public override string ToString()
        {
            return $"Node: {Node}";
        }
    }
}