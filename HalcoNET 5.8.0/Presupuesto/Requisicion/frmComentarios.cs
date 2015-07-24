using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presupuesto.Requisicion
{
    public partial class frmComentarios : Form
    {
        string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }


        public frmComentarios(string value)
        {
            InitializeComponent();

            richTextBox1.Text = value;
            richTextBox1.SelectAll();
        }

        private void frmComentarios_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.MinimizeBox = false;
            this.MaximizeBox = false;

        }

        private void btnAcept_Click(object sender, EventArgs e)
        {
            _value = richTextBox1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
