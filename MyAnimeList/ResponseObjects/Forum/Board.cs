using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum;

[DataContract]
public class Board
{
    public Board(int id, string title, string description, List<Subboard>? subboards)
    {
        Id = id;
        Title = title;
        Description = description;
        Subboards = subboards;
    }

    [DataMember] public int Id { get; }

    [DataMember] public string Title { get; }

    [DataMember] public string Description { get; }

    [DataMember] public IReadOnlyList<Subboard>? Subboards { get; }

    public override string ToString()
    {
        return $"Id: {Id}, "
               + $"Title: {Title}, "
               + $"Description: {Description}, "
               + $"Subboards: {(Subboards != null ? string.Join(" | ", Subboards) : "N/A")}"
            ;
    }
}