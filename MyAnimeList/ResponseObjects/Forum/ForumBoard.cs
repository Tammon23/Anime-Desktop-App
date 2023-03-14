using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum;

[DataContract]
public class ForumBoard
{
    public ForumBoard(List<Category>? categories)
    {
        Categories = categories;
    }

    [DataMember] public IReadOnlyList<Category>? Categories { get; }

    public override string ToString()
    {
        return $"Categories: {(Categories != null ? string.Join(" | ", Categories) : "N/A")}";
    }
}