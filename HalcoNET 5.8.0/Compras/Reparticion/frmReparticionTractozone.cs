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
    public partial class frmReparticionTractozone : Form
    {

        DataTable Articul = new DataTable();
        DataTable Repartir = new DataTable();
        DataTable Procesado = new DataTable();

        DataTable almacenes = new DataTable();

        public enum ColumnasGrd
        {
            Articulo,
            Linea,
            mty,
            pue,
            gdl,
            cor,
            mex,
            tep,
            api,
            prov
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasGrd.mty].Width = 70;
            dgv.Columns[(int)ColumnasGrd.pue].Width = 70;
            dgv.Columns[(int)ColumnasGrd.gdl].Width = 70;
            dgv.Columns[(int)ColumnasGrd.cor].Width = 70;
            dgv.Columns[(int)ColumnasGrd.mex].Width = 70;
            dgv.Columns[(int)ColumnasGrd.api].Width = 70;
            dgv.Columns[(int)ColumnasGrd.tep].Width = 70;

        }

        public frmReparticionTractozone()
        {
            InitializeComponent();

        }

        private void ReparticionStock_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            txtCantidad.Focus();
            this.ListaArticulos();

            txtArticulo.AutoCompleteCustomSource = Autocomplete(Articul, "ItemCode");
            txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;

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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
               // dgvProcesado.DataSource = null;
                DataTable aux = new DataTable();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_ReparticionStock";
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 5);
                        command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                        command.Parameters.AddWithValue("@CantiadOK", Convert.ToDecimal(txtCantidad.Text));

                        command.CommandTimeout = 0;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        DataTable table = new DataTable();
                        if (dgvProcesado.DataSource != null)
                            aux = (dgvProcesado.DataSource as DataTable).Copy();

                        adapter.Fill(table);

                        if(aux.Columns.Count > 0)
                            table.Merge(aux);
                        dgvProcesado.DataSource = table;


                        this.Formato(dgvProcesado);
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

                            button2_Click(sender, e);
                        }

                        encabezado = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportarExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarAExcel exp = new ExportarAExcel();
                exp.Exportar(dgvProcesado);
            }
            catch (Exception)
            {
                
            }
        }

    }
}
