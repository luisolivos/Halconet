using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compras
{
    public partial class frmNuevoCompras : Form
    {
        DataTable Tbl_Stock = new DataTable();
        DataTable Tbl_Ventas = new DataTable();
        List<string> FiltroLineas = new List<string>();

        public enum ColumasGrid1
        {
            ID, 
            Linea,
            Stock,
            Ideal,
            Diferencia
        }

        public enum ColumasGrid2
        {
            Articulo,
            Descripcion, 
            Meses,
            Ideal, 
            Stock, 
            Diferencia, 
            Venta, 
        }

        public enum ColumasGrid3
        {
            Almacen,
            Ideal,
            Stock,
            Diferencia,
            Venta 
        }

        public void Formato1(DataGridView dgv )
        {
            dgv.Columns[(int)ColumasGrid1.ID].Visible = false;

            dgv.Columns[(int)ColumasGrid1.Linea].Width = 100;
            dgv.Columns[(int)ColumasGrid1.Ideal].Width = 100;
            dgv.Columns[(int)ColumasGrid1.Stock].Width = 100;
            dgv.Columns[(int)ColumasGrid1.Diferencia].Width = 100;

            dgv.Columns[(int)ColumasGrid1.Ideal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasGrid1.Stock].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasGrid1.Diferencia].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumasGrid1.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasGrid1.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasGrid1.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public void Formato2(DataGridView dgv)
        {

            dgv.Columns[(int)ColumasGrid2.Articulo].Width = 100;
            dgv.Columns[(int)ColumasGrid2.Descripcion].Width = 250;
            dgv.Columns[(int)ColumasGrid2.Ideal].Width = 100;
            dgv.Columns[(int)ColumasGrid2.Stock].Width = 100;
            dgv.Columns[(int)ColumasGrid2.Diferencia].Width = 100;

            dgv.Columns[(int)ColumasGrid2.Ideal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasGrid2.Stock].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasGrid2.Diferencia].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasGrid2.Venta].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumasGrid2.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasGrid2.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasGrid2.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasGrid2.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void Formato3(DataGridView dgv)
        {

            dgv.Columns[(int)ColumasGrid3.Almacen].Width = 100;
            dgv.Columns[(int)ColumasGrid3.Ideal].Width = 100;
            dgv.Columns[(int)ColumasGrid3.Stock].Width = 100;
            dgv.Columns[(int)ColumasGrid3.Diferencia].Width = 100;

            dgv.Columns[(int)ColumasGrid3.Ideal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasGrid3.Stock].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasGrid3.Diferencia].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasGrid3.Venta].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumasGrid3.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasGrid3.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasGrid3.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasGrid3.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void CargarLinea(CheckedListBox _cb, string _inicio)
        {
            using (SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ)))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 2);
                command.Parameters.AddWithValue("@Articulo", string.Empty);
                command.Parameters.AddWithValue("@Linea", 0);
                command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
                command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
                command.Parameters.AddWithValue("@Proveedor", 0);

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
        }

        public List<string> GetCadena(CheckedListBox cb)
        {
            List<string> listReturn = new List<string>();

            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in cb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!cb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                    listReturn.Add(item["Nombre"].ToString());
                }
            }
            if (cb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in cb.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!cb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append(item["Codigo"].ToString());
                        listReturn.Add(item["Nombre"].ToString());
                    }
                }
            }
            return listReturn;
        }

        public frmNuevoCompras()
        {
            InitializeComponent();
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            try
            {
                FiltroLineas = this.GetCadena(clbLinea);
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_Nuevo", connection))
                    {
                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                       // command.Parameters.AddWithValue("@Lineas", Lineas);

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(Tbl_Stock);

                    }
                }
               

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_Nuevo", connection))
                    {
                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@Desde", dtDesde.Value);
                        command.Parameters.AddWithValue("@Hasta", dtHasta.Value);
                        

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(Tbl_Ventas);

                    }
                }

                var list_Lineas = (from item in Tbl_Stock.AsEnumerable()
                                   where FiltroLineas.Contains(item.Field<string>("Linea"))
                                   select  new 
                                   {
                                       ID = item.Field<Int16>("ItmsGrpCod"),
                                       Linea = item.Field<string>("Linea"),
                                       IdealM = item.Field<decimal?>("Ideal") * item.Field<decimal?>("AvgPrice"),
                                       StockM = item.Field<decimal?>("Stock") * item.Field<decimal?>("AvgPrice")
                                   });

                var tbl_Final = (from item in  list_Lineas
                                 group item by new
                                 {
                                     ID = item.ID,
                                     Linea = item.Linea
                                 } into grouped
                                 select new 
                                 {
                                     ID = grouped.Key.ID,
                                     Linea = grouped.Key.Linea,
                                     Ideal = grouped.Sum(ix => ix.IdealM),
                                     Stock = grouped.Sum(ix => ix.StockM),
                                     Diferencia = grouped.Sum(ix => ix.IdealM) - grouped.Sum(ix => ix.StockM)
                                 }).ToList();

                dgvLinea.DataSource = Cobranza.ListConverter.ToDataTable(tbl_Final.ToList());
                this.Formato1(dgvLinea);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLinea_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var list_Articulos = from item in Tbl_Stock.AsEnumerable()
                                     where item.Field<Int16>("ItmsGrpCod") == Convert.ToInt16((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumasGrid1.ID].Value)
                                     select new
                                     {
                                         Articulo = item.Field<string>("Articulo"),
                                         Descripcion = item.Field<string>("Descripcion"),
                                         Meses = item.Field<int>("Meses"),
                                         Ideal = item.Field<decimal>("Ideal") * item.Field<decimal>("AvgPrice"),
                                         Stock = item.Field<decimal>("Stock") * item.Field<decimal>("AvgPrice")
                                     };

                var list_Articulos1 = (from item in list_Articulos
                                       group item by new
                                       {
                                           Articulo = item.Articulo,
                                           Descripcion = item.Descripcion,
                                           Meses = item.Meses
                                       } into grouped
                                       select new
                                       {
                                           Articulo = grouped.Key.Articulo,
                                           Descripcion = grouped.Key.Descripcion,
                                           Meses = grouped.Key.Meses,
                                           Ideal = grouped.Sum(ix => ix.Ideal),
                                           Stock = grouped.Sum(ix => ix.Stock),
                                           Diferencia = grouped.Sum(ix => ix.Ideal) - grouped.Sum(ix => ix.Stock)
                                       });

                var list_Articulos2 = from t1 in list_Articulos1
                                join t2 in Tbl_Ventas.AsEnumerable()
                                    on t1.Articulo equals t2.Field<string>("ItemCode") into Joined
                                from venta in Joined.DefaultIfEmpty()
                                select new
                                {
                                    Articulo = t1.Articulo,
                                    Descripcion = t1.Descripcion,
                                    Meses = t1.Meses,
                                    Ideal = t1.Ideal,
                                    Stock = t1.Stock,
                                    Diferencia = t1.Diferencia,
                                    Venta = venta != null ? venta.Field<decimal>("Venta") : 0
                                };

                var tbl_Final = from item in list_Articulos2
                                group item by new
                                {
                                    Articulo = item.Articulo,
                                    Descripcion = item.Descripcion,
                                    Meses = item.Meses,
                                    Ideal = item.Ideal,
                                    Stock = item.Stock,
                                    Diferencia = item.Diferencia
                                } into grouped
                                select new
                                {
                                    Articulo = grouped.Key.Articulo,
                                    Descripcion = grouped.Key.Descripcion,
                                    Meses = grouped.Key.Meses,
                                    Ideal = grouped.Key.Ideal,
                                    Stock = grouped.Key.Stock,
                                    Diferencia = grouped.Key.Diferencia,
                                    Venta = grouped.Average(ix => ix.Venta)
                                };

                dgvArticulos.DataSource = Cobranza.ListConverter.ToDataTable(tbl_Final.ToList());
                this.Formato2(dgvArticulos);
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvArticulos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var list_Almacenes = from item in Tbl_Stock.AsEnumerable()
                                     where item.Field<string>("Articulo") == Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumasGrid2.Articulo].Value)
                                     select new
                                     {
                                         Almacen = item.Field<string>("Almacen"),
                                         Ideal = item.Field<decimal>("Ideal") * item.Field<decimal>("AvgPrice"),
                                         Stock = item.Field<decimal>("Stock") * item.Field<decimal>("AvgPrice")
                                     };

                var list_Almacenes1 = (from item in list_Almacenes
                                       group item by new
                                       {
                                           Almacen = item.Almacen,
                                       } into grouped
                                       select new
                                       {
                                           Almacen = grouped.Key.Almacen,
                                           Ideal = grouped.Sum(ix => ix.Ideal),
                                           Stock = grouped.Sum(ix => ix.Stock),
                                           Diferencia = grouped.Sum(ix => ix.Ideal) - grouped.Sum(ix => ix.Stock)
                                       });

                DataTable Tbl_VtsSucursal = new DataTable();
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_Nuevo", connection))
                    {
                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Desde", dtDesde.Value);
                        command.Parameters.AddWithValue("@Hasta", dtHasta.Value);
                        command.Parameters.AddWithValue("@Articulo", Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumasGrid2.Articulo].Value));

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(Tbl_VtsSucursal);

                    }
                }

                var Tbl_final = from t1 in list_Almacenes1
                                join t2 in Tbl_VtsSucursal.AsEnumerable()
                                           on t1.Almacen equals t2.Field<string>("WhsName") into Joined
                                from venta in Joined.DefaultIfEmpty()
                                select new
                                {
                                    Almacen = t1.Almacen,
                                    Ideal = t1.Ideal,
                                    Stock = t1.Stock,
                                    Diferencia = t1.Diferencia,
                                    Venta = venta != null ? venta.Field<decimal>("Venta") : 0
                                };

                dgvSucursales.DataSource = Cobranza.ListConverter.ToDataTable(Tbl_final.ToList());
                this.Formato3(dgvSucursales);
            }
            catch (Exception)
            {
            }
        }

        private void NuevoCompras_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.CargarLinea(clbLinea, "Todas");
        }

        private void clbLinea_Click(object sender, EventArgs e)
        {
            if (clbLinea.SelectedIndex == 0)
            {
                if (clbLinea.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbLinea.Items.Count; item++)
                    {
                        clbLinea.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbLinea.Items.Count; item++)
                    {
                        clbLinea.SetItemChecked(item, true);
                    }
                }
            }
        }
    }
}
