using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.GestionCobranza
{
    public partial class frmNoLlamadas : Form
    {
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();

        private void GetData(string selectCommand)
        {
            try
            {
                dataAdapter = new SqlDataAdapter(selectCommand, ClasesSGUV.Propiedades.conectionSGUV);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }


        public frmNoLlamadas()
        {
            InitializeComponent();
        }

        private void frmNoLlamadas_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
            GetData(@"SELECT [CardCode]
                        FROM [SGUV].[dbo].[tbl_NoLlamada]");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataAdapter.Update((DataTable)bindingSource1.DataSource);
            GetData(dataAdapter.SelectCommand.CommandText);
        }
    }
}
