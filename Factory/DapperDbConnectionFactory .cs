using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using FactoryMethod.Entities;
using FactoryMethod.Enums;
using FactoryMethod.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;

namespace FactoryMethod.Factory
{
    public class DapperDbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<DatabaseConnectionName, string> _connectionDict;
        private readonly ConfigAPI _configAPI;

        public DapperDbConnectionFactory(IDictionary<DatabaseConnectionName, string> connectionDict,
                                         IOptions<ConfigAPI> configAPI)
        {
            _connectionDict = connectionDict;
            _configAPI = configAPI.Value;
        }

        public IDbConnection CreateDbConnection(DatabaseConnectionName connectionName)
        {
            IDbConnection conn;
            string connectionString = null;

            if (_connectionDict.TryGetValue(connectionName, out connectionString))
            {
                if(_configAPI.DatabaseStorage.ToString() == "SQL Server")
                    return  new SqlConnection(connectionString);
                else if(_configAPI.DatabaseStorage.ToString() == "SqLite")
                    return  new SqliteConnection(connectionString);  
            }

            throw new ArgumentNullException();
        }
    }
}