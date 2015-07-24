using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compras.IndicadorDesabasto
{
    public partial class frmPenalizaciones : Form
    {
        DataTable datos = new DataTable();

        public frmPenalizaciones(DataTable dat)
        {
            datos = dat;
            InitializeComponent();
        }

        private void Penalizaciones_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            dataGridView1.DataSource = datos;

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns[2].DefaultCellStyle.Format = "P2";
                dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
    }
}
