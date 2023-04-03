using System.Runtime.Serialization;
using MyAnimeList.ResponseObjects.General;

namespace MyAnimeList.ResponseObjects.Anime;

public class AnimeSeasonal
{
    public AnimeSeasonal(List<AnimeListDatum>? data, Paging? paging, SeasonInfo? season
    )
    {
        Data = data;
        Paging = paging;
        Season = season;
    }

    [DataMember] public IReadOnlyList<AnimeListDatum>? Data { get; }

    [DataMember] public Paging? Paging { get; }

    [DataMember] public SeasonInfo? Season { get; }

    public override string ToString()
    {
        return $"Data: {(Data != null ? string.Join(" \n=> ", Data) : "N/A")}, "
               + $"Paging: {Paging}, "
               + $"Season: {Season}"
            ;
    }
}