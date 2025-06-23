namespace Thor_Bar
{
    partial class FormAdmin
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAdmin));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAdmin = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblMesas = new System.Windows.Forms.Label();
            this.lblCocina = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.txtAddUser = new System.Windows.Forms.TextBox();
            this.txtAddPass = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tabAdmin = new System.Windows.Forms.TabControl();
            this.tabUsers = new System.Windows.Forms.TabPage();
            this.cmbRol = new System.Windows.Forms.ComboBox();
            this.cbEstado = new System.Windows.Forms.CheckBox();
            this.txtSueldo = new System.Windows.Forms.TextBox();
            this.txtContacto = new System.Windows.Forms.TextBox();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblSueldo = new System.Windows.Forms.Label();
            this.lblContacto = new System.Windows.Forms.Label();
            this.lblRol = new System.Windows.Forms.Label();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.lblApellido = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.tabGastos = new System.Windows.Forms.TabPage();
            this.lblGastos = new System.Windows.Forms.Label();
            this.dgvGastos = new System.Windows.Forms.DataGridView();
            this.tabCaja = new System.Windows.Forms.TabPage();
            this.tabProveedores = new System.Windows.Forms.TabPage();
            this.tabComprobantes = new System.Windows.Forms.TabPage();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.tabAdmin.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.tabGastos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox2.Location = new System.Drawing.Point(-3, 65);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1696, 38);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DarkOrange;
            this.pictureBox1.Location = new System.Drawing.Point(-3, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 73);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblAdmin
            // 
            this.lblAdmin.AutoSize = true;
            this.lblAdmin.BackColor = System.Drawing.Color.LightGray;
            this.lblAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdmin.Location = new System.Drawing.Point(897, 118);
            this.lblAdmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdmin.Name = "lblAdmin";
            this.lblAdmin.Size = new System.Drawing.Size(185, 29);
            this.lblAdmin.TabIndex = 13;
            this.lblAdmin.Text = "Administracion";
            this.lblAdmin.Click += new System.EventHandler(this.lblAdmin_Click);
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.BackColor = System.Drawing.Color.LightGray;
            this.lblStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStock.Location = new System.Drawing.Point(808, 118);
            this.lblStock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(78, 29);
            this.lblStock.TabIndex = 12;
            this.lblStock.Text = "Stock";
            this.lblStock.Click += new System.EventHandler(this.lblStock_Click);
            // 
            // lblMesas
            // 
            this.lblMesas.AutoSize = true;
            this.lblMesas.BackColor = System.Drawing.Color.LightGray;
            this.lblMesas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMesas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMesas.Location = new System.Drawing.Point(700, 118);
            this.lblMesas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMesas.Name = "lblMesas";
            this.lblMesas.Size = new System.Drawing.Size(89, 29);
            this.lblMesas.TabIndex = 11;
            this.lblMesas.Text = "Mesas";
            this.lblMesas.Click += new System.EventHandler(this.lblMesas_Click);
            // 
            // lblCocina
            // 
            this.lblCocina.AutoSize = true;
            this.lblCocina.BackColor = System.Drawing.Color.LightGray;
            this.lblCocina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCocina.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCocina.Location = new System.Drawing.Point(592, 118);
            this.lblCocina.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCocina.Name = "lblCocina";
            this.lblCocina.Size = new System.Drawing.Size(94, 29);
            this.lblCocina.TabIndex = 10;
            this.lblCocina.Text = "Cocina";
            this.lblCocina.Click += new System.EventHandler(this.lblCocina_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.LightGray;
            this.pictureBox4.Location = new System.Drawing.Point(-3, 98);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(1696, 66);
            this.pictureBox4.TabIndex = 9;
            this.pictureBox4.TabStop = false;
            // 
            // dgvUsuarios
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Silver;
            this.dgvUsuarios.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Location = new System.Drawing.Point(170, 129);
            this.dgvUsuarios.Margin = new System.Windows.Forms.Padding(4);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.RowHeadersWidth = 51;
            this.dgvUsuarios.Size = new System.Drawing.Size(1298, 279);
            this.dgvUsuarios.TabIndex = 14;
            // 
            // lblAdminTitle
            // 
            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdminTitle.Location = new System.Drawing.Point(495, 54);
            this.lblAdminTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(655, 42);
            this.lblAdminTitle.TabIndex = 15;
            this.lblAdminTitle.Text = "Panel de administración de usuarios";
            this.lblAdminTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtAddUser
            // 
            this.txtAddUser.Location = new System.Drawing.Point(301, 467);
            this.txtAddUser.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddUser.Name = "txtAddUser";
            this.txtAddUser.Size = new System.Drawing.Size(187, 30);
            this.txtAddUser.TabIndex = 16;
            // 
            // txtAddPass
            // 
            this.txtAddPass.Location = new System.Drawing.Point(300, 545);
            this.txtAddPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddPass.Name = "txtAddPass";
            this.txtAddPass.Size = new System.Drawing.Size(188, 30);
            this.txtAddPass.TabIndex = 17;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(295, 428);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(86, 25);
            this.lblName.TabIndex = 18;
            this.lblName.Text = "Usuario";
            this.lblName.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.Location = new System.Drawing.Point(295, 506);
            this.lblPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(68, 25);
            this.lblPass.TabIndex = 19;
            this.lblPass.Text = "Clave";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(626, 629);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(128, 49);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "&Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(774, 629);
            this.btnModify.Margin = new System.Windows.Forms.Padding(4);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(129, 49);
            this.btnModify.TabIndex = 21;
            this.btnModify.Text = "&Modificar";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(924, 629);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(129, 49);
            this.btnDelete.TabIndex = 22;
            this.btnDelete.Text = "&Eliminar";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tabAdmin
            // 
            this.tabAdmin.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabAdmin.Controls.Add(this.tabUsers);
            this.tabAdmin.Controls.Add(this.tabGastos);
            this.tabAdmin.Controls.Add(this.tabCaja);
            this.tabAdmin.Controls.Add(this.tabProveedores);
            this.tabAdmin.Controls.Add(this.tabComprobantes);
            this.tabAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAdmin.ItemSize = new System.Drawing.Size(244, 21);
            this.tabAdmin.Location = new System.Drawing.Point(-5, 153);
            this.tabAdmin.Margin = new System.Windows.Forms.Padding(4);
            this.tabAdmin.Name = "tabAdmin";
            this.tabAdmin.SelectedIndex = 0;
            this.tabAdmin.Size = new System.Drawing.Size(1696, 736);
            this.tabAdmin.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabAdmin.TabIndex = 23;
            // 
            // tabUsers
            // 
            this.tabUsers.Controls.Add(this.cmbRol);
            this.tabUsers.Controls.Add(this.cbEstado);
            this.tabUsers.Controls.Add(this.txtSueldo);
            this.tabUsers.Controls.Add(this.txtContacto);
            this.tabUsers.Controls.Add(this.txtDocumento);
            this.tabUsers.Controls.Add(this.txtApellido);
            this.tabUsers.Controls.Add(this.txtNombre);
            this.tabUsers.Controls.Add(this.dgvUsuarios);
            this.tabUsers.Controls.Add(this.btnDelete);
            this.tabUsers.Controls.Add(this.lblAdminTitle);
            this.tabUsers.Controls.Add(this.btnModify);
            this.tabUsers.Controls.Add(this.txtAddUser);
            this.tabUsers.Controls.Add(this.btnAdd);
            this.tabUsers.Controls.Add(this.txtAddPass);
            this.tabUsers.Controls.Add(this.lblSueldo);
            this.tabUsers.Controls.Add(this.lblContacto);
            this.tabUsers.Controls.Add(this.lblRol);
            this.tabUsers.Controls.Add(this.lblDocumento);
            this.tabUsers.Controls.Add(this.lblApellido);
            this.tabUsers.Controls.Add(this.lblNombre);
            this.tabUsers.Controls.Add(this.lblPass);
            this.tabUsers.Controls.Add(this.lblName);
            this.tabUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabUsers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabUsers.Location = new System.Drawing.Point(4, 25);
            this.tabUsers.Margin = new System.Windows.Forms.Padding(4);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new System.Windows.Forms.Padding(4);
            this.tabUsers.Size = new System.Drawing.Size(1688, 707);
            this.tabUsers.TabIndex = 0;
            this.tabUsers.Text = "Usuarios";
            this.tabUsers.UseVisualStyleBackColor = true;
            this.tabUsers.Click += new System.EventHandler(this.tabUsers_Click);
            // 
            // cmbRol
            // 
            this.cmbRol.FormattingEnabled = true;
            this.cmbRol.Location = new System.Drawing.Point(1031, 464);
            this.cmbRol.Name = "cmbRol";
            this.cmbRol.Size = new System.Drawing.Size(188, 33);
            this.cmbRol.TabIndex = 25;
            // 
            // cbEstado
            // 
            this.cbEstado.AutoSize = true;
            this.cbEstado.Location = new System.Drawing.Point(1290, 464);
            this.cbEstado.Name = "cbEstado";
            this.cbEstado.Size = new System.Drawing.Size(94, 29);
            this.cbEstado.TabIndex = 24;
            this.cbEstado.Text = "Activo";
            this.cbEstado.UseVisualStyleBackColor = true;
            // 
            // txtSueldo
            // 
            this.txtSueldo.Location = new System.Drawing.Point(1031, 545);
            this.txtSueldo.Name = "txtSueldo";
            this.txtSueldo.Size = new System.Drawing.Size(188, 30);
            this.txtSueldo.TabIndex = 23;
            // 
            // txtContacto
            // 
            this.txtContacto.Location = new System.Drawing.Point(793, 545);
            this.txtContacto.Name = "txtContacto";
            this.txtContacto.Size = new System.Drawing.Size(188, 30);
            this.txtContacto.TabIndex = 23;
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(793, 467);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(188, 30);
            this.txtDocumento.TabIndex = 23;
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(551, 545);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(188, 30);
            this.txtApellido.TabIndex = 23;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(551, 467);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(188, 30);
            this.txtNombre.TabIndex = 23;
            // 
            // lblSueldo
            // 
            this.lblSueldo.AutoSize = true;
            this.lblSueldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSueldo.Location = new System.Drawing.Point(1026, 507);
            this.lblSueldo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSueldo.Name = "lblSueldo";
            this.lblSueldo.Size = new System.Drawing.Size(80, 25);
            this.lblSueldo.TabIndex = 19;
            this.lblSueldo.Text = "Sueldo";
            // 
            // lblContacto
            // 
            this.lblContacto.AutoSize = true;
            this.lblContacto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContacto.Location = new System.Drawing.Point(788, 506);
            this.lblContacto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContacto.Name = "lblContacto";
            this.lblContacto.Size = new System.Drawing.Size(99, 25);
            this.lblContacto.TabIndex = 19;
            this.lblContacto.Text = "Contacto";
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = true;
            this.lblRol.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRol.Location = new System.Drawing.Point(1026, 428);
            this.lblRol.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(43, 25);
            this.lblRol.TabIndex = 19;
            this.lblRol.Text = "Rol";
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocumento.Location = new System.Drawing.Point(788, 428);
            this.lblDocumento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(48, 25);
            this.lblDocumento.TabIndex = 19;
            this.lblDocumento.Text = "DNI";
            // 
            // lblApellido
            // 
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblApellido.Location = new System.Drawing.Point(546, 506);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(90, 25);
            this.lblApellido.TabIndex = 19;
            this.lblApellido.Text = "Apellido";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(546, 428);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(87, 25);
            this.lblNombre.TabIndex = 19;
            this.lblNombre.Text = "Nombre";
            // 
            // tabGastos
            // 
            this.tabGastos.Controls.Add(this.lblGastos);
            this.tabGastos.Controls.Add(this.dgvGastos);
            this.tabGastos.Location = new System.Drawing.Point(4, 25);
            this.tabGastos.Margin = new System.Windows.Forms.Padding(4);
            this.tabGastos.Name = "tabGastos";
            this.tabGastos.Padding = new System.Windows.Forms.Padding(4);
            this.tabGastos.Size = new System.Drawing.Size(1688, 707);
            this.tabGastos.TabIndex = 1;
            this.tabGastos.Text = "Gastos";
            this.tabGastos.UseVisualStyleBackColor = true;
            // 
            // lblGastos
            // 
            this.lblGastos.AutoSize = true;
            this.lblGastos.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGastos.Location = new System.Drawing.Point(669, 210);
            this.lblGastos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGastos.Name = "lblGastos";
            this.lblGastos.Size = new System.Drawing.Size(328, 42);
            this.lblGastos.TabIndex = 1;
            this.lblGastos.Text = "Control de gastos";
            // 
            // dgvGastos
            // 
            this.dgvGastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGastos.Location = new System.Drawing.Point(683, 305);
            this.dgvGastos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvGastos.Name = "dgvGastos";
            this.dgvGastos.RowHeadersWidth = 51;
            this.dgvGastos.Size = new System.Drawing.Size(320, 185);
            this.dgvGastos.TabIndex = 0;
            // 
            // tabCaja
            // 
            this.tabCaja.Location = new System.Drawing.Point(4, 25);
            this.tabCaja.Margin = new System.Windows.Forms.Padding(4);
            this.tabCaja.Name = "tabCaja";
            this.tabCaja.Padding = new System.Windows.Forms.Padding(4);
            this.tabCaja.Size = new System.Drawing.Size(1688, 707);
            this.tabCaja.TabIndex = 2;
            this.tabCaja.Text = "Caja";
            this.tabCaja.UseVisualStyleBackColor = true;
            // 
            // tabProveedores
            // 
            this.tabProveedores.Location = new System.Drawing.Point(4, 25);
            this.tabProveedores.Margin = new System.Windows.Forms.Padding(4);
            this.tabProveedores.Name = "tabProveedores";
            this.tabProveedores.Padding = new System.Windows.Forms.Padding(4);
            this.tabProveedores.Size = new System.Drawing.Size(1688, 707);
            this.tabProveedores.TabIndex = 3;
            this.tabProveedores.Text = "Proveedores";
            this.tabProveedores.UseVisualStyleBackColor = true;
            // 
            // tabComprobantes
            // 
            this.tabComprobantes.Location = new System.Drawing.Point(4, 25);
            this.tabComprobantes.Margin = new System.Windows.Forms.Padding(4);
            this.tabComprobantes.Name = "tabComprobantes";
            this.tabComprobantes.Padding = new System.Windows.Forms.Padding(4);
            this.tabComprobantes.Size = new System.Drawing.Size(1688, 707);
            this.tabComprobantes.TabIndex = 4;
            this.tabComprobantes.Text = "Comprobantes";
            this.tabComprobantes.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.DarkOrange;
            this.pictureBox3.Image = global::Thor_Bar.Properties.Resources.Thor_Bar_Logo2;
            this.pictureBox3.Location = new System.Drawing.Point(3, -8);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(91, 73);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 41;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.DarkOrange;
            this.pictureBox6.Location = new System.Drawing.Point(-9, -8);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(1696, 73);
            this.pictureBox6.TabIndex = 45;
            this.pictureBox6.TabStop = false;
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.BackColor = System.Drawing.Color.DimGray;
            this.lblNombreUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNombreUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreUsuario.Location = new System.Drawing.Point(1486, 69);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(102, 25);
            this.lblNombreUsuario.TabIndex = 47;
            this.lblNombreUsuario.Text = "lblSession";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.DarkOrange;
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(1570, 13);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(43, 42);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 46;
            this.pictureBox5.TabStop = false;
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1685, 897);
            this.Controls.Add(this.lblNombreUsuario);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.tabAdmin);
            this.Controls.Add(this.lblAdmin);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lblMesas);
            this.Controls.Add(this.lblCocina);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox6);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAdmin";
            this.Text = "Panel de Administracion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.tabAdmin.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            this.tabGastos.ResumeLayout(false);
            this.tabGastos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAdmin;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblMesas;
        private System.Windows.Forms.Label lblCocina;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.TextBox txtAddUser;
        private System.Windows.Forms.TextBox txtAddPass;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TabControl tabAdmin;
        private System.Windows.Forms.TabPage tabUsers;
        private System.Windows.Forms.TabPage tabGastos;
        private System.Windows.Forms.TabPage tabCaja;
        private System.Windows.Forms.TabPage tabProveedores;
        private System.Windows.Forms.TabPage tabComprobantes;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblGastos;
        private System.Windows.Forms.DataGridView dgvGastos;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.Label lblApellido;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtContacto;
        private System.Windows.Forms.Label lblContacto;
        private System.Windows.Forms.ComboBox cmbRol;
        private System.Windows.Forms.CheckBox cbEstado;
        private System.Windows.Forms.TextBox txtSueldo;
        private System.Windows.Forms.Label lblSueldo;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}