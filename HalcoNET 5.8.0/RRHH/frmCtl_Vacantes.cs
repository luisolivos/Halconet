using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RRHH
{
    public partial class frmCtl_Vacantes : Form
    {
        private DataTable DatosPJ = new DataTable();
        private DataTable DatosTRZONE = new DataTable();
        private bool first = false;

        public frmCtl_Vacantes()
        {
            InitializeComponent();
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        public enum Columas
        {
            No,
            FechaReq,
            Puesto,
            Area,
            Tipo,
            Sucursal,
            Nivel,
            Responsable,
            Estatus,
            Observaciones,
            Sueldo,
            Medios,
            Requisicion,
            Dias,
            FechaCuierre,
            Confidencial,
            Costo
        }

        private DataTable GetData(int _type)
        {
            DataTable _tbl = new DataTable();
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_RRHH", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", _type);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(_tbl);
                }
            }

            return _tbl;
        }

        public void Formato(DataGridView dgv)
        {
            bool visible = ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.RecursosHumanos
                   || (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador);
            dgv.Columns[(int)Columas.No].ReadOnly = true;
            dgv.Columns[(int)Columas.Dias].ReadOnly = true;
            dgv.Columns[(int)Columas.Confidencial].Visible = visible;

            dgv.Columns[(int)Columas.No].Visible = false;
            dgv.Columns[(int)Columas.FechaReq].Width = 75;
            dgv.Columns[(int)Columas.Puesto].Width = 100;
            dgv.Columns[(int)Columas.Area].Width = 80;
            dgv.Columns[(int)Columas.Tipo].Width = 70;
            dgv.Columns[(int)Columas.Sucursal].Width = 70;
            dgv.Columns[(int)Columas.Nivel].Width = 60;
            dgv.Columns[(int)Columas.Responsable].Width = 100;
            dgv.Columns[(int)Columas.Estatus].Width = 70;
            dgv.Columns[(int)Columas.Observaciones].Width = 100;
            dgv.Columns[(int)Columas.Sueldo].Width = 100;
            dgv.Columns[(int)Columas.Medios].Width = 100;
            dgv.Columns[(int)Columas.Requisicion].Width = 90;
            dgv.Columns[(int)Columas.Dias].Width = 75;
            dgv.Columns[(int)Columas.Confidencial].Width = 70;
            dgv.Columns[(int)Columas.Costo].Width = 80;

            dgv.Columns[(int)Columas.FechaReq].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Puesto].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Area].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Tipo].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Sucursal].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Nivel].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Responsable].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Estatus].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Observaciones].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Sueldo].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Medios].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Requisicion].ReadOnly = !visible;
            dgv.Columns[(int)Columas.Costo].ReadOnly = !visible;

            dgv.Columns[(int)Columas.Costo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columas.Costo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void CargarFiltros()
        {
            clbFiltro.DataSource = this.GetData(8);
            clbFiltro.DisplayMember = "Nombre";
            clbFiltro.ValueMember = "Nombre";
        }

        private void Combo(int filaEdit, int columnaEdit, int ColumnaCombo, DataGridView dgv, DataTable _DataSource, string ValueMember, string DisplayMember)
        {
            if (filaEdit == -1) return;
            if (columnaEdit == ColumnaCombo)
            {
                object objValCell = dgv.Rows[filaEdit].Cells[columnaEdit].Value;
                if (objValCell == null)
                    return;
                string valCell = objValCell.ToString();
                DataGridViewComboBoxCell celcombo = new DataGridViewComboBoxCell();
                object objTipoInci = dgv.Rows[filaEdit].Cells[ColumnaCombo].Value;

                if (objTipoInci == null) return;

                string tipoInci = objTipoInci.ToString();
                celcombo.DataSource = _DataSource;
                celcombo.ValueMember = ValueMember;
                celcombo.DisplayMember = DisplayMember;
                //celcombo.
                // el campo es NULL(BD, no se a justificado) 
                if (valCell == string.Empty)
                {
                    dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                }
                else
                {
                    celcombo.Value = valCell.Trim();
                    dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                }
            }
        }

        private void frmCtl_Vacantes_Load(object sender, EventArgs e)
        {
            try
            {
                row = 0;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_RRHH", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 7);
                        command.Parameters.AddWithValue("@Todo", checkBox1.Checked);
                        command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.NombreUsuario);

                        connection.Open();
                        string __in = command.ExecuteScalar().ToString();

                        if (!__in.Equals("Ok"))
                        {
                            btnConsult.Enabled = false;
                            btnGuardar.Enabled = false;
                            checkBox1.Enabled = false;

                            MessageBox.Show("No tienes permiso para ingresar a este reporte", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }

                dgvPJ.AllowUserToAddRows = (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.RecursosHumanos
                   || (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador;
                dgvTrZone.AllowUserToAddRows = (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.RecursosHumanos
                  || (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador;
                btnConsult.Enabled = (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.RecursosHumanos
                   || (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador;
                btnGuardar.Enabled = (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.RecursosHumanos
                   || (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador;
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_RRHH", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Todo", checkBox1.Checked);
                        command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.NombreUsuario);
                        command.Parameters.AddWithValue("@Empresa", "PJ");
                        command.Parameters.AddWithValue("@Desde", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@Hasta", dateTimePicker2.Value);
                        if (cbContratacion.Checked | cbRequisicion.Checked)
                        {
                            string Fecha = string.Empty;
                            if (cbRequisicion.Checked)
                                Fecha = "FechaRequisicion";
                            if (cbContratacion.Checked)
                                Fecha = "FechaCierre";

                            command.Parameters.AddWithValue("@FiltroFecha", Fecha);
                        }

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DatosPJ.Clear();

                        da.Fill(DatosPJ);
                        dgvPJ.DataSource = DatosPJ;

                        this.Formato(dgvPJ);
                    }
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_RRHH", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Todo", checkBox1.Checked);
                        command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.NombreUsuario);
                        command.Parameters.AddWithValue("@Empresa", "TRZONE");
                        command.Parameters.AddWithValue("@Desde", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@Hasta", dateTimePicker2.Value);

                        if (cbContratacion.Checked | cbRequisicion.Checked)
                        {
                            string Fecha = string.Empty;
                            if (cbRequisicion.Checked)
                                Fecha = "FechaRequisicion";
                            if (cbContratacion.Checked)
                                Fecha = "FechaCierre";

                            command.Parameters.AddWithValue("@FiltroFecha", Fecha);
                        }

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DatosTRZONE.Clear();

                        da.Fill(DatosTRZONE);
                        dgvTrZone.DataSource = DatosTRZONE;

                        this.Formato(dgvTrZone);
                    }
                }


                this.CargarFiltros();
                if (!first)
                {

                    for (int item = 0; item < clbFiltro.Items.Count - 1; item++)
                    {
                        clbFiltro.SetItemChecked(item, true);
                    }
                    first = true;
                }

                button1_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GuardarDatos(DataTable tbl, string empresa)
        {
            toolStripStatusLabel.Text = string.Empty;

            foreach (DataRow item in tbl.Rows)
            {
                if (item.RowState == DataRowState.Added || item.RowState == DataRowState.Modified)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_RRHH", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 2);
                            command.Parameters.AddWithValue("@Code", item.Field<decimal>("No"));

                            if (item[1] != DBNull.Value)
                                command.Parameters.AddWithValue("@FechaReq", item.Field<DateTime>("Fecha requisición"));

                            if (item[2] != DBNull.Value)
                                command.Parameters.AddWithValue("@Puesto", item.Field<string>("Puesto vacante"));

                            if (item[3] != DBNull.Value)
                                command.Parameters.AddWithValue("@Area", item.Field<string>("Área"));

                            if (item[4] != DBNull.Value)
                                command.Parameters.AddWithValue("@TipoVacante", item.Field<string>("Tipo de vacante"));

                            if (item[5] != DBNull.Value)
                                command.Parameters.AddWithValue("@Sucursal", item.Field<string>("Sucursal"));

                            if (item[6] != DBNull.Value)
                                command.Parameters.AddWithValue("@Urgencia", item.Field<string>("Nivel de urgencia").Substring(0, 1));

                            if (item[7] != DBNull.Value)
                                command.Parameters.AddWithValue("@Responsble", item.Field<string>("Responsable"));

                            if (item[8] != DBNull.Value)
                                command.Parameters.AddWithValue("@Estatus", item.Field<string>("Estatus"));

                            if (item[9] != DBNull.Value)
                                command.Parameters.AddWithValue("@Observaciones", item.Field<string>("Observaciones"));

                            if (item[10] != DBNull.Value)
                                command.Parameters.AddWithValue("@Sueldo", item.Field<string>("Sueldo ofertado"));

                            if (item[11] != DBNull.Value)
                                command.Parameters.AddWithValue("@Medios", item.Field<string>("Medios de reclutamiento"));

                            if (item[12] != DBNull.Value)
                                command.Parameters.AddWithValue("@Requisicion", item.Field<string>("Requisición"));

                            if (item[14] != DBNull.Value)
                                command.Parameters.AddWithValue("@FechaCierre", item.Field<DateTime>("Fecha en que se cubre"));

                            if (item[15] != DBNull.Value)
                                command.Parameters.AddWithValue("@Confidencial", item.Field<bool>("Confidencial"));

                            if (item[16] != DBNull.Value)
                                command.Parameters.AddWithValue("@Costo", item.Field<decimal>("Costo"));

                            command.Parameters.AddWithValue("@Empresa", empresa);
                            command.Parameters.AddWithValue("@UserId", ClasesSGUV.Login.Id_Usuario);

                            connection.Open();

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }

            tbl.AcceptChanges();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                this.GuardarDatos(DatosPJ, "PJ");
                this.GuardarDatos(DatosTRZONE, "TRZONE");

                this.OnLoad(e);
                toolStripStatusLabel.Text = "Listo";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
            }
        }
        private void dgvDatos_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[(int)Columas.No].Value = 0;
                e.Row.Cells[(int)Columas.Nivel].Value = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dgvDatos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            try
            {
                DataGridView dgv = (sender as DataGridView);
                if (dgv == null) return;

                int filaEdit = e.RowIndex;
                int columnaEdit = e.ColumnIndex;
                this.Combo(filaEdit, columnaEdit, (int)Columas.Sucursal, dgv, this.GetData(3), "Nombre", "Nombre");
                this.Combo(filaEdit, columnaEdit, (int)Columas.Nivel, dgv, this.GetData(4), "Valor", "Nombre");
                this.Combo(filaEdit, columnaEdit, (int)Columas.Responsable, dgv, this.GetData(5), "Nombre", "Nombre");
                this.Combo(filaEdit, columnaEdit, (int)Columas.Estatus, dgv, this.GetData(8), "Nombre", "Nombre");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            int x = 0;
            foreach (DataRow item in DatosPJ.Rows)
            {
                if (item.RowState == DataRowState.Added || item.RowState == DataRowState.Modified)
                    x++;
            }

            int y = 0;
            foreach (DataRow item in DatosTRZONE.Rows)
            {
                if (item.RowState == DataRowState.Added || item.RowState == DataRowState.Modified)
                    y++;
            }

            if (x != 0 || y != 0)
                if (MessageBox.Show("¿Desea guardar los cambios?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    btnGuardar_Click(sender, e);
            //   else
            this.OnLoad(e);
        }

        private void dgvDatos_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                toolStripStatusLabel.Text = string.Empty;
                if (MessageBox.Show("La vacante será eliminada, \r\n¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_RRHH", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 6);
                            command.Parameters.AddWithValue("@Code", e.Row.Cells[(int)Columas.No].Value);

                            connection.Open();

                            command.ExecuteNonQuery();

                            toolStripStatusLabel.Text = "Listo";
                        }
                    }
                }
                else
                    e.Cancel = true;

            }
            catch (Exception ex)
            {
                toolStripStatusLabel.Text = ex.Message;
            }
        }

        int row = 0;
        private void dgvDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            
            var grid = (sender as DataGridView);
          
            if (grid.Rows[e.RowIndex].Visible)
            {
                var rowIdx = (row + 1).ToString();
                var centerFormat = new StringFormat()
                {

                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
                e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
                row++;
            }
        }
        
        private void clbFiltro_Click(object sender, EventArgs e)
        {

            //if (clb.CheckedItems.Count == 0)
            //{
            //    foreach (DataRowView item in clb.Items)
            //    {
            //        if (item["Codigo"].ToString() != "0")
            //        {
            //            if (!clb.ToString().Equals(string.Empty))
            //            {
            //                stb.Append(",");
            //            }
            //            stb.Append(item["Codigo"].ToString());
            //        }
            //    }
            //}

        }
        List<string> listaFiltros = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                listaFiltros.Clear();

                CheckedListBox clb = clbFiltro;

                StringBuilder stb = new StringBuilder();
                foreach (DataRowView item in clb.CheckedItems)
                {
                    if (item["Nombre"].ToString() != "0")
                    {
                        if (!clb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append("'" + item["Nombre"].ToString() + "'");
                        listaFiltros.Add(item["Nombre"].ToString());
                    }
                }

                foreach (DataGridViewRow item in dgvPJ.Rows)
                {
                    if (!item.IsNewRow)
                    {
                        if (this.dgvPJ.CurrentCell != null)
                            this.dgvPJ.CurrentCell = null;

                        if (!listaFiltros.Contains(item.Cells[(int)Columas.Estatus].Value.ToString()))
                        {
                            item.Visible = false;
                        }
                        else
                        {
                            item.Visible = true;
                        }
                    }
                }

                foreach (DataGridViewRow item in dgvTrZone.Rows)
                {
                    if (!item.IsNewRow)
                    {
                        if (this.dgvTrZone.CurrentCell != null)
                            this.dgvTrZone.CurrentCell = null;

                        if (!listaFiltros.Contains(item.Cells[(int)Columas.Estatus].Value.ToString()))
                        {
                            item.Visible = false;
                        }
                        else
                        {
                            item.Visible = true;
                        }
                    }
                }
            }catch(Exception)
            {
            }

        }

        private void buttonfiltrar_Click(object sender, EventArgs e)
        {
            btnConsult_Click(sender, e);
        }

        private void dgvPJ_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            button1_Click(sender, e);
        }

        private void dgv_Paint(object sender, PaintEventArgs e)
        {
            row = 0;
        }
    }
}
