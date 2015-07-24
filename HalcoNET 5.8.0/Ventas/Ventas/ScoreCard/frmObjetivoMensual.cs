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
    public partial class ObjetivoMensual : Form
    {

        Clases.Logs log;
        #region PARAMETROS
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
        public enum TipoConsulta
        {
            ConsultaVendedores = 1
        }

        public enum ColumnasGrid
        {
            ID = 0,
            Codigo = 1,
            Nombre = 2,
            Sucursal = 3,
            Mes = 4,
            Year = 5,
            Objetivo = 6
        }

        public SqlConnection conectionSGUV = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        public SqlConnection conectionPJ = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        public int ID;
        public string Codigo;
        public string NombreVendedor;
        public string Sucursal;
        public int Mes;
        public int Year;
        public double Objetivo;

        #endregion


        #region METODOS
        private DataTable CrearTabla(DataTable t)
        {
            t.Columns.Add("ID", typeof(int));
            t.Columns.Add("Codigo", typeof(string));
            t.Columns.Add("Nombre", typeof(string));
            t.Columns.Add("Sucursal", typeof(string));
            t.Columns.Add("Mes", typeof(int));
            t.Columns.Add("Year", typeof(int));
            t.Columns.Add("Objetivo", typeof(double));
            return t;
        }

        private void DarFormatoGrid()
        {
            dgObjetivoMes.Columns[(int)ColumnasGrid.ID].Width = 1;
            dgObjetivoMes.Columns[(int)ColumnasGrid.ID].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Codigo].Width = 80;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Codigo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Nombre].Width = 250;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Nombre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Sucursal].Width = 80;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Sucursal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Mes].Width = 80;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Mes].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Year].Width = 80;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Year].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Year].HeaderText = "Año";
            dgObjetivoMes.Columns[(int)ColumnasGrid.Objetivo].Width = 90;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Objetivo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Objetivo].DefaultCellStyle.Format = "C0";

            dgObjetivoMes.Columns[(int)ColumnasGrid.ID].ReadOnly = true;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Codigo].ReadOnly = true;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Nombre].ReadOnly = true;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Mes].ReadOnly = false;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Year].ReadOnly = false;
            dgObjetivoMes.Columns[(int)ColumnasGrid.Objetivo].ReadOnly = false;
        }
        #endregion

        #region EVENTOS
        public ObjetivoMensual()
        {
            InitializeComponent();
        }

        private void ObjetivoMensual_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            cmbYear.Text = DateTime.Now.Year.ToString();
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                Esperar();               
                conectionSGUV.Open();
                foreach (DataGridViewRow item in dgObjetivoMes.Rows)
                {
                    string mes = "";
                    string year = "";
                    SqlCommand command2 = new SqlCommand("ObjetivosVentas", conectionSGUV);
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaObjetivos.Insertar);
                    string auxSucursal = "";
                    try
                    {

                        Codigo = Convert.ToString(item.Cells[(int)ColumnasGrid.Codigo].Value);
                        NombreVendedor = Convert.ToString(item.Cells[(int)ColumnasGrid.Nombre].Value);
                        mes = Convert.ToString(item.Cells[(int)ColumnasGrid.Mes].Value);
                        year = Convert.ToString(item.Cells[(int)ColumnasGrid.Year].Value);
                        try
                        {
                            Objetivo = Convert.ToDouble(item.Cells[(int)ColumnasGrid.Objetivo].Value);
                        }
                        catch (Exception)
                        {
                            Objetivo = 0;
                        }
                        auxSucursal = Convert.ToString(item.Cells[(int)ColumnasGrid.Sucursal].Value);
                        ID = Convert.ToInt32(item.Cells[(int)ColumnasGrid.ID].Value);

                    }
                    catch (Exception)
                    {
                        ID = 0;
                    }

                    switch (auxSucursal)
                    {
                        case "PUEBLA": Sucursal = "01"; break;
                        case "MTY": Sucursal = "02"; break;
                        case "APIZACO": Sucursal = "03"; break;
                        case "CORDOBA": Sucursal = "05"; break;
                        case "TEPEACA": Sucursal = "06"; break;
                        case "EDOMEX": Sucursal = "16"; break;
                        case "GDL": Sucursal = "18"; break;
                    }

                    command2.Parameters.AddWithValue("@ID", ID);
                    command2.Parameters.AddWithValue("@Codigo", Codigo);
                    command2.Parameters.AddWithValue("@NombreVendedor", NombreVendedor);
                    command2.Parameters.AddWithValue("@Sucursal", Sucursal);
                    command2.Parameters.AddWithValue("@Mes", mes);
                    command2.Parameters.AddWithValue("@Year", year);
                    command2.Parameters.AddWithValue("@Objetivo", Objetivo);
                    command2.ExecuteNonQuery();
                }
                btnCargar_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conectionSGUV.Close();
                Continuar();
            }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            dgObjetivoMes.Columns.Clear();
            dgObjetivoMes.DataSource = null;
            dgObjetivoMes.Columns.Clear();
            dgObjetivoMes.DataSource = null;

            try
            {
                Mes = cmbMes.SelectedIndex + 1;
                Year = Int32.Parse(cmbYear.Text.ToString());


                DataTable table = new DataTable();
                table = this.CrearTabla(table);

                SqlCommand command = new SqlCommand("ObjetivosVentas", conectionSGUV);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("ID", 0);
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaObjetivos.Consultar);
                command.Parameters.AddWithValue("@Codigo", String.Empty);
                command.Parameters.AddWithValue("@NombreVendedor", String.Empty);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Mes", Mes);
                command.Parameters.AddWithValue("@Year", Year);
                command.Parameters.AddWithValue("@Objetivo", 0);
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count != 0)
                {
                    dgObjetivoMes.DataSource = table;
                    this.DarFormatoGrid();
                    button1.Visible = true;
                }
                else
                {
                    string mensaje = "No se han guardado los objetivos para " + cmbMes.Text + " de " + cmbYear.Text + "\r\n ¿Desea guardalos ahora?";
                    DialogResult dialog = MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialog == DialogResult.Yes)
                    {
                        CargarDatos();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecciona Mes y Año.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            
        }

        public void CargarDatos()
        {
            try
            {
                Esperar();
                dgObjetivoMes.Columns.Clear();
                dgObjetivoMes.DataSource = null;
                dgObjetivoMes.Columns.Clear();
                dgObjetivoMes.DataSource = null;

                DataTable table = new DataTable();
                table = this.CrearTabla(table);



                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@SlpCode", 0);
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dgObjetivoMes.DataSource = table;

                foreach (DataGridViewRow item in dgObjetivoMes.Rows)
                {

                    item.Cells[(int)ColumnasGrid.Mes].Value = cmbMes.SelectedIndex + 1;
                    item.Cells[(int)ColumnasGrid.Year].Value = Int32.Parse(cmbYear.Text.ToString());

                }

                this.DarFormatoGrid();
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Esperar();
                dgObjetivoMes.Columns.Clear();
                dgObjetivoMes.DataSource = null;
                dgObjetivoMes.Columns.Clear();
                dgObjetivoMes.DataSource = null;

                DataTable table = new DataTable();
                table = this.CrearTabla(table);



                SqlCommand command = new SqlCommand("ObjetivosVentas", conectionSGUV);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaSGUV.Eliminar);
                command.Parameters.AddWithValue("@ID", 0);
                command.Parameters.AddWithValue("@Codigo", string.Empty);
                command.Parameters.AddWithValue("@NombreVendedor", string.Empty);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Mes", 0);
                command.Parameters.AddWithValue("@Year", 0);
                command.Parameters.AddWithValue("@Objetivo", 0);
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dgObjetivoMes.DataSource = table;

                foreach (DataGridViewRow item in dgObjetivoMes.Rows)
                {

                    item.Cells[(int)ColumnasGrid.Mes].Value = DateTime.Now.Month;
                    item.Cells[(int)ColumnasGrid.Year].Value = DateTime.Now.Year; ;

                }

                this.DarFormatoGrid();
                if (table.Rows.Count == 0)
                {
                    MessageBox.Show("No hay vendedores faltantes.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
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
    }
}
