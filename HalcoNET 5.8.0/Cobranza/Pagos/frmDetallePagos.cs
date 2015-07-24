using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Cobranza
{
    public partial class DetallePagos : Form
    {
        private string _Factura;
        public DetallePagos(String Factura)
        {
            _Factura = Factura;
            InitializeComponent();
        }

        private void DetallePagos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            textBox1.Text = _Factura;
            ConsultarPagos(_Factura);
        }

        public void ConsultarPagos(string _factura)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    //con.Open();
                    SqlCommand command = new SqlCommand("PJ_SaldosPendientes", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Factura", _factura);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);
                    dataGridView1.DataSource = table;

                    DataRow _total = table.NewRow();
                    _total["Pago"] = Convert.ToDecimal(table.Compute("SUM(Pago)", ""));
                    table.Rows.Add(_total);

                    dataGridView1.Columns[1].DefaultCellStyle.Format = "C2";

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Style.BackColor = Color.LightGray;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
          
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
