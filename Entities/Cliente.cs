using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Cliente
    {
        public Int16 IdCliente { get; set; } 
        public String Apellido { get; set; }
        public String Nombre { get; set; }
        public String Dni { get; set; }
        public String Pasaporte { get; set; }
        public String Domicilio { get; set; }
        public String Ciudad { get; set; }
        public String Celular { get; set; }
        public String Email { get; set; }
        public int Estado { get; set; }
        public String Telefono { get; set; } 

    }
}
