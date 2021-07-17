using FactoryMethod.Enums;
using FactoryMethod.Interfaces;
using System.Data;

namespace FactoryMethod.Repository
{
    public abstract class DbConnection1RepositoryBase
    {
        public IDbConnection DbConnection { get; private set; }

        public DbConnection1RepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
                this.DbConnection = dbConnectionFactory.CreateDbConnection(DatabaseConnectionName.SQLServer);
        }

       
    }
}