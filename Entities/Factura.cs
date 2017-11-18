using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Factura
    {
        public Int16 IdFactura { get; set; }
        public DateTime Fecha { get; set; }
        public String Estado { get; set; }
        public Decimal Importe { get; set; }
        public String TipoPago{ get; set; }
        public List<DetalleFactura> Items { get; set; }
    }
}
