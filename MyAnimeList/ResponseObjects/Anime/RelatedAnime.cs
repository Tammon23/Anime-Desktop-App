using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
public class RelatedAnime
{
    public RelatedAnime(AnimeNode node, string relationType, string relationTypeFormatted)
    {
        Node = node;
        RelationType = relationType;
        RelationTypeFormatted = relationTypeFormatted;
    }

    [DataMember] public AnimeNode Node { get; }

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