using FactoryMethod.Enums;
using FactoryMethod.Interfaces;
using System.Data;

namespace FactoryMethod.Repository
{
    public abstract class DbConnection2RepositoryBase
    {
        public IDbConnection DbConnection { get; private set; }

        public DbConnection2RepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
                this.DbConnection = dbConnectionFactory.CreateDbConnection(DatabaseConnectionName.SqLite);
        }

    }
}