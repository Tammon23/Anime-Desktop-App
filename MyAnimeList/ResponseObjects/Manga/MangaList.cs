using System.Collections.Generic;
using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Manga
{
    [DataContract]
    public class MangaList
    {
        public MangaList(List<MangaListDatum> data, Paging paging)
        {
            this.Data = data;
            this.Paging = paging;
        }

        [DataMember]
        public IReadOnlyList<Datum> Data { get; }

        [DataMember]
        public Paging Paging { get; }
        
        public override string ToString()
        {
            return $"Data: {(Data != null ? string.Join(" | ", Data) : "N/A")}, "
                   + $"Paging: {Paging}"
                ;
        }
    }
}