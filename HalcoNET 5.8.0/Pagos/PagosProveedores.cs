using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Pagos
{
    public partial class PagosProveedores : Form
    {
        #region PARAMETROS
        Logs log;

        private BindingSource bindingSource1 = new BindingSource();
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();

        /*totales*/
        decimal n00; decimal n01; decimal n02; decimal n03; decimal n04; decimal n05; decimal n06;
        //decimal na00; decimal na01; decimal na02; decimal na03; decimal na04; decimal na05; decimal na06;
        decimal n10; decimal n11; decimal n12; decimal n13; decimal n14; decimal n15; decimal n16;
        //decimal na10; decimal na11; decimal na12; decimal na13; decimal na14; decimal na15; decimal na16;
        decimal n20; decimal n21; decimal n22; decimal n23; decimal n24; decimal n25; decimal n26;
        decimal n30; decimal n31; decimal n32; decimal n33; decimal n34; decimal n35; decimal n36;
        decimal n40; decimal n41; decimal n42; decimal n43; decimal n44; decimal n45; decimal n46;
        decimal n55; decimal n56; decimal n65; decimal n66; decimal n75; decimal n76; decimal n85;

        /*Totales gastos*/
        decimal g00, g01;
        decimal g10, g11;
        decimal g20, g21;
        decimal gtUSD, gtMXP;

        decimal l1, l2, l3, l4, l5, l6, l7;

        /**/

        /**/
        decimal TC;
        decimal Disponible;
        decimal Pendiente;
        decimal Libre;
        /**/
        DataTable _tableMXP = new DataTable();
        DataTable _tableUSD = new DataTable();
        DataTable _tableIMP = new DataTable();
        DataTable _tableBanco = new DataTable();
        DataTable _tableLibre = new DataTable();
        DataTable _tblProveedores = new DataTable();

        private int Rol;

        public enum Columnas
        {
            Estatus,
            Provedor,
            Nombre,
            FechaContabilizacion,
            FechaVencimiento,
            Facura,
            FolioSAP,
            Moneda,
            MontoOriginal,
            Total,
            Bit1,
            Propuesta,
            Bit2,
            Aprobado,
            Comentarios,
            Situacion,
            Docentry,
            TC,
            TotalMXP,
            AprobadoMXP,
            PagarUSD,
            Cuenta,
            Tipo,
            Condiciones,
            Descuento
        }

        public enum ColumnasLibre
        {
            Code, Fecha, Descripcion, MXP, USD, Propuesta, Aprobado, colum1, colum2, colum3, total
        }

        public enum ColumnasGastos
        {
            Code,
            Proveedor,
            Concepto,
            Factura,
            Moneda,
            Propuesta,
            Aprob,
            Aprobado,
            Solicita,
            FechaLimite,
            Cuenta,
            Banco,
            Prioridad,
            TipoPago
        }
        #endregion

        #region CONSTRUCTORES
        public PagosProveedores(int _rol)
        {
            Rol = _rol;
            InitializeComponent();
        }
        #endregion

        #region METODOS
        public void Formato(DataGridView dgv, bool usd, bool mxp, bool tc, bool payUSD)
        {

            try
            {
                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    item.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgv.Columns[(int)Columnas.Docentry].Visible = false;
                dgv.Columns[(int)Columnas.Moneda].Visible = false;
                dgv.Columns[(int)Columnas.Cuenta].Visible = false;
                dgv.Columns[(int)Columnas.Condiciones].Visible = false;
                dgv.Columns[(int)Columnas.Tipo].Visible = false;
                dgv.Columns[(int)Columnas.Descuento].Visible = false;

                dgv.Columns[(int)Columnas.TC].Visible = tc;
                dgv.Columns[(int)Columnas.TotalMXP].Visible = mxp;
                dgv.Columns[(int)Columnas.AprobadoMXP].Visible = mxp;

                dgv.Columns[(int)Columnas.Propuesta].Visible = usd;
                dgv.Columns[(int)Columnas.Aprobado].Visible = usd;

                dgv.Columns[(int)Columnas.PagarUSD].Visible = payUSD;

                dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dgv.Columns[(int)Columnas.Estatus].HeaderText = "Estatus";
                dgv.Columns[(int)Columnas.Provedor].HeaderText = "Proveedor";
                dgv.Columns[(int)Columnas.Nombre].HeaderText = "Nombre";
                dgv.Columns[(int)Columnas.FechaContabilizacion].HeaderText = "Fecha de\r\ncontabilización";
                dgv.Columns[(int)Columnas.FechaVencimiento].HeaderText = "Fecha de\r\nvencimiento";
                dgv.Columns[(int)Columnas.Facura].HeaderText = "Factura";
                dgv.Columns[(int)Columnas.FolioSAP].HeaderText = "Folio\r\nSAP";
                dgv.Columns[(int)Columnas.Moneda].HeaderText = "Mondea del\r\ndocumento";
                dgv.Columns[(int)Columnas.Total].HeaderText = "Monto\r\nOriginal";
                dgv.Columns[(int)Columnas.Total].HeaderText = "Total con\r\ndescuento";
                dgv.Columns[(int)Columnas.Bit1].HeaderText = "Prop";
                dgv.Columns[(int)Columnas.Propuesta].HeaderText = "Propuesta\r\nUSD";
                dgv.Columns[(int)Columnas.Bit2].HeaderText = "Apro";
                dgv.Columns[(int)Columnas.Aprobado].HeaderText = "Aprobado\r\nUSD";
                dgv.Columns[(int)Columnas.Comentarios].HeaderText = "Comentarios";
                dgv.Columns[(int)Columnas.Situacion].HeaderText = "Situación";
                dgv.Columns[(int)Columnas.Docentry].HeaderText = "DocEntry";
                dgv.Columns[(int)Columnas.TC].HeaderText = "Tipo de\r\ncambio";
                dgv.Columns[(int)Columnas.TotalMXP].HeaderText = "Propuesta\r\nMXP";
                dgv.Columns[(int)Columnas.AprobadoMXP].HeaderText = "Aprobado\r\nMXP";
                dgv.Columns[(int)Columnas.PagarUSD].HeaderText = "Pagar\r\nUSD";

                dgv.Columns[(int)Columnas.Estatus].ReadOnly = true;
                dgv.Columns[(int)Columnas.Provedor].ReadOnly = true;
                dgv.Columns[(int)Columnas.Nombre].ReadOnly = true;
                dgv.Columns[(int)Columnas.FechaContabilizacion].ReadOnly = true;
                dgv.Columns[(int)Columnas.FechaVencimiento].ReadOnly = true;
                dgv.Columns[(int)Columnas.Facura].ReadOnly = true;
                dgv.Columns[(int)Columnas.FolioSAP].ReadOnly = true;
                dgv.Columns[(int)Columnas.FolioSAP].DefaultCellStyle.Format = "0";
                dgv.Columns[(int)Columnas.Total].ReadOnly = true;
                dgv.Columns[(int)Columnas.MontoOriginal].ReadOnly = true;
                dgv.Columns[(int)Columnas.Propuesta].ReadOnly = false;
                dgv.Columns[(int)Columnas.Aprobado].ReadOnly = false;
                dgv.Columns[(int)Columnas.Comentarios].ReadOnly = false;
                dgv.Columns[(int)Columnas.Moneda].ReadOnly = false;
                //dgv.Columns[(int)Columnas.Clasificacion].ReadOnly = true;
                dgv.Columns[(int)Columnas.Situacion].ReadOnly = true;


                if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                    dgv.Columns[(int)Columnas.Bit2].ReadOnly = false;
                else
                    dgv.Columns[(int)Columnas.Bit2].ReadOnly = true;


                dgv.Columns[(int)Columnas.Estatus].Width = 60;
                dgv.Columns[(int)Columnas.Provedor].Width = 58;
                dgv.Columns[(int)Columnas.Nombre].Width = 180;
                dgv.Columns[(int)Columnas.FechaContabilizacion].Width = 90;
                dgv.Columns[(int)Columnas.FechaVencimiento].Width = 80;
                dgv.Columns[(int)Columnas.Facura].Width = 80;
                dgv.Columns[(int)Columnas.FolioSAP].Width = 80;
                dgv.Columns[(int)Columnas.Moneda].Width = 80;
                dgv.Columns[(int)Columnas.Total].Width = 90;
                dgv.Columns[(int)Columnas.MontoOriginal].Width = 90;
                dgv.Columns[(int)Columnas.Propuesta].Width = 90;
                dgv.Columns[(int)Columnas.Aprobado].Width = 90;
                dgv.Columns[(int)Columnas.Comentarios].Width = 150;
                dgv.Columns[(int)Columnas.PagarUSD].Width = 50;
                dgv.Columns[(int)Columnas.Situacion].Width = 65;



                dgv.Columns[(int)Columnas.Bit1].Width = 30;
                dgv.Columns[(int)Columnas.Bit2].Width = 30;
                dgv.Columns[(int)Columnas.TC].Width = 50;

                dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)Columnas.MontoOriginal].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)Columnas.Propuesta].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)Columnas.Aprobado].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)Columnas.TC].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)Columnas.TotalMXP].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)Columnas.AprobadoMXP].DefaultCellStyle.Format = "C2";

                dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)Columnas.MontoOriginal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)Columnas.Propuesta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)Columnas.Aprobado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)Columnas.Comentarios].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.Columns[(int)Columnas.TC].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)Columnas.TotalMXP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)Columnas.AprobadoMXP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
     
                dgv.Columns[(int)Columnas.Estatus].DisplayIndex = 0;
                dgv.Columns[(int)Columnas.Provedor].DisplayIndex = 1;
                dgv.Columns[(int)Columnas.Nombre].DisplayIndex = 2;
                dgv.Columns[(int)Columnas.FechaContabilizacion].DisplayIndex = 3;
                dgv.Columns[(int)Columnas.FechaVencimiento].DisplayIndex = 4;
                dgv.Columns[(int)Columnas.Facura].DisplayIndex = 5;
                dgv.Columns[(int)Columnas.FolioSAP].DisplayIndex = 6;
                dgv.Columns[(int)Columnas.Moneda].DisplayIndex = 7;
                dgv.Columns[(int)Columnas.MontoOriginal].DisplayIndex = 8;
                dgv.Columns[(int)Columnas.Total].DisplayIndex = 9;
                dgv.Columns[(int)Columnas.TC].DisplayIndex = 10;
                dgv.Columns[(int)Columnas.Bit1].DisplayIndex = 11;
                dgv.Columns[(int)Columnas.Propuesta].DisplayIndex = 12;
                dgv.Columns[(int)Columnas.TotalMXP].DisplayIndex = 13;
                dgv.Columns[(int)Columnas.Bit2].DisplayIndex = 14;
                dgv.Columns[(int)Columnas.Aprobado].DisplayIndex = 15;
                dgv.Columns[(int)Columnas.AprobadoMXP].DisplayIndex = 16;
                dgv.Columns[(int)Columnas.Comentarios].DisplayIndex = 17;
                dgv.Columns[(int)Columnas.Situacion].DisplayIndex = 18;
                dgv.Columns[(int)Columnas.Docentry].DisplayIndex = 19;
                dgv.Columns[(int)Columnas.PagarUSD].DisplayIndex = 20;
            }
            catch (Exception)
            {
            }
        }

        public void Formato(DataGridView dgv)
        {
            // dgv.Columns[(int)Columnas.Docentry].Visible = false;
            try
            {
                dgvLibre.Columns["Code"].Visible = false;
                dgv.Columns["Code"].Visible = false;
                dgv.Columns[(int)ColumnasLibre.Fecha].Width = 90;
                dgv.Columns[(int)ColumnasLibre.Descripcion].Width = 300;
                dgv.Columns[(int)ColumnasLibre.MXP].Width = 100;
                dgv.Columns[(int)ColumnasLibre.USD].Width = 100;

                dgv.Columns[(int)ColumnasLibre.MXP].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasLibre.USD].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasLibre.colum1].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasLibre.colum2].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasLibre.colum3].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasLibre.total].DefaultCellStyle.Format = "C2";

                dgv.Columns[(int)ColumnasLibre.Propuesta].Width = 80;
                dgv.Columns[(int)ColumnasLibre.Aprobado].Width = 80;

                dgv.Columns[(int)ColumnasLibre.MXP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)ColumnasLibre.USD].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)ColumnasLibre.colum1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)ColumnasLibre.colum2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)ColumnasLibre.colum3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)ColumnasLibre.total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
            catch (Exception)
            {
            }
        }

        public void FormatoGastos(DataGridView dgv)
        {
            // dgv.Columns[(int)Columnas.Docentry].Visible = false;
            try
            {
                dgv.Columns[(int)ColumnasGastos.Code].Visible = false;
                dgv.Columns[(int)ColumnasGastos.TipoPago].Visible = false;

                dgv.Columns[(int)ColumnasGastos.Moneda].ReadOnly = true;
                dgv.Columns[(int)ColumnasGastos.Proveedor].ReadOnly = true;

                if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                {
                    dgv.Columns[(int)ColumnasGastos.Aprob].ReadOnly = false;
                    dgv.Columns[(int)ColumnasGastos.Aprobado].ReadOnly = false;
                }
                else
                {
                    dgv.Columns[(int)ColumnasGastos.Aprob].ReadOnly = true;
                    dgv.Columns[(int)ColumnasGastos.Aprobado].ReadOnly = true;
                }

                dgv.Columns[(int)ColumnasGastos.Proveedor].Width = 150;
                dgv.Columns[(int)ColumnasGastos.Concepto].Width = 150;
                dgv.Columns[(int)ColumnasGastos.Factura].Width = 90;
                dgv.Columns[(int)ColumnasGastos.Propuesta].Width = 90;
                dgv.Columns[(int)ColumnasGastos.Aprob].Width = 30;
                dgv.Columns[(int)ColumnasGastos.Moneda].Width = 60;
                dgv.Columns[(int)ColumnasGastos.Aprobado].Width = 90;
                dgv.Columns[(int)ColumnasGastos.Solicita].Width = 100;
                dgv.Columns[(int)ColumnasGastos.FechaLimite].Width = 90;
                dgv.Columns[(int)ColumnasGastos.Cuenta].Width = 80;
                dgv.Columns[(int)ColumnasGastos.Banco].Width = 80;
                dgv.Columns[(int)ColumnasGastos.Prioridad].Width = 70;

                dgv.Columns[(int)ColumnasGastos.Propuesta].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumnasGastos.Aprobado].DefaultCellStyle.Format = "C2";

                dgv.Columns[(int)ColumnasGastos.Propuesta].Width = 80;
                dgv.Columns[(int)ColumnasGastos.Aprobado].Width = 80;

                dgv.Columns[(int)ColumnasGastos.Prioridad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgv.Columns[(int)ColumnasGastos.Propuesta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgv.Columns[(int)ColumnasGastos.Aprobado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            catch (Exception)
            {
            }
        }

        public void SaveGrid(DataGridView dgv)
        {
            //DataTable datos = dgv.DataSource as DataTable;

            //DataTable lflflf = datos.GetChanges(DataRowState.Modified);

            foreach (DataGridViewRow item in dgv.Rows)
            {
                if (Convert.ToString(item.Cells[(int)Columnas.Estatus].Value) != string.Empty)
                {
                    int DocNum = Convert.ToInt32(item.Cells[(int)Columnas.Docentry].Value);

                    decimal Propuesta = decimal.Zero;
                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true)
                        Propuesta = Convert.ToDecimal(item.Cells[(int)Columnas.TotalMXP].Value);

                    decimal PropuestaUSD = decimal.Zero;
                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true)
                        PropuestaUSD = Convert.ToDecimal(item.Cells[(int)Columnas.Propuesta].Value);

                    decimal Aprobado = decimal.Zero;
                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) == true)
                        Aprobado = Convert.ToDecimal(item.Cells[(int)Columnas.AprobadoMXP].Value);

                    decimal AprobadoUSD = decimal.Zero;
                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) == true)
                        AprobadoUSD = Convert.ToDecimal(item.Cells[(int)Columnas.Aprobado].Value);

                    decimal _TC = decimal.Zero;
                    //if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true)
                    _TC = Convert.ToDecimal(item.Cells[(int)Columnas.TC].Value);

                    string _Tipo = string.Empty;
                    //if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true)
                    _Tipo = Convert.ToString(item.Cells[(int)Columnas.Tipo].Value);

                    string _usd = string.Empty;
                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true || Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true)
                    {
                        if (Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value) == true)
                        {
                            _usd = "Y";
                        }

                    }

                    decimal _Total = Convert.ToDecimal(item.Cells[(int)Columnas.Total].Value);



                    string Comentario = Convert.ToString(item.Cells[(int)Columnas.Comentarios].Value);

                    string Moneda = Convert.ToString(item.Cells[(int)Columnas.Moneda].Value);

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 2);
                            command.Parameters.AddWithValue("@FechaDesde", dtFecha1.Value);
                            command.Parameters.AddWithValue("@FechaHasta", dtFecha2.Value);
                            command.Parameters.AddWithValue("@Sucursales", _usd);
                            command.Parameters.AddWithValue("@Proveedores", _Tipo);
                            command.Parameters.AddWithValue("@GroupCode", 0);

                            command.Parameters.AddWithValue("@DocNum", DocNum);
                            command.Parameters.AddWithValue("@Comentario", Comentario);
                            command.Parameters.AddWithValue("@Propuesta", Propuesta);
                            command.Parameters.AddWithValue("@Aprobado", Aprobado);

                            command.Parameters.AddWithValue("@Estatus", Moneda);
                            command.Parameters.AddWithValue("@Usuario", log.Usuario);

                            command.Parameters.AddWithValue("@PropuestaUSD", PropuestaUSD);
                            command.Parameters.AddWithValue("@AprobadoUSD", AprobadoUSD);
                            command.Parameters.AddWithValue("@TC", _TC);
                            command.CommandTimeout = 0;

                            connection.Open();

                            command.ExecuteNonQuery();
                        }
                    }
                }

            }

            //lflflf.AcceptChanges();
        }

        public void SaveValue(decimal valor, string nombre)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                    command.Parameters.AddWithValue("@FechaDesde", dtFecha1.Value);
                    command.Parameters.AddWithValue("@FechaHasta", dtFecha2.Value);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@Proveedores", string.Empty);
                    command.Parameters.AddWithValue("@GroupCode", 0);

                    command.Parameters.AddWithValue("@DocNum", 0);
                    command.Parameters.AddWithValue("@Comentario", nombre);
                    command.Parameters.AddWithValue("@Propuesta", valor);
                    command.Parameters.AddWithValue("@Aprobado", decimal.Zero);

                    command.Parameters.AddWithValue("@Estatus", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", log.Usuario);

                    command.Parameters.AddWithValue("@PropuestaUSD", decimal.Zero);
                    command.Parameters.AddWithValue("@AprobadoUSD", decimal.Zero);
                    command.Parameters.AddWithValue("@TC", decimal.Zero);
                    command.CommandTimeout = 0;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public void CaragarValores()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(log.Usuario))
                            log.Usuario = string.Empty;

                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@FechaDesde", dtFecha1.Value);
                        command.Parameters.AddWithValue("@FechaHasta", dtFecha2.Value);
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Proveedores", string.Empty);
                        command.Parameters.AddWithValue("@GroupCode", 0);

                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);
                        command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                        command.Parameters.AddWithValue("@Aprobado", decimal.Zero);

                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Usuario", log.Usuario);

                        command.Parameters.AddWithValue("@PropuestaUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@AprobadoUSD", decimal.Zero);
                        command.Parameters.AddWithValue("@TC", decimal.Zero);

                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable ta = new DataTable();
                        da.Fill(ta);


                        decimal BANCOMER_DIS_MXP, BANCOMER_DIS_USD, BANAMEX_DIS_MXP, BANAMEX_DIS_USD,BANCOMER_PEN_MXP,BANCOMER_PEN_USD,
                            HSBC_DIS_MXP, HSBC_DIS_USD, BANAMEX_PEN_MXP, BANAMEX_PEN_USD, HSBC_PEN_MXP, HSBC_PEN_USD;

                        BANCOMER_DIS_MXP = BANCOMER_DIS_USD = BANAMEX_DIS_MXP = BANAMEX_DIS_USD = BANCOMER_PEN_MXP = BANCOMER_PEN_USD = HSBC_DIS_MXP = HSBC_DIS_USD = BANAMEX_PEN_MXP = BANAMEX_PEN_USD = HSBC_PEN_MXP = HSBC_PEN_USD = decimal.Zero;

                        BANCOMER_DIS_MXP = this.SetValor(ta, "BANCOMER_DIS_MXP");
                        BANCOMER_DIS_USD = this.SetValor(ta, "BANCOMER_DIS_USD");
                        BANAMEX_DIS_MXP = this.SetValor(ta, "BANAMEX_DIS_MXP");
                        BANAMEX_DIS_USD = this.SetValor(ta, "BANAMEX_DIS_USD");
                        BANCOMER_PEN_MXP = this.SetValor(ta, "BANCOMER_PEN_MXP");
                        BANCOMER_PEN_USD = this.SetValor(ta, "BANCOMER_PEN_USD");
                        HSBC_DIS_MXP = this.SetValor(ta, "HSBC_DIS_MXP");
                        HSBC_DIS_USD = this.SetValor(ta, "HSBC_DIS_USD");
                        BANAMEX_PEN_MXP = this.SetValor(ta, "BANAMEX_PEN_MXP");
                        BANAMEX_PEN_USD = this.SetValor(ta, "BANAMEX_PEN_USD");
                        HSBC_PEN_MXP = this.SetValor(ta, "HSBC_PEN_MXP");
                        HSBC_PEN_USD = this.SetValor(ta, "HSBC_PEN_USD"); 

                        TC = this.SetValor(ta, "TC");
                        Disponible = BANCOMER_DIS_MXP + BANAMEX_DIS_MXP + HSBC_DIS_MXP;
                        Pendiente = BANCOMER_PEN_MXP + BANAMEX_PEN_MXP + HSBC_PEN_MXP;
                        n55 = BANCOMER_DIS_USD + BANAMEX_DIS_USD + HSBC_DIS_USD;
                        n75 = BANCOMER_PEN_USD + BANAMEX_PEN_USD + HSBC_PEN_USD;

                        txtV1.Text = BANCOMER_DIS_USD.ToString();
                        txtV2.Text = BANCOMER_DIS_MXP.ToString();
                        txtV3.Text = BANCOMER_PEN_USD.ToString();
                        txtV4.Text = BANCOMER_PEN_MXP.ToString();

                        txtV5.Text = BANAMEX_DIS_USD.ToString();
                        txtV6.Text = BANAMEX_DIS_MXP.ToString();
                        txtV7.Text = BANAMEX_PEN_USD.ToString();
                        txtV8.Text = BANAMEX_PEN_MXP.ToString();

                        txtV9.Text = HSBC_DIS_USD.ToString();
                        txtV10.Text = HSBC_DIS_MXP.ToString();
                        txtV11.Text = HSBC_PEN_USD.ToString();
                        txtV12.Text = HSBC_PEN_MXP.ToString();
                        //TC = (from tc in ta.AsEnumerable()
                        //      where tc.Field<string>("U_Nombre") == "TC"
                        //      select tc.Field<decimal>("U_Valor")).FirstOrDefault();

                        //Disponible = (from tc in ta.AsEnumerable()
                        //              where tc.Field<string>("U_Nombre") == "Disponible"
                        //              select tc.Field<decimal>("U_Valor")).FirstOrDefault();

                        //Pendiente = (from tc in ta.AsEnumerable()
                        //             where tc.Field<string>("U_Nombre") == "Pendiente"
                        //             select tc.Field<decimal>("U_Valor")).FirstOrDefault();

                        //Libre = (from tc in ta.AsEnumerable()
                        //         where tc.Field<string>("U_Nombre") == "Libre"
                        //         select tc.Field<decimal>("U_Valor")).FirstOrDefault();

                        //n55 = (from tc in ta.AsEnumerable()
                        //       where tc.Field<string>("U_Nombre") == "DisponibleUSD"
                        //       select tc.Field<decimal>("U_Valor")).FirstOrDefault();

                        //n75 = (from tc in ta.AsEnumerable()
                        //       where tc.Field<string>("U_Nombre") == "PendienteUSD"
                        //       select tc.Field<decimal>("U_Valor")).FirstOrDefault();

                        txtTC.Text = TC.ToString();
                        txt73.Text = Pendiente.ToString("C2");
                        txt53.Text = Disponible.ToString("C2");

                        txt55.Text = n55.ToString("C2");
                        txt75.Text = n75.ToString("C2");

                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }


        public decimal SetValor(DataTable ta, string _valor)
        {
            return (from tc in ta.AsEnumerable()
                   where tc.Field<string>("U_Nombre") == _valor
                   select tc.Field<decimal>("U_Valor")).FirstOrDefault();
        }

        //public void Totales(DataGridView dgv)
        //{
        //    try
        //    {
        //        DataTable table = new DataTable();
        //        table.Columns.Add("Total", typeof(decimal));
        //        table.Columns.Add("Propuesta", typeof(decimal));
        //        table.Columns.Add("Aprobado", typeof(decimal));

        //        if (dgv.Columns.Count > 0)
        //        {
        //            DataRow row = table.NewRow();

        //            row["Total"] = (dgv.DataSource as DataTable).Compute("SUM(Total)", string.Empty);
        //            row["Propuesta"] = (dgv.DataSource as DataTable).Compute("SUM(Propuesta)", string.Empty);
        //            row["Aprobado"] = (dgv.DataSource as DataTable).Compute("SUM(Aprobado)", string.Empty);

        //            table.Rows.Add(row);

        //            dgvTotal.DataSource = table;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        public decimal GetTotal(DataGridView dgv, string _moneda, int Columna, bool payUSD, string _clasificiacion)
        {
            decimal total = decimal.Zero;
            foreach (DataGridViewRow item in dgv.Rows)
            {
                if (Columna == (int)Columnas.Total)
                {

                    if (!Convert.ToString(item.Cells[(int)Columnas.Provedor].Value).Equals("0") && !Convert.ToString(item.Cells[(int)Columnas.Provedor].Value).Equals("Div"))
                    {
                        if (dgv.Name == "dgvImp" || dgv.Name == "dgvUSD")
                        {
                            if (Convert.ToString(item.Cells[(int)Columnas.Moneda].Value).Equals(_moneda) )//&& Convert.ToDecimal(item.Cells[Columna].Value) >= decimal.Zero && Convert.ToString(item.Cells[(int)Columnas.Clasificacion].Value).Equals(_clasificiacion))
                                total += Convert.ToDecimal(item.Cells[Columna].Value);
                        }
                        else
                        {
                           if (Convert.ToString(item.Cells[(int)Columnas.Moneda].Value).Equals(_moneda) )//&& Convert.ToString(item.Cells[(int)Columnas.Clasificacion].Value).Equals(_clasificiacion))
                                total += Convert.ToDecimal(item.Cells[Columna].Value);
                        }

                    }
                }
                else
                {
                    if (!Convert.ToString(item.Cells[(int)Columnas.Provedor].Value).Equals("0") && !Convert.ToString(item.Cells[(int)Columnas.Provedor].Value).Equals("Div"))
                    {
                        if (payUSD)
                        {
                            if (Columna == (int)Columnas.Propuesta || Columna == (int)Columnas.TotalMXP)
                            {
                                if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true && Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value) == payUSD )//&& Convert.ToString(item.Cells[(int)Columnas.Clasificacion].Value).Equals(_clasificiacion))
                                    total += Convert.ToDecimal(item.Cells[Columna].Value);
                            }
                            if (Columna == (int)Columnas.Aprobado || Columna == (int)Columnas.AprobadoMXP)
                            {
                                if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) == true && Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value) == payUSD )//&& Convert.ToString(item.Cells[(int)Columnas.Clasificacion].Value).Equals(_clasificiacion))
                                    total += Convert.ToDecimal(item.Cells[Columna].Value);
                            }
                        }
                        else
                        {
                            if (dgv.Name == "dgvMXP" || dgv.Name == "dgvBanco" )
                            {
                                if (Columna == (int)Columnas.Propuesta || Columna == (int)Columnas.TotalMXP)
                                {
                                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true && Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value) == payUSD)// && Convert.ToString(item.Cells[(int)Columnas.Clasificacion].Value).Equals(_clasificiacion))
                                        total += Convert.ToDecimal(item.Cells[Columna].Value);
                                }
                                if (Columna == (int)Columnas.Aprobado || Columna == (int)Columnas.AprobadoMXP)
                                {
                                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) == true && Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value) == payUSD)// Convert.ToString(item.Cells[(int)Columnas.Clasificacion].Value).Equals(_clasificiacion))
                                        total += Convert.ToDecimal(item.Cells[Columna].Value);
                                }
                            }
                            else
                            {
                                if (Columna == (int)Columnas.Propuesta || Columna == (int)Columnas.TotalMXP)
                                {
                                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit1].Value) == true && Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value) == payUSD)// && Convert.ToString(item.Cells[(int)Columnas.Clasificacion].Value).Equals(_clasificiacion))
                                        total += Convert.ToDecimal(item.Cells[Columna].Value) * TC;
                                }
                                if (Columna == (int)Columnas.Aprobado || Columna == (int)Columnas.AprobadoMXP)
                                {
                                    if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) == true && Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value) == payUSD)// && Convert.ToString(item.Cells[(int)Columnas.Clasificacion].Value).Equals(_clasificiacion))
                                        total += Convert.ToDecimal(item.Cells[Columna].Value) * TC;
                                }
                            }
                        }
                    }
                }
            }

            return total;

        }

        public void Totales()
        {
            try
            {
                l1 = l2 = l3 = l4 = l5 = l6 = l7 = decimal.Zero;

                foreach (DataGridViewRow item in dgvLibre.Rows)
                {
                    if (item.Cells["Fecha"].Value != DBNull.Value)
                        if (Convert.ToDateTime(item.Cells["Fecha"].Value) >= Convert.ToDateTime(dtFecha1.Value.ToShortDateString()) && Convert.ToDateTime(item.Cells["Fecha"].Value) <= Convert.ToDateTime(dtFecha2.Value.ToShortDateString()))
                        {
                            if (Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value) >= decimal.Zero)
                                l1 += Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value);

                            if (Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value) >= decimal.Zero)
                                l2 += Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value);

                            if (Convert.ToBoolean(item.Cells["Propuesta"].Value == DBNull.Value ? false : item.Cells["Propuesta"].Value) == true)
                            {
                                if (Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value) >= decimal.Zero)
                                    l4 += Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value);
                                if (Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value) >= decimal.Zero)
                                    l6 += Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value);
                            }
                            if (Convert.ToBoolean(item.Cells["Aprobado"].Value == DBNull.Value ? false : item.Cells["Aprobado"].Value) == true)
                            {
                                if (Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value) >= decimal.Zero)
                                    l5 += Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value);
                                if (Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value) >= decimal.Zero)
                                    l7 += Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value);
                            }
                        }
                }


                l3 = l2 + (l1 * TC);

                txtl1.Text = l1.ToString("C2");
                txtl2.Text = l2.ToString("C2");
                txtl3.Text = l3.ToString("C2");
                txtl4.Text = l4.ToString("C2");
                txtl5.Text = l5.ToString("C2");

                txtl6.Text = l6.ToString("C2");
                txtl7.Text = l7.ToString("C2");
            }
            catch (Exception)
            {

            }

            try
            {
                n00 = n01 = n02 = n03 = n04 = n05 = n06 = decimal.Zero;
                n10 = n11 = n12 = n13 = n14 = n15 = n16 = decimal.Zero;
                n20 = n21 = n22 = n23 = n24 = n25 = n26 = decimal.Zero;
                n30 = n31 = n32 = n33 = n34 = n35 = n36 = decimal.Zero;
                n40 = n41 = n42 = n43 = n44 = n45 = n46 = decimal.Zero;

                //MXP Urgente
                n00 = this.GetTotal(dgvMXP, "USD", (int)Columnas.Total, false, "A");
                n01 = this.GetTotal(dgvMXP, "$", (int)Columnas.Total, false, "A");

                n10 = this.GetTotal(dgvUSD, "USD", (int)Columnas.Total, false, "A");
                n11 = this.GetTotal(dgvUSD, "$", (int)Columnas.Total, false, "A");
                
                n20 = this.GetTotal(dgvImp, "USD", (int)Columnas.Total, false, "A");
                n21 = this.GetTotal(dgvImp, "$", (int)Columnas.Total, false, "A");

                n30 = this.GetTotal(dgvBanco, "USD", (int)Columnas.Total, false, "A");
                n31 = this.GetTotal(dgvBanco, "$", (int)Columnas.Total, false, "A");

                n03 = this.GetTotal(dgvMXP, "$", (int)Columnas.TotalMXP, false, "A");
                //n13 = this.GetTotal(dgvUSD, "$", (int)Columnas.TotalMXP, false, "A");
                n13 = this.GetTotal(dgvUSD, "$", (int)Columnas.Propuesta, false, "A");

                n23 = this.GetTotal(dgvImp, "$", (int)Columnas.Propuesta, false, "A");
                n33 = this.GetTotal(dgvBanco, "$", (int)Columnas.TotalMXP, false, "A");

                n04 = this.GetTotal(dgvMXP, "$", (int)Columnas.AprobadoMXP, false, "A");
                //n14 = this.GetTotal(dgvUSD, "$", (int)Columnas.AprobadoMXP, false, "A");
                n14 = this.GetTotal(dgvUSD, "$", (int)Columnas.Aprobado, false, "A");

                n24 = this.GetTotal(dgvImp, "$", (int)Columnas.Aprobado, false, "A");
                n34 = this.GetTotal(dgvBanco, "$", (int)Columnas.AprobadoMXP, false, "A");

                n05 = this.GetTotal(dgvMXP, "USD", (int)Columnas.Propuesta, false, "A");
               // n15 = this.GetTotal(dgvUSD, "USD", (int)Columnas.Propuesta, false, "A");
                n15 = this.GetTotal(dgvUSD, "USD", (int)Columnas.Propuesta, true, "A");

                n25 = this.GetTotal(dgvImp, "USD", (int)Columnas.Propuesta, true, "A");
                n35 = this.GetTotal(dgvBanco, "USD", (int)Columnas.Propuesta, false, "A");

                n06 = this.GetTotal(dgvMXP, "USD", (int)Columnas.Aprobado, false, "A");
                //n16 = this.GetTotal(dgvUSD, "USD", (int)Columnas.Aprobado, false, "A");
                n16 = this.GetTotal(dgvUSD, "USD", (int)Columnas.Aprobado, true, "A");

                n26 = this.GetTotal(dgvImp, "USD", (int)Columnas.Aprobado, true, "A");
                n36 = this.GetTotal(dgvBanco, "USD", (int)Columnas.Aprobado, false, "A");


                n02 = (n00 * TC) + n01;
                //na02 = (na00 * TC) + na01;
                n12 = (n10 * TC) + n11;
               // na12 = (na10 * TC) + na11;
                n22 = (n20 * TC) + n21;
                n32 = (n30 * TC) + n31;

                n40 = n00  + n10  + n20 + n30 + l1 + gtUSD;
                n41 = n01  + n11  + n21 + n31 + l2 + gtMXP;
                n42 = n02  + n12  + n22 + n32 + l3 + (gtMXP + (gtUSD) * TC);
                n43 = n03  + n13  + n23 + n33 + l4 + g00;
                n44 = n04  + n14  + n24 + n34 + l5 + g01;
                n45 = n05  + n15  + n25 + n35 + l6 + g10;
                n46 = n06  + n16  + n26 + n36 + l7 + g11;

                n56 = n55;
                n76 = n75;

                //MXP
                txt63.Text = (Disponible - n43).ToString("C2");
                txt83.Text = (Disponible + Pendiente - n43).ToString("C2");
                txt54.Text = Disponible.ToString("C2");
                txt64.Text = (Disponible - n44).ToString("C2");
                txt74.Text = Pendiente.ToString("C2");
                txt84.Text = (Disponible + Pendiente - n44).ToString("C2");

                //USD
                txt65.Text = (n55 - n45).ToString("C2");
                txt85.Text = (n55 + n75 - n45).ToString("C2");
                txt66.Text = (n56 - n46).ToString("C2");
                txt86.Text = (n56 + n76 - n46).ToString("C2");
                txt75.Text = n75.ToString("C2");
                txt76.Text = n76.ToString("C2");

                txt00.Text = n00.ToString("C2");
                txt10.Text = n10.ToString("C2");
                txt20.Text = n20.ToString("C2");
                txt30.Text = n30.ToString("C2");

                txt01.Text = n01.ToString("C2");
                txt11.Text = n11.ToString("C2");
                txt21.Text = n21.ToString("C2");
                txt31.Text = n31.ToString("C2");

                txt03.Text = n03.ToString("C2");
                txt13.Text = n13.ToString("C2");
                txt23.Text = n23.ToString("C2");
                txt33.Text = n33.ToString("C2");

                txt04.Text = n04.ToString("C2");
                txt14.Text = n14.ToString("C2");
                txt24.Text = n24.ToString("C2");
                txt34.Text = n34.ToString("C2");

                txt05.Text = n05.ToString("C2");
                txt15.Text = n15.ToString("C2");
                txt25.Text = n25.ToString("C2");
                txt35.Text = n35.ToString("C2");

                txt06.Text = n06.ToString("C2");
                txt16.Text = n16.ToString("C2");
                txt26.Text = n26.ToString("C2");
                txt36.Text = n36.ToString("C2");

                txt02.Text = n02.ToString("C2");
                txt12.Text = n12.ToString("C2");
                txt22.Text = n22.ToString("C2");
                txt32.Text = n32.ToString("C2");

                txt40.Text = n40.ToString("C2");
                txt41.Text = n41.ToString("C2");
                txt42.Text = n42.ToString("C2");
                txt43.Text = n43.ToString("C2");
                txt44.Text = n44.ToString("C2");
                txt45.Text = n45.ToString("C2");
                txt46.Text = n46.ToString("C2");

                txt56.Text = n56.ToString("C2");
                txt76.Text = n76.ToString("C2");

                txt53.Text = Disponible.ToString("C2");
                txt73.Text = Pendiente.ToString("C2");

                txt55.Text = n55.ToString("C2");

                //MXP Urgente
                //txtA00.Text = na00.ToString("C2");
                //txtA01.Text = na01.ToString("C2");
                //txtA02.Text = na02.ToString("C2");
                //txtA03.Text = na03.ToString("C2");
                //txtA04.Text = na04.ToString("C2");
                //txtA05.Text = na05.ToString("C2");
                //txtA06.Text = na06.ToString("C2");

                //txtA10.Text = na10.ToString("C2");
                //txtA11.Text = na11.ToString("C2");
                //txtA12.Text = na12.ToString("C2");
                //txtA13.Text = na13.ToString("C2");
                //txtA14.Text = na14.ToString("C2");
                //txtA15.Text = na15.ToString("C2");
                //txtA16.Text = na16.ToString("C2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable Filtar(DataTable _table)
        {
            DataTable _t1 = new DataTable();
            try
            {

                StringBuilder stbPagos = new StringBuilder();
                foreach (DataRowView item in cblPagos.CheckedItems)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!cblPagos.ToString().Equals(string.Empty))
                        {
                            stbPagos.Append(",");
                        }
                        stbPagos.Append(item["Codigo"].ToString());
                    }
                }
                if (cblPagos.CheckedItems.Count == 0)
                {
                    foreach (DataRowView item in cblPagos.Items)
                    {
                        if (item["Codigo"].ToString() != "0")
                        {
                            if (!cblPagos.ToString().Equals(string.Empty))
                            {
                                stbPagos.Append(",");
                            }
                            stbPagos.Append(item["Codigo"].ToString());
                        }
                    }
                }

                if (_table.Rows.Count > 0)
                {
                    string[] filtro = stbPagos.ToString().TrimStart(',').Split(new char[] { ',' });

                    var query = (from item in _table.AsEnumerable()
                                 where filtro.Contains(item.Field<string>("Situación"))
                                 select item);

                    if (query.Count() > 0)
                    {
                        _t1 = query.CopyToDataTable();

                        /////////////////

                        if (_t1.Rows.Count > 0)
                        {
                            var query1 = (from item in _t1.AsEnumerable()
                                          select item.Field<string>("Nombre")).Distinct();


                            foreach (var item in query1.ToList())
                            {
                                DataRow r = _t1.NewRow();
                                r["Estatus"] = string.Empty;
                                r["Proveedor"] = 0;
                                r["Nombre"] = item + " Total";

                                r["Moneda del documento"] = string.Empty;
                                r["Prop"] = false;
                                r["Apro"] = false;
                                r["Comentarios"] = (from acum in _t1.AsEnumerable()
                                                    where acum.Field<string>("Nombre") == item
                                                    select acum.Field<string>("U_Condiciones")).FirstOrDefault();
                                r["Situación"] = string.Empty;
                                r["Docentry"] = 0;
                                r["TC"] = 0;
                                r["Propuesta MXP"] = 0;
                                r["Aprobado MXP"] = 0;
                                r["Pagar USD"] = false;
                                r["Folio Factura"] = (from acum in _t1.AsEnumerable()
                                                      where acum.Field<string>("Nombre") == item
                                                      select acum.Field<string>("Cuenta")).FirstOrDefault();
                                r["Total"] = (from acum in _t1.AsEnumerable()
                                              where acum.Field<string>("Nombre") == item
                                              select acum.Field<decimal>("Total")).Sum();
                                r["Propuesta"] = (from acum in _t1.AsEnumerable()
                                                  where acum.Field<string>("Nombre") == item
                                                    && acum.Field<bool>("Prop") == true
                                                  select acum.Field<decimal>("Propuesta")).Sum();
                                r["Aprobado"] = (from acum in _t1.AsEnumerable()
                                                 where acum.Field<string>("Nombre") == item
                                                   && acum.Field<bool>("Apro") == true
                                                 select acum.Field<decimal>("Aprobado")).Sum();
                                r["Propuesta MXP"] = (from acum in _t1.AsEnumerable()
                                                      where acum.Field<string>("Nombre") == item
                                                        && acum.Field<bool>("Prop") == true
                                                      select acum.Field<decimal>("Propuesta MXP")).Sum();
                                r["Aprobado MXP"] = (from acum in _t1.AsEnumerable()
                                                     where acum.Field<string>("Nombre") == item
                                                       && acum.Field<bool>("Apro") == true
                                                     select acum.Field<decimal>("Aprobado MXP")).Sum();
                                _t1.Rows.Add(r);
                                DataRow row_div = _t1.NewRow();
                                row_div["Estatus"] = string.Empty;
                                row_div["Proveedor"] = "DIV";
                                row_div["Nombre"] = item + " Total";

                                row_div["Moneda del documento"] = string.Empty;
                                row_div["Prop"] = false;
                                row_div["Apro"] = false;
                                row_div["Comentarios"] = string.Empty;
                                row_div["Situación"] = string.Empty;
                                row_div["Docentry"] = 0;
                                row_div["TC"] = 0;
                                row_div["Propuesta MXP"] = 0;
                                row_div["Aprobado MXP"] = 0;
                                row_div["Pagar USD"] = false;
                                row_div["Folio Factura"] = decimal.Zero;
                                row_div["Total"] = decimal.Zero;
                                row_div["Propuesta"] = decimal.Zero;
                                row_div["Aprobado"] = decimal.Zero;
                                row_div["Propuesta MXP"] = decimal.Zero;
                                row_div["Aprobado MXP"] = decimal.Zero;
                                _t1.Rows.Add(row_div);
                            }

                            _t1 = (from tv in _t1.AsEnumerable()
                                   orderby tv.Field<string>("Nombre")
                                   select tv).CopyToDataTable();
                        }
                    }
                }
            }
            catch (Exception)
            {
                string name = _table.TableName;
            }
            return _t1;

        }

        public DataTable Fill(int tipoConsulta, int grpCode, string sucursales, string proveedres)
        {

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", tipoConsulta);
                    command.Parameters.AddWithValue("@FechaDesde", dtFecha1.Value);
                    command.Parameters.AddWithValue("@FechaHasta", dtFecha2.Value);
                    command.Parameters.AddWithValue("@Sucursales", sucursales);
                    command.Parameters.AddWithValue("@Proveedores", proveedres);
                    command.Parameters.AddWithValue("@GroupCode", grpCode);

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

                    if (table.Rows.Count > 0)
                    {
                        var query = (from item in table.AsEnumerable()
                                     select item.Field<string>("Nombre")).Distinct();


                        foreach (var item in query.ToList())
                        {
                            DataRow r = table.NewRow();
                            r["Estatus"] = string.Empty;
                            r["Proveedor"] = 0;
                            r["Nombre"] = item + " Total";

                            r["Moneda del documento"] = string.Empty;
                            r["Prop"] = false;
                            r["Apro"] = false;
                            r["Comentarios"] = (from acum in table.AsEnumerable()
                                                where acum.Field<string>("Nombre") == item
                                                select acum.Field<string>("U_Condiciones")).FirstOrDefault();
                            r["Situación"] = string.Empty;
                            r["Docentry"] = 0;
                            r["TC"] = 0;
                            r["Propuesta MXP"] = 0;
                            r["Aprobado MXP"] = 0;
                            r["Pagar USD"] = false;
                            r["Folio Factura"] = (from acum in table.AsEnumerable()
                                                  where acum.Field<string>("Nombre") == item
                                                  select acum.Field<string>("Cuenta")).FirstOrDefault();
                            r["Folio SAP"] = (from acum in table.AsEnumerable()
                                                  where acum.Field<string>("Nombre") == item
                                                  select acum.Field<decimal>("Descuento")).FirstOrDefault();
                            r["Monto original"] = (from acum in table.AsEnumerable()
                                          where acum.Field<string>("Nombre") == item
                                                   select acum.Field<decimal>("Monto original")).Sum();
                            r["Total"] = (from acum in table.AsEnumerable()
                                          where acum.Field<string>("Nombre") == item
                                          select acum.Field<decimal>("Total")).Sum();
                            r["Propuesta"] = (from acum in table.AsEnumerable()
                                              where acum.Field<string>("Nombre") == item
                                                && acum.Field<bool>("Prop") == true
                                              select acum.Field<decimal>("Propuesta")).Sum();
                            r["Aprobado"] = (from acum in table.AsEnumerable()
                                             where acum.Field<string>("Nombre") == item
                                               && acum.Field<bool>("Apro") == true
                                             select acum.Field<decimal>("Aprobado")).Sum();
                            r["Propuesta MXP"] = (from acum in table.AsEnumerable()
                                                  where acum.Field<string>("Nombre") == item
                                                    && acum.Field<bool>("Prop") == true
                                                  select acum.Field<decimal>("Propuesta MXP")).Sum();
                            r["Aprobado MXP"] = (from acum in table.AsEnumerable()
                                                 where acum.Field<string>("Nombre") == item
                                                   && acum.Field<bool>("Apro") == true
                                                 select acum.Field<decimal>("Aprobado MXP")).Sum();
                            table.Rows.Add(r);

                            DataRow row_div = table.NewRow();
                            row_div["Estatus"] = string.Empty;
                            row_div["Proveedor"] = "DIV";
                            row_div["Nombre"] = item + " Total";

                            row_div["Moneda del documento"] = string.Empty;
                            row_div["Prop"] = false;
                            row_div["Apro"] = false;
                            row_div["Comentarios"] = string.Empty;
                            row_div["Situación"] = string.Empty;
                            row_div["Docentry"] = 0;
                            row_div["TC"] = 0;
                            row_div["Propuesta MXP"] = 0;
                            row_div["Aprobado MXP"] = 0;
                            row_div["Pagar USD"] = false;
                            row_div["Folio Factura"] = decimal.Zero;
                            row_div["Total"] = decimal.Zero;
                            row_div["Propuesta"] = decimal.Zero;
                            row_div["Aprobado"] = decimal.Zero;
                            row_div["Propuesta MXP"] = decimal.Zero;
                            row_div["Aprobado MXP"] = decimal.Zero;
                            table.Rows.Add(row_div);

                        }

                        table = (from tv in table.AsEnumerable()
                                 orderby tv.Field<string>("Nombre")
                                 select tv).CopyToDataTable();

                    }
                    return table;
                }
            }


        }

        public void GetData(string query)
        {
            try
            {

                dataAdapter = new SqlDataAdapter(query, ClasesSGUV.Propiedades.conectionSGUV);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                DataTable _tableLibre = new DataTable();
                _tableLibre.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(_tableLibre);
                bindingSource1.DataSource = _tableLibre;
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }

        }

        public DataTable GetProveedores()
        {

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_PagosProveedores", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 6);
                    command.Parameters.AddWithValue("@FechaDesde", dtFecha1.Value);
                    command.Parameters.AddWithValue("@FechaHasta", dtFecha2.Value);
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

                    return table;
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

        public AutoCompleteStringCollection Autocomplete()
        {
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            //recorrer y cargar los items para el autocompletado
            foreach (DataRow row in _tblProveedores.Rows)
            {
                coleccion.Add(Convert.ToString(row["CardName"]));
            }

            return coleccion;
        }

        #endregion

        #region EVENTOS
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();

                btnFiltar.Visible = false;
                cblPagos.Visible = false;

                toolTotalSuma.Text = string.Empty;
                lblEstatus.Text = string.Empty;

                _tableMXP = (this.Fill(11, 1, string.Empty, string.Empty));
                //_tableMXP.Columns.Add("Propuesta MXP", typeof(decimal));
                //_tableMXP.Columns.Add("Aprobado MXP", typeof(decimal));

                _tableUSD = (this.Fill(11, 2, string.Empty, string.Empty));
                //_tableUSD.Columns.Add("Propuesta MXP", typeof(decimal));
                //_tableUSD.Columns.Add("Aprobado MXP", typeof(decimal));

                _tableIMP = (this.Fill(11, 3, string.Empty, string.Empty));
                //_tableIMP.Columns.Add("Propuesta MXP", typeof(decimal));
                //_tableIMP.Columns.Add("Aprobado MXP", typeof(decimal));

                _tableBanco = (this.Fill(11, 4, string.Empty, string.Empty));
                //_tableBanco.Columns.Add("Propuesta MXP", typeof(decimal));
                //_tableBanco.Columns.Add("Aprobado MXP", typeof(decimal));



                dgvMXP.DataSource = _tableMXP;// this.Fill(1, 1, string.Empty, string.Empty);
                dgvUSD.DataSource = _tableUSD; // this.Fill(1, 2, string.Empty, string.Empty);
                dgvImp.DataSource = _tableIMP; //this.Fill(1, 3, string.Empty, string.Empty);
                dgvBanco.DataSource = _tableBanco;//this.Fill(1, 4, string.Empty, string.Empty);


                dgvLibre.DataSource = null;
                dgvLibre.DataSource = bindingSource1;
                GetData(@"select 
			                Code, 
                            U_CreateDate 'Fecha',
			                U_Concepto 'Concepto', 
			                U_MXP 'MXP', 
			                U_USD 'USD',
                            U_Propuesta 'Propuesta',
                            U_Aprobado 'Aprobado',
                            U_ATZNOR 'ATZNOR',
                            U_SUSPENCIONES 'SUSPENCIONES',
                            U_MEZA 'MEZA',
                            cast(0 as decimal(16,6)) 'TOTAL' 
		                from PJ_PagosLibres");
               

                ////Formato Detalle
                if (dgvMXP.Columns.Count > 0)
                    this.Formato(dgvMXP, false, true, false, false);
                if (dgvUSD.Columns.Count > 0)
                    this.Formato(dgvUSD, true, false, false, true);//liz 30-03-2015
                this.Formato(dgvUSD, true, false, false, true);
                if (dgvImp.Columns.Count > 0)
                    this.Formato(dgvImp, true, false, false, true);
                if (dgvBanco.Columns.Count > 0)
                    this.Formato(dgvBanco, true, true, true, false);

                this.Formato(dgvLibre);

                try
                {
                    /*T O T A L E S      D E     E N C A B E Z A D O*/

                    DataTable _otrosProv = new DataTable();
                    _otrosProv = (DataTable)((BindingSource)dgvLibre.DataSource).DataSource;

                    decimal _mxp = decimal.Zero;
                    decimal _usd = decimal.Zero;
                    decimal _atznor = decimal.Zero;
                    decimal _suspenciones = decimal.Zero;
                    decimal _meza = decimal.Zero;


                    _mxp = Convert.ToDecimal(_otrosProv.Compute("SUM(MXP)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(MXP)", string.Empty));
                    _usd = Convert.ToDecimal(_otrosProv.Compute("SUM(USD)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(USD)", string.Empty));
                    _atznor = Convert.ToDecimal(_otrosProv.Compute("SUM(ATZNOR)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(ATZNOR)", string.Empty));
                    _suspenciones = Convert.ToDecimal(_otrosProv.Compute("SUM(SUSPENCIONES)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(SUSPENCIONES)", string.Empty));
                    _meza = Convert.ToDecimal(_otrosProv.Compute("SUM(MEZA)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(MEZA)", string.Empty));

                    DataTable _totales = new DataTable();
                    _totales = _otrosProv.Copy();
                    _totales.Rows.Clear();

                    DataRow _row = _totales.NewRow();
                    _row["MXP"] = _mxp;
                    _row["USD"] = _usd;
                    _row["ATZNOR"] = _atznor;
                    _row["SUSPENCIONES"] = _suspenciones;
                    _row["MEZA"] = _meza;
                    _row["TOTAL"] = _atznor + _suspenciones + _meza;



                    _totales.Rows.Add(_row);

                    dgvTotalesOtros.DataSource = _totales;
                    this.Formato(dgvTotalesOtros);
                }
                catch (Exception)
                {
               
                }

                try
                {
                    //subtotales RACSA
                    decimal _atznor = decimal.Zero;
                    decimal _suspenciones = decimal.Zero;
                    decimal _meza = decimal.Zero;

                    foreach (DataGridViewRow item in dgvLibre.Rows)
                    {
                        _atznor = Convert.ToDecimal(item.Cells["ATZNOR"].Value == DBNull.Value ? decimal.Zero : item.Cells["ATZNOR"].Value);
                        _suspenciones = Convert.ToDecimal(item.Cells["SUSPENCIONES"].Value == DBNull.Value ? decimal.Zero : item.Cells["SUSPENCIONES"].Value);
                        _meza = Convert.ToDecimal(item.Cells["MEZA"].Value == DBNull.Value ? decimal.Zero : item.Cells["MEZA"].Value);

                        item.Cells["TOTAL"].Value = _atznor + _suspenciones + _meza;
                    }

                    dgvLibre.Columns[(int)ColumnasLibre.Code].Visible = false;
                }
                catch (Exception)
                {
                }

                btnFiltar.Visible = true;
                cblPagos.Visible = true;

                this.CaragarValores();

                txt75.Text = n75.ToString("C2");

                this.Totales();

                this.GastosProveedores();//PJ
                this.GastosProveedoresSC();//Services
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    //Colorear estatus
                    if (item.Cells[(int)Columnas.Estatus].Value.ToString() == "Vencido")
                    {
                        item.Cells[(int)Columnas.Estatus].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Estatus].Style.ForeColor = Color.White;
                    }
                    else if (item.Cells[(int)Columnas.Estatus].Value.ToString() == string.Empty)
                    {
                        item.Cells[(int)Columnas.Provedor].Style.ForeColor = Color.White;

                        item.Cells[(int)Columnas.Nombre].Style.Font = new Font("Calibri", 12, FontStyle.Bold);
                        item.Cells[(int)Columnas.Total].Style.Font = new Font("Calibri", 12, FontStyle.Bold);
                        item.Cells[(int)Columnas.MontoOriginal].Style.Font = new Font("Calibri", 12, FontStyle.Bold);
                        item.Cells[(int)Columnas.Propuesta].Style.Font = new Font("Calibri", 12, FontStyle.Bold);
                        item.Cells[(int)Columnas.TotalMXP].Style.Font = new Font("Calibri", 12, FontStyle.Bold);
                        item.Cells[(int)Columnas.Aprobado].Style.Font = new Font("Calibri", 12, FontStyle.Bold);
                        item.Cells[(int)Columnas.AprobadoMXP].Style.Font = new Font("Calibri", 12, FontStyle.Bold);

                        item.Cells[(int)Columnas.FolioSAP].Style.BackColor = Color.Orange;
                        item.Cells[(int)Columnas.FolioSAP].Style.Format = "P2";
                        item.Cells[(int)Columnas.FolioSAP].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        item.Cells[(int)Columnas.Cuenta].Style.Font = new Font("Calibri", 9, FontStyle.Regular);
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Estatus].Style.BackColor = Color.Green;
                        item.Cells[(int)Columnas.Estatus].Style.ForeColor = Color.Black;
                    }


                    //Colorear Aprobados
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.AprobadoMXP].Value) != decimal.Zero)
                    {
                        item.Cells[(int)Columnas.AprobadoMXP].Style.BackColor = Color.FromArgb(216, 228, 188);
                        item.Cells[(int)Columnas.AprobadoMXP].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.AprobadoMXP].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.AprobadoMXP].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Aprobado].Value) != decimal.Zero)
                    {
                        item.Cells[(int)Columnas.Aprobado].Style.BackColor = Color.FromArgb(216, 228, 188);
                        item.Cells[(int)Columnas.Aprobado].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Aprobado].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Aprobado].Style.ForeColor = Color.Black;
                    }

                    //Colorear Propuestas
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.TotalMXP].Value) != decimal.Zero)
                    {
                        item.Cells[(int)Columnas.TotalMXP].Style.BackColor = Color.FromArgb(255, 255, 153);
                        item.Cells[(int)Columnas.TotalMXP].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.TotalMXP].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.TotalMXP].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Propuesta].Value) != decimal.Zero)
                    {
                        item.Cells[(int)Columnas.Propuesta].Style.BackColor = Color.FromArgb(255, 255, 153);
                        item.Cells[(int)Columnas.Propuesta].Style.ForeColor = Color.Black;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Propuesta].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Propuesta].Style.ForeColor = Color.Black;
                    }

                    //Formato separadores
                    if (Convert.ToString(item.Cells[(int)Columnas.Provedor].Value).Equals("DIV"))
                    {
                        item.DefaultCellStyle.ForeColor = Color.White;
                        item.DefaultCellStyle.BackColor = Color.White;
                        item.Height = 15;

                        item.Cells[(int)Columnas.AprobadoMXP].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.AprobadoMXP].Style.ForeColor = Color.White;
                        item.Cells[(int)Columnas.Aprobado].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Aprobado].Style.ForeColor = Color.White;
                        item.Cells[(int)Columnas.TotalMXP].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.TotalMXP].Style.ForeColor = Color.White;
                        item.Cells[(int)Columnas.Propuesta].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Propuesta].Style.ForeColor = Color.White;
                        item.Cells[(int)Columnas.FolioSAP].Style.BackColor = Color.White;
                    }

                }
                this.Totales();
            }
            catch (Exception)
            {

            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                    if (e.ColumnIndex == (int)Columnas.Bit1)
                    {
                        if (Convert.ToBoolean(row.Cells[(int)Columnas.Bit1].Value) == false)
                        {
                            //if (row.Cells[(int)Columnas.Situacion].Value.ToString() != "Aprobado")
                            {
                                if (Convert.ToString(row.Cells[(int)Columnas.Moneda].Value) == "USD" && Convert.ToDecimal(row.Cells[(int)Columnas.TC].Value) > 0)
                                    row.Cells[(int)Columnas.TotalMXP].Value = Convert.ToDecimal(row.Cells[(int)Columnas.Total].Value) * Convert.ToDecimal(row.Cells[(int)Columnas.TC].Value);
                                if (Convert.ToString(row.Cells[(int)Columnas.Moneda].Value) == "USD" && Convert.ToDecimal(row.Cells[(int)Columnas.TC].Value) == 0)
                                    row.Cells[(int)Columnas.Propuesta].Value = Convert.ToDecimal(row.Cells[(int)Columnas.Total].Value);
                                if (Convert.ToString(row.Cells[(int)Columnas.Moneda].Value) == "$")
                                    row.Cells[(int)Columnas.TotalMXP].Value = row.Cells[(int)Columnas.Total].Value;

                                row.Cells[(int)Columnas.Bit1].Value = true;
                            }

                            if (row.Cells[(int)Columnas.Nombre].Value.ToString().Contains("Total"))
                            {
                                foreach (DataGridViewRow itemSelect in (sender as DataGridView).Rows)
                                {
                                    if (row.Cells[(int)Columnas.Nombre].Value.ToString().Replace("Total", "").Trim() == itemSelect.Cells[(int)Columnas.Nombre].Value.ToString().Trim())
                                    {
                                        //if (itemSelect.Cells[(int)Columnas.Situacion].Value.ToString() != "Aprobado")
                                        {

                                            if (Convert.ToString(itemSelect.Cells[(int)Columnas.Moneda].Value) == "USD" && Convert.ToDecimal(itemSelect.Cells[(int)Columnas.TC].Value) > 0)
                                                itemSelect.Cells[(int)Columnas.TotalMXP].Value = Convert.ToDecimal(itemSelect.Cells[(int)Columnas.Total].Value) * Convert.ToDecimal(itemSelect.Cells[(int)Columnas.TC].Value);
                                            if (Convert.ToString(itemSelect.Cells[(int)Columnas.Moneda].Value) == "USD" && Convert.ToDecimal(itemSelect.Cells[(int)Columnas.TC].Value) == 0)
                                                itemSelect.Cells[(int)Columnas.Propuesta].Value = Convert.ToDecimal(itemSelect.Cells[(int)Columnas.Total].Value);
                                            if (Convert.ToString(itemSelect.Cells[(int)Columnas.Moneda].Value) == "$")
                                                itemSelect.Cells[(int)Columnas.TotalMXP].Value = itemSelect.Cells[(int)Columnas.Total].Value;

                                            itemSelect.Cells[(int)Columnas.Bit1].Value = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            row.Cells[(int)Columnas.Propuesta].Value = decimal.Zero;
                            row.Cells[(int)Columnas.TotalMXP].Value = decimal.Zero;
                            row.Cells[(int)Columnas.Bit1].Value = false;

                            if (row.Cells[(int)Columnas.Nombre].Value.ToString().Contains("Total"))
                            {
                                foreach (DataGridViewRow itemSelect in (sender as DataGridView).Rows)
                                {
                                    if (row.Cells[(int)Columnas.Nombre].Value.ToString().Replace("Total", "").Trim() == itemSelect.Cells[(int)Columnas.Nombre].Value.ToString().Trim())
                                    {
                                        itemSelect.Cells[(int)Columnas.Propuesta].Value = decimal.Zero;
                                        itemSelect.Cells[(int)Columnas.TotalMXP].Value = decimal.Zero;

                                        itemSelect.Cells[(int)Columnas.Bit1].Value = false;
                                    }
                                }
                            }

                        }

                    }

                    if (e.ColumnIndex == (int)Columnas.Bit2 && Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || e.ColumnIndex == (int)Columnas.Bit2 && Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                    {
                        if (Convert.ToBoolean(row.Cells[(int)Columnas.Bit2].Value) == false)
                        {
                            //if (row.Cells[(int)Columnas.Situacion].Value.ToString() != "Aprobado")
                            {

                                if (Convert.ToString(row.Cells[(int)Columnas.Moneda].Value) == "USD" && Convert.ToDecimal(row.Cells[(int)Columnas.TC].Value) > 0)
                                    row.Cells[(int)Columnas.AprobadoMXP].Value = Convert.ToDecimal(row.Cells[(int)Columnas.TotalMXP].Value);// *Convert.ToDecimal(row.Cells[(int)Columnas.TC].Value);
                                if (Convert.ToString(row.Cells[(int)Columnas.Moneda].Value) == "USD" && Convert.ToDecimal(row.Cells[(int)Columnas.TC].Value) == 0)
                                    row.Cells[(int)Columnas.Aprobado].Value = Convert.ToDecimal(row.Cells[(int)Columnas.Propuesta].Value);
                                if (Convert.ToString(row.Cells[(int)Columnas.Moneda].Value) == "$")
                                    row.Cells[(int)Columnas.AprobadoMXP].Value = row.Cells[(int)Columnas.TotalMXP].Value;

                                row.Cells[(int)Columnas.Bit2].Value = true;
                            }

                            if (row.Cells[(int)Columnas.Nombre].Value.ToString().Contains("Total"))
                            {
                                foreach (DataGridViewRow itemSelect in (sender as DataGridView).Rows)
                                {
                                    if (row.Cells[(int)Columnas.Nombre].Value.ToString().Replace("Total", "").Trim() == itemSelect.Cells[(int)Columnas.Nombre].Value.ToString().Trim())
                                    {
                                        //if (itemSelect.Cells[(int)Columnas.Situacion].Value.ToString() != "Aprobado")
                                        {
                                            if (Convert.ToString(itemSelect.Cells[(int)Columnas.Moneda].Value) == "USD" && Convert.ToDecimal(itemSelect.Cells[(int)Columnas.TC].Value) > 0)
                                                itemSelect.Cells[(int)Columnas.AprobadoMXP].Value = Convert.ToDecimal(itemSelect.Cells[(int)Columnas.TotalMXP].Value) * Convert.ToDecimal(itemSelect.Cells[(int)Columnas.TC].Value);
                                            if (Convert.ToString(itemSelect.Cells[(int)Columnas.Moneda].Value) == "USD" && Convert.ToDecimal(itemSelect.Cells[(int)Columnas.TC].Value) == 0)
                                                itemSelect.Cells[(int)Columnas.Aprobado].Value = Convert.ToDecimal(itemSelect.Cells[(int)Columnas.Propuesta].Value);
                                            if (Convert.ToString(itemSelect.Cells[(int)Columnas.Moneda].Value) == "$")
                                                itemSelect.Cells[(int)Columnas.AprobadoMXP].Value = itemSelect.Cells[(int)Columnas.TotalMXP].Value;

                                            itemSelect.Cells[(int)Columnas.Bit2].Value = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            row.Cells[(int)Columnas.Aprobado].Value = decimal.Zero;
                            row.Cells[(int)Columnas.AprobadoMXP].Value = decimal.Zero;

                            row.Cells[(int)Columnas.Bit2].Value = false;

                            if (row.Cells[(int)Columnas.Nombre].Value.ToString().Contains("Total"))
                            {
                                foreach (DataGridViewRow itemSelect in (sender as DataGridView).Rows)
                                {
                                    if (row.Cells[(int)Columnas.Nombre].Value.ToString().Replace("Total", "").Trim() == itemSelect.Cells[(int)Columnas.Nombre].Value.ToString().Trim())
                                    {
                                        itemSelect.Cells[(int)Columnas.Aprobado].Value = decimal.Zero;
                                        itemSelect.Cells[(int)Columnas.AprobadoMXP].Value = decimal.Zero;

                                        itemSelect.Cells[(int)Columnas.Bit2].Value = false;
                                    }
                                }
                            }
                        }
                    }

                    if (e.ColumnIndex == (int)Columnas.PagarUSD)
                        if (row.Cells[(int)Columnas.Nombre].Value.ToString().Contains("Total"))
                            if (Convert.ToBoolean(row.Cells[(int)Columnas.PagarUSD].Value) == false)
                            {
                                row.Cells[(int)Columnas.PagarUSD].Value = true;

                                foreach (DataGridViewRow itemSelect in (sender as DataGridView).Rows)
                                {
                                    if (row.Cells[(int)Columnas.Nombre].Value.ToString().Replace("Total", "").Trim() == itemSelect.Cells[(int)Columnas.Nombre].Value.ToString().Trim())
                                    {
                                        //if (itemSelect.Cells[(int)Columnas.Situacion].Value.ToString() != "Aprobado")
                                        {
                                            itemSelect.Cells[(int)Columnas.PagarUSD].Value = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                row.Cells[(int)Columnas.PagarUSD].Value = false;

                                foreach (DataGridViewRow itemSelect in (sender as DataGridView).Rows)
                                {
                                    if (row.Cells[(int)Columnas.Nombre].Value.ToString().Replace("Total", "").Trim() == itemSelect.Cells[(int)Columnas.Nombre].Value.ToString().Trim())
                                    {
                                        //if (itemSelect.Cells[(int)Columnas.Situacion].Value.ToString() != "Aprobado")
                                        {
                                            itemSelect.Cells[(int)Columnas.PagarUSD].Value = false;
                                        }
                                    }
                                }
                            }
                        else
                        {
                            if (Convert.ToBoolean(row.Cells[(int)Columnas.PagarUSD].Value) == false)
                        
                                row.Cells[(int)Columnas.PagarUSD].Value = true;
                            else
                                row.Cells[(int)Columnas.PagarUSD].Value = false;

                        
                        }

                }
                Totales();
                //Totales encabezado
                TabPage selected = tabControl1.SelectedTab;
                if (selected.Text.Equals("MXP"))
                {
                    txtPropMXP.Text = txt03.Text;
                    txtAproMXP.Text = txt04.Text;
                    txtPropUSD.Text = txt05.Text;
                    txtAproUSD.Text = txt06.Text;
                }
                if (selected.Text.Equals("USD"))
                {
                    txtPropMXP.Text = txt13.Text;
                    txtAproMXP.Text = txt14.Text;
                    txtPropUSD.Text = txt15.Text;
                    txtAproUSD.Text = txt16.Text;
                }
                if (selected.Text.Equals("Importación"))
                {
                    txtPropMXP.Text = txt23.Text;
                    txtAproMXP.Text = txt24.Text;
                    txtPropUSD.Text = txt25.Text;
                    txtAproUSD.Text = txt26.Text;
                }
                if (selected.Text.Equals("Banco"))
                {
                    txtPropMXP.Text = txt33.Text;
                    txtAproMXP.Text = txt34.Text;
                    txtPropUSD.Text = txt35.Text;
                    txtAproUSD.Text = txt36.Text;
                }
                if (selected.Text.Equals("Otros Proveedores"))
                {
                    txtPropMXP.Text = txtl4.Text;
                    txtAproMXP.Text = txtl5.Text;
                    txtPropUSD.Text = txtl6.Text;
                    txtAproUSD.Text = txtl7.Text;
                }
                if (selected.Text.Equals("Gastos"))
                {
                    txtPropMXP.Text = string.Empty;
                    txtAproMXP.Text = string.Empty;
                    txtPropUSD.Text = string.Empty;
                    txtAproUSD.Text = string.Empty;
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgv_CellClickP(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void GastosProveedores()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_GastosPagos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Code", 0);
                    command.Parameters.AddWithValue("@User", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Concepto", string.Empty);
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                    command.Parameters.AddWithValue("@Aprobado", decimal.Zero);
                    command.Parameters.AddWithValue("@Solicita", string.Empty);
                    command.Parameters.AddWithValue("@FechaPago", DateTime.Now);
                    command.Parameters.AddWithValue("@Cuenta", string.Empty);
                    command.Parameters.AddWithValue("@Banco", string.Empty);
                    command.Parameters.AddWithValue("@TipoPago", string.Empty);
                    command.Parameters.AddWithValue("@Prioridad", 0);
                    command.Parameters.AddWithValue("@Estatus", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);


                    if (table.Rows.Count > 0)
                    {
                        try
                        {
                            var query = (from item in table.AsEnumerable()
                                         select item.Field<string>("U_TipoPago")).Distinct();


                            foreach (var item in query.ToList())
                            {
                                DataRow r = table.NewRow();
                                r["Proveedor"] = string.Empty;
                                r["Concepto"] = string.Empty;
                                r["Factura"] = item + " Total";

                                r["Moneda"] = string.Empty;
                                r["Aprob"] = false;
                                r["Propuesta"] = (from acum in table.AsEnumerable()
                                                  where acum.Field<string>("U_TipoPago") == item
                                                  select acum.Field<decimal>("Propuesta")).Sum();
                                r["Aprobado"] = (from acum in table.AsEnumerable()
                                                 where acum.Field<string>("U_TipoPago") == item
                                                 select acum.Field<decimal>("Propuesta")).Sum();
                                r["Solicita"] = string.Empty;
                                r["Cuenta"] = string.Empty;
                                r["Banco"] = string.Empty;
                                r["U_TipoPago"] = item + " Total";

                                table.Rows.Add(r);
                            }

                            table = (from tv in table.AsEnumerable()
                                     orderby tv.Field<string>("U_TipoPago")
                                     select tv).CopyToDataTable();

                            dgvGastos.DataSource = table;



                            this.FormatoGastos(dgvGastos);

                            DataTable _tblGastos1 = (from item in (dgvGastos.DataSource as DataTable).AsEnumerable()
                                                     where !item.Field<string>("U_TipoPago").Contains("Total")
                                                     select item).CopyToDataTable();
                            

                            DataTable _tblGastos = (from item in _tblGastos1.AsEnumerable()
                                                    where item.Field<DateTime>("Fecha limite") >= dtFecha1.Value &&
                                                          item.Field<DateTime>("Fecha limite") <= dtFecha2.Value
                                                    select item).CopyToDataTable();


                            txtg1.Text = decimal.Zero.ToString("C2");
                            txtg2.Text = decimal.Zero.ToString("C2");
                            txtg3.Text = decimal.Zero.ToString("C2");
                            txtg4.Text = decimal.Zero.ToString("C2");
                            txtg5.Text = decimal.Zero.ToString("C2");
                            txtg6.Text = decimal.Zero.ToString("C2");
                            txtg7.Text = decimal.Zero.ToString("C2");

                            g00 = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'"));
                            g10 = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'"));

                            gtMXP = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'"));
                            gtUSD = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'"));

                            g20 = g00 + (g10 * TC);

                            g01 = Convert.ToDecimal(_tblGastos.Compute("SUM(Aprobado)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Aprobado)", "Moneda = 'MXP'"));
                            g11 = Convert.ToDecimal(_tblGastos.Compute("SUM(Aprobado)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Aprobado)", "Moneda = 'USD'"));
                            g21 = g01 + (g11 * TC);

                            txtg00.Text = g00.ToString("C2");
                            txtg10.Text = g10.ToString("C2");
                            txtg01.Text = g01.ToString("C2");
                            txtg11.Text = g11.ToString("C2");

                            txtg21.Text = g21.ToString("C2");
                            txtg20.Text = g20.ToString("C2");

                            txtg1.Text = gtUSD.ToString("C2");
                            txtg2.Text = gtMXP.ToString("C2");
                            txtg3.Text = (gtMXP + (gtUSD * TC)).ToString("C2");
                            txtg4.Text = g00.ToString("C2");
                            txtg5.Text = g01.ToString("C2");
                            txtg6.Text = g10.ToString("C2");
                            txtg7.Text = g11.ToString("C2");

                            
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        }

                    }
                }

            }
        }

        public void GastosProveedoresSC()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_GastosPagos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 4);
                    command.Parameters.AddWithValue("@Code", 0);
                    command.Parameters.AddWithValue("@User", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Concepto", string.Empty);
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                    command.Parameters.AddWithValue("@Aprobado", decimal.Zero);
                    command.Parameters.AddWithValue("@Solicita", string.Empty);
                    command.Parameters.AddWithValue("@FechaPago", DateTime.Now);
                    command.Parameters.AddWithValue("@Cuenta", string.Empty);
                    command.Parameters.AddWithValue("@Banco", string.Empty);
                    command.Parameters.AddWithValue("@TipoPago", string.Empty);
                    command.Parameters.AddWithValue("@Prioridad", 0);
                    command.Parameters.AddWithValue("@Estatus", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);


                    if (table.Rows.Count > 0)
                    {
                        try
                        {
                            var query = (from item in table.AsEnumerable()
                                         select item.Field<string>("U_TipoPago")).Distinct();


                            foreach (var item in query.ToList())
                            {
                                DataRow r = table.NewRow();
                                r["Proveedor"] = string.Empty;
                                r["Concepto"] = string.Empty;
                                r["Factura"] = item + " Total";

                                r["Moneda"] = string.Empty;
                                r["Aprob"] = false;
                                r["Propuesta"] = (from acum in table.AsEnumerable()
                                                  where acum.Field<string>("U_TipoPago") == item
                                                  select acum.Field<decimal>("Propuesta")).Sum();
                                r["Aprobado"] = (from acum in table.AsEnumerable()
                                                 where acum.Field<string>("U_TipoPago") == item
                                                 select acum.Field<decimal>("Propuesta")).Sum();
                                r["Solicita"] = string.Empty;
                                r["Cuenta"] = string.Empty;
                                r["Banco"] = string.Empty;
                                r["U_TipoPago"] = item + " Total";

                                table.Rows.Add(r);
                            }

                            table = (from tv in table.AsEnumerable()
                                     orderby tv.Field<string>("U_TipoPago")
                                     select tv).CopyToDataTable();

                            dgvGastosSC.DataSource = table;



                            this.FormatoGastos(dgvGastosSC);

                            DataTable _tblGastos1 = (from item in (dgvGastosSC.DataSource as DataTable).AsEnumerable()
                                                     where !item.Field<string>("U_TipoPago").Contains("Total")
                                                     select item).CopyToDataTable();


                            DataTable _tblGastos = (from item in _tblGastos1.AsEnumerable()
                                                    where item.Field<DateTime>("Fecha limite") >= dtFecha1.Value &&
                                                          item.Field<DateTime>("Fecha limite") <= dtFecha2.Value
                                                    select item).CopyToDataTable();



                            decimal g00SC = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'"));
                            decimal g10SC = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'"));

                            decimal gtMXPSC = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'"));
                            decimal gtUSDSC = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'"));

                            decimal g20SC = g00SC + (g10SC * TC);

                            decimal g01SC = Convert.ToDecimal(_tblGastos.Compute("SUM(Aprobado)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Aprobado)", "Moneda = 'MXP'"));
                            decimal g11SC = Convert.ToDecimal(_tblGastos.Compute("SUM(Aprobado)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Aprobado)", "Moneda = 'USD'"));
                            decimal g21SC = g01SC + (g11SC * TC);

                            txtg00SC.Text = g00SC.ToString("C2");
                            txtg10SC.Text = g10SC.ToString("C2");
                            txtg01SC.Text = g01SC.ToString("C2");
                            txtg11SC.Text = g11SC.ToString("C2");

                            txtg21SC.Text = g21SC.ToString("C2");
                            txtg20SC.Text = g20SC.ToString("C2");
                            /*
                             
                            txtg00.Text = g00.ToString("C2");
                            txtg10.Text = g10.ToString("C2");
                            txtg01.Text = g01.ToString("C2");
                            txtg11.Text = g11.ToString("C2");

                            txtg21.Text = g21.ToString("C2");
                            txtg20.Text = g20.ToString("C2");

                            txtg1.Text = gtUSD.ToString("C2");
                            txtg2.Text = gtMXP.ToString("C2");
                            txtg3.Text = (gtMXP + (gtUSD * TC)).ToString("C2");
                            txtg4.Text = g00.ToString("C2");
                            txtg5.Text = g01.ToString("C2");
                            txtg6.Text = g10.ToString("C2");
                            txtg7.Text = g11.ToString("C2");
                             */


                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        }

                    }
                }

            }
        }

        private void PagosProveedores_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            lblUrgente.BackColor = Color.FromArgb(192, 0, 0);
            lblInter.BackColor = Color.FromArgb(255, 255, 0);
            lblVencido.BackColor = Color.FromArgb(0, 176, 80);

            lblUrgente.ForeColor = Color.White;
            lblInter.ForeColor = Color.Black;
            lblVencido.ForeColor = Color.Black; 


            log = new Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

            if (Rol == (int)(ClasesSGUV.Propiedades.RolesHalcoNET.GerentePagos) || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
            {

                btnUpdate.Visible = false;
                //leerVariables();

                pictureBox1.BackColor = Color.White;

                DataTable t = new DataTable();

                t.Columns.Add("Codigo", typeof(string));
                DataRow r1 = t.NewRow();
                r1["Codigo"] = "Todo";
                DataRow r2 = t.NewRow();
                r2["Codigo"] = "Normal";
                DataRow r3 = t.NewRow();
                r3["Codigo"] = "Propuesta";
                DataRow r4 = t.NewRow();
                r4["Codigo"] = "Aprobado";

                t.Rows.Add(r1);
                t.Rows.Add(r2);
                t.Rows.Add(r3);
                t.Rows.Add(r4);

                cblPagos.DataSource = t;

                cblPagos.DisplayMember = "Codigo";
                cblPagos.ValueMember = "Codigo";

                this.CaragarValores();

                foreach (Control item in tpGastos.Controls)
                {
                    item.Visible = false;
                }
            }

            if (Rol == (int)(ClasesSGUV.Propiedades.RolesHalcoNET.GerentePagos) || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)(ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas))
            {
                this.CaragarValores();
                btnUpdate.Visible = true;
                if (Rol == (int)(ClasesSGUV.Propiedades.RolesHalcoNET.GerentePagos))
                {

                    //groupBox1.Visible = false;

                    //dgvMXP.Visible = false;
                    //dgvUSD.Visible = false;
                    //dgvImp.Visible = false;
                    //dgvLibre.Visible = false;
                    //dgvBanco.Visible = false;

                    //label1.Visible = false;
                    //label2.Visible = false;
                    //dtFecha1.Visible = false;
                    //dtFecha2.Visible = false;
                    //kryptonButton1.Visible = false;
                    //kryptonButton2.Visible = false;
                }

                _tblProveedores = this.GetProveedores();
                txtProveedor.DataSource = _tblProveedores;
                txtProveedor.ValueMember = "CardCode";
                txtProveedor.DisplayMember = "CardName";

                txtProveedor.AutoCompleteCustomSource = Autocomplete();
                txtProveedor.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource;

                this.GastosProveedores();
                this.GastosProveedoresSC();

                foreach (Control item in tpGastos.Controls)
                {
                    item.Visible = true;
                }
            }
        }

        private void kryptonCommand1_Execute(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                //grabarVariable("FechaDesde", dtFecha1.Value.ToShortDateString());
                //grabarVariable("FechaHasta", dtFecha2.Value.ToShortDateString());

                lblEstatus.Text = string.Empty;
                lblEstatus.BackColor = Color.FromName("Control");
                lblEstatus.ForeColor = Color.Black;

                if (string.IsNullOrEmpty(log.Usuario))
                    log.Usuario = "Pruebas";

                this.SaveGrid(dgvMXP);
                this.SaveGrid(dgvUSD);
                this.SaveGrid(dgvImp);
                this.SaveGrid(dgvBanco);

                this.SaveValue(Disponible, "Disponible");
                this.SaveValue(Pendiente, "Pendiente");
                this.SaveValue(TC, "TC");
                this.SaveValue(Libre, "Libre");
                this.SaveValue(n55, "DisponibleUSD");
                this.SaveValue(n75, "PendienteUSD");

                this.SaveValue(Convert.ToDecimal(txtV2.Text), "BANCOMER_DIS_MXP");
                this.SaveValue(Convert.ToDecimal(txtV1.Text), "BANCOMER_DIS_USD");
                this.SaveValue(Convert.ToDecimal(txtV6.Text), "BANAMEX_DIS_MXP");
                this.SaveValue(Convert.ToDecimal(txtV5.Text), "BANAMEX_DIS_USD");
                this.SaveValue(Convert.ToDecimal(txtV10.Text), "HSBC_DIS_MXP");
                this.SaveValue(Convert.ToDecimal(txtV9.Text), "HSBC_DIS_USD");
                this.SaveValue(Convert.ToDecimal(txtV8.Text), "BANAMEX_PEN_MXP");
                this.SaveValue(Convert.ToDecimal(txtV7.Text), "BANAMEX_PEN_USD");
                this.SaveValue(Convert.ToDecimal(txtV12.Text), "HSBC_PEN_MXP");
                this.SaveValue(Convert.ToDecimal(txtV11.Text), "HSBC_PEN_USD");
                this.SaveValue(Convert.ToDecimal(txtV4.Text), "BANCOMER_PEN_MXP");
                this.SaveValue(Convert.ToDecimal(txtV3.Text), "BANCOMER_PEN_USD");

                lblEstatus.Text = "Registro exitoso.";
                lblEstatus.BackColor = Color.Green;
                lblEstatus.ForeColor = Color.Black;

                dataAdapter.Update((DataTable)bindingSource1.DataSource);
                GetData(dataAdapter.SelectCommand.CommandText);
            }
            catch (Exception ex)
            {
                lblEstatus.Text = ex.Message;
                lblEstatus.BackColor = Color.Red;
                lblEstatus.ForeColor = Color.White;
            }
            finally
            {
                this.Continuar();
            }
        }

        private void txtTC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TC = Convert.ToDecimal(txtTC.Text);

                txtTC.BackColor = Color.FromName("Control");
                txtTC.ForeColor = Color.Black;
            }
            catch (Exception)
            {
                TC = 0;

                txtTC.BackColor = Color.Red;
                txtTC.ForeColor = Color.White;
            }
            finally
            {
                this.Totales();
            }
        }

        private void cblPagos_Click(object sender, EventArgs e)
        {
            if (cblPagos.SelectedIndex == 0)
            {
                if (cblPagos.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < cblPagos.Items.Count; item++)
                    {
                        cblPagos.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < cblPagos.Items.Count; item++)
                    {
                        cblPagos.SetItemChecked(item, true);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();

                lblEstatus.Text = string.Empty;

                /************************/
                dgvMXP.DataSource = this.Filtar(_tableMXP);
                dgvUSD.DataSource = this.Filtar(_tableUSD);
                dgvImp.DataSource = this.Filtar(_tableIMP);
                dgvBanco.DataSource = this.Filtar(_tableBanco);

                ////Formato Detalle
                if (dgvMXP.Columns.Count > 0)
                    this.Formato(dgvMXP, false, true, false, false);
                if (dgvUSD.Columns.Count > 0)
                    this.Formato(dgvUSD, true, false, false, true);
                if (dgvImp.Columns.Count > 0)
                    this.Formato(dgvImp, true, false, false, true);
                if (dgvBanco.Columns.Count > 0)
                    this.Formato(dgvBanco, true, true, true, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void txtDisponible_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Disponible = Convert.ToDecimal(kryptonTextBox4.Text);
                Disponible = Convert.ToDecimal(txtV2.Text) + Convert.ToDecimal(txtV6.Text) + Convert.ToDecimal(txtV10.Text);
                tt1.Text = Disponible.ToString("C2");
                txt53.BackColor = Color.FromName("Control");
                txt53.ForeColor = Color.Black;
            }
            catch (Exception)
            {
                Disponible = 0;
                (sender as TextBox).Text = "0";
                txt53.BackColor = Color.Red;
                txt53.ForeColor = Color.White;
            }
            finally
            {
                this.Totales();
            }
        }

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Pendiente MXP
                //Pendiente = Convert.ToDecimal(kryptonTextBox3.Text);
                Pendiente = Convert.ToDecimal(txtV4.Text) + Convert.ToDecimal(txtV8.Text) + Convert.ToDecimal(txtV12.Text);
                tt2.Text = Pendiente.ToString("C2");
            }
            catch (Exception)
            {
                Pendiente = 0;
                (sender as TextBox).Text = "0";
            }
            finally
            {
                this.Totales();
            }
        }

        private void PagosProveedores_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void PagosProveedores_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerentePagos || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                {
                    DialogResult cerrar = MessageBox.Show("¿Desea guardar los cambios efectuados?", "HalcoNET", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

                    if (cerrar == System.Windows.Forms.DialogResult.Cancel)//Cancelar
                    {
                        e.Cancel = true;
                    }
                    else if (cerrar == System.Windows.Forms.DialogResult.No)//No guargar cambios
                    {
                        log.Fin();
                    }
                    else if (cerrar == System.Windows.Forms.DialogResult.Yes)//Guardar cambios
                    {
                        kryptonCommand1_Execute(sender, e);
                        log.Fin();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            TabPage selected = tabControl1.SelectedTab;

            if (selected.Text.Equals("Resumen"))
            {
                txtPropMXP.Visible = false;
                txtAproMXP.Visible = false;
                txtPropUSD.Visible = false;
                txtAproUSD.Visible = false;

                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
                label28.Visible = true;

                label44.Visible = false;
                label42.Visible = false;
                label43.Visible = false;
                label36.Visible = false;

                kryptonTextBox1.Visible = true;
                kryptonTextBox2.Visible = true;
                kryptonTextBox3.Visible = true;
                kryptonTextBox4.Visible = true;
            }
            else
            {
                txtPropMXP.Visible = true;
                txtAproMXP.Visible = true;
                txtPropUSD.Visible = true;
                txtAproUSD.Visible = true;

                label25.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                label28.Visible = false;

                label44.Visible = true;
                label42.Visible = true;
                label43.Visible = true;
                label36.Visible = true;

                kryptonTextBox1.Visible = false;
                kryptonTextBox2.Visible = false;
                kryptonTextBox3.Visible = false;
                kryptonTextBox4.Visible = false;
            }

            if (selected.Text.Equals("MXP"))
            {
                txtPropMXP.Text = txt03.Text;
                txtAproMXP.Text = txt04.Text;
                txtPropUSD.Text = txt05.Text;
                txtAproUSD.Text = txt06.Text;
            }
            if (selected.Text.Equals("USD"))
            {
                txtPropMXP.Text = txt13.Text;
                txtAproMXP.Text = txt14.Text;
                txtPropUSD.Text = txt15.Text;
                txtAproUSD.Text = txt16.Text;
            }
            if (selected.Text.Equals("Importación"))
            {
                txtPropMXP.Text = txt23.Text;
                txtAproMXP.Text = txt24.Text;
                txtPropUSD.Text = txt25.Text;
                txtAproUSD.Text = txt26.Text;
            }
            if (selected.Text.Equals("Banco"))
            {
                txtPropMXP.Text = txt33.Text;
                txtAproMXP.Text = txt34.Text;
                txtPropUSD.Text = txt35.Text;
                txtAproUSD.Text = txt36.Text;
            }
            if (selected.Text.Equals("Otros Proveedores"))
            {
                txtPropMXP.Text = txtl4.Text;
                txtAproMXP.Text = txtl5.Text;
                txtPropUSD.Text = txtl6.Text;
                txtAproUSD.Text = txtl7.Text;
            }
            if (selected.Text.Equals("Gastos") || selected.Text.Equals("Servicios Corporativos"))
            {
                txtPropMXP.Visible = false;
                txtAproMXP.Visible = false;
                txtPropUSD.Visible = false;
                txtAproUSD.Visible = false;

                label25.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                label28.Visible = false;

                label44.Visible = false;
                label42.Visible = false;
                label43.Visible = false;
                label36.Visible = false;

                kryptonTextBox1.Visible = false;
                kryptonTextBox2.Visible = false;
                kryptonTextBox3.Visible = false;
                kryptonTextBox4.Visible = false;
            }
        }

        private void txt55_TextChanged(object sender, EventArgs e)
        {//disponible USD
            try
            {
                //n55 = Convert.ToDecimal(kryptonTextBox2.Text);
                n55 = Convert.ToDecimal(txtV1.Text) + Convert.ToDecimal(txtV5.Text) + Convert.ToDecimal(txtV9.Text);
                tt3.Text = n55.ToString("C2");
                txt55.BackColor = Color.FromName("Control");
                txt55.ForeColor = Color.Black;
            }
            catch (Exception)
            {
                n55 = 0;
                (sender as TextBox).Text = "0";
                txt55.BackColor = Color.Red;
                txt55.ForeColor = Color.White;
            }
            finally
            {
                this.Totales();
            }
        }

        private void txt75_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Pendiente USD
                //n75 = Convert.ToDecimal(kryptonTextBox1.Text);
                n75 = Convert.ToDecimal(txtV3.Text) + Convert.ToDecimal(txtV7.Text) + Convert.ToDecimal(txtV11.Text);
                tt4.Text = n75.ToString("N2");
            }
            catch (Exception)
            {
                n75 = 0;
                (sender as TextBox).Text = "0";
            }
            finally
            {
                this.Totales();
            }
        }

        private void kryptonDataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgvLibre_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Totales();
                txtPropMXP.Text = txtl4.Text;
                txtAproMXP.Text = txtl5.Text;
                txtPropUSD.Text = txtl6.Text;
                txtAproUSD.Text = txtl7.Text;

                /*T O T A L E S      D E     E N C A B E Z A D O*/

                DataTable _otrosProv = new DataTable();
                _otrosProv = (DataTable)((BindingSource)dgvLibre.DataSource).DataSource;
                
                decimal _mxp = decimal.Zero;
                decimal _usd = decimal.Zero;
                decimal _atznor = decimal.Zero;
                decimal _suspenciones = decimal.Zero;
                decimal _meza = decimal.Zero;


                _mxp = Convert.ToDecimal(_otrosProv.Compute("SUM(MXP)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(MXP)", string.Empty));
                _usd = Convert.ToDecimal(_otrosProv.Compute("SUM(USD)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(USD)", string.Empty));
                _atznor = Convert.ToDecimal(_otrosProv.Compute("SUM(ATZNOR)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(ATZNOR)", string.Empty));
                _suspenciones = Convert.ToDecimal(_otrosProv.Compute("SUM(SUSPENCIONES)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(SUSPENCIONES)", string.Empty));
                _meza = Convert.ToDecimal(_otrosProv.Compute("SUM(MEZA)", string.Empty) == DBNull.Value ? decimal.Zero : _otrosProv.Compute("SUM(MEZA)", string.Empty));

                DataTable _totales = new DataTable();
                _totales = _otrosProv.Copy();
                _totales.Rows.Clear();

                DataRow _row = _totales.NewRow();
                _row["MXP"] = _mxp;
                _row["USD"] = _usd;
                _row["ATZNOR"] = _atznor;
                _row["SUSPENCIONES"] = _suspenciones;
                _row["MEZA"] = _meza;

                _totales.Rows.Add(_row);

                dgvTotalesOtros.DataSource = _totales;
                this.Formato(dgvTotalesOtros);
            }
            catch (Exception )
            {
            
            }

            try
            {
                //subtotales RACSA
                decimal _atznor = decimal.Zero;
                decimal _suspenciones = decimal.Zero;
                decimal _meza = decimal.Zero;

                DataGridViewRow item = (sender as DataGridView).Rows[e.RowIndex];
                _atznor = Convert.ToDecimal(item.Cells["ATZNOR"].Value == DBNull.Value ? decimal.Zero : item.Cells["ATZNOR"].Value);
                _suspenciones = Convert.ToDecimal(item.Cells["SUSPENCIONES"].Value == DBNull.Value ? decimal.Zero : item.Cells["SUSPENCIONES"].Value);
                _meza = Convert.ToDecimal(item.Cells["MEZA"].Value == DBNull.Value ? decimal.Zero : item.Cells["MEZA"].Value);

                item.Cells["TOTAL"].Value = _atznor + _suspenciones + _meza;

            }
            catch (Exception)
            {
            }
        }

        private void dgvLibre_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDateTime(item.Cells[(int)ColumnasLibre.Fecha].Value) < DateTime.Now.Date)
                    {
                        item.Cells[(int)ColumnasLibre.Fecha].Style.BackColor = Color.Red;
                        item.Cells[(int)ColumnasLibre.Fecha].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog1 = new PrintDialog();
            printDialog1.Document = printDocument1;
            DialogResult result = printDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                printDocument1.DefaultPageSettings.Landscape = true;
                // printDocument1.DefaultPageSettings.PrinterSettings.
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.pResumen.Width, this.pResumen.Height);
            pResumen.DrawToBitmap(bm, new Rectangle(0, 0, this.pResumen.Width, this.pResumen.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void dgvMXP_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            decimal sumita = decimal.Zero;
            try
            {
                var seleccionadas = (sender as DataGridView).SelectedCells;
                foreach (DataGridViewCell item in seleccionadas)
                {
                    sumita += Convert.ToDecimal(item.Value);
                }

                toolTotalSuma.Text = "Suma: " + sumita.ToString("C2");
            }
            catch (Exception)
            {
                toolTotalSuma.Text = "Suma: " + sumita.ToString("C2");
            }
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((sender as DataGridView).Rows.Count > 0 && e.RowIndex != -1)
            {
                List<string> query = (from item in ((sender as DataGridView).DataSource as DataTable).AsEnumerable()
                                      where !item.Field<string>("Nombre").ToLower().Contains("total".ToLower())
                                      select item.Field<string>("Nombre")).Distinct().ToList();

                DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];

                decimal sumatoriaPropuestaMXP = decimal.Zero;
                decimal sumatoriaPropuestaUSD = decimal.Zero;

                decimal sumatoriaAprobadoMXP = decimal.Zero;
                decimal sumatoriaAprobadoUSD = decimal.Zero;

                string prov = row.Cells[(int)Columnas.Nombre].Value.ToString();
                
                foreach (DataGridViewRow fila in (sender as DataGridView).Rows)
                {
                    if (fila.Cells[(int)Columnas.Nombre].Value.ToString().Trim().Contains(prov))
                    {
                        if (fila.Cells[(int)Columnas.Nombre].Value.ToString() == prov && !fila.Cells[(int)Columnas.Nombre].Value.ToString().Contains("Total"))
                        {
                            sumatoriaPropuestaMXP += Convert.ToDecimal(fila.Cells[(int)Columnas.TotalMXP].Value);
                            sumatoriaPropuestaUSD += Convert.ToDecimal(fila.Cells[(int)Columnas.Propuesta].Value);
                            sumatoriaAprobadoMXP += Convert.ToDecimal(fila.Cells[(int)Columnas.AprobadoMXP].Value);
                            sumatoriaAprobadoUSD += Convert.ToDecimal(fila.Cells[(int)Columnas.Aprobado].Value);
                        }
                        if (fila.Cells[(int)Columnas.Nombre].Value.ToString() == prov + " Total")
                        {
                            fila.Cells[(int)Columnas.TotalMXP].Value = sumatoriaPropuestaMXP;
                            fila.Cells[(int)Columnas.Propuesta].Value = sumatoriaPropuestaUSD;
                            fila.Cells[(int)Columnas.AprobadoMXP].Value = sumatoriaAprobadoMXP;
                            fila.Cells[(int)Columnas.Aprobado].Value = sumatoriaAprobadoUSD;
                        }
                    }
                }
            }
        }

        private void btnAddPago_Click(object sender, EventArgs e)
        {
            try
            {
                //insert in database
                string _proveedor = txtProveedor.Text;
                string _concepto = txtConcepto.Text;
                string _factura = txtFactura.Text;
                string _solicita = txtSolicita.Text;
                DateTime _fechaLimite = dtLimitePago.Value;
                string _cuenta = txtCuenta.Text;
                string _banco = txtBanco.Text;
                string _tipoPago = cbTipoPago.Text;
                string status = string.Empty;

                if (!string.IsNullOrEmpty(_proveedor))
                {
                    if (!string.IsNullOrEmpty(_concepto))
                    {
                        if (!string.IsNullOrEmpty(_factura))
                        {
                            if (!string.IsNullOrEmpty(txtCantidad.Text))
                            {
                                decimal _cantidad = Convert.ToDecimal(txtCantidad.Text);
                                if (!string.IsNullOrEmpty(_solicita))
                                {
                                    if (!string.IsNullOrEmpty(_tipoPago))
                                    {
                                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                        {
                                            using (SqlCommand command = new SqlCommand("PJ_GastosPagos", connection))
                                            {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.CommandTimeout = 0;

                                                command.Parameters.AddWithValue("@TipoConsulta", 1);
                                                command.Parameters.AddWithValue("@Code", 0);
                                                if (string.IsNullOrEmpty(ClasesSGUV.Login.NombreUsuario))
                                                    ClasesSGUV.Login.NombreUsuario = string.Empty;
                                                command.Parameters.AddWithValue("@User", ClasesSGUV.Login.NombreUsuario);
                                                command.Parameters.AddWithValue("@Proveedor", _proveedor);
                                                command.Parameters.AddWithValue("@Concepto", _concepto);
                                                command.Parameters.AddWithValue("@Factura", _factura);
                                                command.Parameters.AddWithValue("@Propuesta", _cantidad);
                                                command.Parameters.AddWithValue("@Aprobado", decimal.Zero);
                                                command.Parameters.AddWithValue("@Solicita", _solicita);
                                                command.Parameters.AddWithValue("@FechaPago", _fechaLimite);
                                                command.Parameters.AddWithValue("@Cuenta", _cuenta);
                                                command.Parameters.AddWithValue("@Banco", _banco);
                                                command.Parameters.AddWithValue("@TipoPago", _tipoPago);
                                                command.Parameters.AddWithValue("@Prioridad", 0);
                                                command.Parameters.AddWithValue("@Estatus", string.Empty);
                                                connection.Open();

                                                command.ExecuteNonQuery();

                                                lblEstatus.Text = "Registro exitoso.";
                                                lblEstatus.BackColor = Color.Green;
                                                lblEstatus.ForeColor = Color.Black;

                                                txtProveedor.Text = string.Empty;
                                                txtConcepto.Clear();
                                                txtFactura.Clear();
                                                txtCantidad.Clear();
                                                txtSolicita.Clear();
                                                dtLimitePago.Value = DateTime.Now;
                                                txtCuenta.Clear();
                                                txtBanco.Clear();
                                                cbTipoPago.Text = string.Empty;

                                                this.FormatoGastos(dgvGastos);

                                            }
                                        }

                                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                        {
                                            using (SqlCommand command = new SqlCommand("PJ_GastosPagos", connection))
                                            {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.CommandTimeout = 0;

                                                command.Parameters.AddWithValue("@TipoConsulta", 2);
                                                command.Parameters.AddWithValue("@Code", 0);
                                                command.Parameters.AddWithValue("@User", string.Empty);
                                                command.Parameters.AddWithValue("@Proveedor", string.Empty);
                                                command.Parameters.AddWithValue("@Concepto", string.Empty);
                                                command.Parameters.AddWithValue("@Factura", string.Empty);
                                                command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                                                command.Parameters.AddWithValue("@Aprobado", decimal.Zero);
                                                command.Parameters.AddWithValue("@Solicita", string.Empty);
                                                command.Parameters.AddWithValue("@FechaPago", DateTime.Now);
                                                command.Parameters.AddWithValue("@Cuenta", string.Empty);
                                                command.Parameters.AddWithValue("@Banco", string.Empty);
                                                command.Parameters.AddWithValue("@TipoPago", string.Empty);
                                                command.Parameters.AddWithValue("@Prioridad", 0);
                                                command.Parameters.AddWithValue("@Estatus", string.Empty);

                                                DataTable table = new DataTable();
                                                SqlDataAdapter da = new SqlDataAdapter();
                                                da.SelectCommand = command;
                                                da.Fill(table);
                                                if (table.Rows.Count > 0)
                                                {
                                                    var query = (from item in table.AsEnumerable()
                                                                 select item.Field<string>("U_TipoPago")).Distinct();


                                                    foreach (var item in query.ToList())
                                                    {
                                                        DataRow r = table.NewRow();
                                                        r["Proveedor"] = string.Empty;
                                                        r["Concepto"] = string.Empty;
                                                        r["Factura"] = item + " Total";

                                                        r["Moneda"] = string.Empty;
                                                        r["Aprob"] = false;
                                                        r["Propuesta"] = (from acum in table.AsEnumerable()
                                                                          where acum.Field<string>("U_TipoPago") == item
                                                                          select acum.Field<decimal>("Propuesta")).Sum();
                                                        r["Aprobado"] = (from acum in table.AsEnumerable()
                                                                         where acum.Field<string>("U_TipoPago") == item
                                                                         select acum.Field<decimal>("Aprobado")).Sum();
                                                        r["Solicita"] = string.Empty;
                                                        r["Cuenta"] = string.Empty;
                                                        r["Banco"] = string.Empty;
                                                        r["U_TipoPago"] = item + " Total";

                                                        table.Rows.Add(r);
                                                    }

                                                    table = (from tv in table.AsEnumerable()
                                                             orderby tv.Field<string>("U_TipoPago")
                                                             select tv).CopyToDataTable();
                                                }
                                                dgvGastos.DataSource = table;
                                            }
                                        }

                                    }
                                    else
                                        MessageBox.Show("El Campo [Tipo de pago] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    MessageBox.Show("El Campo [Solicita] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("El Campo [Cantidad] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("El Campo [Factura] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("El Campo [Concepto] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("El Campo [Proveedor] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvGastos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                    if (e.ColumnIndex == (int)ColumnasGastos.Aprob)
                    {
                        if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                        {
                            if (Convert.ToBoolean(row.Cells[(int)ColumnasGastos.Aprob].Value) == false)
                            {
                                if (Convert.ToDecimal(row.Cells[(int)ColumnasGastos.Aprobado].Value) == decimal.Zero)
                                    row.Cells[(int)ColumnasGastos.Aprobado].Value = row.Cells[(int)ColumnasGastos.Propuesta].Value;

                                row.Cells[(int)ColumnasGastos.Aprob].Value = true;
                            }
                            else
                            {
                                row.Cells[(int)ColumnasGastos.Aprobado].Value = decimal.Zero;
                                row.Cells[(int)ColumnasGastos.Aprob].Value = false;
                            }
                        }
                    }


                    if ((sender as DataGridView).Rows.Count > 0)
                    {
                        List<string> query = (from item in ((sender as DataGridView).DataSource as DataTable).AsEnumerable()
                                              where !item.Field<string>("U_TipoPago").ToLower().Contains("total".ToLower())
                                              select item.Field<string>("U_TipoPago")).Distinct().ToList();

                        decimal sumatoriaPropuesta = decimal.Zero;
                        decimal sumatoriaAprobado = decimal.Zero;

                        string prov = row.Cells[(int)ColumnasGastos.TipoPago].Value.ToString();

                        foreach (DataGridViewRow fila in (sender as DataGridView).Rows)
                        {
                            if (fila.Cells[(int)ColumnasGastos.TipoPago].Value.ToString().Trim().Contains(prov))
                            {
                                if (fila.Cells[(int)ColumnasGastos.TipoPago].Value.ToString() == prov && !fila.Cells[(int)ColumnasGastos.TipoPago].Value.ToString().Contains("Total"))
                                {
                                    sumatoriaPropuesta += Convert.ToDecimal(fila.Cells[(int)ColumnasGastos.Propuesta].Value);
                                    sumatoriaAprobado += Convert.ToDecimal(fila.Cells[(int)ColumnasGastos.Aprobado].Value);
                                }
                                if (fila.Cells[(int)ColumnasGastos.Factura].Value.ToString() == prov + " Total")
                                {
                                    fila.Cells[(int)ColumnasGastos.Propuesta].Value = sumatoriaPropuesta;
                                    fila.Cells[(int)ColumnasGastos.Aprobado].Value = sumatoriaAprobado;
                                }
                            }
                        }
                    }
                    //////////////////SUMAS
                    string name = (sender as DataGridView).Name;
                    if (name.Equals("dgvGastos"))
                    {
                        DataTable _tblGastos = dgvGastos.DataSource as DataTable;
                        g00 = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP' AND Propuesta >= 0") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP' AND Propuesta >= 0"));
                        g10 = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD' AND Propuesta >= 0") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD' AND Propuesta >= 0"));

                        gtMXP = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP' AND Propuesta >= 0") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP' AND Propuesta >= 0"));
                        gtUSD = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD' AND Propuesta >= 0") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD' AND Propuesta >= 0"));

                        g20 = g00 + (g10 * TC);

                        g01 = Convert.ToDecimal(_tblGastos.Compute("SUM(Aprobado)", "Moneda = 'MXP' AND Aprobado >= 0") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Aprobado)", "Moneda = 'MXP' AND Aprobado >= 0"));
                        g11 = Convert.ToDecimal(_tblGastos.Compute("SUM(Aprobado)", "Moneda = 'USD' AND Aprobado >= 0") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Aprobado)", "Moneda = 'USD' AND Aprobado >= 0"));
                        g21 = g01 + (g11 * TC);

                        txtg00.Text = g00.ToString("C2");
                        txtg10.Text = g10.ToString("C2");
                        txtg01.Text = g01.ToString("C2");
                        txtg11.Text = g11.ToString("C2");

                        txtg21.Text = g21.ToString("C2");
                        txtg20.Text = g20.ToString("C2");

                        txtg1.Text = gtUSD.ToString("C2");
                        txtg2.Text = gtMXP.ToString("C2");
                        txtg3.Text = (gtMXP + (gtUSD * TC)).ToString("C2");
                        txtg4.Text = g00.ToString("C2");
                        txtg5.Text = g01.ToString("C2");
                        txtg6.Text = g10.ToString("C2");
                        txtg7.Text = g11.ToString("C2");
                    }
                    else
                    {
                        DataTable _tblGastos1 = (from item in (dgvGastosSC.DataSource as DataTable).AsEnumerable()
                                                 where !item.Field<string>("U_TipoPago").Contains("Total")
                                                 select item).CopyToDataTable();


                        DataTable _tblGastos = (from item in _tblGastos1.AsEnumerable()
                                                where item.Field<DateTime>("Fecha limite") >= dtFecha1.Value &&
                                                      item.Field<DateTime>("Fecha limite") <= dtFecha2.Value
                                                select item).CopyToDataTable();



                        decimal g00SC = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'"));
                        decimal g10SC = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'"));

                        decimal gtMXPSC = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'MXP'"));
                        decimal gtUSDSC = Convert.ToDecimal(_tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Propuesta)", "Moneda = 'USD'"));

                        decimal g20SC = g00SC + (g10SC * TC);

                        decimal g01SC = Convert.ToDecimal(_tblGastos.Compute("SUM(Aprobado)", "Moneda = 'MXP'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Aprobado)", "Moneda = 'MXP'"));
                        decimal g11SC = Convert.ToDecimal(_tblGastos.Compute("SUM(Aprobado)", "Moneda = 'USD'") == DBNull.Value ? decimal.Zero : _tblGastos.Compute("SUM(Aprobado)", "Moneda = 'USD'"));
                        decimal g21SC = g01SC + (g11SC * TC);

                        txtg00SC.Text = g00SC.ToString("C2");
                        txtg10SC.Text = g10SC.ToString("C2");
                        txtg01SC.Text = g01SC.ToString("C2");
                        txtg11SC.Text = g11SC.ToString("C2");

                        txtg21SC.Text = g21SC.ToString("C2");
                        txtg20SC.Text = g20SC.ToString("C2");
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataTable Reporte = new DataTable();
                Reporte.Columns.Add("Factura", typeof(string));
                Reporte.Columns.Add("Proveedor", typeof(string));
                Reporte.Columns.Add("Autorizado", typeof(decimal));
                Reporte.Columns.Add("Cuenta", typeof(string));
                Reporte.Columns.Add("Moneda", typeof(string));

                foreach (DataGridViewRow item in dgvMXP.Rows)
                {
                    if (!Convert.ToString(item.Cells[(int)Columnas.Nombre].Value).ToLower().Contains("total"))
                    {
                        if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value))
                        {
                            DataRow row = Reporte.NewRow();

                            row["Factura"] = Convert.ToString(item.Cells[(int)Columnas.FolioSAP].Value);
                            row["Proveedor"] = Convert.ToString(item.Cells[(int)Columnas.Nombre].Value);
                            row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)Columnas.AprobadoMXP].Value);
                            row["Cuenta"] = Convert.ToString(item.Cells[(int)Columnas.Cuenta].Value);
                            row["Moneda"] = "MXP";

                            Reporte.Rows.Add(row);
                        }
                    }
                }

                foreach (DataGridViewRow item in dgvUSD.Rows)
                {
                    if (!Convert.ToString(item.Cells[(int)Columnas.Nombre].Value).ToLower().Contains("total"))
                    {
                        if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) && !Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value))
                        {
                            DataRow row = Reporte.NewRow();

                            row["Factura"] = Convert.ToString(item.Cells[(int)Columnas.FolioSAP].Value);
                            row["Proveedor"] = Convert.ToString(item.Cells[(int)Columnas.Nombre].Value);
                            row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)Columnas.Aprobado].Value) * TC;
                            row["Cuenta"] = Convert.ToString(item.Cells[(int)Columnas.Cuenta].Value);
                            row["Moneda"] = "MXP";

                            Reporte.Rows.Add(row);
                        }
                        if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) && Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value))
                        {
                            DataRow row = Reporte.NewRow();

                            row["Factura"] = Convert.ToString(item.Cells[(int)Columnas.FolioSAP].Value);
                            row["Proveedor"] = Convert.ToString(item.Cells[(int)Columnas.Nombre].Value);
                            row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)Columnas.Aprobado].Value);
                            row["Cuenta"] = Convert.ToString(item.Cells[(int)Columnas.Cuenta].Value);
                            row["Moneda"] = "USD";

                            Reporte.Rows.Add(row);
                        }
                    }
                }

                foreach (DataGridViewRow item in dgvImp.Rows)
                {
                    if (!Convert.ToString(item.Cells[(int)Columnas.Nombre].Value).ToLower().Contains("total"))
                    {
                        if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) && !Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value))
                        {
                            DataRow row = Reporte.NewRow();

                            row["Factura"] = Convert.ToString(item.Cells[(int)Columnas.FolioSAP].Value);
                            row["Proveedor"] = Convert.ToString(item.Cells[(int)Columnas.Nombre].Value);
                            row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)Columnas.Aprobado].Value) * TC;
                            row["Cuenta"] = Convert.ToString(item.Cells[(int)Columnas.Cuenta].Value);
                            row["Moneda"] = "MXP";

                            Reporte.Rows.Add(row);
                        }
                        if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) && Convert.ToBoolean(item.Cells[(int)Columnas.PagarUSD].Value))
                        {
                            DataRow row = Reporte.NewRow();

                            row["Factura"] = Convert.ToString(item.Cells[(int)Columnas.FolioSAP].Value);
                            row["Proveedor"] = Convert.ToString(item.Cells[(int)Columnas.Nombre].Value);
                            row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)Columnas.Aprobado].Value);
                            row["Cuenta"] = Convert.ToString(item.Cells[(int)Columnas.Cuenta].Value);
                            row["Moneda"] = "USD";

                            Reporte.Rows.Add(row);
                        }
                    }
                }

                foreach (DataGridViewRow item in dgvBanco.Rows)
                {
                    if (!Convert.ToString(item.Cells[(int)Columnas.Nombre].Value).ToLower().Contains("total"))
                    {
                        if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) && Convert.ToDecimal(item.Cells[(int)Columnas.TC].Value) != decimal.Zero)
                        {
                            DataRow row = Reporte.NewRow();

                            row["Factura"] = Convert.ToString(item.Cells[(int)Columnas.FolioSAP].Value);
                            row["Proveedor"] = Convert.ToString(item.Cells[(int)Columnas.Nombre].Value);
                            row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)Columnas.TotalMXP].Value);
                            row["Cuenta"] = Convert.ToString(item.Cells[(int)Columnas.Cuenta].Value);
                            row["Moneda"] = "MXP";

                            Reporte.Rows.Add(row);
                        }
                        if (Convert.ToBoolean(item.Cells[(int)Columnas.Bit2].Value) && Convert.ToDecimal(item.Cells[(int)Columnas.TC].Value) == decimal.Zero)
                        {
                            DataRow row = Reporte.NewRow();

                            row["Factura"] = Convert.ToString(item.Cells[(int)Columnas.FolioSAP].Value);
                            row["Proveedor"] = Convert.ToString(item.Cells[(int)Columnas.Nombre].Value);
                            row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)Columnas.Aprobado].Value);
                            row["Cuenta"] = Convert.ToString(item.Cells[(int)Columnas.Cuenta].Value);
                            row["Moneda"] = "USD";

                            Reporte.Rows.Add(row);
                        }
                    }
                }

                foreach (DataGridViewRow item in dgvLibre.Rows)
                {
                    if (item.Cells[(int)ColumnasLibre.Aprobado].Value == DBNull.Value ? false : Convert.ToBoolean(item.Cells[(int)ColumnasLibre.Aprobado].Value) && (item.Cells[(int)ColumnasLibre.MXP].Value == DBNull.Value ? decimal.Zero : Convert.ToDecimal(item.Cells[(int)ColumnasLibre.MXP].Value)) != decimal.Zero)
                    {
                        DataRow row = Reporte.NewRow();

                        row["Factura"] = string.Empty;
                        row["Proveedor"] = Convert.ToString(item.Cells[(int)ColumnasLibre.Descripcion].Value);
                        row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)ColumnasLibre.MXP].Value);
                        row["Cuenta"] = string.Empty;
                        row["Moneda"] = "MXP";

                        Reporte.Rows.Add(row);
                    }
                    if (item.Cells[(int)ColumnasLibre.Aprobado].Value == DBNull.Value ? false : Convert.ToBoolean(item.Cells[(int)ColumnasLibre.Aprobado].Value) && (item.Cells[(int)ColumnasLibre.USD].Value == DBNull.Value ? decimal.Zero : Convert.ToDecimal(item.Cells[(int)ColumnasLibre.USD].Value)) != decimal.Zero)
                    {
                        DataRow row = Reporte.NewRow();

                        row["Factura"] = string.Empty;
                        row["Proveedor"] = Convert.ToString(item.Cells[(int)ColumnasLibre.Descripcion].Value);
                        row["Autorizado"] = Convert.ToDecimal(item.Cells[(int)ColumnasLibre.USD].Value);
                        row["Cuenta"] = string.Empty;
                        row["Moneda"] = "USD";

                        Reporte.Rows.Add(row);
                    }
                }


                ReporteAutorizado re = new ReporteAutorizado(Reporte);
                re.ShowInTaskbar = false;
                re.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvGastos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[(int)ColumnasGastos.Moneda].Value.ToString().Trim().Equals(string.Empty))
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10f, FontStyle.Bold);
                    }
                    else
                    {
                        if (Convert.ToDateTime(Convert.ToDateTime(item.Cells[(int)ColumnasGastos.FechaLimite].Value).ToShortDateString()) <= Convert.ToDateTime(dtFecha2.Value.ToShortDateString()))
                        {
                            if (Convert.ToDateTime(Convert.ToDateTime(item.Cells[(int)ColumnasGastos.FechaLimite].Value).ToShortDateString()) <= Convert.ToDateTime(dtFecha2.Value.ToShortDateString()))
                            {
                                item.DefaultCellStyle.BackColor = Color.FromArgb(0, 176, 80);
                                item.DefaultCellStyle.ForeColor = Color.Black;
                            }

                            if (Convert.ToInt32(item.Cells[(int)ColumnasGastos.Prioridad].Value) == 1)
                            {
                                item.DefaultCellStyle.BackColor = Color.FromArgb(192, 0, 0);
                                item.DefaultCellStyle.ForeColor = Color.White;
                            }
                            else if (Convert.ToInt32(item.Cells[(int)ColumnasGastos.Prioridad].Value) == 2)
                            {
                                item.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 0);
                                item.DefaultCellStyle.ForeColor = Color.Black;
                            }
                            //else
                            //{
                            //    item.DefaultCellStyle.BackColor = Color.White;
                            //    item.DefaultCellStyle.ForeColor = Color.Black;
                            //}
                        }

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgvGastos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string name = (sender as DataGridView).Name;
                DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];

                if (row.Cells[(int)ColumnasGastos.Moneda].Value.ToString() != string.Empty)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_GastosPagos", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            if (name.Equals("dgvGastos"))
                            command.Parameters.AddWithValue("@TipoConsulta", 1);
                            else
                                command.Parameters.AddWithValue("@TipoConsulta", 3);
                            command.Parameters.AddWithValue("@Code", Convert.ToInt32(row.Cells[(int)ColumnasGastos.Code].Value));
                            command.Parameters.AddWithValue("@User", ClasesSGUV.Login.NombreUsuario);
                            command.Parameters.AddWithValue("@Proveedor", Convert.ToString(row.Cells[(int)ColumnasGastos.Proveedor].Value));
                            command.Parameters.AddWithValue("@Concepto", Convert.ToString(row.Cells[(int)ColumnasGastos.Concepto].Value));
                            command.Parameters.AddWithValue("@Factura", Convert.ToString(row.Cells[(int)ColumnasGastos.Factura].Value));
                            command.Parameters.AddWithValue("@Propuesta", Convert.ToDecimal(row.Cells[(int)ColumnasGastos.Propuesta].Value));
                            command.Parameters.AddWithValue("@Aprobado", Convert.ToBoolean(row.Cells[(int)ColumnasGastos.Aprob].Value) == true ? Convert.ToDecimal(row.Cells[(int)ColumnasGastos.Aprobado].Value) : decimal.Zero);
                            command.Parameters.AddWithValue("@Solicita", Convert.ToString(row.Cells[(int)ColumnasGastos.Solicita].Value));
                            command.Parameters.AddWithValue("@FechaPago", Convert.ToDateTime(row.Cells[(int)ColumnasGastos.FechaLimite].Value));
                            command.Parameters.AddWithValue("@Cuenta", Convert.ToString(row.Cells[(int)ColumnasGastos.Cuenta].Value));
                            command.Parameters.AddWithValue("@Banco", Convert.ToString(row.Cells[(int)ColumnasGastos.Banco].Value));
                            command.Parameters.AddWithValue("@TipoPago", string.Empty);
                            command.Parameters.AddWithValue("@Prioridad", Convert.ToInt32(row.Cells[(int)ColumnasGastos.Prioridad].Value));
                            command.Parameters.AddWithValue("@Estatus", Convert.ToDecimal(Convert.ToBoolean(row.Cells[(int)ColumnasGastos.Aprob].Value) == true ? Convert.ToDecimal(row.Cells[(int)ColumnasGastos.Aprobado].Value) : decimal.Zero) != decimal.Zero ? "A" : "P");
                            connection.Open();

                            command.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvGastos_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                if (e.Row != null)
                {
                    string name = (sender as DataGridView).Name;
                    //delete from database <<Update con Estatus = null>>
                    DialogResult result = MessageBox.Show("¿Desea dejar de visualizar esta linea?\r\nProveedor: " + Convert.ToString(e.Row.Cells[(int)ColumnasGastos.Proveedor].Value)
                            + "\r\nFactura: " + Convert.ToString(e.Row.Cells[(int)ColumnasGastos.Factura].Value), "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (e.Row.Cells[(int)ColumnasGastos.Moneda].Value.ToString() != string.Empty)
                        {
                            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                            {
                                using (SqlCommand command = new SqlCommand("PJ_GastosPagos", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandTimeout = 0;

                                    if (name.Equals("dgvGastos"))
                                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                                    else
                                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                                    command.Parameters.AddWithValue("@Code", Convert.ToInt32(e.Row.Cells[(int)ColumnasGastos.Code].Value));
                                    command.Parameters.AddWithValue("@User", ClasesSGUV.Login.NombreUsuario);
                                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                                    command.Parameters.AddWithValue("@Concepto", string.Empty);
                                    command.Parameters.AddWithValue("@Factura", string.Empty);
                                    command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                                    command.Parameters.AddWithValue("@Aprobado", decimal.Zero);
                                    command.Parameters.AddWithValue("@Solicita", string.Empty);
                                    command.Parameters.AddWithValue("@FechaPago", DateTime.Now);
                                    command.Parameters.AddWithValue("@Cuenta", string.Empty);
                                    command.Parameters.AddWithValue("@Banco", string.Empty);
                                    command.Parameters.AddWithValue("@TipoPago", string.Empty);
                                    command.Parameters.AddWithValue("@Prioridad", 0);
                                    command.Parameters.AddWithValue("@Estatus", "Del");

                                    connection.Open();

                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtProveedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string cardcode = txtProveedor.SelectedValue.ToString();

                DataRow selected = (from item in _tblProveedores.AsEnumerable()
                                    where item.Field<string>("CardCode").ToLower().Equals(cardcode.ToLower())
                                    select item).FirstOrDefault();

                txtCuenta.Text = selected["DflAccount"].ToString();
                txtBanco.Text = selected["BankName"].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void txtProveedor_Leave(object sender, EventArgs e)
        {
            try
            {
                string cardcode = txtProveedor.SelectedValue.ToString();

                DataRow selected = (from item in _tblProveedores.AsEnumerable()
                                    where item.Field<string>("CardCode").ToLower().Equals(cardcode.ToLower())
                                    select item).FirstOrDefault();

                txtCuenta.Text = selected["DflAccount"].ToString();
                txtBanco.Text = selected["BankName"].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                PagosProveedores_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            Pagos.PagosEfectuados formulario = new PagosEfectuados();
            formulario.MdiParent = this.MdiParent;

            formulario.Show();
        }
        #endregion

        private void kryptonButton2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //insert in database
                string _proveedor = cbProveedorSC.Text;
                string _concepto = txtConceptoSC.Text;
                string _factura = txtFacturaSC.Text;
                string _solicita = txtSolicitaSC.Text;
                DateTime _fechaLimite = dtLimitePagoSC.Value;
                string _cuenta = txtCuentaSC.Text;
                string _banco = txtBancoSC.Text;
                string _tipoPago = cbTipoPagoSC.Text;
                string status = string.Empty;

                if (!string.IsNullOrEmpty(_proveedor))
                {
                    if (!string.IsNullOrEmpty(_concepto))
                    {
                        if (!string.IsNullOrEmpty(_factura))
                        {
                            if (!string.IsNullOrEmpty(txtCantidadSC.Text))
                            {
                                decimal _cantidad = Convert.ToDecimal(txtCantidadSC.Text);
                                if (!string.IsNullOrEmpty(_solicita))
                                {
                                    if (!string.IsNullOrEmpty(_tipoPago))
                                    {
                                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                        {
                                            using (SqlCommand command = new SqlCommand("PJ_GastosPagos", connection))
                                            {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.CommandTimeout = 0;

                                                command.Parameters.AddWithValue("@TipoConsulta", 3);
                                                command.Parameters.AddWithValue("@Code", 0);
                                                if (string.IsNullOrEmpty(ClasesSGUV.Login.NombreUsuario))
                                                    ClasesSGUV.Login.NombreUsuario = string.Empty;
                                                command.Parameters.AddWithValue("@User", ClasesSGUV.Login.NombreUsuario);
                                                command.Parameters.AddWithValue("@Proveedor", _proveedor);
                                                command.Parameters.AddWithValue("@Concepto", _concepto);
                                                command.Parameters.AddWithValue("@Factura", _factura);
                                                command.Parameters.AddWithValue("@Propuesta", _cantidad);
                                                command.Parameters.AddWithValue("@Aprobado", decimal.Zero);
                                                command.Parameters.AddWithValue("@Solicita", _solicita);
                                                command.Parameters.AddWithValue("@FechaPago", _fechaLimite);
                                                command.Parameters.AddWithValue("@Cuenta", _cuenta);
                                                command.Parameters.AddWithValue("@Banco", _banco);
                                                command.Parameters.AddWithValue("@TipoPago", _tipoPago);
                                                command.Parameters.AddWithValue("@Prioridad", 0);
                                                command.Parameters.AddWithValue("@Estatus", string.Empty);
                                                connection.Open();

                                                command.ExecuteNonQuery();

                                                lblEstatus.Text = "Registro exitoso.";
                                                lblEstatus.BackColor = Color.Green;
                                                lblEstatus.ForeColor = Color.Black;

                                                cbProveedorSC.Text = string.Empty;
                                                txtConceptoSC.Clear();
                                                txtFacturaSC.Clear();
                                                txtCantidadSC.Clear();
                                                txtSolicitaSC.Clear();
                                                dtLimitePagoSC.Value = DateTime.Now;
                                                txtCuentaSC.Clear();
                                                txtBancoSC.Clear();
                                                cbTipoPagoSC.Text = string.Empty;

                                                this.FormatoGastos(dgvGastosSC);

                                            }
                                        }

                                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                        {
                                            using (SqlCommand command = new SqlCommand("PJ_GastosPagos", connection))
                                            {
                                                command.CommandType = CommandType.StoredProcedure;
                                                command.CommandTimeout = 0;

                                                command.Parameters.AddWithValue("@TipoConsulta", 4);
                                                command.Parameters.AddWithValue("@Code", 0);
                                                command.Parameters.AddWithValue("@User", string.Empty);
                                                command.Parameters.AddWithValue("@Proveedor", string.Empty);
                                                command.Parameters.AddWithValue("@Concepto", string.Empty);
                                                command.Parameters.AddWithValue("@Factura", string.Empty);
                                                command.Parameters.AddWithValue("@Propuesta", decimal.Zero);
                                                command.Parameters.AddWithValue("@Aprobado", decimal.Zero);
                                                command.Parameters.AddWithValue("@Solicita", string.Empty);
                                                command.Parameters.AddWithValue("@FechaPago", DateTime.Now);
                                                command.Parameters.AddWithValue("@Cuenta", string.Empty);
                                                command.Parameters.AddWithValue("@Banco", string.Empty);
                                                command.Parameters.AddWithValue("@TipoPago", string.Empty);
                                                command.Parameters.AddWithValue("@Prioridad", 0);
                                                command.Parameters.AddWithValue("@Estatus", string.Empty);

                                                DataTable table = new DataTable();
                                                SqlDataAdapter da = new SqlDataAdapter();
                                                da.SelectCommand = command;
                                                da.Fill(table);
                                                if (table.Rows.Count > 0)
                                                {
                                                    var query = (from item in table.AsEnumerable()
                                                                 select item.Field<string>("U_TipoPago")).Distinct();


                                                    foreach (var item in query.ToList())
                                                    {
                                                        DataRow r = table.NewRow();
                                                        r["Proveedor"] = string.Empty;
                                                        r["Concepto"] = string.Empty;
                                                        r["Factura"] = item + " Total";

                                                        r["Moneda"] = string.Empty;
                                                        r["Aprob"] = false;
                                                        r["Propuesta"] = (from acum in table.AsEnumerable()
                                                                          where acum.Field<string>("U_TipoPago") == item
                                                                          select acum.Field<decimal>("Propuesta")).Sum();
                                                        r["Aprobado"] = (from acum in table.AsEnumerable()
                                                                         where acum.Field<string>("U_TipoPago") == item
                                                                         select acum.Field<decimal>("Aprobado")).Sum();
                                                        r["Solicita"] = string.Empty;
                                                        r["Cuenta"] = string.Empty;
                                                        r["Banco"] = string.Empty;
                                                        r["U_TipoPago"] = item + " Total";

                                                        table.Rows.Add(r);
                                                    }

                                                    table = (from tv in table.AsEnumerable()
                                                             orderby tv.Field<string>("U_TipoPago")
                                                             select tv).CopyToDataTable();
                                                }
                                                dgvGastosSC.DataSource = table;
                                                this.FormatoGastos(dgvGastosSC);
                                            }
                                        }

                                    }
                                    else
                                        MessageBox.Show("El Campo [Tipo de pago] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    MessageBox.Show("El Campo [Solicita] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("El Campo [Cantidad] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("El Campo [Factura] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("El Campo [Concepto] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("El Campo [Proveedor] no puede estar vacio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLibre_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)ColumnasLibre.Propuesta)
                {
                    if (!Convert.ToBoolean((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasLibre.Propuesta].Value == DBNull.Value ? false : (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasLibre.Propuesta].Value))
                        (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasLibre.Propuesta].Value = true;
                    else
                        (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasLibre.Propuesta].Value = false;
                }
                if (e.ColumnIndex == (int)ColumnasLibre.Aprobado)
                {
                    if (!Convert.ToBoolean((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasLibre.Aprobado].Value == DBNull.Value ? false : (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasLibre.Aprobado].Value))
                        (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasLibre.Aprobado].Value = true;
                    else
                        (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasLibre.Aprobado].Value = false;
                }
                l1 = l2 = l3 = l4 = l5 = l6 = l7 = decimal.Zero;

                foreach (DataGridViewRow item in dgvLibre.Rows)
                {
                    if (item.Cells["Fecha"].Value != DBNull.Value)
                        if (Convert.ToDateTime(item.Cells["Fecha"].Value) >= Convert.ToDateTime(dtFecha1.Value.ToShortDateString()) && Convert.ToDateTime(item.Cells["Fecha"].Value) <= Convert.ToDateTime(dtFecha2.Value.ToShortDateString()))
                        {
                            if (Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value) >= decimal.Zero)
                                l1 += Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value);

                            if (Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value) >= decimal.Zero)
                                l2 += Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value);

                            if (Convert.ToBoolean(item.Cells["Propuesta"].Value == DBNull.Value ? false : item.Cells["Propuesta"].Value) == true)
                            {
                                if (Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value) >= decimal.Zero)
                                    l4 += Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value);
                                if (Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value) >= decimal.Zero)
                                    l6 += Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value);
                            }
                            if (Convert.ToBoolean(item.Cells["Aprobado"].Value == DBNull.Value ? false : item.Cells["Aprobado"].Value) == true)
                            {
                                if (Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value) >= decimal.Zero)
                                    l5 += Convert.ToDecimal(item.Cells["MXP"].Value == DBNull.Value ? 0 : item.Cells["MXP"].Value);
                                if (Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value) >= decimal.Zero)
                                    l7 += Convert.ToDecimal(item.Cells["USD"].Value == DBNull.Value ? 0 : item.Cells["USD"].Value);
                            }
                        }
                }


                l3 = l2 + (l1 * TC);

                txtl1.Text = l1.ToString("C2");
                txtl2.Text = l2.ToString("C2");
                txtl3.Text = l3.ToString("C2");
                txtl4.Text = l4.ToString("C2");
                txtl5.Text = l5.ToString("C2");

                txtl6.Text = l6.ToString("C2");
                txtl7.Text = l7.ToString("C2");

                txtPropMXP.Text = txtl4.Text;
                txtAproMXP.Text = txtl5.Text;

                txtPropUSD.Text = txtl6.Text;
                txtAproUSD.Text = txtl7.Text;

            }
            catch (Exception)
            {
            }

           
        }

        private void txtPropUSD_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
