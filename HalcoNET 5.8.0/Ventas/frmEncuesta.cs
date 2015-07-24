using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas
{
    public partial class frmEncuesta : Form
    {
        private string Cliente = string.Empty;
        public frmEncuesta()
        {
            InitializeComponent();
        }

        public void getData()
        {
            dgvDatos.DataSource = null;
            dgvDatos.Columns.Clear();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_Encuesta", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                    command.Parameters.AddWithValue("@Rol", ClasesSGUV.Login.Rol);
                    command.Parameters.AddWithValue("@SlpCode", clbVendedor.SelectedValue);

                    DataTable tbl = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(tbl);

                    //tbl.Columns.Add("Cantidad", typeof(Int32));

                    dgvDatos.DataSource = tbl;

                    if (tbl.Columns.Count <= 0)
                    {
                        MessageBox.Show("El cliente no existe o no esta asignado en la cartera del vendedor seleccionado.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    dgvDatos.Columns[0].Visible = false;
                    dgvDatos.Columns[1].Width = 180;
                    dgvDatos.Columns[1].ReadOnly = true;
                    dgvDatos.Columns[2].Width = 75;
                    dgvDatos.Columns[2].DefaultCellStyle.Format = "N0";
                    dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        private void CargarVendedores()
        {
            if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal);
                command.Parameters.AddWithValue("@SlpCode", ClasesSGUV.Login.Vendedor1);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "Selecciona un vendedor";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            else //if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 11);
                command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal);
                command.Parameters.AddWithValue("@SlpCode", ClasesSGUV.Login.Vendedor1);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "Selecciona un vendedor";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }

            clbVendedor.SelectedValue = ClasesSGUV.Login.Vendedor1;
        }       

        private void frmEncuesta_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                this.CargarVendedores();
                clbVendedor_SelectionChangeCommitted(sender, e);
                if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                    clbVendedor.Enabled = false;
                
                txtCliente.Focus();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_Encuesta", connection))
                    {
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                        command.Parameters.AddWithValue("@Rol", ClasesSGUV.Login.Rol);
                        command.Parameters.AddWithValue("@SlpCode", clbVendedor.SelectedValue);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            Cliente = reader[0].ToString();
                            lblMsg.Text = "Ingresa el número de vehículos que tiene el cliente: \r\n" + reader[1].ToString();

                            this.getData();

                            dgvDatos.Focus();
                        }
                        else
                        {

                            lblMsg.Text = "Ingresa el número de vehículos que tiene el cliente: \r\n";
                            dgvDatos.DataSource = null;
                            dgvDatos.Columns.Clear();

                            using (SqlConnection connection2 = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                            {
                                using (SqlCommand command2 = new SqlCommand("sp_Encuesta", connection2))
                                {
                                    command2.CommandTimeout = 0;
                                    command2.CommandType = CommandType.StoredProcedure;

                                    command2.Parameters.AddWithValue("@TipoConsulta", 7);
                                    command2.Parameters.AddWithValue("@Cliente", txtCliente.Text);

                                    connection2.Open();

                                    string agrupador = command2.ExecuteScalar().ToString();
                                    if (!string.IsNullOrEmpty(agrupador))
                                    {
                                        if (MessageBox.Show("El código de cliente: " + txtCliente.Text + " se cambiara por el agrupador: " + agrupador + " ¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                                            txtCliente.Text = agrupador;
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtCliente.Focus();
            txtCliente.Text = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvDatos.Rows)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_Encuesta", connection))
                        {
                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 3);
                            command.Parameters.AddWithValue("@Cliente", Cliente);
                            command.Parameters.AddWithValue("@Tipo", row.Cells[0].Value);
                            command.Parameters.AddWithValue("@Cantidad", row.Cells[2].Value);


                            connection.Open();

                            command.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Registro exitoso!", "HalcoNet", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "HalcoNet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clbVendedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_Encuesta", connection))
                        {
                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 4);
                            command.Parameters.AddWithValue("@SlpCode", clbVendedor.SelectedValue);

                            DataTable table = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            dataGridView1.DataSource = table;

                            dataGridView1.Columns[0].Width = 90;
                            dataGridView1.Columns[1].Width = 85;
                            dataGridView1.Columns[2].Width = 85;
                        }
                    }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
