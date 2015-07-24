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

namespace Ventas.Catalogos
{
    public partial class Promociones : Form
    {
        private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();


        public Promociones()
        {
            InitializeComponent();
        }

        private void GetData(string selectCommand)
        {
            try
            {
                // Specify a connection string. Replace the given value with a  
                // valid connection string for a Northwind SQL Server sample 
                // database accessible to your system.

                // Create a new data adapter based on the specified query.
                dataAdapter = new SqlDataAdapter(selectCommand, ClasesSGUV.Propiedades.conectionSGUV);

                // Create a command builder to generate SQL update, insert, and 
                // delete commands based on selectCommand. These are used to 
                // update the database.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable();
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                //dataGridView1.AutoResizeColumns(
                //    DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }

        private void Promociones_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bindingSource1;
            GetData(@"SELECT [Code]
                      ,[U_Sucursal] 'Sucursal'
                      ,[U_Articulo] 'Artículo'
                      ,[U_Precio] 'Precio de promocion'
                      ,[U_Mes] 'Mes'
                  FROM [SGUV].[dbo].[@PromocionesPJ]
                  WHERE [U_Articulo] like '" + textBox1.Text + "%'");
        }

        private void submit_Click(object sender, EventArgs e)
        {
            dataAdapter.Update((DataTable)bindingSource1.DataSource);
            GetData(dataAdapter.SelectCommand.CommandText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Promociones_Load(sender, e);
           // GetData(dataAdapter.SelectCommand.CommandText);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var query = from item in (bindingSource1.DataSource as DataTable).AsEnumerable()
                            where item.Field<string>("U_Articulo").Contains(textBox1.Text)
                            select item;

                bindingSource1.DataSource = query.CopyToDataTable();
            }
            catch (Exception)
            {
            }
        }
    }
}
