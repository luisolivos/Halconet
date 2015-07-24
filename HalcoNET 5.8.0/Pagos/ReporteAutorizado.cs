using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pagos
{
    public partial class ReporteAutorizado : Form
    {
        DataTable Reporte = new DataTable();

        public ReporteAutorizado(DataTable _reporte)
        {
            InitializeComponent();
            Reporte = _reporte;
        }

        private void ReporteAutorizado_Load(object sender, EventArgs e)
        {
            try
            {

                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                if (Reporte.Rows.Count > 0)
                {
                    var query = (from item in Reporte.AsEnumerable()
                                 select item.Field<string>("Moneda")).Distinct();

                    foreach (var item in query.ToList())
                    {
                        DataRow r = Reporte.NewRow();


                        r["Moneda"] = item + " TOTAL";
                        r["Proveedor"] = "TOTAL " + item;

                        r["Autorizado"] = (from acum in Reporte.AsEnumerable()
                                           where acum.Field<string>("Moneda") == item
                                           select acum.Field<decimal>("Autorizado")).Sum();
                        //  r["Moneda"] = string.Empty;
                        r["Cuenta"] = string.Empty;

                        Reporte.Rows.Add(r);
                    }

                    Reporte = (from tv in Reporte.AsEnumerable()
                               orderby tv.Field<string>("Moneda")
                               select tv).CopyToDataTable();
                }
                dgvReporte.DataSource = Reporte;

                dgvReporte.Columns[0].Width = 70;
                dgvReporte.Columns[1].Width = 200;
                dgvReporte.Columns[2].Width = 90;
                dgvReporte.Columns[3].Width = 90;
                dgvReporte.Columns[4].Visible = false;

                dgvReporte.Columns[2].DefaultCellStyle.Format = "C2";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvReporte_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow item in (sender as DataGridView).Rows)
            {
                if (Convert.ToString(item.Cells[1].Value).ToLower().Contains("total"))
                {
                    foreach(DataGridViewCell  cell in item.Cells)
                    {
                         cell.Style.Font = new Font("Calibri", 10f, FontStyle.Bold);
                    }
                   
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                CrearPDF pdf = new CrearPDF();
                pdf.ToPDF(Reporte);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
