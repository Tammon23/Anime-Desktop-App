using System.Collections.Generic;
using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime
{

    public class AnimeSeasonal
    {
        public AnimeSeasonal(List<AnimeListDatum>? data, Paging? paging, SeasonInfo? season
        )
        {
            this.Data = data;
            this.Paging = paging;
            this.Season = season;
        }

        [DataMember]
        public IReadOnlyList<AnimeListDatum>? Data { get; }
    
        [DataMember]
        public Paging? Paging { get; }

        [DataMember]
        public SeasonInfo? Season { get; }

        public override string ToString()
        {
            return $"Data: {((Data != null) ? string.Join(" | ", Data) : "N/A")}, "
                    + $"Paging: {Paging}, "
                    + $"Season: {Season}"
                ;
        }
    }

}