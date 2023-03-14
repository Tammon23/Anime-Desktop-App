using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Manga;

[DataContract]
public class RelatedManga
{
    public RelatedManga(MangaDetailsNode node, string relationType, string relationTypeFormatted)
    {
        Node = node;
        RelationType = relationType;
        RelationTypeFormatted = relationTypeFormatted;
    }

    [DataMember] public MangaDetailsNode Node { get; }

    [DataMember(Name = "relation_type")] public string RelationType { get; }

    [DataMember(Name = "relation_type_formatted")]
    public string RelationTypeFormatted { get; }

    public override string ToString()
    {
        return $"Node: {Node}"
               + $", Relation Type: {RelationType}"
               + $", Relation Type : {RelationTypeFormatted}"
            ;
    }
}