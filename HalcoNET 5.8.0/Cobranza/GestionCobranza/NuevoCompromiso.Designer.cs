namespace Cobranza.AntiguedadSaldos
{
    partial class NuevoCompromiso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NuevoCompromiso));
            this.gridFacturas = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtFecha = new System.Windows.Forms.DateTimePicker();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.rtCompromiso = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtComprometido = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbNormal = new System.Windows.Forms.RadioButton();
            this.rbRecuperacion = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridFacturas)).BeginInit();
            this.SuspendLayout();
            // 
            // gridFacturas
            // 
            this.gridFacturas.AllowUserToAddRows = false;
            this.gridFacturas.AllowUserToDeleteRows = false;
            this.gridFacturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridFacturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridFacturas.Location = new System.Drawing.Point(12, 172);
            this.gridFacturas.Name = "gridFacturas";
            this.gridFacturas.ReadOnly = true;
            this.gridFacturas.Size = new System.Drawing.Size(505, 207);
            this.gridFacturas.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fecha de compromiso:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Monto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Compromiso:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Facturas:";
            // 
            // dtFecha
            // 
            this.dtFecha.CustomFormat = " hh:mm:ss tt - dd/MM/yyyy";
            this.dtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFecha.Location = new System.Drawing.Point(146, 7);
            this.dtFecha.Name = "dtFecha";
            this.dtFecha.Size = new System.Drawing.Size(126, 20);
            this.dtFecha.TabIndex = 0;
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(146, 34);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ReadOnly = true;
            this.txtMonto.Size = new System.Drawing.Size(126, 20);
            this.txtMonto.TabIndex = 6;
            this.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rtCompromiso
            // 
            this.rtCompromiso.Location = new System.Drawing.Point(146, 91);
            this.rtCompromiso.Name = "rtCompromiso";
            this.rtCompromiso.Size = new System.Drawing.Size(371, 62);
            this.rtCompromiso.TabIndex = 1;
            this.rtCompromiso.Text = "";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::Cobranza.Properties.Resources.Apply;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(424, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "Guardar \r\ncompromiso";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtComprometido
            // 
            this.txtComprometido.Location = new System.Drawing.Point(146, 60);
            this.txtComprometido.Name = "txtComprometido";
            this.txtComprometido.Size = new System.Drawing.Size(126, 20);
            this.txtComprometido.TabIndex = 8;
            this.txtComprometido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Cantidad comprometida:";
            // 
            // rbNormal
            // 
            this.rbNormal.AccessibleDescription = "01";
            this.rbNormal.AutoSize = true;
            this.rbNormal.Checked = true;
            this.rbNormal.Location = new System.Drawing.Point(299, 62);
            this.rbNormal.Name = "rbNormal";
            this.rbNormal.Size = new System.Drawing.Size(104, 17);
            this.rbNormal.TabIndex = 9;
            this.rbNormal.TabStop = true;
            this.rbNormal.Text = "Cobranza normal";
            this.rbNormal.UseVisualStyleBackColor = true;
            // 
            // rbRecuperacion
            // 
            this.rbRecuperacion.AccessibleDescription = "02";
            this.rbRecuperacion.AutoSize = true;
            this.rbRecuperacion.Location = new System.Drawing.Point(409, 62);
            this.rbRecuperacion.Name = "rbRecuperacion";
            this.rbRecuperacion.Size = new System.Drawing.Size(92, 17);
            this.rbRecuperacion.TabIndex = 10;
            this.rbRecuperacion.Text = "Recuperación";
            this.rbRecuperacion.UseVisualStyleBackColor = true;
            // 
            // NuevoCompromiso
            // 
            this.AccessibleDescription = "Compromisos - Llamadas";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 391);
            this.Controls.Add(this.rbRecuperacion);
            this.Controls.Add(this.rbNormal);
            this.Controls.Add(this.txtComprometido);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtCompromiso);
            this.Controls.Add(this.txtMonto);
            this.Controls.Add(this.dtFecha);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridFacturas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NuevoCompromiso";
            this.Text = "Nuevo compromiso";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NuevoCompromiso_FormClosing);
            this.Load += new System.EventHandler(this.NuevoCompromiso_Load);
            this.Shown += new System.EventHandler(this.NuevoCompromiso_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridFacturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridFacturas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtFecha;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.RichTextBox rtCompromiso;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtComprometido;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbNormal;
        private System.Windows.Forms.RadioButton rbRecuperacion;
    }
}