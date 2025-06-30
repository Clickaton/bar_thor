using System.Windows.Forms;
using Thor_Bar;

namespace Thor_Bar.Helpers
{
    public static class SesionHelper
    {
        public static void CerrarSesion(Form mdiContainer)
        {
            var confirm = MessageBox.Show(
                "¿Estás seguro de que querés cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // Limpiar sesión global
                string nombreUsuario = SesionGlobal.UsuarioActual?.nombre ?? "Desconocido";
                SesionGlobal.UsuarioActual = null;

                // Cerrar formulario activo
                if (mdiContainer.ActiveMdiChild != null)
                    mdiContainer.ActiveMdiChild.Close();

                // Confirmar que el contenedor es FormMain antes de continuar
                FormMain mainForm = mdiContainer as FormMain;
                if (mainForm != null)
                {
                    // Abrir nuevamente el FormLogin
                    FormLogin login = new FormLogin(mainForm);
                    login.MdiParent = mdiContainer;
                    login.Show();
                }
                else
                {
                    MessageBox.Show("Error: no se pudo acceder al formulario principal para volver al login.");
                }
            }
        }
    }
}