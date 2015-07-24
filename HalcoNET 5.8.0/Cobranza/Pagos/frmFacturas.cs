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

namespace Cobranza.Pagos
{
    public partial class frmFacturas : Form
    {
        string Cliente;
        string ClienteOriginal;
        int Code;
        decimal Abono;
        string Descripcion;
        string Moneda;
        string CuentaNI;
        string AcumClientes;
        DateTime FechaMovimiento;
        string FormaPago = string.Empty;
        List<string> DepEfectivo = new List<string>();
        string __monedaCliente = string.Empty;
        decimal __tipoCambio;
        List<string> SinCandado = new List<string>();

        DataTable TblPagos = new DataTable();
        DataTable __tipoPagos = new DataTable();


        string []ArrayClientes;

        public enum Columnas
        {
            Seleccionar, 
            Docentry, 
            Cliente, 
            Factura, 
            Moneda, 
            Fecha, 
            FechaVto, 
            Total, 
            Saldo, 
            NCPESug, 
            AplicacaPE, 
            NCPPSug, 
            AplicadaPP, 
            NCPE, 
            NCPP,
            Pago, 
            PorAplicar, 
            Diferencia,
            TipoPago,
            Editar,
            FormaPago
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Docentry].Visible = false;
            dgv.Columns[(int)Columnas.Moneda].Visible = false;

            dgv.Columns[(int)Columnas.Diferencia].Visible = false;
            dgv.Columns[(int)Columnas.Editar].Visible = false;

            dgv.Columns[(int)Columnas.TipoPago].Visible = false;

            dgv.Columns[(int)Columnas.Seleccionar].Width = 25;
            dgv.Columns[(int)Columnas.Docentry].Width = 70;
            dgv.Columns[(int)Columnas.Cliente].Width = 70;
            dgv.Columns[(int)Columnas.Factura].Width = 70;
            dgv.Columns[(int)Columnas.Moneda].Width = 70;
            dgv.Columns[(int)Columnas.Fecha].Width = 70;
            dgv.Columns[(int)Columnas.FechaVto].Width = 70;
            dgv.Columns[(int)Columnas.Total].Width = 85;
            dgv.Columns[(int)Columnas.Saldo].Width = 85;
            dgv.Columns[(int)Columnas.NCPESug].Width = 85;
            dgv.Columns[(int)Columnas.AplicacaPE].Width = 60;
            dgv.Columns[(int)Columnas.NCPPSug].Width = 85;
            dgv.Columns[(int)Columnas.AplicadaPP].Width = 60;
            dgv.Columns[(int)Columnas.Pago].Width = 85;
            dgv.Columns[(int)Columnas.PorAplicar].Width = 85;
            dgv.Columns[(int)Columnas.NCPE].Width = 85;
            dgv.Columns[(int)Columnas.NCPP].Width = 85;

            dgv.Columns[(int)Columnas.AplicacaPE].HeaderText = "Aplicada";
            dgv.Columns[(int)Columnas.AplicadaPP].HeaderText = "Aplicada";

            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NCPESug].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.AplicacaPE].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NCPPSug].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Pago].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NCPE].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NCPP].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PorAplicar].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NCPESug].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NCPPSug].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Pago].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NCPE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NCPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PorAplicar].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Seleccionar].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns[(int)Columnas.Pago].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns[(int)Columnas.NCPE].DefaultCellStyle.BackColor = Color.Gainsboro;
            dgv.Columns[(int)Columnas.NCPP].DefaultCellStyle.BackColor = Color.Gainsboro;

            dgv.Columns[(int)Columnas.Docentry].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dgv.Columns[(int)Columnas.Factura].ReadOnly = true;
            dgv.Columns[(int)Columnas.Moneda].ReadOnly = true;
            dgv.Columns[(int)Columnas.Fecha].ReadOnly = true;
            dgv.Columns[(int)Columnas.FechaVto].ReadOnly = true;
            dgv.Columns[(int)Columnas.Total].ReadOnly = true;
            dgv.Columns[(int)Columnas.Saldo].ReadOnly = true;
            dgv.Columns[(int)Columnas.NCPESug].ReadOnly = true;
            dgv.Columns[(int)Columnas.AplicacaPE].ReadOnly = true;
            dgv.Columns[(int)Columnas.NCPPSug].ReadOnly = true;
            dgv.Columns[(int)Columnas.AplicadaPP].ReadOnly = true;
            dgv.Columns[(int)Columnas.PorAplicar].ReadOnly = true;  
        }

        public frmFacturas(string _cliente, string _descripcion, decimal _abono, int _idMov, string _cuentaNI, DateTime _fechaMov)
        {
            InitializeComponent();

            Cliente = _cliente;
            Descripcion = _descripcion;
            Abono = _abono;
            Code = _idMov;
            CuentaNI = _cuentaNI;
            FechaMovimiento = _fechaMov;
            TblPagos.Columns.Add("Cliente", typeof(string));
            TblPagos.Columns.Add("Moneda", typeof(string));
            TblPagos.Columns.Add("Factura", typeof(int));
            TblPagos.Columns.Add("DocEntry", typeof(int));
            TblPagos.Columns.Add("Pago", typeof(decimal));
            TblPagos.Columns.Add("NCPE", typeof(decimal));
            TblPagos.Columns.Add("NCPP", typeof(decimal));
            TblPagos.Columns.Add("TipoPago", typeof(string));
            TblPagos.Columns.Add("FormaPago", typeof(string));
            TblPagos.Columns.Add("TipoCambio", typeof(decimal));
            TblPagos.Columns.Add("MXP", typeof(decimal), "TipoCambio*Pago");
            TblPagos.Columns.Add("PagoTotal", typeof(decimal));

            DepEfectivo.Add("CV_6503000007276663");
            DepEfectivo.Add("CV_6503000007276698");
            DepEfectivo.Add("CV_6503000007276833");
            DepEfectivo.Add("CV_6503000007276248");
            DepEfectivo.Add("CV_6503000007276663");
            DepEfectivo.Add("CV_6503000007276671");
            DepEfectivo.Add("CV_6503000002319526");
            DepEfectivo.Add("CV_6503000007276272");
        }

        public void GuardarPagos(DataTable t)
        {
            string mensaje = "";
            try
            {

                foreach (DataRow item in t.Rows)
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "PJ_Pagos";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 7);
                            command.Parameters.AddWithValue("@Sucursal", item.Field<string>("TipoPago"));
                            command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                            command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                            command.Parameters.AddWithValue("@Banco", FormaPago);
                            command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                            command.Parameters.AddWithValue("@Abono", item.Field<int>("DocEntry"));
                            command.Parameters.AddWithValue("@Referencia", string.Empty);
                            command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                            command.Parameters.AddWithValue("@Cliente", item.Field<string>("Cliente"));
                            command.Parameters.AddWithValue("@Cantidad", item.Field<decimal>("Pago"));

                            command.Parameters.AddWithValue("@NCPP", item.Field<decimal>("NCPP"));
                            command.Parameters.AddWithValue("@NCPE", item.Field<decimal>("NCPE"));
                            command.Parameters.AddWithValue("@TipoCambio", item.Field<decimal>("TipoCambio"));
                            command.Parameters.AddWithValue("@TotalPago", item.Field<decimal>("PagoTotal"));

                            command.Parameters.AddWithValue("@Code", Code);

                            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                            parameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter);

                            connection.Open();

                            command.ExecuteNonQuery();
                            mensaje = Convert.ToString(command.Parameters["@Message"].Value);

                            string Find = (from itemC in ArrayClientes
                                           where itemC.Trim() == txtCliente.Text
                                           select itemC).FirstOrDefault();

                            string cta = Descripcion.Split(' ')[0];

                            bool tr = SinCandado.Contains(cta);

                            if ((!string.IsNullOrEmpty(Find) && Descripcion.Substring(0, 2).Equals("CV"))
                                || !Descripcion.Substring(0, 2).Equals("CV") || ClienteOriginal.Trim() == string.Empty
                                //|| SinCandado.Contains(cta.Trim())
                                //|| ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador)
                                )
                            {
                                
                            }
                            else
                            {
                                this.InactivarRegistros(Descripcion, Abono, item.Field<int>("DocEntry"), item.Field<string>("Cliente"), item.Field<decimal>("PagoTotal"));
                                this.GuardarAuditoria(Descripcion, Abono, item.Field<int>("DocEntry"), item.Field<string>("Cliente"), item.Field<decimal>("PagoTotal"));
                                //MessageBox.Show("Cliente:" + item.Field<string>("Cliente"));
                            }
                        }
                    }
                }

                MessageBox.Show(mensaje, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                
            }
        }

        public void GuardarDemasias(DataTable t)
        {
            string mensaje = "";
            try
            {
                //string[] clientes = (from item in t.AsEnumerable()
                //                      select item.Field<>)

                foreach (DataRow item in t.Rows)
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "PJ_Pagos";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 12);
                            command.Parameters.AddWithValue("@Sucursal", item.Field<string>("Moneda"));
                            command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                            command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                            command.Parameters.AddWithValue("@Banco", string.Empty);
                            command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                            command.Parameters.AddWithValue("@Abono", 0);
                            command.Parameters.AddWithValue("@Referencia", string.Empty);
                            command.Parameters.AddWithValue("@CuentaNI", item.Field<string>("CuentaNI"));

                            command.Parameters.AddWithValue("@Cliente", item.Field<string>("Cliente"));
                            command.Parameters.AddWithValue("@Cantidad", item.Field<decimal>("Demasia"));

                            command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                            command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                            command.Parameters.AddWithValue("@Code", Code);

                            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                            parameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter);

                            connection.Open();

                            command.ExecuteNonQuery();
                            mensaje = Convert.ToString(command.Parameters["@Message"].Value);
                        }
                    }
                }

                MessageBox.Show(mensaje, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

            }
        }

        public void GuardarNC(DataTable t)
        {
            string mensaje = "";
            try
            {
                //string[] clientes = (from item in t.AsEnumerable()
                //                      select item.Field<>)

                foreach (DataRow item in t.Rows)
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = "PJ_Pagos";
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 12);
                            command.Parameters.AddWithValue("@Sucursal", item.Field<string>("Moneda"));
                            command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                            command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                            command.Parameters.AddWithValue("@Banco", string.Empty);
                            command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                            command.Parameters.AddWithValue("@Abono", 0);
                            command.Parameters.AddWithValue("@Referencia", string.Empty);
                            command.Parameters.AddWithValue("@CuentaNI", item.Field<string>("CuentaNI"));

                            command.Parameters.AddWithValue("@Cliente", item.Field<string>("Cliente"));
                            command.Parameters.AddWithValue("@Cantidad", item.Field<decimal>("Demasia"));

                            command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                            command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                            command.Parameters.AddWithValue("@Code", Code);

                            SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                            parameter.Direction = ParameterDirection.Output;
                            command.Parameters.Add(parameter);

                            connection.Open();

                            command.ExecuteNonQuery();
                            mensaje = Convert.ToString(command.Parameters["@Message"].Value);
                        }
                    }
                }

                MessageBox.Show(mensaje, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {

            }
        }

        public void AcumularPagos(DataTable datos, decimal _total)
        {
            toolStatus.Text = string.Empty;
            toolStatus.BackColor = Color.FromName("Control");
            toolStatus.ForeColor = Color.Black;

            Moneda = string.Empty;

            try
            {
                foreach (DataRow item in datos.Rows)
                {
                    Moneda = item.Field<string>("Moneda");
                    DataRow row = TblPagos.NewRow();
                    row["Cliente"] = Cliente;
                    row["Factura"] = item.Field<int>("Factura");
                    row["DocEntry"] = item.Field<int>("DocEntry");
                    row["Pago"] = item.Field<decimal>("Pago");
                    row["NCPP"] = item.Field<decimal>("NC (P. PP)");
                    row["NCPE"] = item.Field<decimal>("NC (P. Esp)");
                    row["Moneda"] = item.Field<string>("Moneda");
                    row["TipoPago"] = item.Field<string>("TipoPago");
                    row["FormaPago"] = item.Field<string>("FormaPago");
                    row["TipoCambio"] = __monedaCliente == "USD" ? Convert.ToDecimal(txtTipo.Text) : (decimal)1;
                    row["PagoTotal"] = _total;
                    TblPagos.Rows.Add(row);
                }
                toolStatus.Text = "Listo.";
                toolStatus.BackColor = Color.Green;
                toolStatus.ForeColor = Color.Black;

                //MessageBox.Show("Listo", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                toolStatus.Text = "Error: " + ex.Message;
                toolStatus.BackColor = Color.Red;
                toolStatus.ForeColor = Color.White;
            }
        }

        public void GuardarAuditoria(string _descripcion, decimal _total, int _factura, string _cliente, decimal _aplicado)
        {

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 24);
                    command.Parameters.AddWithValue("@Cliente", _cliente);
                    command.Parameters.AddWithValue("@TotalPago", _total);
                    command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);
                    command.Parameters.AddWithValue("@Referencia", _descripcion);
                    command.Parameters.AddWithValue("@Aplicado", _aplicado);
                    command.Parameters.AddWithValue("@Factura", _factura);
                    command.Parameters.AddWithValue("@Code", Code);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public void InactivarRegistros(string _descripcion, decimal _total, int _factura, string _cliente, decimal _aplicado)
        {

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 25);
                    command.Parameters.AddWithValue("@Cliente", _cliente);
                    command.Parameters.AddWithValue("@TotalPago", _total);
                    command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);
                    command.Parameters.AddWithValue("@Referencia", _descripcion);
                    command.Parameters.AddWithValue("@Aplicado", _aplicado);
                    command.Parameters.AddWithValue("@Factura", _factura);
                    command.Parameters.AddWithValue("@Code", Code);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
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

        private decimal Sumar(DataGridView dgv)

        {
            decimal suma = 0;

            if (__monedaCliente.Equals("$"))
                __tipoCambio = 1;
            else __tipoCambio = Convert.ToDecimal(txtTipo.Text);

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if(Convert.ToBoolean(row.Cells["Seleccionar"].Value) == true)
                    suma += Convert.ToDecimal(row.Cells["Pago"].Value) * __tipoCambio;  //aqui recorre las celdas y las va sumando
            }
            return suma;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "PJ_Pagos";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 22);
                        command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                        connection.Open();
                        __monedaCliente = Convert.ToString(command.ExecuteScalar());
                    }
                }

                if (__monedaCliente.Equals("USD"))
                {
                    lblTipo.Visible = true;
                    txtTipo.Visible = true;
                    txtTipo.Text = "0";
                    txtTipo.Focus();
                }
                else
                {
                    lblTipo.Visible = false;
                    txtTipo.Visible = false;
                    __tipoCambio = 1;
                    dgvFacturas.Focus();
                }

                statusLbl.Text = string.Empty;


                this.Esperar();
                dgvFacturas.DataSource = null;
                dgvFacturas.Columns.Clear();

                string Find = (from item in ArrayClientes
                               where item.Trim() == txtCliente.Text
                               select item).FirstOrDefault();

                string cta = Descripcion.Split(' ')[0];

                bool tr = SinCandado.Contains(cta);

                if ((!string.IsNullOrEmpty(Find) && Descripcion.Substring(0, 2).Equals("CV"))
                    || !Descripcion.Substring(0, 2).Equals("CV") || ClienteOriginal.Trim() == string.Empty
                    || SinCandado.Contains(cta.Trim())
                    || ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador))
                {

                }
                else
                {
                    //Registrar Movimiento
                }
                    if (Abono - Pago > 0)
                    {
                        Cliente = txtCliente.Text;
                        dgvFacturas.DataSource = null;
                        dgvFacturas.Columns.Clear();
                        using (SqlConnection connection = new SqlConnection())
                        {
                            connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                            using (SqlCommand command = new SqlCommand())
                            {
                                command.Connection = connection;
                                command.CommandText = "PJ_Pagos";
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@TipoConsulta", 6);
                                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                                command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                                command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                                command.Parameters.AddWithValue("@Banco", string.Empty);
                                command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                                command.Parameters.AddWithValue("@Abono", decimal.Zero);
                                command.Parameters.AddWithValue("@Referencia", string.Empty);
                                command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                                command.Parameters.AddWithValue("@Cliente", Cliente);
                                command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                                command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                                command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                                command.Parameters.AddWithValue("@Code", Code);

                                SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                                parameter.Direction = ParameterDirection.Output;
                                command.Parameters.Add(parameter);

                                DataTable table = new DataTable();
                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command;
                                adapter.SelectCommand.CommandTimeout = 0;
                                adapter.Fill(table);


                                dgvFacturas.DataSource = table;

                                if (table.Rows.Count > 0)
                                {
                                    this.Formato(dgvFacturas);
                                }
                            }
                        }
                    }
                    else
                    {
                        dgvFacturas.DataSource = null;
                        statusLbl.Text = "El monto del pago no es suficiente para cubirir otra factura";
                    }
                //else
                //{
                //    MessageBox.Show("El referencia: " + Descripcion + " no corresponde al cliente " + txtCliente.Text, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int Errors = 0;
                foreach (DataGridViewRow item in dgvFacturas.Rows)
                {
                    if (item.Cells[(int)Columnas.Pago].Style.BackColor == Color.Red)
                    {
                        Errors++;
                    }
                }

                if (Errors == 0)
                {
                    var query = from item in (dgvFacturas.DataSource as DataTable).AsEnumerable()
                                where item.Field<decimal>("Pago") != 0
                                select item;

                    if (query.Count() > 0)
                    {
                        DataTable datos = query.CopyToDataTable();
                        decimal pago = Convert.ToDecimal(datos.Compute("SUM(Pago)", string.Empty)) * __tipoCambio;
                        if (pago <= Abono + 10)
                        {
                            Cobranza.Pagos.frmConfirmarPagos pagoForm = new frmConfirmarPagos(datos, 0, false, pago/__tipoCambio);
                            DialogResult p = pagoForm.ShowDialog();
                            if (p == DialogResult.OK)
                            {
                               // MessageBox.Show(pagoForm.MontoPago.ToString());

                                AcumClientes += Cliente + ",";
                                this.AcumularPagos(datos, pagoForm.MontoPago);

                                int num = TblPagos.Rows.Count;
                                lblnum.Text = "Facturas agregadas: " + num.ToString();

                                /////////////////////
                                dgvFacturas.DataSource = null;

                                //actualizar suma en txt's superiores 
                                decimal p1 = 0;
                                try
                                {
                                    if (dgvFacturas.DataSource != null)
                                    {
                                        p1 = this.Sumar((sender as DataGridView));
                                    }
                                    decimal p2 = 0;
                                    if (TblPagos.Rows.Count > 0)
                                    {
                                        p2 = Convert.ToDecimal(TblPagos.Compute("SUM(MXP)", string.Empty));
                                        num = TblPagos.Rows.Count;
                                        lblnum.Text = "Facturas agregadas: " + num.ToString();
                                    }
                                    Pago = p2 + p1;
                                    txtPagos.Text = Pago.ToString("C4");
                                    txtDiferencia.Text = (Abono - Pago).ToString("C4");
                                }
                                catch (Exception)
                                {
                                }
                            }

                        }
                        else
                        {
                            MessageBox.Show("No se puede superar el monto del abono.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar al menos una factura.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Corrija las celdas marcadas en rojo para continuar.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se han seleccionado facturas.\r\nError: " + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            decimal p1 = 0;
            int num = 0;
            try
            {
                if (dgvFacturas.DataSource != null)
                {
                    p1 = this.Sumar((sender as DataGridView));
                }
                decimal p2 = 0;
                if (TblPagos.Rows.Count > 0)
                {
                    p2 = Convert.ToDecimal(TblPagos.Compute("SUM(MXP)", string.Empty));
                    num = TblPagos.Rows.Count;
                    lblnum.Text = "Facturas agregadas: " + num.ToString();
                }
                //if (p1 > p2)
                //{
                //    Pago = p1;
                //}
                //else
                //{
                    Pago = p2 + p1;
                //}
                txtPagos.Text = Pago.ToString("C4");
                txtDiferencia.Text = (Abono - Pago).ToString("C4");
            }
            catch (Exception)
            {
            }

            int Errors = 0;
            foreach (DataGridViewRow item in dgvFacturas.Rows)
            {
                if (item.Cells[(int)Columnas.Pago].Style.BackColor == Color.Red)
                {
                    Errors++;
                }
            }

            if (Errors == 0)
            {
                var query = from item in (TblPagos).AsEnumerable()
                            where item.Field<decimal>("Pago") != 0
                            select item;

                if (query.Count() > 0)
                {
                    DataTable datos = query.CopyToDataTable();
                    decimal pago = Convert.ToDecimal(datos.Compute("SUM(MXP)", string.Empty));
                    if (pago <= Abono + 10)
                    {
                        decimal demasia = Abono - pago;

                        if (demasia <= 10)
                        {
                            demasia = 0;
                        }


                        Cobranza.Pagos.frmConfirmarPagos pagoForm = new frmConfirmarPagos(datos, demasia, true, decimal.Zero);
                        DialogResult p = pagoForm.ShowDialog();
                        if (p == DialogResult.OK)
                        {
                            FormaPago = pagoForm.FormaPago;

                            this.GuardarPagos(datos);
                            //this.GuardarNC(datos);
                            if (Abono - pago > 10)
                            {
                                DataTable demasias = new DataTable();
                                demasias.Columns.Add("Moneda", typeof(string));
                                demasias.Columns.Add("Cliente", typeof(string));
                                demasias.Columns.Add("CuentaNI", typeof(string));
                                demasias.Columns.Add("Demasia", typeof(decimal));

                                DataRow row = demasias.NewRow();
                                row["Moneda"] = Moneda;
                                row["Cliente"] = AcumClientes;
                                row["CuentaNI"] = CuentaNI;
                                row["Demasia"] = Abono - pago;
                                demasias.Rows.Add(row);
                                this.GuardarDemasias(demasias);
                            }
                        }
                    }
                    else
                    {
                        //Guardar demasias!!!
                        MessageBox.Show("No se puede superar el monto del abono.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                else
                {
                    MessageBox.Show("Debe seleccionar al menos una factura.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Corrija las celdas marcadas en rojo para continuar.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            ClienteOriginal = Cliente;
            txtCliente.CharacterCasing = CharacterCasing.Upper;
            txtMovimiento.Text = Abono.ToString("C4");
            txtDescripcion.Text = Cliente + "\r\n" + Descripcion;
            ArrayClientes = Cliente.Split(new Char[] {','});

            __tipoPagos.Columns.Add("Codigo", typeof(string));
            __tipoPagos.Columns.Add("Nombre", typeof(string));

            DataRow row = __tipoPagos.NewRow();
            row[0] = "Normal";
            row[1] = "Normal";

            DataRow row2 = __tipoPagos.NewRow();
            row2[0] = "Pago parcial";
            row2[1] = "Pago parcial";

            __tipoPagos.Rows.Add(row);
            __tipoPagos.Rows.Add(row2);


            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 23);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataTable tbl = new DataTable();


                    da.Fill(tbl);

                    SinCandado = (from item in tbl.AsEnumerable()
                               select item.Field<string>("Referencia").Trim()
                               ).ToList();

                }
            }
            
        }
        
        decimal Pago = 0;
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellClick(sender, e);
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            DialogResult resp = MessageBox.Show("Se eliminará la información correspondiente a este pago, \r\n ¿Desea continuar?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp == System.Windows.Forms.DialogResult.Yes)
            {
                AcumClientes = string.Empty;
                TblPagos.Rows.Clear();
                Pago = 0;
                txtPagos.Text = Pago.ToString("C4");
                txtDiferencia.Text = (Abono - Pago).ToString("C4");

                int num = TblPagos.Rows.Count;
                lblnum.Text = "Facturas agregadas: " + num.ToString();
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)Columnas.FormaPago )
            {
                var source = new AutoCompleteStringCollection();


                string[] stringArray = Array.ConvertAll<DataRow, String>(__tipoPagos.Select(), delegate(DataRow row) { return (String)row["Codigo"]; });

                source.AddRange(stringArray);

                TextBox prodCode = e.Control as TextBox;
                if (prodCode != null)
                {
                    prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    prodCode.AutoCompleteCustomSource = source;
                    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
        }

        private void dvgCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dvgComboFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.NCPP].Value) > decimal.Zero && Convert.ToDateTime(item.Cells[(int)Columnas.FechaVto].Value) < FechaMovimiento)
                    {
                        item.Cells[(int)Columnas.NCPP].Style.BackColor = Color.Yellow;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.NCPP].Style.BackColor = Color.Gainsboro;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Saldo].Value) + (decimal)10 < Convert.ToDecimal(item.Cells[(int)Columnas.Pago].Value))
                    {
                        item.Cells[(int)Columnas.Pago].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Pago].Style.ForeColor = Color.Gainsboro;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Pago].Style.BackColor = Color.Gainsboro;
                        item.Cells[(int)Columnas.Pago].Style.ForeColor = Color.Black;
                    }
                }

            }
            catch (StackOverflowException)
            {
            }
        }

        int Contador = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                

            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView dgv = (sender as DataGridView);
                if (dgv == null) return;

                int filaEdit = e.RowIndex;
                int columnaEdit = e.ColumnIndex;

                

                if (filaEdit == -1) return;
                if (columnaEdit == (int)Columnas.FormaPago)
                {
                    dgv.Rows[filaEdit].Cells[columnaEdit].Value = "Normal";                    
                    object objValCell = dgv.Rows[filaEdit].Cells[columnaEdit].Value;
                    if (objValCell == null)
                        return;
                    string valCell = objValCell.ToString();
                    DataGridViewComboBoxCell celcombo = new DataGridViewComboBoxCell();
                    object objTipoInci = dgv.Rows[filaEdit].Cells[(int)Columnas.FormaPago].Value;

                    if (objTipoInci == null) return;

                    string tipoInci = objTipoInci.ToString();
                    celcombo.DataSource = __tipoPagos;
                    celcombo.ValueMember = "Nombre";
                    celcombo.DisplayMember = "Nombre";

                    if (valCell == string.Empty)
                    {
                        dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                    }
                    else
                    {
                        celcombo.Value = valCell.Trim();
                        dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dataGridView1_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //actualizar suma en txt's superiores 
                decimal p1 = 0;
                int num = 0;
                if (((sender as DataGridView).DataSource as DataTable).Rows.Count > 0)
                {
                    p1 = this.Sumar((sender as DataGridView));
                }
                decimal p2 = 0;
                if (TblPagos.Rows.Count > 0)
                {
                    p2 = Convert.ToDecimal(TblPagos.Compute("SUM(MXP)", string.Empty));
                    num = TblPagos.Rows.Count;
                    lblnum.Text = "Facturas agregadas: " + num.ToString();
                }
                Pago = p2 + p1;

                txtPagos.Text = Pago.ToString("C4");
                txtDiferencia.Text = (Abono - Pago).ToString("C4");
                //////////////////////////////////////////////////////////////////////////////
                int RowIndex = e.RowIndex;
                int ColumnIndex = e.ColumnIndex;

                if (ColumnIndex == (sender as DataGridView).Rows[RowIndex].Cells["Seleccionar"].ColumnIndex)
                {
                    bool valor = Convert.ToBoolean((sender as DataGridView).Rows[RowIndex].Cells["Seleccionar"].Value);
                    if (valor == true)
                    {
                        //Nota de crédito PE
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPE].Value = Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPESug].Value);
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPE].ReadOnly = false;
                        //Nota de crédito PP
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPP].Value = Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPPSug].Value);
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPP].ReadOnly = false;
                        //Saldo
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Pago].Value = Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Saldo].Value)
                                    - (Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPP].Value) + Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPE].Value));
                        //Tipo de pago
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.TipoPago].Value = "Normal";
                        //(sender as DataGridView).Rows[RowIndex].Cells["cmbTipo"].Value = "Normal";

                        (sender as DataGridView).Rows[RowIndex].Cells["Seleccionar"].Value = true;
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.TipoPago].Value = "Normal";
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.FormaPago].Value = "Normal";  
                    }
                    else
                    {
                        //Nota de crédito PE
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPE].Value = decimal.Zero;
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPE].ReadOnly = false;
                        //Nota de crédito PP
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPP].Value = decimal.Zero;
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPP].ReadOnly = false;
                        //Saldo
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Pago].Value = decimal.Zero;
                        //Tipo de pago
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.TipoPago].Value = string.Empty;
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.FormaPago].Value = string.Empty;

                        (sender as DataGridView).Rows[RowIndex].Cells["Seleccionar"].Value = false;
                    }
                }
                else
                {

                    bool valor = Convert.ToBoolean((sender as DataGridView).Rows[RowIndex].Cells["Seleccionar"].Value);
                    if (valor)
                    {
                        //Saldo por aplicar = Saldo - Pago - NCPP - NCPE
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.PorAplicar].Value =
                                Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Saldo].Value)
                                    - Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Pago].Value)
                                        - Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPE].Value)
                                            - Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPP].Value);

                        //Direfencia
                        (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Diferencia].Value = Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.PorAplicar].Value) - (Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPP].Value) + Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.NCPE].Value));
                        if (Convert.ToDecimal((sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Diferencia].Value) < -10)
                        {
                            (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Diferencia].Style.BackColor = Color.Red;
                            (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Diferencia].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Diferencia].Style.BackColor = Color.White;
                            (sender as DataGridView).Rows[RowIndex].Cells[(int)Columnas.Diferencia].Style.ForeColor = Color.Black;
                        }
                    }
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //actualizar suma en txt's superiores 
                p1 = 0;
                num = 0;
                if (((sender as DataGridView).DataSource as DataTable).Rows.Count > 0)
                {
                    p1 = this.Sumar((sender as DataGridView));
                }
                p2 = 0;
                if (TblPagos.Rows.Count > 0)
                {
                    p2 = Convert.ToDecimal(TblPagos.Compute("SUM(MXP)", string.Empty));
                    num = TblPagos.Rows.Count;
                    lblnum.Text = "Facturas agregadas: " + num.ToString();
                }
                Pago = p2 + p1;

                txtPagos.Text = Pago.ToString("C4");
                txtDiferencia.Text = (Abono - Pago).ToString("C4");
            }
            catch (Exception)
            {
            }
           
        }
    }
}
