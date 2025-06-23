using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Thor_Bar
{
    public partial class FormMesas : Form
    {
        public FormMesas()
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

        private void FormMesas_Load(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is PictureBox pb && pb.Name.StartsWith("pbMesa"))
                {
                    int numeroMesa = int.Parse(pb.Name.Replace("pbMesa", ""));
                    pb.Click += Mesa_Click;
                    pb.Tag = numeroMesa;

                    ActualizarVisualMesa(pb, numeroMesa);
                }
            }
        }

        private void Mesa_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb == null) return;

            int numeroMesa = pb.Tag is int n ? n : 0;

            FormPedidoMesa formPedido = new FormPedidoMesa(numeroMesa);
            formPedido.StartPosition = FormStartPosition.CenterScreen;
            formPedido.Size = new Size(400, 500);
            formPedido.ShowDialog();

            // Refrescar visual después del pedido
            ActualizarVisualMesa(pb, numeroMesa);
        }

        private void ActualizarVisualMesa(PictureBox pb, int numeroMesa)
        {
            string estado = ObtenerEstadoMesa(numeroMesa);

            // Colores según estado
            Color colorFondo = estado == "ocupada" ? Color.Red : Color.Green;

            // Actualizar PictureBox
            pb.BackColor = colorFondo;

            // Buscar y actualizar Label asociado
            string labelName = "lblMesa" + numeroMesa;
            Control[] encontrados = this.Controls.Find(labelName, true);
            if (encontrados.Length > 0 && encontrados[0] is Label lbl)
            {
                lbl.BackColor = pb.BackColor;
                lbl.ForeColor = Color.Black;
                // Opcional: mostrar el estado en texto
                // lbl.Text = $"Mesa {numeroMesa} ({estado})";
            }
        }

        private string ObtenerEstadoMesa(int numero)
        {
            try
            {
                using (var conn = new SQLiteConnection("Data Source=thor_bar.sqlite"))
                {
                    conn.Open();
                    string query = "SELECT estado FROM mesas WHERE numero = @num";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@num", numero);
                        object result = cmd.ExecuteScalar();
                        return result?.ToString() ?? "libre";
                    }
                }
            }
            catch
            {
                return "libre";
            }
        }

        private void lblCocina_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is FormMain main
                && (SesionGlobal.UsuarioActual.rol == RolUsuario.Administrador
                || SesionGlobal.UsuarioActual.rol == RolUsuario.Cocinero))
            {
                main.AbrirFormulario(new FormCocina());
            }
            else {
                MessageBox.Show("Usted no tiene los permisos para poder acceder a esta sección.");
            }
        }

        private void lblAdmin_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is FormMain main && SesionGlobal.UsuarioActual.rol == RolUsuario.Administrador)
            {
                main.AbrirFormulario(new FormAdmin());
            }
            else
            {
                MessageBox.Show("Usted no tiene los permisos para poder acceder a esta sección.");
            }
        }

        private void lblStock_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is FormMain main)
                main.AbrirFormulario(new FormStock());
        }

        // Métodos de evento vacíos (opcionales)
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void pictureBox4_Click(object sender, EventArgs e) { }
        private void lblMesas_Click(object sender, EventArgs e) { }
        private void lblMesa1_Click(object sender, EventArgs e) { }
    }
}
