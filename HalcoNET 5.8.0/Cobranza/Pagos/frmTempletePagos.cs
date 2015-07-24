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
using System.IO;

using Excel = Microsoft.Office.Interop.Excel;

namespace Cobranza.Pagos
{
    public partial class frmTempletePagos : Form
    {
        DataTable TblDetalle = new DataTable();
        Clases.Logs log;
        public frmTempletePagos()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Code, Folio, FechaMov, Cliente, Moneda, Abono, FormaPagoDs, TipoCambio, Boton
        }

        public enum SeriesPagos
        {
            PRPUE = 17, 
            PRAPZ = 77, 
            PRCOR = 78, 
            PRMEX = 79, 
            PRGDL = 80, 
            PRMTY = 81, 
            PRTEP = 82,
            PRSAL = 160
        }

        public void Formato()
        {
            DataGridViewButtonColumn boton = new DataGridViewButtonColumn();
            {
                boton.Name = "Desconciliar";
                boton.HeaderText = "Desconciliar Mov.";
                boton.Text = "Desconciliar";
                boton.Width = 100;
                boton.UseColumnTextForButtonValue = true;
            }

            dataGridView1.Columns.Add(boton);

            dataGridView1.Columns[(int)Columnas.Abono].DefaultCellStyle.Format = "C4";

            dataGridView1.Columns[(int)Columnas.Folio].HeaderText = "No.";

            dataGridView1.Columns[(int)Columnas.Code].Visible = false;
            dataGridView1.Columns[(int)Columnas.Folio].Width = 50;
            dataGridView1.Columns[(int)Columnas.Folio].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.FechaMov].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.Abono].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.Moneda].ReadOnly = true;
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
                    adapter.Fill(table);


                    cbBanco.DataSource = table;
                    cbBanco.ValueMember = "Codigo";
                    cbBanco.DisplayMember = "Nombre";
                }
            }
        }

        public void Detalle()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 10);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

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

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(TblDetalle);

                }
            }
        }

        public void ActualizarTempleate(int Folio)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 17);
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

                    command.Parameters.AddWithValue("@Code", Folio);

                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            //string stringquery = "";
            //using (SqlConnection connection = new SqlConnection())
            //{
            //    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand())
            //    {
            //        command.CommandText = stringquery;
            //        command.Connection = connection;

            //        command.CommandType = CommandType.Text;
            //        command.Parameters.AddWithValue("@Folio", Folio);

            //        command.ExecuteNonQuery();
            //    }
                
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TblDetalle.Clear();
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

                        command.Parameters.AddWithValue("@TipoConsulta", 8);
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
                        this.Formato();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TempletePagos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                this.CargarCtasBancos();
                progressBar.Step = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Excel.Workbook MyBookRCT2 = null;
            Excel.Workbook MyBook = null;
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    FolderBrowserDialog f = new FolderBrowserDialog();
                    f.Description = "Elija una carpeta para guardar el template.";
                    f.ShowDialog();

                    string path = f.SelectedPath;

                    if (!string.IsNullOrEmpty(path))
                    {
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\ORCT_Pagos.xlsx", path + "\\ORCT_Pagos.xlsx", true);
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\RCT2_Pagos.xlsx", path + "\\RCT2_Pagos.xlsx", true);
                        int lastRow = 0;

                        Excel.Application MyApp = null;
                        Excel.Worksheet MySheet = null;

                        MyApp = new Excel.Application();
                        MyApp.Visible = false;
                        MyBook = MyApp.Workbooks.Open(path + "\\ORCT_Pagos.xlsx");
                        MySheet = (Excel.Worksheet)MyBook.Sheets[1]; // Explicit cast is not required here
                        lastRow = 3;

                        DataTable Datos = dataGridView1.DataSource as DataTable;

                        toolStatus.Text = "Generando: ORCT_Pagos.xlsx";

                        progressBar.Maximum = Datos.Rows.Count;
                        progressBar.Value = 0;

                        foreach (DataRow item in Datos.Rows)
                        {
                            MySheet.Cells[lastRow, 1] = item.Field<Int64>("Folio");
                            MySheet.Cells[lastRow, 2] = "rCustomer";
                            MySheet.Cells[lastRow, 3] = item.Field<DateTime>("Fecha movimiento").ToString("yyyyMMdd");
                            MySheet.Cells[lastRow, 4] = item.Field<string>("Cliente");
                            MySheet.Cells[lastRow, 5] = item.Field<string>("Moneda");
                            MySheet.Cells[lastRow, 6] = item.Field<string>("Cuenta contable");
                            MySheet.Cells[lastRow, 7] = item.Field<decimal>("U_TotalPago");
                            MySheet.Cells[lastRow, 8] = item.Field<DateTime>("Fecha movimiento").ToString("yyyyMMdd");
                            

                            int series = 0;
                            switch (item.Field<string>("Cuenta contable"))
                            {
                                case "1130-004-000": series = (int)SeriesPagos.PRPUE; break;
                                case "1130-005-000": series = (int)SeriesPagos.PRMTY; break;
                                case "1130-006-000": series = (int)SeriesPagos.PRMEX; break;
                                case "1130-007-000": series = (int)SeriesPagos.PRAPZ; break;
                                case "1130-008-000": series = (int)SeriesPagos.PRCOR; break;
                                case "1130-009-000": series = (int)SeriesPagos.PRTEP; break;
                                case "1130-010-000": series = (int)SeriesPagos.PRGDL; break;
                                case "1130-018-000": series = (int)SeriesPagos.PRSAL; break;
                            }
                            MySheet.Cells[lastRow, 10] = series;
                            MySheet.Cells[lastRow, 11] = item.Field<string>("FormaPagoDs");
                            MySheet.Cells[lastRow, 12] = "'" + item.Field<string>("FormaPago");
                            MySheet.Cells[lastRow, 13] = item.Field<string>("Moneda") == "USD" ? item.Field<decimal>("TipoCambio").ToString():string.Empty; 
                            lastRow++;
                            progressBar.PerformStep();
                        }

                        MyBook.Save();
                        MyBook.Close();

                       // this.Detalle();

                        Excel.Application MyAppRCT2 = null;
                        Excel.Worksheet MySheetRCT2 = null;

                        MyAppRCT2 = new Excel.Application();
                        MyAppRCT2.Visible = false;
                        MyBookRCT2 = MyAppRCT2.Workbooks.Open(path + "\\RCT2_Pagos.xlsx");
                        MySheetRCT2 = (Excel.Worksheet)MyBookRCT2.Sheets[1]; // Explicit cast is not required here
                        lastRow = 3;

                        toolStatus.Text = "Generando: RCT2_Pagos.xlsx";

                        progressBar.Maximum = dataGridView1.Rows.Count;
                        progressBar.Value = 0;

                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            int Code = 0;
                            string Cliente = string.Empty;


                            Code = Convert.ToInt32(item.Cells[(int)Columnas.Code].Value);
                            Cliente = Convert.ToString(item.Cells[(int)Columnas.Cliente].Value);

                            using (SqlConnection connection = new SqlConnection())
                            {
                                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                                using (SqlCommand command = new SqlCommand())
                                {
                                    command.Connection = connection;
                                    command.CommandText = "PJ_Pagos";
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.AddWithValue("@TipoConsulta", 9);
                                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                                    command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                                    command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                                    command.Parameters.AddWithValue("@Banco", string.Empty);
                                    command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                                    command.Parameters.AddWithValue("@Abono", decimal.Zero);
                                    command.Parameters.AddWithValue("@Referencia", string.Empty);
                                    command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                                    command.Parameters.AddWithValue("@Cliente", Cliente);
                                    command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                                    command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                                    command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                                    command.Parameters.AddWithValue("@Code", Code);

                                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                                    parameter.Direction = ParameterDirection.Output;
                                    command.Parameters.Add(parameter);

                                    DataTable table = new DataTable();
                                    SqlDataAdapter adapter = new SqlDataAdapter();
                                    adapter.SelectCommand = command;
                                    adapter.Fill(table);

                                    dataGridView2.DataSource = table;
                                    foreach (DataRow row in table.Rows)
                                    {
                                        MySheetRCT2.Cells[lastRow, 1] = Convert.ToInt32(item.Cells[(int)Columnas.Folio].Value);
                                        MySheetRCT2.Cells[lastRow, 2] = row.Field<Int32>("Folio");
                                        MySheetRCT2.Cells[lastRow, 3] = row.Field<string>("MXN");
                                        MySheetRCT2.Cells[lastRow, 4] = row.Field<string>("USD");
                                        MySheetRCT2.Cells[lastRow, 5] = "it_Invoice";
                                        MySheetRCT2.Cells[lastRow, 6] = row.Field<string>("TipoPago"); 
                                        lastRow++;
                                        progressBar.PerformStep();
                                    }
                                }
                            }
                            this.ActualizarTempleate(Convert.ToInt32(item.Cells[(int)Columnas.Code].Value));
                        }
                        

                        MyBookRCT2.Save();
                        MyBookRCT2.Close();


                        toolStatus.Text = "Listo.";
                        progressBar.Value = 0;

                        MessageBox.Show("El template se genero exitosamente", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = null;
                        dataGridView2.DataSource = null;
                        button1_Click(sender, e);

                    }
                }
            }
            catch (Exception ex)
            {
                toolStatus.Text = "";
                progressBar.Value = 0;
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            finally
            {
                // MyBook.Close();
                //  MyBookRCT2.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TempletePagos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();

            }
            catch (Exception)
            {
              
            }

        }

        private void TempletePagos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Code = 0;
            string Cliente = string.Empty;
            try
            {
                if (e.ColumnIndex != (sender as  DataGridView).Columns["Desconciliar"].Index)
                {
                    Code = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Code"].Value);
                    Cliente = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[(int)Columnas.Cliente].Value);

                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "PJ_Pagos";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 9);
                            command.Parameters.AddWithValue("@Sucursal", string.Empty);
                            command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                            command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                            command.Parameters.AddWithValue("@Banco", string.Empty);
                            command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                            command.Parameters.AddWithValue("@Abono", decimal.Zero);
                            command.Parameters.AddWithValue("@Referencia", string.Empty);
                            command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                            command.Parameters.AddWithValue("@Cliente", Cliente);
                            command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                            command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                            command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                            command.Parameters.AddWithValue("@Code", Code);

                            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                            parameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter);

                            DataTable table = new DataTable();
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = command;
                            adapter.Fill(table);

                            dataGridView2.DataSource = table;
                        }
                    }
                }
                else
                {
                    Code = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Code"].Value);

                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                        using (SqlCommand command = new SqlCommand())
                        {
                            connection.Open();

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

                            command.Parameters.AddWithValue("@Cliente", Cliente);
                            command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                            command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                            command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                            command.Parameters.AddWithValue("@Code", Code);

                            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                            parameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter);

                            command.ExecuteNonQuery();

                            MessageBox.Show("El movimiento ha sido eliminado.", "Informaión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            button1_Click(sender, e);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
