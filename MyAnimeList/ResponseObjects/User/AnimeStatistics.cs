using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.User;

[DataContract]
public class AnimeStatistics
{
    public AnimeStatistics(int numItemsWatching,
        int numItemsCompleted,
        int numItemsOnHold,
        int numItemsDropped,
        int numItemsPlanToWatch,
        int numItems,
        double numDaysWatched,
        double numDaysWatching,
        double numDaysCompleted,
        double numDaysOnHold,
        double numDaysDropped,
        double numDays,
        int numEpisodes,
        int numTimesRewatched,
        double meanScore
    )
    {
        NumItemsWatching = numItemsWatching;
        NumItemsCompleted = numItemsCompleted;
        NumItemsOnHold = numItemsOnHold;
        NumItemsDropped = numItemsDropped;
        NumItemsPlanToWatch = numItemsPlanToWatch;
        NumItems = numItems;
        NumDaysWatched = numDaysWatched;
        NumDaysWatching = numDaysWatching;
        NumDaysCompleted = numDaysCompleted;
        NumDaysOnHold = numDaysOnHold;
        NumDaysDropped = numDaysDropped;
        NumDays = numDays;
        NumEpisodes = numEpisodes;
        NumTimesRewatched = numTimesRewatched;
        MeanScore = meanScore;
    }

    [DataMember(Name = "num_items_watching")]
    public int NumItemsWatching { get; }

    [DataMember(Name = "num_items_completed")]
    public int NumItemsCompleted { get; }

    [DataMember(Name = "num_items_on_hold")]
    public int NumItemsOnHold { get; }

    [DataMember(Name = "num_items_dropped")]
    public int NumItemsDropped { get; }

    [DataMember(Name = "num_items_plan_to_watch")]
    public int NumItemsPlanToWatch { get; }

    [DataMember(Name = "num_items")] public int NumItems { get; }

    [DataMember(Name = "num_days_watched")]
    public double NumDaysWatched { get; }

    [DataMember(Name = "num_days_watching")]
    public double NumDaysWatching { get; }

    [DataMember(Name = "num_days_completed")]
    public double NumDaysCompleted { get; }

    [DataMember(Name = "num_days_on_hold")]
    public double NumDaysOnHold { get; }

    [DataMember(Name = "num_days_dropped")]
    public double NumDaysDropped { get; }

    [DataMember(Name = "num_days")] public double NumDays { get; }

    [DataMember(Name = "num_episodes")] public int NumEpisodes { get; }

    [DataMember(Name = "num_times_rewatched")]
    public int NumTimesRewatched { get; }

    [DataMember(Name = "mean_score")] public double MeanScore { get; }

    public override string ToString()
    {
        return $"Number of items watching: {NumItemsWatching}, "
               + $"Number of items completed: {NumItemsCompleted}, "
               + $"Number of items on hold: {NumItemsOnHold}, "
               + $"Number of items dropped: {NumItemsDropped}, "
               + $"Number of items plan to watch: {NumItemsPlanToWatch}, "
               + $"Number of items: {NumItems}, "
               + $"Number of days watched: {NumDaysWatched}, "
               + $"Number of days watching: {NumDaysWatching}, "
               + $"Number of days completed: {NumDaysCompleted}, "
               + $"Number of days on hold: {NumDaysOnHold}, "
               + $"Number of days dropped: {NumDaysDropped}, "
               + $"Number of days: {NumDays}, "
               + $"Number of episodes: {NumEpisodes}, "
               + $"Number of times rewatched: {NumTimesRewatched}, "
               + $"Mean score: {MeanScore}"
            ;
    }
}