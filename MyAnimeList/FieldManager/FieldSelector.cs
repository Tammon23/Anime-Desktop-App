using System.Reflection;

namespace MyAnimeList.FieldManager;

public class FieldSelector
{
    private readonly HashSet<string> _fields = new();

    public void AddField(Fields field)
    {
        _fields.Add(field.StringValue());
    }
    
    public void AddAllFields(FieldTypes type = FieldTypes.Anime)
    {
        _fields.UnionWith(Enum.GetValues<Fields>().Where(x => x.FieldType(type)).Select(x => x.StringValue()));
    }

    public string GetFieldsAsString()
    {
        return string.Join(",", _fields.ToArray());
    }

    public bool Contains(Fields field)
    {
        return _fields.Contains(field.StringValue());
    }
    
}

public enum Fields
{
    [StringValue("id")][TypeValue(FieldTypes.Anime, FieldTypes.User, FieldTypes.Manga)]
    Id,
    
    [StringValue("title")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Title,
    
    [StringValue("main_pictures")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    MainPicture,
    
    [StringValue("alternative_titles")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    AlternativeTitles,
    
    [StringValue("start_date")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    StartDate,
    
    [StringValue("end_date")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    EndDate,
    
    [StringValue("synopsis")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Synopsis,
    
    [StringValue("mean")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Mean,
    
    [StringValue("rank")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Rank,
    
    [StringValue("popularity")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Popularity,
    
    [StringValue("num_list_users")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    NumberOfListUsers,
    
    [StringValue("num_scoring_users")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    NumberOfScoringUsers,
    
    [StringValue("nsfw")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Nsfw,
    
    [StringValue("genres")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Genres,
    
    [StringValue("created_at")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    CreatedAt,
    
    [StringValue("updated_at")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    UpdatedAt,
    
    [StringValue("media_type")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    MediaType,
    
    [StringValue("status")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Status,
    
    [StringValue("my_list_status")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    MyListStatus,
    
    [StringValue("num_episodes")][TypeValue(FieldTypes.Anime)]
    NumberOfEpisodes,
    
    [StringValue("start_season")][TypeValue(FieldTypes.Anime)]
    StartSeason,
    
    [StringValue("broadcast")][TypeValue(FieldTypes.Anime)]
    Broadcast,

    [StringValue("source")][TypeValue(FieldTypes.Anime)]
    Source,
    
    [StringValue("average_episode_duration")][TypeValue(FieldTypes.Anime)]
    AverageEpisodeDuration,
    
    [StringValue("rating")][TypeValue(FieldTypes.Anime)]
    Rating,
    
    [StringValue("studios")][TypeValue(FieldTypes.Anime)]
    Studios,
    
    [StringValue("pictures")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Pictures,
    
    [StringValue("background")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Background,
    
    [StringValue("related_anime")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    RelatedAnime,
    
    [StringValue("related_manga")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    RelatedManga,
    [StringValue("recommendations")][TypeValue(FieldTypes.Anime, FieldTypes.Manga)]
    Recommendations,
    
    [StringValue("statistics")][TypeValue(FieldTypes.Anime)]
    Statistics,
    
    [StringValue("name")][TypeValue(FieldTypes.User)]
    Name,
    
    [StringValue("picture")][TypeValue(FieldTypes.User)]
    Picture,
    
    [StringValue("gender")][TypeValue(FieldTypes.User)]
    Gender,
    
    [StringValue("birthday")][TypeValue(FieldTypes.User)]
    Birthday,
    
    [StringValue("location")][TypeValue(FieldTypes.User)]
    Location,
    
    [StringValue("joined_at")][TypeValue(FieldTypes.User)]
    JoinedAt,
    
    [StringValue("anime_statistics")][TypeValue(FieldTypes.User)]
    AnimeStatistics,
    
    [StringValue("time_zone")][TypeValue(FieldTypes.User)]
    TimeZone,
    
    [StringValue("is_supporter")][TypeValue(FieldTypes.User)]
    IsSupporter,
    
    [StringValue("num_volumes")][TypeValue(FieldTypes.Manga)]
    NumberOfVolumes,
    
    [StringValue("num_chapters")][TypeValue(FieldTypes.Manga)]
    NumberOfChapters,
    
    [StringValue("authors")][TypeValue(FieldTypes.Manga)]
    Authors,
    
    [StringValue("serialization")][TypeValue(FieldTypes.Manga)]
    Serialization

}

/// <summary>
/// Source: https://www.reddit.com/r/csharp/comments/weckmr/comment/iinu7bz/?utm_source=share&utm_medium=web2x&context=3
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
internal sealed class StringValueAttribute : Attribute
{
    internal StringValueAttribute(string value)
    {
        Value = value;
    }

    internal string Value { get; }
}

[AttributeUsage(AttributeTargets.Field)]
internal sealed class TypeValueAttribute : Attribute
{
    public readonly FieldTypes[] Types;

    internal TypeValueAttribute(params FieldTypes[] types)
    {
        Types = types;
    }
}

public enum FieldTypes
{
    Anime,
    Manga,
    User
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
    
    public static bool FieldType<T>(this T value, FieldTypes fieldType)
        where T : Enum
    {
        var fieldName = value.ToString();
        var field = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
        var result = field?.GetCustomAttribute<TypeValueAttribute>()?.Types.Contains(fieldType);

        return result.GetValueOrDefault();
    }

}