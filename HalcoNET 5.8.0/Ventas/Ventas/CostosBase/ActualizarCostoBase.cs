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
    public partial class ActualizarCostoBase : Form
    {
        #region PARAMETROS
        Clases.Logs log;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        public string Articulo;
        public string Mes;
        public string Año;
        public decimal  Precio;

        public enum ColumnasGrid
        {
            Linea, Articulo, NombreArticulo, CostoActual, Mes, Año
        }
        #endregion
        public ActualizarCostoBase()
        {
            InitializeComponent();
        }


        #region EVENTOS

        

        private void ActualizarCostoBase_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.CargarArticulos();
            txtAño.Text = DateTime.Now.Year.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtArticulo.Text))
            {
                Articulo = txtArticulo.Text;
                if (!string.IsNullOrEmpty(clbMeses.Text))
                {
                    Mes = GetMes(clbMeses.Text);
                    if (!string.IsNullOrEmpty(txtAño.Text))
                    {
                        Año = txtAño.Text;
                        SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 19);
                        command.Parameters.AddWithValue("@Vendedores", string.Empty);
                        command.Parameters.AddWithValue("@Lineas", string.Empty);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);
                        command.Parameters.AddWithValue("@Articulo", Articulo);
                        command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                        command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                        command.Parameters.AddWithValue("@Factura", string.Empty);
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@GranCanales", string.Empty);
                        command.Parameters.AddWithValue("@Canales", string.Empty);
                        command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                        command.Parameters.AddWithValue("@Mes", Mes);
                        command.Parameters.AddWithValue("@Anio", Año);
                        command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                        command.Parameters.AddWithValue("@Precio", ClasesSGUV.Propiedades.RolesHalcoNET.Administrador);
                        command.Parameters.AddWithValue("@Moneda", string.Empty);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        dgvArticulo.DataSource = table;
                        if (dgvArticulo.Rows.Count > 0)
                        {
                            label2.Enabled = true;
                            Actualizar.Enabled = true;
                            txtNuevo.Enabled = true;
                            FormatoGrid();
                        }
                        else
                        {
                            MessageBox.Show("No se encontro el artículo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            label2.Enabled = false;
                            Actualizar.Enabled = false;
                            txtNuevo.Enabled = false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Ingese un año.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtAño.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un mes.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    clbMeses.Focus();
                }
            }
            else
            {
                MessageBox.Show("Ingese un artículo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtArticulo.Focus();
            }
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNuevo.Text))
                {
                    Precio = decimal.Parse(txtNuevo.Text);
                    SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 20);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@Lineas", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Articulo", Articulo);
                    command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                    command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@GranCanales", string.Empty);
                    command.Parameters.AddWithValue("@Canales", string.Empty);
                    command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Mes", Mes);
                    command.Parameters.AddWithValue("@Anio", Año);
                    command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                    command.Parameters.AddWithValue("@Precio", Precio);
                    command.Parameters.AddWithValue("@Moneda", string.Empty);
                    conection.Open();

                    command.ExecuteNonQuery();
                    MessageBox.Show("El costo se actualizo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //button1_Click(sender, e);
                    txtArticulo.Clear();
                    clbMeses.Text = "";
                    txtAño.Clear();
                    dgvArticulo.DataSource = null;

                    txtNuevo.Enabled = false;
                    Actualizar.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad. ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNuevo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conection.Close();
            }

        }

        #endregion

        #region METODOS
        private void CargarArticulos()
        {
            SqlCommand command = new SqlCommand("PJ_Ventas", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 14);
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
            command.Parameters.AddWithValue("@Precio", ClasesSGUV.Propiedades.RolesHalcoNET.Administrador);
            command.Parameters.AddWithValue("@Moneda", string.Empty);
            try
            {
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                //var stringArr = table.Rows[0].ItemArray.Select(x => x.ToString()).ToArray();
                //txtArticulo.AutoCompleteCustomSource =  
                var source = new AutoCompleteStringCollection();
                source.AddRange(table.Rows[0].ItemArray.Select(x => x.ToString()).ToArray());
                source.AddRange(Array.ConvertAll<DataRow, String>(table.Select(), delegate(DataRow row) { return (String)row[0]; }));
                txtArticulo.AutoCompleteCustomSource = source;
                txtArticulo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception)
            {
            }
        }

        private string GetMes(string mes)
        {
           switch (mes)
            {
                case "Enero":
                    Mes = "1";
                    break;
                case "Febrero":
                    Mes = "2";
                    break;
                case "Marzo":
                    Mes = "3";
                    break;
                case "Abril":
                    Mes = "4";
                    break;
                case "Mayo":
                    Mes = "5";
                    break;
                case "Junio":
                    Mes = "6";
                    break;
                case "Julio":
                    Mes = "7";
                    break;
                case "Agosto":
                    Mes = "8";
                    break;
                case "Septiembre":
                    Mes = "9";
                    break;
                case "Octubre":
                    Mes = "10";
                    break;
                case "Noviembre":
                    Mes = "11";
                    break;
                case "Diciembre":
                    Mes = "12";
                    break;
            }
           return Mes;
        }

        public void FormatoGrid()
        {
            dgvArticulo.Columns[(int)ColumnasGrid.Linea].Width = 90;
            dgvArticulo.Columns[(int)ColumnasGrid.Articulo].Width = 150;
            dgvArticulo.Columns[(int)ColumnasGrid.NombreArticulo].Width = 223;
            dgvArticulo.Columns[(int)ColumnasGrid.CostoActual].Width = 80;
            dgvArticulo.Columns[(int)ColumnasGrid.Mes].Visible = false;
            dgvArticulo.Columns[(int)ColumnasGrid.Año].Visible = false;

            dgvArticulo.Columns[(int)ColumnasGrid.CostoActual].DefaultCellStyle.Format = "C4";
        }

        #endregion

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                this.button1_Click(sender, e);
            }
        }

        private void txt_KeyPress1(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                this.Actualizar_Click(sender, e);
            }
        }

        private void ActualizarCostoBase_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
                        
        }

        private void ActualizarCostoBase_FormClosing(object sender, FormClosingEventArgs e)
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
