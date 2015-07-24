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

namespace Compras.Desarrollo
{
    public partial class frmCompras : Form
    {
        int __formato = 0;
        bool __Almacen = false;
        string ZNORTE = string.Empty;
        string ZCENTRO = string.Empty;
        string OTROS = string.Empty;

        public enum ColumnasCompras
        {
            Linea,
            Articulo,
            Nombre,
            Clasificacion,
            idealIMPO,
            stockZIMPO,
            IMPO,
            idealZNORTE,
            stockZNORTE,
            ZNORTE,
            idealZCENTRO,
            stockZCENTRO,
            ZCENTRO,
            idealpue,
            stockpue,
            pue,
            idealapi,
            stockapi,
            api,
            idealcor,
            stockcor,
            cor,
            idealtep,
            stocktep,
            tep,
            idealmex,
            stockmex,
            mex,
            idealmty,
            stockmty,
            mty,
            idealsal,
            stocksal,
            sal,
            idealgdl,
            stockgdl,
            gdl
        }

        public frmCompras()
        {
            InitializeComponent();
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasCompras.Linea].Width = 80;
            dgv.Columns[(int)ColumnasCompras.Articulo].Width = 90;
            dgv.Columns[(int)ColumnasCompras.Nombre].Width = 130;
            dgv.Columns[(int)ColumnasCompras.Clasificacion].Width = 80;

            dgv.Columns[(int)ColumnasCompras.Linea].ReadOnly = true;
            dgv.Columns[(int)ColumnasCompras.Articulo].ReadOnly = true;
            dgv.Columns[(int)ColumnasCompras.Nombre].ReadOnly = true;
            dgv.Columns[(int)ColumnasCompras.Clasificacion].ReadOnly = true;

            int x =0;
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                if (x > 3)
                {
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    item.DefaultCellStyle.Format = "N0";
                    item.Width = 85;
                    item.ReadOnly = true;
                }
                x++;
            }

            dgv.Columns["Autorizado"].DefaultCellStyle.BackColor = Color.FromName("Info");
            dgv.Columns["Autorizado"].ReadOnly = false;
            dgv.Columns["Veces Ideal"].DefaultCellStyle.Format = "N2";

            dgv.Columns["TotalPrice"].Visible = false;
            dgv.Columns["Price"].Visible = false;
        }

        public void CargarAlmacenes()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 7);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    DataTable table = new DataTable();

                    da.Fill(table);
                    foreach (DataRow item in table.Rows)
                    {
                        lbAlmacenes.Items.Add(item.Field<string>("Codigo") + " " + item.Field<string>("Nombre"));
                    }
                }
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

        private void Compras_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            this.CargarAlmacenes();
            this.CargarLinea(cbLinea, string.Empty);
            this.CargarProveedores(cbProveedor, string.Empty);

            lbAlmacenes.AllowDrop = true;

        }

        private void lbAlmacenes_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void lbAlmacenes_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (lbAlmacenes.Items.Count == 0)
                    return;

                int index = lbAlmacenes.IndexFromPoint(e.X, e.Y);
                string s = lbAlmacenes.Items[index].ToString();
                DragDropEffects dde1 = DoDragDrop(s, DragDropEffects.All);

                if(__Almacen)
                    if (dde1 == DragDropEffects.All)
                    {
                        lbAlmacenes.Items.RemoveAt(lbAlmacenes.IndexFromPoint(e.X, e.Y));
                    }
            }
            catch (Exception) { }
        }

        private void lbZNORTE_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void lbZNORTE_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                int __zcentro = lbZCENTRO.Items.Count;
                int __znorte = lbZNORTE.Items.Count;
                int __otros = lbOTROS.Items.Count;

                if (((sender as ListBox).Name.Equals("lbZCENTRO") && __znorte == 0 && __otros == 0)
                    || ((sender as ListBox).Name.Equals("lbZNORTE") && __zcentro == 0 && __otros == 0)
                    || ((sender as ListBox).Name.Equals("lbOTROS") && __znorte == 0 && __zcentro == 0 && __otros == 0))
                {
                    string str = (string)e.Data.GetData(
                        DataFormats.StringFormat);

                    (sender as ListBox).Items.Add(str);
                    __Almacen = true;
                }
                else
                {
                    __Almacen = false;
                    MessageBox.Show("Solo se puede realizar una compra a la vez!", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbZNORTE.SelectedIndex != -1)
                {
                    lbAlmacenes.Items.Add(lbZNORTE.Text);

                    lbZNORTE.Items.RemoveAt(lbZNORTE.SelectedIndex);

                    //if (lbZNORTE.Text.Length > 2)
                    //    if (lbZNORTE.Text.Substring(0, 2) == "18")
                    //        __gdl--;

                    lbZNORTE.SelectedIndex = 0;
                }
            }
            catch (Exception) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbZCENTRO.SelectedIndex != -1)
                {
                    lbAlmacenes.Items.Add(lbZCENTRO.Text);

                    lbZCENTRO.Items.RemoveAt(lbZCENTRO.SelectedIndex);

                    //if (lbZCENTRO.Text.Length > 2)
                    //    if (lbZCENTRO.Text.Substring(0, 2) == "18")
                    //        __gdl--;

                    lbZCENTRO.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbOTROS.SelectedIndex != -1)
                {
                    lbAlmacenes.Items.Add(lbOTROS.Text);

                    lbOTROS.Items.RemoveAt(lbOTROS.SelectedIndex);

                    //if (lbOTROS.Text.Length > 2)
                    //    if (lbOTROS.Text.Substring(0, 2) == "18")
                    //        __gdl--;

                    lbOTROS.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {      }
        }

        decimal minoredr;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbLinea.SelectedIndex != 0 || cbProveedor.SelectedIndex != 0)
                {
                    ZNORTE = string.Empty;
                    ZCENTRO = string.Empty;
                    OTROS = string.Empty;

                    foreach (var listBoxItem in lbZNORTE.Items)
                    {
                        if (listBoxItem.ToString().Length >= 2)
                            ZNORTE += "'" + listBoxItem.ToString().Substring(0, 2) + "',";
                    }

                    foreach (var listBoxItem in lbZCENTRO.Items)
                    {
                        if (listBoxItem.ToString().Length >= 2)
                            ZCENTRO += "'" + listBoxItem.ToString().Substring(0, 2) + "',";
                    }

                    foreach (var listBoxItem in lbOTROS.Items)
                    {
                        if (listBoxItem.ToString().Length >= 2)
                            OTROS += "'" + listBoxItem.ToString().Substring(0, 2) + "',";
                    }
                    if (string.IsNullOrEmpty(ZNORTE)) ZNORTE = "''";
                    if (string.IsNullOrEmpty(ZCENTRO)) ZCENTRO = "''";
                    if (string.IsNullOrEmpty(OTROS)) OTROS = "''";

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 6);
                            command.Parameters.AddWithValue("@AlmacenesZNORTE", ZNORTE.Trim(','));
                            command.Parameters.AddWithValue("@AlmacenesZCENTRO", ZCENTRO.Trim(','));
                            command.Parameters.AddWithValue("@AlmacenesOTROS", OTROS.Trim(','));
                            command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                            command.Parameters.AddWithValue("@IMPO", checkBox1.Checked ? "Y" : "N");
                            command.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);
                            command.Parameters.AddWithValue("@Proveedores", cbProveedor.SelectedValue);

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;

                            DataTable table = new DataTable();
                            da.Fill(table);

                            gridCompras.DataSource = table;
                            //IIF(QUALITY<=9.0,1.0,0.0)
                            table.Columns.Add("Veces Ideal", typeof(decimal), "(Stock+[Autorizado])/IIF(Ideal=0,1,Ideal)");
                            table.Columns.Add("TotalPrice", typeof(decimal), "([Autorizado]*Price)");

                            this.Formato(gridCompras);


                        }
                    }

                    //======REPARTO
                    dgvReparto.Visible = !checkBox1.Checked;


                    //============= R E P A R T O .
                    if (!checkBox1.Checked)
                    {
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@TipoConsulta", 13);
                                command.Parameters.AddWithValue("@AlmacenesZNORTE", ZNORTE.Trim(','));
                                command.Parameters.AddWithValue("@AlmacenesZCENTRO", ZCENTRO.Trim(','));
                                command.Parameters.AddWithValue("@AlmacenesOTROS", OTROS.Trim(','));
                                command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                                command.Parameters.AddWithValue("@IMPO", checkBox1.Checked ? "Y" : "N");
                                command.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);
                                command.Parameters.AddWithValue("@Proveedores", cbProveedor.SelectedValue);

                                SqlDataAdapter da = new SqlDataAdapter();
                                da.SelectCommand = command;
                                DataTable tbl = new DataTable();
                                da.Fill(tbl);
                                dgvReparto.DataSource = tbl;

                                if (dgvReparto.Columns.Count > 1)
                                {
                                    dgvReparto.Columns[0].Visible = false;

                                    dgvReparto.Columns[3].DefaultCellStyle.Format = "C2";
                                    dgvReparto.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    dgvReparto.Columns[2].DefaultCellStyle.Format = "N0";
                                    dgvReparto.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                                    if (tbl.Rows.Count > 0)
                                    {
                                        DataRow row = tbl.NewRow();
                                        row[1] = "TOTAL";
                                        row[2] = Convert.ToDecimal(tbl.Compute("SUM([Cantidad PZ])", string.Empty));
                                        row[3] = Convert.ToDecimal(tbl.Compute("SUM([Cantidad $])", string.Empty));

                                        tbl.Rows.Add(row);
                                    }
                                }
                            }
                        }
                    }

                    ////Totales min order
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 20);
                            command.Parameters.AddWithValue("@Proveedores", cbProveedor.SelectedValue);
                            connection.Open();

                            minoredr = Convert.ToDecimal(command.ExecuteScalar());

                            DataTable tblTotales = new DataTable();
                            tblTotales.Columns.Add("Minimo de compra", typeof(decimal));
                            tblTotales.Columns.Add("Total (PZ)", typeof(decimal));
                            tblTotales.Columns.Add("Total ($)", typeof(decimal));

                            DataRow rowTotal = tblTotales.NewRow();
                            rowTotal[0] = minoredr;
                            rowTotal[1] = Convert.ToDecimal((gridCompras.DataSource as DataTable).Compute("SUM(Autorizado)", string.Empty));
                            rowTotal[2] = Convert.ToDecimal((gridCompras.DataSource as DataTable).Compute("SUM(TotalPrice)", string.Empty));
                            tblTotales.Rows.Add(rowTotal);

                            dataGridView1.DataSource = tblTotales;

                            dataGridView1.Columns[0].DefaultCellStyle.Format = "C2";
                            dataGridView1.Columns[1].DefaultCellStyle.Format = "N0";
                            dataGridView1.Columns[2].DefaultCellStyle.Format = "C2";

                        }
                    }
                }
                else MessageBox.Show("Seleccione una Linea o Proveedor", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtArticulo.Clear();
            cbProveedor.SelectedIndex = 0;
            cbLinea.SelectedIndex = 0;

            txtArticulo.Focus();

            gridCompras.Rows.Clear();
            dgvDetails.Rows.Clear();

            this.Compras_Load(sender, e);
        }

        private void gridCompras_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                string ZNORTE = string.Empty;
                string ZCENTRO = string.Empty;
                string OTROS = string.Empty;

                foreach (var listBoxItem in lbZNORTE.Items)
                {
                    if (listBoxItem.ToString().Length >= 2)
                        ZNORTE += "'" + listBoxItem.ToString().Substring(0, 2) + "',";
                }

                foreach (var listBoxItem in lbZCENTRO.Items)
                {
                    if (listBoxItem.ToString().Length >= 2)
                        ZCENTRO += "'" + listBoxItem.ToString().Substring(0, 2) + "',";
                }

                foreach (var listBoxItem in lbOTROS.Items)
                {
                    if (listBoxItem.ToString().Length >= 2)
                        OTROS += "'" + listBoxItem.ToString().Substring(0, 2) + "',";
                }
                if (string.IsNullOrEmpty(ZNORTE)) ZNORTE = "''";
                if (string.IsNullOrEmpty(ZCENTRO)) ZCENTRO = "''";
                if (string.IsNullOrEmpty(OTROS)) OTROS = "''";


                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 8);
                        command.Parameters.AddWithValue("@AlmacenesZNORTE", ZNORTE.Trim(','));
                        command.Parameters.AddWithValue("@AlmacenesZCENTRO", ZCENTRO.Trim(','));
                        command.Parameters.AddWithValue("@AlmacenesOTROS", OTROS.Trim(','));
                        command.Parameters.AddWithValue("@IMPO", checkBox1.Checked ? "Y" : "N");
                        command.Parameters.AddWithValue("@Articulo", (sender as DataGridView).Rows[e.RowIndex].Cells[1].Value);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        DataTable table = new DataTable();
                        da.Fill(table);

                        dgvDetails.DataSource = table;

                        foreach (DataGridViewColumn item in dgvDetails.Columns)
                        {
                            if (item.Index > 1)
                            {
                                item.Width = 60;
                                item.DefaultCellStyle.Format = "N0";
                                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            }
                            else
                            {
                                item.Width = 80;
                                item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            }
                        }
                    }
                }
                gridCompras_CellEndEdit(sender, e);
                
            }
            catch (Exception)
            {
                
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int cols = 0;
            Color color = Color.LightGray;

            // if (__Reublicable)
            foreach (DataGridViewRow item in (sender as DataGridView).Rows)
            {
                foreach (DataGridViewCell cell in item.Cells)
                {
                    if (cell.ColumnIndex > 1)
                    {
                        cell.Style.BackColor = color;
                        cols++;

                        if (cols == 2)
                        {
                            if (color == Color.LightGray) color = Color.White;
                            else color = Color.LightGray;
                            cols = 0;
                        }
                    }
                }
            }
        }

        private void gridCompras_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            exp.ExportarSinFormato(gridCompras);
        }

        private void gridCompras_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == (sender as DataGridView).Columns["Reubicable"].Index)
                    {
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                            {
                                string __almacenes = string.Empty;
                                if (!ZNORTE.Equals("''"))
                                    __almacenes = ZNORTE;
                                else if (!ZCENTRO.Equals("''"))
                                    __almacenes = ZCENTRO;
                                else if (!OTROS.Equals("''"))
                                    __almacenes = OTROS;

                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@TipoConsulta", 12);
                                command.Parameters.AddWithValue("@Almacenes", __almacenes.Trim(','));
                                command.Parameters.AddWithValue("@IMPO", checkBox1.Checked ? "Y" : "N");
                                command.Parameters.AddWithValue("@Articulo", (sender as DataGridView).Rows[e.RowIndex].Cells[1].Value);

                                SqlDataAdapter da = new SqlDataAdapter();
                                da.SelectCommand = command;

                                DataTable table = new DataTable();
                                da.Fill(table);

                                dgvDetails.DataSource = table;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }


        private void gridCompras_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    //                    if (cont < 2)
                    //                    {
                    //cont++;

                    DataTable tblTotales = new DataTable();
                    tblTotales.Columns.Add("Minimo de compra", typeof(decimal));
                    tblTotales.Columns.Add("Total (PZ)", typeof(decimal));
                    tblTotales.Columns.Add("Total ($)", typeof(decimal));

                    DataRow rowTotal = tblTotales.NewRow();
                    rowTotal[0] = minoredr;
                    rowTotal[1] = Convert.ToDecimal((gridCompras.DataSource as DataTable).Compute("SUM(Autorizado)", string.Empty));
                    rowTotal[2] = Convert.ToDecimal((gridCompras.DataSource as DataTable).Compute("SUM(TotalPrice)", string.Empty));
                    tblTotales.Rows.Add(rowTotal);

                    dataGridView1.DataSource = tblTotales;

                    dataGridView1.Columns[0].DefaultCellStyle.Format = "C2";
                    dataGridView1.Columns[1].DefaultCellStyle.Format = "N0";
                    dataGridView1.Columns[2].DefaultCellStyle.Format = "C2";
                    //gridCompras_CellEndEdit(sender, e);
                    //if (e.ColumnIndex == (sender as DataGridView).Columns["Autorizado MULT"].Index)
                    //{
                    //    (sender as DataGridView).Rows[e.RowIndex].Cells["Autorizado"].Value = Convert.ToDecimal((sender as DataGridView).Rows[e.RowIndex].Cells["Autorizado MULT"].Value) * Convert.ToDecimal((sender as DataGridView).Rows[e.RowIndex].Cells["Multiplo"].Value);
                    //}
                    //}
                    //else
                    //{
                    //    cont = 0;


                    //}
                }
            }
            catch (Exception) { }
        }

        private void dgvReparto_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                string __almacen = string.Empty;
                if (lbZCENTRO.Items.Count == 1)
                    __almacen = lbZCENTRO.Items[0].ToString().Substring(0, 2);
                if (lbZNORTE.Items.Count == 1)
                    __almacen = lbZNORTE.Items[0].ToString().Substring(0, 2);

                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[1].Value.ToString().Equals("TOTAL"))
                    {
                        item.DefaultCellStyle.Font = new Font(this.Font.FontFamily,  this.Font.Size, FontStyle.Bold);
                    }
                    if (lbZCENTRO.Items.Count == 1)
                        if (!item.Cells[0].Value.ToString().Equals(__almacen))
                            item.Visible = false;

                    if (lbZNORTE.Items.Count == 1)
                        if (!item.Cells[0].Value.ToString().Equals(__almacen))
                            item.Visible = false;


               }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string __almacen = (sender as DataGridView).Columns[e.ColumnIndex].HeaderText.Substring(0, 2);
                string __articulo = (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString();

                SqlCommand command = new SqlCommand("sp_HistorialVentas");
                command.Parameters.AddWithValue("@TipoConsulta", 1);
                command.Parameters.AddWithValue("@Almacen", __almacen);
                command.Parameters.AddWithValue("@Articulo", __articulo);

                Clases.Ventas vts = new Clases.Ventas();
                dgvVentas.DataSource = vts.GetVentas(command);
                
                //using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                //{
                //    using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                //    {

                //        command.CommandType = CommandType.StoredProcedure;

                //        command.Parameters.AddWithValue("@TipoConsulta", 14);
                //        command.Parameters.AddWithValue("@Almacenes", __almacen);
                //        command.Parameters.AddWithValue("@Articulo", (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value);

                //        SqlDataAdapter da = new SqlDataAdapter();
                //        da.SelectCommand = command;

                //        DataTable table = new DataTable();
                //        da.Fill(table);

                //        dgvVentas.DataSource = table;

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
                //    }
                //}
            }
        }

        private void cbLinea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)(sender as ComboBox).SelectedItem;

                if (Convert.ToString(row[2]).Equals("Y"))
                {
                    groupBox2.Visible = false;
                    checkBox1.Checked = true;
                    checkBox1.Enabled = true;
                }
                else
                {
                    groupBox2.Visible = true;
                    checkBox1.Checked = false;
                    checkBox1.Enabled = false;
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void gridCompras_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                gridCompras_CellEndEdit(sender, e);
            }
            catch (Exception) { }
        }

        private void gridCompras_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                gridCompras_CellEndEdit(sender, e);
            }
            catch (Exception) { }
        }

    }
}