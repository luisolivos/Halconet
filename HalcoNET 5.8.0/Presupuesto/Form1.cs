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
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

namespace Presupuesto
{
    public partial class Form1 : Form
    {
        Logs log;

        #region PARAMETROS
        private enum ColumnasGrid
        {
            Fecha,Mes, Año, Directo, Dispersado, Total, PresupuestoDirecto, PresupuestoDiferido,Presupuesto, Diferencia
        }

        public DataTable TablaPrincipal = new DataTable();
        #endregion

        #region VARIABLES

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        public string Sucursal, Cuenta;

        public bool tipoGrafico = false;

        #endregion

        #region Eventos
        public Form1()
        {
            InitializeComponent();
        }

        private void frmTendencia_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            CargarCuentas();
            CargarNR();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                dgvDirecto.Columns.Clear();
                dgvDirecto.DataSource = null;
                TablaPrincipal.Rows.Clear();
                TablaPrincipal.Columns.Clear();

                Sucursal = cmbSucursal.SelectedValue.ToString();
                Cuenta = cmbProyectos.SelectedValue.ToString();

                if (Sucursal == "GRUPO" && Cuenta == "")
                {
                    
                    TablaPrincipal = this.CrearTabla(TablaPrincipal);
                    TablaPrincipal = this.GurpoDirecto(TablaPrincipal);
                    dgvDirecto.DataSource = TablaPrincipal;
                    this.GraficarDirecto(dgvDirecto, Sucursal);
                    tipoGrafico = false;
                }
                else if (Sucursal == "GRUPO" && Cuenta != "")
                {
                    TablaPrincipal = this.CrearTabla(TablaPrincipal);
                    TablaPrincipal = this.GrupoCuentaDirecto(TablaPrincipal);
                    dgvDirecto.DataSource = TablaPrincipal;

                    this.GraficarDirecto(dgvDirecto, Sucursal + " " + Cuenta);
                    tipoGrafico = false;
                }
                else if (Sucursal != "GRUPO" && Cuenta == "")
                {
                    TablaPrincipal = CrearTabla(TablaPrincipal);
                    TablaPrincipal = this.SucursalDirecto(TablaPrincipal);
                    TablaPrincipal = this.SucursalDispersado(TablaPrincipal);
                    dgvDirecto.DataSource = TablaPrincipal;
                    this.GraficarDirectoDisprsado(dgvDirecto, Sucursal);
                    tipoGrafico = true;
                }
                else if (Sucursal != "GRUPO" && Cuenta != "")
                {
                    TablaPrincipal = CrearTabla(TablaPrincipal);
                    TablaPrincipal = this.SucursalDirectoCuenta(TablaPrincipal);
                    TablaPrincipal = this.SucursalDispersadoCuenta(TablaPrincipal);
                    dgvDirecto.DataSource = TablaPrincipal;
                    this.GraficarDirectoDisprsado(dgvDirecto, Sucursal + " " + Cuenta);
                    tipoGrafico = true;
                }
                button2.Image = Properties.Resources._5;
                button2.Enabled = true;
                DarFormatoGrid();
                dgvDirecto.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int w = 0;
            int h = 0;
            if (chGrafica.Dock != DockStyle.Fill)
            {
                chGrafica.Dock = DockStyle.Fill;
                w = chGrafica.Width;
                h = chGrafica.Height;
            }
            else
            {
                chGrafica.Dock = DockStyle.None;

                chGrafica.Width = w;
                chGrafica.Height = h;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tipoGrafico)
            {
                if (chGrafica.Series["Directo"].ChartType == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn)
                {
                    chGrafica.Series["Directo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                    chGrafica.Series["Dispersado"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                    button2.Image = Properties.Resources._5;
                }
                else
                {
                    chGrafica.Series["Directo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                    chGrafica.Series["Dispersado"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                    button2.Image = Properties.Resources._6;
                }
            }
            else
            {
                if (chGrafica.Series["Directo"].ChartType == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn)
                {
                    chGrafica.Series["Directo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
                    button2.Image = Properties.Resources._5;
                }
                else if (chGrafica.Series["Directo"].ChartType == System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea)
                {
                    chGrafica.Series["Directo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
                    button2.Image = Properties.Resources._6;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Principal newForm = new Principal();

            //this.Hide();
            //newForm.ShowDialog();
            //this.Close();
        }
        #endregion

        #region METODOS
        private void CargarCuentas()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVarias", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 9);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            table.Rows.InsertAt(row, 0);
            cmbProyectos.DataSource = table;
            cmbProyectos.DisplayMember = "CuentaNombre";
            cmbProyectos.ValueMember = "AcctCode";
        }

        private void CargarNR()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVarias", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 10);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["NR"] = "GRUPO";
            table.Rows.InsertAt(row, 0);
            cmbSucursal.DataSource = table;
            cmbSucursal.DisplayMember = "NR";
            cmbSucursal.ValueMember = "NR";
        }

        private void DarFormatoGrid()
        {
            /***********IMAGENES*/
            DataGridViewImageColumn img = new DataGridViewImageColumn();
            {
                img.Name = "Img";
                img.HeaderText = "";
                img.Width = 30;
            }
            /**/
            dgvDirecto.Columns.Add(img);

            dgvDirecto.Columns[(int)ColumnasGrid.Fecha].Visible = false; ;
            dgvDirecto.Columns[(int)ColumnasGrid.Mes].Width = 40;

            dgvDirecto.Columns[(int)ColumnasGrid.Año].Width = 40;

            dgvDirecto.Columns[(int)ColumnasGrid.Directo].Width = 80;
            dgvDirecto.Columns[(int)ColumnasGrid.Directo].DefaultCellStyle.Format = "C0";
            dgvDirecto.Columns[(int)ColumnasGrid.Directo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDirecto.Columns[(int)ColumnasGrid.Dispersado].Width = 80;
            dgvDirecto.Columns[(int)ColumnasGrid.Dispersado].DefaultCellStyle.Format = "C0";
            dgvDirecto.Columns[(int)ColumnasGrid.Dispersado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDirecto.Columns[(int)ColumnasGrid.Total].Width = 80;
            dgvDirecto.Columns[(int)ColumnasGrid.Total].DefaultCellStyle.Format = "C0";
            dgvDirecto.Columns[(int)ColumnasGrid.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDirecto.Columns[(int)ColumnasGrid.PresupuestoDirecto].Width = 80;
            dgvDirecto.Columns[(int)ColumnasGrid.PresupuestoDirecto].DefaultCellStyle.Format = "C0";
            dgvDirecto.Columns[(int)ColumnasGrid.PresupuestoDirecto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDirecto.Columns[(int)ColumnasGrid.PresupuestoDirecto].Visible = false;
            dgvDirecto.Columns[(int)ColumnasGrid.PresupuestoDiferido].Width = 80;
            dgvDirecto.Columns[(int)ColumnasGrid.PresupuestoDiferido].DefaultCellStyle.Format = "C0";
            dgvDirecto.Columns[(int)ColumnasGrid.PresupuestoDiferido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDirecto.Columns[(int)ColumnasGrid.PresupuestoDiferido].Visible = false;
            dgvDirecto.Columns[(int)ColumnasGrid.Presupuesto].Width = 80;
            dgvDirecto.Columns[(int)ColumnasGrid.Presupuesto].DefaultCellStyle.Format = "C0";
            dgvDirecto.Columns[(int)ColumnasGrid.Presupuesto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDirecto.Columns[(int)ColumnasGrid.Diferencia].Width = 75;
            dgvDirecto.Columns[(int)ColumnasGrid.Diferencia].DefaultCellStyle.Format = "C0";
            dgvDirecto.Columns[(int)ColumnasGrid.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDirecto.Columns[(int)ColumnasGrid.Diferencia].Visible = false;

            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Mes].Style.ForeColor = dgvDirecto.ColumnHeadersDefaultCellStyle.ForeColor;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Mes].Style.BackColor = dgvDirecto.ColumnHeadersDefaultCellStyle.BackColor;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Año].Style.ForeColor = dgvDirecto.ColumnHeadersDefaultCellStyle.ForeColor;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Año].Style.BackColor = dgvDirecto.ColumnHeadersDefaultCellStyle.BackColor;

            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Directo].Style.ForeColor = Color.Black;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Directo].Style.BackColor = Color.Cyan;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Dispersado].Style.ForeColor = Color.Black;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Dispersado].Style.BackColor = Color.Cyan;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Total].Style.ForeColor = Color.Black;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Total].Style.BackColor = Color.Cyan;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Presupuesto].Style.ForeColor = Color.Black;
            dgvDirecto.Rows[11].Cells[(int)ColumnasGrid.Presupuesto].Style.BackColor = Color.Cyan;

            dgvDirecto.Columns[(int)ColumnasGrid.Mes].DefaultCellStyle.BackColor = dgvDirecto.ColumnHeadersDefaultCellStyle.BackColor;
            dgvDirecto.Columns[(int)ColumnasGrid.Año].DefaultCellStyle.BackColor = dgvDirecto.ColumnHeadersDefaultCellStyle.BackColor;
            
            /*******IMAGENES*/
            foreach (DataGridViewRow item in dgvDirecto.Rows)
            {
                if (Convert.ToDouble(item.Cells[(int)ColumnasGrid.Diferencia].Value) < 0.0)
                {
                   
                    item.Cells["Img"].Value = Properties.Resources.Circle_Red;
                }
                else
                {
                    item.Cells["Img"].Value = Properties.Resources.Circle_Green;
                }
            }
             
            foreach (DataGridViewRow item in dgvDirecto.Rows)
            {
                item.Cells["Img"].ToolTipText = Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Diferencia].Value).ToString("C2");
                //item.Cells["Img"].Style.ForeColor = Color.White;

            }
            /**/
            foreach (DataGridViewColumn item in dgvDirecto.Columns)
            {
                //.SortMode = DataGridViewColumnSortMode.NotSortable;
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgvDirecto.AllowUserToResizeColumns = false;
        }
        private DataTable GurpoDirecto(DataTable table)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.GurpoDirecto);//TipoConsulta = 1
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", String.Empty);
                command.Parameters.AddWithValue("@Presupuesto", 0);
                command.CommandTimeout = 0;
                conection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(reader["Mes"]);
                    annio = Convert.ToString(reader["Año"]);
                    directo = Convert.ToDouble(reader["Cantidad"]);
                   // Dim fechafin As DateTime = fechainicio.AddMonths(1).AddDays(-1)
                   // int day()
                    string aux = ("01" + "/" + (mes) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x]["Mes"] = getMes(mes);
                            TablaPrincipal.Rows[x]["Año"] = annio;
                            TablaPrincipal.Rows[x]["Directo"] = 0.0;
                            TablaPrincipal.Rows[x]["Directo"] = directo;

                            TablaPrincipal.Rows[x]["Dispersado"] = 0.0;
                            TablaPrincipal.Rows[x]["PresupuestoDirecto"] = 0.0;
                            TablaPrincipal.Rows[x]["PresupuestoDiferido"] = 0.0;
                            
                        }
                        x++;
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            try
            {
                conection.Open();
                SqlCommand command1 = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.GurpoDirecto);//TipoConsulta = 1
                command1.Parameters.AddWithValue("@Sucursal", Sucursal);
                command1.Parameters.AddWithValue("@Cuenta", string.Empty);
                command1.Parameters.AddWithValue("@Presupuesto", 1);
                command1.CommandTimeout = 0;

                SqlDataReader readerp = command1.ExecuteReader();

                while (readerp.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(readerp["Mes"]);
                    annio = Convert.ToString(readerp["Año"]);
                    directo = Convert.ToDouble(readerp["Cantidad"]);

                    string aux = ("01" + "/" + (mes+1) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x][6] = directo;
                        }
                        x++;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            return table;
        }

        private DataTable GrupoCuentaDirecto(DataTable table)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.GrupoCuentaDirecto);//TipoConsulta = 2
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", Cuenta);
                command.Parameters.AddWithValue("@Presupuesto", 0);
                command.CommandTimeout = 0;
                conection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(reader["Mes"]);
                    annio = Convert.ToString(reader["Año"]);
                    directo = Convert.ToDouble(reader["Cantidad"]);

                    string aux = ("01" + "/" + (mes) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x]["Mes"] = getMes(mes);
                            TablaPrincipal.Rows[x]["Año"] = annio;
                            TablaPrincipal.Rows[x]["Directo"] = 0.0;
                            TablaPrincipal.Rows[x]["Directo"] = directo;

                            TablaPrincipal.Rows[x]["Dispersado"] = 0.0;
                            TablaPrincipal.Rows[x]["PresupuestoDirecto"] = 0.0;
                            TablaPrincipal.Rows[x]["PresupuestoDiferido"] = 0.0;
                        }
                        x++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            try
            {
                conection.Open();
                SqlCommand command1 = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.GrupoCuentaDirecto);//TipoConsulta = 1
                command1.Parameters.AddWithValue("@Sucursal", Sucursal);
                command1.Parameters.AddWithValue("@Cuenta", Cuenta);
                command1.Parameters.AddWithValue("@Presupuesto", 1);
                command1.CommandTimeout = 0;

                SqlDataReader readerp = command1.ExecuteReader();

                while (readerp.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(readerp["Mes"]);
                    annio = Convert.ToString(readerp["Año"]);
                    directo = Convert.ToDouble(readerp["Cantidad"]);

                    string aux = ("01" + "/" + (mes+1) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x][6] = directo;
                        }
                        x++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }
            return table;
           
        }

        private DataTable SucursalDirecto(DataTable table)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.SucursalDirecto);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", string.Empty);
                command.Parameters.AddWithValue("Presupuesto", 0);
                command.CommandTimeout = 0;
                conection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(reader["Mes"]);
                    annio = Convert.ToString(reader["Año"]);
                    directo = Convert.ToDouble(reader["Cantidad"]);

                    string aux = ("01" + "/" + (mes) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x]["Mes"] = getMes(mes);
                            TablaPrincipal.Rows[x]["Año"] = annio;
                            TablaPrincipal.Rows[x]["Directo"] = 0.0;
                            TablaPrincipal.Rows[x]["Directo"] = directo;

                            TablaPrincipal.Rows[x]["Dispersado"] = 0.0;
                            TablaPrincipal.Rows[x]["PresupuestoDirecto"] = 0.0;
                            TablaPrincipal.Rows[x]["PresupuestoDiferido"] = 0.0;
                        }
                        x++;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            try
            {
                conection.Open();
                SqlCommand command1 = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.SucursalDirecto);//TipoConsulta = 1
                command1.Parameters.AddWithValue("@Sucursal", Sucursal);
                command1.Parameters.AddWithValue("@Cuenta", string.Empty);
                command1.Parameters.AddWithValue("@Presupuesto", 1);
                command1.CommandTimeout = 0;

                SqlDataReader readerp = command1.ExecuteReader();

                while (readerp.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(readerp["Mes"]);
                    annio = Convert.ToString(readerp["Año"]);
                    directo = Convert.ToDouble(readerp["Cantidad"]);

                    string aux = ("01" + "/" + (mes+1) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x][6] = directo;
                        }
                        x++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            return table;
        }
        private DataTable SucursalDispersado(DataTable table)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.SucursalDispersado);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", String.Empty);
                command.Parameters.AddWithValue("Presupuesto", 0);
                command.CommandTimeout = 0;

                conection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(reader["Mes"]);
                    annio = Convert.ToString(reader["Año"]);
                    directo = Convert.ToDouble(reader["Cantidad"]);

                    string aux = ("01" + "/" + (mes) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x][4] = reader["Cantidad"];
                        }
                        x++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            try
            {
                conection.Open();
                SqlCommand command1 = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.SucursalDispersado);//TipoConsulta = 1
                command1.Parameters.AddWithValue("@Sucursal", Sucursal);
                command1.Parameters.AddWithValue("@Cuenta", string.Empty);
                command1.Parameters.AddWithValue("@Presupuesto", 1);
                command1.CommandTimeout = 0;

                SqlDataReader readerp = command1.ExecuteReader();

                while (readerp.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(readerp["Mes"]);
                    annio = Convert.ToString(readerp["Año"]);
                    directo = Convert.ToDouble(readerp["Cantidad"]);

                    string aux = ("01" + "/" + (mes+1) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x][7] = directo;
                        }
                        x++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            return table;
        }

        
        private DataTable SucursalDirectoCuenta(DataTable table)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.SucursalDirectoCuenta);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", Cuenta);
                command.Parameters.AddWithValue("Presupuesto", 0);
                command.CommandTimeout = 0;

                conection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(reader["Mes"]);
                    annio = Convert.ToString(reader["Año"]);
                    directo = Convert.ToDouble(reader["Cantidad"]);

                    string aux = ("01" + "/" + (mes) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x]["Mes"] = getMes(mes);
                            TablaPrincipal.Rows[x]["Año"] = annio;
                            TablaPrincipal.Rows[x]["Directo"] = 0.0;
                            TablaPrincipal.Rows[x]["Directo"] = directo;

                            TablaPrincipal.Rows[x]["Dispersado"] = 0.0;
                            TablaPrincipal.Rows[x]["PresupuestoDirecto"] = 0.0;
                            TablaPrincipal.Rows[x]["PresupuestoDiferido"] = 0.0;
                        }
                        x++;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            try
            {
                conection.Open();
                SqlCommand command1 = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.SucursalDirectoCuenta);//TipoConsulta = 1
                command1.Parameters.AddWithValue("@Sucursal", Sucursal);
                command1.Parameters.AddWithValue("@Cuenta", Cuenta);
                command1.Parameters.AddWithValue("@Presupuesto", 1);
                command1.CommandTimeout = 0;

                SqlDataReader readerp = command1.ExecuteReader();


                while (readerp.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(readerp["Mes"]);
                    annio = Convert.ToString(readerp["Año"]);
                    directo = Convert.ToDouble(readerp["Cantidad"]);

                    string aux = ("01" + "/" + (mes+1) + "/" + annio);

                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x][6] = directo;
                        }
                        x++;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            return table;
        }
        private DataTable SucursalDispersadoCuenta(DataTable table)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.SucursalDispersadoCuenta);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", Cuenta);
                command.Parameters.AddWithValue("Presupuesto", 0);
                command.CommandTimeout = 0;

                conection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(reader["Mes"]);
                    annio = Convert.ToString(reader["Año"]);
                    directo = Convert.ToDouble(reader["Cantidad"]);

                    string aux = ("01" + "/" + (mes) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x][4] = reader["Cantidad"];
                        }
                        x++;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }

            try
            {
                conection.Open();
                SqlCommand command1 = new SqlCommand("PJ_TendenciaGastoPrueba", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", Constantes.TipoConsultaTendenciaGasto.SucursalDispersadoCuenta);//TipoConsulta = 1
                command1.Parameters.AddWithValue("@Sucursal", Sucursal);
                command1.Parameters.AddWithValue("@Cuenta", Cuenta);
                command1.Parameters.AddWithValue("@Presupuesto", 1);
                command1.CommandTimeout = 0;

                SqlDataReader readerp = command1.ExecuteReader();


                while (readerp.Read())
                {
                    int mes = 0; string annio = ""; double directo = 0;
                    mes = Convert.ToInt32(readerp["Mes"]);
                    annio = Convert.ToString(readerp["Año"]);
                    directo = Convert.ToDouble(readerp["Cantidad"]);

                    string aux = ("01" + "/" + (mes+1) + "/" + annio);
                    int x = 0;
                    foreach (DataRow item2 in TablaPrincipal.Rows)
                    {
                        if (aux.Equals(item2["Fecha"]))
                        {
                            TablaPrincipal.Rows[x][7] = directo;
                        }
                        x++;
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }
            return table;
        }

        private DataTable CrearTabla(DataTable t)
        {
            t.Columns.Add("Fecha", typeof(string));
            t.Columns.Add("Mes", typeof(string));
            t.Columns.Add("Año", typeof(int));
            t.Columns.Add("Directo", typeof(double));
            t.Columns.Add("Dispersado", typeof(double));
            t.Columns.Add("Total", typeof(double), "Directo+Dispersado");
            t.Columns.Add("PresupuestoDirecto", typeof(double));
            t.Columns.Add("PresupuestoDiferido", typeof(double));
            t.Columns.Add("Presupuesto", typeof(double), "PresupuestoDirecto+PresupuestoDiferido");
            t.Columns.Add("Diferencia", typeof(double), ("Presupuesto-Total"));

            for (int x = 11; x >= 0; x--)
            {
                DataRow row = t.NewRow();
                DateTime d1 = DateTime.Now.AddMonths(x * (-1));
                string d = "01/"+d1.Month+"/"+d1.Year;
                row["Fecha"] = d;
                row["Mes"] = getMes(int.Parse(DateTime.Parse(Convert.ToString(row["Fecha"])).Month.ToString()));
                row["Año"] = DateTime.Parse(Convert.ToString(row["Fecha"])).Year.ToString();


                row["Directo"] = 0.0;
                row["Dispersado"] = 0.0;
                row["PresupuestoDirecto"] = 0.0;
                row["PresupuestoDiferido"] = 0.0;
                row["Presupuesto"] = 0.0;
                t.Rows.Add(row);
            }
            return t;
        }

        private void GraficarDirectoDisprsado(DataGridView dgv, string titulo)
        {
            chGrafica.Series.Clear();
            chGrafica.Titles.Clear();

            chGrafica.Titles.Add("Tendencia del gasto\r\n" + titulo);
            chGrafica.Titles[0].Alignment = ContentAlignment.TopCenter;
            chGrafica.Titles[0].TextStyle = TextStyle.Shadow;
            chGrafica.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular);

            chGrafica.Series.Add("Directo");
            
            for (int i = 0; i < dgv.RowCount; i++)
            {
                double yValue = double.Parse(dgv.Rows[i].Cells[3].Value.ToString());
                chGrafica.Series["Directo"].Points.AddY(yValue);
                chGrafica.Series["Directo"].Points[i].AxisLabel = dgv.Rows[i].Cells[1].Value.ToString() + " " + dgv.Rows[i].Cells[2].Value.ToString();
            }
            chGrafica.Series["Directo"].SmartLabelStyle.Enabled = false;
            chGrafica.Series["Directo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;
            chGrafica.Series["Directo"].Label = "#VALY{C0}";
            chGrafica.Series["Directo"].LabelToolTip = "#VALY{C2}";
            chGrafica.Series["Directo"].LabelAngle = -90;
            chGrafica.Series["Directo"].Color = Color.FromArgb(128, 45, 189, 223);
            chGrafica.Series["Directo"].CustomProperties = "LabelStyle=Bottom";
            //hGrafica.Series["Directo"].Color = Color.LightSkyBlue;


            chGrafica.Series.Add("Dispersado");
            for (int i = 0; i < dgv.RowCount; i++)
            {
                double yValue = double.Parse(dgv.Rows[i].Cells[4].Value.ToString());
                chGrafica.Series["Dispersado"].Points.AddY(yValue);
            }
            chGrafica.Series["Dispersado"].SmartLabelStyle.Enabled = false;
            chGrafica.Series["Dispersado"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;//Range--SplineArea--StackedArea
            chGrafica.Series["Dispersado"].Label = "#VALY{C0}";
            chGrafica.Series["Dispersado"].LabelToolTip = "#VALY{C2}";
            chGrafica.Series["Dispersado"].LabelAngle = -90;
            chGrafica.Series["Dispersado"].Color = Color.FromArgb(128, 242, 135, 115);
            chGrafica.Series["Dispersado"].CustomProperties = "LabelStyle=Bottom";
            //chGrafica.Series["Dispersado"].Color = Color.LightSalmon;

            /*chGrafica.Series.Add("Total");
            for (int i = 0; i < dgv.RowCount; i++)
            {
                double yValue = double.Parse(dgv.Rows[i].Cells[5].Value.ToString());
                chGrafica.Series["Total"].Points.AddY(yValue);
            }
            chGrafica.Series["Total"].SmartLabelStyle.Enabled = false;
            chGrafica.Series["Total"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;//Range--SplineArea--StackedArea
            chGrafica.Series["Total"].Color = Color.Black;
           // chGrafica.Series["Total"].Label = "#VALY{C0}";
            //chGrafica.Series["Total"].LabelAngle = 90;*/

            chGrafica.Series.Add("Presupuesto");
            for (int i = 0; i < dgv.RowCount; i++)
            {
                double yValue = double.Parse(dgv.Rows[i].Cells[(int)ColumnasGrid.Presupuesto].Value.ToString());
                chGrafica.Series["Presupuesto"].Points.AddY(yValue);
            }
            chGrafica.Series["Presupuesto"].SmartLabelStyle.Enabled = false;
            chGrafica.Series["Presupuesto"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//Range--SplineArea--StackedArea
            //chGrafica.Series["Presupuesto"].Label = "#VALY{C0}";
            //chGrafica.Series["Presupuesto"].LabelAngle = -90;
            chGrafica.Series["Presupuesto"].LabelToolTip = "#VALY{C2}";
            chGrafica.Series["Presupuesto"].LabelForeColor = Color.Blue;
            chGrafica.Series["Presupuesto"].BorderWidth = 3;
            chGrafica.Series["Presupuesto"].Color = Color.Blue;
            chGrafica.Series["Presupuesto"].BorderDashStyle = ChartDashStyle.Dash;
            //chGrafica.Series["Presupuesto"].CustomProperties = "LabelStyle=Top";

            chGrafica.ChartAreas[0].AxisX.Interval = 1;
            chGrafica.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            chGrafica.ChartAreas[0].AxisX2.Interval = 1;
            chGrafica.ChartAreas[0].AxisY.LabelStyle.Format = "C0";//{0:0,}";

            
        }

        private void GraficarDirecto(DataGridView dgv, string titulo)
        {
            chGrafica.Series.Clear();
            chGrafica.Titles.Clear();
            chGrafica.ChartAreas.Clear();
            chGrafica.ChartAreas.Add(new ChartArea());

            chGrafica.Titles.Add("Tendencia del gasto\r\n" + titulo);
            chGrafica.Titles[0].Alignment = ContentAlignment.TopCenter;
            chGrafica.Titles[0].TextStyle = TextStyle.Shadow;
            chGrafica.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Regular);
            chGrafica.Series.Add("Directo");

            for (int i = 0; i < dgv.RowCount; i++)
            {
                double yValue = double.Parse(dgv.Rows[i].Cells[3].Value.ToString());
                chGrafica.Series["Directo"].Points.AddY(yValue);

                chGrafica.Series["Directo"].Points[i].AxisLabel = dgv.Rows[i].Cells[1].Value.ToString() + " " + dgv.Rows[i].Cells[2].Value.ToString();
            }
            chGrafica.Series["Directo"].SmartLabelStyle.Enabled = false;
            chGrafica.Series["Directo"].Font = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            chGrafica.Series["Directo"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedArea;//Range--SplineArea--StackedArea
            chGrafica.Series["Directo"].Label = "#VALY{C0}";
            chGrafica.Series["Directo"].LabelToolTip = "#VALY{C0}";
            chGrafica.Series["Directo"].LabelAngle = -90;
            chGrafica.Series["Directo"].CustomProperties = "LabelStyle=Center";
           // chGrafica.Series["Directo"].SmartLabelStyle.Enabled = true;
            //chGrafica.Series["Directo"].
            //chGrafica.Series["Directo"].LabelAngle = -45;
            chGrafica.Series["Directo"].Color = Color.FromArgb(128, 45, 189, 223);
          

            chGrafica.Series.Add("Presupuesto");
            for (int i = 0; i < dgv.RowCount; i++)
            {
                double yValue = double.Parse(dgv.Rows[i].Cells[6].Value.ToString());
                chGrafica.Series["Presupuesto"].Points.AddY(yValue);
                
            }
            chGrafica.Series["Presupuesto"].SmartLabelStyle.Enabled = false;
            chGrafica.Series["Presupuesto"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//Range--SplineArea--StackedArea
            chGrafica.Series["Presupuesto"].Label = "#VALY{C0}";
            chGrafica.Series["Presupuesto"].LabelToolTip = "#VALY{C0}";
            chGrafica.Series["Presupuesto"].Font = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            chGrafica.Series["Presupuesto"].BorderWidth = 3;
            chGrafica.Series["Presupuesto"].Color = Color.Blue;
            chGrafica.Series["Presupuesto"].LabelAngle = -90;
            chGrafica.Series["Presupuesto"].LabelForeColor = Color.Blue;
            chGrafica.Series["Presupuesto"].BorderDashStyle = ChartDashStyle.Dash;
            chGrafica.Series["Presupuesto"].CustomProperties = "LabelStyle=Top";

            chGrafica.ChartAreas[0].AxisX.Interval = 1;
            chGrafica.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            chGrafica.ChartAreas[0].AxisX2.Interval = 1;
            chGrafica.ChartAreas[0].AxisY.LabelStyle.Format = "C0";// "{0:0,}";
            
        }

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

        public string getMes(int mes)
        {
            string auxmes = "";
            switch (mes)
            {
                case 1:
                    auxmes = "Ene";
                    break;
                case 2:
                    auxmes = "Feb";
                    break;
                case 3:
                    auxmes = "Mar";
                    break;
                case 4:
                    auxmes = "Abr";
                    break;
                case 5:
                    auxmes = "May";
                    break;
                case 6:
                    auxmes = "Jun";
                    break;
                case 7:
                    auxmes = "Jul";
                    break;
                case 8:
                    auxmes = "Ago";
                    break;
                case 9:
                    auxmes = "Sep";
                    break;
                case 10:
                    auxmes = "Oct";
                    break;
                case 11:
                    auxmes = "Nov";
                    break;
                case 12:
                    auxmes = "Dic";
                    break;
            }
            return auxmes;
        }

        public int getMes(string mes)
        {
            int auxmes = 0;
            switch (mes)
            {
                case "Ene":
                    auxmes = 1;
                    break;
                case "Feb":
                    auxmes = 2;
                    break;
                case "Mar":
                    auxmes = 3;
                    break;
                case "Abr":
                    auxmes = 4;
                    break;
                case "May":
                    auxmes = 5;
                    break;
                case "Jun":
                    auxmes = 6;
                    break;
                case "Jul":
                    auxmes = 7;
                    break;
                case "Ago":
                    auxmes = 8;
                    break;
                case "Sep":
                    auxmes = 9;
                    break;
                case "Oct":
                    auxmes = 10;
                    break;
                case "Nov":
                    auxmes = 11;
                    break;
                case "Dic":
                    auxmes = 12;
                    break;
            }
            return auxmes;
        }
        #endregion

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
