using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Thor_Bar
{
    public partial class FormPedidoMesa : Form
    {
        private int mesaAsignada;
        private int idPedido = 0;
        //private DataGridView dgvProductos;
        private Button btnConfirmar;
        private Button btnCerrarPedido;

        public FormPedidoMesa(int numeroMesa)
        {
            this.mesaAsignada = numeroMesa;
            InicializarFormulario();
            InicializarControles();
            CargarProductos();
        }

        public FormPedidoMesa(int numeroMesa, int idPedido)
        {
            this.mesaAsignada = numeroMesa;
            this.idPedido = idPedido;
            InicializarFormulario();
            InicializarControles();
            CargarPedidoExistente();
        }

        private void InicializarFormulario()
        {
            this.Text = $"🧾 Pedido - Mesa {mesaAsignada}";
            this.Size = new Size(400, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.WhiteSmoke;
        }

        private void InicializarControles()
        {
            dgvProductos = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(340, 360),
                AllowUserToAddRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White
            };
            dgvProductos.CellValueChanged += DgvProductos_CellValueChanged;
            dgvProductos.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvProductos.IsCurrentCellDirty)
                    dgvProductos.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            this.Controls.Add(dgvProductos);

            btnConfirmar = new Button
            {
                Text = "✅ Confirmar Pedido",
                Size = new Size(340, 40),
                Location = new Point(20, 400),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnConfirmar.Click += BtnConfirmar_Click;
            this.Controls.Add(btnConfirmar);

            btnCerrarPedido = new Button
            {
                Text = "🧾 Cerrar Pedido",
                Size = new Size(340, 40),
                Location = new Point(20, 400),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };
            btnCerrarPedido.Click += BtnCerrarPedido_Click;
            this.Controls.Add(btnCerrarPedido);
        }

        private void CargarProductos()
        {
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();
                string query = "SELECT id, nombre, stock, precio FROM productos";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable tabla = new DataTable();
                    adapter.Fill(tabla);
                    tabla.Columns.Add("cantidad", typeof(int));
                    foreach (DataRow fila in tabla.Rows)
                        fila["cantidad"] = 0;

                    dgvProductos.DataSource = tabla;
                    dgvProductos.Columns["nombre"].ReadOnly = true;
                    dgvProductos.Columns["stock"].ReadOnly = true;
                    dgvProductos.Columns["cantidad"].ReadOnly = false;
                    dgvProductos.Columns["precio"].ReadOnly = true;
                }
            }

            dgvProductos.Columns["id"].Visible = false;
            dgvProductos.Columns["nombre"].HeaderText = "Producto";
            dgvProductos.Columns["stock"].HeaderText = "Stock";
            dgvProductos.Columns["cantidad"].HeaderText = "Cantidad";
            dgvProductos.Columns["precio"].HeaderText = "Precio";
        }

        private void CargarPedidoExistente()
        {
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();
                string query = @"
                    SELECT p.id, p.nombre, p.stock, d.cantidad, p.precio 
                    FROM detalles_pedido d
                    JOIN productos p ON d.producto_id = p.id
                    WHERE d.pedido_id = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", idPedido);
                    using (var adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable tabla = new DataTable();
                        adapter.Fill(tabla);

                        dgvProductos.DataSource = tabla;
                        dgvProductos.Columns["id"].Visible = false;
                        dgvProductos.Columns["nombre"].HeaderText = "Producto";
                        dgvProductos.Columns["stock"].HeaderText = "Stock";
                        dgvProductos.Columns["cantidad"].HeaderText = "Cantidad";
                        dgvProductos.Columns["precio"].HeaderText = "Precio";

                        dgvProductos.Columns["nombre"].ReadOnly = true;
                        dgvProductos.Columns["stock"].ReadOnly = true;
                        dgvProductos.Columns["cantidad"].ReadOnly = true;
                        dgvProductos.Columns["precio"].ReadOnly = true;

                    }
                }
            }

            btnConfirmar.Visible = false;
            btnCerrarPedido.Visible = true;
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();
                var insertPedido = new SQLiteCommand("INSERT INTO pedidos (mesa, estado) VALUES (@mesa, 'abierto')", conn);
                insertPedido.Parameters.AddWithValue("@mesa", mesaAsignada);
                insertPedido.ExecuteNonQuery();
                long pedidoId = conn.LastInsertRowId;

                foreach (DataGridViewRow fila in dgvProductos.Rows)
                {
                    if (fila.IsNewRow) continue;
                    int cantidad = Convert.ToInt32(fila.Cells["cantidad"].Value);
                    if (cantidad > 0)
                    {
                        int productoId = Convert.ToInt32(fila.Cells["id"].Value);
                        var insertDetalle = new SQLiteCommand(
                            "INSERT INTO detalles_pedido (pedido_id, producto_id, cantidad) VALUES (@pid, @prod, @cant)", conn);
                        insertDetalle.Parameters.AddWithValue("@pid", pedidoId);
                        insertDetalle.Parameters.AddWithValue("@prod", productoId);
                        insertDetalle.Parameters.AddWithValue("@cant", cantidad);
                        insertDetalle.ExecuteNonQuery();
                    }
                }

                var updateMesa = new SQLiteCommand("UPDATE mesas SET estado = 'ocupada' WHERE numero = @numero", conn);
                updateMesa.Parameters.AddWithValue("@numero", mesaAsignada);
                updateMesa.ExecuteNonQuery();

                MessageBox.Show("✅ Pedido generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void BtnCerrarPedido_Click(object sender, EventArgs e)
        {
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();

                var cerrarPedido = new SQLiteCommand("DELETE FROM pedidos WHERE id = @id", conn);
                cerrarPedido.Parameters.AddWithValue("@id", idPedido);
                cerrarPedido.ExecuteNonQuery();
           


                var liberarMesa = new SQLiteCommand("UPDATE mesas SET estado = 'libre' WHERE numero = @numero", conn);
                liberarMesa.Parameters.AddWithValue("@numero", mesaAsignada);
                liberarMesa.ExecuteNonQuery();

                MessageBox.Show("🧾 Pedido cerrado y mesa liberada.", "Cierre exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void DgvProductos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProductos.Columns[e.ColumnIndex].Name != "cantidad")
                return;

            var fila = dgvProductos.Rows[e.RowIndex];
            if (!int.TryParse(fila.Cells["cantidad"].Value?.ToString(), out int cantidad) || cantidad < 0)
                fila.Cells["cantidad"].Value = 0;
        }
    }
}
