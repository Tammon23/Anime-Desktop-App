using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
public class AnimeListNode : Node
{
    public AnimeListNode(int id, string title, Picture mainPicture) 
        : base(id, title, mainPicture)
    {
    }
    
    [DataMember]
    public int Id { get; }

    [DataMember]
    public string Title { get; }

    [DataMember(Name = "main_picture")]
    public Picture MainPicture { get; }
        
    public override string ToString()
    {
        return base.ToString()
            + $""
            ;
    }
    
}