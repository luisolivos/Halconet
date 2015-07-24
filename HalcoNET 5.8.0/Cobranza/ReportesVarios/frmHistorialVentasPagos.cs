using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.ReportesVarios
{
    public partial class frmHistorialVentasPagos : Form
    {
        public frmHistorialVentasPagos()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Cliente,
            Nombre,
            Vendedor,
            Mes,
            Año,
            Ventas,
            Pagos,
            Porcentaje
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Debe ingresar un código de cliente", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                using (SqlConnection connection = new  SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", connection))
                    {
                        command.Parameters.AddWithValue("@TipoConsulta", 12);
                        command.Parameters.AddWithValue("@Hasta", dtpFin.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Desde", dtpInicio.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Cliente", textBox1.Text);

                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();
                        
                        da.Fill(table);

                        

                        var query = (from item in table.AsEnumerable()
                                     select item.Field<string>("Año")).Distinct();

                        foreach (var item in query.ToList())
                        {
                            DataRow r = table.NewRow();
                            r["Año"] =  item +" TOTAL"; 
                            r["Ventas"] = (from acum in table.AsEnumerable()
                                           where acum.Field<string>("Año") == item
                                                  select acum.Field<decimal>("Ventas")).Sum();
                            r["Pagos"] = (from acum in table.AsEnumerable()
                                          where acum.Field<string>("Año") == item
                                              select acum.Field<decimal>("Pagos")).Sum();

                            r["%"] = Convert.ToDecimal(r["Ventas"]) == decimal.Zero ? decimal.Zero : Convert.ToDecimal(r["Pagos"]) / Convert.ToDecimal(r["Ventas"]);
                            table.Rows.Add(r);
                        }

                        table = (from tv in table.AsEnumerable()
                                         orderby tv.Field<string>("Año")
                                         select tv).CopyToDataTable();
                        gridDatos.DataSource = table;

                        //tblVendedores.TableName = "TablaVendedores";

                        gridDatos.Columns[(int)Columnas.Ventas].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        gridDatos.Columns[(int)Columnas.Pagos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        gridDatos.Columns[(int)Columnas.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        gridDatos.Columns[(int)Columnas.Ventas].DefaultCellStyle.Format = "C2";
                        gridDatos.Columns[(int)Columnas.Pagos].DefaultCellStyle.Format = "C2";
                        gridDatos.Columns[(int)Columnas.Porcentaje].DefaultCellStyle.Format = "P2";
                    }
                }
            }
            catch (Exception ex)
            {
                 MessageBox.Show(ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            if (exp.Exportar(gridDatos, false))
            {
                MessageBox.Show("El archivo se creo con exíto.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmHistorialVentasPagos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }

        private void gridDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
    }
}
