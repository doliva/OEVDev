using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Voucher
    {
        public Int16 Cantindad { get; set; }
        public Cliente Cliente { get; set; }
        public String Detalle { get; set; }
        public String Estado { get; set; }
        public DateTime Fecha { get; set; }
        public String Numero { get; set; }
        public Producto Producto { get; set; }
        // TODO: Hace cosas
        public DateTime FechaInicio { get; set; }
        // TODO: Hace cosas
        public DateTime FechaFin { get; set; }

    }
}
