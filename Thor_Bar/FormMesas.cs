using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Thor_Bar.Helpers;

namespace Thor_Bar
{
    public partial class FormMesas : Form
    {
        private bool arrastrando = false;
        private bool fueArrastrado = false;
        private Point offset;
        private PictureBox mesaSeleccionada = null;
        private const string RutaArchivoMesas = "mesas_posiciones.txt";

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
                    pb.Tag = numeroMesa;

                    pb.Click += Mesa_Click;
                    pb.MouseDown += Mesa_MouseDown;
                    pb.MouseMove += Mesa_MouseMove;
                    pb.MouseUp += Mesa_MouseUp;
                    pb.Paint += Mesa_Paint;

                    ActualizarVisualMesa(pb, numeroMesa);
                }
            }

            CargarPosicionesMesas();
        }

        private void Mesa_Click(object sender, EventArgs e)
        {
            if (fueArrastrado)
            {
                fueArrastrado = false;
                return;
            }

            PictureBox pb = sender as PictureBox;
            if (pb == null) return;

            int numeroMesa = pb.Tag is int n ? n : 0;

            FormPedidoMesa formPedido = new FormPedidoMesa(numeroMesa);
            formPedido.StartPosition = FormStartPosition.CenterScreen;
            formPedido.Size = new Size(400, 500);
            formPedido.ShowDialog();

            ActualizarVisualMesa(pb, numeroMesa);
        }

        private void ActualizarVisualMesa(PictureBox pb, int numeroMesa)
        {
            string estado = ObtenerEstadoMesa(numeroMesa);
            Color colorFondo = estado == "ocupada" ? Color.Red : Color.Green;

            pb.BackColor = colorFondo;
            pb.Invalidate();
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

        private void Mesa_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastrando = true;
                fueArrastrado = false;
                mesaSeleccionada = sender as PictureBox;
                offset = e.Location;
            }
        }

        private void Mesa_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando && mesaSeleccionada != null)
            {
                Point nuevaPos = mesaSeleccionada.Location;
                nuevaPos.X += e.X - offset.X;
                nuevaPos.Y += e.Y - offset.Y;
                mesaSeleccionada.Location = nuevaPos;

                fueArrastrado = true;
            }
        }

        private void Mesa_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                arrastrando = false;

                if (mesaSeleccionada != null)
                {
                    GuardarPosicionesMesas();
                    mesaSeleccionada = null;
                }
            }
        }

        private void Mesa_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            if (pb == null || pb.Tag == null) return;

            int numeroMesa = (int)pb.Tag;
            string texto = $"Mesa {numeroMesa}";

            using (Font fuente = new Font("Arial", 16, FontStyle.Bold))
            using (Brush pincel = new SolidBrush(Color.Black))
            using (StringFormat formato = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                e.Graphics.DrawString(texto, fuente, pincel, pb.ClientRectangle, formato);
            }
        }

        private void GuardarPosicionesMesas()
        {
            List<string> lineas = new List<string>();

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is PictureBox pb && pb.Name.StartsWith("pbMesa") && pb.Tag is int numero)
                {
                    string linea = $"{numero},{pb.Location.X},{pb.Location.Y}";
                    lineas.Add(linea);
                }
            }

            File.WriteAllLines(RutaArchivoMesas, lineas);
        }

        private void CargarPosicionesMesas()
        {
            if (!File.Exists(RutaArchivoMesas)) return;

            try
            {
                string[] lineas = File.ReadAllLines(RutaArchivoMesas);

                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split(',');
                    if (partes.Length == 3 &&
                        int.TryParse(partes[0], out int numero) &&
                        int.TryParse(partes[1], out int x) &&
                        int.TryParse(partes[2], out int y))
                    {
                        string nombre = $"pbMesa{numero}";
                        Control[] encontrados = this.Controls.Find(nombre, true);
                        if (encontrados.Length > 0 && encontrados[0] is PictureBox pb)
                        {
                            pb.Location = new Point(x, y);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar posiciones de mesas: " + ex.Message);
            }
        }

        // Métodos del menú
        private void lblCocina_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is FormMain main &&
                (SesionGlobal.UsuarioActual.rol == RolUsuario.Administrador ||
                 SesionGlobal.UsuarioActual.rol == RolUsuario.Cocinero))
            {
                main.AbrirFormulario(new FormCocina());
            }
            else
            {
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

        private void lblExit_Click(object sender, EventArgs e)
        {
            SesionHelper.CerrarSesion(this.MdiParent);
        }

        // Métodos vacíos opcionales
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void pictureBox3_Click(object sender, EventArgs e) { }
        private void pictureBox4_Click(object sender, EventArgs e) { }
        private void lblMesas_Click(object sender, EventArgs e) { }
        private void lblMesa1_Click(object sender, EventArgs e) { }
        private void pbMesa1_Click(object sender, EventArgs e) { }
    }
}
