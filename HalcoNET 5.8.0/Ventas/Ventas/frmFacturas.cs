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

namespace Ventas
{
    public partial class Facturas : Form
    {
        Clases.Logs log;
        #region PARÁMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);

        public string NombreUsuario;
        public string Vendedores;
        public string Sucursales;
        public string Lineas;
        public string Cliente;
        public string Factura;
        public string Articulo;
        public decimal PrecioEspecial;
        public string FechaInicial;
        public string FechaFinal;
        public int RolUsuario;
        public string Moneda;
        public string Canales;
        public string LineNum;
        public int CodigoVendedor;
        public string Sucursal;
        public string StringVendedor;
        public string StringSucursal;

        /// <summary>
        /// Enumerador de las columnas del Grid
        /// </summary>
        private enum ColumnasGrid
        {
            FechaFactura ,
            NoFactura,
            Sucursal ,
            Vendedor ,
            Cliente ,
            NombreCliente,
            GranCanal,
            PrecioCompra,
            PrecioVenta,
            PrecioCPP,
            PrecioEspecial,
            PrecioVentaFinal,
            Utilidad
        }

        /// <summary>
        /// Enumerador de las columnas del grid Detalles
        /// </summary>
        private enum ColumnasGridDetalle
        {
            LineNum,
            DocNum,
            DocDate,
            Sucursal,
            SlpName,
            GranCanal,
            CardCode,
            CardName,
            DocRate,
            ItemCode,
            ItemName,
            ItmsGrpName,
            Cantidad,
            Moneda,
            U_PCB,
            PrecioCompra,
            Price,
            Descuento,
            PrecioCPP,
            PrecioEspecial,
            Utilidad,
            Total,
            PrecioVentaMXN,
            PrecioCompraMXN,
            PrecioEspecialMXN,
            PrecioCPPMXN,
            PrecioVentaFinal,
            Boton
        }

        #endregion        


        #region EVENTOS

        public Facturas(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();

            RolUsuario = rolUsuario;
            NombreUsuario = nombreUsuario;
            CodigoVendedor = codigoVendedor;
            //Sucursal = sucursal;
            Sucursal = sucursal;
            //*Modificación EOB 29-Ene-2014, desaparecen campos de la tabla de facturas
            /*if (rolUsuario == (int)Constantes.RolSistemaSGUV.Vendedor)
            {
                lblVendedor.Visible = false;
                clbVendedor.Visible = false;
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                Vendedores = "," + codigoVendedor.ToString();
            }*/
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnExportar
        /// Llama a la clase que realiza el proceso
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            ClasesSGUV.Exportar excel = new ClasesSGUV.Exportar();
            if (excel.ExportarSinFormato(gridExcel) == true)
            {
                MessageBox.Show("El documento se creó correctamente.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocurrió un error al crear el documento, no se creó el archivo.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Size tamano = System.Windows.Forms.SystemInformation.PrimaryMonitorSize;
            
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                this.Restricciones();
                CargarVendedores();
                CargarLinea();
                CargarSucursales();
                CargarGranCanales();

                txtCliente.Clear();
                txtFactura.Clear();
                txtArticulo.Clear();

                txtVenta.Clear();
                txtVolumen.Clear();
                txtUtilidad.Clear();
                txtObjetivo.Clear();
                txtUtiSucursal.Clear();

                button1.Enabled = false;
                gridFacturas.DataSource = null;
                gridDetalles.DataSource = null;

                cbFiltrar.Visible = ClasesSGUV.Login.Rol == 1 || ClasesSGUV.Login.Rol == 2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnPresentar
        /// Ejecuta la consulta deacuerdo a los criterios establecidos
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnPresentar_Click(object sender, EventArgs e)
        {
            this.Esperar();
            try
            {
                txtUtiSucursal.Clear();
                txtObjetivo.Clear();
                ConsultarFacturas();
                StringVendedor = "";
            }
            catch (Exception ex)
            {
                button1.Visible = false;
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (Control item in groupBox1.Controls)
                {
                    if (item.Name == control.Name)
                    {
                        item.Focus();
                        break;
                    }
                }
            }
            finally
            {
                this.Continuar();
                //button1.Visible = true;
                toolTotalSuma.Text = "Listo";
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
        private void clbLinea_Click(object sender, EventArgs e)
        {
            if (clbLinea.SelectedIndex == 0)
            {
                if (clbLinea.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbLinea.Items.Count; item++)
                    {
                        clbLinea.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbLinea.Items.Count; item++)
                    {
                        clbLinea.SetItemChecked(item, true);
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
        private void clbGranCanal_Click(object sender, EventArgs e)
        {
            if (clbCanal.SelectedIndex == 0)
            {
                if (clbCanal.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbCanal.Items.Count; item++)
                    {
                        clbCanal.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbCanal.Items.Count; item++)
                    {
                        clbCanal.SetItemChecked(item, true);
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
        /// Evento que ocurre al completar el enlace de datos en el gridFacturas
        /// Pinta (rojo) la columna Precio Venta si cumple con requisitos
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridFacturas.Rows)
                {
                    if (Convert.ToInt32(row.Cells[(int)ColumnasGrid.PrecioEspecial].Value) == 0)
                    {
                        row.Cells[(int)ColumnasGrid.PrecioEspecial].Style.ForeColor = Color.Red;
                    }
                    /////////////////////
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Utilidad].Value) < (decimal)0.13 && Convert.ToString(row.Cells[(int)ColumnasGrid.GranCanal].Value) == "Mayoreo")
                    {
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.BackColor = Color.Yellow;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Utilidad].Value) < (decimal)0.16 && Convert.ToString(row.Cells[(int)ColumnasGrid.GranCanal].Value) == "Transporte")
                        {
                            row.Cells[(int)ColumnasGrid.Utilidad].Style.ForeColor = Color.Black;
                            row.Cells[(int)ColumnasGrid.Utilidad].Style.BackColor = Color.Yellow;
                        }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Utilidad].Value) < (decimal)0.13 && Convert.ToString(row.Cells[(int)ColumnasGrid.GranCanal].Value) == "Armadores")
                    {
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.BackColor = Color.Yellow;
                    }
                    ////////////////////
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Utilidad].Value) <= 0)
                    {
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.BackColor = Color.Red;
                    }
                }

            }
            catch (Exception)
            {
                // MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_KeyPress( object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                this.btnPresentar_Click(sender, e);
                control = (Control)sender;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                Facturas_Load(sender, e);
            }

        }

        private void gridDetalles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridDetalles.Rows)
                {
                    if (Convert.ToInt32(row.Cells[(int)ColumnasGridDetalle.PrecioEspecial].Value) == 0)
                    {
                        row.Cells[(int)ColumnasGridDetalle.PrecioEspecial].Style.ForeColor = Color.Red;
                    }
                   
                    DataGridViewRow selecionada = gridFacturas.CurrentRow;
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < (decimal)0.13 && Convert.ToString(selecionada.Cells[(int)ColumnasGrid.GranCanal].Value) == "Mayoreo")
                    {
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Yellow;
                    }
                    else
                        if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < (decimal)0.16 && Convert.ToString(selecionada.Cells[(int)ColumnasGrid.GranCanal].Value) == "Transporte")
                        {
                            row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
                            row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Yellow;
                        }
                        else
                            if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < (decimal)0.13 && Convert.ToString(selecionada.Cells[(int)ColumnasGrid.GranCanal].Value) == "Armadores")
                            {
                                row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
                                row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Yellow;
                            }
       
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) <= decimal.Zero)
                    {
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.White;
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Red;
                    }
                }

            }
            catch (Exception)
            {
                // MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Esperar();
            ActualizarFactura();
            Continuar();
        }
        #endregion  


        #region FUNCIONES
        /// <summary>
        /// Método que contiene restricciones para los direfentes roles
        public void Restricciones()
        {
            //Rol Vendedor
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                //ocultat sucursalesa
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                Point l1 = new Point(label10.Location.X, label10.Location.Y);
                Point c1 = new Point(clbLinea.Location.X, clbLinea.Location.Y);

                Point l2 = new Point(lblSucursal.Location.X, lblSucursal.Location.Y);
                Point c2 = new Point(clbSucursal.Location.X, clbSucursal.Location.Y);

                Point l3 = new Point(lblVendedor.Location.X, lblVendedor.Location.Y);
                Point c3 = new Point(clbVendedor.Location.X, clbVendedor.Location.Y);

                lblVendedor.Location = l1;
                clbVendedor.Location = c1;

                label10.Location = l2;
                clbLinea.Location = c2;

                lblCanal.Location = l3;
                clbCanal.Location = c3;

               // lblObjetivo.Visible = false;
               // txtObjetivo.Visible = false;
            }
            //Rol Ventas Especial
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {

                label10.Location = new Point(lblSucursal.Location.X, lblSucursal.Location.Y);
                clbLinea.Location = new Point(clbSucursal.Location.X, clbSucursal.Location.Y);

                lblCanal.Location = new Point(label10.Location.X, label10.Location.Y);
                clbCanal.Location = new Point(clbLinea.Location.X, clbLinea.Location.Y);
                
                lblVendedor.Visible = false;
                clbVendedor.Visible = false;
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                Vendedores = "," + CodigoVendedor.ToString();

                lblUtiSucursal.Visible = false;
                txtUtiSucursal.Visible = false;
                try
                {
                    SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                    commandVendedor.CommandType = CommandType.StoredProcedure;
                    commandVendedor.Parameters.AddWithValue("@TipoConsulta", 9);
                    commandVendedor.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                    commandVendedor.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    commandVendedor.Parameters.AddWithValue("@Bono", 0);
                    commandVendedor.Parameters.AddWithValue("@From", string.Empty);
                    commandVendedor.Parameters.AddWithValue("@Mensaje", string.Empty);
                    commandVendedor.CommandTimeout = 0;

                    DataTable tbl = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = commandVendedor;
                    da.SelectCommand.CommandTimeout = 0;
                    da.Fill(tbl);
                    decimal UtilidadObjetivoM = 0;
                    var queryUOM = (from item in tbl.AsEnumerable()
                                    where item[1].ToString() == "[8]"
                                    select item).ToList();
                    if (queryUOM.Count != 0)
                        UtilidadObjetivoM = Convert.ToDecimal(queryUOM[0].ItemArray[2].ToString());

                    decimal UtilidadObjetivoT = 0;
                    var queryUOT = (from item in tbl.AsEnumerable()
                                    where item[1].ToString() == "[9]"
                                    select item).ToList();
                    if (queryUOT.Count != 0)
                        UtilidadObjetivoT = Convert.ToDecimal(queryUOT[0].ItemArray[2].ToString());

                    if (UtilidadObjetivoM == 0 || UtilidadObjetivoT == 0)
                    {
                        if (UtilidadObjetivoM > 0)
                            txtObjetivo.Text = UtilidadObjetivoM.ToString("P");
                        else
                            txtObjetivo.Text = UtilidadObjetivoT.ToString("P");
                    }
                    else{
                        txtObjetivo.Text = UtilidadObjetivoM.ToString("P") + " -- " + UtilidadObjetivoT.ToString("P");
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

        }

        public string GetCadena(CheckedListBox clb)
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

            return stb.ToString().Trim(',');
        }
        /// </summary>
        /// <summary>
        /// Método que realiza la consulta de Facturas
        /// </summary>
        public void ConsultarFacturas()
        {
            gridFacturas.Columns.Clear();
            gridFacturas.DataSource = null;
            gridDetalles.Columns.Clear();
            gridDetalles.DataSource = null;
            txtArticulo.Focus();
            
            if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                Vendedores = this.GetCadena(clbVendedor);
            }

            Lineas = this.GetCadena(clbLinea);
            Sucursales = this.GetCadena(clbSucursal);
            Canales = this.GetCadena(clbCanal);

            Articulo = txtArticulo.Text;

            if (cbFiltrar.Checked)
                Articulo = "ejes";

            Cliente = txtCliente.Text;
            Factura = txtFactura.Text;

            FechaInicial = dtpFechaInicial.Value.ToString("yyyy-MM-dd");
            FechaFinal = dtpFechaFinal.Value.ToString("yyyy-MM-dd");
            DataSet data = new DataSet();
            BindingSource masterBindingSource = new BindingSource();
            BindingSource detailsBindingSource = new BindingSource();
            //gridFacturas.DataSource = masterBindingSource;
            //gridDetalles.DataSource = detailsBindingSource;

            SqlCommand command = new SqlCommand("PJ_Utildad", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);
            command.Parameters.AddWithValue("@Vendedores", Vendedores.Trim(','));
            command.Parameters.AddWithValue("@Lineas", Lineas.Trim(','));
            command.Parameters.AddWithValue("@Cliente", Cliente.Trim(','));
            command.Parameters.AddWithValue("@Articulo", Articulo.Trim(','));
            command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
            command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
            command.Parameters.AddWithValue("@Factura", Factura);
            command.Parameters.AddWithValue("@Sucursales", Sucursales.Trim(','));
            command.Parameters.AddWithValue("@GranCanales", Canales.Trim(','));
            command.Parameters.AddWithValue("@Canales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@Mes", string.Empty);
            command.Parameters.AddWithValue("@Anio", string.Empty);
            command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
            command.Parameters.AddWithValue("@Precio", 0);
            command.Parameters.AddWithValue("@Moneda", string.Empty);
            command.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(data, "TablaDetalles");

           // gridDetalles.DataSource = data.Tables["TablaFactura"];
            if (data.Tables["TablaDetalles"].Rows.Count > 0)
            {
                var query = from item in data.Tables["TablaDetalles"].AsEnumerable()
                            group item by new
                            {
                                Fecha = item.Field<DateTime>("DocDate"),
                                Factura = item.Field<Int32>("DocNum"),
                                Sucursal = item.Field<string>("Sucursal"),
                                Vendedor = item.Field<string>("SlpName"),
                                Cliente = item.Field<string>("CardCode"),
                                NombreCliente = item.Field<string>("CardName"),
                                GranCanal = item.Field<string>("GranCanal")
                            } into grouped
                            select new
                            {
                                Fecha = grouped.Key.Fecha,
                                Factura = grouped.Key.Factura,
                                Sucursal = grouped.Key.Sucursal,
                                Vendedor = grouped.Key.Vendedor,
                                Cliente = grouped.Key.Cliente,
                                NombreCliente = grouped.Key.NombreCliente,
                                GranCanal = grouped.Key.GranCanal,
                                PrecioCompra = grouped.Sum(ix => ix.Field<decimal>("PrecioCompra") * ix.Field<Int32>("Cantidad")),
                                PrecioVenta = grouped.Sum(ix => ix.Field<decimal>("Price") * ix.Field<Int32>("Cantidad")),
                                PrecioPP = grouped.Sum(ix => ix.Field<decimal>("PrecioCPP") * ix.Field<Int32>("Cantidad")),
                                PrecioReal = grouped.Sum(ix => ix.Field<decimal>("PrecioEspecial") * ix.Field<Int32>("Cantidad")),
                                TotalFactura = grouped.Sum(ix => ix.Field<decimal>("Total")),
                                Utilidad = grouped.Sum(ix => ix.Field<decimal>("Total")) == 0 ? 0 : 1 - (grouped.Sum(ix => ix.Field<decimal>("PrecioCompra") * ix.Field<Int32>("Cantidad")) / grouped.Sum(ix => ix.Field<decimal>("Total")))
                            };


                if (query.Count() > 0)
                {
                    DataTable detalle = Clases.ListConverter.ToDataTable(query.ToList());
                    detalle.TableName = "TablaFactura";
                    data.Tables.Add(detalle);

                    DataRelation relation = new DataRelation("FacturaDetalle", data.Tables["TablaFactura"].Columns["Factura"], data.Tables["TablaDetalles"].Columns["DocNum"]);
                    data.Relations.Add(relation);

                    if (0 == (int)data.Tables["TablaFactura"].Rows.Count)
                    {
                        button1.Visible = false;
                        foreach (Control item in groupBox1.Controls)
                        {
                            if (item.Name == control.Name)
                                item.Focus();
                        }
                    }
                    else
                    {
                        button1.Visible = true;
                        button1.Enabled = true;

                        foreach (Control item in groupBox1.Controls)
                        {
                            if (item.Name == control.Name)
                                item.Focus();
                        }
                    }

                    masterBindingSource.DataSource = data;
                    masterBindingSource.DataMember = "TablaFactura";
                    detailsBindingSource.DataSource = masterBindingSource;
                    detailsBindingSource.DataMember = "FacturaDetalle";
                    gridFacturas.DataSource = masterBindingSource;
                    gridDetalles.DataSource = detailsBindingSource;
                }

                gridExcel.DataSource = CrearTablaExcel(data.Tables["TablaDetalles"]);
                decimal totalPrecioCompra = 0;
                decimal totalPrecioVenta = 0;
                decimal totalPrecioReal = 0;
                decimal totalFacturaMN = 0;


                totalPrecioCompra = Convert.ToDecimal(data.Tables["TablaDetalles"].Compute("SUM(PrecioCompraMXN)", string.Empty));
                totalPrecioVenta = Convert.ToDecimal(data.Tables["TablaDetalles"].Compute("SUM(PrecioVentaMXN)", string.Empty));
                totalPrecioReal = Convert.ToDecimal(data.Tables["TablaDetalles"].Compute("SUM(PrecioVentaFinal)", string.Empty));
                totalFacturaMN = Convert.ToDecimal(data.Tables["TablaDetalles"].Compute("SUM(PrecioVentaFinal)", string.Empty));
                //foreach (DataGridViewRow item in gridFacturas.Rows)
                //{
                //    totalPrecioCompra += decimal.Parse(item.Cells["TotalCompraMN"].Value.ToString());
                //    totalPrecioVenta += decimal.Parse(item.Cells["TotalPVentaMN"].Value.ToString());
                //    totalPrecioReal += decimal.Parse(item.Cells["TotalFacturaMN"].Value.ToString());
                //    totalFacturaMN += decimal.Parse(item.Cells["TotalFacturaMN"].Value.ToString());
                //}

                txtVenta.Text = totalPrecioVenta.ToString("c");
                txtVolumen.Text = totalPrecioReal.ToString("c");

                if (totalFacturaMN != 0)
                {
                    txtUtilidad.Text = (1 - (totalPrecioCompra / totalFacturaMN)).ToString("P2");
                }
                else
                {
                    txtUtilidad.Text = "0 %";
                }

                DarFormatoGrid();
                DarFormatoGridDetalle("C2", "P2", true);
            }
        }

        /// <summary>
        /// Función que crea tabla para exportar a excel
        /// </summary>
        public DataTable CrearTablaExcel(DataTable table)
        {
            DataTable t = new DataTable(); ;
            t.Columns.Add("Vendedor", typeof(string));
            t.Columns.Add("Factura", typeof(string));//ok
            t.Columns.Add("Fecha", typeof(DateTime));//ok
            t.Columns.Add("Cliente", typeof(string));//ok
            t.Columns.Add("NombreCliente", typeof(string));//ok
            t.Columns.Add("Sucursal", typeof(string));//ok
            t.Columns.Add("GranCanal", typeof(string));
            t.Columns.Add("Cantidad", typeof(int));//ok
            t.Columns.Add("Artículo", typeof(string));//ok
            t.Columns.Add("Nombre Artículo", typeof(string));//ok
            t.Columns.Add("Linea", typeof(string));//ok
            t.Columns.Add("PrecioReal", typeof(decimal));
            t.Columns.Add("CostoBase", typeof(decimal));//ok
            t.Columns.Add("TOTAL VENTA", typeof(decimal), "Cantidad*PrecioReal");//ok
            t.Columns.Add("TOTAL COMPRA", typeof(string), "Cantidad*[CostoBase]");//ok

            foreach (DataRow row in table.Rows)
            {
                DataRow r = t.NewRow();
                r["Vendedor"] = row["SlpName"];
                r["Factura"] = row["DocNum"];//ok
                r["Fecha"] = row["DocDate"];//ok
                r["Cliente"] = row["CardCode"];//ok
                r["NombreCliente"] = row["CardName"];//ok
                r["Sucursal"] = row["Sucursal"];//ok
                r["GranCanal"] = row["GranCanal"];
                r["Cantidad"] = row["Cantidad"];//ok
                r["Artículo"] = row["ItemCode"];//ok
                r["Nombre Artículo"] = row["ItemName"];//ok
                r["Linea"] = row["ItmsGrpNam"];//ok
                r["PrecioReal"] = Convert.ToDecimal(row["PrecioVentaFinal"]) / Convert.ToDecimal(row["Cantidad"]);
                r["CostoBase"] = Convert.ToDecimal(row["PrecioCompraMXN"]) / Convert.ToDecimal(row["Cantidad"]);//ok
                t.Rows.Add(r);
            }
            //t = table.Copy();
            return t;
        }

        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoGrid()
        {
            gridFacturas.Columns[(int)ColumnasGrid.FechaFactura].HeaderText = "Fecha";
            gridFacturas.Columns[(int)ColumnasGrid.NoFactura].HeaderText = "Factura";
            gridFacturas.Columns[(int)ColumnasGrid.Sucursal].HeaderText = "Sucursal";
            gridFacturas.Columns[(int)ColumnasGrid.Vendedor].HeaderText = "Vendedor";
            gridFacturas.Columns[(int)ColumnasGrid.Cliente].HeaderText = "Cliente";
            gridFacturas.Columns[(int)ColumnasGrid.NombreCliente].HeaderText = "Nombre del cliente";
            gridFacturas.Columns[(int)ColumnasGrid.GranCanal].HeaderText = "Gran canal";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioCompra].HeaderText = "Precio de compra";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioVenta].HeaderText = "Precio de venta";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioCPP].HeaderText = "Precio CPP";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioEspecial].HeaderText = "Precio real";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioVentaFinal].HeaderText = "Total factura";
            gridFacturas.Columns[(int)ColumnasGrid.Utilidad].HeaderText = "Utilidad";

            gridFacturas.Columns[(int)ColumnasGrid.FechaFactura].Width = 90;
            gridFacturas.Columns[(int)ColumnasGrid.FechaFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.NoFactura].Width = 90;
            gridFacturas.Columns[(int)ColumnasGrid.NoFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.Sucursal].Width = 90;
            gridFacturas.Columns[(int)ColumnasGrid.Sucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.Vendedor].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.Vendedor].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.Cliente].Width = 60;
            gridFacturas.Columns[(int)ColumnasGrid.Cliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.NombreCliente].Width = 200;
            gridFacturas.Columns[(int)ColumnasGrid.NombreCliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.GranCanal].Width = 80;
            gridFacturas.Columns[(int)ColumnasGrid.GranCanal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //Modificación Emmanuel 30-Ene-2014
            gridFacturas.Columns[(int)ColumnasGrid.PrecioCompra].Width = 90;
            gridFacturas.Columns[(int)ColumnasGrid.PrecioCompra].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioCompra].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            gridFacturas.Columns[(int)ColumnasGrid.PrecioCompra].Visible = false;


            gridFacturas.Columns[(int)ColumnasGrid.PrecioVenta].Width = 90;
            gridFacturas.Columns[(int)ColumnasGrid.PrecioVenta].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioVenta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.PrecioCPP].Width = 90;
            gridFacturas.Columns[(int)ColumnasGrid.PrecioCPP].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioCPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.PrecioEspecial].Width = 90;
            gridFacturas.Columns[(int)ColumnasGrid.PrecioEspecial].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioEspecial].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.PrecioVentaFinal].Width = 90;
            gridFacturas.Columns[(int)ColumnasGrid.PrecioVentaFinal].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.PrecioVentaFinal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridFacturas.Columns[(int)ColumnasGrid.Utilidad].Width = 80;
            gridFacturas.Columns[(int)ColumnasGrid.Utilidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFacturas.Columns[(int)ColumnasGrid.Utilidad].DefaultCellStyle.Format = "P2";


            gridFacturas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            gridFacturas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 54, 92);
            gridFacturas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridFacturas.ColumnHeadersDefaultCellStyle.Font = new Font(gridFacturas.Font, FontStyle.Bold);
            gridFacturas.RowHeadersWidth = 40;
            gridFacturas.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 230, 241);
            gridFacturas.RowHeadersDefaultCellStyle.ForeColor = Color.White;
        
            try
            {
                foreach (DataGridViewRow row in gridFacturas.Rows)
                {
                    if (Convert.ToInt32(row.Cells[(int)ColumnasGrid.PrecioEspecial].Value) == 0)
                    {
                        row.Cells[(int)ColumnasGrid.PrecioEspecial].Style.ForeColor = Color.Red;
                    }
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Utilidad].Value) < (decimal)0.13 && Convert.ToString(row.Cells[(int)ColumnasGrid.GranCanal].Value) == "Mayoreo")
                    {
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.BackColor = Color.Yellow;
                    }
                    else
                        if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Utilidad].Value) < (decimal)0.16 && Convert.ToString(row.Cells[(int)ColumnasGrid.GranCanal].Value) == "Transporte")
                        {
                            row.Cells[(int)ColumnasGrid.Utilidad].Style.ForeColor = Color.Black;
                            row.Cells[(int)ColumnasGrid.Utilidad].Style.BackColor = Color.Yellow;
                        }
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Utilidad].Value) < 0)
                    {
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGrid.Utilidad].Style.BackColor = Color.Red;
                    }
                }

            }
            catch (Exception)
            {
                // MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoGridDetalle(string _moneda, string _porcentaje, bool _add)
        {
            if (_add)
            {
                DataGridViewButtonColumn botonPCB = new DataGridViewButtonColumn();
                {
                    botonPCB.Name = "Actualizar Rebate";
                    botonPCB.HeaderText = "Actualizar Rebate";
                    botonPCB.Text = "Actu Rebate";
                    botonPCB.Width = 130;
                    botonPCB.UseColumnTextForButtonValue = true;
                }
                gridDetalles.Columns.Add(botonPCB);
            }

            gridDetalles.Columns[(int)ColumnasGridDetalle.LineNum].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.DocDate].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Sucursal].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.SlpName].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.GranCanal].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.CardCode].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.CardName].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.DocRate].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Total].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVentaMXN].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompraMXN].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioEspecialMXN].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCPPMXN].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVentaFinal].Visible = false;

            gridDetalles.Columns[(int)ColumnasGridDetalle.DocNum].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItemCode].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItemName].Width = 150;
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItmsGrpName].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Moneda].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.U_PCB].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Price].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCPP].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioEspecial].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Utilidad].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Boton].Width = 100;

            gridDetalles.Columns[(int)ColumnasGridDetalle.DocNum].HeaderText = "Factura";
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItemCode].HeaderText = "Artículo";
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItemName].HeaderText = "Nombre del Artículo";
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItmsGrpName].HeaderText = "Línea";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].HeaderText = "Cantidad";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Moneda].HeaderText = "Moneda";
            gridDetalles.Columns[(int)ColumnasGridDetalle.U_PCB].HeaderText = "Rebate";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].HeaderText = "Costo base";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Price].HeaderText = "Precio de venta";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].HeaderText = "Descuento";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCPP].HeaderText = "Precio CPP";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioEspecial].HeaderText = "Precio real";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Utilidad].HeaderText = "Utilidad";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Boton].HeaderText = "Actualizar rebate";

            gridDetalles.Columns[(int)ColumnasGridDetalle.DocNum].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItemCode].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItemName].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.ItmsGrpName].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Moneda].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.U_PCB].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Price].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCPP].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioEspecial].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Utilidad].ReadOnly = true;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Boton].Width = 100;

            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioEspecial].DefaultCellStyle.BackColor = Color.FromArgb(221,217,196);
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioEspecial].Width = 90;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridDetalles.Columns[(int)ColumnasGridDetalle.U_PCB].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Price].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioEspecial].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Utilidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.U_PCB].DefaultCellStyle.Format = _moneda;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].DefaultCellStyle.Format = _moneda;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Price].DefaultCellStyle.Format = _moneda;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCPP].DefaultCellStyle.Format = _moneda;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioEspecial].DefaultCellStyle.Format = _moneda;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].DefaultCellStyle.Format = _porcentaje;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Utilidad].DefaultCellStyle.Format = _porcentaje;

            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].Visible = false;
            }

            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                gridDetalles.Columns[(int)ColumnasGridDetalle.U_PCB].ReadOnly = false;
                gridDetalles.Columns[(int)ColumnasGridDetalle.Boton].Visible = true;
            }
            else
            {
                gridDetalles.Columns[(int)ColumnasGridDetalle.U_PCB].ReadOnly = true;
                gridDetalles.Columns[(int)ColumnasGridDetalle.Boton].Visible = false;
            }

            gridDetalles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            gridDetalles.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 54, 92);
            gridDetalles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridDetalles.ColumnHeadersDefaultCellStyle.Font = new Font(gridDetalles.Font, FontStyle.Bold);
            gridDetalles.RowHeadersWidth = 40;
            gridDetalles.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 230, 241);
            gridDetalles.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            //try
            //{
            //    foreach (DataGridViewRow row in gridDetalles.Rows)
            //    {
            //        if (Convert.ToInt32(row.Cells[(int)ColumnasGridDetalle.PrecioEspecial].Value) == 0)
            //        {
            //            row.Cells[(int)ColumnasGridDetalle.PrecioEspecial].Style.ForeColor = Color.Red;
            //        }

            //        DataGridViewRow selecionada = gridFacturas.CurrentRow;
            //        if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < (decimal)0.13 && Convert.ToString(selecionada.Cells[(int)ColumnasGrid.GranCanal].Value) == "Mayoreo")
            //        {
            //            row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
            //            row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Yellow;
            //        }
            //        else
            //            if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < (decimal)0.16 && Convert.ToString(selecionada.Cells[(int)ColumnasGrid.GranCanal].Value) == "Transporte")
            //            {
            //                row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
            //                row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Yellow;
            //            }

            //        if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < 0)
            //        {
            //            row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.White;
            //            row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Red;
            //        }
            //    }

            //}
            //catch (Exception)
            //{
            //}
        }

        /// <summary>
        /// Método que carga las sucursales en el cbSucursal
        /// </summary>
        private void CargarSucursales()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
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
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
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
                row["Nombre"] = "TODOS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET .GerenteVentasSucursal)
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
                row["Nombre"] = "TODOS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
        }       

        /// <summary>
        /// Método que carga las Lineas en el clbLinea
        /// </summary>
        private void CargarLinea()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Linea);
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

            clbLinea.DataSource = table;
            clbLinea.DisplayMember = "Nombre";
            clbLinea.ValueMember = "Codigo";
        }

        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
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

        private void ActualizarFactura()
        {
            string noFactura = "";
            string result = string.Empty;

            foreach (DataGridViewRow row in gridDetalles.Rows)
            {
                try
                {
                    Factura = row.Cells[(int)ColumnasGridDetalle.DocNum].Value.ToString();
                    LineNum = Convert.ToString(row.Cells["LineNum"].Value);
                    Articulo = row.Cells[(int)ColumnasGridDetalle.ItemCode].Value.ToString();
                    PrecioEspecial = Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.PrecioEspecial].Value);

                    SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaPJ.ActualizarPrecioEspecial);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@Lineas", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Articulo", Articulo);
                    command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                    command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                    command.Parameters.AddWithValue("@Factura", Factura);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@GranCanales", string.Empty);
                    command.Parameters.AddWithValue("@Canales", string.Empty);
                    command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Mes", LineNum);
                    command.Parameters.AddWithValue("@Anio", string.Empty);
                    command.Parameters.AddWithValue("@NombreArticulo", ClasesSGUV.Login.NombreUsuario);
                    command.Parameters.AddWithValue("@Precio", PrecioEspecial);
                    command.Parameters.AddWithValue("@Moneda", string.Empty);
                    conection.Open();

                    //command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                        result = reader.GetString(0);

                    noFactura = Factura;
                }
                catch (Exception) { }
                finally { conection.Close(); }
            }


            ConsultarFacturas();
            
            if (string.IsNullOrEmpty(result))
                MessageBox.Show("Se actualizó correctamente la factura: " + noFactura + ".", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(result, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarGranCanales()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.GranCanal);
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

            clbCanal.DataSource = table;
            clbCanal.DisplayMember = "Nombre";
            clbCanal.ValueMember = "Codigo";
        }

        decimal UtilidadObjetivoM = 0, UtilidadObjetivoT = 0;

        private void gridFacturas_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)ColumnasGrid.Vendedor)
                    {

                        if (StringVendedor != gridFacturas.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.Vendedor].Value.ToString())
                        {
                            StringVendedor = gridFacturas.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.Vendedor].Value.ToString();
                            Esperar();
                            SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                            commandVendedor.CommandType = CommandType.StoredProcedure;
                            commandVendedor.Parameters.AddWithValue("@TipoConsulta", 10);
                            commandVendedor.Parameters.AddWithValue("@SlpCode", string.Empty);
                            commandVendedor.Parameters.AddWithValue("@Fecha", DateTime.Parse(FechaFinal));
                            commandVendedor.Parameters.AddWithValue("@Bono", 0);
                            commandVendedor.Parameters.AddWithValue("@From", string.Empty);
                            commandVendedor.Parameters.AddWithValue("@Mensaje", StringVendedor);

                            DataTable tbl = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = commandVendedor;
                            da.SelectCommand.CommandTimeout = 0;
                            da.Fill(tbl);

                            var queryUOM = (from item in tbl.AsEnumerable()
                                            where item[1].ToString() == "[8]"
                                            select item).ToList();

                            if (queryUOM.Count != 0)
                                UtilidadObjetivoM = Convert.ToDecimal(queryUOM[0].ItemArray[2].ToString());

                            var queryUOT = (from item in tbl.AsEnumerable()
                                            where item[1].ToString() == "[9]"
                                            select item).ToList();
                            if (queryUOT.Count != 0)
                                UtilidadObjetivoT = Convert.ToDecimal(queryUOT[0].ItemArray[2].ToString());

                            if (UtilidadObjetivoM == 0 || UtilidadObjetivoT == 0)
                            {
                                if (UtilidadObjetivoM > 0)
                                    txtObjetivo.Text = UtilidadObjetivoM.ToString("P");
                                else
                                    txtObjetivo.Text = UtilidadObjetivoT.ToString("P");
                            }
                            else
                            {
                                txtObjetivo.Text = UtilidadObjetivoM.ToString("P") + " -- " + UtilidadObjetivoT.ToString("P");
                            }

                            UtilidadObjetivoT = 0;
                            UtilidadObjetivoM = 0;

                            StringSucursal = gridFacturas.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.Sucursal].Value.ToString();
                            Esperar();
                            SqlCommand CommandSucursal = new SqlCommand("PJ_VariasScoreCard", conection);
                            CommandSucursal.CommandType = CommandType.StoredProcedure;
                            CommandSucursal.Parameters.AddWithValue("@TipoConsulta", 11);
                            CommandSucursal.Parameters.AddWithValue("@SlpCode", string.Empty);
                            CommandSucursal.Parameters.AddWithValue("@Fecha", DateTime.Parse(FechaFinal));
                            CommandSucursal.Parameters.AddWithValue("@Bono", 0);
                            CommandSucursal.Parameters.AddWithValue("@From", string.Empty);
                            CommandSucursal.Parameters.AddWithValue("@Mensaje", StringSucursal);

                            DataTable tbl1 = new DataTable();
                            SqlDataAdapter da1 = new SqlDataAdapter();
                            da1.SelectCommand = CommandSucursal;
                            da1.SelectCommand.CommandTimeout = 0;
                            da1.Fill(tbl1);

                            var queryUOM1 = (from item in tbl1.AsEnumerable()
                                             where item[1].ToString() == "[8]"
                                             select item).ToList();

                            if (queryUOM1.Count != 0)
                                UtilidadObjetivoM = Convert.ToDecimal(queryUOM1[0].ItemArray[2].ToString());

                            var queryUOT1 = (from item in tbl1.AsEnumerable()
                                             where item[1].ToString() == "[9]"
                                             select item).ToList();
                            if (queryUOT1.Count != 0)
                                UtilidadObjetivoT = Convert.ToDecimal(queryUOT1[0].ItemArray[2].ToString());

                            if (UtilidadObjetivoM == 0 || UtilidadObjetivoT == 0)
                            {
                                if (UtilidadObjetivoM > 0)
                                    txtUtiSucursal.Text = UtilidadObjetivoM.ToString("P");
                                else
                                    txtUtiSucursal.Text = UtilidadObjetivoT.ToString("P");
                            }
                            else
                            {
                                txtUtiSucursal.Text = UtilidadObjetivoM.ToString("P") + " -- " + UtilidadObjetivoT.ToString("P");
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();

            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnNoPrecioEspecial dentro del GridFacturas
        /// Aplica No precio especial a todos los productos de la factura indicada
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void gridDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                   if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)ColumnasGridDetalle.Boton)
                    {
                        string noFactura = "";
                        try
                        {
                            foreach (DataGridViewRow item in(sender as DataGridView).Rows)
                            {

                                Factura = item.Cells[(int)ColumnasGridDetalle.DocNum].Value.ToString();
                                Articulo = item.Cells[(int)ColumnasGridDetalle.ItemCode].Value.ToString();
                                decimal PCB = Convert.ToDecimal(item.Cells[(int)ColumnasGridDetalle.U_PCB].Value);
                                LineNum = item.Cells["LineNum"].Value.ToString();

                                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                {
                                    using (SqlCommand command = new SqlCommand("PJ_Ventas", conn))
                                    {
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaPJ.ActualizaPCB);
                                        command.Parameters.AddWithValue("@Vendedores", string.Empty);
                                        command.Parameters.AddWithValue("@Lineas", string.Empty);
                                        command.Parameters.AddWithValue("@Cliente", string.Empty);
                                        command.Parameters.AddWithValue("@Articulo", Articulo);
                                        command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                                        command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                                        command.Parameters.AddWithValue("@Factura", Factura);
                                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                                        command.Parameters.AddWithValue("@GranCanales", string.Empty);
                                        command.Parameters.AddWithValue("@Canales", string.Empty);
                                        command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                                        command.Parameters.AddWithValue("@Mes", LineNum);
                                        command.Parameters.AddWithValue("@Anio", string.Empty);
                                        command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                                        command.Parameters.AddWithValue("@Precio", PCB);
                                        command.Parameters.AddWithValue("@Moneda", string.Empty);

                                        conn.Open();
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }
                            noFactura = Factura;
                        }
                        catch (Exception) { }
                        finally {  }
                        Esperar();
                        ConsultarFacturas();
                        Continuar();
                        MessageBox.Show("Se actualizó correctamente la factura: " + noFactura + ".", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFactura_Validating(object sender, CancelEventArgs e)
        {
            int fact = 0;
            bool bStatus = false;
            try
            {
                if (String.IsNullOrEmpty(txtFactura.Text))
                    fact = 0;
                else
                    fact = int.Parse(txtFactura.Text);
                bStatus = true;
            }
            catch (Exception)
            {
                bStatus = false;
            }

            if (!bStatus)
                errorProvider.SetError(txtFactura, "El campo 'Factura' solo acepta números.");
            else
                errorProvider.Clear();
            
        }

        Control control = new Control();
        private void txtFactura_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnPresentar_Click(sender, e);
                control = txtFactura;
            }

            if (e.KeyCode == Keys.Escape)
            {
                Facturas_Load(sender, e);
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                var clickedMenuItem = e.ClickedItem as ToolStripMenuItem ;
                string menuText = clickedMenuItem.Text;

                switch (menuText)
                {
                    case "Ver 2 decimales":
                        this.DarFormatoGridDetalle("C2", "P2", false);
                        break;

                    case "Ver 4 decimales":
                        this.DarFormatoGridDetalle("C4", "P4", false);
                        break;

                    case "Ver 6 decimales":
                        this.DarFormatoGridDetalle("C6", "P6", false);
                        break;
                }
            }
            catch (Exception)
            {
            }
        }

        private void Facturas_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void Facturas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
            }
        }

        private void gridDetalles_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            decimal sumita = decimal.Zero;
            try
            {
                var seleccionadas = (sender as DataGridView).SelectedCells;
                foreach (DataGridViewCell item in seleccionadas)
                {
                    sumita += Convert.ToDecimal(item.Value);
                }

                toolTotalSuma.Text = "Suma: " + sumita.ToString("C2");
            }
            catch (Exception)
            {
                toolTotalSuma.Text = "Suma: " + sumita.ToString("C2");
            }
        }

        private void gridDetalles_MouseUp(object sender, MouseEventArgs e)
        {
            decimal sumita = decimal.Zero;
            try
            {
                var seleccionadas = (sender as DataGridView).SelectedCells;
                foreach (DataGridViewCell item in seleccionadas)
                {
                    sumita += Convert.ToDecimal(item.Value);
                }

                toolTotalSuma.Text = "Suma: " + sumita.ToString("C2");
            }
            catch (Exception)
            {
                toolTotalSuma.Text = "Suma: " + sumita.ToString("C2");
            }
        }

   
        /// </sumary>
        #endregion


       
    }
}
