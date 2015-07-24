namespace Cobranza
{
    partial class FacturasPendientes
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FacturasPendientes));
            this.dgvFacts = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtFecha = new System.Windows.Forms.DateTimePicker();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.clbSucursal = new System.Windows.Forms.CheckedListBox();
            this.btnReporte = new System.Windows.Forms.Button();
            this.btnCorcho = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chbRefacturaciones = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFacts
            // 
            this.dgvFacts.AllowUserToAddRows = false;
            this.dgvFacts.AllowUserToDeleteRows = false;
            this.dgvFacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            //this.dgvFacts.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            //this.dgvFacts.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ContextMenuHeading;
            this.dgvFacts.Location = new System.Drawing.Point(8, 77);
            this.dgvFacts.Name = "dgvFacts";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvFacts.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFacts.Size = new System.Drawing.Size(840, 478);
            this.dgvFacts.TabIndex = 6;
            this.dgvFacts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUSD_CellClick);
            this.dgvFacts.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFacts_CellEndEdit);
            this.dgvFacts.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvUSD_DataBindingComplete);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Fecha:";
            // 
            // dtFecha
            // 
            this.dtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFecha.Location = new System.Drawing.Point(67, 9);
            this.dtFecha.Name = "dtFecha";
            this.dtFecha.Size = new System.Drawing.Size(105, 20);
            this.dtFecha.TabIndex = 8;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConsultar.BackColor = System.Drawing.Color.Silver;
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConsultar.Image = global::Cobranza.Properties.Resources.search_2;
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConsultar.Location = new System.Drawing.Point(598, 9);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(59, 48);
            this.btnConsultar.TabIndex = 9;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnConsultar, "Consultar facuras para validación");
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Factura:";
            // 
            // txtFactura
            // 
            this.txtFactura.Enabled = false;
            this.txtFactura.Location = new System.Drawing.Point(67, 41);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(105, 20);
            this.txtFactura.TabIndex = 11;
            this.txtFactura.TextChanged += new System.EventHandler(this.txtFactura_TextChanged);
            // 
            // clbSucursal
            // 
            this.clbSucursal.CheckOnClick = true;
            this.clbSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbSucursal.FormattingEnabled = true;
            this.clbSucursal.Location = new System.Drawing.Point(178, 9);
            this.clbSucursal.Name = "clbSucursal";
            this.clbSucursal.Size = new System.Drawing.Size(161, 52);
            this.clbSucursal.TabIndex = 12;
            this.toolTip1.SetToolTip(this.clbSucursal, "Elige una sucursal");
            this.clbSucursal.Click += new System.EventHandler(this.clbSucursal_Click);
            // 
            // btnReporte
            // 
            this.btnReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReporte.Image = global::Cobranza.Properties.Resources.invoice1;
            this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReporte.Location = new System.Drawing.Point(662, 9);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(59, 48);
            this.btnReporte.TabIndex = 13;
            this.btnReporte.Text = "Reporte";
            this.btnReporte.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnReporte, "Reporte de facturas \r\npendientes de entregar a Crédito y Cobranza");
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // btnCorcho
            // 
            this.btnCorcho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCorcho.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCorcho.Image = global::Cobranza.Properties.Resources.invoice1;
            this.btnCorcho.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCorcho.Location = new System.Drawing.Point(726, 9);
            this.btnCorcho.Name = "btnCorcho";
            this.btnCorcho.Size = new System.Drawing.Size(59, 48);
            this.btnCorcho.TabIndex = 15;
            this.btnCorcho.Text = "Corcho";
            this.btnCorcho.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.btnCorcho, "Corcho");
            this.btnCorcho.UseVisualStyleBackColor = true;
            this.btnCorcho.Click += new System.EventHandler(this.btnCorcho_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image = global::Cobranza.Properties.Resources.print__1_;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(790, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 48);
            this.button1.TabIndex = 16;
            this.button1.Text = "Imprimir";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolTip1.SetToolTip(this.button1, "Imprimir");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chbRefacturaciones
            // 
            this.chbRefacturaciones.AutoSize = true;
            this.chbRefacturaciones.Location = new System.Drawing.Point(346, 43);
            this.chbRefacturaciones.Name = "chbRefacturaciones";
            this.chbRefacturaciones.Size = new System.Drawing.Size(104, 17);
            this.chbRefacturaciones.TabIndex = 17;
            this.chbRefacturaciones.Text = "Refacturaciones";
            this.chbRefacturaciones.UseVisualStyleBackColor = true;
            this.chbRefacturaciones.Visible = false;
            // 
            // FacturasPendientes
            // 
            this.AccessibleDescription = "Corcho";
            this.AccessibleName = "Corcho";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 567);
            this.Controls.Add(this.chbRefacturaciones);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCorcho);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.clbSucursal);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtFecha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvFacts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FacturasPendientes";
            this.Text = "Facturas pendientes por entregar a Crédito y Cobranza";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FacturasPendientes_FormClosing);
            this.Load += new System.EventHandler(this.FacturasPendientes_Load);
            this.Shown += new System.EventHandler(this.FacturasPendientes_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFacts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFacts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtFecha;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.CheckedListBox clbSucursal;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Button btnCorcho;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chbRefacturaciones;
    }
}