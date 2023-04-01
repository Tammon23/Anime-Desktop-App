using System.Runtime.Serialization;
using MyAnimeList.Converters;
using Newtonsoft.Json;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
public class Broadcast
{
    public Broadcast(DayOfWeek? dayOfTheWeek, TimeOnly startTime)
    {
        DayOfTheWeek = dayOfTheWeek;
        StartTime = startTime;
    }

    [DataMember(Name = "day_of_the_week")] public DayOfWeek? DayOfTheWeek { get; }

    [DataMember(Name = "start_time")]
    [JsonConverter(typeof(TimeOnlyJsonConverter))]
    public TimeOnly StartTime { get; }

    public override string ToString()
    {
        return $"Day of the Week: {DayOfTheWeek}, "
               + $"Start Time: {StartTime}"
            ;
    }
}