using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.General;

[DataContract, JsonConverter(typeof(StringEnumConverter))]
public enum NsfwEnum
{
    [EnumMember(Value = "white")] [Description("This work is safe for work")]
    White,
    
    [EnumMember(Value = "gray")] [Description("This work may be not safe for work")]
    Gray,
    
    [EnumMember(Value = "black")] [Description("This work is not safe for work")]
    Black
}