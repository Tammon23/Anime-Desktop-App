﻿using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum;

[DataContract]
public class ForumTopicCreatedBy
{
    public ForumTopicCreatedBy(string id, string name)
    {
        Id = id;
        Name = name;
    }

    [DataMember] public string Id { get; }

    [DataMember] public string Name { get; }


    public override string ToString()
    {
        return $"Id: {Id}, "
               + $"Name: {Name}, "
            ;
    }
}