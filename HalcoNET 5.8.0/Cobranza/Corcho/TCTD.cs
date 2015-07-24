using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.Corcho
{
    public partial class TCTD : Form
    {
        string _tC_TC;

        public string TC_TC
        {
            get { return _tC_TC; }
            set { _tC_TC = value; }
        }

        public TCTD()
        {
            InitializeComponent();
        }

        private void TCTD_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _tC_TC = comboBox1.Text;

            if (!string.IsNullOrEmpty(_tC_TC))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
