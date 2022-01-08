using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.General
{
    [DataContract]
    public class Ranking
    {
        public Ranking(int rank)
        {
            this.Rank = rank;
        }

        [DataMember]
        public int Rank { get; }

        public override string ToString()
        {
            return $"Rank: {Rank}";
        }
    }
}