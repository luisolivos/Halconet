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

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Cobranza.Corcho
{
    public partial class frmCorteCaja : Form
    {
        int __TipoReporte = 0;
        //1 -->Captura
        //2 -->Jefas Cobranza
        //3 -->Auditoria

        DataTable __DATOS = new DataTable();
        DataTable __metodos = new DataTable();
        DataTable __clientes = new DataTable();
        public DataTable JefasCobranza = new DataTable();

        private enum ColumnasCaptura
        {
            Code,
            Agregar,
            Factura,
            DocEntry,
            Cliente,
            Nombre,
            JefaCobranza,
            CondicionCredito,
            MetodoPago,
            FechaCorte,
            Monto,
            Recibido,
            Prefijo,
            Folio,
            Cancelado
        }
        private enum Columnas
        {
            Factura,
            Cliente,
            Nombre,
            JefaCobranza,
            CondicionCredito,
            MetodoPago,
            FechaCorte,
            Monto,
            Prefijo,
            Folio,
            Cancelado
        }
        private enum ColumnasReporteJefas
        {
            Factura,
            Cliente,
            Nombre,
            JefaCobranza,
            CondicionCredito,
            MetodoPago,
            FechaCorte,
            Monto,
            Prefijo,
            Folio,
            Cancelado
        }

        public frmCorteCaja(int _type, string name)
        {
            InitializeComponent();
            this.Text = name;
            __TipoReporte = _type;

            grpCaptura.Visible = __TipoReporte == 1;
            grpReporte1.Visible = __TipoReporte == 2;
            grpReporte2.Visible = __TipoReporte == 3;
            btnTitulo2.Visible = __TipoReporte == 3;
            btnEncabecazo1.Visible = __TipoReporte == 3;
            btnImprimir.Visible = __TipoReporte == 2;
            btnPDF.Visible = __TipoReporte == 1;

            if (__TipoReporte == 1)
            {
                //this.CargarInfo();
                this.CargarMetodos();
                //this.CargarClientes();
                //dgvDatos.DataSource = __DATOS;
                //this.Formato(dgvDatos);

                //if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador |
                //               (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza |
                //               (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                //{
                label8.Visible = true;
                cbSucursal1.Visible = true;

                
                //}
                //else
                //{
                //    label8.Visible = false;
                //    cbSucursal1.Visible = false;
                //}
                this.CargarSucursales(cbSucursal1);
                cbSucursal1.SelectedIndex = 1;
            }

            if (__TipoReporte == 2)
            {
                btnSave.Visible = false;
                if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador |
                               (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza |
                               (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                {
                    label6.Visible = true;
                    cbSucursal.Visible = true;
                }
                else
                {
                    label6.Visible = false;
                    cbSucursal.Visible = false;
                }
                this.CargarSucursales(cbSucursal);

            }

            if (__TipoReporte == 3)
            {
                btnSave.Visible = false;
                this.CargarJefesCobranza();

            }
        }

        private void FormatoCaptura(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasCaptura.Code].Visible = false;
            dgv.Columns[(int)ColumnasCaptura.DocEntry].Visible = false;

            dgv.Columns[(int)ColumnasCaptura.Agregar].Width = 70;
            dgv.Columns[(int)ColumnasCaptura.Cliente].Width = 70;
            dgv.Columns[(int)ColumnasCaptura.Nombre].Width = 250;
            dgv.Columns[(int)ColumnasCaptura.JefaCobranza].Width = 170;

            dgv.Columns[(int)ColumnasCaptura.Code].ReadOnly = true;
            dgv.Columns[(int)ColumnasCaptura.Factura].ReadOnly = true;
            dgv.Columns[(int)ColumnasCaptura.DocEntry].ReadOnly = true;
            dgv.Columns[(int)ColumnasCaptura.Cliente].ReadOnly = true;
            dgv.Columns[(int)ColumnasCaptura.Nombre].ReadOnly = true;
            dgv.Columns[(int)ColumnasCaptura.JefaCobranza].ReadOnly = true;
            dgv.Columns[(int)ColumnasCaptura.Monto].ReadOnly = true;

            dgv.Columns[(int)ColumnasCaptura.CondicionCredito].Width = 80;
            dgv.Columns[(int)ColumnasCaptura.MetodoPago].Width = 150;
            dgv.Columns[(int)ColumnasCaptura.FechaCorte].Width = 90;
            dgv.Columns[(int)ColumnasCaptura.Monto].Width = 80;
            dgv.Columns[(int)ColumnasCaptura.Recibido].Width = 80;
            dgv.Columns[(int)ColumnasCaptura.Prefijo].Width = 70;
            dgv.Columns[(int)ColumnasCaptura.Folio].Width = 70;
            dgv.Columns[(int)ColumnasCaptura.Cancelado].Width = 70;

            dgv.Columns[(int)ColumnasCaptura.FechaCorte].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns[(int)ColumnasCaptura.Monto].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasCaptura.Folio].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasCaptura.FechaCorte].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasCaptura.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasCaptura.Recibido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasCaptura.Prefijo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasCaptura.Folio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Factura].Width = 70;
            dgv.Columns[(int)Columnas.Cliente].Width = 70;
            dgv.Columns[(int)Columnas.Nombre].Width = 250;
            dgv.Columns[(int)Columnas.JefaCobranza].Width = 170;

            dgv.Columns[(int)Columnas.Factura].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dgv.Columns[(int)Columnas.Nombre].ReadOnly = true;
            dgv.Columns[(int)Columnas.JefaCobranza].ReadOnly = true;
            dgv.Columns[(int)Columnas.CondicionCredito].ReadOnly = true;
            dgv.Columns[(int)Columnas.MetodoPago].ReadOnly = true;
            dgv.Columns[(int)Columnas.FechaCorte].ReadOnly = true;
            dgv.Columns[(int)Columnas.Monto].ReadOnly = true;
            dgv.Columns[(int)Columnas.Prefijo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Folio].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cancelado].ReadOnly = true;

            dgv.Columns[(int)Columnas.CondicionCredito].Width = 80;
            dgv.Columns[(int)Columnas.MetodoPago].Width = 150;
            dgv.Columns[(int)Columnas.FechaCorte].Width = 90;
            dgv.Columns[(int)Columnas.Monto].Width = 80;
            dgv.Columns[(int)Columnas.Prefijo].Width = 70;
            dgv.Columns[(int)Columnas.Folio].Width = 70;
            dgv.Columns[(int)Columnas.Cancelado].Width = 70;

            dgv.Columns[(int)Columnas.FechaCorte].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns[(int)Columnas.Monto].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.FechaCorte].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Prefijo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Folio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void FormatoReporteJefas(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasReporteJefas.Factura].Width = 70;

            dgv.Columns[(int)ColumnasReporteJefas.Cliente].Width = 70;
            dgv.Columns[(int)ColumnasReporteJefas.Nombre].Width = 250;
            dgv.Columns[(int)ColumnasReporteJefas.JefaCobranza].Width = 170;

            dgv.Columns[(int)ColumnasReporteJefas.Factura].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.Cliente].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.Nombre].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.JefaCobranza].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.CondicionCredito].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.FechaCorte].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.Monto].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.Prefijo].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.Folio].ReadOnly = true;
            dgv.Columns[(int)ColumnasReporteJefas.Cancelado].ReadOnly = true;

            dgv.Columns[(int)ColumnasReporteJefas.CondicionCredito].Width = 80;
            dgv.Columns[(int)ColumnasReporteJefas.MetodoPago].Width = 150;
            dgv.Columns[(int)ColumnasReporteJefas.FechaCorte].Width = 90;
            dgv.Columns[(int)ColumnasReporteJefas.Monto].Width = 80;
            dgv.Columns[(int)ColumnasReporteJefas.Prefijo].Width = 70;
            dgv.Columns[(int)ColumnasReporteJefas.Folio].Width = 70;
            dgv.Columns[(int)ColumnasReporteJefas.Cancelado].Width = 70;

            dgv.Columns[(int)ColumnasReporteJefas.FechaCorte].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgv.Columns[(int)ColumnasReporteJefas.Monto].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasReporteJefas.FechaCorte].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasReporteJefas.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasReporteJefas.Prefijo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasReporteJefas.Folio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void CargarMetodos()
        {
            __metodos.Clear();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 3);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(__metodos);
                }
            }
        }
       
        public void CargarClientes()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 4);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(__clientes);
                }
            }
        }

        public void CargarInfo()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Desde", dtpCaptura.Value);
                    command.Parameters.AddWithValue("@Sucursal", cbSucursal1.SelectedValue.ToString().Length >= 4 ? cbSucursal1.SelectedValue.ToString().Substring(0, 4).Trim(',') : string.Empty);

                    //if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador ||
                    //                (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza ||
                    //                (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                    //{
                    //    command.Parameters.AddWithValue("@Sucursal", cbSucursal1.Text);

                    //}
                    //else
                    //    command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal.Trim());

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(__DATOS);

                    dgvDatos.DataSource = __DATOS;
                }
            }
        }

        public void MinDate()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    
                    connection.Open();

                    dtpCaptura.MinDate = Convert.ToDateTime(command.ExecuteScalar());
                }
            }
        }

        private void CargarSucursales()
        {

            SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.CommandTimeout = 0;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                DataTable _t = new DataTable();
                string suc = "";
                if (ClasesSGUV.Login.Sucursal.ToUpper() == "MTY")
                    suc = "MONTERREY";


                else if (ClasesSGUV.Login.Sucursal.ToUpper() == "GDL")
                    suc = "GUADALAJARA";
                else
                    suc = ClasesSGUV.Login.Sucursal;

                var query = from item in table.AsEnumerable()
                            where item.Field<string>("Codigo").ToUpper() == suc
                            select item;
                
                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                    cbSucursal.DataSource = _t;
                    cbSucursal.DisplayMember = "Nombre";
                    cbSucursal.ValueMember = "Codigo";
                }

            }
            else
            //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador)
            {
                cbSucursal.DataSource = table;
                cbSucursal.DisplayMember = "Nombre";
                cbSucursal.ValueMember = "Codigo";
            }

        }

        private void CargarSucursales(ComboBox cb)
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

                    cb.DataSource = table;
                    cb.DisplayMember = "Nombre";
                    cb.ValueMember = "Codigo";

                }
            }
        }

        private void CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
            command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                DataTable _t = new DataTable();
                var query = from item in table.AsEnumerable()
                            where item.Field<string>("Codigo").ToUpper() == getMemo(ClasesSGUV.Login.Sucursal)
                            select item;

                if (query.Count() > 0)
                {

                    _t = query.CopyToDataTable();
                    cbCobranza.DataSource = _t;
                    cbCobranza.DisplayMember = "Nombre";
                    cbCobranza.ValueMember = "Codigo";
                    JefasCobranza = _t.Copy();
                    DataRow row = _t.NewRow();
                    row["Nombre"] = "TODAS";
                    row["Codigo"] = "0";
                    _t.Rows.InsertAt(row, 0);
                }

            }
            else
            {
                cbCobranza.DataSource = table;
                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);
                cbCobranza.DisplayMember = "Nombre";
                cbCobranza.ValueMember = "Codigo";
                JefasCobranza = table.Copy();
            }
        }

        public string getMemo(string Sucursal)
        {
            string _memo = "";
            switch (Sucursal)
            {
                case "PUEBLA": _memo = "01"; break;
                case "MONTERREY": _memo = "02"; break;
                case "MTY": _memo = "02"; break;
                case "APIZACO": _memo = "03"; break;
                case "CORDOBA": _memo = "05"; break;
                case "TEPEACA": _memo = "06"; break;
                case "EDOMEX": _memo = "16"; break;
                case "GDL": _memo = "18"; break;
                case "GUADALAJARA": _memo = "18"; break;
                case "SALTILLO": _memo = "23"; break;
            }

            return _memo;
        }

        public string getCarpeta(string Sucursal)
        {
            string _memo = "";
            switch (Sucursal)
            {
                case "Puebla": _memo = "PUEBLA"; break;
                case "Monterrey": _memo = "MTY"; break;
                case "Apizaco": _memo = "APIZACO"; break;
                case "Cordoba": _memo = "CORDOBA"; break;
                case "Tepeaca": _memo = "TEPEACA"; break;
                case "Estado de Mexico": _memo = "EDOMEX"; break;
                case "Guadalajara": _memo = "GDL"; break;
                case "Saltillo": _memo = "SALTILLO"; break;
            }

            return _memo;
        }

        private void frmIngresos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                if (__TipoReporte == 1)
                {
                    //dtpCaptura.MinDate = DateTime.Now;
                    this.MinDate();
                    //this.CargarInfo();
                    this.CargarMetodos();
                    this.CargarClientes();
                    //dgvDatos.DataSource = __DATOS;
                    //this.FormatoCaptura(dgvDatos);
                    
                    //if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador |
                    //               (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza |
                    //               (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                    //{
                    //    label8.Visible = true;
                    //    cbSucursal1.Visible = true;
                    //}
                    //else
                    //{
                    //    label8.Visible = false;
                    //    cbSucursal1.Visible = false;
                    //}
                    //this.CargarSucursales(cbSucursal1);
                }

                if (__TipoReporte == 2)
                {
                    //btnSave.Visible = false;
                    //if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador |
                    //               (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza |
                    //               (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                    //{
                    //    label6.Visible = true;
                    //    cbSucursal.Visible = true;
                    //}
                    //else
                    //{
                    //    label6.Visible = false;
                    //    cbSucursal.Visible = false;
                    //} 
                    //this.CargarSucursales(cbSucursal);
                    
                }

                if (__TipoReporte == 3)
                {
                    //btnSave.Visible = false;
                    //this.CargarJefesCobranza();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
     
        }

        private void gridFacturas_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (__TipoReporte == 1)
                {
                    DataGridView dgv = (sender as DataGridView);
                    if (dgv == null) return;

                    int filaEdit = e.RowIndex;
                    int columnaEdit = e.ColumnIndex;

                    if (filaEdit == -1) return;
                    if (columnaEdit == (int)ColumnasCaptura.MetodoPago)
                    {
                        object objValCell = dgv.Rows[filaEdit].Cells[columnaEdit].Value;
                        if (objValCell == null)
                            return;
                        string valCell = objValCell.ToString();
                        DataGridViewComboBoxCell celcombo = new DataGridViewComboBoxCell();
                        object objTipoInci = dgv.Rows[filaEdit].Cells[(int)ColumnasCaptura.MetodoPago].Value;

                        if (objTipoInci == null) return;

                        string tipoInci = objTipoInci.ToString();
                        celcombo.DataSource = __metodos;
                        celcombo.ValueMember = "Nombre";
                        celcombo.DisplayMember = "Nombre";
                        //celcombo.
                        // el campo es NULL(BD, no se a justificado) 
                        if (valCell == string.Empty)
                        {
                            dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                        }
                        else
                        {
                            celcombo.Value = valCell.Trim();
                            dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.ProductName, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridFacturas_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)ColumnasCaptura.Cliente & __TipoReporte == 1)
            //{
            //    var source = new AutoCompleteStringCollection();


            //    string[] stringArray = Array.ConvertAll<DataRow, String>(__clientes.Select(), delegate(DataRow row) { return (String)row["Codigo"]; });

            //    source.AddRange(stringArray);

            //    TextBox prodCode = e.Control as TextBox;
            //    if (prodCode != null)
            //    {
            //        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //        prodCode.AutoCompleteCustomSource = source;
            //        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //    }
            //}
            //else
            //{
            //    TextBox prodCode = e.Control as TextBox;
            //    if (prodCode != null)
            //    {
            //        prodCode.AutoCompleteMode = AutoCompleteMode.None;
            //        prodCode.AutoCompleteSource = AutoCompleteSource.None;
            //    }
            //}
        }

        private void gridFacturas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)Columnas.Cliente & __TipoReporte == 1)
                {
                    string item = (sender as DataGridView).CurrentCell.Value.ToString();

                    var qry = (from itemvar in __clientes.AsEnumerable()
                               where itemvar.Field<string>("Codigo").ToLower().Equals(item.ToLower())
                               select itemvar).CopyToDataTable();

                    if (qry.Rows.Count > 0)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = (sender as DataGridView).CurrentRow;

                        row.Cells[(int)Columnas.Nombre].Value = qry.Rows[0].Field<string>("Nombre");
                        row.Cells[(int)Columnas.JefaCobranza].Value = qry.Rows[0].Field<string>("VatIdUnCmp");
                        row.Cells[(int)Columnas.CondicionCredito].Value = qry.Rows[0].Field<string>("PymntGroup");
                    }
                }
            }
            catch (Exception) { }
        }

        private void gridFacturas_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (__TipoReporte == 1)
            {
                var grid = (sender as DataGridView);
                e.Row.Cells[(int)ColumnasCaptura.FechaCorte].Value = DateTime.Now;
                e.Row.Cells[(int)ColumnasCaptura.Cancelado].Value = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Esta seguro que desea guadar los cambios?.\r\nSi elige continuar, no podrá modificar la información almacenada", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != System.Windows.Forms.DialogResult.Yes)
                return;

            SqlCommand command = null;
            try
            {
                toolStatus.Text = string.Empty;
                foreach (DataRow item in __DATOS.Rows)
                {
                    if (item.RowState == DataRowState.Added || item.RowState == DataRowState.Modified)
                    {
                        if (item.Field<bool>("Agregar"))

                            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                            {
                                using (command = new SqlCommand("sp_Ingresos", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                                    command.Parameters.AddWithValue("@Code", item[0]);
                                    command.Parameters.AddWithValue("@Factura", item[2]);
                                    command.Parameters.AddWithValue("@DocEntry", item[3]);
                                    command.Parameters.AddWithValue("@Cliente", item[4]);
                                    command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);
                                    command.Parameters.AddWithValue("@Jefa", item[6]);
                                    command.Parameters.AddWithValue("@MetodoPago", item[8]);
                                    command.Parameters.AddWithValue("@FechaCorte", item[9]);
                                    command.Parameters.AddWithValue("@Monto", item[11]);
                                    command.Parameters.AddWithValue("@Prefijo", item[12]);
                                    command.Parameters.AddWithValue("@Folio", item[13]);
                                    command.Parameters.AddWithValue("@Cancelado", item[14]);

                                    if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador ||
                                        (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza ||
                                        (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                                    {
                                        command.Parameters.AddWithValue("@Sucursal", this.getCarpeta(cbSucursal1.Text));

                                    }
                                    else
                                        command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal.Trim());


                                    connection.Open();

                                    if (command.ExecuteNonQuery() != 1)
                                        return;


                                }
                            }
                    }
                    //if (item.RowState == DataRowState.Deleted)
                    //{
                    //    item.RejectChanges();
                    //    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    //    {
                    //        using (command = new SqlCommand("sp_Ingresos", connection))
                    //        {
                    //            command.CommandType = CommandType.StoredProcedure;
                    //            command.Parameters.AddWithValue("@TipoConsulta", 7);
                    //            command.Parameters.AddWithValue("@Code", item[0]);
                    //            command.Parameters.AddWithValue("@Cliente", item[1]);
                    //            command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);
                    //            command.Parameters.AddWithValue("@Jefa", item[3]);
                    //            command.Parameters.AddWithValue("@MetodoPago", item[5]);
                    //            command.Parameters.AddWithValue("@FechaCorte", item[6]);
                    //            command.Parameters.AddWithValue("@Monto", item[7]);
                    //            command.Parameters.AddWithValue("@Prefijo", item[8]);
                    //            command.Parameters.AddWithValue("@Folio", item[9]);
                    //            command.Parameters.AddWithValue("@Cancelado", item[10]);

                    //            connection.Open();

                    //            if (command.ExecuteNonQuery() != 1)
                    //                return;


                    //        }
                    //    }
                    //}
                }
                toolStatus.Text = "Listo!";
                __DATOS.AcceptChanges();

                button2_Click(sender, e);
            }
            catch (Exception ex)
            {
                toolStatus.Text = ex.Message;
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ClasesSGUV.ConvertToPDF pdf = new ClasesSGUV.ConvertToPDF();
                string nombre = pdf.CreatePDF(dgvDatos);

                //string nombre = pdf.Nombre;
                PdfReader reader = new PdfReader(nombre);
                string nombreCopia = Path.GetTempFileName() + ".pdf";
                PdfStamper stamper = new PdfStamper(reader, new FileStream(nombreCopia, FileMode.Create));
                AcroFields fields = stamper.AcroFields;
                stamper.JavaScript = "this.print(true);\r";
                stamper.FormFlattening = true;
                stamper.Close();
                reader.Close();

                System.Diagnostics.Process.Start(nombreCopia);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //dgvDatos.AllowUserToAddRows = true;
                int x = 0;
                foreach (DataRow row in __DATOS.Rows)
                {
                    if (row.RowState == DataRowState.Modified || row.RowState == DataRowState.Added)
                        x++;
                }
                if (x > 0)
                {
                    DialogResult _result = MessageBox.Show("¿Desea guardar los cambios Efetuados?", "HalcoNET", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (_result == System.Windows.Forms.DialogResult.Cancel)
                        return;
                    if (_result == System.Windows.Forms.DialogResult.No)
                    {
                        __DATOS.Clear();
                        dgvDatos.DataSource = null;
                        //frmIngresos_Load(sender, e);
                        this.CargarInfo();
                        foreach (DataRow item in __DATOS.Rows)
                        {
                            if ((item.Field<string>("Metodo de pago") == null ? string.Empty : item.Field<string>("Metodo de pago")).Equals("Efectivo"))
                                item.SetField<bool>("Agregar", true);
                        }
                        this.FormatoCaptura(dgvDatos);
                        return;
                    }

                    if (_result == System.Windows.Forms.DialogResult.Yes)
                    {
                        btnSave_Click(sender, e);
                    }
                }

                __DATOS.Clear();
                dgvDatos.DataSource = null;
                this.CargarInfo();

                foreach (DataRow item in __DATOS.Rows)
                {
                    if ((item.Field<string>("Metodo de pago") == null ? string.Empty : item.Field<string>("Metodo de pago")).Equals("Efectivo"))
                        item.SetField<bool>("Agregar", true);
                }
                this.FormatoCaptura(dgvDatos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //dgvDatos.AllowUserToAddRows = false;
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 5);
                    command.Parameters.AddWithValue("@Desde", dtpDesde.Value); 
                    command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);
                    if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador ||
                                    (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza ||
                                    (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                    {
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.Text);

                    }
                    else
                        command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal.Trim());

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataTable table = new DataTable();
                    da.Fill(table);

                    dgvDatos.DataSource = table;

                    FormatoReporteJefas(dgvDatos);
                }
            }
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //rpt Jefas Cobranza
            if (__TipoReporte == 2)
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[(int)ColumnasReporteJefas.MetodoPago].Value.ToString().Contains("TOTAL:"))
                    {
                        foreach (DataGridViewCell cell in item.Cells)
                        {
                            cell.Style.BackColor = Color.LightGray;
                        }
                    }
                    else
                    {
                        foreach (DataGridViewCell cell in item.Cells)
                        {
                            cell.Style.BackColor = Color.White;
                        }
                    }
                }
            if (__TipoReporte == 3)
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[(int)Columnas.MetodoPago].Value.ToString().Contains("TOTAL:"))
                    {
                        foreach (DataGridViewCell cell in item.Cells)
                        {
                            if (cell.ColumnIndex >= (int)Columnas.MetodoPago)
                                cell.Style.BackColor = btnEncabecazo1.BackColor;
                        }

                    }
                    if (item.Cells[(int)Columnas.JefaCobranza].Value.ToString().Contains("TOTAL:"))
                    {
                        foreach (DataGridViewCell cell in item.Cells)
                        {
                            if (cell.ColumnIndex >= (int)Columnas.JefaCobranza)
                                cell.Style.BackColor = btnTitulo2.BackColor;
                        }
                    }
                }
             if (__TipoReporte == 1)
                 foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                 {
                     if (Convert.ToDecimal(item.Cells[(int)ColumnasCaptura.Code].Value) > decimal.Zero)
                         item.ReadOnly = true;
                 }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //dgvDatos.AllowUserToAddRows = false;
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 6);
                    command.Parameters.AddWithValue("@Desde", dtpReporteDesde.Value);
                    command.Parameters.AddWithValue("@Hasta", dtpReporteHasta.Value);
                    
                    command.Parameters.AddWithValue("@Jefa",cbCobranza.Text);
                    command.Parameters.AddWithValue("@Sucursal", cbSucursal.Text);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataTable table = new DataTable();
                    da.Fill(table);

                    dgvDatos.DataSource = table;
                    
                    Formato(dgvDatos);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ClasesSGUV.Exportar expo = new ClasesSGUV.Exportar();
                expo.ExportarColores(dgvDatos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnEncabecazo1_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialogE.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                btnEncabecazo1.BackColor = colorDialogE.Color;
                button4_Click(sender, e);
            }
        }

        private void btnTitulo2_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialogE.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                btnTitulo2.BackColor = colorDialogE.Color;
                button4_Click(sender, e);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string __carpeta = string.Empty;
            if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador ||
                                   (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza ||
                                   (int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
            {
                __carpeta = this.getCarpeta(cbSucursal1.Text);
            }
            else
                __carpeta = ClasesSGUV.Login.Sucursal.Trim();


            if (!string.IsNullOrEmpty(__carpeta))
            {

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.FileName = "";
                string filePath = "";
                if (DialogResult.OK == ofd.ShowDialog(this))
                {
                    filePath = ofd.FileName;

                    System.IO.Directory.CreateDirectory(ClasesSGUV.Propiedades.pathDigitalizacion + "\\Ingresos\\" + __carpeta);

                    if (System.IO.File.Exists(ClasesSGUV.Propiedades.pathDigitalizacion + "\\Ingresos\\" + __carpeta + "\\" + "ingresos - " + DateTime.Now.ToString("ddMMyyyy") + ".pdf"))
                    {
                        if (MessageBox.Show("Ya se ha cargado un archivo, ¿Desea reemplazarlo?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.IO.File.Copy(filePath, ClasesSGUV.Propiedades.pathDigitalizacion + "\\Ingresos\\" + __carpeta + "\\" + "ingresos - " + DateTime.Now.ToString("ddMMyyyy") + ".pdf", true);
                        }

                    }
                    else
                    {
                        System.IO.File.Copy(filePath, ClasesSGUV.Propiedades.pathDigitalizacion + "\\Ingresos\\" + __carpeta + "\\" + "ingresos - " + DateTime.Now.ToString("ddMMyyyy") + ".pdf", true);
                    }
                }
            }
        }



    }
}
