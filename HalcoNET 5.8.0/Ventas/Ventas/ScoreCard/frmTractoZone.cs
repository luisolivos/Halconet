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
    public partial class frmTractoZone : Form
    {
        Clases.Logs log;
        public string NombreUsuario;
        public string Vendedores;
        public string Sucursales;
        public DateTime Fecha;
        public int RolUsuario;
        public int CodigoVendedor;
        public string Sucursal;
        public decimal DiasMes = 0;
        public decimal DiasTranscurridos = 0;
        public decimal DiasRestantes = 0;
        public decimal AvanceOptimo = 0;
        public DateTime FechaOK;
        public int Mes;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        private enum TipoConsulta
        {
            DiasMes = 1,
            DiasTranscurridos = 2,
            LineasObjetivo = 9
        }

        private enum Columnas
        {
            ClaveVendedor,
            Vendedor,
            Sucursal,
            Linea,
            Objetivo,
            Acumulado,
            AcumuladoCoutaP,
            AcumuladoCoutaM,
            PronosticoM,
            PronosticoP,
            Orden
        }
        public frmTractoZone(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();

            RolUsuario = rolUsuario;
            NombreUsuario = nombreUsuario;
            CodigoVendedor = codigoVendedor;
            Sucursal = sucursal;
        }

        private void CargarLineas()
        {
            DataTable tblLineas = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_RtpLineas",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Fecha", dtFecha.Value);
                    command.Parameters.AddWithValue("@Form", this.Name);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    command.CommandTimeout = 0;

                    da.Fill(tblLineas);

                }
                
            }

            cbLinea.DataSource = tblLineas;
            cbLinea.DisplayMember = "Nombre";
            cbLinea.ValueMember = "Codigo";
        }

        /// <summary>
        /// Método que carga las sucursales en el cbSucursal
        /// </summary>
        private void CargarSucursales()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
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

                clbSucursal.DataSource = table;
                clbSucursal.DisplayMember = "Nombre";
                clbSucursal.ValueMember = "Codigo";
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 12);
                command.Parameters.AddWithValue("@Sucursal", Sucursal.Trim());
                command.Parameters.AddWithValue("@SlpCode", 0);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbSucursal.DataSource = table;
                clbSucursal.DisplayMember = "Nombre";
                clbSucursal.ValueMember = "Codigo";
            }
        }

        /// <summary>
        /// Método que carga los Vendedores en el clbVendedor
        /// </summary>
        private void CargarVendedores()
        {
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

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
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

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
        }

        /// <sumary> 
        /// Metodos que crea una cadena con el formato ,num1,nm2 etc
        /// La cadena se crea apartir de los elementos seleccionados en un CheckedListBox
        /// Si no se selecciono ningún elemento del CheckListBox se crea una cadena 
        /// que contiene todos los elmentos del mismo
        /// </sumary>
        private string CrearCadena(CheckedListBox clb)
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

        private void CargarDias()
        {
            SqlCommand command1 = new SqlCommand("PJ_ScoreCardEfectividad", conection);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.AddWithValue("@TipoConsulta", TipoConsulta.DiasMes);
            command1.Parameters.AddWithValue("@Cliente", string.Empty);
            command1.Parameters.AddWithValue("@Sucursales", string.Empty);
            command1.Parameters.AddWithValue("@Vendedores", string.Empty);
            command1.Parameters.AddWithValue("@Presupuesto", 0);
            command1.Parameters.AddWithValue("@Fecha", Fecha);
            command1.CommandTimeout = 0;

            SqlCommand command = new SqlCommand("PJ_ScoreCardEfectividad", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", TipoConsulta.DiasTranscurridos);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@Vendedores", string.Empty);
            command.Parameters.AddWithValue("@Presupuesto", 0);
            command.Parameters.AddWithValue("@Fecha", Fecha);
            command.CommandTimeout = 0;

            try
            {
                conection.Open();
                SqlDataReader reader = command1.ExecuteReader();
                if (reader.Read())
                    DiasMes = Convert.ToDecimal(reader[0]);

                SqlDataReader reader1 = command.ExecuteReader();
                if (reader1.Read())
                    DiasTranscurridos = Convert.ToDecimal(reader1[0]);

                DiasRestantes = DiasMes - DiasTranscurridos;
                AvanceOptimo = DiasTranscurridos / DiasMes;

                txtDiasMes.Text = DiasMes.ToString();
                txtDiasTranscurridos.Text = DiasTranscurridos.ToString();
                txtDiasRestantes.Text = DiasRestantes.ToString();
                txtAvanceOptimo.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }
        }

        private void CargarDiasReales()
        {
            SqlCommand command1 = new SqlCommand("PJ_ScoreCardEfectividad", conection);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.AddWithValue("@TipoConsulta", 10);
            command1.Parameters.AddWithValue("@Cliente", string.Empty);
            command1.Parameters.AddWithValue("@Sucursales", string.Empty);
            command1.Parameters.AddWithValue("@Vendedores", string.Empty);
            command1.Parameters.AddWithValue("@Presupuesto", 0);
            command1.Parameters.AddWithValue("@Fecha", Fecha);

            SqlCommand command = new SqlCommand("PJ_ScoreCardEfectividad", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 11);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@Vendedores", string.Empty);
            command.Parameters.AddWithValue("@Presupuesto", 0);
            command.Parameters.AddWithValue("@Fecha", Fecha);

            try
            {
                conection.Open();
                SqlDataReader reader = command1.ExecuteReader();
                if (reader.Read())
                    DiasMes = Convert.ToDecimal(reader[0]);

                SqlDataReader reader1 = command.ExecuteReader();
                if (reader1.Read())
                    DiasTranscurridos = Convert.ToDecimal(reader1[0]);

                DiasRestantes = DiasMes - DiasTranscurridos;
                AvanceOptimo = DiasTranscurridos / DiasMes;
                txtAvanceOptimo.Text = AvanceOptimo.ToString("P1");
                //txtDiasMes.Text = DiasMes.ToString();
                //txtDiasTranscurridos.Text = DiasTranscurridos.ToString();
                //txtDiasRestantes.Text = DiasRestantes.ToString();
                //txtAvanceOptimo.Text = AvanceOptimo.ToString("P2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }
        }

        public void Restricciones()
        {
            //Rol Vendedor
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                button3.Visible = false;
            }
            //Rol Ventas Especial
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {

                lblVendedor.Visible = false;
                clbVendedor.Visible = false;
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                Vendedores = "," + CodigoVendedor.ToString();
            }

        }

        private void Formato(DataGridView dgv, string _format)
        {
            dgv.Columns[(int)Columnas.ClaveVendedor].Visible = false;
            dgv.Columns[(int)Columnas.Orden].Visible = false;

            dgv.Columns[(int)Columnas.Vendedor].Width = 180;

            dgv.Columns[(int)Columnas.Objetivo].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 204);
            dgv.Columns[(int)Columnas.PronosticoM].DefaultCellStyle.BackColor = Color.FromArgb(252, 213, 180);

            dgv.Columns[(int)Columnas.Objetivo].DefaultCellStyle.Format = _format;
            dgv.Columns[(int)Columnas.Acumulado].DefaultCellStyle.Format = _format;
            dgv.Columns[(int)Columnas.AcumuladoCoutaM].DefaultCellStyle.Format = _format;
            dgv.Columns[(int)Columnas.AcumuladoCoutaP].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)Columnas.PronosticoM].DefaultCellStyle.Format = _format;
            dgv.Columns[(int)Columnas.PronosticoP].DefaultCellStyle.Format = "P2";

            dgv.Columns[(int)Columnas.Objetivo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Acumulado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.AcumuladoCoutaM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.AcumuladoCoutaP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PronosticoM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PronosticoP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void frmLineas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                this.CargarLineas();
                this.CargarSucursales();
                this.CargarVendedores();
                this.Restricciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Fecha = DateTime.Parse(dtFecha.Text);

                CargarDias();
                CargarDiasReales();

                string __format = string.Empty;
                int __statement = 0;

                DataRowView linea = (DataRowView)cbLinea.SelectedItem;
                __format = linea["Formato"].ToString();
                __statement = Convert.ToInt32(linea["Statement"]);

                if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                {
                    Vendedores = CrearCadena(clbVendedor);
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_RtpLineas", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", __statement);
                        command.Parameters.AddWithValue("@Fecha", dtFecha.Value);
                        command.Parameters.AddWithValue("@Form", this.Name);
                        command.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);
                        command.Parameters.AddWithValue("@Sucursales", this.CrearCadena(clbSucursal));
                        command.Parameters.AddWithValue("@Vendedores", Vendedores);

                        DataTable tblLineas = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        command.CommandTimeout = 0;
                        
                        da.Fill(tblLineas);

                        dgvDatos.DataSource = tblLineas;
                    }

                }

                this.Formato(dgvDatos, __format);

                decimal Objetivo = 0;
                decimal Venta = 0;
                decimal VentavsCuota = 0;
                decimal PorVender = 0;
                decimal PronosticoFinMesM = 0;
                decimal PronosticoFinMesP = 0;

                foreach (DataGridViewRow item in dgvDatos.Rows)
                {
                    Objetivo += Convert.ToDecimal(item.Cells[(int)Columnas.Objetivo].Value);
                    Venta += Convert.ToDecimal(item.Cells[(int)Columnas.Acumulado].Value);
                    PorVender += Convert.ToDecimal(item.Cells[(int)Columnas.AcumuladoCoutaM].Value);
                    PronosticoFinMesM += Convert.ToDecimal(item.Cells[(int)Columnas.PronosticoM].Value);
                }

                if (Objetivo != 0 && DiasTranscurridos != 0)
                {
                    VentavsCuota = Venta / Objetivo;
                    PronosticoFinMesP = ((Venta / DiasTranscurridos) * DiasMes) / Objetivo;
                }

                DataTable tblTotales = new DataTable();
                tblTotales.Columns.Add("Objetivo", typeof(decimal));
                tblTotales.Columns.Add("Venta", typeof(decimal));
                tblTotales.Columns.Add("Venta vs Cuota", typeof(decimal));
                tblTotales.Columns.Add("Por vender", typeof(decimal));
                tblTotales.Columns.Add("Tendencia fin mes($)", typeof(decimal));
                tblTotales.Columns.Add("Tendencia fin mes(%)", typeof(decimal));

                DataRow registro = tblTotales.NewRow();
                registro["Objetivo"] = Objetivo;
                registro["Venta"] = Venta;
                registro["Venta vs Cuota"] = VentavsCuota;
                registro["Por vender"] = PorVender;
                registro["Tendencia fin mes($)"] = PronosticoFinMesM;
                registro["Tendencia fin mes(%)"] = PronosticoFinMesP;
                
                tblTotales.Rows.Add(registro);
                gridTotales.DataSource = tblTotales;

                gridTotales.Columns[0].DefaultCellStyle.Format = __format;
                gridTotales.Columns[1].DefaultCellStyle.Format = __format;
                gridTotales.Columns[2].DefaultCellStyle.Format = "P2";
                gridTotales.Columns[3].DefaultCellStyle.Format = __format;
                gridTotales.Columns[4].DefaultCellStyle.Format = __format;
                gridTotales.Columns[5].DefaultCellStyle.Format = "P2";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtFecha_ValueChanged(object sender, EventArgs e)
        {
            this.CargarLineas();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmObjetivos frm = new frmObjetivos();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarAExcel ex = new ExportarAExcel();
                ex.ExportarCobranza(dgvDatos);
                MessageBox.Show("El archivo se creo con exito", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in (sender as DataGridView).Rows)
            {
                if (Convert.ToDecimal(row.Cells[(int)Columnas.AcumuladoCoutaP].Value) >= (decimal)1)
                {
                    row.Cells[(int)Columnas.AcumuladoCoutaP].Style.BackColor = Color.FromArgb(0, 176, 80);
                    row.Cells[(int)Columnas.AcumuladoCoutaP].Style.ForeColor = Color.Black;

                }
                else if (Convert.ToDecimal(row.Cells[(int)Columnas.AcumuladoCoutaP].Value) >= (decimal)0.85
                    && Convert.ToDecimal(row.Cells[(int)Columnas.AcumuladoCoutaP].Value) < (decimal)1)
                {
                    row.Cells[(int)Columnas.AcumuladoCoutaP].Style.BackColor = Color.FromArgb(255, 255, 0);
                    row.Cells[(int)Columnas.AcumuladoCoutaP].Style.ForeColor = Color.Black;
                }
                else if (Convert.ToDecimal(row.Cells[(int)Columnas.AcumuladoCoutaP].Value) < (decimal)0.85)
                {
                    row.Cells[(int)Columnas.AcumuladoCoutaP].Style.BackColor = Color.FromArgb(255, 0, 0);
                    row.Cells[(int)Columnas.AcumuladoCoutaP].Style.ForeColor = Color.White;
                }

                if (Convert.ToDecimal(row.Cells[(int)Columnas.PronosticoP].Value) >= (decimal)1)
                {
                    row.Cells[(int)Columnas.PronosticoP].Style.BackColor = Color.FromArgb(0, 176, 80);
                    row.Cells[(int)Columnas.PronosticoP].Style.ForeColor = Color.Black;

                }
                else if (Convert.ToDecimal(row.Cells[(int)Columnas.PronosticoP].Value) >= (decimal)0.85
                    && Convert.ToDecimal(row.Cells[(int)Columnas.PronosticoP].Value) < (decimal)1)
                {
                    row.Cells[(int)Columnas.PronosticoP].Style.BackColor = Color.FromArgb(255, 255, 0);
                    row.Cells[(int)Columnas.PronosticoP].Style.ForeColor = Color.Black;
                }
                else if (Convert.ToDecimal(row.Cells[(int)Columnas.PronosticoP].Value) < (decimal)0.85)
                {
                    row.Cells[(int)Columnas.PronosticoP].Style.BackColor = Color.FromArgb(255, 0, 0);
                    row.Cells[(int)Columnas.PronosticoP].Style.ForeColor = Color.White;
                }
            }
        }

        private void gridTotales_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(row.Cells[2].Value) >= (decimal)1)
                    {
                        row.Cells[2].Style.BackColor = Color.FromArgb(0, 176, 80);
                        row.Cells[2].Style.ForeColor = Color.Black;

                    }
                    else if (Convert.ToDecimal(row.Cells[2].Value) >= (decimal)0.85
                        && Convert.ToDecimal(row.Cells[2].Value) < (decimal)1)
                    {
                        row.Cells[2].Style.BackColor = Color.FromArgb(255, 255, 0);
                        row.Cells[2].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[2].Value) < (decimal)0.85)
                    {
                        row.Cells[2].Style.BackColor = Color.FromArgb(255, 0, 0);
                        row.Cells[2].Style.ForeColor = Color.White;
                    }

                    if (Convert.ToDecimal(row.Cells[5].Value) >= (decimal)1)
                    {
                        row.Cells[5].Style.BackColor = Color.FromArgb(0, 176, 80);
                        row.Cells[5].Style.ForeColor = Color.Black;

                    }
                    else if (Convert.ToDecimal(row.Cells[5].Value) >= (decimal)0.85
                        && Convert.ToDecimal(row.Cells[5].Value) < (decimal)1)
                    {
                        row.Cells[5].Style.BackColor = Color.FromArgb(255, 255, 0);
                        row.Cells[5].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[5].Value) < (decimal)0.85)
                    {
                        row.Cells[5].Style.BackColor = Color.FromArgb(255, 0, 0);
                        row.Cells[5].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void frmTractoZone_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void frmTractoZone_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }


    }
}
