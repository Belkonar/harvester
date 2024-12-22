using harvester_agent.Models;
using harvester_shared.Models;

namespace harvester_agent.Lang;

public interface ILang
{
    CollectorRequest? Request { get; set; }
    public Task<ScanResults> Collect();
    public Task Sample();
}
