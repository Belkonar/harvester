using harvester_shared.Models;

namespace harvester_agent.Lang;

public abstract class LangBase(CollectorRequest request)
{
    public abstract Task Collect();
    public abstract Task Sample();
}