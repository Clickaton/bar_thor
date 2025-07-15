namespace Thor_Bar
{
    partial class FormCierreCaja
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btn_agregarCierreCaja = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbl_signMoney = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(82, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(389, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingrese el cierre de caja:";
            // 
            // btn_agregarCierreCaja
            // 
            this.btn_agregarCierreCaja.Location = new System.Drawing.Point(208, 207);
            this.btn_agregarCierreCaja.Name = "btn_agregarCierreCaja";
            this.btn_agregarCierreCaja.Size = new System.Drawing.Size(108, 35);
            this.btn_agregarCierreCaja.TabIndex = 1;
            this.btn_agregarCierreCaja.Text = "Agregar";
            this.btn_agregarCierreCaja.UseVisualStyleBackColor = true;
            this.btn_agregarCierreCaja.Click += new System.EventHandler(this.btn_agregarCierreCaja_Click); 
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(172, 128);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(186, 22);
            this.textBox1.TabIndex = 2;
            // 
            // lbl_signMoney
            // 
            this.lbl_signMoney.AutoSize = true;
            this.lbl_signMoney.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_signMoney.Location = new System.Drawing.Point(146, 125);
            this.lbl_signMoney.Name = "lbl_signMoney";
            this.lbl_signMoney.Size = new System.Drawing.Size(23, 25);
            this.lbl_signMoney.TabIndex = 3;
            this.lbl_signMoney.Text = "$";
            // 
            // FormCierreCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 269);
            this.Controls.Add(this.lbl_signMoney);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_agregarCierreCaja);
            this.Controls.Add(this.label1);
            this.Name = "FormCierreCaja";
            this.Text = "FormCierreCaja";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_agregarCierreCaja;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbl_signMoney;
    }
}