using System.Text.Json;

namespace PromotionEngine.Entities.Converters;
internal static class JsonConverterExtensions
{
    internal static JsonElement? GetPropertyValue(this JsonElement jsonElement, string propertyName)
    {
        foreach (var property in jsonElement.EnumerateObject().OfType<JsonProperty>())
        {
            if (property.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase))
            {
                return property.Value;
            }
        }

        return null;
    }

    internal static void Serialize<TBase, T>(Utf8JsonWriter writer, TBase baseObject, JsonSerializerOptions options)
        where T : TBase
    {
        JsonSerializer.Serialize(writer, (T?)baseObject, options);
    }
}
