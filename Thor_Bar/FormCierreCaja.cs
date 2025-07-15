using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Thor_Bar
{


    public partial class FormCierreCaja : Form
    {

        public decimal MontoIngresado { get; private set; }

        private void btn_agregarCierreCaja_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out decimal monto))
            {
                MontoIngresado = monto;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, ingresá un monto válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public FormCierreCaja()
        {
            InitializeComponent();
        }
    }
}

