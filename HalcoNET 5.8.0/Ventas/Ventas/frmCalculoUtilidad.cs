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

namespace Ventas.Ventas
{
    public partial class frmCalculoUtilidad : Form
    {
        public string Articulo;
        public decimal Pesos;
        public decimal Porcentaje;
        public decimal Usd;
        public decimal PrecioCompra;
        public DataTable Articulos = new DataTable();
        private Clases.Logs log;

        public enum TipoConsulta
        {
            Pesos = 1,
            Usd = 2,
            ListaPercios = 3,
            Articulos = 4,
            Stocks = 5,
            TC = 6
        }

        public enum Columnas
        {
            PriceList,
            MXP, USD
        }

        public void FormatoGridListaPrecios(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.PriceList].Width = 140;
            dgv.Columns[(int)Columnas.MXP].Width = 80;
            dgv.Columns[(int)Columnas.USD].Width = 80;

            dgv.Columns[(int)Columnas.MXP].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.USD].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.MXP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.USD].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void FormatoGridStocks(DataGridView dgv)
        {
            dgv.Columns[0].Width = 90;
            dgv.Columns[1].Width = 50;
            dgv.Columns[2].Width = 60;

            dgv.Columns[1].DefaultCellStyle.Format = "N0";
            dgv.Columns[2].DefaultCellStyle.Format = "N0";

            dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public frmCalculoUtilidad()
        {
            InitializeComponent();
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

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
                Articulo = txtArticulo.Text;

                if (txtPesos.Text != string.Empty)
                {
                    Pesos = Convert.ToDecimal(txtPesos.Text);

                    SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Pesos);
                    command.Parameters.AddWithValue("@Articulo", Articulo);
                    SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", decimal.Zero);
                    PrecCompra.Direction = ParameterDirection.Output;
                    PrecCompra.DbType = DbType.Decimal;
                    PrecCompra.Scale = 6;
                    command.Parameters.Add(PrecCompra);

                    connection.Open();
                    command.ExecuteNonQuery();
                    PrecioCompra = Convert.ToDecimal(command.Parameters["@PrecioCompra"].Value.ToString());
                    connection.Close();

                    // txtUtilidadCompra.Text = decimal.Round((Pesos / PrecioCompra), 2).ToString() + " %";
                    txtUtilidadVenta.Text = decimal.Round((((PrecioCompra / Pesos) - 1) * -100), 2).ToString() + "%";
                }
                else
                {
                    if (txtUsd.Text != string.Empty)
                    {
                        Usd = Convert.ToDecimal(txtUsd.Text);

                        SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Usd);
                        command.Parameters.AddWithValue("@Articulo", Articulo);
                        SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                        PrecCompra.Direction = ParameterDirection.Output;
                        PrecCompra.DbType = DbType.Decimal;
                        PrecCompra.Scale = 6;
                        command.Parameters.Add(PrecCompra);

                        connection.Open();
                        command.ExecuteNonQuery();
                        PrecioCompra = Convert.ToDecimal(command.Parameters["@PrecioCompra"].Value.ToString());
                        connection.Close();

                        //txtUtilidadCompra.Text = decimal.Round((Pesos / PrecioCompra), 2).ToString() + " %";
                        txtUtilidadVenta.Text = decimal.Round((((PrecioCompra / Usd) - 1) * -100), 2).ToString() + "%";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CalculoUtilidad_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                comboBox1.Text = string.Empty;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                {
                    using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Articulos);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                        PrecCompra.Direction = ParameterDirection.Output;
                        command.Parameters.Add(PrecCompra);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(Articulos);
                    }
                }

                txtArticulo.AutoCompleteCustomSource = Autocomplete(Articulos, "ItemCode");
                txtArticulo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;

                comboBox1.DataSource = Articulos;
                comboBox1.DisplayMember = "Dscription";
                comboBox1.ValueMember = "ItemCode";
                //txtArticulo.AutoCompleteMode = AutoCompleteMode.

                txtDscription.AutoCompleteCustomSource = Autocomplete(Articulos, "Dscription");
                txtDscription.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtDscription.AutoCompleteSource = AutoCompleteSource.CustomSource;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                {
                    using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.TC);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                        PrecCompra.Direction = ParameterDirection.Output;
                        command.Parameters.Add(PrecCompra);
                        PrecCompra.DbType = DbType.Decimal;
                        PrecCompra.Scale = 6;

                        connection.Open();
                        command.ExecuteNonQuery();
                        PrecioCompra = Convert.ToDecimal(command.Parameters["@PrecioCompra"].Value.ToString());

                        lblTC.Text = "TC: " + PrecioCompra.ToString("C2");
                    }
                }

                txtArticulo.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtArticulo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    txtDscription.Clear();
                    comboBox1.Text = string.Empty;

                    Articulo = txtArticulo.Text;
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.ListaPercios);
                            command.Parameters.AddWithValue("@Articulo", Articulo);
                            SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                            PrecCompra.Direction = ParameterDirection.Output;
                            command.Parameters.Add(PrecCompra);

                            DataTable table = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            dgvGastos.DataSource = table;
                            this.FormatoGridListaPrecios(dgvGastos);

                            lblName.Text = (from item in Articulos.AsEnumerable()
                                               where item.Field<string>("ItemCode").ToLower().Equals(Articulo.ToLower())
                                               select item.Field<string>("ItemName")).FirstOrDefault();
                        }

                    }

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Stocks);
                            command.Parameters.AddWithValue("@Articulo", Articulo);
                            SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                            PrecCompra.Direction = ParameterDirection.Output;
                            command.Parameters.Add(PrecCompra);

                            DataTable table = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            kryptonDataGridView1.DataSource = table;
                            this.FormatoGridStocks(kryptonDataGridView1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CalculoUtilidad_Shown(object sender, EventArgs e)
        {

            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void CalculoUtilidad_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                txtPorcentaje.Clear();

                SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

                if (rbPesos.Checked)
                {
                    if (txtPesos.Text != string.Empty)
                    {
                        Pesos = Convert.ToDecimal(txtPesos.Text);

                        SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Pesos);
                        command.Parameters.AddWithValue("@Articulo", Articulo);
                        SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", decimal.Zero);
                        PrecCompra.Direction = ParameterDirection.Output;
                        PrecCompra.DbType = DbType.Decimal;
                        PrecCompra.Scale = 6;
                        command.Parameters.Add(PrecCompra);

                        connection.Open();
                        command.ExecuteNonQuery();
                        PrecioCompra = Convert.ToDecimal(command.Parameters["@PrecioCompra"].Value.ToString());
                        connection.Close();

                        txtPorcentaje.Text = decimal.Round((((PrecioCompra / Pesos) - 1) * -100), 2).ToString() + "%";
                    }
                }
                if (rbDolares.Checked)
                {
                    if (txtPesos.Text != string.Empty)
                    {
                        Usd = Convert.ToDecimal(txtPesos.Text);

                        SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Usd);
                        command.Parameters.AddWithValue("@Articulo", Articulo);
                        SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                        PrecCompra.Direction = ParameterDirection.Output;
                        PrecCompra.DbType = DbType.Decimal;
                        PrecCompra.Scale = 6;
                        command.Parameters.Add(PrecCompra);

                        connection.Open();
                        command.ExecuteNonQuery();
                        PrecioCompra = Convert.ToDecimal(command.Parameters["@PrecioCompra"].Value.ToString());
                        connection.Close();

                        txtPorcentaje.Text = decimal.Round((((PrecioCompra / Usd) - 1) * -100), 2).ToString() + "%";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
                txtPesos.Clear();

                if (rbPesos.Checked)
                {
                    if (txtPorcentaje.Text != string.Empty)
                    {
                        Porcentaje = Convert.ToDecimal(txtPorcentaje.Text);

                        SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Pesos);
                        command.Parameters.AddWithValue("@Articulo", Articulo);
                        SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", decimal.Zero);
                        PrecCompra.Direction = ParameterDirection.Output;
                        PrecCompra.DbType = DbType.Decimal;
                        PrecCompra.Scale = 6;
                        command.Parameters.Add(PrecCompra);

                        connection.Open();
                        command.ExecuteNonQuery();
                        PrecioCompra = Convert.ToDecimal(command.Parameters["@PrecioCompra"].Value.ToString());
                        connection.Close();
                        
                        txtPesos.Text = decimal.Round((PrecioCompra/(100-Porcentaje))*100, 2).ToString("C2");
                    }
                }
                if (rbDolares.Checked)
                {
                    if (txtPorcentaje.Text != string.Empty)
                    {
                        Porcentaje = Convert.ToDecimal(txtPorcentaje.Text);

                        SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Usd);
                        command.Parameters.AddWithValue("@Articulo", Articulo);
                        SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                        PrecCompra.Direction = ParameterDirection.Output;
                        PrecCompra.DbType = DbType.Decimal;
                        PrecCompra.Scale = 6;
                        command.Parameters.Add(PrecCompra);

                        connection.Open();
                        command.ExecuteNonQuery();
                        PrecioCompra = Convert.ToDecimal(command.Parameters["@PrecioCompra"].Value.ToString());
                        connection.Close();

                        txtPesos.Text = decimal.Round((PrecioCompra / (100 - Porcentaje)) * 100, 2).ToString("C2");
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
            txtPesos.Clear();
            txtPorcentaje.Clear();

            txtDscription.Clear();
            txtArticulo.Clear();

            txtArticulo.Focus();
        }

        private void txtDscription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    txtArticulo.Clear();

                    Articulo = (from item in Articulos.AsEnumerable()
                                where (item.Field<string>("Dscription") == null ? string.Empty : item.Field<string>("Dscription").Trim().ToUpper()) == comboBox1.Text.Trim().ToUpper()
                               select item.Field<string>("ItemCode")).FirstOrDefault();


                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.ListaPercios);
                            command.Parameters.AddWithValue("@Articulo", Articulo);
                            SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                            PrecCompra.Direction = ParameterDirection.Output;
                            command.Parameters.Add(PrecCompra);

                            DataTable table = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            dgvGastos.DataSource = table;
                            this.FormatoGridListaPrecios(dgvGastos);

                            lblName.Text = (from item in Articulos.AsEnumerable()
                                            where item.Field<string>("ItemCode").ToLower().Equals(Articulo.ToLower())
                                            select item.Field<string>("ItemName")).FirstOrDefault();
                        }

                    }

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Stocks);
                            command.Parameters.AddWithValue("@Articulo", Articulo);
                            SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                            PrecCompra.Direction = ParameterDirection.Output;
                            command.Parameters.Add(PrecCompra);

                            DataTable table = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            kryptonDataGridView1.DataSource = table;
                            this.FormatoGridStocks(kryptonDataGridView1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtDscription_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                    txtArticulo.Clear();

                    Articulo = (from item in Articulos.AsEnumerable()
                                where (item.Field<string>("Dscription") == null ? string.Empty : item.Field<string>("Dscription").Trim().ToUpper()) == txtDscription.Text.Trim().ToUpper()
                                select item.Field<string>("ItemCode")).FirstOrDefault();
                    
                    if (Articulo == null) Articulo = string.Empty;

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.ListaPercios);
                            command.Parameters.AddWithValue("@Articulo", Articulo);
                            SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                            PrecCompra.Direction = ParameterDirection.Output;
                            command.Parameters.Add(PrecCompra);

                            DataTable table = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            dgvGastos.DataSource = table;
                            this.FormatoGridListaPrecios(dgvGastos);

                            lblName.Text = (from item in Articulos.AsEnumerable()
                                            where item.Field<string>("ItemCode").ToLower().Equals(Articulo.ToLower())
                                            select item.Field<string>("ItemName")).FirstOrDefault();
                        }

                    }

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Stocks);
                            command.Parameters.AddWithValue("@Articulo", Articulo);
                            SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                            PrecCompra.Direction = ParameterDirection.Output;
                            command.Parameters.Add(PrecCompra);

                            DataTable table = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            kryptonDataGridView1.DataSource = table;
                            this.FormatoGridStocks(kryptonDataGridView1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
