using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thor_Bar.Clases
{
    internal class DetallePedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal total { get; set; }

        public  DetallePedido(int id, int pedidoId, int productoId, int cantidad, decimal total)
        {
            Id = id;
            PedidoId = pedidoId;
            ProductoId = productoId;
            Cantidad = cantidad;
            this.total = total;
        }

        public DetallePedido()
        {
        }
    }
}
