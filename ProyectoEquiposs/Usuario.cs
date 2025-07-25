using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEquiposs
{
    public class Usuario
    {
        [Key] public int id { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cedula { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
