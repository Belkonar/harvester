using System.Text.Json;
using harvester_agent.Lang;
using harvester_shared.Constants;
using harvester_shared.Models;

namespace harvester_agent;

public class Collector(IServiceProvider provider)
{
    public async Task Run()
    {
        var request = provider.GetRequiredService<CollectorRequest>();
        var lang = provider.GetRequiredKeyedService<LangBase>(request.SourceType);

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

    public static CollectorRequest? GetRequest()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
        var input = (sr.ReadToEnd()).Trim();

        try
        {
            return JsonSerializer.Deserialize<CollectorRequest>(input);
        }
        catch (JsonException e)
        {
            Console.WriteLine(e);
            Environment.Exit(1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Environment.Exit(10);
        }

        return null;
    }
}