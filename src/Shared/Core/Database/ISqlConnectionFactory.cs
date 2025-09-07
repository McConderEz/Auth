using System.Data;

namespace Core.Database;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}