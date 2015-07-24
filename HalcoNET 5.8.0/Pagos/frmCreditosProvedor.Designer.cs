namespace Pagos
{
    partial class frmCreditosProvedor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreditosProvedor));
            this.grpbDatos = new System.Windows.Forms.GroupBox();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.txtComentario = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.cmbMes = new System.Windows.Forms.ComboBox();
            this.cmbPeriodicidad = new System.Windows.Forms.ComboBox();
            this.txtLiberacion = new System.Windows.Forms.TextBox();
            this.cmbProvedor = new System.Windows.Forms.ComboBox();
            this.lblComentario = new System.Windows.Forms.Label();
            this.lblImporte = new System.Windows.Forms.Label();
            this.lblAnio = new System.Windows.Forms.Label();
            this.lblMes = new System.Windows.Forms.Label();
            this.lblPeriodicidad = new System.Windows.Forms.Label();
            this.lblLiberacion = new System.Windows.Forms.Label();
            this.lblProvedor = new System.Windows.Forms.Label();
            this.dgvShowDatos = new System.Windows.Forms.DataGridView();
            this.contextMenuStripAcciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.modificarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpbDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowDatos)).BeginInit();
            this.contextMenuStripAcciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpbDatos
            // 
            this.grpbDatos.Controls.Add(this.btnNuevo);
            this.grpbDatos.Controls.Add(this.btnGuardar);
            this.grpbDatos.Controls.Add(this.txtComentario);
            this.grpbDatos.Controls.Add(this.txtImporte);
            this.grpbDatos.Controls.Add(this.txtAnio);
            this.grpbDatos.Controls.Add(this.cmbMes);
            this.grpbDatos.Controls.Add(this.cmbPeriodicidad);
            this.grpbDatos.Controls.Add(this.txtLiberacion);
            this.grpbDatos.Controls.Add(this.cmbProvedor);
            this.grpbDatos.Controls.Add(this.lblComentario);
            this.grpbDatos.Controls.Add(this.lblImporte);
            this.grpbDatos.Controls.Add(this.lblAnio);
            this.grpbDatos.Controls.Add(this.lblMes);
            this.grpbDatos.Controls.Add(this.lblPeriodicidad);
            this.grpbDatos.Controls.Add(this.lblLiberacion);
            this.grpbDatos.Controls.Add(this.lblProvedor);
            this.grpbDatos.Location = new System.Drawing.Point(13, 13);
            this.grpbDatos.Name = "grpbDatos";
            this.grpbDatos.Size = new System.Drawing.Size(791, 246);
            this.grpbDatos.TabIndex = 0;
            this.grpbDatos.TabStop = false;
            this.grpbDatos.Text = "Datos";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(629, 217);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 15;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(710, 217);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 14;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtComentario
            // 
            this.txtComentario.Location = new System.Drawing.Point(389, 140);
            this.txtComentario.Multiline = true;
            this.txtComentario.Name = "txtComentario";
            this.txtComentario.Size = new System.Drawing.Size(270, 64);
            this.txtComentario.TabIndex = 13;
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(389, 76);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(128, 20);
            this.txtImporte.TabIndex = 12;
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(389, 34);
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(84, 20);
            this.txtAnio.TabIndex = 11;
            // 
            // cmbMes
            // 
            this.cmbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMes.FormattingEnabled = true;
            this.cmbMes.Location = new System.Drawing.Point(108, 169);
            this.cmbMes.Name = "cmbMes";
            this.cmbMes.Size = new System.Drawing.Size(166, 21);
            this.cmbMes.TabIndex = 10;
            // 
            // cmbPeriodicidad
            // 
            this.cmbPeriodicidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodicidad.FormattingEnabled = true;
            this.cmbPeriodicidad.Location = new System.Drawing.Point(108, 120);
            this.cmbPeriodicidad.Name = "cmbPeriodicidad";
            this.cmbPeriodicidad.Size = new System.Drawing.Size(166, 21);
            this.cmbPeriodicidad.TabIndex = 9;
            // 
            // txtLiberacion
            // 
            this.txtLiberacion.Location = new System.Drawing.Point(108, 77);
            this.txtLiberacion.Name = "txtLiberacion";
            this.txtLiberacion.Size = new System.Drawing.Size(166, 20);
            this.txtLiberacion.TabIndex = 8;
            // 
            // cmbProvedor
            // 
            this.cmbProvedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvedor.FormattingEnabled = true;
            this.cmbProvedor.Location = new System.Drawing.Point(108, 36);
            this.cmbProvedor.Name = "cmbProvedor";
            this.cmbProvedor.Size = new System.Drawing.Size(166, 21);
            this.cmbProvedor.TabIndex = 7;
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Location = new System.Drawing.Point(315, 160);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(63, 13);
            this.lblComentario.TabIndex = 6;
            this.lblComentario.Text = "Comentario:";
            // 
            // lblImporte
            // 
            this.lblImporte.AutoSize = true;
            this.lblImporte.Location = new System.Drawing.Point(315, 80);
            this.lblImporte.Name = "lblImporte";
            this.lblImporte.Size = new System.Drawing.Size(45, 13);
            this.lblImporte.TabIndex = 5;
            this.lblImporte.Text = "Importe:";
            // 
            // lblAnio
            // 
            this.lblAnio.AutoSize = true;
            this.lblAnio.Location = new System.Drawing.Point(315, 36);
            this.lblAnio.Name = "lblAnio";
            this.lblAnio.Size = new System.Drawing.Size(29, 13);
            this.lblAnio.TabIndex = 4;
            this.lblAnio.Text = "Año:";
            // 
            // lblMes
            // 
            this.lblMes.AutoSize = true;
            this.lblMes.Location = new System.Drawing.Point(20, 169);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(30, 13);
            this.lblMes.TabIndex = 3;
            this.lblMes.Text = "Mes:";
            // 
            // lblPeriodicidad
            // 
            this.lblPeriodicidad.AutoSize = true;
            this.lblPeriodicidad.Location = new System.Drawing.Point(20, 123);
            this.lblPeriodicidad.Name = "lblPeriodicidad";
            this.lblPeriodicidad.Size = new System.Drawing.Size(72, 13);
            this.lblPeriodicidad.TabIndex = 2;
            this.lblPeriodicidad.Text = "Periodocidad:";
            // 
            // lblLiberacion
            // 
            this.lblLiberacion.AutoSize = true;
            this.lblLiberacion.Location = new System.Drawing.Point(20, 80);
            this.lblLiberacion.Name = "lblLiberacion";
            this.lblLiberacion.Size = new System.Drawing.Size(59, 13);
            this.lblLiberacion.TabIndex = 1;
            this.lblLiberacion.Text = "Liberación:";
            // 
            // lblProvedor
            // 
            this.lblProvedor.AutoSize = true;
            this.lblProvedor.Location = new System.Drawing.Point(20, 38);
            this.lblProvedor.Name = "lblProvedor";
            this.lblProvedor.Size = new System.Drawing.Size(53, 13);
            this.lblProvedor.TabIndex = 0;
            this.lblProvedor.Text = "Provedor:";
            // 
            // dgvShowDatos
            // 
            this.dgvShowDatos.AllowUserToAddRows = false;
            this.dgvShowDatos.AllowUserToDeleteRows = false;
            this.dgvShowDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShowDatos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShowDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowDatos.EnableHeadersVisualStyles = false;
            this.dgvShowDatos.Location = new System.Drawing.Point(13, 275);
            this.dgvShowDatos.Name = "dgvShowDatos";
            this.dgvShowDatos.Size = new System.Drawing.Size(791, 150);
            this.dgvShowDatos.TabIndex = 1;
            this.dgvShowDatos.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvShowDatos_CellMouseDown);
            this.dgvShowDatos.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvShowDatos_DataBindingComplete);
            // 
            // contextMenuStripAcciones
            // 
            this.contextMenuStripAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modificarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStripAcciones.Name = "contextMenuStripAcciones";
            this.contextMenuStripAcciones.Size = new System.Drawing.Size(126, 48);
            // 
            // modificarToolStripMenuItem
            // 
            this.modificarToolStripMenuItem.Name = "modificarToolStripMenuItem";
            this.modificarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.modificarToolStripMenuItem.Text = "Modificar";
            this.modificarToolStripMenuItem.Click += new System.EventHandler(this.modificarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // frmCreditosProvedor
            // 
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 441);
            this.Controls.Add(this.dgvShowDatos);
            this.Controls.Add(this.grpbDatos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCreditosProvedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Creditos Provedor";
            this.Load += new System.EventHandler(this.frmCreditosProvedor_Load);
            this.grpbDatos.ResumeLayout(false);
            this.grpbDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowDatos)).EndInit();
            this.contextMenuStripAcciones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpbDatos;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.Label lblImporte;
        private System.Windows.Forms.Label lblAnio;
        private System.Windows.Forms.Label lblMes;
        private System.Windows.Forms.Label lblPeriodicidad;
        private System.Windows.Forms.Label lblLiberacion;
        private System.Windows.Forms.Label lblProvedor;
        private System.Windows.Forms.ComboBox cmbProvedor;
        private System.Windows.Forms.TextBox txtLiberacion;
        private System.Windows.Forms.ComboBox cmbPeriodicidad;
        private System.Windows.Forms.ComboBox cmbMes;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtComentario;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView dgvShowDatos;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAcciones;
        private System.Windows.Forms.ToolStripMenuItem modificarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Button btnNuevo;
    }
}