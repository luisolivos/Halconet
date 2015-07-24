namespace Ventas.Ventas
{
    partial class frmAnalisisVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnalisisVenta));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpbPorPzMx = new System.Windows.Forms.GroupBox();
            this.rdbTotalMoneda = new System.Windows.Forms.RadioButton();
            this.rdbTotalPiezas = new System.Windows.Forms.RadioButton();
            this.txtShowTotalM = new System.Windows.Forms.TextBox();
            this.txtShowTotalPz = new System.Windows.Forms.TextBox();
            this.lblShowTotalM = new System.Windows.Forms.Label();
            this.lblShowTotalPz = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.clbLineas = new System.Windows.Forms.CheckedListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtDesde = new System.Windows.Forms.DateTimePicker();
            this.dtHasta = new System.Windows.Forms.DateTimePicker();
            this.splitContainerGrids = new System.Windows.Forms.SplitContainer();
            this.dgvVenta = new System.Windows.Forms.DataGridView();
            this.dgvDesglose = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.grpbPorPzMx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGrids)).BeginInit();
            this.splitContainerGrids.Panel1.SuspendLayout();
            this.splitContainerGrids.Panel2.SuspendLayout();
            this.splitContainerGrids.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesglose)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grpbPorPzMx);
            this.groupBox1.Controls.Add(this.txtShowTotalM);
            this.groupBox1.Controls.Add(this.txtShowTotalPz);
            this.groupBox1.Controls.Add(this.lblShowTotalM);
            this.groupBox1.Controls.Add(this.lblShowTotalPz);
            this.groupBox1.Controls.Add(this.lblTotal);
            this.groupBox1.Controls.Add(this.clbLineas);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnConsultar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtDesde);
            this.groupBox1.Controls.Add(this.dtHasta);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 165);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Análisis de venta";
            // 
            // grpbPorPzMx
            // 
            this.grpbPorPzMx.Controls.Add(this.rdbTotalMoneda);
            this.grpbPorPzMx.Controls.Add(this.rdbTotalPiezas);
            this.grpbPorPzMx.Location = new System.Drawing.Point(197, 60);
            this.grpbPorPzMx.Name = "grpbPorPzMx";
            this.grpbPorPzMx.Size = new System.Drawing.Size(166, 80);
            this.grpbPorPzMx.TabIndex = 19;
            this.grpbPorPzMx.TabStop = false;
            this.grpbPorPzMx.Text = "Total Por:";
            // 
            // rdbTotalMoneda
            // 
            this.rdbTotalMoneda.AutoSize = true;
            this.rdbTotalMoneda.Location = new System.Drawing.Point(15, 44);
            this.rdbTotalMoneda.Name = "rdbTotalMoneda";
            this.rdbTotalMoneda.Size = new System.Drawing.Size(64, 17);
            this.rdbTotalMoneda.TabIndex = 1;
            this.rdbTotalMoneda.Text = "Moneda";
            this.rdbTotalMoneda.UseVisualStyleBackColor = true;
            // 
            // rdbTotalPiezas
            // 
            this.rdbTotalPiezas.AutoSize = true;
            this.rdbTotalPiezas.Checked = true;
            this.rdbTotalPiezas.Location = new System.Drawing.Point(15, 19);
            this.rdbTotalPiezas.Name = "rdbTotalPiezas";
            this.rdbTotalPiezas.Size = new System.Drawing.Size(56, 17);
            this.rdbTotalPiezas.TabIndex = 0;
            this.rdbTotalPiezas.TabStop = true;
            this.rdbTotalPiezas.Text = "Piezas";
            this.rdbTotalPiezas.UseVisualStyleBackColor = true;
            // 
            // txtShowTotalM
            // 
            this.txtShowTotalM.Enabled = false;
            this.txtShowTotalM.Location = new System.Drawing.Point(445, 98);
            this.txtShowTotalM.Name = "txtShowTotalM";
            this.txtShowTotalM.Size = new System.Drawing.Size(100, 20);
            this.txtShowTotalM.TabIndex = 18;
            this.txtShowTotalM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShowTotalM.Visible = false;
            // 
            // txtShowTotalPz
            // 
            this.txtShowTotalPz.Enabled = false;
            this.txtShowTotalPz.Location = new System.Drawing.Point(445, 71);
            this.txtShowTotalPz.Name = "txtShowTotalPz";
            this.txtShowTotalPz.Size = new System.Drawing.Size(100, 20);
            this.txtShowTotalPz.TabIndex = 17;
            this.txtShowTotalPz.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShowTotalPz.Visible = false;
            // 
            // lblShowTotalM
            // 
            this.lblShowTotalM.AutoSize = true;
            this.lblShowTotalM.Location = new System.Drawing.Point(389, 101);
            this.lblShowTotalM.Name = "lblShowTotalM";
            this.lblShowTotalM.Size = new System.Drawing.Size(49, 13);
            this.lblShowTotalM.TabIndex = 16;
            this.lblShowTotalM.Text = "Total ($):";
            this.lblShowTotalM.Visible = false;
            // 
            // lblShowTotalPz
            // 
            this.lblShowTotalPz.AutoSize = true;
            this.lblShowTotalPz.Location = new System.Drawing.Point(389, 73);
            this.lblShowTotalPz.Name = "lblShowTotalPz";
            this.lblShowTotalPz.Size = new System.Drawing.Size(55, 13);
            this.lblShowTotalPz.TabIndex = 15;
            this.lblShowTotalPz.Text = "Total (Pz):";
            this.lblShowTotalPz.Visible = false;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(389, 115);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 13);
            this.lblTotal.TabIndex = 14;
            // 
            // clbLineas
            // 
            this.clbLineas.FormattingEnabled = true;
            this.clbLineas.Location = new System.Drawing.Point(24, 71);
            this.clbLineas.Name = "clbLineas";
            this.clbLineas.Size = new System.Drawing.Size(157, 64);
            this.clbLineas.TabIndex = 12;
            this.clbLineas.Click += new System.EventHandler(this.clb_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Líneas:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(388, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(157, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Artículo:";
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(601, 126);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 6;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Desde:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Hasta:";
            // 
            // dtDesde
            // 
            this.dtDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDesde.Location = new System.Drawing.Point(83, 24);
            this.dtDesde.Name = "dtDesde";
            this.dtDesde.Size = new System.Drawing.Size(94, 20);
            this.dtDesde.TabIndex = 0;
            // 
            // dtHasta
            // 
            this.dtHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtHasta.Location = new System.Drawing.Point(227, 24);
            this.dtHasta.Name = "dtHasta";
            this.dtHasta.Size = new System.Drawing.Size(94, 20);
            this.dtHasta.TabIndex = 1;
            // 
            // splitContainerGrids
            // 
            this.splitContainerGrids.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerGrids.Location = new System.Drawing.Point(12, 183);
            this.splitContainerGrids.Name = "splitContainerGrids";
            this.splitContainerGrids.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerGrids.Panel1
            // 
            this.splitContainerGrids.Panel1.Controls.Add(this.dgvVenta);
            // 
            // splitContainerGrids.Panel2
            // 
            this.splitContainerGrids.Panel2.Controls.Add(this.dgvDesglose);
            this.splitContainerGrids.Panel2MinSize = 55;
            this.splitContainerGrids.Size = new System.Drawing.Size(905, 479);
            this.splitContainerGrids.SplitterDistance = 340;
            this.splitContainerGrids.TabIndex = 15;
            // 
            // dgvVenta
            // 
            this.dgvVenta.AllowUserToAddRows = false;
            this.dgvVenta.AllowUserToDeleteRows = false;
            this.dgvVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVenta.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvVenta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvVenta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVenta.Location = new System.Drawing.Point(3, 3);
            this.dgvVenta.Name = "dgvVenta";
            this.dgvVenta.ReadOnly = true;
            this.dgvVenta.RowHeadersWidth = 20;
            this.dgvVenta.Size = new System.Drawing.Size(899, 334);
            this.dgvVenta.TabIndex = 14;
            this.dgvVenta.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvVenta_DataBindingComplete);
            this.dgvVenta.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVenta_RowEnter);
            // 
            // dgvDesglose
            // 
            this.dgvDesglose.AllowUserToAddRows = false;
            this.dgvDesglose.AllowUserToDeleteRows = false;
            this.dgvDesglose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDesglose.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDesglose.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDesglose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDesglose.Location = new System.Drawing.Point(3, 3);
            this.dgvDesglose.Name = "dgvDesglose";
            this.dgvDesglose.ReadOnly = true;
            this.dgvDesglose.RowHeadersWidth = 20;
            this.dgvDesglose.Size = new System.Drawing.Size(899, 129);
            this.dgvDesglose.TabIndex = 15;
            this.dgvDesglose.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDesglose_CellDoubleClick);
            this.dgvDesglose.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDesglose_DataBindingComplete);
            // 
            // AnalisisVenta
            // 
            this.AcceptButton = this.btnConsultar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 674);
            this.Controls.Add(this.splitContainerGrids);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnalisisVenta";
            this.Text = "Análisis de venta";
            this.Load += new System.EventHandler(this.AnalisisVenta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpbPorPzMx.ResumeLayout(false);
            this.grpbPorPzMx.PerformLayout();
            this.splitContainerGrids.Panel1.ResumeLayout(false);
            this.splitContainerGrids.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGrids)).EndInit();
            this.splitContainerGrids.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesglose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtDesde;
        private System.Windows.Forms.DateTimePicker dtHasta;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox clbLineas;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblShowTotalM;
        private System.Windows.Forms.Label lblShowTotalPz;
        private System.Windows.Forms.TextBox txtShowTotalM;
        private System.Windows.Forms.TextBox txtShowTotalPz;
        private System.Windows.Forms.GroupBox grpbPorPzMx;
        private System.Windows.Forms.RadioButton rdbTotalMoneda;
        private System.Windows.Forms.RadioButton rdbTotalPiezas;
        private System.Windows.Forms.SplitContainer splitContainerGrids;
        private System.Windows.Forms.DataGridView dgvVenta;
        private System.Windows.Forms.DataGridView dgvDesglose;
    }
}