using System.Collections.Generic;
using FactoryMethod.Entities;
using FactoryMethod.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FactoryMethod.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoRepositorySQLServer _alunoRepositorySQLServer;
        private readonly IAlunoRepositorySqLite _alunoRepositorySqLite;
        private readonly ConfigAPI _configAPI;

        public AlunosController(IAlunoRepositorySQLServer alunoRepositorySQLServer, 
                                IAlunoRepositorySqLite alunoRepositorySqLite, 
                                IOptions<ConfigAPI> configAPI)
        {
            _alunoRepositorySQLServer = alunoRepositorySQLServer;
            _alunoRepositorySqLite = alunoRepositorySqLite;
            _configAPI = configAPI.Value;
        }

        [HttpGet("todos")]
        public IEnumerable<Aluno> GetAll()
        {
            if (_configAPI.DatabaseStorage == "SQL Server")
                return _alunoRepositorySQLServer.GetAll();

            return _alunoRepositorySqLite.GetAll();
        }
    }
}