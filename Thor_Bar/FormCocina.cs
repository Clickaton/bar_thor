using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Thor_Bar.Helpers;

namespace Thor_Bar
{
    public partial class FormCocina : Form
    {
        public FormCocina()
        {
            InitializeComponent();
            this.Load += FormCocina_Load;
        }

        public class ProductoDelPedido
        {
            public string Producto { get; set; }
            public int Cantidad { get; set; }
        }

        public class PedidoAgrupado
        {
            public int PedidoId { get; set; }
            public int Mesa { get; set; }
            public List<ProductoDelPedido> Detalles { get; set; }

            public PedidoAgrupado()
            {
                Detalles = new List<ProductoDelPedido>();
            }
        }

        private void FormCocina_Load(object sender, EventArgs e)
        {
            List<PedidoAgrupado> pedidos = ObtenerPedidosAgrupados();
            MostrarPedidosAgrupados(pedidos);
        }

        private List<PedidoAgrupado> ObtenerPedidosAgrupados()
        {
            List<PedidoAgrupado> lista = new List<PedidoAgrupado>();

            try
            {
                DatabaseConnection db = new DatabaseConnection();
                db.OpenConnection();

                string query = @"
                    SELECT p.id AS PedidoId, p.mesa, pr.nombre AS Producto, dp.cantidad
                    FROM pedidos p
                    JOIN detalles_pedido dp ON p.id = dp.pedido_id
                    JOIN productos pr ON pr.id = dp.producto_id
                    WHERE p.estado IS NULL OR p.estado = 'pendiente'
                    ORDER BY p.id DESC";

                SQLiteCommand cmd = new SQLiteCommand(query, db.GetConnection());
                SQLiteDataReader reader = cmd.ExecuteReader();

                Dictionary<int, PedidoAgrupado> agrupados = new Dictionary<int, PedidoAgrupado>();

                while (reader.Read())
                {
                    int pedidoId = Convert.ToInt32(reader["PedidoId"]);
                    int mesa = Convert.ToInt32(reader["mesa"]);
                    string producto = reader["Producto"].ToString();
                    int cantidad = Convert.ToInt32(reader["cantidad"]);

                    if (!agrupados.ContainsKey(pedidoId))
                    {
                        PedidoAgrupado nuevoPedido = new PedidoAgrupado();
                        nuevoPedido.PedidoId = pedidoId;
                        nuevoPedido.Mesa = mesa;
                        agrupados[pedidoId] = nuevoPedido;
                    }

                    ProductoDelPedido detalle = new ProductoDelPedido();
                    detalle.Producto = producto;
                    detalle.Cantidad = cantidad;

                    agrupados[pedidoId].Detalles.Add(detalle);
                }

                lista = new List<PedidoAgrupado>(agrupados.Values);
                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener pedidos: " + ex.Message);
            }

            return lista;
        }

        private void MostrarPedidosAgrupados(List<PedidoAgrupado> pedidos)
        {
            flpPedidos.Controls.Clear();

            foreach (PedidoAgrupado pedido in pedidos)
            {
                Panel tarjeta = new Panel();
                tarjeta.Width = 220;
                tarjeta.Height = 150;
                tarjeta.BackColor = Color.AntiqueWhite;
                tarjeta.Margin = new Padding(10);
                tarjeta.BorderStyle = BorderStyle.FixedSingle;

                Label lblCabecera = new Label();
                lblCabecera.Text = $"🧾 Pedido #{pedido.PedidoId} - Mesa {pedido.Mesa}";
                lblCabecera.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblCabecera.Dock = DockStyle.Top;
                lblCabecera.Height = 30;
                lblCabecera.TextAlign = ContentAlignment.MiddleCenter;

                Label lblDetalle = new Label();
                lblDetalle.Dock = DockStyle.Fill;
                lblDetalle.Padding = new Padding(10);
                lblDetalle.Font = new Font("Segoe UI", 9);
                lblDetalle.TextAlign = ContentAlignment.TopLeft;

                List<string> lineas = new List<string>();
                foreach (ProductoDelPedido d in pedido.Detalles)
                {
                    lineas.Add($"• {d.Producto} x{d.Cantidad}");
                }
                lblDetalle.Text = string.Join(Environment.NewLine, lineas);

                tarjeta.Controls.Add(lblDetalle);
                tarjeta.Controls.Add(lblCabecera);
                flpPedidos.Controls.Add(tarjeta);
            }
        }

        // Navegación
        private void lblMesas_Click(object sender, EventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            if (main != null)
                main.AbrirFormulario(new FormMesas());
        }

        private void flpPedidos_Paint(object sender, PaintEventArgs e)
        {
            // Vacío. Lo podés dejar así o eliminarlo si no lo necesitás.
        }

        private void lblStock_Click(object sender, EventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            if (main != null)
                main.AbrirFormulario(new FormStock());
        }

        private void lblAdmin_Click(object sender, EventArgs e)
        {
            FormMain main = this.MdiParent as FormMain;
            if (main != null)
                main.AbrirFormulario(new FormAdmin());
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            SesionHelper.CerrarSesion(this.MdiParent); 
        }
    }
}
