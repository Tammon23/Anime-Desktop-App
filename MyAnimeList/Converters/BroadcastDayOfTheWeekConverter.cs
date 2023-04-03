using Newtonsoft.Json;

namespace MyAnimeList.Converters;

public class BroadcastDayOfTheWeekConverter : JsonConverter<DayOfWeek?>
{
    public override void WriteJson(JsonWriter writer, DayOfWeek? value, JsonSerializer serializer)
    {
        writer.WriteValue(value == null ? "other" : value.ToString());
    }

    public override DayOfWeek? ReadJson(JsonReader reader, Type objectType, DayOfWeek? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {

        if ((string) reader.Value! == "other")
            return null;
        return (DayOfWeek) Enum.Parse(typeof(DayOfWeek), (string) reader.Value, true);
    }
}