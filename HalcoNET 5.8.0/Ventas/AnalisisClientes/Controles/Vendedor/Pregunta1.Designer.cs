namespace Ventas.AnalisisClientes.Controles
{
    partial class Pregunta1
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
            this.lblCliente = new System.Windows.Forms.Label();
            this.gpPregunta = new System.Windows.Forms.GroupBox();
            this.txtEspecificar = new System.Windows.Forms.TextBox();
            this.rbO = new System.Windows.Forms.RadioButton();
            this.rbF = new System.Windows.Forms.RadioButton();
            this.kryptonButton1 = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.rbE = new System.Windows.Forms.RadioButton();
            this.rbD = new System.Windows.Forms.RadioButton();
            this.rbC = new System.Windows.Forms.RadioButton();
            this.rbB = new System.Windows.Forms.RadioButton();
            this.rbA = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.gpPregunta.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(18, 18);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(140, 19);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "Nombre del cliente";
            // 
            // gpPregunta
            // 
            this.gpPregunta.Controls.Add(this.txtEspecificar);
            this.gpPregunta.Controls.Add(this.rbO);
            this.gpPregunta.Controls.Add(this.rbF);
            this.gpPregunta.Controls.Add(this.kryptonButton1);
            this.gpPregunta.Controls.Add(this.rbE);
            this.gpPregunta.Controls.Add(this.rbD);
            this.gpPregunta.Controls.Add(this.rbC);
            this.gpPregunta.Controls.Add(this.rbB);
            this.gpPregunta.Controls.Add(this.rbA);
            this.gpPregunta.Location = new System.Drawing.Point(22, 53);
            this.gpPregunta.Name = "gpPregunta";
            this.gpPregunta.Size = new System.Drawing.Size(484, 525);
            this.gpPregunta.TabIndex = 1;
            this.gpPregunta.TabStop = false;
            this.gpPregunta.Text = "1. El cliente puede comprar mas? ";
            // 
            // txtEspecificar
            // 
            this.txtEspecificar.Location = new System.Drawing.Point(42, 426);
            this.txtEspecificar.Multiline = true;
            this.txtEspecificar.Name = "txtEspecificar";
            this.txtEspecificar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEspecificar.Size = new System.Drawing.Size(407, 50);
            this.txtEspecificar.TabIndex = 8;
            this.txtEspecificar.Visible = false;
            this.txtEspecificar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // rbO
            // 
            this.rbO.AccessibleName = "O";
            this.rbO.AutoSize = true;
            this.rbO.Location = new System.Drawing.Point(21, 403);
            this.rbO.Name = "rbO";
            this.rbO.Size = new System.Drawing.Size(114, 17);
            this.rbO.TabIndex = 7;
            this.rbO.TabStop = true;
            this.rbO.Text = "Otros (Espeficique)";
            this.rbO.UseVisualStyleBackColor = true;
            this.rbO.Visible = false;
            this.rbO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // rbF
            // 
            this.rbF.AccessibleName = "F";
            this.rbF.AutoSize = true;
            this.rbF.Location = new System.Drawing.Point(21, 344);
            this.rbF.Name = "rbF";
            this.rbF.Size = new System.Drawing.Size(14, 13);
            this.rbF.TabIndex = 6;
            this.rbF.TabStop = true;
            this.rbF.UseVisualStyleBackColor = true;
            this.rbF.Visible = false;
            this.rbF.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // kryptonButton1
            // 
            this.kryptonButton1.Location = new System.Drawing.Point(318, 482);
            this.kryptonButton1.Name = "kryptonButton1";
            this.kryptonButton1.Size = new System.Drawing.Size(131, 31);
            this.kryptonButton1.TabIndex = 5;
            this.kryptonButton1.Values.Text = "Guardar y continuar";
            this.kryptonButton1.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // rbE
            // 
            this.rbE.AccessibleName = "E";
            this.rbE.AutoSize = true;
            this.rbE.Location = new System.Drawing.Point(21, 285);
            this.rbE.Name = "rbE";
            this.rbE.Size = new System.Drawing.Size(14, 13);
            this.rbE.TabIndex = 4;
            this.rbE.TabStop = true;
            this.rbE.UseVisualStyleBackColor = true;
            this.rbE.Visible = false;
            this.rbE.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // rbD
            // 
            this.rbD.AccessibleName = "D";
            this.rbD.AutoSize = true;
            this.rbD.Location = new System.Drawing.Point(21, 226);
            this.rbD.Name = "rbD";
            this.rbD.Size = new System.Drawing.Size(14, 13);
            this.rbD.TabIndex = 3;
            this.rbD.TabStop = true;
            this.rbD.UseVisualStyleBackColor = true;
            this.rbD.Visible = false;
            this.rbD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // rbC
            // 
            this.rbC.AccessibleName = "C";
            this.rbC.AutoSize = true;
            this.rbC.Location = new System.Drawing.Point(21, 167);
            this.rbC.Name = "rbC";
            this.rbC.Size = new System.Drawing.Size(14, 13);
            this.rbC.TabIndex = 2;
            this.rbC.TabStop = true;
            this.rbC.UseVisualStyleBackColor = true;
            this.rbC.Visible = false;
            this.rbC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // rbB
            // 
            this.rbB.AccessibleName = "B";
            this.rbB.AutoSize = true;
            this.rbB.Location = new System.Drawing.Point(21, 108);
            this.rbB.Name = "rbB";
            this.rbB.Size = new System.Drawing.Size(39, 17);
            this.rbB.TabIndex = 1;
            this.rbB.TabStop = true;
            this.rbB.Text = "No";
            this.rbB.UseVisualStyleBackColor = true;
            this.rbB.Click += new System.EventHandler(this.rb_Click);
            this.rbB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // rbA
            // 
            this.rbA.AccessibleName = "A";
            this.rbA.AutoSize = true;
            this.rbA.Location = new System.Drawing.Point(21, 49);
            this.rbA.Name = "rbA";
            this.rbA.Size = new System.Drawing.Size(34, 17);
            this.rbA.TabIndex = 0;
            this.rbA.TabStop = true;
            this.rbA.Text = "Si";
            this.rbA.UseVisualStyleBackColor = true;
            this.rbA.Click += new System.EventHandler(this.rb_Click);
            this.rbA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 586);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(530, 27);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.Image = global::Ventas.Properties.Resources.left;
            this.toolStripStatusLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(105, 22);
            this.toolStripStatusLabel1.Text = "Regresar (ESC)";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // Pregunta1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gpPregunta);
            this.Controls.Add(this.lblCliente);
            this.Name = "Pregunta1";
            this.Size = new System.Drawing.Size(530, 613);
            this.Load += new System.EventHandler(this.Pregunta1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            this.gpPregunta.ResumeLayout(false);
            this.gpPregunta.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.GroupBox gpPregunta;
        private System.Windows.Forms.TextBox txtEspecificar;
        private System.Windows.Forms.RadioButton rbO;
        private System.Windows.Forms.RadioButton rbF;
        private ComponentFactory.Krypton.Toolkit.KryptonButton kryptonButton1;
        private System.Windows.Forms.RadioButton rbE;
        private System.Windows.Forms.RadioButton rbD;
        private System.Windows.Forms.RadioButton rbC;
        private System.Windows.Forms.RadioButton rbB;
        private System.Windows.Forms.RadioButton rbA;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
