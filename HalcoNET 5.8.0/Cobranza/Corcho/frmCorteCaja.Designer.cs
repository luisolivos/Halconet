namespace Cobranza.Corcho
{
    partial class frmCorteCaja
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorteCaja));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dtpCaptura = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.grpCaptura = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbSucursal1 = new System.Windows.Forms.ComboBox();
            this.grpReporte1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.cbSucursal = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.cbCobranza = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.grpReporte2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpReporteHasta = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpReporteDesde = new System.Windows.Forms.DateTimePicker();
            this.button5 = new System.Windows.Forms.Button();
            this.colorDialogE = new System.Windows.Forms.ColorDialog();
            this.btnEncabecazo1 = new System.Windows.Forms.Button();
            this.btnTitulo2 = new System.Windows.Forms.Button();
            this.btnPDF = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.grpCaptura.SuspendLayout();
            this.grpReporte1.SuspendLayout();
            this.grpReporte2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 490);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStatus
            // 
            this.toolStatus.Name = "toolStatus";
            this.toolStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // dtpCaptura
            // 
            this.dtpCaptura.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCaptura.Location = new System.Drawing.Point(106, 21);
            this.dtpCaptura.Name = "dtpCaptura";
            this.dtpCaptura.Size = new System.Drawing.Size(100, 20);
            this.dtpCaptura.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fecha de captura:";
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.AllowUserToResizeRows = false;
            this.dgvDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDatos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgvDatos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(84)))), ((int)(((byte)(106)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.EnableHeadersVisualStyles = false;
            this.dgvDatos.Location = new System.Drawing.Point(16, 84);
            this.dgvDatos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDatos.Name = "dgvDatos";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.dgvDatos.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDatos.Size = new System.Drawing.Size(805, 361);
            this.dgvDatos.TabIndex = 91;
            this.dgvDatos.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridFacturas_CellBeginEdit);
            this.dgvDatos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridFacturas_CellEndEdit);
            this.dgvDatos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDatos_DataBindingComplete);
            this.dgvDatos.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.gridFacturas_DefaultValuesNeeded);
            this.dgvDatos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridFacturas_EditingControlShowing);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(586, 452);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 35);
            this.btnSave.TabIndex = 92;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImprimir.Location = new System.Drawing.Point(666, 452);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 35);
            this.btnImprimir.TabIndex = 93;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(264, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 27);
            this.button2.TabIndex = 94;
            this.button2.Text = "Consultar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // grpCaptura
            // 
            this.grpCaptura.Controls.Add(this.label8);
            this.grpCaptura.Controls.Add(this.cbSucursal1);
            this.grpCaptura.Controls.Add(this.label1);
            this.grpCaptura.Controls.Add(this.button2);
            this.grpCaptura.Controls.Add(this.dtpCaptura);
            this.grpCaptura.Location = new System.Drawing.Point(16, 12);
            this.grpCaptura.Name = "grpCaptura";
            this.grpCaptura.Size = new System.Drawing.Size(398, 65);
            this.grpCaptura.TabIndex = 95;
            this.grpCaptura.TabStop = false;
            this.grpCaptura.Text = "Captura";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 101;
            this.label8.Text = "Sucursal:";
            this.label8.Visible = false;
            // 
            // cbSucursal1
            // 
            this.cbSucursal1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSucursal1.FormattingEnabled = true;
            this.cbSucursal1.Location = new System.Drawing.Point(106, 41);
            this.cbSucursal1.Name = "cbSucursal1";
            this.cbSucursal1.Size = new System.Drawing.Size(121, 21);
            this.cbSucursal1.TabIndex = 100;
            this.cbSucursal1.Visible = false;
            // 
            // grpReporte1
            // 
            this.grpReporte1.Controls.Add(this.label6);
            this.grpReporte1.Controls.Add(this.label3);
            this.grpReporte1.Controls.Add(this.dtpHasta);
            this.grpReporte1.Controls.Add(this.cbSucursal);
            this.grpReporte1.Controls.Add(this.label2);
            this.grpReporte1.Controls.Add(this.button3);
            this.grpReporte1.Controls.Add(this.dtpDesde);
            this.grpReporte1.Location = new System.Drawing.Point(16, 12);
            this.grpReporte1.Name = "grpReporte1";
            this.grpReporte1.Size = new System.Drawing.Size(501, 65);
            this.grpReporte1.TabIndex = 96;
            this.grpReporte1.TabStop = false;
            this.grpReporte1.Text = "Reporte";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 99;
            this.label6.Text = "Sucursal:";
            this.label6.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 96;
            this.label3.Text = "Hasta:";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(64, 39);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(100, 20);
            this.dtpHasta.TabIndex = 95;
            // 
            // cbSucursal
            // 
            this.cbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSucursal.FormattingEnabled = true;
            this.cbSucursal.Location = new System.Drawing.Point(222, 19);
            this.cbSucursal.Name = "cbSucursal";
            this.cbSucursal.Size = new System.Drawing.Size(121, 21);
            this.cbSucursal.TabIndex = 98;
            this.cbSucursal.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Desde:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(375, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(85, 27);
            this.button3.TabIndex = 94;
            this.button3.Text = "Consultar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(64, 21);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(100, 20);
            this.dtpDesde.TabIndex = 1;
            // 
            // cbCobranza
            // 
            this.cbCobranza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCobranza.FormattingEnabled = true;
            this.cbCobranza.Location = new System.Drawing.Point(101, 40);
            this.cbCobranza.Name = "cbCobranza";
            this.cbCobranza.Size = new System.Drawing.Size(140, 21);
            this.cbCobranza.TabIndex = 97;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(449, 18);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(85, 27);
            this.button4.TabIndex = 97;
            this.button4.Text = "Consultar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // grpReporte2
            // 
            this.grpReporte2.Controls.Add(this.label7);
            this.grpReporte2.Controls.Add(this.label4);
            this.grpReporte2.Controls.Add(this.dtpReporteHasta);
            this.grpReporte2.Controls.Add(this.cbCobranza);
            this.grpReporte2.Controls.Add(this.button4);
            this.grpReporte2.Controls.Add(this.label5);
            this.grpReporte2.Controls.Add(this.dtpReporteDesde);
            this.grpReporte2.Location = new System.Drawing.Point(16, 12);
            this.grpReporte2.Name = "grpReporte2";
            this.grpReporte2.Size = new System.Drawing.Size(577, 65);
            this.grpReporte2.TabIndex = 97;
            this.grpReporte2.TabStop = false;
            this.grpReporte2.Text = "Reporte";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 13);
            this.label7.TabIndex = 100;
            this.label7.Text = "Jefa de Cobranza:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 96;
            this.label4.Text = "Fecha final:";
            // 
            // dtpReporteHasta
            // 
            this.dtpReporteHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReporteHasta.Location = new System.Drawing.Point(311, 19);
            this.dtpReporteHasta.Name = "dtpReporteHasta";
            this.dtpReporteHasta.Size = new System.Drawing.Size(100, 20);
            this.dtpReporteHasta.TabIndex = 95;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Fecha inical:";
            // 
            // dtpReporteDesde
            // 
            this.dtpReporteDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpReporteDesde.Location = new System.Drawing.Point(101, 19);
            this.dtpReporteDesde.Name = "dtpReporteDesde";
            this.dtpReporteDesde.Size = new System.Drawing.Size(100, 20);
            this.dtpReporteDesde.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.Location = new System.Drawing.Point(746, 452);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 35);
            this.button5.TabIndex = 98;
            this.button5.Text = "Exportar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // btnEncabecazo1
            // 
            this.btnEncabecazo1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEncabecazo1.BackColor = System.Drawing.Color.LightGray;
            this.btnEncabecazo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEncabecazo1.Location = new System.Drawing.Point(13, 464);
            this.btnEncabecazo1.Name = "btnEncabecazo1";
            this.btnEncabecazo1.Size = new System.Drawing.Size(47, 23);
            this.btnEncabecazo1.TabIndex = 99;
            this.btnEncabecazo1.Text = "Titulo 1";
            this.btnEncabecazo1.UseVisualStyleBackColor = false;
            this.btnEncabecazo1.Click += new System.EventHandler(this.btnEncabecazo1_Click);
            // 
            // btnTitulo2
            // 
            this.btnTitulo2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTitulo2.BackColor = System.Drawing.Color.DarkGray;
            this.btnTitulo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTitulo2.Location = new System.Drawing.Point(66, 464);
            this.btnTitulo2.Name = "btnTitulo2";
            this.btnTitulo2.Size = new System.Drawing.Size(50, 23);
            this.btnTitulo2.TabIndex = 100;
            this.btnTitulo2.Text = "Titulo 2";
            this.btnTitulo2.UseVisualStyleBackColor = false;
            this.btnTitulo2.Click += new System.EventHandler(this.btnTitulo2_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPDF.Location = new System.Drawing.Point(505, 452);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(75, 35);
            this.btnPDF.TabIndex = 101;
            this.btnPDF.Text = "Cargar PDF";
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // frmCorteCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 512);
            this.Controls.Add(this.grpReporte1);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.btnTitulo2);
            this.Controls.Add(this.btnEncabecazo1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpCaptura);
            this.Controls.Add(this.grpReporte2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCorteCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Ingresos";
            this.Load += new System.EventHandler(this.frmIngresos_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.grpCaptura.ResumeLayout(false);
            this.grpCaptura.PerformLayout();
            this.grpReporte1.ResumeLayout(false);
            this.grpReporte1.PerformLayout();
            this.grpReporte2.ResumeLayout(false);
            this.grpReporte2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStatus;
        private System.Windows.Forms.DateTimePicker dtpCaptura;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox grpCaptura;
        private System.Windows.Forms.GroupBox grpReporte1;
        private System.Windows.Forms.ComboBox cbSucursal;
        private System.Windows.Forms.ComboBox cbCobranza;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox grpReporte2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpReporteHasta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpReporteDesde;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ColorDialog colorDialogE;
        private System.Windows.Forms.Button btnEncabecazo1;
        private System.Windows.Forms.Button btnTitulo2;
        private System.Windows.Forms.Button btnPDF;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbSucursal1;
    }
}