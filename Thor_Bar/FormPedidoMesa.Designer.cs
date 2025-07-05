using System;

namespace Thor_Bar
{
    partial class FormPedidoMesa
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormPedidoMesa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 381);
            this.Name = "FormPedidoMesa";
            this.Text = "FormPedidoMesa";
            this.Load += new System.EventHandler(this.FormPedidoMesa_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductos;

        // ✅ Este método tiene que ir DENTRO de la clase
        private void FormPedidoMesa_Load(object sender, EventArgs e)
        {
            // Puedes dejarlo vacío si no lo usás.
        }
    }
}
