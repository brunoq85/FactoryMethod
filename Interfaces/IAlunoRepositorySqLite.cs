using System.Collections.Generic;
using FactoryMethod.Entities;

namespace FactoryMethod.Interfaces
{
    public interface IAlunoRepositorySqLite
    {
         IEnumerable<Aluno> GetAll();
    }
}