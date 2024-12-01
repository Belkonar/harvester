using Npgsql;
using NpgsqlTypes;

namespace harvester.Data;

public class DtoSpecifier : IDto<DtoSpecifier>, ISaveableDto
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public List<string> Options { get; set; } = [];

    public static DtoSpecifier Map(NpgsqlDataReader reader)
    {
        return new DtoSpecifier()
        {
            Id = reader.GetGuid(reader.GetOrdinal("id")),
            Name = reader.GetString(reader.GetOrdinal("name")),
            Options = reader.GetFieldValue<List<string>>(reader.GetOrdinal("options")) ?? []
        };
    }

    public async Task Save(NpgsqlDataSource source)
    {
        Id ??= Guid.NewGuid();

        const string sql = "CALL upsert_specifier(@id, @name, @options);";

        await using var cmd = source.CreateCommand(sql);

        cmd.Parameters.AddWithValue("id", NpgsqlDbType.Uuid, Id);
        cmd.Parameters.AddWithValue("name", NpgsqlDbType.Text, Name);
        cmd.Parameters.AddWithValue("options", NpgsqlDbType.Array | NpgsqlDbType.Text, Options);

        await cmd.ExecuteNonQueryAsync();
    }
}