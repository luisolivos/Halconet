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

namespace Cobranza.Score
{
    public partial class Objetivos : Form
    {
        public Objetivos()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Code,
            U_Cobranza,
            U_Sucursal,
            U_ValidFrom,
            U_ValidTo,
            U_Objevito,
            Boton
        }
        private DataTable _Datos = new DataTable();

        private void Objetivos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            DateTime fechatemp = DateTime.Today;
            DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);
            DateTime fecha2 = new DateTime(fechatemp.Year, fechatemp.Month + 1, 1).AddDays(-1);


            dtInicial.Value = fecha1;
            dtFinal.Value = fecha2;
        }

        public void Formato()
        {
            DataGridViewButtonColumn b = new DataGridViewButtonColumn();
            b.HeaderText = "Eliminar";
            b.Name = "Eliminar";
            b.Text = "Eliminar";
            b.UseColumnTextForButtonValue = true;
            
            gridFacturas.Columns.Add(b);

            /*
              botonPCB.Name = "Actualizar Rebate";
                botonPCB.HeaderText = "Actualizar Rebate";
                botonPCB.Text = "Actu Rebate";
                botonPCB.Width = 130;
                botonPCB.UseColumnTextForButtonValue = true;
             */

            gridFacturas.Columns[(int)Columnas.Code].Visible = false;

            gridFacturas.Columns[(int)Columnas.U_Cobranza].Width = 200;
            gridFacturas.Columns[(int)Columnas.U_Sucursal].Width = 100;
            gridFacturas.Columns[(int)Columnas.U_ValidFrom].Width = 100;
            gridFacturas.Columns[(int)Columnas.U_ValidTo].Width = 100;
            gridFacturas.Columns[(int)Columnas.U_Objevito].Width = 150;

            gridFacturas.Columns[(int)Columnas.U_Sucursal].ReadOnly = true;
            gridFacturas.Columns[(int)Columnas.U_ValidFrom].ReadOnly = true;
            gridFacturas.Columns[(int)Columnas.U_ValidTo].ReadOnly = true;

            gridFacturas.Columns[(int)Columnas.U_Objevito].DefaultCellStyle.Format = "C4";
            gridFacturas.Columns[(int)Columnas.U_Objevito].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btn_Consultar_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            DataTable _t = new DataTable();
            _Datos.Clear();
            gridFacturas.Columns.Clear();
            gridFacturas.DataSource = null;

            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 9);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                    command.Parameters.AddWithValue("@FechaInicial", dtInicial.Value);
                    command.Parameters.AddWithValue("@FechaFinal", dtFinal.Value);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.CommandTimeout = 0;


                    //Datos.Clear();
                    
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(_t);
                    command.CommandTimeout = 0;

                    gridFacturas.DataSource = _t;
                    _Datos = _t.Copy();
                    if (_Datos.Rows.Count > 0)
                    {
                        textBox1.Enabled = true;
                        this.Formato();
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var query = from item in _Datos.AsEnumerable()
                        where item.Field<string>("U_Cobranza").ToUpper().Contains(textBox1.Text.ToUpper())
                        select item;

            if (query.Count() > 0)
            {
                gridFacturas.DataSource = query.CopyToDataTable();
            }
        }

        private void gridFacturas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)Columnas.U_Objevito)
                {

                    string query = "Update ObjMesCobranza SET U_Objetivo = @Objetivo where Code = @Code";
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            decimal objetivo = Convert.ToDecimal(gridFacturas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                            int code = Convert.ToInt32(gridFacturas.Rows[e.RowIndex].Cells[(int)Columnas.Code].Value);
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@Objetivo", objetivo);
                            command.Parameters.AddWithValue("@Code", code);

                            connection.Open();

                            command.ExecuteNonQuery();
                        }
                    }

                }
                if (e.ColumnIndex == (int)Columnas.U_Cobranza)
                {
                    string query = "Update ObjMesCobranza SET U_Cobranza = @Cobranza where Code = @Code";
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            string cobranza = Convert.ToString(gridFacturas.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                            int code = Convert.ToInt32(gridFacturas.Rows[e.RowIndex].Cells[(int)Columnas.Code].Value);
                            command.CommandType = CommandType.Text;
                            command.Parameters.AddWithValue("@Cobranza", cobranza);
                            command.Parameters.AddWithValue("@Code", code);

                            connection.Open();

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NuevoObjetiv nuevo = new NuevoObjetiv();
            nuevo.ShowDialog();

            btn_Consultar_Click(sender, e);
            textBox1_TextChanged(sender, e);
        }

        private void gridFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {
            try
            {
                if (gridFacturas.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    DialogResult dialogResult = MessageBox.Show("El registro se eliminara de forma permanente\r\n¿Desea continuar?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string query = "Delete From ObjMesCobranza where Code = @Code";
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                int code = Convert.ToInt32(gridFacturas.Rows[e.RowIndex].Cells["Code"].Value);
                                command.CommandType = CommandType.Text;
                                command.Parameters.AddWithValue("@Code", code);

                                connection.Open();

                                command.ExecuteNonQuery();
                                btn_Consultar_Click(sender, e);
                                textBox1_TextChanged(sender, e);
                            }
                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //do something else
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
