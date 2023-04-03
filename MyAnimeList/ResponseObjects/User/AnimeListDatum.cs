using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Anime;

namespace MyAnimeList.ResponseObjects.User;

[DataContract]
public class AnimeListDatum
{
    public AnimeListDatum(AnimeNode node, ListStatus listStatus)
    {
        Node = node;
        ListStatus = listStatus;
    }

    [DataMember] public AnimeNode Node { get; }

    [DataMember(Name = "list_status")] public ListStatus ListStatus { get; }

    public override string ToString()
    {
        return $"Node: {Node}, "
               + $"List Status: {ListStatus}"
            ;
    }
}