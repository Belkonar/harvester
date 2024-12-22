using System.Text.Json;
using harvester_shared;

namespace harvester_parsers;

public class JsonScanner(JsonDocument doc)
{
    public FieldSet Fields { get; } = new();

    public void Scan()
    {
        Dive("", doc.RootElement);
    }

    private void Dive(string prefix, JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
            {
                Fields.Add($"{prefix}", "object");

                foreach (var prop in element.EnumerateObject())
                {
                    Dive($"{prefix}.{prop.Name}", prop.Value);
                }

                break;
            }
            case JsonValueKind.Array:
            {
                Fields.Add($"{prefix}", "array");

                foreach (var elem in element.EnumerateArray())
                {
                    Dive($"{prefix}[]", elem);
                }

                break;
            }
            case JsonValueKind.True:
            case JsonValueKind.False:
                Fields.Add(prefix, "boolean");
                break;
            default:
                Fields.Add(prefix, element.ValueKind.ToString());
                break;
        }
    }
}
