using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Ventas.KPIS
{
    public partial class frmScoreRegionales : Form
    {
        Clases.Logs log;
        System.Globalization.DateTimeFormatInfo dtfInfo = new System.Globalization.CultureInfo("es-ES", false).DateTimeFormat;
        private string usuario = string.Empty;
        private SqlConnection conexion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);

        private DataTable dtUsuarios = new DataTable();

        public frmScoreRegionales()
        {
            InitializeComponent();
            this.usuario = ClasesSGUV.Login.Edit;
        }

        public enum ColumnasIndicadores
        {
            Num,
            Descripcion,
            Couta,
            Venta,
            AcumuladoCouta,
            Pronostico,
            Formato,
            Meta,
            Target,
            Real,
            Bono
        }

        private void formatoGrid() {
            try
            {
                dgvDatos.Columns[(int)ColumnasIndicadores.Num].Visible = false;
                dgvDatos.Columns[(int)ColumnasIndicadores.Formato].Visible = false;
                dgvDatos.Columns[(int)ColumnasIndicadores.Descripcion].HeaderText = "Indicador";
                dgvDatos.Columns[(int)ColumnasIndicadores.Couta].HeaderText = "Cuota";
                //dgvDatos.Columns[(int)ColumnasIndicadores.Venta].HeaderText = "Venta";
                dgvDatos.Columns[(int)ColumnasIndicadores.AcumuladoCouta].HeaderText = "% Vs Cuota";
                dgvDatos.Columns[(int)ColumnasIndicadores.Pronostico].HeaderText = "Tendencia";
                //
                dgvDatos.Columns[(int)ColumnasIndicadores.Descripcion].Width = 150;
                dgvDatos.Columns[(int)ColumnasIndicadores.Couta].Width = 100;
                dgvDatos.Columns[(int)ColumnasIndicadores.Venta].Width = 100;
                dgvDatos.Columns[(int)ColumnasIndicadores.AcumuladoCouta].Width = 100;
                dgvDatos.Columns[(int)ColumnasIndicadores.Pronostico].Width = 100;
                dgvDatos.Columns[(int)ColumnasIndicadores.Meta].Width = 100;
                dgvDatos.Columns[(int)ColumnasIndicadores.Target].Width = 100;
                dgvDatos.Columns[(int)ColumnasIndicadores.Real].Width = 100;
                dgvDatos.Columns[(int)ColumnasIndicadores.Bono].Width = 100;

                //
                dgvDatos.Columns[(int)ColumnasIndicadores.Couta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)ColumnasIndicadores.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)ColumnasIndicadores.AcumuladoCouta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)ColumnasIndicadores.Pronostico].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)ColumnasIndicadores.Meta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)ColumnasIndicadores.Target].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)ColumnasIndicadores.Real].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)ColumnasIndicadores.Bono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                foreach (DataGridViewColumn col in dgvDatos.Columns)
                {
                    col.ReadOnly = true;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dgvDatos.RowHeadersVisible = false;
                dgvDatos.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(1, 3, 1, 3);
                //dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 73, 125);
                //dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                //dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                dgvDatos.Columns[(int)ColumnasIndicadores.Descripcion].DefaultCellStyle.BackColor = dgvDatos.ColumnHeadersDefaultCellStyle.BackColor;
                dgvDatos.Columns[(int)ColumnasIndicadores.Descripcion].DefaultCellStyle.ForeColor = dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor;
            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado: \r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formatoGridGeneral() {
            try
            {
                dgvDDatos.Columns[(int)ColumnasIndicadores.Num].Visible = false;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Formato].Visible = false;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Descripcion].HeaderText = "Indicador";
                dgvDDatos.Columns[(int)ColumnasIndicadores.Couta].HeaderText = "Cuota";
                //dgvDDatos.Columns[(int)ColumnasIndicadores.Venta].HeaderText = "Venta";
                dgvDDatos.Columns[(int)ColumnasIndicadores.AcumuladoCouta].HeaderText = "% Vs Cuota";
                dgvDDatos.Columns[(int)ColumnasIndicadores.Pronostico].HeaderText = "Tendencia";
                //
                dgvDDatos.Columns[(int)ColumnasIndicadores.Descripcion].Width = 150;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Couta].Width = 100;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Venta].Width = 100;
                dgvDDatos.Columns[(int)ColumnasIndicadores.AcumuladoCouta].Width = 100;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Pronostico].Width = 100;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Meta].Width = 100;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Target].Width = 100;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Real].Width = 100;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Bono].Width = 100;

                //
                dgvDDatos.Columns[(int)ColumnasIndicadores.Couta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDDatos.Columns[(int)ColumnasIndicadores.AcumuladoCouta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Pronostico].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Meta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Target].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Real].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Bono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                foreach (DataGridViewColumn col in dgvDDatos.Columns)
                {
                    col.ReadOnly = true;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dgvDDatos.RowHeadersVisible = false;
                dgvDDatos.ColumnHeadersVisible = false;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Descripcion].DefaultCellStyle.BackColor = dgvDDatos.ColumnHeadersDefaultCellStyle.BackColor;
                dgvDDatos.Columns[(int)ColumnasIndicadores.Descripcion].DefaultCellStyle.ForeColor = dgvDDatos.ColumnHeadersDefaultCellStyle.ForeColor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: \r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarUsuarios() {
            dtUsuarios.Columns.Add("codigo", typeof(string));
            dtUsuarios.Columns.Add("nombre", typeof(string));
            DataRow dr = dtUsuarios.NewRow();
            dr["codigo"] = "LNavarro";
            dr["nombre"] = "Liliana Navarro Kai";
            dtUsuarios.Rows.Add(dr);
            DataRow drr = dtUsuarios.NewRow();
            drr["codigo"] = "ISABEL_ARREOLA";
            drr["nombre"] = "Isabel Arreola Sanchez";
            dtUsuarios.Rows.Add(drr);

            DataRow drr1 = dtUsuarios.NewRow();
            drr1["codigo"] = "MariCarmen";
            drr1["nombre"] = "Maricarmen";
            dtUsuarios.Rows.Add(drr1);

            cmbUsuario.DisplayMember = "nombre";
            cmbUsuario.ValueMember = "codigo";
            cmbUsuario.DataSource = dtUsuarios;
        }

        private void frmScoreRegionales_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            cargarUsuarios();
            if (this.usuario == "N")
            {
                cmbUsuario.SelectedValue = this.usuario;
                cmbUsuario.Enabled = false;
            }

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
        }

        private DataTable obtenerDatos(){
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_scoreRegionales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", 1);
                cmd.Parameters.AddWithValue("@usuario", cmbUsuario.SelectedValue);
                cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value.Date);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                if (!reader.IsClosed)
                    reader.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error inesperado: \r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: \r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conexion.Close();
            }
            return dt;
        }

        private DataTable obtenerDatosGenerales() {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_scoreRegionales", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", 2);
                cmd.Parameters.AddWithValue("@usuario", cmbUsuario.SelectedValue);
                cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value.Date);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                if (!reader.IsClosed)
                    reader.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error inesperado: \r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: \r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string mes =  System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtfInfo.GetMonthName(dtpFecha.Value.Date.Month));
            this.Text = "Score Regionales "+mes;
            btnConsultar.Enabled = false;
            if (cmbUsuario.SelectedValue == null)
                return;
            dgvDatos.DataSource = obtenerDatos();
            formatoGrid();
            dgvDDatos.DataSource = obtenerDatosGenerales();
            formatoGridGeneral();
            btnConsultar.Enabled = true;
            Cursor = Cursors.Default;


            decimal real1 = Convert.ToDecimal((dgvDatos.DataSource as DataTable).Compute("SUM(Real)", string.Empty));
            decimal bono1 = (real1 * (decimal)0.6) / 1;
            decimal bono2 = Convert.ToDecimal((dgvDDatos.DataSource as DataTable).Compute("SUM(Bono)", string.Empty));

            txtReal1.Text = real1.ToString("P1");
            txtBono1.Text = bono1.ToString("P1");
            txtBono2.Text = bono2.ToString("P1");

            textBox1.Text = (bono1 + bono2).ToString("P1");

        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvDatos.Rows) {
                string format = row.Cells[(int)ColumnasIndicadores.Formato].Value.ToString();
                row.Cells[(int)ColumnasIndicadores.Couta].Style.Format = format;
                row.Cells[(int)ColumnasIndicadores.Venta].Style.Format = format;
                //
                row.Cells[(int)ColumnasIndicadores.AcumuladoCouta].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Pronostico].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Meta].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Target].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Real].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Bono].Style.Format = "P1";
                if (row.Cells[(int)ColumnasIndicadores.Pronostico].Value != DBNull.Value)
                {
                    string val = row.Cells[(int)ColumnasIndicadores.Pronostico].Value.ToString();
                    decimal dec = 0;
                    if (decimal.TryParse(val, out dec))
                    {
                        if (dec >= 1)
                        {
                            row.Cells[(int)ColumnasIndicadores.Pronostico].Style.BackColor = Color.Green;
                        }
                        else if (dec >= (decimal)0.85)
                        {
                            row.Cells[(int)ColumnasIndicadores.Pronostico].Style.BackColor = Color.FromArgb(255, 255, 0);
                        }
                        else if (dec < (decimal)0.85)
                        {
                            row.Cells[(int)ColumnasIndicadores.Pronostico].Style.BackColor = Color.Red;
                            row.Cells[(int)ColumnasIndicadores.Pronostico].Style.ForeColor = Color.White;
                        }
                    }
                }
            }
        }

        private void dgvDDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvDDatos.Rows)
            {
                string format = row.Cells[(int)ColumnasIndicadores.Formato].Value.ToString();
                row.Cells[(int)ColumnasIndicadores.Couta].Style.Format = format;
                row.Cells[(int)ColumnasIndicadores.Venta].Style.Format = format;
                //
                row.Cells[(int)ColumnasIndicadores.AcumuladoCouta].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Pronostico].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Meta].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Target].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Real].Style.Format = "P1";
                row.Cells[(int)ColumnasIndicadores.Bono].Style.Format = "P1";
                if (row.Cells[(int)ColumnasIndicadores.Pronostico].Value != DBNull.Value)
                {
                    string val = row.Cells[(int)ColumnasIndicadores.Pronostico].Value.ToString();
                    decimal dec = 0;
                    if (decimal.TryParse(val, out dec))
                    {
                        if (dec >= 1)
                        {
                            row.Cells[(int)ColumnasIndicadores.Pronostico].Style.BackColor = Color.Green;
                        }
                        else if (dec >= (decimal)0.85)
                        {
                            row.Cells[(int)ColumnasIndicadores.Pronostico].Style.BackColor = Color.FromArgb(255, 255, 0);
                        }
                        else if (dec < (decimal)0.85)
                        {
                            row.Cells[(int)ColumnasIndicadores.Pronostico].Style.BackColor = Color.Red;
                            row.Cells[(int)ColumnasIndicadores.Pronostico].Style.ForeColor = Color.White;
                        }
                    }
                }
            }
        }

        private void frmScoreRegionales_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void frmScoreRegionales_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }
    }
}
