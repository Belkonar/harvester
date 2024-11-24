using System.Text.Json;
using harvester_collector.Lang;
using harvester_shared.Constants;
using harvester_shared.Models;

namespace harvester_collector;

public class Collector(IServiceProvider provider)
{
    public async Task Run()
    {
        var request = await GetRequest();
        if (request == null)
        {
            return;
        }

        var lang = provider.GetRequiredKeyedService<ILang>(request.SourceType);

        switch (request.Command)
        {
            case CommandType.Collect:
                await lang.Collect(request.SourceOptions);
                break;
            case CommandType.Sample:
                await lang.Sample(request.SourceOptions, request.CommandOptions);
                break;
            default:
                throw new Exception($"Command {request.Command} is not valid");
        }
    }

    private static async Task<CollectorRequest?> GetRequest()
    {
        using var sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
        var input = (await sr.ReadToEndAsync()).Trim();

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