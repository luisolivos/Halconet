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


namespace Pagos
{
    public partial class TemplatePagosProv : Form
    {
        int DocEntry;
        string Tipo;
        Logs log;

        public enum Columnas
        {
            Num, Docentry, FolioSAP, Proveedor, Nombre, Moneda, Aprobado, FechaCont, FechaTrans, Cuenta, Tipo, DocLine
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Docentry].Visible = false;
            dgv.Columns[(int)Columnas.Moneda].Visible = false;
            dgv.Columns[(int)Columnas.Tipo].Visible = false;
            dgv.Columns[(int)Columnas.DocLine].Visible = false;
            dgv.Columns[(int)Columnas.Num].Visible = false;

            dgv.Columns[(int)Columnas.FolioSAP].Width = 60;
            dgv.Columns[(int)Columnas.Proveedor].Width = 60;
            dgv.Columns[(int)Columnas.Nombre].Width = 200;
            dgv.Columns[(int)Columnas.Aprobado].Width = 95;
            dgv.Columns[(int)Columnas.FechaCont].Width = 90;
            dgv.Columns[(int)Columnas.FechaTrans].Width = 90;
            dgv.Columns[(int)Columnas.Cuenta].Width = 90;

            dgv.Columns[(int)Columnas.Aprobado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight;
            dgv.Columns[(int)Columnas.Aprobado].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.FechaCont].DefaultCellStyle.Format = "d";
            dgv.Columns[(int)Columnas.FechaTrans].DefaultCellStyle.Format = "d";

            dgv.Columns[(int)Columnas.FolioSAP].HeaderText = "Folio SAP";
            dgv.Columns[(int)Columnas.Proveedor].HeaderText = "Proveedor";
            dgv.Columns[(int)Columnas.Nombre].HeaderText = "Nombre";
            dgv.Columns[(int)Columnas.Aprobado].HeaderText = "Aprobado";
            dgv.Columns[(int)Columnas.FechaCont].HeaderText = "Fecha de\r\ncontabilización";
            dgv.Columns[(int)Columnas.FechaTrans].HeaderText = "Fecha de\r\ntransferencia";
            dgv.Columns[(int)Columnas.Cuenta].HeaderText = "Cuenta\r\ncontable";
        }

        public TemplatePagosProv()
        {
            InitializeComponent();
        }

        public void CargarCuentas()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@Proveedores", string.Empty);
                    command.Parameters.AddWithValue("@GroupCode", 0);

                    command.Parameters.AddWithValue("@DocNum", 0);
                    command.Parameters.AddWithValue("@Comentario", string.Empty);
                    command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                    command.Parameters.AddWithValue("@Aprobado", decimal.Zero);

                    command.Parameters.AddWithValue("@Estatus", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", string.Empty);

                    command.Parameters.AddWithValue("@PropuestaUSD", decimal.Zero);
                    command.Parameters.AddWithValue("@AprobadoUSD", decimal.Zero);
                    command.Parameters.AddWithValue("@TC", decimal.Zero);

                    command.CommandTimeout = 0;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.SelectCommand.CommandTimeout = 0;

                    DataTable table = new DataTable();

                    da.Fill(table);

                    cbCtaContable.DataSource = table;
                    cbCtaContable.DisplayMember = "Nombre";
                    cbCtaContable.ValueMember = "Codigo";
                }
            }
        }

        public void EstatusTemplate(int _docEntry, string _tipo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 9);
                        command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Proveedores", string.Empty);
                        command.Parameters.AddWithValue("@GroupCode", 0);

                        command.Parameters.AddWithValue("@DocNum", _docEntry);
                        command.Parameters.AddWithValue("@Comentario", _tipo);
                        command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                        command.Parameters.AddWithValue("@Aprobado", decimal.Zero);

                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Usuario", string.Empty);

                        command.Parameters.AddWithValue("@PropuestaUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@AprobadoUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@TC", decimal.Zero);

                        command.CommandTimeout = 0;

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        private void TemplatePagosProv_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 7);
                        command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Proveedores", string.Empty);
                        command.Parameters.AddWithValue("@GroupCode", 0);

                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);
                        command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                        command.Parameters.AddWithValue("@Aprobado", decimal.Zero);

                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Usuario", string.Empty);

                        command.Parameters.AddWithValue("@PropuestaUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@AprobadoUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@TC", decimal.Zero);

                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;

                        DataTable table = new DataTable();

                        da.Fill(table);

                        dgvGastos.DataSource = table;
                       
                        this.Formato(dgvGastos);
                    }
                }

                this.CargarCuentas();
            }
            catch (Exception ex)
            {
            }
        }

        private void dgvGastos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Tipo = string.Empty;
                DocEntry = 0;

                DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];

                txtFolio.Text = Convert.ToString(row.Cells[(int)Columnas.FolioSAP].Value);
                txtNombre.Text = Convert.ToString(row.Cells[(int)Columnas.Nombre].Value);
                cbFechaCont.Value = Convert.ToDateTime(row.Cells[(int)Columnas.FechaCont].Value);
                cbFechaTrans.Value = Convert.ToDateTime(row.Cells[(int)Columnas.FechaTrans].Value);
                cbCtaContable.SelectedValue = Convert.ToString(row.Cells[(int)Columnas.Cuenta].Value);


                DocEntry = Convert.ToInt32(row.Cells[(int)Columnas.Docentry].Value);
                Tipo = Convert.ToString(row.Cells[(int)Columnas.Tipo].Value);
            }
            catch (Exception)
            {
            }
        }

        private void cbCtaContable_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataTable t = (dgvGastos.DataSource) as DataTable;
                //dgvGastos.DataSource = null;
                foreach (DataRow item in t.Rows)
                {
                    if (item.Field<string>("Tipo") == Tipo && item.Field<int>("Docentry") == DocEntry)
                    {
                        item.SetField("Cuenta contable", cbCtaContable.SelectedValue.ToString());
                    }
                }

                dgvGastos.DataSource = t;
                Tipo = string.Empty;
                DocEntry = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbFechaTrans_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable t = (dgvGastos.DataSource) as DataTable;

                foreach (DataRow item in t.Rows)
                {
                    if (item.Field<string>("Tipo") == Tipo && item.Field<int>("Docentry") == DocEntry)
                    {
                        item.SetField("Fecha transferencia", cbFechaTrans.Value);
                    }
                }

                dgvGastos.DataSource = t;

                Tipo = string.Empty;
                DocEntry = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbFechaCont_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable t = (dgvGastos.DataSource) as DataTable;

                foreach (DataRow item in t.Rows)
                {
                    if (item.Field<string>("Tipo") == Tipo && item.Field<int>("Docentry") == DocEntry)
                    {
                        item.SetField("FechaContabilizacion", cbFechaCont.Value);
                    }
                }

                dgvGastos.DataSource = t;

                Tipo = string.Empty;
                DocEntry = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvGastos_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)Columnas.FechaCont || e.ColumnIndex == (int)Columnas.FechaTrans || e.ColumnIndex == (int)Columnas.Cuenta)
                {
                    string valor = string.Empty;

                    var seleccionadas = (sender as DataGridView).SelectedCells;
                    Stack<DataGridViewCell> Seleccionadas = new Stack<DataGridViewCell>();

                    foreach (DataGridViewCell item in seleccionadas)
                    {
                        Seleccionadas.Push(item);
                    }

                    bool flag = false;
                    foreach (DataGridViewCell item in Seleccionadas)
                    {
                        if (!flag)
                            valor = item.Value.ToString();
                        item.Value = valor;
                        flag = true;
                    }
                }
            }
            catch (Exception )
            {
                
            }
        }

        private void dataset_to_file_csv(DataSet ds, String path)
        {
            String[] texto;
            texto = new String[ds.Tables[0].Rows.Count + 1];
            //Rellenamos la cabecera del fichero
            texto[0] = String.Empty;
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                texto[0] += ds.Tables[0].Columns[i].ColumnName + "\t";
            }
            //Rellenamos el detalle del fichero
            String linea;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                linea = String.Empty;
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    linea += ds.Tables[0].Rows[i][j].ToString() + "\t";
                }
                texto[i + 1] = linea;
            }
            File.WriteAllLines(path + ".csv", texto);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Excel.Workbook MyBookRCT2 = null;
            //Excel.Workbook MyBook = null;

            try
            {
                if (dgvGastos.Rows.Count > 0)
                {
                    string[] encabezado;
                    string[] detalle;
                    int lastRow = 2;
                    int lastRowDetalle = 2;

                    FolderBrowserDialog f = new FolderBrowserDialog();
                    f.Description = "Elija una carpeta para guardar el template.";
                    f.ShowDialog();

                    string path = f.SelectedPath;
                    if (!string.IsNullOrEmpty(path))
                    {
                        DataTable Datos = dgvGastos.DataSource as DataTable;
                        //DataSet dg = new DataSet();
                        //dg.Tables.Add(dgvGastos.DataSouLLrce as DataTable);
                        //dataset_to_file_csv(dg, path+"\\tem");

                        var provs = (from item in Datos.AsEnumerable()
                                     where !string.IsNullOrEmpty(item.Field<string>("Proveedor"))
                                           && !item.Field<string>("Estatus").Contains("Procesado")
                                           && item.Field<Int16>("Grupo") != 101
                                     select item.Field<string>("Proveedor")).Distinct().ToList();

                        encabezado = new string[provs.Count + 2];
                        detalle = new string[Datos.Rows.Count + 2];

                        encabezado[0] = "DocNum\tDocType\tDocDate\tCardCode\tTransferAccount\tTransferSum\tTransferDate\tTransferReference\tDocCurrency\tJournalRemarks\tU_MetPago";
                        encabezado[1] = "DocNum\tDocType\tDocDate\tCardCode\tTrsfrAcct\tTrsfrSum\tTrsfrDate\tTrsfrRef\tDocCurr\tJrnlMemo\tU_MetPago";

                        detalle[0] = "ParentKey\tDocEntry\tSumApplied\tAppliedFC\tInvoiceType\tDocLine";
                        detalle[1] = "DocNum\tDocEntry\tSumApplied\tAppliedFC\tInvType\tDocLine";

                        int count = 1;
                        foreach (string item in provs)
                        {
                            encabezado[lastRow] += count.ToString() + "\t";
                            encabezado[lastRow] += "rSupplier\t";
                            encabezado[lastRow] += (from pp in Datos.AsEnumerable()
                                                   where pp.Field<string>("Proveedor") == item
                                                         && !pp.Field<string>("Estatus").Contains("Procesado")
                                                   select pp.Field<DateTime>("FechaContabilizacion")).FirstOrDefault().ToString("yyyyMMdd") + "\t";

                            encabezado[lastRow] += (from pp in Datos.AsEnumerable()
                                                   where pp.Field<string>("Proveedor") == item
                                                         && !pp.Field<string>("Estatus").Contains("Procesado")
                                                   select pp.Field<string>("Proveedor")).FirstOrDefault() + "\t";

                            encabezado[lastRow] += (from pp in Datos.AsEnumerable()
                                                   where pp.Field<string>("Proveedor") == item
                                                         && !pp.Field<string>("Estatus").Contains("Procesado")
                                                   select pp.Field<string>("Cuenta contable")).FirstOrDefault() + "\t";

                            encabezado[lastRow] += (from pp in Datos.AsEnumerable()
                                                   where pp.Field<string>("Proveedor") == item
                                                         && !pp.Field<string>("Estatus").Contains("Procesado")
                                                   select pp.Field<decimal>("Aprobado")).Sum().ToString() + "\t";

                            encabezado[lastRow] += (from pp in Datos.AsEnumerable()
                                                   where pp.Field<string>("Proveedor") == item
                                                         && !pp.Field<string>("Estatus").Contains("Procesado")
                                                   select pp.Field<DateTime>("Fecha transferencia")).FirstOrDefault().ToString("yyyyMMdd") + "\t";

                            string aux = (from pp in Datos.AsEnumerable()
                                          where pp.Field<string>("Proveedor") == item
                                                && !pp.Field<string>("Estatus").Contains("Procesado")
                                          select pp.Field<string>("Nombre")).FirstOrDefault().ToString();

                            if (aux.Length > 27)
                                aux = aux.Substring(0, 27);

                            encabezado[lastRow] += aux + "\t";

                            encabezado[lastRow] += (from pp in Datos.AsEnumerable()
                                                   where pp.Field<string>("Proveedor") == item
                                                         && !pp.Field<string>("Estatus").Contains("Procesado")
                                                   select pp.Field<string>("Moneda")).FirstOrDefault() + "\t";

                            aux = (from pp in Datos.AsEnumerable()
                                                    where pp.Field<string>("Proveedor") == item
                                                          && !pp.Field<string>("Estatus").Contains("Procesado")
                                                    select pp.Field<string>("Nombre")).FirstOrDefault().ToString();
                            if(aux.Length > 50)
                                aux = aux.Substring(0, 50);

                            encabezado[lastRow] += aux + "\t";

                            string met_pago = string.Empty;
                            if ((from pp in Datos.AsEnumerable()
                                 where pp.Field<string>("Proveedor") == item
                                       && !pp.Field<string>("Estatus").Contains("Procesado")
                                 select pp.Field<string>("Cuenta contable")).FirstOrDefault().Trim() == "2140-002-000")
                                met_pago = "99";

                            encabezado[lastRow] += met_pago;
                            lastRow++;

                            foreach (DataRow line in Datos.Rows)
                            {
                                if (line.Field<string>("Proveedor").Equals(item) && !line.Field<string>("Estatus").Contains("Procesado"))
                                {

                                    detalle[lastRowDetalle] += count.ToString() + "\t";
                                    detalle[lastRowDetalle] += line.Field<Int32>("DocEntry").ToString() + "\t";

                                    if (line.Field<string>("Moneda").Equals("$"))
                                        detalle[lastRowDetalle] += line.Field<decimal>("Aprobado").ToString() + "\t\t";

                                    if (line.Field<string>("Moneda").Equals("USD"))
                                        detalle[lastRowDetalle] += "\t" + line.Field<decimal>("Aprobado").ToString() + "\t";

                                    detalle[lastRowDetalle] += line.Field<string>("Tipo")+"\t";
                                    detalle[lastRowDetalle] += line.Field<Int32>("DocLine").ToString();

                                    lastRowDetalle++;

                                    this.EstatusTemplate(line.Field<Int32>("DocEntry"), line.Field<string>("Tipo"));
                                }
                            }
                            count++;
                        }

                        File.WriteAllLines(path + "\\OVPM_Payments.txt", encabezado, Encoding.UTF8);
                        File.WriteAllLines(path + "\\VPM2_Payments.txt", detalle, Encoding.UTF8);



                        MessageBox.Show("Listo!", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

        private void tpGastos_Click(object sender, EventArgs e)
        {

        }

        private void TemplatePagosProv_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void TemplatePagosProv_FormClosing(object sender, FormClosingEventArgs e)
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
