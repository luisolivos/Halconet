using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compras
{
    public partial class frmTraspasosFactura : Form
    {
        public enum Columas
        {
            NoFact,
            FechaFact,
            CardName,
            MontoFact,
            NoTraspasos,
            Fechatraspaso,
            Montototal,
            Dif,
            Origen,
            Destino
        }

        public frmTraspasosFactura()
        {
            InitializeComponent();
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

        private void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columas.NoFact].Width = 95;
            dgv.Columns[(int)Columas.FechaFact].Width = 95;
            dgv.Columns[(int)Columas.CardName].Width = 200;
            dgv.Columns[(int)Columas.MontoFact].Width = 95;
            dgv.Columns[(int)Columas.NoTraspasos].Width = 95;
            dgv.Columns[(int)Columas.Fechatraspaso].Width = 95;
            dgv.Columns[(int)Columas.Montototal].Width = 95;
            dgv.Columns[(int)Columas.Origen].Width = 120;
            dgv.Columns[(int)Columas.Destino].Width = 120;
            dgv.Columns[(int)Columas.Dif].Width = 95;

            dgv.Columns[(int)Columas.Montototal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columas.MontoFact].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columas.Dif].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columas.Montototal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.MontoFact].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.Dif].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        private void frmTraspasosFactura_Load(object sender, EventArgs e)
        {
            this.Almacenes();

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;
                        command.Parameters.AddWithValue("@TipoConsulta", 24);
                        command.Parameters.AddWithValue("@FechaInical", dtpInicial.Value);
                        command.Parameters.AddWithValue("@FechaFinal", dtpFinal.Value);
                        command.Parameters.AddWithValue("@Almacenes", this.Cadena(cbAlmacen));

                        DataTable tbl = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;

                        this.Formato(dgvDatos);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in (sender as DataGridView).Rows)
                {
                    if (row.Cells[(int)Columas.NoFact].Value.ToString().Contains("Total"))
                    {
                        row.Cells[(int)Columas.NoFact].Style.ForeColor = Color.LightGray;
                        row.DefaultCellStyle.BackColor = Color.LightGray;

                        if (Convert.ToDecimal(row.Cells[(int)Columas.Dif].Value) < decimal.Zero)
                        {
                            row.Cells[(int)Columas.Dif].Style.BackColor = Color.Red;
                            row.Cells[(int)Columas.Dif].Style.ForeColor = Color.White;
                        }
                    }
                    else
                    {
                        row.Cells[(int)Columas.Dif].Style.ForeColor = Color.White;
                    }

                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
