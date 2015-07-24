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
    public partial class frmIdealesArticulosAlmacen : Form
    {
        Clases.Logs log;

        DataTable Articulos = new DataTable();

        public enum Columnas 
        {
            Linea,
            Artiuculo,
            Descripcion,
            Almacen,
            Nombre,
            Stock,
            Solicitado,
            Ideal
        }

        public void Formato(DataGridView dgv)
        {
            dgv.RowHeadersWidth = 50;

            dgv.Columns[(int)Columnas.Linea].Width = 90;
            dgv.Columns[(int)Columnas.Artiuculo].Width = 100;
            dgv.Columns[(int)Columnas.Descripcion].Width = 250;
            dgv.Columns[(int)Columnas.Almacen].Width = 70;
            dgv.Columns[(int)Columnas.Nombre].Width = 90;
            dgv.Columns[(int)Columnas.Stock].Width = 90;
            dgv.Columns[(int)Columnas.Solicitado].Width = 90;
            dgv.Columns[(int)Columnas.Ideal].Width = 90;

            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Solicitado].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Ideal].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Solicitado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public frmIdealesArticulosAlmacen()
        {
            InitializeComponent();
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
                    adapter.Fill(Articulos);

                    //return Articulos;
                }
            }
        }

        private string Cadena(CheckedListBox clb)
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

        public void Almacenes()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_ReparticionStock";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 4);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@CantiadOK", decimal.Zero);
                    command.Parameters.AddWithValue("@Incremento", decimal.Zero);

                    command.CommandTimeout = 0;

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = "Todos";
                    row["Codigo"] = "0";
                    table.Rows.InsertAt(row, 0);

                    cbAlmacen.DataSource = table;
                    cbAlmacen.DisplayMember = "Nombre";
                    cbAlmacen.ValueMember = "Codigo";

                    //return Articulos;
                }
            }
        }

        private void IdealesArticulosAlmacen_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.CargarLinea(cbLinea, "Todas");
                this.ListaArticulos();
                this.Almacenes();

                txtArticulo.AutoCompleteCustomSource = Autocomplete(Articulos, "ItemCode");
                txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV)))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 5);
                    command.Parameters.AddWithValue("@AlmacenCabecera", string.Empty);
                    command.Parameters.AddWithValue("@Almacenes", this.Cadena(cbAlmacen));
                    command.Parameters.AddWithValue("@Lineas", this.Cadena(cbLinea));
                    command.Parameters.AddWithValue("@Proveedores", string.Empty);
                    command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);

                    dataGridView1.DataSource = table;

                    this.Formato(dataGridView1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clb_Click(object sender, EventArgs e)
        {
            if ((sender as CheckedListBox).SelectedIndex == 0)
            {
                if ((sender as CheckedListBox).CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < (sender as CheckedListBox).Items.Count; item++)
                    {
                        (sender as CheckedListBox).SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < (sender as CheckedListBox).Items.Count; item++)
                    {
                        (sender as CheckedListBox).SetItemChecked(item, true);
                    }
                }
            }

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Exportar sin formato?\r\n Si elige 'No' el proceso puede durar varios minutos.", "HalcoNET", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
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

        private void IdealesArticulosAlmacen_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void IdealesArticulosAlmacen_FormClosing(object sender, FormClosingEventArgs e)
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
