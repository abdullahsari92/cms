
using AS.Core.Constants;
using AS.Core.Converters;
using System.Text.Json;

namespace AS.Core.Helpers;
public static class JsonHelper
{
    public static string ToCreateHistoryAsJson(this object entity)
    {
        var options = JsonConstants.SerializerOptions;
        options.Converters.Add(new HistoryConverterFactory());

        var data = JsonSerializer.Serialize(entity, options);
        return data;
    }
}
