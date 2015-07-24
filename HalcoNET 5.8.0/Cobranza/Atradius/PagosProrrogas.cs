using System;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza
{
    public partial class PagosProrrogas : Form
    {
        public PagosProrrogas()
        {
            InitializeComponent();
        }

        private void PagosProrrogas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_AtradiusP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 11);
                        command.Parameters.AddWithValue("@Desde", DateTime.Now);
                        command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                        command.Parameters.AddWithValue("@CardCode", string.Empty);
                        command.Parameters.AddWithValue("@CardName", string.Empty);
                        command.Parameters.AddWithValue("@DocEntry", 0);
                        command.Parameters.AddWithValue("@Docnum", 0);

                        SqlParameter ValidaUsuario = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                        ValidaUsuario.Direction = ParameterDirection.Output;
                        command.Parameters.Add(ValidaUsuario);

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable tble = new DataTable();
                        da.SelectCommand = command;
                        da.Fill(tble);

                        dgvDatos.DataSource = tble;

                        dgvDatos.Columns[1].DefaultCellStyle.Format = "C2";
                        dgvDatos.Columns[2].DefaultCellStyle.Format = "C2";
                        dgvDatos.Columns[3].DefaultCellStyle.Format = "C2";

                        dgvDatos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvDatos.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvDatos.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                string _mensajeMail = @"<FONT FACE='arial' SIZE=5>
                                        <H3>FACTURAS PAGADAS EN PRORROGA " + DateTime.Now.ToShortDateString() +@"</H3><BR>
                                         <table BORDER = 1>
                                            <TR>
                                                <TH>FACTURA</TH>
                                                <TH>SALDO EN<br>PRORROGA</TH>
                                                <TH>PAGOS</TH>
                                                <TH>SALDO ACTUAL</TH>
                                            </TR>
                                        ";
                foreach (DataGridViewRow item in dgvDatos.Rows)
                {
                    _mensajeMail += @"<TR>
                                         <TD>" + item.Cells[0].Value + "</TD>" +
                                        "<TD align='right'>" + Convert.ToDecimal(item.Cells[1].Value).ToString("C2") + "</TD>" +
                                        "<TD align='right'>" + Convert.ToDecimal(item.Cells[2].Value).ToString("C2") + "</TD>" +
                                        "<TD align='right'>" + Convert.ToDecimal(item.Cells[3].Value).ToString("C2") + "</TD>" 
                                    + "</TR>";
                }

                _mensajeMail += @"</table>";

                SendMail mail = new SendMail();

                string _correosDestinatarios = System.Configuration.ConfigurationSettings.AppSettings["CorreosAtradiusPagos"].ToString();
                if (dgvDatos.Rows.Count > 0)
                {
                    Clases.CrearPDF pdf = new Clases.CrearPDF();
                    string ruta = pdf.ToPDFAtradius(dgvDatos, string.Empty, "Facturas pagadas en prorroga " + DateTime.Now.ToShortDateString());
                    if (!string.IsNullOrEmpty(ruta))
                    {
                        mail.EnviarAtradius(ruta, _correosDestinatarios, _mensajeMail, "Facturas pagadas en prorroga");
                        MessageBox.Show("Envio Exitoso", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
