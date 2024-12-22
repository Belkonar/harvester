using harvester_agent.Constants;

namespace harvester_agent.Models;

public class ConfigSource
{
    public required string Name { get; set; }
    public required Guid Id { get; set; }
    public required string SourceType { get; set; }
    public required Dictionary<string, string> ConnectionParams { get; set; }
}
