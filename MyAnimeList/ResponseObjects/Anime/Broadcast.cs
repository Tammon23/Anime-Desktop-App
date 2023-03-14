using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
public class Broadcast
{
    public Broadcast(string dayOfTheWeek, string startTime)
    {
        DayOfTheWeek = dayOfTheWeek;
        StartTime = startTime;
    }

    [DataMember(Name = "day_of_the_week")] public string DayOfTheWeek { get; }

    [DataMember(Name = "start_time")] public string StartTime { get; }

    public override string ToString()
    {
        return $"Day of the Week: {DayOfTheWeek}, "
               + $"Start Time: {StartTime}"
            ;
    }
}