using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.Manga;

[DataContract]
[JsonConverter(typeof(StringEnumConverter))]
public enum MangaRankingTypeEnum
{
    [EnumMember(Value = "all")] [Description("All")]
    All,

    [EnumMember(Value = "manga")] [Description("Top Manga")]
    Manga,

    [EnumMember(Value = "novels")] [Description("Top Novels")]
    Novels,

    [EnumMember(Value = "oneshots")] [Description("Top One-shots")]
    OneShots,

    [EnumMember(Value = "doujin")] [Description("Top Doujinshi")]
    Doujin,

    [EnumMember(Value = "manhwa")] [Description("Top Manhwa")]
    Manhwa,

    [EnumMember(Value = "manhua")] [Description("Top Manhua")]
    Manhua,

    [EnumMember(Value = "bypopularity")] [Description("Top Anime by Popularity")]
    ByPopularity,

    [EnumMember(Value = "favorite")] [Description("Top Favorited Anime")]
    Favorite
}