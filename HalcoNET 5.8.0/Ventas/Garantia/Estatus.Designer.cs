namespace Ventas.Garantia
{
    partial class Estatus
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
            this.rbAlta = new System.Windows.Forms.RadioButton();
            this.rbProceso = new System.Windows.Forms.RadioButton();
            this.rbRechazada = new System.Windows.Forms.RadioButton();
            this.rbAprobada = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDetalles = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rbAlta
            // 
            this.rbAlta.AccessibleName = "A";
            this.rbAlta.AutoSize = true;
            this.rbAlta.Location = new System.Drawing.Point(24, 27);
            this.rbAlta.Name = "rbAlta";
            this.rbAlta.Size = new System.Drawing.Size(43, 17);
            this.rbAlta.TabIndex = 0;
            this.rbAlta.TabStop = true;
            this.rbAlta.Text = "Alta";
            this.rbAlta.UseVisualStyleBackColor = true;
            // 
            // rbProceso
            // 
            this.rbProceso.AccessibleName = "P";
            this.rbProceso.AutoSize = true;
            this.rbProceso.Location = new System.Drawing.Point(24, 50);
            this.rbProceso.Name = "rbProceso";
            this.rbProceso.Size = new System.Drawing.Size(79, 17);
            this.rbProceso.TabIndex = 1;
            this.rbProceso.TabStop = true;
            this.rbProceso.Text = "En proceso";
            this.rbProceso.UseVisualStyleBackColor = true;
            // 
            // rbRechazada
            // 
            this.rbRechazada.AccessibleName = "R";
            this.rbRechazada.AutoSize = true;
            this.rbRechazada.Location = new System.Drawing.Point(24, 96);
            this.rbRechazada.Name = "rbRechazada";
            this.rbRechazada.Size = new System.Drawing.Size(80, 17);
            this.rbRechazada.TabIndex = 2;
            this.rbRechazada.TabStop = true;
            this.rbRechazada.Text = "Rechazada";
            this.rbRechazada.UseVisualStyleBackColor = true;
            // 
            // rbAprobada
            // 
            this.rbAprobada.AccessibleName = "B";
            this.rbAprobada.AutoSize = true;
            this.rbAprobada.Location = new System.Drawing.Point(24, 73);
            this.rbAprobada.Name = "rbAprobada";
            this.rbAprobada.Size = new System.Drawing.Size(71, 17);
            this.rbAprobada.TabIndex = 3;
            this.rbAprobada.TabStop = true;
            this.rbAprobada.Text = "Aprobada";
            this.rbAprobada.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Selecciona un estatus:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(166, 215);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(59, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Dictamen";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView
            // 
            this.listView.Location = new System.Drawing.Point(12, 137);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(170, 20);
            this.listView.TabIndex = 9;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.List;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Detalles:";
            // 
            // txtDetalles
            // 
            this.txtDetalles.Location = new System.Drawing.Point(60, 166);
            this.txtDetalles.MaxLength = 500;
            this.txtDetalles.Name = "txtDetalles";
            this.txtDetalles.Size = new System.Drawing.Size(165, 43);
            this.txtDetalles.TabIndex = 12;
            this.txtDetalles.Text = "";
            // 
            // Estatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 247);
            this.Controls.Add(this.txtDetalles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbAprobada);
            this.Controls.Add(this.rbRechazada);
            this.Controls.Add(this.rbProceso);
            this.Controls.Add(this.rbAlta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Estatus";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Estatus";
            this.Load += new System.EventHandler(this.Estatus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbAlta;
        private System.Windows.Forms.RadioButton rbProceso;
        private System.Windows.Forms.RadioButton rbRechazada;
        private System.Windows.Forms.RadioButton rbAprobada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtDetalles;
    }
}