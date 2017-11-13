using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Actividad
    {
        public Int16 IdActividad { get; set; }
        public String Descripcion { get; set; }
        public String Dias { get; set; }
        public String Horarios { get; set; }
        public String Nombre { get; set; }
        public String Dificultad { get; set; }
        public int Estado { get; set; }        
    }
}
