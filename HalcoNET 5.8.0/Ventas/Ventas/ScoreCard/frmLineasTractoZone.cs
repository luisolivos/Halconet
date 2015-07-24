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
using System.Threading;

namespace Ventas.Ventas.ScoreCard
{
    public partial class LineasTractozone : Form
    {
        Clases.Logs log;
        public LineasTractozone(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            RolUsuario = rolUsuario;
            NombreUsuario = nombreUsuario;
            CodigoVendedor = codigoVendedor;
            Sucursal = sucursal;
            InitializeComponent();

        }

        #region PARÁMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

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

        /// <summary>
        /// Enumerador de las columnas del Grid
        /// </summary>
        private enum ColumnasGrid
        {
            SlpCode, Vendedor, Sucursal, Linea, Objetivo, Venta, VentavsCuota, Porvender, TendenciaFinMesM, TendenciaFinMesP, Orden, Boton
        }

        private enum TipoConsulta
        {
            DiasMes = 1,
            DiasTranscurridos = 2,
            LineasObjetivo = 9
        }
        #endregion        

        #region EVENTOS
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarAExcel ex = new ExportarAExcel();
                ex.ExportarCobranza(gridLineas);
                MessageBox.Show("El archivo se creo con exito", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            Fecha = DateTime.Parse(dateTimePicker1.Text);
            //if (Mes != Fecha.Month)
            //{
            //    Mes = Fecha.Month;
            //    CargarLinea();
            //}
            Esperar();
            CargarDias();
            CargarDiasReales();
            //CargarLinea();

            Sucursales = CrearCadena(clbSucursal);
            if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                Vendedores = CrearCadena(clbVendedor);
            }
            try
            {
                gridLineas.DataSource = null;
                gridTotales.DataSource = null;
                gridLineas.Columns.Clear();

                SqlCommand commandLienasMoradas = new SqlCommand("PJ_ScoreCardEfectividad", conection);
                commandLienasMoradas.CommandType = CommandType.StoredProcedure;
                if (Convert.ToInt32(cbLinea.SelectedValue) == 135)
                {
                    commandLienasMoradas.Parameters.AddWithValue("@TipoConsulta", 20);
                } 
                if (Convert.ToInt32(cbLinea.SelectedValue) == 159)
                {
                    commandLienasMoradas.Parameters.AddWithValue("@TipoConsulta", 21);
                }
                if (Convert.ToInt32(cbLinea.SelectedValue) == 241)
                {
                    commandLienasMoradas.Parameters.AddWithValue("@TipoConsulta", 22);
                }
                commandLienasMoradas.Parameters.AddWithValue("@Cliente", string.Empty);
                commandLienasMoradas.Parameters.AddWithValue("@Sucursales", Sucursales);
                commandLienasMoradas.Parameters.AddWithValue("@Vendedores", Vendedores);
                commandLienasMoradas.Parameters.AddWithValue("@Presupuesto", 0);
                commandLienasMoradas.Parameters.AddWithValue("@Fecha", Fecha);
                FechaOK = Fecha;
                commandLienasMoradas.CommandTimeout = 0;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = commandLienasMoradas;
                adapter.Fill(table);
                gridLineas.DataSource = table;

                DarFormatoGrid();

                decimal Objetivo = 0;
                decimal Venta = 0;
                decimal VentavsCuota = 0;
                decimal PorVender = 0;
                decimal PronosticoFinMesM = 0;
                decimal PronosticoFinMesP = 0;

                foreach (DataGridViewRow item in gridLineas.Rows)
                {
                    Objetivo += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Objetivo].Value);
                    Venta += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Venta].Value);
                    PorVender += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Porvender].Value);
                    PronosticoFinMesM += Convert.ToDecimal(item.Cells[(int)ColumnasGrid.TendenciaFinMesM].Value);
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

                
                if (Convert.ToInt32(cbLinea.SelectedValue) == 201 || Convert.ToInt32(cbLinea.SelectedValue) == 135 || Convert.ToInt32(cbLinea.SelectedValue) == 241)
                {
                    gridTotales.Columns[0].DefaultCellStyle.Format = "C0";
                    gridTotales.Columns[1].DefaultCellStyle.Format = "C0";
                    gridTotales.Columns[2].DefaultCellStyle.Format = "P2";
                    gridTotales.Columns[3].DefaultCellStyle.Format = "C0";
                    gridTotales.Columns[4].DefaultCellStyle.Format = "C0";
                    gridTotales.Columns[5].DefaultCellStyle.Format = "P2";
                }
                if (Convert.ToInt32(cbLinea.SelectedValue) == 242 || Convert.ToInt32(cbLinea.SelectedValue) == 159)
                {
                    gridTotales.Columns[0].DefaultCellStyle.Format = "C0";
                    gridTotales.Columns[1].DefaultCellStyle.Format = "C0";
                    gridTotales.Columns[2].DefaultCellStyle.Format = "P2";
                    gridTotales.Columns[3].DefaultCellStyle.Format = "C0";
                    gridTotales.Columns[4].DefaultCellStyle.Format = "C0";
                    gridTotales.Columns[5].DefaultCellStyle.Format = "P2";
                }

                
                if (Convert.ToInt32(cbLinea.SelectedValue) == 242 || Convert.ToInt32(cbLinea.SelectedValue) == 159 )
                {
                   // gridTotales.Columns["Tendencia fin mes($)"].HeaderText = "Tendencia fin mes(PZ)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
            }
        }
        #endregion 

        

        #region METODOS

        public void Restricciones()
        {
            //Rol Vendedor
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                //ocultat sucursalesa
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                //Point l1 = new Point(label10.Location.X, label10.Location.Y);
                //Point c1 = new Point(clbLinea.Location.X, clbLinea.Location.Y);

                //Point l2 = new Point(lblSucursal.Location.X, lblSucursal.Location.Y);
                //Point c2 = new Point(clbSucursal.Location.X, clbSucursal.Location.Y);

                //Point l3 = new Point(lblVendedor.Location.X, lblVendedor.Location.Y);
                //Point c3 = new Point(clbVendedor.Location.X, clbVendedor.Location.Y);

                //lblVendedor.Location = l1;
                //clbVendedor.Location = c1;

                //label10.Location = l2;
                //clbLinea.Location = c2;

                //lblCanal.Location = l3;
                //clbCanal.Location = c3;
            }
            //Rol Ventas Especial
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {

                //label10.Location = new Point(lblSucursal.Location.X, lblSucursal.Location.Y);
                //clbLinea.Location = new Point(clbSucursal.Location.X, clbSucursal.Location.Y);

                //lblCanal.Location = new Point(label10.Location.X, label10.Location.Y);
                //clbCanal.Location = new Point(clbLinea.Location.X, clbLinea.Location.Y);

                lblVendedor.Visible = false;
                clbVendedor.Visible = false;
                clbSucursal.Visible = false;
                lblSucursal.Visible = false;
                Vendedores = "," + CodigoVendedor.ToString();
            }

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

        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
        /// </sumary>
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

        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
        /// </sumary>
        private void DarFormatoGrid()
        {
            gridLineas.Columns[(int)ColumnasGrid.SlpCode].Visible = false;
            gridLineas.Columns[(int)ColumnasGrid.Orden].Visible = false;

            gridLineas.Columns[(int)ColumnasGrid.Vendedor].Width = 180;
            gridLineas.Columns[(int)ColumnasGrid.Sucursal].Width = 100;
            gridLineas.Columns[(int)ColumnasGrid.Linea].Width = 100;
            gridLineas.Columns[(int)ColumnasGrid.Objetivo].Width = 100;
            gridLineas.Columns[(int)ColumnasGrid.Venta].Width = 100;
            gridLineas.Columns[(int)ColumnasGrid.VentavsCuota].Width = 100;
            gridLineas.Columns[(int)ColumnasGrid.Porvender].Width = 100;
            gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesM].Width = 100;
            gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesP].Width = 100;

            if (Convert.ToInt32(cbLinea.SelectedValue) == 201 || Convert.ToInt32(cbLinea.SelectedValue) == 135 || Convert.ToInt32(cbLinea.SelectedValue) == 241)
            {
                gridLineas.Columns[(int)ColumnasGrid.Objetivo].DefaultCellStyle.Format = "C0";
                gridLineas.Columns[(int)ColumnasGrid.Venta].DefaultCellStyle.Format = "C0";
                gridLineas.Columns[(int)ColumnasGrid.VentavsCuota].DefaultCellStyle.Format = "P2";
                gridLineas.Columns[(int)ColumnasGrid.Porvender].DefaultCellStyle.Format = "C0";
                gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesM].DefaultCellStyle.Format = "C0";
                gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesP].DefaultCellStyle.Format = "P2";
            }
            if (Convert.ToInt32(cbLinea.SelectedValue) == 242 || Convert.ToInt32(cbLinea.SelectedValue) == 159)
            {
                gridLineas.Columns[(int)ColumnasGrid.Objetivo].DefaultCellStyle.Format = "C0";
                gridLineas.Columns[(int)ColumnasGrid.Venta].DefaultCellStyle.Format = "C0";
                gridLineas.Columns[(int)ColumnasGrid.VentavsCuota].DefaultCellStyle.Format = "P2";
                gridLineas.Columns[(int)ColumnasGrid.Porvender].DefaultCellStyle.Format = "C0";
                gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesM].DefaultCellStyle.Format = "C0";
                gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesP].DefaultCellStyle.Format = "P2";
            }
            gridLineas.Columns[(int)ColumnasGrid.Objetivo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGrid.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGrid.VentavsCuota].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGrid.Porvender].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            gridLineas.Columns[(int)ColumnasGrid.Objetivo].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 204);
            gridLineas.Columns[(int)ColumnasGrid.TendenciaFinMesM].DefaultCellStyle.BackColor = Color.FromArgb(252, 213, 180);
        }

        ///<sumary>
        /// Método que llena los TextBox Dias
        /// </sumary>
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
        #endregion

        private void LineaObjetivo_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                Restricciones();
                
                CargarSucursales();
                CargarVendedores();
                CargarLinea();

                dateTimePicker1.Value = DateTime.Now;
                gridLineas.DataSource = null;
                gridLineas.Columns.Clear();
                gridTotales.DataSource = null;

                dateTimePicker1.Value = DateTime.Now;
                txtAvanceOptimo.Clear();
                txtDiasMes.Clear();
                txtDiasRestantes.Clear();
                txtDiasTranscurridos.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void U_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                btnPresentar_Click(sender, e);
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                LineaObjetivo_Load(sender, e);
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el checkedlistbox
        /// Selecciona todas o deselecciona todas
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void clbVendedor_Click(object sender, EventArgs e)
        {
            if (clbVendedor.SelectedIndex == 0)
            {
                if (clbVendedor.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbVendedor.Items.Count; item++)
                    {
                        clbVendedor.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbVendedor.Items.Count; item++)
                    {
                        clbVendedor.SetItemChecked(item, true);
                    }
                }
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el checkedlistbox
        /// Selecciona todas o deselecciona todas
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
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

        private void CargarLinea()
        {
            try
            {
                conection.Open();
                Fecha = dateTimePicker1.Value;
                Mes = Fecha.Month;

                SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                commandVendedor.CommandType = CommandType.StoredProcedure;
                commandVendedor.Parameters.AddWithValue("@TipoConsulta", 8);
                commandVendedor.Parameters.AddWithValue("@SlpCode", 0);
                commandVendedor.Parameters.AddWithValue("@Fecha", DateTime.Now);
                commandVendedor.Parameters.AddWithValue("@Bono", Fecha.Month);
                commandVendedor.Parameters.AddWithValue("@From", "TRZ");
                commandVendedor.Parameters.AddWithValue("@Mensaje", string.Empty);

                DataTable table = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = commandVendedor;
                da.Fill(table);

                cbLinea.DataSource = table;
                cbLinea.DisplayMember = "Nombre";
                cbLinea.ValueMember = "Codigo";
            }
            catch (Exception)
            {
            }
            finally
            {
                conection.Close();
            }
        }

        private void form_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                cbLinea.DataSource = null;
                cbLinea.Text = string.Empty;
                this.CargarLinea();
            }
            catch (Exception)
            {

                // throw;
            }
        }

        private void gridLineas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.VentavsCuota].Value) >= (decimal)1)
                    {
                        row.Cells[(int)ColumnasGrid.VentavsCuota].Style.BackColor = Color.FromArgb(0, 176, 80);
                        row.Cells[(int)ColumnasGrid.VentavsCuota].Style.ForeColor = Color.Black;

                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.VentavsCuota].Value) >= (decimal)0.85
                        && Convert.ToDecimal(row.Cells[(int)ColumnasGrid.VentavsCuota].Value) < (decimal)1)
                    {
                        row.Cells[(int)ColumnasGrid.VentavsCuota].Style.BackColor = Color.FromArgb(255, 255, 0);
                        row.Cells[(int)ColumnasGrid.VentavsCuota].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.VentavsCuota].Value) < (decimal)0.85)
                    {
                        row.Cells[(int)ColumnasGrid.VentavsCuota].Style.BackColor = Color.FromArgb(255, 0, 0);
                        row.Cells[(int)ColumnasGrid.VentavsCuota].Style.ForeColor = Color.White;
                    }

                    if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Value) >= (decimal)1)
                    {
                        row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Style.BackColor = Color.FromArgb(0, 176, 80);
                        row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Style.ForeColor = Color.Black;

                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Value) >= (decimal)0.85
                        && Convert.ToDecimal(row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Value) < (decimal)1)
                    {
                        row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Style.BackColor = Color.FromArgb(255, 255, 0);
                        row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Style.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Value) < (decimal)0.85)
                    {
                        row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Style.BackColor = Color.FromArgb(255, 0, 0);
                        row.Cells[(int)ColumnasGrid.TendenciaFinMesP].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {

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
    }
}
