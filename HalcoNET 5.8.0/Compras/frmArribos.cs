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
    public partial class frmArribos : Form
    {
        private int Vendedor;
        string Memo;
        Clases.Logs log;

        DataTable Datos = new DataTable();

        public frmArribos(int _slpcode)
        {
            InitializeComponent();
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription,0);
            Vendedor = _slpcode;
        }

        private string GetMemo()
        {
            string qry = "select Memo from OSLP Where SlpCode  = @Vendedor";
            try
            {
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                {
                    using (SqlCommand command = new SqlCommand(qry, conn))
                    {
                        conn.Open();

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Vendedor", Vendedor);

                        string memo = Convert.ToString(command.ExecuteScalar());
                        return memo;
                    }

                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
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
        DataTable Articul = new DataTable();
        private void Arribos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            Memo = GetMemo();

            SqlCommand command = new SqlCommand("PJ_NotificacionStock", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 5);
            command.Parameters.AddWithValue("@Comprador", string.Empty);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@Almacen", Memo);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

           // dgvStock.DataSource = table;

            Datos = table.Copy();

            this.ListaArticulos();

            txtArticulo.AutoCompleteCustomSource = Autocomplete(Articul, "ItemCode");
            txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Arribos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();   
            }
            catch (Exception)
            {
            }
        }

        private void Arribos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
        }

        private void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    var qry = from item in Datos.AsEnumerable()
            //              where item.Field<string>("Artículo").ToLower().Contains(txtArticulo.Text.ToLower())
            //              select item;

            //    dgvStock.DataSource = qry.CopyToDataTable();
            //}
            //catch (Exception)
            //{
            //    dgvStock.DataSource = null;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            toolMsj.BackColor = Color.FromName("Control");
            toolMsj.ForeColor = Color.Black;

            toolMsj.Text = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(txtArticulo.Text))
                {
                    var qry = from item in Datos.AsEnumerable()
                              where item.Field<string>("Artículo").ToLower().Equals(txtArticulo.Text.ToLower())
                              select item;

                    dgvStock.DataSource = qry.CopyToDataTable();
                }
                else
                {
                    var qry = from item in Datos.AsEnumerable()
                              //where item.Field<string>("Artículo").ToLower().Equals(txtArticulo.Text.ToLower())
                              select item;

                    dgvStock.DataSource = qry.CopyToDataTable();
                }
            }
            catch (Exception)
            {
                toolMsj.Text = "No hay pedidos recientes para este artículo.";
                toolMsj.BackColor = Color.Red;
                toolMsj.ForeColor = Color.White;

                dgvStock.DataSource = null;
            }
        }

        private void txtArticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
