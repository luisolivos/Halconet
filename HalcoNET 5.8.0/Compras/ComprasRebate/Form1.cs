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

namespace Compras
{
    public partial class Form1 : Form
    {
        private string Unidad;
        private int Linea;
        Clases.Logs log;


        #region METODOS
        public Form1()
        {
            InitializeComponent();
        }

        private void Esperar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }

        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }

        private void CargarLineas()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVarias", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);

            DataTable table = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(table);

            cbLineas.DataSource = table;
            cbLineas.DisplayMember = "Nombre";
            cbLineas.ValueMember = "Codigo";
        }

        private void FormatoGrid()
        {
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.Width = 100;
                if(Unidad == "Pesos" || Unidad == "USD")
                    item.DefaultCellStyle.Format = "C2";
                else
                    item.DefaultCellStyle.Format = "N0";
                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            dataGridView1.Columns[0].Width = 250;
            //dataGridView1.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font(dataGridView1.DefaultCellStyle.Font, FontStyle.Bold);
            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[1].Width = 80;

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[0].Value.ToString() == "CUMPLIMIENTO")
                {
                    item.DefaultCellStyle.Format = "P2";
                }
                if (item.Cells[0].Value.ToString() == "INVENTARIO MES")
                {
                    item.DefaultCellStyle.Format = "N2";
                }
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            //dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;

            dataGridView1.AllowUserToResizeColumns = true;
            
        }
        #endregion

        #region EVENTOS
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            this.CargarLineas();
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Esperar();
                foreach (Control item in groupBox1.Controls)
                {
                    if (item is RadioButton)
                    {
                        RadioButton ch = (RadioButton)item;
                        if (ch.Checked)
                            Unidad = ch.Text;
                    }
                }


                Linea = Convert.ToInt32(cbLineas.SelectedValue);
                SqlCommand command = new SqlCommand("PJ_REBATE_COMPRAS", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 0);
                command.Parameters.AddWithValue("@Linea", Linea);
                command.Parameters.AddWithValue("@Unidad", Unidad);
                command.Parameters.AddWithValue("@Desde", dateTimePicker1.Value);
                command.Parameters.AddWithValue("@Hasta", dateTimePicker2.Value);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(table);


                dataGridView1.DataSource = table;

                FormatoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {

                int j = 0;
              //  int rowAcumulado = 0;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (j == 0)
                    {
                        for (int i = 2; i < dataGridView1.Columns.Count; i++)
                        {

                            if (Convert.ToDecimal(item.Cells[i].Value) >= 1)
                            {
                                item.Cells[i].Style.BackColor = Color.Green;
                                item.Cells[i].Style.ForeColor = Color.Black;
                            }
                            else if (Convert.ToDecimal(item.Cells[i].Value) >= (decimal)0.9 && Convert.ToDecimal(item.Cells[i].Value) < 1)
                            {
                                item.Cells[i].Style.BackColor = Color.Yellow;
                                item.Cells[i].Style.ForeColor = Color.Black;
                            }
                            else if (Convert.ToDecimal(item.Cells[i].Value) < (decimal)0.9)
                            {
                                item.Cells[i].Style.BackColor = Color.Red;
                                item.Cells[i].Style.ForeColor = Color.White;
                            }
                        }
                        
                    }
                    if (j == 4)
                    {
                        for (int i = 2; i < dataGridView1.Columns.Count; i++)
                        {

                            if (Convert.ToDecimal(item.Cells[i].Value) >= 0)
                            {
                                item.Cells[i].Style.BackColor = Color.Green;
                                item.Cells[i].Style.ForeColor = Color.Black;
                            }
                            else if (Convert.ToDecimal(item.Cells[i].Value) < (decimal)0)
                            {
                                item.Cells[i].Style.BackColor = Color.Red;
                                item.Cells[i].Style.ForeColor = Color.White;
                            }
                        }
                    }
                    j++;
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult r = fontDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                this.Font = fontDialog1.Font;
            }
        }
        #endregion

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
