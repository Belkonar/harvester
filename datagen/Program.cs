// See https://aka.ms/new-console-template for more information

using datagen;
using Npgsql;

Console.WriteLine("Hello, World!");

var connectionString = "Host=127.0.0.1;Username=postgres;Password=garden;Database=harvester";
await using var dataSource = NpgsqlDataSource.Create(connectionString);

await Gen.GenSpecifier(dataSource);

var testGet = dataSource.CreateCommand("SELECT * FROM specifier");

var reader = testGet.ExecuteReader();