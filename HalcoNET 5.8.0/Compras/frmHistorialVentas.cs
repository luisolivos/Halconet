using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace Compras
{
    public partial class frmHistorialVentas : Form
    {
        Clases.Logs log;
        public frmHistorialVentas()
        {
            InitializeComponent();
        }
        DataTable Articul = new DataTable();
        private void HistorialVentas_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

            cbAlmacen.SelectedIndex = 0;
            CargarLinea(cbLinea, "Todas");
            cbLinea.SelectedIndex = 0;
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

        private void button1_Click(object sender, EventArgs e)
        {
            gridTotal.DataSource = null;
            
            string almacen = cbAlmacen.Text.Substring(0, 2) == "00" ? string.Empty : cbAlmacen.Text.Substring(0, 2);
            string linea = cbLinea.SelectedValue.ToString() == "0" ? string.Empty : cbLinea.SelectedValue.ToString();

            SqlCommand command = new SqlCommand("sp_HistorialVentas");
            command.Parameters.AddWithValue("@TipoConsulta", 1);
            command.Parameters.AddWithValue("@Almacen", almacen);
            command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
            command.Parameters.AddWithValue("@Linea", linea);

            Clases.Ventas vts = new Clases.Ventas();
            gridTotal.DataSource = vts.GetVentas(command);


            foreach (DataGridViewColumn item in gridTotal.Columns)
            {
                if (item.Index > 2)
                {
                    item.Width = 60;
                    item.DefaultCellStyle.Format = "N0";
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else
                {
                    item.Width = 100;
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                }

            }

            //SqlConnection c = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            //SqlCommand command = new SqlCommand("PJ_TransferenciasP", c);
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@TipoConsulta", 4);
            //command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
            //command.Parameters.AddWithValue("@Linea", cbLinea.SelectedValue);
            //command.Parameters.AddWithValue("@AlmacenDestino", almacen);
            //command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            //command.Parameters.AddWithValue("@Proveedor", string.Empty);
            //command.Parameters.AddWithValue("@Importacion", string.Empty);

            //DataTable table = new DataTable();
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = command;
            //adapter.SelectCommand.CommandTimeout = 0;
            //adapter.Fill(table);

            //gridTotal.DataSource = table;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cbAlmacen.SelectedIndex = 0;
            cbLinea.SelectedIndex = 0;
            txtArticulo.Text = "";
            gridTotal.DataSource = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Archivo de valores separados por comas de Microsoft Excel (.csv)|*.csv";
                // "Excel (*.xls)|*.xls";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    //    MessageBox.Show("El archivo se creo con exíto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StreamWriter file = new StreamWriter(fichero.FileName);

                    foreach (DataGridViewColumn item in gridTotal.Columns)
                    {
                        file.Write(item.HeaderText + ",");
                    }
                    file.Write("\n");
                    foreach (DataGridViewRow row in gridTotal.Rows)
                    {
                        for (int i = 0; i < gridTotal.Columns.Count; i++)
                        {
                            file.Write(row.Cells[i].Value.ToString() + ",");
                        }
                        file.Write("\n");
                    }
                    file.Close();
                    MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HistorialVentas_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void HistorialVentas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                log.Fin();
            }
        }
    }
}
