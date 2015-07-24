namespace Pagos
{
    partial class TemplatePagosProv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplatePagosProv));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGastos = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.txtFolio = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.cbCtaContable = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFechaTrans = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.txtNombre = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.cbFechaCont = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.label32 = new System.Windows.Forms.Label();
            this.dgvGastos = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1.SuspendLayout();
            this.tpGastos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCtaContable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpGastos);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(4, 44);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(921, 589);
            this.tabControl1.TabIndex = 10;
            // 
            // tpGastos
            // 
            this.tpGastos.AutoScroll = true;
            this.tpGastos.Controls.Add(this.button1);
            this.tpGastos.Controls.Add(this.txtFolio);
            this.tpGastos.Controls.Add(this.label2);
            this.tpGastos.Controls.Add(this.label41);
            this.tpGastos.Controls.Add(this.cbCtaContable);
            this.tpGastos.Controls.Add(this.label1);
            this.tpGastos.Controls.Add(this.cbFechaTrans);
            this.tpGastos.Controls.Add(this.txtNombre);
            this.tpGastos.Controls.Add(this.label38);
            this.tpGastos.Controls.Add(this.cbFechaCont);
            this.tpGastos.Controls.Add(this.label32);
            this.tpGastos.Controls.Add(this.dgvGastos);
            this.tpGastos.ImageIndex = 1;
            this.tpGastos.Location = new System.Drawing.Point(4, 4);
            this.tpGastos.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.tpGastos.Name = "tpGastos";
            this.tpGastos.Size = new System.Drawing.Size(913, 563);
            this.tpGastos.TabIndex = 6;
            this.tpGastos.Text = "Template";
            this.tpGastos.UseVisualStyleBackColor = true;
            this.tpGastos.Click += new System.EventHandler(this.tpGastos_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Image = global::Pagos.Properties.Resources.procesDirectory;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(818, 498);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 55);
            this.button1.TabIndex = 130;
            this.button1.Text = "Generar template";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFolio
            // 
            this.txtFolio.Location = new System.Drawing.Point(4, 29);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.ReadOnly = true;
            this.txtFolio.Size = new System.Drawing.Size(67, 21);
            this.txtFolio.StateActive.Back.Color1 = System.Drawing.Color.White;
            this.txtFolio.StateActive.Border.Color1 = System.Drawing.Color.Black;
            this.txtFolio.StateActive.Border.Color2 = System.Drawing.Color.Black;
            this.txtFolio.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtFolio.StateActive.Border.Rounding = 3;
            this.txtFolio.StateActive.Content.Color1 = System.Drawing.Color.Black;
            this.txtFolio.StateActive.Content.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFolio.StateDisabled.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.StateNormal.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFolio.TabIndex = 129;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 128;
            this.label2.Text = "Folio SAP";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.BackColor = System.Drawing.Color.White;
            this.label41.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.Location = new System.Drawing.Point(550, 11);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(96, 15);
            this.label41.TabIndex = 127;
            this.label41.Text = "Cuenta contable";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbCtaContable
            // 
            this.cbCtaContable.DropButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Standalone;
            this.cbCtaContable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCtaContable.DropDownWidth = 400;
            this.cbCtaContable.Location = new System.Drawing.Point(533, 29);
            this.cbCtaContable.Name = "cbCtaContable";
            this.cbCtaContable.Size = new System.Drawing.Size(130, 21);
            this.cbCtaContable.TabIndex = 126;
            this.cbCtaContable.SelectionChangeCommitted += new System.EventHandler(this.cbCtaContable_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(397, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 15);
            this.label1.TabIndex = 125;
            this.label1.Text = "Fecha de transferencia";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbFechaTrans
            // 
            this.cbFechaTrans.CalendarTodayDate = new System.DateTime(2014, 11, 5, 0, 0, 0, 0);
            this.cbFechaTrans.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cbFechaTrans.Location = new System.Drawing.Point(399, 29);
            this.cbFechaTrans.Name = "cbFechaTrans";
            this.cbFechaTrans.Size = new System.Drawing.Size(126, 21);
            this.cbFechaTrans.TabIndex = 124;
            this.cbFechaTrans.ValueChanged += new System.EventHandler(this.cbFechaTrans_ValueChanged);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(77, 29);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(185, 21);
            this.txtNombre.StateActive.Back.Color1 = System.Drawing.Color.White;
            this.txtNombre.StateActive.Border.Color1 = System.Drawing.Color.Black;
            this.txtNombre.StateActive.Border.Color2 = System.Drawing.Color.Black;
            this.txtNombre.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtNombre.StateActive.Border.Rounding = 3;
            this.txtNombre.StateActive.Content.Color1 = System.Drawing.Color.Black;
            this.txtNombre.StateActive.Content.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.StateCommon.Content.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNombre.StateDisabled.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.StateNormal.Content.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.TabIndex = 123;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.BackColor = System.Drawing.Color.White;
            this.label38.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(271, 11);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(120, 15);
            this.label38.TabIndex = 120;
            this.label38.Text = "Fecha contabilización";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbFechaCont
            // 
            this.cbFechaCont.CalendarTodayDate = new System.DateTime(2014, 11, 5, 0, 0, 0, 0);
            this.cbFechaCont.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cbFechaCont.Location = new System.Drawing.Point(273, 29);
            this.cbFechaCont.Name = "cbFechaCont";
            this.cbFechaCont.Size = new System.Drawing.Size(117, 21);
            this.cbFechaCont.TabIndex = 112;
            this.cbFechaCont.ValueChanged += new System.EventHandler(this.cbFechaCont_ValueChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.BackColor = System.Drawing.Color.White;
            this.label32.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(107, 11);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(114, 15);
            this.label32.TabIndex = 115;
            this.label32.Text = "Nombre proveedor";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvGastos
            // 
            this.dgvGastos.AllowUserToAddRows = false;
            this.dgvGastos.AllowUserToDeleteRows = false;
            this.dgvGastos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGastos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGastos.GridStyles.Style = ComponentFactory.Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dgvGastos.GridStyles.StyleBackground = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ContextMenuHeading;
            this.dgvGastos.Location = new System.Drawing.Point(3, 56);
            this.dgvGastos.Name = "dgvGastos";
            this.dgvGastos.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvGastos.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGastos.Size = new System.Drawing.Size(890, 432);
            this.dgvGastos.TabIndex = 8;
            this.dgvGastos.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGastos_CellMouseUp);
            this.dgvGastos.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGastos_RowEnter);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 636);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(926, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TemplatePagosProv
            // 
            this.AccessibleDescription = "Template pagos proveedores";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 658);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TemplatePagosProv";
            this.Text = "Template pagos proveedores";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TemplatePagosProv_FormClosing);
            this.Load += new System.EventHandler(this.TemplatePagosProv_Load);
            this.Shown += new System.EventHandler(this.TemplatePagosProv_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tpGastos.ResumeLayout(false);
            this.tpGastos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbCtaContable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGastos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGastos;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvGastos;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtNombre;
        private System.Windows.Forms.Label label38;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker cbFechaCont;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker cbFechaTrans;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtFolio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label41;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbCtaContable;
        private System.Windows.Forms.Button button1;
    }
}