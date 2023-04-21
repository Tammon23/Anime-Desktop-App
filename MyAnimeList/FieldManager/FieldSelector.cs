using System.Reflection;

namespace MyAnimeList.FieldManager;

public class FieldSelector
{
    private readonly HashSet<string> _fields = new();

    public void AddField(Fields field)
    {
        _fields.Add(field.StringValue());
    }
    
    public void AddAllFields(bool includeNsfw=false)
    {
        _fields.UnionWith(Enum.GetValues<Fields>().Select(x => x.StringValue()));

        if (!includeNsfw)
        {
            _fields.Remove("nsfw");
        }
    }

    public string GetFieldsAsString()
    {
        return string.Join(",", _fields.ToArray());
    }
    
}

public enum Fields
{
    [StringValue("id")]
    Id,
    
    [StringValue("title")]
    Title,
    
    [StringValue("main_pictures")]
    MainPicture,
    
    [StringValue("alternative_titles")]
    AlternativeTitles,
    
    [StringValue("start_date")]
    StartDate,
    
    [StringValue("end_date")]
    EndDate,
    
    [StringValue("synopsis")]
    Synopsis,
    
    [StringValue("mean")]
    Mean,
    
    [StringValue("rank")]
    Rank,
    
    [StringValue("popularity")]
    Popularity,
    
    [StringValue("num_list_users")]
    NumberOfListUsers,
    
    [StringValue("num_scoring_users")]
    NumberOfScoringUsers,
    
    [StringValue("nsfw")]
    Nsfw,
    
    [StringValue("genres")]
    Genres,
    
    [StringValue("created_at")]
    CreatedAt,
    
    [StringValue("updated_at")]
    UpdatedAt,
    
    [StringValue("media_type")]
    MediaType,
    
    [StringValue("status")]
    Status,
    
    [StringValue("my_list_status")]
    MyListStatus,
    
    [StringValue("num_episodes")]
    NumberOfEpisodes,
    
    [StringValue("start_season")]
    StartSeason,
    
    [StringValue("broadcast")]
    Broadcast,

    [StringValue("source")]
    Source,
    
    [StringValue("average_episode_duration")]
    AverageEpisodeDuration,
    
    [StringValue("rating")]
    Rating,
    
    [StringValue("studios")]
    Studios,
    
    [StringValue("pictures")]
    Pictures,
    
    [StringValue("background")]
    Background,
    
    [StringValue("related_anime")]
    RelatedAnime,
    
    [StringValue("related_manga")]
    RelatedManga,
    [StringValue("recommendations")]
    Recommendations,
    
    [StringValue("statistics")]
    Statistics
}

/// <summary>
/// Source: https://www.reddit.com/r/csharp/comments/weckmr/comment/iinu7bz/?utm_source=share&utm_medium=web2x&context=3
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class StringValueAttribute : Attribute
{
    public StringValueAttribute(string value)
    {
        Value = value;
    }

    public string Value { get; }
}

/// <summary>
/// Source: https://www.reddit.com/r/csharp/comments/weckmr/comment/iinu7bz/?utm_source=share&utm_medium=web2x&context=3
/// </summary>
public static class EnumExtensions
{
    public static string StringValue<T>(this T value)
        where T : Enum
    {
        var fieldName = value.ToString();
        var field = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
        return field?.GetCustomAttribute<StringValueAttribute>()?.Value ?? fieldName;
    }
}