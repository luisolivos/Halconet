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

namespace Ventas
{
    public partial class CosultaCostoBase : Form
    {
        Clases.Logs log;
        #region PARAMETROS
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        public int Rol;
        public string Usuario;
        public enum ColumnasGrid
        {
            Articulo, Descripcion, Linea, PrecioPesos, MonedaPesos, PrecioUSD, MonedaUSD, Precio, UtilidadPorcentaje, Porcentaje, UtilidadCantidad
        }

        #endregion
        public CosultaCostoBase(int rol, string username)
        {
            Rol = rol;
            Usuario = username;
            InitializeComponent();
        }
        #region METODOS
        private void CargarLinea()
        {
            SqlCommand command = new SqlCommand("PJ_Ventas", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 17);
            command.Parameters.AddWithValue("@Vendedores", string.Empty);
            command.Parameters.AddWithValue("@Lineas", string.Empty);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", string.Empty);
            command.Parameters.AddWithValue("@FechaFinal", string.Empty);
            command.Parameters.AddWithValue("@Factura", string.Empty);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@GranCanales", string.Empty);
            command.Parameters.AddWithValue("@Canales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@Mes", string.Empty);
            command.Parameters.AddWithValue("@Anio", string.Empty);
            command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
            command.Parameters.AddWithValue("@Precio", Rol);
            command.Parameters.AddWithValue("@Moneda", string.Empty);
            
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);
            
            cmbLinea.DataSource = table;
            cmbLinea.DisplayMember = "Nombre";
            cmbLinea.ValueMember = "Codigo";
        }

        private void CargarArticulos()
        {
            String Linea = "";
            Linea = cmbLinea.Text;

            //MessageBox.Show(Linea);
            SqlCommand command = new SqlCommand("PJ_Ventas", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 14);
            command.Parameters.AddWithValue("@Vendedores", string.Empty);
            command.Parameters.AddWithValue("@Lineas", string.Empty);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", string.Empty);
            command.Parameters.AddWithValue("@FechaFinal", string.Empty);
            command.Parameters.AddWithValue("@Factura", string.Empty);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@GranCanales", string.Empty);
            command.Parameters.AddWithValue("@Canales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@Mes", string.Empty);
            command.Parameters.AddWithValue("@Anio", string.Empty);
            command.Parameters.AddWithValue("@NombreArticulo", Linea);
            command.Parameters.AddWithValue("@Precio", Rol);
            command.Parameters.AddWithValue("@Moneda", string.Empty);
            try
            {
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                //var stringArr = table.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                //txtArticulo.AutoCompleteCustomSource =  
                var source = new AutoCompleteStringCollection();
                source.AddRange(table.Rows[0].ItemArray.Select(x => x.ToString()).ToArray());
                source.AddRange(Array.ConvertAll<DataRow, String>(table.Select(), delegate(DataRow row) { return (String)row[0]; }));
                txtArticulo.AutoCompleteCustomSource = source;
                txtArticulo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception)
            {
            }
        }

        private void DarformatoGrid()
        {
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Articulo].Width = 85;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Articulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Articulo].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.Descripcion].Width = 200;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Descripcion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Descripcion].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.Linea].Width = 70;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Linea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Linea].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioPesos].Width = 90;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioPesos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioPesos].DefaultCellStyle.Format = "C4";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioPesos].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaPesos].Width = 60;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaPesos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaPesos].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioUSD].Width = 90;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioUSD].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioUSD].DefaultCellStyle.Format = "C4";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioUSD].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaUSD].Width = 60;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaUSD].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaUSD].HeaderText = "Moneda";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaUSD].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].Width = 90;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].DefaultCellStyle.Format = "C4";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].DefaultCellStyle.NullValue = "0";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].DefaultCellStyle.BackColor = Color.FromArgb(245,245,245);
           // dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].DefaultCellStyle.Format = "C4""

            // Color.FromArgb(0,0,0);
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadPorcentaje].Width = 90;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadPorcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadPorcentaje].DefaultCellStyle.Format = "P4";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadPorcentaje].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.Porcentaje].Width = 90;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Porcentaje].DefaultCellStyle.Format = "P4";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Porcentaje].DefaultCellStyle.NullValue = "0";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Porcentaje].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadCantidad].Width = 90;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadCantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadCantidad].DefaultCellStyle.Format = "C4";
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadCantidad].ReadOnly = true;

            dgvCostosOriginales.Columns[(int)ColumnasGrid.Articulo].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Descripcion].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Linea].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioPesos].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaPesos].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioUSD].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaUSD].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadPorcentaje].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Porcentaje].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.UtilidadCantidad].SortMode = DataGridViewColumnSortMode.NotSortable;

            /// PERMISOS
            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas)
            {
                dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioPesos].Visible = true;
                dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaPesos].Visible = true;
                dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioUSD].Visible = true;
                dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaUSD].Visible = true;
            }
            else
            {
                dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioPesos].Visible = false;
                dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaPesos].Visible = false;
                dgvCostosOriginales.Columns[(int)ColumnasGrid.PrecioUSD].Visible = false;
                dgvCostosOriginales.Columns[(int)ColumnasGrid.MonedaUSD].Visible = false;
            }

        }

        private void FormatoCeldas()
        {
            string linea = "";
            foreach (DataGridViewRow item in dgvCostosOriginales.Rows)
            {
                
                decimal fac = 0;
                try
                {
                    string aux = Convert.ToString(item.Cells[(int)ColumnasGrid.Linea].Value).Trim();
                    
                    if (aux != linea)
                    {
                        linea = aux; 
                        var query = (from factor in tblLineas.AsEnumerable()
                                     where (factor.Field<string>("Linea") == linea)
                                     select (decimal)factor.Field<decimal>("Factor"));

                        if (query.Count() > 0)
                        {
                            fac = query.Distinct().Single();
                        }
                    }
                    if (Convert.ToDecimal(item.Cells[(int)ColumnasGrid.UtilidadPorcentaje].Value) < fac && Convert.ToDecimal(item.Cells[(int)ColumnasGrid.UtilidadPorcentaje].Value) != 0)
                    {
                        item.Cells[(int)ColumnasGrid.UtilidadPorcentaje].Style.BackColor = Color.Red;
                        item.Cells[(int)ColumnasGrid.UtilidadPorcentaje].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)ColumnasGrid.UtilidadPorcentaje].Style.BackColor = Color.White;
                        item.Cells[(int)ColumnasGrid.UtilidadPorcentaje].Style.ForeColor = Color.Black;
                    }
                   
                    if (Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Porcentaje].Value) < fac && Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Porcentaje].Value) != 0)
                    {
                        item.Cells[(int)ColumnasGrid.UtilidadCantidad].Style.BackColor = Color.Red;
                        item.Cells[(int)ColumnasGrid.UtilidadCantidad].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)ColumnasGrid.UtilidadCantidad].Style.BackColor = Color.White;
                        item.Cells[(int)ColumnasGrid.UtilidadCantidad].Style.ForeColor = Color.Black;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
        private void Esperar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region EVENTOS
        private void cmbLinea_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            CargarArticulos();
        }
        DataTable tblLineas = new DataTable();
        private void btnCargar_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            string Linea = cmbLinea.Text.ToString().Trim();
            string Articulo = txtArticulo.Text;

            SqlCommand command = new SqlCommand("PJ_Ventas", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 15);
            command.Parameters.AddWithValue("@Vendedores", string.Empty);
            command.Parameters.AddWithValue("@Lineas", string.Empty);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@Articulo", Articulo);
            command.Parameters.AddWithValue("@FechaInicial", string.Empty);
            command.Parameters.AddWithValue("@FechaFinal", string.Empty);
            command.Parameters.AddWithValue("@Factura", string.Empty);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@GranCanales", string.Empty);
            command.Parameters.AddWithValue("@Canales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@Mes", string.Empty);
            command.Parameters.AddWithValue("@Anio", string.Empty);
            command.Parameters.AddWithValue("@NombreArticulo", Linea);
            command.Parameters.AddWithValue("@Precio", Rol);
            command.Parameters.AddWithValue("@Moneda", string.Empty);
            try
            {
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                DataColumn r1 = new DataColumn();

                table.Columns.Add("Precio", typeof(decimal));
                table.Columns["Precio"].DefaultValue = (decimal)0;
                table.Columns.Add("Utilidad sobre venta", typeof(decimal));
                table.Columns["Utilidad sobre venta"].DefaultValue = (decimal)0;
                table.Columns.Add("Porcentaje(%)", typeof(decimal));
                table.Columns["Porcentaje(%)"].DefaultValue = 0;
                table.Columns.Add("Precio sugerido", typeof(decimal));
                table.Columns["Precio sugerido"].DefaultValue = (decimal)0;

                foreach (DataRow item in table.Rows)
                {
                    item["Precio"] = (decimal)0;
                    item["Utilidad sobre venta"] = (decimal)0;
                    item["Porcentaje(%)"] = (decimal)0;
                    item["Precio sugerido"] = (decimal)0;
                }

                SqlCommand command1 = new SqlCommand("PJ_ConsultasVarias", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", 11);

                
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                adapter1.SelectCommand = command1;
                adapter1.Fill(tblLineas);


                dgvCostosOriginales.DataSource = table;
                DarformatoGrid();
            }
            catch (Exception)
            {
            }

        }


        private void txtArticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCargar_Click(sender, e);
            }

        }
        
        private void CosultaCostoBase_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            CargarLinea();         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Esperar();  
            foreach (DataGridViewRow fila in dgvCostosOriginales.Rows)
            {
                try
                {
                    decimal PrecioCompra = 0;
                    decimal Pesos = 0;
                    decimal Porcentaje = 0;

                    if (rbPesos.Checked)
                    {
                        PrecioCompra = Convert.ToDecimal(fila.Cells[(int)ColumnasGrid.PrecioPesos].Value);

                    }
                    else
                        PrecioCompra = Convert.ToDecimal(fila.Cells[(int)ColumnasGrid.PrecioUSD].Value);

                    Pesos = Convert.ToDecimal(fila.Cells[(int)ColumnasGrid.Precio].Value);

                    Porcentaje = Convert.ToDecimal(fila.Cells[(int)ColumnasGrid.Porcentaje].Value) * 100;


                    if (Pesos != 0)
                    {
                        fila.Cells[(int)ColumnasGrid.UtilidadPorcentaje].Value = (((PrecioCompra / Pesos) - 1) * -100) / 100;
                        //fila.Cells[(int)ColumnasGrid.UtilidadPorcentaje].Style.Format = "P2";
                    }
                    if (Porcentaje != 0)
                    {

                        fila.Cells[(int)ColumnasGrid.UtilidadCantidad].Value = (PrecioCompra / (100 - Porcentaje)) * 100;
                        //fila.Cells[(int)ColumnasGrid.UtilidadCantidad].Style.Format = "C2";
                    }
                }
                catch (Exception) { }

            }
            //FormatoCeldas();
            Continuar();
        }
        #endregion

        private void dgvCostosOriginales_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
           // FormatoCeldas();
            
        }

        private void dgvCostosOriginales_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            FormatoCeldas();
        }

        private void CosultaCostoBase_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void CosultaCostoBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
            }
        }

        private void txtArticulo_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
