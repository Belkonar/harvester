using harvester_collector;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<Collector>();

var host = builder.Build();
var collector = host.Services.GetRequiredService<Collector>();

await collector.Run();