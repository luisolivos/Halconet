using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presupuesto
{
    public partial class frmPerdidaGanancia : Form
    {
        List<string> ListaCuentas = new List<string>();
        List<string> ListaCuentasDetalle = new List<string>();

        public frmPerdidaGanancia()
        {
            InitializeComponent();
            ListaCuentas.Add("7100-100-001");
            ListaCuentas.Add("7100-100-002");
            ListaCuentas.Add("7100-100-003");
            ListaCuentas.Add("7100-100-004");
            ListaCuentas.Add("7100-100-006");

            ListaCuentas.Add("7200-100-001");
            ListaCuentas.Add("7200-100-002");
            ListaCuentas.Add("7200-100-003");
            ListaCuentas.Add("7200-100-006");

            ListaCuentas.Add("7300-100-001");
            ListaCuentas.Add("7300-100-002");
            ListaCuentas.Add("7300-100-003");
            ListaCuentas.Add("7300-100-004");
            ListaCuentas.Add("7300-100-006");
            ListaCuentas.Add("7300-100-005");

            ListaCuentas.Add("7400-100-001");
            ListaCuentas.Add("7400-100-002");
            ListaCuentas.Add("7400-100-003");
            ListaCuentas.Add("7400-100-004");
            ListaCuentas.Add("7400-100-005");


            ListaCuentasDetalle.Add("7300-100-001");
            ListaCuentasDetalle.Add("7300-100-002");
        }

        private void FormatoDispersiones(string _format, DataGridView dgv)
        {
            int x = 0;
            foreach (DataGridViewColumn item in dgv.Columns)
            {
                if (x > 0)
                {
                    item.Width = 85;
                    item.DefaultCellStyle.Format = _format;
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                x++;
            }
        }

        private void FormatoPrincipal(DataGridView dgv)
        {
            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Visible = false;
            dgv.Columns[2].Width = 85;
            dgv.Columns[3].Width = 220;
            dgv.Columns[4].Width = 90;

            dgv.Columns[4].DefaultCellStyle.Format = "C2";

            dgv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_PerdidaGanancia", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Desde", dtpDesde.Value);
                        command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();
                        da.Fill(table);

                        dgvDispersiones.DataSource = table;
                        this.FormatoDispersiones("P2", dgvDispersiones);
                    }
                }
                /////////////////////////////////////////////////////////////
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_PerdidaGanancia", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@Desde", dtpDesde.Value);
                        command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);
                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();
                        da.Fill(table);

                        DataTable filter = (from fil in table.AsEnumerable()
                                            where ListaCuentas.Contains(fil.Field<string>("Cuenta"))
                                            select fil).CopyToDataTable();

                        var query = (from item in filter.AsEnumerable()
                                     select item.Field<string>("ORD")).Distinct();

                        foreach (var item in query.ToList())
                        {
                            DataRow r = filter.NewRow();
                            r[0] = string.Empty;
                            r[1] = item+" TOTAL";
                            r[2] = string.Empty;
                            r[3] = "TOTAL " + item;
                            r[4] = (from acum in filter.AsEnumerable()
                                                  where acum.Field<string>("ORD") == item
                                                  select acum.Field<decimal>("Monto")).Sum();
                            filter.Rows.Add(r);
                        }

                        DataTable tblfinal = (from tv in filter.AsEnumerable()
                                              orderby tv.Field<string>("ORD")
                                              select tv).CopyToDataTable();

                        dgvPrincipal.DataSource = tblfinal;

                        this.FormatoPrincipal(dgvPrincipal);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void frmPerdidaGanancia_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_PerdidaGanancia",connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Desde", dtpDesde.Value);
                        command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();
                        da.Fill(table);

                        dgvDispersiones.DataSource = table;
                        this.FormatoDispersiones("P2", dgvDispersiones);
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvPrincipal_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow item in dgvPrincipal.Rows)
            {
                if (item.Cells[3].Value.ToString().Contains("TOTAL"))
                {
                    item.DefaultCellStyle.BackColor = Color.FromArgb(68, 84, 106);
                    item.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }
        string cta = string.Empty;

        private void dgvPrincipal_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                lblCuenta.Text = "Cuenta: " + (sender as DataGridView).Rows[e.RowIndex].Cells[1].Value.ToString();
                dgvDetalle.DataSource = null;

                if (!string.IsNullOrEmpty(lblCuenta.Text) )
                {
                    if (ListaCuentasDetalle.Contains((sender as DataGridView).Rows[e.RowIndex].Cells[2].Value.ToString()))
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("sp_PerdidaGanancia", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@TipoConsulta", 3);
                                command.Parameters.AddWithValue("@Desde", dtpDesde.Value);
                                command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);
                                command.Parameters.AddWithValue("@Cuenta", (sender as DataGridView).Rows[e.RowIndex].Cells[2].Value.ToString());

                                SqlDataAdapter da = new SqlDataAdapter();
                                da.SelectCommand = command;
                                DataTable table = new DataTable();
                                da.Fill(table);
                                dgvDetalle.DataSource = null;

                                dgvDetalle.DataSource = table;
                                dgvDetalle.Columns[1].Width = 200;
                                dgvDetalle.Columns[2].DefaultCellStyle.Format = "C2";
                                dgvDetalle.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            }
                        }



                    if (cta != (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString())
                    {
                        cta = (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString();
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("sp_PerdidaGanancia", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@TipoConsulta", 4);
                                command.Parameters.AddWithValue("@Desde", dtpDesde.Value);
                                command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);
                                command.Parameters.AddWithValue("@Cuenta", (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value.ToString());

                                SqlDataAdapter da = new SqlDataAdapter();
                                da.SelectCommand = command;
                                DataTable table = new DataTable();
                                da.Fill(table);

                                DataRow row = table.NewRow();
                                row[0] = "TOTAL";
                                row[1] = Convert.ToDecimal(table.Compute("SUM(APIZACO)", string.Empty));
                                row[2] = Convert.ToDecimal(table.Compute("SUM(CORDOBA)", string.Empty));
                                row[3] = Convert.ToDecimal(table.Compute("SUM(EDOMEX)", string.Empty));
                                row[4] = Convert.ToDecimal(table.Compute("SUM(GDL)", string.Empty));
                                row[5] = Convert.ToDecimal(table.Compute("SUM(MTY)", string.Empty));
                                row[6] = Convert.ToDecimal(table.Compute("SUM(PUEBLA)", string.Empty));
                                row[7] = Convert.ToDecimal(table.Compute("SUM(TEPEACA)", string.Empty));
                                row[8] = Convert.ToDecimal(table.Compute("SUM(SALTILLO)", string.Empty));
                                row[9] = Convert.ToDecimal(table.Compute("SUM(TOTAl)", string.Empty));
                                table.Rows.Add(row);

                                dgvDispersion.DataSource = table;
                                this.FormatoDispersiones("C2", dgvDispersion);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow item in dgvDetalle.Rows)
            {
                if (item.Cells[1].Value.ToString().Contains("TOTAL"))
                {
                    item.DefaultCellStyle.BackColor = Color.FromArgb(68, 84, 106);
                    item.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void dgvDispersion_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow item in dgvDispersion.Rows)
            {
                if (item.Cells[0].Value.ToString().Contains("TOTAL"))
                {
                    item.DefaultCellStyle.BackColor = Color.FromArgb(68, 84, 106);
                    item.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }
    }
}
