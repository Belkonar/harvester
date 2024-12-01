using System.Data;
using Npgsql;

namespace harvester.Data;

public class DtoSpecifier : IDto<DtoSpecifier>
{
    public Guid Id { get; set; }
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
}