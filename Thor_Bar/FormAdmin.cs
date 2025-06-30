using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;
using Thor_Bar.Helpers;
using static Mysqlx.Crud.Order.Types;

namespace Thor_Bar
{
    public partial class FormAdmin : Form
    {
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
        }

        private void lblNombreUsuario_Click(object sender, EventArgs e)
        {
           
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            //Cargar enum RolUsuario en el ComboBox
            cmbRol.DataSource = Enum.GetValues(typeof(RolUsuario));
            VerificarYAgregarColumnas();
            List<Usuario> usuarios = ObtenerUsuarios();
            CargarUsuariosEnGrid(usuarios);
            EstilizarDataGridView();

            DatabaseConnection db = new DatabaseConnection();
            db.OpenConnection();

            string query = "SELECT * FROM usuarios";
            SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
            dgvUsuarios.CellClick += dgvUsuarios_CellClick;

        }

        private List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                DatabaseConnection db = new DatabaseConnection();
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

                db.CloseConnection();
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
            DatabaseConnection db = new DatabaseConnection();
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

            db.CloseConnection();
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
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Celdas
            dgvUsuarios.DefaultCellStyle.BackColor = Color.White;
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.Black;
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;

            // Interlineado de filas
            dgvUsuarios.RowTemplate.Height = 35;

            // Alternar colores de fila
            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230);
        }


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

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void tabUsers_Click(object sender, EventArgs e)
        {

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
                    DatabaseConnection db = new DatabaseConnection();
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
                        FormAdmin_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar el registro.");
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
                        DatabaseConnection db = new DatabaseConnection();
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
                            FormAdmin_Load(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Error al actualizar el registro.");
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
                int id = Convert.ToInt32(filaSeleccionada.Cells[0].Value);

                // Asignar los demás valores a los TextBox
                txtAddUser.Text = filaSeleccionada.Cells[1].Value.ToString();
                txtAddPass.Text = filaSeleccionada.Cells[2].Value.ToString();
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
                //int id = Convert.ToInt32(filaSeleccionada.Cells["id"].Value);
                int id = Convert.ToInt32(filaSeleccionada.Cells[0].Value);


                // Confirmación antes de eliminar
                DialogResult confirmacion = MessageBox.Show("¿Estás seguro de que deseas eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacion == DialogResult.No) return;

                DatabaseConnection db = new DatabaseConnection();
                db.OpenConnection();

                string query = "DELETE FROM USUARIOS WHERE id=@id";
                SQLiteCommand command = new SQLiteCommand(query, db.GetConnection());
                command.Parameters.AddWithValue("@id", id);

                int filasAfectadas = command.ExecuteNonQuery();

                db.CloseConnection();

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
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar usuario: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblExit_Click(object sender, EventArgs e)
        {
            SesionHelper.CerrarSesion(this.MdiParent);
        }
    }


}
