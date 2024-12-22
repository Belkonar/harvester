namespace harvester_agent.Models;

public class ScannedTable
{
    public required string Name { get; set; }
    public List<ScannedField> Fields { get; set; } = [];
}
