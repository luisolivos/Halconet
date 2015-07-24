namespace Ventas.Ventas
{
    partial class frmIndicadores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndicadores));
            this.cbVendedores = new System.Windows.Forms.ComboBox();
            this.lblVendedor = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPronostico1M = new System.Windows.Forms.TextBox();
            this.txtAcum1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtPronostico1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUtilidadReal = new System.Windows.Forms.TextBox();
            this.txtUtilidadObjetivo = new System.Windows.Forms.TextBox();
            this.txtAcumvsCuota = new System.Windows.Forms.TextBox();
            this.txtVentaActual = new System.Windows.Forms.TextBox();
            this.txtObjVenta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPronostico2M = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtAcum2 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPronostico2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtObvsCuotaHalcon = new System.Windows.Forms.TextBox();
            this.txtVentaHalcon = new System.Windows.Forms.TextBox();
            this.txtObjHalcon = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPronostico3M = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtAcum3 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtPronostico3 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtObjetivovsCuotaObjetivo = new System.Windows.Forms.TextBox();
            this.txtVentaObjetivo = new System.Windows.Forms.TextBox();
            this.txtObjetivoObjetivo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAclarar = new System.Windows.Forms.Button();
            this.btnArribo = new System.Windows.Forms.Button();
            this.btnStockReciente = new System.Windows.Forms.Button();
            this.btnPPC = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbVendedores
            // 
            this.cbVendedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVendedores.DropDownWidth = 250;
            this.cbVendedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVendedores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cbVendedores.FormattingEnabled = true;
            this.cbVendedores.Location = new System.Drawing.Point(80, 5);
            this.cbVendedores.Name = "cbVendedores";
            this.cbVendedores.Size = new System.Drawing.Size(187, 21);
            this.cbVendedores.TabIndex = 0;
            this.cbVendedores.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // lblVendedor
            // 
            this.lblVendedor.AutoSize = true;
            this.lblVendedor.Location = new System.Drawing.Point(8, 9);
            this.lblVendedor.Name = "lblVendedor";
            this.lblVendedor.Size = new System.Drawing.Size(63, 15);
            this.lblVendedor.TabIndex = 3;
            this.lblVendedor.Text = "Vendedor:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.txtPronostico1M);
            this.groupBox1.Controls.Add(this.txtAcum1);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtPronostico1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtUtilidadReal);
            this.groupBox1.Controls.Add(this.txtUtilidadObjetivo);
            this.groupBox1.Controls.Add(this.txtAcumvsCuota);
            this.groupBox1.Controls.Add(this.txtVentaActual);
            this.groupBox1.Controls.Add(this.txtObjVenta);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(11, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(542, 141);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cuota / Utilidad ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(166, 122);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(77, 16);
            this.label22.TabIndex = 23;
            this.label22.Text = "M   --   T  --  A";
            // 
            // txtPronostico1M
            // 
            this.txtPronostico1M.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPronostico1M.Location = new System.Drawing.Point(138, 74);
            this.txtPronostico1M.Name = "txtPronostico1M";
            this.txtPronostico1M.ReadOnly = true;
            this.txtPronostico1M.Size = new System.Drawing.Size(116, 22);
            this.txtPronostico1M.TabIndex = 22;
            this.txtPronostico1M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPronostico1M.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtAcum1
            // 
            this.txtAcum1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcum1.Location = new System.Drawing.Point(138, 48);
            this.txtAcum1.Name = "txtAcum1";
            this.txtAcum1.ReadOnly = true;
            this.txtAcum1.Size = new System.Drawing.Size(116, 22);
            this.txtAcum1.TabIndex = 21;
            this.txtAcum1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAcum1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(12, 77);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(121, 16);
            this.label16.TabIndex = 20;
            this.label16.Text = "Pronostico fin de mes($)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(12, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(125, 16);
            this.label17.TabIndex = 19;
            this.label17.Text = "Acumulado vs Cuota ($):";
            // 
            // txtPronostico1
            // 
            this.txtPronostico1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPronostico1.Location = new System.Drawing.Point(399, 74);
            this.txtPronostico1.Name = "txtPronostico1";
            this.txtPronostico1.ReadOnly = true;
            this.txtPronostico1.Size = new System.Drawing.Size(116, 22);
            this.txtPronostico1.TabIndex = 18;
            this.txtPronostico1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPronostico1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(270, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Pronostico fin de mes(%)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(429, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 16);
            this.label13.TabIndex = 16;
            this.label13.Text = "M   --   T  --  A";
            // 
            // txtUtilidadReal
            // 
            this.txtUtilidadReal.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUtilidadReal.Location = new System.Drawing.Point(399, 100);
            this.txtUtilidadReal.Name = "txtUtilidadReal";
            this.txtUtilidadReal.ReadOnly = true;
            this.txtUtilidadReal.Size = new System.Drawing.Size(137, 22);
            this.txtUtilidadReal.TabIndex = 14;
            this.txtUtilidadReal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUtilidadReal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtUtilidadObjetivo
            // 
            this.txtUtilidadObjetivo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUtilidadObjetivo.Location = new System.Drawing.Point(138, 100);
            this.txtUtilidadObjetivo.Name = "txtUtilidadObjetivo";
            this.txtUtilidadObjetivo.ReadOnly = true;
            this.txtUtilidadObjetivo.Size = new System.Drawing.Size(133, 22);
            this.txtUtilidadObjetivo.TabIndex = 13;
            this.txtUtilidadObjetivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUtilidadObjetivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtAcumvsCuota
            // 
            this.txtAcumvsCuota.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcumvsCuota.Location = new System.Drawing.Point(399, 48);
            this.txtAcumvsCuota.Name = "txtAcumvsCuota";
            this.txtAcumvsCuota.ReadOnly = true;
            this.txtAcumvsCuota.Size = new System.Drawing.Size(116, 22);
            this.txtAcumvsCuota.TabIndex = 12;
            this.txtAcumvsCuota.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAcumvsCuota.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtVentaActual
            // 
            this.txtVentaActual.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVentaActual.Location = new System.Drawing.Point(399, 22);
            this.txtVentaActual.Name = "txtVentaActual";
            this.txtVentaActual.ReadOnly = true;
            this.txtVentaActual.Size = new System.Drawing.Size(116, 22);
            this.txtVentaActual.TabIndex = 11;
            this.txtVentaActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVentaActual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtObjVenta
            // 
            this.txtObjVenta.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjVenta.Location = new System.Drawing.Point(138, 22);
            this.txtObjVenta.Name = "txtObjVenta";
            this.txtObjVenta.ReadOnly = true;
            this.txtObjVenta.Size = new System.Drawing.Size(116, 22);
            this.txtObjVenta.TabIndex = 6;
            this.txtObjVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtObjVenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(270, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "% Utilidad real:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Utilidad objetivo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(270, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Acumulado vs Cuota (%):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(270, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Venta actual:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Objetivo venta:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtPronostico2M);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtAcum2);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.txtPronostico2);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtObvsCuotaHalcon);
            this.groupBox2.Controls.Add(this.txtVentaHalcon);
            this.groupBox2.Controls.Add(this.txtObjHalcon);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(10, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 105);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lineas halcón";
            // 
            // txtPronostico2M
            // 
            this.txtPronostico2M.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPronostico2M.Location = new System.Drawing.Point(139, 73);
            this.txtPronostico2M.Name = "txtPronostico2M";
            this.txtPronostico2M.ReadOnly = true;
            this.txtPronostico2M.Size = new System.Drawing.Size(116, 22);
            this.txtPronostico2M.TabIndex = 24;
            this.txtPronostico2M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPronostico2M.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(13, 77);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(121, 16);
            this.label18.TabIndex = 23;
            this.label18.Text = "Pronostico fin de mes($)";
            // 
            // txtAcum2
            // 
            this.txtAcum2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcum2.Location = new System.Drawing.Point(139, 47);
            this.txtAcum2.Name = "txtAcum2";
            this.txtAcum2.ReadOnly = true;
            this.txtAcum2.Size = new System.Drawing.Size(116, 22);
            this.txtAcum2.TabIndex = 22;
            this.txtAcum2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAcum2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(13, 51);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(125, 16);
            this.label19.TabIndex = 21;
            this.label19.Text = "Acumulado vs Cuota ($):";
            // 
            // txtPronostico2
            // 
            this.txtPronostico2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPronostico2.Location = new System.Drawing.Point(399, 73);
            this.txtPronostico2.Name = "txtPronostico2";
            this.txtPronostico2.ReadOnly = true;
            this.txtPronostico2.Size = new System.Drawing.Size(116, 22);
            this.txtPronostico2.TabIndex = 20;
            this.txtPronostico2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPronostico2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(271, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(126, 16);
            this.label14.TabIndex = 19;
            this.label14.Text = "Pronostico fin de mes(%)";
            // 
            // txtObvsCuotaHalcon
            // 
            this.txtObvsCuotaHalcon.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObvsCuotaHalcon.Location = new System.Drawing.Point(399, 47);
            this.txtObvsCuotaHalcon.Name = "txtObvsCuotaHalcon";
            this.txtObvsCuotaHalcon.ReadOnly = true;
            this.txtObvsCuotaHalcon.Size = new System.Drawing.Size(116, 22);
            this.txtObvsCuotaHalcon.TabIndex = 17;
            this.txtObvsCuotaHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtObvsCuotaHalcon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtVentaHalcon
            // 
            this.txtVentaHalcon.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVentaHalcon.Location = new System.Drawing.Point(399, 21);
            this.txtVentaHalcon.Name = "txtVentaHalcon";
            this.txtVentaHalcon.ReadOnly = true;
            this.txtVentaHalcon.Size = new System.Drawing.Size(116, 22);
            this.txtVentaHalcon.TabIndex = 16;
            this.txtVentaHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVentaHalcon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtObjHalcon
            // 
            this.txtObjHalcon.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjHalcon.Location = new System.Drawing.Point(139, 21);
            this.txtObjHalcon.Name = "txtObjHalcon";
            this.txtObjHalcon.ReadOnly = true;
            this.txtObjHalcon.Size = new System.Drawing.Size(116, 22);
            this.txtObjHalcon.TabIndex = 15;
            this.txtObjHalcon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtObjHalcon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(271, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "Acumulado vs Cuota (%):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(271, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 16);
            this.label8.TabIndex = 12;
            this.label8.Text = "Venta actual:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 16);
            this.label7.TabIndex = 11;
            this.label7.Text = "Objetivo:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtPronostico3M);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.txtAcum3);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.txtPronostico3);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtObjetivovsCuotaObjetivo);
            this.groupBox3.Controls.Add(this.txtVentaObjetivo);
            this.groupBox3.Controls.Add(this.txtObjetivoObjetivo);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(11, 280);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(542, 95);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Linea objetivo";
            // 
            // txtPronostico3M
            // 
            this.txtPronostico3M.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPronostico3M.Location = new System.Drawing.Point(138, 68);
            this.txtPronostico3M.Name = "txtPronostico3M";
            this.txtPronostico3M.ReadOnly = true;
            this.txtPronostico3M.Size = new System.Drawing.Size(116, 22);
            this.txtPronostico3M.TabIndex = 26;
            this.txtPronostico3M.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPronostico3M.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(12, 71);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(121, 16);
            this.label20.TabIndex = 25;
            this.label20.Text = "Pronostico fin de mes($)";
            // 
            // txtAcum3
            // 
            this.txtAcum3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcum3.Location = new System.Drawing.Point(138, 44);
            this.txtAcum3.Name = "txtAcum3";
            this.txtAcum3.ReadOnly = true;
            this.txtAcum3.Size = new System.Drawing.Size(116, 22);
            this.txtAcum3.TabIndex = 24;
            this.txtAcum3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAcum3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(12, 47);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(125, 16);
            this.label21.TabIndex = 23;
            this.label21.Text = "Acumulado vs Cuota ($):";
            // 
            // txtPronostico3
            // 
            this.txtPronostico3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPronostico3.Location = new System.Drawing.Point(399, 64);
            this.txtPronostico3.Name = "txtPronostico3";
            this.txtPronostico3.ReadOnly = true;
            this.txtPronostico3.Size = new System.Drawing.Size(116, 22);
            this.txtPronostico3.TabIndex = 22;
            this.txtPronostico3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPronostico3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(270, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 16);
            this.label15.TabIndex = 21;
            this.label15.Text = "Pronostico fin de mes(%)";
            // 
            // txtObjetivovsCuotaObjetivo
            // 
            this.txtObjetivovsCuotaObjetivo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjetivovsCuotaObjetivo.Location = new System.Drawing.Point(399, 40);
            this.txtObjetivovsCuotaObjetivo.Name = "txtObjetivovsCuotaObjetivo";
            this.txtObjetivovsCuotaObjetivo.ReadOnly = true;
            this.txtObjetivovsCuotaObjetivo.Size = new System.Drawing.Size(116, 22);
            this.txtObjetivovsCuotaObjetivo.TabIndex = 20;
            this.txtObjetivovsCuotaObjetivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtObjetivovsCuotaObjetivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtVentaObjetivo
            // 
            this.txtVentaObjetivo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVentaObjetivo.Location = new System.Drawing.Point(399, 16);
            this.txtVentaObjetivo.Name = "txtVentaObjetivo";
            this.txtVentaObjetivo.ReadOnly = true;
            this.txtVentaObjetivo.Size = new System.Drawing.Size(116, 22);
            this.txtVentaObjetivo.TabIndex = 19;
            this.txtVentaObjetivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVentaObjetivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // txtObjetivoObjetivo
            // 
            this.txtObjetivoObjetivo.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObjetivoObjetivo.Location = new System.Drawing.Point(138, 20);
            this.txtObjetivoObjetivo.Name = "txtObjetivoObjetivo";
            this.txtObjetivoObjetivo.ReadOnly = true;
            this.txtObjetivoObjetivo.Size = new System.Drawing.Size(116, 22);
            this.txtObjetivoObjetivo.TabIndex = 18;
            this.txtObjetivoObjetivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtObjetivoObjetivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(270, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 16);
            this.label12.TabIndex = 14;
            this.label12.Text = "Acumulado vs Cuota (%):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(270, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 16);
            this.label11.TabIndex = 13;
            this.label11.Text = "Venta actual:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(12, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Objetivo:";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.button2.Location = new System.Drawing.Point(372, 478);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "Consultar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 495);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(97, 21);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(465, 478);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 38);
            this.button1.TabIndex = 3;
            this.button1.Text = "Salir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnAclarar);
            this.groupBox4.Controls.Add(this.btnArribo);
            this.groupBox4.Controls.Add(this.btnStockReciente);
            this.groupBox4.Controls.Add(this.btnPPC);
            this.groupBox4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(10, 381);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(542, 91);
            this.groupBox4.TabIndex = 27;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Notificaciones";
            // 
            // btnAclarar
            // 
            this.btnAclarar.BackColor = System.Drawing.SystemColors.Info;
            this.btnAclarar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAclarar.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F);
            this.btnAclarar.Image = ((System.Drawing.Image)(resources.GetObject("btnAclarar.Image")));
            this.btnAclarar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAclarar.Location = new System.Drawing.Point(383, 22);
            this.btnAclarar.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnAclarar.Name = "btnAclarar";
            this.btnAclarar.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnAclarar.Size = new System.Drawing.Size(110, 56);
            this.btnAclarar.TabIndex = 9;
            this.btnAclarar.Text = "NC por \r\naclarar";
            this.btnAclarar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAclarar.UseVisualStyleBackColor = false;
            this.btnAclarar.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // btnArribo
            // 
            this.btnArribo.BackColor = System.Drawing.SystemColors.Info;
            this.btnArribo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnArribo.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F);
            this.btnArribo.Image = ((System.Drawing.Image)(resources.GetObject("btnArribo.Image")));
            this.btnArribo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnArribo.Location = new System.Drawing.Point(269, 22);
            this.btnArribo.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnArribo.Name = "btnArribo";
            this.btnArribo.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnArribo.Size = new System.Drawing.Size(110, 56);
            this.btnArribo.TabIndex = 8;
            this.btnArribo.Text = "Arribos";
            this.btnArribo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnArribo.UseVisualStyleBackColor = false;
            this.btnArribo.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // btnStockReciente
            // 
            this.btnStockReciente.BackColor = System.Drawing.SystemColors.Info;
            this.btnStockReciente.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStockReciente.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F);
            this.btnStockReciente.Image = ((System.Drawing.Image)(resources.GetObject("btnStockReciente.Image")));
            this.btnStockReciente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStockReciente.Location = new System.Drawing.Point(146, 22);
            this.btnStockReciente.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnStockReciente.Name = "btnStockReciente";
            this.btnStockReciente.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnStockReciente.Size = new System.Drawing.Size(110, 56);
            this.btnStockReciente.TabIndex = 7;
            this.btnStockReciente.Text = "Stock\r\nReciente";
            this.btnStockReciente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStockReciente.UseVisualStyleBackColor = false;
            this.btnStockReciente.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnPPC
            // 
            this.btnPPC.BackColor = System.Drawing.SystemColors.Info;
            this.btnPPC.Enabled = false;
            this.btnPPC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPPC.Font = new System.Drawing.Font("MS Reference Sans Serif", 8F);
            this.btnPPC.Image = ((System.Drawing.Image)(resources.GetObject("btnPPC.Image")));
            this.btnPPC.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPPC.Location = new System.Drawing.Point(23, 22);
            this.btnPPC.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.btnPPC.Name = "btnPPC";
            this.btnPPC.Padding = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnPPC.Size = new System.Drawing.Size(110, 56);
            this.btnPPC.TabIndex = 6;
            this.btnPPC.Text = "PPC /\r\nCompras especiales";
            this.btnPPC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnPPC, "Artículos PPC y Compras especiales\r\npendientes por vender.");
            this.btnPPC.UseVisualStyleBackColor = false;
            this.btnPPC.Click += new System.EventHandler(this.btnPPC_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // frmIndicadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(570, 524);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblVendedor);
            this.Controls.Add(this.cbVendedores);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmIndicadores";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Indicadores";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Indicadores_FormClosing);
            this.Load += new System.EventHandler(this.Indicadores_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbVendedores_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbVendedores;
        private System.Windows.Forms.Label lblVendedor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtUtilidadReal;
        private System.Windows.Forms.TextBox txtUtilidadObjetivo;
        private System.Windows.Forms.TextBox txtAcumvsCuota;
        private System.Windows.Forms.TextBox txtVentaActual;
        private System.Windows.Forms.TextBox txtObjVenta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtObvsCuotaHalcon;
        private System.Windows.Forms.TextBox txtVentaHalcon;
        private System.Windows.Forms.TextBox txtObjHalcon;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtObjetivovsCuotaObjetivo;
        private System.Windows.Forms.TextBox txtVentaObjetivo;
        private System.Windows.Forms.TextBox txtObjetivoObjetivo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txtPronostico1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPronostico2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPronostico3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtPronostico1M;
        private System.Windows.Forms.TextBox txtAcum1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtPronostico2M;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtAcum2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPronostico3M;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtAcum3;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button btnPPC;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.Button btnStockReciente;
        public System.Windows.Forms.Button btnArribo;
        public System.Windows.Forms.Button btnAclarar;
    }
}