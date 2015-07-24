namespace Cobranza
{
    partial class DocsElectronicos
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocsElectronicos));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnCorreos = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.gridDocumentos = new System.Windows.Forms.DataGridView();
            this.cbTipoDocto = new System.Windows.Forms.ComboBox();
            this.cbCobranza = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolMensajes = new System.Windows.Forms.ToolStripStatusLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFactura = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridDocumentos)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(91, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(67, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cliente:";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(91, 40);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(175, 20);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(91, 69);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(175, 20);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Fecha inicial:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Fecha final:";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::Cobranza.Properties.Resources.search_2;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(642, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 42);
            this.button2.TabIndex = 6;
            this.button2.Text = "Buscar Facturas";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // btnCorreos
            // 
            this.btnCorreos.Enabled = false;
            this.btnCorreos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorreos.Image = global::Cobranza.Properties.Resources.send;
            this.btnCorreos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCorreos.Location = new System.Drawing.Point(642, 59);
            this.btnCorreos.Name = "btnCorreos";
            this.btnCorreos.Size = new System.Drawing.Size(88, 39);
            this.btnCorreos.TabIndex = 8;
            this.btnCorreos.Text = "Enviar\r\n correos";
            this.btnCorreos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCorreos.UseVisualStyleBackColor = true;
            this.btnCorreos.Click += new System.EventHandler(this.btnCorreos_Click);
            this.btnCorreos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Documentos:";
            // 
            // gridDocumentos
            // 
            this.gridDocumentos.AllowUserToAddRows = false;
            this.gridDocumentos.AllowUserToDeleteRows = false;
            this.gridDocumentos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridDocumentos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDocumentos.Location = new System.Drawing.Point(10, 103);
            this.gridDocumentos.Name = "gridDocumentos";
            this.gridDocumentos.Size = new System.Drawing.Size(813, 457);
            this.gridDocumentos.TabIndex = 7;
            this.gridDocumentos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDocumentos_CellValueChanged);
            this.gridDocumentos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // cbTipoDocto
            // 
            this.cbTipoDocto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTipoDocto.FormattingEnabled = true;
            this.cbTipoDocto.Items.AddRange(new object[] {
            "Todo",
            "Facturas",
            "Nota de crédito"});
            this.cbTipoDocto.Location = new System.Drawing.Point(398, 69);
            this.cbTipoDocto.Name = "cbTipoDocto";
            this.cbTipoDocto.Size = new System.Drawing.Size(148, 21);
            this.cbTipoDocto.TabIndex = 5;
            this.cbTipoDocto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // cbCobranza
            // 
            this.cbCobranza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCobranza.FormattingEnabled = true;
            this.cbCobranza.Location = new System.Drawing.Point(398, 40);
            this.cbCobranza.Name = "cbCobranza";
            this.cbCobranza.Size = new System.Drawing.Size(148, 21);
            this.cbCobranza.TabIndex = 4;
            this.cbCobranza.Visible = false;
            this.cbCobranza.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(300, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Jefa de cobranza:";
            this.label5.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatus,
            this.toolMensajes});
            this.statusStrip1.Location = new System.Drawing.Point(0, 578);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(835, 22);
            this.statusStrip1.TabIndex = 17;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStatus
            // 
            this.toolStatus.Name = "toolStatus";
            this.toolStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolMensajes
            // 
            this.toolMensajes.Name = "toolMensajes";
            this.toolMensajes.Size = new System.Drawing.Size(0, 17);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Folio SAP:";
            // 
            // txtFactura
            // 
            this.txtFactura.Location = new System.Drawing.Point(398, 11);
            this.txtFactura.Name = "txtFactura";
            this.txtFactura.Size = new System.Drawing.Size(67, 20);
            this.txtFactura.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtFactura, "Número de factura o \r\nnota de crédito");
            this.txtFactura.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // DocsElectronicos
            // 
            this.AccessibleDescription = "Envio de docs electronicos";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 600);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFactura);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbCobranza);
            this.Controls.Add(this.cbTipoDocto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCorreos);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gridDocumentos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocsElectronicos";
            this.Text = "Enviar documentos fiscales";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridDocumentos)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnCorreos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView gridDocumentos;
        private System.Windows.Forms.ComboBox cbTipoDocto;
        private System.Windows.Forms.ComboBox cbCobranza;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFactura;
        private System.Windows.Forms.ToolStripStatusLabel toolMensajes;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

