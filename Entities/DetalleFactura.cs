using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DetalleFactura
    {
        public Int16 IdDetalle { get; set; }
        public String Detalle { get; set; }
        public Decimal Precio { get; set; }
        public Int16 Cantidad { get; set; }
    }
}
