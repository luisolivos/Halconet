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

namespace Compras.Desarrollo
{
    public partial class frmConsignas : Form
    {
        int __formato = 0;
        public frmConsignas()
        {
            InitializeComponent();
        }

        DataTable ClientesConsigna = new DataTable();

        public enum Columnas
        {
            cliente,
            nombre,
            sucursal,
            Vendedor,
            Articulo,
            stock,
            mnx,
            pz,
            ratio
        }

        public void formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Articulo].Width = 90;
            dgv.Columns[(int)Columnas.nombre].Width = 150;
            dgv.Columns[(int)Columnas.cliente].Width = 90;
            dgv.Columns[(int)Columnas.stock].Width = 90;
            dgv.Columns[(int)Columnas.mnx].Width = 90;
            dgv.Columns[(int)Columnas.pz].Width = 90;
            dgv.Columns[(int)Columnas.ratio].Width = 90;

            dgv.Columns[(int)Columnas.stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.mnx].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.pz].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.ratio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.stock].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.mnx].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas.pz].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.ratio].DefaultCellStyle.Format = "N2";
        }

        public void ListaClientes()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_Compras";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 10);

                    command.CommandTimeout = 0;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(ClientesConsigna);

                    DataRow row = ClientesConsigna.NewRow();
                    row["Nombre"] = "TODOS";
                    row["Codigo"] = "";
                    ClientesConsigna.Rows.InsertAt(row, 0);

                    cbClientes.DataSource = ClientesConsigna;
                    cbClientes.DisplayMember  = "Nombre";
                    cbClientes.ValueMember = "Codigo";
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 9);
                        command.Parameters.AddWithValue("@Cliente", cbClientes.SelectedValue);
                        command.Parameters.AddWithValue("@FechaInical", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@FechaFinal", dateTimePicker2.Value);


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        DataTable table = new DataTable();
                        da.Fill(table);



                        if (table.Columns.Count == 1)
                        {
                            MessageBox.Show("El rago de fechas debe ser de mas de 1 mes");
                        }
                        else
                        {
                            gridCompras.DataSource = table;
                            if (__formato < 1)
                                this.formato(gridCompras);

                            if (gridCompras.Rows.Count > 0)
                                __formato++;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void frmConsignas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                this.ListaClientes();
            }
            catch (Exception)
            {
                
            }
        }
    }
}
