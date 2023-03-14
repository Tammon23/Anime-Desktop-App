using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Manga;

[DataContract]
public class Serialization
{
    public Serialization(Node node, string role)
    {
        Node = node;
        Role = role;
    }

    [DataMember] public Node Node { get; }

    [DataMember] public string Role { get; }

    public override string ToString()
    {
        return $"Node: {Node}"
               + $"Role: {Role} "
            ;
    }
}