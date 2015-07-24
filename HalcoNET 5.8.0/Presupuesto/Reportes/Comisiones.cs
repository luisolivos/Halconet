using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presupuesto
{
    public partial class Comisiones : Form
    {
        Logs log;
        
        public Comisiones()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Nombre, FechaContabilizacion, FechaVto, FolioSap, FolooProv, Monto, Pagos, NC, Saldo
        }

        public void Estatus()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Comisiones";
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Desde", DateTime.Now);
                    command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                    command.Parameters.AddWithValue("@Estatus", string.Empty);

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = command;

                        DataTable table = new DataTable();

                        da.Fill(table);

                        DataRow row = table.NewRow();
                        row["Nombre"] = "Todo";
                        row["Codigo"] = "A";
                        table.Rows.InsertAt(row, 0);

                        cbEstatus.DataSource = table;

                        cbEstatus.DisplayMember = "Nombre";
                        cbEstatus.ValueMember = "Codigo";
                    }
                }
            }
        }

        public decimal Total(int tipoConsulta)
        {
            decimal total = decimal.Zero;
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Comisiones";
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", tipoConsulta);
                    command.Parameters.AddWithValue("@Desde", dtFecha1.Value.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Hasta", dtFecha2.Value.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@Estatus", Convert.ToString(cbEstatus.SelectedValue));

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                        total = reader.GetDecimal(0);

                    return total;
                }
            }
        }

        public void Formato(DataGridView dgv)
        {
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.Columns[(int)Columnas.Nombre].HeaderText = "Nombre";
            dgv.Columns[(int)Columnas.FechaContabilizacion].HeaderText = "Fecha de\r\ncontabilización";
            dgv.Columns[(int)Columnas.FechaVto].HeaderText = "Fecha de\r\nVto";
            dgv.Columns[(int)Columnas.FolioSap].HeaderText = "No Fac\r\nSAP";
            dgv.Columns[(int)Columnas.FolooProv].HeaderText = "No Fac\r\nProv";
            dgv.Columns[(int)Columnas.Monto].HeaderText = "Monto ($)";
            dgv.Columns[(int)Columnas.Saldo].HeaderText = "Saldo\r\npendiente";
            dgv.Columns[(int)Columnas.Pagos].HeaderText = "Pagos\r\naplicados";
            dgv.Columns[(int)Columnas.NC].HeaderText = "NC";

            dgv.Columns[(int)Columnas.Nombre].Width = 200;
            dgv.Columns[(int)Columnas.FechaContabilizacion].Width = 90;
            dgv.Columns[(int)Columnas.FechaVto].Width = 90;
            dgv.Columns[(int)Columnas.FolioSap].Width = 90;
            dgv.Columns[(int)Columnas.FolooProv].Width = 90;
            dgv.Columns[(int)Columnas.Monto].Width = 90;
            dgv.Columns[(int)Columnas.Saldo].Width = 90;
            dgv.Columns[(int)Columnas.Pagos].Width = 90;
            dgv.Columns[(int)Columnas.NC].Width = 90;

            dgv.Columns[(int)Columnas.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Pagos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NC].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Monto].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Pagos].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NC].DefaultCellStyle.Format = "C2";
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "PJ_Comisiones";
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@Desde", dtFecha1.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Hasta", dtFecha2.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Estatus", Convert.ToString(cbEstatus.SelectedValue));

                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            da.SelectCommand = command;

                            DataTable table = new DataTable();

                            da.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                var query = (from item in table.AsEnumerable()
                                             select item.Field<string>("Nombre")).Distinct();


                                foreach (var item in query.ToList())
                                {
                                    DataRow r = table.NewRow();
                                    r["Nombre"] = item + " TOTAL";

                                    r["Monto"] = (from acum in table.AsEnumerable()
                                                  where acum.Field<string>("Nombre") == item
                                                  select acum.Field<decimal>("Monto")).Sum();
                                    r["Pagos"] = (from acum in table.AsEnumerable()
                                                      where acum.Field<string>("Nombre") == item
                                                  select acum.Field<decimal>("Pagos")).Sum();
                                    r["NC"] = (from acum in table.AsEnumerable()
                                                  where acum.Field<string>("Nombre") == item
                                                  select acum.Field<decimal>("NC")).Sum();
                                    r["Saldo"] = (from acum in table.AsEnumerable()
                                                          where acum.Field<string>("Nombre") == item
                                                  select acum.Field<decimal>("Saldo")).Sum();

                                    table.Rows.Add(r);

                                }

                                table = (from tv in table.AsEnumerable()
                                         orderby tv.Field<string>("Nombre")
                                         select tv).CopyToDataTable();

                                decimal total = Convert.ToDecimal(table.Compute("Sum(Saldo)", "Nombre not like '%TOTAL'"));

                                lblSaldo.Text = total.ToString("C2");
                            }
                            dgvImp.DataSource = table;
                            this.Formato(dgvImp);

                            lblMonto.Text = this.Total(3).ToString("C2");
                            //lblPagos.Text = this.Total(4).ToString("C2");
                            //lblSaldo.Text = this.Total(5).ToString("C2");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Comisiones_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                this.Estatus();
            }
            catch (Exception ex )
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvImp_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if(item.Cells[(int)Columnas.Nombre].Value.ToString().Contains("TOTAL"))
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 11, FontStyle.Bold);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Comisiones_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void Comisiones_FormClosing(object sender, FormClosingEventArgs e)
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
