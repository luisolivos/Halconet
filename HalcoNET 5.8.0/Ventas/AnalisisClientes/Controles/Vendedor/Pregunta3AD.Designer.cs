namespace Ventas.AnalisisClientes.Controles
{
    partial class Pregunta3AD
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pregunta3AD));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvItems = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConsumo = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnFinalizar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolBack = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolHome = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCliente = new System.Windows.Forms.Label();
            this.Artículo = new System.Windows.Forms.Label();
            this.txtArticulo = new System.Windows.Forms.TextBox();
            this.txtPrecioPJ = new System.Windows.Forms.TextBox();
            this.txtPrecioCompetencia = new System.Windows.Forms.TextBox();
            this.txtNombreComp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.label1.Location = new System.Drawing.Point(13, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(587, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.UseCompatibleTextRendering = true;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dgvItems.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dgvItems.Location = new System.Drawing.Point(16, 163);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvItems.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvItems.Size = new System.Drawing.Size(568, 174);
            this.dgvItems.TabIndex = 6;
            this.dgvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRanking_CellEndEdit);
            this.dgvItems.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvRanking_RowPostPaint);
            this.dgvItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
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
            this.label2.Location = new System.Drawing.Point(3, 358);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(390, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "2. ¿Cuál es el consumo aproximado mensual del cliente de esta línea?";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.UseCompatibleTextRendering = true;
            this.label2.Visible = false;
            // 
            // txtConsumo
            // 
            this.txtConsumo.Location = new System.Drawing.Point(16, 379);
            this.txtConsumo.Name = "txtConsumo";
            this.txtConsumo.Size = new System.Drawing.Size(147, 26);
            this.txtConsumo.StateActive.Back.Color1 = System.Drawing.SystemColors.Info;
            this.txtConsumo.StateActive.Border.Color1 = System.Drawing.Color.Black;
            this.txtConsumo.StateActive.Border.Color2 = System.Drawing.Color.Black;
            this.txtConsumo.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtConsumo.StateActive.Border.Rounding = 3;
            this.txtConsumo.StateActive.Content.Color1 = System.Drawing.Color.Black;
            this.txtConsumo.StateActive.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsumo.StateDisabled.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsumo.StateNormal.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsumo.TabIndex = 46;
            this.txtConsumo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtConsumo.Visible = false;
            this.txtConsumo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // btnFinalizar
            // 
            this.btnFinalizar.Location = new System.Drawing.Point(453, 442);
            this.btnFinalizar.Name = "btnFinalizar";
            this.btnFinalizar.Size = new System.Drawing.Size(131, 31);
            this.btnFinalizar.TabIndex = 47;
            this.btnFinalizar.Values.Text = "Finalizar y Guardar";
            this.btnFinalizar.Click += new System.EventHandler(this.kryptonButton1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBack,
            this.toolHome});
            this.statusStrip1.Location = new System.Drawing.Point(0, 499);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(600, 23);
            this.statusStrip1.TabIndex = 48;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            // 
            // toolBack
            // 
            this.toolBack.Image = global::Ventas.Properties.Resources.left;
            this.toolBack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBack.Name = "toolBack";
            this.toolBack.Size = new System.Drawing.Size(99, 18);
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
            this.toolHome.Size = new System.Drawing.Size(52, 20);
            this.toolHome.Text = "Inicio";
            this.toolHome.Visible = false;
            this.toolHome.Click += new System.EventHandler(this.lblHome_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(12, 17);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(140, 19);
            this.lblCliente.TabIndex = 49;
            this.lblCliente.Text = "Nombre del cliente";
            // 
            // Artículo
            // 
            this.Artículo.AutoSize = true;
            this.Artículo.Location = new System.Drawing.Point(39, 116);
            this.Artículo.Name = "Artículo";
            this.Artículo.Size = new System.Drawing.Size(44, 13);
            this.Artículo.TabIndex = 50;
            this.Artículo.Text = "Artículo";
            // 
            // txtArticulo
            // 
            this.txtArticulo.BackColor = System.Drawing.SystemColors.Info;
            this.txtArticulo.Location = new System.Drawing.Point(16, 133);
            this.txtArticulo.Name = "txtArticulo";
            this.txtArticulo.Size = new System.Drawing.Size(91, 20);
            this.txtArticulo.TabIndex = 51;
            // 
            // txtPrecioPJ
            // 
            this.txtPrecioPJ.BackColor = System.Drawing.SystemColors.Info;
            this.txtPrecioPJ.Location = new System.Drawing.Point(115, 133);
            this.txtPrecioPJ.Name = "txtPrecioPJ";
            this.txtPrecioPJ.Size = new System.Drawing.Size(100, 20);
            this.txtPrecioPJ.TabIndex = 52;
            this.txtPrecioPJ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPrecioCompetencia
            // 
            this.txtPrecioCompetencia.BackColor = System.Drawing.SystemColors.Info;
            this.txtPrecioCompetencia.Location = new System.Drawing.Point(223, 133);
            this.txtPrecioCompetencia.Name = "txtPrecioCompetencia";
            this.txtPrecioCompetencia.Size = new System.Drawing.Size(100, 20);
            this.txtPrecioCompetencia.TabIndex = 53;
            this.txtPrecioCompetencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNombreComp
            // 
            this.txtNombreComp.BackColor = System.Drawing.SystemColors.Info;
            this.txtNombreComp.Location = new System.Drawing.Point(331, 133);
            this.txtNombreComp.Name = "txtNombreComp";
            this.txtNombreComp.Size = new System.Drawing.Size(220, 20);
            this.txtNombreComp.TabIndex = 54;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Precio PJ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 56;
            this.label4.Text = "Precio competencia";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(387, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 57;
            this.label5.Text = "Nombre competencia";
            // 
            // button1
            // 
            this.button1.Image = global::Ventas.Properties.Resources.add;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(558, 129);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 28);
            this.button1.TabIndex = 58;
            this.toolTip1.SetToolTip(this.button1, "Agregar Item");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Pregunta3AD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNombreComp);
            this.Controls.Add(this.txtPrecioCompetencia);
            this.Controls.Add(this.txtPrecioPJ);
            this.Controls.Add(this.txtArticulo);
            this.Controls.Add(this.Artículo);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnFinalizar);
            this.Controls.Add(this.txtConsumo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.label1);
            this.Name = "Pregunta3AD";
            this.Size = new System.Drawing.Size(600, 522);
            this.Load += new System.EventHandler(this.Pregunta3AD_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvItems;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtConsumo;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnFinalizar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolBack;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ToolStripStatusLabel toolHome;
        private System.Windows.Forms.Label Artículo;
        private System.Windows.Forms.TextBox txtArticulo;
        private System.Windows.Forms.TextBox txtPrecioPJ;
        private System.Windows.Forms.TextBox txtPrecioCompetencia;
        private System.Windows.Forms.TextBox txtNombreComp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
