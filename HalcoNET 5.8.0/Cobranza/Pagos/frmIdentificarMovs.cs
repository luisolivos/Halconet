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

namespace Cobranza.Pagos
{
    public partial class frmIdentificarMovs : Form
    {
        Clases.Logs log;
        public frmIdentificarMovs()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            ID, Fecha, Cuenta, Mondeda, Pago, JmlMemo, Descripcion, Identificado, Buton, Desconsiliar
        }

        public void Formato(DataGridView dgv)
        {
           
            DataGridViewButtonColumn boton = new DataGridViewButtonColumn();
            {
                boton.Name = "Identificar";
                boton.HeaderText = "Identificar";
                boton.Text = "Identificar";
                boton.Width = 100;
                boton.UseColumnTextForButtonValue = true;
            }
            dgv.Columns.Add(boton);

            DataGridViewButtonColumn boton1 = new DataGridViewButtonColumn();
            {
                boton1.Name = "Desconciliar";
                boton1.HeaderText = "Desconciliar";
                boton1.Text = "Desconciliar";
                boton1.Width = 100;
                boton1.UseColumnTextForButtonValue = true;
            }
            dgv.Columns.Add(boton1);

            dgv.Columns[(int)Columnas.ID].Visible = false;
            dgv.Columns[(int)Columnas.Cuenta].Visible = false;

            dgv.Columns[(int)Columnas.Fecha].Width = 120;
            dgv.Columns[(int)Columnas.Cuenta].Width = 100;
            dgv.Columns[(int)Columnas.Mondeda].Width = 100;
            dgv.Columns[(int)Columnas.Pago].Width = 100;
            dgv.Columns[(int)Columnas.Pago].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Pago].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.JmlMemo].Width = 100;
            dgv.Columns[(int)Columnas.Descripcion].Width = 100;
            dgv.Columns[(int)Columnas.Identificado].Width = 100;

            dgv.Columns[(int)Columnas.Fecha].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cuenta].ReadOnly = true;
            dgv.Columns[(int)Columnas.Mondeda].ReadOnly = true;
            dgv.Columns[(int)Columnas.Pago].ReadOnly = true;
            dgv.Columns[(int)Columnas.JmlMemo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Descripcion].ReadOnly = true;
            dgv.Columns[(int)Columnas.Identificado].ReadOnly = true;
        }

        public void CargarCtasBancos()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 4);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                    command.Parameters.AddWithValue("@Banco", string.Empty);
                    command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                    command.Parameters.AddWithValue("@Abono", decimal.Zero);
                    command.Parameters.AddWithValue("@Referencia", string.Empty);
                    command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                    command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                    command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                    command.Parameters.AddWithValue("@Code", 0);

                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);


                    cbBanco.DataSource = table;
                    cbBanco.ValueMember = "Codigo";
                    cbBanco.DisplayMember = "Nombre";
                }
            }
        }

        private void IdentificacionMovimientos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                this.CargarCtasBancos();
            }
            catch (Exception )
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 5);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaDesde", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@FechaHasta", dateTimePicker2.Value);

                    command.Parameters.AddWithValue("@Banco", string.Empty);
                    command.Parameters.AddWithValue("@CuentaContable", cbBanco.SelectedValue);
                    command.Parameters.AddWithValue("@Abono", decimal.Zero);
                    command.Parameters.AddWithValue("@Referencia", string.Empty);
                    command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                    command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                    command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                    command.Parameters.AddWithValue("@Code", 0);

                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);


                    dataGridView1.DataSource = table;

                    this.Formato(dataGridView1);
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)Columnas.Buton)
                {
                    if ((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Identificado].Value.ToString() != "Identificado")
                    {
                        string Cliente = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.JmlMemo].Value.ToString();
                        int Code = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.ID].Value);
                        decimal Abono = Convert.ToDecimal((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Pago].Value);
                        string Descripcion = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Descripcion].Value.ToString();
                        string CuentaNI = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Cuenta].Value.ToString();
                        DateTime FechaMov = Convert.ToDateTime((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Fecha].Value);

                        Cobranza.Pagos.frmFacturas fact = new frmFacturas(Cliente, Descripcion, Abono, Code, CuentaNI, FechaMov);
                        fact.ShowDialog();

                        button1_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("El movimiento ya fue identificado.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                if (e.ColumnIndex == (int)Columnas.Desconsiliar)
                {

                    //string Cliente = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.JmlMemo].Value.ToString();
                    //int Code = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.ID].Value);
                    //decimal Abono = Convert.ToDecimal((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Pago].Value);
                    //string Descripcion = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Descripcion].Value.ToString();
                    //string CuentaNI = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Cuenta].Value.ToString();
                    //DateTime FechaMov = Convert.ToDateTime((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Fecha].Value);

                    int _code = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[(int)Columnas.ID].Value);

                    //Cliente = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[(int)Columnas.Cliente].Value);

                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "PJ_Pagos";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 11);
                            command.Parameters.AddWithValue("@Sucursal", string.Empty);
                            command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                            command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                            command.Parameters.AddWithValue("@Banco", string.Empty);
                            command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                            command.Parameters.AddWithValue("@Abono", decimal.Zero);
                            command.Parameters.AddWithValue("@Referencia", string.Empty);
                            command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                            command.Parameters.AddWithValue("@Cliente", string.Empty);
                            command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                            command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                            command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                            command.Parameters.AddWithValue("@Code", _code);

                            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                            parameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter);

                            DialogResult res = MessageBox.Show("El movimiento será desconciliado, \r\n¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (res == System.Windows.Forms.DialogResult.Yes)
                            {
                                connection.Open();
                                command.ExecuteNonQuery();

                                button1_Click(sender, e);
                            }
                        }
                    }

                }
                
            }
            catch (Exception)
            {
            }
        }

        private void IdentificacionMovimientos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();

            }
            catch (Exception)
            {
             
            }
        }

        private void IdentificacionMovimientos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //try
            //{
            //    foreach (DataGridViewRow item in dataGridView1.Rows)
            //    {
            //        if (item.Cells[(int)Columnas.Identificado].Value.ToString() == "Identificado")
            //        {
            //            item.Cells[(int)Columnas.Buton]. = true;
            //        }
            //        else
            //        {
            //            item.Cells[(int)Columnas.Buton].ReadOnly = false;
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //}
        }
    }
}
