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

namespace Presupuesto
{
    public partial class FormDetalle : Form
    {
        Logs log;
        DataTable DatosDetalle = new DataTable();

        public FormDetalle(string cuenta, string nombreCuenta, string sucursal, string fechaInicial, string fechaFinal, int _rol)
        {
            InitializeComponent();
            
            _Cuenta = cuenta;
            _NombreCuenta = nombreCuenta;
            _Sucursal = sucursal;
            _FechaInicial = fechaInicial;
            _FechaFinal = fechaFinal;
            RolUsuario = _rol;
        }

        #region PARÁMETROS

        public string _Cuenta;
        public string _NombreCuenta;
        public string _Sucursal;
        public string _FechaInicial;
        public string _FechaFinal;
        private DataTable DtsCuenta;
        private int RolUsuario;
        //public SqlConnection conection = new SqlConnection(@"Data Source=GIL-PC\SQLSERVER;Initial Catalog=dbPJNueva;Integrated Security=True");
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        /// <summary>
        /// Enumerador para los tipos de consulta
        /// </summary>
        private enum TipoConsulta
        {
            ConsultaGeneral = 1,
            ConsultaPorCuenta = 2,
        }

        /// <summary>
        /// Enumerador para las columnas de DataGridView
        /// </summary>
        private enum ColumnasGrid
        {
            CuentaDeGasto = 0,
            Proyecto = 1,
            Sucursal = 2,
            CodigoPuesto = 3,
            Puesto = 4,
            Area = 5,
            Placa = 6,
            TipoVehiculo = 7,
            NoTelefono = 8,
            Compañia = 9,
            Presupuesto = 10,
            Utilizado =11,
            Porcentaje =12,
            Desviacion = 13,
            Comentarios = 14,
            Boton = 15
        }

        #endregion       
       

        #region EVENTOS

        /// <summary>
        /// Evento que ocurre al hacer click en el btnExportar
        /// Llama a la clase que realiza el proceso
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void button1_Click(object sender, EventArgs e)
        {
            ExportarAExcel excel = new ExportarAExcel();            
            if (excel.Exportar(dgvDetalle) == true)
            {
                MessageBox.Show("El documento se creó correctamente.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocurrió un error al crear el documento, no se creó el archivo.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al cargarse el Formulario
        /// Llena el DataGridView con los parámetros especificados
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void FormDetalle_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.MaximizeBox = true;
            try
            {
                dgvDetalle.Columns.Clear();
                dgvDetalle.DataSource = null;

                //SqlCommand command = new SqlCommand("PJ_PresupuestoCuenta", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                //command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.ConsultaPorCuenta);
                //command.Parameters.AddWithValue("@Sucursal", _Sucursal);
                //command.Parameters.AddWithValue("@Cuenta", _Cuenta);
                //command.Parameters.AddWithValue("@FechaInicial", _FechaInicial);
                //command.Parameters.AddWithValue("@FechaFinal", _FechaFinal);
                //command.Parameters.AddWithValue("@Comentario", string.Empty);
                //command.Parameters.AddWithValue("@Rol", RolUsuario);
                
                SqlCommand command = new SqlCommand("sp_AuditoriaGasto", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 1);
                command.Parameters.AddWithValue("@Sucursal", _Sucursal);
                command.Parameters.AddWithValue("@Cuenta", _Cuenta);
                command.Parameters.AddWithValue("@Inicial", _FechaInicial);
                command.Parameters.AddWithValue("@Final", _FechaFinal);
                
                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(table);

                DatosDetalle = table.Copy();

                double totalColPresup = 0;
                double totalColUtilizado = 0;
                double totalColDesviacion = 0;
                foreach (DataRow row in table.Rows)
                {
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
                            row["Porcentaje"] = row["Porcentaje"] + "%";
                        }
                    }
                    totalColPresup += row.Field<double>("Presupuesto");
                    totalColUtilizado += row.Field<double>("Utilizado");
                    totalColDesviacion += row.Field<double>("Desviacion");
                }
                DataRow rowTotales = table.NewRow();
                rowTotales["U_C_gasto"] = "Total General";
                rowTotales["U_Proyecto"] = string.Empty;
                rowTotales["U_Sucursal"] = string.Empty;
                rowTotales["U_CPuesto"] = string.Empty;
                rowTotales["U_Nombre"] = string.Empty;
                rowTotales["Presupuesto"] = totalColPresup;
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

                dgvDetalle.DataSource = table;
                DtsCuenta = table;
                DarFormatoADataGrid();

                txtCuenta.Text = _Cuenta;
                txtNombreCuenta.Text = _NombreCuenta;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al completar el enlace de datos en el dgvDetalle
        /// Pinta (verde/rojo) la columna Diferencia de Gasto
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    try
                    {
                        if (Convert.ToInt32(row.Cells["Desviacion"].Value) > 0)
                        {
                            row.Cells["Desviacion"].Style.ForeColor = Color.Red;
                        }
                        else
                        {
                            row.Cells["Desviacion"].Style.ForeColor = Color.Green;
                        }
                    }
                    catch (Exception )
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dgvDetalle.Columns.Clear();
                dgvDetalle.DataSource = null;
                IEnumerable<DataRow> query = from cuenta in DtsCuenta.AsEnumerable()
                                             where Convert.ToInt32(cuenta.Field<double>("Desviacion")) > 0 & cuenta.Field<string>("U_C_gasto") != "Total General"
                                             orderby cuenta.Field<double>("Desviacion")  descending
                                             select cuenta;
          
                if (query.Count() > 0)
                {
                    DataTable cuentas = query.CopyToDataTable<DataRow>();
                    double totalColPresup = 0;
                    double totalColUtilizado = 0;
                    double totalColDesviacion = 0;
                    foreach (DataRow row in cuentas.Rows)
                    {
                        totalColPresup += row.Field<double>("Presupuesto");
                        totalColUtilizado += row.Field<double>("Utilizado");
                        totalColDesviacion += row.Field<double>("Desviacion");
                    }
                    DataRow rowTotales = cuentas.NewRow();
                    rowTotales["U_C_gasto"] = "Total General";
                    rowTotales["U_Proyecto"] = string.Empty;
                    rowTotales["U_Sucursal"] = string.Empty;
                    rowTotales["U_CPuesto"] = string.Empty;
                    rowTotales["U_Nombre"] = string.Empty;
                    rowTotales["Presupuesto"] = totalColPresup;
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
                    cuentas.Rows.Add(rowTotales);
                    dgvDetalle.DataSource = cuentas;
                    DarFormatoADataGrid();
                }
                else
                {
                    dgvDetalle.Columns.Clear();
                    dgvDetalle.DataSource = null;
                    MostrarEncabezados();
                }
                dgvDetalle.Columns[(int)ColumnasGrid.Desviacion].SortMode = DataGridViewColumnSortMode.Automatic;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion       
       

        #region FUNCIONES

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
            dgvDetalle.Columns.Add(buttonComent);

            dgvDetalle.Columns[(int)ColumnasGrid.Comentarios].Visible = false;

            dgvDetalle.Columns[(int)ColumnasGrid.CuentaDeGasto].HeaderText = "Cuenta de Gasto";
            dgvDetalle.Columns[(int)ColumnasGrid.CuentaDeGasto].Width = 105;
            dgvDetalle.Columns[(int)ColumnasGrid.CuentaDeGasto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Proyecto].HeaderText = "Proyecto";
            dgvDetalle.Columns[(int)ColumnasGrid.Proyecto].Width = 90;
            dgvDetalle.Columns[(int)ColumnasGrid.Proyecto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Sucursal].HeaderText = "Norma de reparto";
            dgvDetalle.Columns[(int)ColumnasGrid.Sucursal].Width = 95;
            dgvDetalle.Columns[(int)ColumnasGrid.Sucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.CodigoPuesto].HeaderText = "Codigo de Puesto";
            dgvDetalle.Columns[(int)ColumnasGrid.CodigoPuesto].Width = 110;
            dgvDetalle.Columns[(int)ColumnasGrid.CodigoPuesto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Puesto].HeaderText = "Nombre";
            dgvDetalle.Columns[(int)ColumnasGrid.Puesto].Width = 185;
            dgvDetalle.Columns[(int)ColumnasGrid.Puesto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Placa].HeaderText = "Placa";
            dgvDetalle.Columns[(int)ColumnasGrid.Placa].Width = 95;
            dgvDetalle.Columns[(int)ColumnasGrid.Placa].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Area].HeaderText = "Area";
            dgvDetalle.Columns[(int)ColumnasGrid.Area].Width = 95;
            dgvDetalle.Columns[(int)ColumnasGrid.Area].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.TipoVehiculo].HeaderText = "Tipo de Vehículo";
            dgvDetalle.Columns[(int)ColumnasGrid.TipoVehiculo].Width = 95;
            dgvDetalle.Columns[(int)ColumnasGrid.TipoVehiculo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.NoTelefono].HeaderText = "No. de Teléfono";
            dgvDetalle.Columns[(int)ColumnasGrid.NoTelefono].Width = 95;
            dgvDetalle.Columns[(int)ColumnasGrid.NoTelefono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Compañia].HeaderText = "Compañia";
            dgvDetalle.Columns[(int)ColumnasGrid.Compañia].Width = 95;
            dgvDetalle.Columns[(int)ColumnasGrid.Compañia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Presupuesto].HeaderText = "Presupuestado";
            dgvDetalle.Columns[(int)ColumnasGrid.Presupuesto].Width = 100;
            dgvDetalle.Columns[(int)ColumnasGrid.Presupuesto].DefaultCellStyle.Format = "c";
            dgvDetalle.Columns[(int)ColumnasGrid.Presupuesto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Utilizado].HeaderText = "Utilizado";
            dgvDetalle.Columns[(int)ColumnasGrid.Utilizado].Width = 100;
            dgvDetalle.Columns[(int)ColumnasGrid.Utilizado].DefaultCellStyle.Format = "c";
            dgvDetalle.Columns[(int)ColumnasGrid.Utilizado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Porcentaje].HeaderText = "% Diferencia de Gasto";
            dgvDetalle.Columns[(int)ColumnasGrid.Porcentaje].Width = 100;
            dgvDetalle.Columns[(int)ColumnasGrid.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.Columns[(int)ColumnasGrid.Desviacion].HeaderText = "Diferencia de Gasto";
            dgvDetalle.Columns[(int)ColumnasGrid.Desviacion].Width = 100;
            dgvDetalle.Columns[(int)ColumnasGrid.Desviacion].DefaultCellStyle.Format = "c";
            dgvDetalle.Columns[(int)ColumnasGrid.Desviacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgvDetalle.Columns[(int)ColumnasGrid.CuentaDeGasto].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Proyecto].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Sucursal].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.CodigoPuesto].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Puesto].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Area].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Placa].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.TipoVehiculo].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.NoTelefono].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Compañia].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Presupuesto].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Utilizado].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Porcentaje].SortMode = DataGridViewColumnSortMode.NotSortable;
            //dgvDetalle.Columns[(int)ColumnasGrid.Desviacion].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Función que muestra encabezados del DataGridView
        /// </summary>
        private void MostrarEncabezados()
        {
            DataGridViewColumn header;
            header = new DataGridViewColumn();
            header.HeaderText = "Cuenta de Gasto";
            header.Width = 110;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Proyecto";
            header.Width = 100;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Norma de reparto";
            header.Width = 100;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Código Puesto";
            header.Width = 130;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Nombre";
            header.Width = 185;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Area";
            header.Width = 100;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Placa";
            header.Width = 100;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Tipo de Vehículo";
            header.Width = 100;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "No. de Teléfono";
            header.Width = 100;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Compañia";
            header.Width = 100;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Presupuestado";
            header.Width = 110;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Utilizado";
            header.Width = 110;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "% Diferencia de Gasto";
            header.Width = 110;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Diferencia de Gasto";
            header.Width = 110;
            dgvDetalle.Columns.Add(header);
            header = new DataGridViewColumn();
            header.HeaderText = "Comentarios";
            header.Width = 110;
            dgvDetalle.Columns.Add(header);
        }

        #endregion

        private void FormDetalle_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();

            }
            catch (Exception)
            {
            }
        }

        private void FormDetalle_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
            }

        }

        private void dgvDetalle_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {  
            //if (e.ColumnIndex >= 0 && this.dgvDetalle.Columns[e.ColumnIndex].Name == "btnComentarios" && e.RowIndex >= 0)
            //{
            //    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            //    DataGridViewButtonCell celBoton = this.dgvDetalle.Rows[e.RowIndex].Cells["btnComentarios"] as DataGridViewButtonCell;
            //    Icon icoAtomico;

              
            //    icoAtomico = Properties.Resources.icon_comment;
            //    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
                  

            //    this.dgvDetalle.Rows[e.RowIndex].Height = icoAtomico.Height + 10;
            //    this.dgvDetalle.Columns[e.ColumnIndex].Width = icoAtomico.Width + 10;

            //    e.Handled = true;
            //}

            if (e.ColumnIndex >= 0 && this.dgvDetalle.Columns[e.ColumnIndex].Name == "btnComentarios" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvDetalle.Rows[e.RowIndex].Cells["btnComentarios"] as DataGridViewButtonCell;
                Icon icoAtomico;

                if (this.dgvDetalle.Rows[e.RowIndex].Cells["Comentarios"].Value.ToString() != "")
                {

                    icoAtomico = Properties.Resources.icon_comment;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

                }
                else
                {
                    icoAtomico = Properties.Resources.icon_no_comment;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
                }

                this.dgvDetalle.Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                this.dgvDetalle.Columns[e.ColumnIndex].Width = icoAtomico.Width + 10;

                e.Handled = true;
            }
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && this.dgvDetalle.Columns[e.ColumnIndex].Name == "btnComentarios" && e.RowIndex >= 0)
                {
                    string nombre = dgvDetalle.Rows[e.RowIndex].Cells["U_Nombre"].Value.ToString();
                    string proyecto = dgvDetalle.Rows[e.RowIndex].Cells["U_Proyecto"].Value.ToString();
                    string cuenta = dgvDetalle.Rows[e.RowIndex].Cells["U_C_gasto"].Value.ToString();
                    string nr = dgvDetalle.Rows[e.RowIndex].Cells["U_Sucursal"].Value.ToString();

                    AuditoriaPresupuesto.ComentariosDetalle com = new AuditoriaPresupuesto.ComentariosDetalle(proyecto, cuenta, nombre, _FechaInicial, _FechaFinal, _Sucursal, nr);
                    com.Show();
                    com.MdiParent = this;
                }
            }
            catch (Exception)
            {
            }
        }

        private void rbTodo_Click(object sender, EventArgs e)
        {
            if (rbTodo.Checked)
            {
                try
                {
                    IEnumerable<DataRow> query = from cuenta in DatosDetalle.AsEnumerable()
                                                 where cuenta.Field<string>("U_C_gasto") != "Total General"
                                                 orderby cuenta.Field<string>("U_C_gasto") ascending
                                                 select cuenta;
                    if (query.Count() > 0)
                    {
                        dgvDetalle.Columns.Clear();
                        dgvDetalle.DataSource = null;
                        DataTable cuentas = query.CopyToDataTable<DataRow>();
                        //CalcularTotales(cuentas);
                        double totalColPresup = 0;
                        double totalColUtilizado = 0;
                        double totalColDesviacion = 0;
                        foreach (DataRow row in cuentas.Rows)
                        {
                            totalColPresup += row.Field<double>("Presupuesto");
                            totalColUtilizado += row.Field<double>("Utilizado");
                            totalColDesviacion += row.Field<double>("Desviacion");
                        }

                        dgvDetalle.DataSource = cuentas;
                        DarFormatoADataGrid();
                    }
                    else
                    {
                        dgvDetalle.Columns.Clear();
                        dgvDetalle.DataSource = null;
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
                    IEnumerable<DataRow> query = from cuenta in DatosDetalle.AsEnumerable()
                                                 where Convert.ToInt32(cuenta.Field<double>("Desviacion")) < 0 & cuenta.Field<string>("U_C_gasto") != "Total General"
                                                 orderby cuenta.Field<double>("Desviacion") ascending
                                                 select cuenta;
                    if (query.Count() > 0)
                    {


                        dgvDetalle.Columns.Clear();
                        dgvDetalle.DataSource = null;
                        DataTable cuentas = query.CopyToDataTable<DataRow>();
                        //CalcularTotales(cuentas);
                        double totalColPresup = 0;
                        double totalColUtilizado = 0;
                        double totalColDesviacion = 0;
                        foreach (DataRow row in cuentas.Rows)
                        {
                            totalColPresup += row.Field<double>("Presupuesto");
                            totalColUtilizado += row.Field<double>("Utilizado");
                            totalColDesviacion += row.Field<double>("Desviacion");
                        }

                        dgvDetalle.DataSource = cuentas;
                        DarFormatoADataGrid();
                    }
                    else
                    {
                        dgvDetalle.Columns.Clear();
                        dgvDetalle.DataSource = null;
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
                    IEnumerable<DataRow> query = from cuenta in DatosDetalle.AsEnumerable()
                                                 where Convert.ToInt32(cuenta.Field<double>("Desviacion")) > 0 & cuenta.Field<string>("U_C_gasto") != "Total General"
                                                 orderby cuenta.Field<double>("Desviacion") descending
                                                 select cuenta;
                    if (query.Count() > 0)
                    {


                        dgvDetalle.Columns.Clear();
                        dgvDetalle.DataSource = null;
                        DataTable cuentas = query.CopyToDataTable<DataRow>();
                        //CalcularTotales(cuentas);
                        double totalColPresup = 0;
                        double totalColUtilizado = 0;
                        double totalColDesviacion = 0;
                        foreach (DataRow row in cuentas.Rows)
                        {
                            totalColPresup += row.Field<double>("Presupuesto");
                            totalColUtilizado += row.Field<double>("Utilizado");
                            totalColDesviacion += row.Field<double>("Desviacion");
                        }

                        dgvDetalle.DataSource = cuentas;
                        DarFormatoADataGrid();
                    }
                    else
                    {
                        dgvDetalle.Columns.Clear();
                        dgvDetalle.DataSource = null;
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
