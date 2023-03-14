using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.User;

[DataContract]
public class User
{
    public User(int id, string name)
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