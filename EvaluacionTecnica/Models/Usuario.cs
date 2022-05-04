using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public String nombreUsuario { get; set; }
        public String contraseña { get; set; }
        public int idProducto { get; set; }
    }
}
