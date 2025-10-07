using System.Text.Json;

namespace Markey.CrossCutting.Helpers;

public class DateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
{
    private readonly string _format;
    public DateTimeConverter(string format) => _format = format;

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTime.Parse(reader.GetString()!);

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(_format));
}
