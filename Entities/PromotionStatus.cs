using System.Text.Json.Serialization;

namespace PromotionEngine.Entities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PromotionStatus
{
    Enabled,
    Disabled
}
