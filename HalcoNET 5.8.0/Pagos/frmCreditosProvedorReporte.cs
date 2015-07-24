using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pagos
{
    public partial class frmCreditosProvedorReporte : Form
    {
        private string cadenaConexion = ClasesSGUV.Propiedades.conectionSGUV;

        public frmCreditosProvedorReporte()
        {
            InitializeComponent();
        }
        
        private enum TipoStatement
        {
            Insert = 1,
            Update = 2,
            Select = 3,
            Delete = 4,
            UpdateLiberado = 5
        }
        private enum Columnas { 
            Periodicidad,
            Provedor,
            Liberacion,
            Check1,
            Enero,
            Check2,
            Febrero,
            Check3,
            Marzo,
            Check4,
            Abril,
            Check5,
            Mayo,
            Check6,
            Junio,
            Check7,
            Julio,
            Check8,
            Agosto,
            Check9,
            Septiembre,
            Check10,
            Octubre,
            Check11,
            Noviembre,
            Check12,
            Diciembre,
            Total,
            Comentario,
            Anio
        }


        private void formatoGrid() {
            try
            {
                //dgvCPRResultado.Columns[(int)Columnas].HeaderText = "";
                dgvCPRResultado.Columns[(int)Columnas.Periodicidad].Width = 80;
                dgvCPRResultado.Columns[(int)Columnas.Provedor].Width = 100;
                dgvCPRResultado.Columns[(int)Columnas.Liberacion].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Enero].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Febrero].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Marzo].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Abril].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Mayo].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Junio].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Julio].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Agosto].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Septiembre].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Octubre].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Noviembre].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Diciembre].Width = 70;
                dgvCPRResultado.Columns[(int)Columnas.Total].Width = 100;
                dgvCPRResultado.Columns[(int)Columnas.Comentario].Width = 100;
                dgvCPRResultado.Columns[(int)Columnas.Anio].Visible = false;
                //
                DataGridViewCellStyle dgvcs = new DataGridViewCellStyle();
                dgvcs.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvcs.Format = "C2";

                dgvCPRResultado.Columns[(int)Columnas.Enero].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Febrero].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Marzo].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Abril].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Mayo].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Junio].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Julio].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Agosto].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Septiembre].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Octubre].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Noviembre].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Diciembre].DefaultCellStyle = dgvcs;
                dgvCPRResultado.Columns[(int)Columnas.Total].DefaultCellStyle = dgvcs;
                //
                dgvCPRResultado.Columns[(int)Columnas.Check1].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check2].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check3].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check4].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check5].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check6].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check7].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check8].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check9].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check10].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check11].HeaderText = string.Empty;
                dgvCPRResultado.Columns[(int)Columnas.Check12].HeaderText = string.Empty;
                //
                dgvCPRResultado.Columns[(int)Columnas.Check1].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check2].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check3].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check4].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check5].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check6].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check7].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check8].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check9].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check10].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check11].Width = 20;
                dgvCPRResultado.Columns[(int)Columnas.Check12].Width = 20;

                foreach (DataGridViewColumn col in dgvCPRResultado.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (col.Index == (int)Columnas.Check1 || col.Index == (int)Columnas.Check2 || col.Index == (int)Columnas.Check3 || col.Index == (int)Columnas.Check4 || col.Index == (int)Columnas.Check5 || col.Index == (int)Columnas.Check6 || col.Index == (int)Columnas.Check7 || col.Index == (int)Columnas.Check8 || col.Index == (int)Columnas.Check9 || col.Index == (int)Columnas.Check10 || col.Index == (int)Columnas.Check11 || col.Index == (int)Columnas.Check12)
                        col.ReadOnly = false;
                    else
                        col.ReadOnly = true;
                }
                dgvCPRResultado.RowHeadersVisible = false;
                dgvCPRResultado.ColumnHeadersDefaultCellStyle.Padding = new Padding(2, 5, 2, 3);
                dgvCPRResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCreditosProvedorReporte_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            cargarDatosReporte();
        }

        private void cargarDatosReporte() {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_Creditos_Provedor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Select);
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@provedor",string.Empty);
                cmd.Parameters.AddWithValue("@liberacion", string.Empty);
                cmd.Parameters.AddWithValue("@periodicidad", string.Empty);
                cmd.Parameters.AddWithValue("@mes", 0);
                cmd.Parameters.AddWithValue("@anio", 0);
                cmd.Parameters.AddWithValue("@importe", 0);
                cmd.Parameters.AddWithValue("@comentario", string.Empty);
                cmd.Parameters.AddWithValue("@tipoSelect", 2);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DataRow dr = dt.NewRow();
                dr["Periodicidad"] = string.Empty;
                dr["Provedor"] = string.Empty;
                dr["Liberacion"] = string.Empty;
                dr["Liberado"] = DBNull.Value;
                dr["Enero"] = dt.Compute("SUM(Enero)", string.Empty);
                dr["Febrero"] = dt.Compute("SUM(Febrero)", string.Empty);
                dr["Marzo"] = dt.Compute("SUM(Marzo)", string.Empty);
                dr["Abril"] = dt.Compute("SUM(Abril)", string.Empty);
                dr["Mayo"] = dt.Compute("SUM(Mayo)", string.Empty);
                dr["Junio"] = dt.Compute("SUM(Junio)", string.Empty);
                dr["Julio"] = dt.Compute("SUM(Julio)", string.Empty);
                dr["Agosto"] = dt.Compute("SUM(Agosto)", string.Empty);
                dr["Septiembre"] = dt.Compute("SUM(Septiembre)", string.Empty);
                dr["Octubre"] = dt.Compute("SUM(Octubre)", string.Empty);
                dr["Noviembre"] = dt.Compute("SUM(Noviembre)", string.Empty);
                dr["Diciembre"] = dt.Compute("SUM(Diciembre)", string.Empty);
                dr["Anio"] = 0;
                dt.Rows.Add(dr);
                dt = (from item in dt.AsEnumerable() select item).CopyToDataTable();
                dgvCPRResultado.DataSource = dt;
                formatoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conexion.Close();
            }
        }

        private void dgvCPRResultado_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                if (dgvCPRResultado.Rows[e.RowIndex].Cells[(int)Columnas.Provedor].Value.ToString() ==string.Empty )
                {

                }
                else
                {
                    if (e.ColumnIndex == 0)
                    {
                        Brush gridColor = new SolidBrush(dgvCPRResultado.GridColor);
                        Brush backColorCell = new SolidBrush(e.CellStyle.BackColor);
                        //
                        Pen gridLinePen = new Pen(gridColor);
                        e.Graphics.FillRectangle(backColorCell, e.CellBounds);
                        //
                        if (e.RowIndex < dgvCPRResultado.Rows.Count && dgvCPRResultado.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                        {
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                        }
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        //
                        if (String.IsNullOrEmpty(e.Value.ToString()))
                        {
                            if (e.RowIndex > 0 && dgvCPRResultado.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
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
                                if (dgvCPRResultado.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
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

        private void dgvCPRResultado_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            foreach (DataGridViewRow row in dgvCPRResultado.Rows)
            {
                if (row.Index == (dgvCPRResultado.RowCount - 1))
                    break;

                /*int mes = 12; //DateTime.Now.Month;
                int count = 1;
                for (int i = 3; i < row.Cells.Count - 3; i=i+2) 
                {
                    if (count > mes)
                        break;
                    //if (i % 3 != 0)
                        //continue;
                    if (Convert.ToBoolean(row.Cells[i].Value))
                    {
                        row.Cells[i +1].Style.BackColor = Color.FromArgb(60, 179, 113);
                        row.Cells[i+1].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.Cells[i+1].Style.BackColor = Color.White;
                        row.Cells[i+1].Style.ForeColor = Color.Black;
                    }
                    count++;
                }*/

                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check1].Value))
                {
                    row.Cells[(int)Columnas.Enero].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Enero].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[(int)Columnas.Enero].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Enero].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check2].Value))
                {
                    row.Cells[(int)Columnas.Febrero].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Febrero].Style.ForeColor = Color.Black;
                }
                else
                {
                    row.Cells[(int)Columnas.Febrero].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Febrero].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check3].Value))
                {
                    row.Cells[(int)Columnas.Marzo].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Marzo].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Marzo].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Marzo].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check4].Value))
                {
                    row.Cells[(int)Columnas.Abril].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Abril].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Abril].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Abril].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check5].Value))
                {
                    row.Cells[(int)Columnas.Mayo].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Mayo].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Mayo].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Mayo].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check6].Value))
                {
                    row.Cells[(int)Columnas.Junio].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Junio].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Junio].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Junio].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check7].Value))
                {
                    row.Cells[(int)Columnas.Julio].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Julio].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Julio].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Julio].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check8].Value))
                {
                    row.Cells[(int)Columnas.Agosto].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Agosto].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Agosto].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Agosto].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check9].Value))
                {
                    row.Cells[(int)Columnas.Septiembre].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Septiembre].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Septiembre].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Septiembre].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check10].Value))
                {
                    row.Cells[(int)Columnas.Octubre].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Octubre].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Octubre].Style.BackColor = Color.White; 
                    row.Cells[(int)Columnas.Octubre].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check11].Value))
                {
                    row.Cells[(int)Columnas.Noviembre].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Noviembre].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Noviembre].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Noviembre].Style.ForeColor = Color.Black;
                }
                if (Convert.ToBoolean(row.Cells[(int)Columnas.Check12].Value))
                {
                    row.Cells[(int)Columnas.Diciembre].Style.BackColor = Color.FromArgb(60, 179, 113);
                    row.Cells[(int)Columnas.Diciembre].Style.ForeColor = Color.White;
                }
                else
                {
                    row.Cells[(int)Columnas.Diciembre].Style.BackColor = Color.White;
                    row.Cells[(int)Columnas.Diciembre].Style.ForeColor = Color.Black;
                }
            }
        }

        private void dgvCPRResultado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int fila = e.RowIndex;
                if (fila == (dgvCPRResultado.RowCount - 1))
                    return;
                if (fila == -1)
                    return;
                int column = e.ColumnIndex;
                int mesUpdate = -1;
                if (column == (int)Columnas.Check1)
                    mesUpdate = 1;
                else if (column == (int)Columnas.Check2)
                    mesUpdate = 2;
                else if (column == (int)Columnas.Check3)
                    mesUpdate = 3;
                else if (column == (int)Columnas.Check4)
                    mesUpdate = 4;
                else if (column == (int)Columnas.Check5)
                    mesUpdate = 5;
                else if (column == (int)Columnas.Check6)
                    mesUpdate = 6;
                else if (column == (int)Columnas.Check7)
                    mesUpdate = 7;
                else if (column == (int)Columnas.Check8)
                    mesUpdate = 8;
                else if (column == (int)Columnas.Check9)
                    mesUpdate = 9;
                else if (column == (int)Columnas.Check10)
                    mesUpdate = 10;
                else if (column == (int)Columnas.Check11)
                    mesUpdate = 11;
                else if (column == (int)Columnas.Check12)
                    mesUpdate = 12;
                else
                    return;
                string periodicidad = dgvCPRResultado.Rows[fila].Cells[(int)Columnas.Periodicidad].Value.ToString().Trim();
                string provedor = dgvCPRResultado.Rows[fila].Cells[(int)Columnas.Provedor].Value.ToString().Trim();
                Int32 anio = Int32.Parse(dgvCPRResultado.Rows[fila].Cells[(int)Columnas.Anio].Value.ToString().Trim());
                DataGridViewCheckBoxCell chkCell = dgvCPRResultado.Rows[fila].Cells[column] as DataGridViewCheckBoxCell;
                bool cambio = Convert.ToBoolean(chkCell.Value);
                //bool cambio = Convert.ToBoolean(dgvCPRResultado.Rows[fila].Cells[column].Value);
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_tbl_Creditos_Provedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.UpdateLiberado);
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@provedor", provedor);
                    cmd.Parameters.AddWithValue("@liberacion", string.Empty);
                    cmd.Parameters.AddWithValue("@periodicidad", periodicidad);
                    cmd.Parameters.AddWithValue("@mes", mesUpdate);
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@importe", 0);
                    cmd.Parameters.AddWithValue("@comentario", string.Empty);
                    cmd.Parameters.AddWithValue("@liberado", !cambio);
                    cmd.Parameters.AddWithValue("@tipoSelect", 0);
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();
                    cmd.ExecuteNonQuery();
                    //dgvCPRResultado.Rows[fila].Cells[column].Value = !cambio;
                    cargarDatosReporte();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCPRResultado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int fila = e.RowIndex;
                if (fila == (dgvCPRResultado.RowCount - 1))
                    return;
                if (fila == -1)
                    return;
                int column = e.ColumnIndex;
                int mesUpdate = -1;
                if (column == (int)Columnas.Check1)
                    mesUpdate = 1;
                else if (column == (int)Columnas.Check2)
                    mesUpdate = 2;
                else if (column == (int)Columnas.Check3)
                    mesUpdate = 3;
                else if (column == (int)Columnas.Check4)
                    mesUpdate = 4;
                else if (column == (int)Columnas.Check5)
                    mesUpdate = 5;
                else if (column == (int)Columnas.Check6)
                    mesUpdate = 6;
                else if (column == (int)Columnas.Check7)
                    mesUpdate = 7;
                else if (column == (int)Columnas.Check8)
                    mesUpdate = 8;
                else if (column == (int)Columnas.Check9)
                    mesUpdate = 9;
                else if (column == (int)Columnas.Check10)
                    mesUpdate = 10;
                else if (column == (int)Columnas.Check11)
                    mesUpdate = 11;
                else if (column == (int)Columnas.Check12)
                    mesUpdate = 12;
                else
                    return;
                string periodicidad = dgvCPRResultado.Rows[fila].Cells[(int)Columnas.Periodicidad].Value.ToString().Trim();
                string provedor = dgvCPRResultado.Rows[fila].Cells[(int)Columnas.Provedor].Value.ToString().Trim();
                Int32 anio = Int32.Parse(dgvCPRResultado.Rows[fila].Cells[(int)Columnas.Anio].Value.ToString().Trim());
                DataGridViewCheckBoxCell chkCell = dgvCPRResultado.Rows[fila].Cells[column] as DataGridViewCheckBoxCell;
                bool cambio = Convert.ToBoolean(chkCell.Value);
                //bool cambio = Convert.ToBoolean(dgvCPRResultado.Rows[fila].Cells[column].Value);
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_tbl_Creditos_Provedor", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.UpdateLiberado);
                    cmd.Parameters.AddWithValue("@id", 0);
                    cmd.Parameters.AddWithValue("@provedor", provedor);
                    cmd.Parameters.AddWithValue("@liberacion", string.Empty);
                    cmd.Parameters.AddWithValue("@periodicidad", periodicidad);
                    cmd.Parameters.AddWithValue("@mes", mesUpdate);
                    cmd.Parameters.AddWithValue("@anio", anio);
                    cmd.Parameters.AddWithValue("@importe", 0);
                    cmd.Parameters.AddWithValue("@comentario", string.Empty);
                    cmd.Parameters.AddWithValue("@liberado", !cambio);
                    cmd.Parameters.AddWithValue("@tipoSelect", 0);
                    if (conexion.State != ConnectionState.Open)
                        conexion.Open();
                    cmd.ExecuteNonQuery();
                    dgvCPRResultado.Rows[fila].Cells[column].Value = !cambio;
                    cargarDatosReporte();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
