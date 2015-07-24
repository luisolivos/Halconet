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

namespace Compras.Transferencias_Compras
{
    public partial class Compras : Form
    {
        #region PARAMETROS
        Clases.Logs log;
        DataTable Almacenes = new DataTable();
        string AlmacenesSeleccionados;
        string AlmacenCabecera;
        DataTable Articul = new DataTable();

        public enum Columas
        {
            Linea, Articulo, Descripcion, Clasificacion, Stock, Ideal, Comprar, Price, VecesIdeal, ComprarEdit, VI, Total
        }

        public enum ColumnasDetalle
        {
            Articulo, Stock01, Ideal01, Stock02, Ideal02, Stock03, Ideal03, Stock05, Ideal05, Stock06, Ideal06, Stock16, Ideal16, Stock18, Ideal18, Stock00, Ideal00, StockImp, IdealImp
        }

        #endregion

        private void Transferencias_Compras_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.ListaArticulos();

            txtArticulo.AutoCompleteCustomSource = Autocomplete(Articul, "ItemCode");
            txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            CargarLinea(cbLinea, "");

            CargarProveedores(cbProveedor, "");
        }

        
        
        #region METODOS

        public DataTable ListaArticulos()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_ReparticionStock";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@CantiadOK", decimal.Zero);
                    command.Parameters.AddWithValue("@Incremento", decimal.Zero);

                    command.CommandTimeout = 0;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(Articul);
                    
                    return Articul;
                }
            }
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



        public Compras()
        {
            InitializeComponent();
            Almacenes.Columns.Add("Almacen", typeof(string));

            DataRow r01 = Almacenes.NewRow();
            r01["Almacen"] = "01 - PUE";
            Almacenes.Rows.Add(r01);

            DataRow r02 = Almacenes.NewRow();
            r02["Almacen"] = "02 - MTY";
            Almacenes.Rows.Add(r02);

            DataRow r03 = Almacenes.NewRow();
            r03["Almacen"] = "03 - API";
            Almacenes.Rows.Add(r03);

            DataRow r05 = Almacenes.NewRow();
            r05["Almacen"] = "05 - COR";
            Almacenes.Rows.Add(r05);

            DataRow r06 = Almacenes.NewRow();
            r06["Almacen"] = "06 - TEP";
            Almacenes.Rows.Add(r06);

            DataRow r16 = Almacenes.NewRow();
            r16["Almacen"] = "16 - MEX";
            Almacenes.Rows.Add(r16);

            DataRow r18 = Almacenes.NewRow();
            r18["Almacen"] = "18 - GDL";
            Almacenes.Rows.Add(r18);
        }

        public void CargarLinea(ComboBox _cb, string _inicio)
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

        public void CargarProveedores(ComboBox _cb, string _inicio)
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

        public void CargarTotales()
        {
            AlmacenesSeleccionados = string.Empty;

            if (cbZCENTRO.Checked) AlmacenCabecera = cbZCENTRO.AccessibleName;
            else if (cbZNORTE.Checked) AlmacenCabecera = cbZNORTE.AccessibleName;
            else AlmacenCabecera = string.Empty;

            if (cbZCENTRO.Checked)
            {
                if (cbAPI.Checked)
                    AlmacenesSeleccionados += cbAPI.AccessibleName + ",";
                if (cbTEP.Checked)
                    AlmacenesSeleccionados += cbTEP.AccessibleName + ",";
                if (cbMEX.Checked)
                    AlmacenesSeleccionados += cbMEX.AccessibleName + ",";
                if (cbCOR.Checked)
                    AlmacenesSeleccionados += cbCOR.AccessibleName + ",";
                if (cbPUE.Checked)
                    AlmacenesSeleccionados += cbPUE.AccessibleName + ",";
                if (cbZCENTRO.Checked)
                    AlmacenesSeleccionados += cbZCENTRO.AccessibleName + ",";
            }
            else if (cbZNORTE.Checked)
            {
                if (cbGDL.Checked)
                    AlmacenesSeleccionados += cbGDL.AccessibleName + ",";
                if (cbMTY.Checked)
                    AlmacenesSeleccionados += cbMTY.AccessibleName + ",";
                if (cbSAL.Checked)
                    AlmacenesSeleccionados += cbSAL.AccessibleName + ",";
                if (cbZNORTE.Checked)
                    AlmacenesSeleccionados += cbZNORTE.AccessibleName + ",";
            }
            else
            {
                if (cbAPI.Checked)
                    AlmacenesSeleccionados += cbAPI.AccessibleName + ",";
                if (cbTEP.Checked)
                    AlmacenesSeleccionados += cbTEP.AccessibleName + ",";
                if (cbMEX.Checked)
                    AlmacenesSeleccionados += cbMEX.AccessibleName + ",";
                if (cbCOR.Checked)
                    AlmacenesSeleccionados += cbCOR.AccessibleName + ",";
                if (cbPUE.Checked)
                    AlmacenesSeleccionados += cbPUE.AccessibleName + ",";
                if (cbGDL.Checked)
                    AlmacenesSeleccionados += cbGDL.AccessibleName + ",";
                if (cbMTY.Checked)
                    AlmacenesSeleccionados += cbMTY.AccessibleName + ",";
                if (cbSAL.Checked)
                    AlmacenesSeleccionados += cbSAL.AccessibleName + ",";
            }

            if (cbImportacion.Checked) AlmacenesSeleccionados = "03,05,16,18,02,01,06,14,23";

            if (!string.IsNullOrEmpty(AlmacenesSeleccionados))
            {
                SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                if (cbTodo.Checked)
                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                else
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                command.Parameters.AddWithValue("@AlmacenCabecera", AlmacenCabecera);
                command.Parameters.AddWithValue("@Almacenes", AlmacenesSeleccionados);
                command.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);
                command.Parameters.AddWithValue("@Proveedores", cbProveedor.SelectedValue);
                command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(table);

                table.Columns.Add("Veces ideal", typeof(decimal));
                
                table.Columns.Add("Comprar modif.", typeof(decimal));
                table.Columns.Add("VI", typeof(decimal));
                table.Columns.Add("Total", typeof(decimal),"[Comprar modif.] * Price");

                table.Columns["Veces ideal"].Expression = "IIF(Ideal = 0, 0, (Comprar+Stock)/Ideal)";
                foreach (DataRow  item in table.Rows)
                {
                    item.SetField<decimal>("Comprar modif.", item.Field<decimal>("Comprar"));
                }
                gridCompras.DataSource = table;

                Formato(gridCompras);

                if (((DataTable)gridCompras.DataSource).Rows.Count > 0)
                {
                    decimal total = decimal.Zero;
                    total = Convert.ToDecimal(((DataTable)gridCompras.DataSource).Compute("SUM(Total)", string.Empty));
                    label3.Text = "Total: " + total.ToString("C2");
                }

                lblHorizonte.Text = string.Empty;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command1 = new SqlCommand("PJ_Compras", connection))
                    {
                        command1.CommandType = CommandType.StoredProcedure;
                        command1.Parameters.AddWithValue("@TipoConsulta", 4);
                        command1.Parameters.AddWithValue("@AlmacenCabecera", AlmacenCabecera);
                        command1.Parameters.AddWithValue("@Almacenes", AlmacenesSeleccionados);
                        command1.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);
                        command1.Parameters.AddWithValue("@Proveedores", cbProveedor.SelectedValue);
                        command1.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                        connection.Open();
                        SqlDataReader reader = command1.ExecuteReader();
                        if (reader.Read())
                        {
                            lblHorizonte.Text = "Horizonte de planeacion: " + reader.GetDecimal(0).ToString("N0");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione almacenes.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void CargarDetalle(string _articulo, string _almacen, DataGridView _dg)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 1);
            command.Parameters.AddWithValue("@Articulo", _articulo);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@AlmacenDestino", _almacen);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            table.TableName = "Detalle";
            _dg.DataSource = table;
        }

        public void CargarDetalleCompras(string _articulo, string _almacen, DataGridView _dg)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 1);
            command.Parameters.AddWithValue("@Articulo", _articulo);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@AlmacenDestino", _almacen);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            table.TableName = "Detalle";
            _dg.DataSource = table;
        }

        public void CargarVentas(string _articulo, string _almacen, DataGridView _dg)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);
            command.Parameters.AddWithValue("@Articulo", _articulo);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@AlmacenDestino", _almacen);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            table.TableName = "Detalle";
            _dg.DataSource = table;
        }

        public void FormatoDetalle(DataGridView _dg)
        {
            _dg.Columns[(int)ColumnasDetalle.Articulo].Width = 80;
            _dg.Columns[(int)ColumnasDetalle.Stock01].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Ideal01].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Stock02].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Ideal02].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Stock03].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Ideal03].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Stock05].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Ideal05].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Stock06].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Ideal06].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Stock16].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Ideal16].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Stock18].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Ideal18].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Stock00].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.Ideal00].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.StockImp].Width = 70;
            _dg.Columns[(int)ColumnasDetalle.IdealImp].Width = 70;

            _dg.Columns[(int)ColumnasDetalle.Stock01].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Ideal01].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Stock02].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Ideal02].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Stock03].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Ideal03].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Stock05].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Ideal05].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Stock06].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Ideal06].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Stock16].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Ideal16].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Stock18].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Ideal18].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Stock00].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Ideal00].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.StockImp].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.IdealImp].DefaultCellStyle.Format = "N0";

            _dg.Columns[(int)ColumnasDetalle.Stock01].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Ideal01].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Stock02].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Ideal02].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Stock03].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Ideal03].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Stock05].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Ideal05].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Stock06].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Ideal06].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Stock16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Ideal16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Stock18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Ideal18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Stock00].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Ideal00].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.StockImp].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.IdealImp].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            string[] almacenes = AlmacenesSeleccionados.Split(',');
            foreach (DataGridViewColumn item in _dg.Columns)
            {
                if (!almacenes.Contains(item.HeaderText.Substring(0, 2)))
                {
                    item.Visible = false;
                }
            }
            _dg.Columns[(int)ColumnasDetalle.Articulo].Visible = true;
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columas.Linea].Width = 90;
            dgv.Columns[(int)Columas.Articulo].Width = 90;
            dgv.Columns[(int)Columas.Clasificacion].Width = 70;
            dgv.Columns[(int)Columas.Descripcion].Width = 250;
            dgv.Columns[(int)Columas.Stock].Width = 90;
            dgv.Columns[(int)Columas.Ideal].Width = 90;
            dgv.Columns[(int)Columas.Comprar].Width = 80;
            dgv.Columns[(int)Columas.VecesIdeal].Width = 50;
            dgv.Columns[(int)Columas.ComprarEdit].Width = 70;
            dgv.Columns[(int)Columas.VI].Width = 70;
            dgv.Columns[(int)Columas.Price].Visible = false;


            dgv.Columns[(int)Columas.Linea].ReadOnly = true;
            dgv.Columns[(int)Columas.Articulo].ReadOnly = true;
            dgv.Columns[(int)Columas.Clasificacion].ReadOnly = true;
            dgv.Columns[(int)Columas.Descripcion].ReadOnly = true;
            dgv.Columns[(int)Columas.Stock].ReadOnly = true;
            dgv.Columns[(int)Columas.Ideal].ReadOnly = true;
            dgv.Columns[(int)Columas.VecesIdeal].ReadOnly = true;
            dgv.Columns[(int)Columas.Comprar].ReadOnly = true;

            dgv.Columns[(int)Columas.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columas.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columas.Comprar].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columas.VecesIdeal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columas.ComprarEdit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
            dgv.Columns[(int)Columas.VI].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columas.Stock].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.Ideal].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.Comprar].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.VecesIdeal].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)Columas.ComprarEdit].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.VI].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)Columas.Total].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columas.ComprarEdit].DefaultCellStyle.BackColor = Color.LightGray;
            dgv.Columns[(int)Columas.VI].DefaultCellStyle.BackColor = Color.LightGray;
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

        #endregion

        #region EVENTOS
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                label3.Text = string.Empty;
                Esperar();
                gridCompras.DataSource = null;
                gridCompras.Columns.Clear();
                gridDetalle.DataSource = null;
                gridVentas.DataSource = null;

                CargarTotales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtArticulo.Clear();
            cbLinea.SelectedIndex = 0;
            cbProveedor.SelectedIndex = 0;

            cbZCENTRO.Checked = false;
            cbZNORTE.Checked = false;
            cbImportacion.Checked = false;
            cbAPI.Checked = false;
            cbTEP.Checked = false;
            cbMEX.Checked = false;
            cbCOR.Checked = false;
            cbPUE.Checked = false;

            cbGDL.Checked = false;
            cbMTY.Checked = false;
            cbSAL.Checked = false;

            gridCompras.DataSource = null;
            gridDetalle.DataSource = null;
            gridVentas.DataSource = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExportarAExcel ex = new ExportarAExcel();
            if(ex.Exportar(gridCompras))
            {
                MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Transferencias_Compras_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void Transferencias_Compras_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
                log.Fin();
            }

        }

        private void chb03_Click(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                cbAPI.Checked = true;
                cbCOR.Checked = true;
                cbMEX.Checked = true;
                cbPUE.Checked = true;
                cbTEP.Checked = true;

                cbMTY.Checked = false;
                cbGDL.Checked = false;
                cbZNORTE.Checked = false; 
                cbImportacion.Checked = false;
                cbSAL.Checked = false;
            }
            else
            {
                cbAPI.Checked = false;
                cbCOR.Checked = false;
                cbMEX.Checked = false;
                cbPUE.Checked = false;
                cbTEP.Checked = false;
            }
        }
        
        private void chb16_Click(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                cbMTY.Checked = true;
                cbGDL.Checked = true;

                cbAPI.Checked = false;
                cbCOR.Checked = false;
                cbMEX.Checked = false;
                cbPUE.Checked = false;
                cbTEP.Checked = false;
                cbZCENTRO.Checked = false;
                cbImportacion.Checked = false;
                cbSAL.Checked = true;
            }
            else
            {
                cbMTY.Checked = false;
                cbGDL.Checked = false;
                cbSAL.Checked = false;
            }
        }

        private void gridCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string _articulo =  (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columas.Articulo].Value.ToString();
                SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 2);
                command.Parameters.AddWithValue("@AlmacenCabecera", string.Empty);
                command.Parameters.AddWithValue("@Almacenes", AlmacenesSeleccionados);
                command.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);
                command.Parameters.AddWithValue("@Proveedores", string.Empty);
                command.Parameters.AddWithValue("@Articulo", _articulo);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(table);

                gridDetalle.DataSource = table;
                this.FormatoDetalle(gridDetalle);
            }
            catch (Exception)
            {
            }
        }

        private void gridDetalle_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    string _articulo = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Articulo].Value.ToString();
                    string _almacen = (sender as DataGridView).Columns[e.ColumnIndex].HeaderText.Substring(0, 2);

                    //this.CargarVentas(_articulo, _almacen, gridVentas);
                    SqlCommand command = new SqlCommand("sp_HistorialVentas");
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Almacen", _almacen);
                    command.Parameters.AddWithValue("@Articulo", _articulo);

                    Clases.Ventas vts = new Clases.Ventas();
                    gridVentas.DataSource = vts.GetVentas(command);

                    foreach (DataGridViewColumn item in gridVentas.Columns)
                    {
                        if (item.Index > 1)
                        {
                            item.Width = 60;
                            item.DefaultCellStyle.Format = "N0";
                            item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        else
                        {
                            item.Width = 75;
                            item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        }

                    }
                }

            }
            catch (Exception)
            {

            }
        }

        private void cbImportacion_Click(object sender, EventArgs e)
        {
            if (cbImportacion.Checked)
            {
                cbZCENTRO.Checked = false;
                cbZNORTE.Checked = false;

                cbAPI.Checked = false;
                cbMTY.Checked = false;
                cbPUE.Checked = false;
                cbCOR.Checked = false;
                cbTEP.Checked = false;
                cbMEX.Checked = false;
                cbGDL.Checked = false;
                cbSAL.Checked = false;
            }
        }
        
        #endregion 

        private void gridCompras_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void gridCompras_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)Columas.VI)
                {
                    DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                    row.Cells[(int)Columas.ComprarEdit].Value = (Convert.ToDecimal(row.Cells[(int)Columas.Ideal].Value) * Convert.ToDecimal(row.Cells[(int)Columas.VI].Value)) - Convert.ToDecimal(row.Cells[(int)Columas.Stock].Value);                   
                }

                if (((DataTable)(sender as DataGridView).DataSource).Rows.Count > 0)
                {
                    decimal total = decimal.Zero;
                    total = Convert.ToDecimal(((DataTable)(sender as DataGridView).DataSource).Compute("SUM(Total)", string.Empty));
                    label3.Text = "Total: " + total.ToString("C2");
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
