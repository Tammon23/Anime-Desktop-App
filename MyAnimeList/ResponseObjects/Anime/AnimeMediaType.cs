using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
[JsonConverter(typeof(StringEnumConverter))]
public enum AnimeMediaType
{ 
    Unknown,
    Tv,
    Ova,
    Movie,
    Special,
    Ona,
    Music
}