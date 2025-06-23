using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Thor_Bar
{
    public partial class FormLogin : Form
    {
        private FormMain main; // Referencia a FormMain

        public FormLogin(FormMain mainForm)
        {
            InitializeComponent();
            this.main = mainForm;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DatabaseConnection db = new DatabaseConnection();
            db.OpenConnection();

            string query = "SELECT * FROM usuarios WHERE user = @user AND pass = @pass";
            using (SQLiteCommand command = new SQLiteCommand(query, db.GetConnection()))
            {
                command.Parameters.AddWithValue("@user", txtUser.Text.Trim());
                command.Parameters.AddWithValue("@pass", txtPass.Text.Trim());

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // ✅ Crear usuario desde la DB
                        Usuario usuario = new Usuario
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            NombreUsuario = reader["user"].ToString(),
                            Password = reader["pass"].ToString(),
                            nombre = reader["nombre"] != DBNull.Value ? reader["nombre"].ToString() : "",
                            apellido = reader["apellido"] != DBNull.Value ? reader["apellido"].ToString() : "",
                            documento = reader["documento"] != DBNull.Value ? reader["documento"].ToString() : "",
                            contacto = reader["contacto"] != DBNull.Value ? reader["contacto"].ToString() : "",
                            estado = reader["estado"] != DBNull.Value ? Convert.ToBoolean(reader["estado"]) : false,
                            rol = (RolUsuario)Enum.Parse(typeof(RolUsuario), reader["rol"].ToString(), ignoreCase: true),
                            sueldo = reader["sueldo"] != DBNull.Value ? Convert.ToDecimal(reader["sueldo"]) : 0
                        };

                        // ✅ Guardar en la sesión global
                        SesionGlobal.UsuarioActual = usuario;

                        // ✅ Cerrar el login y abrir FormMesas
                        this.Close();

                        if (main != null)
                            main.AbrirFormulario(new FormMesas());
                        else
                            MessageBox.Show("Error: No se pudo obtener la referencia de FormMain.");
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos.");
                    }
                }
            }

            db.CloseConnection();
        }
    }
}