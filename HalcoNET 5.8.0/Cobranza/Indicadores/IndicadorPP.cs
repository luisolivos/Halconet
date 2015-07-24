using System;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.Indicadores
{
    public partial class IndicadorPP : Form
    {
        string Sucursal;
        string Mes;
        string Año;

        public enum Columas
        {
            Factura,
            FechaContabilizacion,
            FechaVencimiento,
            Cliente,
            Nombre,
            Sucursal,
            JC,
            Vendedor,
            Evaluacion,
            TotalFactura,
            PPAplicado,
            DiasTrans,
            Saldo,
            Pagos

        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columas.Factura].Width = 90;
            dgv.Columns[(int)Columas.FechaContabilizacion].Width = 90;
            dgv.Columns[(int)Columas.FechaVencimiento].Width = 90;
            dgv.Columns[(int)Columas.Cliente].Width = 80;
            dgv.Columns[(int)Columas.Nombre].Width = 110;
            dgv.Columns[(int)Columas.Sucursal].Width = 90;
            dgv.Columns[(int)Columas.JC].Width = 100;
            dgv.Columns[(int)Columas.Vendedor].Width = 100;
            dgv.Columns[(int)Columas.Evaluacion].Width = 70;
            dgv.Columns[(int)Columas.TotalFactura].Width = 90;
            dgv.Columns[(int)Columas.PPAplicado].Width = 90;
            dgv.Columns[(int)Columas.DiasTrans].Width = 90;
            dgv.Columns[(int)Columas.Saldo].Width = 90;
            dgv.Columns[(int)Columas.Pagos].Visible = false;

            dgv.Columns[(int)Columas.TotalFactura].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columas.PPAplicado].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columas.DiasTrans].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.Saldo].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columas.TotalFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columas.PPAplicado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columas.DiasTrans].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public IndicadorPP(string _sucursal, string _mes, string _año)
        {
            InitializeComponent();

            Sucursal = _sucursal;
            Mes = _mes;
            Año = _año;
            txtAño.Text = Año;
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

        private void CargarMeses()
        {
            DataTable Meses = new DataTable();
            Meses.Columns.Add("Index", typeof(int));
            Meses.Columns.Add("Mes", typeof(string));
            string[] array = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            for (int i = 0; i < 12; i++)
            {
                DataRow row = Meses.NewRow();
                row["Index"] = i;
                row["Mes"] = array[i];

                Meses.Rows.Add(row);
            }
            cbMes.DataSource = Meses;
            cbMes.DisplayMember = "Mes";
            cbMes.ValueMember = "Index";

        }

        private void IndicadorPP_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                try
                {
                    this.CargarSucursales();
                    this.CargarMeses();
                    cbSucursal.SelectedValue = Sucursal;
                    cbMes.SelectedIndex = int.Parse(Mes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Sucursal = cbSucursal.SelectedValue.ToString();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Fecha", new DateTime(Int32.Parse(txtAño.Text), cbMes.SelectedIndex + 1, 1).AddMonths(1).AddDays(-1));

                        DataTable tbl = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;

                        this.Formato(dgvDatos);


                        decimal _montoTotal = Convert.ToDecimal(tbl.Compute("SUM([Total factura])", string.Empty) == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([Total factura])", string.Empty));
                        decimal _montoDescontadoDentro = Convert.ToDecimal(tbl.Compute("SUM([PP Aplicado])", "[Evaluación] = 'OK'") == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([PP Aplicado])", "[Evaluación] = 'OK'"));
                        decimal _montoDescontadoFuera = Convert.ToDecimal(tbl.Compute("SUM([PP Aplicado])", "[Evaluación] = 'PP no valido'") == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([PP Aplicado])", "[Evaluación] = 'PP no valido'"));
                        decimal _montoGanado = Convert.ToDecimal(tbl.Compute("SUM([Total factura])", "[Evaluación] = 'SIN PP' AND Pagos > 0 AND Saldo = 0") == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([Total factura])", "[Evaluación] = 'SIN PP' AND Pagos > 0 AND Saldo = 0")) * (decimal)0.05;
                        decimal _saldo = Convert.ToDecimal(tbl.Compute("SUM([Saldo])", string.Empty) == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([Saldo])", string.Empty));


                        txtTotal.Text = _montoTotal.ToString("C2");
                        txtDentro.Text = _montoDescontadoDentro.ToString("C2");
                        txtFuera.Text = _montoDescontadoFuera.ToString("C2");
                        txtMontoGanado.Text = _montoGanado.ToString("C2");
                        txtSaldo.Text = _saldo.ToString("C2");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Fecha", new DateTime(Int32.Parse(txtAño.Text), cbMes.SelectedIndex + 1, 1).AddMonths(1).AddDays(-1));

                        DataTable tbl = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;

                        this.Formato(dgvDatos);


                        decimal _montoTotal = Convert.ToDecimal(tbl.Compute("SUM([Total factura])", string.Empty) == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([Total factura])", string.Empty)) * (decimal)0.05;
                        decimal _montoDescontadoDentro = Convert.ToDecimal(tbl.Compute("SUM([PP Aplicado])", "[Evaluación] = 'OK'") == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([PP Aplicado])", "[Evaluación] = 'OK'"));
                        decimal _montoDescontadoFuera = Convert.ToDecimal(tbl.Compute("SUM([PP Aplicado])", "[Evaluación] = 'PP no valido'") == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([PP Aplicado])", "[Evaluación] = 'PP no valido'"));
                        decimal _montoGanado = Convert.ToDecimal(tbl.Compute("SUM([Total factura])", "[Evaluación] = 'SIN PP' AND Pagos > 0 AND Saldo = 0") == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([Total factura])", "[Evaluación] = 'SIN PP' AND Pagos > 0 AND Saldo = 0")) * (decimal)0.05;
                        decimal _saldo = Convert.ToDecimal(tbl.Compute("SUM([Saldo])", string.Empty) == DBNull.Value ? decimal.Zero : tbl.Compute("SUM([Saldo])", string.Empty));


                        txtTotal.Text = _montoTotal.ToString("C2");
                        txtDentro.Text = _montoDescontadoDentro.ToString("C2");
                        txtFuera.Text = _montoDescontadoFuera.ToString("C2");
                        txtMontoGanado.Text= _montoGanado.ToString("C2");
                        txtSaldo.Text = _saldo.ToString("C2");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
