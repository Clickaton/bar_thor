using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing; // Importa System.Drawing para Color y Font de UI
using System.Windows.Forms;
using Thor_Bar.Estilos;
using Thor_Bar.Helpers; // Asegúrate de que PdfGenerator esté en este namespace o en uno nuevo que crees
using Thor_Bar.Managers;

// Asegúrate de tener las referencias a iTextSharp.
// Si las instalaste por NuGet, estas directivas 'using' deberían ser válidas.
// Si no, verifica los nombres correctos de los paquetes y namespaces.
using iTextSharp.text; // Usado para elementos base de iTextSharp
using iTextSharp.text.pdf; // Usado para PdfPTable, PdfPCell, PdfWriter
using System.IO;


namespace Thor_Bar
{
    public partial class FormAdmin : Form
    {
        // Declara una instancia de tu nueva clase GenerarComprobante
        private GenerarComprobante _generarComprobante;

        public FormAdmin()
        {
            InitializeComponent();

            if (SesionGlobal.UsuarioActual != null)
            {
                lblNombreUsuario.Text = $"Bienvenido, {SesionGlobal.UsuarioActual.nombre}";
            }
            else
            {
                lblNombreUsuario.Text = "Usuario no identificado";
            }

            // Estas líneas buscan los controles dentro de tabComprobantes.
            // Asegúrate de que los nombres de los controles 'dgvComprobantes' y 'rtbDetallesComprobante'
            // en el diseñador de tu FormAdmin (dentro de la pestaña 'tabComprobantes')
            // coincidan exactamente con estos nombres.
            DataGridView dgv = tabComprobantes.Controls["dgvComprobantes"] as DataGridView;
            RichTextBox rtb = tabComprobantes.Controls["rtbDetallesComprobante"] as RichTextBox;

            if (dgv != null && rtb != null)
            {
                _generarComprobante = new GenerarComprobante(dgv, rtb);
            }
            else
            {
                MessageBox.Show("Error: Los controles 'dgvComprobantes' o 'rtbDetallesComprobante' no se encontraron. La gestión de comprobantes no funcionará correctamente.", "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblNombreUsuario_Click(object sender, EventArgs e)
        {
            // Puedes dejarlo vacío si no hay lógica asociada a este evento.
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            // Cargar enum RolUsuario en el ComboBox
            cmbRol.DataSource = Enum.GetValues(typeof(RolUsuario));
            VerificarYAgregarColumnas();
            List<Usuario> usuarios = ObtenerUsuarios();
            CargarUsuariosEnGrid(usuarios);
            EstilizarDataGridView();

            using (DatabaseConnection db = new DatabaseConnection())
            {
                db.OpenConnection();
                string query = "SELECT * FROM usuarios";
                SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            }

            dgvUsuarios.CellClick += dgvUsuarios_CellClick;

            CargarGastos();
            CalcularTotal();
            dgvGastos.CellClick += dgvGastos_CellClick;
            EstilosDataGrid.AplicarEstilo(dgvGastos);

            // Cargar los comprobantes usando el manager
            if (_generarComprobante != null)
            {
                _generarComprobante.CargarComprobantes();
            }

            // Suscribirse al evento SelectedIndexChanged del TabControl 'tabControlAdmin'.
            // Esto asegura que los comprobantes se recarguen cada vez que se selecciona su pestaña.
            tabAdmin.SelectedIndexChanged += TabControlAdmin_SelectedIndexChanged;
        }

        private void CargarGastos()
        {
            try
            {
                using (DatabaseConnection db = new DatabaseConnection()) // Uso de 'using' para asegurar cierre de conexión
                {
                    db.OpenConnection();

                    string query = "SELECT * FROM Gastos";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(query, db.GetConnection());
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvGastos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los gastos: " + ex.Message);
            }
        }

        private void CalcularTotal()
        {
            double total = 0;

            foreach (DataGridViewRow row in dgvGastos.Rows)
            {
                if (row.Cells["Monto"].Value != null)
                {
                    double monto;
                    if (double.TryParse(row.Cells["Monto"].Value.ToString(), out monto))
                    {
                        total += monto;
                    }
                }
            }

            lblTotal.Text = $"Total: ${total:N2}";
        }

        private List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (DatabaseConnection db = new DatabaseConnection()) // Uso de 'using' para asegurar cierre de conexión
                {
                    db.OpenConnection();

                    string query = @"SELECT id, user, pass, nombre, apellido, documento, contacto, estado, rol, sueldo FROM usuarios";
                    SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                NombreUsuario = reader["user"]?.ToString(),
                                Password = reader["pass"]?.ToString(),
                                nombre = reader["nombre"] != DBNull.Value ? reader["nombre"].ToString() : null,
                                apellido = reader["apellido"] != DBNull.Value ? reader["apellido"].ToString() : null,
                                documento = reader["documento"] != DBNull.Value ? reader["documento"].ToString() : null,
                                contacto = reader["contacto"] != DBNull.Value ? reader["contacto"].ToString() : null,
                                estado = reader["estado"] != DBNull.Value ? Convert.ToBoolean(reader["estado"]) : false,
                                rol = reader["rol"] != DBNull.Value ?
                                    Enum.TryParse(reader["rol"].ToString(), out RolUsuario rolParsed) ? rolParsed : RolUsuario.camarero
                                    : RolUsuario.camarero,
                                sueldo = reader["sueldo"] != DBNull.Value ? Convert.ToDecimal(reader["sueldo"]) : 0
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener usuarios: " + ex.Message);
            }

            return usuarios;
        }

        private void CargarUsuariosEnGrid(List<Usuario> usuarios)
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ID", DataPropertyName = "Id", Width = 50 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Usuario", DataPropertyName = "NombreUsuario", Width = 120 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Contraseña", DataPropertyName = "Password", Width = 120 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Nombre", DataPropertyName = "nombre", Width = 100 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Apellido", DataPropertyName = "apellido", Width = 100 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Documento", DataPropertyName = "documento", Width = 100 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Contacto", DataPropertyName = "contacto", Width = 120 });
            dgvUsuarios.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Activo", DataPropertyName = "estado", Width = 60 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Rol", DataPropertyName = "rol", Width = 100 });
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Sueldo", DataPropertyName = "sueldo", Width = 80 });

            dgvUsuarios.DataSource = usuarios;
        }

        private void VerificarYAgregarColumnas()
        {
            using (DatabaseConnection db = new DatabaseConnection()) // Uso de 'using' para asegurar cierre de conexión
            {
                db.OpenConnection();
                SQLiteConnection conn = db.GetConnection();

                // Diccionario: nombre de columna → tipo
                Dictionary<string, string> columnas = new Dictionary<string, string>
                {
                    { "nombre", "TEXT" },
                    { "apellido", "TEXT" },
                    { "documento", "TEXT" },
                    { "contacto", "TEXT" },
                    { "estado", "INTEGER" },
                    { "rol", "TEXT" },
                    { "sueldo", "REAL" }
                };

                foreach (var columna in columnas)
                {
                    try
                    {
                        // Intentar acceder a la columna
                        string testQuery = $"SELECT {columna.Key} FROM usuarios LIMIT 1";
                        using (var testCmd = new SQLiteCommand(testQuery, conn))
                        {
                            testCmd.ExecuteScalar();
                        }
                    }
                    catch
                    {
                        // Si lanza error, la columna no existe → se agrega
                        string alterQuery = $"ALTER TABLE usuarios ADD COLUMN {columna.Key} {columna.Value}";
                        using (var alterCmd = new SQLiteCommand(alterQuery, conn))
                        {
                            alterCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void EstilizarDataGridView()
        {
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsuarios.BackgroundColor = Color.FromArgb(240, 240, 240);
            dgvUsuarios.EnableHeadersVisualStyles = false;

            // Encabezados
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold); // Corregido

            // Celdas
            dgvUsuarios.DefaultCellStyle.BackColor = Color.White;
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
            dgvUsuarios.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10); // Corregido
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;

            // Interlineado de filas
            dgvUsuarios.RowTemplate.Height = 35;

            // Alternar colores de fila
            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
        }

        // --- SECCIÓN: MÉTODOS Y EVENTOS PARA COMPROBANTES ---

        /// <summary>
        /// Método de ejemplo para abrir FormPedidoMesa.
        /// Es crucial que en tu código real, donde sea que abras FormPedidoMesa,
        /// suscribas el evento PedidoCerrado.
        /// </summary>
        private void AbrirFormularioPedido(int numeroMesa, int idPedido)
        {
            FormPedidoMesa formPedidoMesa;
            if (idPedido == 0) // Si es un nuevo pedido
            {
                formPedidoMesa = new FormPedidoMesa(numeroMesa);
            }
            else // Si es un pedido existente
            {
                formPedidoMesa = new FormPedidoMesa(numeroMesa, idPedido);
            }

            formPedidoMesa.PedidoCerrado += FormPedidoMesa_PedidoCerrado;
            formPedidoMesa.ShowDialog();
        }

        /// <summary>
        /// Este método se ejecuta cuando FormPedidoMesa notifica que un pedido ha sido cerrado.
        /// Ahora simplemente llama al método de CargarComprobantes del manager.
        /// </summary>
        private void FormPedidoMesa_PedidoCerrado(object sender, EventArgs e)
        {
            if (_generarComprobante != null)
            {
                _generarComprobante.CargarComprobantes();
            }
        }

        /// <summary>
        /// Maneja el evento de cambio de pestaña en el TabControl principal.
        /// Recarga los comprobantes si se selecciona la pestaña de comprobantes.
        /// </summary>
        private void TabControlAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Solo recarga los comprobantes si la pestaña seleccionada es la de comprobantes
            if (tabAdmin.SelectedTab.Name == "tabComprobantes" && _generarComprobante != null)
            {
                _generarComprobante.CargarComprobantes();
            }
        }

        /// <summary>
        /// Evento Click para el nuevo botón "Generar PDF"
        /// </summary>
        private void btnGenerarPdf_Click(object sender, EventArgs e)
        {
            // Asegúrate de que haya un comprobante seleccionado en el dgvComprobantes
            DataGridView dgvComprobantes = tabComprobantes.Controls["dgvComprobantes"] as DataGridView;
            RichTextBox rtbDetallesComprobante = tabComprobantes.Controls["rtbDetallesComprobante"] as RichTextBox;

            if (dgvComprobantes != null && dgvComprobantes.SelectedRows.Count > 0)
            {
                // 1. Obtener el ID del comprobante seleccionado
                // Asegúrate de que la columna con el ID de comprobante en tu dgvComprobantes se llame "IdComprobante" o el nombre de tu DataPropertyName
                int idComprobante = Convert.ToInt32(dgvComprobantes.SelectedRows[0].Cells["IdComprobante"].Value);

                // 2. Obtener los detalles del comprobante que se muestran en el RichTextBox
                string detallesTexto = rtbDetallesComprobante.Text;

                // 3. Obtener los productos asociados a este comprobante
                DataTable productosComprobante = ObtenerDetallesProductosDeComprobante(idComprobante);

                // 4. Obtener el total del comprobante
                string totalTexto = "Total: $" + ObtenerTotalDeComprobante(idComprobante).ToString("N2");

                // 5. Abrir un SaveFileDialog para que el usuario elija dónde guardar el PDF
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    sfd.FileName = $"Factura_Comprobante_{idComprobante}.pdf"; // Nombre de archivo sugerido

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        // 6. Llamar a la función para generar el PDF
                        PdfGenerator.GenerarFacturaPdf(sfd.FileName, detallesTexto, productosComprobante, totalTexto);
                        MessageBox.Show("Factura generada y guardada en: " + sfd.FileName, "PDF Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un comprobante para generar la factura.", "Sin Comprobante Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Obtiene los detalles de los productos para un comprobante específico desde la base de datos.
        /// </summary>
        /// <param name="idComprobante">El ID del comprobante.</param>
        /// <returns>Un DataTable con los productos del comprobante.</returns>
        private DataTable ObtenerDetallesProductosDeComprobante(int idComprobante)
        {
            DataTable dtProductos = new DataTable();
            try
            {
                using (DatabaseConnection db = new DatabaseConnection())
                {
                    db.OpenConnection();

                    // Esta query ahora usa tus nombres de tabla y columna específicos.
                    string query = @"
                SELECT
                    P.nombre AS Producto,      -- Usamos 'nombreproducto' de tu tabla 'productos'
                    CP.cantidad AS Cantidad,           -- Usamos 'cantidad' de 'comprobantes_productos'
                    CP.precio_unitario AS PrecioUnitario, -- Usamos 'precio_unitario' de 'comprobantes_productos'
                    (CP.cantidad * CP.precio_unitario) AS Subtotal
                FROM comprobantes_productos CP         -- Alias 'CP' para tu tabla 'comprobantes_productos'
                JOIN productos P ON CP.producto_id = P.id -- Unimos con 'productos' usando 'producto_id' y 'id'
                WHERE CP.comprobante_id = @idComprobante";

                    SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
                    command.Parameters.AddWithValue("@idComprobante", idComprobante);

                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    da.Fill(dtProductos);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los detalles del comprobante para el PDF: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dtProductos;
        }

        /// <summary>
        /// Obtiene el total de un comprobante específico desde la base de datos.
        /// </summary>
        /// <param name="idComprobante">El ID del comprobante.</param>
        /// <returns>El monto total del comprobante.</returns>
        private decimal ObtenerTotalDeComprobante(int idComprobante)
        {
            decimal total = 0;
            try
            {
                using (DatabaseConnection db = new DatabaseConnection())
                {
                    db.OpenConnection();
                    // ¡ATENCIÓN: CUIDADO CON LAS MAYÚSCULAS Y MINÚSCULAS, Y LOS GUIONES!
                    // La tabla es 'comprobantes' (minúsculas)
                    // La columna del total es 'total_pedido' (minúsculas, guion bajo)
                    // La columna del ID es 'id' (minúsculas)
                    string query = "SELECT total_pedido FROM comprobantes WHERE id = @idComprobante";
                    using (SQLiteCommand command = new SQLiteCommand(query, db.GetConnection()))
                    {
                        command.Parameters.AddWithValue("@idComprobante", idComprobante);
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            total = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el total del comprobante para el PDF: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return total;
        }

        // --- FIN SECCIÓN: MÉTODOS Y EVENTOS PARA COMPROBANTES ---


        // Métodos de navegación (no modificados)
        private void lblMesas_Click(object sender, EventArgs e)
        {
            FormMain main = (FormMain)this.MdiParent;
            if (main != null)
            {
                main.AbrirFormulario(new FormMesas());
            }
            else
            {
                MessageBox.Show("Error: No se pudo obtener la referencia de FormMain.");
            }
        }

        private void lblStock_Click(object sender, EventArgs e)
        {
            FormMain main = (FormMain)this.MdiParent;
            if (main != null)
            {
                main.AbrirFormulario(new FormStock());
            }
            else
            {
                MessageBox.Show("Error: No se pudo obtener la referencia de FormMain.");
            }
        }

        private void lblCocina_Click(object sender, EventArgs e)
        {
            FormMain main = (FormMain)this.MdiParent;
            if (main != null)
            {
                main.AbrirFormulario(new FormCocina());
            }
            else
            {
                MessageBox.Show("Error: No se pudo obtener la referencia de FormMain.");
            }
        }

        private void lblAdmin_Click(object sender, EventArgs e)
        {
            // Podés dejarlo vacío por ahora o usarlo para refrescar usuarios
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Evento no implementado.
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            // Evento no implementado.
        }

        private void tabUsers_Click(object sender, EventArgs e)
        {
            // Evento no implementado.
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string user = txtAddUser.Text.Trim();
                string pass = txtAddPass.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                string apellido = txtApellido.Text.Trim();
                string documento = txtDocumento.Text.Trim();
                string contacto = txtContacto.Text.Trim();
                bool estado = cbEstado.Checked;
                string rol = cmbRol.SelectedItem?.ToString(); // Asegurate de que esté bien enlazado
                decimal sueldo = 0;
                decimal.TryParse(txtSueldo.Text.Trim(), out sueldo);

                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
                {
                    using (DatabaseConnection db = new DatabaseConnection()) // Uso de 'using'
                    {
                        db.OpenConnection();

                        string query = @"INSERT INTO USUARIOS
                                         (user, pass, nombre, apellido, documento, contacto, estado, rol, sueldo)
                                         VALUES
                                         (@user, @pass, @nombre, @apellido, @documento, @contacto, @estado, @rol, @sueldo)";

                        SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@pass", pass);
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@apellido", apellido);
                        command.Parameters.AddWithValue("@documento", documento);
                        command.Parameters.AddWithValue("@contacto", contacto);
                        command.Parameters.AddWithValue("@estado", estado ? 1 : 0);
                        command.Parameters.AddWithValue("@rol", rol);
                        command.Parameters.AddWithValue("@sueldo", sueldo);

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Registro agregado correctamente.");
                            FormAdmin_Load(null, null); // Recargar datos
                        }
                        else
                        {
                            MessageBox.Show("Error al agregar el registro.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Los campos Usuario y Contraseña son obligatorios.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count > 0)
                {
                    DataGridViewRow filaSeleccionada = dgvUsuarios.SelectedRows[0];
                    int id = Convert.ToInt32(filaSeleccionada.Cells[0].Value);

                    string user = txtAddUser.Text.Trim();
                    string pass = txtAddPass.Text.Trim();
                    string nombre = txtNombre.Text.Trim();
                    string apellido = txtApellido.Text.Trim();
                    string documento = txtDocumento.Text.Trim();
                    string contacto = txtContacto.Text.Trim();
                    bool estado = cbEstado.Checked;
                    string rol = cmbRol.SelectedItem?.ToString();
                    decimal sueldo = 0;
                    decimal.TryParse(txtSueldo.Text.Trim(), out sueldo);

                    if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
                    {
                        using (DatabaseConnection db = new DatabaseConnection()) // Uso de 'using'
                        {
                            db.OpenConnection();

                            string query = @"UPDATE USUARIOS SET
                                             user=@user, pass=@pass, nombre=@nombre, apellido=@apellido,
                                             documento=@documento, contacto=@contacto, estado=@estado,
                                             rol=@rol, sueldo=@sueldo
                                             WHERE id=@id";

                            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
                            command.Parameters.AddWithValue("@user", user);
                            command.Parameters.AddWithValue("@pass", pass);
                            command.Parameters.AddWithValue("@nombre", nombre);
                            command.Parameters.AddWithValue("@apellido", apellido);
                            command.Parameters.AddWithValue("@documento", documento);
                            command.Parameters.AddWithValue("@contacto", contacto);
                            command.Parameters.AddWithValue("@estado", estado ? 1 : 0);
                            command.Parameters.AddWithValue("@rol", rol);
                            command.Parameters.AddWithValue("@sueldo", sueldo);
                            command.Parameters.AddWithValue("@id", id);

                            int filasAfectadas = command.ExecuteNonQuery();

                            if (filasAfectadas > 0)
                            {
                                MessageBox.Show("Registro actualizado correctamente.");
                                FormAdmin_Load(null, null); // Recargar datos
                            }
                            else
                            {
                                MessageBox.Show("Error al actualizar el registro.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Los campos Usuario y Contraseña son obligatorios.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dgvUsuarios.Rows[e.RowIndex];

                // Obtener el ID usando el índice de la columna en lugar del nombre
                // Asegúrate de que el orden de las columnas en el DataGridView sea consistente
                txtAddUser.Text = filaSeleccionada.Cells["NombreUsuario"].Value?.ToString();
                txtAddPass.Text = filaSeleccionada.Cells["Password"].Value?.ToString();
                txtNombre.Text = filaSeleccionada.Cells["nombre"].Value?.ToString();
                txtApellido.Text = filaSeleccionada.Cells["apellido"].Value?.ToString();
                txtDocumento.Text = filaSeleccionada.Cells["documento"].Value?.ToString();
                txtContacto.Text = filaSeleccionada.Cells["contacto"].Value?.ToString();
                cbEstado.Checked = Convert.ToBoolean(filaSeleccionada.Cells["estado"].Value);
                // Asegúrate de que cmbRol esté enlazado correctamente al enum RolUsuario
                if (filaSeleccionada.Cells["rol"].Value != null && Enum.TryParse(filaSeleccionada.Cells["rol"].Value.ToString(), out RolUsuario rolParsed))
                {
                    cmbRol.SelectedItem = rolParsed;
                }
                txtSueldo.Text = filaSeleccionada.Cells["sueldo"].Value?.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selecciona un usuario antes de eliminarlo.");
                    return;
                }

                // Obtener el ID de la fila seleccionada
                DataGridViewRow filaSeleccionada = dgvUsuarios.SelectedRows[0];
                int id = Convert.ToInt32(filaSeleccionada.Cells["Id"].Value); // Usar DataPropertyName "Id"

                // Confirmación antes de eliminar
                DialogResult confirmacion = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacion == DialogResult.No) return;

                using (DatabaseConnection db = new DatabaseConnection()) // Uso de 'using'
                {
                    db.OpenConnection();

                    string query = "DELETE FROM USUARIOS WHERE id=@id";
                    SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
                    command.Parameters.AddWithValue("@id", id);

                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Usuario eliminado correctamente.");
                        FormAdmin_Load(null, null); // Recargar el DataGridView
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario con ese ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar usuario: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Evento no implementado.
        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            SesionHelper.CerrarSesion(this.MdiParent);
        }

        private void tabGastos_Click(object sender, EventArgs e)
        {
            // Evento no implementado.
        }

        private void btnGastosAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string descripcion = txtDesc.Text;
                decimal monto = Convert.ToDecimal(txtMonto.Text);
                string categoria = txtCategoria.Text;
                string fecha = dtpFecha.Value.ToString("yyyy-MM-dd"); // Formato seguro para SQLite

                AgregarGasto.Ejecutar(descripcion, monto, fecha, categoria);

                CargarGastos();
                CalcularTotal();

                MessageBox.Show("✅ Gasto agregado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnGastosModificar_Click(object sender, EventArgs e)
        {
            if (dgvGastos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un gasto para modificar.");
                return;
            }

            try
            {
                int idSeleccionado = Convert.ToInt32(dgvGastos.CurrentRow.Cells["Id"].Value);
                string descripcion = txtDesc.Text;
                decimal monto = Convert.ToDecimal(txtMonto.Text);
                string categoria = txtCategoria.Text;
                DateTime fecha = dtpFecha.Value;

                ModificarGasto.Ejecutar(idSeleccionado, descripcion, monto, fecha, categoria);

                CargarGastos();
                CalcularTotal();

                MessageBox.Show("✏️ Gasto modificado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar gasto: " + ex.Message);
            }
        }

        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvGastos.Rows[e.RowIndex];

                txtDesc.Text = fila.Cells["Descripcion"].Value?.ToString();
                txtMonto.Text = fila.Cells["Monto"].Value?.ToString();
                txtCategoria.Text = fila.Cells["Categoria"].Value?.ToString();

                // Convertir la fecha desde texto a DateTime
                if (DateTime.TryParse(fila.Cells["Fecha"].Value?.ToString(), out DateTime fecha))
                {
                    dtpFecha.Value = fecha;
                }
            }
        }

        private void dgvGastos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evento no implementado.
        }

        private void btnGastosDel_Click(object sender, EventArgs e)
        {
            if (dgvGastos.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná un gasto para eliminar.");
                return;
            }

            DialogResult confirmacion = MessageBox.Show("¿Estás seguro de que querés eliminar este gasto?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    int idSeleccionado = Convert.ToInt32(dgvGastos.CurrentRow.Cells["Id"].Value);
                    EliminarGasto.Ejecutar(idSeleccionado);

                    CargarGastos();
                    CalcularTotal();

                    MessageBox.Show("🗑️ Gasto eliminado correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar gasto: " + ex.Message);
                }
            }
        }

        private void rtbDetallesComprobante_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerarPdf_Click_1(object sender, EventArgs e)
        {
            DataGridView dgvComprobantes = tabComprobantes.Controls["dgvComprobantes"] as DataGridView;
            RichTextBox rtbDetallesComprobante = tabComprobantes.Controls["rtbDetallesComprobante"] as RichTextBox;

            if (dgvComprobantes != null && dgvComprobantes.SelectedRows.Count > 0)
            {
                // ✅ Usamos el ID real de la tabla comprobantes (columna 'id')
                int idComprobante = Convert.ToInt32(dgvComprobantes.SelectedRows[0].Cells["id"].Value);


                string detallesTexto = rtbDetallesComprobante.Text;

                DataTable productosComprobante = ObtenerDetallesProductosDeComprobante(idComprobante);

                // ✅ Obtenemos el total usando el ID correcto
                decimal totalDecimal = ObtenerTotalDeComprobante(idComprobante);


                string totalTexto = "Total: $" + totalDecimal.ToString("N2");

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Archivos PDF (*.pdf)|*.pdf";
                    sfd.FileName = $"Factura_Comprobante_{idComprobante}.pdf";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        PdfGenerator.GenerarFacturaPdf(sfd.FileName, detallesTexto, productosComprobante, totalTexto);
                        MessageBox.Show("Factura generada y guardada en: " + sfd.FileName, "PDF Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un comprobante para generar la factura.", "Sin Comprobante Seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}