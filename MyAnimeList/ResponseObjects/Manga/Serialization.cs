using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Manga;

[DataContract]
public class Serialization
{
    public Serialization(SerializationNode node, string role)
    {
        Node = node;
        Role = role;
    }

    [DataMember] public SerializationNode Node { get; }

    [DataMember] public string Role { get; }

    public override string ToString()
    {
        return $"Node: {Node}"
               + $"Role: {Role} "
            ;
    }
}