using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
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
        
        [EnumMember(Value="plan_to_watch")]
        PlanToWatch
    }
}