using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas
{
    public partial class Fechas : Form
    {
        public Fechas()
        {
            InitializeComponent();
        }
        public DateTime fi { get; set; }
        public DateTime ff { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcPickerFinal.MaxDate = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fi = dtPickerInicial.Value;
            ff = tcPickerFinal.Value;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
