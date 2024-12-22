namespace harvester_agent.Models;

public class ScannedField
{
    public required string Name { get; set; }
    public List<string> Types { get; set; } = [];
    public bool IsSubField { get; set; } = false;
}
