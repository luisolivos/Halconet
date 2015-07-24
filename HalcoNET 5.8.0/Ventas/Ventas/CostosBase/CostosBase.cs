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
    public partial class ConsultarCostosBase : Form
    {

        #region PARÁMETROS
        Clases.Logs log;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);

        public string Articulo;
        public string Linea;
        public string Mes;
        public string Anio;
        public string NombreArticulo;
        public decimal Precio;

        public enum ColumnasGrid
        {
            Linea = 0,
            Articulo = 1,
            NombreArticulo = 2,
            Precio = 3
        }

        #endregion


        #region EVENTOS

        public ConsultarCostosBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que ocurre al cargarse el form
        /// Craga los meses
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void CostosBase_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                CargarMeses();
                CargarLinea();
                CargarArticulos();
                CargarClientes();

                clbMeses.SelectedIndex = DateTime.Now.Month - 1;
                txtAnio.Text = DateTime.Now.Year.ToString(); ;
            }            
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnCargar
        /// Carga el Articulo especificado en el grid
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnCargar_Click_1(object sender, EventArgs e)
        {
            try
            {
                Articulo = txtArticulo.Text;
                //Linea = txtLinea.Text;

                SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaPJ.CargarArticulo);
                command.Parameters.AddWithValue("@Vendedores", string.Empty);
                command.Parameters.AddWithValue("@Lineas", Linea);
                command.Parameters.AddWithValue("@Cliente", string.Empty);
                command.Parameters.AddWithValue("@Articulo", Articulo);
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
                gridListaPrecios.DataSource = table;
                DarFormatoGrid();

                if (gridListaPrecios.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el Artículo en la Base de Datos. Verifique el texto.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnGuardar
        /// Guarda en la BD el nuevo precio del artículo
        /// </summary>
        /// <param name="sender">OBjeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string cliente = null;
                Articulo = txtArticulo.Text;// row.Cells[(int)ColumnasGrid.Articulo].Value.ToString();
                NombreArticulo = cbLineas.SelectedValue.ToString();// row.Cells[(int)ColumnasGrid.NombreArticulo].Value.ToString();
                if (String.IsNullOrEmpty(Articulo) && NombreArticulo == "0")
                {
                    MessageBox.Show("Llene el campo 'Artículo' o 'Línea' para poder continuar.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (String.IsNullOrEmpty(Articulo) && NombreArticulo != "0" || !String.IsNullOrEmpty(Articulo) && NombreArticulo == "0")
                {
                    
                    if (!String.IsNullOrEmpty(txtDescuento.Text))
                    {
                        if (!String.IsNullOrEmpty(txtAnio.Text))
                        {
                            switch (clbMeses.SelectedItem.ToString())
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
                            if (String.IsNullOrEmpty(Articulo))
                                Articulo = string.Empty;
                            if (NombreArticulo == "0")
                                NombreArticulo = string.Empty;
                            if (String.IsNullOrEmpty(txtCliente.Text))
                                cliente = string.Empty;
                            else
                                cliente = txtCliente.Text;
                            Anio = txtAnio.Text;  
                            //// inicia procedimiento

                            Precio = decimal.Parse(txtDescuento.Text);// Convert.ToDecimal(row.Cells[(int)ColumnasGrid.Precio].Value.ToString());
                            try
                            {
                                conection.Open();

                                SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaPJ.ModificarListaPrecios);
                                command.Parameters.AddWithValue("@Vendedores", string.Empty);
                                command.Parameters.AddWithValue("@Lineas", string.Empty);
                                command.Parameters.AddWithValue("@Cliente", cliente);
                                command.Parameters.AddWithValue("@Articulo", Articulo);
                                command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                                command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                                command.Parameters.AddWithValue("@Factura", string.Empty);
                                command.Parameters.AddWithValue("@Sucursales", string.Empty);
                                command.Parameters.AddWithValue("@GranCanales", string.Empty);
                                command.Parameters.AddWithValue("@Canales", string.Empty);
                                command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                                command.Parameters.AddWithValue("@Mes", Mes);
                                command.Parameters.AddWithValue("@Anio", Anio);
                                command.Parameters.AddWithValue("@NombreArticulo", NombreArticulo);
                                command.Parameters.AddWithValue("@Precio", Precio);
                                command.Parameters.AddWithValue("@Moneda", string.Empty);
                                //var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                                //returnParameter.Direction = ParameterDirection.ReturnValue;
                                int x = command.ExecuteNonQuery();
                                //var result = returnParameter.Value;
                                //MessageBox.Show(result.ToString());
                                if(x == 10 )
                                    MessageBox.Show("El registro ha sido guardado correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Verifique que el Artículo o el Cliente esten escritos correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                conection.Close();
                            }
                            
                        }
                        else
                            MessageBox.Show("Falta llenar el campo 'Año'.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Falta llenar el campo '%Descuento'.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                conection.Close();
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region FUNCIONES


        private void CargarArticulos()
        {
            //MessageBox.Show(Linea);
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
            command.Parameters.AddWithValue("@Precio", 4);
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

        private void CargarClientes()
        {
            SqlCommand command = new SqlCommand("PJ_Ventas", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 18);
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
                txtCliente.AutoCompleteCustomSource = source;
                txtCliente.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Método que carga las sucursales en el cbSucursal
        /// </summary>
        private void CargarMeses()
        {
            clbMeses.Items.Clear();
            clbMeses.Items.Add("Enero");
            clbMeses.Items.Add("Febrero");
            clbMeses.Items.Add("Marzo");
            clbMeses.Items.Add("Abril");
            clbMeses.Items.Add("Mayo");
            clbMeses.Items.Add("Junio");
            clbMeses.Items.Add("Julio");
            clbMeses.Items.Add("Agosto");
            clbMeses.Items.Add("Septiembre");
            clbMeses.Items.Add("Octubre");
            clbMeses.Items.Add("Noviembre");
            clbMeses.Items.Add("Diciembre");
            clbMeses.SelectedIndex = 0;
            clbMeses.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoGrid()
        {
            gridListaPrecios.Columns[(int)ColumnasGrid.Linea].Width = 80;
            gridListaPrecios.Columns[(int)ColumnasGrid.Linea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridListaPrecios.Columns[(int)ColumnasGrid.Articulo].Width = 80;
            gridListaPrecios.Columns[(int)ColumnasGrid.Articulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridListaPrecios.Columns[(int)ColumnasGrid.NombreArticulo].Width = 260;
            gridListaPrecios.Columns[(int)ColumnasGrid.NombreArticulo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridListaPrecios.Columns[(int)ColumnasGrid.Precio].Width = 80;
            gridListaPrecios.Columns[(int)ColumnasGrid.Precio].DefaultCellStyle.Format = "C4";
            gridListaPrecios.Columns[(int)ColumnasGrid.Precio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridListaPrecios.AllowUserToAddRows = false;
            gridListaPrecios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridListaPrecios.Columns[(int)ColumnasGrid.Linea].ReadOnly = true;
            gridListaPrecios.Columns[(int)ColumnasGrid.Articulo].ReadOnly = true;
            gridListaPrecios.Columns[(int)ColumnasGrid.NombreArticulo].ReadOnly = true;
            gridListaPrecios.Columns[(int)ColumnasGrid.Precio].ReadOnly = false;

            gridListaPrecios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 54, 92);
            gridListaPrecios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridListaPrecios.ColumnHeadersDefaultCellStyle.Font = new Font(gridListaPrecios.Font, FontStyle.Bold);

            gridListaPrecios.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(220, 230, 241);
            gridListaPrecios.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            gridListaPrecios.AutoGenerateColumns = false;
        }


        private void CargarLinea()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Linea);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            cbLineas.DataSource = table;
            cbLineas.DisplayMember = "Nombre";
            cbLineas.ValueMember = "Codigo";

            cbLineas.SelectedIndex = 0;
            cbLineas.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        #endregion

        private void ConsultarCostosBase_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {                
            }
        }

        private void ConsultarCostosBase_FormClosing(object sender, FormClosingEventArgs e)
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
