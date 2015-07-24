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
    public partial class frmTempleteNI : Form
    {
        DataTable TblDetalle = new DataTable();
        Clases.Logs log;
        bool Faltantes = true;

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

        public frmTempleteNI()
        {
            InitializeComponent();
            this.CargarCtasBancos();
        }

        public frmTempleteNI(bool _faltantes)
        {
            InitializeComponent();
            Faltantes = _faltantes;
            this.CargarCtasBancos();
        }

        public enum Columnas
        {
            Folio, Banco, Cuenta, FechaMov, Moneda, Abono, Referencia, CtaNI, Cliente
        }

        public void Formato()
        {
            //DataGridViewButtonColumn boton = new DataGridViewButtonColumn();
            //{
            //    boton.Name = "Eliminar mov";
            //    boton.HeaderText = "Eliminar mov";
            //    boton.Text = "Eliminar mov";
            //    boton.Width = 100;
            //    boton.UseColumnTextForButtonValue = true;
            //}
            //dgvMovimientos.Columns.Add(boton);

            dgvMovimientos.Columns[(int)Columnas.Abono].DefaultCellStyle.Format = "C4";

            dgvMovimientos.Columns[(int)Columnas.Folio].HeaderText = "No.";


            dgvMovimientos.Columns[(int)Columnas.Folio].ReadOnly = true;
            dgvMovimientos.Columns[(int)Columnas.Banco].ReadOnly = true;
            dgvMovimientos.Columns[(int)Columnas.Cuenta].ReadOnly = true;
            dgvMovimientos.Columns[(int)Columnas.FechaMov].ReadOnly = true;
            dgvMovimientos.Columns[(int)Columnas.Moneda].ReadOnly = true;
            dgvMovimientos.Columns[(int)Columnas.Abono].ReadOnly = true;
            dgvMovimientos.Columns[(int)Columnas.Referencia].ReadOnly = true;
            dgvMovimientos.Columns[(int)Columnas.CtaNI].ReadOnly = Faltantes;
            dgvMovimientos.Columns[(int)Columnas.Cliente].ReadOnly = Faltantes;


            dgvMovimientos.Columns[(int)Columnas.Abono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
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
            string stringquery = "Update [@MovimientosBancarios] Set U_Template = 'Y' Where Code = @Folio";
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

        public void ActualizarTempleate(int Folio, string cta, string cte)
        {
            string stringquery = @"Update [@MovimientosBancarios] 
                                   Set U_Template = 'Y'--,
                                       --U_Cliente = @Cliente,
                                       --U_CtaContableNI = @Cuenta
                                   Where Code = @Folio";
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
                    //command.Parameters.AddWithValue("@Cuenta", cta);
                    //command.Parameters.AddWithValue("@Cliente", cte);

                    command.ExecuteNonQuery();
                }

            }
        }

        public void Template(object sender, EventArgs e)
        {
            Excel.Workbook MyBookRCT2 = null;
            Excel.Workbook MyBook = null;

            DataTable table = new DataTable();
            int SinRef = 0;

            //try
            //{
            //    table = dgvMovimientos.DataSource as DataTable;

            //    SinRef = (from item in table.AsEnumerable()
            //              where item.Field<string>("Cta Mov no identificados") == ""
            //              select item).Count();
            //}
            //catch (Exception)
            //{
            //}

            try
            {
                if (dgvMovimientos.Rows.Count > 0)// && SinRef == 0)
                {
                    FolderBrowserDialog f = new FolderBrowserDialog();
                    f.Description = "Elija una carpeta para guardar el template.";
                    f.ShowDialog();

                    string path = f.SelectedPath;
                    if (!string.IsNullOrEmpty(path))
                    {
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\ORCT_NI.xlsx", path + "\\ORCT_NI.xlsx", true);
                        System.IO.File.Copy(Application.StartupPath + "\\Resources\\RCT4_NI.xlsx", path + "\\RCT4_NI.xlsx", true);
                        int lastRow = 0;

                        Excel.Application MyApp = null;
                        Excel.Worksheet MySheet = null;

                        MyApp = new Excel.Application();
                        MyApp.Visible = false;
                        MyBook = MyApp.Workbooks.Open(path + "\\ORCT_NI.xlsx");
                        MySheet = (Excel.Worksheet)MyBook.Sheets[1]; // Explicit cast is not required here
                        lastRow = 3;

                        DataTable Datos = dgvMovimientos.DataSource as DataTable;

                        toolStatus.Text = "Generando: ORCT_NI.xlsx";
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
                                MySheet.Cells[lastRow, 3] = item.Field<DateTime>("Fecha movimiento").ToString("yyyyMMdd");
                                MySheet.Cells[lastRow, 4] = item.Field<string>("Cuenta contable");
                                MySheet.Cells[lastRow, 5] = item.Field<decimal>("Abono");
                                MySheet.Cells[lastRow, 6] = item.Field<DateTime>("Fecha movimiento").ToString("yyyyMMdd");
                                MySheet.Cells[lastRow, 7] = item.Field<string>("Cliente").Trim().TrimEnd(',');

                                string u_tipo_ingreso = string.Empty;
                                switch (item.Field<string>("U_TipoPago").Trim())
                                {
                                    case "CHEQUE": u_tipo_ingreso = "04"; break;
                                    case "TRANSFERENCIA": u_tipo_ingreso = "05"; break;
                                    case "Tarjeta de Credito / Debito": u_tipo_ingreso = "03"; break;
                                    case "EFECTIVO": u_tipo_ingreso = "02"; break;
                                }
                                MySheet.Cells[lastRow, 8] = u_tipo_ingreso;

                                int series = 0;
                                switch (item.Field<string>("Cta Mov no identificados"))
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
                                MySheet.Cells[lastRow, 9] = series;

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
                        MyBookRCT2 = MyAppRCT2.Workbooks.Open(path + "\\RCT4_NI.xlsx");
                        MySheetRCT2 = (Excel.Worksheet)MyBookRCT2.Sheets[1]; // Explicit cast is not required here
                        lastRow = 3;

                        toolStatus.Text = "Generando: RCT4_NI.xlsx";
                        progressBar.Maximum = (from item in Datos.AsEnumerable()
                                               where !string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados"))
                                               select item).Count();
                        progressBar.Value = 0;

                        foreach (DataRow item in Datos.Rows)
                        {
                            if (!string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados")))
                            {
                                MySheetRCT2.Cells[lastRow, 1] = item.Field<Int32>("Folio");
                                MySheetRCT2.Cells[lastRow, 2] = item.Field<string>("Cta Mov no identificados");

                                if (item.Field<string>("Moneda").Equals("MXN"))
                                {
                                    MySheetRCT2.Cells[lastRow, 3] = (item.Field<decimal>("Abono")).ToString();
                                    MySheetRCT2.Cells[lastRow, 4] = string.Empty;
                                }
                                else
                                    if (item.Field<string>("Moneda").Equals("USD"))
                                    {
                                        MySheetRCT2.Cells[lastRow, 3] = string.Empty;
                                        MySheetRCT2.Cells[lastRow, 4] = item.Field<decimal>("Abono").ToString();
                                    }

                                MySheetRCT2.Cells[lastRow, 5] = item.Field<string>("Referencia");
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

                        toolStatus.Text = "Listo.";
                        progressBar.Value = 0;

                        MessageBox.Show("El template se genero exitosamente", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        button1_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo generar el template. \r\nPara poder continuar todos los clientes deben tener asignara una referencia en SAP.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvMovimientos.Sort(dgvMovimientos.Columns[(int)Columnas.Referencia], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                // MyBook.Close();
                //  MyBookRCT2.Close();
            }
        }

        public void TemplateFaltantes(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            int SinRef = 0;

            try
            {
                table = dgvMovimientos.DataSource as DataTable;
                table.Columns.Add("Correcto", typeof(string));

                #region Valida Cuenta contable
                foreach (DataRow item in table.Rows)
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "PJ_Pagos";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 20);
                            command.Parameters.AddWithValue("@Sucursal", string.Empty);
                            command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                            command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                            command.Parameters.AddWithValue("@Banco", string.Empty);
                            command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                            command.Parameters.AddWithValue("@Abono", decimal.Zero);
                            command.Parameters.AddWithValue("@Referencia", string.Empty);
                            command.Parameters.AddWithValue("@CuentaNI", item.Field<string>("Cta Mov no identificados") == null ? string.Empty : item.Field<string>("Cta Mov no identificados"));

                            command.Parameters.AddWithValue("@Cliente", item.Field<string>("Cliente").TrimEnd(',') == null ? string.Empty : item.Field<string>("Cliente").TrimEnd(','));
                            command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                            command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                            command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                            command.Parameters.AddWithValue("@Code", 0);

                            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                            parameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter);

                            DataTable tt = new DataTable();
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = command;
                            adapter.Fill(tt);

                            item.SetField("Correcto", parameter.SqlValue.ToString());

                            if (item.Field<string>("Correcto") != string.Empty)
                            {
                                item.SetField("Cliente", string.Empty);
                                item.SetField("Cta Mov no identificados", string.Empty);
                            }
                        }
                    }
                }

                SinRef = (from item in table.AsEnumerable()
                          where item.Field<string>("Cta Mov no identificados") == ""
                          select item).Count();

                #endregion

                if (dgvMovimientos.Rows.Count > 0)
                {
                    FolderBrowserDialog f = new FolderBrowserDialog();
                    f.Description = "Elija una carpeta para guardar el template.";
                    f.ShowDialog();

                    string path = f.SelectedPath;
                    if (!string.IsNullOrEmpty(path))
                    {
                        string[] encabezado;
                        int lastRow = 0;
                        encabezado = new string[dgvMovimientos.Rows.Count + 2];

                        encabezado[0] += "DocNum\tDocType\tDocDate\tTransferAccount\tTransferSum\tTransferDate\tJournalRemarks\tU_TipoIngreso\tSeries";
                        encabezado[1] += "DocNum\tDocType\tDocDate\tTrsfrAcct\tTrsfrSum\tTrsfrDate\tJrnlMemo\tU_TipoIngreso\tSeries";
                        lastRow = 2;

                        DataTable Datos = dgvMovimientos.DataSource as DataTable;

                        toolStatus.Text = "Generando: ORCT_NI.xlsx";
                        progressBar.Maximum = (from item in Datos.AsEnumerable()
                                               where !string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados"))
                                               select item).Count();
                        progressBar.Value = 0;

                        foreach (DataRow item in Datos.Rows)
                        {
                                encabezado[lastRow] += item.Field<Int32>("Folio") + "\t";

                                encabezado[lastRow] += "rAccount\t";

                                encabezado[lastRow] += item.Field<DateTime>("Fecha movimiento").ToString("yyyyMMdd") + "\t";

                                encabezado[lastRow] += item.Field<string>("Cuenta contable") + "\t";

                                encabezado[lastRow] += item.Field<decimal>("Abono") + "\t";

                                encabezado[lastRow] += item.Field<DateTime>("Fecha movimiento").ToString("yyyyMMdd") + "\t";

                                encabezado[lastRow] += item.Field<string>("Cliente") == null ? string.Empty : item.Field<string>("Cliente").Trim().TrimEnd(',') + "\t";

                                string u_tipo_ingreso = string.Empty;
                                switch (item.Field<string>("U_TipoPago").Trim())
                                {
                                    case "CHEQUE": u_tipo_ingreso = "04"; break;
                                    case "TRANSFERENCIA": u_tipo_ingreso = "05"; break;
                                    case "Tarjeta de Credito / Debito": u_tipo_ingreso = "03"; break;
                                    case "EFECTIVO": u_tipo_ingreso = "02"; break;
                                }

                                encabezado[lastRow] += u_tipo_ingreso + "\t";
                                
                                int series = 0;
                                series = (int)SeriesPagos.PRPUE;
                                switch (item.Field<string>("Cta Mov no identificados"))
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

                                encabezado[lastRow] += series.ToString();

                                lastRow++;
                                progressBar.PerformStep();
                        }
                        File.WriteAllLines(path + "\\ORCT_NI.txt", encabezado, Encoding.UTF8);

                        string[] detalle;
                        detalle = new string[Datos.Rows.Count + 2];
                        detalle[0] = "ParentKey\tAccountCode\tSumPaid\tSumPaidFC\tDecription";
                        detalle[1] = "DocNum\tAcctCode\tSumApplied\tAppliedFC\tDescrip";
                        lastRow = 2;

                        toolStatus.Text = "Generando: RCT4_NI.xlsx";
                        progressBar.Maximum = (from item in Datos.AsEnumerable()
                                               where !string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados"))
                                               select item).Count();
                        progressBar.Value = 0;

                        foreach (DataRow item in Datos.Rows)
                        {
                            detalle[lastRow] += item.Field<Int32>("Folio") + "\t";
                            if (!string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados")))
                                detalle[lastRow] += item.Field<string>("Cta Mov no identificados") + "\t";
                            else
                                detalle[lastRow] += "1130-004-000\t";

                            if (item.Field<string>("Moneda").Equals("MXN"))
                            {
                                detalle[lastRow] += (item.Field<decimal>("Abono")).ToString() + "\t";
                                detalle[lastRow] += string.Empty + "\t";
                            }
                            else
                                if (item.Field<string>("Moneda").Equals("USD"))
                                {
                                    detalle[lastRow] += string.Empty + "\t";
                                    detalle[lastRow] += item.Field<decimal>("Abono").ToString() + "\t";
                                }
                            detalle[lastRow] += item.Field<string>("Referencia");
                            lastRow++;
                            progressBar.PerformStep();
                        }
                        File.WriteAllLines(path + "\\RCT4_NI.txt", detalle, Encoding.UTF8);

                        toolStatus.Text = "Actualizando registros.";
                        progressBar.Maximum = (from item in Datos.AsEnumerable()
                                               where !string.IsNullOrEmpty(item.Field<string>("Cta Mov no identificados"))
                                               select item).Count();
                        progressBar.Value = 0;

                        foreach (DataRow item in Datos.Rows)
                        {
                            this.ActualizarTempleate(item.Field<Int32>("Folio"), item.Field<string>("Cta Mov no identificados"), item.Field<string>("Cliente") == null ? string.Empty : item.Field<string>("Cliente").Trim().TrimEnd(',').ToUpper());
                            progressBar.PerformStep();
                        }

                        toolStatus.Text = "Listo.";
                        progressBar.Value = 0;

                        MessageBox.Show("El template se genero exitosamente", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        button1_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo generar el template. \r\nPara poder continuar todos los clientes deben tener asignara una referencia en SAP.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvMovimientos.Sort(dgvMovimientos.Columns[(int)Columnas.Referencia], System.ComponentModel.ListSortDirection.Ascending);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TblDetalle.Clear();
                dgvMovimientos.DataSource = null;
                dgvMovimientos.Columns.Clear();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "PJ_Pagos";
                        command.CommandType = CommandType.StoredProcedure;

                        //if (Faltantes)
                        //    command.Parameters.AddWithValue("@TipoConsulta", 3);
                        //else
                            command.Parameters.AddWithValue("@TipoConsulta", 19);
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


                        dgvMovimientos.DataSource = table;
                       // this.Formato();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TempletePagos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                progressBar.Step = 1;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
            //if (Faltantes)
            //{
            //    this.Template(sender, e);
            //}
            //else
            //{
                this.TemplateFaltantes(sender, e);
            //}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int Code = 0;
            //try
            //{
            //    if (e.ColumnIndex != (int)Columnas.Boton)
            //    {
            //        Code = Convert.ToInt32(dgvMovimientos.Rows[e.RowIndex].Cells[(int)Columnas.Folio].Value);

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

            //                command.Parameters.AddWithValue("@NCPP", decimal.Zero);
            //                command.Parameters.AddWithValue("@NCPE", decimal.Zero);

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
            //        Code = Convert.ToInt32(dgvMovimientos.Rows[e.RowIndex].Cells[(int)Columnas.Folio].Value);

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

            //                command.Parameters.AddWithValue("@NCPP", decimal.Zero);
            //                command.Parameters.AddWithValue("@NCPE", decimal.Zero);

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
                if (Faltantes)
                {
                    this.button1_Click(sender, e);
                    this.button2_Click(sender, e);
                }
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

        private void dgvMovimientos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dgvMovimientos.Rows)
                {
                    if (item.Cells[(int)Columnas.Referencia].Value.ToString().Trim() == "")
                    {
                       item.Cells[(int)Columnas.Referencia].Style.BackColor = Color.Red;
                       item.Cells[(int)Columnas.Referencia].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
