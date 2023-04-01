﻿using System.Diagnostics;
using System.Globalization;
using Newtonsoft.Json;

namespace MyAnimeList.Converters;


public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
    private const string TimeFormat = "hh:mm";

    public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        Debug.WriteLine("reading");
        return TimeOnly.Parse((string)reader.Value);
    }

    public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
    {
        Debug.WriteLine("writing");

        writer.WriteValue(value.ToString(TimeFormat, CultureInfo.InvariantCulture));
    }
}