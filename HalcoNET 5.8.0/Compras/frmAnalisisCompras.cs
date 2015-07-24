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

namespace Compras
{
    public partial class frmAnalisisCompras : Form
    {
        public string Lineas;
        public decimal VecesIdeal;
        public string Articulo;
        public DataTable Analisis = new DataTable();
        Clases.Logs log;

        public enum ColumasGrid
        {
            Linea, Proveedor, Articulo, Descripcion, Clasificacion, Remate, Dev, Descontinuado,
            CompraEspecial, PLinea, PPC, Ideal, IdealM, Stock, StockM, Diferencia, DiferenciaM,
            VecesIdeal, Meses, Tipo, Dias, PlanningCode, Solicitado, SolicitadoM, Consigna, Promocion, 
            Comprador, Compromiso, Nuevo
          //  Col1, Col2, Col3, Col4, Col5, Col6
        }
        
        public frmAnalisisCompras()
        {
            InitializeComponent();

            Lineas = "";
            VecesIdeal = 0;
            Articulo = "";
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

        public void CargarDetalle(string _articulo, DataGridView dg)
        {
            SqlCommand command = new SqlCommand("PJ_RemateDevolucion", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);
            command.Parameters.AddWithValue("@Lineas", string.Empty);
            command.Parameters.AddWithValue("@VecesIdeal", 0);
            command.Parameters.AddWithValue("@Articulo", _articulo);
            command.Parameters.AddWithValue("@Tipo", "Todos");
            command.CommandTimeout = 0;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            dg.DataSource = table;
        }

        public void FormatoGrid()
        {
            gridExceso.Columns[(int)ColumasGrid.PlanningCode].Visible = false;
            gridExceso.Columns[(int)ColumasGrid.Dias].Visible = false;

            if (!chRemate.Checked & !chDevolucion.Checked & !chDescontinuado.Checked & !chbPPC.Checked & !cbbEspecial.Checked & chLinea.Checked)
            {
                gridExceso.Columns[(int)ColumasGrid.Meses].Visible = false;
                gridExceso.Columns[(int)ColumasGrid.Dias].Visible = true;
            }
            gridExceso.Columns[(int)ColumasGrid.Linea].Width = 70;
            gridExceso.Columns[(int)ColumasGrid.Meses].Width = 70;
            gridExceso.Columns[(int)ColumasGrid.Dias].Width = 70;
            gridExceso.Columns[(int)ColumasGrid.Articulo].Width = 100;
            gridExceso.Columns[(int)ColumasGrid.Descripcion].Width = 250;
            gridExceso.Columns[(int)ColumasGrid.Clasificacion].Width = 80;
            gridExceso.Columns[(int)ColumasGrid.Ideal].Width = 90;
            gridExceso.Columns[(int)ColumasGrid.IdealM].Width = 90;
            gridExceso.Columns[(int)ColumasGrid.Stock].Width = 90;
            gridExceso.Columns[(int)ColumasGrid.StockM].Width = 90;
            gridExceso.Columns[(int)ColumasGrid.Diferencia].Width = 90;
            gridExceso.Columns[(int)ColumasGrid.DiferenciaM].Width = 90;
            gridExceso.Columns[(int)ColumasGrid.DiferenciaM].Width = 80;

            gridExceso.Columns[(int)ColumasGrid.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.IdealM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.StockM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.DiferenciaM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.VecesIdeal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.Meses].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)ColumasGrid.PlanningCode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            gridExceso.Columns[(int)ColumasGrid.IdealM].DefaultCellStyle.Format = "C2";
            gridExceso.Columns[(int)ColumasGrid.StockM].DefaultCellStyle.Format = "C2";
            gridExceso.Columns[(int)ColumasGrid.DiferenciaM].DefaultCellStyle.Format = "C2";
            gridExceso.Columns[(int)ColumasGrid.Ideal].DefaultCellStyle.Format = "N0";
            gridExceso.Columns[(int)ColumasGrid.Stock].DefaultCellStyle.Format = "N0";
            gridExceso.Columns[(int)ColumasGrid.Diferencia].DefaultCellStyle.Format = "N0";
            gridExceso.Columns[(int)ColumasGrid.VecesIdeal].DefaultCellStyle.Format = "N2";

            gridExceso.Columns[(int)ColumasGrid.Remate].Width = 65;
            gridExceso.Columns[(int)ColumasGrid.Dev].Width = 65;
            gridExceso.Columns[(int)ColumasGrid.Descontinuado].Width = 65;
            gridExceso.Columns[(int)ColumasGrid.PPC].Width = 65;
            gridExceso.Columns[(int)ColumasGrid.CompraEspecial].Width = 65;
            gridExceso.Columns[(int)ColumasGrid.PLinea].Width = 65;

            gridExceso.Columns[(int)ColumasGrid.Consigna].Width = 65;
            gridExceso.Columns[(int)ColumasGrid.Promocion].Width = 65;
            gridExceso.Columns[(int)ColumasGrid.Compromiso].Width = 65;
        }

        public void FormatoDetalle()
        {
            foreach (DataGridViewColumn item in gridDetalle.Columns)
            {
                item.DefaultCellStyle.Format = "N0";
                item.Width = 50;
            }
        }

        public DataTable Filtro(DataTable t, decimal _veces)
        {
            DataTable _t = new DataTable();
            if (chRemate.Checked && chDevolucion.Checked && chDescontinuado.Checked)
            {
                var query = (from item in t.AsEnumerable()
                             where (item.Field<string>("Remate") == "Si"
                                   || item.Field<string>("Devolución") == "Si"
                                   || item.Field<string>("Descontinuado") == "Si")
                                   && item.Field<decimal>("Veces Ideal") > _veces
                             select item);

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                }
            }
            else if (chRemate.Checked && !chDevolucion.Checked && !chDescontinuado.Checked)
            {
                var query = (from item in t.AsEnumerable()
                      where item.Field<string>("Remate") == "Si"
                            && item.Field<decimal>("Veces Ideal") > _veces
                      select item);

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                }
            }
            else if (chRemate.Checked && chDevolucion.Checked && !chDescontinuado.Checked)
            {
                var query = (from item in t.AsEnumerable()
                             where (item.Field<string>("Remate") == "Si"
                                   || item.Field<string>("Devolución") == "Si")
                                   && item.Field<decimal>("Veces Ideal") > _veces
                             select item);

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                }
            }
            else if (chRemate.Checked && !chDevolucion.Checked && chDescontinuado.Checked)
            {
                var query = (from item in t.AsEnumerable()
                             where (item.Field<string>("Remate") == "Si"
                                   || item.Field<string>("Descontinuado") == "Si")
                                   && item.Field<decimal>("Veces Ideal") > _veces
                             select item);

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                }
            }
            else if (!chRemate.Checked && chDevolucion.Checked && !chDescontinuado.Checked)
            {
                var query = (from item in t.AsEnumerable()
                             where item.Field<string>("Devolución") == "Si"
                                   && item.Field<decimal>("Veces Ideal") > _veces
                             select item);

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                }
            }
            else if (!chRemate.Checked && chDevolucion.Checked && chDescontinuado.Checked)
            {
                var query = (from item in t.AsEnumerable()
                             where (item.Field<string>("Devolución") == "Si"
                             || item.Field<string>("Descontinuado") == "Si")
                             && item.Field<decimal>("Veces Ideal") > _veces
                             select item);

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                }
            }
            else if (!chRemate.Checked && !chDevolucion.Checked && chDescontinuado.Checked)
            {
                var query = (from item in t.AsEnumerable()
                             where item.Field<string>("Descontinuado") == "Si"
                                   && item.Field<decimal>("Veces Ideal") > _veces
                             select item);

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                }
            }
            else
            {
                var query = (from item in t.AsEnumerable()
                             where (item.Field<string>("Remate") == "No"
                             || item.Field<string>("Devolución") == "No"
                             || item.Field<string>("Descontinuado") == "No")
                             && item.Field<decimal>("Veces Ideal") > _veces
                             select item);

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                }
            }

            return _t;
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
        DataTable Articul = new DataTable();
        private void ExcesoStock_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.CargarLinea(clbSucursal, "Todas");
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

            this.ListaArticulos();

            txtArticulo.AutoCompleteCustomSource = Autocomplete(Articul, "ItemCode");
            txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;

        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                gridExceso.DataSource = null;
                gridDetalle.DataSource = null;
                gridVentas.DataSource = null;
                dataGridView1.DataSource = null;
                Analisis.Clear();

                Lineas = GetCadena(clbSucursal);
                Articulo = txtArticulo.Text;

                decimal veces = 0;
                try
                {
                    veces = decimal.Parse(txtVeces.Text);
                }
                catch (Exception)
                {
                    veces = 0;
                }
                string _tipo ="";
                if (chDevolucion.Checked)
                    _tipo += ",Devolucion";
                if (chRemate.Checked)
                    _tipo = ",Remate";
                if (chbPPC.Checked)
                    _tipo += ",PPC";
                if (chDescontinuado.Checked)
                    _tipo += ",Descontinuado";
                if (chLinea.Checked)
                    _tipo += ",Linea";
                if (cbbEspecial.Checked)
                    _tipo += ",Compra especial";
                if (String.IsNullOrEmpty(_tipo))
                    _tipo = ",NA";

                SqlCommand command = new SqlCommand("PJ_RemateDevolucion", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 1);
                command.Parameters.AddWithValue("@Lineas", Lineas);
                command.Parameters.AddWithValue("@VecesIdeal", veces);
                command.Parameters.AddWithValue("@Articulo", Articulo);
                command.Parameters.AddWithValue("@Tipo", _tipo);
                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                gridExceso.DataSource = table;
            //   FormatoGrid();

                if (table.Columns.Count > 0)
                {
                    FormatoGrid();
                    Analisis = table.Copy();
                    DataTable _totales = new DataTable();
                    _totales.Columns.Add("Ideal ($)", typeof(decimal));
                    _totales.Columns.Add("Stock ($)", typeof(decimal));
                    _totales.Columns.Add("Diferencia ($)", typeof(decimal));

                    DataRow _rowTotal = _totales.NewRow();
                    _rowTotal["Ideal ($)"] = table.Compute("SUM([Ideal ($)])", "");
                    _rowTotal["Stock ($)"] = table.Compute("SUM([Stock ($)])", "");
                    _rowTotal["Diferencia ($)"] = table.Compute("SUM([Diferencia ($)])", "");
                    _totales.Rows.Add(_rowTotal);

                    dataGridView1.DataSource = _totales;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void gridExceso_Click(object sender, EventArgs e)
        {
            try
            {
                int row = gridExceso.CurrentCell.RowIndex;
                int column = gridExceso.CurrentCell.ColumnIndex;
                gridDetalle.DataSource = null;

                string _articulo = gridExceso.Rows[row].Cells[(int)ColumasGrid.Articulo].Value.ToString();
     
                CargarDetalle(_articulo, gridDetalle);
                FormatoDetalle();

            }
            catch (Exception)
            {
            }
        }

        private void txtArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void gridDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                int row = gridDetalle.CurrentCell.RowIndex;
                int column = gridDetalle.CurrentCell.ColumnIndex;

                gridVentas.DataSource = null;
                if (row >= 0 && column >= 1)
                {
                    string _articulo = gridDetalle.Rows[row].Cells[0].Value.ToString();
                    string _almacen = gridDetalle.Columns[column].HeaderText.Trim().Substring(0, 2);

                    //CargarVentas(_articulo, _almacen, gridVentas);
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

        private void gridExceso_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach(DataGridViewRow  item in gridExceso.Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)ColumasGrid.Ideal].Value)  == 0M)
                    {
                        item.Cells[(int)ColumasGrid.VecesIdeal].Style.ForeColor = Color.White;
                        item.Cells[(int)ColumasGrid.VecesIdeal].Style.BackColor = Color.Red;
                    }
                    if (Convert.ToString(item.Cells[(int)ColumasGrid.PlanningCode].Value) == "N")
                    {
                        item.Cells[(int)ColumasGrid.PlanningCode].Style.ForeColor = Color.White;
                        item.Cells[(int)ColumasGrid.PlanningCode].Style.BackColor = Color.Red;
                    } 
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chDevolucion.Checked = false;
            chbPPC.Checked = false;
            chDescontinuado.Checked = false;
            chLinea.Checked = false;
            cbbEspecial.Checked = false;
            chRemate.Checked = false;

            Analisis.Clear();
            txtVeces.Text = "10.0";
            txtArticulo.Clear();
            gridExceso.DataSource = null;
            gridDetalle.DataSource = null;
            gridVentas.DataSource = null;
            dataGridView1.DataSource = null;
        }

        private void chb_Click(object sender, EventArgs e)
        {
            List<string> Propiedades = new List<string>();

            if (chDevolucion.Checked)
                Propiedades.Add(chDevolucion.AccessibleDescription);
            if (chRemate.Checked)
                Propiedades.Add(chRemate.AccessibleDescription);
            if (chbPPC.Checked)
                Propiedades.Add(chbPPC.AccessibleDescription);
            if (chDescontinuado.Checked)
                Propiedades.Add(chDescontinuado.AccessibleDescription);
            if (chLinea.Checked)
                Propiedades.Add(chLinea.AccessibleDescription);
            if (cbbEspecial.Checked)
                Propiedades.Add(cbbEspecial.AccessibleDescription);

            if (Analisis.Rows.Count > 0)
            {
                if (Propiedades.Count > 0)
                {
                    // GetProducts().Where(p => prodIDs.Contains(p.ProductID)).ToArray<Product>();
                    var query = Analisis.AsEnumerable().Where(p => Propiedades.Contains(p.Field<string>("Tipo")));
                    if (query.Count() > 0)
                    {
                        DataTable _t = query.CopyToDataTable();
                        gridExceso.DataSource = _t;

                        if (_t.Columns.Count > 0)
                        {
                            FormatoGrid();
                          
                            DataTable _totales = new DataTable();
                            _totales.Columns.Add("Ideal ($)", typeof(decimal));
                            _totales.Columns.Add("Stock ($)", typeof(decimal));
                            _totales.Columns.Add("Diferencia ($)", typeof(decimal));

                            DataRow _rowTotal = _totales.NewRow();
                            _rowTotal["Ideal ($)"] = _t.Compute("SUM([Ideal ($)])", "");
                            _rowTotal["Stock ($)"] = _t.Compute("SUM([Stock ($)])", "");
                            _rowTotal["Diferencia ($)"] = _t.Compute("SUM([Diferencia ($)])", "");
                            _totales.Rows.Add(_rowTotal);

                            dataGridView1.DataSource = _totales;
                        }

                        FormatoGrid();
                    }
                }
                else
                {
                    gridExceso.DataSource = Analisis;
                    if (Analisis.Columns.Count > 0)
                    {
                        FormatoGrid();
                        Analisis = Analisis.Copy();
                        DataTable _totales = new DataTable();
                        _totales.Columns.Add("Ideal ($)", typeof(decimal));
                        _totales.Columns.Add("Stock ($)", typeof(decimal));
                        _totales.Columns.Add("Diferencia ($)", typeof(decimal));

                        DataRow _rowTotal = _totales.NewRow();
                        _rowTotal["Ideal ($)"] = Analisis.Compute("SUM([Ideal ($)])", "");
                        _rowTotal["Stock ($)"] = Analisis.Compute("SUM([Stock ($)])", "");
                        _rowTotal["Diferencia ($)"] = Analisis.Compute("SUM([Diferencia ($)])", "");
                        _totales.Rows.Add(_rowTotal);

                        dataGridView1.DataSource = _totales;
                    }
                    FormatoGrid();
                }
            }
        }

        private void AnalisisCompras_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void AnalisisCompras_FormClosing(object sender, FormClosingEventArgs e)
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
