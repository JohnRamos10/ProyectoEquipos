using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;
using ProyectoEquiposs;

namespace ProyectoEquipos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProyectosController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<IEnumerable<Proyecto>> Get()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var proyectos = await connection.QueryAsync<Proyecto>("SELECT * FROM Proyecto");
            return proyectos;
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> Get(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var proyecto = await connection.QuerySingleOrDefaultAsync<Proyecto>(
                "SELECT * FROM Proyecto WHERE id = @id", new { id });
            if (proyecto == null) return NotFound();
            return proyecto;
        }


        // POST: api/Proyectos
        [HttpPost]
            public async Task<ActionResult> Post([FromBody] Proyecto proyecto)
            {
                using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
                var sql = "INSERT INTO Proyecto (nombre, descripcion, fechaProyecto) VALUES (@nombre, @descripcion, @fechaProyecto)";
                await connection.ExecuteAsync(sql, proyecto);
                return Ok();
            }
        // PUT: api/Proyectos/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Proyecto proyecto)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = "UPDATE Proyecto SET nombre = @nombre, descripcion = @descripcion, fechaProyecto = @fechaProyecto WHERE id = @id";
            var affected = await connection.ExecuteAsync(sql, new { proyecto.nombre, proyecto.descripcion, proyecto.fechaProyecto, id });
            if (affected == 0) return NotFound();
            return Ok();
        }
        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            var sql = "DELETE FROM Proyecto WHERE id = @id";
            var affected = await connection.ExecuteAsync(sql, new { id });
            if (affected == 0) return NotFound();
            return Ok();
        }
    }
}
