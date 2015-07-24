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
    public partial class frmMttoBancos : Form
    {
        private string[] MonedaStr = new string[] { "MXP", "USD" };
        string valCuenta = string.Empty;

        public frmMttoBancos()
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

        private enum ColumnasMantenimiento
        {
            Banco,
            TipoCredito,
            Tasa,
            Moneda,
            LineaAut,
            CostoEmision,
            Garantias,
            OSolidarios,
            Cuenta
        }

        private void formatoGridMantenimiento()
        {
            try
            {
                dgvVistaRegistros.RowHeadersVisible = false;
                //
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.Banco].Width = 100;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.TipoCredito].Width = 180;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.Tasa].Width = 50;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.Moneda].Width = 50;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.LineaAut].Width = 80;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.CostoEmision].Width = 120;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.Garantias].Width = 200;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.OSolidarios].Width = 250;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.Cuenta].Visible = false;
                //
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.OSolidarios].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.LineaAut].DefaultCellStyle.Format = "C0";
                //dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.CostoEmision].DefaultCellStyle.Format = "C0";
                //
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.LineaAut].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.Tasa].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvVistaRegistros.Columns[(int)ColumnasMantenimiento.Moneda].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //
                dgvVistaRegistros.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
                foreach (DataGridViewColumn col in dgvVistaRegistros.Columns)
                {
                    col.ReadOnly = true;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    col.ContextMenuStrip = null;
                    col.ContextMenuStrip = cmsMenuContextual;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadGridMantenimiento()
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
                cmd.Parameters.AddWithValue("@moneda", "MXP");
                cmd.Parameters.AddWithValue("@lineaAutorizada", decimal.Zero);
                cmd.Parameters.AddWithValue("@costoEmision", string.Empty);
                cmd.Parameters.AddWithValue("@garantias", string.Empty);
                cmd.Parameters.AddWithValue("@oSolidarios", string.Empty);
                cmd.Parameters.AddWithValue("@cuenta", string.Empty);

                if (coneccion.State != ConnectionState.Open)
                    coneccion.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dgvVistaRegistros.DataSource = dt;
                this.formatoGridMantenimiento();
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

        private bool validarDatos()
        {
            try
            {
                if (String.IsNullOrEmpty(txtBanco.Text.Trim()))
                {
                    txtBanco.Focus();
                    return false;
                }
                if (String.IsNullOrEmpty(txtTipoCredito.Text.Trim()))
                {
                    txtTipoCredito.Focus();
                    return false;
                }
                if (String.IsNullOrEmpty(txtTasa.Text.Trim()))
                {
                    txtTasa.Focus();
                    return false;
                }
                if (String.IsNullOrEmpty(txtLineaAut.Text.Trim()))
                {
                    txtLineaAut.Focus();
                    return false;
                }
                decimal linea = 0;
                if (!decimal.TryParse(txtLineaAut.Text.Trim(), out linea))
                {
                    txtLineaAut.Focus();
                    MessageBox.Show("La cantidad introducida tiene un formato no valido.", "Cantidad no valida.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                /*if (String.IsNullOrEmpty(txtCosto.Text.Trim())) {
                    txtCosto.Focus();
                    return false;
                }*/
                if (String.IsNullOrEmpty(txtOSolidarios.Text.Trim()))
                {
                    txtOSolidarios.Focus();
                    return false;
                }
                if (String.IsNullOrEmpty(txtCuenta.Text.Trim()))
                {
                    txtCuenta.Focus();
                    return false;
                }
                if (cmbMoneda.SelectedIndex < 0)
                {
                    cmbMoneda.Focus();
                    return false;
                }
                if (!String.IsNullOrEmpty(txtCosto.Text.Trim()))
                {
                    //string costo = string.Empty;
                    //if (!decimal.TryParse(txtCosto.Text.Trim(), out costo))
                    //{
                    //    txtCosto.Focus();
                    //    MessageBox.Show("La cantidad introducida tiene un formato no valido.", "Cantidad no valida.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return false;
                    //}
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnAddMantenimiento_Click(object sender, EventArgs e)
        {
            SqlConnection coneccion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            try
            {
                if (!this.validarDatos())
                    return;
                Cursor = Cursors.WaitCursor;
                SqlCommand cmd = new SqlCommand("sp_tbl_mttobancos", coneccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                //
                cmd.Parameters.AddWithValue("@tipoStatement", 1);
                cmd.Parameters.AddWithValue("@banco", txtBanco.Text.Trim());
                cmd.Parameters.AddWithValue("@tipoCredito", txtTipoCredito.Text.Trim());
                cmd.Parameters.AddWithValue("@tasa", txtTasa.Text.Trim());
                int indiceSelected = cmbMoneda.SelectedIndex;
                cmd.Parameters.AddWithValue("@moneda", MonedaStr[indiceSelected]);
                decimal linea = decimal.Parse(txtLineaAut.Text.Trim());
                cmd.Parameters.AddWithValue("@lineaAutorizada", linea);//txtLineaAut.Text.Trim());
                if (String.IsNullOrEmpty(txtCosto.Text.Trim()))
                    cmd.Parameters.AddWithValue("@costoEmision", DBNull.Value);
                else
                {
                    string costo = txtCosto.Text.Trim();
                    cmd.Parameters.AddWithValue("@costoEmision", costo);
                }
                if (String.IsNullOrEmpty(txtGarantias.Text.Trim()))
                    cmd.Parameters.AddWithValue("@garantias", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@garantias", txtGarantias.Text.Trim());
                if (String.IsNullOrEmpty(txtOSolidarios.Text.Trim()))
                    cmd.Parameters.AddWithValue("@oSolidarios", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@oSolidarios", txtOSolidarios.Text.Trim());
                cmd.Parameters.AddWithValue("@cuenta", txtCuenta.Text.Trim());
                SqlParameter parametroSalida = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                parametroSalida.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parametroSalida);
                if (coneccion.State != ConnectionState.Open)
                    coneccion.Open();
                cmd.ExecuteNonQuery();
                string msnSalida = Convert.ToString(cmd.Parameters["@MensajeSuccess"].Value.ToString());
                MessageBox.Show(msnSalida, "Suceso...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if ("Información guardada correctamente.".ToLower().Equals(msnSalida.ToLower()))
                {
                    foreach (Control c in splitContainerPrincipal.Panel1.Controls)
                    {
                        if (c is TextBox)
                        {
                            (c as TextBox).Clear();
                        }
                        else if (c is ComboBox)
                        {
                            (c as ComboBox).SelectedIndex = -1;
                        }
                    }
                    valCuenta = string.Empty;
                    this.loadGridMantenimiento();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                coneccion.Close();
            }
            Cursor = Cursors.Default;
        }

        private void dgvVistaRegistros_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    int filaIndex = e.RowIndex;
                    if (filaIndex != -1)
                    {
                        dgvVistaRegistros.ClearSelection();
                        dgvVistaRegistros.Rows[filaIndex].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItemEliminar_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            DialogResult dialog = MessageBox.Show("El registro se eliminará, ¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialog == System.Windows.Forms.DialogResult.Yes)
            {
            
                SqlConnection coneccion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
                try
                {
                    int fila = dgvVistaRegistros.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                    DataTable dt = (dgvVistaRegistros.DataSource as DataTable);
                    if (dgvVistaRegistros.Rows.Count == dt.Rows.Count)
                    {
                        string cuenta = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.Cuenta].Value.ToString();
                        if (String.IsNullOrEmpty(cuenta))
                            throw new ArgumentException("No existe un valor consistente en el registro.");

                        SqlCommand cmd = new SqlCommand("sp_tbl_mttobancos", coneccion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        //
                        cmd.Parameters.AddWithValue("@tipoStatement", TipoStatement.Delete);
                        cmd.Parameters.AddWithValue("@banco", string.Empty);
                        cmd.Parameters.AddWithValue("@tipoCredito", string.Empty);
                        cmd.Parameters.AddWithValue("@tasa", string.Empty);
                        cmd.Parameters.AddWithValue("@moneda", string.Empty);
                        cmd.Parameters.AddWithValue("@lineaAutorizada", decimal.Zero);
                        cmd.Parameters.AddWithValue("@costoEmision", string.Empty);
                        cmd.Parameters.AddWithValue("@garantias", string.Empty);
                        cmd.Parameters.AddWithValue("@oSolidarios", string.Empty);
                        cmd.Parameters.AddWithValue("@cuenta", cuenta.Trim());
                        SqlParameter parameterSalida = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                        parameterSalida.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(parameterSalida);
                        if (coneccion.State != ConnectionState.Open)
                            coneccion.Open();
                        cmd.ExecuteNonQuery();
                        string mensaje = Convert.ToString(cmd.Parameters["@MensajeSuccess"].Value.ToString());
                        MessageBox.Show(mensaje, "Suceso...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.loadGridMantenimiento();
                        valCuenta = string.Empty;
                    }
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
            Cursor = Cursors.Default;
        }

        private void frmMttoBancos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            cmbMoneda.Items.AddRange(MonedaStr);
            loadGridMantenimiento();
        }

        private void toolStripMenuItemEditar_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdateMantenimiento.Location = new Point(btnAddMantenimiento.Location.X, btnAddMantenimiento.Location.Y);
                btnAddMantenimiento.Visible = false;
                btnUpdateMantenimiento.Visible = true;               

                int fila = dgvVistaRegistros.Rows.GetFirstRow(DataGridViewElementStates.Selected);
                txtBanco.Text = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.Banco].Value.ToString();
                txtTipoCredito.Text = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.TipoCredito].Value.ToString();
                txtTasa.Text = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.Tasa].Value.ToString();
                txtLineaAut.Text = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.LineaAut].Value.ToString();
                txtCosto.Text = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.CostoEmision].Value.ToString();
                txtGarantias.Text = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.Garantias].Value.ToString();
                txtOSolidarios.Text = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.OSolidarios].Value.ToString();
                txtCuenta.Text = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.Cuenta].Value.ToString();
                int indexOf = Array.IndexOf(MonedaStr, dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.Moneda].Value.ToString().Trim());
                cmbMoneda.SelectedIndex = indexOf;
                valCuenta = dgvVistaRegistros.Rows[fila].Cells[(int)ColumnasMantenimiento.Cuenta].Value.ToString();

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateMantenimiento_Click(object sender, EventArgs e)
        {
            SqlConnection coneccion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            try
            {
                if (!this.validarDatos())
                    return;
                Cursor = Cursors.WaitCursor;
                SqlCommand cmd = new SqlCommand("sp_tbl_mttobancos", coneccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                //
                cmd.Parameters.AddWithValue("@tipoStatement", 2);
                cmd.Parameters.AddWithValue("@banco", txtBanco.Text.Trim());
                cmd.Parameters.AddWithValue("@tipoCredito", txtTipoCredito.Text.Trim());
                cmd.Parameters.AddWithValue("@tasa", txtTasa.Text.Trim());
                int indiceSelected = cmbMoneda.SelectedIndex;
                cmd.Parameters.AddWithValue("@moneda", MonedaStr[indiceSelected]);
                decimal linea = decimal.Parse(txtLineaAut.Text.Trim());
                cmd.Parameters.AddWithValue("@lineaAutorizada", linea);//txtLineaAut.Text.Trim());
                if (String.IsNullOrEmpty(txtCosto.Text.Trim()))
                    cmd.Parameters.AddWithValue("@costoEmision", DBNull.Value);
                else
                {
                    string costo = txtCosto.Text.Trim();
                    cmd.Parameters.AddWithValue("@costoEmision", costo);
                }
                if (String.IsNullOrEmpty(txtGarantias.Text.Trim()))
                    cmd.Parameters.AddWithValue("@garantias", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@garantias", txtGarantias.Text.Trim());
                if (String.IsNullOrEmpty(txtOSolidarios.Text.Trim()))
                    cmd.Parameters.AddWithValue("@oSolidarios", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@oSolidarios", txtOSolidarios.Text.Trim());
                cmd.Parameters.AddWithValue("@cuenta", txtCuenta.Text.Trim());
                cmd.Parameters.AddWithValue("@prevCuenta", valCuenta.Trim());
                SqlParameter parametroSalida = new SqlParameter("@MensajeSuccess", SqlDbType.NVarChar, 500);
                parametroSalida.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parametroSalida);
                if (coneccion.State != ConnectionState.Open)
                    coneccion.Open();
                cmd.ExecuteNonQuery();
                string msnSalida = Convert.ToString(cmd.Parameters["@MensajeSuccess"].Value.ToString());
                MessageBox.Show(msnSalida, "Suceso...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if ("Información guardada correctamente.".ToLower().Equals(msnSalida.ToLower()))
                {
                    foreach (Control c in splitContainerPrincipal.Panel1.Controls)
                    {
                        if (c is TextBox)
                        {
                            (c as TextBox).Clear();
                        }
                        else if (c is ComboBox)
                        {
                            (c as ComboBox).SelectedIndex = -1;
                        }
                    }
                    btnUpdateMantenimiento.Visible = false;
                    btnAddMantenimiento.Visible = true;                    
                    this.loadGridMantenimiento();
                    valCuenta = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error inesperado.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                coneccion.Close();
                Cursor = Cursors.Default;
            }
            
        }

        private void btnNuevoMantenimiento_Click(object sender, EventArgs e)
        {
            btnUpdateMantenimiento.Visible = false;
            btnAddMantenimiento.Visible = true;
            foreach (Control c in splitContainerPrincipal.Panel1.Controls)
            {
                if (c is TextBox)
                {
                    (c as TextBox).Clear();
                }
                else if (c is ComboBox)
                {
                    (c as ComboBox).SelectedIndex = -1;
                }


            }

            txtBanco.Focus();
        }

    }
}
