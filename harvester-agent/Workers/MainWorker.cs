namespace harvester_agent.Workers;

public class MainWorker : IWorker
{
    public async Task Run()
    {
        Console.WriteLine("Run");
    }
}