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
using System.Globalization;
using System.Threading;
using System.Reflection;


namespace Ventas.Desarrollo
{
    public partial class UtilidadSucursales : Form
    {
        Clases.Logs log;

        public UtilidadSucursales(int rolusuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();

            RolUsuario = rolusuario;
            NombreUsuario = nombreUsuario;
            CodigoVendedor = codigoVendedor;
            Sucursal = sucursal;
        }

        private void UtilidadLineas_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.MaximizeBox = true;
            try
            {
                Restricciones();
                CargarVendedores();
                CargarLinea();
                CargarSucursales();
                CargarGranCanales();
                int act = DateTime.Now.Month;
                int ant_i = DateTime.Now.AddMonths(-3).Month;
                int ant_f = DateTime.Now.AddMonths(-1).Month;
                DateTimeFormatInfo fecha = CultureInfo.CurrentCulture.DateTimeFormat;

                string actual = fecha.GetMonthName(DateTime.Now.Month);
                string atrasado = fecha.GetMonthName(ant_i) + "-" + fecha.GetMonthName(ant_f);

                lblVenta.Text += " " + actual;
                lblUtilidad.Text += " " + actual;

                txtCliente.Clear();
                txtArticulo.Clear();
                txtCliente.Focus();
                txtVenta.Clear();
                txtUtilidadEstimada.Clear();

                dgvSucursal.DataSource = null;
                dgvVendedor.DataSource = null;

                //clbSucursal.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region PARAMETROS
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

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
        public DataSet data;
        public int CodigoVendedor;
        public string Sucursal;
        public enum Consultas
        {
            Lineas = 1,
            Articulos = 2,
            Clientes = 3
        }

        public enum ColumnasVendedor
        {
            Susucursal, Vendedor, Venta, Utilidad
        }

        public enum ColumnasSucursal
        {
           Sucursal,Compra, Venta, Utilidad
        }

        public enum ColumnasCanal
        {
            Canal, Venta, Utilidad
        }

        #endregion

        #region METODOS

        public void Restricciones()
        {
            //Rol Vendedor
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                //ocultat sucursalesa
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
            }

            //Rol Ventas Especial
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                lblVendedor.Visible = false;
                clbVendedor.Visible = false;
                Vendedores = "," + CodigoVendedor.ToString();
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

                clbSucursales.DataSource = table;
                clbSucursales.DisplayMember = "Nombre";
                clbSucursales.ValueMember = "Codigo";
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

                clbSucursales.DataSource = table;
                clbSucursales.DisplayMember = "Nombre";
                clbSucursales.ValueMember = "Codigo";
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
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal )
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

        public string GetCadena(CheckedListBox cb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in cb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!cb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                }
            }
            if (cb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in cb.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!cb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append(item["Codigo"].ToString());
                    }
                }
            }
            return stb.ToString();
        }

        public void FormatoSucursal()
        {
            dgvSucursal.Columns[(int)ColumnasSucursal.Compra].Visible = false;

            dgvSucursal.Columns[(int)ColumnasSucursal.Sucursal].Width = 150;
            //dgvSucursal.Columns[(int)ColumnasSucursal.Compra].Width = 90;
            //dgvSucursal.Columns[(int)ColumnasSucursal.Venta].Width = 90;
            //dgvSucursal.Columns[(int)ColumnasSucursal.Utilidad].Width = 90;

            dgvSucursal.Columns[(int)ColumnasSucursal.Compra].DefaultCellStyle.Format = "C2";
            dgvSucursal.Columns[(int)ColumnasSucursal.Venta].DefaultCellStyle.Format = "C2";
            dgvSucursal.Columns[(int)ColumnasSucursal.Utilidad].DefaultCellStyle.Format = "P2";

            dgvSucursal.Columns[(int)ColumnasSucursal.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSucursal.Columns[(int)ColumnasSucursal.Utilidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
         }

        public void FormatoVendedor()
        {
            dgvVendedor.Columns[(int)ColumnasVendedor.Susucursal].Visible = false;
            dgvVendedor.Columns[(int)ColumnasVendedor.Vendedor].Width = 200;
            //dgvVendedor.Columns[(int)ColumnasVendedor.Venta].Width = 100;
            //dgvVendedor.Columns[(int)ColumnasVendedor.Utilidad].Width = 100;

            dgvVendedor.Columns[(int)ColumnasSucursal.Venta].DefaultCellStyle.Format = "C2";
            dgvVendedor.Columns[(int)ColumnasSucursal.Utilidad].DefaultCellStyle.Format = "P2";

            dgvVendedor.Columns[(int)ColumnasSucursal.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvVendedor.Columns[(int)ColumnasSucursal.Utilidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void FormatoCanal()
        {
            dgvCanal.Columns[(int)ColumnasCanal.Venta].DefaultCellStyle.Format = "C2";
            dgvCanal.Columns[(int)ColumnasCanal.Utilidad].DefaultCellStyle.Format = "P2";
            
            dgvCanal.Columns[(int)ColumnasCanal.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCanal.Columns[(int)ColumnasCanal.Utilidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

        SqlDataAdapter adapterClientes = new SqlDataAdapter();

        #endregion

        #region EVENTOS

        //public string GetCadena(CheckedListBox clb)
        //{
        //    StringBuilder stb = new StringBuilder();
        //    foreach (DataRowView item in clb.CheckedItems)
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
        //    if (clb.CheckedItems.Count == 0)
        //    {
        //        foreach (DataRowView item in clb.Items)
        //        {
        //            if (item["Codigo"].ToString() != "0")
        //            {
        //                if (!clb.ToString().Equals(string.Empty))
        //                {
        //                    stb.Append(",");
        //                }
        //                stb.Append(item["Codigo"].ToString());
        //            }
        //        }
        //    }

        //    return stb.ToString().Trim(',');
        //}

        private void clbSucursales_Click(object sender, EventArgs e)
        {
            if (clbSucursales.SelectedIndex == 0)
            {
                if (clbSucursales.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbSucursales.Items.Count; item++)
                    {
                        clbSucursales.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbSucursales.Items.Count; item++)
                    {
                        clbSucursales.SetItemChecked(item, true);
                    }
                }
            }
        }

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

        DataTable tablaprincipal = new DataTable();
        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                tablaprincipal.Clear();
                Esperar();
                
                dgvSucursal.DataSource = null;
                dgvVendedor.DataSource = null;
                
                txtVenta.Clear();
                txtUtilidadEstimada.Clear();

                //DataTable tablaarticulos = new DataTable();
                Sucursales = this.GetCadena(clbSucursales);
                Lineas = GetCadena(clbLinea);
                Articulo = txtArticulo.Text.Trim();
                if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                    Vendedores = GetCadena(clbVendedor);
                Cliente = txtCliente.Text.Trim();

                FechaInicial = dtpDesde.Value.ToString("yyyy-MM-dd");
                string FechaFinal = dtpHasta.Value.ToString("yyyy-MM-dd");
                Canales = GetCadena(clbCanal);

                //Datos
                SqlCommand command2 = new SqlCommand("PJ_Utildad", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command2.CommandType = CommandType.StoredProcedure;
                command2.Parameters.AddWithValue("@TipoConsulta", 5);
                command2.Parameters.AddWithValue("@Sucursales", Sucursales.Trim(','));
                command2.Parameters.AddWithValue("@Lineas", Lineas.Trim(','));
                command2.Parameters.AddWithValue("@Articulo", Articulo);
                command2.Parameters.AddWithValue("@Factura", string.Empty);
                command2.Parameters.AddWithValue("@Vendedores", Vendedores.Trim(','));
                command2.Parameters.AddWithValue("@Cliente", Cliente);
                command2.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                command2.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                command2.Parameters.AddWithValue("@GranCanales", Canales.Trim(','));

               
                adapterClientes.SelectCommand = command2;
                adapterClientes.SelectCommand.CommandTimeout = 0;
                adapterClientes.Fill(tablaprincipal);
                //gridLineas.DataSource = tablaprincipal;       
                ////////////grid Articulos
                //List<Clases.Articulos> list_articulos = new List<Clases.Articulos>();
                var list_vendor = (from item in tablaprincipal.AsEnumerable()
                                  group item by new
                                  {
                                      ID = item.Field<string>("Sucursal"),
                                      Vendedor = item.Field<string>("SlpName")
                                  } into grouped
                                  select new 
                                  {
                                      ID = grouped.Key.ID,
                                      Vendedor = grouped.Key.Vendedor,
                                      Venta = grouped.Sum(ix => ix.Field<decimal?>("PrecioVentaFinal")),
                                      Utilidad = (grouped.Sum(ix => ix.Field<decimal?>("PrecioVentaFinal"))) == 0 ? 0 : 1 - ((grouped.Sum(iy => iy.Field<decimal?>("PrecioCompraMXN"))) / (grouped.Sum(iy => iy.Field<decimal?>("PrecioVentaFinal"))))
                                  }).ToList();
                //gridArticulos.DataSource = list_articulos; 
                //grid Lineas

                var list_sucursal = (from item in tablaprincipal.AsEnumerable()
                                   group item by new
                                   {
                                       ID = item.Field<string>("Sucursal"),
                                   } into grouped
                                   select new
                                   {
                                       Sucursal = grouped.Key.ID,
                                       Compra = grouped.Sum(ix => ix.Field<decimal?>("PrecioCompraMXN")),
                                       Venta = grouped.Sum(ix => ix.Field<decimal?>("PrecioVentaFinal")),
                                       Utilidad = (grouped.Sum(ix => ix.Field<decimal?>("PrecioVentaFinal"))) == 0 ? 0 : 1 - ((grouped.Sum(iy => iy.Field<decimal?>("PrecioCompraMXN"))) / (grouped.Sum(iy => iy.Field<decimal?>("PrecioVentaFinal")))),
                                   }).ToList();

                //gridLineas.DataSource = list_lineas;

                ////tablas
                //DataTable tblClietes = Clases.ListConverter.ToDataTable(list_clientes);
                DataTable tblSucursal = Clases.ListConverter.ToDataTable(list_sucursal);
                DataTable tblVendor = Clases.ListConverter.ToDataTable(list_vendor);

                BindingSource lineasBindingSource = new BindingSource();
                BindingSource articulosBindingSource = new BindingSource();
                //BindingSource clientesBindingsource = new BindingSource();

                tblSucursal.TableName = "TablaSucursal";
                //tblClietes.TableName = "TablaClientes";
                tblVendor.TableName = "TablaVendor";

                data = new DataSet();
                data.Tables.Add(tblVendor);
                data.Tables.Add(tblSucursal);
                DataRelation relation = new DataRelation("Sucursal-Vendor", data.Tables["TablaSucursal"].Columns["Sucursal"], data.Tables["TablaVendor"].Columns["ID"]);
                data.Relations.Add(relation);

                lineasBindingSource.DataSource = data;
                lineasBindingSource.DataMember = "TablaSucursal";
                articulosBindingSource.DataSource = lineasBindingSource;
                articulosBindingSource.DataMember = "Sucursal-Vendor";

                dgvSucursal.DataSource = lineasBindingSource;
                dgvVendedor.DataSource = articulosBindingSource;
                gridExcel.DataSource = tablaprincipal;

                this.FormatoSucursal();
                this.FormatoVendedor();

                decimal venta = decimal.Zero;
                decimal compra = decimal.Zero;
                decimal utilidad = decimal.Zero;

                venta = Convert.ToDecimal(tblSucursal.Compute("SUM(Venta)", string.Empty) == DBNull.Value ? decimal.Zero : tblSucursal.Compute("SUM(Venta)", string.Empty));
                compra = Convert.ToDecimal(tblSucursal.Compute("SUM(Compra)", string.Empty) == DBNull.Value ? decimal.Zero : tblSucursal.Compute("SUM(Compra)", string.Empty));

                utilidad = compra == decimal.Zero ? 1 : (1 - (compra / venta));

                txtVenta.Text = venta.ToString("C2");
                txtUtilidadEstimada.Text = utilidad.ToString("P2");

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

        private void btnExportar_Click(object sender, EventArgs e)
        {
            Esperar();
            ExportarAExcel ex = new ExportarAExcel();
            if (ex.ExportarCobranza(gridExcel))
                MessageBox.Show("El Archivo se creo con exito.");

            Continuar();
        }


        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                btnPresentar_Click(sender, e);
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                UtilidadLineas_Load(sender, e);
            }
        }
        #endregion

        private void form_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void gridArticulos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                //foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                //{

                //    if (item.Cells[(int)ColumnasCanal.Col1].Value.ToString() == "Y")
                //    {
                //        item.Cells[(int)ColumnasCanal.CodigoArticulo].Style.BackColor = Color.Red;
                //        item.Cells[(int)ColumnasCanal.CodigoArticulo].Style.ForeColor = Color.White;
                //    }

                //}
            }
            catch (Exception)
            {
                
            }
        }

        private void gridArticulos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string vendor = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasVendedor.Vendedor].Value.ToString();
               //string linea = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasGridArticulos.Col1].Value.ToString();


                var list_facturas = (from item in tablaprincipal.AsEnumerable()
                                     where item.Field<string>("SlpName") == vendor
                                     group item by new
                                     {
                                         Canal = item.Field<string>("GranCanal")
                                     } into grouped
                                     select new
                                     {
                                         Canal = grouped.Key.Canal,
                                         //Precio_Compra = grouped.Sum(ix => ix.Field<decimal?>("PrecioCompra")),
                                         Venta = grouped.Sum(ix => ix.Field<decimal?>("PrecioVentaFinal")),
                                         //Precio_Real = grouped.Sum(ix => ix.Field<decimal?>("PrecioEspecial")),
                                         Utilidad = (grouped.Sum(ix => ix.Field<decimal?>("PrecioVentaFinal"))) == 0 ? 0 : 1 - ((grouped.Sum(iy => iy.Field<decimal?>("PrecioCompraMXN"))) / (grouped.Sum(iy => iy.Field<decimal?>("PrecioVentaFinal"))))
                                     }).ToList();

                dgvCanal.Columns.Clear();

                DataTable tblFra = Clases.ListConverter.ToDataTable(list_facturas);

                dgvCanal.DataSource = tblFra;

                this.FormatoCanal();
            }
            catch (Exception)
            { }

        }
    }
}
