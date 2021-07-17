using Dapper;
using FactoryMethod.Entities;
using FactoryMethod.Interfaces;
using System.Collections.Generic;

namespace FactoryMethod.Repository
{
    public class AlunoRepositorySqLite : DbConnection2RepositoryBase, IAlunoRepositorySqLite
    {
        public AlunoRepositorySqLite(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory) { }

        public IEnumerable<Aluno> GetAll()
        {
            const string sql = @"SELECT * FROM Alunos";

            // No need to use using statement. Dapper will automatically
            // open, close and dispose the connection for you.
            return base.DbConnection.Query<Aluno>(sql);
        }
    }
}
