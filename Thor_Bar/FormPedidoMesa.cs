using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using System.Text; // Necesario para StringBuilder

namespace Thor_Bar
{
    public partial class FormPedidoMesa : Form
    {
        private int mesaAsignada;
        private int idPedido = 0;
        private Button btnConfirmar;
        private Button btnCerrarPedido;
        private Label lblTotal;
        private Label lblEstadoPedido;

        // --- INICIO: Agregado para la comunicación con FormAdmin ---
        // Define un delegado para el evento
        public delegate void PedidoCerradoEventHandler(object sender, EventArgs e);

        // Declara el evento
        public event PedidoCerradoEventHandler PedidoCerrado;
        // --- FIN: Agregado para la comunicación con FormAdmin ---

        public FormPedidoMesa(int numeroMesa)
        {
            this.mesaAsignada = numeroMesa;
            InicializarFormulario();
            InicializarControles();
            CargarProductos(); // Para nuevos pedidos, muestra todos los productos disponibles
        }

        public FormPedidoMesa(int numeroMesa, int idPedido)
        {
            this.mesaAsignada = numeroMesa;
            this.idPedido = idPedido;
            InicializarFormulario();
            InicializarControles();
            CargarPedidoExistente(); // Para pedidos existentes, carga los productos de ese pedido
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
            // DataGridView de Productos
            dgvProductos = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(340, 310),
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

            // Etiqueta de Estado del Pedido
            lblEstadoPedido = new Label
            {
                Text = "",
                Location = new Point(20, 340),
                Size = new Size(340, 20),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.DarkOrange
            };
            this.Controls.Add(lblEstadoPedido);

            // Etiqueta de Total del Pedido
            lblTotal = new Label
            {
                Text = "Total: $0.00",
                Location = new Point(20, 365),
                Size = new Size(340, 25),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight
            };
            this.Controls.Add(lblTotal);

            // Botón Confirmar Pedido (visible para nuevos pedidos)
            btnConfirmar = new Button
            {
                Text = "✅ Confirmar Pedido",
                Size = new Size(340, 40),
                Location = new Point(20, 400),
                BackColor = Color.FromArgb(40, 167, 69), // Verde
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnConfirmar.Click += BtnConfirmar_Click;
            this.Controls.Add(btnConfirmar);

            // Botón Cerrar Pedido (visible para pedidos existentes)
            btnCerrarPedido = new Button
            {
                Text = "🧾 Cerrar Pedido",
                Size = new Size(340, 40),
                Location = new Point(20, 400),
                BackColor = Color.DarkRed, // Rojo oscuro
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false // Inicialmente oculto
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
                    tabla.Columns.Add("Subtotal", typeof(decimal)); // Añadir columna Subtotal
                    foreach (DataRow fila in tabla.Rows)
                    {
                        fila["cantidad"] = 0;
                        fila["Subtotal"] = 0m; // Inicializar en 0
                    }

                    dgvProductos.DataSource = tabla;
                    dgvProductos.Columns["nombre"].ReadOnly = true;
                    dgvProductos.Columns["stock"].ReadOnly = true;
                    dgvProductos.Columns["cantidad"].ReadOnly = false;
                    dgvProductos.Columns["precio"].ReadOnly = true;
                    dgvProductos.Columns["Subtotal"].ReadOnly = true; // Subtotal es calculado

                    // Configurar encabezados de columna
                    dgvProductos.Columns["id"].Visible = false;
                    dgvProductos.Columns["nombre"].HeaderText = "Producto";
                    dgvProductos.Columns["stock"].HeaderText = "Stock";
                    dgvProductos.Columns["cantidad"].HeaderText = "Cantidad";
                    dgvProductos.Columns["precio"].HeaderText = "P. Unitario";
                    dgvProductos.Columns["Subtotal"].HeaderText = "Subtotal";
                }
            }
        }

        private void CargarPedidoExistente()
        {
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();

                // Consulta para obtener productos con su cantidad y subtotal para un pedido específico
                string query = @"
                    SELECT p.id, p.nombre, p.stock, d.cantidad, p.precio, (d.cantidad * p.precio) AS Subtotal
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
                        dgvProductos.Columns["precio"].HeaderText = "P. Unitario";
                        dgvProductos.Columns["Subtotal"].HeaderText = "Subtotal";

                        // En pedidos existentes, todo es de solo lectura (hasta que se implemente una edición de pedido)
                        dgvProductos.Columns["nombre"].ReadOnly = true;
                        dgvProductos.Columns["stock"].ReadOnly = true;
                        dgvProductos.Columns["cantidad"].ReadOnly = true;
                        dgvProductos.Columns["precio"].ReadOnly = true;
                        dgvProductos.Columns["Subtotal"].ReadOnly = true;
                    }
                }

                // Calcula y muestra el total del pedido existente
                decimal total = CalcularTotalPedido(idPedido);
                lblTotal.Text = $"Total: ${total:N2}";

                // Obtiene y muestra el estado del pedido
                string estadoPedido = GetOrderStatus(idPedido);
                lblEstadoPedido.Text = $"Estado: {estadoPedido.ToUpper()}";

                // Colorea el estado según su valor
                if (estadoPedido.ToLower() == "listo")
                {
                    lblEstadoPedido.ForeColor = Color.Green;
                }
                else if (estadoPedido.ToLower() == "abierto")
                {
                    lblEstadoPedido.ForeColor = Color.Orange;
                }
                else if (estadoPedido.ToLower() == "en proceso")
                {
                    lblEstadoPedido.ForeColor = Color.Blue;
                }
                else
                {
                    lblEstadoPedido.ForeColor = Color.DarkGray;
                }
            }

            // Para pedidos existentes, el botón "Confirmar" se oculta y "Cerrar Pedido" se muestra
            btnConfirmar.Visible = false;
            btnCerrarPedido.Visible = true;
        }

        private decimal CalcularTotalPedido(int pedidoId)
        {
            decimal total = 0;
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();
                string query = "SELECT SUM(dp.cantidad * p.precio) FROM detalles_pedido dp JOIN productos p ON dp.producto_id = p.id WHERE dp.pedido_id = @pedidoId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        total = Convert.ToDecimal(result);
                    }
                }
            }
            return total;
        }

        private string GetOrderStatus(int pedidoId)
        {
            string status = "desconocido";
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();
                string query = "SELECT estado FROM pedidos WHERE id = @pedidoId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        status = result.ToString();
                    }
                }
            }
            return status;
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction()) // Inicia una transacción para el nuevo pedido
                {
                    try
                    {
                        // Insertar el nuevo pedido en la tabla 'pedidos'
                        var insertPedido = new SQLiteCommand("INSERT INTO pedidos (mesa, estado) VALUES (@mesa, 'abierto')", conn, transaction);
                        insertPedido.Parameters.AddWithValue("@mesa", mesaAsignada);
                        insertPedido.ExecuteNonQuery();
                        long pedidoId = conn.LastInsertRowId; // Obtener el ID del pedido recién insertado

                        // Insertar los detalles del pedido en 'detalles_pedido'
                        foreach (DataGridViewRow fila in dgvProductos.Rows)
                        {
                            if (fila.IsNewRow) continue;
                            int cantidad = Convert.ToInt32(fila.Cells["cantidad"].Value);
                            if (cantidad > 0)
                            {
                                int productoId = Convert.ToInt32(fila.Cells["id"].Value);
                                var insertDetalle = new SQLiteCommand(
                                    "INSERT INTO detalles_pedido (pedido_id, producto_id, cantidad) VALUES (@pid, @prod, @cant)", conn, transaction);
                                insertDetalle.Parameters.AddWithValue("@pid", pedidoId);
                                insertDetalle.Parameters.AddWithValue("@prod", productoId);
                                insertDetalle.Parameters.AddWithValue("@cant", cantidad);
                                insertDetalle.ExecuteNonQuery();

                                // Opcional: Actualizar el stock del producto
                                var updateStock = new SQLiteCommand("UPDATE productos SET stock = stock - @cantidad WHERE id = @productoId", conn, transaction);
                                updateStock.Parameters.AddWithValue("@cantidad", cantidad);
                                updateStock.Parameters.AddWithValue("@productoId", productoId);
                                updateStock.ExecuteNonQuery();
                            }
                        }

                        // Actualizar el estado de la mesa a 'ocupada'
                        var updateMesa = new SQLiteCommand("UPDATE mesas SET estado = 'ocupada' WHERE numero = @numero", conn, transaction);
                        updateMesa.Parameters.AddWithValue("@numero", mesaAsignada);
                        updateMesa.ExecuteNonQuery();

                        transaction.Commit(); // Confirma la transacción
                        MessageBox.Show("✅ Pedido generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.idPedido = (int)pedidoId; // Asignar el ID al pedido actual
                        CargarPedidoExistente(); // Recargar el formulario para mostrar el pedido como existente
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Si algo falla, se revierte todo
                        MessageBox.Show($"Error al generar el pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // --- INICIO: Modificado para generar comprobante y validar estado ---
        private void BtnCerrarPedido_Click(object sender, EventArgs e)
        {
            string currentStatus = GetOrderStatus(idPedido);

            if (currentStatus.ToLower() != "listo")
            {
                MessageBox.Show("🚫 El pedido no se puede cerrar porque su estado no es 'LISTO'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Detiene la ejecución si el estado no es "listo"
            }

            using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction()) // Usa una transacción para asegurar la atomicidad
                {
                    try
                    {
                        // 1. Actualizar el estado del pedido a 'cerrado'
                        var cerrarPedido = new SQLiteCommand("UPDATE pedidos SET estado = 'cerrado' WHERE id = @id", conn, transaction);
                        cerrarPedido.Parameters.AddWithValue("@id", idPedido);
                        cerrarPedido.ExecuteNonQuery();

                        // 2. Liberar la mesa
                        var liberarMesa = new SQLiteCommand("UPDATE mesas SET estado = 'libre' WHERE numero = @numero", conn, transaction);
                        liberarMesa.Parameters.AddWithValue("@numero", mesaAsignada);
                        liberarMesa.ExecuteNonQuery();

                        // 3. Calcular el total final del pedido
                        decimal totalFinal = CalcularTotalPedido(idPedido);

                        // 4. Obtener los detalles de los productos para el comprobante
                        string detallesProductosTexto = GetOrderDetailsForReceipt(idPedido, conn, transaction);

                        // 5. Insertar el nuevo registro en la tabla 'comprobantes'
                        var insertComprobante = new SQLiteCommand(
                            "INSERT INTO comprobantes (pedido_id, mesa_numero, fecha_hora_cierre, total_pedido, detalles_productos) VALUES (@pedidoId, @mesaNum, @fechaCierre, @total, @detalles)", conn, transaction);
                        insertComprobante.Parameters.AddWithValue("@pedidoId", idPedido);
                        insertComprobante.Parameters.AddWithValue("@mesaNum", mesaAsignada);
                        insertComprobante.Parameters.AddWithValue("@fechaCierre", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); // Formato ISO 8601
                        insertComprobante.Parameters.AddWithValue("@total", totalFinal);
                        insertComprobante.Parameters.AddWithValue("@detalles", detallesProductosTexto);
                        insertComprobante.ExecuteNonQuery();

                        transaction.Commit(); // Confirma todos los cambios si todo fue exitoso
                        MessageBox.Show("🧾 Pedido cerrado, recibo generado y mesa liberada.", "Cierre exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Lanza el evento para notificar a FormAdmin que un pedido ha sido cerrado
                        PedidoCerrado?.Invoke(this, EventArgs.Empty);

                        this.Close(); // Cierra el formulario de pedido
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Revierte todos los cambios si ocurre algún error
                        MessageBox.Show($"Error al cerrar el pedido: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Nuevo método auxiliar para obtener los detalles del producto como texto para el comprobante
        private string GetOrderDetailsForReceipt(int pedidoId, SQLiteConnection conn, SQLiteTransaction transaction)
        {
            // Usamos StringBuilder para construir la cadena de texto de manera eficiente
            StringBuilder sb = new StringBuilder();
            string query = @"
                SELECT p.nombre, dp.cantidad, p.precio
                FROM detalles_pedido dp
                JOIN productos p ON dp.producto_id = p.id
                WHERE dp.pedido_id = @pedidoId";

            using (var cmd = new SQLiteCommand(query, conn, transaction)) // Pasamos la transacción al comando
            {
                cmd.Parameters.AddWithValue("@pedidoId", pedidoId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = reader["nombre"].ToString();
                        int cantidad = Convert.ToInt32(reader["cantidad"]);
                        decimal precioUnitario = Convert.ToDecimal(reader["precio"]);
                        sb.AppendLine($"{nombre} x {cantidad} (${precioUnitario:N2} c/u)"); // Formato para el recibo
                    }
                }
            }
            return sb.ToString();
        }
        // --- FIN: Modificado para generar comprobante y validar estado ---

        private void DgvProductos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Asegura que el cambio es en la columna "cantidad" y no en una fila nueva o inválida
            if (e.RowIndex < 0 || dgvProductos.Columns[e.ColumnIndex].Name != "cantidad")
                return;

            var fila = dgvProductos.Rows[e.RowIndex];

            // Valida que la cantidad sea un número entero no negativo
            if (!int.TryParse(fila.Cells["cantidad"].Value?.ToString(), out int cantidad) || cantidad < 0)
            {
                fila.Cells["cantidad"].Value = 0; // Restablece a 0 si es inválido
                cantidad = 0; // Asegura que la variable 'cantidad' también sea 0
            }

            // Calcula y actualiza el subtotal de la fila
            if (fila.Cells["precio"].Value != null) // Asegura que el precio no sea nulo
            {
                decimal precio = Convert.ToDecimal(fila.Cells["precio"].Value);
                fila.Cells["Subtotal"].Value = precio * cantidad;
            }

            // Recalcula el total del pedido solo si es un pedido nuevo (idPedido == 0)
            // Para pedidos existentes, el total no debe cambiar por interacción del usuario en esta vista.
            if (idPedido == 0)
            {
                RecalculateCurrentOrderTotal();
            }
        }

        private void RecalculateCurrentOrderTotal()
        {
            decimal currentTotal = 0;
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (!row.IsNewRow && row.Cells["cantidad"].Value != null && row.Cells["precio"].Value != null)
                {
                    int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value);
                    decimal precio = Convert.ToDecimal(row.Cells["precio"].Value);
                    currentTotal += (cantidad * precio);
                }
            }
            lblTotal.Text = $"Total: ${currentTotal:N2}";
        }
    }
}