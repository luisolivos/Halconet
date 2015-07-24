namespace Cobranza
{
    partial class Principal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPage = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbCobro = new System.Windows.Forms.RadioButton();
            this.cbRevision = new System.Windows.Forms.RadioButton();
            this.gridRecibo = new System.Windows.Forms.DataGridView();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbVencimiento = new System.Windows.Forms.CheckBox();
            this.chbEspeciales = new System.Windows.Forms.CheckBox();
            this.chbSabado = new System.Windows.Forms.CheckBox();
            this.chbViernes = new System.Windows.Forms.CheckBox();
            this.chbJueves = new System.Windows.Forms.CheckBox();
            this.chbMiercoles = new System.Windows.Forms.CheckBox();
            this.chbMartes = new System.Windows.Forms.CheckBox();
            this.chbLunes = new System.Windows.Forms.CheckBox();
            this.clbCobranza2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button9 = new System.Windows.Forms.Button();
            this.gridConsultaRecibo = new System.Windows.Forms.DataGridView();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.txtRecibo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorP = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.TabPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecibo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConsultaRecibo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorP)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.TabPage);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1017, 587);
            this.tabControl1.TabIndex = 0;
            // 
            // TabPage
            // 
            this.TabPage.Controls.Add(this.textBox1);
            this.TabPage.Controls.Add(this.label4);
            this.TabPage.Controls.Add(this.dateTimePicker1);
            this.TabPage.Controls.Add(this.txtFolio);
            this.TabPage.Controls.Add(this.label3);
            this.TabPage.Controls.Add(this.groupBox2);
            this.TabPage.Controls.Add(this.gridRecibo);
            this.TabPage.Controls.Add(this.button4);
            this.TabPage.Controls.Add(this.button5);
            this.TabPage.Controls.Add(this.button6);
            this.TabPage.Controls.Add(this.groupBox1);
            this.TabPage.Controls.Add(this.clbCobranza2);
            this.TabPage.Controls.Add(this.label1);
            this.TabPage.Location = new System.Drawing.Point(4, 25);
            this.TabPage.Name = "TabPage";
            this.TabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage.Size = new System.Drawing.Size(1009, 558);
            this.TabPage.TabIndex = 1;
            this.TabPage.Text = "Filtro especifico";
            this.TabPage.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(206, 114);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(107, 20);
            this.textBox1.TabIndex = 26;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Folio:";
            this.label4.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(669, 45);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 24;
            this.dateTimePicker1.Visible = false;
            // 
            // txtFolio
            // 
            this.txtFolio.Location = new System.Drawing.Point(47, 115);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.ReadOnly = true;
            this.txtFolio.Size = new System.Drawing.Size(107, 20);
            this.txtFolio.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Folio:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbCobro);
            this.groupBox2.Controls.Add(this.cbRevision);
            this.groupBox2.Location = new System.Drawing.Point(10, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(132, 69);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actividad:";
            // 
            // cbCobro
            // 
            this.cbCobro.AutoSize = true;
            this.cbCobro.Location = new System.Drawing.Point(9, 42);
            this.cbCobro.Name = "cbCobro";
            this.cbCobro.Size = new System.Drawing.Size(50, 17);
            this.cbCobro.TabIndex = 3;
            this.cbCobro.Text = "Pago";
            this.cbCobro.UseVisualStyleBackColor = true;
            // 
            // cbRevision
            // 
            this.cbRevision.AutoSize = true;
            this.cbRevision.Checked = true;
            this.cbRevision.Location = new System.Drawing.Point(9, 19);
            this.cbRevision.Name = "cbRevision";
            this.cbRevision.Size = new System.Drawing.Size(66, 17);
            this.cbRevision.TabIndex = 2;
            this.cbRevision.TabStop = true;
            this.cbRevision.Text = "Revisión";
            this.cbRevision.UseVisualStyleBackColor = true;
            // 
            // gridRecibo
            // 
            this.gridRecibo.AllowUserToAddRows = false;
            this.gridRecibo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridRecibo.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRecibo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridRecibo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRecibo.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridRecibo.Location = new System.Drawing.Point(10, 141);
            this.gridRecibo.Name = "gridRecibo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRecibo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridRecibo.Size = new System.Drawing.Size(991, 409);
            this.gridRecibo.TabIndex = 10;
            this.gridRecibo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRecibo_CellEndEdit);
            this.gridRecibo.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gridRecibo_DataBindingComplete);
            this.gridRecibo.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.gridRecibo_EditingControlShowing);
            this.gridRecibo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridRecibo_KeyDown);
            this.gridRecibo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridRecibo_KeyPress);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(923, 82);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(78, 30);
            this.button4.TabIndex = 20;
            this.button4.Text = "Limpiar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(923, 9);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(78, 30);
            this.button5.TabIndex = 19;
            this.button5.Text = "Consultar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(923, 45);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(78, 30);
            this.button6.TabIndex = 18;
            this.button6.Text = "Recibo";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbVencimiento);
            this.groupBox1.Controls.Add(this.chbEspeciales);
            this.groupBox1.Controls.Add(this.chbSabado);
            this.groupBox1.Controls.Add(this.chbViernes);
            this.groupBox1.Controls.Add(this.chbJueves);
            this.groupBox1.Controls.Add(this.chbMiercoles);
            this.groupBox1.Controls.Add(this.chbMartes);
            this.groupBox1.Controls.Add(this.chbLunes);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(161, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(502, 69);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Día:";
            // 
            // chbVencimiento
            // 
            this.chbVencimiento.AutoSize = true;
            this.chbVencimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbVencimiento.Location = new System.Drawing.Point(391, 42);
            this.chbVencimiento.Name = "chbVencimiento";
            this.chbVencimiento.Size = new System.Drawing.Size(90, 17);
            this.chbVencimiento.TabIndex = 7;
            this.chbVencimiento.Text = "A vencimiento";
            this.chbVencimiento.UseVisualStyleBackColor = true;
            this.chbVencimiento.Click += new System.EventHandler(this.chbVencimiento_Click);
            // 
            // chbEspeciales
            // 
            this.chbEspeciales.AutoSize = true;
            this.chbEspeciales.BackColor = System.Drawing.Color.Crimson;
            this.chbEspeciales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbEspeciales.ForeColor = System.Drawing.Color.White;
            this.chbEspeciales.Location = new System.Drawing.Point(391, 17);
            this.chbEspeciales.Name = "chbEspeciales";
            this.chbEspeciales.Size = new System.Drawing.Size(74, 17);
            this.chbEspeciales.TabIndex = 6;
            this.chbEspeciales.Text = "Especiales";
            this.chbEspeciales.UseVisualStyleBackColor = false;
            this.chbEspeciales.Click += new System.EventHandler(this.clbCobranza2_SelectedValueChanged);
            // 
            // chbSabado
            // 
            this.chbSabado.AutoSize = true;
            this.chbSabado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbSabado.Location = new System.Drawing.Point(265, 42);
            this.chbSabado.Name = "chbSabado";
            this.chbSabado.Size = new System.Drawing.Size(60, 17);
            this.chbSabado.TabIndex = 5;
            this.chbSabado.Text = "Sabado";
            this.chbSabado.UseVisualStyleBackColor = true;
            this.chbSabado.Click += new System.EventHandler(this.clbCobranza2_SelectedValueChanged);
            // 
            // chbViernes
            // 
            this.chbViernes.AutoSize = true;
            this.chbViernes.BackColor = System.Drawing.Color.Pink;
            this.chbViernes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbViernes.Location = new System.Drawing.Point(265, 17);
            this.chbViernes.Name = "chbViernes";
            this.chbViernes.Size = new System.Drawing.Size(58, 17);
            this.chbViernes.TabIndex = 4;
            this.chbViernes.Text = "Viernes";
            this.chbViernes.UseVisualStyleBackColor = false;
            this.chbViernes.Click += new System.EventHandler(this.clbCobranza2_SelectedValueChanged);
            // 
            // chbJueves
            // 
            this.chbJueves.AutoSize = true;
            this.chbJueves.BackColor = System.Drawing.Color.SkyBlue;
            this.chbJueves.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbJueves.Location = new System.Drawing.Point(129, 42);
            this.chbJueves.Name = "chbJueves";
            this.chbJueves.Size = new System.Drawing.Size(57, 17);
            this.chbJueves.TabIndex = 3;
            this.chbJueves.Text = "Jueves";
            this.chbJueves.UseVisualStyleBackColor = false;
            this.chbJueves.Click += new System.EventHandler(this.clbCobranza2_SelectedValueChanged);
            // 
            // chbMiercoles
            // 
            this.chbMiercoles.AutoSize = true;
            this.chbMiercoles.BackColor = System.Drawing.Color.LightGreen;
            this.chbMiercoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbMiercoles.Location = new System.Drawing.Point(129, 17);
            this.chbMiercoles.Name = "chbMiercoles";
            this.chbMiercoles.Size = new System.Drawing.Size(68, 17);
            this.chbMiercoles.TabIndex = 2;
            this.chbMiercoles.Text = "Miercoles";
            this.chbMiercoles.UseVisualStyleBackColor = false;
            this.chbMiercoles.Click += new System.EventHandler(this.clbCobranza2_SelectedValueChanged);
            // 
            // chbMartes
            // 
            this.chbMartes.AutoSize = true;
            this.chbMartes.BackColor = System.Drawing.Color.Yellow;
            this.chbMartes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbMartes.Location = new System.Drawing.Point(9, 42);
            this.chbMartes.Name = "chbMartes";
            this.chbMartes.Size = new System.Drawing.Size(55, 17);
            this.chbMartes.TabIndex = 1;
            this.chbMartes.Text = "Martes";
            this.chbMartes.UseVisualStyleBackColor = false;
            this.chbMartes.Click += new System.EventHandler(this.clbCobranza2_SelectedValueChanged);
            // 
            // chbLunes
            // 
            this.chbLunes.AutoSize = true;
            this.chbLunes.BackColor = System.Drawing.Color.Orange;
            this.chbLunes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbLunes.Location = new System.Drawing.Point(9, 17);
            this.chbLunes.Name = "chbLunes";
            this.chbLunes.Size = new System.Drawing.Size(52, 17);
            this.chbLunes.TabIndex = 0;
            this.chbLunes.Text = "Lunes";
            this.chbLunes.UseVisualStyleBackColor = false;
            this.chbLunes.Click += new System.EventHandler(this.clbCobranza2_SelectedValueChanged);
            // 
            // clbCobranza2
            // 
            this.clbCobranza2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clbCobranza2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clbCobranza2.FormattingEnabled = true;
            this.clbCobranza2.Location = new System.Drawing.Point(131, 9);
            this.clbCobranza2.Name = "clbCobranza2";
            this.clbCobranza2.Size = new System.Drawing.Size(225, 21);
            this.clbCobranza2.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Jefa de Cobranza:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button9);
            this.tabPage3.Controls.Add(this.gridConsultaRecibo);
            this.tabPage3.Controls.Add(this.button8);
            this.tabPage3.Controls.Add(this.button7);
            this.tabPage3.Controls.Add(this.txtRecibo);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1009, 558);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Consultar recibo";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(896, 79);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(105, 32);
            this.button9.TabIndex = 19;
            this.button9.Text = "Imprimir";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // gridConsultaRecibo
            // 
            this.gridConsultaRecibo.AllowUserToAddRows = false;
            this.gridConsultaRecibo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridConsultaRecibo.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridConsultaRecibo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridConsultaRecibo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridConsultaRecibo.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridConsultaRecibo.Location = new System.Drawing.Point(10, 117);
            this.gridConsultaRecibo.Name = "gridConsultaRecibo";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridConsultaRecibo.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridConsultaRecibo.Size = new System.Drawing.Size(991, 432);
            this.gridConsultaRecibo.TabIndex = 18;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.Location = new System.Drawing.Point(896, 42);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(105, 32);
            this.button8.TabIndex = 17;
            this.button8.Text = "Guardar cambios";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(896, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(105, 32);
            this.button7.TabIndex = 16;
            this.button7.Text = "Buscar";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtRecibo
            // 
            this.txtRecibo.Location = new System.Drawing.Point(135, 10);
            this.txtRecibo.Name = "txtRecibo";
            this.txtRecibo.Size = new System.Drawing.Size(115, 20);
            this.txtRecibo.TabIndex = 15;
            this.txtRecibo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecibo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Folio del recibo:";
            // 
            // errorP
            // 
            this.errorP.ContainerControl = this;
            // 
            // Principal
            // 
            this.AccessibleDescription = "Recibos";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 587);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Principal";
            this.Text = "Reporte de Facturas a Revisión / Cobro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Principal_FormClosing);
            this.Load += new System.EventHandler(this.Principal_Load);
            this.Shown += new System.EventHandler(this.Principal_Shown);
            this.tabControl1.ResumeLayout(false);
            this.TabPage.ResumeLayout(false);
            this.TabPage.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecibo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridConsultaRecibo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPage;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView gridRecibo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbEspeciales;
        private System.Windows.Forms.CheckBox chbSabado;
        private System.Windows.Forms.CheckBox chbViernes;
        private System.Windows.Forms.CheckBox chbJueves;
        private System.Windows.Forms.CheckBox chbMiercoles;
        private System.Windows.Forms.CheckBox chbMartes;
        private System.Windows.Forms.CheckBox chbLunes;
        private System.Windows.Forms.ComboBox clbCobranza2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox txtRecibo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.DataGridView gridConsultaRecibo;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ErrorProvider errorP;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton cbCobro;
        private System.Windows.Forms.RadioButton cbRevision;
        private System.Windows.Forms.CheckBox chbVencimiento;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
    }
}