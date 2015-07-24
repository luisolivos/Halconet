namespace Presupuesto.Requisicion
{
    partial class ctrlGeneral
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.grpEspecifico = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIVA = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.grpBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRFC1 = new System.Windows.Forms.TextBox();
            this.cbProveedor1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRFC2 = new System.Windows.Forms.TextBox();
            this.cbProveedor2 = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRFC3 = new System.Windows.Forms.TextBox();
            this.cbProveedor3 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.grpEspecifico.SuspendLayout();
            this.grpBox1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Location = new System.Drawing.Point(4, 85);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvDetalle.Size = new System.Drawing.Size(1125, 242);
            this.dgvDetalle.TabIndex = 0;
            this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellContentClick);
            this.dgvDetalle.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellContentClick);
            this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellEndEdit);
            this.dgvDetalle.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDetalle_DataBindingComplete);
            this.dgvDetalle.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvDetalle_DefaultValuesNeeded);
            // 
            // grpEspecifico
            // 
            this.grpEspecifico.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEspecifico.Controls.Add(this.label1);
            this.grpEspecifico.Controls.Add(this.textBox1);
            this.grpEspecifico.Controls.Add(this.label11);
            this.grpEspecifico.Controls.Add(this.txtTotal);
            this.grpEspecifico.Controls.Add(this.label9);
            this.grpEspecifico.Controls.Add(this.txtIVA);
            this.grpEspecifico.Controls.Add(this.label10);
            this.grpEspecifico.Controls.Add(this.txtSubtotal);
            this.grpEspecifico.Location = new System.Drawing.Point(746, 351);
            this.grpEspecifico.Name = "grpEspecifico";
            this.grpEspecifico.Size = new System.Drawing.Size(388, 116);
            this.grpEspecifico.TabIndex = 14;
            this.grpEspecifico.TabStop = false;
            this.grpEspecifico.Text = "Totales";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(10, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Gasto total utilizado las sugerencias de usuarios:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(247, 86);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(128, 20);
            this.textBox1.TabIndex = 20;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label11.Location = new System.Drawing.Point(133, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(112, 13);
            this.label11.TabIndex = 19;
            this.label11.Text = "Total importe con IVA:";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(247, 65);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(128, 20);
            this.txtTotal.TabIndex = 18;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label9.Location = new System.Drawing.Point(221, 46);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "IVA";
            // 
            // txtIVA
            // 
            this.txtIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIVA.Location = new System.Drawing.Point(247, 42);
            this.txtIVA.Name = "txtIVA";
            this.txtIVA.ReadOnly = true;
            this.txtIVA.Size = new System.Drawing.Size(128, 20);
            this.txtIVA.TabIndex = 16;
            this.txtIVA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label10.Location = new System.Drawing.Point(51, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(194, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Importe sin IVA con los mejores precios:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubtotal.Location = new System.Drawing.Point(247, 19);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(128, 20);
            this.txtSubtotal.TabIndex = 14;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpBox1
            // 
            this.grpBox1.Controls.Add(this.label2);
            this.grpBox1.Controls.Add(this.label3);
            this.grpBox1.Controls.Add(this.txtRFC1);
            this.grpBox1.Controls.Add(this.cbProveedor1);
            this.grpBox1.Location = new System.Drawing.Point(318, 9);
            this.grpBox1.Name = "grpBox1";
            this.grpBox1.Size = new System.Drawing.Size(262, 70);
            this.grpBox1.TabIndex = 15;
            this.grpBox1.TabStop = false;
            this.grpBox1.Text = "Proveedor 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label2.Location = new System.Drawing.Point(30, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "RFC:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label3.Location = new System.Drawing.Point(14, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nombre:";
            // 
            // txtRFC1
            // 
            this.txtRFC1.Location = new System.Drawing.Point(67, 44);
            this.txtRFC1.Name = "txtRFC1";
            this.txtRFC1.ReadOnly = true;
            this.txtRFC1.Size = new System.Drawing.Size(122, 20);
            this.txtRFC1.TabIndex = 1;
            // 
            // cbProveedor1
            // 
            this.cbProveedor1.FormattingEnabled = true;
            this.cbProveedor1.Location = new System.Drawing.Point(67, 19);
            this.cbProveedor1.Name = "cbProveedor1";
            this.cbProveedor1.Size = new System.Drawing.Size(189, 21);
            this.cbProveedor1.TabIndex = 0;
            this.cbProveedor1.SelectionChangeCommitted += new System.EventHandler(this.cbProveedor1_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtRFC2);
            this.groupBox1.Controls.Add(this.cbProveedor2);
            this.groupBox1.Location = new System.Drawing.Point(586, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 70);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proveedor 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label4.Location = new System.Drawing.Point(30, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "RFC:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label5.Location = new System.Drawing.Point(14, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Nombre:";
            // 
            // txtRFC2
            // 
            this.txtRFC2.Location = new System.Drawing.Point(67, 44);
            this.txtRFC2.Name = "txtRFC2";
            this.txtRFC2.ReadOnly = true;
            this.txtRFC2.Size = new System.Drawing.Size(122, 20);
            this.txtRFC2.TabIndex = 1;
            // 
            // cbProveedor2
            // 
            this.cbProveedor2.FormattingEnabled = true;
            this.cbProveedor2.Location = new System.Drawing.Point(67, 19);
            this.cbProveedor2.Name = "cbProveedor2";
            this.cbProveedor2.Size = new System.Drawing.Size(189, 21);
            this.cbProveedor2.TabIndex = 0;
            this.cbProveedor2.SelectionChangeCommitted += new System.EventHandler(this.cbProveedor2_SelectionChangeCommitted);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtRFC3);
            this.groupBox2.Controls.Add(this.cbProveedor3);
            this.groupBox2.Location = new System.Drawing.Point(854, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 70);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proveedor 3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label6.Location = new System.Drawing.Point(30, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "RFC:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label7.Location = new System.Drawing.Point(14, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Nombre:";
            // 
            // txtRFC3
            // 
            this.txtRFC3.Location = new System.Drawing.Point(67, 44);
            this.txtRFC3.Name = "txtRFC3";
            this.txtRFC3.ReadOnly = true;
            this.txtRFC3.Size = new System.Drawing.Size(122, 20);
            this.txtRFC3.TabIndex = 1;
            // 
            // cbProveedor3
            // 
            this.cbProveedor3.FormattingEnabled = true;
            this.cbProveedor3.Location = new System.Drawing.Point(67, 19);
            this.cbProveedor3.Name = "cbProveedor3";
            this.cbProveedor3.Size = new System.Drawing.Size(189, 21);
            this.cbProveedor3.TabIndex = 0;
            this.cbProveedor3.SelectionChangeCommitted += new System.EventHandler(this.cbProveedor3_SelectionChangeCommitted);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.grpBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.dgvDetalle);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1132, 342);
            this.panel1.TabIndex = 17;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ctrlGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpEspecifico);
            this.Controls.Add(this.panel1);
            this.Name = "ctrlGeneral";
            this.Size = new System.Drawing.Size(1149, 472);
            this.Load += new System.EventHandler(this.ctrlEspecifico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.grpEspecifico.ResumeLayout(false);
            this.grpEspecifico.PerformLayout();
            this.grpBox1.ResumeLayout(false);
            this.grpBox1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.GroupBox grpEspecifico;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIVA;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.GroupBox grpBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRFC1;
        private System.Windows.Forms.ComboBox cbProveedor1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRFC2;
        private System.Windows.Forms.ComboBox cbProveedor2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRFC3;
        private System.Windows.Forms.ComboBox cbProveedor3;
        private System.Windows.Forms.Panel panel1;
    }
}
