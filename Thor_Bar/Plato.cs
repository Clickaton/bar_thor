using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Thor_Bar
{
    public class Plato
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Image Imagen { get; set; }
        public int Stock { get; set; }  // Cantidad disponible
        public string Categoria { get; set; }
        public bool EsBebida { get; set; } // Para diferenciar si es comida o bebida
    }
}
