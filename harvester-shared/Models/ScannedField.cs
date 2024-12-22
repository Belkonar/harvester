namespace harvester_shared.Models;

public class ScannedField
{
    public required Guid Source { get; set; }
    public required string TableName { get; set; }
    public required string Name { get; set; }
    public List<string> Types { get; set; } = [];
    public bool IsSubField { get; set; } = false;
}
