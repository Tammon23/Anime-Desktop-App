using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;
using MyAnimeList.ResponseObjects.User;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class Datum
    {
        public Datum(Node node, ListStatus? listStatus)
        {
            this.Node = node;
            this.ListStatus = listStatus;
        }

        [DataMember]
        public Node Node { get; }
        
        [DataMember(Name = "list_status")]
        public ListStatus? ListStatus { get; }
        
        public override string ToString()
        {
            return $"Node: {Node}, "
                    + $"List Status: {ListStatus}"
                ;
        }
    }
}