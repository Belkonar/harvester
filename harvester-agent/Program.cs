using harvester_agent;
using harvester_agent.Lang;
using harvester_agent.Workers;
using harvester_shared.Constants;
using harvester_shared.Models;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<MainWorker>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<Collector>();

builder.Services.AddSingleton<CollectorRequest>((_ => Collector.GetRequest()!));

builder.Services.AddKeyedSingleton<LangBase, LangPostgres>(SourceType.Postgres);

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