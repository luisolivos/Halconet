namespace Cobranza
{
    partial class frmMttoBancos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMttoBancos));
            this.splitContainerPrincipal = new System.Windows.Forms.SplitContainer();
            this.btnUpdateMantenimiento = new System.Windows.Forms.Button();
            this.cmbMoneda = new System.Windows.Forms.ComboBox();
            this.btnAddMantenimiento = new System.Windows.Forms.Button();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.txtGarantias = new System.Windows.Forms.TextBox();
            this.txtOSolidarios = new System.Windows.Forms.TextBox();
            this.txtCosto = new System.Windows.Forms.TextBox();
            this.txtLineaAut = new System.Windows.Forms.TextBox();
            this.txtTasa = new System.Windows.Forms.TextBox();
            this.txtTipoCredito = new System.Windows.Forms.TextBox();
            this.txtBanco = new System.Windows.Forms.TextBox();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.lblCosto = new System.Windows.Forms.Label();
            this.lblOSolidarios = new System.Windows.Forms.Label();
            this.lblGarantias = new System.Windows.Forms.Label();
            this.lblLineaAut = new System.Windows.Forms.Label();
            this.lblMoneda = new System.Windows.Forms.Label();
            this.lblTasa = new System.Windows.Forms.Label();
            this.lblTipoCredito = new System.Windows.Forms.Label();
            this.lblBanco = new System.Windows.Forms.Label();
            this.dgvVistaRegistros = new System.Windows.Forms.DataGridView();
            this.cmsMenuContextual = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNuevoMantenimiento = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).BeginInit();
            this.splitContainerPrincipal.Panel1.SuspendLayout();
            this.splitContainerPrincipal.Panel2.SuspendLayout();
            this.splitContainerPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVistaRegistros)).BeginInit();
            this.cmsMenuContextual.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerPrincipal
            // 
            this.splitContainerPrincipal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerPrincipal.Location = new System.Drawing.Point(12, 12);
            this.splitContainerPrincipal.Name = "splitContainerPrincipal";
            this.splitContainerPrincipal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerPrincipal.Panel1
            // 
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnNuevoMantenimiento);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnUpdateMantenimiento);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.cmbMoneda);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.btnAddMantenimiento);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtCuenta);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtGarantias);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtOSolidarios);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtCosto);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtLineaAut);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtTasa);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtTipoCredito);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.txtBanco);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblCuenta);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblCosto);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblOSolidarios);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblGarantias);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblLineaAut);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblMoneda);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblTasa);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblTipoCredito);
            this.splitContainerPrincipal.Panel1.Controls.Add(this.lblBanco);
            // 
            // splitContainerPrincipal.Panel2
            // 
            this.splitContainerPrincipal.Panel2.Controls.Add(this.dgvVistaRegistros);
            this.splitContainerPrincipal.Size = new System.Drawing.Size(924, 421);
            this.splitContainerPrincipal.SplitterDistance = 169;
            this.splitContainerPrincipal.TabIndex = 2;
            // 
            // btnUpdateMantenimiento
            // 
            this.btnUpdateMantenimiento.Location = new System.Drawing.Point(688, 67);
            this.btnUpdateMantenimiento.Name = "btnUpdateMantenimiento";
            this.btnUpdateMantenimiento.Size = new System.Drawing.Size(88, 23);
            this.btnUpdateMantenimiento.TabIndex = 10;
            this.btnUpdateMantenimiento.Text = "Guardar";
            this.btnUpdateMantenimiento.UseVisualStyleBackColor = true;
            this.btnUpdateMantenimiento.Visible = false;
            this.btnUpdateMantenimiento.Click += new System.EventHandler(this.btnUpdateMantenimiento_Click);
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoneda.FormattingEnabled = true;
            this.cmbMoneda.Location = new System.Drawing.Point(109, 92);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(221, 21);
            this.cmbMoneda.TabIndex = 3;
            // 
            // btnAddMantenimiento
            // 
            this.btnAddMantenimiento.Location = new System.Drawing.Point(688, 111);
            this.btnAddMantenimiento.Name = "btnAddMantenimiento";
            this.btnAddMantenimiento.Size = new System.Drawing.Size(88, 23);
            this.btnAddMantenimiento.TabIndex = 9;
            this.btnAddMantenimiento.Text = "Guardar";
            this.btnAddMantenimiento.UseVisualStyleBackColor = true;
            this.btnAddMantenimiento.Click += new System.EventHandler(this.btnAddMantenimiento_Click);
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(744, 7);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(131, 20);
            this.txtCuenta.TabIndex = 8;
            // 
            // txtGarantias
            // 
            this.txtGarantias.Location = new System.Drawing.Point(451, 41);
            this.txtGarantias.Multiline = true;
            this.txtGarantias.Name = "txtGarantias";
            this.txtGarantias.Size = new System.Drawing.Size(221, 43);
            this.txtGarantias.TabIndex = 6;
            // 
            // txtOSolidarios
            // 
            this.txtOSolidarios.Location = new System.Drawing.Point(451, 94);
            this.txtOSolidarios.Multiline = true;
            this.txtOSolidarios.Name = "txtOSolidarios";
            this.txtOSolidarios.Size = new System.Drawing.Size(221, 45);
            this.txtOSolidarios.TabIndex = 7;
            // 
            // txtCosto
            // 
            this.txtCosto.Location = new System.Drawing.Point(451, 7);
            this.txtCosto.Name = "txtCosto";
            this.txtCosto.Size = new System.Drawing.Size(221, 20);
            this.txtCosto.TabIndex = 5;
            // 
            // txtLineaAut
            // 
            this.txtLineaAut.Location = new System.Drawing.Point(109, 119);
            this.txtLineaAut.Name = "txtLineaAut";
            this.txtLineaAut.Size = new System.Drawing.Size(221, 20);
            this.txtLineaAut.TabIndex = 4;
            // 
            // txtTasa
            // 
            this.txtTasa.Location = new System.Drawing.Point(109, 64);
            this.txtTasa.Name = "txtTasa";
            this.txtTasa.Size = new System.Drawing.Size(221, 20);
            this.txtTasa.TabIndex = 2;
            // 
            // txtTipoCredito
            // 
            this.txtTipoCredito.Location = new System.Drawing.Point(109, 36);
            this.txtTipoCredito.Name = "txtTipoCredito";
            this.txtTipoCredito.Size = new System.Drawing.Size(221, 20);
            this.txtTipoCredito.TabIndex = 1;
            // 
            // txtBanco
            // 
            this.txtBanco.Location = new System.Drawing.Point(109, 8);
            this.txtBanco.Name = "txtBanco";
            this.txtBanco.Size = new System.Drawing.Size(221, 20);
            this.txtBanco.TabIndex = 0;
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Location = new System.Drawing.Point(697, 10);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(41, 13);
            this.lblCuenta.TabIndex = 8;
            this.lblCuenta.Text = "Cuenta";
            // 
            // lblCosto
            // 
            this.lblCosto.AutoSize = true;
            this.lblCosto.Location = new System.Drawing.Point(352, 10);
            this.lblCosto.Name = "lblCosto";
            this.lblCosto.Size = new System.Drawing.Size(34, 13);
            this.lblCosto.TabIndex = 6;
            this.lblCosto.Text = "Costo";
            // 
            // lblOSolidarios
            // 
            this.lblOSolidarios.AutoSize = true;
            this.lblOSolidarios.Location = new System.Drawing.Point(349, 111);
            this.lblOSolidarios.Name = "lblOSolidarios";
            this.lblOSolidarios.Size = new System.Drawing.Size(100, 13);
            this.lblOSolidarios.TabIndex = 7;
            this.lblOSolidarios.Text = "Obligados solidarios";
            // 
            // lblGarantias
            // 
            this.lblGarantias.AutoSize = true;
            this.lblGarantias.Location = new System.Drawing.Point(352, 53);
            this.lblGarantias.Name = "lblGarantias";
            this.lblGarantias.Size = new System.Drawing.Size(52, 13);
            this.lblGarantias.TabIndex = 5;
            this.lblGarantias.Text = "Garantias";
            // 
            // lblLineaAut
            // 
            this.lblLineaAut.AutoSize = true;
            this.lblLineaAut.Location = new System.Drawing.Point(12, 122);
            this.lblLineaAut.Name = "lblLineaAut";
            this.lblLineaAut.Size = new System.Drawing.Size(86, 13);
            this.lblLineaAut.TabIndex = 4;
            this.lblLineaAut.Text = "Linea Autorizada";
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoSize = true;
            this.lblMoneda.Location = new System.Drawing.Point(12, 94);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(46, 13);
            this.lblMoneda.TabIndex = 3;
            this.lblMoneda.Text = "Moneda";
            // 
            // lblTasa
            // 
            this.lblTasa.AutoSize = true;
            this.lblTasa.Location = new System.Drawing.Point(12, 67);
            this.lblTasa.Name = "lblTasa";
            this.lblTasa.Size = new System.Drawing.Size(31, 13);
            this.lblTasa.TabIndex = 2;
            this.lblTasa.Text = "Tasa";
            // 
            // lblTipoCredito
            // 
            this.lblTipoCredito.AutoSize = true;
            this.lblTipoCredito.Location = new System.Drawing.Point(12, 39);
            this.lblTipoCredito.Name = "lblTipoCredito";
            this.lblTipoCredito.Size = new System.Drawing.Size(79, 13);
            this.lblTipoCredito.TabIndex = 1;
            this.lblTipoCredito.Text = "Tipo de Credito";
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Location = new System.Drawing.Point(12, 10);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(38, 13);
            this.lblBanco.TabIndex = 0;
            this.lblBanco.Text = "Banco";
            // 
            // dgvVistaRegistros
            // 
            this.dgvVistaRegistros.AllowUserToAddRows = false;
            this.dgvVistaRegistros.AllowUserToDeleteRows = false;
            this.dgvVistaRegistros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVistaRegistros.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvVistaRegistros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVistaRegistros.Location = new System.Drawing.Point(4, 3);
            this.dgvVistaRegistros.Name = "dgvVistaRegistros";
            this.dgvVistaRegistros.Size = new System.Drawing.Size(915, 242);
            this.dgvVistaRegistros.TabIndex = 0;
            this.dgvVistaRegistros.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvVistaRegistros_CellMouseDown);
            // 
            // cmsMenuContextual
            // 
            this.cmsMenuContextual.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemEditar,
            this.toolStripMenuItemEliminar});
            this.cmsMenuContextual.Name = "cmsMenuContextual";
            this.cmsMenuContextual.Size = new System.Drawing.Size(118, 48);
            // 
            // toolStripMenuItemEditar
            // 
            this.toolStripMenuItemEditar.Name = "toolStripMenuItemEditar";
            this.toolStripMenuItemEditar.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemEditar.Text = "Editar";
            this.toolStripMenuItemEditar.Click += new System.EventHandler(this.toolStripMenuItemEditar_Click);
            // 
            // toolStripMenuItemEliminar
            // 
            this.toolStripMenuItemEliminar.Name = "toolStripMenuItemEliminar";
            this.toolStripMenuItemEliminar.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuItemEliminar.Text = "Eliminar";
            this.toolStripMenuItemEliminar.Click += new System.EventHandler(this.toolStripMenuItemEliminar_Click);
            // 
            // btnNuevoMantenimiento
            // 
            this.btnNuevoMantenimiento.Location = new System.Drawing.Point(825, 111);
            this.btnNuevoMantenimiento.Name = "btnNuevoMantenimiento";
            this.btnNuevoMantenimiento.Size = new System.Drawing.Size(75, 23);
            this.btnNuevoMantenimiento.TabIndex = 11;
            this.btnNuevoMantenimiento.Text = "Nuevo";
            this.btnNuevoMantenimiento.UseVisualStyleBackColor = true;
            this.btnNuevoMantenimiento.Click += new System.EventHandler(this.btnNuevoMantenimiento_Click);
            // 
            // frmMttoBancos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 445);
            this.Controls.Add(this.splitContainerPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMttoBancos";
            this.Text = "Mantenimiento Banco";
            this.Load += new System.EventHandler(this.frmMttoBancos_Load);
            this.splitContainerPrincipal.Panel1.ResumeLayout(false);
            this.splitContainerPrincipal.Panel1.PerformLayout();
            this.splitContainerPrincipal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPrincipal)).EndInit();
            this.splitContainerPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVistaRegistros)).EndInit();
            this.cmsMenuContextual.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerPrincipal;
        private System.Windows.Forms.ComboBox cmbMoneda;
        private System.Windows.Forms.Button btnAddMantenimiento;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.TextBox txtGarantias;
        private System.Windows.Forms.TextBox txtOSolidarios;
        private System.Windows.Forms.TextBox txtCosto;
        private System.Windows.Forms.TextBox txtLineaAut;
        private System.Windows.Forms.TextBox txtTasa;
        private System.Windows.Forms.TextBox txtTipoCredito;
        private System.Windows.Forms.TextBox txtBanco;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.Label lblCosto;
        private System.Windows.Forms.Label lblOSolidarios;
        private System.Windows.Forms.Label lblGarantias;
        private System.Windows.Forms.Label lblLineaAut;
        private System.Windows.Forms.Label lblMoneda;
        private System.Windows.Forms.Label lblTasa;
        private System.Windows.Forms.Label lblTipoCredito;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.DataGridView dgvVistaRegistros;
        private System.Windows.Forms.ContextMenuStrip cmsMenuContextual;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEliminar;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEditar;
        private System.Windows.Forms.Button btnUpdateMantenimiento;
        private System.Windows.Forms.Button btnNuevoMantenimiento;
    }
}