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
    public partial class DatosMes : Form
    {
        int _mes;

        public int Mes
        {
            get { return _mes; }
            set { _mes = value; }
        }
        int _año;

        public int Año
        {
            get { return _año; }
            set { _año = value; }
        }

        public DatosMes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbMes.SelectedIndex + 1 > 0 && !string.IsNullOrEmpty(txtAño.Text))
            {
                _mes = cbMes.SelectedIndex + 1;
                _año = Convert.ToInt32(txtAño.Text);


                this.DialogResult = DialogResult.Yes;
            }
            else
            {
                MessageBox.Show("Para continuar seleccione mes y año.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void DatosMes_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            cbMes.SelectedIndex = DateTime.Now.Month - 1;
            txtAño.Text = DateTime.Now.Year.ToString();
        }
    }
}
