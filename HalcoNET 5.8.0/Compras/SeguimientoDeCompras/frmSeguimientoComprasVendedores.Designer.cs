namespace Compras
{
    partial class SeguimientoComprasVendedores
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeguimientoComprasVendedores));
            this.gridClasificacion = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridClasificacion)).BeginInit();
            this.SuspendLayout();
            // 
            // gridClasificacion
            // 
            this.gridClasificacion.AllowDrop = true;
            this.gridClasificacion.AllowUserToAddRows = false;
            this.gridClasificacion.AllowUserToDeleteRows = false;
            this.gridClasificacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridClasificacion.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridClasificacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridClasificacion.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.gridClasificacion.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridClasificacion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridClasificacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridClasificacion.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridClasificacion.EnableHeadersVisualStyles = false;
            this.gridClasificacion.Location = new System.Drawing.Point(12, 39);
            this.gridClasificacion.Name = "gridClasificacion";
            this.gridClasificacion.ReadOnly = true;
            this.gridClasificacion.Size = new System.Drawing.Size(701, 437);
            this.gridClasificacion.TabIndex = 4;
            this.gridClasificacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // SeguimientoComprasVendedores
            // 
            this.AccessibleDescription = "Seg. Compras Vendedor";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 488);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridClasificacion);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SeguimientoComprasVendedores";
            this.Text = "PPC y Compras especiales pendientes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SeguimientoComprasVendedores_FormClosing);
            this.Load += new System.EventHandler(this.SeguimientoComprasVendedores_Load);
            this.Shown += new System.EventHandler(this.SeguimientoComprasVendedores_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.gridClasificacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridClasificacion;
        private System.Windows.Forms.Label label1;
    }
}