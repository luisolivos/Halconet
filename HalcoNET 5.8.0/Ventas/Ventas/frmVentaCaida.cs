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

namespace Ventas.Ventas
{
    public partial class frmVentaCaida : Form
    {
        private int RolUsuario;
        private int CodigoVendedor;
        private string Sucursal;
        private string NombreUsuario;
        private DataTable TBLVendedores = new DataTable();
        private DataTable TBLLineas = new DataTable();
        Clases.Logs log;
        public int Opcion;

        private enum Columnas
        {
            Linea,
            Mes6,
            Mes5,
            Mes4,
            Mes3,
            Mes2,
            Mes1,
            Promedio,
            TendenciaMesActual,
            TendenciaPorcentaje,
            Trimestre1,
            Trimestre2,
            Crecimiento
        }

        private enum ColumnasClientes
        {
            Cliente,
            NombreClinte,
            Mes6,
            Mes5,
            Mes4,
            Mes3,
            Mes2,
            Mes1,
            Promedio,
            TendenciaMesActual,
            TendenciaPorcentaje,
            Trimestre1,
            Trimestre2,
            Crecimiento
        }

        public frmVentaCaida(int _rol, int _vendor, string _sucursal, string usuario)
        {
            InitializeComponent();

            RolUsuario = _rol;
            CodigoVendedor = _vendor;
            Sucursal = _sucursal;
            NombreUsuario = usuario;
        }

        private void CargarSucursales()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "--";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            clbSucursal.DataSource = table;
            clbSucursal.DisplayMember = "Nombre";
            clbSucursal.ValueMember = "Codigo";
        }

        private void CargarVendedores()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", 0);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "--";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);


                TBLVendedores = table.Copy();

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            else
            {
                SqlCommand command = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 4);
                command.Parameters.AddWithValue("@Vendedores", string.Empty);
                command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                command.Parameters.AddWithValue("@Cliente", string.Empty);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                command.Parameters.AddWithValue("@Factura", string.Empty);
                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                TBLVendedores = table.Copy();

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }

        }

        public string getMemo(int GroupCode)
        {
            string _memo = "";
            switch (GroupCode)
            {
                case 107: _memo = "01"; break;
                case 105: _memo = "02"; break;
                case 106: _memo = "02"; break;
                case 100: _memo = "03"; break;
                case 102: _memo = "05"; break;
                case 108: _memo = "06"; break;
                case 103: _memo = "16"; break;
                case 104: _memo = "18"; break;
            }

            return _memo;
        }

        public DataTable llenarTabla(DataTable _tbl)
        {
            foreach (DataRow item in TBLLineas.Rows)
            {
                var query = from linea in _tbl.AsEnumerable()
                            where linea.Field<string>("ItmsGrpNam").Equals(item.Field<string>("ItmsGrpNam"))
                            select item;

                if (query.Count() < 1)
                {
                    DataRow row = _tbl.NewRow();
                    row["ItmsGrpNam"] = item.Field<string>("ItmsGrpNam");
                    row["MES6"] = 0;
                    row["MES5"] = 0;
                    row["MES4"] = 0;
                    row["MES3"] = 0;
                    row["MES2"] = 0;
                    row["MES1"] = 0;
                    row["Avg3Meses"] = 0;
                    row["Tendencia"] = 0;
                    row["% Tendencia"] = 0;
                    row["Trimestre1"] = 0;
                    row["Trimestre2"] = 0;
                    row["Crecimiento"] = 0;
                    _tbl.Rows.Add(row);
                }
            }

            return _tbl;
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

        private void GetDatos(int TipoConsulta, int Sucursal, int Vendedor, string Linea, bool Llenar)
        {
            try
            {
                //dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                this.Esperar();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandTimeout = 0;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PJ_VentaCaida";
                        command.Parameters.AddWithValue("@TipoConsulta", TipoConsulta);
                        command.Parameters.AddWithValue("@Sucursal", Sucursal);
                        command.Parameters.AddWithValue("@Vendedor", Vendedor);
                        command.Parameters.AddWithValue("@Linea", Linea);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);


                        if (Llenar)
                            dataGridView1.DataSource = this.llenarTabla(table); 
                        else
                            dataGridView2.DataSource = table;
                    }
                }
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

        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        private void GetlLineas(int TipoConsulta, int Sucursal, int Vendedor)
        {
            try
            {
                TBLLineas.Clear();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandTimeout = 0;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "PJ_VentaCaida";
                        command.Parameters.AddWithValue("@TipoConsulta", TipoConsulta);
                        command.Parameters.AddWithValue("@Sucursal", Sucursal);
                        command.Parameters.AddWithValue("@Vendedor", Vendedor);
                        command.Parameters.AddWithValue("@Linea", string.Empty);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(TBLLineas);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Formato(DataGridView dgv)
        {
            string mes0 = this.MonthName(DateTime.Now.Month);
            string mes1 = this.MonthName(DateTime.Now.AddMonths(-1).Month);
            string mes2 = this.MonthName(DateTime.Now.AddMonths(-2).Month);
            string mes3 = this.MonthName(DateTime.Now.AddMonths(-3).Month);
            string mes4 = this.MonthName(DateTime.Now.AddMonths(-4).Month);
            string mes5 = this.MonthName(DateTime.Now.AddMonths(-5).Month);
            string mes6 = this.MonthName(DateTime.Now.AddMonths(-6).Month);

            dgv.Columns[(int)Columnas.Linea].HeaderText = "Línea";
            dgv.Columns[(int)Columnas.Mes6].HeaderText = mes6;
            dgv.Columns[(int)Columnas.Mes5].HeaderText = mes5;
            dgv.Columns[(int)Columnas.Mes4].HeaderText = mes4;
            dgv.Columns[(int)Columnas.Mes3].HeaderText = mes3;
            dgv.Columns[(int)Columnas.Mes2].HeaderText = mes2;
            dgv.Columns[(int)Columnas.Mes1].HeaderText = mes1;
            dgv.Columns[(int)Columnas.Promedio].HeaderText = "Promedio mayor 3 meses";
            dgv.Columns[(int)Columnas.TendenciaMesActual].HeaderText = mes0 + " Tendencia";
            dgv.Columns[(int)Columnas.TendenciaPorcentaje].HeaderText = "%";
            dgv.Columns[(int)Columnas.Trimestre1].HeaderText = mes6 + " - " + mes4;
            dgv.Columns[(int)Columnas.Trimestre2].HeaderText = mes3 + " - " + mes1;

            dgv.Columns[(int)Columnas.Linea].Width = 120;
            dgv.Columns[(int)Columnas.Mes6].Width = 100;
            dgv.Columns[(int)Columnas.Mes5].Width = 100;
            dgv.Columns[(int)Columnas.Mes4].Width = 100;
            dgv.Columns[(int)Columnas.Mes3].Width = 100;
            dgv.Columns[(int)Columnas.Mes2].Width = 100;
            dgv.Columns[(int)Columnas.Mes1].Width = 100;
            dgv.Columns[(int)Columnas.Promedio].Width = 100;
            dgv.Columns[(int)Columnas.TendenciaMesActual].Width = 100;
            dgv.Columns[(int)Columnas.TendenciaPorcentaje].Width = 100;
            dgv.Columns[(int)Columnas.Trimestre1].Width = 100;
            dgv.Columns[(int)Columnas.Trimestre2].Width = 100;
            dgv.Columns[(int)Columnas.Crecimiento].Width = 100;

            dgv.Columns[(int)Columnas.Mes6].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes5].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Promedio].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.TendenciaMesActual].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.TendenciaPorcentaje].DefaultCellStyle.Format = "P1";
            dgv.Columns[(int)Columnas.Trimestre1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Trimestre2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Crecimiento].DefaultCellStyle.Format = "P1";

            dgv.Columns[(int)Columnas.Mes6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.TendenciaMesActual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.TendenciaPorcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Trimestre1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Trimestre2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Crecimiento].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.TendenciaMesActual].ToolTipText = "(Venta mes " + mes0 + ")/ Dias trasncurridos * Dias mes";
            dgv.Columns[(int)Columnas.TendenciaPorcentaje].ToolTipText = mes0 + " tendecina / Promedio";
            dgv.Columns[(int)Columnas.Trimestre1].ToolTipText = mes6 + " + " + mes5 + " + " + mes4;
            dgv.Columns[(int)Columnas.Trimestre2].ToolTipText = mes3 + " + " + mes2 + " + " + mes1;
            dgv.Columns[(int)Columnas.Crecimiento].ToolTipText = "Trimestre 2 / Trimestre 1";

            /*
             ext = mes6 +" - " +mes4;
            dataGridView1.Columns[(int)Columnas.Trimestre2].HeaderText = mes3 + " - " + mes1;
             */

        }

        public void FormatoClientes(DataGridView dgv)
        {
            string mes0 = this.MonthName(DateTime.Now.Month);
            string mes1 = this.MonthName(DateTime.Now.AddMonths(-1).Month);
            string mes2 = this.MonthName(DateTime.Now.AddMonths(-2).Month);
            string mes3 = this.MonthName(DateTime.Now.AddMonths(-3).Month);
            string mes4 = this.MonthName(DateTime.Now.AddMonths(-4).Month);
            string mes5 = this.MonthName(DateTime.Now.AddMonths(-5).Month);
            string mes6 = this.MonthName(DateTime.Now.AddMonths(-6).Month);

            dgv.Columns[(int)ColumnasClientes.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)ColumnasClientes.NombreClinte].Visible = false;
            dgv.Columns[(int)ColumnasClientes.Mes6].HeaderText = mes6;
            dgv.Columns[(int)ColumnasClientes.Mes5].HeaderText = mes5;
            dgv.Columns[(int)ColumnasClientes.Mes4].HeaderText = mes4;
            dgv.Columns[(int)ColumnasClientes.Mes3].HeaderText = mes3;
            dgv.Columns[(int)ColumnasClientes.Mes2].HeaderText = mes2;
            dgv.Columns[(int)ColumnasClientes.Mes1].HeaderText = mes1;
            dgv.Columns[(int)ColumnasClientes.Promedio].HeaderText = "Promedio mayor 3 meses";
            dgv.Columns[(int)ColumnasClientes.TendenciaMesActual].HeaderText = mes0 + " Tendencia";
            dgv.Columns[(int)ColumnasClientes.TendenciaPorcentaje].HeaderText = "%";
            dgv.Columns[(int)ColumnasClientes.Trimestre1].HeaderText = mes6 + " - " + mes4;
            dgv.Columns[(int)ColumnasClientes.Trimestre2].HeaderText = mes3 + " - " + mes1;

            dgv.Columns[(int)ColumnasClientes.Cliente].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Mes6].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Mes5].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Mes4].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Mes3].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Mes2].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Mes1].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Promedio].Width = 100;
            dgv.Columns[(int)ColumnasClientes.TendenciaMesActual].Width = 100;
            dgv.Columns[(int)ColumnasClientes.TendenciaPorcentaje].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Trimestre1].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Trimestre2].Width = 100;
            dgv.Columns[(int)ColumnasClientes.Crecimiento].Width = 100;

            dgv.Columns[(int)ColumnasClientes.Mes6].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.Mes5].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.Mes4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.Mes3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.Mes2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.Mes1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.Promedio].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.TendenciaMesActual].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.TendenciaPorcentaje].DefaultCellStyle.Format = "P1";
            dgv.Columns[(int)ColumnasClientes.Trimestre1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.Trimestre2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasClientes.Crecimiento].DefaultCellStyle.Format = "P1";

            dgv.Columns[(int)ColumnasClientes.Mes6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Mes5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Mes4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Mes3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Mes2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Mes1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.TendenciaMesActual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.TendenciaPorcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Trimestre1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Trimestre2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasClientes.Crecimiento].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasClientes.TendenciaMesActual].ToolTipText = "(Venta mes " + mes0 + ")/ Dias trasncurridos * Dias mes";
            dgv.Columns[(int)ColumnasClientes.TendenciaPorcentaje].ToolTipText = mes0 + " tendecina / Promedio";
            dgv.Columns[(int)ColumnasClientes.Trimestre1].ToolTipText = mes6 + " + " + mes5 + " + " + mes4;
            dgv.Columns[(int)ColumnasClientes.Trimestre2].ToolTipText = mes3 + " + " + mes2 + " + " + mes1;
            dgv.Columns[(int)ColumnasClientes.Crecimiento].ToolTipText = "Trimestre 2 / Trimestre 1";

            foreach (DataGridViewRow item in dgv.Rows)
            {
                item.Cells[(int)ColumnasClientes.Cliente].ToolTipText = Convert.ToString(item.Cells[(int)ColumnasClientes.NombreClinte].Value);
            }
        }

        private void VentaCaida_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            cbCanal.SelectedIndex = 0;
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.CargarSucursales();
            this.CargarVendedores();
            this.GetlLineas(4, 0,0);
        }

        DataTable JefasxSucursal = new DataTable();
        DataTable VendedorxSucursal = new DataTable();

        private void clbSucursal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Opcion = 2;
            clbVendedor.DataSource = null;
            
            JefasxSucursal.Clear();
            VendedorxSucursal.Clear();
            try
            {
                DataRowView v = clbSucursal.Items[((ComboBox)sender).SelectedIndex] as DataRowView;
                string _memo = getMemo(Convert.ToInt32(v["Codigo"]));
                VendedorxSucursal.Merge((from item in TBLVendedores.AsEnumerable() where item["Memo"].ToString() == _memo select item).CopyToDataTable());


                DataView vistaV = new DataView(VendedorxSucursal);
                DataTable aV = vistaV.ToTable(true, new string[] { "Codigo", "Nombre" });
                DataRow row = aV.NewRow();
                row["Nombre"] = "--";
                row["Codigo"] = "0";
                aV.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = aV;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            catch (Exception)
            {
                VendedorxSucursal.Clear();
                clbVendedor.DataSource = TBLVendedores;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }

            ComboBox c = (ComboBox)sender;
            Sucursal = Convert.ToString(c.Text);

            this.GetDatos(2, Convert.ToInt32(c.SelectedValue), 0, string.Empty, true);
            this.Formato(dataGridView1);

            label2.Visible = true;
            clbVendedor.Visible = true;
            cbCanal.Visible = true;
            label3.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.GetDatos(1, 0, 0, string.Empty, true);
            this.Formato(dataGridView1);
            Opcion = 1;

            label2.Visible = false;
            clbVendedor.Visible = false;
            cbCanal.Visible = false;
            label3.Visible = false;
        }

        private void clbVendedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Opcion = 3;
            cbCanal.SelectedIndex = 0;
            ComboBox c = (ComboBox)sender;

            this.GetDatos(3,0, Convert.ToInt32(c.SelectedValue), string.Empty, true);
            this.Formato(dataGridView1);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow  item in dataGridView1.Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.TendenciaPorcentaje].Value) < (decimal)-0.1)
                    {
                        item.Cells[(int)Columnas.TendenciaPorcentaje].Style.BackColor = Color.FromArgb(192, 0, 0);
                        item.Cells[(int)Columnas.TendenciaPorcentaje].Style.ForeColor = Color.White;
                    }
                    else if (Convert.ToDecimal(item.Cells[(int)Columnas.TendenciaPorcentaje].Value) > (decimal)0)
                    {
                        item.Cells[(int)Columnas.TendenciaPorcentaje].Style.BackColor = Color.FromArgb(146, 208, 80);
                        item.Cells[(int)Columnas.TendenciaPorcentaje].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(item.Cells[(int)Columnas.TendenciaPorcentaje].Value) >= (decimal)-0.1 &&
                                Convert.ToDecimal(item.Cells[(int)Columnas.TendenciaPorcentaje].Value) <= (decimal)0)
                    {
                        item.Cells[(int)Columnas.TendenciaPorcentaje].Style.BackColor = Color.FromArgb(255, 192, 0);
                        item.Cells[(int)Columnas.TendenciaPorcentaje].Style.ForeColor = Color.Black;
                    }

                    /// CRECIMIENTO
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Crecimiento].Value) > 0)
                    {
                        item.Cells[(int)Columnas.Crecimiento].Style.BackColor = Color.FromArgb(146,208,80);
                        item.Cells[(int)Columnas.Crecimiento].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(item.Cells[(int)Columnas.Crecimiento].Value) <= 0 &&
                        Convert.ToDecimal(item.Cells[(int)Columnas.Crecimiento].Value) >= (decimal)-0.1)
                    {
                        item.Cells[(int)Columnas.Crecimiento].Style.BackColor = Color.FromArgb(255, 192, 0);
                        item.Cells[(int)Columnas.Crecimiento].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(item.Cells[(int)Columnas.Crecimiento].Value) < (decimal)-0.1)
                    {
                        item.Cells[(int)Columnas.Crecimiento].Style.BackColor = Color.FromArgb(192,0,0);
                        item.Cells[(int)Columnas.Crecimiento].Style.ForeColor = Color.White;
                    }

                    ///// PROMEDIOS
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Mes1].Value) < Convert.ToDecimal(item.Cells[(int)Columnas.Promedio].Value))
                    {
                        item.Cells[(int)Columnas.Mes1].Style.BackColor = Color.FromArgb(192, 0, 0);
                        item.Cells[(int)Columnas.Mes1].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Mes1].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Mes1].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Mes2].Value) < Convert.ToDecimal(item.Cells[(int)Columnas.Promedio].Value))
                    {
                        item.Cells[(int)Columnas.Mes2].Style.BackColor = Color.FromArgb(192, 0, 0);
                        item.Cells[(int)Columnas.Mes2].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Mes2].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Mes2].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Mes3].Value) < Convert.ToDecimal(item.Cells[(int)Columnas.Promedio].Value))
                    {
                        item.Cells[(int)Columnas.Mes3].Style.BackColor = Color.FromArgb(192, 0, 0);
                        item.Cells[(int)Columnas.Mes3].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Mes3].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Mes3].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Mes4].Value) < Convert.ToDecimal(item.Cells[(int)Columnas.Promedio].Value))
                    {
                        item.Cells[(int)Columnas.Mes4].Style.BackColor = Color.FromArgb(192, 0, 0);
                        item.Cells[(int)Columnas.Mes4].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Mes4].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Mes4].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Mes5].Value) < Convert.ToDecimal(item.Cells[(int)Columnas.Promedio].Value))
                    {
                        item.Cells[(int)Columnas.Mes5].Style.BackColor = Color.FromArgb(192, 0, 0);
                        item.Cells[(int)Columnas.Mes5].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Mes5].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Mes5].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Mes6].Value) < Convert.ToDecimal(item.Cells[(int)Columnas.Promedio].Value))
                    {
                        item.Cells[(int)Columnas.Mes6].Style.BackColor = Color.FromArgb(192, 0, 0);
                        item.Cells[(int)Columnas.Mes6].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Mes6].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Mes6].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch(Exception)
            {
            }
        }

        private void VentaCaida_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void VentaCaida_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == (int)Columnas.Linea)
                {
                    dataGridView2.DataSource = null;
                    if (Opcion == 1)
                    {
                        lblClientes.Text = "Clientes: PJ - " + Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        this.GetDatos(6, 0, 0, Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value), false);
                        this.FormatoClientes(dataGridView2);
                    }
                    if (Opcion == 2)
                    {
                        lblClientes.Text = "Clientes: " + clbSucursal.Text + " - " + Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        this.GetDatos(7, Convert.ToInt32(clbSucursal.SelectedValue), 0, Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value), false);
                        this.FormatoClientes(dataGridView2);
                    }
                    if (Opcion == 3)
                    {
                        lblClientes.Text = "Clientes: " + clbVendedor.Text + " - " + Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        this.GetDatos(8, 0, Convert.ToInt32(clbVendedor.SelectedValue), Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value), false);
                        this.FormatoClientes(dataGridView2);
                    }
                    if (Opcion == 9)
                    {
                        int Canal = 0;
                        if (cbCanal.Text == "Transporte")
                        {
                            Canal = 60;
                        }
                        else if (cbCanal.Text == "Mayoreo")
                        {
                            Canal = 61;
                        }

                        lblClientes.Text = "Clientes: " + cbCanal.Text + " - " + Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                        this.GetDatos(10, Convert.ToInt32(clbSucursal.SelectedValue), Canal, Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex].Value), false);
                        this.FormatoClientes(dataGridView2);
                    }
                }
                aux = lblClientes.Text;
            }
            catch (Exception)
            {

            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
        //    try
        //    {
        //        dataGridView2.DataSource = null;

        //        int ColumnIndex = (sender as DataGridView).CurrentCell.ColumnIndex;
        //        int RowIndex = (sender as DataGridView).CurrentCell.RowIndex;

        //        if (ColumnIndex == (int)Columnas.Linea)
        //        {
        //            if (Opcion == 1)
        //            {
        //                lblClientes.Text = "Clientes: PJ - " + Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value);
        //                this.GetDatos(6, 0, 0, Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value), false);
        //                Formato(dataGridView2);
        //            }
        //            if (Opcion == 2)
        //            {
        //                lblClientes.Text = "Clientes: PJ - " + Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value);
        //                this.GetDatos(7, Convert.ToInt32(clbSucursal.SelectedValue), 0, Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value), false);
        //                Formato(dataGridView2);
        //            }
        //            if (Opcion == 3)
        //            {
        //                lblClientes.Text = "Clientes: PJ - " + Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value);
        //                this.GetDatos(8, 0, Convert.ToInt32(clbVendedor.SelectedValue), Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value), false);
        //                Formato(dataGridView2);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        }

        string aux = "";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == (int)ColumnasClientes)
                // {

                string Nombre = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasClientes.NombreClinte].Value);

                lblClientes.Text = aux + " - " + Nombre;
                // }
            }
            catch (Exception)
            {

            }
        }

        private void cbCanal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {clbVendedor.SelectedIndex = 0;
                int Canal = 0;
                if (cbCanal.Text == "Transporte")
                {
                    Canal = 60;
                }
                else if (cbCanal.Text == "Mayoreo")
                {
                    Canal = 61;
                }

                Opcion = 9;
                ComboBox c = (ComboBox)sender;

                this.GetDatos(9, Convert.ToInt32(clbSucursal.SelectedValue), Canal, string.Empty, true);
                this.Formato(dataGridView1);


            }
            catch (Exception)
            {
                
       
            }
        }
    }
}
