using System.Net;
using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.User;

[DataContract]
public class AnimeListDatum : AnimeDatum
{
    public AnimeListDatum(AnimeNode node, ListStatus listStatus) : base(node)
    {
        Node = node;
        ListStatus = listStatus;
    }

    [DataMember] public new AnimeNode Node { get; }

    [DataMember(Name = "list_status")] public ListStatus ListStatus { get; }

    public override string ToString()
    {
        return $"Node: {Node}, "
               + $"List Status: {ListStatus}"
            ;
    }
}