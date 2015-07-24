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

namespace Compras
{
    public partial class frmAnalisisObsLento : Form
    {
        public frmAnalisisObsLento()
        {
            InitializeComponent();
        }
        DataTable Datos = new DataTable();
        public enum Columnas
        {
            Articulo,
            Descripcion,
            Linea,
            Proveedor,
            Clasificacion,
            Col,
            Clasificacion1,
            Moneda,
            Price,
            Stock,
            Total,
            Total_MXN,
            Dias,
            Promedio,
            Planning,
            VI,
            LeadTime,
            P_Obsoleto,
            Revisado
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Articulo].Width = 85;
            dgv.Columns[(int)Columnas.Descripcion].Width = 200;
            dgv.Columns[(int)Columnas.Linea].Width = 90;
            dgv.Columns[(int)Columnas.Proveedor].Width = 120;
            dgv.Columns[(int)Columnas.Clasificacion].Width = 90;
            dgv.Columns[(int)Columnas.Price].Width = 90;
            dgv.Columns[(int)Columnas.Stock].Width = 70;
            dgv.Columns[(int)Columnas.Moneda].Width = 60;
            dgv.Columns[(int)Columnas.Total].Width = 90;
            dgv.Columns[(int)Columnas.Clasificacion1].Width = 90;
            dgv.Columns[(int)Columnas.Planning].Width = 90;
            dgv.Columns[(int)Columnas.VI].Width = 80;
            dgv.Columns[(int)Columnas.LeadTime].Width = 80;

            dgv.Columns[(int)Columnas.Total_MXN].Visible = false;
            dgv.Columns[(int)Columnas.Revisado].Visible = false;
            dgv.Columns[(int)Columnas.Col].Visible = false;
            dgv.Columns[(int)Columnas.P_Obsoleto].Visible = false;

            dgv.Columns[(int)Columnas.Price].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Promedio].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Price].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Planning].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.VI].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.LeadTime].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            int x = 0;
            foreach (DataGridViewColumn item in dgvDetalle.Columns)
            {
                if (x > 0)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    item.DefaultCellStyle.Format = "N0";
                    item.Width = 85;
                    item.ReadOnly = true;
                }
                x++;
            }
        }

        public void CargarLinea(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 15);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public void CargarProveedores(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 16);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        private void frmAnalisisObsLento_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.CargarLinea(cbLinea, string.Empty);
            this.CargarProveedores(cbProveedor, string.Empty);

            cbFiltro.SelectedIndex = 0;
            cbFiltro2.SelectedIndex = 0;

            //DataTable tbl = new DataTable();
            //using (SqlConnection connecion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            //{
            //    using (SqlCommand command = new SqlCommand("PJ_Compras", connecion))
            //    {
            //        command.CommandType = CommandType.StoredProcedure;
            //        command.Parameters.AddWithValue("@TipoConsulta", 18);
            //        command.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);
            //        command.Parameters.AddWithValue("@Proveedores", cbProveedor.SelectedValue);
            //        command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);

            //        SqlDataAdapter da = new SqlDataAdapter();
            //        da.SelectCommand = command;
            //        da.SelectCommand.CommandTimeout = 0;


            //        da.Fill(tbl);

            //        dgvDatos.DataSource = tbl;

            //        this.Formato(dgvDatos);
            //    }
            //}
            //try
            //{
            //    DataTable tblTotal = new DataTable();
            //    tblTotal.Columns.Add("Tipo", typeof(string));
            //    tblTotal.Columns.Add("Promoción", typeof(decimal));
            //    tblTotal.Columns.Add("Remate", typeof(decimal));
            //    tblTotal.Columns.Add("Devolución", typeof(decimal));
            //    tblTotal.Columns.Add("TOTAL", typeof(decimal), "Promoción+Remate+Devolución");

            //    DataRow row1 = tblTotal.NewRow();
            //    row1[0] = "(PZ)";
            //    row1[1] = Convert.ToDecimal(tbl.Compute("Sum([Stock])", "Clasificación='Promoción'") == DBNull.Value ? 0 : tbl.Compute("Sum([Stock])", "Clasificación='Promoción'"));
            //    row1[2] = Convert.ToDecimal(tbl.Compute("Sum([Stock])", "Clasificación='Remate'") == DBNull.Value ? 0 : tbl.Compute("Sum([Stock])", "Clasificación='Remate'"));
            //    row1[3] = Convert.ToDecimal(tbl.Compute("Sum([Stock])", "Clasificación='Devolución'") == DBNull.Value ? 0 : tbl.Compute("Sum([Stock])", "Clasificación='Devolución'"));
            //    tblTotal.Rows.Add(row1);

            //    DataRow row2 = tblTotal.NewRow();
            //    row2[0] = "($)";
            //    row2[1] = Convert.ToDecimal(tbl.Compute("Sum([Total_MXN])", "Clasificación='Promoción'") == DBNull.Value ? 0 : tbl.Compute("Sum([Total_MXN])", "Clasificación='Promoción'"));
            //    row2[2] = Convert.ToDecimal(tbl.Compute("Sum([Total_MXN])", "Clasificación='Remate'") == DBNull.Value ? 0 : tbl.Compute("Sum([Total_MXN])", "Clasificación='Remate'"));
            //    row2[3] = Convert.ToDecimal(tbl.Compute("Sum([Total_MXN])", "Clasificación='Devolución'") == DBNull.Value ? 0 : tbl.Compute("Sum([Total_MXN])", "Clasificación='Devolución'"));
            //    tblTotal.Rows.Add(row2);

            //    dgvTotales.DataSource = tblTotal;

            //    dgvTotales.Columns[0].HeaderText = string.Empty;
            //    dgvTotales.Columns[0].Width = 40;
            //    dgvTotales.Columns[1].Width = 80;
            //    dgvTotales.Columns[2].Width = 80;
            //    dgvTotales.Columns[3].Width = 80;
            //    dgvTotales.Columns[4].Width = 80;

            //    dgvTotales.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            //    dgvTotales.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            //    dgvTotales.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            //    dgvTotales.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            //    dgvTotales.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            //    dgvTotales.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    dgvTotales.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    dgvTotales.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //    dgvTotales.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //    dgvTotales.Rows[0].DefaultCellStyle.Format = "N0";
            //    dgvTotales.Rows[1].DefaultCellStyle.Format = "C2";
            //}
            //catch (Exception)
            //{

            //}
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (string.IsNullOrEmpty(item.Cells[(int)Columnas.Clasificacion].Value.ToString()))
                    {
                        item.Cells[(int)Columnas.Clasificacion].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Clasificacion].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Clasificacion].Style.BackColor = Color.Green;
                        item.Cells[(int)Columnas.Clasificacion].Style.ForeColor = Color.Black;
                    }

                    if (item.Cells[(int)Columnas.Clasificacion1].Value.ToString().Equals("Descontinuado") &&
                        item.Cells[(int)Columnas.Planning].Value.ToString().Equals(string.Empty))
                    {
                        item.Cells[(int)Columnas.Clasificacion1].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Clasificacion1].Style.ForeColor = Color.White;
                    }

                    if (item.Cells[(int)Columnas.Clasificacion1].Value.ToString().Equals("Obsoleto") &&
                        Convert.ToDecimal(item.Cells[(int)Columnas.Promedio].Value) == decimal.Zero &&
                        item.Cells[(int)Columnas.P_Obsoleto].Value.ToString().Equals("N"))
                    {
                        item.Cells[(int)Columnas.Clasificacion1].Style.BackColor = Color.Orange;
                        item.Cells[(int)Columnas.Clasificacion1].Style.ForeColor = Color.White;
                    }

                    if (item.Cells[(int)Columnas.Revisado].Value.ToString().Equals("Y") )
                    {
                        item.Cells[(int)Columnas.Articulo].Style.BackColor = Color.Green;
                        item.Cells[(int)Columnas.Articulo].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception) { }
        }

        private void dataGridView3_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable tbl = new DataTable();
                using (SqlConnection connecion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Compras", connecion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 19);
                        command.Parameters.AddWithValue("@Articulo", dgvDatos.Rows[e.RowIndex].Cells[(int)Columnas.Articulo].Value.ToString());


                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;


                        da.Fill(tbl);

                        dgvDetalle.DataSource = tbl;

                        foreach (DataGridViewColumn item in dgvDetalle.Columns)
                        {
                            if (item.Index > 1)
                            {
                                item.Width = 70;
                                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            }
                            else
                            {
                                item.Width = 75;
                                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                            }

                        }

                        dgvDetalle.Rows[0].DefaultCellStyle.Format = "N0";
                        dgvDetalle.Rows[1].DefaultCellStyle.Format = "N2";
                    }

                }
            }
            catch (Exception)
            {

            }

            if (e.RowIndex > -1)
            {
                string __articulo = (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString();

                SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 21);
                command.Parameters.AddWithValue("@Articulo", __articulo);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = command;
                DataTable table = new DataTable();

                da.Fill(table);
                dgvVentas.DataSource = table;
                foreach (DataGridViewColumn item in dgvVentas.Columns)
                {
                    if (item.Index > 1)
                    {
                        item.Width = 60;
                        item.DefaultCellStyle.Format = "N0";
                        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else
                    {
                        item.Width = 75;
                        item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    }

                }

                


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Datos.Clear();
                //DataTable tbl = new DataTable();
                using (SqlConnection connecion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Compras", connecion))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 18);
                        command.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);
                        command.Parameters.AddWithValue("@Proveedores", cbProveedor.SelectedValue);
                        command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;


                        da.Fill(Datos);

                        dgvDatos.DataSource = Datos;

                        this.Formato(dgvDatos);
                    }
                }
                try
                {
                    cbFiltro_SelectionChangeCommitted(sender, e);
                    cbFiltro_SelectionChangeCommitted(sender, e);
                    
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {

            }
        }

        private void cbFiltro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string filter = string.Empty;
                DataView view = new DataView(Datos);

                if (cbFiltro.SelectedIndex == 0) filter = "Clasificación like '%%'";
                else if (cbFiltro.SelectedIndex == 1) filter = "Clasificación=''";
                else filter = "Clasificación='" + cbFiltro.Text + "'";

                if (cbFiltro2.SelectedIndex == 0) filter += " AND Clasificación1 like '%%'";
                else if (cbFiltro2.SelectedIndex == 1) filter = "Clasificación1=''";
                else filter += " AND Clasificación1='" + cbFiltro2.Text + "'";

                view.RowFilter = filter;
                dgvDatos.DataSource = view.ToTable();

                DataTable __datos = new DataTable();
                __datos = dgvDatos.DataSource as DataTable;
                try
                {
                    DataTable tblTotal = new DataTable();
                    tblTotal.Columns.Add("Tipo", typeof(string));
                    tblTotal.Columns.Add("Promoción", typeof(decimal));
                    tblTotal.Columns.Add("Remate", typeof(decimal));
                    tblTotal.Columns.Add("Devolución", typeof(decimal));
                    //*tblTotal.Columns.Add("Descontinuado", typeof(decimal));
                    tblTotal.Columns.Add("Remate(No devolución)", typeof(decimal));
                    tblTotal.Columns.Add("Sin clasificacón", typeof(decimal));
                    tblTotal.Columns.Add("TOTAL", typeof(decimal), "Promoción+Remate+Devolución+[Sin clasificacón]+[Remate(No devolución)]");

                    DataRow row1 = tblTotal.NewRow();
                    row1[0] = "(PZ)";
                    row1[1] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación='Promoción'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación='Promoción'"));
                    row1[2] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación='Remate'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación='Remate'"));
                    row1[3] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación='Devolución'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación='Devolución'"));
                    //row1[4] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación='Descontinuado'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación='Descontinuado'"));
                    row1[4] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación='Remate por no devolución'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación='Remate por no devolución'"));
                    row1[5] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación=''") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación=''"));
                    tblTotal.Rows.Add(row1);

                    DataRow row2 = tblTotal.NewRow();
                    row2[0] = "($)";
                    row2[1] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación='Promoción'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación='Promoción'"));
                    row2[2] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación='Remate'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación='Remate'"));
                    row2[3] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación='Devolución'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación='Devolución'"));
                    //row2[4] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación='Descontinuado'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación='Descontinuado'"));
                    row2[4] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación='Remate por no devolución'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación='Remate por no devolución'"));
                    row2[5] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación=''") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación=''"));
                    tblTotal.Rows.Add(row2);

                    dgvTotales.DataSource = tblTotal;

                    dgvTotales.Columns[0].HeaderText = string.Empty;
                    dgvTotales.Columns[0].Width = 40;
                    dgvTotales.Columns[1].Width = 80;
                    dgvTotales.Columns[2].Width = 80;
                    dgvTotales.Columns[3].Width = 80;
                    dgvTotales.Columns[4].Width = 80;
                    dgvTotales.Columns[5].Width = 80;
                    dgvTotales.Columns[6].Width = 80;

                    dgvTotales.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotales.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotales.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotales.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotales.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotales.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotales.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;


                    dgvTotales.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotales.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotales.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotales.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotales.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotales.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvTotales.Rows[0].DefaultCellStyle.Format = "N0";
                    dgvTotales.Rows[1].DefaultCellStyle.Format = "C2";
                }
                catch (Exception)
                {

                }
                //////calsificacion 1
                try
                {
                    DataTable tblTotal1 = new DataTable();
                    tblTotal1.Columns.Add("Tipo", typeof(string));
                    tblTotal1.Columns.Add("Obsoleto", typeof(decimal));
                    tblTotal1.Columns.Add("Lento movimiento", typeof(decimal));
                    tblTotal1.Columns.Add("Descontinuado", typeof(decimal));
                    tblTotal1.Columns.Add("N(Mov)", typeof(decimal));
                    tblTotal1.Columns.Add("TOTAL", typeof(decimal), "Obsoleto+[Lento movimiento]+[Descontinuado]+[N(Mov)]");

                    DataRow row3 = tblTotal1.NewRow();
                    row3[0] = "(PZ)";
                    row3[1] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación1='Obsoleto'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación1='Obsoleto'"));
                    row3[2] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación1='Lento movimiento'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación1='Lento movimiento'"));
                    row3[3] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación1='Descontinuado'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación1='Descontinuado'"));
                    row3[4] = Convert.ToDecimal(__datos.Compute("Sum([Stock])", "Clasificación1='N(Mov)'") == DBNull.Value ? 0 : __datos.Compute("Sum([Stock])", "Clasificación1='N(Mov)'"));
                    tblTotal1.Rows.Add(row3);

                    DataRow row4 = tblTotal1.NewRow();
                    row4[0] = "($)";
                    row4[1] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación1='Obsoleto'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación1='Obsoleto'"));
                    row4[2] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación1='Lento movimiento'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación1='Lento movimiento'"));
                    row4[3] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación1='Descontinuado'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación1='Descontinuado'"));
                    row4[4] = Convert.ToDecimal(__datos.Compute("Sum([Total_MXN])", "Clasificación1='N(Mov)'") == DBNull.Value ? 0 : __datos.Compute("Sum([Total_MXN])", "Clasificación1='N(Mov)'"));
                    tblTotal1.Rows.Add(row4);

                    dgvTotal2.DataSource = tblTotal1;
                    dgvTotal2.Rows[0].DefaultCellStyle.Format = "N0";
                    dgvTotal2.Rows[1].DefaultCellStyle.Format = "C2";

                    dgvTotal2.Columns[0].HeaderText = string.Empty;
                    dgvTotal2.Columns[0].Width = 40;
                    dgvTotal2.Columns[1].Width = 80;
                    dgvTotal2.Columns[2].Width = 80;
                    dgvTotal2.Columns[3].Width = 80;
                    dgvTotal2.Columns[4].Width = 80;
                    dgvTotal2.Columns[5].Width = 80;

                    dgvTotal2.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotal2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotal2.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotal2.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotal2.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvTotal2.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;


                    dgvTotal2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotal2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotal2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotal2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvTotal2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgvTotal2.Rows[0].DefaultCellStyle.Format = "N0";
                    dgvTotal2.Rows[1].DefaultCellStyle.Format = "C2";
                }
                catch (Exception)
                {

                }
            }
            catch (Exception)
            {

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtArticulo.Clear();
            cbLinea.SelectedIndex = 0;
            cbProveedor.SelectedIndex = 0;
            cbFiltro.SelectedIndex = 0;

            dgvDatos.DataSource = null;
            dgvTotales.DataSource = null;
            dgvDetalle.DataSource = null;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Exportar sin formato?\r\n Si elige 'No' el proceso puede durar varios minutos.", "HalcoNET", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ExportarAExcel ex = new ExportarAExcel();
                if (ex.ExportarSinFormato(dgvDatos))
                    MessageBox.Show("El archivo se creo correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (dialogResult == DialogResult.No)
            {
                ExportarAExcel ex = new ExportarAExcel();
                if (ex.ExportarTodo(dgvDatos))
                    MessageBox.Show("El archivo se creo correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                int x = 0;
                foreach (DataGridViewCell item in (sender as DataGridView).Rows[1].Cells)
                {
                    if (x > 0)
                        if (Convert.ToDecimal(item.Value) > decimal.Zero)
                            item.Style.BackColor = Color.Green;
                    x++;
                }
            }
            catch (Exception)
            {
                
            }
        }


    }
}
