using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class RelatedAnime
    {
        public RelatedAnime(Node node, string relationType, string relationTypeFormatted)
        {
            this.Node = node;
            this.RelationType = relationType;
            this.RelationTypeFormatted = relationTypeFormatted;
        }

        [DataMember]
        public Node Node { get; }

        [DataMember(Name = "relation_type")]
        public string RelationType { get; }

        [DataMember(Name = "relation_type_formatted")]
        public string RelationTypeFormatted { get; }
        
        public override string ToString()
        {
            return $"Node: {Node}, "
                   + $"Relation Type: {RelationType}, "
                   + $"Related Type Formatted: {RelationTypeFormatted}"
                ;
        }
    }
}