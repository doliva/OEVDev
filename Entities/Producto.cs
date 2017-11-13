using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Producto
    {
        public Int16 IdProducto { get; set; }
        public Actividad Actividad { get; set; }
        public int Estado { get; set; }
        public String Itinerario { get; set; }
        public String Nombre { get; set; }
        public Decimal Precio { get; set; }
        public String TipoProducto { get; set; }
        
    }
}
