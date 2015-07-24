namespace Cobranza
{
    partial class GarantiasCob
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GarantiasCob));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConsultar = new System.Windows.Forms.Button();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvRepartir = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripción = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtFacturado = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMontoGarantia = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepartir)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConsultar);
            this.groupBox1.Controls.Add(this.txtFactura);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cobranza Garantías";
            // 
            // txtConsultar
            // 
            this.txtConsultar.Location = new System.Drawing.Point(198, 30);
            this.txtConsultar.Name = "txtConsultar";
            this.txtConsultar.Size = new System.Drawing.Size(90, 25);
            this.txtConsultar.TabIndex = 7;
            this.txtConsultar.Text = "Buscar";
            this.txtConsultar.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(71, 32);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(121, 20);
            this.txtFactura.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Factura:";
            // 
            // dgvRepartir
            // 
            this.dgvRepartir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRepartir.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRepartir.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Descripción,
            this.Column3,
            this.Column4});
            this.dgvRepartir.Location = new System.Drawing.Point(13, 108);
            this.dgvRepartir.Name = "dgvRepartir";
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvRepartir.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRepartir.Size = new System.Drawing.Size(531, 237);
            this.dgvRepartir.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Artículo";
            this.Column1.Name = "Column1";
            // 
            // Descripción
            // 
            this.Descripción.HeaderText = "Descripción";
            this.Descripción.Name = "Descripción";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Cantidad\\r\\nGarantía";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Situacion";
            this.Column4.Name = "Column4";
            // 
            // txtFacturado
            // 
            this.txtFacturado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFacturado.Location = new System.Drawing.Point(84, 364);
            this.txtFacturado.Name = "txtFacturado";
            this.txtFacturado.ReadOnly = true;
            this.txtFacturado.Size = new System.Drawing.Size(95, 20);
            this.txtFacturado.TabIndex = 10;
            this.txtFacturado.Text = "$8,611.21";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 368);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Facturado:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(208, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Monto con garantía:";
            // 
            // txtMontoGarantia
            // 
            this.txtMontoGarantia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMontoGarantia.Location = new System.Drawing.Point(329, 364);
            this.txtMontoGarantia.Name = "txtMontoGarantia";
            this.txtMontoGarantia.ReadOnly = true;
            this.txtMontoGarantia.Size = new System.Drawing.Size(95, 20);
            this.txtMontoGarantia.TabIndex = 12;
            this.txtMontoGarantia.Text = "$6,117.72";
            // 
            // GarantiasCob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 412);
            this.Controls.Add(this.txtMontoGarantia);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFacturado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvRepartir);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GarantiasCob";
            this.Text = "HalcoNET - Garantias";
            this.Load += new System.EventHandler(this.Garantias_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepartir)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFacturado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button txtConsultar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripción;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMontoGarantia;
        private System.Windows.Forms.DataGridView dgvRepartir;
    }
}