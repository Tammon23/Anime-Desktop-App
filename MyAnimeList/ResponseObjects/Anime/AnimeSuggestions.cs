using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
public class AnimeSuggestions
{
    public AnimeSuggestions(List<AnimeListDatum>? data, Paging? paging
    )
    {
        Data = data;
        Paging = paging;
    }

    [DataMember] public IReadOnlyList<AnimeListDatum>? Data { get; }

    [DataMember] public Paging? Paging { get; }

    public override string ToString()
    {
        return $"Data: {(Data != null ? string.Join(" | ", Data) : "N/A")}, "
               + $"Paging: {Paging}"
            ;
    }
}