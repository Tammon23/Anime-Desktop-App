using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General;

[DataContract]
public class Node
{
    public Node(int id, string title, Picture mainPicture)
    {
        Id = id;
        Title = title;
        MainPicture = mainPicture;
    }

    [DataMember] public int Id { get; }

    [DataMember] public string Title { get; }

    [DataMember(Name = "main_picture")] public Picture MainPicture { get; }

    public override string ToString()
    {
        return $"Id: {Id}, "
               + $"Title: {Title}, "
               + $"Picture Links: {MainPicture}"
            ;
    }
}