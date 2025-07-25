using Microsoft.AspNetCore.Mvc;
using ProyectoEquiposs;
using Dapper;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoEquipos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase

    {
        private readonly IConfiguration _config;
        public UsuariosController(IConfiguration config)
        {
            _config = config;
        }
        // GET: api/Usuarios
        [HttpGet]
        public async Task<IEnumerable<Usuario>> Get()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppContext"));
            var usuarios = await connection.QueryAsync<Usuario>("SELECT * FROM Usuario");
            return usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppContext"));
            var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>(
                "SELECT * FROM Usuario WHERE id = @id", new { id });
            if (usuario == null) return NotFound();
            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppContext"));
            var sql = @"INSERT INTO Usuario (nombre, apellido, cedula, FechaNacimiento)
                        VALUES (@nombre, @apellido, @cedula, @FechaNacimiento)";
            await connection.ExecuteAsync(sql, usuario);
            return Ok();
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppContext"));
            var sql = @"UPDATE Usuario SET nombre = @nombre, apellido = @apellido, cedula = @cedula, FechaNacimiento = @FechaNacimiento
                        WHERE id = @id";
            var affected = await connection.ExecuteAsync(sql, new
            {
                usuario.nombre,
                usuario.apellido,
                usuario.cedula,
                usuario.FechaNacimiento,
                id
            });
            if (affected == 0) return NotFound();
            return Ok();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("AppContext"));
            var sql = "DELETE FROM Usuario WHERE id = @id";
            var affected = await connection.ExecuteAsync(sql, new { id });
            if (affected == 0) return NotFound();
            return Ok();
        }
    }
}
