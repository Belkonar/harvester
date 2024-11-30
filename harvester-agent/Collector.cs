using System.Text.Json;
using harvester_agent.Constants;
using harvester_agent.Lang;
using harvester_agent.Models;

namespace harvester_agent;

public class Collector(IServiceProvider provider)
{
    public async Task Run(CollectorRequest request)
    {
        var lang = provider.GetRequiredKeyedService<ILang>(request.SourceType);
        lang.Request = request;

        switch (request.Command)
        {
            case CommandType.Collect:
                await lang.Collect();
                break;
            case CommandType.Sample:
                await lang.Sample();
                break;
            default:
                throw new Exception($"Command {request.Command} is not valid");
        }
    }
}