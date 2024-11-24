namespace harvester_collector.Lang;

public interface ILang
{
    Task Collect(Dictionary<string, string> sourceOptions);
    Task Sample(Dictionary<string, string> sourceOptions, Dictionary<string, string> commandOptions);
}