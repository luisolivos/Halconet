using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pagos
{
    public partial class PagosEfectuados : Form
    {
        public enum Columnas
        {
            Fecha,
            Factura,
            Proveedor,
            MXP,
            USD
        }

        private void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Fecha].Width = 90;
            dgv.Columns[(int)Columnas.Factura].Width = 90;
            dgv.Columns[(int)Columnas.Proveedor].Width = 250;
            dgv.Columns[(int)Columnas.MXP].Width = 90;
            dgv.Columns[(int)Columnas.USD].Width = 90;

            dgv.Columns[(int)Columnas.Fecha].HeaderText += "\r\n";
            dgv.Columns[(int)Columnas.Factura].HeaderText += "\r\n";
            dgv.Columns[(int)Columnas.Proveedor].HeaderText += "\r\n";
            dgv.Columns[(int)Columnas.MXP].HeaderText += "\r\n";
            dgv.Columns[(int)Columnas.USD].HeaderText += "\r\n";

            dgv.Columns[(int)Columnas.MXP].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.USD].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.MXP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.USD].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public PagosEfectuados()
        {
            InitializeComponent();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 10);
                        command.Parameters.AddWithValue("@FechaDesde", dtDesde.Value);
                        command.Parameters.AddWithValue("@FechaHasta", dtHasta.Value);
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Proveedores", string.Empty);
                        command.Parameters.AddWithValue("@GroupCode", 0);

                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);
                        command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                        command.Parameters.AddWithValue("@Aprobado", decimal.Zero);

                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Usuario", string.Empty);

                        command.Parameters.AddWithValue("@PropuestaUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@AprobadoUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@TC", decimal.Zero);

                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;


                        DataTable table = new DataTable();
                        da.Fill(table);

                        

                        var query = (from item in table.AsEnumerable()
                                     select item.Field<string>("Proveedor")).Distinct();

                        foreach (var item in query.ToList())
                        {
                            DataRow r = table.NewRow();
                            r["Proveedor"] = item + " Total";

                            r["MXP"] = (from acum in table.AsEnumerable()
                                        where acum.Field<string>("Proveedor") == item
                                        select acum.Field<decimal>("MXP")).Sum();

                            r["USD"] = (from acum in table.AsEnumerable()
                                        where acum.Field<string>("Proveedor") == item
                                        select acum.Field<decimal>("USD")).Sum();

                            table.Rows.Add(r);
                        }

                        table = (from tv in table.AsEnumerable()
                                 orderby tv.Field<string>("Proveedor")
                                 select tv).CopyToDataTable();
                        
                        dgvPagos.DataSource = table;

                        this.Formato(dgvPagos);


                        DataTable totales = new DataTable();
                        totales.Columns.Add("Total MXP", typeof(decimal));
                        totales.Columns.Add("Total USD", typeof(decimal));

                        DataRow row = totales.NewRow();

                        row["Total MXP"] = Convert.ToDecimal(table.Compute("SUM(MXP)", "Factura IS NOT NULL"));
                        row["Total USD"] = Convert.ToDecimal(table.Compute("SUM(USD)", "Factura IS NOT NULL")); 

                        totales.Rows.Add(row);

                        dgvTotales.DataSource = totales;

                        dgvTotales.Columns[0].DefaultCellStyle.Format = "C2";
                        dgvTotales.Columns[1].DefaultCellStyle.Format = "C2";

                        dgvTotales.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvTotales.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPagos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToString(item.Cells[(int)Columnas.Proveedor].Value).Contains("Total"))
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10f, FontStyle.Bold);
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvPagos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                if (!Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Proveedor].Value).Contains("Total"))
                    e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void PagosEfectuados_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }       
    }
}
