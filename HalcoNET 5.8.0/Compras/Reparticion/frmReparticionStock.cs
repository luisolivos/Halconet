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

namespace Compras.Reparticion
{
    public partial class frmReparticionStock : Form
    {
        DataTable Articul = new DataTable();
        DataTable Repartir = new DataTable();
        DataTable Procesado = new DataTable();

        public enum Columnas
        {
            Articulo,
            Nombre,
            Linea,
            Cantidad,
            StockMTY,
            //StockMTY1,
            IdealMTY,
            StockZCENTRO,
           // StockZCENTRO1,
            IdealZCENTRO,
            StockGDL,
           // StockGDL1,
            IdealGDL
        }

        public enum ColumasReparto
        {
            Articulo,
            StockMTY,
            CantidadMTY,
            PorcentajeMTY,
            StockZCENTRO,
            CantidadZCENTRO,
            PorcentajeZCENTRO,
            StockGDL,
            CantidadGDL,
            PorcentajeGDL
           
        }

        DataTable almacenes = new DataTable();

        public void CargarAlmacenes()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_ReparticionStock";
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 5);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@CantiadOK", decimal.Zero);
                    command.Parameters.AddWithValue("@Incremento", decimal.Zero);
                    command.Parameters.AddWithValue("@almacenRestar", string.Empty);

                    command.CommandTimeout = 0;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    cbAlmacen.DataSource = table;
                    cbAlmacen.DisplayMember = "Nombre";
                    cbAlmacen.ValueMember = "Codigo";
                }
            }
        }


        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Articulo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Nombre].ReadOnly = true;
            dgv.Columns[(int)Columnas.Linea].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cantidad].ReadOnly = false;
            dgv.Columns[(int)Columnas.StockMTY].ReadOnly = true;
            dgv.Columns[(int)Columnas.IdealMTY].ReadOnly = true;
            dgv.Columns[(int)Columnas.StockGDL].ReadOnly = true;
            dgv.Columns[(int)Columnas.IdealGDL].ReadOnly = true;
            dgv.Columns[(int)Columnas.StockZCENTRO].ReadOnly = true;
            dgv.Columns[(int)Columnas.IdealZCENTRO].ReadOnly = true;

            dgv.Columns[(int)Columnas.Cantidad].Width = 70;
            dgv.Columns[(int)Columnas.StockMTY].Width = 70;
            dgv.Columns[(int)Columnas.IdealMTY].Width = 70;
            dgv.Columns[(int)Columnas.StockGDL].Width = 70;
            dgv.Columns[(int)Columnas.IdealGDL].Width = 70;
            dgv.Columns[(int)Columnas.StockZCENTRO].Width = 70;
            dgv.Columns[(int)Columnas.IdealZCENTRO].Width = 70;

            dgv.Columns[(int)Columnas.Cantidad].HeaderText = "Cantidad";
            dgv.Columns[(int)Columnas.StockMTY].HeaderText = "Stock\r\nMTY";
            dgv.Columns[(int)Columnas.IdealMTY].HeaderText = "Ideal\r\nMTY";
            dgv.Columns[(int)Columnas.StockGDL].HeaderText = "Stock\r\nGDL";
            dgv.Columns[(int)Columnas.IdealGDL].HeaderText = "Ideal\r\nGDL";
            dgv.Columns[(int)Columnas.StockZCENTRO].HeaderText = "Stock\r\nZCENTRO";
            dgv.Columns[(int)Columnas.IdealZCENTRO].HeaderText = "Ideal\r\nZCENTRO";

            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.StockMTY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.IdealMTY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.StockGDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.IdealGDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.StockZCENTRO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.IdealZCENTRO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void FormatoReparto(DataGridView dgv)
        {
            dgv.Columns[(int)ColumasReparto.StockMTY].Width = 70;
            dgv.Columns[(int)ColumasReparto.CantidadMTY].Width = 70;
            dgv.Columns[(int)ColumasReparto.StockGDL].Width = 70;
            dgv.Columns[(int)ColumasReparto.CantidadGDL].Width = 70;
            dgv.Columns[(int)ColumasReparto.StockZCENTRO].Width = 70;
            dgv.Columns[(int)ColumasReparto.CantidadZCENTRO].Width = 70;
            dgv.Columns[(int)ColumasReparto.PorcentajeGDL].Width = 50;
            dgv.Columns[(int)ColumasReparto.PorcentajeMTY].Width = 50;
            dgv.Columns[(int)ColumasReparto.PorcentajeZCENTRO].Width = 50;

            dgv.Columns[(int)ColumasReparto.StockMTY].HeaderText = "Stock\r\nMTY";
            dgv.Columns[(int)ColumasReparto.CantidadMTY].HeaderText = "Cantidad\r\na MTY";
            dgv.Columns[(int)ColumasReparto.StockGDL].HeaderText = "Stock\r\nGDL";
            dgv.Columns[(int)ColumasReparto.CantidadGDL].HeaderText = "Cantidad\r\na GDL";
            dgv.Columns[(int)ColumasReparto.StockZCENTRO].HeaderText = "Stock\r\nZCENTRO";
            dgv.Columns[(int)ColumasReparto.CantidadZCENTRO].HeaderText = "Cantidad\r\na ZCENTRO";

            dgv.Columns[(int)ColumasReparto.StockMTY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasReparto.CantidadMTY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasReparto.StockGDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasReparto.CantidadGDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasReparto.StockZCENTRO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasReparto.CantidadZCENTRO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasReparto.PorcentajeGDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasReparto.PorcentajeMTY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasReparto.PorcentajeZCENTRO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[(int)ColumasReparto.PorcentajeGDL].DefaultCellStyle.Format = "P1";
            dgv.Columns[(int)ColumasReparto.PorcentajeMTY].DefaultCellStyle.Format = "P1";
            dgv.Columns[(int)ColumasReparto.PorcentajeZCENTRO].DefaultCellStyle.Format = "P1";
        }

        public frmReparticionStock()
        {
            InitializeComponent();

            Repartir.Columns.Add("Artículo", typeof(string));
            Repartir.Columns.Add("Nombre", typeof(string));
            Repartir.Columns.Add("Línea", typeof(string));
            Repartir.Columns.Add("Cantidad", typeof(decimal));

            Repartir.Columns.Add("Stock MTY", typeof(decimal));
           // Repartir.Columns.Add("Stock MTY**", typeof(decimal));
            Repartir.Columns.Add("Ideal MTY", typeof(decimal));

            Repartir.Columns.Add("Stock ZCENTRO", typeof(decimal));
            //Repartir.Columns.Add("Stock ZCENTRO**", typeof(decimal));
            Repartir.Columns.Add("Ideal ZCENTRO", typeof(decimal));

            Repartir.Columns.Add("Stock GDL", typeof(decimal));
           // Repartir.Columns.Add("Stock GDL**", typeof(decimal));
            Repartir.Columns.Add("Ideal GDL", typeof(decimal));

            ////////////////
            Procesado.Columns.Add("Artículo", typeof(string));
            Procesado.Columns.Add("Línea", typeof(string));

            Procesado.Columns.Add("Stock MTY", typeof(decimal));
            Procesado.Columns.Add("Cantidad MTY", typeof(decimal));

            Procesado.Columns.Add("Stock ZCENTRO", typeof(decimal));
            Procesado.Columns.Add("Cantidad ZCENTRO", typeof(decimal));

            Procesado.Columns.Add("Stock GDL", typeof(decimal));
            Procesado.Columns.Add("Cantidad GDL", typeof(decimal));

            Procesado.Columns.Add("Sobrante", typeof(decimal));
        }

        private void ReparticionStock_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            txtCantidad.Focus();
            this.ListaArticulos();

            txtArticulo.AutoCompleteCustomSource = Autocomplete(Articul, "ItemCode");
            txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;

            dgvRepartir.DataSource = Repartir;

            this.Formato(dgvRepartir);
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

        public void ListaArticulos()
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

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCantidad.Text))
                {
                    if (!string.IsNullOrEmpty(txtArticulo.Text))
                    {

                        using (SqlConnection connection = new SqlConnection())
                        {
                            connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                            using (SqlCommand command = new SqlCommand())
                            {
                                command.CommandText = "PJ_ReparticionStock";
                                string almacen = Convert.ToString(cbAlmacen.SelectedValue);
                                command.Connection = connection;
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", 2);
                                command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                                command.Parameters.AddWithValue("@CantiadOK", Convert.ToDecimal (txtCantidad.Text));
                                command.Parameters.AddWithValue("@Incremento", decimal.Zero);
                                command.Parameters.AddWithValue("@almacenRestar", almacen);
	
                                command.CommandTimeout = 0;

                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command;
                                adapter.SelectCommand.CommandTimeout = 0;
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                

                                var query = from item in table.AsEnumerable()
                                            where item.Field<string>("ItemCode").Equals(txtArticulo.Text)
                                            select item;
                                if (query.Count() > 0)
                                {
                                    DataRow row = Repartir.NewRow();
                                    row["Artículo"] = (from item in query.AsEnumerable()
                                                       select item.Field<string>("ItemCode")).FirstOrDefault();

                                    row["Nombre"] = (from item in query.AsEnumerable()
                                                     select item.Field<string>("ItemName")).FirstOrDefault();

                                    row["Línea"] = (from item in query.AsEnumerable()
                                                    select item.Field<string>("ItmsGrpNam")).FirstOrDefault();

                                    row["Cantidad"] = Convert.ToDecimal(txtCantidad.Text);

                                    row["Stock MTY"] = (from item in query.AsEnumerable()
                                                        select item.Field<decimal>("Stock MTY")).FirstOrDefault();
                                    row["Ideal MTY"] = (from item in query.AsEnumerable()
                                                        select item.Field<decimal>("Ideal MTY")).FirstOrDefault();

                                    row["Stock ZCENTRO"] = (from item in query.AsEnumerable()
                                                            select item.Field<decimal>("Stock ZCENTRO")).FirstOrDefault();
                                    row["Ideal ZCENTRO"] = (from item in query.AsEnumerable()
                                                            select item.Field<decimal>("Ideal ZCENTRO")).FirstOrDefault();

                                    row["Stock GDL"] = (from item in query.AsEnumerable()
                                                        select item.Field<decimal>("Stock GDL")).FirstOrDefault();
                                    row["Ideal GDL"] = (from item in query.AsEnumerable()
                                                        select item.Field<decimal>("Ideal GDL")).FirstOrDefault();

                                    Repartir.Rows.Add(row);
                                }

                            }
                        }
                        txtArticulo.Clear();
                        txtCantidad.Clear();
                        txtArticulo.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El campo [Artículo] no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtArticulo.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("El campo [Cantidad] no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCantidad.Focus();
                }
 
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Procesado.Clear();
                dgvProcesado.Columns.Clear();

                if (Repartir.Rows.Count > 0)
                {
                  foreach (DataRow item in Repartir.Rows)
                    {
                        using (SqlConnection connection = new SqlConnection())
                        {
                            connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                            using (SqlCommand command = new SqlCommand())
                            {
                                command.CommandText = "PJ_ReparticionStock";

                                //decimal _incremento = 
                                string almacen = cbAlmacen.Text.Substring(0, 2);
                                command.Connection = connection;
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", 3);
                                command.Parameters.AddWithValue("@Articulo", item.Field<string>("Artículo"));
                                command.Parameters.AddWithValue("@CantiadOK", item.Field<decimal>("Cantidad"));
                                command.Parameters.AddWithValue("@Incremento", decimal.Zero);
                                command.Parameters.AddWithValue("@almacenRestar", almacen);
                                command.Parameters.AddWithValue("@Limite", kryptonTextBox1.Text);

                                command.CommandTimeout = 0;

                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command;
                                adapter.SelectCommand.CommandTimeout = 0;
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                var query = from row in table.AsEnumerable()
                                            where row.Field<string>("Artículo").Equals(item.Field<string>("Artículo"))
                                            select row;


                                if (dgvProcesado.Columns.Count == 0)
                                {
                                    //decimal sobrante = (from row1 in query.AsEnumerable()
                                    //                    select row1.Field<decimal>("Sobrante")).FirstOrDefault();

                                    //if (sobrante >= decimal.Zero)
                                    //{
                                    Procesado = table.Copy();
                                    dgvProcesado.DataSource = Procesado;
                                    this.FormatoReparto(dgvProcesado);
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("La cantidad a repartir no es sificiente para igualar los almacenes.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    //}
                                }
                                else if (query.Count() > 0)
                                {

                                    DataRow row = Procesado.NewRow();
                                    row["Artículo"] = (from row1 in query.AsEnumerable()
                                                       select row1.Field<string>("Artículo")).FirstOrDefault();

                                    row["Stock MTY"] = (from row1 in query.AsEnumerable()
                                                        select row1.Field<decimal>("Stock MTY")).FirstOrDefault();
                                    row["Cantidad MTY"] = (from row1 in query.AsEnumerable()
                                                           select row1.Field<decimal>("Cantidad MTY")).FirstOrDefault();
                                    row["% MTY"] = (from row1 in query.AsEnumerable()
                                                    select row1.Field<decimal>("% MTY")).FirstOrDefault();

                                    row["Stock GDL"] = (from row1 in query.AsEnumerable()
                                                        select row1.Field<decimal>("Stock GDL")).FirstOrDefault();
                                    row["Cantidad GDL"] = (from row1 in query.AsEnumerable()
                                                           select row1.Field<decimal>("Cantidad GDL")).FirstOrDefault();
                                    row["% ZCENTRO"] = (from row1 in query.AsEnumerable()
                                                        select row1.Field<decimal>("% ZCENTRO")).FirstOrDefault();

                                    row["Stock ZCENTRO"] = (from row1 in query.AsEnumerable()
                                                            select row1.Field<decimal>("Stock ZCENTRO")).FirstOrDefault();
                                    row["Cantidad ZCENTRO"] = (from row1 in query.AsEnumerable()
                                                               select row1.Field<decimal>("Cantidad ZCENTRO")).FirstOrDefault();
                                    row["% GDL"] = (from row1 in query.AsEnumerable()
                                                    select row1.Field<decimal>("% GDL")).FirstOrDefault();
                                    Procesado.Rows.Add(row);
                                    dgvProcesado.DataSource = Procesado;
                                    this.FormatoReparto(dgvProcesado);

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = openFileDialog.ShowDialog();

                if (dialog == System.Windows.Forms.DialogResult.OK)
                {
                    string path = openFileDialog.FileName;
                    string line = string.Empty;
                    bool encabezado = true;
                    System.IO.StreamReader file = new System.IO.StreamReader(path, Encoding.UTF8);

                    while ((line = file.ReadLine()) != null)
                    {
                        if (!encabezado)
                        {
                            string[] items = line.Split('\t');

                            txtArticulo.Text = items[0];
                            txtCantidad.Text = items[1];
                            cbAlmacen.SelectedValue = items[2];


                            button1_Click(sender, e);
                        }

                        encabezado = false;
                    }
                }

                button2_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
