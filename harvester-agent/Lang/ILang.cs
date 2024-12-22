using harvester_agent.Models;

namespace harvester_agent.Lang;

public interface ILang
{
    CollectorRequest? Request { get; set; }
    public Task<List<ScannedTable>> Collect();
    public Task Sample();
}
