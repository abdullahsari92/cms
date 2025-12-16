using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AS.Core.Converters;
public class HistoryConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        var isIEntity = typeToConvert.GetInterfaces().FirstOrDefault(x => x.Name == nameof(IEntity));
        return isIEntity != null;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var valueType = typeToConvert;
        return (JsonConverter)Activator.CreateInstance(
            type: typeof(HistoryConverter<>).MakeGenericType(new Type[] { valueType }),
            bindingAttr: BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: null,
            culture: null
        );
    }
}
