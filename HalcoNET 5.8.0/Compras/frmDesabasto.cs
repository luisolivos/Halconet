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
    public partial class frmDesabasto : Form
    {
        bool Flag = false;
        Clases.Logs log;

       List<string> Articulos;

        public frmDesabasto()
        {
            InitializeComponent();
        }

        public frmDesabasto(EnumerableRowCollection<DataRow> _tbl)
        {
            InitializeComponent();

            Articulos = (from item in _tbl.AsEnumerable() select item.Field<string>("articulo")).ToList();
        }

        public enum ColumnasDesabasto
        {
            Linea,
            U_comprador,
            Codigo,
            Descripcion,
            Clasificacion,
            Almacen,
            Stock,
            Ideal,
            Solicitado,
            Desabasto,
            Costo,
            Monto,
            Porcentaje,
            TipoDesabasto,
            Traspaso,
            StockAjustado,
            Pue,
            Mty,
            Api,
            Cor,
            Tep,
            Mex,
            Gdl
        }

        public enum ColumnasDetalleDesabasto
        {
            ItemCode,
            Pue,
            Mty,
            Api,
            Cor,
            Tep,
            Mex,
            Gdl
        }

        public DataTable CargarDesabasto(int _linea, string _comprador, string _almacen, string _proveedor)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 9);
            command.Parameters.AddWithValue("@Articulo", _comprador);
            command.Parameters.AddWithValue("@Linea", _linea);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", _almacen);
            command.Parameters.AddWithValue("@Proveedor", _proveedor);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);
            respaldo = table.Copy();
            return table;
        }

        public DataTable CargarDetalleDesabasto(string _articulo)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 10);
            command.Parameters.AddWithValue("@Articulo", _articulo);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", string.Empty);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            return table;
        }

        public void FormatoGrid(DataTable _t)
        {
            try
            {
                _t.Columns.Add("Traspaso (Cantidad)", typeof(int));
                _t.Columns.Add("Stock ajustado (Porcentaje)", typeof(decimal), "([Stock Actual]+[Traspaso (Cantidad)])/IIF([Stock Ideal]=0, 1, [Stock Ideal])*IIF([Stock Ideal]=0,0,1)");
                _t.Columns.Add("PUE", typeof(decimal));
                _t.Columns.Add("MTY", typeof(decimal));
                _t.Columns.Add("API", typeof(decimal));
                _t.Columns.Add("COR", typeof(decimal));
                _t.Columns.Add("TEP", typeof(decimal));
                _t.Columns.Add("MEX", typeof(decimal));
                _t.Columns.Add("GDL", typeof(decimal));
            }
            catch (Exception) { }
            //-/ IIF([Almacen origen ideal] = 0, 1, [Almacen origen ideal]) * IIF([Almacen origen ideal] = 0, 0, 1)
            dataGridView1.Columns[(int)ColumnasDesabasto.Linea].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Codigo].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Clasificacion].Width = 80;
            dataGridView1.Columns[(int)ColumnasDesabasto.Descripcion].Width = 200;
            dataGridView1.Columns[(int)ColumnasDesabasto.Almacen].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Stock].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Ideal].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Desabasto].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Costo].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Monto].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Porcentaje].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.Traspaso].Width = 90;
            dataGridView1.Columns[(int)ColumnasDesabasto.StockAjustado].Width = 90;

            dataGridView1.Columns[(int)ColumnasDesabasto.Linea].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.Codigo].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.Descripcion].DefaultCellStyle.Format = "N0";
            //dataGridView1.Columns[(int)ColumnasDesabasto.Almacen].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.Stock].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.Ideal].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.Solicitado].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.Desabasto].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.Costo].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasDesabasto.Monto].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasDesabasto.Porcentaje].DefaultCellStyle.Format = "P2";
            dataGridView1.Columns[(int)ColumnasDesabasto.Traspaso].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.StockAjustado].DefaultCellStyle.Format = "P2";

            dataGridView1.Columns[(int)ColumnasDesabasto.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[(int)ColumnasDesabasto.Almacen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[(int)ColumnasDesabasto.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasDesabasto.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasDesabasto.Desabasto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasDesabasto.Costo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasDesabasto.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasDesabasto.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasDesabasto.Traspaso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasDesabasto.StockAjustado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasDesabasto.Solicitado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[(int)ColumnasDesabasto.Pue].Visible = false;
            dataGridView1.Columns[(int)ColumnasDesabasto.Mty].Visible = false;
            dataGridView1.Columns[(int)ColumnasDesabasto.Api].Visible = false;
            dataGridView1.Columns[(int)ColumnasDesabasto.Cor].Visible = false;
            dataGridView1.Columns[(int)ColumnasDesabasto.Tep].Visible = false;
            dataGridView1.Columns[(int)ColumnasDesabasto.Mex].Visible = false;
            dataGridView1.Columns[(int)ColumnasDesabasto.Gdl].Visible = false;

            dataGridView1.Columns[(int)ColumnasDesabasto.Linea].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Codigo].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Descripcion].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Almacen].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Stock].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Ideal].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Desabasto].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Costo].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Monto].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Solicitado].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Porcentaje].ReadOnly = true;
            dataGridView1.Columns[(int)ColumnasDesabasto.Traspaso].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns[(int)ColumnasDesabasto.StockAjustado].ReadOnly = true;
        }

        public void FormatoDetalle()
        {
            dataGridView2.Columns[(int)ColumnasDetalleDesabasto.Pue].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns[(int)ColumnasDetalleDesabasto.Mty].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns[(int)ColumnasDetalleDesabasto.Api].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns[(int)ColumnasDetalleDesabasto.Cor].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns[(int)ColumnasDetalleDesabasto.Tep].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns[(int)ColumnasDetalleDesabasto.Mex].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns[(int)ColumnasDetalleDesabasto.Gdl].DefaultCellStyle.Format = "N0";
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

        public void CargarCompradores(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 12);
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

        public void Totales(DataTable _table)
        {
            DataTable _t = new DataTable();
            _t.Columns.Add("Costo", typeof(decimal));
            _t.Columns.Add("Monto desabasto", typeof(decimal));
            DataRow row = _t.NewRow();
            row["Costo"] = _table.Compute("SUM(Costo)", "");
            row["Monto desabasto"] = _table.Compute("SUM([Monto desabasto])", "[Tipo Abastecimiento] = 'Desabasto'");
            _t.Rows.Add(row);
            gridTotales.DataSource = _t;
        }

        private void Desabasto_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                CargarLinea(cbLinea, "Todas");
                CargarProveedores(cbProveedor, "Todos");
                CargarCompradores(cbComprador, "Todos");
                cbAlmacen.SelectedIndex = 0;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("HalcoNET"+ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Flag = false;
                _ItemCode = "";
                groupBox2.Enabled = false;
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                DataTable t = new DataTable("Encabezado");

                int _linea = Convert.ToInt32(cbLinea.SelectedValue);
                string _comprador = Convert.ToString(cbComprador.Text);
                string _almacen = cbAlmacen.SelectedItem.ToString().Substring(0, 2);
                string _proveedor = Convert.ToString(cbProveedor.SelectedValue);

                t = CargarDesabasto(_linea, _comprador, _almacen, _proveedor);
                this.Totales(t);
                if (t.Columns.Count > 0)
                {
                    groupBox2.Enabled = true;
                    dataGridView1.DataSource = t;
                    FormatoGrid(t);
                }
                Flag = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (Flag)
                {
                    string _articulo = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[(int)ColumnasDesabasto.Codigo].Value.ToString();
                   // MessageBox.Show(_articulo);
                    dataGridView2.DataSource = CargarDetalleDesabasto(_articulo);
                    FormatoDetalle();
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)ColumnasDesabasto.Porcentaje].Value) == (decimal)0) //Stock Out
                    {
                        item.Cells[(int)ColumnasDesabasto.Porcentaje].Style.BackColor = Color.Red;
                        item.Cells[(int)ColumnasDesabasto.Porcentaje].Style.ForeColor = Color.White;
                    }
                    else if (Convert.ToDecimal(item.Cells[(int)ColumnasDesabasto.Porcentaje].Value) > (decimal)0 //Desabasto
                        && Convert.ToDecimal(item.Cells[(int)ColumnasDesabasto.Porcentaje].Value) <= (decimal)0.30)
                    {
                        item.Cells[(int)ColumnasDesabasto.Porcentaje].Style.BackColor = Color.Yellow;
                        item.Cells[(int)ColumnasDesabasto.Porcentaje].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(item.Cells[(int)ColumnasDesabasto.Porcentaje].Value) > (decimal)1.25)
                    {
                        item.Cells[(int)ColumnasDesabasto.Porcentaje].Style.BackColor = Color.Green;
                        item.Cells[(int)ColumnasDesabasto.Porcentaje].Style.ForeColor = Color.Black;
                    }

                    if (Articulos.Count() > 0)
                    {
                        if (Articulos.ToList().Contains(item.Cells[(int)ColumnasDesabasto.Codigo].Value.ToString()))
                        {
                            item.Cells[(int)ColumnasDesabasto.Codigo].Style.BackColor = Color.Yellow;
                            item.Cells[(int)ColumnasDesabasto.Codigo].Style.ForeColor = Color.Black;
                        }
                        else
                        {
                            item.Cells[(int)ColumnasDesabasto.Codigo].Style.BackColor = Color.White;
                            item.Cells[(int)ColumnasDesabasto.Codigo].Style.ForeColor = Color.Black;
                        }
                    }

                }
            }
            catch (Exception)
            {
            }
        }


        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.dataGridView2.DoDragDrop(this.dataGridView2.CurrentRow, DragDropEffects.All);
            }
        }

        string _ItemCode = "";
        string _alm = "";
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            string valorcelda = "";
            _alm = "";
            DataGridView.HitTestInfo info = dataGridView2.HitTest(e.X, e.Y);
 
            // buscar fila bajo el Mouse
 
            if ((info.RowIndex >= 0) & (info.ColumnIndex >=0))
            {
            if (dataGridView2.Rows[info.RowIndex].Cells[info.ColumnIndex].Value != null)
            {
                valorcelda = dataGridView2.Rows[info.RowIndex].Cells[info.ColumnIndex].Value.ToString();
                _alm = dataGridView2.Columns[info.ColumnIndex].HeaderText;
                _ItemCode = dataGridView2.Rows[info.RowIndex].Cells[(int)ColumnasDetalleDesabasto.ItemCode].Value.ToString(); 
                dataGridView2.DoDragDrop(valorcelda, DragDropEffects.Copy);
            }
            }
        }

        private void dataGridView2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void dataGridView2_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string valornuevo = Convert.ToString(e.Data.GetData(DataFormats.StringFormat));
                // calcular ubicacion del Mouse en coordenadas relativas a la grilla
                Point gridDrop = dataGridView1.PointToClient(new Point(e.X, e.Y));
                // buscar fila bajo el Mouse
                int rowDrop = dataGridView1.HitTest(gridDrop.X, gridDrop.Y).RowIndex;
                int colDrop = dataGridView1.HitTest(gridDrop.X, gridDrop.Y).ColumnIndex;

                int valoranterior = 0;
                try{
                    valoranterior = Convert.ToInt32(dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Traspaso].Value);
                }catch(Exception){ valoranterior = 0;}
                int cantidadnueva = Convert.ToInt32(Convert.ToDecimal(valornuevo)) + valoranterior;

                if (_ItemCode == dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Codigo].Value.ToString()
                    && colDrop == (int)ColumnasDesabasto.Traspaso)
                {
                    dataGridView1.Rows[rowDrop].Cells[colDrop].Value = cantidadnueva;
                    if (_alm == "PUE")
                        dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Pue].Value = Convert.ToInt32(Convert.ToDecimal(valornuevo));
                    else if (_alm == "MTY")
                        dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Mty].Value = Convert.ToInt32(Convert.ToDecimal(valornuevo));
                    else if (_alm == "API")
                        dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Api].Value = Convert.ToInt32(Convert.ToDecimal(valornuevo));
                    else if (_alm == "COR")
                        dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Cor].Value = Convert.ToInt32(Convert.ToDecimal(valornuevo));
                    else if (_alm == "TEP")
                        dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Tep].Value = Convert.ToInt32(Convert.ToDecimal(valornuevo));
                    else if (_alm == "MEX")
                        dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Mex].Value = Convert.ToInt32(Convert.ToDecimal(valornuevo));
                    else if (_alm == "GDL")
                        dataGridView1.Rows[rowDrop].Cells[(int)ColumnasDesabasto.Gdl].Value = Convert.ToInt32(Convert.ToDecimal(valornuevo));
                }
                valornuevo = "";


            }
            catch (Exception)
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("¿Exportar sin formato?\r\n Si elige 'No' el proceso puede durar varios minutos.", "HalcoNET", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                DataGridView dgv = new DataGridView();

                dgv = dataGridView1;
                dgv.Columns.Add("XX", "XX");
                foreach (DataGridViewRow item in dgv.Rows)
                {
                    if (item.Cells[(int)ColumnasDesabasto.Codigo].Style.BackColor == Color.Yellow)
                    {
                        item.Cells["XX"].Value = "Y";
                    }
                    else
                        item.Cells["XX"].Value = "N";
                }


                ExportarAExcel ex = new ExportarAExcel();
                if (ex.ExportarSinFormato(dataGridView1))
                    MessageBox.Show("El archivo se creo correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (dialogResult == DialogResult.No)
            {
                ExportarAExcel ex = new ExportarAExcel();
                if (ex.ExportarTodo(dataGridView1))
                    MessageBox.Show("El archivo se creo correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cbAlmacen.SelectedIndex = 0;
            cbComprador.SelectedIndex = 0;
            cbLinea.SelectedIndex = 0;
            cbProveedor.SelectedIndex = 0;

            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
        }

        DataTable respaldo = new DataTable();
        private void checkBox1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable _tabla = new DataTable();
                if (checkBox1.Checked)//= 0%
                {
                    DataTable _verde = (from item in respaldo.AsEnumerable()
                                        where item.Field<decimal>((int)ColumnasDesabasto.Porcentaje) == (decimal)0
                                        select item).CopyToDataTable();
                    _tabla.Merge(_verde);
                }
                if (checkBox2.Checked)//> 0 y <=30%
                {
                    DataTable _amarillo = (from item in respaldo.AsEnumerable()
                                           where item.Field<decimal>((int)ColumnasDesabasto.Porcentaje) > (decimal)0
                                           && item.Field<decimal>((int)ColumnasDesabasto.Porcentaje) <= (decimal)0.30
                                           select item).CopyToDataTable();
                    _tabla.Merge(_amarillo);
                }
                if (checkBox3.Checked)//>125%
                {
                    DataTable _rojo = (from item in respaldo.AsEnumerable()
                                       where item.Field<decimal>((int)ColumnasDesabasto.Porcentaje) > (decimal)1.25
                                       select item).CopyToDataTable();
                    _tabla.Merge(_rojo);
                }


                dataGridView1.DataSource = _tabla;
                if (_tabla.Columns.Count > 0)
                {
                    FormatoGrid(_tabla);
                    this.Totales(_tabla);
                }
            }
            catch (Exception)
            {
            }
        }

        private void Desabasto_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void Desabasto_FormClosing(object sender, FormClosingEventArgs e)
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
