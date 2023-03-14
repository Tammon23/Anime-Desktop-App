using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum;

[DataContract]
public class CreatedBy : User.User
{
    public CreatedBy(int id, string name, string forumAvator)
        : base(id, name)
    {
        ForumAvator = forumAvator;
    }

    [DataMember(Name = "forum_avator")] public string ForumAvator { get; }

    public override string ToString()
    {
        return base.ToString()
               + $", Forum Avator: {ForumAvator}"
            ;
    }
}