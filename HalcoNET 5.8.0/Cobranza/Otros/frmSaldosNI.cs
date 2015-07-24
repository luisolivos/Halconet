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

namespace Cobranza
{
    public partial class frmSaldosNI : Form
    {
        private string Usuario;
        private int IDLog;
        public frmSaldosNI(string _usuario)
        {
            InitializeComponent();
            Usuario = _usuario;
        }

        public enum ColumnasGrid
        {
            Sucursal, NoIdentificado, NoIdentificadoVencido, IngresosNI, SalidasNI, IngresosNIV, SalidasNIV, Cta
        }

        public enum Columnas
        {
            Fecha,
            Asiento,
            Tipo,
            Movimiento,
            Cargo,
            Abono,
            Saldo
        }


        public void FormatoGrid(DataGridView _dgv, bool _visible)
        {
            _dgv.Columns[(int)ColumnasGrid.Sucursal].Visible = _visible;
            _dgv.Columns[(int)ColumnasGrid.Cta].Visible = false;

            _dgv.Columns[(int)ColumnasGrid.Sucursal].Width = 90;
            _dgv.Columns[(int)ColumnasGrid.NoIdentificado].Width = 120;
            _dgv.Columns[(int)ColumnasGrid.NoIdentificadoVencido].Width = 120;
            _dgv.Columns[(int)ColumnasGrid.IngresosNI].Width = 100;
            _dgv.Columns[(int)ColumnasGrid.SalidasNI].Width = 100;
            _dgv.Columns[(int)ColumnasGrid.IngresosNIV].Width = 100;
            _dgv.Columns[(int)ColumnasGrid.SalidasNIV].Width = 100;

            _dgv.Columns[(int)ColumnasGrid.IngresosNI].HeaderText = "Ingresos";
            _dgv.Columns[(int)ColumnasGrid.SalidasNI].HeaderText = "Salidas";
            _dgv.Columns[(int)ColumnasGrid.IngresosNIV].HeaderText = "Ingresos";
            _dgv.Columns[(int)ColumnasGrid.SalidasNIV].HeaderText = "Salidas";

            _dgv.Columns[(int)ColumnasGrid.NoIdentificado].DefaultCellStyle.Format = "C0";
            _dgv.Columns[(int)ColumnasGrid.NoIdentificadoVencido].DefaultCellStyle.Format = "C0";
            _dgv.Columns[(int)ColumnasGrid.IngresosNI].DefaultCellStyle.Format = "C0";
            _dgv.Columns[(int)ColumnasGrid.SalidasNI].DefaultCellStyle.Format = "C0";
            _dgv.Columns[(int)ColumnasGrid.IngresosNIV].DefaultCellStyle.Format = "C0";
            _dgv.Columns[(int)ColumnasGrid.SalidasNIV].DefaultCellStyle.Format = "C0";

            _dgv.Columns[(int)ColumnasGrid.NoIdentificado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dgv.Columns[(int)ColumnasGrid.NoIdentificadoVencido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dgv.Columns[(int)ColumnasGrid.IngresosNI].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dgv.Columns[(int)ColumnasGrid.SalidasNI].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dgv.Columns[(int)ColumnasGrid.IngresosNIV].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dgv.Columns[(int)ColumnasGrid.SalidasNIV].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
          
            _dgv.Columns[(int)ColumnasGrid.Sucursal].SortMode = DataGridViewColumnSortMode.NotSortable;
            _dgv.Columns[(int)ColumnasGrid.NoIdentificado].SortMode = DataGridViewColumnSortMode.NotSortable;
            _dgv.Columns[(int)ColumnasGrid.NoIdentificadoVencido].SortMode = DataGridViewColumnSortMode.NotSortable;
            _dgv.Columns[(int)ColumnasGrid.IngresosNI].SortMode = DataGridViewColumnSortMode.NotSortable;
            _dgv.Columns[(int)ColumnasGrid.SalidasNI].SortMode = DataGridViewColumnSortMode.NotSortable;
            _dgv.Columns[(int)ColumnasGrid.IngresosNIV].SortMode = DataGridViewColumnSortMode.NotSortable;
            _dgv.Columns[(int)ColumnasGrid.SalidasNIV].SortMode = DataGridViewColumnSortMode.NotSortable;
          
        }

        private void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Cargo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Abono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Cargo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Abono].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
        }

        private void SaldosNI_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                dataGridView1.Columns[1].HeaderText = "Movimientos al " + dateTimePicker1.Value.ToShortDateString();
                dataGridView1.Columns[2].HeaderText = "No Identificados \r\n " + dateTimePicker1.Value.ToShortDateString();
                dataGridView1.Columns[3].HeaderText = "No Identificados vencidos " + dateTimePicker1.Value.ToShortDateString();

                SqlCommand ni = new SqlCommand("PJ_ScoreCardCobranza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                ni.CommandType = CommandType.StoredProcedure;
                ni.Parameters.AddWithValue("@TipoConsulta", 6);
                ni.Parameters.AddWithValue("@Sucursales", string.Empty);
                ni.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                ni.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);
                ni.Parameters.AddWithValue("@FechaFinal", string.Empty);
                ni.Parameters.AddWithValue("@Sucursal", string.Empty);
                ni.CommandTimeout = 0;

                DataTable tabla_ni = new DataTable();
                SqlDataAdapter adapter_ni = new SqlDataAdapter();
                adapter_ni.SelectCommand = ni;
                adapter_ni.Fill(tabla_ni);

                gridNI.DataSource = tabla_ni;

                DataRow _rowNi = tabla_ni.NewRow();
                _rowNi["Sucursal"] = "Total";
                _rowNi["No identificado"] = tabla_ni.Compute("SUM([No identificado])", "");
                _rowNi["No Identificado vencido"] = tabla_ni.Compute("SUM([No Identificado vencido])", "");
                _rowNi["IngresosNI"] = tabla_ni.Compute("SUM([IngresosNI])", "");
                _rowNi["SalidasNI"] = tabla_ni.Compute("SUM([SalidasNI])", "");
                _rowNi["IngresosNIV"] = tabla_ni.Compute("SUM([IngresosNIV])", "");
                _rowNi["SalidasNIV"] = tabla_ni.Compute("SUM([SalidasNIV])", "");
                tabla_ni.Rows.Add(_rowNi);

                FormatoGrid(gridNI, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaldosNI_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            if(exp.Exportar(gridNI, false))
            {
                MessageBox.Show("El archivo se creo con exíto.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaldosNI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Clases.Logs log = new Clases.Logs(Usuario, this.AccessibleDescription, IDLog);
            log.Fin();

            e.Cancel = false;
        }

        private void SaldosNI_Shown(object sender, EventArgs e)
        {
            try
            {
                Clases.Logs log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
                IDLog = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void gridNI_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void gridNI_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string Cuenta = gridNI.Rows[e.RowIndex].Cells[(int)ColumnasGrid.Cta].Value.ToString();
                if (!string.IsNullOrEmpty(Cuenta))
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", 10);
                                command.Parameters.AddWithValue("@Cuenta", Cuenta);
                                command.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);

                                command.CommandTimeout = 0;

                                DataTable table = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter();
                                da.SelectCommand = command;
                                da.Fill(table);

                                dataGridView2.DataSource = table;

                                this.Formato(dataGridView2);

                                label2.Text = "Operaciones no reconciliadas:" + gridNI.Rows[e.RowIndex].Cells[(int)ColumnasGrid.Sucursal].Value.ToString();
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }


    }
}
