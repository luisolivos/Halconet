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

namespace Cobranza.Catalogos
{
    public partial class CatalogosCob : Form
    {
        string TipoIndicador;// = "CV";
       
       // private DataGridView dataGridView1 = new DataGridView();
        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();

        public enum Columnas
        {
            Code, Tipo, Mes, Objetivo, Jefa
        }

        public CatalogosCob(string _tipo)
        {
            InitializeComponent();
            TipoIndicador = _tipo;
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Code].Visible = false;

            dgv.Columns[(int)Columnas.Tipo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Mes].ReadOnly = true;
            dgv.Columns[(int)Columnas.Code].Width = 60;
            dgv.Columns[(int)Columnas.Tipo].Width = 60;
            dgv.Columns[(int)Columnas.Mes].Width = 60;
            dgv.Columns[(int)Columnas.Objetivo].Width = 80;
            dgv.Columns[(int)Columnas.Jefa].Width = 150;

            if (TipoIndicador == "CV")
                dgv.Columns[(int)Columnas.Objetivo].DefaultCellStyle.Format = "P2";
            if (TipoIndicador == "DC")
                dgv.Columns[(int)Columnas.Objetivo].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)Columnas.Objetivo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dgv.Columns[(int)Columnas.Objetivo].DefaultCellStyle.BackColor = Color.FromName("Active");
            //dgv.Columns[(int)Columnas.Jefa].DefaultCellStyle.BackColor = Color.FromName("Active");
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

        private void CatalogosCob_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                if (TipoIndicador == "CV")
                {
                    this.Text = "Objetivos de cartera vencida";
                    label2.Text = "Objetivo de cartera vencida (%).";
                }
                if (TipoIndicador == "DC")
                {
                    label2.Text = "Objetivo de días cartera.";
                    this.Text = "Objetivos de días cartera";
                }
            }
            catch (Exception) { }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = TipoIndicador;
                }

                if (e.ColumnIndex == 2)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = comboBox1.SelectedIndex + 1;
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != string.Empty)
                {
                    int mes = comboBox1.SelectedIndex + 1;
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = bindingSource1;
                    GetData(@"DECLARE @NUM INT =  (Select COUNT(*) 
				            from [@ObjetivosCobranza]
				            where U_Mes = " + mes + " AND U_Tipo = '" + TipoIndicador + "' )" +

                            @"IF @NUM = 0
	                            INSERT INTO [@ObjetivosCobranza] 
	                            Select distinct '" + TipoIndicador + "', " + mes + @", 0, U_Cobranza 
	                            from [SBO-DistPJ].DBO.OSLP
	                            where U_Cobranza is not null 
	                            and Active = 'Y'"+
                            @"Select 
                                    Code, U_Tipo as Tipo, U_Mes as Mes, U_Objetivo as Objetivo, U_JefaCobranza as 'Jefa de cobranza'
                            from [@ObjetivosCobranza]
                            where U_Mes = "+ mes +" AND U_Tipo = '" + TipoIndicador + "' ");

                    this.Formato(dataGridView1);
                }
            }
            catch (Exception ) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataAdapter.Update((DataTable)bindingSource1.DataSource);
            GetData(dataAdapter.SelectCommand.CommandText);
        }
    }
}
