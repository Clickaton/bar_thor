using System;
using System.Windows.Forms;

namespace Thor_Bar
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true; // Activa el modo MDI
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Abrir FormLogin dentro del MDI al iniciar
            FormLogin login = new FormLogin(this);
            login.MdiParent = this;
            login.Show(); // Modal para forzar el login antes de abrir otras ventanas
        }

        // Método para abrir formularios dentro del MDI
        public void AbrirFormulario(Form form)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Close(); // Cierra el formulario anterior
            }

            form.MdiParent = this;
            form.WindowState = FormWindowState.Maximized;
            form.Show();
        }
    }
}
