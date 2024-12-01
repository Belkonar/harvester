// See https://aka.ms/new-console-template for more information

using harvester.Data;
using Npgsql;

Console.WriteLine("Hello, World!");

var connectionString = "Host=127.0.0.1;Username=postgres;Password=garden;Database=harvester";
await using var dataSource = NpgsqlDataSource.Create(connectionString);

// Retrieve all rows
await using (var cmd = dataSource.CreateCommand("SELECT * FROM specifier"))
await using (var reader = await cmd.ExecuteReaderAsync())
{
    var data = await reader.MapRows<DtoSpecifier>();
    Console.WriteLine(data.First().Options.First());
}