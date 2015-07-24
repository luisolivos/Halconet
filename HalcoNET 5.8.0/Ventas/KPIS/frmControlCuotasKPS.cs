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
    public partial class frmControlCuotasKPS : Form
    {
        private SqlConnection conexion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        private DataTable dtSucursales = new DataTable();

        public frmControlCuotasKPS()
        {
            InitializeComponent();
        }

        private enum Columnas { 
            Code,
            Sucursal,
            Tipo,
            Nombre,
            Cuota, 
            Cliente,
            Mes,
            Anio
        }

        private void loadSucursales(){
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT A.GroupCode, A.GroupName FROM [SBO-DistPJ].dbo.OCRG A WHERE GroupCode IN (107, 105, 106, 121, 100, 102, 108, 104, 103);", conexion);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                if(conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dtSucursales.Load(reader);
            }
            catch (SqlException sqlex) {
                MessageBox.Show("Error inesperado:\r\n" + sqlex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formatoGrid() {
            try {
                dgvDatos.Columns[(int)Columnas.Mes].HeaderText = "Mes";
                dgvDatos.Columns[(int)Columnas.Anio].HeaderText = "Año";
                //
                dgvDatos.Columns[(int)Columnas.Code].Visible = false;
                dgvDatos.Columns[(int)Columnas.Sucursal].Width = 80;
                dgvDatos.Columns[(int)Columnas.Tipo].Width = 120;
                dgvDatos.Columns[(int)Columnas.Nombre].Width = 80;
                dgvDatos.Columns[(int)Columnas.Cuota].Width = 80;
                dgvDatos.Columns[(int)Columnas.Cliente].Width = 80;
                dgvDatos.Columns[(int)Columnas.Mes].Width = 80;
                dgvDatos.Columns[(int)Columnas.Anio].Width = 80;
                dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(112, 128, 144);
                dgvDatos.ColumnHeadersDefaultCellStyle.Padding = new Padding(5, 5, 5, 5);
                dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold, GraphicsUnit.Pixel);
                dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(224, 255, 255);
            
            }
            catch(Exception ex){
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ControlCuotasKPS_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            cargarMeses();
            txtAnio.Text = DateTime.Now.Date.Year.ToString();
            cmbTipo.DisplayMember = "Tipo";
            cmbTipo.ValueMember = "Tipo";
            cmbTipo.DataSource = obtenerTipo();
            cmbMes.SelectedValue = DateTime.Now.Date.Month;
            loadSucursales();
        }

        private void cargarMeses() {
            try
            {
                System.Globalization.DateTimeFormatInfo dtfInfo = new System.Globalization.CultureInfo("es-ES", false).DateTimeFormat;
                DataTable dtMeses = new DataTable();
                dtMeses.Columns.Add("noMes", typeof(Int32));
                dtMeses.Columns.Add("nomMes", typeof(string));
                for (int i = 1; i <= 12; i++)
                {
                    DataRow dr = dtMeses.NewRow();
                    dr["noMes"] = i;
                    dr["nomMes"] = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtfInfo.GetMonthName(i));
                    dtMeses.Rows.Add(dr);
                }
                cmbMes.DisplayMember = "nomMes";
                cmbMes.ValueMember = "noMes";
                cmbMes.DataSource = dtMeses;
            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado:\r\n"+ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable obtenerTipo() {
            DataTable dt = new DataTable();
            try
            {
                string sq = "SELECT DISTINCT A.Tipo FROM tbl_OKPS A";
                SqlCommand cmd = new SqlCommand(sq, conexion);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 0;
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                if (!reader.IsClosed)
                    reader.Close();
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

        private void getDatos(string selectCommand) {
            try
            {
                dataAdapter = new SqlDataAdapter(selectCommand, conexion);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(dt);
                bindingSource1.DataSource = dt;
            }
            catch (SqlException sqlex) {
                MessageBox.Show("Error inesperado:\r\n" + sqlex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnconsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string selectCommand = string.Empty;
                string tipo = cmbTipo.SelectedValue.ToString();
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = bindingSource1;
                if (tipo == "Consignaciones" || tipo == "ProgramaHalcon" || tipo == "Subdistribuciones")
                {
                    selectCommand = "DECLARE @NUM int = (SELECT COUNT(*) FROM tbl_OKPS tbo WHERE Tipo = '" + tipo + "')";

                    selectCommand += "IF @NUM = 0 INSERT INTO tbl_OKPS SELECT DISTINCT tbo.Sucursal, tbo.Tipo, NULL, 0, tbo.Cliente, NULL, NULL, NULL FROM tbl_OKPS tbo WHERE Tipo = '" + tipo + "'";

                    selectCommand += "SELECT tbo.Code, tbo.Sucursal, tbo.Tipo, tbo.Nombre, tbo.Couta, tbo.Cliente, tbo.U_Mes, tbo.U_Year, tbo.Agrupador FROM tbl_OKPS tbo WHERE Tipo = '" + tipo + "'";
                }
                else
                {
                    if (txtAnio.Text.Trim() == string.Empty) {
                        dgvDatos.DataSource = null;
                        txtAnio.Focus();
                        return;
                    }
                    Int16 a = 0;
                    if (!Int16.TryParse(txtAnio.Text.Trim(), out a))
                    {
                        dgvDatos.DataSource = null;
                        txtAnio.Focus();
                        txtAnio.Select(0, txtAnio.Text.Length);
                        return;
                    }


                    selectCommand = "DECLARE @NUM int = (SELECT COUNT(*) FROM tbl_OKPS tbo WHERE U_Mes = " + cmbMes.SelectedValue + " AND U_Year = " + txtAnio.Text + " AND Tipo = '" + tipo + "')";

                    selectCommand += "IF @NUM = 0 INSERT INTO tbl_OKPS SELECT DISTINCT tbo.Sucursal, tbo.Tipo, tbo.Nombre, 0, null, "+cmbMes.SelectedValue+", "+txtAnio.Text+", NULL FROM tbl_OKPS tbo WHERE Tipo = '" + tipo + "'";

                    selectCommand += "SELECT tbo.Code, tbo.Sucursal, tbo.Tipo, tbo.Nombre, tbo.Couta, tbo.Cliente, tbo.U_Mes, tbo.U_Year, tbo.Agrupador FROM tbl_OKPS tbo WHERE U_Mes = " + cmbMes.SelectedValue + " AND U_Year = " + txtAnio.Text + " AND Tipo = '" + tipo + "'";
                }                
                getDatos(selectCommand);
                this.formatoGrid();
            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            string tipo = cmbTipo.SelectedValue.ToString();
            if (tipo == "Consignaciones" || tipo == "ProgramaHalcon" || tipo == "Subdistribuciones")
            {
                e.Row.Cells[(int)Columnas.Tipo].Value = cmbTipo.SelectedValue.ToString();
                return;
            }

            e.Row.Cells[(int)Columnas.Tipo].Value = cmbTipo.SelectedValue.ToString();
            e.Row.Cells[(int)Columnas.Mes].Value = cmbMes.SelectedValue.ToString();
            e.Row.Cells[(int)Columnas.Anio].Value = txtAnio.Text.Trim();
        }

        private void dgvDatos_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            int fila = e.RowIndex;
            int col = e.ColumnIndex;
            if (fila == -1) return;
            if (col == -1) return;

            if (col == (int)Columnas.Sucursal) {
                if (dgvDatos.Rows[fila].Cells[col].Value == DBNull.Value)
                    return;
                if (dgvDatos.Rows[fila].Cells[col].Value == null)
                    return;
                if (dgvDatos.Rows[fila].Cells[col].Value.ToString() == string.Empty)
                    return;
                string cod = dgvDatos.Rows[fila].Cells[col].Value.ToString();
                string val = dtSucursales.AsEnumerable().Where(w => w.Field<Int16>("GroupCode") == Int16.Parse(cod)).First().Field<string>("GroupName");
                dgvDatos.Rows[fila].Cells[col].ToolTipText = val;
            }


        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dataAdapter.Update((DataTable)bindingSource1.DataSource);
            getDatos(dataAdapter.SelectCommand.CommandText);
        }


    }
}
