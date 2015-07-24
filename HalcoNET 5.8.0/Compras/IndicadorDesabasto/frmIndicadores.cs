using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compras.IndicadorDesabasto
{
    public partial class frmIndicadores : Form
    {
        DataTable DATOS = new DataTable();
        DataTable LIMITES = new DataTable();

        DataTable totalesCompras = new DataTable();
        DataTable desabastoCompras = new DataTable();
        DataTable stockoutCompras = new DataTable();
        DataTable indicadorCompras1 = new DataTable();
        DataTable indicadorCompras2 = new DataTable();

        DataTable totalInventarios = new DataTable();
        DataTable desabastoInventarios = new DataTable();
        DataTable stockourInventarios = new DataTable();
        DataTable indicadorInventarios1 = new DataTable();
        DataTable indicadorInventarios2 = new DataTable();
        Clases.Logs log;

        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter1 = new SqlDataAdapter();

        private BindingSource bindingSource2 = new BindingSource();
        private SqlDataAdapter dataAdapter2 = new SqlDataAdapter();

        private BindingSource bindingSource3 = new BindingSource();
        private SqlDataAdapter dataAdapter3 = new SqlDataAdapter();

        private enum Columnas
        {
            Comprador, AAA, AA, A, BBB, BB, B, C, penalizacion, subtotal, total, tipo, inv, articulo, porc
        }

        private enum ColumnasVI
        {
            Comprador, Col1, Col2, Col3, Promedio, Status, dgv
        }

        private enum ColumnIndicadores
        {
            Comprador,
            PDesabasto,
            PPermitido,
            Cumplimiento,
            PCumplimiento
        }
        
        public enum ColumsLimites
        {
            Code, 
            Tipo,
            Indicador, 
            Comprador,
            Enero,
            Febrero,
            Marzo,
            Abril,
			Mayo,
            Junio,
            Julio,
            Agosto,
            Septiembre, 
            Octubre, 
            Noviembre,
            Diciembre
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.tipo].Visible = false;
            dgv.Columns[(int)Columnas.inv].Visible = false;
            dgv.Columns[(int)Columnas.articulo].Visible = false;
            dgv.Columns[(int)Columnas.porc].Visible = false;

            dgv.Columns[(int)Columnas.AAA].Width = 50;
            dgv.Columns[(int)Columnas.AA].Width = 50;
            dgv.Columns[(int)Columnas.A].Width = 50;
            dgv.Columns[(int)Columnas.BBB].Width = 50;
            dgv.Columns[(int)Columnas.BB].Width = 50;
            dgv.Columns[(int)Columnas.B].Width = 50;
            dgv.Columns[(int)Columnas.C].Width = 50;

            dgv.Columns[(int)Columnas.AAA].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.AA].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.A].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.BBB].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.BB].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.B].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.C].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.penalizacion].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.total].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.subtotal].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)Columnas.AAA].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.AA].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.A].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.BBB].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.BB].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.B].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.C].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.penalizacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.subtotal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public void FormatoIndicadores(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnIndicadores.Comprador].Width = 85;
            dgv.Columns[(int)ColumnIndicadores.PDesabasto].Width = 70;
            dgv.Columns[(int)ColumnIndicadores.PPermitido].Width = 70;
            dgv.Columns[(int)ColumnIndicadores.Cumplimiento].Width = 100;
            dgv.Columns[(int)ColumnIndicadores.PCumplimiento].Width = 80;

            dgv.Columns[(int)ColumnIndicadores.PDesabasto].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumnIndicadores.PPermitido].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumnIndicadores.PCumplimiento].DefaultCellStyle.Format = "P2";
        }

        public void FormatoLimites(DataGridView dgv)
        {
            dgv.Columns[(int)ColumsLimites.Code].Visible = false;
            dgv.Columns[(int)ColumsLimites.Tipo].Visible = false;
            
            dgv.Columns[(int)ColumsLimites.Indicador].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Comprador].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Enero].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Febrero].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Marzo].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Abril].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Mayo].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Junio].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Julio].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Agosto].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Septiembre].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Octubre].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Noviembre].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumsLimites.Diciembre].DefaultCellStyle.Format = "P2";

            dgv.Columns[(int)ColumsLimites.Indicador].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Comprador].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Enero].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Febrero].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Marzo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Abril].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Mayo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Junio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Julio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Agosto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Septiembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Octubre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Noviembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumsLimites.Diciembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void FormatoVI(DataGridView dgv, string _clasificacion)
        {
            dgv.Columns[(int)ColumnasVI.dgv].Visible = false;
            if (_clasificacion.Equals("C"))
            {
                dgv.Columns[(int)ColumnasVI.Col2].Visible = false;
                dgv.Columns[(int)ColumnasVI.Col3].Visible = false;
            }

            dgv.Columns[(int)ColumnasVI.Col1].HeaderText = _clasificacion;
            dgv.Columns[(int)ColumnasVI.Col2].HeaderText = _clasificacion + _clasificacion;
            dgv.Columns[(int)ColumnasVI.Col3].HeaderText = _clasificacion + _clasificacion + _clasificacion;

            dgv.Columns[(int)ColumnasVI.Comprador].Width = 60;
            dgv.Columns[(int)ColumnasVI.Col1].Width = 50;
            dgv.Columns[(int)ColumnasVI.Col2].Width = 50;
            dgv.Columns[(int)ColumnasVI.Col3].Width = 50;
            dgv.Columns[(int)ColumnasVI.Promedio].Width = 70;
            dgv.Columns[(int)ColumnasVI.Status].Width = 70;

            dgv.Columns[(int)ColumnasVI.Col1].DefaultCellStyle.Format = "N1";
            dgv.Columns[(int)ColumnasVI.Col2].DefaultCellStyle.Format = "N1";
            dgv.Columns[(int)ColumnasVI.Col3].DefaultCellStyle.Format = "N1";
            dgv.Columns[(int)ColumnasVI.Promedio].DefaultCellStyle.Format = "N1";

            dgv.Columns[(int)ColumnasVI.Col1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasVI.Col2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasVI.Col3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasVI.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public frmIndicadores()
        {
            InitializeComponent();
        }

        public DataTable CrearTablas(DataTable tbl)
        {
            tbl.Clear();
            tbl.Columns.Clear();
            
            tbl.Columns.Add("Comprador", typeof(string));
            tbl.Columns.Add("% Desabasto", typeof(decimal));
            tbl.Columns.Add("% Permitido", typeof(decimal));
            tbl.Columns.Add("Cumplimiento", typeof(string));
            tbl.Columns.Add("% Cumplimiento", typeof(decimal), "[% Permitido]-[% Desabasto]");

            return tbl;        
        }

        public void GetData1(string query)
        {
            try
            {

                dataAdapter1 = new SqlDataAdapter(query, ClasesSGUV.Propiedades.conectionSGUV);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter1);
                DataTable _tableLibre = new DataTable();
                _tableLibre.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter1.Fill(_tableLibre);
                bindingSource1.DataSource = _tableLibre;
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }

        }

        public void GetData2(string query)
        {
            try
            {

                dataAdapter2 = new SqlDataAdapter(query, ClasesSGUV.Propiedades.conectionSGUV);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter2);
                DataTable _tableLibre = new DataTable();
                _tableLibre.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter2.Fill(_tableLibre);
                bindingSource2.DataSource = _tableLibre;
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }

        }

        public void GetData3(string query)
        {
            try
            {

                dataAdapter3 = new SqlDataAdapter(query, ClasesSGUV.Propiedades.conectionSGUV);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter3);
                DataTable _tableLibre = new DataTable();
                _tableLibre.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter3.Fill(_tableLibre);
                bindingSource3.DataSource = _tableLibre;
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }

        }

        public void DatosVI()
        {
            dgvConfigLineas.DataSource = bindingSource3;
            GetData3(@"SELECT 
                            b.ItmsGrpCod Code, 
                            b.nombre Linea, 
                            b.China, b.EUA, 
                            b.Compromiso
                        FROM  Tbl_ConfLineas b");

            //using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            //{
            //    using (SqlCommand command  =  new SqlCommand("PJ_Desabasto", connection))
            //    {
            //        command.CommandType = CommandType.StoredProcedure;
            //        command.Parameters.AddWithValue("@TipoConsulta", 4);

            //        SqlDataAdapter da = new SqlDataAdapter();
            //        da.SelectCommand = command;

            //        DataTable tbl = new DataTable();
            //        da.Fill(tbl);

            //        dgvConfigLineas.DataSource = tbl;
            //    }
            //}

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Desabasto", connection))
                {
                    try
                    {
                        
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 5);

                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable ta = new DataTable();
                        da.Fill(ta);

                        txtCA.Text = (from tc in ta.AsEnumerable()
                              where tc.Field<string>("U_Nombre") == "C_A"
                                      select tc.Field<string>("U_Valor")).FirstOrDefault(); 

                        txtCB.Text = (from tc in ta.AsEnumerable()
                                      where tc.Field<string>("U_Nombre") == "C_B"
                                      select tc.Field<string>("U_Valor")).FirstOrDefault();

                        txtCC.Text = (from tc in ta.AsEnumerable()
                                     where tc.Field<string>("U_Nombre") == "C_C"
                                      select tc.Field<string>("U_Valor")).FirstOrDefault();

                        txtSA.Text = (from tc in ta.AsEnumerable()
                                 where tc.Field<string>("U_Nombre") == "S_A"
                                      select tc.Field<string>("U_Valor")).FirstOrDefault();

                        txtSB.Text = (from tc in ta.AsEnumerable()
                               where tc.Field<string>("U_Nombre") == "S_B"
                                      select tc.Field<string>("U_Valor")).FirstOrDefault();

                        txtSC.Text = (from tc in ta.AsEnumerable()
                               where tc.Field<string>("U_Nombre") == "S_C"
                                      select tc.Field<string>("U_Valor")).FirstOrDefault();

                    }
                    catch (Exception)
                    {
                    }
                }
            }


        }

        public void SaveValue(string valor, string nombre)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Desabasto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 6);
                    command.Parameters.AddWithValue("@Articulo", valor);
                    command.Parameters.AddWithValue("@Proveedor", nombre);
                    command.CommandTimeout = 0;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        private void Indicadores_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                Cursor = Cursors.WaitCursor;
                if (ClasesSGUV.Login.Rol != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma && ClasesSGUV.Login.Rol != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador)
                    tabControl1.TabPages.Remove(tabConfig);

                DATOS = new DataTable();
                LIMITES = new DataTable();

                totalesCompras = new DataTable();
                desabastoCompras = new DataTable();
                stockoutCompras = new DataTable();
                indicadorCompras1 = new DataTable();
                indicadorCompras2 = new DataTable();

                totalInventarios = new DataTable();
                desabastoInventarios = new DataTable();
                stockourInventarios = new DataTable();
                indicadorInventarios1 = new DataTable();
                indicadorInventarios2 = new DataTable();



                bindingSource1 = new BindingSource();
                dataAdapter1 = new SqlDataAdapter();

                bindingSource2 = new BindingSource();
                dataAdapter2 = new SqlDataAdapter();

                indicadorCompras1 = this.CrearTablas(indicadorCompras1);
                indicadorCompras2 = this.CrearTablas(indicadorCompras2);

                indicadorInventarios1 = this.CrearTablas(indicadorInventarios1);
                indicadorInventarios2 = this.CrearTablas(indicadorInventarios2);

                toolStripStatusLabel1.Text = "Espere un momento: Cargando información.";

                //if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador)
                workerLimites.RunWorkerAsync();
                this.DatosVI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void workerCompras_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Desabasto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@Linea", string.Empty);
                        command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
                        command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
                        command.Parameters.AddWithValue("@Proveedor", string.Empty);

                        command.CommandTimeout = 0;

                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {

                            da.SelectCommand = command;
                            da.Fill(DATOS);

                        }
                    }
                    ////////////////////////C O M P R A S 
                    totalesCompras = (from item in DATOS.AsEnumerable()
                                      where item.Field<string>("tipo").Equals("TotalC")
                                      select item).CopyToDataTable();
                    desabastoCompras = (from item in DATOS.AsEnumerable()
                                        where item.Field<string>("tipo").Equals("DesabastoC")
                                        select item).CopyToDataTable();
                    stockoutCompras = (from item in DATOS.AsEnumerable()
                                       where item.Field<string>("tipo").Equals("StockoutC")
                                       select item).CopyToDataTable();

                    decimal total = decimal.Zero;
                    decimal desabasto = decimal.Zero;
                    decimal stockout = decimal.Zero;
                    decimal limiteD = decimal.Zero;
                     decimal limiteS = decimal.Zero;

                    foreach (DataRow item in totalesCompras.Rows)
                    {
                        total = (from i in totalesCompras.AsEnumerable()
                                 where i.Field<string>("Comprador").Equals(item.Field<string>("Comprador"))
                                 select i.Field<decimal>("Total")).FirstOrDefault();

                        desabasto = (from i in desabastoCompras.AsEnumerable()
                                     where i.Field<string>("Comprador").Equals(item.Field<string>("Comprador"))
                                     select i.Field<decimal>("Total")).FirstOrDefault();

                        stockout = (from i in stockoutCompras.AsEnumerable()
                                    where i.Field<string>("Comprador").Equals(item.Field<string>("Comprador"))
                                    select i.Field<decimal>("Total")).FirstOrDefault();

                        limiteD = (from i in LIMITES.AsEnumerable()
                                   where (i.Field<string>("nombre").Equals(item.Field<string>("Comprador")) && i.Field<string>("tipo").Equals("Desabasto") && i.Field<string>("Type").Equals("Compras")) 
                                   select i.Field<decimal>("limite")).FirstOrDefault();

                        limiteS = (from i in LIMITES.AsEnumerable()
                                   where (i.Field<string>("nombre").Equals(item.Field<string>("Comprador")) && i.Field<string>("tipo").Equals("Stockout") && i.Field<string>("Type").Equals("Compras"))
                                   select i.Field<decimal>("limite")).FirstOrDefault();

                        DataRow rowDesabasto = indicadorCompras1.NewRow();
                        rowDesabasto["Comprador"] = item.Field<string>("Comprador");

                        if (total != decimal.Zero)
                        {
                            if (desabasto / total > limiteD) rowDesabasto["Cumplimiento"] = "No cumple";
                            else rowDesabasto["Cumplimiento"] = "Si cumple";
                            rowDesabasto["% Desabasto"] = desabasto / total;
                        }
                        else rowDesabasto["% Desabasto"] = decimal.Zero;

                       

                        rowDesabasto["% Permitido"] = limiteD;
                        

                        indicadorCompras1.Rows.Add(rowDesabasto);

                        DataRow rowStockout = indicadorCompras2.NewRow();
                        rowStockout["Comprador"] = item.Field<string>("Comprador");
                        rowStockout["% Permitido"] = limiteS;

                        if (total != decimal.Zero)
                        {
                            if (stockout / total > limiteS) rowStockout["Cumplimiento"] = "No cumple";
                            else rowStockout["Cumplimiento"] = "Si cumple";
                            rowStockout["% Desabasto"] = stockout / total;
                        }
                        else rowStockout["% Desabasto"] = decimal.Zero;

                       
                        
                        indicadorCompras2.Rows.Add(rowStockout);
                    }


                    ////////////////////////I N V E N T A R I O S 
                    total = decimal.Zero;
                    desabasto = decimal.Zero;
                    stockout = decimal.Zero;
                    limiteD = decimal.Zero;

                    totalInventarios = (from item in DATOS.AsEnumerable()
                                        where item.Field<string>("tipo").Equals("TotalI")
                                      select item).CopyToDataTable();

                    desabastoInventarios = (from item in DATOS.AsEnumerable()
                                            where item.Field<string>("tipo").Equals("DesabastoI")
                                        select item).CopyToDataTable();

                    stockourInventarios = (from item in DATOS.AsEnumerable()
                                           where item.Field<string>("tipo").Equals("StockoutI")
                                           select item).CopyToDataTable();

                    var qry = (from inv in totalInventarios.AsEnumerable()
                               select inv.Field<string>("inv")).Distinct().ToList();

                    foreach (string item in qry)
                    {
                        total = (from i in totalInventarios.AsEnumerable()
                                 where i.Field<string>("inv").Equals(item)
                                 select i.Field<decimal>("Total")).Sum();

                        desabasto = (from i in desabastoInventarios.AsEnumerable()
                                     where i.Field<string>("inv").Equals(item)
                                     select i.Field<decimal>("Total")).Sum();

                        stockout = (from i in stockourInventarios.AsEnumerable()
                                    where i.Field<string>("inv").Equals(item)
                                    select i.Field<decimal>("Total")).Sum();

                        limiteD = (from i in LIMITES.AsEnumerable()
                                   where (i.Field<string>("nombre").Equals(item) && i.Field<string>("tipo").Equals("Desabasto") && i.Field<string>("Type").Equals("Inventario"))
                                   select i.Field<decimal>("limite")).FirstOrDefault();

                        limiteS = (from i in LIMITES.AsEnumerable()
                                   where (i.Field<string>("nombre").Equals(item) && i.Field<string>("tipo").Equals("Stockout") && i.Field<string>("Type").Equals("Inventario"))
                                   select i.Field<decimal>("limite")).FirstOrDefault();

         

                        DataRow rowDesabasto = indicadorInventarios1.NewRow();
                        rowDesabasto["Comprador"] = item;
                        rowDesabasto["% Permitido"] = limiteD;

                        if (total != decimal.Zero)
                        {
                            rowDesabasto["% Desabasto"] = desabasto / total;
                            if (desabasto / total > limiteD) rowDesabasto["Cumplimiento"] = "No cumple";
                            else rowDesabasto["Cumplimiento"] = "Si cumple";
                        }
                        else rowDesabasto["% Desabasto"] = decimal.Zero;

                        indicadorInventarios1.Rows.Add(rowDesabasto);

                        DataRow rowStockout = indicadorInventarios2.NewRow();
                        rowStockout["Comprador"] = item;
                        rowStockout["% Permitido"] = limiteS;

                        if (total != decimal.Zero)
                        {
                            if (stockout / total > limiteS) rowStockout["Cumplimiento"] = "No cumple";
                            else rowStockout["Cumplimiento"] = "Si cumple";
                            rowStockout["% Desabasto"] = stockout / total;
                        }
                        else rowStockout["% Desabasto"] = decimal.Zero;

                        indicadorInventarios2.Rows.Add(rowStockout);
                    }
                    
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void workerCompras_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                dgvTotalesC.DataSource = totalesCompras;
                dgvDesabastoC.DataSource = desabastoCompras;
                dgvStockoutC.DataSource = stockoutCompras;

                dgvIndicador1.DataSource = indicadorCompras1;
                dgvIndicador2.DataSource = indicadorCompras2;

                dgvTotalesI.DataSource = totalInventarios;
                dgvDesabastoI.DataSource = desabastoInventarios;
                dgvInventarioStock.DataSource = stockourInventarios;
                dgvIndicadorInv1.DataSource = indicadorInventarios1;
                dgvIndicadorInv2.DataSource = indicadorInventarios2;

                this.Formato(dgvTotalesC);
                this.Formato(dgvDesabastoC);
                this.Formato(dgvStockoutC);


                this.Formato(dgvTotalesI);
                this.Formato(dgvDesabastoI);
                this.Formato(dgvInventarioStock);

                this.FormatoIndicadores(dgvIndicador1);
                this.FormatoIndicadores(dgvIndicador2);

                this.FormatoIndicadores(dgvIndicadorInv1);
                this.FormatoIndicadores(dgvIndicadorInv2);

                toolStripStatusLabel1.Text = "Listo";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void workerCompras_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
           
        }

        private void workerLimites_DoWork(object sender, DoWorkEventArgs e)
        {

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Desabasto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Linea", string.Empty);
                    command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
                    command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);

                    command.CommandTimeout = 0;

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {

                        da.SelectCommand = command;
                        da.Fill(LIMITES);
                    }
                }
            }

            dgvComprasLimites.DataSource = null;
            dgvComprasLimites.DataSource = bindingSource1;
            GetData1(@"  Select 
			                Code, Type, Indicador, Comprador, Mes1 Enero, Mes2 Febrero, Mes3 Marzo, Mes4 Abril,
								                  Mes5 Mayo, Mes6 Junio, Mes7 Julio, Mes8 Agosto,
								                  Mes9 Septiembre, Mes10 Octubre, Mes11 Noviembre, Mes12 Diciembre
		                From Tbl_IndicadoresCompras
		                Where Active = 1
                              AND Type = 'Compras'");

            this.FormatoLimites(dgvComprasLimites);


            dgvInventariosLimites.DataSource = null;
            dgvInventariosLimites.DataSource = bindingSource2;
            GetData2(@"  Select 
			                Code, Type, Indicador, Comprador, Mes1 Enero, Mes2 Febrero, Mes3 Marzo, Mes4 Abril,
								                  Mes5 Mayo, Mes6 Junio, Mes7 Julio, Mes8 Agosto,
								                  Mes9 Septiembre, Mes10 Octubre, Mes11 Noviembre, Mes12 Diciembre
		                From Tbl_IndicadoresCompras
		                Where Active = 1
                            AND Type = 'Inventario'");
            if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador)
            {
                this.FormatoLimites(dgvInventariosLimites);
            }

        }

        private void workerLimites_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            workerCompras.RunWorkerAsync();
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)ColumnIndicadores.PCumplimiento].Value) > decimal.Zero)
                    {
                        item.Cells[(int)ColumnIndicadores.PCumplimiento].Style.BackColor = Color.FromArgb(196, 215, 155);
                        item.Cells[(int)ColumnIndicadores.PCumplimiento].Style.ForeColor = Color.Black;

                        item.Cells[(int)ColumnIndicadores.PCumplimiento].Style.SelectionBackColor = Color.FromArgb(196, 215, 155);
                        item.Cells[(int)ColumnIndicadores.PCumplimiento].Style.SelectionForeColor = Color.Black;
                        //item.Cells[(int)ColumnIndicadores.Cumplimiento].Value = "Si cumple";
                    }
                    else
                    {
                        item.Cells[(int)ColumnIndicadores.PCumplimiento].Style.BackColor = Color.FromArgb(255, 199, 206);
                        item.Cells[(int)ColumnIndicadores.PCumplimiento].Style.ForeColor = Color.Black;

                        item.Cells[(int)ColumnIndicadores.PCumplimiento].Style.SelectionBackColor = Color.FromArgb(255, 199, 206);
                        item.Cells[(int)ColumnIndicadores.PCumplimiento].Style.SelectionForeColor = Color.Black;
                        //item.Cells[(int)ColumnIndicadores.Cumplimiento].Value = "No cumple";
                    }
                }
            }
            catch (Exception) { }
                

        }

        private void dgvTotalesI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)Columnas.penalizacion)
                {
                    if ((sender as DataGridView).AccessibleName != "total")
                    {
                        var qry = from item in DATOS.AsEnumerable()
                                  where item.Field<string>("Comprador") == (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Comprador].Value.ToString()
                                     && item.Field<string>("inv") == (sender as DataGridView).AccessibleDescription
                                      && item.Field<string>("tipo") == (sender as DataGridView).AccessibleName
                                  select new
                                  {
                                      Articulo = item.Field<string>("articulo"),
                                      Tipo = item.Field<string>("tipo"),
                                      Porcentaje = item.Field<decimal>("porcentaje")
                                  };

                        if (qry.Count() > 0)
                        {
                            DataTable d = Cobranza.ListConverter.ToDataTable(qry.ToList());

                            IndicadorDesabasto.frmPenalizaciones form = new frmPenalizaciones(d);
                            form.ShowDialog();
                        }
                    }
                    else
                    {
                        var qry = from item in DATOS.AsEnumerable()
                                  where item.Field<string>("Comprador") == (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Comprador].Value.ToString()
                                     && item.Field<string>("inv") == (sender as DataGridView).AccessibleDescription
                                     //&& item.Field<string>("tipo") == (sender as DataGridView).AccessibleName
                                  select new
                                  {
                                      Articulo = item.Field<string>("articulo"),
                                      Tipo = item.Field<string>("tipo"),
                                      Porcentaje = item.Field<decimal>("porcentaje")
                                  };

                        if (qry.Count() > 0)
                        {
                            DataTable d = Cobranza.ListConverter.ToDataTable(qry.ToList());

                            IndicadorDesabasto.frmPenalizaciones form = new frmPenalizaciones(d);
                            form.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var qry = from item in DATOS.AsEnumerable()
                      where item.Field<string>("inv") == "I"
                      select item;

            if (qry.Count() > 0)
            {
                Compras.frmDesabasto frm = new frmDesabasto(qry);
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            else
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                dataAdapter1.Update((DataTable)bindingSource1.DataSource);
                GetData1(dataAdapter1.SelectCommand.CommandText);

                dataAdapter2.Update((DataTable)bindingSource2.DataSource);
                GetData2(dataAdapter2.SelectCommand.CommandText);

                dataAdapter3.Update((DataTable)bindingSource3.DataSource);
                GetData3(dataAdapter3.SelectCommand.CommandText);

                this.SaveValue(txtCA.Text, "C_A");
                this.SaveValue(txtCB.Text, "C_B");
                this.SaveValue(txtCC.Text, "C_C");

                this.SaveValue(txtSA.Text, "S_A");
                this.SaveValue(txtSB.Text, "S_B");
                this.SaveValue(txtSC.Text, "S_C");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void dgvComprasLimites_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[(int)ColumsLimites.Tipo].Value = "Compras";
        }

        private void dgvInventariosLimites_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[(int)ColumsLimites.Tipo].Value = "Inventario";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Indicadores_Load(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Desabasto", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@Linea", string.Empty);
                        command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
                        command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
                        command.Parameters.AddWithValue("@Proveedor", string.Empty);

                        command.CommandTimeout = 0;

                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            DataTable TBL_VI = new DataTable();
                            da.SelectCommand = command;
                            da.Fill(TBL_VI);

                            this.VI(TBL_VI, dgvVI1, "A");
                            this.VI(TBL_VI, dgvVI2, "B");
                            this.VI(TBL_VI, dgvVI3, "C");
                            this.VI(TBL_VI, dgvVI4, "A");
                            this.VI(TBL_VI, dgvVI5, "B");
                            this.VI(TBL_VI, dgvVI6, "C");

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
                Cursor = Cursors.Default;
            }
        }


        public void VI(DataTable tbl, DataGridView dgv, string _clasificacion)
        {
            var qry = from item in tbl.AsEnumerable()
                      where item.Field<string>("dgv") == dgv.Name
                      select item;
            DataTable table = new DataTable();
            if (qry.Count() > 0)
            {
                table = qry.CopyToDataTable();
                dgv.DataSource = table;
                this.FormatoVI(dgv, _clasificacion);
            }


        }

        private void dgv_DataBindingCompleteVI(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToString(item.Cells[(int)ColumnasVI.Status].Value).Equals("Si Cumple"))
                    {
                        item.Cells[(int)ColumnasVI.Status].Style.BackColor = Color.FromArgb(196, 215, 155);
                        item.Cells[(int)ColumnasVI.Status].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        item.Cells[(int)ColumnasVI.Status].Style.BackColor = Color.FromArgb(255, 199, 206);
                        item.Cells[(int)ColumnasVI.Status].Style.ForeColor = Color.Black; 
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Indicadores_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void Indicadores_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                log.Fin();
            }
        }

        private void R(object sender, EventArgs e)
        {
            frmAnalisisCompras form = new frmAnalisisCompras();
            form.MdiParent = this.MdiParent;
            form.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }
    }
}
