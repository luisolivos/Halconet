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

namespace Ventas.Desarrollo
{
    public partial class frmReporte : Form
    {
        Clases.Logs log;
        #region PARÁMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        public string Lineas;
        public string Producto;
        public string Cliente;
        public string Sucursales;
        public string Vendedores;
        public string GranCanales;
        public string Canales;
        public string FechaInicial;
        public string FechaFinal;
        public string NombreUsuario;
        public int RolUsuario;
        public int CodigoVendedor;
        public string Sucursal;

       

        /// <summary>
        /// Enumerador de las columnas del Grid
        /// </summary>
        private enum ColumnasGrid
        {
            PrecioVenta = 0,
            PrecioPorVolumen = 1,
            Utilidad = 2
        }

        /// <summary>
        /// Enumerador de las columnas del Grid
        /// </summary>
        private enum ColumnasGridDetalle
        {
            Fecha,
            Sucursal, 
            Vendedor, 
            Cliente,
            NombreCliente,
            Factura,
            Articulo, 
            NombreArticulo, 
            Linea,
            GranCanal, 
            Canal,
            Cantidad, 
            PrecioCompra,
            PrecioVenta, 
            PrecioPP, 
            PrecioVol, 
            TotalPrecioCompra,
            TotalPrecioVenta, 
            TotalPP, 
            TotalVolumen,
            PrecioVentaFinal,
            Utilidad
        }     

        #endregion


        #region EVENTOS

        public frmReporte(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();

            RolUsuario = rolUsuario;
            NombreUsuario = nombreUsuario;
            CodigoVendedor = codigoVendedor;
            //Sucursal = sucursal;
            Sucursal = sucursal;

            try
            {
                CargarLinea();
                CargarSucursales();
                CargarVendedores();
                CargarCanales();
                CargarGranCanales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Evento que ocurre al hacer click en el btnBuscar
        /// Muestra el reporte de acuerdo a los criterios de búsqueda
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                Producto = txtProducto.Text;
                //if (cbFiltarEjes.Checked) Producto = "'HJ-77.5F','KHD-77.5C','HLLND-EJE 77.5'";
                Cliente = txtCliente.Text;
                Lineas = GetCadena(clbLinea); //stbLineas.ToString();
                Sucursales = GetCadena(clbSucursal); //stbSucursales.ToString();
                gridReporte.DataSource = null;
                gridDetalles.DataSource = null;
                if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                {
                    Vendedores = GetCadena(clbVendedor);//stbVendedores.ToString();
                }

                GranCanales = GetCadena(clbGranCanal);//stbGranCanal.ToString();
                Canales = GetCadena(clbCanal);//stbCanal.ToString();
                FechaInicial = dtpFechaInicial.Value.Date.ToString("yyyy-MM-dd");
                FechaFinal = dtpFechaFinal.Value.Date.ToString("yyyy-MM-dd");



                SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);

                SqlCommand command = new SqlCommand("PJ_Utildad", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 3);

                command.Parameters.AddWithValue("@Vendedores", Vendedores.Trim(','));
                command.Parameters.AddWithValue("@Lineas", Lineas.Trim(','));
                command.Parameters.AddWithValue("@Cliente", Cliente);
                command.Parameters.AddWithValue("@Articulo", Producto);
                command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                command.Parameters.AddWithValue("@Factura", string.Empty);
                command.Parameters.AddWithValue("@Sucursales", Sucursales.Trim(','));
                command.Parameters.AddWithValue("@GranCanales", GranCanales.Trim(','));
                command.Parameters.AddWithValue("@Canales", Canales.Trim(','));
                command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                command.Parameters.AddWithValue("@Mes", string.Empty);
                command.Parameters.AddWithValue("@Anio", string.Empty);
                command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                command.Parameters.AddWithValue("@Precio", 0);
                command.Parameters.AddWithValue("@Moneda", string.Empty);
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                gridDetalles.DataSource = table;

                decimal totalPrecioCompra = 0;
                decimal totalPrecioVenta = 0;
                decimal totalPrecioCPP = 0;
                decimal totalPrecioPorVolumen = 0;
                //decimal totalPrecioEspecial = 0;
                decimal totalPrecioVentaFinal = 0;
                
                decimal utilidad = 0;
                foreach (DataRow row in table.Rows)
                {
                    totalPrecioVenta += row.Field<decimal>("SubTotal Precio Venta");
                    totalPrecioCPP += row.Field<decimal>("SubTotal CPP");
                    totalPrecioPorVolumen += row.Field<decimal>("SubTotal Precio Real");
                    totalPrecioVentaFinal += row.Field<decimal>("Precio Venta Final");
                    totalPrecioCompra += row.Field<decimal>("Subtotal Precio Compra");
                }

                if (totalPrecioVentaFinal != 0)
                {
                    utilidad = ((decimal.Parse(totalPrecioCompra.ToString()) / totalPrecioVentaFinal) - 1) * -1;
                }
                else
                {
                    utilidad = 0;
                }
                
                DataTable tabla = new DataTable("TablaTotales");

                tabla.Columns.Add("Precio Venta");
                tabla.Columns.Add("Precio Real");
                tabla.Columns.Add("Utilidad");
                DataRow registro = tabla.NewRow();
                registro["Precio Venta"] = totalPrecioVenta.ToString("c");
                registro["Precio Real"] = totalPrecioPorVolumen.ToString("c");
                registro["Utilidad"] = utilidad.ToString("P2");
                tabla.Rows.Add(registro);
                gridReporte.DataSource = tabla;

                DarFormatoGridPrincipal();
                DarFormatoGrid();
                gridDetalles.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
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
        private void clbGranCanal_Click(object sender, EventArgs e)
        {
            if (clbGranCanal.SelectedIndex == 0)
            {
                if (clbGranCanal.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbGranCanal.Items.Count; item++)
                    {
                        clbGranCanal.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbGranCanal.Items.Count; item++)
                    {
                        clbGranCanal.SetItemChecked(item, true);
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
        private void clbCanal_Click(object sender, EventArgs e)
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
        /// Evento que ocurre al completar el enlace de datos en el gridDetalles
        /// Pinta (rojo) la columna Utilidad si es menor a %10 y pinta todo el cuadro si es negativo
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void gridDetalles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridDetalles.Rows)
                {
                    if (Convert.ToInt32(row.Cells[(int)ColumnasGridDetalle.TotalVolumen].Value) == 0)
                    {
                        row.Cells[(int)ColumnasGridDetalle.TotalVolumen].Style.ForeColor = Color.Red;
                    }
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < (decimal)0.13 && Convert.ToString(row.Cells[(int)ColumnasGridDetalle.GranCanal].Value) == "Mayoreo")
                    {
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Yellow;
                    }
                    else
                        if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < (decimal)0.16 && Convert.ToString(row.Cells[(int)ColumnasGridDetalle.GranCanal].Value) == "Transporte")
                        {
                            row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
                            row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Yellow;
                        }
                        else
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < (decimal)0.13 && Convert.ToString(row.Cells[(int)ColumnasGridDetalle.GranCanal].Value) == "Armadores")
                    {
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Yellow;
                    }
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGridDetalle.Utilidad].Value) < 0)
                    {
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.ForeColor = Color.Black;
                        row.Cells[(int)ColumnasGridDetalle.Utilidad].Style.BackColor = Color.Red;
                    }
                }

            }
            catch (Exception)
            {
                // MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ClasesSGUV.Exportar excel = new ClasesSGUV.Exportar();
            if (excel.ExportarSinFormato(gridDetalles) == true)
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
                this.btnBuscar_Click(sender, e);                
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                Reporte_Load(sender, e);
            }

        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            //this.MaximizeBox = false;
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.Restricciones();
            
            txtProducto.Clear();
            txtCliente.Clear();
            dtpFechaFinal.Value = DateTime.Now;
            dtpFechaInicial.Value = DateTime.Now;

            gridDetalles.DataSource = null;
            gridReporte.DataSource = null;

            //cbFiltarEjes.Visible = ClasesSGUV.Login.Rol == 1 || ClasesSGUV.Login.Rol == 2;
        }    
        #endregion


        #region FUNCIONES

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
        ///</sumary>
        
        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoGridPrincipal()
        {

            gridReporte.Columns[(int)ColumnasGrid.PrecioVenta].Width = 90;
            gridReporte.Columns[(int)ColumnasGrid.PrecioVenta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridReporte.Columns[(int)ColumnasGrid.PrecioVenta].DefaultCellStyle.Format = "C2";


            gridReporte.Columns[(int)ColumnasGrid.PrecioPorVolumen].Width = 90;
            gridReporte.Columns[(int)ColumnasGrid.PrecioPorVolumen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridReporte.Columns[(int)ColumnasGrid.PrecioPorVolumen].DefaultCellStyle.Format = "C2";

            gridReporte.Columns[(int)ColumnasGrid.Utilidad].Width = 90;
            gridReporte.Columns[(int)ColumnasGrid.Utilidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridReporte.Columns[(int)ColumnasGrid.Utilidad].DefaultCellStyle.Format = "P2";

            gridReporte.AllowUserToAddRows = false;
            gridReporte.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 54, 92);
            gridReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridReporte.ColumnHeadersDefaultCellStyle.Font = new Font(gridReporte.Font, FontStyle.Bold);

            gridReporte.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 230, 241);
            gridReporte.RowHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoGrid()
        {
            
            gridDetalles.Columns[(int)ColumnasGridDetalle.Fecha].Width = 80;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Fecha].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Sucursal].Width = 80;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Sucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Vendedor].Width = 150;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Vendedor].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Cliente].Width = 70;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Cliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.NombreCliente].Width = 150;
            gridDetalles.Columns[(int)ColumnasGridDetalle.NombreCliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Factura].Width = 70;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Factura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Articulo].Width = 70;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Articulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.NombreArticulo].Width = 150;
            gridDetalles.Columns[(int)ColumnasGridDetalle.NombreArticulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Linea].Width = 70;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Linea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.GranCanal].Width = 70;
            gridDetalles.Columns[(int)ColumnasGridDetalle.GranCanal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Canal].Width = 80;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Canal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].Width = 80;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVenta].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVenta].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVenta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioPP].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioPP].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVol].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVol].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVol].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPrecioCompra].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPrecioCompra].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPrecioCompra].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPrecioCompra].Visible = false;

            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPrecioVenta].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPrecioVenta].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPrecioVenta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPP].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPP].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalVolumen].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalVolumen].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.TotalVolumen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVentaFinal].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVentaFinal].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVentaFinal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioVentaFinal].Visible = false;

            gridDetalles.Columns[(int)ColumnasGridDetalle.Utilidad].Width = 85;
            gridDetalles.Columns[(int)ColumnasGridDetalle.Utilidad].DefaultCellStyle.Format = "P2";
            gridDetalles.Columns[(int)ColumnasGridDetalle.Utilidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.AllowUserToAddRows = false;
            gridDetalles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridDetalles.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 54, 92);
            gridDetalles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridDetalles.ColumnHeadersDefaultCellStyle.Font = new Font(gridDetalles.Font, FontStyle.Bold);

            gridDetalles.RowHeadersWidth = 40;
            gridDetalles.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 230, 241);
            gridDetalles.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas
                || (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                gridDetalles.Columns[(int)ColumnasGridDetalle.PrecioCompra].Visible = false;
                gridDetalles.Columns[(int)ColumnasGridDetalle.TotalPrecioCompra].Visible = false;
            }
        }

        /// <summary>
        /// Método que carga las sucursales
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
        /// Método que carga los Vendedores
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

        /// <summary>
        /// Método que carga las Lineas
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

        /// <summary>
        /// Método que carga los Canales
        /// </summary>
        private void CargarCanales()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Canal);
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


        /// <summary>
        /// Método que carga los Gran Canales
        /// </summary>
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

            clbGranCanal.DataSource = table;
            clbGranCanal.DisplayMember = "Nombre";
            clbGranCanal.ValueMember = "Codigo";
        }

        private string GetCadena(CheckedListBox clb) {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (DataRowView item in clb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (count > 0)
                        sb.Append(",");
                    sb.Append(item["Codigo"].ToString());
                    count++;
                }
            }
            return sb.ToString();
        }

        public void Restricciones()
        {
            
            //Rol Vendedor
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                //ocultat sucursalesa
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                //Point l1 = new Point(label10.Location.X, label10.Location.Y);
                //Point c1 = new Point(clbLinea.Location.X, clbLinea.Location.Y);

                //Point l2 = new Point(lblSucursal.Location.X, lblSucursal.Location.Y);
                //Point c2 = new Point(clbSucursal.Location.X, clbSucursal.Location.Y);

                //Point l3 = new Point(lblVendedor.Location.X, lblVendedor.Location.Y);
                //Point c3 = new Point(clbVendedor.Location.X, clbVendedor.Location.Y);

                //lblVendedor.Location = l1;
                //clbVendedor.Location = c1;

                //label10.Location = l2;
                //clbLinea.Location = c2;

                //lblCanal.Location = l3;
                //clbCanal.Location = c3;
            }

           
            //Rol Ventas Especial
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {

                //label10.Location = new Point(lblSucursal.Location.X, lblSucursal.Location.Y);
                //clbLinea.Location = new Point(clbSucursal.Location.X, clbSucursal.Location.Y);

                //lblCanal.Location = new Point(label10.Location.X, label10.Location.Y);
                //clbCanal.Location = new Point(clbLinea.Location.X, clbLinea.Location.Y);

                lblVendedor.Visible = false;
                clbVendedor.Visible = false;
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                Vendedores = "," + CodigoVendedor.ToString();
            }

        }
        #endregion       

        private void Reporte_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void Reporte_FormClosing(object sender, FormClosingEventArgs e)
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
