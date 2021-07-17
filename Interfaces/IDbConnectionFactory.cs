using System.Data;
using FactoryMethod.Enums;

namespace FactoryMethod.Interfaces
{
    public interface IDbConnectionFactory
    {
         IDbConnection CreateDbConnection(DatabaseConnectionName connectionName);
    }
}