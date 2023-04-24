using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MyAnimeList.ResponseObjects.Anime;

[DataContract]
[JsonConverter(typeof(StringEnumConverter))]
public enum AnimeSourceTypeEnum
{
    Other,
    Original,
    Manga,
    [EnumMember(Value = "4_koma_manga")]
    Four_koma_manga,
    Web_manga,
    Digital_manga,
    Novel,
    Light_novel,
    Visual_novel,
    Game,
    Card_game,
    Book,
    Picture_book,
    Radio,
    Music

}