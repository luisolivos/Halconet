using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sistemas
{
    public partial class frmReportePendientesABM : Form
    {
        int idActualizar = 0;
        private string[] cmbOptions = new string[] { "Todo", "Pendiente", "Finalizado" };
        public frmReportePendientesABM()
        {
            InitializeComponent();
        }

        private enum Columnas { 
            idRP,
		    Proyecto,
			Descripcion,
			SolicitudPor,
			FechaSolicitud,
			FechaConclusion,
			Situacion,
			Observaciones,
			Estatus
        }

        private enum TipoSelect { 
            Todo = 0,
            ByEstatus = 1
        }

        private enum TipoStatement { 
            Insert = 1,
            Update = 2,
            Delete = 3,
            Select = 4
        }

        private void formatoGrid() {
            try
            {
                dgvShowDatos.Columns[(int)Columnas.idRP].Visible = false;
                dgvShowDatos.Columns[(int)Columnas.Proyecto].HeaderText = "Poyecto";
                dgvShowDatos.Columns[(int)Columnas.Descripcion].HeaderText = "Descripción";
                dgvShowDatos.Columns[(int)Columnas.SolicitudPor].HeaderText = "Solicitado Por";
                dgvShowDatos.Columns[(int)Columnas.FechaSolicitud].HeaderText = "Fecha de solicitud";
                dgvShowDatos.Columns[(int)Columnas.FechaConclusion].HeaderText = "Fecha tentativa de conclusión";
                dgvShowDatos.Columns[(int)Columnas.Situacion].HeaderText = "Situación";
                dgvShowDatos.Columns[(int)Columnas.Observaciones].HeaderText = "Observaciones";
                dgvShowDatos.Columns[(int)Columnas.Estatus].HeaderText = "Estatus";

                dgvShowDatos.Columns[(int)Columnas.Situacion].DefaultCellStyle.Format = "P0";
                dgvShowDatos.Columns[(int)Columnas.Situacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //
                dgvShowDatos.Columns[(int)Columnas.Proyecto].Width = 150;
                dgvShowDatos.Columns[(int)Columnas.Descripcion].Width = 200;
                dgvShowDatos.Columns[(int)Columnas.SolicitudPor].Width = 150;
                dgvShowDatos.Columns[(int)Columnas.FechaSolicitud].Width = 100;
                dgvShowDatos.Columns[(int)Columnas.FechaConclusion].Width = 100;
                dgvShowDatos.Columns[(int)Columnas.Situacion].Width = 60;
                dgvShowDatos.Columns[(int)Columnas.Observaciones].Width = 200;
                dgvShowDatos.Columns[(int)Columnas.Estatus].Width = 50;

                dgvShowDatos.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                foreach (DataGridViewColumn col in dgvShowDatos.Columns) {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.ReadOnly = true;
                    col.ContextMenuStrip = null;
                    col.ContextMenuStrip = contextMenuStripAcciones;
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Error inseperado: \r\n" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formatoGridByFiltro()
        {
            try
            {
                dgvShowDatosResultado.Columns[(int)Columnas.idRP].Visible = false;
                dgvShowDatosResultado.Columns[(int)Columnas.Estatus].Visible = false;
                dgvShowDatosResultado.Columns[(int)Columnas.Proyecto].HeaderText = "Poyecto";
                dgvShowDatosResultado.Columns[(int)Columnas.Descripcion].HeaderText = "Descripción";
                dgvShowDatosResultado.Columns[(int)Columnas.SolicitudPor].HeaderText = "Solicitado Por";
                dgvShowDatosResultado.Columns[(int)Columnas.FechaSolicitud].HeaderText = "Fecha de solicitud";
                dgvShowDatosResultado.Columns[(int)Columnas.FechaConclusion].HeaderText = "Fecha tentativa de conclusión";
                dgvShowDatosResultado.Columns[(int)Columnas.Situacion].HeaderText = "Situación";
                dgvShowDatosResultado.Columns[(int)Columnas.Observaciones].HeaderText = "Observaciones";
                //dgvShowDatosResultado.Columns[(int)Columnas.Estatus].HeaderText = "Estatus";
                //
                dgvShowDatosResultado.Columns[(int)Columnas.Situacion].DefaultCellStyle.Format ="P0";
                dgvShowDatosResultado.Columns[(int)Columnas.Situacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvShowDatosResultado.Columns[(int)Columnas.Proyecto].Width = 150;
                dgvShowDatosResultado.Columns[(int)Columnas.Descripcion].Width = 200;
                dgvShowDatosResultado.Columns[(int)Columnas.SolicitudPor].Width = 150;
                dgvShowDatosResultado.Columns[(int)Columnas.FechaSolicitud].Width = 100;
                dgvShowDatosResultado.Columns[(int)Columnas.FechaConclusion].Width = 100;
                dgvShowDatosResultado.Columns[(int)Columnas.Situacion].Width = 60;
                dgvShowDatosResultado.Columns[(int)Columnas.Observaciones].Width = 200;
                //dgvShowDatosResultado.Columns[(int)Columnas.Estatus].Width = 50;

                dgvShowDatosResultado.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                foreach (DataGridViewColumn col in dgvShowDatosResultado.Columns)
                {
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inseperado: \r\n" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmReportePendientesABM_Load(object sender, EventArgs e)
        {
             this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            cmbFiltros.Items.AddRange(cmbOptions);
            cargarInformacion();
        }

        private void cargarInformacion() {
            string cadenaConeccion = ClasesSGUV.Propiedades.conectionSGUV;
            SqlConnection conexion = new SqlConnection(cadenaConeccion);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_reporte_pendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Select);
                cmd.Parameters.AddWithValue("@tipoSelect", TipoSelect.Todo);
                cmd.Parameters.AddWithValue("@idrp", 0);
                cmd.Parameters.AddWithValue("@proyecto", string.Empty);
                cmd.Parameters.AddWithValue("@descripcion", string.Empty);
                cmd.Parameters.AddWithValue("@solicitudPor", string.Empty);
                cmd.Parameters.AddWithValue("@fechaSolicitud", DateTime.Now);
                cmd.Parameters.AddWithValue("@fechaConclusion", DateTime.Now);
                cmd.Parameters.AddWithValue("@situacion", 0);
                cmd.Parameters.AddWithValue("@observaciones", string.Empty);
                cmd.Parameters.AddWithValue("@estatus", true);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (!reader.IsClosed)
                    reader.Close();
                dgvShowDatos.DataSource = dt;
                this.formatoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inseperado: \r\n" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conexion.Close();
            }
                
        }

        private void cargarInformacionByFiltro()
        {
            int indexSelected = cmbFiltros.SelectedIndex;
            string cadenaConeccion = ClasesSGUV.Propiedades.conectionSGUV;
            SqlConnection conexion = new SqlConnection(cadenaConeccion);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_tbl_reporte_pendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Select);
                if (indexSelected == 0)
                    cmd.Parameters.AddWithValue("@tipoSelect", TipoSelect.Todo);
                else if (indexSelected == 1)
                {
                    cmd.Parameters.AddWithValue("@tipoSelect", TipoSelect.ByEstatus);
                    cmd.Parameters.AddWithValue("@estatus", false);
                }
                else if (indexSelected == 2)
                {
                    cmd.Parameters.AddWithValue("@tipoSelect", TipoSelect.ByEstatus);
                    cmd.Parameters.AddWithValue("@estatus", true);
                }
                cmd.Parameters.AddWithValue("@idrp", 0);
                cmd.Parameters.AddWithValue("@proyecto", string.Empty);
                cmd.Parameters.AddWithValue("@descripcion", string.Empty);
                cmd.Parameters.AddWithValue("@solicitudPor", string.Empty);
                cmd.Parameters.AddWithValue("@fechaSolicitud", DateTime.Now);
                cmd.Parameters.AddWithValue("@fechaConclusion", DateTime.Now);
                cmd.Parameters.AddWithValue("@situacion", 0);
                cmd.Parameters.AddWithValue("@observaciones", string.Empty);

                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                if (!reader.IsClosed)
                    reader.Close();
                dgvShowDatosResultado.DataSource = dt;
                this.formatoGridByFiltro();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inseperado: \r\n" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtProyecto.Clear();
            txtDescripcion.Clear();
            txtSolicitadoPor.Clear();
            txtSituacion.Clear();
            txtObservaciones.Clear();
            maskedTxtFechaSolicitud.Clear();
            maskedTxtFechaConclusion.Clear();
            btnActualizar.Visible = false;
            btnAgregar.Visible = true;
            chkEstatus.Checked = false;
            idActualizar = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string proyecto = txtProyecto.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string solicitadoPor = txtSolicitadoPor.Text.Trim();
            string fechaS = maskedTxtFechaSolicitud.Text.Trim();
            string fechaC = maskedTxtFechaConclusion.Text.Trim();
            string situacion = txtSituacion.Text.Trim();
            string observaciones = txtObservaciones.Text.Trim();
            if (String.IsNullOrEmpty(proyecto)) {
                txtProyecto.Focus();
                return;
            }
            if (String.IsNullOrEmpty(descripcion))
            {
                txtDescripcion.Focus();
                return;
            }
            if (String.IsNullOrEmpty(solicitadoPor))
            {
                txtSolicitadoPor.Focus();
                return;
            }
            int SituacionP = 0;
            if(!Int32.TryParse(situacion, out SituacionP)){
                txtSituacion.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            SituacionP = Int32.Parse(situacion);            
            string cadenaConeccion = ClasesSGUV.Propiedades.conectionSGUV;
            SqlConnection conexion = new SqlConnection(cadenaConeccion);
            try
            {
                string regExStr = "[0-9]{2}/[0-9]{2}/[0-9]{4}";
                SqlCommand cmd = new SqlCommand("sp_tbl_reporte_pendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Insert);
                cmd.Parameters.AddWithValue("@idrp", 0);
                cmd.Parameters.AddWithValue("@proyecto", proyecto);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@solicitudPor", solicitadoPor);
                if (System.Text.RegularExpressions.Regex.IsMatch(fechaS, regExStr)) {
                    DateTime dt = DateTime.Parse(fechaS);
                    cmd.Parameters.AddWithValue("@fechaSolicitud", dt.Date);
                }
                else
                    cmd.Parameters.AddWithValue("@fechaSolicitud", DBNull.Value);
                if (System.Text.RegularExpressions.Regex.IsMatch(fechaC, regExStr)) {
                    DateTime dt = DateTime.Parse(fechaC);
                    cmd.Parameters.AddWithValue("@fechaConclusion", dt.Date);
                }
                else
                    cmd.Parameters.AddWithValue("@fechaConclusion", DBNull.Value);   
                cmd.Parameters.AddWithValue("@situacion", SituacionP);
                cmd.Parameters.AddWithValue("@observaciones", observaciones);
                cmd.Parameters.AddWithValue("@estatus", chkEstatus.Checked);
                SqlParameter par = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                par.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                cmd.ExecuteNonQuery();
                string mensaje = Convert.ToString(cmd.Parameters["@MensajeSuccess"].Value.ToString());
                MessageBox.Show(mensaje, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cargarInformacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inseperado: \r\n" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                conexion.Close();
            }
            Cursor = Cursors.Default;
        }

        private void dgvShowDatos_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    int filaIndex = e.RowIndex;
                    if (filaIndex != -1)
                    {
                        dgvShowDatos.ClearSelection();
                        dgvShowDatos.Rows[filaIndex].Selected = true;
                        idActualizar = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult dialogo = MessageBox.Show("El registro se eliminará, ¿Deseas continuar?", "Confirmar...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogo == System.Windows.Forms.DialogResult.No)
                return;
            string cadenaConeccion = ClasesSGUV.Propiedades.conectionSGUV;
            SqlConnection conexion = new SqlConnection(cadenaConeccion);
            Cursor = Cursors.WaitCursor;
            try
            {
                int fila = dgvShowDatos.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (fila == -1)
                {
                    Cursor = Cursors.Default;
                    return;
                }
                string idStr = dgvShowDatos.Rows[fila].Cells[(int)Columnas.idRP].Value.ToString().Trim();
                if (String.IsNullOrEmpty(idStr)) {
                    Cursor = Cursors.Default;
                    return;
                }
                int id = 0;
                if(!Int32.TryParse(idStr, out id)){
                    Cursor = Cursors.Default;
                    return;
                }
                id = Int32.Parse(idStr);
                SqlCommand cmd = new SqlCommand("sp_tbl_reporte_pendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Delete);
                cmd.Parameters.AddWithValue("@idrp", id);
                cmd.Parameters.AddWithValue("@proyecto", string.Empty);
                cmd.Parameters.AddWithValue("@descripcion", string.Empty);
                cmd.Parameters.AddWithValue("@solicitudPor", string.Empty);
                cmd.Parameters.AddWithValue("@fechaSolicitud", DateTime.Now);
                cmd.Parameters.AddWithValue("@fechaConclusion", DateTime.Now);
                cmd.Parameters.AddWithValue("@situacion", 0);
                cmd.Parameters.AddWithValue("@observaciones", string.Empty);
                cmd.Parameters.AddWithValue("@estatus", true);
                SqlParameter par = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                par.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                cmd.ExecuteNonQuery();
                string mensaje = Convert.ToString(cmd.Parameters["@MensajeSuccess"].Value.ToString());
                MessageBox.Show(mensaje, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cargarInformacion();
                idActualizar = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally {
                btnAgregar.Visible = true;
                btnActualizar.Visible = false;
                conexion.Close();
            }   
            Cursor = Cursors.Default;
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int fila = dgvShowDatos.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                if (fila == -1)
                    return;
                string proyecto = dgvShowDatos.Rows[fila].Cells[(int)Columnas.Proyecto].Value.ToString();
                string descripcion = dgvShowDatos.Rows[fila].Cells[(int)Columnas.Descripcion].Value.ToString();
                string solicitadoPor = dgvShowDatos.Rows[fila].Cells[(int)Columnas.SolicitudPor].Value.ToString();
                string fechaSolicitud = dgvShowDatos.Rows[fila].Cells[(int)Columnas.FechaSolicitud].Value.ToString();
                string fechaConclusion = dgvShowDatos.Rows[fila].Cells[(int)Columnas.FechaConclusion].Value.ToString();
                string situacion = (Convert.ToDecimal(dgvShowDatos.Rows[fila].Cells[(int)Columnas.Situacion].Value) * 100).ToString("N0");
                string observaciones = dgvShowDatos.Rows[fila].Cells[(int)Columnas.Observaciones].Value.ToString();
                bool estatus = Convert.ToBoolean(dgvShowDatos.Rows[fila].Cells[(int)Columnas.Estatus].Value.ToString());
                txtProyecto.Text = proyecto.Trim();
                txtDescripcion.Text = descripcion.Trim();
                txtSolicitadoPor.Text = solicitadoPor.Trim();
                if (fechaSolicitud.Trim() == "Pendiente")
                    maskedTxtFechaSolicitud.Text = string.Empty;
                else
                {
                    DateTime dt = DateTime.Parse(fechaSolicitud.Trim());
                    maskedTxtFechaSolicitud.Text = dt.Date.ToString("dd/MM/yyyy");
                }
                if (fechaConclusion.Trim() == "Pendiente")
                    maskedTxtFechaConclusion.Text = string.Empty;
                else
                {
                    DateTime dt = DateTime.Parse(fechaConclusion.Trim());
                    maskedTxtFechaConclusion.Text = dt.Date.ToString("dd/MM/yyyy");
                }
                txtSituacion.Text = situacion.Trim();
                txtObservaciones.Text = observaciones.Trim();
                btnActualizar.Location = new Point(btnAgregar.Location.X, btnAgregar.Location.Y);
                chkEstatus.Checked = estatus;
                btnAgregar.Visible = false;
                btnActualizar.Visible = true;
                idActualizar = Convert.ToInt32(dgvShowDatos.Rows[fila].Cells[(int)Columnas.idRP].Value.ToString().Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string proyecto = txtProyecto.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            string solicitadoPor = txtSolicitadoPor.Text.Trim();
            string fechaS = maskedTxtFechaSolicitud.Text.Trim();
            string fechaC = maskedTxtFechaConclusion.Text.Trim();
            string situacion = txtSituacion.Text.Trim();
            string observaciones = txtObservaciones.Text.Trim();
            if (String.IsNullOrEmpty(proyecto))
            {
                txtProyecto.Focus();
                return;
            }
            if (String.IsNullOrEmpty(descripcion))
            {
                txtDescripcion.Focus();
                return;
            }
            if (String.IsNullOrEmpty(solicitadoPor))
            {
                txtSolicitadoPor.Focus();
                return;
            }
            int SituacionP = 0;
            if (!Int32.TryParse(situacion, out SituacionP))
            {
                txtSituacion.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            SituacionP = Int32.Parse(situacion);
            string cadenaConeccion = ClasesSGUV.Propiedades.conectionSGUV;
            SqlConnection conexion = new SqlConnection(cadenaConeccion);
            try
            {
                string regExStr = "[0-9]{2}/[0-9]{2}/[0-9]{4}";
                SqlCommand cmd = new SqlCommand("sp_tbl_reporte_pendientes", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Update);
                cmd.Parameters.AddWithValue("@idrp", idActualizar);
                cmd.Parameters.AddWithValue("@proyecto", proyecto);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.Parameters.AddWithValue("@solicitudPor", solicitadoPor);
                if (System.Text.RegularExpressions.Regex.IsMatch(fechaS, regExStr))
                {
                    DateTime dt = DateTime.Parse(fechaS);
                    cmd.Parameters.AddWithValue("@fechaSolicitud", dt.Date);
                }
                else
                    cmd.Parameters.AddWithValue("@fechaSolicitud", DBNull.Value);
                if (System.Text.RegularExpressions.Regex.IsMatch(fechaC, regExStr))
                {
                    DateTime dt = DateTime.Parse(fechaC);
                    cmd.Parameters.AddWithValue("@fechaConclusion", dt.Date);
                }
                else
                    cmd.Parameters.AddWithValue("@fechaConclusion", DBNull.Value);
                cmd.Parameters.AddWithValue("@situacion", SituacionP);
                cmd.Parameters.AddWithValue("@observaciones", observaciones);
                cmd.Parameters.AddWithValue("@estatus", chkEstatus.Checked);
                SqlParameter par = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                par.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(par);
                if (conexion.State != ConnectionState.Open)
                    conexion.Open();
                cmd.ExecuteNonQuery();
                string mensaje = Convert.ToString(cmd.Parameters["@MensajeSuccess"].Value.ToString());
                MessageBox.Show(mensaje, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cargarInformacion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inseperado: \r\n" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }
            Cursor = Cursors.Default;
        }

        private void btnBuscarByFiltro_Click(object sender, EventArgs e)
        {
            this.cargarInformacionByFiltro();
        }
    }
}
