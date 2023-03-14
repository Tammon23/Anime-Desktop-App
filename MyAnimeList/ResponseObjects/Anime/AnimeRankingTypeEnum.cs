using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
[JsonConverter(typeof(StringEnumConverter))]
public enum AnimeRankingTypeEnum
{
    [EnumMember(Value = "all")] [Description("Top Anime Series")]
    All,

    [EnumMember(Value = "airing")] [Description("Top Airing Anime")]
    Airing,

    [EnumMember(Value = "upcoming")] [Description("Top Upcoming Anime")]
    Upcoming,

    [EnumMember(Value = "tv")] [Description("Top Anime TV Series")]
    Tv,

    [EnumMember(Value = "ova")] [Description("Top Anime OVA Series")]
    Ova,

    [EnumMember(Value = "movie")] [Description("Top Anime Movies")]
    Movie,

    [EnumMember(Value = "special")] [Description("Top Anime Specials")]
    Special,

    [EnumMember(Value = "bypopularity")] [Description("Top Anime by Popularity")]
    ByPopularity,

    [EnumMember(Value = "favorite")] [Description("Top Favorited Anime")]
    Favorite
}