using harvester_collector;
using harvester_collector.Lang;
using harvester_shared.Constants;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<Collector>();
builder.Services.AddKeyedSingleton<ILang, LangPostgres>(SourceType.Postgres);

var host = builder.Build();
var collector = host.Services.GetRequiredService<Collector>();

await collector.Run();