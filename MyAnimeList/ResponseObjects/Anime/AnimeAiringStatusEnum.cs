using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract(Name = "Status")]
[JsonConverter(typeof(StringEnumConverter))]
public enum AnimeAiringStatusEnum
{
    [EnumMember(Value = "finished_airing")] FinishedAiring,

    [EnumMember(Value = "currently_airing")] CurrentlyAiring,

    [EnumMember(Value = "not_yet_aired")] NotYetAired
}