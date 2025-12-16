using AS.Core.Constants;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AS.Core.Converters;
public class HistoryConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        using (var document = JsonDocument.Parse(JsonSerializer.Serialize(value, JsonConstants.SerializerOptions)))
        {
            foreach (var property in document.RootElement.EnumerateObject())
            {
                if (property.Value.ValueKind != JsonValueKind.Object)
                {
                    property.WriteTo(writer);
                }
                else
                {
                    writer.WriteStartObject(property.Name);
                    writer.WritePropertyName("Id");
                    //writer.WriteStringValue(property.Value.GetProperty("Id").ToString());
                    writer.WriteEndObject();

                }
            }
        }
        writer.WriteEndObject();
    }
}
