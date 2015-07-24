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
    public partial class GarantiasCob : Form
    {
        public GarantiasCob()
        {
            InitializeComponent();
        }

        private void Garantias_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET; 
            dgvRepartir.Columns[0].HeaderText = "Artículo";
            dgvRepartir.Columns[1].HeaderText = "Descripción";
            dgvRepartir.Columns[2].HeaderText = "Cantidad\r\nGarantía";
            dgvRepartir.Columns[3].HeaderText = "Situación";

            dgvRepartir.Columns[0].Width = 90;
            dgvRepartir.Columns[1].Width = 200;
            dgvRepartir.Columns[2].Width = 80;
            dgvRepartir.Columns[3].Width = 80;

            dgvRepartir.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvRepartir.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataGridView dgv = dgvRepartir;

            dgv.BorderStyle = BorderStyle.FixedSingle;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
