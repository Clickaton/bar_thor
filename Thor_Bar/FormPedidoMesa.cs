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
       // private DataGridView dgvProductos;
        private Button btnConfirmar;

        public FormPedidoMesa(int numeroMesa)
        {
            mesaAsignada = numeroMesa;
            InicializarFormulario();
            InicializarControles();
            CargarProductos();
        }

        private void FormPedidoMesa_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }


        private void InicializarFormulario()
        {
            this.Text = $"🧾 Pedido - Mesa {mesaAsignada}";
            this.Size = new Size(400, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.WhiteSmoke;
        }
        private void DgvProductos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvProductos.Columns[e.ColumnIndex].Name != "cantidad")
                return;

            var fila = dgvProductos.Rows[e.RowIndex];
            int cantidad;

            if (!int.TryParse(fila.Cells["cantidad"].Value?.ToString(), out cantidad) || cantidad < 0)
            {
                fila.Cells["cantidad"].Value = 0;
                return;
            }

            // Acá podrías actualizar un resumen, total o incluso mostrar un preview del pedido
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
        }

        private void CargarProductos()
        {
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();
                string query = "SELECT id, nombre, stock FROM productos";

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

                }
            }

            dgvProductos.Columns["id"].Visible = false;
            dgvProductos.Columns["nombre"].HeaderText = "Producto";
            dgvProductos.Columns["stock"].HeaderText = "Stock";
            dgvProductos.Columns["cantidad"].HeaderText = "Cantidad";
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();

                var insertPedido = new SQLiteCommand("INSERT INTO pedidos (mesa) VALUES (@mesa)", conn);
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
                            "INSERT INTO detalles_pedido (pedido_id, producto_id, cantidad) VALUES (@pid, @prod, @cant)",
                            conn
                        );
                        insertDetalle.Parameters.AddWithValue("@pid", pedidoId);
                        insertDetalle.Parameters.AddWithValue("@prod", productoId);
                        insertDetalle.Parameters.AddWithValue("@cant", cantidad);
                        insertDetalle.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("✅ Pedido generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var updateMesa = new SQLiteCommand("UPDATE mesas SET estado = 'ocupada' WHERE numero = @numero", conn);
                updateMesa.Parameters.AddWithValue("@numero", mesaAsignada);
                updateMesa.ExecuteNonQuery();

                this.Close();


            }
        }
    }
}