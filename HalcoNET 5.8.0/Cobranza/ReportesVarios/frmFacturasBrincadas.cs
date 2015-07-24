using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Cobranza
{
    public partial class frmFacturasBrincadas : Form
    {
        int __TipoReporte = 0;
        DataTable __Datos = new DataTable();

        public frmFacturasBrincadas(int _tipoReporte)
        {
            InitializeComponent();
            __TipoReporte = _tipoReporte;
            //grp1.Visible = _tipoReporte == (int)TipoReporte.FacturasPagos;
        }

        public enum ColumnasVentasPagos
        {
            Situacion,
            Cliente,
            Nombre,
            Grupo,
            Jefa,
            SaldoActual,
            CondPago,
            LimiteCredito,
            Ventas,
            Pagos
        }

        public enum ColumasFacturasPagos
        {
            Factura,
            Fecha,
            Cliente,
            Nombre,
            Sucursal,
            Situacion,
            Jefa,
            Total,
            Saldo
        }

        public void FormatoVentasPagos(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasVentasPagos.Situacion].Width = 80;
            dgv.Columns[(int)ColumnasVentasPagos.Cliente].Width = 80;
            dgv.Columns[(int)ColumnasVentasPagos.Nombre].Width = 150;
            dgv.Columns[(int)ColumnasVentasPagos.Grupo].Width = 80;
            dgv.Columns[(int)ColumnasVentasPagos.Jefa].Width = 90;
            dgv.Columns[(int)ColumnasVentasPagos.SaldoActual].Width = 80;
            dgv.Columns[(int)ColumnasVentasPagos.CondPago].Width = 80;
            dgv.Columns[(int)ColumnasVentasPagos.LimiteCredito].Width = 80;
            dgv.Columns[(int)ColumnasVentasPagos.Ventas].Width = 80;
            dgv.Columns[(int)ColumnasVentasPagos.Pagos].Width = 80;

            dgv.Columns[(int)ColumnasVentasPagos.SaldoActual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasVentasPagos.LimiteCredito].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasVentasPagos.Ventas].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasVentasPagos.Pagos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasVentasPagos.SaldoActual].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasVentasPagos.LimiteCredito].DefaultCellStyle.Format = "C2"; 
            dgv.Columns[(int)ColumnasVentasPagos.Ventas].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasVentasPagos.Pagos].DefaultCellStyle.Format = "C2";
        }

        public void FormatoFacturasPagos(DataGridView dgv)
        {
            dgv.Columns[(int)ColumasFacturasPagos.Factura].Width = 80;
            dgv.Columns[(int)ColumasFacturasPagos.Fecha].Width = 80;
            dgv.Columns[(int)ColumasFacturasPagos.Cliente].Width = 80;
            dgv.Columns[(int)ColumasFacturasPagos.Nombre].Width = 200;
            dgv.Columns[(int)ColumasFacturasPagos.Sucursal].Width = 80;
            dgv.Columns[(int)ColumasFacturasPagos.Situacion].Width = 80;
            dgv.Columns[(int)ColumasFacturasPagos.Jefa].Width = 80;
            dgv.Columns[(int)ColumasFacturasPagos.Total].Width = 80;
            dgv.Columns[(int)ColumasFacturasPagos.Saldo].Width = 80;

            dgv.Columns[(int)ColumasFacturasPagos.Total].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasFacturasPagos.Saldo].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumasFacturasPagos.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasFacturasPagos.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public enum TipoReporte
        {
            AnalisisVentasPagos = 1,
            FacturasPagos
        }

        private void CargarSucursales()
        {
            SqlCommand command = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 11);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            clbSucursal.DataSource = table;
            clbSucursal.DisplayMember = "Nombre";
            clbSucursal.ValueMember = "Codigo";
        }

        private void CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
            command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal.Trim());
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            clbCobranza.DataSource = table;
            clbCobranza.DisplayMember = "Nombre";
            clbCobranza.ValueMember = "Codigo";
        }

        public string GetCadena(CheckedListBox clb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in clb.CheckedItems)
            {
                if (clb.CheckedItems.Count != 0)
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clb.ToString().Equals(string.Empty))
                        {
                            stb.Append("'" + item["Nombre"].ToString() + "',");
                        }
                    }
            }

            return stb.ToString().Trim(',');

        }

        private void frmVarios_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                this.CargarJefesCobranza();
                this.CargarSucursales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clb_Click(object sender, EventArgs e)
        {
            CheckedListBox clb = (sender as CheckedListBox);
            if (clb.SelectedIndex == 0)
            {
                if (clb.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clb.Items.Count; item++)
                    {
                        clb.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clb.Items.Count; item++)
                    {
                        clb.SetItemChecked(item, true);
                    }
                }
            }
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                __Datos.Clear();

                string sucursales = GetCadena(clbSucursal);
                string jefas = GetCadena(clbCobranza);

                using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV)))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if(__TipoReporte == (int)TipoReporte.AnalisisVentasPagos)
                    command.Parameters.AddWithValue("@TipoConsulta", 9);
                    else if(__TipoReporte == (int)TipoReporte.FacturasPagos)
                    command.Parameters.AddWithValue("@TipoConsulta", 10);

                    command.Parameters.AddWithValue("@JefaCobranza", jefas);
                    command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                    command.Parameters.AddWithValue("@Sucursal", sucursales);
                    command.Parameters.AddWithValue("@Desde", dtpInicio.Value.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Hasta", dtpFin.Value.ToString("yyyy-MM-dd"));

                    command.CommandTimeout = 0;

                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable tbl = new DataTable();
                    da.SelectCommand = command;
                    da.Fill(__Datos);

                    gridFacturas.DataSource = __Datos;

                    if (__TipoReporte == (int)TipoReporte.AnalisisVentasPagos)
                        this.FormatoVentasPagos(gridFacturas);
                    else if (__TipoReporte == (int)TipoReporte.FacturasPagos)
                        this.FormatoFacturasPagos(gridFacturas);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                ClasesSGUV.Exportar expo = new ClasesSGUV.Exportar();
                expo.ExportarSinFormato(gridFacturas);
            }
            catch (Exception) { }
            
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                string _cliente = string.Empty;
                int i = 0;

                if (__TipoReporte == (int)TipoReporte.FacturasPagos)
                    foreach (DataGridViewRow row in (sender as DataGridView).Rows)
                    {
                        string _aux = row.Cells[(int)ColumasFacturasPagos.Cliente].Value.ToString();
                        if (!_cliente.Equals(_aux))
                        {
                            _cliente = _aux;
                            if (i > 0)
                            {
                                (sender as DataGridView).Rows[row.Index - 1].DividerHeight = 3; 
                            }

                        }
                        i++;

                        if (row.Cells[(int)ColumasFacturasPagos.Situacion].Value.ToString().Equals("No Pagado"))
                            row.Cells[(int)ColumasFacturasPagos.Situacion].Style.BackColor = Color.LightGray;
                    }
            }
            catch (Exception)
            {

            }
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                toolStripStatusLabel1.Text = string.Empty;
                var qry = from item in __Datos.AsEnumerable()
                          where item.Field<string>("Cliente").ToLower().Contains(txtCliente.Text.ToLower())
                          select item;

                if (qry.Count() > 0)
                {
                    gridFacturas.DataSource = qry.CopyToDataTable();
                    if (__TipoReporte == (int)TipoReporte.AnalisisVentasPagos)
                        this.FormatoVentasPagos(gridFacturas);
                    else if (__TipoReporte == (int)TipoReporte.FacturasPagos)
                        this.FormatoFacturasPagos(gridFacturas);

                    toolStripStatusLabel1.Text = "(" + qry.Count() + ") Resultados";
                }
                else
                    toolStripStatusLabel1.Text = "No se encontraron coincidencias para '" + txtCliente.Text + "'";
            }
            catch (Exception)
            {
                
                throw;
            }
        }


    }
}
