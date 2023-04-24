using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using MyAnimeList;
using MyAnimeList.FieldManager;
using MyAnimeList.ResponseObjects.Anime;
using MyAnimeList.ResponseObjects.General;
using Newtonsoft.Json;

namespace GUI.Models;

public class AnimeTimetables : MALConnector
{
    private static ScheduleAnimeTimetable? _schedule;
    private Show? _showData;
    private Broadcast? _animeBroadcast;
    
    private AnimeTimetables(Node node) : base(node)
    {
    }

    public readonly DayOfWeek? DayOfTheWeek;
    public readonly TimeSpan? Time;
    public readonly string TimeZoneShort;

    private Broadcast? AnimeBroadcast
    {
        get => _animeBroadcast;
        init
        {
            _animeBroadcast = value;
            DayOfTheWeek = _animeBroadcast?.DayOfTheWeek;
            Time = _animeBroadcast?.StartTime.ToTimeSpan();
            TimeZoneShort = "JPN";
        }
    }


    public Show? ShowData
    {
        get => _showData;
        private init
        {
            _showData = value;
            DayOfTheWeek = _showData?.AirTime.DayOfWeek;
            Time = _showData?.AirTime.TimeOfDay;
            TimeZoneShort = "EST";
        }
    }

    private static DirectoryInfo? TryGetSolutionDirectoryInfo(string? currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    
    /// <summary>
    /// Used to manage when a timetable should be retrieved from github and loading it into memory
    /// </summary>
    private static void LoadTimetableJson(bool forceReload=false)
    {
        if (!forceReload && _schedule != null)
            return;
        
        var directory = TryGetSolutionDirectoryInfo();
        if (directory != null)
        {
            var timetableJsonPath = directory.FullName + "\\anime_weekly_timetable.json";
            using var r = new StreamReader(timetableJsonPath);
            var json = r.ReadToEnd();
            _schedule = JsonConvert.DeserializeObject<ScheduleAnimeTimetable>(json);
        }
        else
        {
            // pull from web https://raw.githubusercontent.com/Tammon23/Anime-Desktop-App/master/anime_weekly_timetable.json
        }

    }
    
    /// <summary>
    /// Used to sort out all anime that isn't currently airing, and that do not have a valid broadcast field or
    /// DayOfTheWeek value
    /// </summary>
    /// <param name="animeDatums"></param>
    /// <returns></returns>
    private static IEnumerable<AnimeTimetables> GetValidAnime(IEnumerable<AnimeDatum>? animeDatums)
    {
        if (animeDatums == null)
            return new List<AnimeTimetables>();
        
        return (from datum 
            in animeDatums 
            where datum.Node is {Status: AnimeAiringStatusEnum.CurrentlyAiring, Broadcast.DayOfTheWeek: { }} 
                select new AnimeTimetables(datum.Node) {AnimeBroadcast = datum.Node.Broadcast}).ToList();
    }

    /// <summary>
    /// Used to obtain all anime timetables of current season
    /// </summary>
    /// <param name="method"></param>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <returns>An enumerable of AnimeTimetables of type <see cref="MALConnector"/></returns>
    public static async Task<IEnumerable<AnimeTimetables>> GetAllAnimeTimetables(
        ScheduledRetrievalAnimeMethod method = ScheduledRetrievalAnimeMethod.SCHEDULEANIMENET, 
        int offset = 0, int limit = 100)
    {
        LoadTimetableJson();

        switch (method)
        {
            case ScheduledRetrievalAnimeMethod.MYANIMELIST:
            {
                var fieldSelector = new FieldSelector();
                fieldSelector.AddField(Fields.Broadcast);
                fieldSelector.AddField(Fields.Status);

                var time = DateTime.Now;
        
                var selectedSeason = time.Month switch
                {
                    <= 3 => SeasonEnum.Winter,
                    > 3 and <= 6 => SeasonEnum.Spring,
                    > 6 and <= 9 => SeasonEnum.Summer,
                    _ => SeasonEnum.Fall
                };
        
                var r = await Anime.GetSeasonalAnime(time.Year, selectedSeason, offset, limit, fieldSelector);

                return GetValidAnime(r?.Data);
            }
            case ScheduledRetrievalAnimeMethod.SCHEDULEANIMENET:
            {
                var result = new List<AnimeTimetables>();
            
                foreach (var show in _schedule.AnimeTimetable)
                {
                    var id = show.MALID;
                    var r = await Anime.GetAnimeDetails(show.MALID);
                    if (r == null) continue;
                    result.Add(new AnimeTimetables(r) {ShowData = show});
                }
                return result;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(method), method, "Unhandled method");
        }
    }
    
    /// <summary>
    /// Used to obtain all anime timetables of based on the current user's watching anime list
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <returns>An enumerable of AnimeTimetables of type <see cref="MALConnector"/></returns>
    public static async Task<IEnumerable<AnimeTimetables>> GetUserAnimeTimetables(
        ScheduledRetrievalAnimeMethod method = ScheduledRetrievalAnimeMethod.SCHEDULEANIMENET, 
        int offset = 0, int limit = 100)
    {
        var fieldSelector = new FieldSelector();
        fieldSelector.AddField(Fields.Broadcast);
        fieldSelector.AddField(Fields.Status);
        
        // ------ get user currently watching
        var r = await User.GetUserAnimeList(status: AnimeStatusEnum.Watching, offset:offset, limit:limit
            , fields:fieldSelector);
        
        switch (method)
        {
            case ScheduledRetrievalAnimeMethod.MYANIMELIST:
            {
                return  GetValidAnime(r?.Data);
            }
            case ScheduledRetrievalAnimeMethod.SCHEDULEANIMENET:
            {
                var ids = _schedule?.AnimeTimetable.ToDictionary(show => show.MALID);

                if (r?.Data == null || ids == null)
                    return new List<AnimeTimetables>();
                
                var filteredUserList = r.Data.Where(x => ids.ContainsKey(x.Node.Id)).ToList();
                return filteredUserList.Select(datum => new AnimeTimetables(datum.Node) {ShowData = ids[datum.Node.Id]}).ToList();

            }
            default:
                throw new ArgumentOutOfRangeException(nameof(method), method, null);
        }
    }
}
internal class ScheduleAnimeTimetable
{
    private readonly IReadOnlyList<Show> _animeTimeTable = null!;
    
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("timetable")]
    public IReadOnlyList<Show> AnimeTimetable
    {
        get => _animeTimeTable;
        init
        {
            _animeTimeTable = value.OrderBy(o=>o.AirTime).ToList();
        }
    }

    public override string ToString()
    {
        return $"CreatedAt: {CreatedAt}\nTimetable: \n{string.Join("", AnimeTimetable)}";
    }
}


public class Show
{
    public TimetableLanguage Language { get; set; }
    [JsonProperty("raw_name")] public string RawName { get; set; }
    [JsonProperty("clean_name")] public string CleanName { get; set; }
    public int Episode { get; set; }
    [JsonProperty("datetime")] public DateTime AirTime { get; set; }
    [JsonProperty("mal_id")] public int MALID { get; set; }

    public override string ToString()
    {
        return $"\tLanguage: {Language}, Raw Name: {RawName}, Clean Name: {CleanName}, Episode: {Episode}, Datetime: {AirTime}, Mal ID: {MALID}\n";
    }
}

public enum TimetableLanguage
{
    SUB,
    DUB
}

public enum ScheduledRetrievalAnimeMethod
{
    MYANIMELIST,
    SCHEDULEANIMENET
}
