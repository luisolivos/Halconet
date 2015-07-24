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

namespace Ventas.Ventas
{
    public partial class frmAnalisisVenta : Form
    {
        int RolUsuario = ClasesSGUV.Login.Rol;
        string Sucursal = ClasesSGUV.Login.Sucursal;
        int CodigoVendedor = ClasesSGUV.Login.Vendedor1;
        Clases.Logs log;
        DataTable table = new DataTable();
        DataTable datosCopy2 = new DataTable();
        public DataTable Articulos = new DataTable();
        public enum Columnas
        {
            Sucursal,
            Vendedor,
            CodigoVendedor,
            PZ,
            MX
        }

        public enum ColumnasDGVDesglose { 
            Linea,
            Articulo,
            Enero,
            Febrero,
            Marzo,
            Abril,
            Mayo,
            Junio,
            Julio,
            Agosto,
            Septiembre,
            Octubre,
            Noviembre,
            Diciembre,
            Total
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Sucursal].HeaderText = "Sucursal";
            dgv.Columns[(int)Columnas.Vendedor].HeaderText = "Vendedor";
            dgv.Columns[(int)Columnas.CodigoVendedor].HeaderText = "Codigo Vendedor";
            dgv.Columns[(int)Columnas.PZ].HeaderText = "Total (PZ)";
            dgv.Columns[(int)Columnas.MX].HeaderText = "Total ($)";

            dgv.Columns[(int)Columnas.Sucursal].Width = 170;
            dgv.Columns[(int)Columnas.Vendedor].Width = 150;
            dgv.Columns[(int)Columnas.PZ].Width = 100;
            dgv.Columns[(int)Columnas.MX].Width = 100;
            if (rdbTotalPiezas.Checked)
            {
                dgv.Columns[(int)Columnas.MX].Visible = false;
                dgv.Columns[(int)Columnas.PZ].Visible = true;
            }
            else if (rdbTotalMoneda.Checked)
            {
                dgv.Columns[(int)Columnas.PZ].Visible = false;
                dgv.Columns[(int)Columnas.MX].Visible = true;
            }
            dgv.Columns[(int)Columnas.CodigoVendedor].Visible = false;


            dgv.Columns[(int)Columnas.PZ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.MX].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.PZ].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.MX].DefaultCellStyle.Format = "C2";

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void formatoDesglose(DataGridView dgv) {
            /*dgv.Columns[(int)ColumnasDGVDesglose.Sucursal].HeaderText = "Sucursal";
            dgv.Columns[(int)ColumnasDGVDesglose].HeaderText = "Artículo";
            dgv.Columns[(int)ColumnasDGVDesglose].HeaderText = "Descripción";
            dgv.Columns[(int)ColumnasDGVDesglose].HeaderText = "Total (PZ)";
            dgv.Columns[(int)ColumnasDGVDesglose].HeaderText = "Total ($)";*/
            dgv.Columns[(int)ColumnasDGVDesglose.Linea].Width = 140;
            dgv.Columns[(int)ColumnasDGVDesglose.Articulo].Width = 110;
            dgv.Columns[(int)ColumnasDGVDesglose.Enero].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Febrero].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Marzo].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Abril].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Mayo].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Junio].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Julio].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Agosto].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Septiembre].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Octubre].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Noviembre].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Diciembre].Width = 70;
            dgv.Columns[(int)ColumnasDGVDesglose.Total].Width = 70;

            if (rdbTotalPiezas.Checked)
            {
                dgv.Columns[(int)ColumnasDGVDesglose.Total].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Enero].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Febrero].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Marzo].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Abril].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Mayo].DefaultCellStyle.Format = "N0"; ;
                dgv.Columns[(int)ColumnasDGVDesglose.Junio].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Julio].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Agosto].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Septiembre].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Octubre].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Noviembre].DefaultCellStyle.Format = "N0";
                dgv.Columns[(int)ColumnasDGVDesglose.Diciembre].DefaultCellStyle.Format = "N0";
            }
            else if (rdbTotalMoneda.Checked)
            {
                dgv.Columns[(int)ColumnasDGVDesglose.Total].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Enero].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Febrero].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Marzo].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Abril].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Mayo].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Junio].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Julio].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Agosto].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Septiembre].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Octubre].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Noviembre].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasDGVDesglose.Diciembre].DefaultCellStyle.Format = "C2";
            }
            
            dgv.Columns[(int)ColumnasDGVDesglose.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }        
        }
        
        public frmAnalisisVenta()
        {
            InitializeComponent();
        }

        private void CargarVendedores()
        {
            SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ); //new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                //clbVendedor.DataSource = table;
                //clbVendedor.DisplayMember = "Nombre";
                //clbVendedor.ValueMember = "Codigo";
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 11);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                //clbVendedor.DataSource = table;
                //clbVendedor.DisplayMember = "Nombre";
                //clbVendedor.ValueMember = "Codigo";
            }
        }

        private void CargarSucursales()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));//ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "Todas";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            /*if (RolUsuario != (int)Constantes.RolesSistemaSGUV.Administrador)
                foreach (DataRow item in table.Rows)
                {
                    if (item.Field<string>("Nombre").Equals("Racsa"))
                        table.Rows.Remove(item);
                }
            */
            //clbSucursal.DataSource = table;
            //clbSucursal.DisplayMember = "Nombre";
            //clbSucursal.ValueMember = "Codigo";
        }

        private void CargarLinea()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Linea);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            clbLineas.DataSource = table;
            clbLineas.DisplayMember = "Nombre";
            clbLineas.ValueMember = "Codigo";
        }

        private string Cadena(CheckedListBox clb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in clb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                }
            }
            if (clb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clb.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append(item["Codigo"].ToString());
                    }
                }
            }

            return stb.ToString();
        }

        private void clb_Click(object sender, EventArgs e)
        {
            if ((sender as CheckedListBox).SelectedIndex == 0)
            {
                if ((sender as CheckedListBox).CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < (sender as CheckedListBox).Items.Count; item++)
                    {
                        (sender as CheckedListBox).SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < (sender as CheckedListBox).Items.Count; item++)
                    {
                        (sender as CheckedListBox).SetItemChecked(item, true);
                    }
                }
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                dgvDesglose.DataSource = null;
                dgvVenta.DataSource = null;
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_VtaEfectiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;
                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Desde", dtDesde.Value.Date);
                        command.Parameters.AddWithValue("@Hasta", dtHasta.Value.Date);
                        command.Parameters.AddWithValue("@Vendedores", string.Empty);//this.Cadena(clbVendedor));
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);//this.Cadena(clbSucursal));
                        command.Parameters.AddWithValue("@Lineas", this.Cadena(clbLineas));
                        command.Parameters.AddWithValue("@Articulo", textBox1.Text);

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(dt);

                        lblTotal.Text = string.Empty;
                        if (dt.Rows.Count > 0)
                        {
                            Int32 sumaTotalPizas = 0;
                            decimal sumaTotalMoneda = 0;
                            if (dt.Columns["Total (PZ)"] == null)
                            {
                                sumaTotalPizas = Convert.ToInt32((from item in dt.AsEnumerable()
                                         where item.Field<string>("articulo").Contains("Total:")
                                         select item.Field<decimal>("pz")).Sum());
                                sumaTotalMoneda = (from item in dt.AsEnumerable()
                                                   where item.Field<string>("articulo").Contains("Total:")
                                                   select item.Field<decimal>("mx")).Sum();
                                
                            }
                            else {
                                sumaTotalPizas = Convert.ToInt32(dt.Compute("SUM([Total (PZ)])", string.Empty));
                                sumaTotalMoneda = Convert.ToDecimal(dt.Compute("SUM([Total ($)])", string.Empty));
                            }

                            txtShowTotalPz.Text = sumaTotalPizas.ToString();
                            txtShowTotalM.Text = sumaTotalMoneda.ToString("C0");
                            lblShowTotalM.Visible = true;
                            lblShowTotalPz.Visible = true;
                            txtShowTotalM.Visible = true;
                            txtShowTotalPz.Visible = true;                          
                        }
                        else {
                            txtShowTotalPz.Text = 0.ToString();
                            txtShowTotalM.Text = 0.ToString("C0");
                            lblShowTotalM.Visible = false;
                            lblShowTotalPz.Visible = false;
                            txtShowTotalM.Visible = false;
                            txtShowTotalPz.Visible = false;
                        }
                        var query = (from item in dt.AsEnumerable()
                                     select item.Field<string>("Sucursal")
                                    ).Distinct();
                        foreach (var s in query.ToList()) {
                            DataRow dr = dt.NewRow();
                            dr["Sucursal"] = s+ " Total";
                            dr["Total (PZ)"] = Convert.ToInt32(dt.Compute("SUM([Total (PZ)])", "Sucursal = '"+s.Trim()+"'"));
                            dr["Total ($)"] = Convert.ToInt32(dt.Compute("SUM([Total ($)])", "Sucursal = '" + s.Trim() + "'"));
                            dt.Rows.Add(dr);
                        }
                        if(query.Count() > 0)
                            dt = (from tv in dt.AsEnumerable()
                              orderby tv.Field<string>("Sucursal")
                              select tv).CopyToDataTable();


                        dgvVenta.DataSource = dt;
                        this.Formato(dgvVenta);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.Cursor = Cursors.Default;
        }

        private void AnalisisVenta_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                //this.CargarVendedores();
                //this.CargarSucursales();
                this.CargarLinea();

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);


                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                {
                    using (SqlCommand command = new SqlCommand("PJ_CalculoUtilidad", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        SqlParameter PrecCompra = new SqlParameter("@PrecioCompra", 0.0);
                        PrecCompra.Direction = ParameterDirection.Output;
                        command.Parameters.Add(PrecCompra);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(Articulos);
                    }
                }
                textBox1.AutoCompleteCustomSource = frmCalculoUtilidad.Autocomplete(Articulos, "ItemCode");
                textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvVenta_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgvVenta_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[(int)Columnas.Sucursal].Value.ToString().Contains("Total"))
                    {
                        item.DefaultCellStyle.Font = new Font("Arial", 9f, FontStyle.Bold);
                    }
                    /*if (item.Cells[(int)Columnas.Vendedor].Value.ToString().Contains("Total"))
                        item.DefaultCellStyle.Font = new Font("Arial", 9f, FontStyle.Bold);
                    if(item.Cells[(int)Columnas.Articulo].Value.ToString().Contains("Total"))
                        item.DefaultCellStyle.Font = new Font("Arial", 9f, FontStyle.Bold);*/
                }
            }
            catch (Exception)
            {

            }
            this.Cursor = Cursors.Default;
        }

        private void dgvVenta_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgvDesglose.DataSource = null;
                dgvDesglose.Rows.Clear();

                string ST = dgvVenta.Rows[e.RowIndex].Cells[(int)Columnas.Sucursal].Value.ToString();
                if (ST.Contains("Total"))
                    return;
                string codigoVendedor = dgvVenta.Rows[e.RowIndex].Cells[(int)Columnas.CodigoVendedor].Value.ToString();

                using (SqlConnection conexion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand cmd = new SqlCommand("PJ_VtaEfectiva", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        //
                        cmd.Parameters.AddWithValue("@TipoConsulta", 4);
                        cmd.Parameters.AddWithValue("@Desde", dtDesde.Value.Date);
                        cmd.Parameters.AddWithValue("@Hasta", dtHasta.Value.Date);
                        if (rdbTotalMoneda.Checked)
                            cmd.Parameters.AddWithValue("@Vendedores", "mx");
                        else
                            cmd.Parameters.AddWithValue("@Vendedores", string.Empty);
                        cmd.Parameters.AddWithValue("@Sucursales", this.Cadena(clbLineas));
                        cmd.Parameters.AddWithValue("@Lineas", codigoVendedor);//this.Cadena(clbLineas));
                        cmd.Parameters.AddWithValue("@Articulo", textBox1.Text);//textBox1.Text);

                        DataTable dt = new DataTable();
                        SqlDataAdapter sqlda = new SqlDataAdapter();
                        sqlda.SelectCommand = cmd;
                        sqlda.Fill(dt);

                        var query = (from item in dt.AsEnumerable()
                                     select item.Field<string>("Linea")).Distinct();

                        foreach (var item in query.ToList())
                        {
                            DataRow r = dt.NewRow();
                            r["Linea"] = item + " Total";
                            decimal sumEnero = (from acum in dt.AsEnumerable()
                                                where acum.Field<string>("Linea") == item
                                                select acum.Field<decimal>("Enero")).Sum();
                            decimal sumFebrero = (from acum in dt.AsEnumerable()
                                                  where acum.Field<string>("Linea") == item
                                                  select acum.Field<decimal>("Febrero")).Sum();
                            decimal sumMarzo = (from acum in dt.AsEnumerable()
                                                where acum.Field<string>("Linea") == item
                                                select acum.Field<decimal>("Marzo")).Sum();
                            decimal sumAbril = (from acum in dt.AsEnumerable()
                                                where acum.Field<string>("Linea") == item
                                                select acum.Field<decimal>("Abril")).Sum();
                            decimal sumMayo = (from acum in dt.AsEnumerable()
                                               where acum.Field<string>("Linea") == item
                                               select acum.Field<decimal>("Mayo")).Sum();
                            decimal sumJunio = (from acum in dt.AsEnumerable()
                                                where acum.Field<string>("Linea") == item
                                                select acum.Field<decimal>("Junio")).Sum();
                            decimal sumJulio = (from acum in dt.AsEnumerable()
                                                where acum.Field<string>("Linea") == item
                                                select acum.Field<decimal>("Julio")).Sum();
                            decimal sumAgosto = (from acum in dt.AsEnumerable()
                                                 where acum.Field<string>("Linea") == item
                                                 select acum.Field<decimal>("Agosto")).Sum();
                            decimal sumSeptiembre = (from acum in dt.AsEnumerable()
                                                     where acum.Field<string>("Linea") == item
                                                     select acum.Field<decimal>("Septiembre")).Sum();
                            decimal sumOcutubre = (from acum in dt.AsEnumerable()
                                                   where acum.Field<string>("Linea") == item
                                                   select acum.Field<decimal>("Octubre")).Sum();
                            decimal sumNoviembre = (from acum in dt.AsEnumerable()
                                                    where acum.Field<string>("Linea") == item
                                                    select acum.Field<decimal>("Noviembre")).Sum();
                            decimal sumDiciembre = (from acum in dt.AsEnumerable()
                                                    where acum.Field<string>("Linea") == item
                                                    select acum.Field<decimal>("Diciembre")).Sum();

                            r["Enero"] = sumEnero;
                            r["Febrero"] = sumFebrero;
                            r["Marzo"] = sumMarzo;
                            r["Abril"] = sumAbril;
                            r["Mayo"] = sumMayo;
                            r["Junio"] = sumJunio;
                            r["Julio"] = sumJulio;
                            r["Agosto"] = sumAgosto;
                            r["Septiembre"] = sumSeptiembre;
                            r["Octubre"] = sumOcutubre;
                            r["Noviembre"] = sumNoviembre;
                            r["Diciembre"] = sumDiciembre;
                            r["Total"] = (sumEnero + sumFebrero + sumMarzo + sumAbril + sumMayo + sumJunio + sumJulio + sumAgosto + sumSeptiembre + sumOcutubre + sumNoviembre + sumDiciembre);
                            dt.Rows.Add(r);
                        }

                        if (query.Count() > 0)
                            dt = (from tv in dt.AsEnumerable()
                                  orderby tv.Field<string>("Linea")
                                  select tv).CopyToDataTable();

                        dgvDesglose.DataSource = dt;
                        this.formatoDesglose(dgvDesglose);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado.\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDesglose_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[(int)ColumnasDGVDesglose.Linea].Value.ToString().Contains("Total"))
                    {
                        item.DefaultCellStyle.Font = new Font("Arial", 9f, FontStyle.Bold);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgvDesglose_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                (sender as DataGridView).CurrentCell = null;
                DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                if (e.ColumnIndex == (int)ColumnasDGVDesglose.Linea) {
                    string valueSucursal = dgvDesglose.Rows[e.RowIndex].Cells[(int)ColumnasDGVDesglose.Linea].Value.ToString();
                    if (valueSucursal.Contains("Total"))
                    {
                        string[] splitSucursal = valueSucursal.Split(' ');
                        string sucursal = string.Empty;
                        for (int i = 0; i < splitSucursal.Length - 1; i++)
                        {
                            sucursal += " " + splitSucursal[i];
                        }

                        foreach (DataGridViewRow fila in dgvDesglose.Rows)
                        {
                            if (fila.Cells[(int)ColumnasDGVDesglose.Linea].Value == null)
                                continue;
                            if (fila.Cells[(int)ColumnasDGVDesglose.Linea].Value.ToString() == "")
                                continue;
                            if (fila.Cells[(int)ColumnasDGVDesglose.Linea].Value.ToString().Contains("Total"))
                                continue;
                            if (fila.Cells[(int)ColumnasDGVDesglose.Linea].Value.ToString() == sucursal.Trim())
                            {
                                fila.Visible = fila.Visible ? false : true;
                            }                        
                        }
                    }                
                }
            }
            catch (Exception ex) { 
            
            }
        }





    }
}
