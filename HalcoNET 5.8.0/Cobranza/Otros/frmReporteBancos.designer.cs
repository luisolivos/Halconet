namespace Cobranza
{
    partial class frmReporteBancos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReporteBancos));
            this.btnUpdateTC = new System.Windows.Forms.Button();
            this.btnLoadReporte = new System.Windows.Forms.Button();
            this.txtTC = new System.Windows.Forms.TextBox();
            this.dgvMttoBancosReporte = new System.Windows.Forms.DataGridView();
            this.lblTC = new System.Windows.Forms.Label();
            this.dtpFiltroFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFiltroFecha = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMttoBancosReporte)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdateTC
            // 
            this.btnUpdateTC.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateTC.Image")));
            this.btnUpdateTC.Location = new System.Drawing.Point(665, 10);
            this.btnUpdateTC.Name = "btnUpdateTC";
            this.btnUpdateTC.Size = new System.Drawing.Size(25, 23);
            this.btnUpdateTC.TabIndex = 14;
            this.toolTip1.SetToolTip(this.btnUpdateTC, "Actualizar Tipo de Cambio");
            this.btnUpdateTC.UseVisualStyleBackColor = true;
            this.btnUpdateTC.Click += new System.EventHandler(this.btnUpdateTC_Click);
            // 
            // btnLoadReporte
            // 
            this.btnLoadReporte.Location = new System.Drawing.Point(212, 11);
            this.btnLoadReporte.Name = "btnLoadReporte";
            this.btnLoadReporte.Size = new System.Drawing.Size(108, 27);
            this.btnLoadReporte.TabIndex = 15;
            this.btnLoadReporte.Text = "Generar reporte";
            this.btnLoadReporte.UseVisualStyleBackColor = true;
            this.btnLoadReporte.Click += new System.EventHandler(this.btnLoadReporte_Click);
            // 
            // txtTC
            // 
            this.txtTC.Location = new System.Drawing.Point(527, 11);
            this.txtTC.Name = "txtTC";
            this.txtTC.Size = new System.Drawing.Size(132, 20);
            this.txtTC.TabIndex = 12;
            this.txtTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dgvMttoBancosReporte
            // 
            this.dgvMttoBancosReporte.AllowUserToAddRows = false;
            this.dgvMttoBancosReporte.AllowUserToDeleteRows = false;
            this.dgvMttoBancosReporte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMttoBancosReporte.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvMttoBancosReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMttoBancosReporte.EnableHeadersVisualStyles = false;
            this.dgvMttoBancosReporte.Location = new System.Drawing.Point(12, 84);
            this.dgvMttoBancosReporte.Name = "dgvMttoBancosReporte";
            this.dgvMttoBancosReporte.Size = new System.Drawing.Size(974, 291);
            this.dgvMttoBancosReporte.TabIndex = 13;
            this.dgvMttoBancosReporte.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvMttoBancosReporte_CellPainting);
            this.dgvMttoBancosReporte.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMttoBancosReporte_DataBindingComplete);
            // 
            // lblTC
            // 
            this.lblTC.AutoSize = true;
            this.lblTC.Location = new System.Drawing.Point(476, 13);
            this.lblTC.Name = "lblTC";
            this.lblTC.Size = new System.Drawing.Size(45, 13);
            this.lblTC.TabIndex = 9;
            this.lblTC.Text = "T.C    $:";
            // 
            // dtpFiltroFecha
            // 
            this.dtpFiltroFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroFecha.Location = new System.Drawing.Point(59, 14);
            this.dtpFiltroFecha.Name = "dtpFiltroFecha";
            this.dtpFiltroFecha.Size = new System.Drawing.Size(105, 20);
            this.dtpFiltroFecha.TabIndex = 11;
            // 
            // lblFiltroFecha
            // 
            this.lblFiltroFecha.AutoSize = true;
            this.lblFiltroFecha.Location = new System.Drawing.Point(13, 17);
            this.lblFiltroFecha.Name = "lblFiltroFecha";
            this.lblFiltroFecha.Size = new System.Drawing.Size(40, 13);
            this.lblFiltroFecha.TabIndex = 10;
            this.lblFiltroFecha.Text = "Fecha:";
            // 
            // button1
            // 
            this.button1.Image = global::Cobranza.Properties.Resources.undo1;
            this.button1.Location = new System.Drawing.Point(696, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 23);
            this.button1.TabIndex = 16;
            this.toolTip1.SetToolTip(this.button1, "Regresar al valor original");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmReporteBancos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 387);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnUpdateTC);
            this.Controls.Add(this.btnLoadReporte);
            this.Controls.Add(this.txtTC);
            this.Controls.Add(this.dgvMttoBancosReporte);
            this.Controls.Add(this.lblTC);
            this.Controls.Add(this.dtpFiltroFecha);
            this.Controls.Add(this.lblFiltroFecha);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmReporteBancos";
            this.Text = "Reporte de créditos bancarios";
            this.Load += new System.EventHandler(this.frmReporteBancos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMttoBancosReporte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateTC;
        private System.Windows.Forms.Button btnLoadReporte;
        private System.Windows.Forms.TextBox txtTC;
        private System.Windows.Forms.DataGridView dgvMttoBancosReporte;
        private System.Windows.Forms.Label lblTC;
        private System.Windows.Forms.DateTimePicker dtpFiltroFecha;
        private System.Windows.Forms.Label lblFiltroFecha;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}