﻿using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Forum;

[DataContract]
public class Option
{
    public Option(string id, string text, string votes)
    {
        Id = id;
        Text = text;
        Votes = votes;
    }

    [DataMember] public string Id { get; }

    [DataMember] public string Text { get; }

    [DataMember] public string Votes { get; }

    public override string ToString()
    {
        return $"Id: {Id}, "
               + $"Text: {Text}, "
               + $"Votes: {Votes}"
            ;
    }
}