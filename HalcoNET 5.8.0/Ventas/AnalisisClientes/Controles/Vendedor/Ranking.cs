using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.Sql;
using System.Data.SqlClient;


namespace Ventas.AnalisisClientes.Controles
{
    public partial class Ranking : UserControl
    {
        private DataTable _Table = new DataTable();

        public enum Columnas
        {
            PJ,
            Sucursal,
            Vendedor,
            Linea,
            Boton
        }

        public Ranking()
        {
            InitializeComponent();
        }

        private void CargarVendedores()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            {
                using (SqlCommand command = new SqlCommand(@"Select SlpCode Codigo, SlpName Nombre, 
                                                             Case When Memo = '01' Then 'Puebla'
                                                              When Memo = '02' Then 'Monterrey'
                                                              When Memo = '03' Then 'Apizaco'
                                                              When Memo = '05' Then 'Cordoba'
                                                              When Memo = '06' Then 'Tepeaca'
                                                              When Memo = '16' Then 'Estado de México'
                                                              When Memo = '18' Then 'Guadalajara' End Sucursal from OSLP Where Active = 'Y'
                                                              AND SlpCode not in (41, 53)
	                                                          AND SlpCode in (select SlpCode from OCRD where CardType = 'C')
	                                                          ORDER BY Nombre", connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = command;

                    DataTable table = new DataTable();
                    da.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = string.Empty;
                    row["Codigo"] = "0";
                    table.Rows.InsertAt(row, 0);

                    clbVendedor.DataSource = table;
                    clbVendedor.DisplayMember = "Nombre";
                    clbVendedor.ValueMember = "Codigo";

                }
            }

            //using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            //{
            //    using (SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", connection))
            //    {

            //        command.CommandType = CommandType.StoredProcedure;
            //        command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
            //        command.Parameters.AddWithValue("@Sucursal", string.Empty);
            //        command.Parameters.AddWithValue("@SlpCode", 0);
            //        DataTable table = new DataTable();
            //        SqlDataAdapter adapter = new SqlDataAdapter();
            //        adapter.SelectCommand = command;
            //        adapter.Fill(table);

            //        DataRow row = table.NewRow();
            //        row["Nombre"] = string.Empty;
            //        row["Codigo"] = "0";
            //        table.Rows.InsertAt(row, 0);

            //        clbVendedor.DataSource = table;
            //        clbVendedor.DisplayMember = "Nombre";
            //        clbVendedor.ValueMember = "Codigo";
            //    }
            //}
            
        }

        public void Formato()
        {
            DataGridViewButtonColumn buttonComent = new DataGridViewButtonColumn();
            {
                buttonComent.Name = "btnSeleccionar";
                buttonComent.HeaderText = "Seleccionar";
                buttonComent.Width = 100;
                buttonComent.UseColumnTextForButtonValue = true;
                buttonComent.FlatStyle = FlatStyle.Popup;
                //buttonComent.DisplayIndex = (int)ColumnasGrid.BtnComentarios;
            }

            dgvRanking.Columns.Add(buttonComent);
        }

        public DataTable PJ(DataTable _Datos)
        {
            try
            {

                decimal TotalPromedio = Convert.ToDecimal(_Datos.Compute("SUM(Promedio)", string.Empty));

                int x = 1;
                var list_PJ = (from item in _Datos.AsEnumerable()
                               group item by new
                               {
                                   ItmsGrpNam = item.Field<string>("ItmsGrpNam")
                               } into grouped
                               orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("Promedio"))) descending
                               select new
                               {
                                   Row = x++,
                                   Linea = grouped.Key.ItmsGrpNam,
                                   Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("Promedio"))),
                                   Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)(grouped.Sum(ix => ix.Field<decimal>("Promedio")) / TotalPromedio)
                               }
                                   ).ToList();//.OrderBy(d => d.Porcentaje1).Reverse();

                DataTable _DatosPJ = Clases.ListConverter.ToDataTable(list_PJ);

                return _DatosPJ;

            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        public DataTable DatosSucursal(DataTable table, DataTable _DatosPJ, string _Sucursal)
        {
            try
            {

                DataTable _TblSucursal = (from item in table.AsEnumerable()
                                          where item.Field<string>("GroupName").Contains(_Sucursal)
                                          select item).CopyToDataTable();

                decimal TotalPromedio = Convert.ToDecimal(_TblSucursal.Compute("sum(Promedio)", string.Empty));

                int x = 1;
                var list_Sucursal = (from item in _TblSucursal.AsEnumerable()
                                     group item by new
                                     {
                                         ItmsGrpNam = item.Field<string>("ItmsGrpNam")
                                     } into grouped
                                     orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("Promedio"))) descending
                                     select new
                                     {
                                         Row = x++,
                                         Linea = grouped.Key.ItmsGrpNam,
                                         Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("Promedio"))),
                                         Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3"))) / TotalPromedio
                                     }
                                   ).ToList();

                DataTable auxsuc = Clases.ListConverter.ToDataTable(list_Sucursal);
                int x2 = auxsuc.Rows.Count + 1;

                var LeftJoin = from pj in _DatosPJ.AsEnumerable()
                               join sucursal in auxsuc.AsEnumerable()
                               on pj.Field<string>("Linea") equals sucursal.Field<string>("Linea") into JoinedPJSucursal
                               from sucursal in JoinedPJSucursal.DefaultIfEmpty()
                               select new
                               {
                                   Row1 = pj.Field<int>("Row"),
                                   Linea = pj.Field<string>("Linea"),
                                   PromedioPJ = pj.Field<decimal>("Promedio"),
                                   PorcentajePJ = pj.Field<decimal>("Porcentaje1"),
                                   Row2 = sucursal != null ? sucursal.Field<int>("Row") : x2++,
                                   PromedioSucursal = sucursal != null ? sucursal.Field<decimal>("Promedio") : 0,
                                   PorcentajeSucursal = sucursal != null ? sucursal.Field<decimal>("Porcentaje1") : 0
                               };
                DataTable _DatosSucusal = Clases.ListConverter.ToDataTable(LeftJoin.ToList());

                return _DatosSucusal;
            }
            catch (Exception)
            {
                return new DataTable();
            }

        }

        public DataTable DatosVendedor(DataTable table, DataTable _DatosSucursal, string _Vendedor)
        {
            try
            {

                DataTable _TblVendor = (from item in table.AsEnumerable()
                                        where item.Field<string>("SlpName").Equals(_Vendedor)
                                        select item).CopyToDataTable();

                decimal TotalPromedio = Convert.ToDecimal(_TblVendor.Compute("sum(Promedio)", string.Empty));

                int x = 1;
                var list_Vendedor = (from item in _TblVendor.AsEnumerable()
                                     group item by new
                                     {
                                         ItmsGrpNam = item.Field<string>("ItmsGrpNam")
                                     } into grouped
                                     orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("Promedio"))) descending
                                     select new
                                     {
                                         Row = x++,
                                         Linea = grouped.Key.ItmsGrpNam,
                                         Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                         Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)(grouped.Sum(ix => ix.Field<decimal>("Promedio"))) / TotalPromedio
                                     }
                                   ).ToList();

                DataTable auxvendor = Clases.ListConverter.ToDataTable(list_Vendedor);
                int x2 = auxvendor.Rows.Count + 1;


                var LeftJoin = from sucursal in _DatosSucursal.AsEnumerable()
                               join vendedor in auxvendor.AsEnumerable()
                               on sucursal.Field<string>("Linea") equals vendedor.Field<string>("Linea") into JoinedPJSVendedor
                               from vendedor in JoinedPJSVendedor.DefaultIfEmpty()
                               select new
                               {
                                   PJ = sucursal.Field<Int32>("Row1"),
                                   Sucursal = sucursal.Field<Int32>("Row2"),
                                   Vendedor = vendedor != null ? vendedor.Field<Int32>("Row") : x2++,

                                   Linea = sucursal.Field<string>("Linea")

                                   /*PromedioPJ = sucursal.Field<decimal>("PromedioPJ"),
                                   PorcentajePJ = sucursal.Field<decimal>("PorcentajePJ"),
                                   
                                   PromedioSucursal = sucursal.Field<decimal>("PromedioSucursal"),
                                   PorcentajeSucursal = sucursal.Field<decimal>("PorcentajeSucursal"),
                                   
                                   PromedioVendedor = vendedor != null ? vendedor.Field<decimal>("Promedio") : 0,
                                   PorcentajeVendedor = vendedor != null ? vendedor.Field<decimal>("Porcentaje1") : 0*/
                               };

                DataTable ta = Clases.ListConverter.ToDataTable(LeftJoin.ToList());

                return (from it in ta.AsEnumerable()
                        orderby it.Field<Int32>("Vendedor")
                        select it).CopyToDataTable();
            }
            catch (Exception)
            {
                return new DataTable();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(clbVendedor.SelectedValue);

            dgvRanking.Columns.Clear();

            string sucursal = (from suc in (clbVendedor.DataSource as DataTable).AsEnumerable()
                               where suc.Field<Int32>("Codigo") == id
                               select suc.Field<string>("Sucursal")).FirstOrDefault();

            Clases.Contantes.Vendedor = id;

            dgvRanking.DataSource = this.DatosVendedor(_Table, this.DatosSucursal(_Table, this.PJ(_Table), sucursal), clbVendedor.Text);

            this.Formato();

        }

        private void Ranking_Load(object sender, EventArgs e)
        {
            try
            {
                this.CargarVendedores();

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_AnalisisVentas";

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Pregunta", 0);
                        command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                        command.Parameters.AddWithValue("@Letra", string.Empty);
                        command.Parameters.AddWithValue("@Especificacion", string.Empty);
                        command.Parameters.AddWithValue("@Linea", 0);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);

                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                        command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                        command.Parameters.AddWithValue("@Nombre", string.Empty);

                        command.CommandTimeout = 0;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(_Table);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Focus();
        }

        private void dgvRanking_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvRanking.Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dgvRanking.Rows[e.RowIndex].Cells["btnSeleccionar"] as DataGridViewButtonCell;
                Icon icoAtomico;

         

                    icoAtomico = Properties.Resources.miniarrow_right_blue;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left, e.CellBounds.Top);

                

                this.dgvRanking.Rows[e.RowIndex].Height = icoAtomico.Height + 2;
                this.dgvRanking.Columns[e.ColumnIndex].Width = icoAtomico.Width + 2;

                e.Handled = true;
            }
        }

        private void dgvRanking_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)Columnas.Boton)
                {
                    string Linea = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Linea].Value);
                    Clases.Contantes.Linea = Linea;

                    Controles.form1 f1 = new form1(clbVendedor.Text, Linea, _Table);
                    f1.Dock = DockStyle.Fill;
                    f1.Parent = this.Parent;
                    f1.BringToFront();
                    f1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
