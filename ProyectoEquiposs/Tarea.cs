using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEquiposs
{
    public class Tarea
    {
        [Key] public int id { get; set; }
        public string Estado { get; set; }

        public string prioridad { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int ProyectoId { get; set; }
        public int UsuarioAsignadoId { get; set; }

    }
}
