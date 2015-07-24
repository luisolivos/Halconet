using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza
{
    public partial class frmFechaSiniestro : Form
    {
        private DateTime _Fecha;

        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }
        public frmFechaSiniestro()
        {
            InitializeComponent();
        }

        private void frmFechaSiniestro_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Fecha = dateTimePicker1.Value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
