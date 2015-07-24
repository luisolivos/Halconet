using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.Contado
{
    public partial class frm_ReciboPago : Form
    {
        string _cliente;
        DataTable Facturas = new DataTable();

        public frm_ReciboPago()
        {
            InitializeComponent();

            Facturas.Columns.Add(new DataColumn() { ColumnName = "Factura", DataType = typeof(decimal), AllowDBNull = false });
            Facturas.Columns.Add("Saldo", typeof(decimal));
            Facturas.Columns.Add("Importe recibido", typeof(decimal));
        }

        private void CargarSucursales()
        {

            SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.CommandTimeout = 0;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
        
            if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                DataTable _t = new DataTable();
                string suc = "";
                if (ClasesSGUV.Login.Sucursal.ToUpper() == "MTY")
                    suc = "MONTERREY";

                else if (ClasesSGUV.Login.Sucursal.ToUpper() == "GDL")
                    suc = "GUADALAJARA";
                else
                    suc = ClasesSGUV.Login.Sucursal;

                var query = from item in table.AsEnumerable()
                            where item.Field<string>("Codigo").ToUpper() == suc
                            select item;

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                    clbSucursal.DataSource = _t;
                    clbSucursal.DisplayMember = "Nombre";
                    clbSucursal.ValueMember = "Codigo";
                }

            }
            else
            //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador)
            {
                clbSucursal.DataSource = table;
                clbSucursal.DisplayMember = "Nombre";
                clbSucursal.ValueMember = "Codigo";
            }

        }

        private void CargarVendedores()
        {

            SqlCommand command = new SqlCommand("sp_Ingresos", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 10);
                command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal);
                command.Parameters.AddWithValue("@Rol", ClasesSGUV.Login.Rol);
                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = ClasesSGUV.Login.Usuario;
                row["Nombre"] = string.Empty;
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Nombre";

        }

        private void frm_ReciboPago_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.CargarSucursales();
                this.CargarVendedores();

                dgvFacturas.DataSource = Facturas;
                dgvFacturas.Columns[1].DefaultCellStyle.Format = "C2";
                dgvFacturas.Columns[2].DefaultCellStyle.Format = "C2";

                dgvFacturas.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvFacturas.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void rbEfectivo_Click(object sender, EventArgs e)
        {
            if (rbEfectivo.Checked)
            {
                dtpFechaDeposito.Visible = false;
                txtNoCheque.Visible = false;
                txtBanco.Visible = false;

                label9.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
            }
            //else
            //{
            //    dtpFechaDeposito.Visible = true;
            //    txtNoCheque.Visible = true;
            //    cbBanco.Visible = true;
            //}
        }

        private void rbTC_Click(object sender, EventArgs e)
        {
            if (rbTC.Checked)
            {
                dtpFechaDeposito.Visible = false;
                txtNoCheque.Visible = false;
                txtBanco.Visible = true;

                label9.Visible = false;
                label11.Visible = true;
                label12.Visible = false;
            }
            //else
            //{
            //    dtpFechaDeposito.Visible = true;
            //    txtNoCheque.Visible = true;
            //    cbBanco.Visible = true;
            //}
        }

        private void rbCheque_Click(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                dtpFechaDeposito.Visible = true;
                txtNoCheque.Visible = true;
                txtBanco.Visible = true;

                label9.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // if (e.KeyChar == Convert.ToChar(Keys.Enter))
                //{
                //    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                //    {
                //        using (SqlCommand command = new SqlCommand("sp_EstadoCuenta", connection))
                //        {
                //            command.CommandType = CommandType.StoredProcedure;
                //            command.Parameters.AddWithValue("@TipoConsulta", 2);
                //            command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                //            connection.Open();

                //            SqlDataReader reader = command.ExecuteReader();
                //            if (reader.Read())
                //            {
                //                txtCliente.Text = Convert.ToString(reader[0]);
                //                txtCardName.Text = Convert.ToString(reader["CardName"]);
                //                _cliente = Convert.ToString(reader[0]);

                //                Facturas.Rows.Clear();
                //                txtMonto.Clear();
                //                txtMontoNum.Clear();
                //            }
                //        }
                //    }
                //}
                //else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                //{
                //    txtCliente.Clear();

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int errors = 0;
                foreach (DataGridViewRow item in dgvFacturas.Rows)
                {
                    if (!item.IsNewRow)
                    {
                        if (!string.IsNullOrEmpty(item.ErrorText))
                            errors++;
                    }
                }

                if (errors > 0)
                {
                    return;
                }
                if(rbTC.Checked)
                    if (string.IsNullOrEmpty(txtBanco.Text.Trim()))
                    {
                        MessageBox.Show("Ingresa el nombre del Banco", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtBanco.Focus();
                        return;
                    }

                if (rbPosfechado.Checked | rbProtegido.Checked)
                {
                    if (string.IsNullOrEmpty(txtBanco.Text.Trim()))
                    {
                        MessageBox.Show("Ingrea el nombre del Banco", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtBanco.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtNoCheque.Text.Trim()))
                    {
                        MessageBox.Show("Ingrea el No de Cheque", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNoCheque.Focus();
                        return;
                    }
                }

                if (clbVendedor.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El campo [Nombre del agente es oblgatorio]", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    clbVendedor.Focus();
                    return;
                }

                decimal suma = Facturas.Compute("Sum([Importe recibido])", string.Empty) == DBNull.Value ? decimal.Zero : Convert.ToDecimal(Facturas.Compute("Sum([Importe recibido])", string.Empty));
                txtMonto.Text = suma.ToString("C2");

                if (suma == decimal.Zero || dgvFacturas.Rows.Count == 0)
                {
                    MessageBox.Show("El monto del recibo no puede ser $ 0.00", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                decimal folio = decimal.Zero;
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Code", txtFolio.Text == string.Empty ? "0" : txtFolio.Text);
                        command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                        command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);

                        string __metodo = string.Empty;
                        if (rbEfectivo.Checked) __metodo = rbEfectivo.AccessibleName;
                        if (rbTC.Checked) __metodo = rbTC.AccessibleName;
                        if (rbPosfechado.Checked) __metodo = rbPosfechado.AccessibleName;
                        if (rbProtegido.Checked) __metodo = rbProtegido.AccessibleName;
                        if (btTransferencia.Checked) __metodo = btTransferencia.AccessibleName;

                        if (__metodo == string.Empty)
                        {
                            MessageBox.Show("Debe seleccionar un Método de pago.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            rbEfectivo.Focus();
                            return;
                        }
                        
                        command.Parameters.AddWithValue("@MetodoPago", __metodo);
                        command.Parameters.AddWithValue("@FechaCorte", dtpFechaDeposito.Value);
                        
                        command.Parameters.AddWithValue("@Monto", suma);
                        command.Parameters.AddWithValue("@Obervaciones", txtObservaciones.Text);
                        command.Parameters.AddWithValue("@Sucursal", clbSucursal.Text);
                        command.Parameters.AddWithValue("@Agente", clbVendedor.Text);
                        command.Parameters.AddWithValue("@Folio", txtFolioFisico.Text);

                        //-----CONDICONES DE PAGO
                        if (rbTC.Checked) command.Parameters.AddWithValue("@Banco", txtBanco.Text);
                        if (rbProtegido.Checked || rbPosfechado.Checked)
                        {
                            command.Parameters.AddWithValue("@FechaDeposito", dtpFechaDeposito.Value);
                            command.Parameters.AddWithValue("@NoCheque", txtNoCheque.Text);
                            command.Parameters.AddWithValue("@Banco", txtBanco.Text);
                        }

                        connection.Open();
                        folio = Convert.ToDecimal(command.ExecuteScalar());
                        if (folio > 0)
                        {
                            txtFolio.Text = folio.ToString();
                        }
                        else
                        {
                            txtFolio.Text = string.Empty;
                        }
                    }
                }
                int line = 0;
                foreach (DataRow item in Facturas.Rows)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", 3);
                            command.Parameters.AddWithValue("@Folio", folio);
                            command.Parameters.AddWithValue("@LineNum", line);
                            command.Parameters.AddWithValue("@Factura", item.Field<decimal>("Factura"));
                            command.Parameters.AddWithValue("@Monto", item.Field<decimal>("Importe recibido"));

                            connection.Open();
                            command.ExecuteNonQuery();
                            line++;
                        }
                    }
                }
                btnGuardar.Enabled = false;
                Cobranza.ReporteCrystal frm = new ReporteCrystal(folio);
                frm.MdiParent = this.MdiParent;
                frm.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvFacturas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!(sender as DataGridView).Rows[e.RowIndex].IsNewRow)
                {
                    if ((sender as DataGridView).Rows[e.RowIndex].Cells[0].Value != DBNull.Value)
                    {
                        if (e.ColumnIndex == 0)
                        {
                            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                            {
                                using (SqlCommand command = new SqlCommand("sp_Ingresos", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                                    command.Parameters.AddWithValue("@Cliente", _cliente);
                                    command.Parameters.AddWithValue("@Factura", (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value);

                                    connection.Open();

                                    SqlDataReader reader = command.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        (sender as DataGridView).Rows[e.RowIndex].ErrorText = string.Empty;
                                        (sender as DataGridView).Rows[e.RowIndex].Cells[1].Value = Convert.ToDecimal(reader["DocTotal"]) - Convert.ToDecimal(reader["PaidToDate"]);
                                    }
                                    else
                                    {
                                        (sender as DataGridView).Rows[e.RowIndex].ErrorText = "La Factura " + (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value + "  no corresponde al cliente: " + _cliente;
                                        MessageBox.Show("La Factura " + (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value + " no corresponde al cliente: " + _cliente, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                            }
                        }
                        if (e.ColumnIndex == 2)
                        {
                            if (Convert.ToDecimal((sender as DataGridView).Rows[e.RowIndex].Cells[2].Value) > Convert.ToDecimal((sender as DataGridView).Rows[e.RowIndex].Cells[1].Value))
                                MessageBox.Show("El monto recibido no puede ser mayor al saldo de la factura", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            else
                            {
                                decimal suma = Facturas.Compute("Sum([Importe recibido])", string.Empty) == DBNull.Value ? decimal.Zero : Convert.ToDecimal(Facturas.Compute("Sum([Importe recibido])", string.Empty));

                                txtMonto.Text = suma.ToString("C2");
                                txtMontoNum.Text = suma.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtFolio.Clear();
            btnGuardar.Enabled = true;
            txtCardName.Clear();
            txtCliente.Clear();
            dtpFechaCorte.Value = DateTime.Now;
            clbVendedor.SelectedIndex = 0;

            Facturas.Rows.Clear();
            txtObservaciones.Clear();
            rbEfectivo.Checked = false;
            rbTC.Checked = false;
            rbPosfechado.Checked = false;
            rbProtegido.Checked = false;
            dtpFechaDeposito.Value = DateTime.Now;
            txtNoCheque.Clear();
            txtBanco.Clear();
            txtFolioFisico.Clear();
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_EstadoCuenta", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtCliente.Text = Convert.ToString(reader[0]);
                        txtCardName.Text = Convert.ToString(reader["CardName"]);
                        _cliente = Convert.ToString(reader[0]);

                        Facturas.Rows.Clear();
                        txtMonto.Clear();
                        txtMontoNum.Clear();
                    }
                }
            }
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception)
            {
                
            }
        }
    }
}
