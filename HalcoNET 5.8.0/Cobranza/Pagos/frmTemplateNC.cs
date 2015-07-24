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
    public partial class frmTemplateNC : Form
    {
        DataTable TblDetalle = new DataTable();
        Clases.Logs log;
        public frmTemplateNC()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Code, Folio, Fecha, Cliente, GroupCode, Sucursal, AcctCode, Factura, Monto, MXN, USD, Moneda, Tipo
        }

        public enum SeriesNC
        {
            NCEPUE = 129,
            NCEAPZ = 130,
            NCECOR = 132,
            NCEMEX = 133,
            NCEGDL = 134,
            NCEMTY = 136,
            NCETEP = 137,
            NCESAL = 156
        }

        public void Formato()
        {
            dgvNC.Columns[(int)Columnas.Folio].HeaderText = "No.";
            dgvNC.Columns[(int)Columnas.Folio].Width = 70;
            dgvNC.Columns[(int)Columnas.Fecha].Width = 100;
            dgvNC.Columns[(int)Columnas.Cliente].Width = 100;
            dgvNC.Columns[(int)Columnas.Monto].Width = 100;
            dgvNC.Columns[(int)Columnas.MXN].Visible = false;
            dgvNC.Columns[(int)Columnas.Code].Visible = false;
            dgvNC.Columns[(int)Columnas.USD].Visible = false; 
            dgvNC.Columns[(int)Columnas.GroupCode].Visible = false;
            dgvNC.Columns[(int)Columnas.Moneda].Width = 100;

            dgvNC.Columns[(int)Columnas.Monto].DefaultCellStyle.Format = "C2";
        }

        public void CargarCtasBancos()
        {
            SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.CommandTimeout = 0;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "--";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            cbBanco.DataSource = table;
            cbBanco.ValueMember = "Codigo";
            cbBanco.DisplayMember = "Nombre";
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

        public void ActualizarTempleate(int Folio, string tipo)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 18);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                    command.Parameters.AddWithValue("@Banco", string.Empty);
                    command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                    command.Parameters.AddWithValue("@Abono", decimal.Zero);
                    command.Parameters.AddWithValue("@Referencia", string.Empty);
                    command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                    command.Parameters.AddWithValue("@Cliente", tipo);
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TblDetalle.Clear();
                dgvNC.DataSource = null;
                dgvNC.Columns.Clear();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "PJ_Pagos";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 14);
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

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(table);


                        dgvNC.DataSource = table;
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
                if (dgvNC.Rows.Count > 0)
                {
                    FolderBrowserDialog f = new FolderBrowserDialog();
                    f.Description = "Elija una carpeta para guardar el template.";
                    f.ShowDialog();

                    string path = f.SelectedPath;
                    if (!string.IsNullOrEmpty(path))
                    {
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\ORIN_Notas_credito.xlsx", path + "\\ORIN_Notas_credito.xlsx", true);
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\RIN1_Notas_credito.xlsx", path + "\\RIN1_Notas_credito.xlsx", true);
                        int lastRow = 0;

                        Excel.Application MyApp = null;
                        Excel.Worksheet MySheet = null;

                        MyApp = new Excel.Application();
                        MyApp.Visible = false;
                        MyBook = MyApp.Workbooks.Open(path + "\\ORIN_Notas_credito.xlsx");
                        MySheet = (Excel.Worksheet)MyBook.Sheets[1]; // Explicit cast is not required here
                        lastRow = 3;

                        DataTable Datos = dgvNC.DataSource as DataTable;

                        toolStatus.Text = "Generando: ORIN_Notas_credito.xlsx";
                        progressBar.Maximum = (from item in Datos.AsEnumerable()
                                               // where !string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados"))
                                               select item).Count();
                        progressBar.Value = 0;
                        foreach (DataRow item in Datos.Rows)
                        {
                            int Series = 0;
                            switch (item.Field<Int16>("GroupCode"))
                            {
                                case 107: Series = (int)SeriesNC.NCEPUE; break;
                                case 105: Series = (int)SeriesNC.NCEMTY; break;
                                case 106: Series = (int)SeriesNC.NCEMTY; break;
                                case 100: Series = (int)SeriesNC.NCEAPZ; break;
                                case 102: Series = (int)SeriesNC.NCECOR; break;
                                case 108: Series = (int)SeriesNC.NCETEP; break;
                                case 103: Series = (int)SeriesNC.NCEMEX; break;
                                case 104: Series = (int)SeriesNC.NCEGDL; break;
                                case 121: Series = (int)SeriesNC.NCESAL; break;

                            }

                            MySheet.Cells[lastRow, 1] = item.Field<Int64>("Folio");
                            MySheet.Cells[lastRow, 2] = "dDocument_Service";
                            MySheet.Cells[lastRow, 3] = item.Field<DateTime>("Fecha").ToString("yyyyMMdd");
                            MySheet.Cells[lastRow, 4] = item.Field<string>("Cliente");
                            MySheet.Cells[lastRow, 5] = item.Field<string>("DocTotal");
                            MySheet.Cells[lastRow, 6] = item.Field<string>("DocTotalFC");
                            MySheet.Cells[lastRow, 7] = Series;
                            MySheet.Cells[lastRow, 8] = item.Field<string>("Moneda");
                            MySheet.Cells[lastRow, 9] = item.Field<string>("Tipo");
                            MySheet.Cells[lastRow, 10] = "L";
                            MySheet.Cells[lastRow, 11] = item.Field<Int32>("Factura");
                            lastRow++;

                            progressBar.PerformStep();

                        }

                        MyBook.Save();
                        MyBook.Close();

                        Excel.Application MyAppRCT2 = null;
                        Excel.Worksheet MySheetRCT2 = null;

                        MyAppRCT2 = new Excel.Application();
                        MyAppRCT2.Visible = false;
                        MyBookRCT2 = MyAppRCT2.Workbooks.Open(path + "\\RIN1_Notas_credito.xlsx");
                        MySheetRCT2 = (Excel.Worksheet)MyBookRCT2.Sheets[1]; // Explicit cast is not required here
                        lastRow = 3;

                        toolStatus.Text = "Generando: RIN1_Notas_credito.xlsx";

                        progressBar.Maximum = dgvNC.Rows.Count;
                        progressBar.Value = 0;

                        foreach (DataRow item in Datos.Rows)
                        {

                            MySheetRCT2.Cells[lastRow, 1] = item.Field<Int64>("Folio");
                            MySheetRCT2.Cells[lastRow, 2] = "NC " + item.Field<Int32>("Factura");
                           // MySheetRCT2.Cells[lastRow, 3] = "116";
                            MySheetRCT2.Cells[lastRow, 3] = decimal.Round(item.Field<decimal>("Monto NC"), 2);
                            MySheetRCT2.Cells[lastRow, 4] = item.Field<Int32>("Factura");
                            MySheetRCT2.Cells[lastRow, 5] = item.Field<string>("AcctCode");
                            lastRow++;
                            this.ActualizarTempleate(item.Field<Int32>("Code"), item.Field<string>("Tipo"));
                            progressBar.PerformStep();

                        }
                        //foreach (DataGridViewRow item in dgvNC.Rows)
                        //{
                        //    int Code = 0;
                        //    string Cliente = string.Empty;


                        //    Code = Convert.ToInt32(item.Cells[(int)Columnas.Code].Value);
                        //   // Cliente = Convert.ToString(item.Cells[(int)Columnas.Cliente].Value);

                        //    using (SqlConnection connection = new SqlConnection())
                        //    {
                        //        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                        //        using (SqlCommand command = new SqlCommand())
                        //        {
                        //            command.Connection = connection;
                        //            command.CommandText = "PJ_Pagos";
                        //            command.CommandType = CommandType.StoredProcedure;

                        //            command.Parameters.AddWithValue("@TipoConsulta", 15);
                        //            command.Parameters.AddWithValue("@Sucursal", string.Empty);
                        //            command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                        //            command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                        //            command.Parameters.AddWithValue("@Banco", string.Empty);
                        //            command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                        //            command.Parameters.AddWithValue("@Abono", decimal.Zero);
                        //            command.Parameters.AddWithValue("@Referencia", string.Empty);
                        //            command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                        //            command.Parameters.AddWithValue("@Cliente", string.Empty);
                        //            command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                        //            command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                        //            command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                        //            command.Parameters.AddWithValue("@Code", Code);

                        //            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                        //            parameter.Direction = ParameterDirection.Output;
                        //            command.Parameters.Add(parameter);

                        //            DataTable table = new DataTable();
                        //            SqlDataAdapter adapter = new SqlDataAdapter();
                        //            adapter.SelectCommand = command;
                        //            adapter.Fill(table);

                        //            dgvFacturas.DataSource = table;
                        //            foreach (DataRow row in table.Rows)
                        //            {
                        //                MySheetRCT2.Cells[lastRow, 1] = Convert.ToInt32(item.Cells[(int)Columnas.Folio].Value);
                        //                MySheetRCT2.Cells[lastRow, 2] = "NC " + row.Field<Int32>("DocNum");
                        //                MySheetRCT2.Cells[lastRow, 3] = "116";
                        //                MySheetRCT2.Cells[lastRow, 4] = row.Field<Int32>("DocNum");
                        //                MySheetRCT2.Cells[lastRow, 5] = row.Field<string>("AcctCode");
                        //                lastRow++;
                        //                progressBar.PerformStep();
                        //            }
                        //        }
                        //    }

                        //}


                        MyBookRCT2.Save();
                        MyBookRCT2.Close();
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

        private void dgvNC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int Code = 0;
            //try
            //{

            //    Code = Convert.ToInt32(dgvNC.Rows[e.RowIndex].Cells[(int)Columnas.Folio].Value);

            //    using (SqlConnection connection = new SqlConnection())
            //    {
            //        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

            //        using (SqlCommand command = new SqlCommand())
            //        {
            //            connection.Open();

            //            command.Connection = connection;
            //            command.CommandText = "PJ_Pagos";
            //            command.CommandType = CommandType.StoredProcedure;

            //            command.Parameters.AddWithValue("@TipoConsulta", 15);
            //            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            //            command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
            //            command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

            //            command.Parameters.AddWithValue("@Banco", string.Empty);
            //            command.Parameters.AddWithValue("@CuentaContable", string.Empty);
            //            command.Parameters.AddWithValue("@Cargo", decimal.Zero);
            //            command.Parameters.AddWithValue("@Abono", decimal.Zero);
            //            command.Parameters.AddWithValue("@Referencia", string.Empty);
            //            command.Parameters.AddWithValue("@Comentarios", string.Empty);

            //            command.Parameters.AddWithValue("@Cliente", string.Empty);
            //            command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

            //            command.Parameters.AddWithValue("@NCPP", decimal.Zero);
            //            command.Parameters.AddWithValue("@NCPE", decimal.Zero);

            //            command.Parameters.AddWithValue("@Code", Code);

            //            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
            //            parameter.Direction = ParameterDirection.Output;
            //            command.Parameters.Add(parameter);

            //            command.ExecuteNonQuery();

            //            MessageBox.Show("El movimiento ha sido eliminado.", "Informaión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            button1_Click(sender, e);
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
