using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.Manga;

namespace MyAnimeList.ResponseObjects.General;

public class MangaDatum : Datum
{
    public MangaDatum(MangaNode node) : base(node)
    {
        Node = node;
    }

    [DataMember] public new MangaNode Node { get; }

    public override string ToString()
    {
        return $"Node: {Node}";
    }
}