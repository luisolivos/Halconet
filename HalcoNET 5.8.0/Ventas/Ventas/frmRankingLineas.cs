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
using System.Globalization;

namespace Ventas.Ventas
{
    public partial class frmRankingLineas : Form
    {
        private string qryDiasMes = "select cast(DBO.DiasHabiles(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)), DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)), 1) as decimal(10,2));";
        private string qryDiasTra = "select cast(DBO.DiasHabiles(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)), GETDATE(), 1) as decimal(10,2));";

        private decimal Opcion = 0;
        private decimal TotalPromedio;
        private decimal TotalPRonostico;

        private int RolUsuario;
        private int CodigoVendedor;
        private string Sucursal;
        private string NombreUsuario;
        Clases.Logs log;
        private enum ColumnasPJ
        {
            Ranking,
            Linea,
            Promedio,
            Porcentaje,
            Pronostico,
            Porcentaje2
        }

        private enum ColumnasSucursal
        {
            RankingPJ,
            Linea,
            PromedioPJ,
            PorcentajePJ,
            RankingSucursal,
            PromedioSucursal,
            PorcentajeSucursal,
            PronosticoSucursal,
            ProrcentajePronostico
        }

        private enum ColumnasVendedor
        {
            Ranking1,
            Linea,
            PromedioPJ,
            PorcentajePJ,
            Rankin2,
            PromedioSucursal,
            PorcentajeSucursal,
            Ranking3,
            PromedioVendedor,
            PorcentajeVendedor,
            PronosticoVendedor,
            PorcentajePronostico
        }

        private enum ColumnasClientes
        {
            Ranking,
            Cliente,
            Nombre,
            Sucursal,
            Promedio,
            Porcentaje,
            Pronostico,
            Porcentaje2
        }

        private DataTable _Datos = new DataTable();

        private DataTable _DatosPJ = new DataTable();
        private DataTable _DatosSucusal = new DataTable();
        private DataTable _DatosVendedor = new DataTable();
        private DataTable TBLVendedores = new DataTable();

        public frmRankingLineas(int _rol, int _vendor, string _sucursal, string usuario)
        {
            InitializeComponent();

            RolUsuario = _rol;
            CodigoVendedor = _vendor;
            Sucursal = _sucursal;
            NombreUsuario = usuario;
        }

        private decimal GetDias(string qry)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionPJ;
                    using (SqlCommand command = new SqlCommand())
                    {
                        
                        command.CommandText = qry;
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 0;
                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                            return reader.GetDecimal(0);

                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }

        private void FormatoPJ()
        {
            string mes1 = this.MonthName(DateTime.Now.Month);
            string mes2 = this.MonthName(DateTime.Now.AddMonths(-1).Month);
            string mes3 = this.MonthName(DateTime.Now.AddMonths(-2).Month);

            dataGridView1.Columns[(int)ColumnasPJ.Ranking].Width = 50;
            dataGridView1.Columns[(int)ColumnasPJ.Linea].Width = 80;
            dataGridView1.Columns[(int)ColumnasPJ.Promedio].Width = 120;
            dataGridView1.Columns[(int)ColumnasPJ.Porcentaje].Width = 120;
            dataGridView1.Columns[(int)ColumnasPJ.Pronostico].Width = 120;
            dataGridView1.Columns[(int)ColumnasPJ.Porcentaje2].Width = 120;

            dataGridView1.Columns[(int)ColumnasPJ.Ranking].HeaderText = "Ranking";
            dataGridView1.Columns[(int)ColumnasPJ.Linea].HeaderText = "Línea";
            dataGridView1.Columns[(int)ColumnasPJ.Promedio].HeaderText = "Promedio " + mes2 + " - " + mes3;
            dataGridView1.Columns[(int)ColumnasPJ.Porcentaje].HeaderText = "%ind";
            dataGridView1.Columns[(int)ColumnasPJ.Pronostico].HeaderText = "Pronostico " + mes1;
            dataGridView1.Columns[(int)ColumnasPJ.Porcentaje2].HeaderText = "%ind";

            dataGridView1.Columns[(int)ColumnasPJ.Promedio].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasPJ.Porcentaje].DefaultCellStyle.Format = "P1";
            dataGridView1.Columns[(int)ColumnasPJ.Pronostico].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasPJ.Porcentaje2].DefaultCellStyle.Format = "P1";

            dataGridView1.Columns[(int)ColumnasPJ.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasPJ.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasPJ.Pronostico].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasPJ.Porcentaje2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void FormatoSucursal()
        {
            string mes1 = this.MonthName(DateTime.Now.Month);
            string mes2 = this.MonthName(DateTime.Now.AddMonths(-1).Month);
            string mes3 = this.MonthName(DateTime.Now.AddMonths(-2).Month);

            dataGridView1.Columns[(int)ColumnasSucursal.RankingPJ].Width = 70;
            dataGridView1.Columns[(int)ColumnasSucursal.RankingSucursal].Width = 70;
            dataGridView1.Columns[(int)ColumnasSucursal.Linea].Width = 80;
            dataGridView1.Columns[(int)ColumnasSucursal.PromedioPJ].Width = 100;
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajePJ].Width = 100;
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajeSucursal].Width = 100;
            dataGridView1.Columns[(int)ColumnasSucursal.PromedioSucursal].Width = 100;
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajeSucursal].Width = 100;
            dataGridView1.Columns[(int)ColumnasSucursal.PronosticoSucursal].Width = 100;
            dataGridView1.Columns[(int)ColumnasSucursal.ProrcentajePronostico].Width = 100;

            dataGridView1.Columns[(int)ColumnasSucursal.RankingPJ].HeaderText = "Ranking PJ";
            dataGridView1.Columns[(int)ColumnasSucursal.RankingSucursal].HeaderText = "Ranking " + Sucursal;
            dataGridView1.Columns[(int)ColumnasSucursal.Linea].HeaderText = "Línea";
            dataGridView1.Columns[(int)ColumnasSucursal.PromedioPJ].HeaderText = "Promedio PJ " + mes2 + " - " + mes3;
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajePJ].HeaderText = "% ind PJ";
            dataGridView1.Columns[(int)ColumnasSucursal.PromedioSucursal].HeaderText = "Promedio " +  Sucursal + " " + mes2 + " - " + mes3; 
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajeSucursal].HeaderText = "% ind";
            dataGridView1.Columns[(int)ColumnasSucursal.PronosticoSucursal].HeaderText = "Pronostico " + Sucursal + " " + mes1;
            dataGridView1.Columns[(int)ColumnasSucursal.ProrcentajePronostico].HeaderText = "% ind";

            dataGridView1.Columns[(int)ColumnasSucursal.PromedioPJ].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajePJ].DefaultCellStyle.Format = "P1";
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajeSucursal].DefaultCellStyle.Format = "P1";
            dataGridView1.Columns[(int)ColumnasSucursal.PromedioSucursal].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajeSucursal].DefaultCellStyle.Format = "P1";
            dataGridView1.Columns[(int)ColumnasSucursal.PronosticoSucursal].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasSucursal.ProrcentajePronostico].DefaultCellStyle.Format = "P1";

            dataGridView1.Columns[(int)ColumnasSucursal.PromedioPJ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajePJ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajeSucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasSucursal.PromedioSucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasSucursal.PorcentajeSucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasSucursal.PronosticoSucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasSucursal.ProrcentajePronostico].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void FormatoVendedor()
        {
            string mes1 = this.MonthName(DateTime.Now.Month);
            string mes2 = this.MonthName(DateTime.Now.AddMonths(-1).Month);
            string mes3 = this.MonthName(DateTime.Now.AddMonths(-2).Month);

            dataGridView1.Columns[(int)ColumnasVendedor.Ranking1].Width = 70;
            dataGridView1.Columns[(int)ColumnasVendedor.Rankin2].Width = 70;
            dataGridView1.Columns[(int)ColumnasVendedor.Ranking3].Width = 70;

            dataGridView1.Columns[(int)ColumnasVendedor.Linea].Width = 80;
            dataGridView1.Columns[(int)ColumnasVendedor.PromedioPJ].Width = 100;
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePJ].Width = 100;
            dataGridView1.Columns[(int)ColumnasVendedor.PromedioSucursal].Width = 100;
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeSucursal].Width = 100;
            dataGridView1.Columns[(int)ColumnasVendedor.PromedioVendedor].Width = 100;
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeVendedor].Width = 100;
            dataGridView1.Columns[(int)ColumnasVendedor.PronosticoVendedor].Width = 100;
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePronostico].Width = 100;

            if (Opcion == 3)
            {
                dataGridView1.Columns[(int)ColumnasVendedor.Ranking1].HeaderText = "Ranking PJ";
                dataGridView1.Columns[(int)ColumnasVendedor.Linea].HeaderText = "Línea";
                dataGridView1.Columns[(int)ColumnasVendedor.PromedioPJ].HeaderText = "Promedio PJ " + mes2 + " - " + mes3;
                dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePJ].HeaderText = "% ind PJ";
                dataGridView1.Columns[(int)ColumnasVendedor.Rankin2].HeaderText = "Ranking " + clbSucursal.Text;
                dataGridView1.Columns[(int)ColumnasVendedor.PromedioSucursal].HeaderText = "Promedio " + clbSucursal.Text + " " + mes2 + " - " + mes3;
                dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeSucursal].HeaderText = "% ind";
                dataGridView1.Columns[(int)ColumnasVendedor.Ranking3].HeaderText = "Ranking " + clbVendedor.Text;
                dataGridView1.Columns[(int)ColumnasVendedor.PromedioVendedor].HeaderText = "Promedio " + clbVendedor.Text + " " + mes2 + " - " + mes3;
                dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeVendedor].HeaderText = "% ind";
                dataGridView1.Columns[(int)ColumnasVendedor.PronosticoVendedor].HeaderText = "Pronostico " + clbVendedor.Text + " " + mes1;
                dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePronostico].HeaderText = "% ind";
            }
            else if (Opcion == 4)
            {
                dataGridView1.Columns[(int)ColumnasVendedor.Ranking1].HeaderText = "Ranking PJ";
                dataGridView1.Columns[(int)ColumnasVendedor.Linea].HeaderText = "Línea";
                dataGridView1.Columns[(int)ColumnasVendedor.PromedioPJ].HeaderText = "Promedio PJ " + mes2 + " - " + mes3;
                dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePJ].HeaderText = "% ind PJ";
                dataGridView1.Columns[(int)ColumnasVendedor.Rankin2].HeaderText = "Ranking " + clbSucursal.Text;
                dataGridView1.Columns[(int)ColumnasVendedor.PromedioSucursal].HeaderText = "Promedio " + clbSucursal.Text + " " + mes2 + " - " + mes3;
                dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeSucursal].HeaderText = "% ind";
                dataGridView1.Columns[(int)ColumnasVendedor.Ranking3].HeaderText = "Ranking " + cbCanal.Text;
                dataGridView1.Columns[(int)ColumnasVendedor.PromedioVendedor].HeaderText = "Promedio " + cbCanal.Text + " " + mes2 + " - " + mes3;
                dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeVendedor].HeaderText = "% ind";
                dataGridView1.Columns[(int)ColumnasVendedor.PronosticoVendedor].HeaderText = "Pronostico " + cbCanal.Text + " " + mes1;
                dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePronostico].HeaderText = "% ind";
            }
            dataGridView1.Columns[(int)ColumnasVendedor.PromedioPJ].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePJ].DefaultCellStyle.Format = "P1";
            dataGridView1.Columns[(int)ColumnasVendedor.PromedioSucursal].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeSucursal].DefaultCellStyle.Format = "P1";
            dataGridView1.Columns[(int)ColumnasVendedor.PromedioVendedor].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeVendedor].DefaultCellStyle.Format = "P1";
            dataGridView1.Columns[(int)ColumnasVendedor.PronosticoVendedor].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePronostico].DefaultCellStyle.Format = "P1";

            dataGridView1.Columns[(int)ColumnasVendedor.PromedioPJ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePJ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasVendedor.PromedioSucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeSucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasVendedor.PromedioVendedor].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajeVendedor].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasVendedor.PronosticoVendedor].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasVendedor.PorcentajePronostico].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void FormatoClientes()
        {
            string mes1 = this.MonthName(DateTime.Now.Month);
            string mes2 = this.MonthName(DateTime.Now.AddMonths(-1).Month);
            string mes3 = this.MonthName(DateTime.Now.AddMonths(-2).Month);

            dataGridView2.Columns[(int)ColumnasClientes.Ranking].Width = 50;
            dataGridView2.Columns[(int)ColumnasClientes.Cliente].Width = 90;
            dataGridView2.Columns[(int)ColumnasClientes.Nombre].Width = 200;
            dataGridView2.Columns[(int)ColumnasClientes.Promedio].Width = 120;
            dataGridView2.Columns[(int)ColumnasClientes.Porcentaje].Width = 120;
            dataGridView2.Columns[(int)ColumnasClientes.Pronostico].Width = 120;
            dataGridView2.Columns[(int)ColumnasClientes.Porcentaje2].Width = 120;

            dataGridView2.Columns[(int)ColumnasClientes.Ranking].HeaderText = "Ranking";
            dataGridView2.Columns[(int)ColumnasClientes.Cliente].HeaderText = "Cliente";
            dataGridView2.Columns[(int)ColumnasClientes.Nombre].HeaderText = "Nombre del cliente";
            dataGridView2.Columns[(int)ColumnasClientes.Promedio].HeaderText = "Promedio " + mes2 + " - " + mes3;
            dataGridView2.Columns[(int)ColumnasClientes.Porcentaje].HeaderText = "%ind";
            dataGridView2.Columns[(int)ColumnasClientes.Pronostico].HeaderText = "Pronostico " + mes1;
            dataGridView2.Columns[(int)ColumnasClientes.Porcentaje2].HeaderText = "%ind";

            dataGridView2.Columns[(int)ColumnasClientes.Promedio].DefaultCellStyle.Format = "C2";
            dataGridView2.Columns[(int)ColumnasClientes.Porcentaje].DefaultCellStyle.Format = "P1";
            dataGridView2.Columns[(int)ColumnasClientes.Pronostico].DefaultCellStyle.Format = "C2";
            dataGridView2.Columns[(int)ColumnasClientes.Porcentaje2].DefaultCellStyle.Format = "P1";

            dataGridView2.Columns[(int)ColumnasClientes.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[(int)ColumnasClientes.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[(int)ColumnasClientes.Pronostico].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[(int)ColumnasClientes.Porcentaje2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dataGridView2.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void CargarSucursales()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            clbSucursal.DataSource = table;
            clbSucursal.DisplayMember = "Nombre";
            clbSucursal.ValueMember = "Codigo";
        }

        private void CargarVendedores()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", 0);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                TBLVendedores = table.Copy();

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            else
            {
                SqlCommand command = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 4);
                command.Parameters.AddWithValue("@Vendedores", string.Empty);
                command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                command.Parameters.AddWithValue("@Cliente", string.Empty);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                command.Parameters.AddWithValue("@Factura", string.Empty);
                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                TBLVendedores = table.Copy();

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }

        }

        public string getMemo(int GroupCode)
        {
            string _memo = "";
            switch (GroupCode)
            {
                case 107: _memo = "01"; break;
                case 105: _memo = "02"; break;
                case 106: _memo = "02"; break;
                case 100: _memo = "03"; break;
                case 102: _memo = "05"; break;
                case 108: _memo = "06"; break;
                case 103: _memo = "16"; break;
                case 104: _memo = "18"; break;
            }

            return _memo;
        }

        public void DataBingindCompletePJ(DataGridView dgv)
        {
            
        }

        public void DataBingindCompleteSucursal(DataGridView dgv)
        {
            try
            {
                foreach  (DataGridViewRow row in dgv.Rows)
                {
                    int diferencia = Convert.ToInt32(row.Cells[(int)ColumnasSucursal.RankingSucursal].Value) - Convert.ToInt32(row.Cells[(int)ColumnasSucursal.RankingPJ].Value);

                    if (diferencia <= 4)
                    {
                        row.Cells[(int)ColumnasSucursal.RankingSucursal].Style.BackColor = Color.Green;
                        row.Cells[(int)ColumnasSucursal.RankingSucursal].Style.ForeColor = Color.Black;
                    }
                    else if (diferencia >= 4 && diferencia <= 8)
                    {
                        row.Cells[(int)ColumnasSucursal.RankingSucursal].Style.BackColor = Color.Yellow;
                        row.Cells[(int)ColumnasSucursal.RankingSucursal].Style.ForeColor = Color.Black;
                    }
                    else if (diferencia >= 9)
                    {
                        row.Cells[(int)ColumnasSucursal.RankingSucursal].Style.BackColor = Color.Red;
                        row.Cells[(int)ColumnasSucursal.RankingSucursal].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void DataBingindCompleteVendedor(DataGridView dgv)
        {
            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    int diferencia = Convert.ToInt32(row.Cells[(int)ColumnasVendedor.Rankin2].Value) - Convert.ToInt32(row.Cells[(int)ColumnasVendedor.Ranking1].Value);

                    if (diferencia <= 4)
                    {
                        row.Cells[(int)ColumnasVendedor.Rankin2].Style.BackColor = Color.Green;
                        row.Cells[(int)ColumnasVendedor.Rankin2].Style.ForeColor = Color.Black;
                    }
                    else if (diferencia >= 5 && diferencia <= 8)
                    {
                        row.Cells[(int)ColumnasVendedor.Rankin2].Style.BackColor = Color.Yellow;
                        row.Cells[(int)ColumnasVendedor.Rankin2].Style.ForeColor = Color.Black;
                    }
                    else if (diferencia >= 9)
                    {
                        row.Cells[(int)ColumnasVendedor.Rankin2].Style.BackColor = Color.Red;
                        row.Cells[(int)ColumnasVendedor.Rankin2].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
            }

            try
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    int diferencia = Convert.ToInt32(row.Cells[(int)ColumnasVendedor.Ranking3].Value) - Convert.ToInt32(row.Cells[(int)ColumnasVendedor.Ranking1].Value);

                    if (diferencia <= 4)
                    {
                        row.Cells[(int)ColumnasVendedor.Ranking3].Style.BackColor = Color.Green;
                        row.Cells[(int)ColumnasVendedor.Ranking3].Style.ForeColor = Color.Black;
                    }
                    else if (diferencia >= 5 && diferencia <= 8)
                    {
                        row.Cells[(int)ColumnasVendedor.Ranking3].Style.BackColor = Color.Yellow;
                        row.Cells[(int)ColumnasVendedor.Ranking3].Style.ForeColor = Color.Black;
                    }
                    else if (diferencia >= 9)
                    {
                        row.Cells[(int)ColumnasVendedor.Ranking3].Style.BackColor = Color.Red;
                        row.Cells[(int)ColumnasVendedor.Ranking3].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void RankingClientesPJ(string linea)
        {
            try
            {

                dataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();

                decimal DiasMes = this.GetDias(qryDiasMes);
                decimal DiasTrans = this.GetDias(qryDiasTra);

                DataTable clientes = (from item in _Datos.AsEnumerable()
                                      where item.Field<string>("ItmsGrpNam").Equals(linea)
                                      select item).CopyToDataTable();

                TotalPromedio = (Convert.ToDecimal(clientes.Compute("sum(MES3)", string.Empty)) + Convert.ToDecimal(clientes.Compute("sum(MES2)", string.Empty))) / 2;
                TotalPRonostico = (Convert.ToDecimal(clientes.Compute("sum(MES1)", string.Empty)) / DiasTrans) * DiasMes;

                int x = 1;
                var list_PJ = (from item in clientes.AsEnumerable()
                               group item by new
                               {
                                   Cliente = item.Field<string>("CardCode"),
                                   NombreCliente = item.Field<string>("CardName"),
                                   Sucursal = item.Field<string>("GroupName")
                               } into grouped
                               orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2 descending
                               select new
                               {
                                   Row = x++,
                                   Cliente = grouped.Key.Cliente,
                                   Nombre = grouped.Key.NombreCliente,
                                   Sucursal = grouped.Key.Sucursal,
                                   Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                   Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2) / TotalPromedio,
                                   PronostiocoActual = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes,
                                   Porcentaje2 = TotalPRonostico == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes) / TotalPRonostico
                               }
                                   ).ToList();//.OrderBy(d => d.Porcentaje1).Reverse();

                DataTable tablita = Clases.ListConverter.ToDataTable(list_PJ);

                foreach (DataRow item in tablita.Rows)
                {
                    if (item.Field<decimal>("Promedio") == decimal.Zero)
                    {
                        item.SetField("Porcentaje1", -1);
                    }
                    if (item.Field<decimal>("PronostiocoActual") == decimal.Zero)
                    {
                        item.SetField("Porcentaje2", -1);
                    }
                }


                dataGridView2.DataSource = tablita;


            }
            catch (Exception)
            {
            }
        }

        public void RankingClientesSucursal(string linea)
        {
            try
            {

                dataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();

                decimal DiasMes = this.GetDias(qryDiasMes);
                decimal DiasTrans = this.GetDias(qryDiasTra);

                DataTable clientes = (from item in _Datos.AsEnumerable()
                                      where item.Field<string>("ItmsGrpNam").Equals(linea)
                                            && item.Field<string>("GroupName").Equals(clbSucursal.Text)
                                      select item).CopyToDataTable();

                TotalPromedio = (Convert.ToDecimal(clientes.Compute("sum(MES3)", string.Empty)) + Convert.ToDecimal(clientes.Compute("sum(MES2)", string.Empty))) / 2;
                TotalPRonostico = (Convert.ToDecimal(clientes.Compute("sum(MES1)", string.Empty)) / DiasTrans) * DiasMes;

                int x = 1;
                var list_Sucursal = (from item in clientes.AsEnumerable()
                               group item by new
                               {
                                   Cliente = item.Field<string>("CardCode"),
                                   NombreCliente = item.Field<string>("CardName"),
                                   Sucursal = item.Field<string>("GroupName")
                               } into grouped
                               orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2 descending
                               select new
                               {
                                   Row = x++,
                                   Cliente = grouped.Key.Cliente,
                                   Nombre = grouped.Key.NombreCliente,
                                   Sucursal = grouped.Key.Sucursal,
                                   Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                   Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2) / TotalPromedio,
                                   PronostiocoActual = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes,
                                   Porcentaje2 = TotalPRonostico == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes) / TotalPRonostico
                               }
                                   ).ToList();//.OrderBy(d => d.Porcentaje1).Reverse();

                DataTable tablita = Clases.ListConverter.ToDataTable(list_Sucursal);

                foreach (DataRow item in tablita.Rows)
                {
                    if (item.Field<decimal>("Promedio") == decimal.Zero)
                    {
                        item.SetField("Porcentaje1", -1);
                    }
                    if (item.Field<decimal>("PronostiocoActual") == decimal.Zero)
                    {
                        item.SetField("Porcentaje2", -1);
                    }
                }


                dataGridView2.DataSource = tablita;


            }
            catch (Exception)
            {
            }
        }

        public void RankingClientesVendedor(string linea)
        {
            try
            {

                dataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();

                decimal DiasMes = this.GetDias(qryDiasMes);
                decimal DiasTrans = this.GetDias(qryDiasTra);

                DataTable clientes = (from item in _Datos.AsEnumerable()
                                      where item.Field<string>("ItmsGrpNam").Equals(linea)
                                         //   && item.Field<string>("GroupName").Equals(clbSucursal.Text)
                                            && item.Field<string>("SlpName").Equals(clbVendedor.Text)
                                      select item).CopyToDataTable();

                TotalPromedio = (Convert.ToDecimal(clientes.Compute("sum(MES3)", string.Empty)) + Convert.ToDecimal(clientes.Compute("sum(MES2)", string.Empty))) / 2;
                TotalPRonostico = (Convert.ToDecimal(clientes.Compute("sum(MES1)", string.Empty)) / DiasTrans) * DiasMes;

                int x = 1;
                var list_Sucursal = (from item in clientes.AsEnumerable()
                                     group item by new
                                     {
                                         Cliente = item.Field<string>("CardCode"),
                                         NombreCliente = item.Field<string>("CardName"),
                                         Sucursal = item.Field<string>("GroupName")
                                     } into grouped
                                     orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2 descending
                                     select new
                                     {
                                         Row = x++,
                                         Cliente = grouped.Key.Cliente,
                                         Nombre = grouped.Key.NombreCliente,
                                         Sucursal = grouped.Key.Sucursal,
                                         Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                         // PromedioSucursal = sucursal != null ? sucursal.Field<decimal>("Promedio") : 0,
                                         Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2) / TotalPromedio,
                                         PronostiocoActual = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes,
                                         Porcentaje2 = TotalPRonostico == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes) / TotalPRonostico
                                     }
                                   ).ToList();//.OrderBy(d => d.Porcentaje1).Reverse();

                DataTable tablita = Clases.ListConverter.ToDataTable(list_Sucursal);

                foreach (DataRow item in tablita.Rows)
                {
                    if (item.Field<decimal>("Promedio") == decimal.Zero)
                    {
                        item.SetField("Porcentaje1", -1);
                    }
                    if (item.Field<decimal>("PronostiocoActual") == decimal.Zero)
                    {
                        item.SetField("Porcentaje2", -1);
                    }
                }


                dataGridView2.DataSource = tablita;


            }
            catch (Exception )
            {
              //  MessageBox.Show(ex.Message);
            }
        }

        public void RankingClientesCanal(string linea)
        {
            try
            {

                dataGridView2.DataSource = null;
                dataGridView2.Columns.Clear();

                decimal DiasMes = this.GetDias(qryDiasMes);
                decimal DiasTrans = this.GetDias(qryDiasTra);

                DataTable clientes = (from item in _Datos.AsEnumerable()
                                      where item.Field<string>("ItmsGrpNam").Equals(linea)
                                            && item.Field<string>("GroupName").Equals(clbSucursal.Text)
                                            && item.Field<string>("Canal").Equals(cbCanal.Text)
                                      select item).CopyToDataTable();

                TotalPromedio = (Convert.ToDecimal(clientes.Compute("sum(MES3)", string.Empty)) + Convert.ToDecimal(clientes.Compute("sum(MES2)", string.Empty))) / 2;
                TotalPRonostico = (Convert.ToDecimal(clientes.Compute("sum(MES1)", string.Empty)) / DiasTrans) * DiasMes;

                int x = 1;
                var list_Sucursal = (from item in clientes.AsEnumerable()
                                     group item by new
                                     {
                                         Cliente = item.Field<string>("CardCode"),
                                         NombreCliente = item.Field<string>("CardName"),
                                         Sucursal = item.Field<string>("GroupName")
                                     } into grouped
                                     orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2 descending
                                     select new
                                     {
                                         Row = x++,
                                         Cliente = grouped.Key.Cliente,
                                         Nombre = grouped.Key.NombreCliente,
                                         Sucursal = grouped.Key.Sucursal,
                                         Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                         // PromedioSucursal = sucursal != null ? sucursal.Field<decimal>("Promedio") : 0,
                                         Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2) / TotalPromedio,
                                         PronostiocoActual = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes,
                                         Porcentaje2 = TotalPRonostico == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes) / TotalPRonostico
                                     }
                                   ).ToList();//.OrderBy(d => d.Porcentaje1).Reverse();

                DataTable tablita = Clases.ListConverter.ToDataTable(list_Sucursal);

                foreach (DataRow item in tablita.Rows)
                {
                    if (item.Field<decimal>("Promedio") == decimal.Zero)
                    {
                        item.SetField("Porcentaje1", -1);
                    }
                    if (item.Field<decimal>("PronostiocoActual") == decimal.Zero)
                    {
                        item.SetField("Porcentaje2", -1);
                    }
                }


                dataGridView2.DataSource = tablita;


            }
            catch (Exception )
            {
                //  MessageBox.Show(ex.Message);
            }
        }
        DataGridView dgv = new DataGridView();
        private void RankingLineas_Load(object sender, EventArgs e)
        {
            try
            {
                dgv = dataGridView1;
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                _Datos.Clear();
                label2.Visible = false;
                clbVendedor.Visible = false;

                this.CargarSucursales();
                this.CargarVendedores();
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_VentaCaida";
                        
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 5);
                        command.Parameters.AddWithValue("@Sucursal", 0);
                        command.Parameters.AddWithValue("@Vendedor", 0);
                        command.Parameters.AddWithValue("@Linea", string.Empty);
                        command.CommandTimeout = 0;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(_Datos);

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
            lblClientes.Text = "Clientes";
            Opcion = 1;
            try
            {
                label2.Visible = false;
                clbVendedor.Visible = false;

                label3.Visible = false;
                cbCanal.Visible = false;

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                decimal DiasMes = this.GetDias(qryDiasMes);
                decimal DiasTrans = this.GetDias(qryDiasTra);

                TotalPromedio = (Convert.ToDecimal(_Datos.Compute("sum(MES3)", string.Empty)) + Convert.ToDecimal(_Datos.Compute("sum(MES2)", string.Empty))) / 2;
                TotalPRonostico = (Convert.ToDecimal(_Datos.Compute("sum(MES1)", string.Empty)) / DiasTrans) * DiasMes;
                int x = 1;
                var list_PJ = (from item in _Datos.AsEnumerable()
                               group item by new
                               {
                                   ItmsGrpNam = item.Field<string>("ItmsGrpNam")
                               } into grouped
                               orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2 descending
                               select new
                               {
                                   Row = x++,
                                   Linea = grouped.Key.ItmsGrpNam,
                                   Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                   Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2) / TotalPromedio,
                                   PronostiocoActual = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes,
                                   Porcentaje2 = TotalPRonostico == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes) / TotalPRonostico
                               }
                                   ).ToList();//.OrderBy(d => d.Porcentaje1).Reverse();

                _DatosPJ = Clases.ListConverter.ToDataTable(list_PJ);
                dataGridView1.DataSource = _DatosPJ;

                this.FormatoPJ();
            }
            catch (Exception)
            {
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            lblClientes.Text = "Clientes";
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
            RankingLineas_Load(sender, e);
        }

        private void btnSucursal_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
            lblClientes.Text = "Clientes";
            Opcion = 2;
            try
            {
                if (_DatosPJ.Rows.Count == 0)
                    button1_Click(sender, e);

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                _DatosSucusal.Clear();

                decimal DiasMes = this.GetDias(qryDiasMes);
                decimal DiasTrans = this.GetDias(qryDiasTra);

                DataTable _TblSucursal = (from item in _Datos.AsEnumerable()
                                          where item.Field<string>("GroupName").Equals(Sucursal)
                                          select item).CopyToDataTable();

                TotalPromedio = (Convert.ToDecimal(_TblSucursal.Compute("sum(MES3)", string.Empty)) + Convert.ToDecimal(_TblSucursal.Compute("sum(MES2)", string.Empty))) / 2;
                TotalPRonostico = (Convert.ToDecimal(_TblSucursal.Compute("sum(MES1)", string.Empty)) / DiasTrans) * DiasMes;

                int x = 1;
                var list_Sucursal = (from item in _TblSucursal.AsEnumerable()
                                     group item by new
                                     {
                                         ItmsGrpNam = item.Field<string>("ItmsGrpNam")
                                     } into grouped
                                     orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2 descending
                                     select new
                                     {
                                         Row = x ++,
                                         Linea = grouped.Key.ItmsGrpNam,
                                         Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                         Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2) / TotalPromedio,
                                         PronostiocoActual = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes,
                                         Porcentaje2 = TotalPRonostico == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes) / TotalPRonostico
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
                                   Row2 = sucursal != null ? sucursal.Field<int>("Row"): x2++,
                                   PromedioSucursal = sucursal != null ? sucursal.Field<decimal>("Promedio") : 0,
                                   PorcentajeSucursal = sucursal != null ? sucursal.Field<decimal>("Porcentaje1") : 0,
                                   PronosticoSucursal = sucursal != null ? sucursal.Field<decimal>("PronostiocoActual") : 0,
                                   PorcentajePronostico = sucursal != null ? sucursal.Field<decimal>("Porcentaje2") : 0
                               };
                _DatosSucusal = Clases.ListConverter.ToDataTable(LeftJoin.ToList());

                dataGridView1.DataSource = _DatosSucusal;

                this.FormatoSucursal();
            }
            catch (Exception)
            {
            }
        }

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
            lblClientes.Text = "Clientes";
            Opcion = 3;
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                _DatosVendedor.Clear();

                decimal DiasMes = this.GetDias(qryDiasMes);
                decimal DiasTrans = this.GetDias(qryDiasTra);

                DataTable _TblVendor = (from item in _Datos.AsEnumerable()
                                        where item.Field<string>("SlpName").Equals(clbVendedor.Text)
                                        select item).CopyToDataTable();

                TotalPromedio = (Convert.ToDecimal(_TblVendor.Compute("sum(MES3)", string.Empty)) + Convert.ToDecimal(_TblVendor.Compute("sum(MES2)", string.Empty))) / 2;
                TotalPRonostico = (Convert.ToDecimal(_TblVendor.Compute("sum(MES1)", string.Empty)) / DiasTrans) * DiasMes;

                int x = 1;
                if (TotalPRonostico == 0)
                    TotalPRonostico = 1;
                var list_Vendedor = (from item in _TblVendor.AsEnumerable()
                                     group item by new
                                     {
                                         ItmsGrpNam = item.Field<string>("ItmsGrpNam")
                                     } into grouped
                                     orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2 descending
                                     select new
                                     {
                                         Row = x++,
                                         Linea = grouped.Key.ItmsGrpNam,
                                         Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                         Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2) / TotalPromedio,
                                         PronostiocoActual = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes,
                                         Porcentaje2 = TotalPRonostico == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes) / TotalPRonostico
                                     }
                                   ).ToList();

                DataTable auxvendor = Clases.ListConverter.ToDataTable(list_Vendedor);
                int x2 = auxvendor.Rows.Count + 1;


                var LeftJoin = from sucursal in _DatosSucusal.AsEnumerable()
                               join vendedor in auxvendor.AsEnumerable()
                               on sucursal.Field<string>("Linea") equals vendedor.Field<string>("Linea") into JoinedPJSVendedor
                               from vendedor in JoinedPJSVendedor.DefaultIfEmpty()
                               select new
                               {
                                   Row1 = sucursal.Field<Int32>("Row1"),
                                   Linea = sucursal.Field<string>("Linea"),
                                   PromedioPJ = sucursal.Field<decimal>("PromedioPJ"),
                                   PorcentajePJ = sucursal.Field<decimal>("PorcentajePJ"),
                                   Row2 = sucursal.Field<Int32>("Row2"),
                                   PromedioSucursal = sucursal.Field<decimal>("PromedioSucursal"),
                                   PorcentajeSucursal = sucursal.Field<decimal>("PorcentajeSucursal"),
                                   Row3 = vendedor != null ? vendedor.Field<Int32>("Row") : x2++,
                                   PromedioVendedor = vendedor != null ? vendedor.Field<decimal>("Promedio") : 0,
                                   PorcentajeVendedor = vendedor != null ? vendedor.Field<decimal>("Porcentaje1") : 0,
                                   PronosticoVendedor = vendedor != null ? vendedor.Field<decimal>("PronostiocoActual") : 0,
                                   PorcentajePronostico = vendedor != null ? vendedor.Field<decimal>("Porcentaje2") : 0
                               };

                dataGridView1.DataSource = Clases.ListConverter.ToDataTable(LeftJoin.ToList());

                this.FormatoVendedor();
            }
            catch (Exception)
            {
            }
        }

        //DataTable JefasxSucursal = new DataTable();
        DataTable VendedorxSucursal = new DataTable();

        private void clbSucursal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            clbVendedor.DataSource = null;
            
           // JefasxSucursal.Clear();
            VendedorxSucursal.Clear();
            try
            {
                DataRowView v = clbSucursal.Items[((ComboBox)sender).SelectedIndex] as DataRowView;
                string _memo = getMemo(Convert.ToInt32(v["Codigo"]));
                VendedorxSucursal.Merge((from item in TBLVendedores.AsEnumerable() where item["Memo"].ToString() == _memo select item).CopyToDataTable());


                DataView vistaV = new DataView(VendedorxSucursal);
                DataTable aV = vistaV.ToTable(true, new string[] { "Codigo", "Nombre" });
                DataRow row = aV.NewRow();
                row["Nombre"] = "--";
                row["Codigo"] = "0";
                aV.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = aV;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            catch (Exception)
            {
                VendedorxSucursal.Clear();
                clbVendedor.DataSource = TBLVendedores;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }

            ComboBox c = (ComboBox)sender;
            Sucursal = Convert.ToString(c.Text);

            btnSucursal_Click(sender, e);
            label2.Visible = true;
            clbVendedor.Visible = true;
            label3.Visible = true;
            cbCanal.Visible = true;
        }

        private void clbVendedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            Sucursal = Convert.ToString(c.Text);
            cbCanal.SelectedIndex = 0;
            btnVendedor_Click(sender, e);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (Opcion == 1)
                {
                    this.DataBingindCompletePJ(sender as DataGridView);
                }
                if (Opcion == 2)
                {
                    this.DataBingindCompleteSucursal(sender as DataGridView);
                }
                if (Opcion == 3 || Opcion == 4)
                {
                    this.DataBingindCompleteVendedor(sender as DataGridView);
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void RankingLineas_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void RankingLineas_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // if (e.ColumnIndex == (int)ColumnasPJ.Linea)
                // {
                if (Opcion == 1)
                {
                    lblClientes.Text = "Clientes: PJ - " + Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasPJ.Linea].Value);
                    this.RankingClientesPJ(Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasPJ.Linea].Value));
                    FormatoClientes();
                }
                if (Opcion == 2)
                {
                    lblClientes.Text = "Clientes: " + clbSucursal.Text + " - " + Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasPJ.Linea].Value);
                    this.RankingClientesSucursal(Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasPJ.Linea].Value));
                    FormatoClientes();
                }
                if (Opcion == 3)
                {
                    lblClientes.Text = "Clientes: " + clbVendedor.Text + " - " + Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasPJ.Linea].Value);
                    this.RankingClientesVendedor(Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasPJ.Linea].Value));
                    FormatoClientes();
                }
                if (Opcion == 4)
                {
                    lblClientes.Text = "Clientes: " + cbCanal.Text + " - " + Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasPJ.Linea].Value);
                    this.RankingClientesCanal(Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasPJ.Linea].Value));
                    FormatoClientes();
                }
                //  }
            }
            catch (Exception)
            {

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                int ColumnIndex = (int)ColumnasPJ.Linea;
                int RowIndex = (sender as DataGridView).CurrentCell.RowIndex;

                // if (ColumnIndex == (int)ColumnasPJ.Linea)
                //  {
                if (Opcion == 1)
                {
                    lblClientes.Text = "Clientes: PJ - " + Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value);
                    this.RankingClientesPJ(Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value));
                    FormatoClientes();
                }
                if (Opcion == 2)
                {
                    lblClientes.Text = "Clientes: " + clbSucursal.Text + " - " + Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value);
                    this.RankingClientesSucursal(Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value));
                    FormatoClientes();
                }
                if (Opcion == 3)
                {
                    lblClientes.Text = "Clientes: " + clbVendedor.Text + " - " + Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value);
                    this.RankingClientesVendedor(Convert.ToString((sender as DataGridView).Rows[RowIndex].Cells[ColumnIndex].Value));
                    FormatoClientes();
                }
                //}
                aux = lblClientes.Text;
            }
            catch (Exception)
            {

            }
        }

        string aux = "";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == (int)ColumnasClientes)
               // {
                    
                    string Nombre = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasClientes.Nombre].Value);
                    
                    lblClientes.Text = aux + " - " + Nombre;
               // }
            }
            catch (Exception)
            {
              
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2_CellClick(sender, e);
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {

            }
        }

        private void cbCanal_SelectionChangeCommitted(object sender, EventArgs e)
        {

            dataGridView2.DataSource = null;
            dataGridView2.Columns.Clear();
            lblClientes.Text = "Clientes";
            clbVendedor.SelectedIndex = 0;
            Opcion = 4;
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                _DatosVendedor.Clear();

                decimal DiasMes = this.GetDias(qryDiasMes);
                decimal DiasTrans = this.GetDias(qryDiasTra);

                DataTable _TblCanales = (from item in _Datos.AsEnumerable()
                                        where item.Field<string>("Canal").Equals(cbCanal.Text)
                                        && item.Field<string>("GroupName").Equals(clbSucursal.Text)
                                        select item).CopyToDataTable();

                TotalPromedio = (Convert.ToDecimal(_TblCanales.Compute("sum(MES3)", string.Empty)) + Convert.ToDecimal(_TblCanales.Compute("sum(MES2)", string.Empty))) / 2;
                TotalPRonostico = (Convert.ToDecimal(_TblCanales.Compute("sum(MES1)", string.Empty)) / DiasTrans) * DiasMes;

                int x = 1;
                if (TotalPRonostico == 0)
                    TotalPRonostico = 1;
                var list_Vendedor = (from item in _TblCanales.AsEnumerable()
                                     group item by new
                                     {
                                         ItmsGrpNam = item.Field<string>("ItmsGrpNam")
                                     } into grouped
                                     orderby (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2 descending
                                     select new
                                     {
                                         Row = x++,
                                         Linea = grouped.Key.ItmsGrpNam,
                                         Promedio = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2,
                                         Porcentaje1 = TotalPromedio == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES3")) + grouped.Sum(ix => ix.Field<decimal>("MES2"))) / 2) / TotalPromedio,
                                         PronostiocoActual = (decimal)(grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes,
                                         Porcentaje2 = TotalPRonostico == 0 ? 0 : (decimal)((grouped.Sum(ix => ix.Field<decimal>("MES1")) / DiasTrans) * DiasMes) / TotalPRonostico
                                     }
                                   ).ToList();

                DataTable auxvendor = Clases.ListConverter.ToDataTable(list_Vendedor);
                int x2 = auxvendor.Rows.Count + 1;


                var LeftJoin = from sucursal in _DatosSucusal.AsEnumerable()
                               join vendedor in auxvendor.AsEnumerable()
                               on sucursal.Field<string>("Linea") equals vendedor.Field<string>("Linea") into JoinedPJSVendedor
                               from vendedor in JoinedPJSVendedor.DefaultIfEmpty()
                               select new
                               {
                                   Row1 = sucursal.Field<Int32>("Row1"),
                                   Linea = sucursal.Field<string>("Linea"),
                                   PromedioPJ = sucursal.Field<decimal>("PromedioPJ"),
                                   PorcentajePJ = sucursal.Field<decimal>("PorcentajePJ"),
                                   Row2 = sucursal.Field<Int32>("Row2"),
                                   PromedioSucursal = sucursal.Field<decimal>("PromedioSucursal"),
                                   PorcentajeSucursal = sucursal.Field<decimal>("PorcentajeSucursal"),
                                   Row3 = vendedor != null ? vendedor.Field<Int32>("Row") : x2++,
                                   PromedioVendedor = vendedor != null ? vendedor.Field<decimal>("Promedio") : 0,
                                   PorcentajeVendedor = vendedor != null ? vendedor.Field<decimal>("Porcentaje1") : 0,
                                   PronosticoVendedor = vendedor != null ? vendedor.Field<decimal>("PronostiocoActual") : 0,
                                   PorcentajePronostico = vendedor != null ? vendedor.Field<decimal>("Porcentaje2") : 0
                               };

                dataGridView1.DataSource = Clases.ListConverter.ToDataTable(LeftJoin.ToList());

                this.FormatoVendedor();
            }
            catch (Exception)
            {
            }
        }
        //DataGridView DGV = new DataGridView();
        private void button2_Click(object sender, EventArgs e)
        {
            ClasesSGUV.Exportar ex = new ClasesSGUV.Exportar();
            if (ex.ExportarColores(dgv))
                MessageBox.Show("El Archivo se creo con exito.");
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            dgv = (sender as DataGridView);
        }
    }
}
