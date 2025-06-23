using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Thor_Bar
{
    public partial class FormStock : Form
    {
        public FormStock()
        {
            InitializeComponent();
            this.Load += FormStock_Load;
        }

        // Modelo de producto
        public class Producto
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int Stock { get; set; }
            public string Categoria { get; set; }
            public Image Imagen { get; set; }
        }

        private void FormStock_Load(object sender, EventArgs e)
        {
            
            List<Producto> productos = ObtenerProductos();
           

            List<Producto> bebidas = new List<Producto>();
            List<Producto> entradas = new List<Producto>();
            List<Producto> hamburguesas = new List<Producto>();
            List<Producto> platos = new List<Producto>();
            List<Producto> postres = new List<Producto>();

            foreach (Producto p in productos)
            {
                if (p.Categoria == "Bebidas") bebidas.Add(p);
                else if (p.Categoria == "Entradas") entradas.Add(p);
                else if (p.Categoria == "Hamburguesas") hamburguesas.Add(p);
                else if (p.Categoria == "Platos") platos.Add(p);
                else if (p.Categoria == "Postres") postres.Add(p);
            }

            MostrarTarjetas(bebidas, flpBebidas);
            MostrarTarjetas(entradas, flpEntradas);
            MostrarTarjetas(hamburguesas, flpHamburguesas);
            MostrarTarjetas(platos, flpPlatos);
            MostrarTarjetas(postres, flpPostres);
        }

        private List<Producto> ObtenerProductos()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                DatabaseConnection db = new DatabaseConnection();
                db.OpenConnection();

                string query = "SELECT * FROM productos";
                SQLiteCommand cmd = new SQLiteCommand(query, db.GetConnection());
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto prod = new Producto();
                    prod.Id = Convert.ToInt32(reader["id"]);
                    prod.Nombre = reader["nombre"].ToString();
                    prod.Descripcion = reader["descrip"].ToString();
                    prod.Stock = Convert.ToInt32(reader["stock"]);
                    prod.Categoria = reader["categoria"].ToString();
                    prod.Imagen = CargarImagenDesdeBytes((byte[])reader["imagen"]);

                    productos.Add(prod);
                }

                db.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener productos: " + ex.Message);
            }

            return productos;
        }

        private void lblAdmin_Click(object sender, EventArgs e)
        {
            FormMain main = (FormMain)this.MdiParent; // Accede al formulario principal
            if (main != null)
            {
                main.AbrirFormulario(new FormAdmin()); // Abre FormAdmin dentro del MDI
            }
            else
            {
                MessageBox.Show("Error: No se pudo obtener la referencia de FormMain.");
            }
        }
        private void lblCocina_Click(object sender, EventArgs e)
        {
            FormMain main = (FormMain)this.MdiParent; // Accede al formulario principal
            if (main != null)
            {
                main.AbrirFormulario(new FormCocina()); // Abre FormAdmin dentro del MDI
            }
            else
            {
                MessageBox.Show("Error: No se pudo obtener la referencia de FormMain.");
            }
        }
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

        private void tabBebidas_Click(object sender, EventArgs e)
        {
            // Si querés hacer algo cuando hagan clic en la pestaña Bebidas,
            // podés escribirlo acá. Si no, dejalo vacío.
        }
        private Image CargarImagenDesdeBytes(byte[] datos)
        {
            MemoryStream ms = new MemoryStream(datos);
            return Image.FromStream(ms);
        }

        private void MostrarTarjetas(List<Producto> productos, FlowLayoutPanel panel)
        {
            panel.Controls.Clear();

            foreach (Producto prod in productos)
            {
                Panel tarjeta = new Panel();
                tarjeta.Width = 160;
                tarjeta.Height = 160;
                tarjeta.BorderStyle = BorderStyle.FixedSingle;
                tarjeta.Margin = new Padding(8);

                PictureBox pb = new PictureBox();
                pb.Image = prod.Imagen;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Dock = DockStyle.Top;
                pb.Height = 100;

                Label lblNombre = new Label();
                lblNombre.Text = prod.Nombre;
                lblNombre.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblNombre.Dock = DockStyle.Top;
                lblNombre.TextAlign = ContentAlignment.MiddleCenter;

                Label lblStock = new Label();
                lblStock.Text = "Stock: " + prod.Stock;
                lblStock.Font = new Font("Segoe UI", 9);
                lblStock.Dock = DockStyle.Top;
                lblStock.TextAlign = ContentAlignment.MiddleCenter;
                lblStock.ForeColor = prod.Stock > 0 ? Color.Green : Color.Red;

                tarjeta.Controls.Add(pb);         // Imagen abajo
                tarjeta.Controls.Add(lblNombre);  // Nombre encima
                tarjeta.Controls.Add(lblStock);   // Stock más arriba

                panel.Controls.Add(tarjeta);
            }
        }

        private void flpBebidas_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}