using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract(Name = "Status")][JsonConverter(typeof(StringEnumConverter))]
    public enum StatusEnum
    {
        [EnumMember(Value="watching")]
        Watching,

        [EnumMember(Value="completed")]
        Completed,
        
        [EnumMember(Value="on_hold")]
        OnHold,
        
        [EnumMember(Value="dropped")]
        Dropped,
        
        [EnumMember(Value ="plan_to_watch")]
        PlanToWatch
    }
}