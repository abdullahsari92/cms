using System.Text.Json;
using System.Text.Json.Serialization;

namespace AS.Core.Constants
{
    public class JsonConstants
    {
        public static JsonSerializerOptions SerializerOptions => new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
