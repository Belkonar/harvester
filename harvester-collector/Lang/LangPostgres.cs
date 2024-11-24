namespace harvester_collector.Lang;

public class LangPostgres : ILang
{
    public async Task Collect(Dictionary<string, string> sourceOptions)
    {
        Console.WriteLine("scan");
    }

    public Task Sample(Dictionary<string, string> sourceOptions, Dictionary<string, string> commandOptions)
    {
        throw new NotImplementedException();
    }
}