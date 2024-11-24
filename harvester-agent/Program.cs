using harvester_agent;
using harvester_agent.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<MainWorker>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

if (builder.Configuration.GetValue<bool>("LongTerm", false))
{
    host.Run();
}
else
{
    var worker = host.Services.GetRequiredService<MainWorker>();
    await worker.Run();
}