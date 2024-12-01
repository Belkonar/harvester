using Npgsql;

namespace harvester.Data;

public interface IDto<out T>
{
    static abstract T Map(NpgsqlDataReader reader);
}