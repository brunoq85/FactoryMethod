using System.Collections.Generic;
using FactoryMethod.Entities;

namespace FactoryMethod.Interfaces
{
    public interface IAlunoRepositorySQLServer
    {
         IEnumerable<Aluno> GetAll();
    }
}