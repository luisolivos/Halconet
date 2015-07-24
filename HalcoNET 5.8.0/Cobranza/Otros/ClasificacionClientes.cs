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
using System.Globalization;

namespace Cobranza
{
    public partial class ClasificacionClientes : Form
    {
        public enum Columnas
        {
            Cliente,
            Nombre,
            Sucursal,
            Vendedor,
            Canal,
            Venta,
            Pago,
            Utilidad,
            PuntajeUM,
            PuntajeUT,
            PuntajeVM,
            PuntajeVT,
            PuntajePago,
            PuntajeFinal,
            Clasificacion,
            Halcon
        }

        public enum ColumnasDetalle
        {
            Cliente,
            Mes4,
            Mes3,
            Mes2,
            Mes1
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.PuntajeUM].Visible = false;
            dgv.Columns[(int)Columnas.PuntajeUT].Visible = false;
            dgv.Columns[(int)Columnas.PuntajeVM].Visible = false;
            dgv.Columns[(int)Columnas.PuntajeVT].Visible = false;
            dgv.Columns[(int)Columnas.PuntajePago].Visible = false;
            dgv.Columns[(int)Columnas.PuntajeFinal].Visible = false;

            dgv.Columns[(int)Columnas.Cliente].Width = 70;
            dgv.Columns[(int)Columnas.Nombre].Width = 200;
            dgv.Columns[(int)Columnas.Sucursal].Width = 100;
            dgv.Columns[(int)Columnas.Vendedor].Width = 110;
            dgv.Columns[(int)Columnas.Canal].Width = 90;
            dgv.Columns[(int)Columnas.Venta].Width = 80;
            dgv.Columns[(int)Columnas.Pago].Width = 80;
            dgv.Columns[(int)Columnas.Utilidad].Width = 80;

            dgv.Columns[(int)Columnas.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[(int)Columnas.Venta].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Pago].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Utilidad].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)Columnas.Halcon].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Pago].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Utilidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Halcon].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[(int)Columnas.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)Columnas.Nombre].HeaderText = "Nombre";
            dgv.Columns[(int)Columnas.Sucursal].HeaderText = "Sucursal";
            dgv.Columns[(int)Columnas.Vendedor].HeaderText = "Vendedor";
            dgv.Columns[(int)Columnas.Canal].HeaderText = "Canal";
            dgv.Columns[(int)Columnas.Venta].HeaderText = "Promedio\r\nde venta";
            dgv.Columns[(int)Columnas.Pago].HeaderText = "Promedio\r\ndías pago";
            dgv.Columns[(int)Columnas.Utilidad].HeaderText = "Promedio\r\nutilidad";
            dgv.Columns[(int)Columnas.Halcon].HeaderText = "Lineas\r\nHalcón";
            dgv.Columns[(int)Columnas.Clasificacion].HeaderText = "Clasificación";
        }

        public void Formato(DataGridView dgv, string _tipo)
        {
            DateTimeFormatInfo formatoFecha = CultureInfo.CurrentCulture.DateTimeFormat;

            dgv.Columns[(int)ColumnasDetalle.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)ColumnasDetalle.Mes4].HeaderText = _tipo + " " + formatoFecha.GetMonthName(DateTime.Now.AddMonths(-4).Month);
            dgv.Columns[(int)ColumnasDetalle.Mes3].HeaderText = _tipo + " " + formatoFecha.GetMonthName(DateTime.Now.AddMonths(-3).Month);
            dgv.Columns[(int)ColumnasDetalle.Mes2].HeaderText = _tipo + " " + formatoFecha.GetMonthName(DateTime.Now.AddMonths(-2).Month);
            dgv.Columns[(int)ColumnasDetalle.Mes1].HeaderText = _tipo + " " + formatoFecha.GetMonthName(DateTime.Now.AddMonths(-1).Month);

            if (_tipo.Equals("Venta") || _tipo.Equals("Pagos"))
            {
                dgv.Columns[(int)ColumnasDetalle.Mes4].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDetalle.Mes3].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDetalle.Mes2].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDetalle.Mes1].DefaultCellStyle.Format = "C2";
            }
            else if (_tipo.Equals("Utilidad"))
            {
                dgv.Columns[(int)ColumnasDetalle.Mes4].DefaultCellStyle.Format = "P2";
                dgv.Columns[(int)ColumnasDetalle.Mes3].DefaultCellStyle.Format = "P2";
                dgv.Columns[(int)ColumnasDetalle.Mes2].DefaultCellStyle.Format = "P2";
                dgv.Columns[(int)ColumnasDetalle.Mes1].DefaultCellStyle.Format = "P2";
            }

            dgv.Columns[(int)ColumnasDetalle.Mes4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.Mes3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.Mes2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.Mes1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public ClasificacionClientes()
        {
            InitializeComponent();
        }

        private void Esperar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }

        public DataTable DataSource(int _tipoConsulta, string _inicio)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_ClasificacionClientes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", _tipoConsulta);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Vendedor", string.Empty);
                    command.Parameters.AddWithValue("@Canal", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.SelectCommand.CommandTimeout = 0;
                    da.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = _inicio;
                    row["Codigo"] = "0";
                    table.Rows.InsertAt(row, 0);

                    return table;
                }
            }

        }

        private void ClasificacionClientes_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                cbSucursal.DataSource = this.DataSource(1, "TODAS");
                cbSucursal.ValueMember = "Codigo";
                cbSucursal.DisplayMember = "Nombre";

                cbVendedor.DataSource = this.DataSource(2, "TODOS");
                cbVendedor.ValueMember = "Codigo";
                cbVendedor.DisplayMember = "Nombre";

                cbCanal.DataSource = this.DataSource(3, "TODOS");
                cbCanal.ValueMember = "Codigo";
                cbCanal.DisplayMember = "Nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                DataTable table = new DataTable();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_ClasificacionClientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Vendedor", cbVendedor.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Canal", cbCanal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Cliente", string.Empty);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;
                        da.Fill(table);

                        dgvClasificacion.DataSource = table;

                        this.Formato(dgvClasificacion);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void dgvClasificacion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row;
            try
            {
                this.Esperar();

                row = (sender as DataGridView).Rows[e.RowIndex];
                DataTable table = new DataTable();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_ClasificacionClientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (e.ColumnIndex == (int)Columnas.Venta)
                        {
                            command.Parameters.AddWithValue("@TipoConsulta", 5);
                        }
                        if (e.ColumnIndex == (int)Columnas.Pago)
                        {
                            command.Parameters.AddWithValue("@TipoConsulta", 6);
                        }
                        if (e.ColumnIndex == (int)Columnas.Utilidad)
                        {
                            command.Parameters.AddWithValue("@TipoConsulta", 7);
                        }
                        command.Parameters.AddWithValue("@Sucursal", string.Empty);
                        command.Parameters.AddWithValue("@Vendedor", string.Empty);
                        command.Parameters.AddWithValue("@Canal", string.Empty);
                        command.Parameters.AddWithValue("@Cliente", row.Cells[(int)Columnas.Cliente].Value.ToString());

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;
                        da.Fill(table);


                    }
                }
                dgvDetalle.DataSource = table;

                if (e.ColumnIndex == (int)Columnas.Venta)
                {
                    this.Formato(dgvDetalle, "Venta");
                }
                if (e.ColumnIndex == (int)Columnas.Pago)
                {
                    this.Formato(dgvDetalle, "Pagos");
                }
                if (e.ColumnIndex == (int)Columnas.Utilidad)
                {
                    this.Formato(dgvDetalle, "Utilidad");
                }


            }
            catch (Exception)
            {

            }
            finally
            {
                this.Continuar();
            }

            try
            {
                row = (sender as DataGridView).Rows[e.RowIndex];

                if (e.ColumnIndex == (int)Columnas.Clasificacion)
                {
                    txtUtilidad.Text = (Convert.ToInt32(row.Cells[(int)Columnas.PuntajeUM].Value) + Convert.ToInt32(row.Cells[(int)Columnas.PuntajeUT].Value)).ToString();
                    txtPago.Text = Convert.ToInt32(row.Cells[(int)Columnas.PuntajePago].Value).ToString();
                    txtVenta.Text = (Convert.ToInt32(row.Cells[(int)Columnas.PuntajeVM].Value) + Convert.ToInt32(row.Cells[(int)Columnas.PuntajeVT].Value)).ToString();

                    txtFinal.Text = Convert.ToInt32(row.Cells[(int)Columnas.PuntajeFinal].Value).ToString();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
