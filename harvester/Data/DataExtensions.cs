using Npgsql;

namespace harvester.Data;

public static class DataExtensions
{
    public static async Task<List<T>> MapRows<T>(this NpgsqlDataReader reader) where T : IDto<T>
    {
        var list = new List<T>();

        while (await reader.ReadAsync())
        {
            list.Add(T.Map(reader));
        }

        return list;
    }
}