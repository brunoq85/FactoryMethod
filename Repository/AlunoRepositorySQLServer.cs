using System.Collections.Generic;
using Dapper;
using FactoryMethod.Entities;
using FactoryMethod.Interfaces;

namespace FactoryMethod.Repository
{
    public class AlunoRepositorySQLServer : DbConnection1RepositoryBase, IAlunoRepositorySQLServer
    {
        public AlunoRepositorySQLServer(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

        public IEnumerable<Aluno> GetAll()
        {
            const string sql = @"SELECT * FROM Alunos";

            // No need to use using statement. Dapper will automatically
            // open, close and dispose the connection for you.
            return base.DbConnection.Query<Aluno>(sql);
        }
    }
}