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

namespace Ventas
{
    public partial class CostoBaseOriginal : Form
    {
        #region PARAMETROS
        public string Mes;
        Clases.Logs log;
        public string Año;
        public int total;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);

        public enum ColumnasGrid
        {
            Codigo, NombreArticulo, Precio, Moneda
        }
        #endregion

        #region METODOS
        public CostoBaseOriginal()
        {
            InitializeComponent();
        }

        public void FormatoGrid(){
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Codigo].Width = 100;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.NombreArticulo].Width = 310;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].Width = 100;
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Precio].DefaultCellStyle.Format = "C4"; 
            dgvCostosOriginales.Columns[(int)ColumnasGrid.Moneda].Width = 100;
        }


        #endregion 

        #region EVENTOS
        private void CostoBaseOriginal_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            this.MaximizeBox = false;
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription,0);
            cmbYear.Text = DateTime.Now.Year.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = true;
            lblStatus.Text = "";
            Esperar();
            cargar();            
            Continuar();
            FormatoGrid();
        }
        #endregion

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(cmbMes.Text))
            {
                if (!String.IsNullOrEmpty(cmbYear.Text))
                {
                    Mes = cmbMes.Text;
                    Año = cmbYear.Text;
                    total = dgvCostosOriginales.Rows.Count;
                    DialogResult dialogResult = MessageBox.Show("Esta apunto de respaldar "+ dgvCostosOriginales.Rows.Count +" registros\r\n¿Desea continuar?", "Alerta", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Thread tinsertar  = new Thread(insertarRegistros);
                        tinsertar.Start();
                        tinsertar.Join();
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                else
                    MessageBox.Show("Seleccione un año de la lista.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Seleccione un mes de la lista.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }


        private void insertarRegistros()
        {
            //Mes = cmbMes.Invoke((MethodInvoker)(() => cmbMes.Text));
            int registros = 0;

            try
            {
                string progres = "";
                conection.Open();
                foreach (DataGridViewRow row in dgvCostosOriginales.Rows)
                {
                    string ItemCode = "";
                    decimal Precio = 0;
                    string Currency = "";
                    //btnGuardar.Enabled = false;
                    //btnCargar.Enabled = false;
                    ItemCode = Convert.ToString(row.Cells[(int)ColumnasGrid.Codigo].Value);
                    Precio = Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Precio].Value);
                    Currency = Convert.ToString(row.Cells[(int)ColumnasGrid.Moneda].Value);
                    SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 13);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@Lineas", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", ItemCode);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                    command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@GranCanales", string.Empty);
                    command.Parameters.AddWithValue("@Canales", string.Empty);
                    command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Mes", getMes(Mes));
                    command.Parameters.AddWithValue("@Anio", Año.Trim());
                    command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                    command.Parameters.AddWithValue("@Precio", Precio);
                    command.Parameters.AddWithValue("@Moneda", Currency);
                    command.CommandTimeout = 0;
                    lblStatus.Text = (total - registros) + " restantes de " + total;
                    command.ExecuteNonQuery();
                    registros++;
                    if (progres.Length == 200)
                        progres = "";
                    progres += ".";
                    lblProgress.Text = progres;
                }
                lblStatus.Text = "Listo...";
                // btnCargar.Enabled = true;
                // btnGuardar.Enabled = true;
                lblProgress.Text = "";
            }
            catch (Exception)
            {
            }
            finally
            {

                conection.Close();
            }
        }
        private void insertarRegistrosFaltantes()
        {

            //Mes = cmbMes.Invoke((MethodInvoker)(() => cmbMes.Text));
            int registros = 0;

            try
            {
                string progres = "";
                conection.Open();
                foreach (DataGridViewRow row in dgvCostosOriginales.Rows)
                {
                    string ItemCode = "";
                    decimal Precio = 0;
                    string Currency = "";
                    //btnGuardar.Enabled = false;
                    //btnCargar.Enabled = false;
                    ItemCode = Convert.ToString(row.Cells[(int)ColumnasGrid.Codigo].Value);
                    Precio = Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Precio].Value);
                    Currency = Convert.ToString(row.Cells[(int)ColumnasGrid.Moneda].Value);
                    SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 13);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@Lineas", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", ItemCode);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                    command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@GranCanales", string.Empty);
                    command.Parameters.AddWithValue("@Canales", string.Empty);
                    command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Mes", DateTime.Now.Month);
                    command.Parameters.AddWithValue("@Anio", DateTime.Now.Year);
                    command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                    command.Parameters.AddWithValue("@Precio", Precio);
                    command.Parameters.AddWithValue("@Moneda", Currency);
                    command.CommandTimeout = 0;
                    lblStatus.Text = (total - registros) + " restantes de " + total;
                    command.ExecuteNonQuery();
                    registros++;
                    if (progres.Length == 200)
                        progres = "";
                    progres += ".";
                    lblProgress.Text = progres;
                }
                lblStatus.Text = "Listo...";
                // btnCargar.Enabled = true;
                // btnGuardar.Enabled = true;
                lblProgress.Text = "";
            }
            catch (Exception)
            {
            }
            finally
            {

                conection.Close();
            }
        }
        private void cargar()
        {
            SqlCommand command = new SqlCommand("PJ_Ventas", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 12);
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
            command.Parameters.AddWithValue("@Precio", 0);
            command.Parameters.AddWithValue("@Moneda", string.Empty);
            command.CommandTimeout = 0;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            dgvCostosOriginales.DataSource = table;
        }
        private void Esperar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
            dgvCostosOriginales.Cursor = Cursors.Arrow;
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }
        private void eliminarRegistros()
        {
            int registros = 0;
            try
            {
                string progres = "";
                conection.Open();
                foreach (DataGridViewRow row in dgvCostosOriginales.Rows)
                {
                    string ItemCode = "";
                    ItemCode = Convert.ToString(row.Cells[(int)ColumnasGrid.Codigo].Value);
                    SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 16);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@Lineas", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", ItemCode);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                    command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@GranCanales", string.Empty);
                    command.Parameters.AddWithValue("@Canales", string.Empty);
                    command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Mes", getMes(Mes).ToString());
                    command.Parameters.AddWithValue("@Anio", Año.Trim());
                    command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                    command.Parameters.AddWithValue("@Precio", 0);
                    command.Parameters.AddWithValue("@Moneda", string.Empty);
                    command.CommandTimeout = 0;
                    lblStatus.Text = (total - registros) + " restantes de " + total;
                    command.ExecuteNonQuery();
                    registros++;
                    if (progres.Length == 200)
                        progres = "";
                    progres += ".";
                    lblProgress.Text = progres;
                }
                lblStatus.Text = "Listo...";
                lblProgress.Text = "";
            }
            catch (Exception)
            {
            }
            finally
            {

                conection.Close();
            }
        }

        public int getMes(string mes)
        {
            int auxmes = 0;
            switch (mes)
            {
                case "Enero":
                    auxmes = 1;
                    break;
                case "Febrero":
                    auxmes = 2;
                    break;
                case "Marzo":
                    auxmes = 3;
                    break;
                case "Abril":
                    auxmes = 4;
                    break;
                case "Mayo":
                    auxmes = 5;
                    break;
                case "Junio":
                    auxmes = 6;
                    break;
                case "Julio":
                    auxmes = 7;
                    break;
                case "Agosto":
                    auxmes = 8;
                    break;
                case "Septiembre":
                    auxmes = 9;
                    break;
                case "Octubre":
                    auxmes = 10;
                    break;
                case "Novoviembre":
                    auxmes = 11;
                    break;
                case "Diciembre":
                    auxmes = 12;
                    break;
            }
            return auxmes;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(cmbMes.Text))
            {
                if (!String.IsNullOrEmpty(cmbYear.Text))
                {
                    Mes = cmbMes.Text;
                    Año = cmbYear.Text;
                    total = dgvCostosOriginales.Rows.Count;
                    DialogResult dialogResult = MessageBox.Show("Esta apunto de eliminar" + dgvCostosOriginales.Rows.Count + " registros\r\n¿Desea continuar?", "Alerta", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                       Thread t = new Thread(eliminarRegistros);
                        t.Start();
                        t.Join();
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                else
                    MessageBox.Show("Seleccione un año de la lista.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Seleccione un mes de la lista.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 23);
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
                command.Parameters.AddWithValue("@Precio", 0);
                command.Parameters.AddWithValue("@Moneda", string.Empty);
                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dgvCostosOriginales.DataSource = table;
                FormatoGrid();
                if (dgvCostosOriginales.Rows.Count == 0)
                    MessageBox.Show("No hay artículos faltantes", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Esta apunto de respaldar "+ dgvCostosOriginales.Rows.Count +" registros\r\n¿Desea continuar?", "Alerta", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        total = dgvCostosOriginales.Rows.Count;
                        Thread t = new Thread(insertarRegistrosFaltantes);
                        t.Start();
                        t.Join();
                    }
                }
                
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

        private void CostoBaseOriginal_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
                        
        }

        private void CostoBaseOriginal_FormClosing(object sender, FormClosingEventArgs e)
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
