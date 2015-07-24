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

namespace Cobranza
{
    public partial class FacturasPendientes : Form
    {
        DataTable Facturas = new DataTable();
        string Imprimir = string.Empty;
        int Rol;
        string Sucursal;
        Clases.Logs log;
        string Usuario;
        DateTime FechaConsulta;

        public FacturasPendientes(int  _Rol, string _Sucursal, string _usuario)
        {
            InitializeComponent();
            Usuario = _usuario;
            Rol = _Rol;
            Sucursal = _Sucursal;
        }

        public enum Columnas
        {
            CardCode, DocEntry, Tipo, Factura, Fecha, Cliente, Asesor, Condicion, Estatus, Total, Recibido, Responsable, Dias, Causas, CausasCierre, Corcho, Escan, Boton
        }

        public enum ColumnasReporte
        {
            Docentry, Tipo, Factura, Fecha, Cliente, Asesor, Condicion, Estatus, Total, Reicibibo, Responsable, Dias, Causas, Cierre
        }

        public enum ColumnasCorcho
        {
            Docentry, Tipo, Factura, Fecha, CardCode, Cliente, Asesor, Condicion, Estatus, Total, Recibido, Responsable, Dias, Causas, Boton
        }

        private void CargarSucursales()
        {
            using (SqlConnection connction = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_Corcho", connction))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@TipoConsulta", 10);
                    command.Parameters.AddWithValue("@Rol", ClasesSGUV.Login.Rol);
                    command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataTable table = new DataTable();
                    da.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = "TODAS";
                    row["Codigo"] = "0";
                    table.Rows.InsertAt(row, 0);

                    clbSucursal.DataSource = table;
                    clbSucursal.DisplayMember = "Nombre";
                    clbSucursal.ValueMember = "Codigo";

                }
            }
            
            //DataTable tblSucursales = new DataTable();

            //SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            //command.CommandType = CommandType.StoredProcedure;
            //command.Parameters.AddWithValue("@TipoConsulta", 8);
            //command.Parameters.AddWithValue("@Sucursales", string.Empty);
            //command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            //command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            //command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            //command.Parameters.AddWithValue("@Sucursal", string.Empty);
            //command.CommandTimeout = 0;

            //DataTable table = new DataTable();
            //SqlDataAdapter adapter = new SqlDataAdapter();
            //adapter.SelectCommand = command;
            //adapter.Fill(table);

            //if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
            //{
            //    DataRow row = table.NewRow();
            //    row["Nombre"] = "TODAS";
            //    row["Codigo"] = "0";
            //    table.Rows.InsertAt(row, 0);
               
            //    tblSucursales = table.Copy();
            //}
            //else
            //{
            //    if (Sucursal.Trim().ToUpper() == "MTY")
            //        Sucursal = "Monterrey";
            //    if (Sucursal.Trim().ToUpper() == "GDL")
            //        Sucursal = "Guadalajara";

            //    if (Sucursal.Trim().ToUpper() == "PUEBLA")
            //        Sucursal = "Puebla";

                

            //    tblSucursales = (from item in table.AsEnumerable()
            //                    where item.Field<string>("Codigo").Trim().ToLower() == Sucursal.Trim().ToLower()
            //                    select item).CopyToDataTable();

            //    if (Sucursal == "Puebla")
            //    {
            //        DataRow row = tblSucursales.NewRow();
            //        row["Nombre"] = "Diamante";
            //        row["Codigo"] = "Diamante";
            //        tblSucursales.Rows.InsertAt(row, 2);
            //    }
            //}

            

            //clbSucursal.DataSource = tblSucursales;
            //clbSucursal.DisplayMember = "Nombre";
            //clbSucursal.ValueMember = "Codigo";
        }

        public void Formato(DataGridView dgv)
        {
            DataGridViewButtonColumn botonCausas = new DataGridViewButtonColumn();
            {
                botonCausas.Name = "Corcho";
                botonCausas.HeaderText = "Validación";
                botonCausas.Text = "Validación";
                botonCausas.UseColumnTextForButtonValue = true;
            }
            dgv.Columns.Add(botonCausas);
            
            dgv.Columns[(int)Columnas.Corcho].Visible = false;  
            dgv.Columns[(int)Columnas.DocEntry].Visible = false;  
            dgv.Columns[(int)Columnas.Responsable].Visible = false;
            dgv.Columns[(int)Columnas.CardCode].Visible = false;

            dgv.Columns[(int)Columnas.Tipo].HeaderText = "Tipo de\r\ndocumento";
            dgv.Columns[(int)Columnas.Factura].HeaderText = "Folio SAP";
            dgv.Columns[(int)Columnas.Fecha].HeaderText = "Fecha";
            dgv.Columns[(int)Columnas.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)Columnas.Asesor].HeaderText = "Asesor\r\nde ventas";
            dgv.Columns[(int)Columnas.Condicion].HeaderText = "Condición\r\nde pago";
            dgv.Columns[(int)Columnas.Estatus].HeaderText = "Estatus";
            dgv.Columns[(int)Columnas.Total].HeaderText = "Total del\r\ndocumento";
            dgv.Columns[(int)Columnas.Recibido].HeaderText = "Total \r\nrecibido";
            dgv.Columns[(int)Columnas.Responsable].HeaderText = "Responsable de\r\nentregar";
            dgv.Columns[(int)Columnas.Dias].HeaderText = "Días\r\ntrans";

            dgv.Columns[(int)Columnas.Tipo].ReadOnly = true; 
            dgv.Columns[(int)Columnas.Factura].ReadOnly = true;
            dgv.Columns[(int)Columnas.Fecha].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dgv.Columns[(int)Columnas.Asesor].ReadOnly = true;
            dgv.Columns[(int)Columnas.Condicion].ReadOnly = true;
            dgv.Columns[(int)Columnas.Estatus].ReadOnly = true;
            dgv.Columns[(int)Columnas.Total].ReadOnly = true;
            dgv.Columns[(int)Columnas.Recibido].ReadOnly = false;
            dgv.Columns[(int)Columnas.Responsable].ReadOnly = true;
            dgv.Columns[(int)Columnas.Dias].ReadOnly = true;
            dgv.Columns[(int)Columnas.Causas].ReadOnly = true;
            dgv.Columns[(int)Columnas.Escan].ReadOnly = true;
            dgv.Columns[(int)Columnas.CausasCierre].ReadOnly = true;

            dgv.Columns[(int)Columnas.Tipo].Width = 85;
            dgv.Columns[(int)Columnas.Factura].Width = 85;
            dgv.Columns[(int)Columnas.Fecha].Width = 90;
            dgv.Columns[(int)Columnas.Cliente].Width = 200;
            dgv.Columns[(int)Columnas.Asesor].Width = 150;
            dgv.Columns[(int)Columnas.Condicion].Width = 80;
            dgv.Columns[(int)Columnas.Estatus].Width = 70;
            dgv.Columns[(int)Columnas.Total].Width = 90;
            dgv.Columns[(int)Columnas.Recibido].Width = 90;
            dgv.Columns[(int)Columnas.Responsable].Width = 120;
            dgv.Columns[(int)Columnas.Dias].Width = 50;
            dgv.Columns[(int)Columnas.Escan].Width = 75;
            dgv.Columns[(int)Columnas.Boton].Width = 70;

            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Recibido].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Recibido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //foreach (DataGridViewColumn item in dgv.Columns)
            //{
            //    item.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
        }

        public void FormatoReporte(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasReporte.Responsable].Visible = false;
            dgv.Columns[(int)ColumnasReporte.Docentry].Visible = false;
            dgv.Columns[(int)ColumnasReporte.Cierre].Visible = false;

            dgv.Columns[(int)ColumnasReporte.Tipo].HeaderText = "Tipo de\r\ndocumento";
            dgv.Columns[(int)ColumnasReporte.Factura].HeaderText = "Folio SAP";
            dgv.Columns[(int)ColumnasReporte.Fecha].HeaderText = "Fecha";
            dgv.Columns[(int)ColumnasReporte.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)ColumnasReporte.Asesor].HeaderText = "Asesor\r\nde ventas";
            dgv.Columns[(int)ColumnasReporte.Condicion].HeaderText = "Condición\r\nde pago";
            dgv.Columns[(int)ColumnasReporte.Estatus].HeaderText = "Estatus";
            dgv.Columns[(int)ColumnasReporte.Total].HeaderText = "Total del\r\ndocumento";
            dgv.Columns[(int)ColumnasReporte.Responsable].HeaderText = "Responsable de\r\nentregar";
            dgv.Columns[(int)ColumnasReporte.Dias].HeaderText = "Días\r\ntrans";

            dgv.Columns[(int)ColumnasReporte.Tipo].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Factura].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Fecha].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Cliente].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Asesor].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Condicion].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Estatus].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Total].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Reicibibo].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Responsable].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Dias].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporte.Causas].ReadOnly = true;

            dgv.Columns[(int)ColumnasReporte.Tipo].Width = 85;
            dgv.Columns[(int)ColumnasReporte.Factura].Width = 85;
            dgv.Columns[(int)ColumnasReporte.Fecha].Width = 90;
            dgv.Columns[(int)ColumnasReporte.Cliente].Width = 200;
            dgv.Columns[(int)ColumnasReporte.Asesor].Width = 150;
            dgv.Columns[(int)ColumnasReporte.Condicion].Width = 80;
            dgv.Columns[(int)ColumnasReporte.Estatus].Width = 70;
            dgv.Columns[(int)ColumnasReporte.Total].Width = 90;
            dgv.Columns[(int)ColumnasReporte.Reicibibo].Width = 90;
            dgv.Columns[(int)ColumnasReporte.Responsable].Width = 120;
            dgv.Columns[(int)ColumnasReporte.Dias].Width = 50;

            dgv.Columns[(int)ColumnasReporte.Total].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasReporte.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasReporte.Reicibibo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasReporte.Reicibibo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasReporte.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //foreach (DataGridViewColumn item in dgv.Columns)
            //{
            //    item.SortMode = DataGridViewColumnSortMode.NotSortable;
            //}
        }

        public void FormatoCorcho(DataGridView dgv)
        {
            DataGridViewButtonColumn botonCausas = new DataGridViewButtonColumn();
            {
                botonCausas.Name = "Quitar";
                botonCausas.HeaderText = "Quitar";
                botonCausas.Text = "Quitar";
                //botonCausas.Width = 130;
                botonCausas.UseColumnTextForButtonValue = true;
            }
            dgv.Columns.Add(botonCausas);

            dgv.Columns[(int)ColumnasCorcho.Responsable].Visible = false;
            dgv.Columns[(int)ColumnasCorcho.CardCode].Visible = false;
            dgv.Columns[(int)ColumnasCorcho.Docentry].Visible = false;

            dgv.Columns[(int)ColumnasCorcho.Factura].HeaderText = "Factura";
            dgv.Columns[(int)ColumnasCorcho.Fecha].HeaderText = "Fecha";
            dgv.Columns[(int)ColumnasCorcho.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)ColumnasCorcho.Asesor].HeaderText = "Asesor\r\nde ventas";
            dgv.Columns[(int)ColumnasCorcho.Condicion].HeaderText = "Condición\r\nde pago";
            dgv.Columns[(int)ColumnasCorcho.Estatus].HeaderText = "Estatus";
            dgv.Columns[(int)ColumnasCorcho.Total].HeaderText = "Total\r\ndel documento";
            dgv.Columns[(int)ColumnasCorcho.Responsable].HeaderText = "Responsable de\r\nentregar";
            dgv.Columns[(int)ColumnasCorcho.Dias].HeaderText = "Días\r\ntrans";

            dgv.Columns[(int)ColumnasCorcho.Factura].ReadOnly = true;
            dgv.Columns[(int)ColumnasCorcho.Fecha].ReadOnly = true;
            dgv.Columns[(int)ColumnasCorcho.Cliente].ReadOnly = true;
            dgv.Columns[(int)ColumnasCorcho.Asesor].ReadOnly = true;
            dgv.Columns[(int)ColumnasCorcho.Condicion].ReadOnly = true;
            dgv.Columns[(int)ColumnasCorcho.Estatus].ReadOnly = true;
            dgv.Columns[(int)ColumnasCorcho.Total].ReadOnly = true; 
            dgv.Columns[(int)ColumnasCorcho.Recibido].ReadOnly = true;
            dgv.Columns[(int)ColumnasCorcho.Responsable].ReadOnly = true;
            dgv.Columns[(int)ColumnasCorcho.Dias].ReadOnly = true;

            dgv.Columns[(int)ColumnasCorcho.Tipo].Width = 85;
            dgv.Columns[(int)ColumnasCorcho.Factura].Width = 85;
            dgv.Columns[(int)ColumnasCorcho.Fecha].Width = 90;
            dgv.Columns[(int)ColumnasCorcho.Cliente].Width = 200;
            dgv.Columns[(int)ColumnasCorcho.Asesor].Width = 150;
            dgv.Columns[(int)ColumnasCorcho.Condicion].Width = 80;
            dgv.Columns[(int)Columnas.Estatus].Width = 70;
            dgv.Columns[(int)ColumnasCorcho.Total].Width = 90;
            dgv.Columns[(int)ColumnasCorcho.Recibido].Width = 90;
            dgv.Columns[(int)ColumnasCorcho.Responsable].Width = 80;
            dgv.Columns[(int)ColumnasCorcho.Dias].Width = 50;
            dgv.Columns[(int)ColumnasCorcho.Causas].Width = 120;
            dgv.Columns[(int)ColumnasCorcho.Boton].Width = 60;

            dgv.Columns[(int)ColumnasCorcho.Total].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasCorcho.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasCorcho.Recibido].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasCorcho.Recibido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasCorcho.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        string SucursalName = string.Empty;
        public string Cadena(CheckedListBox clb)
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
                    SucursalName =item["Nombre"].ToString();
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

        public int ActualiarEstatus(int docentry, string tipo)
        {
            //if (tipo = )
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "sp_Corcho";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    command.Parameters.AddWithValue("@DocEntry", docentry);
                    command.Parameters.AddWithValue("@DocNum", 0);
                    command.Parameters.AddWithValue("@Responsable", string.Empty);
                    command.Parameters.AddWithValue("@chkb1", false);
                    command.Parameters.AddWithValue("@chkb2", false);
                    command.Parameters.AddWithValue("@chkb3", false);
                    command.Parameters.AddWithValue("@chkb4", false);
                    command.Parameters.AddWithValue("@chkb5", false);
                    command.Parameters.AddWithValue("@chkb6", false);
                    command.Parameters.AddWithValue("@chkb7", false);
                    command.Parameters.AddWithValue("@chkb8", false);
                    command.Parameters.AddWithValue("@chkb9", false);
                    command.Parameters.AddWithValue("@chkb10", false);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@Completar", string.Empty);
                    command.Parameters.AddWithValue("@Tipo", tipo);
                    command.Parameters.AddWithValue("@Estatus", "Impreso");
                    command.Parameters.AddWithValue("@Descuentos", string.Empty);
                    command.Parameters.AddWithValue("@TC_TD", string.Empty);
                    command.Parameters.AddWithValue("@Recibido", decimal.Zero);
                    
                    connection.Open();

                    int row = command.ExecuteNonQuery();

                    return row;
                }
            }
        }

        private void FacturasPendientes_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.CargarSucursales();
                btnConsultar_Click(sender, e);

                log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                //chbRefacturaciones.Visible = true;
                button1.Enabled = false;

                FechaConsulta = dtFecha.Value;
                Imprimir = "Consulta";

                if (Facturas.Columns.Contains("Escaneado"))
                    Facturas.Columns.Remove("Escaneado");

                btnConsultar.BackColor = Color.Silver;
                btnReporte.BackColor = Color.FromName("Control");
                btnCorcho.BackColor = Color.FromName("Control");

                dgvFacts.DataSource = null;
                dgvFacts.Columns.Clear();
                txtFactura.Enabled = false;
                Facturas.Clear();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "sp_Corcho";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@TipoConsulta", 11);
                        command.Parameters.AddWithValue("@Fecha", dtFecha.Value);
                        command.Parameters.AddWithValue("@DocEntry", 0);
                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Responsable", string.Empty);
                        command.Parameters.AddWithValue("@chkb1", false);
                        command.Parameters.AddWithValue("@chkb2", false);
                        command.Parameters.AddWithValue("@chkb3", false);
                        command.Parameters.AddWithValue("@chkb4", false);
                        command.Parameters.AddWithValue("@chkb5", false);
                        command.Parameters.AddWithValue("@chkb6", false);
                        command.Parameters.AddWithValue("@chkb7", false);
                        command.Parameters.AddWithValue("@chkb8", false);
                        command.Parameters.AddWithValue("@chkb9", false);
                        command.Parameters.AddWithValue("@chkb10", false);
                        command.Parameters.AddWithValue("@Sucursales", this.Cadena(clbSucursal) );//+ (chbRefacturaciones.Checked ? ",161" : string.Empty)
                        command.Parameters.AddWithValue("@Completar", string.Empty);
                        command.Parameters.AddWithValue("@Tipo", string.Empty);
                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Descuentos", string.Empty);
                        command.Parameters.AddWithValue("@TC_TD", string.Empty);
                        command.Parameters.AddWithValue("@Recibido", decimal.Zero);

                        DataTable table = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;
                        da.Fill(Facturas);


                        /*--------------------------Agregar Columna para ver escaneados-------------------------------------------*/

                        Facturas.Columns.Add("Escaneado", typeof(bool));
                        foreach (DataRow item in Facturas.Rows)
                        {
                            //string path = @"V:\Facturas\";

                            String path = System.Configuration.ConfigurationSettings.AppSettings["rutaDocumentos"].ToString();

                            path += item.Field<string>("CardCode") + @"\";

                            if (System.IO.Directory.Exists(path))
                            {
                                string[] files = System.IO.Directory.GetFiles(path, item.Field<Int32>("DocEntry") + "-" + item.Field<Int32>("Folio SAP") + ".pdf");

                                if (files.Count() != 0)
                                {
                                    item.SetField("Escaneado", true);
                                }
                                else
                                {
                                    item.SetField("Escaneado", false);
                                }
                            }
                        }
                        
                        
                        /*--------------------------------------------------------------------------------------------------------*/



                        dgvFacts.DataSource = Facturas;
                        this.Formato(dgvFacts);

                        if (Facturas.Rows.Count > 0)
                            txtFactura.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUSD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((sender as DataGridView).Columns[e.ColumnIndex].Name == "Corcho")
                {
                    int DocEntry = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells["DocEntry"].Value);
                    int DocNum = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells["Folio SAP"].Value);
                    string Condicion = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells["Condicion de pago"].Value);
                    string Tipo = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells["Tipo"].Value);
                    string Cliente = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells["CardCode"].Value);

                    Corcho.Causas frmCausas = new Corcho.Causas(DocEntry, DocNum, false, Condicion, Tipo, Cliente);
                    frmCausas.ShowDialog();

                    //btnConsultar_Click(sender, e);
                }
                if ((sender as DataGridView).Columns[e.ColumnIndex].Name == "Quitar")
                {
                    int DocEntry = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells["DocEntry"].Value);
                    int DocNum = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells["Folio SAP"].Value);
                    string Condicion = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells["Condicion de pago"].Value);
                    string Tipo = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells["Tipo"].Value);
                    string Cliente = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells["CardCode"].Value);

                    Corcho.Causas frmCausas = new Corcho.Causas(DocEntry, DocNum, true, Condicion, Tipo, Cliente);
                    frmCausas.ShowDialog();

                    //btnCorcho_Click(sender, e);
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtFactura_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable t1 = new DataTable();

                dgvFacts.DataSource = null;
                dgvFacts.Columns.Clear();

                t1 =( from item in Facturas.AsEnumerable()
                    where !item.Field<string>("Asesor de ventas").Contains("Total")
                    select item).CopyToDataTable();

                DataTable filter = new DataTable();

                filter = (from item in t1.AsEnumerable()
                         where item.Field<Int32>("Folio SAP").ToString().Contains(txtFactura.Text)
                         select item).CopyToDataTable();

                dgvFacts.DataSource = filter;
                this.Formato(dgvFacts);

            }
            catch (Exception)
            {
                
            }
        }

        private void clbSucursal_Click(object sender, EventArgs e)
        {
            if (clbSucursal.SelectedIndex == 0)
            {
                if (clbSucursal.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbSucursal.Items.Count; item++)
                    {
                        clbSucursal.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbSucursal.Items.Count; item++)
                    {
                        clbSucursal.SetItemChecked(item, true);
                    }
                }
            }
        }
        
        private void btnCorcho_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = true;

                FechaConsulta = dtFecha.Value;
                btnConsultar.BackColor = Color.FromName("Control");
                btnReporte.BackColor = Color.FromName("Control");
                btnCorcho.BackColor = Color.Silver; 

                Imprimir = "Corcho";
                txtFactura.Clear();
                txtFactura.Enabled = false;

                dgvFacts.DataSource = null;
                dgvFacts.Columns.Clear();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "sp_Corcho";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@TipoConsulta", 6);
                        command.Parameters.AddWithValue("@Fecha", dtFecha.Value);
                        command.Parameters.AddWithValue("@DocEntry", 0);
                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Responsable", string.Empty);
                        command.Parameters.AddWithValue("@chkb1", false);
                        command.Parameters.AddWithValue("@chkb2", false);
                        command.Parameters.AddWithValue("@chkb3", false);
                        command.Parameters.AddWithValue("@chkb4", false);
                        command.Parameters.AddWithValue("@chkb5", false);
                        command.Parameters.AddWithValue("@chkb6", false);
                        command.Parameters.AddWithValue("@chkb7", false);
                        command.Parameters.AddWithValue("@chkb8", false);
                        command.Parameters.AddWithValue("@chkb9", false);
                        command.Parameters.AddWithValue("@chkb10", false);
                        command.Parameters.AddWithValue("@Sucursales", this.Cadena(clbSucursal) );//+ (chbRefacturaciones.Checked ? ",161" : string.Empty)
                        command.Parameters.AddWithValue("@Completar", string.Empty);
                        command.Parameters.AddWithValue("@Tipo", string.Empty);
                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Descuentos", string.Empty);
                        command.Parameters.AddWithValue("@TC_TD", string.Empty);
                        command.Parameters.AddWithValue("@Recibido", decimal.Zero);

                        DataTable table = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;
                        da.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            var query = (from item in table.AsEnumerable()
                                         select item.Field<string>("Asesor de ventas")).Distinct();

                            foreach (var item in query.ToList())
                            {
                                DataRow r = table.NewRow();
                                r["Asesor de ventas"] = item.ToString() + " Total";

                                r["Total del documento"] = (from acum in table.AsEnumerable()
                                                            where acum.Field<string>("Asesor de ventas") == item
                                                            select acum.Field<decimal>("Total del documento")).Sum();
                                r["Total recibido"] = (from acum in table.AsEnumerable()
                                                            where acum.Field<string>("Asesor de ventas") == item
                                                            select acum.Field<decimal>("Total recibido")).Sum();
                                table.Rows.Add(r);

                            }

                            table = (from tv in table.AsEnumerable()
                                     orderby tv.Field<string>("Asesor de ventas")
                                     select tv).CopyToDataTable();

                            dgvFacts.DataSource = table;
                           this.FormatoCorcho(dgvFacts);

                            dgvUSD_DataBindingComplete(dgvFacts as object, e as DataGridViewBindingCompleteEventArgs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = true;

                FechaConsulta = dtFecha.Value;
                btnConsultar.BackColor = Color.FromName("Control");
                btnReporte.BackColor = Color.Silver;
                btnCorcho.BackColor = Color.FromName("Control");

                Imprimir = "Reporte";

                txtFactura.Clear();
                txtFactura.Enabled = false;

                dgvFacts.DataSource = null;
                dgvFacts.Columns.Clear();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "sp_Corcho";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@TipoConsulta", 5);
                        command.Parameters.AddWithValue("@Fecha", dtFecha.Value);
                        command.Parameters.AddWithValue("@DocEntry", 0);
                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Responsable", string.Empty);
                        command.Parameters.AddWithValue("@chkb1", false);
                        command.Parameters.AddWithValue("@chkb2", false);
                        command.Parameters.AddWithValue("@chkb3", false);
                        command.Parameters.AddWithValue("@chkb4", false);
                        command.Parameters.AddWithValue("@chkb5", false);
                        command.Parameters.AddWithValue("@chkb6", false);
                        command.Parameters.AddWithValue("@chkb7", false);
                        command.Parameters.AddWithValue("@chkb8", false);
                        command.Parameters.AddWithValue("@chkb9", false);
                        command.Parameters.AddWithValue("@chkb10", false);
                        command.Parameters.AddWithValue("@Sucursales", this.Cadena(clbSucursal) );//+ (chbRefacturaciones.Checked ? ",161" : string.Empty)
                        command.Parameters.AddWithValue("@Sucursal", SucursalName);
                        command.Parameters.AddWithValue("@Completar", string.Empty);
                        command.Parameters.AddWithValue("@Tipo", string.Empty);
                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Descuentos", string.Empty);
                        command.Parameters.AddWithValue("@TC_TD", string.Empty);
                        command.Parameters.AddWithValue("@Recibido", decimal.Zero);

                        DataTable table = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;
                        da.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            if (table.Columns.Count > 1)
                            {
                                var query = (from item in table.AsEnumerable()
                                             select item.Field<string>("Condicion de pago")).Distinct();

                                foreach (var item in query.ToList())
                                {
                                    DataRow r = table.NewRow();
                                    r["Condicion de pago"] = item.ToString() + " Total";

                                    r["Total del documento"] = (from acum in table.AsEnumerable()
                                                                where acum.Field<string>("Condicion de pago") == item
                                                                select acum.Field<decimal>("Total del documento")).Sum();
                                    r["Total recibido"] = (from acum in table.AsEnumerable()
                                                                where acum.Field<string>("Condicion de pago") == item
                                                           select acum.Field<decimal>("Total recibido")).Sum();
                                    table.Rows.Add(r);

                                }

                                table = (from tv in table.AsEnumerable()
                                         orderby tv.Field<string>("Condicion de pago")
                                         select tv).CopyToDataTable();

                                dgvFacts.DataSource = table;
                                this.FormatoReporte(dgvFacts);

                                dgvUSD_DataBindingComplete(dgvFacts as object, e as DataGridViewBindingCompleteEventArgs);
                            }
                            else
                            {
                                int Pendientes = (from item in table.AsEnumerable()
                                                  select item.Field<int>("Pendientes")).FirstOrDefault();

                                MessageBox.Show("No se pudo generar el reporte\r\n\r\nTienes " + Pendientes + " factura(s) pendientes por validar\r\npara generar este reporte debes validar todas las facturas",
                                    "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvUSD_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Imprimir.Equals("Reporte"))
                    {
                        if (item.Cells[(int)ColumnasReporte.Condicion].Value.ToString().Contains("Total"))
                        {
                            item.Cells[(int)ColumnasReporte.Condicion].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                            item.Cells[(int)ColumnasReporte.Total].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                        }

                        if (item.Cells[(int)ColumnasReporte.Fecha].Value != DBNull.Value)
                            if (Convert.ToDateTime(item.Cells[(int)ColumnasReporte.Fecha].Value) == FechaConsulta.Date)
                            {
                                item.Cells[(int)ColumnasReporte.Fecha].Style.BackColor = Color.Green;
                                item.Cells[(int)ColumnasReporte.Fecha].Style.ForeColor = Color.Black;
                            }
                            else
                            {
                                item.Cells[(int)ColumnasReporte.Fecha].Style.BackColor = Color.Red;
                                item.Cells[(int)ColumnasReporte.Fecha].Style.ForeColor = Color.White;
                            }
                    }

                    if (Imprimir.Equals("Corcho"))
                    {
                        if (item.Cells[(int)ColumnasCorcho.Asesor].Value.ToString().Contains("Total"))
                        {
                            item.Cells[(int)ColumnasCorcho.Asesor].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                            item.Cells[(int)ColumnasCorcho.Total].Style.Font = new Font("Calibri", 11, FontStyle.Bold);
                        }

                    }

                    if (Imprimir.Equals("Consulta"))
                    {
                        if (item.Cells[(int)Columnas.Causas].Value.ToString().Trim().Equals(string.Empty))
                        {
                            item.Cells[(int)Columnas.Factura].Style.BackColor = Color.FromArgb(255, 255, 0);
                        }

                        if (item.Cells[(int)Columnas.Corcho].Value.ToString().Equals("Y"))
                        {
                            item.Cells[(int)Columnas.CausasCierre].Style.BackColor = Color.LightGray;
                            item.Cells[(int)Columnas.CausasCierre].Style.BackColor = Color.LightGray;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvFacts.DataSource != null)
            {
                if (Imprimir.Equals("Reporte"))
                {
                    DialogResult dialog = MessageBox.Show("¿Esta seguro que desea imprimir el reporte?", "HalcoNET", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if(dialog == System.Windows.Forms.DialogResult.OK)
                    {
                        Clases.CrearPDF pdf = new Clases.CrearPDF();
                        string _path = pdf.ToPDFCorchoxCP(dgvFacts.DataSource as DataTable, FechaConsulta);
                        if (!string.IsNullOrEmpty(_path))
                        {
                            //Actualizar U_Impreso = 'Y' PJ_TblCorcho
                            int _actualizadas = 0;
                            DataTable _datos = (from item in (dgvFacts.DataSource as DataTable).AsEnumerable()
                                                where !item.Field<string>("Condicion de pago").ToLower().Contains("total")
                                                select item).CopyToDataTable();

                            foreach (DataRow item in _datos.Rows)
                            {
                                _actualizadas += this.ActualiarEstatus(item.Field<Int32>("DocEntry"), item.Field<string>("Tipo"));

                            }
                            System.Diagnostics.Process.Start(_path);
                        }
                    }

                }
                if (Imprimir.Equals("Corcho"))
                {
                    Clases.CrearPDF pdf = new Clases.CrearPDF();
                    // pdf.ToPDFCorcho(dgvUSD.DataSource as DataTable);

                    pdf.ToPDFCorcho(dgvFacts.DataSource as DataTable);
                }
            }
        }

        private void FacturasPendientes_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void FacturasPendientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
        }

        private void dgvFacts_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Imprimir.Equals("Consulta"))
                {
                    if(e.ColumnIndex == (int)Columnas.Recibido)
                    {
                        DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand())
                            {
                                command.CommandText = "sp_Corcho";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Connection = connection;

                                command.Parameters.AddWithValue("@TipoConsulta", 3);
                                command.Parameters.AddWithValue("@Fecha", dtFecha.Value);
                                command.Parameters.AddWithValue("@DocEntry", Convert.ToInt32(row.Cells[(int)Columnas.DocEntry].Value));
                                command.Parameters.AddWithValue("@DocNum", 0);
                                command.Parameters.AddWithValue("@Responsable", string.Empty);
                                command.Parameters.AddWithValue("@chkb1", false);
                                command.Parameters.AddWithValue("@chkb2", false);
                                command.Parameters.AddWithValue("@chkb3", false);
                                command.Parameters.AddWithValue("@chkb4", false);
                                command.Parameters.AddWithValue("@chkb5", false);
                                command.Parameters.AddWithValue("@chkb6", false);
                                command.Parameters.AddWithValue("@chkb7", false);
                                command.Parameters.AddWithValue("@chkb8", false);
                                command.Parameters.AddWithValue("@chkb9", false);
                                command.Parameters.AddWithValue("@chkb10", false);
                                command.Parameters.AddWithValue("@Sucursales", this.Cadena(clbSucursal));
                                command.Parameters.AddWithValue("@Completar", string.Empty);
                                command.Parameters.AddWithValue("@Tipo", Convert.ToString(row.Cells[(int)Columnas.Tipo].Value));
                                command.Parameters.AddWithValue("@Estatus", string.Empty);
                                command.Parameters.AddWithValue("@Descuentos", string.Empty);
                                command.Parameters.AddWithValue("@TC_TD", string.Empty);
                                command.Parameters.AddWithValue("@Recibido", Convert.ToDecimal(row.Cells[(int)Columnas.Recibido].Value));

                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }
                        //MessageBox.Show("ok");
                    }
                }
            }
            catch (Exception)
            {
                 
            }
        }
       
    }
}
