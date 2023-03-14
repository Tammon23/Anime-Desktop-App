using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
public class SeasonInfo
{
    public SeasonInfo(int year, SeasonEnum season)
    {
        Year = year;
        Season = season;
    }

    [DataMember] public int Year { get; }

    [DataMember(Name = "Season")] public SeasonEnum Season { get; }

    public override string ToString()
    {
        return $"Year: {Year}, "
               + $"Season: {Season}"
            ;
    }
}