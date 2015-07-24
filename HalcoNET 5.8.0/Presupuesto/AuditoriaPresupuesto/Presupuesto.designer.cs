namespace Presupuesto
{
    partial class frmPresupuesto
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPresupuesto));
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.cbSucursal = new System.Windows.Forms.ComboBox();
            this.btnPresentar = new System.Windows.Forms.Button();
            this.dgvPresupuesto = new System.Windows.Forms.DataGridView();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbDefectos = new System.Windows.Forms.RadioButton();
            this.rbExcesos = new System.Windows.Forms.RadioButton();
            this.rbTodo = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAño1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMes1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.txtAño = new System.Windows.Forms.TextBox();
            this.cmbMes = new System.Windows.Forms.ComboBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.lblCuentasGasto = new System.Windows.Forms.Label();
            this.clbCuentas = new System.Windows.Forms.CheckedListBox();
            this.lblFijo = new System.Windows.Forms.Label();
            this.lblVariableForzoso = new System.Windows.Forms.Label();
            this.lblVariableOpcional = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDesviacionFijo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblDesviacionForzoso = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblDesviacionOpcional = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblTotalDesviacion = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTotalPorcentaje = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblPorcentajeOpcional = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblPorcentajeForzoso = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblPorcentajeFijo = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblTotalUtilizado = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lblUtilizadoOpcional = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lblUtilizadoForzoso = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.lblUtilizadoFijo = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.lblTotalPresupuesto = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.lblPresupuestoOpcional = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.lblPresupuestoForzoso = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.lblPresupuestoFijo = new System.Windows.Forms.Label();
            this.dgvCOM = new System.Windows.Forms.DataGridView();
            this.dgvTotales = new System.Windows.Forms.DataGridView();
            this.eProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresupuesto)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicial.Location = new System.Drawing.Point(13, 79);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(17, 15);
            this.lblFechaInicial.TabIndex = 0;
            this.lblFechaInicial.Text = "A:";
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFinal.Location = new System.Drawing.Point(35, 606);
            this.lblFechaFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(81, 16);
            this.lblFechaFinal.TabIndex = 1;
            this.lblFechaFinal.Text = "Fecha Final:";
            this.lblFechaFinal.Visible = false;
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicial.Location = new System.Drawing.Point(942, 63);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(186, 22);
            this.dtpFechaInicial.TabIndex = 2;
            this.dtpFechaInicial.Visible = false;
            // 
            // dtpFechaFinal
            // 
            this.dtpFechaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFinal.Location = new System.Drawing.Point(134, 601);
            this.dtpFechaFinal.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaFinal.Name = "dtpFechaFinal";
            this.dtpFechaFinal.Size = new System.Drawing.Size(186, 22);
            this.dtpFechaFinal.TabIndex = 3;
            this.dtpFechaFinal.Visible = false;
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucursal.Location = new System.Drawing.Point(13, 122);
            this.lblSucursal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(58, 15);
            this.lblSucursal.TabIndex = 4;
            this.lblSucursal.Text = "Sucursal:";
            // 
            // cbSucursal
            // 
            this.cbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSucursal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSucursal.FormattingEnabled = true;
            this.cbSucursal.Location = new System.Drawing.Point(75, 118);
            this.cbSucursal.Margin = new System.Windows.Forms.Padding(4);
            this.cbSucursal.Name = "cbSucursal";
            this.cbSucursal.Size = new System.Drawing.Size(243, 23);
            this.cbSucursal.TabIndex = 5;
            this.cbSucursal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAño_KeyUp);
            // 
            // btnPresentar
            // 
            this.btnPresentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPresentar.Location = new System.Drawing.Point(866, 51);
            this.btnPresentar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPresentar.Name = "btnPresentar";
            this.btnPresentar.Size = new System.Drawing.Size(116, 30);
            this.btnPresentar.TabIndex = 6;
            this.btnPresentar.Text = "Presentar";
            this.btnPresentar.UseVisualStyleBackColor = true;
            this.btnPresentar.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvPresupuesto
            // 
            this.dgvPresupuesto.AllowUserToAddRows = false;
            this.dgvPresupuesto.AllowUserToDeleteRows = false;
            this.dgvPresupuesto.AllowUserToResizeColumns = false;
            this.dgvPresupuesto.AllowUserToResizeRows = false;
            this.dgvPresupuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPresupuesto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPresupuesto.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgvPresupuesto.Location = new System.Drawing.Point(28, 197);
            this.dgvPresupuesto.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPresupuesto.Name = "dgvPresupuesto";
            this.dgvPresupuesto.ReadOnly = true;
            this.dgvPresupuesto.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPresupuesto.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPresupuesto.Size = new System.Drawing.Size(1136, 462);
            this.dgvPresupuesto.TabIndex = 7;
            this.dgvPresupuesto.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPresupuesto_CellContentClick);
            this.dgvPresupuesto.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvPresupuesto_CellPainting);
            this.dgvPresupuesto.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvPresupuesto_DataBindingComplete);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(1009, 47);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(116, 30);
            this.btnFiltrar.TabIndex = 9;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = true;
            this.btnFiltrar.Visible = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbDefectos);
            this.groupBox1.Controls.Add(this.rbExcesos);
            this.groupBox1.Controls.Add(this.rbTodo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAño1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbMes1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dataGridView2);
            this.groupBox1.Controls.Add(this.txtAño);
            this.groupBox1.Controls.Add(this.cmbMes);
            this.groupBox1.Controls.Add(this.btnExportar);
            this.groupBox1.Controls.Add(this.lblCuentasGasto);
            this.groupBox1.Controls.Add(this.clbCuentas);
            this.groupBox1.Controls.Add(this.btnFiltrar);
            this.groupBox1.Controls.Add(this.btnPresentar);
            this.groupBox1.Controls.Add(this.cbSucursal);
            this.groupBox1.Controls.Add(this.lblFechaInicial);
            this.groupBox1.Controls.Add(this.lblSucursal);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(28, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1136, 153);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PRESUPUESTO GENERAL";
            // 
            // rbDefectos
            // 
            this.rbDefectos.AutoSize = true;
            this.rbDefectos.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDefectos.ForeColor = System.Drawing.Color.Green;
            this.rbDefectos.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbDefectos.Location = new System.Drawing.Point(695, 106);
            this.rbDefectos.Name = "rbDefectos";
            this.rbDefectos.Size = new System.Drawing.Size(132, 19);
            this.rbDefectos.TabIndex = 52;
            this.rbDefectos.Text = "Filtrar defectos        ";
            this.rbDefectos.UseVisualStyleBackColor = true;
            this.rbDefectos.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // rbExcesos
            // 
            this.rbExcesos.AutoSize = true;
            this.rbExcesos.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbExcesos.ForeColor = System.Drawing.Color.Red;
            this.rbExcesos.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rbExcesos.Location = new System.Drawing.Point(695, 77);
            this.rbExcesos.Name = "rbExcesos";
            this.rbExcesos.Size = new System.Drawing.Size(130, 19);
            this.rbExcesos.TabIndex = 51;
            this.rbExcesos.Text = "Filtar Excesos           ";
            this.rbExcesos.UseVisualStyleBackColor = true;
            this.rbExcesos.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // rbTodo
            // 
            this.rbTodo.AutoSize = true;
            this.rbTodo.Checked = true;
            this.rbTodo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTodo.Location = new System.Drawing.Point(695, 48);
            this.rbTodo.Name = "rbTodo";
            this.rbTodo.Size = new System.Drawing.Size(51, 19);
            this.rbTodo.TabIndex = 50;
            this.rbTodo.TabStop = true;
            this.rbTodo.Text = "Todo";
            this.rbTodo.UseVisualStyleBackColor = true;
            this.rbTodo.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 15);
            this.label1.TabIndex = 49;
            this.label1.Text = "De:";
            // 
            // txtAño1
            // 
            this.txtAño1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAño1.Location = new System.Drawing.Point(218, 48);
            this.txtAño1.MaxLength = 4;
            this.txtAño1.Name = "txtAño1";
            this.txtAño1.Size = new System.Drawing.Size(100, 21);
            this.txtAño1.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(253, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 47;
            this.label2.Text = "Año:";
            // 
            // cbMes1
            // 
            this.cbMes1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.cbMes1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMes1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbMes1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMes1.FormattingEnabled = true;
            this.cbMes1.Location = new System.Drawing.Point(75, 47);
            this.cbMes1.Margin = new System.Windows.Forms.Padding(4);
            this.cbMes1.Name = "cbMes1";
            this.cbMes1.Size = new System.Drawing.Size(103, 23);
            this.cbMes1.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(109, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 15);
            this.label3.TabIndex = 45;
            this.label3.Text = "Mes:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(1063, 84);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.Format = "C2";
            this.dataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView2.Size = new System.Drawing.Size(25, 62);
            this.dataGridView2.TabIndex = 44;
            this.dataGridView2.Visible = false;
            // 
            // txtAño
            // 
            this.txtAño.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAño.Location = new System.Drawing.Point(218, 76);
            this.txtAño.MaxLength = 4;
            this.txtAño.Name = "txtAño";
            this.txtAño.Size = new System.Drawing.Size(100, 21);
            this.txtAño.TabIndex = 24;
            this.txtAño.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAño_KeyUp);
            // 
            // cmbMes
            // 
            this.cmbMes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.cmbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMes.FormattingEnabled = true;
            this.cmbMes.Location = new System.Drawing.Point(75, 75);
            this.cmbMes.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMes.Name = "cmbMes";
            this.cmbMes.Size = new System.Drawing.Size(103, 23);
            this.cmbMes.TabIndex = 22;
            // 
            // btnExportar
            // 
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.Location = new System.Drawing.Point(866, 93);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(116, 30);
            this.btnExportar.TabIndex = 21;
            this.btnExportar.Text = "Exportar a Excel";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lblCuentasGasto
            // 
            this.lblCuentasGasto.AutoSize = true;
            this.lblCuentasGasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuentasGasto.Location = new System.Drawing.Point(331, 23);
            this.lblCuentasGasto.Name = "lblCuentasGasto";
            this.lblCuentasGasto.Size = new System.Drawing.Size(107, 15);
            this.lblCuentasGasto.TabIndex = 20;
            this.lblCuentasGasto.Text = "Cuentas de Gasto:";
            // 
            // clbCuentas
            // 
            this.clbCuentas.CheckOnClick = true;
            this.clbCuentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbCuentas.FormattingEnabled = true;
            this.clbCuentas.Location = new System.Drawing.Point(334, 41);
            this.clbCuentas.Name = "clbCuentas";
            this.clbCuentas.Size = new System.Drawing.Size(355, 100);
            this.clbCuentas.TabIndex = 10;
            this.clbCuentas.Click += new System.EventHandler(this.clbCuentas_Click);
            // 
            // lblFijo
            // 
            this.lblFijo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblFijo.AutoSize = true;
            this.lblFijo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFijo.Location = new System.Drawing.Point(661, 561);
            this.lblFijo.Name = "lblFijo";
            this.lblFijo.Size = new System.Drawing.Size(34, 16);
            this.lblFijo.TabIndex = 11;
            this.lblFijo.Text = "Fijo";
            this.lblFijo.UseWaitCursor = true;
            this.lblFijo.Visible = false;
            // 
            // lblVariableForzoso
            // 
            this.lblVariableForzoso.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblVariableForzoso.AutoSize = true;
            this.lblVariableForzoso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVariableForzoso.Location = new System.Drawing.Point(568, 589);
            this.lblVariableForzoso.Name = "lblVariableForzoso";
            this.lblVariableForzoso.Size = new System.Drawing.Size(127, 16);
            this.lblVariableForzoso.TabIndex = 12;
            this.lblVariableForzoso.Text = "Variable Forzoso";
            this.lblVariableForzoso.UseWaitCursor = true;
            this.lblVariableForzoso.Visible = false;
            // 
            // lblVariableOpcional
            // 
            this.lblVariableOpcional.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblVariableOpcional.AutoSize = true;
            this.lblVariableOpcional.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVariableOpcional.Location = new System.Drawing.Point(562, 617);
            this.lblVariableOpcional.Name = "lblVariableOpcional";
            this.lblVariableOpcional.Size = new System.Drawing.Size(133, 16);
            this.lblVariableOpcional.TabIndex = 13;
            this.lblVariableOpcional.Text = "Variable Opcional";
            this.lblVariableOpcional.UseWaitCursor = true;
            this.lblVariableOpcional.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblDesviacionFijo);
            this.panel1.Location = new System.Drawing.Point(1045, 559);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(110, 22);
            this.panel1.TabIndex = 35;
            this.panel1.UseWaitCursor = true;
            this.panel1.Visible = false;
            // 
            // lblDesviacionFijo
            // 
            this.lblDesviacionFijo.AutoSize = true;
            this.lblDesviacionFijo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDesviacionFijo.Location = new System.Drawing.Point(106, 0);
            this.lblDesviacionFijo.Name = "lblDesviacionFijo";
            this.lblDesviacionFijo.Size = new System.Drawing.Size(0, 16);
            this.lblDesviacionFijo.TabIndex = 36;
            this.lblDesviacionFijo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDesviacionFijo.UseWaitCursor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.lblDesviacionForzoso);
            this.panel2.Location = new System.Drawing.Point(1045, 587);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 22);
            this.panel2.TabIndex = 37;
            this.panel2.UseWaitCursor = true;
            this.panel2.Visible = false;
            // 
            // lblDesviacionForzoso
            // 
            this.lblDesviacionForzoso.AutoSize = true;
            this.lblDesviacionForzoso.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDesviacionForzoso.Location = new System.Drawing.Point(106, 0);
            this.lblDesviacionForzoso.Name = "lblDesviacionForzoso";
            this.lblDesviacionForzoso.Size = new System.Drawing.Size(0, 16);
            this.lblDesviacionForzoso.TabIndex = 36;
            this.lblDesviacionForzoso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDesviacionForzoso.UseWaitCursor = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.lblDesviacionOpcional);
            this.panel3.Location = new System.Drawing.Point(1045, 615);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(110, 22);
            this.panel3.TabIndex = 38;
            this.panel3.UseWaitCursor = true;
            this.panel3.Visible = false;
            // 
            // lblDesviacionOpcional
            // 
            this.lblDesviacionOpcional.AutoSize = true;
            this.lblDesviacionOpcional.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDesviacionOpcional.Location = new System.Drawing.Point(106, 0);
            this.lblDesviacionOpcional.Name = "lblDesviacionOpcional";
            this.lblDesviacionOpcional.Size = new System.Drawing.Size(0, 16);
            this.lblDesviacionOpcional.TabIndex = 36;
            this.lblDesviacionOpcional.UseWaitCursor = true;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.lblTotalDesviacion);
            this.panel4.Location = new System.Drawing.Point(1045, 643);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(110, 22);
            this.panel4.TabIndex = 37;
            this.panel4.UseWaitCursor = true;
            this.panel4.Visible = false;
            // 
            // lblTotalDesviacion
            // 
            this.lblTotalDesviacion.AutoSize = true;
            this.lblTotalDesviacion.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalDesviacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDesviacion.Location = new System.Drawing.Point(106, 0);
            this.lblTotalDesviacion.Name = "lblTotalDesviacion";
            this.lblTotalDesviacion.Size = new System.Drawing.Size(0, 16);
            this.lblTotalDesviacion.TabIndex = 36;
            this.lblTotalDesviacion.UseWaitCursor = true;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.lblTotalPorcentaje);
            this.panel5.Location = new System.Drawing.Point(942, 643);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(97, 22);
            this.panel5.TabIndex = 41;
            this.panel5.UseWaitCursor = true;
            this.panel5.Visible = false;
            // 
            // lblTotalPorcentaje
            // 
            this.lblTotalPorcentaje.AutoSize = true;
            this.lblTotalPorcentaje.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPorcentaje.Location = new System.Drawing.Point(93, 0);
            this.lblTotalPorcentaje.Name = "lblTotalPorcentaje";
            this.lblTotalPorcentaje.Size = new System.Drawing.Size(0, 16);
            this.lblTotalPorcentaje.TabIndex = 36;
            this.lblTotalPorcentaje.UseWaitCursor = true;
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Controls.Add(this.lblPorcentajeOpcional);
            this.panel6.Location = new System.Drawing.Point(942, 615);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(97, 22);
            this.panel6.TabIndex = 42;
            this.panel6.Visible = false;
            // 
            // lblPorcentajeOpcional
            // 
            this.lblPorcentajeOpcional.AutoSize = true;
            this.lblPorcentajeOpcional.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPorcentajeOpcional.Location = new System.Drawing.Point(93, 0);
            this.lblPorcentajeOpcional.Name = "lblPorcentajeOpcional";
            this.lblPorcentajeOpcional.Size = new System.Drawing.Size(0, 16);
            this.lblPorcentajeOpcional.TabIndex = 36;
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.lblPorcentajeForzoso);
            this.panel7.Location = new System.Drawing.Point(942, 587);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(97, 22);
            this.panel7.TabIndex = 40;
            this.panel7.UseWaitCursor = true;
            this.panel7.Visible = false;
            // 
            // lblPorcentajeForzoso
            // 
            this.lblPorcentajeForzoso.AutoSize = true;
            this.lblPorcentajeForzoso.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPorcentajeForzoso.Location = new System.Drawing.Point(93, 0);
            this.lblPorcentajeForzoso.Name = "lblPorcentajeForzoso";
            this.lblPorcentajeForzoso.Size = new System.Drawing.Size(0, 16);
            this.lblPorcentajeForzoso.TabIndex = 36;
            this.lblPorcentajeForzoso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPorcentajeForzoso.UseWaitCursor = true;
            // 
            // panel8
            // 
            this.panel8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.Controls.Add(this.lblPorcentajeFijo);
            this.panel8.Location = new System.Drawing.Point(942, 559);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(97, 22);
            this.panel8.TabIndex = 39;
            this.panel8.UseWaitCursor = true;
            this.panel8.Visible = false;
            // 
            // lblPorcentajeFijo
            // 
            this.lblPorcentajeFijo.AutoSize = true;
            this.lblPorcentajeFijo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPorcentajeFijo.Location = new System.Drawing.Point(93, 0);
            this.lblPorcentajeFijo.Name = "lblPorcentajeFijo";
            this.lblPorcentajeFijo.Size = new System.Drawing.Size(0, 16);
            this.lblPorcentajeFijo.TabIndex = 36;
            this.lblPorcentajeFijo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPorcentajeFijo.UseWaitCursor = true;
            // 
            // panel9
            // 
            this.panel9.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel9.Controls.Add(this.lblTotalUtilizado);
            this.panel9.Location = new System.Drawing.Point(826, 643);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(110, 22);
            this.panel9.TabIndex = 41;
            this.panel9.UseWaitCursor = true;
            this.panel9.Visible = false;
            // 
            // lblTotalUtilizado
            // 
            this.lblTotalUtilizado.AutoSize = true;
            this.lblTotalUtilizado.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalUtilizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalUtilizado.Location = new System.Drawing.Point(106, 0);
            this.lblTotalUtilizado.Name = "lblTotalUtilizado";
            this.lblTotalUtilizado.Size = new System.Drawing.Size(0, 16);
            this.lblTotalUtilizado.TabIndex = 36;
            this.lblTotalUtilizado.UseWaitCursor = true;
            // 
            // panel10
            // 
            this.panel10.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel10.Controls.Add(this.lblUtilizadoOpcional);
            this.panel10.Location = new System.Drawing.Point(826, 615);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(110, 22);
            this.panel10.TabIndex = 42;
            this.panel10.UseWaitCursor = true;
            this.panel10.Visible = false;
            // 
            // lblUtilizadoOpcional
            // 
            this.lblUtilizadoOpcional.AutoSize = true;
            this.lblUtilizadoOpcional.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUtilizadoOpcional.Location = new System.Drawing.Point(106, 0);
            this.lblUtilizadoOpcional.Name = "lblUtilizadoOpcional";
            this.lblUtilizadoOpcional.Size = new System.Drawing.Size(0, 16);
            this.lblUtilizadoOpcional.TabIndex = 36;
            this.lblUtilizadoOpcional.UseWaitCursor = true;
            // 
            // panel11
            // 
            this.panel11.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel11.Controls.Add(this.lblUtilizadoForzoso);
            this.panel11.Location = new System.Drawing.Point(826, 587);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(110, 22);
            this.panel11.TabIndex = 40;
            this.panel11.UseWaitCursor = true;
            this.panel11.Visible = false;
            // 
            // lblUtilizadoForzoso
            // 
            this.lblUtilizadoForzoso.AutoSize = true;
            this.lblUtilizadoForzoso.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUtilizadoForzoso.Location = new System.Drawing.Point(106, 0);
            this.lblUtilizadoForzoso.Name = "lblUtilizadoForzoso";
            this.lblUtilizadoForzoso.Size = new System.Drawing.Size(0, 16);
            this.lblUtilizadoForzoso.TabIndex = 36;
            this.lblUtilizadoForzoso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUtilizadoForzoso.UseWaitCursor = true;
            // 
            // panel12
            // 
            this.panel12.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel12.Controls.Add(this.lblUtilizadoFijo);
            this.panel12.Location = new System.Drawing.Point(826, 559);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(110, 22);
            this.panel12.TabIndex = 39;
            this.panel12.UseWaitCursor = true;
            this.panel12.Visible = false;
            // 
            // lblUtilizadoFijo
            // 
            this.lblUtilizadoFijo.AutoSize = true;
            this.lblUtilizadoFijo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUtilizadoFijo.Location = new System.Drawing.Point(106, 0);
            this.lblUtilizadoFijo.Name = "lblUtilizadoFijo";
            this.lblUtilizadoFijo.Size = new System.Drawing.Size(0, 16);
            this.lblUtilizadoFijo.TabIndex = 36;
            this.lblUtilizadoFijo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUtilizadoFijo.UseWaitCursor = true;
            // 
            // panel13
            // 
            this.panel13.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel13.Controls.Add(this.lblTotalPresupuesto);
            this.panel13.Location = new System.Drawing.Point(710, 643);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(110, 22);
            this.panel13.TabIndex = 41;
            this.panel13.UseWaitCursor = true;
            this.panel13.Visible = false;
            // 
            // lblTotalPresupuesto
            // 
            this.lblTotalPresupuesto.AutoSize = true;
            this.lblTotalPresupuesto.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalPresupuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalPresupuesto.Location = new System.Drawing.Point(106, 0);
            this.lblTotalPresupuesto.Name = "lblTotalPresupuesto";
            this.lblTotalPresupuesto.Size = new System.Drawing.Size(0, 16);
            this.lblTotalPresupuesto.TabIndex = 36;
            this.lblTotalPresupuesto.UseWaitCursor = true;
            // 
            // panel14
            // 
            this.panel14.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel14.Controls.Add(this.lblPresupuestoOpcional);
            this.panel14.Location = new System.Drawing.Point(710, 615);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(110, 22);
            this.panel14.TabIndex = 42;
            this.panel14.UseWaitCursor = true;
            this.panel14.Visible = false;
            // 
            // lblPresupuestoOpcional
            // 
            this.lblPresupuestoOpcional.AutoSize = true;
            this.lblPresupuestoOpcional.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPresupuestoOpcional.Location = new System.Drawing.Point(106, 0);
            this.lblPresupuestoOpcional.Name = "lblPresupuestoOpcional";
            this.lblPresupuestoOpcional.Size = new System.Drawing.Size(0, 16);
            this.lblPresupuestoOpcional.TabIndex = 36;
            this.lblPresupuestoOpcional.UseWaitCursor = true;
            // 
            // panel15
            // 
            this.panel15.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel15.Controls.Add(this.lblPresupuestoForzoso);
            this.panel15.Location = new System.Drawing.Point(710, 587);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(110, 22);
            this.panel15.TabIndex = 40;
            this.panel15.UseWaitCursor = true;
            this.panel15.Visible = false;
            // 
            // lblPresupuestoForzoso
            // 
            this.lblPresupuestoForzoso.AutoSize = true;
            this.lblPresupuestoForzoso.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPresupuestoForzoso.Location = new System.Drawing.Point(106, 0);
            this.lblPresupuestoForzoso.Name = "lblPresupuestoForzoso";
            this.lblPresupuestoForzoso.Size = new System.Drawing.Size(0, 16);
            this.lblPresupuestoForzoso.TabIndex = 36;
            this.lblPresupuestoForzoso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPresupuestoForzoso.UseWaitCursor = true;
            // 
            // panel16
            // 
            this.panel16.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel16.Controls.Add(this.lblPresupuestoFijo);
            this.panel16.Location = new System.Drawing.Point(710, 559);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(110, 22);
            this.panel16.TabIndex = 39;
            this.panel16.UseWaitCursor = true;
            this.panel16.Visible = false;
            // 
            // lblPresupuestoFijo
            // 
            this.lblPresupuestoFijo.AutoSize = true;
            this.lblPresupuestoFijo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPresupuestoFijo.Location = new System.Drawing.Point(106, 0);
            this.lblPresupuestoFijo.Name = "lblPresupuestoFijo";
            this.lblPresupuestoFijo.Size = new System.Drawing.Size(0, 16);
            this.lblPresupuestoFijo.TabIndex = 36;
            this.lblPresupuestoFijo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPresupuestoFijo.UseWaitCursor = true;
            // 
            // dgvCOM
            // 
            this.dgvCOM.AllowUserToAddRows = false;
            this.dgvCOM.AllowUserToDeleteRows = false;
            this.dgvCOM.AllowUserToResizeColumns = false;
            this.dgvCOM.AllowUserToResizeRows = false;
            this.dgvCOM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCOM.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvCOM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCOM.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvCOM.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCOM.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCOM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCOM.EnableHeadersVisualStyles = false;
            this.dgvCOM.Location = new System.Drawing.Point(743, 663);
            this.dgvCOM.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCOM.Name = "dgvCOM";
            this.dgvCOM.ReadOnly = true;
            this.dgvCOM.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCOM.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCOM.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCOM.Size = new System.Drawing.Size(419, 136);
            this.dgvCOM.TabIndex = 44;
            this.dgvCOM.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvTotales_DataBindingComplete_1);
            // 
            // dgvTotales
            // 
            this.dgvTotales.AllowUserToAddRows = false;
            this.dgvTotales.AllowUserToDeleteRows = false;
            this.dgvTotales.AllowUserToResizeColumns = false;
            this.dgvTotales.AllowUserToResizeRows = false;
            this.dgvTotales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvTotales.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTotales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTotales.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvTotales.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTotales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotales.EnableHeadersVisualStyles = false;
            this.dgvTotales.Location = new System.Drawing.Point(28, 663);
            this.dgvTotales.Margin = new System.Windows.Forms.Padding(4);
            this.dgvTotales.Name = "dgvTotales";
            this.dgvTotales.ReadOnly = true;
            this.dgvTotales.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvTotales.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvTotales.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTotales.Size = new System.Drawing.Size(707, 136);
            this.dgvTotales.TabIndex = 45;
            this.dgvTotales.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvTotales_DataBindingComplete_1);
            // 
            // eProvider
            // 
            this.eProvider.ContainerControl = this;
            // 
            // frmPresupuesto
            // 
            this.AccessibleDescription = "Auditoria de gasto";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1193, 803);
            this.Controls.Add(this.dgvTotales);
            this.Controls.Add(this.dgvCOM);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel16);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dtpFechaInicial);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblVariableOpcional);
            this.Controls.Add(this.lblVariableForzoso);
            this.Controls.Add(this.lblFijo);
            this.Controls.Add(this.lblFechaFinal);
            this.Controls.Add(this.dtpFechaFinal);
            this.Controls.Add(this.dgvPresupuesto);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmPresupuesto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SGUV- [Auditoria de gasto]";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPresupuesto_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPresupuesto_FormClosed);
            this.Load += new System.EventHandler(this.frmPresupuesto_Load);
            this.Shown += new System.EventHandler(this.frmPresupuesto_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPresupuesto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.Label lblFechaFinal;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.DateTimePicker dtpFechaFinal;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.ComboBox cbSucursal;
        private System.Windows.Forms.Button btnPresentar;
        private System.Windows.Forms.DataGridView dgvPresupuesto;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox clbCuentas;
        private System.Windows.Forms.Label lblCuentasGasto;
        private System.Windows.Forms.Label lblFijo;
        private System.Windows.Forms.Label lblVariableForzoso;
        private System.Windows.Forms.Label lblVariableOpcional;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDesviacionFijo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblDesviacionForzoso;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblDesviacionOpcional;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTotalDesviacion;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTotalPorcentaje;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblPorcentajeOpcional;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblPorcentajeForzoso;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblPorcentajeFijo;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label lblTotalUtilizado;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lblUtilizadoOpcional;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label lblUtilizadoForzoso;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label lblUtilizadoFijo;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label lblTotalPresupuesto;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label lblPresupuestoOpcional;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label lblPresupuestoForzoso;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label lblPresupuestoFijo;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.ComboBox cmbMes;
        private System.Windows.Forms.TextBox txtAño;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dgvCOM;
        private System.Windows.Forms.DataGridView dgvTotales;
        private System.Windows.Forms.TextBox txtAño1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMes1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ErrorProvider eProvider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbExcesos;
        private System.Windows.Forms.RadioButton rbTodo;
        private System.Windows.Forms.RadioButton rbDefectos;
    }
}

