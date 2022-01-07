using System.Collections.Generic;
using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public class AnimeList
    {
        public AnimeList(List<Datum> data, Paging paging)
        {
            Data = data;
            Paging = paging;
        }

        [DataMember]
        public IReadOnlyList<Datum> Data { get; }

        [DataMember]
        public Paging Paging { get; }
        

        public override string ToString()
        {
            return $"Data: {string.Join(" | ", Data)}, "
                    + $"Paging: {Paging}"
                ;
        }
    }
}