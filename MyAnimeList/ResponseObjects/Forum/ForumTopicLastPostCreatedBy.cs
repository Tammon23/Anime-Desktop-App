using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum;

[DataContract]
public class ForumTopicLastPostCreatedBy : User.User
{
    public ForumTopicLastPostCreatedBy(int id, string name)
        : base(id, name)
    {
    }
}