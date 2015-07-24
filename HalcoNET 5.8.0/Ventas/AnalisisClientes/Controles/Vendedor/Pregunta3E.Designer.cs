namespace Ventas.AnalisisClientes.Controles
{
    partial class Pregunta3E
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolBack = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolHome = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCliente = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtLimiteActual = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtNombre = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLimiteRequerido = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnFinalizar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblCliente = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBack,
            this.toolHome});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(464, 23);
            this.statusStrip1.TabIndex = 49;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // toolBack
            // 
            this.toolBack.Image = global::Ventas.Properties.Resources.left;
            this.toolBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBack.Name = "toolBack";
            this.toolBack.Size = new System.Drawing.Size(101, 18);
            this.toolBack.Text = "Regresar (ESC)";
            this.toolBack.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // toolHome
            // 
            this.toolHome.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolHome.Image = global::Ventas.Properties.Resources.home;
            this.toolHome.Name = "toolHome";
            this.toolHome.Size = new System.Drawing.Size(56, 20);
            this.toolHome.Text = "Inicio";
            this.toolHome.Visible = false;
            this.toolHome.Click += new System.EventHandler(this.lblHome_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(304, 18);
            this.label1.TabIndex = 50;
            this.label1.Text = "1.- Ingresa el límite de crédito requerido por el cliente.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.UseCompatibleTextRendering = true;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(172, 81);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(147, 25);
            this.txtCliente.StateActive.Back.Color1 = System.Drawing.Color.White;
            this.txtCliente.StateActive.Border.Color1 = System.Drawing.Color.Black;
            this.txtCliente.StateActive.Border.Color2 = System.Drawing.Color.Black;
            this.txtCliente.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtCliente.StateActive.Border.Rounding = 3;
            this.txtCliente.StateActive.Content.Color1 = System.Drawing.Color.Black;
            this.txtCliente.StateActive.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.StateCommon.Content.Color1 = System.Drawing.Color.Black;
            this.txtCliente.StateDisabled.Content.Color1 = System.Drawing.Color.Black;
            this.txtCliente.StateDisabled.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.StateNormal.Content.Color1 = System.Drawing.Color.Black;
            this.txtCliente.StateNormal.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCliente.TabIndex = 51;
            this.txtCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // txtLimiteActual
            // 
            this.txtLimiteActual.Location = new System.Drawing.Point(172, 155);
            this.txtLimiteActual.Name = "txtLimiteActual";
            this.txtLimiteActual.ReadOnly = true;
            this.txtLimiteActual.Size = new System.Drawing.Size(147, 25);
            this.txtLimiteActual.StateActive.Back.Color1 = System.Drawing.Color.White;
            this.txtLimiteActual.StateActive.Border.Color1 = System.Drawing.Color.Black;
            this.txtLimiteActual.StateActive.Border.Color2 = System.Drawing.Color.Black;
            this.txtLimiteActual.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtLimiteActual.StateActive.Border.Rounding = 3;
            this.txtLimiteActual.StateActive.Content.Color1 = System.Drawing.Color.Black;
            this.txtLimiteActual.StateActive.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimiteActual.StateCommon.Content.Color1 = System.Drawing.Color.Black;
            this.txtLimiteActual.StateDisabled.Content.Color1 = System.Drawing.Color.Black;
            this.txtLimiteActual.StateDisabled.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimiteActual.StateNormal.Content.Color1 = System.Drawing.Color.Black;
            this.txtLimiteActual.StateNormal.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimiteActual.TabIndex = 52;
            this.txtLimiteActual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(172, 118);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(278, 25);
            this.txtNombre.StateActive.Back.Color1 = System.Drawing.Color.White;
            this.txtNombre.StateActive.Border.Color1 = System.Drawing.Color.Black;
            this.txtNombre.StateActive.Border.Color2 = System.Drawing.Color.Black;
            this.txtNombre.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNombre.StateActive.Border.Rounding = 3;
            this.txtNombre.StateActive.Content.Color1 = System.Drawing.Color.Black;
            this.txtNombre.StateActive.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.StateCommon.Content.Color1 = System.Drawing.Color.Black;
            this.txtNombre.StateDisabled.Content.Color1 = System.Drawing.Color.Black;
            this.txtNombre.StateDisabled.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.StateNormal.Content.Color1 = System.Drawing.Color.Black;
            this.txtNombre.StateNormal.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.TabIndex = 53;
            this.txtNombre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 54;
            this.label2.Text = "Límite actual";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoEllipsis = true;
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(70, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 18);
            this.label3.TabIndex = 55;
            this.label3.Text = "Cliente";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.UseCompatibleTextRendering = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(70, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 18);
            this.label4.TabIndex = 56;
            this.label4.Text = "Código";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.UseCompatibleTextRendering = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoEllipsis = true;
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(70, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 18);
            this.label5.TabIndex = 58;
            this.label5.Text = "Límite requerido";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.UseCompatibleTextRendering = true;
            // 
            // txtLimiteRequerido
            // 
            this.txtLimiteRequerido.Location = new System.Drawing.Point(172, 192);
            this.txtLimiteRequerido.Name = "txtLimiteRequerido";
            this.txtLimiteRequerido.Size = new System.Drawing.Size(147, 25);
            this.txtLimiteRequerido.StateActive.Back.Color1 = System.Drawing.SystemColors.Info;
            this.txtLimiteRequerido.StateActive.Border.Color1 = System.Drawing.Color.Black;
            this.txtLimiteRequerido.StateActive.Border.Color2 = System.Drawing.Color.Black;
            this.txtLimiteRequerido.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtLimiteRequerido.StateActive.Border.Rounding = 3;
            this.txtLimiteRequerido.StateActive.Content.Color1 = System.Drawing.Color.Black;
            this.txtLimiteRequerido.StateActive.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimiteRequerido.StateDisabled.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimiteRequerido.StateNormal.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLimiteRequerido.TabIndex = 57;
            this.txtLimiteRequerido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLimiteRequerido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(319, 387);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(131, 31);
            this.btnFinalizar.TabIndex = 59;
            this.btnFinalizar.Values.Text = "Finalizar y Guardar";
            this.btnFinalizar.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(14, 12);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(140, 19);
            this.lblCliente.TabIndex = 60;
            this.lblCliente.Text = "Nombre del cliente";
            // 
            // Pregunta3E
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLimiteRequerido);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtLimiteActual);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Pregunta3E";
            this.Size = new System.Drawing.Size(464, 455);
            this.Load += new System.EventHandler(this.Pregunta3E_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolBack;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCliente;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtLimiteActual;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtLimiteRequerido;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnFinalizar;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ToolStripStatusLabel toolHome;
    }
}
