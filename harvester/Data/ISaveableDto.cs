using System.Xml.Linq;
using Npgsql;

namespace harvester.Data;

public interface ISaveableDto
{
    Task Save(NpgsqlDataSource source);
}