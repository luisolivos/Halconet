using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Cobranza
{
    public partial class frmReporteBancos : Form
    {
        private decimal TOTALMXPLINEA = 0;
        private decimal TOTALUSDLINEA = 0;
        private decimal TOTALUSDOCUPACION = 0;
        private decimal TOTALMXPOCUPACION = 0;
        private decimal TIPOCAMBIO = 0;
        private decimal TOTALBANCOSLINEA = 0;
        private decimal TOTALBANCOSOCUPACION = 0;

        public frmReporteBancos()
        {
            InitializeComponent();
        }

        private enum TipoStatement
        {
            Insert = 1,
            Update = 2,
            Select = 3,
            UpdateTC = 4,
            Delete = 5
        }

        private enum ColumasReporte
        {
            Banco,
            TipoCredito,
            Tasa,
            Moneda,
            LineaAut,
            OcupacionLineaUSD,
            OcupacionLineaMXP,
            DisponibleUSD,
            DisponibleMXP,
            CostoEmision,
            Garantias,
            OSolidarios,
            Cuenta
        }

        private void formatoGridReporte()
        {
            try
            {
                dgvMttoBancosReporte.RowHeadersVisible = false;
                //
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.Banco].Width = 100;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.TipoCredito].Width = 180;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.Tasa].Width = 50;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.Moneda].Width = 50;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.LineaAut].Width = 80;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.OcupacionLineaUSD].Width = 90;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.OcupacionLineaMXP].Width = 90;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.DisponibleUSD].Width = 90;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.DisponibleMXP].Width = 90;

                dgvMttoBancosReporte.Columns[(int)ColumasReporte.CostoEmision].Width = 120;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.Garantias].Width = 200;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.OSolidarios].Width = 250;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.Cuenta].Visible = false;
                //
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.OSolidarios].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.LineaAut].DefaultCellStyle.Format = "C0";
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.OcupacionLineaUSD].DefaultCellStyle.Format = "C0";
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.OcupacionLineaMXP].DefaultCellStyle.Format = "C0";
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.DisponibleUSD].DefaultCellStyle.Format = "C0";
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.DisponibleMXP].DefaultCellStyle.Format = "C0";
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.CostoEmision].DefaultCellStyle.Format = "C0";
                //
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.LineaAut].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.OcupacionLineaUSD].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.OcupacionLineaMXP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.DisponibleUSD].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.DisponibleMXP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.Tasa].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvMttoBancosReporte.Columns[(int)ColumasReporte.Moneda].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //
                dgvMttoBancosReporte.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                dgvMttoBancosReporte.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 78, 120);
                dgvMttoBancosReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 78, 120); ;
                dgvMttoBancosReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvMttoBancosReporte.RowHeadersDefaultCellStyle.ForeColor = Color.White;
                //
                foreach (DataGridViewColumn col in dgvMttoBancosReporte.Columns)
                {
                    if (col.Index == 0)
                    {
                        col.DefaultCellStyle.BackColor = Color.FromArgb(221, 235, 247);
                        col.DefaultCellStyle.Font = new Font("Arial", 7, FontStyle.Bold);
                    }
                    col.ReadOnly = true;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void generarReporte()
        {
            SqlConnection coneccion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_mttobancos", coneccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                //
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Select);
                cmd.Parameters.AddWithValue("@banco", string.Empty);
                cmd.Parameters.AddWithValue("@tipoCredito", string.Empty);
                cmd.Parameters.AddWithValue("@tasa", string.Empty);
                cmd.Parameters.AddWithValue("@moneda", string.Empty);
                cmd.Parameters.AddWithValue("@lineaAutorizada", decimal.Zero);
                cmd.Parameters.AddWithValue("@costoEmision", decimal.Zero);
                cmd.Parameters.AddWithValue("@garantias", string.Empty);
                cmd.Parameters.AddWithValue("@oSolidarios", string.Empty);
                cmd.Parameters.AddWithValue("@cuenta", string.Empty);
                cmd.Parameters.AddWithValue("@fechaReferencia ", dtpFiltroFecha.Value.Date);

                if (coneccion.State != ConnectionState.Open)
                    coneccion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (dt.Rows.Count == 0)
                {
                    dgvMttoBancosReporte.DataSource = dt;
                    this.formatoGridReporte();
                    return;
                }

                decimal totalMXP = 0;
                decimal totalUSD = 0;
                decimal totalOcupacionMXP;
                decimal totalOcupacionUSD;

                decimal lineaAutorizadaMXP = Convert.ToDecimal(dt.Compute("SUM([Linea Autorizada])", "Moneda = 'MXP'"));
                decimal lineaAutorizadaUSD = Convert.ToDecimal(dt.Compute("SUM([Linea Autorizada])", "Moneda = 'USD'"));

                totalOcupacionMXP = Convert.ToDecimal(dt.Compute("SUM([Ocupación de la Linea MXP])", "Moneda = 'MXP'"));
                totalOcupacionUSD = Convert.ToDecimal(dt.Compute("SUM([Ocupación de la Linea USD])", "Moneda = 'USD'"));

                totalMXP = Convert.ToDecimal(dt.Compute("SUM([Disponible MXP])", "Moneda = 'MXP'"));
                totalUSD = Convert.ToDecimal(dt.Compute("SUM([Disponible USD])", "Moneda = 'USD'"));
                
                //TOTALMXPLINEA = totalMXP;
                //TOTALUSDLINEA = totalUSD;
                //TOTALMXPOCUPACION = totalOcupacionMXP;
                //TOTALUSDOCUPACION = totalOcupacionUSD;
                this.calcularTotalBancos();
                #region Nuevas filas con los totales.

                DataRow drTMXP = dt.NewRow();
                drTMXP[(int)ColumasReporte.Banco] = string.Empty;
                drTMXP[(int)ColumasReporte.TipoCredito] = "Total";
                drTMXP[(int)ColumasReporte.Tasa] = string.Empty;
                drTMXP[(int)ColumasReporte.Moneda] = string.Empty;
                drTMXP[(int)ColumasReporte.LineaAut] = decimal.Zero;
                drTMXP[(int)ColumasReporte.OcupacionLineaMXP] = totalOcupacionMXP;
                drTMXP[(int)ColumasReporte.OcupacionLineaUSD] = totalOcupacionUSD;
                drTMXP[(int)ColumasReporte.DisponibleMXP] = totalMXP;
                drTMXP[(int)ColumasReporte.DisponibleUSD] = totalUSD;
                drTMXP[(int)ColumasReporte.Cuenta] = string.Empty;
                dt.Rows.Add(drTMXP);

                DataRow drTMXPTOTAL = dt.NewRow();
                drTMXPTOTAL[(int)ColumasReporte.Banco] = string.Empty;
                drTMXPTOTAL[(int)ColumasReporte.TipoCredito] = "Total MXP:";
                drTMXPTOTAL[(int)ColumasReporte.Tasa] = string.Empty;
                drTMXPTOTAL[(int)ColumasReporte.Moneda] = string.Empty;
                drTMXPTOTAL[(int)ColumasReporte.LineaAut] = lineaAutorizadaMXP + (lineaAutorizadaUSD * TIPOCAMBIO);
                drTMXPTOTAL[(int)ColumasReporte.OcupacionLineaMXP] = totalOcupacionMXP + (totalOcupacionUSD * TIPOCAMBIO);
                drTMXPTOTAL[(int)ColumasReporte.DisponibleMXP] = totalMXP + (totalUSD * TIPOCAMBIO);
                drTMXPTOTAL[(int)ColumasReporte.Cuenta] = string.Empty;
                dt.Rows.Add(drTMXPTOTAL);

                ////
                //DataRow drTUSD = dt.NewRow();
                //drTUSD[(int)ColumasReporte.Banco] = string.Empty;
                //drTUSD[(int)ColumasReporte.TipoCredito] = string.Empty;
                //drTUSD[(int)ColumasReporte.Tasa] = string.Empty;
                //drTUSD[(int)ColumasReporte.Moneda] = "Total USD:";
                //drTUSD[(int)ColumasReporte.LineaAut] = totalUSD;
                //drTUSD[(int)ColumasReporte.OcupacionLinea] = totalOcupacionUSD;
                //drTUSD[(int)ColumasReporte.Cuenta] = string.Empty;
                //dt.Rows.Add(drTUSD);
                ////
                //DataRow drTotalBancos = dt.NewRow();
                //drTotalBancos[(int)ColumasReporte.Banco] = string.Empty;
                //drTotalBancos[(int)ColumasReporte.TipoCredito] = string.Empty;
                //drTotalBancos[(int)ColumasReporte.Tasa] = string.Empty;
                //drTotalBancos[(int)ColumasReporte.Moneda] = "Total Bancos:";
                //drTotalBancos[(int)ColumasReporte.LineaAut] = TOTALBANCOSLINEA;
                //drTotalBancos[(int)ColumasReporte.OcupacionLinea] = TOTALBANCOSOCUPACION;
                //drTotalBancos[(int)ColumasReporte.Cuenta] = string.Empty;
                //dt.Rows.Add(drTotalBancos);
                //
                #endregion
                dgvMttoBancosReporte.DataSource = dt;
                this.formatoGridReporte();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                coneccion.Close();
            }
        }

        private void calcularTotalBancos()
        {
            try
            {
                decimal totalBancos = 0, totalBancosOcupacion = 0;
                totalBancos = ((TOTALUSDLINEA * TIPOCAMBIO) + TOTALMXPLINEA);
                totalBancosOcupacion = ((TOTALUSDOCUPACION * TIPOCAMBIO) + TOTALMXPOCUPACION);
                TOTALBANCOSLINEA = totalBancos;
                TOTALBANCOSOCUPACION = totalBancosOcupacion;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getTCFromBD()
        {
            SqlConnection coneccion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_mttobancos", coneccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                //
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Select);
                cmd.Parameters.AddWithValue("@banco", string.Empty);
                cmd.Parameters.AddWithValue("@tipoCredito", string.Empty);
                cmd.Parameters.AddWithValue("@tasa", string.Empty);
                cmd.Parameters.AddWithValue("@moneda", "TC");
                cmd.Parameters.AddWithValue("@lineaAutorizada", decimal.Zero);
                cmd.Parameters.AddWithValue("@costoEmision", decimal.Zero);
                cmd.Parameters.AddWithValue("@garantias", string.Empty);
                cmd.Parameters.AddWithValue("@oSolidarios", string.Empty);
                cmd.Parameters.AddWithValue("@cuenta", string.Empty);
                cmd.Parameters.AddWithValue("@fechaReferencia ", dtpFiltroFecha.Value.Date);

                if (coneccion.State != ConnectionState.Open)
                    coneccion.Open();
                decimal TC = Convert.ToDecimal(cmd.ExecuteScalar().ToString());
                TIPOCAMBIO = TC;
                txtTC.Text = TC.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                coneccion.Close();
            }
        }

        private void frmReporteBancos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            this.getTCFromBD();
        }

        private void btnLoadReporte_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.generarReporte();
            this.calcularTotalBancos();
            Cursor = Cursors.Default;
        }

        private void btnUpdateTC_Click(object sender, EventArgs e)
        {
            string tipoCambio = txtTC.Text.Trim();
            if (String.IsNullOrEmpty(tipoCambio))
            {
                MessageBox.Show("El valor no tiene el formato correcto.", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTC.Focus();
                return;
            }
            decimal test = 0;
            if (!decimal.TryParse(tipoCambio, out test))
            {
                MessageBox.Show("El valor no tiene el formato correcto.", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTC.Focus();
                return;
            }
            //SqlConnection coneccion = new SqlConnection(cadenaConexion);
            try
            {
                /*SqlCommand cmd = new SqlCommand("sp_tbl_mttobancos", coneccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                //
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.UpdateTC);
                cmd.Parameters.AddWithValue("@banco", string.Empty);
                cmd.Parameters.AddWithValue("@tipoCredito", string.Empty);
                cmd.Parameters.AddWithValue("@tasa", string.Empty);
                cmd.Parameters.AddWithValue("@moneda", string.Empty);
                cmd.Parameters.AddWithValue("@lineaAutorizada", decimal.Zero);
                cmd.Parameters.AddWithValue("@costoEmision", decimal.Zero);
                cmd.Parameters.AddWithValue("@garantias", string.Empty);
                cmd.Parameters.AddWithValue("@oSolidarios", string.Empty);
                cmd.Parameters.AddWithValue("@cuenta", string.Empty);
                cmd.Parameters.AddWithValue("@tc", Convert.ToDecimal(tipoCambio.Trim()));
                SqlParameter pSalida = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                pSalida.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pSalida);
                if (coneccion.State != ConnectionState.Open)
                    coneccion.Open();
                cmd.ExecuteNonQuery();
                string msn = Convert.ToString(cmd.Parameters["@MensajeSuccess"].Value.ToString());
                MessageBox.Show(msn, "Suceso.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.getTCFromBD();*/
                decimal TC = Convert.ToDecimal(tipoCambio);
                TIPOCAMBIO = TC;
                txtTC.Text = TC.ToString();
                btnLoadReporte_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //coneccion.Close();
            }
        }

        private void dgvMttoBancosReporte_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvMttoBancosReporte.Rows)
                {
                    string valor = row.Cells[(int)ColumasReporte.TipoCredito].Value.ToString();
                    if (valor.Contains("Total"))
                    {
                        row.Cells[(int)ColumasReporte.Banco].Style.BackColor = Color.White;
                        if (valor.Contains("Total Bancos"))
                        {
                            row.Cells[(int)ColumasReporte.TipoCredito].Style.BackColor = Color.FromArgb(91, 155, 213);
                            row.Cells[(int)ColumasReporte.TipoCredito].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.OcupacionLineaUSD].Style.BackColor = Color.FromArgb(91, 155, 213);
                            row.Cells[(int)ColumasReporte.OcupacionLineaUSD].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.OcupacionLineaMXP].Style.BackColor = Color.FromArgb(91, 155, 213);
                            row.Cells[(int)ColumasReporte.OcupacionLineaMXP].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.DisponibleUSD].Style.BackColor = Color.FromArgb(91, 155, 213);
                            row.Cells[(int)ColumasReporte.DisponibleUSD].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.DisponibleMXP].Style.BackColor = Color.FromArgb(91, 155, 213);
                            row.Cells[(int)ColumasReporte.DisponibleMXP].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.LineaAut].Style.BackColor = Color.FromArgb(91, 155, 213);
                            row.Cells[(int)ColumasReporte.LineaAut].Style.Font = new Font("Arial", 8, FontStyle.Bold);
                        }
                        else
                        {
                            row.Cells[(int)ColumasReporte.TipoCredito].Style.BackColor = Color.FromArgb(112, 173, 71);
                            row.Cells[(int)ColumasReporte.TipoCredito].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.OcupacionLineaUSD].Style.BackColor = Color.FromArgb(112, 173, 71);
                            row.Cells[(int)ColumasReporte.OcupacionLineaUSD].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.OcupacionLineaUSD].Style.BackColor = Color.FromArgb(112, 173, 71);
                            row.Cells[(int)ColumasReporte.OcupacionLineaUSD].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.OcupacionLineaMXP].Style.BackColor = Color.FromArgb(112, 173, 71);
                            row.Cells[(int)ColumasReporte.OcupacionLineaMXP].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.DisponibleUSD].Style.BackColor = Color.FromArgb(112, 173, 71);
                            row.Cells[(int)ColumasReporte.DisponibleUSD].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.DisponibleMXP].Style.BackColor = Color.FromArgb(112, 173, 71);
                            row.Cells[(int)ColumasReporte.DisponibleMXP].Style.Font = new Font("Arial", 8, FontStyle.Bold);

                            row.Cells[(int)ColumasReporte.LineaAut].Style.BackColor = Color.FromArgb(112, 173, 71);
                            row.Cells[(int)ColumasReporte.LineaAut].Style.Font = new Font("Arial", 8, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void dgvMttoBancosReporte_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (dgvMttoBancosReporte.Rows[e.RowIndex].Cells[(int)ColumasReporte.Moneda].Value.ToString().Contains("Total"))
                {

                }
                else
                {
                    if (e.ColumnIndex == 0)
                    {
                        Brush gridColor = new SolidBrush(dgvMttoBancosReporte.GridColor);
                        Brush backColorCell = new SolidBrush(e.CellStyle.BackColor);
                        //
                        Pen gridLinePen = new Pen(gridColor);
                        e.Graphics.FillRectangle(backColorCell, e.CellBounds);
                        //
                        if (e.RowIndex < dgvMttoBancosReporte.Rows.Count && dgvMttoBancosReporte.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                        {
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                        }
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        //
                        if (String.IsNullOrEmpty(e.Value.ToString()))
                        {
                            if (e.RowIndex > 0 && dgvMttoBancosReporte.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
                            {

                            }
                            else
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 5, StringFormat.GenericDefault);
                            }
                        }
                        else
                        {
                            if (e.RowIndex == 0)
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 5, StringFormat.GenericDefault);
                            }
                            if (e.RowIndex > 0)
                            {
                                if (dgvMttoBancosReporte.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                                {
                                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 5, StringFormat.GenericDefault);
                                }
                            }
                        }
                        e.Handled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.getTCFromBD();
            this.generarReporte();
        }
    }
}
