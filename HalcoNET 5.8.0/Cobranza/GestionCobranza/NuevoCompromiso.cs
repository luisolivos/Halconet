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


namespace Cobranza.AntiguedadSaldos
{
    public partial class NuevoCompromiso : Form
    {
        public DataTable Facturas = new DataTable();
        public DateTime Fecha;
        public decimal Saldo;
        public string Compromiso;
        public string Cliente;
        public string Tipo;
        private string Usuario;
        Clases.Logs log;

        public NuevoCompromiso(DataTable _dt, string _cliente, string _formato, string _tipo, string _usuario)
        {
            InitializeComponent();

            dtFecha.CustomFormat = _formato;
            Cliente = _cliente;
            Tipo = _tipo;
            dtFecha.Width = 257;
            Usuario = _usuario;

            if (Tipo != "03")
            {
                dtFecha.Width = 126;
                Saldo = Convert.ToDecimal(_dt.Compute("SUM([Saldo])", ""));

                var t = (from item in _dt.AsEnumerable()
                         select new
                         {
                             Factura = item.Field<Int32>("Factura"),
                             Total = item.Field<decimal>("Importe Original"),
                             Pagos = item.Field<decimal>("Pagos aplicados"),
                             Saldo = item.Field<decimal>("Saldo")
                         }).ToList();

                Facturas = Cobranza.ListConverter.ToDataTable(t);
                gridFacturas.DataSource = Facturas;
                log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
            }
        }

        private void NuevoCompromiso_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            txtMonto.Text = Saldo.ToString("C");
            txtComprometido.Text = Saldo.ToString();

            if (gridFacturas.Columns.Count > 0)
            {
                gridFacturas.Columns[1].DefaultCellStyle.Format = "C2";
                gridFacturas.Columns[2].DefaultCellStyle.Format = "C2";
                gridFacturas.Columns[3].DefaultCellStyle.Format = "C2";

                gridFacturas.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                gridFacturas.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                gridFacturas.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (Tipo == "03")
            {
                txtMonto.Enabled = false;
                txtComprometido.Enabled = false;
                gridFacturas.Enabled = false;

                rbNormal.Checked = false;
                rbRecuperacion.Checked = false;

                rbNormal.Visible = false;
                rbRecuperacion.Visible = false;


                button1.Text = "Guardar\r\nllamada";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = "";
            string tipo = "";
            if (!String.IsNullOrEmpty(rtCompromiso.Text))
            {
                if (rbNormal.Checked)
                    tipo = rbNormal.AccessibleDescription;
                else if (rbRecuperacion.Checked)
                    tipo = rbRecuperacion.AccessibleDescription;
                else tipo = Tipo;


                string Num = getDocument();
                try
                {
                    decimal comprometido = Convert.ToDecimal(txtComprometido.Text);

                    if (tipo != "03")
                    {
                        foreach (DataRow item in Facturas.Rows)
                        {
                            status = InsertarCompromiso(dtFecha.Value, item.Field<decimal>("Saldo"), rtCompromiso.Text, item.Field<Int32>("Factura"), Cliente, Num, comprometido, tipo);
                        }
                    }
                    else
                    {
                        status = InsertarCompromiso(dtFecha.Value, 0, rtCompromiso.Text, 0, Cliente, Num, 0, tipo);
                    }
                    MessageBox.Show(status, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (status.Equals("Registro exitoso!"))/* */
                    {
                        dtFecha.Enabled = false;
                        rtCompromiso.Enabled = false;
                        gridFacturas.Enabled = false;
                        button1.Enabled = false;
                        txtComprometido.Enabled = false;
                    }
                }
                catch (Exception)
                {

                }
            }
            else
            {
                MessageBox.Show("El campo 'Compromiso' no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string InsertarCompromiso(DateTime _fecha, decimal _monto, string _comentario, int _factura, string _cliente, string _num, decimal _comprometido, string _tipo)
        {
            string mensaje = "Error al realizar la operación";
            using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Fecha", _fecha);
                    command.Parameters.AddWithValue("@FechaFinal", _fecha);
                    command.Parameters.AddWithValue("@Monto", _monto);
                    command.Parameters.AddWithValue("@Comentario", _comentario);
                    command.Parameters.AddWithValue("@Factura", _factura);
                    command.Parameters.AddWithValue("@Otro", _cliente);
                    command.Parameters.AddWithValue("@NumCompromiso", _num);
                    command.Parameters.AddWithValue("@Comprometido", _comprometido);
                    command.Parameters.AddWithValue("@Tipo", _tipo);

                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    command.ExecuteNonQuery();

                    mensaje = Convert.ToString(command.Parameters["@Message"].Value);
                }
            }
            return mensaje;
        }


        private string getDocument()
        {
            string num = "";
            using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                    command.Parameters.AddWithValue("@Monto", 0);
                    command.Parameters.AddWithValue("@Comentario", string.Empty);
                    command.Parameters.AddWithValue("@Factura", 0);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@NumCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Comprometido", 0);
                    command.Parameters.AddWithValue("@Tipo", "");

                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    //command.ExecuteNonQuery();
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        num = Convert.ToString(reader[0]);
                    }

                    //mensaje = Convert.ToString(command.Parameters["@Message"].Value);
                }
            }
            return num;
        }

        private void NuevoCompromiso_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
    
            }
        }

        private void NuevoCompromiso_FormClosing(object sender, FormClosingEventArgs e)
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
