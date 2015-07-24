using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pagos.ReportesHalcoNET
{
    public partial class Logs : Form
    {
        DataTable Datos = new DataTable();
        public Logs()
        {
            InitializeComponent();
        }

        private enum Columnas
        {
            Usuario,
            Rol,
            Sucursal,
            Reporte,
            Ingreso,
        }

        public void Formato()
        {
            dgvDatos.Columns[(int)Columnas.Usuario].Width = 100;
            dgvDatos.Columns[(int)Columnas.Rol].Width = 100;
            dgvDatos.Columns[(int)Columnas.Sucursal].Width = 80;
            dgvDatos.Columns[(int)Columnas.Reporte].Width = 250;
            dgvDatos.Columns[(int)Columnas.Ingreso].Width = 70;

            dgvDatos.Columns[(int)Columnas.Ingreso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvDatos.AllowUserToOrderColumns = false;
        }
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                txtUsuario.Text = string.Empty;
                dgvDatos.DataSource = null;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Logs", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@FechaDesde", dtDesde.Value);
                        command.Parameters.AddWithValue("@FechaFinal", dtHasta.Value);
                        command.Parameters.AddWithValue("@Rol", cbRol.SelectedValue);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue);

                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();
                        da.Fill(table);

                        Datos = table.Copy();

                        if (table.Rows.Count > 0)
                        {
                            //List<string> Usuarios = (from item in table.AsEnumerable()
                            //                         select item.Field<string>("Usuario")).Distinct().ToList();

                            //foreach (string usuario in Usuarios)
                            //{
                            //    DataRow newRow = table.NewRow();

                            //    newRow.SetField("Usuario", usuario + " Total");

                            //    Int32 total = Convert.ToInt32(table.Compute("SUM(Ingresos)", "Usuario='" + usuario + "'"));
                            //    newRow.SetField("Ingresos", total);

                            //    table.Rows.Add(newRow);
                            //}



                            dgvDatos.DataSource = (from row in table.AsEnumerable()
                                                 orderby row.Field<string>("Usuario")
                                                 select row).CopyToDataTable();

                            Formato();

                            lblTotal.Text = "Total de ingresos: " + table.Compute("Sum(Ingresos)", string.Empty);
                        }
                        else
                        {
                            lblTotal.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Logs_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Logs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("TipoConsulta", 2);
                    command.Parameters.AddWithValue("FechaDesde", dtDesde.Value);
                    command.Parameters.AddWithValue("FechaFinal", dtHasta.Value);
                    command.Parameters.AddWithValue("Rol", ClasesSGUV.Login.Rol);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);

                    command.CommandTimeout = 0;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataTable table = new DataTable();
                    da.Fill(table);

                    cbRol.DataSource = table;
                    cbRol.DisplayMember = "Nombre";
                    cbRol.ValueMember = "Rol";
                }
            }

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Logs", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("TipoConsulta", 3);
                    command.Parameters.AddWithValue("FechaDesde", dtDesde.Value);
                    command.Parameters.AddWithValue("FechaFinal", dtHasta.Value);
                    command.Parameters.AddWithValue("Rol", 0);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);

                    command.CommandTimeout = 0;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataTable table = new DataTable();
                    da.Fill(table);

                    cbSucursal.DataSource = table;
                    cbSucursal.DisplayMember = "Sucursal";
                    cbSucursal.ValueMember = "Sucursal";
                }
            }
        }

        private void dgvMXP_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    Font font = item.DefaultCellStyle.Font;

                    if (Convert.ToString(item.Cells[(int)Columnas.Usuario].Value).Contains("Total"))
                    {
                        item.DefaultCellStyle.BackColor = Color.LightGray;
                        //Microsoft Sans Serif, 8.25pt
                        item.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", (float)8.25, FontStyle.Bold);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable t = new DataTable();
                t = (from item in Datos.AsEnumerable()
                     where item.Field<string>("Usuario").ToLower().Contains(txtUsuario.Text.ToLower())
                     orderby item.Field<string>("Usuario")
                     select item).CopyToDataTable();

                dgvDatos.DataSource = t;

                /*
                 DataTable _t = (from item in Datos.AsEnumerable()
                                where item.Field<string>("Código de cliente").ToLower().Contains(txtCliente.Text.ToLower())
                                // && item.Field<string>("Nombre").ToLower().Contains(textBox2.Text.ToLower())
                                select item).CopyToDataTable();
                 */
                Formato();

                lblTotal.Text = "Total de ingresos: " + t.Compute("Sum(Ingresos)", string.Empty);
            }
            catch (Exception)
            {
               
            }
        }
    }
}
