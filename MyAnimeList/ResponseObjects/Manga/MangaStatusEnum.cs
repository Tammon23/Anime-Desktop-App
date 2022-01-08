using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.Manga
{
    [DataContract, JsonConverter(typeof(StringEnumConverter))]
    public enum MangaStatusEnum
    {
        [EnumMember(Value = "finished")]
        Finished,
        
        [EnumMember(Value = "currently_publishing")]
        CurrentlyPublishing,

        [EnumMember(Value = "not_yet_published")]
        NotYetPublished
    }
}