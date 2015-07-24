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

namespace Compras
{
    public partial class frmAcumuladoCompraVenta : Form
    {
        Clases.Logs log;
        DataTable Datos = new DataTable();
        DataTable Items = new DataTable();

        public enum Columnas
        {
            Linea, Articulo, Descripcion, Ideal, Stock, Solicitado, Venta, Compra
        }
       
        public enum ColumnasAcum
        {
            Linea, Articulo, Descripcion, Ideal, Stock, Solicitado, Venta, Compra, IdealAcum, StockAcum, SolicitadoAcum, VentaAcum, CompraAcum
        }

        public frmAcumuladoCompraVenta()
        {
            InitializeComponent();
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Linea].Width = 100;
            dgv.Columns[(int)Columnas.Articulo].Width = 100;
            dgv.Columns[(int)Columnas.Descripcion].Width = 250;
            dgv.Columns[(int)Columnas.Ideal].Width = 80;
            dgv.Columns[(int)Columnas.Stock].Width = 80;
            dgv.Columns[(int)Columnas.Venta].Width = 80;
            dgv.Columns[(int)Columnas.Compra].Width = 80;

            dgv.Columns[(int)Columnas.Ideal].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Solicitado].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Venta].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Compra].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)ColumnasAcum.Compra].HeaderText = "Compra";
            dgv.Columns[(int)ColumnasAcum.Venta].HeaderText = "Venta";

            dgv.Columns[(int)Columnas.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)Columnas.Solicitado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)Columnas.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)Columnas.Compra].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
        }

        public void FormatoAcum(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasAcum.Linea].Width = 100;
            dgv.Columns[(int)ColumnasAcum.Articulo].Width = 100;
            dgv.Columns[(int)ColumnasAcum.Descripcion].Width = 250;
            dgv.Columns[(int)ColumnasAcum.Ideal].Width = 80;
            dgv.Columns[(int)ColumnasAcum.Stock].Width = 80;
            dgv.Columns[(int)ColumnasAcum.Venta].Width = 80;
            dgv.Columns[(int)ColumnasAcum.Compra].Width = 80;
            dgv.Columns[(int)ColumnasAcum.IdealAcum].Width = 80;
            dgv.Columns[(int)ColumnasAcum.StockAcum].Width = 80;
            dgv.Columns[(int)ColumnasAcum.SolicitadoAcum].Width = 80;
            dgv.Columns[(int)ColumnasAcum.VentaAcum].Width = 80;
            dgv.Columns[(int)ColumnasAcum.CompraAcum].Width = 80;

            dgv.Columns[(int)ColumnasAcum.Ideal].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.Stock].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.Venta].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.Solicitado].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.Compra].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.Venta].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.IdealAcum].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.StockAcum].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.SolicitadoAcum].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.VentaAcum].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasAcum.CompraAcum].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)ColumnasAcum.Compra].HeaderText = "Compra";
            dgv.Columns[(int)ColumnasAcum.Venta].HeaderText = "Venta";
            dgv.Columns[(int)ColumnasAcum.IdealAcum].HeaderText = "Ideal acumulado";
            dgv.Columns[(int)ColumnasAcum.StockAcum].HeaderText = "Sotck acumulado";
            dgv.Columns[(int)ColumnasAcum.SolicitadoAcum].HeaderText = "Solicitado acumulado";
            dgv.Columns[(int)ColumnasAcum.VentaAcum].HeaderText = "Venta acumulada";
            dgv.Columns[(int)ColumnasAcum.CompraAcum].HeaderText = "Compra acumulada";

            dgv.Columns[(int)ColumnasAcum.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.Solicitado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.Compra].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.IdealAcum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.StockAcum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.SolicitadoAcum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.VentaAcum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)ColumnasAcum.CompraAcum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
        }

        private void CargarDatos()
        {
            string Lineas = this.GetCadena(clbLineas);
            string Proveedores = this.GetCadena(clbProveedor);
            
            using (SqlConnection connection =  new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AcumuladoCompraVenta";
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                    command.Parameters.AddWithValue("@Lineas", Lineas);
                    command.Parameters.AddWithValue("@Proveedores", Proveedores);
                    command.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@FechaFinal", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@Almacenes", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter();
                    ad.SelectCommand = command;
                    ad.SelectCommand.CommandTimeout = 0;
                    ad.Fill(table);

                    gridExceso.DataSource = table;
                    Datos = table.Copy();
                }
            }
        }

        private DataTable Acumulado()
        {
            string Lineas = this.GetCadena(clbLineas);
            string Proveedores = this.GetCadena(clbProveedor);
            string Almacenes = this.GetCadena(clbAlmacenes);

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AcumuladoCompraVenta";
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                    command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                    command.Parameters.AddWithValue("@Lineas", Lineas);
                    command.Parameters.AddWithValue("@Proveedores", Proveedores);
                    command.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@FechaFinal", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@Almacenes", Almacenes);

                    DataTable table = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter();
                    ad.SelectCommand = command;
                    ad.SelectCommand.CommandTimeout = 0;
                    ad.Fill(table);

                    return table;
                }
            }
        }

        public void CargarLinea(CheckedListBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public void CargarProveedores(CheckedListBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public string GetCadena(CheckedListBox clb)
        {
            StringBuilder stb= new StringBuilder();
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

        public void CargarItems()
        {

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AcumuladoCompraVenta";
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 4);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Lineas", string.Empty);
                    command.Parameters.AddWithValue("@Proveedores", string.Empty);
                    command.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@FechaFinal", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@Almacenes", string.Empty);

                    SqlDataAdapter ad = new SqlDataAdapter();
                    ad.SelectCommand = command;
                    ad.SelectCommand.CommandTimeout = 0;
                    ad.Fill(Items);
                }
            }
        }

        public string GetCadena(ComponentFactory.Krypton.Toolkit.KryptonCheckedListBox clb)
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

        public static AutoCompleteStringCollection Autocomplete(DataTable _t, string _column)
        {
            DataTable dt = _t;

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row[_column]));
            }

            return coleccion;
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                this.CargarDatos();
                this.Formato(gridExceso);
            }
            catch (Exception )
            {
            }
            finally
            {
                this.Continuar();
            }
        }

        private void AcumuladoCompraVenta_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                this.CargarLinea(clbLineas, "TODAS");
                this.CargarProveedores(clbProveedor, "TODOS");
                this.CargarAlmacenes();
                this.CargarItems();

                txtArticulo.AutoCompleteCustomSource = Autocomplete(Items, "ItemCode");
                txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void clbLineas_Click(object sender, EventArgs e)
        {
            if (clbLineas.SelectedIndex == 0)
            {
                if (clbLineas.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbLineas.Items.Count; item++)
                    {
                        clbLineas.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbLineas.Items.Count; item++)
                    {
                        clbLineas.SetItemChecked(item, true);
                    }
                }
            }
        }

        private void AcumuladoCompraVenta_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void AcumuladoCompraVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
        }

        private void CargarAlmacenes()
        {
            //string Lineas = this.GetCadena(clbLineas);
           // string Proveedores = this.GetCadena(clbProveedor);

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AcumuladoCompraVenta";
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                    command.Parameters.AddWithValue("@Lineas", string.Empty);
                    command.Parameters.AddWithValue("@Proveedores", string.Empty);
                    command.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@FechaFinal", dateTimePicker2.Value);
                    command.Parameters.AddWithValue("@Almacenes", string.Empty);
                    
                    DataTable table = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter();
                    ad.SelectCommand = command;
                    ad.Fill(table);

                    clbAlmacenes.ListBox.DataSource = table;
                    clbAlmacenes.ListBox.DisplayMember = "Nombre";
                    clbAlmacenes.ListBox.ValueMember = "Codigo";
                }
            }
        }

        private void clbAlmacenes_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                DataTable TBL = this.Acumulado();
                this.Formato(gridExceso);
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Continuar();
            }
           
        }

        private void clbAlmacenes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
               
                this.Esperar();
                DataTable TBL = this.Acumulado();
                gridExceso.DataSource = null;
                var query = from item1 in Datos.AsEnumerable()
                            join item2 in TBL.AsEnumerable() on
                                item1.Field<string>("Artículo") equals item2.Field<string>("Artículo") into JoinedPJSucursal
                            from item2 in JoinedPJSucursal.DefaultIfEmpty()
                            select new
                            {
                                Linea = item1.Field<string>("Linea"),
                                Articulo = item1.Field<string>("Artículo"),
                                Descripcion = item1.Field<string>("Descripción"),
                                Ideal = item1.Field<Int32>("Ideal"),
                                Stock = item1.Field<decimal>("Stock"),
                                Solicitado = item1.Field<decimal>("Solicitado"),
                                TotalVenta = item1.Field<decimal>("Total Venta"),
                                TotalCompra = item1.Field<decimal>("Total Compra"),

                                IdealAcumulado = item2 == null ? 0 : item2.Field<Int32>("Ideal"),
                                StockAcumulado = item2 == null ? 0 : item2.Field<decimal>("Stock"),
                                SolicitadoAcumulado = item2 == null ? 0 : item2.Field<decimal>("Solicitado"),
                                TotalVentaAcumulado = item2 == null ? 0 : item2.Field<decimal>("Total Venta"),
                                TotalCompraAcumulado = item2 == null ? 0 : item2.Field<decimal>("Total Compra"),
                            };


                gridExceso.DataSource = query.ToList();
               
                this.FormatoAcum(gridExceso);
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Continuar();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarAExcel exp = new ExportarAExcel();
                DialogResult dialogResult = MessageBox.Show("¿Exportar sin formato?\r\n Si elige 'No' el proceso puede durar varios minutos.", "HalcoNET", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something
                    if (exp.ExportarSinFormato(gridExceso))
                        MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something 
                    if (exp.Exportar(gridExceso))
                        MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {
            }
            finally
            {
            }
        }
    }
}
