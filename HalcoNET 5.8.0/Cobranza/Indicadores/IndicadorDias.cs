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

namespace Cobranza.Indicadores
{
    public partial class IndicadorDias : Form
    {
        string Sucursal;

        private enum Columnas
        {
            Factura,
            Cliente,
            Nombre,
            FechaContabilizacion,
            FechaVto,
            Total,
            JefaCobranza,
            Vendedor
        }

        private void formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Factura].Width = 90;
            dgv.Columns[(int)Columnas.Cliente].Width = 90;
            dgv.Columns[(int)Columnas.Nombre].Width = 250;
            dgv.Columns[(int)Columnas.FechaContabilizacion].Width = 90;
            dgv.Columns[(int)Columnas.FechaVto].Width = 90;
            dgv.Columns[(int)Columnas.Total].Width = 90;

            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.JefaCobranza].Width = 110;
            dgv.Columns[(int)Columnas.Vendedor].Width = 110;
        }

        public IndicadorDias(string _sucursal)
        {
            InitializeComponent();
            Sucursal = _sucursal;
        }

        private void CargarSucursales()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    cbSucursal.DataSource = tbl;
                    cbSucursal.ValueMember = "Codigo";
                    cbSucursal.DisplayMember = "Nombre";
                }
            }
        }

        private void CargarJefesCobranza()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", 0);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);


                DataRow row = table.NewRow();
                row["Nombre"] = "Todas";
                row["Codigo"] = "";
                table.Rows.InsertAt(row, 0);

                comboBox1.DataSource = table;
                comboBox1.DisplayMember = "Nombre";
                comboBox1.ValueMember = "Nombre";
            }


        }

        private void IndicadorDias_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                toolStatus.Text = string.Empty;
                try
                {
                    this.CargarSucursales();
                    this.CargarJefesCobranza();
                    cbSucursal.SelectedValue = Sucursal;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@JefaCobranza", comboBox1.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                        DataTable tbl = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;

                        this.formato(dgvDatos);

                        toolStatus.Text = "Monto Total: " + Convert.ToDecimal(tbl.Compute("SUM(Total)", string.Empty)).ToString("C2");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbSucursal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                toolStatus.Text = string.Empty;
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@JefaCobranza", comboBox1.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                        DataTable tbl = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;

                        this.formato(dgvDatos);

                        toolStatus.Text = "Monto Total: " + Convert.ToDecimal(tbl.Compute("SUM(Total)", string.Empty)).ToString("C2");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = (sender as DataGridView);
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {

                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        } 
    }
}
