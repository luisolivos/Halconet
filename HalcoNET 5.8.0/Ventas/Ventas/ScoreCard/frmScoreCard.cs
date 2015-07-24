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
using System.Threading;
using System.Globalization;

namespace Ventas
{
    public partial class ScoreCard : Form
    {
        Clases.Logs log;
        #region PARÁMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        public string NombreUsuario;
        public string Vendedores;
        public string Sucursales;
        public string Cliente;
        public int RolUsuario;
        public string Canales;
        public int CodigoVendedor;
        public string Sucursal;
        public decimal Suma = 0;
        public decimal Objetivo = 0;
        public decimal DiasMes = 0;
        public decimal DiasTranscurridos = 0;
        public decimal DiasRestantes = 0;
        public decimal AvanceOptimo = 0;
        public bool Guardar = false;
        public bool Cerrar = true;
        public DateTime Fecha;

        /// <summary>
        /// Enumerador de los tipos de consulta del ScoreCard
        /// </summary>
        private enum TipoConsulta
        {
            DiasMes = 1,
            DiasTranscurridos = 2,
            Clientes = 10,
            Vendedores = 11,
            Objetivo,
            PresupuestoMensual
        }

        /// <summary>
        /// Enumerador de las columnas del Grid
        /// </summary>
        private enum ColumnasGrid
        {
            ClaveVendedor,
            Vendedor,
            Sucursal,
            VentaDia,
            Acumulado,
            PresupuestoMensual,
            AcumuladoVSCuotaM,
            AcumuladoVSCuotaP,
            VentaRequerida,
            PronosticoFinMesM,
            PronosticoFinMEsP,
            Requerido,
            Orden,
            Utilidad
        }

        /// <summary>
        /// Enumerador de las columnas del grid Detalles
        /// </summary>
        private enum ColumnasGridDetalle
        {
            ClaveVendedor,
            Vendedor,
            ClaveCliente,
            Cliente,
            VentaDia,
            Acumulado,
            PresupuestoMensual,
            AcumuladoVSCuotaM,
            AcumuladoVSCuotaP,
            VentaRequerida,
            PronosticoFinMesM,
            PronosticoFinMEsP,
            Requerido,
            U_Efectividad
        }

        #endregion        


        #region EVENTOS
        
        public ScoreCard(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();

            RolUsuario = rolUsuario;
            NombreUsuario = nombreUsuario;
            CodigoVendedor = codigoVendedor;
            Sucursal = sucursal;
        }

        /// <summary>
        /// Evento que ocurre hacer click en el boton buscar 
        /// </summary>
        /// <param name="sender">Parámetros del evento</param>
        /// <param name="e">Objeto que produce el evento</param>
         DataSet data = new DataSet();
        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                gridVendedores.Columns.Clear();
                gridVendedores.DataSource = null;

                toolStripStatusLabel1.Text = "";
                Guardar = false;

                Fecha = DateTime.Parse(dateTimePicker1.Text);
                Esperar();
                gridTotales.DataSource = null;
                gridVendedores.DataSource = null;
                gridClientes.DataSource = null;
                CargarDias();
                CargarDiasReales();

                if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                {
                    Vendedores = CrearCadena(clbVendedor);
                }
                data.Reset();
                BindingSource masterBindingSource = new BindingSource();
                BindingSource detailsBindingSource = new BindingSource();

                Cliente = "";
                Sucursales = CrearCadena(clbSucursal);

                SqlCommand commandClientes = new SqlCommand("PJ_ScoreCardVentas", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                commandClientes.CommandType = CommandType.StoredProcedure;
                commandClientes.Parameters.AddWithValue("@TipoConsulta", TipoConsulta.Clientes);
                commandClientes.Parameters.AddWithValue("@Cliente", Cliente);
                commandClientes.Parameters.AddWithValue("@Sucursales", Sucursales.Trim(','));
                commandClientes.Parameters.AddWithValue("@Vendedores", Vendedores.Trim(','));
                commandClientes.Parameters.AddWithValue("@Presupuesto", 0);
                commandClientes.Parameters.AddWithValue("@Fecha", Fecha);


                SqlCommand commandVendedores = new SqlCommand("PJ_ScoreCardVentas", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                commandVendedores.CommandType = CommandType.StoredProcedure;
                commandVendedores.Parameters.AddWithValue("@TipoConsulta", TipoConsulta.Vendedores);
                commandVendedores.Parameters.AddWithValue("@Cliente", Cliente);
                commandVendedores.Parameters.AddWithValue("@Sucursales", Sucursales.Trim(','));
                commandVendedores.Parameters.AddWithValue("@Vendedores", Vendedores.Trim(','));
                commandVendedores.Parameters.AddWithValue("@Presupuesto", 0);
                commandVendedores.Parameters.AddWithValue("@Fecha", Fecha);

                SqlDataAdapter adapter1 = new SqlDataAdapter();
                adapter1.SelectCommand = commandClientes;
                adapter1.SelectCommand.CommandTimeout = 0;
                DataTable tblClientes = CrearTablaClientes();
                adapter1.Fill(tblClientes);

                SqlDataAdapter adapter2 = new SqlDataAdapter();
                adapter2.SelectCommand = commandVendedores;
                adapter2.SelectCommand.CommandTimeout = 0;
                DataTable tblVendedores = CrearTablaVendedores();
                adapter2.Fill(tblVendedores);

                if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                {
                    var query = (from item in tblVendedores.AsEnumerable()
                                 select item.Field<string>("Sucursal")).Distinct();
                    int id = 1016;
                    foreach (var item in query.ToList())
                    {
                        DataRow r = tblVendedores.NewRow();
                        r["ClaveVendedor"] = id;
                        r["Vendedor"] = "";
                        r["Sucursal"] = item + " Total";
                        r["Venta del día"] = (from acum in tblVendedores.AsEnumerable()
                                              where acum.Field<string>("Sucursal") == item
                                              select acum.Field<decimal>("Venta del día")).Sum();
                        r["Acumulado"] = (from acum in tblVendedores.AsEnumerable()
                                          where acum.Field<string>("Sucursal") == item
                                          select acum.Field<decimal>("Acumulado")).Sum();
                        r["Presupuesto Mensual"] = (from acum in tblVendedores.AsEnumerable()
                                                    where acum.Field<string>("Sucursal") == item
                                                    select acum.Field<decimal>("Presupuesto Mensual")).Sum();
                        tblVendedores.Rows.Add(r);
                        id++;
                    }

                    tblVendedores = (from tv in tblVendedores.AsEnumerable()
                                     orderby tv.Field<string>("Sucursal")
                                     select tv).CopyToDataTable();
                    tblVendedores.TableName = "TablaVendedores";
                }

                data.Tables.Add(tblClientes);
                data.Tables.Add(tblVendedores);
                //gridVendedores.DataSource = tblVendedores;

                DataRelation relation = new DataRelation("VendedoresCliente", data.Tables["TablaVendedores"].Columns["ClaveVendedor"], data.Tables["TablaClientes"].Columns["ClaveVendedor"]);
                data.Relations.Add(relation);

                masterBindingSource.DataSource = data;
                masterBindingSource.DataMember = "TablaVendedores";
                detailsBindingSource.DataSource = masterBindingSource;
                detailsBindingSource.DataMember = "VendedoresCliente";
                gridVendedores.DataSource = masterBindingSource;
                gridClientes.DataSource = detailsBindingSource;
                DarFormatoGridVendedores();
                DarFormatoGridClientes();

                gridExcel.DataSource = data.Tables["TablaClientes"];

                decimal VentaDia = 0;
                decimal Acumulado = 0;
                decimal PresupuestoMensual = 0;
                decimal AcumuladoVSCuotaM = 0;
                decimal AcumuladoVSCuotaP = 0;
                decimal VentaRequerida = 0;
                decimal PronosticoFinMesM = 0;
                decimal PronosticoFinMesP = 0;
                decimal Requerido = 0;

                foreach (DataGridViewRow item in gridVendedores.Rows)
                {
                    if (Convert.ToString(item.Cells[(int)ColumnasGrid.Vendedor].Value) != "")
                    {
                        VentaDia += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.VentaDia].Value);// item.Field<double>("Venta del día");
                        Acumulado += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Acumulado].Value);
                        PresupuestoMensual += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.PresupuestoMensual].Value);
                        AcumuladoVSCuotaM += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.AcumuladoVSCuotaM].Value);
                        VentaRequerida += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.VentaRequerida].Value);
                        PronosticoFinMesM += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.PronosticoFinMesM].Value);
                        Requerido += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Requerido].Value);
                    }
                }

                if (PresupuestoMensual != 0)
                {
                    AcumuladoVSCuotaP = Acumulado / PresupuestoMensual;
                    PronosticoFinMesP = PronosticoFinMesM / PresupuestoMensual;
                }

                DataTable tblTotales = new DataTable("Totales");
                tblTotales.Columns.Add("Venta del día " + Fecha.ToShortDateString(), typeof(decimal));
                tblTotales.Columns.Add("Acumulado", typeof(decimal));
                tblTotales.Columns.Add("Presupuesto Mensual", typeof(decimal));
                tblTotales.Columns.Add("Acumulado VS Cuota($)", typeof(decimal));
                tblTotales.Columns.Add("Acumulado VS Cuota(%)", typeof(decimal));
                tblTotales.Columns.Add("Venta requerida al día " + Fecha.ToShortDateString(), typeof(decimal));
                tblTotales.Columns.Add("Pronostico fin de mes($)", typeof(decimal));
                tblTotales.Columns.Add("Pronostico fin de mes(%)", typeof(decimal));
                tblTotales.Columns.Add("Requerido por día para cumplir Objetivo", typeof(decimal));

                DataRow registro = tblTotales.NewRow();
                registro["Venta del día " + Fecha.ToShortDateString()] = VentaDia;
                registro["Acumulado"] = Acumulado;
                registro["Presupuesto Mensual"] = PresupuestoMensual;
                registro["Acumulado VS Cuota($)"] = AcumuladoVSCuotaM;
                registro["Acumulado VS Cuota(%)"] = AcumuladoVSCuotaP;
                registro["Venta requerida al día " + Fecha.ToShortDateString()] = VentaRequerida;
                registro["Pronostico fin de mes($)"] = PronosticoFinMesM;
                registro["Pronostico fin de mes(%)"] = PronosticoFinMesP;
                registro["Requerido por día para cumplir Objetivo"] = Requerido;
                tblTotales.Rows.Add(registro);
                gridTotales.DataSource = tblTotales;
                DarFormatoGridTotales();

                if (gridExcel.Rows.Count == 0)
                {
                    btnGuardar.Enabled = false;
                    btnExportar.Enabled = false;
                }
                else
                {
                    btnExportar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lblStatus.Text = ((decimal)0.0).ToString("c");
                Continuar();
            }


        }
        
        /// <summary>
        /// Evento que ocurre al cargarse la página
        /// Llena los controles que van a ser utilizados
        /// </summary>
        /// <param name="sender">Parámetros del evento</param>
        /// <param name="e">Objeto que produce el evento</param>
        private void Facturas_Load(object sender, EventArgs e)
        {
            //this.Text += " 01/" + DateTime.Now.Month + "/" + DateTime.Now.Year + " al " + DateTime.Now.ToShortDateString(); ;
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                CargarVendedores();
                CargarSucursales();
                // CargarDias();
                CargarMensajes();
                Restricciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el checkedlistbox
        /// Selecciona todas o deselecciona todas
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void clbVendedor_Click(object sender, EventArgs e)
        {
            if (clbVendedor.SelectedIndex == 0)
            {
                if (clbVendedor.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbVendedor.Items.Count; item++)
                    {
                        clbVendedor.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbVendedor.Items.Count; item++)
                    {
                        clbVendedor.SetItemChecked(item, true);
                    }
                }
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el checkedlistbox
        /// Selecciona todas o deselecciona todas
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void clbSucursal_Click(object sender, EventArgs e)
        {
            if (clbSucursal.SelectedIndex == 0)
            {
                if (clbSucursal.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbSucursal.Items.Count; item++)
                    {
                        clbSucursal.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbSucursal.Items.Count; item++)
                    {
                        clbSucursal.SetItemChecked(item, true);
                    }
                }
            }
        }

        /// <summary>
        /// Evento que ocurre cuando el valor de una celda de la columna presupuesto mensual cambia
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        int auxVendedor = 0;
        private void gridClientes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)ColumnasGridDetalle.PresupuestoMensual)
            {
                try
                {
                    Guardar = true;
                    this.Text = "HalcoNET - Score card*";
                    btnGuardar.Enabled = true;
                    Suma = 0;
                    if (auxVendedor != Convert.ToInt32(gridClientes.Rows[e.RowIndex].Cells[(int)ColumnasGridDetalle.ClaveVendedor].Value))
                    {
                        auxVendedor = Convert.ToInt32(gridClientes.Rows[e.RowIndex].Cells[(int)ColumnasGridDetalle.ClaveVendedor].Value);
                        SqlCommand commandClientes = new SqlCommand("PJ_ScoreCardEfectividad", conection);
                        commandClientes.CommandType = CommandType.StoredProcedure;
                        commandClientes.Parameters.AddWithValue("@TipoConsulta", TipoConsulta.Objetivo);
                        commandClientes.Parameters.AddWithValue("@Cliente", string.Empty);
                        commandClientes.Parameters.AddWithValue("@Sucursales", string.Empty);
                        commandClientes.Parameters.AddWithValue("@Vendedores", auxVendedor);
                        commandClientes.Parameters.AddWithValue("@Presupuesto", 0);
                        commandClientes.Parameters.AddWithValue("@Fecha", Fecha);
                        conection.Open();
                        SqlDataReader reader = commandClientes.ExecuteReader();
                        if (reader.Read())
                            Objetivo = Convert.ToDecimal(reader[0]);
                    }

                    foreach (DataGridViewRow row in gridClientes.Rows)
                    {
                        Suma += Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.PresupuestoMensual].Value);
                    }


                    toolStripStatusLabel1.Visible = true;
                    btnGuardar.Enabled = true; 
                    toolStripStatusLabel1.Text = "Diferencia: " + (Objetivo - Suma).ToString("c");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conection.Close();
                }
            }

        }

        /// <summary>
        /// Evento que ocurre al hacer click en el botón Guardar
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //Guardar

            if (Decimal.Round(Suma, 2) == Decimal.Round(Objetivo,2))
            {
                Esperar();
                try
                { 
                    conection.Open();
                    foreach (DataGridViewRow item in gridClientes.Rows)
                    {
                        string cliente = item.Cells[(int)ColumnasGridDetalle.ClaveCliente].Value.ToString();
                        string vendedor = item.Cells[(int)ColumnasGridDetalle.ClaveVendedor].Value.ToString();
                        decimal presupuesto = Convert.ToDecimal(item.Cells[(int)ColumnasGridDetalle.PresupuestoMensual].Value);

                        SqlCommand commandClientes = new SqlCommand("PJ_ScoreCardEfectividad", conection);
                        commandClientes.CommandType = CommandType.StoredProcedure;
                        commandClientes.Parameters.AddWithValue("@TipoConsulta", TipoConsulta.PresupuestoMensual);
                        commandClientes.Parameters.AddWithValue("@Cliente", cliente);
                        commandClientes.Parameters.AddWithValue("@Sucursales", string.Empty);
                        commandClientes.Parameters.AddWithValue("@Vendedores", vendedor);
                        commandClientes.Parameters.AddWithValue("@Presupuesto", presupuesto);
                        commandClientes.Parameters.AddWithValue("@Fecha", Fecha);

                        commandClientes.ExecuteNonQuery();
                    }
                    MessageBox.Show("Presupuesto actualizado correctamente", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Guardar = false;
                    this.Text = "HalcoNET - Score card";
                   // btnPresentar_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conection.Close();
                    Continuar();
                }
            }
            //no guardar
            else
            {
                MessageBox.Show("La suma de sus presupuestos mensuales(" + Suma.ToString("c") + ") debe ser igual a su objetivo mensual (" + Objetivo.ToString("c") + ").", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                toolStripStatusLabel1.BackColor = Color.Red;
                toolStripStatusLabel1.ForeColor = Color.White;
                CheckForIllegalCrossThreadCalls = false;
                Thread h = new Thread(CambiarColor);
                h.Start();
                Cerrar = false;
                    
                //h.Join();
                // toolStripStatusLabel1.BackColor = label1.BackColor;
                // toolStripStatusLabel1.ForeColor = label1.ForeColor;
            }
        }

        /// <summary>
        /// Evento que ocurre seleccionar una fecha en el calendario
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void mcFecha_DateChanged(object sender, DateRangeEventArgs e)
        {
            CargarDias();
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                btnPresentar_Click(sender, e);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel ex = new ExportarAExcel();
            if (ex.ExportarCobranza(gridExcel))
                MessageBox.Show("El Archivo se creo con exito.");
        }

        private void gridVendedores_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridVendedores.Rows)
                {
                    //pinta acumulado vs cuota %
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Value) * 100 > 100)
                    {
                        row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Style.BackColor = Color.FromArgb(0, 176, 80);//green
                        row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Value) * 100 >= 90
                        && Convert.ToDecimal(row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Value) * 100 <= 100)
                    {
                        row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Style.BackColor = Color.FromArgb(255, 255, 0);//yellow
                        row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Value) * 100 < 90)
                    {
                        row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Style.BackColor = Color.FromArgb(255, 0, 0);//red
                        row.Cells[(int)ColumnasGrid.AcumuladoVSCuotaP].Style.ForeColor = Color.White;
                    }

                    //pinta pronostico fin de mes %
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Value) * 100 > 100)
                    {
                        row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Style.BackColor = Color.FromArgb(0, 176, 80);//green
                        row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Value) * 100 >= 90
                        && Convert.ToDecimal(row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Value) * 100 <= 100)
                    {
                        row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Style.BackColor = Color.FromArgb(255, 255, 0);//yellow
                        row.Cells[(int)ColumnasGrid.PronosticoFinMesM].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Value) * 100 < 90)
                    {
                        row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Style.BackColor = Color.FromArgb(255, 0, 0);//red
                        row.Cells[(int)ColumnasGrid.PronosticoFinMEsP].Style.ForeColor = Color.White;
                    }   
                 
                    //pinta totales
                    if (Convert.ToString(row.Cells[(int)ColumnasGrid.Vendedor].Value) == "")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(220, 230, 241);
                        row.DefaultCellStyle.Font = new Font(gridVendedores.DefaultCellStyle.Font.FontFamily, (float)9.75, FontStyle.Regular);
                    }
                }
            }
            catch (Exception)
            {
                // MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridClientes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridClientes.Rows)
                {
                    //pinta acumulado vs cuota
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Value) * 100 >= 75)
                    {
                        row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Style.BackColor = Color.FromArgb(0, 176, 80);//green
                        row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Value) * 100 < 75
                        && Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Value) * 100 >= 0)
                    {
                        row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Style.BackColor = Color.FromArgb(255, 255, 0);//yellow
                        row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Value) * 100 <= 0)
                    {
                        row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Style.BackColor = Color.FromArgb(255, 0, 0);//red
                        row.Cells[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Style.ForeColor = Color.White;
                    }

                    //pinta pronostico fin de mes
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Value) * 100 >= 75)
                    {
                        row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Style.BackColor = Color.FromArgb(0, 176, 80);//green
                        row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Value) * 100 < 75
                        && Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Value) * 100 >= 0)
                    {
                        row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Style.BackColor = Color.FromArgb(255, 255, 0);//yellow
                        row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Value) * 100 <= 0)
                    {
                        row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Style.BackColor = Color.FromArgb(255, 0, 0);//red
                        row.Cells[(int)ColumnasGridDetalle.PronosticoFinMEsP].Style.ForeColor = Color.White;
                    }

                    //pinta clientes que no cuentan para el calculo de la efectividad.
                    if (!Convert.ToString(row.Cells[(int)ColumnasGridDetalle.U_Efectividad].Value).Equals("Y"))
                    {
                        row.Cells[(int)ColumnasGridDetalle.Cliente].Style.BackColor = Color.FromArgb(255, 192, 0);//anarillo huevo
                        row.Cells[(int)ColumnasGridDetalle.Cliente].Style.ForeColor = Color.White;
                    }
                }

            }
            catch (Exception)
            {
                // MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScoreCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
            if (Guardar)
            {
                DateTimeFormatInfo fecha = CultureInfo.CurrentCulture.DateTimeFormat;

                string nombreMes = fecha.GetMonthName(Fecha.Month);

                DialogResult dialog = MessageBox.Show("¿Desea guardar los cambios efectuados en Score Card " + nombreMes + "?\r\nSi elige 'No', los cambios hechos se perderán.", "Alerta", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    this.btnGuardar_Click(sender, e);
                    if (!Cerrar)
                        e.Cancel = true;
                    else
                        e.Cancel = false;
                }
                else if (dialog == DialogResult.No)
                {
                    e.Cancel = false;
                }
                else if (dialog == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }

                
            }

        }
        #endregion  


        #region FUNCIONES
        /// <summary>
        /// Método que contiene restricciones para los direfentes roles
        public void Restricciones()
        {
            
            //Rol Ventas Especial
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                lblVendedor.Visible = false;
                clbVendedor.Visible = false;
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                Vendedores = "," + CodigoVendedor.ToString();
                nuevoComentarioToolStripMenuItem.Visible = false;
                btnNComentario.Visible = false;
                //bandejaDeEntradaToolStripMenuItem.Visible = false;
            }
            //Rol Vendedor
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza)
            {
                comentariosToolStripMenuItem.Visible = false;
                nuevoComentarioToolStripMenuItem.Visible = false;
                bandejaDeEntradaToolStripMenuItem.Visible = false;
                btnNComentario.Visible = false;
                btnComentarios.Visible = false;
                btnEfectividad.Visible = false;
            }

        }

        /// <summary>
        /// Método que carga las sucursales en el cbSucursal
        /// </summary>
        private void CargarSucursales()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@SlpCode", 0);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbSucursal.DataSource = table;
                clbSucursal.DisplayMember = "Nombre";
                clbSucursal.ValueMember = "Codigo";
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 12);
                command.Parameters.AddWithValue("@Sucursal", Sucursal.Trim());
                command.Parameters.AddWithValue("@SlpCode", 0);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbSucursal.DataSource = table;
                clbSucursal.DisplayMember = "Nombre";
                clbSucursal.ValueMember = "Codigo";
            }
        }

        /// <summary>
        /// Método que carga los Vendedores en el clbVendedor
        /// </summary>
        private void CargarVendedores()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 11);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
        }       

        ///<sumary>
        /// Método que llena los TextBox Dias
        /// </sumary>
        private void CargarDiasReales()
        {
            SqlCommand command1 = new SqlCommand("PJ_ScoreCardEfectividad", conection);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.AddWithValue("@TipoConsulta", 10);
            command1.Parameters.AddWithValue("@Cliente", string.Empty);
            command1.Parameters.AddWithValue("@Sucursales", string.Empty);
            command1.Parameters.AddWithValue("@Vendedores", string.Empty);
            command1.Parameters.AddWithValue("@Presupuesto", 0);
            command1.Parameters.AddWithValue("@Fecha", Fecha);

            SqlCommand command = new SqlCommand("PJ_ScoreCardEfectividad", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 11);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@Vendedores", string.Empty);
            command.Parameters.AddWithValue("@Presupuesto", 0);
            command.Parameters.AddWithValue("@Fecha", Fecha);

            try
            {
                conection.Open();
                SqlDataReader reader = command1.ExecuteReader();
                if (reader.Read())
                    DiasMes = Convert.ToDecimal(reader[0]);

                SqlDataReader reader1 = command.ExecuteReader();
                if (reader1.Read())
                    DiasTranscurridos = Convert.ToDecimal(reader1[0]);

                DiasRestantes = DiasMes - DiasTranscurridos;
                AvanceOptimo  = DiasTranscurridos / DiasMes;
                txtAvanceOptimo.Text = AvanceOptimo.ToString("P1");
                //txtDiasMes.Text = DiasMes.ToString();
                //txtDiasTranscurridos.Text = DiasTranscurridos.ToString();
                //txtDiasRestantes.Text = DiasRestantes.ToString();
                //txtAvanceOptimo.Text = AvanceOptimo.ToString("P2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }
        }

        ///<sumary>
        /// Método que llena los TextBox Dias
        /// </sumary>
        private void CargarDias()
        {
            Esperar();
            SqlCommand command1 = new SqlCommand("PJ_ScoreCardEfectividad", conection);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.AddWithValue("@TipoConsulta", TipoConsulta.DiasMes);
            command1.Parameters.AddWithValue("@Cliente", string.Empty);
            command1.Parameters.AddWithValue("@Sucursales", string.Empty);
            command1.Parameters.AddWithValue("@Vendedores", string.Empty);
            command1.Parameters.AddWithValue("@Presupuesto", 0);
            command1.Parameters.AddWithValue("@Fecha", Fecha);

            SqlCommand command = new SqlCommand("PJ_ScoreCardEfectividad", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", TipoConsulta.DiasTranscurridos);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@Vendedores", string.Empty);
            command.Parameters.AddWithValue("@Presupuesto", 0);
            command.Parameters.AddWithValue("@Fecha", Fecha);

            try
            {
                conection.Open();
                SqlDataReader reader = command1.ExecuteReader();
                if (reader.Read())
                    DiasMes = Convert.ToDecimal(reader[0]);

                SqlDataReader reader1 = command.ExecuteReader();
                if (reader1.Read())
                    DiasTranscurridos = Convert.ToDecimal(reader1[0]);

                DiasRestantes = DiasMes - DiasTranscurridos;
                AvanceOptimo = DiasTranscurridos / DiasMes;

                txtDiasMes.Text = DiasMes.ToString();
                txtDiasTranscurridos.Text = DiasTranscurridos.ToString();
                txtDiasRestantes.Text = DiasRestantes.ToString();
                txtAvanceOptimo.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
                conection.Close();
            }
        }
        
        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
        /// </sumary>
        private void Esperar()
        {
            
            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }

        /// <sumary> 
        /// Metodos que crea una cadena con el formato ,num1,nm2 etc
        /// La cadena se crea apartir de los elementos seleccionados en un CheckedListBox
        /// Si no se selecciono ningún elemento del CheckListBox se crea una cadena 
        /// que contiene todos los elmentos del mismo
        /// </sumary>
        private string CrearCadena(CheckedListBox clb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in clb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                }
            }
            if (clb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clb.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append(item["Codigo"].ToString());
                    }
                }
            }
            return stb.ToString();
        }

        /// <sumary> 
        /// Metodo que da formato al grid vendedores
        /// </sumary>
        public void DarFormatoGridVendedores()
        {
            DataGridViewButtonColumn botonPCB = new DataGridViewButtonColumn();
            {
                botonPCB.Name = "Utilidad";
                botonPCB.HeaderText = "Utilidad";
                botonPCB.Text = "Utilidad";
                botonPCB.Width = 75;
                botonPCB.UseColumnTextForButtonValue = true;
            }
            gridVendedores.Columns.Add(botonPCB);

            gridVendedores.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridVendedores.Columns[(int)ColumnasGrid.ClaveVendedor].Visible = false;
            gridVendedores.Columns[(int)ColumnasGrid.Orden].Visible = false;

            gridVendedores.Columns[(int)ColumnasGrid.Vendedor].Width = 200;
            //gridVendedores.Columns[(int)ColumnasGrid.Sucursal].Width = 90;
            gridVendedores.Columns[(int)ColumnasGrid.VentaDia].Width = 110;
            gridVendedores.Columns[(int)ColumnasGrid.VentaDia].HeaderText = "Venta del día " + Fecha.ToShortDateString();
            gridVendedores.Columns[(int)ColumnasGrid.Acumulado].Width = 110;
            gridVendedores.Columns[(int)ColumnasGrid.PresupuestoMensual].Width = 110;
            gridVendedores.Columns[(int)ColumnasGrid.AcumuladoVSCuotaM].Width = 110;
            gridVendedores.Columns[(int)ColumnasGrid.AcumuladoVSCuotaP].Width = 110;
            gridVendedores.Columns[(int)ColumnasGrid.VentaRequerida].Width = 110;
            gridVendedores.Columns[(int)ColumnasGrid.VentaRequerida].HeaderText = "Venta requerida al día " + Fecha.ToShortDateString();
            gridVendedores.Columns[(int)ColumnasGrid.PronosticoFinMesM].Width = 110;
            gridVendedores.Columns[(int)ColumnasGrid.PronosticoFinMEsP].Width = 110;
            gridVendedores.Columns[(int)ColumnasGrid.Requerido].Width = 110;

            gridVendedores.Columns[(int)ColumnasGrid.VentaDia].DefaultCellStyle.Format = "C0";
            gridVendedores.Columns[(int)ColumnasGrid.Acumulado].DefaultCellStyle.Format = "C0";
            gridVendedores.Columns[(int)ColumnasGrid.PresupuestoMensual].DefaultCellStyle.Format = "C0";
            gridVendedores.Columns[(int)ColumnasGrid.AcumuladoVSCuotaM].DefaultCellStyle.Format = "C0";
            gridVendedores.Columns[(int)ColumnasGrid.AcumuladoVSCuotaP].DefaultCellStyle.Format = "P1";
            gridVendedores.Columns[(int)ColumnasGrid.VentaRequerida].DefaultCellStyle.Format = "C0";
            gridVendedores.Columns[(int)ColumnasGrid.PronosticoFinMesM].DefaultCellStyle.Format = "C0";
            gridVendedores.Columns[(int)ColumnasGrid.PronosticoFinMEsP].DefaultCellStyle.Format = "P1";
            gridVendedores.Columns[(int)ColumnasGrid.Requerido].DefaultCellStyle.Format = "C0";

            gridVendedores.Columns[(int)ColumnasGrid.VentaDia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridVendedores.Columns[(int)ColumnasGrid.Acumulado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridVendedores.Columns[(int)ColumnasGrid.PresupuestoMensual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridVendedores.Columns[(int)ColumnasGrid.AcumuladoVSCuotaM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridVendedores.Columns[(int)ColumnasGrid.AcumuladoVSCuotaP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridVendedores.Columns[(int)ColumnasGrid.VentaRequerida].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridVendedores.Columns[(int)ColumnasGrid.PronosticoFinMesM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridVendedores.Columns[(int)ColumnasGrid.PronosticoFinMEsP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridVendedores.Columns[(int)ColumnasGrid.Requerido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <sumary> 
        /// Metodo que da formato al grid clientes
        /// </sumary>
        public void DarFormatoGridClientes()
        {
            gridClientes.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;

            gridClientes.ColumnHeadersDefaultCellStyle.BackColor = gridVendedores.ColumnHeadersDefaultCellStyle.BackColor;
            gridClientes.Columns[(int)ColumnasGridDetalle.ClaveVendedor].Visible = false;
            gridClientes.Columns[(int)ColumnasGridDetalle.Vendedor].Visible = false;
            gridClientes.Columns[(int)ColumnasGridDetalle.U_Efectividad].Visible = false;

            gridClientes.Columns[(int)ColumnasGridDetalle.ClaveCliente].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.Cliente].Width = 200;
            gridClientes.Columns[(int)ColumnasGridDetalle.VentaDia].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.VentaDia].HeaderText = "Venta del día " + Fecha.ToShortDateString();
            gridClientes.Columns[(int)ColumnasGridDetalle.Acumulado].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.PresupuestoMensual].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.AcumuladoVSCuotaM].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.VentaRequerida].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.VentaRequerida].HeaderText = "Venta requerida al día " + Fecha.ToShortDateString();
            gridClientes.Columns[(int)ColumnasGridDetalle.PronosticoFinMesM].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.PronosticoFinMEsP].Width = 110;
            gridClientes.Columns[(int)ColumnasGridDetalle.Requerido].Width = 110;


            gridClientes.Columns[(int)ColumnasGridDetalle.VentaDia].DefaultCellStyle.Format = "C0";
            gridClientes.Columns[(int)ColumnasGridDetalle.Acumulado].DefaultCellStyle.Format = "C0";
            gridClientes.Columns[(int)ColumnasGridDetalle.PresupuestoMensual].DefaultCellStyle.Format = "C0";
            gridClientes.Columns[(int)ColumnasGridDetalle.AcumuladoVSCuotaM].DefaultCellStyle.Format = "C0";
            gridClientes.Columns[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].DefaultCellStyle.Format = "P1";
            gridClientes.Columns[(int)ColumnasGridDetalle.VentaRequerida].DefaultCellStyle.Format = "C0";
            gridClientes.Columns[(int)ColumnasGridDetalle.PronosticoFinMesM].DefaultCellStyle.Format = "C0";
            gridClientes.Columns[(int)ColumnasGridDetalle.PronosticoFinMEsP].DefaultCellStyle.Format = "P1";
            gridClientes.Columns[(int)ColumnasGridDetalle.Requerido].DefaultCellStyle.Format = "C0";

            gridClientes.Columns[(int)ColumnasGridDetalle.VentaDia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridDetalle.Acumulado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridDetalle.PresupuestoMensual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridDetalle.AcumuladoVSCuotaM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridDetalle.VentaRequerida].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridDetalle.PronosticoFinMesM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridDetalle.PronosticoFinMEsP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridDetalle.Requerido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            gridClientes.Columns[(int)ColumnasGridDetalle.ClaveVendedor].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.ClaveCliente].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.Cliente].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.VentaDia].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.Acumulado].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.PresupuestoMensual].ReadOnly = false;
            gridClientes.Columns[(int)ColumnasGridDetalle.AcumuladoVSCuotaM].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.AcumuladoVSCuotaP].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.VentaRequerida].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.PronosticoFinMesM].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.PronosticoFinMEsP].ReadOnly = true;
            gridClientes.Columns[(int)ColumnasGridDetalle.Requerido].ReadOnly = true;

            gridClientes.Columns[(int)ColumnasGridDetalle.PresupuestoMensual].DefaultCellStyle.BackColor = Color.FromArgb(220,230,241);

        }

        ///<summary>
        ///Metodo que da formato al grid totales
        /// </summary>
        public void DarFormatoGridTotales()
        {
            gridTotales.Columns[0].DefaultCellStyle.Format = "C0";
            gridTotales.Columns[1].DefaultCellStyle.Format = "C0";
            gridTotales.Columns[2].DefaultCellStyle.Format = "C0";
            gridTotales.Columns[3].DefaultCellStyle.Format = "C0";
            gridTotales.Columns[4].DefaultCellStyle.Format = "P1";
            gridTotales.Columns[5].DefaultCellStyle.Format = "C0";
            gridTotales.Columns[6].DefaultCellStyle.Format = "C0";
            gridTotales.Columns[7].DefaultCellStyle.Format = "P1";
            gridTotales.Columns[8].DefaultCellStyle.Format = "C0";

            //gridTotales.Columns[0].HeaderText = "Venta del día \r\n" + dateTimePicker1.Value.ToShortDateString();
            //gridTotales.Columns[1].HeaderText = "Acumulado";
            //gridTotales.Columns[2].HeaderText = "Presupuesto \r\n mensual";
            //gridTotales.Columns[3].HeaderText = "C0";
            //gridTotales.Columns[4].HeaderText = "P0";
            //gridTotales.Columns[5].HeaderText = "C0";
            //gridTotales.Columns[6].HeaderText = "C0";
            //gridTotales.Columns[7].HeaderText = "P2";
            //gridTotales.Columns[8].HeaderText = "C0";
        }
        /// <sumary> 
        /// Metodos que cambia etiqueta a la etiqueta que muestra la 
        /// diferencia entre el presupuesto mensual y la suma de los
        /// presupuesos por cliente
        /// </sumary>
        private void CambiarColor()
        {
            for (int i = 0; i <= 10; i++)
            {

                if (toolStripStatusLabel1.BackColor == Color.Red)
                {
                    toolStripStatusLabel1.BackColor = label1.BackColor;
                    toolStripStatusLabel1.ForeColor = label1.ForeColor;
                }
                else
                {
                    toolStripStatusLabel1.BackColor = Color.Red;
                    toolStripStatusLabel1.ForeColor = Color.White;
                }
                Thread.Sleep(500);
            }
        }

        /// <sumary> 
        /// Metodos que cambia etiqueta a la etiqueta que muestra la 
        /// diferencia entre el presupuesto mensual y la suma de los
        /// presupuesos por cliente
        /// </sumary>
        private void CambiarColorMensajes()
        {
            for (int i = 0; i <= 100; i++)
            {

                if (lblMensajes.BackColor == Color.Red)
                {
                    lblMensajes.BackColor = label1.BackColor;
                    lblMensajes.ForeColor = label1.ForeColor;
                }
                else
                {
                    lblMensajes.BackColor = Color.Red;
                    lblMensajes.ForeColor = Color.White;
                }
                Thread.Sleep(500);
            }
        }

        /// <sumary> 
        /// Metodo que crea un Datatable donde se almacenara la informacion de ventas x cliente
        /// <return>DataTable</return>
        /// </sumary>
        private DataTable CrearTablaClientes()
        {
            DataTable t = new DataTable("TablaClientes");
            t.Columns.Add("ClaveVendedor", typeof(Int16));
            t.Columns.Add("Vendedor", typeof(string));
            t.Columns.Add("ClaveCliente", typeof(string));
            t.Columns.Add("Cliente", typeof(string));
            t.Columns.Add("Venta del día", typeof(decimal));
            t.Columns.Add("Acumulado", typeof(decimal));
            t.Columns.Add("Presupuesto Mensual", typeof(decimal));
            t.Columns.Add("Acumulado VS Cuota($)", typeof(decimal), "ISNULL(Acumulado - [Presupuesto Mensual],0)");
            t.Columns.Add("Acumulado VS Cuota(%)", typeof(decimal), "IIF([Presupuesto Mensual] > 0, Acumulado / [Presupuesto Mensual], IIF(Acumulado <= 0,0,1))");//"ISNULL(Acumulado / IIF([Presupuesto Mensual] = 0, 1, [Presupuesto Mensual]) * IIF([Presupuesto Mensual] = 0, 0, 1),0)");
            t.Columns.Add("Venta requerida al día", typeof(decimal), "ISNULL([Presupuesto Mensual] * " + AvanceOptimo+",0)");
            t.Columns.Add("Pronostico fin de mes($)", typeof(decimal), "ISNULL((Acumulado / IIF("+DiasTranscurridos+"=0, 1, "+DiasTranscurridos+")* IIF("+DiasTranscurridos+"=0,0,1)) * "+ DiasMes+",0)");
            t.Columns.Add("Pronostico fin de mes(%)", typeof(decimal), "IIF([Presupuesto Mensual] > 0,[Pronostico fin de mes($)] / [Presupuesto Mensual], IIF([Pronostico fin de mes($)]<=0,0,1))");//"ISNULL([Pronostico fin de mes($)] / IIF([Presupuesto Mensual]=0,1, [Presupuesto Mensual])*IIF([Presupuesto Mensual]=0,0,1),0)");
            t.Columns.Add("Requerido por día para cumplir Objetivo", typeof(decimal), "ISNULL(([Presupuesto Mensual] - Acumulado) / IIF("+DiasRestantes+"=0,1,"+DiasRestantes+")* IIF("+DiasRestantes+"=0,0,1),0)");
            return t;
        }

        /// <sumary> 
        /// Metodo que crea un Datatable donde se almacenara la informacion de ventas x vendedor
        /// <return>DataTable</return>
        /// </sumary>
        private DataTable CrearTablaVendedores()
        {
            DataTable t = new DataTable("TablaVendedores");
            t.Columns.Add("ClaveVendedor", typeof(Int16));
            t.Columns.Add("Vendedor", typeof(string));
            t.Columns.Add("Sucursal", typeof(string));
            t.Columns.Add("Venta del día", typeof(decimal));
            t.Columns.Add("Acumulado", typeof(decimal));
            t.Columns.Add("Presupuesto Mensual", typeof(decimal));
            t.Columns.Add("Acumulado VS Cuota($)", typeof(decimal), "ISNULL(Acumulado - [Presupuesto Mensual],0)");
            t.Columns.Add("Acumulado VS Cuota(%)", typeof(decimal), "IIF([Presupuesto Mensual] <> 0, Acumulado / [Presupuesto Mensual], IIF(Acumulado <= 0,0,1))");//"ISNULL(Acumulado / IIF([Presupuesto Mensual] = 0, 1, [Presupuesto Mensual]) * IIF([Presupuesto Mensual] = 0, 0, 1),0)");
            t.Columns.Add("Venta requerida al día", typeof(decimal), "ISNULL([Presupuesto Mensual] * " + AvanceOptimo+",0)");
            t.Columns.Add("Pronostico fin de mes($)", typeof(decimal), "ISNULL((Acumulado / IIF(" + DiasTranscurridos + "=0, 1, " + DiasTranscurridos + ")* IIF(" + DiasTranscurridos + "=0,0,1)) * " + DiasMes + ",0)");
            t.Columns.Add("Pronostico fin de mes(%)", typeof(decimal), "IIF([Presupuesto Mensual] <> 0,[Pronostico fin de mes($)] / [Presupuesto Mensual], IIF([Pronostico fin de mes($)]<=0,0,1))");
            t.Columns.Add("Requerido por día para cumplir Objetivo", typeof(decimal), "ISNULL(([Presupuesto Mensual] - Acumulado) / IIF(" + DiasRestantes + "=0,1," + DiasRestantes + ")* IIF(" + DiasRestantes + "=0,0,1),0)");
            return t;
        }

        /// <summary>
        /// Método que carga numero de mensajes nuevos
        /// </summary>
        private void CargarMensajes()
        {
            try
            {
                SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                commandVendedor.CommandType = CommandType.StoredProcedure;
                commandVendedor.Parameters.AddWithValue("@TipoConsulta", 7);
                commandVendedor.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                commandVendedor.Parameters.AddWithValue("@Fecha", DateTime.Now);
                commandVendedor.Parameters.AddWithValue("@Bono", 0);
                commandVendedor.Parameters.AddWithValue("@From", string.Empty);
                commandVendedor.Parameters.AddWithValue("@Mensaje", string.Empty);
                conection.Open();
                Int32 count = 0;
                SqlDataReader r = commandVendedor.ExecuteReader();
                if (r.Read())
                {
                    count = r.GetInt32(0);
                }

                if (count > 0)
                {
                    lblMensajes.Text = "Usted tiene " + count + " nuevos mensajes.";
                    CheckForIllegalCrossThreadCalls = false;
                    Thread h = new Thread(CambiarColorMensajes);
                    h.Start();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                conection.Close();
            }
        }       
        #endregion       

        private void gridTotales_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridTotales.Rows)
                {
                    //pinta pronostico fin de mes %
                    if (Convert.ToDecimal(row.Cells[7].Value) * 100 > 100)
                    {
                        row.Cells[7].Style.BackColor = Color.FromArgb(0, 176, 80);//green
                        row.Cells[7].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[7].Value) * 100 >= 85
                        && Convert.ToDecimal(row.Cells[7].Value) * 100 <= 100)
                    {
                        row.Cells[7].Style.BackColor = Color.FromArgb(255, 255, 0);//yellow
                        row.Cells[7].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[7].Value) * 100 < 85)
                    {
                        row.Cells[7].Style.BackColor = Color.FromArgb(255, 0, 0);//red
                        row.Cells[7].Style.ForeColor = Color.White;
                    }

                    //pinta acumulado vs cuota %
                    if (Convert.ToDecimal(row.Cells[4].Value)  >= AvanceOptimo )
                    {
                        row.Cells[4].Style.BackColor = Color.FromArgb(0, 176, 80);//green
                        row.Cells[4].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[4].Value)  >= AvanceOptimo - (AvanceOptimo * (decimal)0.1)
                        && Convert.ToDecimal(row.Cells[4].Value)  <= AvanceOptimo )
                    {
                        row.Cells[4].Style.BackColor = Color.FromArgb(255, 255, 0);//yellow
                        row.Cells[4].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[4].Value)  < AvanceOptimo - (AvanceOptimo * (decimal)0.1))
                    {
                        row.Cells[4].Style.BackColor = Color.FromArgb(255, 0, 0);//red
                        row.Cells[4].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    
        private void efectividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.frmEfectividad frmEfectividad = new Ventas.ScoreCard.frmEfectividad(data.Tables["TablaClientes"], data.Tables["TablaVendedores"], "");//gridVendedores.CurrentRow.Cells[(int)ColumnasGrid.Vendedor].Value.ToString()
            frmEfectividad.ShowDialog();
        }

        private void nuevoComentarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.Comentarios.NuevoComentario formNC = new Ventas.ScoreCard.Comentarios.NuevoComentario(RolUsuario, CodigoVendedor, NombreUsuario, Sucursal);
            formNC.MdiParent = this.MdiParent;
            formNC.Show();
        }

        private void bandejaDeEntradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.Comentarios.BandejaMensajes formNC = new Ventas.ScoreCard.Comentarios.BandejaMensajes(RolUsuario, CodigoVendedor, NombreUsuario, Sucursal);
            formNC.MdiParent = this.MdiParent;
            formNC.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.frmEfectividad frmEfectividad = new Ventas.ScoreCard.frmEfectividad(data.Tables["TablaClientes"], data.Tables["TablaVendedores"], "");//gridVendedores.CurrentRow.Cells[(int)ColumnasGrid.Vendedor].Value.ToString()
            frmEfectividad.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.Comentarios.NuevoComentario formNC = new Ventas.ScoreCard.Comentarios.NuevoComentario(RolUsuario, CodigoVendedor, NombreUsuario, Sucursal);
            formNC.MdiParent = this.MdiParent;
            formNC.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.Comentarios.BandejaMensajes formNC = new Ventas.ScoreCard.Comentarios.BandejaMensajes(RolUsuario, CodigoVendedor, NombreUsuario, Sucursal);
            formNC.MdiParent = this.MdiParent;
            formNC.Show();
        }

        private void form_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();

                //using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                //{
                //    using (SqlCommand commandClientes = new SqlCommand("PJ_ScoreCardVentas", connection))
                //    {
                //        commandClientes.CommandType = CommandType.StoredProcedure;
                //        commandClientes.Parameters.AddWithValue("@TipoConsulta", 7);
                //        commandClientes.Parameters.AddWithValue("@Cliente", string.Empty);
                //        commandClientes.Parameters.AddWithValue("@Sucursales", string.Empty);
                //        commandClientes.Parameters.AddWithValue("@Vendedores", string.Empty);
                //        commandClientes.Parameters.AddWithValue("@Presupuesto", 0);
                //        commandClientes.Parameters.AddWithValue("@Fecha", DateTime.Now);

                //        connection.Open();

                //        int x = commandClientes.ExecuteNonQuery();
                //    }

                //}
               
                

                

            }
            catch (Exception)
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Ventas.ScoreCard.frmWP formulario = new Ventas.ScoreCard.frmWP(RolUsuario, CodigoVendedor, NombreUsuario, Sucursal);
            formulario.MdiParent = this.MdiParent;
            
            formulario.Show();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void gridVendedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)ColumnasGrid.Utilidad)
                {
                    string _sucursal = gridVendedores.Rows[e.RowIndex].Cells[(int)ColumnasGrid.Sucursal].Value.ToString();
                    int _vendedor = Convert.ToInt32(gridVendedores.Rows[e.RowIndex].Cells[(int)ColumnasGrid.ClaveVendedor].Value);
                    if (_sucursal.Contains("Total"))
                    {
                        _sucursal = _sucursal.Substring(0, _sucursal.Length - 6);
                        _vendedor = 0;
                    }

                    if (_sucursal.Contains("Total") & (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                        _sucursal = string.Empty;

                    Ventas.ScoreCard.frmUtilidadScore formulario = new Ventas.ScoreCard.frmUtilidadScore(_vendedor, _sucursal, dateTimePicker1.Value);
                    formulario.ShowDialog();

                }

            }
            catch (Exception)
            {
            }
        }

        
    }
}
