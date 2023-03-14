using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General;

[DataContract]
public class Genre
{
    public Genre(int id, string name)
    {
        Id = id;
        Name = name;
    }

    [DataMember] public int Id { get; }

    [DataMember] public string Name { get; }

    public override string ToString()
    {
        return $"Id: {Id}, "
               + $"Name: {Name}"
            ;
    }
}