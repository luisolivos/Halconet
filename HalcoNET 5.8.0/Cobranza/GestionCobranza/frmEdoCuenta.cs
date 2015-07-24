using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace Cobranza.GestionCobranza
{
    public partial class frmEdoCuenta : Form
    {
        string CardCode,
               CardName,
               Condicion,
               Vendedor,
               Jefa,
               CorreoVendedor,
               CorreoJefa,
               CorreoCliente,
               Referencia;

        public enum Columnas
        {
            Factura,
            FechaEmision,
            FechaVto,
            ImporteOriginal,
            NotaDev,
            NotaEsp,
            PagosAplicados,
            Saldo,
            Situacion,
            Enviar,
            Enviado
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Factura].Width = 90;
            dgv.Columns[(int)Columnas.FechaEmision].Width = 100;
            dgv.Columns[(int)Columnas.FechaVto].Width = 100;
            dgv.Columns[(int)Columnas.ImporteOriginal].Width = 100;
            dgv.Columns[(int)Columnas.NotaDev].Width = 100;
            dgv.Columns[(int)Columnas.NotaEsp].Width = 100;
            dgv.Columns[(int)Columnas.PagosAplicados].Width = 100;
            dgv.Columns[(int)Columnas.Saldo].Width = 100;
            dgv.Columns[(int)Columnas.Situacion].Width = 100;
            dgv.Columns[(int)Columnas.Enviado].Width = 75;
            dgv.Columns[(int)Columnas.Enviar].Width = 75;

            dgv.Columns[(int)Columnas.Factura].ReadOnly = true;
            dgv.Columns[(int)Columnas.FechaEmision].ReadOnly = true;
            dgv.Columns[(int)Columnas.FechaVto].ReadOnly = true;
            dgv.Columns[(int)Columnas.ImporteOriginal].ReadOnly = true;
            dgv.Columns[(int)Columnas.NotaDev].ReadOnly = true;
            dgv.Columns[(int)Columnas.NotaEsp].ReadOnly = true;
            dgv.Columns[(int)Columnas.PagosAplicados].ReadOnly = true;
            dgv.Columns[(int)Columnas.Saldo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Situacion].ReadOnly = true;
            dgv.Columns[(int)Columnas.Enviado].ReadOnly = false;
            dgv.Columns[(int)Columnas.Enviar].ReadOnly = false;

            dgv.Columns[(int)Columnas.ImporteOriginal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NotaDev].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NotaEsp].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PagosAplicados].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Situacion].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.ImporteOriginal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NotaDev].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NotaEsp].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PagosAplicados].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Situacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void RegistraEnviado(string _factura)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Facturacion", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 7);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);
                        command.Parameters.AddWithValue("@TipoDocumento", "Factura");
                        command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Usuario);
                        command.Parameters.AddWithValue("@Factura", _factura);
                        command.Parameters.AddWithValue("@EdoCta", "Y");

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public frmEdoCuenta()
        {
            InitializeComponent();
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool _consult = false;
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
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
                                CardName = Convert.ToString(reader["CardName"]);
                                CardCode = Convert.ToString(reader["CardCode"]);
                                Condicion = Convert.ToString(reader["PymntGroup"]);
                                Vendedor = Convert.ToString(reader["SlpName"]);
                                Jefa = Convert.ToString(reader["VatIdUnCmp"]);
                                CorreoVendedor = Convert.ToString(reader["CorreoVendedor"]);
                                CorreoJefa = Convert.ToString(reader["CorreoJefa"]);
                                CorreoCliente = Convert.ToString(reader["CorreoCliente"]);
                                Referencia = Convert.ToString(reader["RefPago"]);

                                txtNombre.Text = CardName;
                                txtCorreo.Text = CorreoCliente;
                                txtCondicion.Text = Condicion;
                                _consult = true;
                            }
                        }
                    }

                    if (_consult)
                    {
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("sp_EstadoCuenta", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", 1);
                                command.Parameters.AddWithValue("@Cliente", CardCode);
                                connection.Open();

                                SqlDataAdapter da = new SqlDataAdapter();
                                da.SelectCommand = command;
                                da.SelectCommand.CommandTimeout = 0;

                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                dataGridView1.DataSource = dt;

                                this.Formato(dataGridView1);
                            }
                        }
                    }
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                {
                    txtCliente.Clear();

                    txtNombre.Clear();
                    txtCorreo.Clear();
                    txtCondicion.Clear();
                    txtComentarios.Clear();
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                String Mensaje = String.Format(@"Anexamos el presente estado de cuenta para su validación y programación de pago. En caso de requerir aclaraciones favor de enviar un correo a {0} ({1}).<br><br>", Jefa, CorreoJefa);


                string tabla = @"
                                    <style type='text/css'>
                                    table, th, td {
                                        border: 1px solid black;
                                        border-spacing: 50px 50px 50px 50px;
                                        border-collapse: collapse;
                                    }
                                    </style>

                                   <br><br>
                                   <table cellpadding='6'>
                                   <tr>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>No<br>Factura</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Fecha de<br>Emisi&oacute;n</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Fecha de<br>Vencimiento</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Monto</strong></font></td>
                                        <td bgcolor='#FFFF00'><font size=2 face='Calibri' color='#000000'><strong>Devolución</strong></font></td>
                                        <td bgcolor='#92D050'><font size=2 face='Calibri' color='#000000'><strong>Nota de Cr&eacute;dito</strong></font></td>
                                        <td bgcolor='#FF0000'><font size=2 face='Calibri' color='#000000'><strong>Pagos<br>Aplicados</strong></font></td>
                                        <!--<td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Saldo<br>Vencido</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Saldo<br>por vencer</strong></font></td>-->
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Saldo<br>Pendiente</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Estatus</strong></font></td>
                                   </tr>";
                if (dataGridView1.Rows.Count > 0)
                {
                    DataTable _resul = dataGridView1.DataSource as DataTable;
                    string _color = string.Empty;
                    int filas = 0;
                    #region Vencido
                    foreach (DataRow row in _resul.Rows)
                    {
                        if (row.Field<bool>("Enviar"))
                        {
                            if (row.Field<DateTime>("Fecha de vencimiento") < DateTime.Now)
                                _color = "#E5FFCC";
                            else
                                _color = "#FFFFFF";

                            tabla += @"<tr>
                                        <td><font size=2 face='Calibri'>" + row.Field<int>("Factura") + "</font></td>" +
                                            "<td><font size=2 face='Calibri'>" + row.Field<DateTime>("Fecha de emisión").ToShortDateString() + "</font></td>" +
                                            "<td><font size=2 face='Calibri'>" + row.Field<DateTime>("Fecha de vencimiento").ToShortDateString() + "</font></td>" +
                                            "<td><font size=2 face='Calibri'><p align='right'>" + row.Field<decimal>("Importe Original").ToString("C2") + "</p></font></td>" +
                                            "<td><font size=2 face='Calibri'><p align='right'>" + row.Field<decimal>("Nota por devolución").ToString("C2") + "</p></font></td>" +
                                            "<td><font size=2 face='Calibri'><p align='right'>" + row.Field<decimal>("Nota por precio especial").ToString("C2") + "</p></font></td>" +
                                            "<td><font size=2 face='Calibri'><p align='right'>" + row.Field<decimal>("Pagos aplicados").ToString("C2") + "</p></font></td>" +
                                            //"<td bgcolor = '" + _color + "'><font size=2 face='Calibri'><p align='right'>" + (row.Field<string>("Situación").Equals("Vencida") ? row.Field<decimal>("Saldo pendiente").ToString("C2") : string.Empty) + "</p></font></td>" +
                                            //"<td><font size=2 face='Calibri'><p align='right'>" + (!row.Field<string>("Situación").Equals("Vencida") ? row.Field<decimal>("Saldo pendiente").ToString("C2") : string.Empty) + "</p></font></td>" +
                                            "<td bgcolor = '" + _color + "'><font size=2 face='Calibri'><p align='right'>" + row.Field<decimal>("Saldo pendiente").ToString("C2") + "</p></font></td>" +
                                            "<td><font size=2 face='Calibri'>" + row.Field<string>("Situación") + "</font></td>" +
                                      "</tr>";
                            filas++;
                        }
                    }
                    #endregion

                    #region Total
                    tabla += @"
                                <tr>
                                      <td><font size=2 face='Calibri'></font></td>" +
                                            "<td><font size=2 face='Calibri'></font></td>" +
                                            "<td><font size=2 face='Calibri'></font></td>" +
                                            "<td><font size=2 face='Calibri'></font></td>" +
                                            "<td><font size=2 face='Calibri'></font></td>" +
                                            "<td><font size=2 face='Calibri'></font></td>" +
                                            "<td><font size=2 face='Calibri'></font></td>" +
                                            //"<td><font size=2 face='Calibri'></font></td>" +
                                            //"<td><font size=2 face='Calibri'></font></td>" +
                                            "<td><font size=2 face='Calibri'></font></td>" +
                                            @"<td><font size=2 face='Calibri'></font></td>
                                      </tr>
                                <tr>
                                        <td bgcolor='#2A2627' colspan = '2'><font size=2 face='Calibri' color='#FFFFFF'><strong>Total general<br></strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong></strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><p align='right'><strong>" + Convert.ToDecimal(_resul.Compute("SUM([Importe Original])", "Enviar=1")).ToString("C2") + @"</strong></p></font></td>
                                        <td bgcolor='#FFFF00'><font size=2 face='Calibri' color='#000000'><p align='right'><strong>" + Convert.ToDecimal(_resul.Compute("SUM([Nota por devolución])", "Enviar=1")).ToString("C2") + @"</strong></p></font></td>
                                        <td bgcolor='#92D050'><font size=2 face='Calibri' color='#000000'><p align='right'><strong>" + Convert.ToDecimal(_resul.Compute("SUM([Nota por precio especial])", "Enviar=1")).ToString("C2") + @"</strong></p></font></td>
                                        <td bgcolor='#FF0000'><font size=2 face='Calibri' color='#000000'><p align='right'><strong>" + Convert.ToDecimal(_resul.Compute("SUM([Pagos aplicados])", "Enviar=1")).ToString("C2") + @"</strong></p></font></td>
                                        <!--<td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><p align='right'><strong>" + Convert.ToDecimal(_resul.Compute("SUM([Saldo pendiente])", "Enviar=1 AND Situación = 'Vencida'")).ToString("C2") + @"</strong></p></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><p align='right'><strong>" + Convert.ToDecimal(_resul.Compute("SUM([Saldo pendiente])", "Enviar=1 AND Situación = 'Por vencer'")).ToString("C2") + @"</strong></p></font></td>-->
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><p align='right'><strong>" + Convert.ToDecimal(_resul.Compute("SUM([Saldo pendiente])", "Enviar=1")).ToString("C2") + @"</strong></p></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><p align='right'><strong></strong></font></td>
                                </tr>
                                </table>
                                ";
                    #endregion
                    Cobranza.SendMail mail = new SendMail();

                    if (filas > 0)
                    {
                        if (!string.IsNullOrEmpty(CorreoCliente))
                        {
                            string _referencia = String.Format( "<br><br><b>RECUERDE REALIZAR SU PAGO EN LA CUENTA BANAMEX: {0}.<b>", Referencia);

                            mail.EnviarEstadoCuenta(CorreoCliente, Mensaje + txtComentarios.Text + tabla + _referencia, "ESTADO DE CUENTA DISTRIBUIDORA PJ S.A. DE CV. Cliente: " + CardCode + " - " + CardName, CorreoJefa + ";" + CorreoVendedor, Jefa, CorreoJefa, string.Empty);
                            
                            //mail.EnviarEstadoCuenta("jose.olivos@pj.com.mx", Mensaje + txtComentarios.Text + tabla + _referencia, "Estado de Cuenta Cliente: " + CardCode + " - " + CardName, "jose.olivos@pj.com.mx", Jefa, CorreoJefa, string.Empty);

                            foreach (DataRow row in _resul.Rows)
                            {
                                if (row.Field<bool>("Enviar"))
                                {
                                    //Validaciones de correos Jefa-Vendedor-Cliente
                                    //Validaciones de Referencia
                                    this.RegistraEnviado(row.Field<int>("Factura").ToString());
                                }

                            }

                            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                            {
                                using (SqlCommand command = new SqlCommand("sp_EstadoCuenta", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                                    command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                                    command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);
                                    connection.Open();
                                    command.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("El Estado de cuenta se ha enviado a: " + CorreoCliente, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("El cliente no tiene asignado una cuenta de correo electronico " , "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmEdoCuenta_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }
    }
}
