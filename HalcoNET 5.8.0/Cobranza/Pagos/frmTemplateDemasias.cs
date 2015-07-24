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
    public partial class frmTemplateDemasias : Form
    {
        DataTable TblDetalle = new DataTable();
        Clases.Logs log;
        public frmTemplateDemasias()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Folio, Cuenta, Cliente, Fecha, Moneda, Monto, SumAplied, AppliedFC
        }

        public void Formato()
        {
            dataGridView1.Columns[(int)Columnas.Folio].HeaderText = "No.";
            dataGridView1.Columns[(int)Columnas.Monto].HeaderText = "Monto de demasía";
            dataGridView1.Columns[(int)Columnas.SumAplied].Visible = false;
            dataGridView1.Columns[(int)Columnas.AppliedFC].Visible = false;

            dataGridView1.Columns[(int)Columnas.Monto].DefaultCellStyle.Format = "C2";
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

                    command.Parameters.AddWithValue("@TipoConsulta", 7);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                    command.Parameters.AddWithValue("@Banco", string.Empty);
                    command.Parameters.AddWithValue("@CuentaContable", cbBanco.SelectedValue);
                    command.Parameters.AddWithValue("@Cargo", decimal.Zero);
                    command.Parameters.AddWithValue("@Abono", decimal.Zero);
                    command.Parameters.AddWithValue("@Referencia", string.Empty);
                    command.Parameters.AddWithValue("@Comentarios", string.Empty);

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
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(TblDetalle);

                }
            }
        }

        public void ActualizarTempleate(int Folio)
        {
            string stringquery = "Update [@Demasias] Set U_Template = 'Y' Where Code = @Folio";
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = stringquery;
                    command.Connection = connection;

                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Folio", Folio);

                    command.ExecuteNonQuery();
                }
                
            }
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

                        command.Parameters.AddWithValue("@TipoConsulta", 13);
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
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\ORCT_Demasias.xlsx", path + "\\ORCT_Demasias.xlsx", true);
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\RCT4_Demasias.xlsx", path + "\\RCT4_Demasias.xlsx", true);
                        int lastRow = 0;

                        Excel.Application MyApp = null;
                        Excel.Worksheet MySheet = null;

                        MyApp = new Excel.Application();
                        MyApp.Visible = false;
                        MyBook = MyApp.Workbooks.Open(path + "\\ORCT_Demasias.xlsx");
                        MySheet = (Excel.Worksheet)MyBook.Sheets[1]; // Explicit cast is not required here
                        lastRow = 3;

                        DataTable Datos = dataGridView1.DataSource as DataTable;

                        toolStatus.Text = "Generando: ORCT_Demasias.xlsx";
                        progressBar.Maximum = (from item in Datos.AsEnumerable()
                                               where !string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados"))
                                               select item).Count();
                        progressBar.Value = 0;
                        foreach (DataRow item in Datos.Rows)
                        {
                            if (!string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados")))
                            {
                                MySheet.Cells[lastRow, 1] = item.Field<Int32>("Folio");
                                MySheet.Cells[lastRow, 2] = "rAccount";
                                MySheet.Cells[lastRow, 3] = item.Field<DateTime>("Fecha").ToString("yyyyMMdd");
                                MySheet.Cells[lastRow, 4] = item.Field<string>("Cta Mov no identificados");
                                MySheet.Cells[lastRow, 5] = item.Field<decimal>("Monto").ToString();
                                MySheet.Cells[lastRow, 6] = item.Field<DateTime>("Fecha").ToString("yyyyMMdd");
                                MySheet.Cells[lastRow, 7] = "Demasía - " + item.Field<string>("Cliente").Trim().TrimEnd(',');
                                lastRow++;

                                progressBar.PerformStep();
                            }
                        }

                        MyBook.Save();
                        MyBook.Close();

                        //this.Detalle();


                        Excel.Application MyAppRCT2 = null;
                        Excel.Worksheet MySheetRCT2 = null;

                        MyAppRCT2 = new Excel.Application();
                        MyAppRCT2.Visible = false;
                        MyBookRCT2 = MyAppRCT2.Workbooks.Open(path + "\\RCT4_Demasias.xlsx");
                        MySheetRCT2 = (Excel.Worksheet)MyBookRCT2.Sheets[1]; // Explicit cast is not required here
                        lastRow = 3;

                        toolStatus.Text = "Generando: RCT4_Demasias.xlsx";
                        progressBar.Maximum = (from item in Datos.AsEnumerable()
                                               where !string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados"))
                                               select item).Count();
                        progressBar.Value = 0;
                        foreach (DataRow item in Datos.Rows)
                        {
                            if (!string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados")))
                            {
                                MySheetRCT2.Cells[lastRow, 1] = item.Field<Int32>("Folio");
                                MySheetRCT2.Cells[lastRow, 2] = "1130-003-000";
                                MySheetRCT2.Cells[lastRow, 3] = (item.Field<string>("SumApplied")).ToString();
                                MySheetRCT2.Cells[lastRow, 4] = (item.Field<string>("AppliedFC")).ToString();
                                MySheetRCT2.Cells[lastRow, 5] = "Demasía - " + item.Field<string>("Cliente").Trim().TrimEnd(',');
                                lastRow++;
                                progressBar.PerformStep();
                            }
                            //   lineNum++;
                        }

                        MyBookRCT2.Save();
                        MyBookRCT2.Close();

                        toolStatus.Text = "Actualizando registros.";
                        progressBar.Maximum = (from item in Datos.AsEnumerable()
                                               where !string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados"))
                                               select item).Count();
                        progressBar.Value = 0;
                        foreach (DataRow item in Datos.Rows)
                        {
                            if (!string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados")))
                            {
                                this.ActualizarTempleate(item.Field<Int32>("Folio"));
                                progressBar.PerformStep();
                            }
                        }
                        progressBar.Value = 0;
                        toolStatus.Text = "Listo.";
                        MessageBox.Show("El template se genero exitosamente", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        button1_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStatus.Text = "";
                progressBar.Value = 0;
            }
            finally
            {
              // MyBook.Close();
              //  MyBookRCT2.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int Code = 0;
            //try
            //{
            //    if (e.ColumnIndex != (int)Columnas.Boton)
            //    {
            //        Code = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[(int)Columnas.Folio].Value);

            //        using (SqlConnection connection = new SqlConnection())
            //        {
            //            connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

            //            using (SqlCommand command = new SqlCommand())
            //            {
            //                command.Connection = connection;
            //                command.CommandText = "PJ_Pagos";
            //                command.CommandType = CommandType.StoredProcedure;

            //                command.Parameters.AddWithValue("@TipoConsulta", 8);
            //                command.Parameters.AddWithValue("@Sucursal", string.Empty);
            //                command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
            //                command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

            //                command.Parameters.AddWithValue("@Banco", string.Empty);
            //                command.Parameters.AddWithValue("@CuentaContable", string.Empty);
            //                command.Parameters.AddWithValue("@Cargo", decimal.Zero);
            //                command.Parameters.AddWithValue("@Abono", decimal.Zero);
            //                command.Parameters.AddWithValue("@Referencia", string.Empty);
            //                command.Parameters.AddWithValue("@Comentarios", string.Empty);

            //                command.Parameters.AddWithValue("@Cliente", string.Empty);
            //                command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

            //                command.Parameters.AddWithValue("@Code", Code);

            //                SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
            //                parameter.Direction = ParameterDirection.Output;
            //                command.Parameters.Add(parameter);

            //                DataTable table = new DataTable();
            //                SqlDataAdapter adapter = new SqlDataAdapter();
            //                adapter.SelectCommand = command;
            //                adapter.Fill(table);

            //                //dataGridView2.DataSource = table;
            //            }
            //        }
            //    }
            //}
            //catch (Exception)
            //{

            //}
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //int Code = 0;
            //try
            //{
            //    if (e.ColumnIndex == (int)Columnas.Boton)
            //    {
            //        Code = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[(int)Columnas.Folio].Value);

            //        using (SqlConnection connection = new SqlConnection())
            //        {
            //            connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

            //            using (SqlCommand command = new SqlCommand())
            //            {
            //                connection.Open();

            //                command.Connection = connection;
            //                command.CommandText = "PJ_Pagos";
            //                command.CommandType = CommandType.StoredProcedure;

            //                command.Parameters.AddWithValue("@TipoConsulta", 9);
            //                command.Parameters.AddWithValue("@Sucursal", string.Empty);
            //                command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
            //                command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

            //                command.Parameters.AddWithValue("@Banco", string.Empty);
            //                command.Parameters.AddWithValue("@CuentaContable", string.Empty);
            //                command.Parameters.AddWithValue("@Cargo", decimal.Zero);
            //                command.Parameters.AddWithValue("@Abono", decimal.Zero);
            //                command.Parameters.AddWithValue("@Referencia", string.Empty);
            //                command.Parameters.AddWithValue("@Comentarios", string.Empty);

            //                command.Parameters.AddWithValue("@Cliente", string.Empty);
            //                command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

            //                command.Parameters.AddWithValue("@Code", Code);

            //                SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
            //                parameter.Direction = ParameterDirection.Output;
            //                command.Parameters.Add(parameter);

            //                command.ExecuteNonQuery();

            //                MessageBox.Show("El movimiento ha sido eliminado.", "Informaión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                button1_Click(sender, e);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
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
    }
}
