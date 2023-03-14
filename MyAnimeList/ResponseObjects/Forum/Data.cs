using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum;

[DataContract]
public class Data
{
    public Data(string title, List<Post> posts, Poll poll)
    {
        Title = title;
        Posts = posts;
        Poll = poll;
    }

    [DataMember] public string Title { get; }

    [DataMember] public IReadOnlyList<Post> Posts { get; }

    [DataMember] public Poll Poll { get; }

    public override string ToString()
    {
        return $"Title: {Title}, "
               + $"Posts: {(Posts != null ? string.Join(" | ", Posts) : "N/A")}"
               + $"Posts: {Poll}"
            ;
    }
}