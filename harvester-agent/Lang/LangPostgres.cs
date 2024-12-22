using harvester_agent.Models;
using Npgsql;

namespace harvester_agent.Lang;

public class LangPostgres : ILang
{
    public CollectorRequest? Request { get; set; }

    // Query Templates
    const string DbListQuery =
        """
        SELECT datname FROM pg_database
        WHERE datistemplate = false AND datname != 'postgres'
        """;

    private const string GetColsQuery =
        """
        SELECT "table_schema", "table_name", "column_name", "data_type"
        FROM information_schema.columns WHERE table_catalog != 'postgres';
        """;

    private static string GetConnStr(string db = "postgres")
    {
        var connStrBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = "localhost",
            Username = "postgres",
            Password = "garden",
            Database = db
        };

        return connStrBuilder.ConnectionString;
    }

    public async Task<List<string>> GetDatabases()
    {
        await using var conn = new NpgsqlConnection(GetConnStr());
        conn.Open();

        await using var command = new NpgsqlCommand(DbListQuery, conn);
        await using var reader = command.ExecuteReader();

        var returnVal = new List<string>();

        while (reader.Read())
        {
            returnVal.Add(reader.GetString(0));
        }

        return returnVal;
    }

    public async Task<List<ScannedTable>> Collect()
    {
        var databases = await GetDatabases();

        databases.ForEach(Console.WriteLine);

        return [];
    }

    public Task Sample()
    {
        throw new NotImplementedException();
    }
}
