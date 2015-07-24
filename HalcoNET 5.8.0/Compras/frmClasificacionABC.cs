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
using System.IO;

namespace Compras
{
    public partial class frmClasificacionABC : Form
    {
        public string Lineas;
        public string Articulo;
        public string Clasificacion;
        Clases.Logs log;

        public enum Columnas
        {
            Articulo, Nombre, Linea, Meses, _8020, _Ventas, _Linea, _Final
        }
        public frmClasificacionABC()
        {
            InitializeComponent();

            DataTable clasificaciones = new DataTable();
            clasificaciones.Columns.Add("Codigo");
            clasificaciones.Columns.Add("Nombre");

            clasificaciones.Rows.Add(new object[] { "0", "Todas" });
            clasificaciones.Rows.Add(new object[] { "AAA", "Clasificación AAA" });
            clasificaciones.Rows.Add(new object[] { "AA", "Clasificación AA" });
            clasificaciones.Rows.Add(new object[] { "A", "Clasificación A" });
            clasificaciones.Rows.Add(new object[] { "BBB", "Clasificación BBB" });
            clasificaciones.Rows.Add(new object[] { "BB", "Clasificación BB" });
            clasificaciones.Rows.Add(new object[] { "B", "Clasificación B" });
            clasificaciones.Rows.Add(new object[] { "C", "Clasificación C" });

            clbClasificacion.DataSource = clasificaciones;
            clbClasificacion.DisplayMember = "Nombre";
            clbClasificacion.ValueMember = "Codigo";
        }

        public void Formato()
        {
            gridClasificacion.Columns[(int)Columnas.Articulo].Width = 100;
            gridClasificacion.Columns[(int)Columnas.Nombre].Width = 200;
            gridClasificacion.Columns[(int)Columnas.Linea].Width = 100;
            gridClasificacion.Columns[(int)Columnas.Meses].Width = 90;
            gridClasificacion.Columns[(int)Columnas._8020].Width = 90;
            gridClasificacion.Columns[(int)Columnas._Ventas].Width = 90;
            gridClasificacion.Columns[(int)Columnas._Linea].Width = 90;
            gridClasificacion.Columns[(int)Columnas._Final].Width = 90;

            gridClasificacion.Columns[(int)Columnas.Meses].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridClasificacion.Columns[(int)Columnas._8020].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridClasificacion.Columns[(int)Columnas._Ventas].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridClasificacion.Columns[(int)Columnas._Linea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridClasificacion.Columns[(int)Columnas._Final].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

        private void button1_Click(object sender, EventArgs e)
        {
            Articulo = txtArticulo.Text;
            Lineas = this.GetCadena(clbLinea);
            Clasificacion = this.GetCadena(clbClasificacion);
            try
            {

                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_ClasificacionABC", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@PArticulo", Articulo);
                        command.Parameters.AddWithValue("@Clasificaciones", Clasificacion);
                        command.Parameters.AddWithValue("@Lineas", Lineas);
                        command.CommandTimeout = 0;

                        DataTable table = new DataTable();
                        SqlDataAdapter adapder = new SqlDataAdapter();
                        adapder.SelectCommand = command;
                        adapder.Fill(table);

                        gridClasificacion.DataSource = table;

                        this.Formato();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: "+ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        DataTable Articul = new DataTable();
        private void ClasificacionABC_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.CargarLinea(clbLinea, "Todas");
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

        private void clbBox_Click(object sender, EventArgs e)
        {
            if (((CheckedListBox)sender).SelectedIndex == 0)
            {
                if (((CheckedListBox)sender).CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < ((CheckedListBox)sender).Items.Count; item++)
                    {
                        ((CheckedListBox)sender).SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < ((CheckedListBox)sender).Items.Count; item++)
                    {
                        ((CheckedListBox)sender).SetItemChecked(item, true);
                    }
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fichero = new SaveFileDialog();
                string[] encabezado;
                fichero.Filter = "Archivo de valores separados por tabulaciones (.txt)|*.txt";
                // "Excel (*.xls)|*.xls";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    encabezado = new string[ gridClasificacion.Rows.Count + 2];
                    //    MessageBox.Show("El archivo se creo con exíto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //StreamWriter file = new StreamWriter(fichero.FileName);
                    int x = 0;
                    foreach (DataGridViewColumn item in gridClasificacion.Columns)
                    {
                        x++;
                        //file.Write(item.HeaderText + ",");
                        encabezado[0] += x.ToString() + "\t";
                        encabezado[1] += item.HeaderText + "\t";
                    }
                    //file.Write("\n");
                    x = 2;
                    foreach (DataGridViewRow row in gridClasificacion.Rows)
                    {
                        for (int i = 0; i < gridClasificacion.Columns.Count; i++)
                        {
                            //file.Write(row.Cells[i].Value.ToString() + ",");
                            encabezado[x] += " " + row.Cells[i].Value.ToString().Replace('\t',' ') + "\t";
                        }
                        //file.Write("\n");
                        x++;
                    }
                   // file.Close();

                    File.WriteAllLines(fichero.FileName, encabezado, Encoding.UTF8);
                        
                    MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gridClasificacion.DataSource = null;
            txtArticulo.Clear();
        }

        private void txtArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button1_Click(sender, e);
        }

        private void ClasificacionABC_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void ClasificacionABC_FormClosing(object sender, FormClosingEventArgs e)
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
