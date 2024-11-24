using harvester_agent;
using harvester_agent.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<MainWorker>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
