namespace Compras
{
    partial class SeguimientoCompras
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeguimientoCompras));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtArticulo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.clbAlmacen = new System.Windows.Forms.CheckedListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cbPPC = new System.Windows.Forms.CheckBox();
            this.cbEspecial = new System.Windows.Forms.CheckBox();
            this.dtFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dtFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gridExceso = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExceso)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtArticulo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.clbAlmacen);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbPPC);
            this.groupBox1.Controls.Add(this.cbEspecial);
            this.groupBox1.Controls.Add(this.dtFechaFinal);
            this.groupBox1.Controls.Add(this.dtFechaInicial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 101);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtArticulo
            // 
            this.txtArticulo.Location = new System.Drawing.Point(92, 66);
            this.txtArticulo.Name = "txtArticulo";
            this.txtArticulo.Size = new System.Drawing.Size(116, 21);
            this.txtArticulo.TabIndex = 15;
            this.txtArticulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Artículo:";
            // 
            // clbAlmacen
            // 
            this.clbAlmacen.FormattingEnabled = true;
            this.clbAlmacen.Location = new System.Drawing.Point(292, 16);
            this.clbAlmacen.Name = "clbAlmacen";
            this.clbAlmacen.Size = new System.Drawing.Size(160, 68);
            this.clbAlmacen.TabIndex = 13;
            this.clbAlmacen.Click += new System.EventHandler(this.clbSucursal_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(629, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 34);
            this.button2.TabIndex = 12;
            this.button2.Text = "Exportar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(629, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 11;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbPPC
            // 
            this.cbPPC.AutoSize = true;
            this.cbPPC.Checked = true;
            this.cbPPC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPPC.Location = new System.Drawing.Point(472, 42);
            this.cbPPC.Name = "cbPPC";
            this.cbPPC.Size = new System.Drawing.Size(50, 19);
            this.cbPPC.TabIndex = 8;
            this.cbPPC.Text = "PPC";
            this.cbPPC.UseVisualStyleBackColor = true;
            // 
            // cbEspecial
            // 
            this.cbEspecial.AutoSize = true;
            this.cbEspecial.Checked = true;
            this.cbEspecial.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbEspecial.Location = new System.Drawing.Point(472, 19);
            this.cbEspecial.Name = "cbEspecial";
            this.cbEspecial.Size = new System.Drawing.Size(119, 19);
            this.cbEspecial.TabIndex = 7;
            this.cbEspecial.Text = "Compra especial";
            this.cbEspecial.UseVisualStyleBackColor = true;
            // 
            // dtFechaFinal
            // 
            this.dtFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaFinal.Location = new System.Drawing.Point(92, 43);
            this.dtFechaFinal.Name = "dtFechaFinal";
            this.dtFechaFinal.Size = new System.Drawing.Size(117, 21);
            this.dtFechaFinal.TabIndex = 5;
            // 
            // dtFechaInicial
            // 
            this.dtFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFechaInicial.Location = new System.Drawing.Point(92, 18);
            this.dtFechaInicial.Name = "dtFechaInicial";
            this.dtFechaInicial.Size = new System.Drawing.Size(117, 21);
            this.dtFechaInicial.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha inicial:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha final:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Almacén:";
            // 
            // gridExceso
            // 
            this.gridExceso.AllowDrop = true;
            this.gridExceso.AllowUserToAddRows = false;
            this.gridExceso.AllowUserToDeleteRows = false;
            this.gridExceso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridExceso.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridExceso.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridExceso.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridExceso.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridExceso.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridExceso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridExceso.EnableHeadersVisualStyles = false;
            this.gridExceso.Location = new System.Drawing.Point(15, 122);
            this.gridExceso.Name = "gridExceso";
            this.gridExceso.ReadOnly = true;
            this.gridExceso.Size = new System.Drawing.Size(844, 463);
            this.gridExceso.TabIndex = 2;
            this.gridExceso.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridExceso_DataBindingComplete);
            // 
            // SeguimientoCompras
            // 
            this.AccessibleDescription = "Seguimiento de compras";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 597);
            this.Controls.Add(this.gridExceso);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SeguimientoCompras";
            this.Text = "Seguimiento de Comp. Esp y PPC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SeguimientoCompras_FormClosing);
            this.Load += new System.EventHandler(this.SeguimientoCompras_Load);
            this.Shown += new System.EventHandler(this.SeguimientoCompras_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExceso)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtFechaFinal;
        private System.Windows.Forms.DateTimePicker dtFechaInicial;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox cbPPC;
        private System.Windows.Forms.CheckBox cbEspecial;
        private System.Windows.Forms.CheckedListBox clbAlmacen;
        private System.Windows.Forms.DataGridView gridExceso;
        private System.Windows.Forms.TextBox txtArticulo;
        private System.Windows.Forms.Label label4;
    }
}