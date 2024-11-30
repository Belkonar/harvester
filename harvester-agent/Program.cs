using harvester_agent;
using harvester_agent.Constants;
using harvester_agent.Lang;
using harvester_agent.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<MainWorker>();

builder.Services.AddHostedService<Worker>();

builder.Services.AddSingleton<Collector>();

builder.Services.AddKeyedSingleton<ILang, LangPostgres>(SourceType.Postgres);

var host = builder.Build();

// Either this runs the host in LongTerm mode or it runs as a container
if (builder.Configuration.GetValue<bool>("LongTerm", false))
{
    host.Run();
}
else
{
    var worker = host.Services.GetRequiredService<MainWorker>();
    await worker.Run();
}