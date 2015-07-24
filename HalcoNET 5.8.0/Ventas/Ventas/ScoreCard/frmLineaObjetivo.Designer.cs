namespace Ventas.Ventas.ScoreCard
{
    partial class frmLineaObjetivo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLineaObjetivo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExportar = new System.Windows.Forms.Button();
            this.gridLineas = new System.Windows.Forms.DataGridView();
            this.gridTotales = new System.Windows.Forms.DataGridView();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.lblFechaInicial = new System.Windows.Forms.Label();
            this.gbTitulo = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAvanceOptimo = new System.Windows.Forms.TextBox();
            this.btnPresentar = new System.Windows.Forms.Button();
            this.txtDiasRestantes = new System.Windows.Forms.TextBox();
            this.txtDiasTranscurridos = new System.Windows.Forms.TextBox();
            this.clbVendedor = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.txtDiasMes = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.clbSucursal = new System.Windows.Forms.CheckedListBox();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbLinea = new System.Windows.Forms.ComboBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.gridLineas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTotales)).BeginInit();
            this.gbTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExportar
            // 
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportar.Image = ((System.Drawing.Image)(resources.GetObject("btnExportar.Image")));
            this.btnExportar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportar.Location = new System.Drawing.Point(72, 391);
            this.btnExportar.Margin = new System.Windows.Forms.Padding(5);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnExportar.Size = new System.Drawing.Size(118, 49);
            this.btnExportar.TabIndex = 27;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            this.btnExportar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridLineas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridLineas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLineas.EnableHeadersVisualStyles = false;
            this.gridLineas.Location = new System.Drawing.Point(214, 140);
            this.gridLineas.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.gridLineas.Name = "gridLineas";
            this.gridLineas.ReadOnly = true;
            this.gridLineas.Size = new System.Drawing.Size(949, 558);
            this.gridLineas.TabIndex = 107;
            this.gridLineas.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridLineas_DataBindingComplete);
            this.gridLineas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // gridTotales
            // 
            this.gridTotales.AllowUserToAddRows = false;
            this.gridTotales.AllowUserToDeleteRows = false;
            this.gridTotales.AllowUserToResizeRows = false;
            this.gridTotales.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridTotales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTotales.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTotales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTotales.EnableHeadersVisualStyles = false;
            this.gridTotales.Location = new System.Drawing.Point(5, 20);
            this.gridTotales.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.gridTotales.Name = "gridTotales";
            this.gridTotales.ReadOnly = true;
            this.gridTotales.Size = new System.Drawing.Size(521, 85);
            this.gridTotales.TabIndex = 91;
            this.gridTotales.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridTotales_DataBindingComplete);
            this.gridTotales.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // lblFechaInicial
            // 
            this.lblFechaInicial.AutoSize = true;
            this.lblFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicial.Location = new System.Drawing.Point(11, 68);
            this.lblFechaInicial.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFechaInicial.Name = "lblFechaInicial";
            this.lblFechaInicial.Size = new System.Drawing.Size(70, 13);
            this.lblFechaInicial.TabIndex = 3;
            this.lblFechaInicial.Text = "Fecha Inicial:";
            // 
            // gbTitulo
            // 
            this.gbTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTitulo.AutoSize = true;
            this.gbTitulo.Controls.Add(this.gridTotales);
            this.gbTitulo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.gbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbTitulo.Location = new System.Drawing.Point(570, 0);
            this.gbTitulo.Margin = new System.Windows.Forms.Padding(5);
            this.gbTitulo.Name = "gbTitulo";
            this.gbTitulo.Padding = new System.Windows.Forms.Padding(5);
            this.gbTitulo.Size = new System.Drawing.Size(531, 110);
            this.gbTitulo.TabIndex = 106;
            this.gbTitulo.TabStop = false;
            this.gbTitulo.Text = "Subtotales";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(341, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 105;
            this.label2.Text = "Avance Optimo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(341, 65);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 103;
            this.label9.Text = "Días Restantes";
            // 
            // txtAvanceOptimo
            // 
            this.txtAvanceOptimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvanceOptimo.Location = new System.Drawing.Point(475, 86);
            this.txtAvanceOptimo.Margin = new System.Windows.Forms.Padding(5);
            this.txtAvanceOptimo.Name = "txtAvanceOptimo";
            this.txtAvanceOptimo.ReadOnly = true;
            this.txtAvanceOptimo.Size = new System.Drawing.Size(85, 20);
            this.txtAvanceOptimo.TabIndex = 104;
            this.txtAvanceOptimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAvanceOptimo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // btnPresentar
            // 
            this.btnPresentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPresentar.Image = ((System.Drawing.Image)(resources.GetObject("btnPresentar.Image")));
            this.btnPresentar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPresentar.Location = new System.Drawing.Point(72, 316);
            this.btnPresentar.Margin = new System.Windows.Forms.Padding(5);
            this.btnPresentar.Name = "btnPresentar";
            this.btnPresentar.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnPresentar.Size = new System.Drawing.Size(118, 49);
            this.btnPresentar.TabIndex = 2;
            this.btnPresentar.Text = "Buscar";
            this.btnPresentar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPresentar.UseVisualStyleBackColor = true;
            this.btnPresentar.Click += new System.EventHandler(this.btnPresentar_Click);
            this.btnPresentar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // txtDiasRestantes
            // 
            this.txtDiasRestantes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasRestantes.Location = new System.Drawing.Point(475, 60);
            this.txtDiasRestantes.Margin = new System.Windows.Forms.Padding(5);
            this.txtDiasRestantes.Name = "txtDiasRestantes";
            this.txtDiasRestantes.ReadOnly = true;
            this.txtDiasRestantes.Size = new System.Drawing.Size(85, 20);
            this.txtDiasRestantes.TabIndex = 102;
            this.txtDiasRestantes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiasRestantes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // txtDiasTranscurridos
            // 
            this.txtDiasTranscurridos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasTranscurridos.Location = new System.Drawing.Point(475, 34);
            this.txtDiasTranscurridos.Margin = new System.Windows.Forms.Padding(5);
            this.txtDiasTranscurridos.Name = "txtDiasTranscurridos";
            this.txtDiasTranscurridos.ReadOnly = true;
            this.txtDiasTranscurridos.Size = new System.Drawing.Size(85, 20);
            this.txtDiasTranscurridos.TabIndex = 99;
            this.txtDiasTranscurridos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiasTranscurridos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // clbVendedor
            // 
            this.clbVendedor.CheckOnClick = true;
            this.clbVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbVendedor.FormattingEnabled = true;
            this.clbVendedor.Location = new System.Drawing.Point(11, 242);
            this.clbVendedor.Margin = new System.Windows.Forms.Padding(5);
            this.clbVendedor.Name = "clbVendedor";
            this.clbVendedor.Size = new System.Drawing.Size(179, 64);
            this.clbVendedor.TabIndex = 10;
            this.clbVendedor.Click += new System.EventHandler(this.clbVendedor_Click);
            this.clbVendedor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(341, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 101;
            this.label4.Text = "Días Transcurridos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(341, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 100;
            this.label1.Text = "Días Mes";
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucursal.Location = new System.Drawing.Point(11, 127);
            this.lblSucursal.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(51, 13);
            this.lblSucursal.TabIndex = 29;
            this.lblSucursal.Text = "Sucursal:";
            // 
            // txtDiasMes
            // 
            this.txtDiasMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasMes.Location = new System.Drawing.Point(475, 9);
            this.txtDiasMes.Margin = new System.Windows.Forms.Padding(5);
            this.txtDiasMes.Name = "txtDiasMes";
            this.txtDiasMes.ReadOnly = true;
            this.txtDiasMes.Size = new System.Drawing.Size(85, 20);
            this.txtDiasMes.TabIndex = 98;
            this.txtDiasMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDiasMes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Ventas.Properties.Resources.HALCONET;
            this.pictureBox1.Location = new System.Drawing.Point(214, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(117, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 97;
            this.pictureBox1.TabStop = false;
            // 
            // clbSucursal
            // 
            this.clbSucursal.CheckOnClick = true;
            this.clbSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbSucursal.FormattingEnabled = true;
            this.clbSucursal.Location = new System.Drawing.Point(11, 145);
            this.clbSucursal.Margin = new System.Windows.Forms.Padding(5);
            this.clbSucursal.Name = "clbSucursal";
            this.clbSucursal.Size = new System.Drawing.Size(179, 64);
            this.clbSucursal.TabIndex = 8;
            this.clbSucursal.Click += new System.EventHandler(this.clbSucursal_Click);
            this.clbSucursal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVendedor.Location = new System.Drawing.Point(11, 223);
            this.lblVendedor.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(56, 13);
            this.lblVendedor.TabIndex = 14;
            this.lblVendedor.Text = "Vendedor:";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.cbLinea);
            this.groupBox1.Controls.Add(this.dtpFecha);
            this.groupBox1.Controls.Add(this.lblSucursal);
            this.groupBox1.Controls.Add(this.clbSucursal);
            this.groupBox1.Controls.Add(this.btnExportar);
            this.groupBox1.Controls.Add(this.btnPresentar);
            this.groupBox1.Controls.Add(this.lblVendedor);
            this.groupBox1.Controls.Add(this.clbVendedor);
            this.groupBox1.Controls.Add(this.lblFechaInicial);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(200, 713);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Linea objetivo";
            // 
            // cbLinea
            // 
            this.cbLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLinea.FormattingEnabled = true;
            this.cbLinea.Location = new System.Drawing.Point(11, 28);
            this.cbLinea.Name = "cbLinea";
            this.cbLinea.Size = new System.Drawing.Size(163, 24);
            this.cbLinea.TabIndex = 96;
            // 
            // dtpFecha
            // 
            this.dtpFecha.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(11, 89);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(124, 20);
            this.dtpFecha.TabIndex = 95;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged);
            this.dtpFecha.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            // 
            // LineaObjetivo
            // 
            this.AccessibleDescription = "Linea objetivo";
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 713);
            this.Controls.Add(this.gridLineas);
            this.Controls.Add(this.gbTitulo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAvanceOptimo);
            this.Controls.Add(this.txtDiasRestantes);
            this.Controls.Add(this.txtDiasTranscurridos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDiasMes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LineaObjetivo";
            this.Text = "Linea Objetivo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form_FormClosing);
            this.Load += new System.EventHandler(this.LineaObjetivo_Load);
            this.Shown += new System.EventHandler(this.form_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.U_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.gridLineas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTotales)).EndInit();
            this.gbTitulo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.DataGridView gridLineas;
        private System.Windows.Forms.DataGridView gridTotales;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Label lblFechaInicial;
        private System.Windows.Forms.GroupBox gbTitulo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAvanceOptimo;
        private System.Windows.Forms.Button btnPresentar;
        private System.Windows.Forms.TextBox txtDiasRestantes;
        private System.Windows.Forms.TextBox txtDiasTranscurridos;
        private System.Windows.Forms.CheckedListBox clbVendedor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.TextBox txtDiasMes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckedListBox clbSucursal;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.ComboBox cbLinea;
    }
}