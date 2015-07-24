namespace Cobranza.Corcho
{
    partial class Causas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Causas));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEspecifique = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbOtros = new System.Windows.Forms.CheckBox();
            this.cbCancelada = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.btnDeshacer = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbOriginal = new System.Windows.Forms.CheckBox();
            this.cbBlanca = new System.Windows.Forms.CheckBox();
            this.cbCompleto = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbTCTD = new System.Windows.Forms.CheckBox();
            this.cbTransferencia = new System.Windows.Forms.CheckBox();
            this.cbCheque = new System.Windows.Forms.CheckBox();
            this.cbEfectivo = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.txtDescuentos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtEspecifique);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbOtros);
            this.groupBox1.Controls.Add(this.cbCancelada);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 304);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Causas documentación faltante";
            // 
            // txtEspecifique
            // 
            this.txtEspecifique.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEspecifique.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEspecifique.Location = new System.Drawing.Point(160, 266);
            this.txtEspecifique.MaxLength = 500;
            this.txtEspecifique.Multiline = true;
            this.txtEspecifique.Name = "txtEspecifique";
            this.txtEspecifique.Size = new System.Drawing.Size(179, 32);
            this.txtEspecifique.TabIndex = 15;
            this.toolTip1.SetToolTip(this.txtEspecifique, "Espeficifique la causa");
            this.txtEspecifique.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(83, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "Especifique:";
            this.label3.Visible = false;
            // 
            // cbOtros
            // 
            this.cbOtros.AccessibleName = "";
            this.cbOtros.AutoSize = true;
            this.cbOtros.Enabled = false;
            this.cbOtros.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOtros.Location = new System.Drawing.Point(6, 262);
            this.cbOtros.Name = "cbOtros";
            this.cbOtros.Size = new System.Drawing.Size(57, 19);
            this.cbOtros.TabIndex = 7;
            this.cbOtros.Text = "Otros";
            this.toolTip1.SetToolTip(this.cbOtros, "Seleccione en caso de que el motivo de documentación faltante  no se encuentre en" +
                    " la lista de arriba.");
            this.cbOtros.UseVisualStyleBackColor = true;
            this.cbOtros.Click += new System.EventHandler(this.cbOtros_Click);
            // 
            // cbCancelada
            // 
            this.cbCancelada.AccessibleName = "";
            this.cbCancelada.AutoSize = true;
            this.cbCancelada.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCancelada.Location = new System.Drawing.Point(6, 282);
            this.cbCancelada.Name = "cbCancelada";
            this.cbCancelada.Size = new System.Drawing.Size(84, 19);
            this.cbCancelada.TabIndex = 6;
            this.cbCancelada.Text = "Cancelada";
            this.cbCancelada.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 231);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Image = global::Cobranza.Properties.Resources.save;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(81, 436);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "     Guardar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.label1.Location = new System.Drawing.Point(87, 332);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Responsable:";
            this.label1.Visible = false;
            // 
            // txtComentarios
            // 
            this.txtComentarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComentarios.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComentarios.Location = new System.Drawing.Point(99, 410);
            this.txtComentarios.MaxLength = 500;
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(252, 23);
            this.txtComentarios.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtComentarios, "¿Por qué se quita la factura del corcho?");
            this.txtComentarios.Visible = false;
            // 
            // btnDeshacer
            // 
            this.btnDeshacer.Image = global::Cobranza.Properties.Resources.undo;
            this.btnDeshacer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeshacer.Location = new System.Drawing.Point(173, 436);
            this.btnDeshacer.Name = "btnDeshacer";
            this.btnDeshacer.Size = new System.Drawing.Size(86, 25);
            this.btnDeshacer.TabIndex = 4;
            this.btnDeshacer.Text = "      Deshacer";
            this.btnDeshacer.UseVisualStyleBackColor = true;
            this.btnDeshacer.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 69);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Documentación";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.cbOriginal);
            this.panel2.Controls.Add(this.cbBlanca);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(126, 44);
            this.panel2.TabIndex = 0;
            // 
            // cbOriginal
            // 
            this.cbOriginal.AccessibleName = "0";
            this.cbOriginal.AutoSize = true;
            this.cbOriginal.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOriginal.Location = new System.Drawing.Point(9, 1);
            this.cbOriginal.Name = "cbOriginal";
            this.cbOriginal.Size = new System.Drawing.Size(72, 19);
            this.cbOriginal.TabIndex = 0;
            this.cbOriginal.Text = "Original";
            this.cbOriginal.UseVisualStyleBackColor = true;
            // 
            // cbBlanca
            // 
            this.cbBlanca.AccessibleName = "1";
            this.cbBlanca.AutoSize = true;
            this.cbBlanca.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBlanca.Location = new System.Drawing.Point(9, 21);
            this.cbBlanca.Name = "cbBlanca";
            this.cbBlanca.Size = new System.Drawing.Size(64, 19);
            this.cbBlanca.TabIndex = 1;
            this.cbBlanca.Text = "Blanca";
            this.cbBlanca.UseVisualStyleBackColor = true;
            // 
            // cbCompleto
            // 
            this.cbCompleto.AutoSize = true;
            this.cbCompleto.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompleto.Location = new System.Drawing.Point(48, 350);
            this.cbCompleto.Name = "cbCompleto";
            this.cbCompleto.Size = new System.Drawing.Size(78, 19);
            this.cbCompleto.TabIndex = 4;
            this.cbCompleto.Text = "Completa";
            this.cbCompleto.UseVisualStyleBackColor = true;
            this.cbCompleto.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbTCTD);
            this.panel3.Controls.Add(this.cbTransferencia);
            this.panel3.Controls.Add(this.cbCheque);
            this.panel3.Controls.Add(this.cbEfectivo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(194, 44);
            this.panel3.TabIndex = 5;
            // 
            // cbTCTD
            // 
            this.cbTCTD.AccessibleName = "2";
            this.cbTCTD.AutoSize = true;
            this.cbTCTD.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTCTD.Location = new System.Drawing.Point(82, 21);
            this.cbTCTD.Name = "cbTCTD";
            this.cbTCTD.Size = new System.Drawing.Size(58, 19);
            this.cbTCTD.TabIndex = 5;
            this.cbTCTD.Text = "TC/TD";
            this.cbTCTD.UseVisualStyleBackColor = true;
            this.cbTCTD.Click += new System.EventHandler(this.cbTCTD_Click);
            // 
            // cbTransferencia
            // 
            this.cbTransferencia.AccessibleName = "2";
            this.cbTransferencia.AutoSize = true;
            this.cbTransferencia.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTransferencia.Location = new System.Drawing.Point(82, 4);
            this.cbTransferencia.Name = "cbTransferencia";
            this.cbTransferencia.Size = new System.Drawing.Size(101, 19);
            this.cbTransferencia.TabIndex = 4;
            this.cbTransferencia.Text = "Transferencia";
            this.cbTransferencia.UseVisualStyleBackColor = true;
            // 
            // cbCheque
            // 
            this.cbCheque.AccessibleName = "2";
            this.cbCheque.AutoSize = true;
            this.cbCheque.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCheque.Location = new System.Drawing.Point(7, 21);
            this.cbCheque.Name = "cbCheque";
            this.cbCheque.Size = new System.Drawing.Size(66, 19);
            this.cbCheque.TabIndex = 3;
            this.cbCheque.Text = "Cheque";
            this.cbCheque.UseVisualStyleBackColor = true;
            // 
            // cbEfectivo
            // 
            this.cbEfectivo.AccessibleName = "3";
            this.cbEfectivo.AutoSize = true;
            this.cbEfectivo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEfectivo.Location = new System.Drawing.Point(7, 1);
            this.cbEfectivo.Name = "cbEfectivo";
            this.cbEfectivo.Size = new System.Drawing.Size(68, 19);
            this.cbEfectivo.TabIndex = 2;
            this.cbEfectivo.Text = "Efectivo";
            this.cbEfectivo.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(155, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 69);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Forma de pago";
            // 
            // btnScan
            // 
            this.btnScan.Image = global::Cobranza.Properties.Resources.scan;
            this.btnScan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.Location = new System.Drawing.Point(265, 436);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(86, 25);
            this.btnScan.TabIndex = 7;
            this.btnScan.Text = "      Scanner";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lvFiles
            // 
            this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvFiles.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvFiles.FullRowSelect = true;
            this.lvFiles.LargeImageList = this.imageList1;
            this.lvFiles.Location = new System.Drawing.Point(16, 464);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(338, 58);
            this.lvFiles.SmallImageList = this.imageList1;
            this.lvFiles.StateImageList = this.imageList1;
            this.lvFiles.TabIndex = 8;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.Click += new System.EventHandler(this.lvFiles_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Archivo";
            this.columnHeader1.Width = 250;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "pdf.png");
            this.imageList1.Images.SetKeyName(1, "pdf_big.png");
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(469, 39);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.lvFiles;
            this.propertyGrid1.Size = new System.Drawing.Size(488, 384);
            this.propertyGrid1.TabIndex = 10;
            this.propertyGrid1.Visible = false;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(13, 414);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(80, 15);
            this.lblTitulo.TabIndex = 11;
            this.lblTitulo.Text = "Comentarios:";
            this.lblTitulo.Visible = false;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(369, 534);
            this.shapeContainer1.TabIndex = 12;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.Visible = false;
            this.lineShape1.X1 = 27;
            this.lineShape1.X2 = 143;
            this.lineShape1.Y1 = 37;
            this.lineShape1.Y2 = 33;
            // 
            // txtDescuentos
            // 
            this.txtDescuentos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescuentos.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuentos.Location = new System.Drawing.Point(99, 386);
            this.txtDescuentos.MaxLength = 500;
            this.txtDescuentos.Name = "txtDescuentos";
            this.txtDescuentos.Size = new System.Drawing.Size(252, 23);
            this.txtDescuentos.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 390);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "Descuentos";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Causas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 534);
            this.Controls.Add(this.txtDescuentos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtComentarios);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnDeshacer);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbCompleto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shapeContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Causas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HalcoNET";
            this.Load += new System.EventHandler(this.Causas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Button btnDeshacer;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbOriginal;
        private System.Windows.Forms.CheckBox cbBlanca;
        private System.Windows.Forms.CheckBox cbCompleto;
        private System.Windows.Forms.CheckBox cbCheque;
        private System.Windows.Forms.CheckBox cbEfectivo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox cbTransferencia;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label lblTitulo;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private System.Windows.Forms.CheckBox cbTCTD;
        private System.Windows.Forms.TextBox txtDescuentos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbCancelada;
        private System.Windows.Forms.CheckBox cbOtros;
        private System.Windows.Forms.TextBox txtEspecifique;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}