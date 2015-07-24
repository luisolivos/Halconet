namespace Sistemas
{
    partial class frmReportePendientesABM
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
            this.contextMenuStripAcciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlPages = new System.Windows.Forms.TabControl();
            this.tabPageABM = new System.Windows.Forms.TabPage();
            this.chkEstatus = new System.Windows.Forms.CheckBox();
            this.dgvShowDatos = new System.Windows.Forms.DataGridView();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.maskedTxtFechaConclusion = new System.Windows.Forms.MaskedTextBox();
            this.maskedTxtFechaSolicitud = new System.Windows.Forms.MaskedTextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.txtSituacion = new System.Windows.Forms.TextBox();
            this.txtSolicitadoPor = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtProyecto = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.lblSituacion = new System.Windows.Forms.Label();
            this.lblFechaConclusion = new System.Windows.Forms.Label();
            this.lblFechaSolicitud = new System.Windows.Forms.Label();
            this.lblSolicitadoPor = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.lblProyecto = new System.Windows.Forms.Label();
            this.tabPageConsultas = new System.Windows.Forms.TabPage();
            this.btnBuscarByFiltro = new System.Windows.Forms.Button();
            this.dgvShowDatosResultado = new System.Windows.Forms.DataGridView();
            this.grpbFiltros = new System.Windows.Forms.GroupBox();
            this.cmbFiltros = new System.Windows.Forms.ComboBox();
            this.contextMenuStripAcciones.SuspendLayout();
            this.tabControlPages.SuspendLayout();
            this.tabPageABM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowDatos)).BeginInit();
            this.tabPageConsultas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowDatosResultado)).BeginInit();
            this.grpbFiltros.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripAcciones
            // 
            this.contextMenuStripAcciones.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuStripAcciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editarToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.contextMenuStripAcciones.Name = "contextMenuStripAcciones";
            this.contextMenuStripAcciones.Size = new System.Drawing.Size(118, 48);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.editarToolStripMenuItem.Text = "Editar";
            this.editarToolStripMenuItem.Click += new System.EventHandler(this.editarToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // tabControlPages
            // 
            this.tabControlPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPages.Controls.Add(this.tabPageABM);
            this.tabControlPages.Controls.Add(this.tabPageConsultas);
            this.tabControlPages.Location = new System.Drawing.Point(13, 13);
            this.tabControlPages.Name = "tabControlPages";
            this.tabControlPages.SelectedIndex = 0;
            this.tabControlPages.Size = new System.Drawing.Size(953, 437);
            this.tabControlPages.TabIndex = 1;
            // 
            // tabPageABM
            // 
            this.tabPageABM.Controls.Add(this.chkEstatus);
            this.tabPageABM.Controls.Add(this.dgvShowDatos);
            this.tabPageABM.Controls.Add(this.btnActualizar);
            this.tabPageABM.Controls.Add(this.maskedTxtFechaConclusion);
            this.tabPageABM.Controls.Add(this.maskedTxtFechaSolicitud);
            this.tabPageABM.Controls.Add(this.btnAgregar);
            this.tabPageABM.Controls.Add(this.btnNuevo);
            this.tabPageABM.Controls.Add(this.txtObservaciones);
            this.tabPageABM.Controls.Add(this.txtSituacion);
            this.tabPageABM.Controls.Add(this.txtSolicitadoPor);
            this.tabPageABM.Controls.Add(this.txtDescripcion);
            this.tabPageABM.Controls.Add(this.txtProyecto);
            this.tabPageABM.Controls.Add(this.lblObservaciones);
            this.tabPageABM.Controls.Add(this.lblSituacion);
            this.tabPageABM.Controls.Add(this.lblFechaConclusion);
            this.tabPageABM.Controls.Add(this.lblFechaSolicitud);
            this.tabPageABM.Controls.Add(this.lblSolicitadoPor);
            this.tabPageABM.Controls.Add(this.lblDescripcion);
            this.tabPageABM.Controls.Add(this.lblProyecto);
            this.tabPageABM.Location = new System.Drawing.Point(4, 22);
            this.tabPageABM.Name = "tabPageABM";
            this.tabPageABM.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageABM.Size = new System.Drawing.Size(945, 411);
            this.tabPageABM.TabIndex = 0;
            this.tabPageABM.Text = "Altas/Modificaciones";
            this.tabPageABM.UseVisualStyleBackColor = true;
            // 
            // chkEstatus
            // 
            this.chkEstatus.AutoSize = true;
            this.chkEstatus.Location = new System.Drawing.Point(16, 217);
            this.chkEstatus.Name = "chkEstatus";
            this.chkEstatus.Size = new System.Drawing.Size(73, 17);
            this.chkEstatus.TabIndex = 42;
            this.chkEstatus.Text = "Finalizado";
            this.chkEstatus.UseVisualStyleBackColor = true;
            // 
            // dgvShowDatos
            // 
            this.dgvShowDatos.AllowUserToAddRows = false;
            this.dgvShowDatos.AllowUserToDeleteRows = false;
            this.dgvShowDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShowDatos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvShowDatos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShowDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowDatos.Location = new System.Drawing.Point(16, 255);
            this.dgvShowDatos.Name = "dgvShowDatos";
            this.dgvShowDatos.Size = new System.Drawing.Size(923, 150);
            this.dgvShowDatos.TabIndex = 40;
            this.dgvShowDatos.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvShowDatos_CellMouseDown);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(445, 217);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 41;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // maskedTxtFechaConclusion
            // 
            this.maskedTxtFechaConclusion.Location = new System.Drawing.Point(546, 61);
            this.maskedTxtFechaConclusion.Mask = "00/00/0000";
            this.maskedTxtFechaConclusion.Name = "maskedTxtFechaConclusion";
            this.maskedTxtFechaConclusion.Size = new System.Drawing.Size(162, 20);
            this.maskedTxtFechaConclusion.TabIndex = 33;
            this.maskedTxtFechaConclusion.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTxtFechaSolicitud
            // 
            this.maskedTxtFechaSolicitud.Location = new System.Drawing.Point(546, 9);
            this.maskedTxtFechaSolicitud.Mask = "00/00/0000";
            this.maskedTxtFechaSolicitud.Name = "maskedTxtFechaSolicitud";
            this.maskedTxtFechaSolicitud.Size = new System.Drawing.Size(162, 20);
            this.maskedTxtFechaSolicitud.TabIndex = 31;
            this.maskedTxtFechaSolicitud.ValidatingType = typeof(System.DateTime);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(633, 217);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 38;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(535, 217);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 23);
            this.btnNuevo.TabIndex = 39;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(491, 143);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(218, 55);
            this.txtObservaciones.TabIndex = 37;
            // 
            // txtSituacion
            // 
            this.txtSituacion.Location = new System.Drawing.Point(491, 106);
            this.txtSituacion.Name = "txtSituacion";
            this.txtSituacion.Size = new System.Drawing.Size(218, 20);
            this.txtSituacion.TabIndex = 34;
            // 
            // txtSolicitadoPor
            // 
            this.txtSolicitadoPor.Location = new System.Drawing.Point(100, 178);
            this.txtSolicitadoPor.Name = "txtSolicitadoPor";
            this.txtSolicitadoPor.Size = new System.Drawing.Size(218, 20);
            this.txtSolicitadoPor.TabIndex = 28;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(100, 65);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(218, 83);
            this.txtDescripcion.TabIndex = 26;
            // 
            // txtProyecto
            // 
            this.txtProyecto.Location = new System.Drawing.Point(100, 14);
            this.txtProyecto.Name = "txtProyecto";
            this.txtProyecto.Size = new System.Drawing.Size(218, 20);
            this.txtProyecto.TabIndex = 24;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(387, 163);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(78, 13);
            this.lblObservaciones.TabIndex = 36;
            this.lblObservaciones.Text = "Observaciones";
            // 
            // lblSituacion
            // 
            this.lblSituacion.AutoSize = true;
            this.lblSituacion.Location = new System.Drawing.Point(387, 109);
            this.lblSituacion.Name = "lblSituacion";
            this.lblSituacion.Size = new System.Drawing.Size(51, 13);
            this.lblSituacion.TabIndex = 35;
            this.lblSituacion.Text = "Situación";
            // 
            // lblFechaConclusion
            // 
            this.lblFechaConclusion.AutoSize = true;
            this.lblFechaConclusion.Location = new System.Drawing.Point(388, 68);
            this.lblFechaConclusion.Name = "lblFechaConclusion";
            this.lblFechaConclusion.Size = new System.Drawing.Size(155, 13);
            this.lblFechaConclusion.TabIndex = 32;
            this.lblFechaConclusion.Text = "Fecha Tentativa de Conclusión";
            // 
            // lblFechaSolicitud
            // 
            this.lblFechaSolicitud.AutoSize = true;
            this.lblFechaSolicitud.Location = new System.Drawing.Point(389, 17);
            this.lblFechaSolicitud.Name = "lblFechaSolicitud";
            this.lblFechaSolicitud.Size = new System.Drawing.Size(80, 13);
            this.lblFechaSolicitud.TabIndex = 30;
            this.lblFechaSolicitud.Text = "Fecha Solicitud";
            // 
            // lblSolicitadoPor
            // 
            this.lblSolicitadoPor.AutoSize = true;
            this.lblSolicitadoPor.Location = new System.Drawing.Point(16, 181);
            this.lblSolicitadoPor.Name = "lblSolicitadoPor";
            this.lblSolicitadoPor.Size = new System.Drawing.Size(72, 13);
            this.lblSolicitadoPor.TabIndex = 29;
            this.lblSolicitadoPor.Text = "Solicitado Por";
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(16, 102);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(63, 13);
            this.lblDescripcion.TabIndex = 27;
            this.lblDescripcion.Text = "Descripción";
            // 
            // lblProyecto
            // 
            this.lblProyecto.AutoSize = true;
            this.lblProyecto.Location = new System.Drawing.Point(16, 22);
            this.lblProyecto.Name = "lblProyecto";
            this.lblProyecto.Size = new System.Drawing.Size(49, 13);
            this.lblProyecto.TabIndex = 25;
            this.lblProyecto.Text = "Proyecto";
            // 
            // tabPageConsultas
            // 
            this.tabPageConsultas.Controls.Add(this.btnBuscarByFiltro);
            this.tabPageConsultas.Controls.Add(this.dgvShowDatosResultado);
            this.tabPageConsultas.Controls.Add(this.grpbFiltros);
            this.tabPageConsultas.Location = new System.Drawing.Point(4, 22);
            this.tabPageConsultas.Name = "tabPageConsultas";
            this.tabPageConsultas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsultas.Size = new System.Drawing.Size(945, 411);
            this.tabPageConsultas.TabIndex = 1;
            this.tabPageConsultas.Text = "Consultas";
            this.tabPageConsultas.UseVisualStyleBackColor = true;
            // 
            // btnBuscarByFiltro
            // 
            this.btnBuscarByFiltro.Location = new System.Drawing.Point(270, 28);
            this.btnBuscarByFiltro.Name = "btnBuscarByFiltro";
            this.btnBuscarByFiltro.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarByFiltro.TabIndex = 4;
            this.btnBuscarByFiltro.Text = "Buscar";
            this.btnBuscarByFiltro.UseVisualStyleBackColor = true;
            this.btnBuscarByFiltro.Click += new System.EventHandler(this.btnBuscarByFiltro_Click);
            // 
            // dgvShowDatosResultado
            // 
            this.dgvShowDatosResultado.AllowUserToAddRows = false;
            this.dgvShowDatosResultado.AllowUserToDeleteRows = false;
            this.dgvShowDatosResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShowDatosResultado.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvShowDatosResultado.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShowDatosResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowDatosResultado.Location = new System.Drawing.Point(6, 93);
            this.dgvShowDatosResultado.Name = "dgvShowDatosResultado";
            this.dgvShowDatosResultado.Size = new System.Drawing.Size(933, 312);
            this.dgvShowDatosResultado.TabIndex = 3;
            // 
            // grpbFiltros
            // 
            this.grpbFiltros.Controls.Add(this.cmbFiltros);
            this.grpbFiltros.Location = new System.Drawing.Point(6, 9);
            this.grpbFiltros.Name = "grpbFiltros";
            this.grpbFiltros.Size = new System.Drawing.Size(231, 57);
            this.grpbFiltros.TabIndex = 2;
            this.grpbFiltros.TabStop = false;
            this.grpbFiltros.Text = "Filtros";
            // 
            // cmbFiltros
            // 
            this.cmbFiltros.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltros.FormattingEnabled = true;
            this.cmbFiltros.Location = new System.Drawing.Point(6, 19);
            this.cmbFiltros.Name = "cmbFiltros";
            this.cmbFiltros.Size = new System.Drawing.Size(208, 21);
            this.cmbFiltros.TabIndex = 0;
            // 
            // frmReportePendientesABM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 462);
            this.Controls.Add(this.tabControlPages);
            this.Name = "frmReportePendientesABM";
            this.Text = "Pendientes Reportes";
            this.Load += new System.EventHandler(this.frmReportePendientesABM_Load);
            this.contextMenuStripAcciones.ResumeLayout(false);
            this.tabControlPages.ResumeLayout(false);
            this.tabPageABM.ResumeLayout(false);
            this.tabPageABM.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowDatos)).EndInit();
            this.tabPageConsultas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowDatosResultado)).EndInit();
            this.grpbFiltros.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripAcciones;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlPages;
        private System.Windows.Forms.TabPage tabPageABM;
        private System.Windows.Forms.CheckBox chkEstatus;
        private System.Windows.Forms.DataGridView dgvShowDatos;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.MaskedTextBox maskedTxtFechaConclusion;
        private System.Windows.Forms.MaskedTextBox maskedTxtFechaSolicitud;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.TextBox txtSituacion;
        private System.Windows.Forms.TextBox txtSolicitadoPor;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtProyecto;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.Label lblSituacion;
        private System.Windows.Forms.Label lblFechaConclusion;
        private System.Windows.Forms.Label lblFechaSolicitud;
        private System.Windows.Forms.Label lblSolicitadoPor;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.Label lblProyecto;
        private System.Windows.Forms.TabPage tabPageConsultas;
        private System.Windows.Forms.DataGridView dgvShowDatosResultado;
        private System.Windows.Forms.GroupBox grpbFiltros;
        private System.Windows.Forms.ComboBox cmbFiltros;
        private System.Windows.Forms.Button btnBuscarByFiltro;
    }
}