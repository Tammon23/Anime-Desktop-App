using System.ComponentModel;
using System.Runtime.Serialization;

namespace MyAnimeList.ResponseObjects.Anime
{
    [DataContract]
    public enum SeasonEnum
    {
        [EnumMember(Value = "winter")] [Description("January, February, March")]
        Winter,
        
        [EnumMember(Value = "spring")] [Description("April, May, June")]
        Spring,
        
        [EnumMember(Value = "summer")] [Description("July, August, September")]
        Summer,
        
        [EnumMember(Value = "fall")] [Description("October, November, December")]
        Fall
    }
}