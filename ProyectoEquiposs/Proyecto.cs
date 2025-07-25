using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoEquiposs
{
    public class Proyecto
    {
        [Key] public int id { get; set; }

        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaProyecto { get; set; }
        public List<Tarea>? Tareas { get; set; }
    }
}
