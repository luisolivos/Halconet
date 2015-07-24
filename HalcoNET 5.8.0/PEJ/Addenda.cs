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


namespace PEJ
{
    public partial class Addenda : Form
    {
        string Tipo = string.Empty;
        public Addenda()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tipo = "Factura";
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PEJ_Facturacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 11);
                    command.Parameters.AddWithValue("@TipoDocumento", Tipo);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (!Convert.ToBoolean(item.Cells["Addenda"].Value))
                {
                    ClasesAddenda.Interfactura objInterfactura = new ClasesAddenda.Interfactura();
                    if(Tipo.Equals("Factura"))
                        objInterfactura.AddedaFactura(Convert.ToInt32(item.Cells["DocEntry"].Value), Tipo);
                    if (Tipo.Equals("NotaDebito"))
                        objInterfactura.AddedaDebito(Convert.ToInt32(item.Cells["DocEntry"].Value), Tipo);
                    if (Tipo.Equals("NotaCredito"))
                        objInterfactura.AddedaCredito(Convert.ToInt32(item.Cells["DocEntry"].Value), Tipo);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Tipo = "NotaDebito";
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PEJ_Facturacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 11);
                    command.Parameters.AddWithValue("@TipoDocumento", Tipo);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Tipo = "NotaCredito";
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PEJ_Facturacion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 11);
                    command.Parameters.AddWithValue("@TipoDocumento", Tipo);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);

                    dataGridView1.DataSource = table;
                }
            }
        }

        private void Addenda_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }
    }
}
