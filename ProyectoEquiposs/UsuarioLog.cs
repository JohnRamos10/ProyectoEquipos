using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEquiposs
{
    public class UsuarioLog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La clave es obligatoria.")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }


        [Required]
        public string Rol { get; set; }

    }
}
