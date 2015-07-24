namespace Ventas.KPIS
{
    partial class frmScoreCardProgsHalcon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmScoreCardProgsHalcon));
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dgvResultado = new System.Windows.Forms.DataGridView();
            this.dtpFeha = new System.Windows.Forms.DateTimePicker();
            this.lblFecha = new System.Windows.Forms.Label();
            this.dgvTotales = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultado)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotales)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(163, 15);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 32);
            this.btnConsultar.TabIndex = 0;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // dgvResultado
            // 
            this.dgvResultado.AllowUserToAddRows = false;
            this.dgvResultado.AllowUserToDeleteRows = false;
            this.dgvResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResultado.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResultado.EnableHeadersVisualStyles = false;
            this.dgvResultado.Location = new System.Drawing.Point(13, 139);
            this.dgvResultado.Name = "dgvResultado";
            this.dgvResultado.Size = new System.Drawing.Size(803, 415);
            this.dgvResultado.TabIndex = 1;
            this.dgvResultado.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvResultado_DataBindingComplete);
            // 
            // dtpFeha
            // 
            this.dtpFeha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFeha.Location = new System.Drawing.Point(56, 21);
            this.dtpFeha.Name = "dtpFeha";
            this.dtpFeha.Size = new System.Drawing.Size(101, 20);
            this.dtpFeha.TabIndex = 2;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(13, 26);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(37, 13);
            this.lblFecha.TabIndex = 3;
            this.lblFecha.Text = "Fecha";
            // 
            // dgvTotales
            // 
            this.dgvTotales.AllowUserToAddRows = false;
            this.dgvTotales.AllowUserToDeleteRows = false;
            this.dgvTotales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTotales.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvTotales.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTotales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTotales.EnableHeadersVisualStyles = false;
            this.dgvTotales.Location = new System.Drawing.Point(343, 26);
            this.dgvTotales.Name = "dgvTotales";
            this.dgvTotales.Size = new System.Drawing.Size(473, 107);
            this.dgvTotales.TabIndex = 4;
            this.dgvTotales.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvTotales_DataBindingComplete);
            // 
            // frmScoreCardProgsHalcon
            // 
            this.AccessibleDescription = "Scorecard Programas Halcon";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 566);
            this.Controls.Add(this.dgvTotales);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpFeha);
            this.Controls.Add(this.dgvResultado);
            this.Controls.Add(this.btnConsultar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmScoreCardProgsHalcon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScoreCard Programas Halcon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmScoreCardProgsHalcon_FormClosing);
            this.Load += new System.EventHandler(this.frmScoreCardProgsHalcon_Load);
            this.Shown += new System.EventHandler(this.frmScoreCardProgsHalcon_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultado)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTotales)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView dgvResultado;
        private System.Windows.Forms.DateTimePicker dtpFeha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.DataGridView dgvTotales;
    }
}