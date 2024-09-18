using System.Text.Json;
using System.Text.Json.Serialization;

namespace PromotionEngine.Entities.Converters;

public class PromotionDiscountJsonConverter : JsonConverter<Discount>
{
    public override Discount Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
        string? text = jsonDocument.RootElement.GetPropertyValue("type")?.ToString() ?? null;
        if (Enum.TryParse<DiscountType>(text, out var result))
        {
#pragma warning disable CS8603 // Possible null reference return.
            return result switch
            {
                DiscountType.Store => jsonDocument.Deserialize<StoreDiscount>(options),
                DiscountType.Online => jsonDocument.Deserialize<OnlineDiscount>(options),
                DiscountType.Unknown => throw new JsonException("Unable to deserialize discount of type '" + Enum.GetName(result) + "'."),
                _ => throw new JsonException("Unable to deserialize discount of type '" + Enum.GetName(result) + "'."),
            };
#pragma warning restore CS8603 // Possible null reference return.
        }

        throw new JsonException("Unable to deserialize discount of type '" + text + "'.");
    }

    public override void Write(Utf8JsonWriter writer, Discount value, JsonSerializerOptions options)
    {
        switch (value.Type)
        {
            case DiscountType.Store:
                JsonConverterExtensions.Serialize<Discount, StoreDiscount>(writer, value, options);
                break;
            case DiscountType.Online:
                JsonConverterExtensions.Serialize<Discount, OnlineDiscount>(writer, value, options);
                break;
            default:
                throw new JsonException("Unable to serialize discount of type '" + Enum.GetName(value.Type) + "'.");
        }
    }
}
