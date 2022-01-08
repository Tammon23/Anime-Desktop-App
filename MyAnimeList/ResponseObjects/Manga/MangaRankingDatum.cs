using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Manga
{
    [DataContract]
    public class MangaRankingDatum
    {
        public MangaRankingDatum(Node node, Ranking ranking)
        {
            this.Node = node;
            this.Ranking = ranking;
        }

        [DataMember] public Node Node { get; }

        [DataMember] public Ranking Ranking { get; }

        public override string ToString()
        {
            return $"Node: {Node}, "
                   + $"Ranking: {Ranking}"
                ;
        }
    }
}