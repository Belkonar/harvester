using Npgsql;

namespace harvester.Data;

public static class DataExtensions
{
    public static async Task<List<T>> MapRowsAsync<T>(this NpgsqlDataReader reader) where T : IDto<T>
    {
        var list = new List<T>();

        while (await reader.ReadAsync())
        {
            list.Add(T.Map(reader));
        }

        return list;
    }

    /// <summary>
    /// Blocking version of the mapper for streaming. Should ideally never be needed.
    /// </summary>
    /// <param name="reader">A reader with some rows.</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<T> MapRows<T>(this NpgsqlDataReader reader) where T : IDto<T>
    {
        while (reader.Read())
        {
            yield return T.Map(reader);
        }
    }
}