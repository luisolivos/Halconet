using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;

namespace Presupuesto
{
    public partial class frmPresupuesto : Form
    {
        Logs log;

        public DataTable Datos = new DataTable();
          
        public frmPresupuesto(int _rol, string _sucursal, string _usuario)
        {
            InitializeComponent();
            RolUsuario = _rol;
            SucursalUsuario = _sucursal;
            Usuario = _usuario;
        }

        #region PARÁMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
        public string Cuenta;
        public string NombreCuenta;
        public string Sucursal;
        public string FechaInicial;
        public string FechaFinal;
        private DataTable DtsCuentas;
        private int RolUsuario;
        private string SucursalUsuario;
        private string Usuario;
        private bool Editar;

        /// <summary>
        /// Enumerador para los tipos de consulta
        /// </summary>
        private enum TipoConsulta
        {
            ConsultaGeneral = 1,
            ConsultaPorCuenta = 2,
            NormaRepartoCOM = 6
        }

        /// <summary>
        /// Enumerador para las columnas del DataGridView
        /// </summary>
        private enum ColumnasGrid
        {
            CuentasDeGasto,
            DescripcionCuenta,
            TipoDeGasto,
            Presupuestado,
            Facturado,
            Remisiones,
            Utilizado,
            Porcentaje,
            Desviación,
            Comentarios,
            BtnComentarios,
            Detalle

        }

        #endregion


        #region EVENTOS

        /// <summary>
        /// Evento que ocurre al hacer click en el btnExportar
        /// Llama a la clase que realiza el proceso
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            ExportarAExcel excel = new ExportarAExcel();
            if (excel.Exportar(dgvPresupuesto) == true)
            {
                MessageBox.Show("El documento se creó correctamente.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocurrió un error al crear el documento, no se creó el archivo.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el clbCuentas
        /// Selecciona todas o deselecciona todas
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void clbCuentas_Click(object sender, EventArgs e)
        {
            if (clbCuentas.SelectedIndex == 0)
            {
                if (clbCuentas.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbCuentas.Items.Count; item++)
                    {
                        clbCuentas.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbCuentas.Items.Count; item++)
                    {
                        clbCuentas.SetItemChecked(item, true);
                    }
                }
            }
        }

        /// <summary>
        /// Evento que ocurre al cargarse el Formulario
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void frmPresupuesto_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                this.MaximizeBox = true;

                CargarMeses();
                CargarSucursales();
                CargarCuentas();

                txtAño.Text = DateTime.Now.Year.ToString();
                cmbMes.SelectedIndex = DateTime.Now.Month - 1;

                txtAño1.Text = DateTime.Now.Year.ToString();
                cbMes1.SelectedIndex = DateTime.Now.Month - 1;
                Restricciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnPresentar
        /// Muestra el resultado de la consulta en el DataGridView dependiendo de los parámetros de entrada
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                rbExcesos.Checked = false;
                rbDefectos.Checked = false;
                rbTodo.Checked = true;

                this.Esperar();

                this.Presentar();
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Continuar();
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnFiltrar
        /// Filtra los datos del DatagGridView mostrando solo los negativos
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                IEnumerable<DataRow> query = from cuenta in DtsCuentas.AsEnumerable()
                                             where Convert.ToInt32(cuenta.Field<double>("Desviacion")) > 0 & cuenta.Field<string>("U_C_gasto") != "Total General"
                                             orderby cuenta.Field<double>("Desviacion") descending
                                             select cuenta;
                if (query.Count() > 0)
                {


                    dgvPresupuesto.Columns.Clear();
                    dgvPresupuesto.DataSource = null;
                    DataTable cuentas = query.CopyToDataTable<DataRow>();
                    CalcularTotales(cuentas);
                    double totalColPresup = 0;
                    double totalColUtilizado = 0;
                    double totalColDesviacion = 0;
                    foreach (DataRow row in cuentas.Rows)
                    {
                        totalColPresup += row.Field<double>("Presupuesto");
                        totalColUtilizado += row.Field<double>("Utilizado");
                        totalColDesviacion += row.Field<double>("Desviacion");
                    }
                    //DataRow rowTotales = cuentas.NewRow();
                    //rowTotales["U_C_gasto"] = "Total General";
                    //rowTotales["AcctName"] = string.Empty;
                    //rowTotales["FrgnName"] = string.Empty;
                    //rowTotales["Presupuesto"] = totalColPresup;
                    //rowTotales["Utilizado"] = totalColUtilizado;
                    //if (totalColPresup != 0)
                    //{
                    //    rowTotales["Porcentaje"] = Convert.ToInt32((totalColUtilizado * 100) / totalColPresup).ToString() + "%";
                    //}
                    //else
                    //{
                    //    if (totalColPresup == 0 && totalColUtilizado == 0)
                    //    {
                    //        rowTotales["Porcentaje"] = "0%";
                    //    }
                    //    else
                    //    {
                    //        if (totalColPresup == 0 && totalColUtilizado != 0)
                    //        {
                    //            rowTotales["Porcentaje"] = "Sobre girado";
                    //        }
                    //    }
                    //}
                    //rowTotales["Desviacion"] = totalColDesviacion;
                    //cuentas.Rows.Add(rowTotales);
                    dgvPresupuesto.DataSource = cuentas;
                    DarFormatoADataGrid();
                }
                else
                {
                    dgvPresupuesto.Columns.Clear();
                    dgvPresupuesto.DataSource = null;
                    MostrarEncabezados();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al completar el enlace de datos en el dgvPresupuesto
        /// Pinta (verde/rojo) la columna Diferencia de Gasto
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void dgvPresupuesto_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvPresupuesto.Rows)
                {
                    if (Convert.ToInt32(row.Cells[(int)ColumnasGrid.Desviación].Value) > 0)
                    {
                        row.Cells[(int)ColumnasGrid.Desviación].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        row.Cells[(int)ColumnasGrid.Desviación].Style.ForeColor = Color.Green;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el contenido de una celda del DataGridView
        /// Controla el click para ver el detalle del row
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void dgvPresupuesto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)ColumnasGrid.Detalle)
                    {
                        Cuenta = dgvPresupuesto.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.CuentasDeGasto].Value.ToString();
                        NombreCuenta = dgvPresupuesto.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.DescripcionCuenta].Value.ToString();
                        FormDetalle frmDetalle = new FormDetalle(Cuenta, NombreCuenta, Sucursal, FechaInicial, FechaFinal, RolUsuario);
                        frmDetalle.MdiParent = this.MdiParent;
                        frmDetalle.Show();
                        frmDetalle.WindowState = FormWindowState.Normal;
                    }


                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)ColumnasGrid.BtnComentarios)
                    {
                        Cuenta = dgvPresupuesto.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.CuentasDeGasto].Value.ToString();
                        NombreCuenta = dgvPresupuesto.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.DescripcionCuenta].Value.ToString();
                        string comentario = dgvPresupuesto.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.Comentarios].Value.ToString();
                        Comentarios frmDetalle = new Comentarios(Cuenta, NombreCuenta, Sucursal, FechaInicial, FechaFinal, RolUsuario, comentario, Editar);
                        frmDetalle.ShowDialog();
                        frmDetalle.WindowState = FormWindowState.Normal;
                        if (frmDetalle.Actualizar)
                        {
                            //Esperar();
                            //Presentar();
                            //Continuar();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPresupuesto_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvPresupuesto.Columns[e.ColumnIndex].Name == "btnComentarios" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvPresupuesto.Rows[e.RowIndex].Cells["btnComentarios"] as DataGridViewButtonCell;
                Icon icoAtomico;

                if (this.dgvPresupuesto.Rows[e.RowIndex].Cells[(int)ColumnasGrid.Comentarios].Value.ToString() != "")
                {
                    if (Convert.ToString(this.dgvPresupuesto.Rows[e.RowIndex].Cells[(int)ColumnasGrid.Comentarios].Value).Replace((char)(13), ' ').Replace((char)(10), ' ').Trim() == "")
                    {
                        icoAtomico = Properties.Resources.icon_no_comment;
                        e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
                    }
                    else
                    {
                        icoAtomico = Properties.Resources.icon_comment;
                        e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
                    }
                }
                else
                {
                    icoAtomico = Properties.Resources.icon_no_comment;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
                }

                this.dgvPresupuesto.Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                this.dgvPresupuesto.Columns[e.ColumnIndex].Width = icoAtomico.Width + 10;

                e.Handled = true;
            }
            if (e.ColumnIndex >= 0 && this.dgvPresupuesto.Columns[e.ColumnIndex].Name == "btnDetalle" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvPresupuesto.Rows[e.RowIndex].Cells["btnDetalle"] as DataGridViewButtonCell;
                Icon icoAtomicoDetalle;
                icoAtomicoDetalle = Properties.Resources.view_detailed;
                e.Graphics.DrawIcon(icoAtomicoDetalle, e.CellBounds.Left + 20, e.CellBounds.Top + 3);
                this.dgvPresupuesto.Rows[e.RowIndex].Height = icoAtomicoDetalle.Height + 10;
                this.dgvPresupuesto.Columns[e.ColumnIndex].Width = icoAtomicoDetalle.Width + 50;
                e.Handled = true;
            }
        }

        private void frmPresupuesto_FormClosed(object sender, FormClosedEventArgs e)
        {

            // Principal newForm = new Principal();

            //  this.Hide();
            // newForm.ShowDialog();
            //this.Close();

        }

        private void txtAño_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                Esperar();
                Presentar();
                Continuar();
            }

        }

        private void frmPresupuesto_Shown(object sender, EventArgs e)
        {
            //if(Usuario != "Administrador")
            button1_Click(sender, e);

            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }
        #endregion


        #region FUNCIONES
        private void Restricciones()
        {
            switch (RolUsuario)
            {
                case (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal:

                    txtAño.ReadOnly = true;
                    lblSucursal.Visible = false;
                    cbSucursal.Visible = false;
                    lblCuentasGasto.Visible = false;
                    clbCuentas.Visible = false;

                    groupBox1.Text = "Auditoria de Gasto: " + SucursalUsuario;
                    break;
                case (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteGastos:
                    groupBox1.Text = "Auditoria de Gasto: General";
                    break;

                case (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador:
                    groupBox1.Text = "Auditoria de Gasto: General";
                    break;
            }

        }
        /// <summary>
        /// Método que limpia los campos de texto
        /// </summary>
        private void LimpiarCampos()
        {
            lblPresupuestoFijo.Text = string.Empty;
            lblPresupuestoForzoso.Text = string.Empty;
            lblPresupuestoOpcional.Text = string.Empty;
            lblUtilizadoFijo.Text = string.Empty;
            lblUtilizadoForzoso.Text = string.Empty;
            lblUtilizadoOpcional.Text = string.Empty;
            lblPorcentajeFijo.Text = string.Empty;
            lblPorcentajeForzoso.Text = string.Empty;
            lblPorcentajeOpcional.Text = string.Empty;
            lblDesviacionFijo.Text = string.Empty;
            lblDesviacionForzoso.Text = string.Empty;
            lblDesviacionOpcional.Text = string.Empty;
            lblTotalPresupuesto.Text = string.Empty;
            lblTotalUtilizado.Text = string.Empty;
            lblTotalPorcentaje.Text = string.Empty;
            lblTotalDesviacion.Text = string.Empty;
        }


        private void COM()
        {
            if (cbSucursal.SelectedIndex != -1)
            {
                dgvPresupuesto.Columns.Clear();
                dgvPresupuesto.DataSource = null;

                StringBuilder stbCuentas = new StringBuilder();

                foreach (DataRowView item in clbCuentas.CheckedItems)
                {
                    if (item["AcctCode"].ToString() != "0")
                    {
                        if (!clbCuentas.ToString().Equals(string.Empty))
                        {
                            stbCuentas.Append(",");
                        }
                        stbCuentas.Append(item["AcctCode"].ToString());
                    }
                }
                if (clbCuentas.CheckedItems.Count == 0)
                {
                    foreach (DataRowView item in clbCuentas.Items)
                    {
                        if (item["AcctCode"].ToString() != "0")
                        {
                            if (!clbCuentas.ToString().Equals(string.Empty))
                            {
                                stbCuentas.Append(",");
                            }
                            stbCuentas.Append(item["AcctCode"].ToString());
                        }
                    }
                }

                Cuenta = stbCuentas.ToString();
                Sucursal = cbSucursal.Text;
                DateTime di = dtpFechaInicial.Value;
                DateTime df = dtpFechaFinal.Value;
                string Mes = "";
                int año = 0;
                int año1 = 0;
                try
                {
                    Mes = cmbMes.Text;
                    año = int.Parse(txtAño.Text);
                    año1 = int.Parse(txtAño1.Text);
                    try
                    {
                        DateTime fecha = Convert.ToDateTime("01/01/" + año);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception)
                {
                }

                FechaInicial = DateTime.Parse("01/" + getMes(cbMes1.Text) + "/" + año1).ToShortDateString();
                FechaFinal = DateTime.Parse("01/" + getMes(cmbMes.Text) + "/" + año).AddMonths(1).AddDays(-1).ToShortDateString();


                SqlCommand command = new SqlCommand("PJ_PresupuestoCuenta", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.NormaRepartoCOM);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", Cuenta);
                command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                command.Parameters.AddWithValue("@Comentario", string.Empty);
                command.Parameters.AddWithValue("@Rol", RolUsuario);

                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(table);

                dataGridView2.DataSource = table;
            }
        }
        /// <summary>
        /// Método que carga las sucursales en el cbSucursal
        /// </summary>
        private void CargarSucursales()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteGastos
                || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Flotilla)
            {
                using (SqlConnection connecion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_PresupuestoCuenta";
                        command.Connection = connecion;
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 8);
                        command.Parameters.AddWithValue("@Rol", ClasesSGUV.Login.Rol);

                        DataTable tbl = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(tbl);

                        cbSucursal.DataSource = tbl;
                        cbSucursal.DisplayMember = "Nombre";
                        cbSucursal.ValueMember = "Codigo";
                    }
                }

            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                cbSucursal.Items.Clear();
                cbSucursal.Items.Add(SucursalUsuario);
                cbSucursal.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Método que carga los meses en el cblMes
        /// </summary>
        private void CargarMeses()
        {
            DataTable Meses = new DataTable();
            Meses.Columns.Add("Index", typeof(int));
            Meses.Columns.Add("Mes", typeof(string));
            string[] array = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            for (int i = 0; i < 12; i++)
            {
                DataRow row = Meses.NewRow();
                row["Index"] = i;
                row["Mes"] = array[i];

                Meses.Rows.Add(row);
            }
            DataTable meses1 = Meses.Copy();
            cmbMes.DataSource = Meses;
            cmbMes.DisplayMember = "Mes";
            cmbMes.ValueMember = "Index";

            cbMes1.DataSource = meses1;
            cbMes1.DisplayMember = "Mes";
            cbMes1.ValueMember = "Index";
        }

        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoADataGrid()
        {
            DataGridViewButtonColumn buttonComent = new DataGridViewButtonColumn();
            {
                buttonComent.Name = "btnComentarios";
                buttonComent.HeaderText = "";
                buttonComent.Width = 100;
                buttonComent.UseColumnTextForButtonValue = true;
                buttonComent.FlatStyle = FlatStyle.Popup;
                //buttonComent.DisplayIndex = (int)ColumnasGrid.BtnComentarios;
            }
            dgvPresupuesto.Columns.Add(buttonComent);

            DataGridViewButtonColumn buttons = new DataGridViewButtonColumn();
            {
                buttons.Name = "btnDetalle";
                buttons.HeaderText = "Detalle";
                buttons.Width = 150;
                buttons.UseColumnTextForButtonValue = true;
                buttons.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                buttons.FlatStyle = FlatStyle.Popup;
                //buttons.DisplayIndex = (int)ColumnasGrid.Detalle;
            }
            dgvPresupuesto.Columns.Add(buttons);

            dgvPresupuesto.Columns[(int)ColumnasGrid.CuentasDeGasto].HeaderText = "Cuenta de Gasto";
            dgvPresupuesto.Columns[(int)ColumnasGrid.CuentasDeGasto].Width = 105;
            dgvPresupuesto.Columns[(int)ColumnasGrid.CuentasDeGasto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPresupuesto.Columns[(int)ColumnasGrid.DescripcionCuenta].HeaderText = "Descripción de Cuenta";
            dgvPresupuesto.Columns[(int)ColumnasGrid.DescripcionCuenta].Width = 310;

            dgvPresupuesto.Columns[(int)ColumnasGrid.TipoDeGasto].HeaderText = "Tipo de Gasto";
            dgvPresupuesto.Columns[(int)ColumnasGrid.TipoDeGasto].Width = 135;
            dgvPresupuesto.Columns[(int)ColumnasGrid.TipoDeGasto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPresupuesto.Columns[(int)ColumnasGrid.Presupuestado].HeaderText = "Monto Presupuestado";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Presupuestado].Width = 106;
            dgvPresupuesto.Columns[(int)ColumnasGrid.Presupuestado].DefaultCellStyle.Format = "c";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Presupuestado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPresupuesto.Columns[(int)ColumnasGrid.Facturado].HeaderText = "Facturado";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Facturado].Width = 106;
            dgvPresupuesto.Columns[(int)ColumnasGrid.Facturado].DefaultCellStyle.Format = "c";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Facturado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPresupuesto.Columns[(int)ColumnasGrid.Remisiones].HeaderText = "Remisiones";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Remisiones].Width = 106;
            dgvPresupuesto.Columns[(int)ColumnasGrid.Remisiones].DefaultCellStyle.Format = "c";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Remisiones].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPresupuesto.Columns[(int)ColumnasGrid.Utilizado].HeaderText = "Gasto Realizado";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Utilizado].Width = 106;
            dgvPresupuesto.Columns[(int)ColumnasGrid.Utilizado].DefaultCellStyle.Format = "c";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Utilizado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPresupuesto.Columns[(int)ColumnasGrid.Porcentaje].HeaderText = "% Diferencia de Gasto";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Porcentaje].Width = 107;
            dgvPresupuesto.Columns[(int)ColumnasGrid.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPresupuesto.Columns[(int)ColumnasGrid.Desviación].HeaderText = "Diferencia de Gasto";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Desviación].Width = 107;
            dgvPresupuesto.Columns[(int)ColumnasGrid.Desviación].DefaultCellStyle.Format = "c";
            dgvPresupuesto.Columns[(int)ColumnasGrid.Desviación].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPresupuesto.Columns[(int)ColumnasGrid.Comentarios].Visible = false; ;

            dgvPresupuesto.AllowUserToAddRows = false;
            dgvPresupuesto.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgvPresupuesto.Rows[dgvPresupuesto.Rows.Count -1].Cells[(int)ColumnasGrid.Detalle] = new DataGridViewTextBoxCell();
            //dgvPresupuesto.Rows[dgvPresupuesto.Rows.Count - 1].ReadOnly = true;

            //dgvPresupuesto.Columns[(int)ColumnasGrid.CuentasDeGasto].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.DescripcionCuenta].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.Presupuestado].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.TipoDeGasto].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.Utilizado].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.Porcentaje].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.Desviación].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.Comentarios].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.Facturado].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvPresupuesto.Columns[(int)ColumnasGrid.Remisiones].SortMode = DataGridViewColumnSortMode.NotSortable;

            foreach (DataGridViewRow row in dgvPresupuesto.Rows)
            {
                if ("" != Convert.ToString(row.Cells[(int)ColumnasGrid.Comentarios].Value))
                {
                    row.Cells[(int)ColumnasGrid.BtnComentarios].ToolTipText = Convert.ToString(row.Cells[(int)ColumnasGrid.Comentarios].Value);
                }
                else
                    row.Cells[(int)ColumnasGrid.BtnComentarios].ToolTipText = "Vacio";
            }

        }

        /// <summary>
        /// Función que carga todas las cuentas de gasto en el clbCuentas
        /// </summary>
        private void CargarCuentas()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.Parameters.AddWithValue("@TipoConsulta", 9);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", RolUsuario);
            command.CommandType = CommandType.StoredProcedure;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["CuentaNombre"] = "TODAS";
            row["AcctCode"] = "0";
            table.Rows.InsertAt(row, 0);

            clbCuentas.DataSource = table;
            clbCuentas.DisplayMember = "CuentaNombre";
            clbCuentas.ValueMember = "AcctCode";
        }

        /// <summary>
        /// Función que muestra encabezados del DataGridView
        /// </summary>
        private void MostrarEncabezados()
        {
            DataGridViewColumn header;
            header = new DataGridViewColumn();
            header.HeaderText = "Cuenta de Gasto";
            header.Width = 130;
            dgvPresupuesto.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Descripción de Cuenta";
            header.Width = 350;
            dgvPresupuesto.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Monto Presupuestado";
            header.Width = 130;
            dgvPresupuesto.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Gasto Realizado";
            header.Width = 130;
            dgvPresupuesto.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "% Diferencia de Gasto";
            header.Width = 130;
            dgvPresupuesto.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Diferencia de Gasto";
            header.Width = 130;
            dgvPresupuesto.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Comentarios";
            dgvPresupuesto.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Detalle";
            dgvPresupuesto.Columns.Add(header);
        }


        ///<sumary>
        ///F
        ///</sumary> 
        public void Presentar()
        {
            try
            {
                if (cbSucursal.SelectedIndex != -1)
                {
                    dgvPresupuesto.Columns.Clear();
                    dgvPresupuesto.DataSource = null;

                    dgvCOM.DataSource = null;
                    dgvTotales.DataSource = null;

                    StringBuilder stbCuentas = new StringBuilder();

                    foreach (DataRowView item in clbCuentas.CheckedItems)
                    {
                        if (item["AcctCode"].ToString() != "0")
                        {
                            if (!clbCuentas.ToString().Equals(string.Empty))
                            {
                                stbCuentas.Append(",");
                            }
                            stbCuentas.Append(item["AcctCode"].ToString());
                        }
                    }
                    if (clbCuentas.CheckedItems.Count == 0)
                    {
                        foreach (DataRowView item in clbCuentas.Items)
                        {
                            if (item["AcctCode"].ToString() != "0")
                            {
                                if (!clbCuentas.ToString().Equals(string.Empty))
                                {
                                    stbCuentas.Append(",");
                                }
                                stbCuentas.Append(item["AcctCode"].ToString());
                            }
                        }
                    }


                    Cuenta = stbCuentas.ToString();


                    Sucursal = cbSucursal.Text;
                    DateTime di = dtpFechaInicial.Value;
                    DateTime df = dtpFechaFinal.Value;
                    string Mes = "";
                    int año = 0;
                    int año1 = 0;

                    if (!String.IsNullOrEmpty(cmbMes.Text))
                    {
                        if (!String.IsNullOrEmpty(txtAño.Text))
                        {
                            try
                            {
                                Mes = cmbMes.Text;
                                año = int.Parse(txtAño.Text);
                                año1 = int.Parse(txtAño1.Text);

                                try
                                {
                                    DateTime fecha = Convert.ToDateTime("01/01/" + año);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                            catch (Exception) { }

                            FechaInicial = DateTime.Parse("01/" + getMes(cbMes1.Text) + "/" + año1).ToShortDateString();
                            FechaFinal = DateTime.Parse("01/" + getMes(cmbMes.Text) + "/" + año).AddMonths(1).AddDays(-1).ToShortDateString();
                            //FechaFinal = DateTime.Parse(FechaInicial).AddMonths(1).AddDays(-1).ToShortDateString();

                            if (DateTime.Parse(FechaInicial) <= DateTime.Parse(FechaFinal))
                            {
                                

                                if (DateTime.Parse(FechaInicial).Month < DateTime.Parse(FechaFinal).Month)
                                    Editar = false;
                                else
                                    Editar = true;

                                SqlCommand command = new SqlCommand("sp_AuditoriaGasto", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", 2);
                                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                                command.Parameters.AddWithValue("@Cuenta", Cuenta);
                                command.Parameters.AddWithValue("@Inicial", FechaInicial);
                                command.Parameters.AddWithValue("@Final", FechaFinal);

                                //SqlCommand command = new SqlCommand("PJ_PresupuestoCuenta", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                                //command.CommandType = CommandType.StoredProcedure;
                                //command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.ConsultaGeneral);
                                //command.Parameters.AddWithValue("@Sucursal", Sucursal);
                                //command.Parameters.AddWithValue("@Cuenta", Cuenta);
                                //command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                                //command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                                //command.Parameters.AddWithValue("@Comentario", string.Empty);
                                //command.Parameters.AddWithValue("@Rol", RolUsuario);

                                command.CommandTimeout = 0;
                                DataTable table = new DataTable();
                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command;
                                adapter.SelectCommand.CommandTimeout = 0;
                               
                                adapter.Fill(table);
                                DtsCuentas = table.Copy();
                                
                                this.COM();
                                this.CalcularTotales(table);

                                this.DarFormatoADataGrid();
                            }
                            else
                            {
                                eProvider.SetError(txtAño, "Rango de fechas incorrecto");
                            }
                        }
                        else
                            MessageBox.Show("Ingrese un año.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                        MessageBox.Show("Seleccione un mes.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Seleccione una Sucursal", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalcularTotales(DataTable table)
        {
            LimpiarCampos();
            double totalColPresup = 0;
            double totalColRemisiones = 0;
            double totalColFacturas = 0;
            double totalColUtilizado = 0;
            double totalColDesviacion = 0;

            double totalPresupuestoFijo = 0;
            double totalFacturadoFijo = 0;
            double totalRemisionesFijo = 0;
            double totalUtilizadoFijo = 0;
            double totalDesviacionFijo = 0;

            double totalPresupuestoForzoso = 0;
            double totalFacturadoForzoso = 0;
            double totalRemisionesForzoso = 0;
            double totalUtilizadoForzoso = 0;
            double totalDesviacionForzoso = 0;

            double totalPresupuestoOpcional = 0;
            double totalFacturadoOpcional = 0;
            double totalRemisionesOpcional = 0;
            double totalUtilizadoOpcional = 0;
            double totalDesviacionOpcional = 0;

            foreach (DataRow row in table.Rows)
            {
                switch (row.Field<string>("FrgnName"))
                {
                    case "Fijo":
                        totalPresupuestoFijo += row.Field<double>("Presupuesto");
                        totalFacturadoFijo += row.Field<double>("Facturado");
                        totalRemisionesFijo += row.Field<double>("Remisiones");
                        totalUtilizadoFijo += row.Field<double>("Utilizado");
                        totalDesviacionFijo += row.Field<double>("Desviacion");
                        break;
                    case "Variable Forzoso":
                        totalPresupuestoForzoso += row.Field<double>("Presupuesto");
                        totalRemisionesForzoso += row.Field<double>("Remisiones");
                        totalFacturadoForzoso += row.Field<double>("Facturado");
                        totalUtilizadoForzoso += row.Field<double>("Utilizado");
                        totalDesviacionForzoso += row.Field<double>("Desviacion");
                        break;
                    case "Variable Opcional":
                        totalPresupuestoOpcional += row.Field<double>("Presupuesto");
                        totalFacturadoOpcional += row.Field<double>("Facturado");
                        totalRemisionesOpcional += row.Field<double>("Remisiones");
                        totalUtilizadoOpcional += row.Field<double>("Utilizado");
                        totalDesviacionOpcional += row.Field<double>("Desviacion");
                        break;
                }

                if (row.Field<string>("Porcentaje") == null && row.Field<double>("Utilizado") != 0)
                {
                    row["Porcentaje"] = "Sobre girado";
                }
                else
                {
                    if (row.Field<string>("Porcentaje") == null && row.Field<double>("Utilizado") == 0)
                    {
                        row["Porcentaje"] = "0%";
                    }
                    else
                    {
                        if (row.Field<string>("Porcentaje").Substring(row.Field<string>("Porcentaje").Length - 1, 1) == "%")
                            row["Porcentaje"] = row.Field<string>("Porcentaje");
                        else
                            row["Porcentaje"] = row["Porcentaje"] + "%";
                    }
                }
                totalColPresup += row.Field<double>("Presupuesto");
                totalColRemisiones += row.Field<double>("Remisiones");
                totalColFacturas += row.Field<double>("Facturado");
                totalColUtilizado += row.Field<double>("Utilizado");
                totalColDesviacion += row.Field<double>("Desviacion");
            }
            DataRow rowTotales = table.NewRow();
            rowTotales["U_C_gasto"] = "Total General";
            rowTotales["AcctName"] = string.Empty;
            rowTotales["FrgnName"] = string.Empty;
            rowTotales["Presupuesto"] = totalColPresup;
            rowTotales["Remisiones"] = totalColRemisiones;
            rowTotales["Facturado"] = totalColFacturas;
           
            rowTotales["Utilizado"] = totalColUtilizado;
            if (totalColPresup != 0)
            {
                rowTotales["Porcentaje"] = Convert.ToInt32((totalColUtilizado * 100) / totalColPresup).ToString() + "%";
            }
            else
            {
                if (totalColPresup == 0 && totalColUtilizado == 0)
                {
                    rowTotales["Porcentaje"] = "0%";
                }
                else
                {
                    if (totalColPresup == 0 && totalColUtilizado != 0)
                    {
                        rowTotales["Porcentaje"] = "Sobre girado";
                    }
                }
            }
            rowTotales["Desviacion"] = totalColDesviacion;
            table.Rows.Add(rowTotales);
            dgvPresupuesto.DataSource = table;
          //  DtsCuentas = table;

            //Llenamos los campos de Gastos Fijo, Opcionales o Forzosos   
            DataTable _totales = new DataTable();
            _totales.Columns.Add("A", typeof(string));
            _totales.Columns.Add("B", typeof(string));
            _totales.Columns.Add("TipoGasto", typeof(string));
            _totales.Columns.Add("Presupuesto", typeof(string));
            _totales.Columns.Add("Facturas", typeof(string));
            _totales.Columns.Add("Remisiones", typeof(string));
            _totales.Columns.Add("Gasto", typeof(string));
            _totales.Columns.Add("DiferenciaP", typeof(string));
            _totales.Columns.Add("DiferenciaM", typeof(string));

            DataTable _COM = _totales.Copy();
            dgvTotales.DataSource = _totales;
            dgvCOM.DataSource = _COM;
            lblPresupuestoFijo.Text = totalPresupuestoFijo.ToString("c");
            lblUtilizadoFijo.Text = totalUtilizadoFijo.ToString("c");
            lblDesviacionFijo.Text = totalDesviacionFijo.ToString("c");
            DataRow _rFijo = _totales.NewRow();
            _rFijo["TipoGasto"] = "Fijo";
            _rFijo["Presupuesto"] = totalPresupuestoFijo.ToString("c");
            _rFijo["Facturas"] = totalFacturadoFijo.ToString("c");
            _rFijo["Remisiones"] = totalRemisionesFijo.ToString("c");
            _rFijo["Gasto"] = totalUtilizadoFijo.ToString("c");
            _rFijo["DiferenciaM"] = totalDesviacionFijo.ToString("c");
            _totales.Rows.Add(_rFijo);

            if (totalDesviacionFijo > 0)
            {
                lblDesviacionFijo.ForeColor = Color.Red;
                dgvTotales.Rows[0].Cells[6].Style.ForeColor = Color.Red;
            }
            else
            {
                lblDesviacionFijo.ForeColor = Color.Green;
                dgvTotales.Rows[0].Cells[6].Style.ForeColor = Color.Green;
            }

            if (totalPresupuestoFijo != 0)
            {
                _rFijo["DiferenciaP"] = Convert.ToInt32((totalUtilizadoFijo * 100) / totalPresupuestoFijo).ToString() + "%";
                lblPorcentajeFijo.Text = Convert.ToInt32((totalUtilizadoFijo * 100) / totalPresupuestoFijo).ToString() + "%";
            }
            else
            {
                if (totalPresupuestoFijo == 0 && totalUtilizadoFijo == 0)
                {
                    _rFijo["DiferenciaP"] = "0%";
                    lblPorcentajeFijo.Text = "0%";
                }
                else
                {
                    if (totalPresupuestoFijo == 0 && totalUtilizadoFijo != 0)
                    {
                        _rFijo["DiferenciaP"] = "Sobre girado";
                        lblPorcentajeFijo.Text = "Sobre girado";
                    }
                }
            }

            lblPresupuestoForzoso.Text = totalPresupuestoForzoso.ToString("c");
            lblUtilizadoForzoso.Text = totalUtilizadoForzoso.ToString("c");
            lblDesviacionForzoso.Text = totalDesviacionForzoso.ToString("c");
            DataRow _rForzoso = _totales.NewRow();
            _rForzoso["TipoGasto"] = "Variable Forzozo";
            _rForzoso["Presupuesto"] = totalPresupuestoForzoso.ToString("c");
            _rForzoso["Facturas"] = totalFacturadoForzoso.ToString("c");
            _rForzoso["Remisiones"] = totalRemisionesForzoso.ToString("c");
            _rForzoso["Gasto"] = totalUtilizadoForzoso.ToString("c");
            _rForzoso["DiferenciaM"] = totalDesviacionForzoso.ToString("c");
            _totales.Rows.Add(_rForzoso);

            if (totalDesviacionForzoso > 0)
            {
                lblDesviacionForzoso.ForeColor = Color.Red;
                dgvTotales.Rows[1].Cells[6].Style.ForeColor = Color.Red;
            }
            else
            {
                lblDesviacionForzoso.ForeColor = Color.Green;
                dgvTotales.Rows[1].Cells[6].Style.ForeColor = Color.Green;
            }

            if (totalPresupuestoForzoso != 0)
            {
                _rForzoso["DiferenciaP"] = Convert.ToInt32((totalUtilizadoForzoso * 100) / totalPresupuestoForzoso).ToString() + "%";
                lblPorcentajeForzoso.Text = Convert.ToInt32((totalUtilizadoForzoso * 100) / totalPresupuestoForzoso).ToString() + "%";
            }
            else
            {
                if (totalPresupuestoForzoso == 0 && totalUtilizadoForzoso == 0)
                {
                    _rForzoso["DiferenciaP"] = "0%";
                    lblPorcentajeForzoso.Text = "0%";
                }
                else
                {
                    if (totalPresupuestoForzoso == 0 && totalUtilizadoForzoso != 0)
                    {
                        _rForzoso["DiferenciaP"] = "Sobre girado";
                        lblPorcentajeForzoso.Text = "Sobre girado";
                    }
                }
            }

            lblPresupuestoOpcional.Text = totalPresupuestoOpcional.ToString("c");
            lblUtilizadoOpcional.Text = totalUtilizadoOpcional.ToString("c");
            lblDesviacionOpcional.Text = totalDesviacionOpcional.ToString("c");
            DataRow _rOpcional = _totales.NewRow();
            _rOpcional["TipoGasto"] = "Variable Opcional";
            _rOpcional["Presupuesto"] = totalPresupuestoOpcional.ToString("c");
            _rOpcional["Facturas"] = totalFacturadoOpcional.ToString("c");
            _rOpcional["Remisiones"] = totalRemisionesOpcional.ToString("c");
            _rOpcional["Gasto"] = totalUtilizadoOpcional.ToString("c");
            _rOpcional["DiferenciaM"] = totalDesviacionOpcional.ToString("c");
            _totales.Rows.Add(_rOpcional);

            if (totalDesviacionOpcional > 0)
            {
                lblDesviacionOpcional.ForeColor = Color.Red;
                dgvTotales.Rows[2].Cells[6].Style.ForeColor = Color.Red;
            }
            else
            {
                lblDesviacionOpcional.ForeColor = Color.Green;
                dgvTotales.Rows[2].Cells[6].Style.ForeColor = Color.Green;
            }

            if (totalPresupuestoOpcional != 0)
            {
                _rOpcional["DiferenciaP"] = Convert.ToInt32((totalUtilizadoOpcional * 100) / totalPresupuestoOpcional).ToString() + "%";
                lblPorcentajeOpcional.Text = Convert.ToInt32((totalUtilizadoOpcional * 100) / totalPresupuestoOpcional).ToString() + "%";
            }
            else
            {
                if (totalPresupuestoOpcional == 0 && totalUtilizadoOpcional == 0)
                {
                    _rOpcional["DiferenciaP"] = "0%";
                    lblPorcentajeOpcional.Text = "0%";
                }
                else
                {
                    if (totalPresupuestoOpcional == 0 && totalUtilizadoOpcional != 0)
                    {
                        _rOpcional["DiferenciaP"] = "Sobregirado";
                        lblPorcentajeOpcional.Text = "Sobre girado";
                    }
                }
            }

            double totalColPres = (totalPresupuestoFijo + totalPresupuestoForzoso + totalPresupuestoOpcional);
            double totalColFac = (totalFacturadoFijo + totalFacturadoForzoso + totalFacturadoOpcional);
            double totalColRem = (totalRemisionesFijo + totalRemisionesForzoso + totalRemisionesOpcional);
            double totalColUti = (totalUtilizadoFijo + totalUtilizadoForzoso + totalUtilizadoOpcional);
            double totalColDes = (totalDesviacionFijo + totalDesviacionForzoso + totalDesviacionOpcional);

            lblTotalPresupuesto.Text = totalColPres.ToString("c");
            lblTotalUtilizado.Text = totalColUti.ToString("c");
            lblTotalDesviacion.Text = totalColDes.ToString("c");
            _totales.Rows.Add(_totales.NewRow());
            DataRow _rTotal = _totales.NewRow();
            _rTotal["TipoGasto"] = "Total";
            _rTotal["Presupuesto"] = totalColPres.ToString("c");
            _rTotal["Facturas"] = totalColFac.ToString("c");
            _rTotal["Remisiones"] = totalColRem.ToString("c");
            _rTotal["Gasto"] = totalColUti.ToString("c");
            _rTotal["DiferenciaM"] = totalColDes.ToString("c");
            _totales.Rows.Add(_rTotal);

            DataRow _rTotalCOM = _COM.NewRow();
            _rTotalCOM["TipoGasto"] = "Total";
            _rTotalCOM["Presupuesto"] = totalColPres.ToString("c");
            _rTotalCOM["Facturas"] = totalColFac.ToString("c");
            _rTotalCOM["Remisiones"] = totalColRem.ToString("c");
            _rTotalCOM["Gasto"] = totalColUti.ToString("c");
            _rTotalCOM["DiferenciaM"] = totalColDes.ToString("c");
            _COM.Rows.Add(_rTotalCOM);
            if (totalColDes > 0)
            {
                lblTotalDesviacion.ForeColor = Color.Red;
                dgvTotales.Rows[4].Cells[6].Style.ForeColor = Color.Red;
                dgvCOM.Rows[0].Cells[6].Style.ForeColor = Color.Red;
            }
            else
            {
                lblTotalDesviacion.ForeColor = Color.Green;
                dgvTotales.Rows[4].Cells[6].Style.ForeColor = Color.Green;
                dgvCOM.Rows[0].Cells[6].Style.ForeColor = Color.Green;
            }

            if (totalColPres != 0)
            {
                _rTotal["DiferenciaP"] = Convert.ToInt32((totalColUti * 100) / totalColPres).ToString() + "%";
                _rTotalCOM["DiferenciaP"] = Convert.ToInt32((totalColUti * 100) / totalColPres).ToString() + "%";
                lblTotalPorcentaje.Text = Convert.ToInt32((totalColUti * 100) / totalColPres).ToString() + "%";
            }
            else
            {
                if (totalColPres == 0 && totalColUti == 0)
                {
                    _rTotal["DiferenciaP"] = "0%";
                    _rTotalCOM["DiferenciaP"] = "0%";
                    lblTotalPorcentaje.Text = "0%";
                }
                else
                {
                    if (totalColPres == 0 && totalColUti != 0)
                    {
                        _rTotalCOM["DiferenciaP"] = "Sobregirado";
                        _rTotal["DiferenciaP"] = "Sobregirado";
                        lblTotalPorcentaje.Text = "Sobre girado";
                    }
                }
            }


            //Total COM

            DataTable _TableCOM = new DataTable("COM");
            _TableCOM = (DataTable)dataGridView2.DataSource;
            DataRow _rCOM = _COM.NewRow();
            _rCOM["TipoGasto"] = "COM";
            _rCOM["Presupuesto"] = Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", "")).ToString("c");
            _rCOM["Facturas"] = Convert.ToDecimal(_TableCOM.Compute("SUM(Facturado)", "")).ToString("c");
            _rCOM["Remisiones"] = Convert.ToDecimal(_TableCOM.Compute("SUM(Remisiones)", "")).ToString("c");
            _rCOM["Gasto"] = Convert.ToDecimal(_TableCOM.Compute("SUM(Utilizado)", "")).ToString("c");
            _COM.Rows.Add(_rCOM);
            _COM.Rows.Add(_COM.NewRow());
            DataRow _totalSinCom = _COM.NewRow();
            _totalSinCom["TipoGasto"] = "Total - COM";
            _totalSinCom["Presupuesto"] = ((decimal)totalColPres - Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", ""))).ToString("c");
            _totalSinCom["Facturas"] = ((decimal)totalColFac - Convert.ToDecimal(_TableCOM.Compute("SUM(Facturado)", ""))).ToString("c");
            _totalSinCom["Remisiones"] = ((decimal)totalColRem - Convert.ToDecimal(_TableCOM.Compute("SUM(Remisiones)", ""))).ToString("c");
            _totalSinCom["Gasto"] = ((decimal)totalColUti - Convert.ToDecimal(_TableCOM.Compute("SUM(Utilizado)", ""))).ToString("c");
            _totalSinCom["DiferenciaM"] = (((decimal)totalColUti - Convert.ToDecimal(_TableCOM.Compute("SUM(Utilizado)", ""))) - ((decimal)totalColPres - Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", "")))).ToString("c");

            if (((decimal)totalColPres - Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", ""))) != 0)
            {
                _totalSinCom["DiferenciaP"] = (((decimal)totalColUti - Convert.ToDecimal(_TableCOM.Compute("SUM(Utilizado)", ""))) / ((decimal)totalColPres - Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", "")))).ToString("P0");
                _COM.Rows.Add(_totalSinCom);
            }
            if (((decimal)totalColPres - Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", ""))) == 0 && (decimal)totalColUti - Convert.ToDecimal(_TableCOM.Compute("SUM(Utilizado)", "")) == 0)
            {
                _totalSinCom["DiferenciaP"] = "0%";
            }
            else
                if (((decimal)totalColPres - Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", ""))) == 0 && (decimal)totalColUti - Convert.ToDecimal(_TableCOM.Compute("SUM(Utilizado)", "")) != 0)
                {
                    _totalSinCom["DiferenciaP"] = "Sobre girado";
                }

            if (((decimal)totalColPres - Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", ""))) != 0)
                if ((((decimal)totalColUti - Convert.ToDecimal(_TableCOM.Compute("SUM(Utilizado)", ""))) / ((decimal)totalColPres - Convert.ToDecimal(_TableCOM.Compute("SUM(Presupuesto)", "")))) > 0)
                {
                    //rojo
                    dgvCOM.Rows[3].Cells[6].Style.ForeColor = Color.Red;
                }
                else
                {
                    //verde
                    dgvCOM.Rows[3].Cells[6].Style.ForeColor = Color.Green;
                }


            try
            {
                dgvCOM.Columns[0].Visible = false;
                dgvCOM.Columns[1].Visible = false;

                dgvCOM.Columns[2].HeaderText = "Tipo de gasto";
                dgvCOM.Columns[3].HeaderText = "Presupuesto";
                dgvCOM.Columns[4].HeaderText = "Facturado";
                dgvCOM.Columns[5].HeaderText = "Remisiones";
                dgvCOM.Columns[6].HeaderText = "Gasto";
                dgvCOM.Columns[7].HeaderText = "Diferencia (%)";
                dgvCOM.Columns[8].HeaderText = "Diferencia ($)";

                dgvCOM.Columns[2].Width = 100;
                dgvCOM.Columns[3].Width = 90;
                dgvCOM.Columns[4].Width = 90;
                dgvCOM.Columns[5].Width = 90;
                dgvCOM.Columns[6].Width = 90;
                dgvCOM.Columns[7].Width = 90;
                dgvCOM.Columns[8].Width = 90;

                dgvTotales.Columns[0].Visible = false;
                dgvTotales.Columns[1].Visible = false;

                dgvTotales.Columns[2].HeaderText = "Tipo de gasto";
                dgvTotales.Columns[3].HeaderText = "Presupuesto";
                dgvTotales.Columns[4].HeaderText = "Facturado";
                dgvTotales.Columns[5].HeaderText = "Remisiones";
                dgvTotales.Columns[6].HeaderText = "Gasto";
                dgvTotales.Columns[7].HeaderText = "Diferencia (%)";
                dgvTotales.Columns[8].HeaderText = "Diferencia ($)";

                dgvTotales.Columns[2].Width = 100;
                dgvTotales.Columns[3].Width = 90;
                dgvTotales.Columns[4].Width = 90;
                dgvTotales.Columns[5].Width = 90;
                dgvTotales.Columns[6].Width = 90;
                dgvTotales.Columns[7].Width = 90;
                dgvTotales.Columns[8].Width = 90;

                foreach (DataGridViewColumn item in dgvTotales.Columns)
                {
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                foreach (DataGridViewColumn item in dgvCOM.Columns)
                {
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception)
            {
            }
        }
        public void Esperar()
        {
            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        public void Continuar()
        {
            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
            dgvPresupuesto.Cursor = Cursors.Arrow;
        }


        public int getMes(string mes)
        {
            int auxmes = 0;
            switch (mes)
            {
                case "Enero":
                    auxmes = 1;
                    break;
                case "Febrero":
                    auxmes = 2;
                    break;
                case "Marzo":
                    auxmes = 3;
                    break;
                case "Abril":
                    auxmes = 4;
                    break;
                case "Mayo":
                    auxmes = 5;
                    break;
                case "Junio":
                    auxmes = 6;
                    break;
                case "Julio":
                    auxmes = 7;
                    break;
                case "Agosto":
                    auxmes = 8;
                    break;
                case "Septiembre":
                    auxmes = 9;
                    break;
                case "Octubre":
                    auxmes = 10;
                    break;
                case "Noviembre":
                    auxmes = 11;
                    break;
                case "Diciembre":
                    auxmes = 12;
                    break;
            }
            return auxmes;
        }
        #endregion



        private void dgvTotales_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {


                foreach (DataGridViewRow item in ((DataGridView)sender).Rows)
                {
                    if (Convert.ToString(item.Cells["TipoGasto"].Value) == "")
                    {
                        item.DefaultCellStyle.BackColor = Color.Black;
                        item.MinimumHeight = 2;

                        item.Height = 2;
                    }
                    else
                    {
                        item.Height = 20;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void frmPresupuesto_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (rbTodo.Checked)
            {
                try
                {
                    IEnumerable<DataRow> query = from cuenta in DtsCuentas.AsEnumerable()
                                                where cuenta.Field<string>("U_C_gasto") != "Total General"
                                                 orderby cuenta.Field<string>("U_C_gasto") ascending
                                                 select cuenta;
                    if (query.Count() > 0)
                    {


                        dgvPresupuesto.Columns.Clear();
                        dgvPresupuesto.DataSource = null;
                        DataTable cuentas = query.CopyToDataTable<DataRow>();
                        CalcularTotales(cuentas);
                        double totalColPresup = 0;
                        double totalColUtilizado = 0;
                        double totalColDesviacion = 0;
                        foreach (DataRow row in cuentas.Rows)
                        {
                            totalColPresup += row.Field<double>("Presupuesto");
                            totalColUtilizado += row.Field<double>("Utilizado");
                            totalColDesviacion += row.Field<double>("Desviacion");
                        }

                        dgvPresupuesto.DataSource = cuentas;
                        DarFormatoADataGrid();
                    }
                    else
                    {
                        dgvPresupuesto.Columns.Clear();
                        dgvPresupuesto.DataSource = null;
                        MostrarEncabezados();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (rbDefectos.Checked)
            {
                try
                {
                    IEnumerable<DataRow> query = from cuenta in DtsCuentas.AsEnumerable()
                                                 where Convert.ToInt32(cuenta.Field<double>("Desviacion")) < 0 & cuenta.Field<string>("U_C_gasto") != "Total General"
                                                 orderby cuenta.Field<double>("Desviacion") ascending
                                                 select cuenta;
                    if (query.Count() > 0)
                    {


                        dgvPresupuesto.Columns.Clear();
                        dgvPresupuesto.DataSource = null;
                        DataTable cuentas = query.CopyToDataTable<DataRow>();
                        CalcularTotales(cuentas);
                        double totalColPresup = 0;
                        double totalColUtilizado = 0;
                        double totalColDesviacion = 0;
                        foreach (DataRow row in cuentas.Rows)
                        {
                            totalColPresup += row.Field<double>("Presupuesto");
                            totalColUtilizado += row.Field<double>("Utilizado");
                            totalColDesviacion += row.Field<double>("Desviacion");
                        }

                        dgvPresupuesto.DataSource = cuentas;
                        DarFormatoADataGrid();
                    }
                    else
                    {
                        dgvPresupuesto.Columns.Clear();
                        dgvPresupuesto.DataSource = null;
                        MostrarEncabezados();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (rbExcesos.Checked)
            {
                try
                {
                    IEnumerable<DataRow> query = from cuenta in DtsCuentas.AsEnumerable()
                                                 where Convert.ToInt32(cuenta.Field<double>("Desviacion")) > 0 & cuenta.Field<string>("U_C_gasto") != "Total General"
                                                 orderby cuenta.Field<double>("Desviacion") descending
                                                 select cuenta;
                    if (query.Count() > 0)
                    {


                        dgvPresupuesto.Columns.Clear();
                        dgvPresupuesto.DataSource = null;
                        DataTable cuentas = query.CopyToDataTable<DataRow>();
                        CalcularTotales(cuentas);
                        double totalColPresup = 0;
                        double totalColUtilizado = 0;
                        double totalColDesviacion = 0;
                        foreach (DataRow row in cuentas.Rows)
                        {
                            totalColPresup += row.Field<double>("Presupuesto");
                            totalColUtilizado += row.Field<double>("Utilizado");
                            totalColDesviacion += row.Field<double>("Desviacion");
                        }

                        dgvPresupuesto.DataSource = cuentas;
                        DarFormatoADataGrid();
                    }
                    else
                    {
                        dgvPresupuesto.Columns.Clear();
                        dgvPresupuesto.DataSource = null;
                        MostrarEncabezados();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



    }
}
