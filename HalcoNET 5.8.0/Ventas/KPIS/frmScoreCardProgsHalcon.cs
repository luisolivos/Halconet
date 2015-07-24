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
    public partial class frmScoreCardProgsHalcon : Form
    {
        Clases.Logs log;
       
        private SqlConnection conexion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        public frmScoreCardProgsHalcon()
        {
            InitializeComponent();
        }

        private enum Columnas { 
            Sucursal,
			Vendedor,
			Cliente,
			NoCte,
			Canal, 
			Nivel,
			Cuota,
			King,
			PowerPro,
			Rodwell,
			Cargo,
			TotalLH,
			AcumVsCuota,
			Tendencia
        }

        private enum ColumnasTotales { 
            Descripcion,
            King,
            PowerPro,
            Rodwell,
            Cargo,
            TotalLH
        }

        private void formatoGrid() {
            try
            {
                dgvResultado.Columns[(int)Columnas.Sucursal].Width = 160;
                dgvResultado.Columns[(int)Columnas.Vendedor].Width = 160;
                dgvResultado.Columns[(int)Columnas.Cliente].Width = 200;
                dgvResultado.Columns[(int)Columnas.NoCte].Width = 50;
                dgvResultado.Columns[(int)Columnas.Canal].Width = 80;
                dgvResultado.Columns[(int)Columnas.Nivel].Width = 50;
                dgvResultado.Columns[(int)Columnas.Cuota].Width = 70;
                dgvResultado.Columns[(int)Columnas.King].Width = 70;
                dgvResultado.Columns[(int)Columnas.PowerPro].Width = 70;
                dgvResultado.Columns[(int)Columnas.Rodwell].Width = 70;
                dgvResultado.Columns[(int)Columnas.Cargo].Width = 70;
                dgvResultado.Columns[(int)Columnas.TotalLH].Width = 70;
                dgvResultado.Columns[(int)Columnas.AcumVsCuota].Width = 80;
                dgvResultado.Columns[(int)Columnas.Tendencia].Width = 80;
                //
                dgvResultado.Columns[(int)Columnas.Cuota].DefaultCellStyle.Format = "C0";
                dgvResultado.Columns[(int)Columnas.King].DefaultCellStyle.Format = "C0";
                dgvResultado.Columns[(int)Columnas.PowerPro].DefaultCellStyle.Format = "C0";
                dgvResultado.Columns[(int)Columnas.Rodwell].DefaultCellStyle.Format = "C0";
                dgvResultado.Columns[(int)Columnas.Cargo].DefaultCellStyle.Format = "C0";
                dgvResultado.Columns[(int)Columnas.TotalLH].DefaultCellStyle.Format = "C0";
                dgvResultado.Columns[(int)Columnas.Cuota].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns[(int)Columnas.King].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns[(int)Columnas.PowerPro].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns[(int)Columnas.Rodwell].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns[(int)Columnas.Cargo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvResultado.Columns[(int)Columnas.TotalLH].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvResultado.Columns[(int)Columnas.AcumVsCuota].DefaultCellStyle.Format = "P0";
                dgvResultado.Columns[(int)Columnas.Tendencia].DefaultCellStyle.Format = "P0";
                dgvResultado.Columns[(int)Columnas.AcumVsCuota].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvResultado.Columns[(int)Columnas.Tendencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                foreach (DataGridViewColumn col in dgvResultado.Columns)
                    col.ReadOnly = true;
                dgvResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 73, 125);
                dgvResultado.ColumnHeadersDefaultCellStyle.Padding = new Padding(1, 2, 1, 2);
                dgvResultado.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                dgvResultado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formatoGridTotales() {
            try
            {
                dgvTotales.Columns[(int)ColumnasTotales.Descripcion].HeaderText = "Descripción";
                dgvTotales.Columns[(int)ColumnasTotales.Descripcion].Width = 90;
                dgvTotales.Columns[(int)ColumnasTotales.King].Width = 70;
                dgvTotales.Columns[(int)ColumnasTotales.PowerPro].Width = 90;
                dgvTotales.Columns[(int)ColumnasTotales.Rodwell].Width = 70;
                dgvTotales.Columns[(int)ColumnasTotales.Cargo].Width = 70;
                dgvTotales.Columns[(int)ColumnasTotales.TotalLH].Width = 85;
                //
                foreach (DataGridViewColumn col in dgvTotales.Columns)
                    col.ReadOnly = true;
                dgvTotales.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 73, 125);
                dgvTotales.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 7, FontStyle.Regular);
                dgvTotales.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvTotales.ColumnHeadersHeight = 15;
                dgvTotales.RowHeadersVisible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnConsultar.Enabled = false;
                DataTable dt = ConsultarDatos();
                dgvResultado.DataSource = dt;
                formatoGrid();
                DataTable dtTotales = getTotales();
                DataRow dr = dtTotales.NewRow();
                dr[(int)ColumnasTotales.Descripcion] = "Subtotales: ";
                dr[(int)ColumnasTotales.King] = dt.Compute("SUM(King)", string.Empty);
                dr[(int)ColumnasTotales.PowerPro] = dt.Compute("SUM([Power Pro])", string.Empty); ;
                dr[(int)ColumnasTotales.Rodwell] = dt.Compute("SUM(Rodwell)", string.Empty); ;
                dr[(int)ColumnasTotales.Cargo] = dt.Compute("SUM(Cargo)", string.Empty);
                dr[(int)ColumnasTotales.TotalLH] = dt.Compute("SUM([Total LH])", string.Empty);
                dtTotales.Rows.InsertAt(dr, 0);
                decimal king = (dtTotales.Rows[0].Field<decimal>("King") / dtTotales.Rows[1].Field<decimal>("King"));
                decimal power = (dtTotales.Rows[0].Field<decimal>("Power Pro") / dtTotales.Rows[1].Field<decimal>("Power Pro"));
                decimal rodwel = (dtTotales.Rows[0].Field<decimal>("Rodwell") / dtTotales.Rows[1].Field<decimal>("Rodwell"));
                decimal cargo = (dtTotales.Rows[0].Field<decimal>("Cargo") / dtTotales.Rows[1].Field<decimal>("Cargo"));
                decimal totalLh = (dtTotales.Rows[0].Field<decimal>("Total LH") / dtTotales.Rows[1].Field<decimal>("Total LH"));
                //
                DataRow drr = dtTotales.NewRow();
                drr[(int)ColumnasTotales.Descripcion] = "% total: ";
                drr[(int)ColumnasTotales.King] = king;
                drr[(int)ColumnasTotales.PowerPro] = power;
                drr[(int)ColumnasTotales.Rodwell] = rodwel;
                drr[(int)ColumnasTotales.Cargo] = cargo;
                drr[(int)ColumnasTotales.TotalLH] = totalLh;
                dtTotales.Rows.Add(drr);
                dgvTotales.DataSource = dtTotales;
                formatoGridTotales();
            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado:\r\n"+ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnConsultar.Enabled = true;
            Cursor = Cursors.Default;
        }

        private DataTable getTotales() {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_KPIS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@TipoConsulta", 6);
                cmd.Parameters.AddWithValue("@Fecha", dtpFeha.Value.Date);
                cmd.Parameters.AddWithValue("@Sucursal", 0);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }
            return dt;
        }

        private DataTable ConsultarDatos() {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_KPIS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@TipoConsulta", 5);
                cmd.Parameters.AddWithValue("@Fecha", dtpFeha.Value.Date);
                cmd.Parameters.AddWithValue("@Sucursal", 0);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conexion.Close();
            }
            return dt;
        }

        private void dgvResultado_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvResultado.Rows) {
                string val = row.Cells[(int)Columnas.Tendencia].Value.ToString();
                decimal tend = 0;
                if(decimal.TryParse(val, out tend)){
                    if (tend > 1) {
                        row.Cells[(int)Columnas.Tendencia].Style.BackColor = Color.FromArgb(0, 176, 80);
                    }
                    else if (tend > (decimal)0.85)
                    {
                        row.Cells[(int)Columnas.Tendencia].Style.BackColor = Color.FromArgb(255, 255, 0);
                    }
                    else {
                        row.Cells[(int)Columnas.Tendencia].Style.BackColor = Color.FromArgb(255, 0, 0);
                    }

                }

            }
        }

        private void dgvTotales_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvTotales.Rows) {
                row.Height = row.Height - 2;
                if (row.Cells[(int)ColumnasTotales.Descripcion].Value.ToString().Trim().Contains("%"))
                {
                    row.Cells[(int)ColumnasTotales.King].Style.Format = "P2";
                    row.Cells[(int)ColumnasTotales.PowerPro].Style.Format = "P2";
                    row.Cells[(int)ColumnasTotales.Rodwell].Style.Format = "P2";
                    row.Cells[(int)ColumnasTotales.Cargo].Style.Format = "P2";
                    row.Cells[(int)ColumnasTotales.TotalLH].Style.Format = "P2";
                }
                else
                {
                    row.Cells[(int)ColumnasTotales.King].Style.Format = "C0";
                    row.Cells[(int)ColumnasTotales.PowerPro].Style.Format = "C0";
                    row.Cells[(int)ColumnasTotales.Rodwell].Style.Format = "C0";
                    row.Cells[(int)ColumnasTotales.Cargo].Style.Format = "C0";
                    row.Cells[(int)ColumnasTotales.TotalLH].Style.Format = "C0";
                }
                //
                row.Cells[(int)ColumnasTotales.Descripcion].Style.Font = new Font("Arial", 7, FontStyle.Bold);
                row.Cells[(int)ColumnasTotales.King].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                row.Cells[(int)ColumnasTotales.PowerPro].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                row.Cells[(int)ColumnasTotales.Rodwell].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                row.Cells[(int)ColumnasTotales.Cargo].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                row.Cells[(int)ColumnasTotales.TotalLH].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void frmScoreCardProgsHalcon_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

            }
            catch (Exception)
            {

            }
        }

        private void frmScoreCardProgsHalcon_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void frmScoreCardProgsHalcon_FormClosing(object sender, FormClosingEventArgs e)
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
