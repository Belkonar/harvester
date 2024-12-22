namespace harvester_shared.Models;

public class ScanResults
{
    public List<ScannedTable> Tables { get; set; } = [];
    public List<ScannedField> Fields { get; set; } = [];
}
