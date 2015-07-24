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

namespace Ventas
{
    public partial class Descargas : Form
    {

        #region PARÁMETROS

        Clases.Logs log;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);

        public string Vendedores;
        public string JefasCobranza;
        public string Sucursales;
        public string Cliente;
        public string Factura;
        public string FechaInicial;
        public string FechaFinal;
        public bool Open = true;
        DataSet data = new DataSet();
        
        /// <summary>
        /// Enumerador para las columnas del grid
        /// </summary>
        private enum ColumnasGrid
        {
            Cliente, Nombre, Factura, FechaFactura, FechaVencimiento, TotalFactura, NCRealSugerida, AplicadaPE, AplicadoSAPPE, Diferencia1,
            NCPPSugerida, AplicadoPP, AplicadoSAPPP,Diferencia2, Saldo, Estatus, DocStatus
            
        }

        /// <summary>
        /// Enumerador para las columnas del gridDetalle
        /// </summary>
        private enum ColumnasGridDetalle
        {
            Factura = 0,
            Articulo = 1,
            NombreArticulo = 2,
            Linea = 3,
            Cantidad = 4,
            Moneda = 5,
            PrecioVenta = 6,
            Descuento = 7,
            PrecioPP = 8,
            PrecioVolumen = 9,
            DocStatus = 10
        }

        #endregion


        #region EVENTOS

        public Descargas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que ocurre al cargarse la página
        /// Llena los controles que van a ser utilizados
        /// </summary>
        /// <param name="sender">Parámetros del evento</param>
        /// <param name="e">Objeto que produce el evento</param>
        private void Cobranza_Load(object sender, EventArgs e)
        {
            //this.MaximizeBox = false;
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                //ToolTip tool = new ToolTip();
                //tool.SetToolTip(button1, "Filtra solo facturas con NC no aplicadas y con sugerencia.");
                CargarVendedores();
                CargarJefesCobranza();
                CargarSucursales();

                txtCliente.Clear();
                txtFactura.Clear();
                checkBox1.Checked = false;
                checkBox1.Enabled = false;

                gridCobranza.DataSource = null;
                gridDetalles.DataSource = null;
                dtpFechaInicial.Focus();

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
        private void clbCobranza_Click(object sender, EventArgs e)
        {
            if (clbCobranza.SelectedIndex == 0)
            {
                if (clbCobranza.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbCobranza.Items.Count; item++)
                    {
                        clbCobranza.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbCobranza.Items.Count; item++)
                    {
                        clbCobranza.SetItemChecked(item, true);
                    }
                }
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnPresentar
        /// Ejecuta la consulta de acuerdo a los criterios de búsqueda
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                Esperar();
                StringBuilder stbVendedores = new StringBuilder();
                foreach (DataRowView item in clbVendedor.CheckedItems)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbVendedor.ToString().Equals(string.Empty))
                        {
                            stbVendedores.Append(",");
                        }
                        stbVendedores.Append(item["Codigo"].ToString());
                    }
                }
                if (clbVendedor.CheckedItems.Count == 0)
                {
                    foreach (DataRowView item in clbVendedor.Items)
                    {
                        if (item["Codigo"].ToString() != "0")
                        {
                            if (!clbVendedor.ToString().Equals(string.Empty))
                            {
                                stbVendedores.Append(",");
                            }
                            stbVendedores.Append(item["Codigo"].ToString());
                        }
                    }
                }

                StringBuilder stbCobranza = new StringBuilder();
                foreach (DataRowView item in clbCobranza.CheckedItems)
                {
                    if (item["Nombre"].ToString() != "TODAS")
                    {
                        if (!clbCobranza.ToString().Equals(string.Empty))
                        {
                            stbCobranza.Append(",");
                        }
                        stbCobranza.Append(item["Nombre"].ToString());
                    }
                }
                if (clbCobranza.CheckedItems.Count == 0)
                {
                    foreach (DataRowView item in clbCobranza.Items)
                    {
                        if (item["Nombre"].ToString() != "TODAS")
                        {
                            if (!clbCobranza.ToString().Equals(string.Empty))
                            {
                                stbCobranza.Append(",");
                            }
                            stbCobranza.Append(item["Nombre"].ToString());
                        }
                    }
                }

                StringBuilder stbSucursales = new StringBuilder();
                foreach (DataRowView item in clbSucursal.CheckedItems)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbSucursal.ToString().Equals(string.Empty))
                        {
                            stbSucursales.Append(",");
                        }
                        stbSucursales.Append(item["Codigo"].ToString());
                    }
                }
                if (clbSucursal.CheckedItems.Count == 0)
                {
                    foreach (DataRowView item in clbSucursal.Items)
                    {
                        if (item["Codigo"].ToString() != "0")
                        {
                            if (!clbSucursal.ToString().Equals(string.Empty))
                            {
                                stbSucursales.Append(",");
                            }
                            stbSucursales.Append(item["Codigo"].ToString());
                        }
                    }
                }

                Vendedores = stbVendedores.ToString();
                Sucursales = stbSucursales.ToString();
                JefasCobranza = stbCobranza.ToString();
                Factura = txtFactura.Text;
                Cliente = txtCliente.Text;
                FechaInicial = dtpFechaInicial.Value.Date.ToShortDateString();
                FechaFinal = dtpFechaFinal.Value.Date.ToShortDateString();
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                checkBox2.Checked = false;

                data.Reset();
                
                BindingSource masterBindingSource = new BindingSource();
                BindingSource detailsBindingSource = new BindingSource();
                gridCobranza.DataSource = masterBindingSource;
                gridDetalles.DataSource = detailsBindingSource;

                SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaPJ.ConsultaDeCobranza);
                command.Parameters.AddWithValue("@Vendedores", Vendedores);
                command.Parameters.AddWithValue("@Lineas", string.Empty);
                command.Parameters.AddWithValue("@Cliente", Cliente);
                command.Parameters.AddWithValue("@Articulo", string.Empty);
                command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                command.Parameters.AddWithValue("@Factura", Factura);
                command.Parameters.AddWithValue("@Sucursales", Sucursales);
                command.Parameters.AddWithValue("@GranCanales", string.Empty);
                command.Parameters.AddWithValue("@Canales", string.Empty);
                command.Parameters.AddWithValue("@JefasCobranza", JefasCobranza);
                command.Parameters.AddWithValue("@Mes", string.Empty);
                command.Parameters.AddWithValue("@Anio", string.Empty);
                command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                command.Parameters.AddWithValue("@Precio", 0);
                command.Parameters.AddWithValue("@Moneda", string.Empty);
                command.CommandTimeout = 0;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(data, "TablaFactura");


                SqlCommand commandDetalles = new SqlCommand("PJ_Ventas", conection);
                commandDetalles.CommandType = CommandType.StoredProcedure;
                commandDetalles.Parameters.AddWithValue("@TipoConsulta", 1);
                commandDetalles.Parameters.AddWithValue("@Vendedores", Vendedores);
                commandDetalles.Parameters.AddWithValue("@Lineas", string.Empty);
                commandDetalles.Parameters.AddWithValue("@Cliente", Cliente);
                commandDetalles.Parameters.AddWithValue("@Articulo", string.Empty);
                commandDetalles.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                commandDetalles.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                commandDetalles.Parameters.AddWithValue("@Factura", Factura);
                commandDetalles.Parameters.AddWithValue("@Sucursales", Sucursales);
                commandDetalles.Parameters.AddWithValue("@GranCanales", string.Empty);
                commandDetalles.Parameters.AddWithValue("@Canales", string.Empty);
                commandDetalles.Parameters.AddWithValue("@JefasCobranza", JefasCobranza);
                commandDetalles.Parameters.AddWithValue("@Mes", string.Empty);
                commandDetalles.Parameters.AddWithValue("@Anio", string.Empty);
                commandDetalles.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                commandDetalles.Parameters.AddWithValue("@Precio", 0);
                commandDetalles.Parameters.AddWithValue("@Moneda", string.Empty);
                commandDetalles.CommandTimeout = 0;
                SqlDataAdapter adapterDetalles = new SqlDataAdapter();
                adapterDetalles.SelectCommand = commandDetalles;
                adapterDetalles.SelectCommand.CommandTimeout = 0;
                adapterDetalles.Fill(data, "TablaDetalles");

                DataRelation relation = new DataRelation("FacturaDetalle", data.Tables["TablaFactura"].Columns["Factura"], data.Tables["TablaDetalles"].Columns["Factura"]);
                data.Relations.Add(relation);

                masterBindingSource.DataSource = data;
                masterBindingSource.DataMember = "TablaFactura";
                detailsBindingSource.DataSource = masterBindingSource;
                detailsBindingSource.DataMember = "FacturaDetalle";
                gridExcel.DataSource = data.Tables["TablaFactura"];

               // gridCobranza.DataSource = data.Tables["TablaFactura"];

                if (gridCobranza.Columns.Count != 0)
                {
                    checkBox1.Enabled = true;
                    checkBox2.Enabled = true;
                }

                /*gridCobranza.DataSource = masterBindingSource;
                gridDetalles.DataSource = detailsBindingSource;*/
                DarFormatoADataGrid();
                DarFormatoGridDetalle();

                ColorearGrid();
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
        /// Evento que ocurre al hacer click en el btnExportar
        /// Llama a la clase que realiza el proceso
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel excel = new ExportarAExcel();
            if (excel.Exportar(gridCobranza, false) == true)
            {
                MessageBox.Show("El documento se creó correctamente.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ocurrió un error al crear el documento, no se creó el archivo.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                this.btnPresentar_Click(sender, e);
            }
            if ((int)e.KeyChar == (int)Keys.Escape)
            {
                this.Cobranza_Load(sender, e);
            }
        }

        #endregion


        #region FUNCIONES

        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoADataGrid()
        {/*
          *  Cliente, Nombre, Factura, FechaFactura, FechaVencimiento, TotalFactura, NCRealSugerida, AplicadaPE, AplicadoSAP, Diferencia1, 
            NCPPSugerida, AplicadoPP, Diferencia2, Saldo, Estatus, Docstatus
          */
            gridCobranza.Columns[(int)ColumnasGrid.DocStatus].Visible = false;
            gridCobranza.Columns[(int)ColumnasGrid.AplicadoSAPPE].Visible = false;
            gridCobranza.Columns[(int)ColumnasGrid.AplicadoSAPPP].Visible = false; 

            gridCobranza.Columns[(int)ColumnasGrid.Cliente].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.Cliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.Cliente].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.Nombre].Width = 250;
            gridCobranza.Columns[(int)ColumnasGrid.Nombre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridCobranza.Columns[(int)ColumnasGrid.Nombre].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.Factura].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.Factura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridCobranza.Columns[(int)ColumnasGrid.Factura].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.FechaFactura].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.FechaFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.FechaFactura].DefaultCellStyle.Format = "d";
            gridCobranza.Columns[(int)ColumnasGrid.FechaFactura].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.FechaVencimiento].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.FechaVencimiento].DefaultCellStyle.Format = "d";
            gridCobranza.Columns[(int)ColumnasGrid.FechaVencimiento].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.FechaVencimiento].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.TotalFactura].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.TotalFactura].DefaultCellStyle.Format = "C2";
            gridCobranza.Columns[(int)ColumnasGrid.TotalFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.TotalFactura].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.NCRealSugerida].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.NCRealSugerida].DefaultCellStyle.Format = "C2";
            gridCobranza.Columns[(int)ColumnasGrid.NCRealSugerida].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.NCRealSugerida].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.AplicadaPE].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.AplicadaPE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.AplicadaPE].ReadOnly = true;
            gridCobranza.Columns[(int)ColumnasGrid.AplicadaPE].HeaderText = "Aplicada";

            gridCobranza.Columns[(int)ColumnasGrid.Diferencia1].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.Diferencia1].DefaultCellStyle.Format = "C2";
            gridCobranza.Columns[(int)ColumnasGrid.Diferencia1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.Diferencia1].ReadOnly = true;
            gridCobranza.Columns[(int)ColumnasGrid.Diferencia1].HeaderText = "Diferencia";

            gridCobranza.Columns[(int)ColumnasGrid.NCPPSugerida].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.NCPPSugerida].DefaultCellStyle.Format = "C2";
            gridCobranza.Columns[(int)ColumnasGrid.NCPPSugerida].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.NCPPSugerida].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.AplicadoPP].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.AplicadoPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.AplicadoPP].ReadOnly = true;
            gridCobranza.Columns[(int)ColumnasGrid.AplicadoPP].HeaderText = "Aplicada";

            gridCobranza.Columns[(int)ColumnasGrid.Diferencia2].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.Diferencia2].DefaultCellStyle.Format = "C2";
            gridCobranza.Columns[(int)ColumnasGrid.Diferencia2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.Diferencia2].ReadOnly = true;
            gridCobranza.Columns[(int)ColumnasGrid.Diferencia2].HeaderText = "Diferencia";

            gridCobranza.Columns[(int)ColumnasGrid.Saldo].Width = 90;
            gridCobranza.Columns[(int)ColumnasGrid.Saldo].DefaultCellStyle.Format = "C2";
            gridCobranza.Columns[(int)ColumnasGrid.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGrid.Saldo].ReadOnly = true;

            gridCobranza.Columns[(int)ColumnasGrid.Estatus].Width = 85;
           
            gridCobranza.AllowUserToAddRows = false;
            gridCobranza.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridCobranza.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 54, 92);
            gridCobranza.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //gridCobranza.ColumnHeadersDefaultCellStyle.Font = new Font(gridCobranza.Font, FontStyle.Bold);

            gridCobranza.RowHeadersWidth = 40;
            gridCobranza.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 230, 241);
            gridCobranza.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            gridCobranza.AutoGenerateColumns = false;            
        }

        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoGridDetalle()
        {
            gridDetalles.Columns[(int)ColumnasGridDetalle.Factura].Visible = false;
            gridDetalles.Columns[(int)ColumnasGridDetalle.DocStatus].Visible = false;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Articulo].Width = 80;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Articulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGridDetalle.Articulo].ReadOnly = true;

            gridDetalles.Columns[(int)ColumnasGridDetalle.NombreArticulo].Width = 240;
            gridDetalles.Columns[(int)ColumnasGridDetalle.NombreArticulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridCobranza.Columns[(int)ColumnasGridDetalle.NombreArticulo].ReadOnly = true;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Linea].Width = 70;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Linea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridCobranza.Columns[(int)ColumnasGridDetalle.Linea].ReadOnly = true;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].Width = 70;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGridDetalle.Cantidad].ReadOnly = true;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Moneda].Width = 75;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Moneda].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Moneda].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGridDetalle.Moneda].ReadOnly = true;
            
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVenta].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVenta].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVenta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGridDetalle.PrecioVenta].ReadOnly = true;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].DefaultCellStyle.Format = "P2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGridDetalle.Descuento].ReadOnly = true;

            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioPP].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioPP].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGridDetalle.PrecioPP].ReadOnly = true;

            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVolumen].Width = 90;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVolumen].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVolumen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridCobranza.Columns[(int)ColumnasGridDetalle.PrecioVolumen].ReadOnly = true;
            
            gridDetalles.AllowUserToAddRows = false;
            gridDetalles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;            
           
            gridDetalles.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 54, 92);
            gridDetalles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
           // gridDetalles.ColumnHeadersDefaultCellStyle.Font = new Font(gridDetalles.Font, FontStyle.Bold);

            gridDetalles.RowHeadersWidth = 40;
            gridDetalles.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 230, 241);
            gridDetalles.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            gridDetalles.AutoGenerateColumns = false;

            //gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioPP].HeaderText = "Precio PP";
            //gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVolumen].HeaderText = "Precio Especial";

            gridDetalles.Columns[(int)ColumnasGridDetalle.Articulo].DisplayIndex = 0;
            gridDetalles.Columns[(int)ColumnasGridDetalle.NombreArticulo].DisplayIndex = 1;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Linea].DisplayIndex = 2;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].DisplayIndex = 3;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Moneda].DisplayIndex = 4;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVenta].DisplayIndex = 5;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVolumen].DisplayIndex = 6;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Descuento].DisplayIndex = 7;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioPP].DisplayIndex = 8;
            
        }

        /// <summary>
        /// Método que carga las sucursales en el cbSucursal
        /// </summary>
        private void CargarSucursales()
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

        /// <summary>
        /// Método que carga las sucursales en el cbSucursal
        /// </summary>
        private void CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
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

            clbCobranza.DataSource = table;
            clbCobranza.DisplayMember = "Nombre";
            clbCobranza.ValueMember = "Codigo";
        }

        /// <summary>
        /// Método que carga los Vendedores en el clbVendedor
        /// </summary>
        private void CargarVendedores()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
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

            clbVendedor.DataSource = table;
            clbVendedor.DisplayMember = "Nombre";
            clbVendedor.ValueMember = "Codigo";
        }

        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
        private void Esperar()
        {
            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
            gridCobranza.Focus();
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }
        
        private void ColorearGrid()
        {
            try
            {
                foreach (DataGridViewRow item in gridCobranza.Rows)
                {
                   
                    if (Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Diferencia1].Value) > 0)
                    {
                        item.Cells[(int)ColumnasGrid.Diferencia1].Style.ForeColor = Color.Red;
                    }
                    if (Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Diferencia2].Value) > 0)
                    {
                        item.Cells[(int)ColumnasGrid.Diferencia2].Style.ForeColor = Color.Red;

                    }

                    //item.Cells[(int)ColumnasGrid.Diferencia1].Style.BackColor = Color.FromArgb(235, 241, 222);
                    //item.Cells[(int)ColumnasGrid.AplicadaPE].Style.BackColor = Color.FromArgb(235, 241, 222);
                    //item.Cells[(int)ColumnasGrid.NCRealSugerida].Style.BackColor = Color.FromArgb(235, 241, 222);

                    //item.Cells[(int)ColumnasGrid.Diferencia2].Style.BackColor = Color.FromArgb(242, 242, 242);
                    //item.Cells[(int)ColumnasGrid.AplicadoPP].Style.BackColor = Color.FromArgb(242, 242, 242);
                    //item.Cells[(int)ColumnasGrid.NCPPSugerida].Style.BackColor = Color.FromArgb(242, 242, 242);
                }
            }catch(Exception)
            {
            }
        }
        #endregion

        private void gridCobranza_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ColorearGrid();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {//solo Abiertas
                try
                {
                    DataSet da = new DataSet();
                    BindingSource masterBindingSource = new BindingSource();
                    BindingSource detailsBindingSource = new BindingSource();


                    var encabezado = (from cob in data.Tables["TablaFactura"].AsEnumerable()
                                      where cob.Field<string>("DocStatus") == "O"
                                      select cob).ToList();
                    var detalles = (from cob in data.Tables["TablaDetalles"].AsEnumerable()
                                    where cob.Field<string>("DocStatus") == "O"
                                    select cob).ToList();

                    if (encabezado.Count() > 0 && detalles.Count() > 0)
                    {

                        DataTable tblEncabezado = encabezado.CopyToDataTable();
                        DataTable tblDetalle = detalles.CopyToDataTable();

                        tblEncabezado.TableName = "TablaFactura";
                        tblDetalle.TableName = "TablaDetalles";

                        da.Tables.Add(tblEncabezado);
                        da.Tables.Add(tblDetalle);

                        DataRelation relation = new DataRelation("FacturaDetalle", da.Tables["TablaFactura"].Columns["Factura"], da.Tables["TablaDetalles"].Columns["Factura"]);
                        da.Relations.Add(relation);

                        masterBindingSource.DataSource = da;
                        masterBindingSource.DataMember = "TablaFactura";
                        detailsBindingSource.DataSource = masterBindingSource;
                        detailsBindingSource.DataMember = "FacturaDetalle";
                        gridCobranza.DataSource = masterBindingSource;
                        gridDetalles.DataSource = detailsBindingSource;
                        gridExcel.DataSource = data.Tables["TablaFactura"];
                    }
                    else
                    {
                        gridCobranza.DataSource = null;
                        gridDetalles.DataSource = null;
                        MessageBox.Show("No se encontraron resultados", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

                Todo();
            }
        }

        public void Todo()
        {
            try
            {
                DataSet da1 = new DataSet();
                BindingSource masterBindingSource = new BindingSource();
                BindingSource detailsBindingSource = new BindingSource();

                var encabezado = (from cob in data.Tables["TablaFactura"].AsEnumerable()

                                  select cob).ToList();
                var detalles = (from cob in data.Tables["TablaDetalles"].AsEnumerable()

                                select cob).ToList();

                DataTable tblEncabezado = encabezado.CopyToDataTable();
                DataTable tblDetalle = detalles.CopyToDataTable();

                tblEncabezado.TableName = "TablaFactura";
                tblDetalle.TableName = "TablaDetalles";

                da1.Tables.Add(tblEncabezado);
                da1.Tables.Add(tblDetalle);

                DataRelation relation = new DataRelation("FacturaDetalle", da1.Tables["TablaFactura"].Columns["Factura"], da1.Tables["TablaDetalles"].Columns["Factura"]);
                da1.Relations.Add(relation);

                masterBindingSource.DataSource = da1;
                masterBindingSource.DataMember = "TablaFactura";
                detailsBindingSource.DataSource = masterBindingSource;
                detailsBindingSource.DataMember = "FacturaDetalle";
                gridCobranza.DataSource = masterBindingSource;
                gridDetalles.DataSource = detailsBindingSource;
                gridExcel.DataSource = data.Tables["TablaFactura"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                try
                {
                    DataSet ds = new DataSet();
                    gridCobranza.DataSource = null;
                    gridDetalles.DataSource = null;

                    var encabezado = (from cob in data.Tables["TablaFactura"].AsEnumerable()
                                      where cob.Field<bool>("Aplicada_PE") == false
                                            && cob.Field<decimal>("NC Real Sugerida") > 0
                                            && cob.Field<string>("DocStatus") == "O"
                                      select cob).CopyToDataTable();

                    var detalles = (from cob in data.Tables["TablaDetalles"].AsEnumerable()
                                    // where cob.Field<string>("DocStatus") == "O"
                                    select cob).CopyToDataTable();

                    var facturas = (from item in encabezado.AsEnumerable()
                                    select item.Field<Int32>("Factura")).ToArray();

                    var results = detalles.AsEnumerable().Where(z => facturas.Contains(z.Field<Int32>("Factura"))).CopyToDataTable();

                    //gridCobranza.DataSource = encabezado;
                    //gridDetalles.DataSource = results.CopyToDataTable();
                    encabezado.TableName = "TablaFactura";
                    results.TableName = "TablaDetalles";

                    ds.Tables.Add(encabezado);
                    ds.Tables.Add(results);

                    BindingSource masterBindingSource = new BindingSource();
                    BindingSource detailsBindingSource = new BindingSource();

                    gridCobranza.DataSource = masterBindingSource;
                    gridDetalles.DataSource = detailsBindingSource;

                    DataRelation relation = new DataRelation("FacturaDetalle", ds.Tables["TablaFactura"].Columns["Factura"], ds.Tables["TablaDetalles"].Columns["Factura"]);
                    ds.Relations.Add(relation);

                    masterBindingSource.DataSource = ds;
                    masterBindingSource.DataMember = "TablaFactura";
                    detailsBindingSource.DataSource = masterBindingSource;
                    detailsBindingSource.DataMember = "FacturaDetalle";
                    gridExcel.DataSource = data.Tables["TablaFactura"];

                    DarFormatoADataGrid();
                    DarFormatoGridDetalle();
                }
                catch (Exception)
                {
                }
            }
            else
            {
                Todo();
            }
        }

        private void Descargas_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void Descargas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
            }
        }

    }
}
