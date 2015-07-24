using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza
{
    public partial class Reporte_MEP_Atradius : Form
    {
        Clases.Logs log;

        public Reporte_MEP_Atradius()
        {
            InitializeComponent();
        }

        public enum ColumasEncabezado
        {
            Cliente,
            Nombre,
            Moneda,
            Sucursal,
            CondicionCredito,
            LimCredito,
            Saldo,
            Plazo1,
            Plazo2,
            Plazo3,
            Prorroga1,
            Prorroga2, 
            Reclamacion,
            Plazo4,
            Plazo5,
            CD,
            Siniestro
           
        }

        DataTable encabezado = new DataTable();
        DataTable detalle = new DataTable();
        DataSet data = new DataSet();
        BindingSource masterBindingSource = new BindingSource();
        BindingSource detailsBindingSource = new BindingSource();

        public enum ColumnasDetalle
        {
            CardCode,
            CardName,
            Factura,
            Fecha,
            Vto,
            Monto,
            Dias,
            Saldo,
            Prorroga1,
            Prorroga2,
            Reclamacion,
            ExistP1,
            ExistP2,
            EsistRE,
            DocEntry,
            DP1,
            DP2,
            FechaConclusion,
            ExisteJ1,
            Juridico
        }

        public void Formato1(DataGridView dgv, bool _columEnabled)
        {
            dgv.Columns[(int)ColumasEncabezado.CD].Visible = false;

            dgv.Columns[(int)ColumasEncabezado.Cliente].ReadOnly = _columEnabled;
            dgv.Columns[(int)ColumasEncabezado.Moneda].ReadOnly = _columEnabled;
            dgv.Columns[(int)ColumasEncabezado.Nombre].ReadOnly = _columEnabled;
            dgv.Columns[(int)ColumasEncabezado.CondicionCredito].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.LimCredito].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Saldo].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Plazo1].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Plazo2].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Plazo3].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Plazo4].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Plazo5].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Sucursal].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Prorroga1].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Prorroga2].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Reclamacion].ReadOnly = true;
            dgv.Columns[(int)ColumasEncabezado.Siniestro].ReadOnly = true;

            dgv.Columns[(int)ColumasEncabezado.Cliente].Width = 80;
            dgv.Columns[(int)ColumasEncabezado.Moneda].Width = 70;
            dgv.Columns[(int)ColumasEncabezado.Nombre].Width = 220;
            dgv.Columns[(int)ColumasEncabezado.CondicionCredito].Width = 90;
            dgv.Columns[(int)ColumasEncabezado.LimCredito].Width = 90;
            dgv.Columns[(int)ColumasEncabezado.Saldo].Width = 90;
            dgv.Columns[(int)ColumasEncabezado.Plazo1].Width = 90;
            dgv.Columns[(int)ColumasEncabezado.Plazo2].Width = 90;
            dgv.Columns[(int)ColumasEncabezado.Plazo3].Width = 90;
            dgv.Columns[(int)ColumasEncabezado.Plazo4].Width = 90;
            dgv.Columns[(int)ColumasEncabezado.Plazo5].Width = 90;

            dgv.Columns[(int)ColumasEncabezado.LimCredito].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasEncabezado.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasEncabezado.Plazo1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasEncabezado.Plazo2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasEncabezado.Plazo3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasEncabezado.Plazo4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasEncabezado.Plazo5].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasEncabezado.Siniestro].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (cbFiltro.SelectedValue.ToString() == "A" )
            {
                dgv.Columns[(int)ColumasEncabezado.Prorroga1].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumasEncabezado.Prorroga2].DefaultCellStyle.Format = "C2";
                dgv.Columns[(int)ColumasEncabezado.Reclamacion].DefaultCellStyle.Format = "C2";

                dgv.Columns[(int)ColumasEncabezado.Prorroga1].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.Columns[(int)ColumasEncabezado.Prorroga2].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.Columns[(int)ColumasEncabezado.Reclamacion].DefaultCellStyle.BackColor = Color.LightGray;

                dgv.Columns[(int)ColumasEncabezado.Prorroga1].Visible = true;
                dgv.Columns[(int)ColumasEncabezado.Prorroga2].Visible = true;
                dgv.Columns[(int)ColumasEncabezado.Reclamacion].Visible = true;

                btnGuardar.Visible = true;
            }
            else
            {
                btnGuardar.Visible = false;
                dgv.Columns[(int)ColumasEncabezado.Prorroga1].Visible = false;
                dgv.Columns[(int)ColumasEncabezado.Prorroga2].Visible = false;
                dgv.Columns[(int)ColumasEncabezado.Reclamacion].Visible = false;

                if (cbFiltro.SelectedValue.ToString() == "B")
                {
                    dgv.Columns[(int)ColumasEncabezado.Prorroga1].DefaultCellStyle.Format = "C2";
                    dgv.Columns[(int)ColumasEncabezado.Prorroga2].DefaultCellStyle.Format = "C2";
                    dgv.Columns[(int)ColumasEncabezado.Reclamacion].DefaultCellStyle.Format = "C2";

                    dgv.Columns[(int)ColumasEncabezado.Prorroga1].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.Columns[(int)ColumasEncabezado.Prorroga2].DefaultCellStyle.BackColor = Color.LightGray;
                    dgv.Columns[(int)ColumasEncabezado.Reclamacion].DefaultCellStyle.BackColor = Color.LightGray;

                    dgv.Columns[(int)ColumasEncabezado.Prorroga1].Visible = true;
                    dgv.Columns[(int)ColumasEncabezado.Prorroga2].Visible = true;
                    dgv.Columns[(int)ColumasEncabezado.Reclamacion].Visible = true;

                    btnGuardar.Visible = true;

                    masterBindingSource.Filter = string.Format("CD = '{0}'", "Y");
                }
                if (cbFiltro.SelectedValue.ToString() == "C")
                    masterBindingSource.Filter = string.Format("ISNULL(CD,'N') <> '{0}'", "Y");
            }

            dgv.Columns[(int)ColumasEncabezado.Prorroga1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasEncabezado.Prorroga2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasEncabezado.Reclamacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumasEncabezado.LimCredito].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasEncabezado.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasEncabezado.Plazo1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasEncabezado.Plazo2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasEncabezado.Plazo3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasEncabezado.Plazo4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasEncabezado.Plazo5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void Formato2(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasDetalle.ExistP1].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.ExistP2].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.DocEntry].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.EsistRE].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.ExisteJ1].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.CardName].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.FechaConclusion].Visible = false;


            if (cbFiltro.SelectedValue.ToString() == "A" || cbFiltro.SelectedValue.ToString() == "B")
            {
                dgv.Columns[(int)ColumnasDetalle.Prorroga1].Width = 80;
                dgv.Columns[(int)ColumnasDetalle.Prorroga2].Width = 80;
                dgv.Columns[(int)ColumnasDetalle.Reclamacion].Width = 80;
                dgv.Columns[(int)ColumnasDetalle.Juridico].Width = 80;

                dgv.Columns[(int)ColumnasDetalle.Prorroga1].Visible = true;
                dgv.Columns[(int)ColumnasDetalle.Prorroga2].Visible = true;
                dgv.Columns[(int)ColumnasDetalle.Reclamacion].Visible = true;
                dgv.Columns[(int)ColumnasDetalle.Juridico].Visible = true;
            }
            else
            {
                dgv.Columns[(int)ColumnasDetalle.Prorroga1].Visible = false;
                dgv.Columns[(int)ColumnasDetalle.Prorroga2].Visible = false;
                dgv.Columns[(int)ColumnasDetalle.Juridico].Visible = false;
                dgv.Columns[(int)ColumnasDetalle.Reclamacion].Visible = false;
                dgv.Columns[(int)ColumnasDetalle.DP1].Visible = false;
                dgv.Columns[(int)ColumnasDetalle.DP2].Visible = false;
            }

            dgv.Columns[(int)ColumnasDetalle.CardCode].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.Factura].Width = 90;
            dgv.Columns[(int)ColumnasDetalle.Fecha].Width = 90;
            dgv.Columns[(int)ColumnasDetalle.Vto].Width = 90;
            dgv.Columns[(int)ColumnasDetalle.Monto].Width = 90;
            dgv.Columns[(int)ColumnasDetalle.Dias].Width = 90;
            dgv.Columns[(int)ColumnasDetalle.Saldo].Width = 90;

            dgv.Columns[(int)ColumnasDetalle.Factura].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.Fecha].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.Vto].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.Monto].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.Dias].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.Saldo].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.DP1].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.DP2].ReadOnly = true;

            dgv.Columns[(int)ColumnasDetalle.Monto].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.Dias].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasDetalle.Saldo].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasDetalle.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasDetalle.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public DataTable Source(int _tipoConsulta, DateTime _fecha)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_AtradiusP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", _tipoConsulta);
                    command.Parameters.AddWithValue("@Desde", _fecha);
                    command.Parameters.AddWithValue("@Hasta", _fecha);
                    command.Parameters.AddWithValue("@CardCode", string.Empty);
                    command.Parameters.AddWithValue("@CardName", cbFiltro.SelectedValue  == null ? string.Empty : cbFiltro.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@DocEntry", string.Empty);
                    command.Parameters.AddWithValue("@DocNum", string.Empty);

                    SqlParameter ValidaUsuario = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                    ValidaUsuario.Direction = ParameterDirection.Output;
                    command.Parameters.Add(ValidaUsuario);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.SelectCommand.CommandTimeout = 0;

                    da.Fill(table);
                }
            }

            return table;
        }

        public string ExcecuteNonQuery(int _tipoConsulta, DateTime _fecha, string _cardCode, string _cardName, int _docentry, int _docNum, string _prorrog, DateTime _siniestro)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_AtradiusP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", _tipoConsulta);
                    command.Parameters.AddWithValue("@Desde", _fecha);
                    command.Parameters.AddWithValue("@Hasta", _fecha);
                    command.Parameters.AddWithValue("@CardCode", _prorrog);
                    command.Parameters.AddWithValue("@CardName", _cardName);
                    command.Parameters.AddWithValue("@DocEntry", _docentry);
                    command.Parameters.AddWithValue("@DocNum", _docNum);
                    if(_prorrog.Equals("RE"))
                        command.Parameters.AddWithValue("@FechaSiniestro", _siniestro);

                    SqlParameter ValidaUsuario = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                    ValidaUsuario.Direction = ParameterDirection.Output;
                    command.Parameters.Add(ValidaUsuario);
                    connection.Open();

                    command.ExecuteNonQuery();

                    string _validat = Convert.ToString(command.Parameters["@Mensaje"].Value.ToString());
                    return _validat;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                data = null;
                data = new DataSet();
                detailsBindingSource = null;
                detailsBindingSource= new BindingSource();
                masterBindingSource = null;
                masterBindingSource = new BindingSource();
                dgvDetalle.DataSource = null;
                dgvEncabezado.DataSource = null;
                dgvTotales.DataSource = null;


                encabezado = Source(6, dateTimePicker1.Value);

                detalle = Source(7, dateTimePicker1.Value);
                encabezado.TableName = "TablaVendedores";
                detalle.TableName = "TablaClientes";


                data.Tables.Add(encabezado);
                data.Tables.Add(detalle);

                DataColumn cp = data.Tables["TablaVendedores"].Columns["Cliente"];
                DataColumn cc = data.Tables["TablaClientes"].Columns["CardCode"];

                DataRelation relation = new DataRelation("VendedoresCliente", cp, cc);
                data.Relations.Add(relation);

                masterBindingSource.DataSource = data;
                masterBindingSource.DataMember = "TablaVendedores";
                detailsBindingSource.DataSource = masterBindingSource;
                detailsBindingSource.DataMember = "VendedoresCliente";
                dgvEncabezado.DataSource = masterBindingSource;
                dgvDetalle.DataSource = detailsBindingSource;

                this.Formato1(dgvEncabezado, true);
                this.Formato2(dgvDetalle);

                //TOTALES
                DataTable totales = new DataTable();
                totales = encabezado.Copy();
                totales.Clear();

                DataRow row = totales.NewRow();
                row["Cliente"] = string.Empty;
                row["Nombre"] = string.Empty;
                row["Saldo actual"] = Convert.ToDecimal(encabezado.Compute("SUM([Saldo actual])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([Saldo actual])", string.Empty));
                row["Dentro de plazo de crédito"] = Convert.ToDecimal(encabezado.Compute("SUM([Dentro de plazo de crédito])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([Dentro de plazo de crédito])", string.Empty));
                row["Dentro de periodo de prorroga (MEP)"] = Convert.ToDecimal(encabezado.Compute("SUM([Dentro de periodo de prorroga (MEP)])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([Dentro de periodo de prorroga (MEP)])", string.Empty));
                row["Dentro de periodo de reclamación"] = Convert.ToDecimal(encabezado.Compute("SUM([Dentro de periodo de reclamación])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([Dentro de periodo de reclamación])", string.Empty));
                row["Prorroga 1"] = Convert.ToDecimal(encabezado.Compute("SUM([Prorroga 1])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([Prorroga 1])", string.Empty));
                row["Prorroga 2"] = Convert.ToDecimal(encabezado.Compute("SUM([Prorroga 2])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([Prorroga 2])", string.Empty));
                row["En reclamación"] = Convert.ToDecimal(encabezado.Compute("SUM([En reclamación])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([En reclamación])", string.Empty));
                row["Fuera del periodo de reclamación"] = Convert.ToDecimal(encabezado.Compute("SUM([Fuera del periodo de reclamación])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([Fuera del periodo de reclamación])", string.Empty));
                row["Incobrable(Fecha de pérdida)"] = Convert.ToDecimal(encabezado.Compute("SUM([Incobrable(Fecha de pérdida)])", string.Empty) == DBNull.Value ? decimal.Zero : encabezado.Compute("SUM([Incobrable(Fecha de pérdida)])", string.Empty));
                
                totales.Rows.Add(row);

                dgvTotales.DataSource = totales;

                dgvTotales.Rows[0].Cells[0].Value = string.Empty;
                dgvTotales.Rows[0].Cells[1].Value = string.Empty;


                this.Formato1(dgvTotales, false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.Dias].Value) == 0)
                    {
                        item.Cells[(int)ColumnasDetalle.Dias].Style.BackColor = Color.Green;
                        item.Cells[(int)ColumnasDetalle.Dias].Style.ForeColor = Color.Black;
                    }
                    else
                        if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.Dias].Value) > 0
                            && Convert.ToInt32(item.Cells[(int)ColumnasDetalle.Dias].Value) <= 90)
                        {
                            item.Cells[(int)ColumnasDetalle.Dias].Style.BackColor = Color.Yellow;
                            item.Cells[(int)ColumnasDetalle.Dias].Style.ForeColor = Color.Black;
                        }
                        else if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.Dias].Value) > 90)
                        {
                            item.Cells[(int)ColumnasDetalle.Dias].Style.BackColor = Color.Red;
                            item.Cells[(int)ColumnasDetalle.Dias].Style.ForeColor = Color.White;
                        }

                //    if (item.Cells[(int)ColumnasDetalle.DP1].Value != DBNull.Value)
                //        if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.DP1].Value) > 15)
                //        {
                //            item.Cells[(int)ColumnasDetalle.DP1].Style.BackColor = Color.Green;
                //            item.Cells[(int)ColumnasDetalle.DP1].Style.ForeColor = Color.Black;
                //        }
                //        else if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.DP1].Value) < 15
                //            && Convert.ToInt32(item.Cells[(int)ColumnasDetalle.DP1].Value) > 1)
                //        {
                //            item.Cells[(int)ColumnasDetalle.DP1].Style.BackColor = Color.Yellow;
                //            item.Cells[(int)ColumnasDetalle.DP1].Style.ForeColor = Color.Black;
                //        }
                //        else if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.DP1].Value) <= 1)
                //        {
                //            item.Cells[(int)ColumnasDetalle.DP1].Style.BackColor = Color.Red;
                //            item.Cells[(int)ColumnasDetalle.DP1].Style.ForeColor = Color.White;
                //        }

                //    if (item.Cells[(int)ColumnasDetalle.DP2].Value != DBNull.Value)
                //        if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.DP2].Value) > 15)
                //        {
                //            item.Cells[(int)ColumnasDetalle.DP2].Style.BackColor = Color.Green;
                //            item.Cells[(int)ColumnasDetalle.DP2].Style.ForeColor = Color.Black;
                //        }
                //        else if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.DP2].Value) < 15
                //            && Convert.ToInt32(item.Cells[(int)ColumnasDetalle.DP2].Value) > 1)
                //        {
                //            item.Cells[(int)ColumnasDetalle.DP2].Style.BackColor = Color.Yellow;
                //            item.Cells[(int)ColumnasDetalle.DP2].Style.ForeColor = Color.Black;
                //        }
                //        else if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.DP2].Value) <= 1)
                //        {
                //            item.Cells[(int)ColumnasDetalle.DP2].Style.BackColor = Color.Red;
                //            item.Cells[(int)ColumnasDetalle.DP2].Style.ForeColor = Color.White;
                //        }

                }
            }
            catch (Exception)
            {
                
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            if (exp.Exportar(dgvEncabezado, false))
                MessageBox.Show("El archivo se creó exitosamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvEncabezado_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            try
            {
                dgvTotales.Columns[e.Column.Index].Width = (sender as DataGridView).Columns[e.Column.Index].Width;
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvEncabezado_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)ColumasEncabezado.LimCredito].Value) < Convert.ToDecimal(item.Cells[(int)ColumasEncabezado.Saldo].Value))
                    {
                        item.Cells[(int)ColumasEncabezado.Saldo].Style.BackColor = Color.Red;
                        item.Cells[(int)ColumasEncabezado.Saldo].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)ColumasEncabezado.Saldo].Style.BackColor = Color.White;
                        item.Cells[(int)ColumasEncabezado.Saldo].Style.ForeColor = Color.Black;
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        private void Reporte_MEP_Atradius_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                btnGuardar.Visible = false;

                DataTable filtros = new DataTable();
                filtros = Source(8, dateTimePicker1.Value);

                cbFiltro.DataSource = filtros;
                cbFiltro.DisplayMember = "Nombre";
                cbFiltro.ValueMember = "Codigo";

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string _CardCode = string.Empty;
                string _Docnum = string.Empty;
                bool fist = false;

                _CardCode = dgvEncabezado.CurrentRow.Cells[(int)ColumasEncabezado.Cliente].Value.ToString();
                _Docnum = dgvDetalle.CurrentRow.Cells[(int)ColumnasDetalle.Factura].Value.ToString(); 

                string _mensaje = string.Empty;
                DialogResult _result = System.Windows.Forms.DialogResult.No;

                foreach (DataRow item in detalle.Rows)
                {
                    if (item.Field<string>("ExistP1") == "N" && item.Field<bool>("Prorroga1") == true)
                    {
                        string _aux = ExcecuteNonQuery(9, dateTimePicker1.Value, string.Empty, string.Empty, item.Field<int>("DocEntry"), item.Field<int>("Factura"), "P1", DateTime.Now);
                        _mensaje += "Factura: " + item.Field<int>("Factura") + " Estado: " + _aux + "\r\n";
                        _result = System.Windows.Forms.DialogResult.Yes;
                    }
                    if (item.Field<string>("ExistP2") == "N" && item.Field<bool>("Prorroga2") == true)
                    {
                        string _aux = ExcecuteNonQuery(9, dateTimePicker1.Value, string.Empty, string.Empty, item.Field<int>("DocEntry"), item.Field<int>("Factura"), "P2", DateTime.Now);
                        _mensaje += "Factura: " + item.Field<int>("Factura") + " Estado: " + _aux + "\r\n";
                        _result = System.Windows.Forms.DialogResult.Yes;

                    }
                    //SI EXISTE ALGUN CLIENTE EN JURIDICO
                    if (item.Field<string>("ExistJ1") == "N" && item.Field<bool>("Juridico") == true)
                    {
                        string _aux = ExcecuteNonQuery(9, dateTimePicker1.Value, string.Empty, string.Empty, item.Field<int>("DocEntry"), item.Field<int>("Factura"), "J1", DateTime.Now);
                        _mensaje += "Factura: " + item.Field<int>("Factura") + " Estado: " + _aux + "\r\n";
                        _result = System.Windows.Forms.DialogResult.Yes;

                    }

                    if (item.Field<string>("ExistRE") == "N" && item.Field<bool>("Reclamación") == true)
                    {
                        string clientes = string.Empty;

                        int countClientes = 0;
                        if (_result == System.Windows.Forms.DialogResult.No)
                        {

                            var qry = (from fa in detalle.AsEnumerable()
                                      where fa.Field<string>("ExistRE") == "N" && fa.Field<bool>("Reclamación") == true
                                      select fa.Field<string>("CardCode")).Distinct();

                            countClientes = qry.Count();

                            foreach (string r in qry)
                            {
                                clientes += r + "\r\n";

                            }

                            if(!fist)
                            _result = MessageBox.Show("Se enviarán los siguientes clientes a Reclamación\r\n" + clientes + "¿Desea continuar?" , "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        }
                        fist = true;

                        if (_result == System.Windows.Forms.DialogResult.Yes)
                        {

                            string _aux = ExcecuteNonQuery(9, dateTimePicker1.Value, string.Empty, string.Empty, item.Field<int>("DocEntry"), item.Field<int>("Factura"), "RE", item.Field<DateTime>("Fecha siniestro"));
                            _mensaje += "Factura: " + item.Field<int>("Factura") + " Estado: " + _aux + "\r\n";
                        }

                    }

                }

                string clientesMail = string.Empty;
                var qryMail = (from fa in detalle.AsEnumerable()
                               where fa.Field<string>("ExistRE") == "N" && fa.Field<bool>("Reclamación") == true
                               select new
                               {
                                   CardCode = fa.Field<string>("CardCode"),
                                   CardName = fa.Field<string>("CardName")
                               }).Distinct();

                foreach (DataRow r in qryMail.ToDataTable().Rows)
                {
                    Clases.CrearPDF pdf = new Clases.CrearPDF();
                    string file = pdf.ToPDFAtradius(r.Field<string>("CardCode"), "Cliente a reclamación:\r\n" + r.Field<string>("CardCode") + " - " + r.Field<string>("CardName") + "\r\n\r\nFacturas a reclamación");

                    string subj = "ATRADIUS - Reclamación - " + r.Field<string>("CardCode") + " - " + r.Field<string>("CardName");

                    SendMail mail = new SendMail();
                    string _correosDestinatarios = System.Configuration.ConfigurationSettings.AppSettings["CorreosAtradius"].ToString();
                    if (file != string.Empty)
                        mail.Enviar(file, _correosDestinatarios, "halconet@pj.com.mx", @"<b>---</b>", subj, true);

                }

                if (_result == System.Windows.Forms.DialogResult.Yes)
                {
                    var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
                    var dialogType = typeof(Form).Assembly.GetType(dialogTypeName);

                    var dialog = (Form)Activator.CreateInstance(dialogType, new PropertyGrid());

                    dialog.Text = "HalcoNET";

                    dialogType.GetProperty("Details").SetValue(dialog, _mensaje, null);
                    dialogType.GetProperty("Message").SetValue(dialog, "Operación completada. De click en el boton 'Detalles' para mayor información.", null);

                    var result = dialog.ShowDialog();
                    
                    button1_Click(sender, e);

                    int indexEncabezado = 0;
                    foreach (DataGridViewRow item in dgvEncabezado.Rows)
                    {
                        if (item.Cells[(int)ColumasEncabezado.Cliente].Value.ToString() == _CardCode)
                            indexEncabezado = item.Index;
                    }

                    dgvEncabezado.CurrentCell = dgvEncabezado.Rows[indexEncabezado].Cells[(int)ColumasEncabezado.Cliente];

                    int indexDetalle = 0;
                    foreach (DataGridViewRow item in dgvDetalle.Rows)
                    {
                        if (item.Cells[(int)ColumnasDetalle.Factura].Value.ToString() == _Docnum)
                            indexDetalle = item.Index;
                    }

                    dgvDetalle.CurrentCell = dgvDetalle.Rows[indexDetalle].Cells[(int)ColumnasDetalle.Factura];
                }
            }
            catch (Exception ex)
            {
                var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
                var dialogType = typeof(Form).Assembly.GetType(dialogTypeName);

                var dialog = (Form)Activator.CreateInstance(dialogType, new PropertyGrid());

                dialog.Text = "HalcoNET";
                dialogType.GetProperty("Details").SetValue(dialog, ex.Message, null);
                dialogType.GetProperty("Message").SetValue(dialog, "Error inesperado", null);

                var result = dialog.ShowDialog();
            }
        }

        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool value =Convert.ToBoolean((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Reclamacion].Value);
                if (e.ColumnIndex == (int)ColumnasDetalle.Reclamacion)
                {
                    bool _siniestro = false;
                    frmFechaSiniestro frm = new frmFechaSiniestro();
                    foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                    {
                        if (value == false)
                        {
                            item.Cells[(int)ColumnasDetalle.Reclamacion].Value = true;
                            if (!_siniestro)
                            {
                                
                                DialogResult resul = frm.ShowDialog();
                                if (resul == DialogResult.OK)
                                {
                                    item.Cells[(int)ColumnasDetalle.FechaConclusion].Value = frm.Fecha;
                                    _siniestro = true;
                                }
                              
                            }
                            else
                            if (_siniestro)
                                item.Cells[(int)ColumnasDetalle.FechaConclusion].Value = frm.Fecha;
                        }
                        else
                        {
                            item.Cells[(int)ColumnasDetalle.Reclamacion].Value = false;
                            item.Cells[(int)ColumnasDetalle.FechaConclusion].Value = DBNull.Value;
                            
                        }

                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void Reporte_MEP_Atradius_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void Reporte_MEP_Atradius_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void dgvTotales_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];

                    masterBindingSource.Filter = string.Format("Cliente LIKE '%{0}%' AND Nombre LIKE '%{1}%'", row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgvEncabezado_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvDetalle_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvEncabezado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
