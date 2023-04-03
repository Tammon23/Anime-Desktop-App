using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Manga;

[DataContract]
public class MangaRankingDatum
{
    public MangaRankingDatum(MangaNode node, Ranking ranking)
    {
        Node = node;
        Ranking = ranking;
    }

    [DataMember] public MangaNode Node { get; }

    [DataMember] public Ranking Ranking { get; }

    public override string ToString()
    {
        return $"Node: {Node}, "
               + $"Ranking: {Ranking}"
            ;
    }
}