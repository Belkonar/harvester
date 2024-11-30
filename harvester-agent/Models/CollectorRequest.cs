using System.Text.Json.Serialization;

namespace harvester_agent.Models;

public class CollectorRequest
{
    [JsonPropertyName("command")]
    public required string Command { get; set; }

    [JsonPropertyName("commandOptions")]
    public required Dictionary<string, string> CommandOptions { get; set; } = new();

    [JsonPropertyName("sourceType")]
    public required string SourceType { get; set; }

    [JsonPropertyName("sourceOptions")]
    public required Dictionary<string, string> SourceOptions { get; set; } = new();
}