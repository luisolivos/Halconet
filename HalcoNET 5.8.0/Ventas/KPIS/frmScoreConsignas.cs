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
    public partial class frmScoreConsignas : Form
    {
        Clases.Logs log;
        private static string strConexion = ClasesSGUV.Propiedades.conectionSGUV;
        private SqlConnection conexion = new SqlConnection(strConexion);
        
        public frmScoreConsignas()
        {
            InitializeComponent();
        }

        private enum Columnas{
            Sucursal,
            Vendedor,
            Cliente, 
            NoCliente,
            MonConsignacion,
            Cuota, 
            VtaAcumConsig,
            VtaAcumTotal,
            AcumConsigVsTotal,
            AcumTotVsCuotaTot,
            TendenciaConsigna,
            TendenciaTotal
        }

        private void formatoGrid() {
            try
            {
                dgvDatos.Columns[(int)Columnas.Sucursal].Width = 100;
                dgvDatos.Columns[(int)Columnas.Vendedor].Width = 130;
                dgvDatos.Columns[(int)Columnas.Cliente].Width = 140;
                dgvDatos.Columns[(int)Columnas.NoCliente].Width = 100;
                dgvDatos.Columns[(int)Columnas.MonConsignacion].Width = 70;
                dgvDatos.Columns[(int)Columnas.Cuota].Width = 70;
                dgvDatos.Columns[(int)Columnas.VtaAcumConsig].Width = 90;
                dgvDatos.Columns[(int)Columnas.VtaAcumTotal].Width = 90;
                dgvDatos.Columns[(int)Columnas.AcumConsigVsTotal].Width = 90;
                dgvDatos.Columns[(int)Columnas.AcumTotVsCuotaTot].Width = 90;
                dgvDatos.Columns[(int)Columnas.TendenciaConsigna].Width = 90;
                dgvDatos.Columns[(int)Columnas.TendenciaTotal].Width = 90;
                //
                dgvDatos.Columns[(int)Columnas.MonConsignacion].DefaultCellStyle.Format = "C2";
                dgvDatos.Columns[(int)Columnas.Cuota].DefaultCellStyle.Format = "C2";
                dgvDatos.Columns[(int)Columnas.VtaAcumConsig].DefaultCellStyle.Format = "C2";
                dgvDatos.Columns[(int)Columnas.VtaAcumTotal].DefaultCellStyle.Format = "C2";
                dgvDatos.Columns[(int)Columnas.AcumConsigVsTotal].DefaultCellStyle.Format = "P2";
                dgvDatos.Columns[(int)Columnas.AcumTotVsCuotaTot].DefaultCellStyle.Format = "P2";
                dgvDatos.Columns[(int)Columnas.TendenciaConsigna].DefaultCellStyle.Format = "P2";
                dgvDatos.Columns[(int)Columnas.TendenciaTotal].DefaultCellStyle.Format = "P2";
                //
                dgvDatos.Columns[(int)Columnas.MonConsignacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)Columnas.Cuota].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)Columnas.VtaAcumConsig].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)Columnas.VtaAcumTotal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)Columnas.AcumConsigVsTotal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)Columnas.AcumTotVsCuotaTot].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)Columnas.TendenciaConsigna].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvDatos.Columns[(int)Columnas.TendenciaTotal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //
                foreach (DataGridViewColumn col in dgvDatos.Columns)
                    col.ReadOnly = true;
                dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 73, 125);
                dgvDatos.ColumnHeadersDefaultCellStyle.Padding = new Padding(1, 1, 1, 1);
                dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable consultarDatos() {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_KPIS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@TipoConsulta", 7);
                cmd.Parameters.AddWithValue("@Fecha", dtpFecha.Value.Date);
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnConsultar.Enabled = false;
                DataTable dt = consultarDatos();
                dgvDatos.DataSource = dt;
                formatoGrid();
            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnConsultar.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvDatos.Rows) {
                string val1 = row.Cells[(int)Columnas.AcumConsigVsTotal].Value.ToString();
                string val2 = row.Cells[(int)Columnas.AcumTotVsCuotaTot].Value.ToString();
                string val3 = row.Cells[(int)Columnas.TendenciaConsigna].Value.ToString();
                string val4 = row.Cells[(int)Columnas.TendenciaTotal].Value.ToString();
                decimal tend1 = 0;
                decimal tend2 = 0;
                decimal tend3 = 0;
                decimal tend4 = 0;
                //
                if (decimal.TryParse(val1, out tend1))
                {
                    if (tend1 > 1)
                    {
                        row.Cells[(int)Columnas.AcumConsigVsTotal].Style.BackColor = Color.FromArgb(0, 176, 80);
                    }
                    else if (tend1 > (decimal)0.85)
                    {
                        row.Cells[(int)Columnas.AcumConsigVsTotal].Style.BackColor = Color.FromArgb(255, 255, 0);
                    }
                    else
                    {
                        row.Cells[(int)Columnas.AcumConsigVsTotal].Style.BackColor = Color.Red;
                    }
                }
                //
                if (decimal.TryParse(val2, out tend2))
                {
                    if (tend2 > 1)
                    {
                        row.Cells[(int)Columnas.AcumTotVsCuotaTot].Style.BackColor = Color.FromArgb(0, 176, 80);
                    }
                    else if (tend2 > (decimal)0.85)
                    {
                        row.Cells[(int)Columnas.AcumTotVsCuotaTot].Style.BackColor = Color.FromArgb(255, 255, 0);
                    }
                    else
                    {
                        row.Cells[(int)Columnas.AcumTotVsCuotaTot].Style.BackColor = Color.FromArgb(255, 0, 0);
                    }
                }
                //
                if (decimal.TryParse(val3, out tend3))
                {
                    if (tend3 > 1)
                    {
                        row.Cells[(int)Columnas.TendenciaConsigna].Style.BackColor = Color.FromArgb(0, 176, 80);
                    }
                    else if (tend3 > (decimal)0.85)
                    {
                        row.Cells[(int)Columnas.TendenciaConsigna].Style.BackColor = Color.FromArgb(255, 255, 0);
                    }
                    else
                    {
                        row.Cells[(int)Columnas.TendenciaConsigna].Style.BackColor = Color.FromArgb(255, 0, 0);
                    }
                }
                //
                if (decimal.TryParse(val4, out tend4))
                {
                    if (tend4 > 1)
                    {
                        row.Cells[(int)Columnas.TendenciaTotal].Style.BackColor = Color.FromArgb(0, 176, 80);
                    }
                    else if (tend4 > (decimal)0.85)
                    {
                        row.Cells[(int)Columnas.TendenciaTotal].Style.BackColor = Color.FromArgb(255, 255, 0);
                    }
                    else
                    {
                        row.Cells[(int)Columnas.TendenciaTotal].Style.BackColor = Color.FromArgb(255, 0, 0);
                    }
                }
            }
        }

        private void frmScoreConsignas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void frmScoreConsignas_Shown(object sender, EventArgs e)
        {
try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void frmScoreConsignas_Load(object sender, EventArgs e)
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


    }
}
