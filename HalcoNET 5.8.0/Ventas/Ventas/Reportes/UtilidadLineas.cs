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


namespace Ventas
{
    public partial class UtilidadLineas : Form
    {
        Clases.Logs log;

        public UtilidadLineas(int rolusuario, int codigoVendedor, string nombreUsuario, string sucursal)
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

                lblCompra.Text += " " + actual;
                lblVenta.Text += " " + actual;
                lblUtilidad.Text += " " + actual;

                txtCliente.Clear();
                txtArticulo.Clear();
                txtCliente.Focus();
                txtVenta.Clear();
                txtUtilidadEstimada.Clear();
                txtVentaEstimada.Clear();

                gridClientes.DataSource = null;
                gridLineas.DataSource = null;
                gridArticulos.DataSource = null;

                clbSucursal.SelectedIndex = 0;
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

        public enum ColumnasGridClientes
        {
            ClaveCliente, Cliente, PromedioAtrasado, UtilidadAtrasado, VentaActual, UtilidadEstimada,VentaEstimada, PromedioCompraActual
        }

        public enum ColumnasGridLinea
        {
            ID, ClaveCliente, Linea, PromedioAtrasado, UtilidadAtrasado, VentaActual, UtilidadEstimada, VentaEstimada
        }

        public enum ColumnasGridArticulos
        {
            ID, Cliente, CodigoArticulo, NombreArticulo, PromedioAtrasado, UtilidadAtrasada, VentaActual, UtilidadEstimada, VentaEstimada
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

        public void DarFormatoGridCliente()
        {
            int act = DateTime.Now.Month;
            int ant_i = DateTime.Now.AddMonths(-3).Month;
            int ant_f = DateTime.Now.AddMonths(-1).Month;
            DateTimeFormatInfo fecha = CultureInfo.CurrentCulture.DateTimeFormat;

            string actual = fecha.GetMonthName(DateTime.Now.Month);
            lblUtilidad.Text = "Utilidad " + actual;
            string atrasado = fecha.GetMonthName(ant_i) + "-" + fecha.GetMonthName(ant_f);

            gridClientes.Columns[(int)ColumnasGridClientes.PromedioCompraActual].Visible = false;

            gridClientes.Columns[(int)ColumnasGridClientes.ClaveCliente].Width = 70;
            gridClientes.Columns[(int)ColumnasGridClientes.Cliente].Width = 250;
            gridClientes.Columns[(int)ColumnasGridClientes.PromedioAtrasado].Width = 100;
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadAtrasado].Width = 100;
            gridClientes.Columns[(int)ColumnasGridClientes.VentaActual].Width = 100;
            gridClientes.Columns[(int)ColumnasGridClientes.VentaEstimada].Width = 100;
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadEstimada].Width = 100;

            gridClientes.Columns[(int)ColumnasGridClientes.ClaveCliente].HeaderText = "Codigo Cliente";
            gridClientes.Columns[(int)ColumnasGridClientes.Cliente].HeaderText = "Nombre Cliente";
            gridClientes.Columns[(int)ColumnasGridClientes.PromedioAtrasado].HeaderText = "Promedio Venta " + atrasado;
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadAtrasado].HeaderText = "Utilidad " + atrasado;
            gridClientes.Columns[(int)ColumnasGridClientes.VentaActual].HeaderText = "Venta " + actual;
            gridClientes.Columns[(int)ColumnasGridClientes.VentaEstimada].HeaderText = "Venta Estimada " + actual;
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadEstimada].HeaderText = "Utilidad " + actual;

            gridClientes.Columns[(int)ColumnasGridClientes.PromedioAtrasado].DefaultCellStyle.Format = "C2";
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadAtrasado].DefaultCellStyle.Format = "P2";
            gridClientes.Columns[(int)ColumnasGridClientes.VentaActual].DefaultCellStyle.Format = "C2";
            gridClientes.Columns[(int)ColumnasGridClientes.VentaEstimada].DefaultCellStyle.Format = "C2";
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadEstimada].DefaultCellStyle.Format = "P2";


            gridClientes.Columns[(int)ColumnasGridClientes.PromedioAtrasado].DefaultCellStyle.NullValue = "0";
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadAtrasado].DefaultCellStyle.NullValue = "0";
            gridClientes.Columns[(int)ColumnasGridClientes.VentaActual].DefaultCellStyle.NullValue = "0";
            gridClientes.Columns[(int)ColumnasGridClientes.VentaEstimada].DefaultCellStyle.NullValue = "0";
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadEstimada].DefaultCellStyle.NullValue = "0";

            gridClientes.Columns[(int)ColumnasGridClientes.PromedioAtrasado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadAtrasado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridClientes.VentaActual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridClientes.VentaEstimada].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridClientes.Columns[(int)ColumnasGridClientes.UtilidadEstimada].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public void DarFormatoGridLineas()
        {
            int act = DateTime.Now.Month;
            int ant_i = DateTime.Now.AddMonths(-3).Month;
            int ant_f = DateTime.Now.AddMonths(-1).Month;
            DateTimeFormatInfo fecha = CultureInfo.CurrentCulture.DateTimeFormat;

            string actual = fecha.GetMonthName(DateTime.Now.Month);
            string atrasado = fecha.GetMonthName(ant_i) + "-" + fecha.GetMonthName(ant_f);

            gridLineas.Columns[(int)ColumnasGridLinea.ID].Visible = false;
            gridLineas.Columns[(int)ColumnasGridLinea.ClaveCliente].Visible = false;

            gridLineas.Columns[(int)ColumnasGridLinea.PromedioAtrasado].Width = 120;
            gridLineas.Columns[(int)ColumnasGridLinea.Linea].Width = 120;
            gridLineas.Columns[(int)ColumnasGridLinea.PromedioAtrasado].Width = 120;
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadAtrasado].Width = 120;
            gridLineas.Columns[(int)ColumnasGridLinea.VentaActual].Width = 120;
            gridLineas.Columns[(int)ColumnasGridLinea.VentaEstimada].Width = 120;
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadEstimada].Width = 120;

            gridLineas.Columns[(int)ColumnasGridLinea.Linea].DefaultCellStyle.Font = new Font(gridLineas.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular);
            gridLineas.Columns[(int)ColumnasGridLinea.PromedioAtrasado].DefaultCellStyle.Font = new Font(gridLineas.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular); ;
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadAtrasado].DefaultCellStyle.Font = new Font(gridLineas.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular); ;
            gridLineas.Columns[(int)ColumnasGridLinea.VentaActual].DefaultCellStyle.Font = new Font(gridLineas.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular); ;
            gridLineas.Columns[(int)ColumnasGridLinea.VentaEstimada].DefaultCellStyle.Font = new Font(gridLineas.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular); ;
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadEstimada].DefaultCellStyle.Font = new Font(gridLineas.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular); ;

            gridLineas.Columns[(int)ColumnasGridLinea.Linea].HeaderText = "Linea";
            gridLineas.Columns[(int)ColumnasGridLinea.PromedioAtrasado].HeaderText = "Promedio Venta " + atrasado;
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadAtrasado].HeaderText = "Utilidad " + atrasado;
            gridLineas.Columns[(int)ColumnasGridLinea.VentaActual].HeaderText = "Venta " + actual;
            gridLineas.Columns[(int)ColumnasGridLinea.VentaEstimada].HeaderText = "Venta Estimada " + actual;
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadEstimada].HeaderText = "Utilidad " + actual;

            gridLineas.Columns[(int)ColumnasGridLinea.PromedioAtrasado].DefaultCellStyle.Format = "C2";
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadAtrasado].DefaultCellStyle.Format = "P2";
            gridLineas.Columns[(int)ColumnasGridLinea.VentaActual].DefaultCellStyle.Format = "C2";
            gridLineas.Columns[(int)ColumnasGridLinea.VentaEstimada].DefaultCellStyle.Format = "C2";
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadEstimada].DefaultCellStyle.Format = "P2";

            gridLineas.Columns[(int)ColumnasGridLinea.PromedioAtrasado].DefaultCellStyle.NullValue = "$0.0";
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadAtrasado].DefaultCellStyle.NullValue = "$0.0";
            gridLineas.Columns[(int)ColumnasGridLinea.VentaActual].DefaultCellStyle.NullValue = "$0.0";
            gridLineas.Columns[(int)ColumnasGridLinea.VentaEstimada].DefaultCellStyle.NullValue = "$0.0";
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadEstimada].DefaultCellStyle.NullValue = "$0.0";

            gridLineas.Columns[(int)ColumnasGridLinea.PromedioAtrasado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadAtrasado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGridLinea.VentaActual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGridLinea.VentaEstimada].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGridLinea.UtilidadEstimada].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void DarFormatoGridArticulos(DataGridView dgv)
        {
            int act = DateTime.Now.Month;
            int ant_i = DateTime.Now.AddMonths(-3).Month;
            int ant_f = DateTime.Now.AddMonths(-1).Month;
            DateTimeFormatInfo fecha = CultureInfo.CurrentCulture.DateTimeFormat;

            string actual = fecha.GetMonthName(DateTime.Now.Month);
            string atrasado = fecha.GetMonthName(ant_i) + "-" + fecha.GetMonthName(ant_f);

            dgv.Columns[(int)ColumnasGridArticulos.ID].Visible = false;
            dgv.Columns[(int)ColumnasGridArticulos.Cliente].Visible = false;

            dgv.Columns[(int)ColumnasGridArticulos.CodigoArticulo].Width = 100;
            dgv.Columns[(int)ColumnasGridArticulos.NombreArticulo].Width = 250;
            dgv.Columns[(int)ColumnasGridArticulos.PromedioAtrasado].Width = 100;
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadAtrasada].Width = 95;
            dgv.Columns[(int)ColumnasGridArticulos.VentaActual].Width = 100;
            dgv.Columns[(int)ColumnasGridArticulos.VentaEstimada].Width = 100;
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadEstimada].Width = 95;

            dgv.Columns[(int)ColumnasGridArticulos.CodigoArticulo].DefaultCellStyle.Font = new Font(gridArticulos.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular);
            dgv.Columns[(int)ColumnasGridArticulos.NombreArticulo].DefaultCellStyle.Font = new Font(gridArticulos.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular);
            dgv.Columns[(int)ColumnasGridArticulos.PromedioAtrasado].DefaultCellStyle.Font = new Font(gridArticulos.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular);
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadAtrasada].DefaultCellStyle.Font = new Font(gridArticulos.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular);
            dgv.Columns[(int)ColumnasGridArticulos.VentaActual].DefaultCellStyle.Font = new Font(gridArticulos.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular);
            dgv.Columns[(int)ColumnasGridArticulos.VentaEstimada].DefaultCellStyle.Font = new Font(gridArticulos.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular);
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadEstimada].DefaultCellStyle.Font = new Font(gridArticulos.DefaultCellStyle.Font.FontFamily, 9, FontStyle.Regular);

            dgv.Columns[(int)ColumnasGridArticulos.CodigoArticulo].HeaderText = "Codigo Artículo";
            dgv.Columns[(int)ColumnasGridArticulos.NombreArticulo].HeaderText = "Nombre Artículo";
            dgv.Columns[(int)ColumnasGridArticulos.PromedioAtrasado].HeaderText = "Promedio Venta " + atrasado;
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadAtrasada].HeaderText = "Utilidad " + atrasado;
            dgv.Columns[(int)ColumnasGridArticulos.VentaActual].HeaderText = "Venta " + actual;
            dgv.Columns[(int)ColumnasGridArticulos.VentaEstimada].HeaderText = "Venta Estimada " + actual;
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadEstimada].HeaderText = "Utilidad " + actual;


            dgv.Columns[(int)ColumnasGridArticulos.PromedioAtrasado].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadAtrasada].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumnasGridArticulos.VentaActual].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasGridArticulos.VentaEstimada].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadEstimada].DefaultCellStyle.Format = "P2";

            dgv.Columns[(int)ColumnasGridArticulos.PromedioAtrasado].DefaultCellStyle.NullValue = "$0.0";
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadAtrasada].DefaultCellStyle.NullValue = "$0.0";
            dgv.Columns[(int)ColumnasGridArticulos.VentaActual].DefaultCellStyle.NullValue = "$0.0";
            dgv.Columns[(int)ColumnasGridArticulos.VentaEstimada].DefaultCellStyle.NullValue = "$0.0";
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadEstimada].DefaultCellStyle.NullValue = "$0.0";

            dgv.Columns[(int)ColumnasGridArticulos.CodigoArticulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[(int)ColumnasGridArticulos.NombreArticulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.Columns[(int)ColumnasGridArticulos.PromedioAtrasado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadAtrasada].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasGridArticulos.VentaActual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasGridArticulos.VentaEstimada].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasGridArticulos.UtilidadEstimada].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
        private void clbSucursal_Click(object sender, EventArgs e)
        {
           
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

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                Esperar();
                
                gridClientes.DataSource = null;
                gridLineas.DataSource = null;
                gridArticulos.DataSource = null;
                
                txtVenta.Clear();
                txtUtilidadEstimada.Clear();
                txtVentaEstimada.Clear();

                //DataTable tablaarticulos = new DataTable();
                Sucursales = "," + clbSucursal.SelectedValue.ToString();
                Lineas = GetCadena(clbLinea);
                Articulo = txtArticulo.Text.Trim();
                if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                    Vendedores = GetCadena(clbVendedor);
                Cliente = txtCliente.Text.Trim();

                FechaInicial = dateTimePicker1.Value.ToShortDateString();
                Canales = GetCadena(clbCanal);
                decimal DiasMes = 0;
                decimal DiasTrans = 0;

                //Datos
                SqlCommand command2 = new SqlCommand("PJ_Utilidad_Linea", conection);
                command2.CommandType = CommandType.StoredProcedure;
                command2.Parameters.AddWithValue("@TipoConsulta", 5);
                command2.Parameters.AddWithValue("@Sucursales", Sucursales.Trim(','));
                command2.Parameters.AddWithValue("@Lineas", Lineas.Trim(','));
                command2.Parameters.AddWithValue("@Articulo", Articulo);
                command2.Parameters.AddWithValue("@Vendedores", Vendedores.Trim(','));
                command2.Parameters.AddWithValue("@Cliente", Cliente);
                command2.Parameters.AddWithValue("@Fecha", FechaInicial);
                command2.Parameters.AddWithValue("@GranCanales", Canales.Trim(','));

                DataTable tablaprincipal = new DataTable();
                adapterClientes.SelectCommand = command2;
                adapterClientes.SelectCommand.CommandTimeout = 0;
                adapterClientes.Fill(tablaprincipal);

                //Dias Dias Mes
                SqlCommand command3 = new SqlCommand("PJ_Utilidad_Linea", conection);
                command3.CommandType = CommandType.StoredProcedure;
                command3.Parameters.AddWithValue("@TipoConsulta", 1);
                command3.Parameters.AddWithValue("@Sucursales", string.Empty);
                command3.Parameters.AddWithValue("@Lineas", string.Empty);
                command3.Parameters.AddWithValue("@Articulo", string.Empty);
                command3.Parameters.AddWithValue("@Vendedores", string.Empty);
                command3.Parameters.AddWithValue("@Cliente", string.Empty);
                command3.Parameters.AddWithValue("@Fecha", string.Empty);
                command3.Parameters.AddWithValue("@GranCanales", string.Empty);

                //Dias Trasncurridos
                SqlCommand command4 = new SqlCommand("PJ_Utilidad_Linea", conection);
                command4.CommandType = CommandType.StoredProcedure;
                command4.Parameters.AddWithValue("@TipoConsulta", 2);
                command4.Parameters.AddWithValue("@Sucursales", string.Empty);
                command4.Parameters.AddWithValue("@Lineas", string.Empty);
                command4.Parameters.AddWithValue("@Articulo", string.Empty);
                command4.Parameters.AddWithValue("@Vendedores", string.Empty);
                command4.Parameters.AddWithValue("@Cliente", string.Empty);
                command4.Parameters.AddWithValue("@Fecha", string.Empty);
                command4.Parameters.AddWithValue("@GranCanales", string.Empty);


                conection.Open();
                SqlDataReader reader1 = command3.ExecuteReader();
                if (reader1.Read())
                    DiasMes = Convert.ToDecimal(reader1[0]);
                SqlDataReader reader2 = command4.ExecuteReader();
                if (reader2.Read())
                    DiasTrans = Convert.ToDecimal(reader2[0]);
                
                ////////////grid Articulos
                //List<Clases.Articulos> list_articulos = new List<Clases.Articulos>();
                var list_articulos = (from item in tablaprincipal.AsEnumerable()
                                  group item by new
                                  {
                                      ID = item.Field<string>("ClaveCliente") + "" + item.Field<Int16>("CodigoLinea"),
                                      Cliente = item.Field<string>("Cliente"),
                                      CodigoArticulo = item.Field<string>("CodigoArticulo"),
                                      NombreArticulo = item.Field<string>("NombreArticulo"),
                                  } into grouped
                                  select new 
                                  {
                                      ID = grouped.Key.ID,
                                      Cliente = grouped.Key.Cliente,
                                      CodArticulo = grouped.Key.CodigoArticulo,
                                      NombreArticulo = grouped.Key.NombreArticulo,
                                      PromVentaAtrasada = grouped.Sum(ix => ix.Field<decimal?>("VentaAtrasado")) / 3,
                                      UtilidadAtrasada = (grouped.Sum(ix => ix.Field<decimal?>("VentaAtrasado")) / 3) == 0 ? 0 : 1 - ((grouped.Sum(iy => iy.Field<decimal?>("CompraAtrasado")) / 3) / (grouped.Sum(iy => iy.Field<decimal?>("VentaAtrasado")) / 3)),
                                      VentaActual = grouped.Sum(ix => ix.Field<decimal?>("VentaActual")),
                                      UtilidadEstimada = grouped.Sum(ix => ix.Field<decimal?>("VentaActual")) == 0 ? 0 : 1 - (grouped.Sum(ix => ix.Field<decimal?>("CompraActual")) / grouped.Sum(ix => ix.Field<decimal?>("VentaActual"))),
                                      EstimadoActual = DiasTrans == 0 ? 0 : (grouped.Sum(ix => ix.Field<decimal?>("VentaActual")) / DiasTrans) * DiasMes
                                  }).ToList();
                //gridArticulos.DataSource = list_articulos; 
                //grid Lineas
                List<Clases.Lineas> list_lineas = new List<Clases.Lineas>();
                list_lineas = (from item in tablaprincipal.AsEnumerable()
                               group item by new
                               {
                                   ID = item.Field<string>("ClaveCliente") + "" + item.Field<Int16>("CodigoLinea"),
                                   ClaveCliente = item.Field<string>("ClaveCliente"),
                                   Linea = item.Field<string>("Linea"),
                               } into grouped
                               select new Clases.Lineas
                               {
                                   ID = grouped.Key.ID,
                                   ClaveCliente = grouped.Key.ClaveCliente,
                                   Linea = grouped.Key.Linea,
                                   PromVentaAtrasada = grouped.Sum(ix => ix.Field<decimal?>("VentaAtrasado"))/3,
                                   UtilidadAtrasada = (grouped.Sum(ix => ix.Field<decimal?>("VentaAtrasado"))/3) == 0 ? 0 : 1 - ((grouped.Sum(iy => iy.Field<decimal?>("CompraAtrasado"))/3)/(grouped.Sum(iy => iy.Field<decimal?>("VentaAtrasado"))/3)),
                                   VentaActual = grouped.Sum(ix => ix.Field<decimal?>("VentaActual")),
                                   UtilidadEstimada = grouped.Sum(ix => ix.Field<decimal?>("VentaActual")) == 0 ? 0 : 1 - (grouped.Sum(ix => ix.Field<decimal?>("CompraActual")) / grouped.Sum(ix => ix.Field<decimal?>("VentaActual"))),
                                   EstimadoActual = DiasTrans == 0 ? 0 : (grouped.Sum(ix => ix.Field<decimal?>("VentaActual")) / DiasTrans) * DiasMes
                               }).ToList();

                //gridLineas.DataSource = list_lineas;

                //grid Clientes
                List<Clases.Clientes> list_clientes = new List<Clases.Clientes>();
                list_clientes = (from item in tablaprincipal.AsEnumerable()
                                 group item by new
                                 {
                                     ClaveCliente = item.Field<string>("ClaveCliente"),
                                     Cliente = item.Field<string>("Cliente"),
                                 } into grouped
                                 select new Clases.Clientes
                                 {
                                     ClaveCliente = grouped.Key.ClaveCliente,
                                     Cliente = grouped.Key.Cliente,
                                     PromVentaAtrasada = grouped.Sum(ix => ix.Field<decimal?>("VentaAtrasado"))/3,
                                     UtilidadAtrasada = (grouped.Sum(ix => ix.Field<decimal?>("VentaAtrasado"))/3) == 0 ? 0 : 1 - ((grouped.Sum(iy => iy.Field<decimal?>("CompraAtrasado"))/3) / (grouped.Sum(iy => iy.Field<decimal?>("VentaAtrasado"))/3)),
                                     VentaActual = grouped.Sum(ix => ix.Field<decimal?>("VentaActual")),
                                     UtilidadEstimada = grouped.Sum(ix => ix.Field<decimal?>("VentaActual")) == 0 ? 0 : 1 - (grouped.Sum(ix => ix.Field<decimal?>("CompraActual")) / grouped.Sum(ix => ix.Field<decimal?>("VentaActual"))),
                                     EstimadoActual = DiasTrans == 0 ? 0 : (grouped.Sum(ix => ix.Field<decimal?>("VentaActual")) / DiasTrans) * DiasMes,
                                     EstimadoActualCompra = DiasTrans == 0 ? 0 : (grouped.Sum(ix => ix.Field<decimal?>("CompraActual"))) ,
                                 }).ToList();
                //gridClientes.DataSource = list_clientes;

                //tablas
                DataTable tblClietes = Clases.ListConverter.ToDataTable(list_clientes);
                DataTable tblLineas = Clases.ListConverter.ToDataTable(list_lineas);
                DataTable tblArticulos = Clases.ListConverter.ToDataTable(list_articulos);

                BindingSource lineasBindingSource = new BindingSource();
                BindingSource articulosBindingSource = new BindingSource();
                BindingSource clientesBindingsource = new BindingSource();

                tblLineas.TableName = "TablaLineas";
                tblClietes.TableName = "TablaClientes";
                tblArticulos.TableName = "TablaArticulos";

                data = new DataSet();
                data.Tables.Add(tblClietes);
                data.Tables.Add(tblArticulos);
                data.Tables.Add(tblLineas);
                DataRelation relation = new DataRelation("Linea-Articulo", data.Tables["TablaLineas"].Columns["ID"], data.Tables["TablaArticulos"].Columns["ID"]);

                data.Relations.Add(relation);
                DataRelation relationCliente_Linea = new DataRelation("Cliente-Linea", data.Tables["TablaClientes"].Columns["ClaveCliente"], data.Tables["TablaLineas"].Columns["ClaveCliente"]);
                data.Relations.Add(relationCliente_Linea);

                lineasBindingSource.DataSource = data;
                lineasBindingSource.DataMember = "TablaLineas";
                articulosBindingSource.DataSource = lineasBindingSource;
                articulosBindingSource.DataMember = "Linea-Articulo";


                clientesBindingsource.DataSource = data;
                clientesBindingsource.DataMember = "TablaClientes";

                lineasBindingSource.DataSource = clientesBindingsource;
                lineasBindingSource.DataMember = "Cliente-Linea";

                articulosBindingSource.DataSource = lineasBindingSource;
                articulosBindingSource.DataMember = "Linea-Articulo";

                gridClientes.DataSource = clientesBindingsource;
                gridLineas.DataSource = lineasBindingSource;
                gridArticulos.DataSource = articulosBindingSource;
                gridExcel.DataSource = data.Tables["TablaArticulos"];
                dataGridView1.DataSource = tablaprincipal;

                DarFormatoGridCliente();
                DarFormatoGridLineas();
                DarFormatoGridArticulos(gridArticulos);
                DarFormatoGridArticulos(gridExcel);

                int act = DateTime.Now.Month;
                int ant_i = DateTime.Now.AddMonths(-3).Month;
                int ant_f = DateTime.Now.AddMonths(-1).Month;
                DateTimeFormatInfo fecha = CultureInfo.CurrentCulture.DateTimeFormat;

                string actual = fecha.GetMonthName(DateTime.Now.Month);
                string atrasado = fecha.GetMonthName(ant_i) + "-" + fecha.GetMonthName(ant_f);

                decimal ventaactual = 0;
                decimal ventaestimada = 0;
                decimal compraestimada = 0;
                //decimal utilidadestimada = 0;

                foreach (DataGridViewRow item in gridClientes.Rows)
                {
                    ventaactual += decimal.Parse(item.Cells[(int)ColumnasGridClientes.VentaActual].Value.ToString());
                    ventaestimada += decimal.Parse(item.Cells[(int)ColumnasGridClientes.VentaEstimada].Value.ToString());
                    compraestimada += decimal.Parse(item.Cells[(int)ColumnasGridClientes.PromedioCompraActual].Value.ToString());
                }
                txtVenta.Text = ventaactual.ToString("c");
                txtVentaEstimada.Text = ventaestimada.ToString("c");
                if (ventaestimada != 0)
                {
                    txtUtilidadEstimada.Text = (((compraestimada / ventaactual) - 1) * -100).ToString("N2") + "%";
                }
                else
                {
                    txtUtilidadEstimada.Text = 0.ToString("N2") + "%";
                }

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
    }
}
