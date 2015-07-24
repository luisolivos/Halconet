namespace Ventas.Ventas.ScoreCard
{
    partial class frmBonoLineasMoradas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBonoLineasMoradas));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.gridExcel = new System.Windows.Forms.DataGridView();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.clbSucursal = new System.Windows.Forms.CheckedListBox();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnPresentar = new System.Windows.Forms.Button();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.clbVendedor = new System.Windows.Forms.CheckedListBox();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.gbTitulo = new System.Windows.Forms.GroupBox();
            this.gridTotales = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAvanceOptimo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDiasRestantes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDiasTranscurridos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDiasMes = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gridLineas = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExcel)).BeginInit();
            this.gbTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTotales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLineas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.gridExcel);
            this.groupBox1.Controls.Add(this.lblSucursal);
            this.groupBox1.Controls.Add(this.clbSucursal);
            this.groupBox1.Controls.Add(this.btnExportar);
            this.groupBox1.Controls.Add(this.btnPresentar);
            this.groupBox1.Controls.Add(this.lblVendedor);
            this.groupBox1.Controls.Add(this.clbVendedor);
            this.groupBox1.Controls.Add(this.lblFechaInicial);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(189, 664);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bono lineas halcón";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(14, 56);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(124, 20);
            this.dateTimePicker1.TabIndex = 95;
            this.dateTimePicker1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // gridExcel
            // 
            this.gridExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridExcel.Location = new System.Drawing.Point(27, 487);
            this.gridExcel.Name = "gridExcel";
            this.gridExcel.Size = new System.Drawing.Size(101, 54);
            this.gridExcel.TabIndex = 64;
            this.gridExcel.Visible = false;
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucursal.Location = new System.Drawing.Point(8, 89);
            this.lblSucursal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(51, 13);
            this.lblSucursal.TabIndex = 29;
            this.lblSucursal.Text = "Sucursal:";
            // 
            // clbSucursal
            // 
            this.clbSucursal.CheckOnClick = true;
            this.clbSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbSucursal.FormattingEnabled = true;
            this.clbSucursal.Location = new System.Drawing.Point(12, 113);
            this.clbSucursal.Margin = new System.Windows.Forms.Padding(4);
            this.clbSucursal.Name = "clbSucursal";
            this.clbSucursal.Size = new System.Drawing.Size(169, 64);
            this.clbSucursal.TabIndex = 8;
            this.clbSucursal.Click += new System.EventHandler(this.clbSucursal_Click);
            this.clbSucursal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // btnExportar
            // 
            this.btnExportar.Enabled = false;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.Location = new System.Drawing.Point(61, 377);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(4);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnExportar.Size = new System.Drawing.Size(120, 48);
            this.btnExportar.TabIndex = 27;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            this.btnExportar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // btnPresentar
            // 
            this.btnPresentar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPresentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPresentar.Image = ((System.Drawing.Image)(resources.GetObject("btnPresentar.Image")));
            this.btnPresentar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPresentar.Location = new System.Drawing.Point(61, 311);
            this.btnPresentar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPresentar.Name = "btnPresentar";
            this.btnPresentar.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnPresentar.Size = new System.Drawing.Size(120, 49);
            this.btnPresentar.TabIndex = 2;
            this.btnPresentar.Text = "Buscar";
            this.btnPresentar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPresentar.UseVisualStyleBackColor = true;
            this.btnPresentar.Click += new System.EventHandler(this.btnPresentar_Click);
            this.btnPresentar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendedor.Location = new System.Drawing.Point(8, 200);
            this.lblVendedor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(56, 13);
            this.lblVendedor.TabIndex = 14;
            this.lblVendedor.Text = "Vendedor:";
            // 
            // clbVendedor
            // 
            this.clbVendedor.CheckOnClick = true;
            this.clbVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbVendedor.FormattingEnabled = true;
            this.clbVendedor.Location = new System.Drawing.Point(12, 224);
            this.clbVendedor.Margin = new System.Windows.Forms.Padding(4);
            this.clbVendedor.Name = "clbVendedor";
            this.clbVendedor.Size = new System.Drawing.Size(169, 79);
            this.clbVendedor.TabIndex = 10;
            this.clbVendedor.Click += new System.EventHandler(this.clbVendedor_Click);
            this.clbVendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicial.Location = new System.Drawing.Point(8, 36);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(70, 13);
            this.lblFechaInicial.TabIndex = 3;
            this.lblFechaInicial.Text = "Fecha Inicial:";
            // 
            // gbTitulo
            // 
            this.gbTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTitulo.Controls.Add(this.gridTotales);
            this.gbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTitulo.Location = new System.Drawing.Point(500, 13);
            this.gbTitulo.Margin = new System.Windows.Forms.Padding(4);
            this.gbTitulo.Name = "gbTitulo";
            this.gbTitulo.Padding = new System.Windows.Forms.Padding(4);
            this.gbTitulo.Size = new System.Drawing.Size(464, 96);
            this.gbTitulo.TabIndex = 105;
            this.gbTitulo.TabStop = false;
            this.gbTitulo.Text = "Subtotales";
            // 
            // gridTotales
            // 
            this.gridTotales.AllowUserToAddRows = false;
            this.gridTotales.AllowUserToDeleteRows = false;
            this.gridTotales.AllowUserToResizeRows = false;
            this.gridTotales.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridTotales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTotales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTotales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTotales.EnableHeadersVisualStyles = false;
            this.gridTotales.Location = new System.Drawing.Point(4, 19);
            this.gridTotales.Margin = new System.Windows.Forms.Padding(5);
            this.gridTotales.Name = "gridTotales";
            this.gridTotales.ReadOnly = true;
            this.gridTotales.Size = new System.Drawing.Size(456, 73);
            this.gridTotales.TabIndex = 91;
            this.gridTotales.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(324, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 104;
            this.label2.Text = "Avance Optimo";
            // 
            // txtAvanceOptimo
            // 
            this.txtAvanceOptimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvanceOptimo.Location = new System.Drawing.Point(427, 77);
            this.txtAvanceOptimo.Margin = new System.Windows.Forms.Padding(4);
            this.txtAvanceOptimo.Name = "txtAvanceOptimo";
            this.txtAvanceOptimo.ReadOnly = true;
            this.txtAvanceOptimo.Size = new System.Drawing.Size(65, 20);
            this.txtAvanceOptimo.TabIndex = 103;
            this.txtAvanceOptimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAvanceOptimo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(324, 60);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 102;
            this.label9.Text = "Días Restantes";
            // 
            // txtDiasRestantes
            // 
            this.txtDiasRestantes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasRestantes.Location = new System.Drawing.Point(427, 56);
            this.txtDiasRestantes.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiasRestantes.Name = "txtDiasRestantes";
            this.txtDiasRestantes.ReadOnly = true;
            this.txtDiasRestantes.Size = new System.Drawing.Size(65, 20);
            this.txtDiasRestantes.TabIndex = 101;
            this.txtDiasRestantes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiasRestantes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(324, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 100;
            this.label4.Text = "Días Transcurridos";
            // 
            // txtDiasTranscurridos
            // 
            this.txtDiasTranscurridos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasTranscurridos.Location = new System.Drawing.Point(427, 35);
            this.txtDiasTranscurridos.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiasTranscurridos.Name = "txtDiasTranscurridos";
            this.txtDiasTranscurridos.ReadOnly = true;
            this.txtDiasTranscurridos.Size = new System.Drawing.Size(65, 20);
            this.txtDiasTranscurridos.TabIndex = 98;
            this.txtDiasTranscurridos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiasTranscurridos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(324, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 99;
            this.label1.Text = "Días Mes";
            // 
            // txtDiasMes
            // 
            this.txtDiasMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasMes.Location = new System.Drawing.Point(427, 14);
            this.txtDiasMes.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiasMes.Name = "txtDiasMes";
            this.txtDiasMes.ReadOnly = true;
            this.txtDiasMes.Size = new System.Drawing.Size(65, 20);
            this.txtDiasMes.TabIndex = 97;
            this.txtDiasMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiasMes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ventas.Properties.Resources.HALCONET;
            this.pictureBox1.Location = new System.Drawing.Point(197, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(117, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 96;
            this.pictureBox1.TabStop = false;
            // 
            // gridLineas
            // 
            this.gridLineas.AllowUserToAddRows = false;
            this.gridLineas.AllowUserToDeleteRows = false;
            this.gridLineas.AllowUserToResizeRows = false;
            this.gridLineas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridLineas.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridLineas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridLineas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLineas.EnableHeadersVisualStyles = false;
            this.gridLineas.Location = new System.Drawing.Point(197, 127);
            this.gridLineas.Margin = new System.Windows.Forms.Padding(5);
            this.gridLineas.Name = "gridLineas";
            this.gridLineas.ReadOnly = true;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridLineas.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gridLineas.Size = new System.Drawing.Size(767, 523);
            this.gridLineas.TabIndex = 106;
            this.gridLineas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLineas_CellContentClick);
            this.gridLineas.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.gridLineas_CellPainting);
            this.gridLineas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // BonoLineasMoradas
            // 
            this.AccessibleDescription = "Bono lineas halcon";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 664);
            this.Controls.Add(this.gridLineas);
            this.Controls.Add(this.gbTitulo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAvanceOptimo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDiasRestantes);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDiasTranscurridos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDiasMes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(986, 592);
            this.Name = "BonoLineasMoradas";
            this.Text = "HalcoNET - Bono lineas halcón";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_FormClosing);
            this.Load += new System.EventHandler(this.BonoLineasMoradas_Load);
            this.Shown += new System.EventHandler(this.form_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExcel)).EndInit();
            this.gbTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTotales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLineas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.CheckedListBox clbSucursal;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnPresentar;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.CheckedListBox clbVendedor;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.GroupBox gbTitulo;
        private System.Windows.Forms.DataGridView gridTotales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAvanceOptimo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDiasRestantes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDiasTranscurridos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDiasMes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView gridExcel;
        private System.Windows.Forms.DataGridView gridLineas;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}