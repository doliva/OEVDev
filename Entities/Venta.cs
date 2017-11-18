using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Venta
    {
        public String Codigo { get; set; }
        public Cliente ClienteVenta { get; set; }
        public Factura FacturaVenta { get; set; }

    }
}
