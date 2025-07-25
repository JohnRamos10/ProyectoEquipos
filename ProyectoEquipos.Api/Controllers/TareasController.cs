using Microsoft.AspNetCore.Mvc;
using ProyectoEquiposs;
using Dapper;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProyectoEquipos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase

    {
        private readonly IConfiguration _config;
        public TareasController(IConfiguration config)
        {
            _config = config;
        }
        // GET: api/Tareas
        [HttpGet]
        public async Task<IEnumerable<Tarea>> Get()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var tareas = await connection.QueryAsync<Tarea>("SELECT * FROM Tarea");
            return tareas;
        }

        // GET: api/Tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> Get(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var tarea = await connection.QuerySingleOrDefaultAsync<Tarea>(
                "SELECT * FROM Tarea WHERE id = @id", new { id });
            if (tarea == null) return NotFound();
            return tarea;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = @"INSERT INTO Usuario (nombre, apellido, cedula, FechaNacimiento)
                        VALUES (@nombre, @apellido, @cedula, @FechaNacimiento)";
            await connection.ExecuteAsync(sql, usuario);
            return Ok();
        }

        // PUT: api/Tareas/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Tarea tarea)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = @"UPDATE Tarea SET Estado = @Estado, prioridad = @prioridad, FechaVencimiento = @FechaVencimiento,
                        ProyectoId = @ProyectoId, UsuarioAsignadoId = @UsuarioAsignadoId WHERE id = @id";
            var affected = await connection.ExecuteAsync(sql, new
            {
                tarea.Estado,
                tarea.prioridad,
                tarea.FechaVencimiento,
                tarea.ProyectoId,
                tarea.UsuarioAsignadoId,
                id
            });
            if (affected == 0) return NotFound();
            return Ok();
        }

        // DELETE: api/Tareas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = "DELETE FROM Tarea WHERE id = @id";
            var affected = await connection.ExecuteAsync(sql, new { id });
            if (affected == 0) return NotFound();
            return Ok();
        }
    }
}
