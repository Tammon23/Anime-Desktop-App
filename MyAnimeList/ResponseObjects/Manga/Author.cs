using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Manga;

[DataContract]
public class Author
{
    public Author(MangaDetailsNode node, string role)
    {
        Node = node;
        Role = role;
    }

    [DataMember] public MangaDetailsNode Node { get; }

    [DataMember] public string Role { get; }

    public override string ToString()
    {
        return $"Node: {Node}"
               + $" ,Role: {Role}"
            ;
    }
}