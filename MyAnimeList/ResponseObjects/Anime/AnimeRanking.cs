using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    public class AnimeRanking
    {
        public AnimeRanking(List<Datum>? data, Paging? paging)
        {
            this.Data = data;
            this.Paging = paging;
        }

        [DataMember]
        public IReadOnlyList<Datum>? Data { get; }

        [DataMember]
        public Paging? Paging { get; }

        public override string ToString()
        {
            return $"Data: {((Data != null) ? string.Join(" | ", Data) : "N/A")}"
                    + $"Paging: {Paging}"
                ;
        }
    }
}