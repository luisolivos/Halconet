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

namespace Presupuesto
{
    public partial class RepNomina : Form
    {
        Logs log;
        public RepNomina(int x)
        {
            VecesConsulta = x;
            InitializeComponent();
        }

        #region Parametros
        private enum ColumnasGrid
        {
            Fecha, Mes, Año, Interna, Outsourcing, Total, PresupuestoInterna, PresupuestoOutsourcing, Presupuesto
        }
        public DataTable TablaPrincipal = new DataTable();
        #endregion

        #region CONSTANTES

        public int VecesConsulta;
        public string[] CuentasInterna = new string[]{"6110-001-000",  "6110-003-000", "6110-004-000", "6110-005-000",
                                                      "6110-006-000", "6110-007-000", "6110-009-000", "6110-010-000", "6110-011-000",
                                                      "6110-012-000", "6110-013-000", "6110-073-000", "6110-080-000",
                                            };
       // ('6110-001-000','6110-002-000','6110-003-000','6110-004-000','6110-005-000','6110-006-000','6110-007-000','6110-009-000','6110-010-000','6110-011-000','6110-012-000','6110-013-000','6110-073-000')
        public string stringCuentasInterna;
        //'6110-013-000','6110-073-000'
        public string[] CuentasOutsourcing = new string[] { "6110-014-000", "6110-015-000" };


        public string stringCuentasOutsourcing;
        #endregion

        #region METODOS
        private void CargarDepartamentos(string sucursal)
        {
            DataTable table = new DataTable();
            cmbCuenta.DataSource = null;
            cmbCuenta.Items.Clear();

            table.Rows.Clear();
            table.Columns.Clear();
            SqlCommand command = new SqlCommand("PJ_ReportedeNomina", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);
            command.Parameters.AddWithValue("@Sucursal", sucursal);
            command.Parameters.AddWithValue("@Departamento", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", "01-12-2013");
            command.Parameters.AddWithValue("@FechaFinal", "01-12-2013");
            command.Parameters.AddWithValue("@Interna", 0);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            table.Rows.InsertAt(row, 0);
            try
            {
                cmbCuenta.DataSource = table;
                cmbCuenta.DisplayMember = "U_Area";
                //.ValueMember = "U_Area";

                cmbAction.DataSource = table;
                cmbAction.DisplayMember = "U_Area";
                //cmbAction.ValueMember = "U_Area";
            }
            catch (Exception) { }
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
        public DataTable Grupo(DataTable t)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_ReportedeNomina", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 1);
                command.Parameters.AddWithValue("@Sucursal", String.Empty);
                command.Parameters.AddWithValue("@Cuenta", String.Empty);
                command.Parameters.AddWithValue("@Cuentas", stringCuentasInterna);
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                gridAux.DataSource = table;

                SqlCommand command1 = new SqlCommand("PJ_ReportedeNomina", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", 1);
                command1.Parameters.AddWithValue("@Sucursal", String.Empty);
                command1.Parameters.AddWithValue("@Cuenta", String.Empty);
                command1.Parameters.AddWithValue("@Cuentas", stringCuentasOutsourcing);
                command1.CommandTimeout = 0;

                SqlDataAdapter adapter1 = new SqlDataAdapter();
                DataTable table1 = new DataTable();
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                gridAuxOutSourcing.DataSource = table1;

            }
            catch(Exception)
            {
            }
            finally{
            }

            return t;
        }
        public DataTable GrupoDepto(DataTable t)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_ReportedeNomina", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 5);
                command.Parameters.AddWithValue("@Sucursal", String.Empty);
                command.Parameters.AddWithValue("@Cuenta", Departamento);
                command.Parameters.AddWithValue("@Cuentas", stringCuentasInterna);
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                gridAux.DataSource = table;

                SqlCommand command1 = new SqlCommand("PJ_ReportedeNomina", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", 5);
                command1.Parameters.AddWithValue("@Sucursal", String.Empty);
                command1.Parameters.AddWithValue("@Cuenta", Departamento);
                command1.Parameters.AddWithValue("@Cuentas", stringCuentasOutsourcing);
                command1.CommandTimeout = 0;

                SqlDataAdapter adapter1 = new SqlDataAdapter();
                DataTable table1 = new DataTable();
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                gridAuxOutSourcing.DataSource = table1;

            }
            catch (Exception)
            {
            }
            finally
            {
            }

            return t;
        }
        public DataTable SucursalNoDepto(DataTable t)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_ReportedeNomina", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 2);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", String.Empty);
                command.Parameters.AddWithValue("@Cuentas", stringCuentasInterna);
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                gridAux.DataSource = table;

                SqlCommand command1 = new SqlCommand("PJ_ReportedeNomina", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", 2);
                command1.Parameters.AddWithValue("@Sucursal", Sucursal);
                command1.Parameters.AddWithValue("@Cuenta", String.Empty);
                command1.Parameters.AddWithValue("@Cuentas", stringCuentasOutsourcing);
                command1.CommandTimeout = 0;

                SqlDataAdapter adapter1 = new SqlDataAdapter();
                DataTable table1 = new DataTable();
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                gridAuxOutSourcing.DataSource = table1;

            }
            catch (Exception)
            {
            }
            finally
            {
            }

            return t;
        }
        public DataTable SucursalDepto(DataTable t)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_ReportedeNomina", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 3);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@Cuenta", Departamento);
                command.Parameters.AddWithValue("@Cuentas", stringCuentasInterna);
                command.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                gridAux.DataSource = table;

                SqlCommand command1 = new SqlCommand("PJ_ReportedeNomina", conection);
                command1.CommandType = CommandType.StoredProcedure;
                command1.Parameters.AddWithValue("@TipoConsulta", 3);
                command1.Parameters.AddWithValue("@Sucursal", Sucursal);
                command1.Parameters.AddWithValue("@Cuenta", Departamento);
                command1.Parameters.AddWithValue("@Cuentas", stringCuentasOutsourcing);
                command1.CommandTimeout = 0;

                SqlDataAdapter adapter1 = new SqlDataAdapter();
                DataTable table1 = new DataTable();
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                gridAuxOutSourcing.DataSource = table1;

            }
            catch (Exception)
            {
            }
            finally
            {
            }

            return t;
        }
        private void GraficarDirecto(DataGridView dgv, string titulo)
        {
            chGrafica.Series.Clear();
            chGrafica.Titles.Clear();
            chGrafica.ChartAreas.Clear();
            chGrafica.ChartAreas.Add(new ChartArea());

            chGrafica.Titles.Add("Tendencia del gasto de Nomina\r\n" + titulo);
            chGrafica.Titles[0].Alignment = ContentAlignment.TopCenter;
            chGrafica.Titles[0].TextStyle = TextStyle.Shadow;
            chGrafica.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            
            chGrafica.Series.Add("Interna");
            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                double yValue = double.Parse(dgv.Rows[0].Cells[i].Value.ToString());
                chGrafica.Series["Interna"].Points.AddY(yValue);
                chGrafica.Series["Interna"].Points[i - 1].AxisLabel = dgv.Columns[i].HeaderText.ToString();
            }
            chGrafica.Series["Interna"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chGrafica.Series["Interna"].Font = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            chGrafica.Series["Interna"].Label = "#VALY{C0}";
            chGrafica.Series["Interna"].LabelToolTip = "#VALY{C2}";
            chGrafica.Series["Interna"].Color = Color.FromArgb(128, 45, 189, 223);
            chGrafica.Series.Add("OutSourcing");
            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                double yValue = double.Parse(dgv.Rows[1].Cells[i].Value.ToString());
                chGrafica.Series["OutSourcing"].Points.AddY(yValue);

            }
            chGrafica.Series["OutSourcing"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chGrafica.Series["OutSourcing"].Font = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            chGrafica.Series["OutSourcing"].Label = "#VALY{C0}";
            chGrafica.Series["OutSourcing"].LabelToolTip = "#VALY{C2}";
            chGrafica.Series["OutSourcing"].Color = Color.FromArgb(128, 242, 135,115);

            chGrafica.Series.Add("Presupuesto");
            for (int i = 1; i < dgv.Columns.Count; i++)
            {
                double yValue = double.Parse(dgv.Rows[3].Cells[i].Value.ToString());
                chGrafica.Series["Presupuesto"].Points.AddY(yValue);

            }
            chGrafica.Series["Presupuesto"].SmartLabelStyle.Enabled = false;
            chGrafica.Series["Presupuesto"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;//Range--SplineArea--StackedArea
            chGrafica.Series["Presupuesto"].Font = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Regular);
            chGrafica.Series["Presupuesto"].Label = "#VALY{C0}";
            chGrafica.Series["Presupuesto"].LabelToolTip = "#VALY{C2}";
            chGrafica.Series["Presupuesto"].LabelForeColor = Color.Blue;
            chGrafica.Series["Presupuesto"].BorderWidth = 3;
            chGrafica.Series["Presupuesto"].Color = Color.Blue;
            chGrafica.Series["Presupuesto"].BorderDashStyle = ChartDashStyle.Dash;

            chGrafica.ChartAreas[0].AxisX.Interval = 1;
            chGrafica.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            chGrafica.ChartAreas[0].AxisX2.Interval = 1;
            chGrafica.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
            //chGrafica.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0,}";

        }
        private void ConstruirGridPrincipal()
        {
            DataTable tableaux = new DataTable();
            //agregar columnas
            tableaux.Columns.Add("Descripción", typeof(string));
            if (gridAux.Rows.Count> gridAuxOutSourcing.Rows.Count) 
            foreach (DataGridViewRow row in gridAux.Rows)
            {
                tableaux.Columns.Add( getMes(Convert.ToInt32(row.Cells["Mes"].Value))+"-"+row.Cells["Año"].Value , typeof(double));

            }
            else
                foreach (DataGridViewRow row in gridAuxOutSourcing.Rows)
                {
                    tableaux.Columns.Add(getMes(Convert.ToInt32(row.Cells["Mes"].Value)) + "-" + row.Cells["Año"].Value, typeof(double));
                }

            double[] interna = new double[tableaux.Columns.Count];
            int x = 0;
            foreach (DataGridViewRow row in gridAux.Rows)
            {
                //foreeaach item table.columns
                //si row cells[mes]+rows cells[año] = item.name
                foreach (DataColumn item in tableaux.Columns)
                {
                    if ((getMes(Convert.ToInt32(row.Cells["Mes"].Value)) + "-" + row.Cells["Año"].Value).ToString().Equals(item.ColumnName))
                    {
                        interna[x] = Convert.ToDouble(row.Cells["Gasto"].Value);
                        x++;
                    }
                }
               
            }
            DataRow r = tableaux.NewRow();
            r["Descripción"] = "INTERNA";

            for (x = 1; x < interna.Count(); x++ )
            {
                r[x] = interna[x-1];
            }
            tableaux.Rows.Add(r);

            //agregar filas outsourcing
            double[] outsourcing = new double[tableaux.Columns.Count];
            x = 0;
            foreach (DataGridViewRow row in gridAuxOutSourcing.Rows)
            {
                foreach (DataColumn item in tableaux.Columns)
                {
                    if ((getMes(Convert.ToInt32(row.Cells["Mes"].Value)) + "-" + row.Cells["Año"].Value).ToString().Equals(item.ColumnName))
                    {
                        outsourcing[x] = Convert.ToDouble(row.Cells["Gasto"].Value);
                        x++;
                    }
                }
            }
            DataRow r2 = tableaux.NewRow();
            r2["Descripción"] = "OUTSOURCING";

            for (x = 1; x < outsourcing.Count(); x++)
            {
                r2[x] = outsourcing[x - 1];
            }
            tableaux.Rows.Add(r2);
            dgvNomina.DataSource = tableaux;

            //agregar filas Totales
            double[] total = new double[tableaux.Columns.Count];
            for(x= 1; x< dgvNomina.Columns.Count; x++)
            {

                    double kdkd = dgvNomina.Rows.Cast<DataGridViewRow>().Sum(z => Convert.ToDouble(z.Cells[x].Value));
                    total[x - 1] = kdkd;
  
            }
            DataRow r3 = tableaux.NewRow();
            r3["Descripción"] = "TOTAL";

            for (x = 1; x < total.Count(); x++)
            {
                r3[x] = total[x - 1];
            }

            tableaux.Rows.Add(r3);

            DarFormatoGridPrincipal();
            ///agregar filas Presupuesto 
            double[] presupuesto = new double[tableaux.Columns.Count];
            x = 0;
            
            foreach (DataGridViewRow row in gridAuxOutSourcing.Rows)
            {
                foreach (DataColumn item in tableaux.Columns)
                {
                    if ((getMes(Convert.ToInt32(row.Cells["Mes"].Value)) + "-" + row.Cells["Año"].Value).ToString().Equals(item.ColumnName))
                    {
                        presupuesto[x] = Convert.ToDouble(row.Cells["Presupuesto"].Value);
                        x++;
                    }
                }
            }
            x = 0;
            foreach (DataGridViewRow row in gridAux.Rows)
            {
                foreach (DataColumn item in tableaux.Columns)
                {
                    if ((getMes(Convert.ToInt32(row.Cells["Mes"].Value)) + "-" + row.Cells["Año"].Value).ToString().Equals(item.ColumnName))
                    {
                        presupuesto[x] += Convert.ToDouble(row.Cells["Presupuesto"].Value);
                        x++;
                    }
                }
            }

            DataRow r4 = tableaux.NewRow();
            r4["Descripción"] = "PRESUPUESTO";

            for (x = 1; x < outsourcing.Count(); x++)
            {
                r4[x] = presupuesto[x - 1];
            }
            tableaux.Rows.Add(r4);
            dgvNomina.DataSource = tableaux;
            
        }
        private void Consultar(string depto)
        {
            try
            {
                Esperar();
                dgvNomina.Columns.Clear();
                dgvNomina.DataSource = null;
                TablaPrincipal.Rows.Clear();
                TablaPrincipal.Columns.Clear();
                string aux = "";
                switch (cmbSucursal.SelectedValue.ToString())
                {
                    case "APIZACO": aux = "APIZACO"; break;
                    case "CORDOBA": aux = "CORDOBA"; break;
                    case "EDOMEX": aux = "EDOMEX"; break;
                    case "GDL": aux = "GDL"; break;
                    case "MTY": aux = "MTY"; break;
                    case "PUEBLA": aux = "PUEBLA"; break;
                    case "TEPEACA": aux = "TEPEACA"; break;
                    case "GRUPO": aux = "GRUPO"; break;
                }
                Sucursal = aux;
                Departamento = depto;

                //interna
                SqlCommand commandInterna = new SqlCommand("PJ_ReportedeNomina", conection);
                commandInterna.CommandType = CommandType.StoredProcedure;
                commandInterna.Parameters.AddWithValue("@TipoConsulta", 2);
                commandInterna.Parameters.AddWithValue("@Sucursal", Sucursal);
                commandInterna.Parameters.AddWithValue("@Departamento", Departamento);
                commandInterna.Parameters.AddWithValue("@FechaInicial", "01-12-2013");
                commandInterna.Parameters.AddWithValue("@FechaFinal", "01-12-2013");
                commandInterna.Parameters.AddWithValue("@Interna", 1);
                commandInterna.CommandTimeout = 0;

                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();
                adapter.SelectCommand = commandInterna;
                adapter.Fill(table);
                gridAux.DataSource = table;

                SqlCommand commandOut = new SqlCommand("PJ_ReportedeNomina", conection);
                commandOut.CommandType = CommandType.StoredProcedure;
                commandOut.Parameters.AddWithValue("@TipoConsulta", 2);
                commandOut.Parameters.AddWithValue("@Sucursal", Sucursal);
                commandOut.Parameters.AddWithValue("@Departamento", Departamento);
                commandOut.Parameters.AddWithValue("@FechaInicial", "01-12-2013");
                commandOut.Parameters.AddWithValue("@FechaFinal", "01-12-2013");
                commandOut.Parameters.AddWithValue("@Interna", 0);
                commandOut.CommandTimeout = 0;

                SqlDataAdapter adapterout = new SqlDataAdapter();
                DataTable tableOut = new DataTable();
                adapter.SelectCommand = commandOut;
                adapter.Fill(tableOut);
                gridAuxOutSourcing.DataSource = tableOut;

                ConstruirGridPrincipal();
                string titulo = Sucursal + " " + Departamento;
                GraficarDirecto(dgvNomina, titulo);
                chGrafica.Visible = true;
                button1.Visible = true;
                dgvNomina.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
            }
        }
        public void DarFormatoGridPrincipal()
        {
            dgvNomina.AllowUserToResizeColumns = false;
            dgvNomina.AllowUserToResizeRows = false;
            dgvNomina.AllowUserToOrderColumns = false;
            

            dgvNomina.Columns[0].DefaultCellStyle.BackColor = dgvNomina.ColumnHeadersDefaultCellStyle.BackColor;
            dgvNomina.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int x = 1; x < dgvNomina.Columns.Count; x++)
            {

                dgvNomina.Columns[x].DefaultCellStyle.Format = "C2";
                dgvNomina.Columns[x].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvNomina.Columns[x].Width = 85;
                dgvNomina.Columns[x].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        #endregion

        #region VARIABLES

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        public string Sucursal, Departamento;

        public bool tipoGrafico = false;

        #endregion

        #region EVENTOS
        private void RepNomina_FormClosed(object sender, FormClosedEventArgs e)
        {
           // Principal newForm = new Principal();
           // this.Hide();
           // newForm.ShowDialog();
           // this.Close();
        }

        private void RepNomina_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            //this.MaximizeBox = false;
            log = new Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            foreach (string item in CuentasInterna)
            {
                stringCuentasInterna += item + ",";
            }

            foreach (string item in CuentasOutsourcing)
            {
                stringCuentasOutsourcing += item + ",";
            }
            CargarNR();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
        
            Departamento = cmbCuenta.SelectedText.Trim();

   
                Consultar(Departamento);
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (chGrafica.Dock != DockStyle.Fill)
            {
                chGrafica.Dock = DockStyle.Fill;
                cmbAction.Visible = true;
                groupBox1.Visible = false;
                label1.Visible = false;
                dgvNomina.Visible = false;
            }
            else
            {
                chGrafica.Dock = DockStyle.None;
                cmbAction.Visible = false;
                dgvNomina.Visible = true;
                groupBox1.Visible = true;
                label1.Visible = true;
            }
        }

        private void cmbSucursal_SelectedValueChanged(object sender, EventArgs e)
        {
            string aux = "";
            // MessageBox.Show("#");
            if (!"GRUPO".Equals(cmbSucursal.SelectedValue))
            {
                switch (cmbSucursal.SelectedValue.ToString())
                {
                    case "APIZACO": aux = "Apizaco"; break;
                    case "CORDOBA": aux = "Córdoba"; break;
                    case "EDOMEX": aux = "Edo. México"; break;
                    case "GDL": aux = "Guadalajara"; break;
                    case "MTY": aux = "Monterrey"; break;
                    case "PUEBLA": aux = "Puebla"; break;
                    case "TEPEACA": aux = "Tepeaca"; break;
                    case "GRUPO": aux = "Grupo"; break;
                }
                
            }
            CargarDepartamentos(aux);
        }

        private void cmbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Departamento = cmbAction.Text.ToString().Trim();
            if(VecesConsulta != 0)
                Consultar(Departamento);
            VecesConsulta++;
        }

        private void chGrafica_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (e.Axis.AxisName == AxisName.X)
            {
                int start = (int)e.Axis.ScaleView.ViewMinimum;
                int end = (int)e.Axis.ScaleView.ViewMaximum;

                // Series ss = chart1.Series.FindByName("SeriesName");
                // use ss instead of chart1.Series[0]

                double[] temp = chGrafica.Series[0].Points.Where((x, i) => i >= start && i <= end).Select(x => x.YValues[0]).ToArray();
                double ymin = temp.Min();
                double ymax = temp.Max();

                chGrafica.ChartAreas[0].AxisY.ScaleView.Position = ymin;
                chGrafica.ChartAreas[0].AxisY.ScaleView.Size = ymax - ymin;
            }
        }
        #endregion

        private void RepNomina_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
             
            }
                        
        }

        private void RepNomina_FormClosing(object sender, FormClosingEventArgs e)
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
