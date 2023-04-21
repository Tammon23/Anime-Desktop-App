using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.General;

[DataContract]
[JsonConverter(typeof(StringEnumConverter))]
public enum RatingEnum
{
    [EnumMember(Value = "g")] [Description("G - All Ages")]
    G,

    [EnumMember(Value = "pg")] [Description("PG - Children")]
    Pg,

    [EnumMember(Value = "pg_13")] [Description("pg-13 - Teens 13 and Older")]
    Pg13,

    [EnumMember(Value = "r")] [Description("R - 17+ (violence & profanity)")]
    R,

    [EnumMember(Value = "r+")] [Description("R+ - Profanity & Mild Nudity")]
    RPlus,

    [EnumMember(Value = "rx")] [Description("Rx - Hentai")]
    Rx
}