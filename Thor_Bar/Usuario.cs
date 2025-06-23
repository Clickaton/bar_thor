using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thor_Bar
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string nombre {  get; set; }
        public string apellido { get; set; }
        public string documento { get; set; }
        public string contacto { get; set; }
        public bool estado { get; set; }
        public RolUsuario rol { get; set; }
        public decimal sueldo { get; set; }




    }
}
