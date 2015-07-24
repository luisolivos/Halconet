namespace Ventas
{
    partial class frmLineas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLineas));
            this.grpName = new System.Windows.Forms.GroupBox();
            this.clbVendedor = new System.Windows.Forms.CheckedListBox();
            this.clbSucursal = new System.Windows.Forms.CheckedListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.cbLinea = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFecha = new System.Windows.Forms.DateTimePicker();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.gridTotales = new System.Windows.Forms.DataGridView();
            this.dgvDatos = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAvanceOptimo = new System.Windows.Forms.TextBox();
            this.txtDiasRestantes = new System.Windows.Forms.TextBox();
            this.txtDiasTranscurridos = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDiasMes = new System.Windows.Forms.TextBox();
            this.grpName.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTotales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // grpName
            // 
            this.grpName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpName.Controls.Add(this.clbVendedor);
            this.grpName.Controls.Add(this.clbSucursal);
            this.grpName.Controls.Add(this.button3);
            this.grpName.Controls.Add(this.cbLinea);
            this.grpName.Controls.Add(this.label4);
            this.grpName.Controls.Add(this.button2);
            this.grpName.Controls.Add(this.button1);
            this.grpName.Controls.Add(this.lblVendedor);
            this.grpName.Controls.Add(this.label2);
            this.grpName.Controls.Add(this.dtFecha);
            this.grpName.Controls.Add(this.lblSucursal);
            this.grpName.Location = new System.Drawing.Point(12, 12);
            this.grpName.Name = "grpName";
            this.grpName.Size = new System.Drawing.Size(180, 619);
            this.grpName.TabIndex = 0;
            this.grpName.TabStop = false;
            this.grpName.Text = "Linea:";
            // 
            // clbVendedor
            // 
            this.clbVendedor.FormattingEnabled = true;
            this.clbVendedor.Location = new System.Drawing.Point(6, 234);
            this.clbVendedor.Name = "clbVendedor";
            this.clbVendedor.Size = new System.Drawing.Size(168, 64);
            this.clbVendedor.TabIndex = 12;
            // 
            // clbSucursal
            // 
            this.clbSucursal.FormattingEnabled = true;
            this.clbSucursal.Location = new System.Drawing.Point(6, 138);
            this.clbSucursal.Name = "clbSucursal";
            this.clbSucursal.Size = new System.Drawing.Size(168, 64);
            this.clbSucursal.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(98, 582);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 31);
            this.button3.TabIndex = 10;
            this.button3.Text = "Objetivos ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbLinea
            // 
            this.cbLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLinea.FormattingEnabled = true;
            this.cbLinea.Location = new System.Drawing.Point(9, 90);
            this.cbLinea.Name = "cbLinea";
            this.cbLinea.Size = new System.Drawing.Size(165, 21);
            this.cbLinea.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Linea";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(98, 369);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 31);
            this.button2.TabIndex = 7;
            this.button2.Text = "Exportar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(98, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 31);
            this.button1.TabIndex = 6;
            this.button1.Text = "Consultar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Location = new System.Drawing.Point(6, 218);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(67, 13);
            this.lblVendedor.TabIndex = 4;
            this.lblVendedor.Text = "Vendedores:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fecha:";
            // 
            // dtFecha
            // 
            this.dtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFecha.Location = new System.Drawing.Point(9, 41);
            this.dtFecha.Name = "dtFecha";
            this.dtFecha.Size = new System.Drawing.Size(117, 20);
            this.dtFecha.TabIndex = 2;
            this.dtFecha.ValueChanged += new System.EventHandler(this.dtFecha_ValueChanged);
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Location = new System.Drawing.Point(6, 122);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(62, 13);
            this.lblSucursal.TabIndex = 0;
            this.lblSucursal.Text = "Sucursales:";
            // 
            // gridTotales
            // 
            this.gridTotales.AllowUserToAddRows = false;
            this.gridTotales.AllowUserToDeleteRows = false;
            this.gridTotales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTotales.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridTotales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTotales.Location = new System.Drawing.Point(475, 12);
            this.gridTotales.Name = "gridTotales";
            this.gridTotales.ReadOnly = true;
            this.gridTotales.Size = new System.Drawing.Size(409, 84);
            this.gridTotales.TabIndex = 1;
            this.gridTotales.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridTotales_DataBindingComplete);
            // 
            // dgvDatos
            // 
            this.dgvDatos.AllowUserToAddRows = false;
            this.dgvDatos.AllowUserToDeleteRows = false;
            this.dgvDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDatos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDatos.Location = new System.Drawing.Point(198, 102);
            this.dgvDatos.Name = "dgvDatos";
            this.dgvDatos.ReadOnly = true;
            this.dgvDatos.Size = new System.Drawing.Size(686, 529);
            this.dgvDatos.TabIndex = 2;
            this.dgvDatos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvDatos_DataBindingComplete);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(200, 73);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "Avance Optimo";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(200, 51);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 111;
            this.label9.Text = "Días Restantes";
            // 
            // txtAvanceOptimo
            // 
            this.txtAvanceOptimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAvanceOptimo.Location = new System.Drawing.Point(334, 69);
            this.txtAvanceOptimo.Margin = new System.Windows.Forms.Padding(5);
            this.txtAvanceOptimo.Name = "txtAvanceOptimo";
            this.txtAvanceOptimo.ReadOnly = true;
            this.txtAvanceOptimo.Size = new System.Drawing.Size(85, 20);
            this.txtAvanceOptimo.TabIndex = 112;
            this.txtAvanceOptimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDiasRestantes
            // 
            this.txtDiasRestantes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasRestantes.Location = new System.Drawing.Point(334, 47);
            this.txtDiasRestantes.Margin = new System.Windows.Forms.Padding(5);
            this.txtDiasRestantes.Name = "txtDiasRestantes";
            this.txtDiasRestantes.ReadOnly = true;
            this.txtDiasRestantes.Size = new System.Drawing.Size(85, 20);
            this.txtDiasRestantes.TabIndex = 110;
            this.txtDiasRestantes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDiasTranscurridos
            // 
            this.txtDiasTranscurridos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasTranscurridos.Location = new System.Drawing.Point(334, 26);
            this.txtDiasTranscurridos.Margin = new System.Windows.Forms.Padding(5);
            this.txtDiasTranscurridos.Name = "txtDiasTranscurridos";
            this.txtDiasTranscurridos.ReadOnly = true;
            this.txtDiasTranscurridos.Size = new System.Drawing.Size(85, 20);
            this.txtDiasTranscurridos.TabIndex = 107;
            this.txtDiasTranscurridos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(200, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 109;
            this.label6.Text = "Días Transcurridos";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(200, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 108;
            this.label7.Text = "Días Mes";
            // 
            // txtDiasMes
            // 
            this.txtDiasMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiasMes.Location = new System.Drawing.Point(334, 5);
            this.txtDiasMes.Margin = new System.Windows.Forms.Padding(5);
            this.txtDiasMes.Name = "txtDiasMes";
            this.txtDiasMes.ReadOnly = true;
            this.txtDiasMes.Size = new System.Drawing.Size(85, 20);
            this.txtDiasMes.TabIndex = 106;
            this.txtDiasMes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmLineas
            // 
            this.AccessibleDescription = "Linea objetivo";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 643);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtAvanceOptimo);
            this.Controls.Add(this.txtDiasRestantes);
            this.Controls.Add(this.txtDiasTranscurridos);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDiasMes);
            this.Controls.Add(this.dgvDatos);
            this.Controls.Add(this.gridTotales);
            this.Controls.Add(this.grpName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLineas";
            this.Text = "Lineas Objetivo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLineas_FormClosing);
            this.Load += new System.EventHandler(this.frmLineas_Load);
            this.Shown += new System.EventHandler(this.frmLineas_Shown);
            this.grpName.ResumeLayout(false);
            this.grpName.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTotales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpName;
        private System.Windows.Forms.ComboBox cbLinea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtFecha;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.DataGridView gridTotales;
        private System.Windows.Forms.DataGridView dgvDatos;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckedListBox clbSucursal;
        private System.Windows.Forms.CheckedListBox clbVendedor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAvanceOptimo;
        private System.Windows.Forms.TextBox txtDiasRestantes;
        private System.Windows.Forms.TextBox txtDiasTranscurridos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDiasMes;
    }
}

