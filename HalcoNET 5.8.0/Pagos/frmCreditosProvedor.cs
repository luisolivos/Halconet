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
    public partial class frmCreditosProvedor : Form
    {
        private string cadenaConexion = ClasesSGUV.Propiedades.conectionSGUV;
        private int idRegistro = 0;


        public frmCreditosProvedor()
        {
            InitializeComponent();
        }

        private enum TipoStatement { 
            Insert = 1,
            Update = 2,
            Select = 3,
            Delete = 4
        }

        private enum Columnas { 
            Id,
            Provedor,
            Liberacion,
            Periodicidad,
            Mes,
            Anio,
            Importe,
            Comentario,
            Liberado
        }

        private void formatoGrid()
        {
            try
            {
                dgvShowDatos.Columns[(int)Columnas.Id].Visible = false;
                dgvShowDatos.Columns[(int)Columnas.Liberado].Visible = false;
                dgvShowDatos.Columns[(int)Columnas.Provedor].HeaderText = "Provedor";
                dgvShowDatos.Columns[(int)Columnas.Liberacion].HeaderText = "Liberación";
                dgvShowDatos.Columns[(int)Columnas.Periodicidad].HeaderText = "Periodicidad";
                dgvShowDatos.Columns[(int)Columnas.Mes].HeaderText = "Mes";
                dgvShowDatos.Columns[(int)Columnas.Anio].HeaderText = "Año";
                dgvShowDatos.Columns[(int)Columnas.Importe].HeaderText = "Importe";
                dgvShowDatos.Columns[(int)Columnas.Comentario].HeaderText = "Comentario";
                dgvShowDatos.Columns[(int)Columnas.Liberado].HeaderText = "Liberado";
                //
                dgvShowDatos.Columns[(int)Columnas.Provedor].Width = 120;
                dgvShowDatos.Columns[(int)Columnas.Liberacion].Width = 100;
                dgvShowDatos.Columns[(int)Columnas.Periodicidad].Width = 120;
                dgvShowDatos.Columns[(int)Columnas.Mes].Width = 100;
                dgvShowDatos.Columns[(int)Columnas.Anio].Width = 100;
                dgvShowDatos.Columns[(int)Columnas.Importe].Width = 120;
                dgvShowDatos.Columns[(int)Columnas.Comentario].Width = 180;
                dgvShowDatos.Columns[(int)Columnas.Liberado].Width = 50;

                dgvShowDatos.Columns[(int)Columnas.Importe].DefaultCellStyle.Format = "C2";
                dgvShowDatos.Columns[(int)Columnas.Importe].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                foreach (DataGridViewColumn col in dgvShowDatos.Columns) {
                    col.ReadOnly = true;
                    col.ContextMenuStrip = null;
                    col.ContextMenuStrip = contextMenuStripAcciones;
                }

                dgvShowDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(128, 128, 128);
                dgvShowDatos.ColumnHeadersDefaultCellStyle.Padding = new Padding(2, 6, 2, 3);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCreditosProvedor_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            cargarProvedores();
            cargarPeriodicidad();
            cargarMeses();
            txtAnio.Text = DateTime.Now.Year.ToString();
            loadDatos();
        }

        private void cargarProvedores() {
            string[] provedores = new string[] { "BENDIX", "CUMMINS", "RIDE CONTROL", "HOLLAND", "ACCURIDE", "VEYANCE", "TIMKEN", "DONALDSON", "AFFINIA", "MOGUL", "STEMCO"};
            cmbProvedor.Items.AddRange(provedores);
        }

        private void cargarPeriodicidad() {
            string[] periodicidad = new string[] { "MENSUAL", "TRIMESTRAL", "SEMESTRAL", "ANUAL"};
            cmbPeriodicidad.Items.AddRange(periodicidad);
        }

        private void cargarMeses() { 
            System.Globalization.DateTimeFormatInfo formatoFecha = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            int selected = 0;
            for(int i=1; i<= 12; i++){
                if (DateTime.Now.Month == i)
                    selected = i;
                cmbMes.Items.Add(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formatoFecha.GetMonthName(i)));
            }
            cmbMes.SelectedIndex = (selected - 1);

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbProvedor.SelectedIndex == -1)
            {
                cmbProvedor.Focus();
                return;
            }
            if (txtLiberacion.Text.Trim() == string.Empty) {
                txtLiberacion.Focus();
                return;
            }
            if (cmbPeriodicidad.SelectedIndex == -1) {
                cmbPeriodicidad.Focus();
                return;
            }
            if (cmbMes.SelectedIndex == -1) {
                cmbMes.Focus();
                return;
            }
            if (txtAnio.Text.Trim() == string.Empty) {
                txtAnio.Focus();
                return;
            }
            decimal importe = 0;
            if (txtImporte.Text.Trim() != string.Empty) { 
                if(!Decimal.TryParse(txtImporte.Text.Trim(), out importe)){
                    txtImporte.Focus();
                    return;
                }
            }
            btnGuardar.Enabled = false;

            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_Creditos_Provedor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                if (idRegistro == 0)
                {
                    cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Insert);
                    cmd.Parameters.AddWithValue("@id", 0);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Update);
                    cmd.Parameters.AddWithValue("@id", idRegistro);
                }                
                cmd.Parameters.AddWithValue("@provedor", cmbProvedor.Text.Trim());
                cmd.Parameters.AddWithValue("@liberacion", txtLiberacion.Text.Trim());
                cmd.Parameters.AddWithValue("@periodicidad", cmbPeriodicidad.Text.Trim());
                cmd.Parameters.AddWithValue("@mes", (cmbMes.SelectedIndex + 1));
                cmd.Parameters.AddWithValue("@anio", txtAnio.Text.Trim());
                cmd.Parameters.AddWithValue("@importe", importe);
                cmd.Parameters.AddWithValue("@comentario", txtComentario.Text.Trim());
                SqlParameter par = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                par.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                if (idRegistro == 0)
                    idRegistro = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                else
                    cmd.ExecuteNonQuery();
                string mensajeSuccess = cmd.Parameters["@MensajeSuccess"].Value.ToString();
                MessageBox.Show(mensajeSuccess, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conexion.Close();
                btnGuardar.Enabled = true;
            }
        }

        private void loadDatos() {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_Creditos_Provedor", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Select);
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@provedor", string.Empty);
                cmd.Parameters.AddWithValue("@liberacion", string.Empty);
                cmd.Parameters.AddWithValue("@periodicidad", string.Empty);
                cmd.Parameters.AddWithValue("@mes", 0);
                cmd.Parameters.AddWithValue("@anio", string.Empty);
                cmd.Parameters.AddWithValue("@importe", decimal.Zero);
                cmd.Parameters.AddWithValue("@comentario", string.Empty);
                cmd.Parameters.AddWithValue("@tipoSelect", 1);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (!reader.IsClosed)
                    reader.Close();
                dgvShowDatos.DataSource = dt;
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

        private void dgvShowDatos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            int fila = e.RowIndex;
            if (fila == -1)
                return;        
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                dgvShowDatos.ClearSelection();
                dgvShowDatos.Rows[fila].Selected = true;                
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int fila = dgvShowDatos.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (fila != -1) {
                    int id = Int32.Parse(dgvShowDatos.Rows[fila].Cells[(int)Columnas.Id].Value.ToString().Trim());
                    string provedor = dgvShowDatos.Rows[fila].Cells[(int)Columnas.Provedor].Value.ToString().Trim();
                    string liberacion = dgvShowDatos.Rows[fila].Cells[(int)Columnas.Liberacion].Value.ToString().Trim();
                    string periodicidad = dgvShowDatos.Rows[fila].Cells[(int)Columnas.Periodicidad].Value.ToString().Trim();
                    int mes = Int32.Parse(dgvShowDatos.Rows[fila].Cells[(int)Columnas.Mes].Value.ToString().Trim());
                    int anio = Int32.Parse(dgvShowDatos.Rows[fila].Cells[(int)Columnas.Anio].Value.ToString().Trim());
                    string importe = dgvShowDatos.Rows[fila].Cells[(int)Columnas.Importe].Value.ToString().Trim();
                    string comentario = dgvShowDatos.Rows[fila].Cells[(int)Columnas.Comentario].Value.ToString().Trim();
                    cmbProvedor.Text = provedor;
                    txtLiberacion.Text = liberacion;
                    cmbPeriodicidad.Text = periodicidad;
                    cmbMes.SelectedIndex = (mes - 1);
                    txtAnio.Text = anio.ToString();
                    txtImporte.Text = importe;
                    txtComentario.Text = comentario;
                    idRegistro = id;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            idRegistro = 0;
            foreach (Control c in grpbDatos.Controls) {
                if (c is ComboBox) {
                    ComboBox cmb = c as ComboBox;
                    cmb.SelectedIndex = -1;
                }
                else if (c is TextBox) {
                    TextBox txt = c as TextBox;
                    txt.Text = string.Empty;
                }
            }
            cmbMes.SelectedIndex = (DateTime.Now.Month - 1);
            txtAnio.Text = DateTime.Now.Year.ToString();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int fila = dgvShowDatos.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (fila != -1)
                {
                    DialogResult result = MessageBox.Show("El registro se eliminara, ¿Deseas continuar?", Application.CompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.No)
                        return;
                    Cursor = Cursors.WaitCursor;
                    int id = Int32.Parse(dgvShowDatos.Rows[fila].Cells[(int)Columnas.Id].Value.ToString().Trim());
                    SqlConnection conexion = new SqlConnection(cadenaConexion);
                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_tbl_Creditos_Provedor", conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Delete);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@provedor", string.Empty);
                        cmd.Parameters.AddWithValue("@liberacion", string.Empty);
                        cmd.Parameters.AddWithValue("@periodicidad", string.Empty);
                        cmd.Parameters.AddWithValue("@mes", 0);
                        cmd.Parameters.AddWithValue("@anio", string.Empty);
                        cmd.Parameters.AddWithValue("@importe", decimal.Zero);
                        cmd.Parameters.AddWithValue("@comentario", string.Empty);
                        SqlParameter par = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                        par.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(par);
                        if (conexion.State != ConnectionState.Open)
                            conexion.Open();
                        cmd.ExecuteNonQuery();
                        if (id == idRegistro)
                            btnNuevo_Click(sender, e);
                        string mensajeSuccess = cmd.Parameters["@MensajeSuccess"].Value.ToString();
                        MessageBox.Show(mensajeSuccess, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDatos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conexion.Close();
                        Cursor = Cursors.Default;
                    }   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvShowDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvShowDatos.Rows) {
                if (row.Index % 2 != 0)
                    row.DefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220);
            }
        }     

    }
}
