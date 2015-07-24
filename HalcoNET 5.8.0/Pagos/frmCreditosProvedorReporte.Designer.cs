namespace Pagos
{
    partial class frmCreditosProvedorReporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreditosProvedorReporte));
            this.dgvCPRResultado = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCPRResultado)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCPRResultado
            // 
            this.dgvCPRResultado.AllowUserToAddRows = false;
            this.dgvCPRResultado.AllowUserToDeleteRows = false;
            this.dgvCPRResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCPRResultado.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvCPRResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCPRResultado.EnableHeadersVisualStyles = false;
            this.dgvCPRResultado.Location = new System.Drawing.Point(13, 46);
            this.dgvCPRResultado.Name = "dgvCPRResultado";
            this.dgvCPRResultado.Size = new System.Drawing.Size(822, 471);
            this.dgvCPRResultado.TabIndex = 0;
            this.dgvCPRResultado.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCPRResultado_CellContentClick);
            this.dgvCPRResultado.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCPRResultado_CellContentDoubleClick);
            this.dgvCPRResultado.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvCPRResultado_CellPainting);
            this.dgvCPRResultado.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCPRResultado_DataBindingComplete);
            // 
            // frmCreditosProvedorReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 529);
            this.Controls.Add(this.dgvCPRResultado);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCreditosProvedorReporte";
            this.Text = "Créditos Provedor Reporte";
            this.Load += new System.EventHandler(this.frmCreditosProvedorReporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCPRResultado)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCPRResultado;
    }
}