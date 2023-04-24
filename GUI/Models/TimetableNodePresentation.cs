using System;
using MyAnimeList.ResponseObjects.General;
using ReactiveUI;

namespace GUI.Models;

public class TimetableNodePresentation : NodePresentation
{
    /*
    private readonly TimeZoneInfo _malDatabaseTimezone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time"); // JST
    */
    public TimetableNodePresentation(Node node, IScreen screen, DayOfWeek? dayOfTheWeek, TimeSpan? time, string timeZoneShort) : base(node,
        screen)
    {

    AirTime = (time == null ? "Unknown" : time.ToString()) ?? "Unknown";
    TimeZoneShort = timeZoneShort;
    
    }
    
    public int Episode { get; }
    public string AirTime { get; }
    public string TimeZoneShort { get; }
}