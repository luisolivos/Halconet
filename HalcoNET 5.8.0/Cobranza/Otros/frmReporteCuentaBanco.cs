using System;
using System.Collections;
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
    public partial class frmReporteCuentaBanco : Form
    {

        public frmReporteCuentaBanco()
        {
            InitializeComponent();
        }

        private enum Columnas {
            NombreBanco,
            TipoMoneda,
            Account,
            TIB,
            Efectivo,
            TPV,
            DepCheque,
            Transferencia,
            Total
        }

        private void formatoGridReporte() {
            try
            {
                dgvResultadoReporte.Columns[(int)Columnas.NombreBanco].HeaderText = "";
                dgvResultadoReporte.Columns[(int)Columnas.TipoMoneda].HeaderText = "Monda";
                dgvResultadoReporte.Columns[(int)Columnas.Account].HeaderText = "Cuenta";                
                dgvResultadoReporte.Columns[(int)Columnas.TIB].HeaderText = "TIB";
                dgvResultadoReporte.Columns[(int)Columnas.Efectivo].HeaderText = "EFECTIVO";
                dgvResultadoReporte.Columns[(int)Columnas.TPV].HeaderText = "TPV";
                dgvResultadoReporte.Columns[(int)Columnas.DepCheque].HeaderText = "DEP.CHEQUE";
                dgvResultadoReporte.Columns[(int)Columnas.Transferencia].HeaderText = "TRANSFERENCIA";
                dgvResultadoReporte.Columns[(int)Columnas.Total].HeaderText = "TOTAL";
                //
                dgvResultadoReporte.Columns[(int)Columnas.NombreBanco].Width = 120;
                dgvResultadoReporte.Columns[(int)Columnas.TipoMoneda].Width = 60;
                dgvResultadoReporte.Columns[(int)Columnas.Account].Width = 100;
                dgvResultadoReporte.Columns[(int)Columnas.TIB].Width = 100;
                dgvResultadoReporte.Columns[(int)Columnas.Efectivo].Width = 100;
                dgvResultadoReporte.Columns[(int)Columnas.TPV].Width = 100;
                dgvResultadoReporte.Columns[(int)Columnas.DepCheque].Width = 100;
                dgvResultadoReporte.Columns[(int)Columnas.Transferencia].Width = 110;
                dgvResultadoReporte.Columns[(int)Columnas.Total].Width = 100;
                //
                dgvResultadoReporte.Columns[(int)Columnas.TIB].DefaultCellStyle.Format = "C0";
                dgvResultadoReporte.Columns[(int)Columnas.Efectivo].DefaultCellStyle.Format = "C0";
                dgvResultadoReporte.Columns[(int)Columnas.TPV].DefaultCellStyle.Format = "C0";
                dgvResultadoReporte.Columns[(int)Columnas.DepCheque].DefaultCellStyle.Format = "C0";
                dgvResultadoReporte.Columns[(int)Columnas.Transferencia].DefaultCellStyle.Format = "C0";
                dgvResultadoReporte.Columns[(int)Columnas.Total].DefaultCellStyle.Format = "C0";
                dgvResultadoReporte.Columns[(int)Columnas.TIB].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultadoReporte.Columns[(int)Columnas.Efectivo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultadoReporte.Columns[(int)Columnas.TPV].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultadoReporte.Columns[(int)Columnas.DepCheque].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultadoReporte.Columns[(int)Columnas.Transferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultadoReporte.Columns[(int)Columnas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //
                dgvResultadoReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 32, 96);
                dgvResultadoReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvResultadoReporte.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                dgvResultadoReporte.ColumnHeadersDefaultCellStyle.Padding = new Padding(2, 1, 2, 15);
                foreach (DataGridViewColumn col in dgvResultadoReporte.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.ReadOnly = true;
                }
                dgvResultadoReporte.RowHeadersVisible = false;
            }
            catch (Exception ex) {
                System.Diagnostics.Debug.Write(ex.Message);
            }
        }
        DataTable Datos = new DataTable();
        private void cargarReporte() {
            Cursor = Cursors.WaitCursor;
            string cadenaConexion = ClasesSGUV.Propiedades.conectionSGUV;
            SqlConnection coneccion = new SqlConnection(cadenaConexion);
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date;

                SqlCommand cmd = new SqlCommand("sp_tbl_mttobancos", coneccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", 6);
                cmd.Parameters.AddWithValue("@banco", string.Empty);
                cmd.Parameters.AddWithValue("@tipoCredito", string.Empty);
                cmd.Parameters.AddWithValue("@tasa", string.Empty);
                cmd.Parameters.AddWithValue("@moneda", "RCB");
                cmd.Parameters.AddWithValue("@lineaAutorizada", decimal.Zero);
                cmd.Parameters.AddWithValue("@costoEmision", decimal.Zero);
                cmd.Parameters.AddWithValue("@garantias", string.Empty);
                cmd.Parameters.AddWithValue("@oSolidarios", string.Empty);
                cmd.Parameters.AddWithValue("@cuenta", string.Empty);
                cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                if (coneccion.State != ConnectionState.Open)
                    coneccion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                decimal Total = Convert.ToDecimal(dt.Compute("SUM(TOTAL)", "").ToString());
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[(int)Columnas.NombreBanco] = "Total: ";
                    dr[(int)Columnas.Account] = string.Empty;
                    dr[(int)Columnas.TIB] = Convert.ToDecimal(dt.Compute("SUM(TIB)", "").ToString());
                    dr[(int)Columnas.Efectivo] = Convert.ToDecimal(dt.Compute("SUM(EFECTIVO)", "").ToString());
                    dr[(int)Columnas.TPV] = Convert.ToDecimal(dt.Compute("SUM(TPV)", "").ToString());
                    dr[(int)Columnas.DepCheque] = Convert.ToDecimal(dt.Compute("SUM([DEP.CHEQUE])", "").ToString());
                    dr[(int)Columnas.Transferencia] = Convert.ToDecimal(dt.Compute("SUM(TRANSFERENCIA)", "").ToString());
                    dr[(int)Columnas.Total] = Total;
                    dt.Rows.Add(dr);
                    dt = (from tv in dt.AsEnumerable() select tv).CopyToDataTable();
                }
                
            

                Datos.Clear();
                dgvResultadoReporte.DataSource = dt;
                
                Datos = dt.Copy();
                formatoGridReporte();

                if (Total > decimal.Zero)
                {
                    dt.Columns.Add("Porcentaje", typeof(decimal), "TOTAL/" + Total);
                    Datos.Columns.Add("Porcentaje", typeof(decimal), "TOTAL/" + Total);

                    dgvResultadoReporte.Columns["Porcentaje"].DefaultCellStyle.Format = "P2";
                    dgvResultadoReporte.Columns["Porcentaje"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (SqlException sqlex) {
                MessageBox.Show("Error inesperado.\r\n" + sqlex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado.\r\n" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                coneccion.Close();
            }
            Cursor = Cursors.Default;
        }

        private void frmReporteCuentaBanco_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarReporte();
        }

        private void dgvResultadoReporte_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvResultadoReporte.Rows)
            {
                string celVal = row.Cells[(int)Columnas.NombreBanco].Value.ToString();
                switch (celVal)
                {
                    case "BANAMEX":
                        row.Cells[(int)Columnas.NombreBanco].Style.BackColor = Color.FromArgb(0, 112, 192);
                        row.Cells[(int)Columnas.NombreBanco].Style.Font = new Font("Arial", 8, FontStyle.Bold);
                        break;
                    case "BANCOMER":
                        row.Cells[(int)Columnas.NombreBanco].Style.BackColor = Color.FromArgb(1, 146, 255);
                        row.Cells[(int)Columnas.NombreBanco].Style.Font = new Font("Arial", 8, FontStyle.Bold);
                        break;
                    case "BANORTE":
                        row.Cells[(int)Columnas.NombreBanco].Style.BackColor = Color.FromArgb(59, 171, 255);
                        row.Cells[(int)Columnas.NombreBanco].Style.Font = new Font("Arial", 8, FontStyle.Bold);
                        break;
                    case "BANREGIO":
                        row.Cells[(int)Columnas.NombreBanco].Style.BackColor = Color.FromArgb(109, 192, 255);
                        row.Cells[(int)Columnas.NombreBanco].Style.Font = new Font("Arial", 8, FontStyle.Bold);
                        break;
                    case "HSBC":
                        row.Cells[(int)Columnas.NombreBanco].Style.BackColor = Color.FromArgb(155, 212, 255);
                        row.Cells[(int)Columnas.NombreBanco].Style.Font = new Font("Arial", 8, FontStyle.Bold);
                        break;
                    case "SANTANDER":
                        row.Cells[(int)Columnas.NombreBanco].Style.BackColor = Color.FromArgb(209, 235, 255);
                        row.Cells[(int)Columnas.NombreBanco].Style.Font = new Font("Arial", 8, FontStyle.Bold);
                        break;
                    default:
                        break;
                }

            }
        }

        private void dgvResultadoReporte_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                if (dgvResultadoReporte.Rows[e.RowIndex].Cells[(int)Columnas.NombreBanco].Value.ToString().Contains("Total"))
                {
   
                }
                else
                {
                    if (e.ColumnIndex == 0)
                    {
                        Brush gridColor = new SolidBrush(dgvResultadoReporte.GridColor);
                        Brush backColorCell = new SolidBrush(e.CellStyle.BackColor);
                        //
                        Pen gridLinePen = new Pen(gridColor);
                        e.Graphics.FillRectangle(backColorCell, e.CellBounds);
                        //
                        if (e.RowIndex < dgvResultadoReporte.Rows.Count && dgvResultadoReporte.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                        {
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                        }
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        //
                        if (String.IsNullOrEmpty(e.Value.ToString()))
                        {
                            if (e.RowIndex > 0 && dgvResultadoReporte.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
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
                                if (dgvResultadoReporte.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
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
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvResultadoReporte_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            decimal sumita = decimal.Zero;
            try
            {
                var seleccionadas = (sender as DataGridView).SelectedCells;
                foreach (DataGridViewCell item in seleccionadas)
                {
                    sumita += Convert.ToDecimal(item.Value);
                }

                toolSuma.Text = "Suma: " + sumita.ToString("C2");
            }
            catch (Exception)
            {
                toolSuma.Text = "Suma: " + sumita.ToString("C2");
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(Datos);
            if (checkBox1.Checked)
            {
                
                dv.RowFilter = "Total > 0";
                dgvResultadoReporte.DataSource = dv.ToTable();
            }
            else
                dgvResultadoReporte.DataSource = dv.Table;
        }
        
    }
}
