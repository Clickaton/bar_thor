using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thor_Bar
{
    internal class Caja
    {
        private DateTime fecha;
        private decimal totalFacturado;
        private decimal valorInicial;
        private decimal cierreCaja;

        public Caja(DateTime fecha, decimal totalFacturado, decimal valorInicial, decimal cierreCaja)
        {
            Fecha = fecha;
            TotalFacturado = totalFacturado;
            ValorInicial = valorInicial;
            CierreCaja = cierreCaja;
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public decimal TotalFacturado
        {
            get { return totalFacturado; }
            set { totalFacturado = value; }
        }

        public decimal ValorInicial
        {
            get { return valorInicial; }
            set { valorInicial = value; }
        }

        public decimal CierreCaja
        {
            get { return cierreCaja; }
            set { cierreCaja = value; }
        }
    }
}
