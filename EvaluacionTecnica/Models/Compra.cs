using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionTecnica.Models
{
    public class Compra
    {
        public int idUsuario { get; set; }
        public int idProducto { get; set; }
        public String NombreUsuario { get; set; }
        public String NombreCompra { get; set; }
        public int Precio { get; set; }
    }
}
