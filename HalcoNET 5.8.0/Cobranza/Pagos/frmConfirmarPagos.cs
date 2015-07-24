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

namespace Cobranza.Pagos
{
    public partial class frmConfirmarPagos : Form
    {
        DataTable Datos;
        decimal Demasia;
        string _formaPago;

        public string FormaPago
        {
            get { return _formaPago; }
            set { _formaPago = value; }
        }

        decimal _montoPago;

        public decimal MontoPago
        {
            get { return _montoPago; }
            set { _montoPago = value; }
        }

        public frmConfirmarPagos(DataTable table, decimal demasia, bool cmb, decimal _pago)
        {
            _montoPago = _pago;
            Datos = table;
            Demasia = demasia;
            InitializeComponent();

            lblForma.Visible = cmb;
            cbForma.Visible = cmb;

            lblMonto.Visible = !cmb;
            txtMontoPago.Visible = !cmb;

            if (cmb)
                txtMontoPago.Text = "0";
            else txtMontoPago.Text = _pago.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfirmarPagos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                string cadena = string.Empty;
                txtDemasia.Text = Demasia.ToString("C2");

                if (Demasia == 0)
                {
                    txtDemasia.Visible = false;
                    label5.Visible = false;
                }

                foreach (DataRow item in Datos.Rows)
                {
                    cadena += item.Field<string>("Cliente") + "\t" + item.Field<int>("Factura") + "\t\t" + item.Field<decimal>("Pago").ToString("C4") + "\r\n";
                }

                richTextBox1.Text = cadena;


                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Pagos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 21);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        DataTable tbl = new DataTable();
                        da.Fill(tbl);

                        cbForma.DataSource = tbl;
                        cbForma.DisplayMember = "Nombre";
                        cbForma.ValueMember = "Codigo";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _formaPago = cbForma.SelectedValue.ToString();

                if (!lblForma.Visible)
                {
                  

                    if (string.IsNullOrEmpty(txtMontoPago.Text))
                    {
                        MessageBox.Show("Debes ingresar el monto del pago", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if ((Convert.ToDecimal(txtMontoPago.Text) - _montoPago) < -10)
                        {
                            MessageBox.Show("La operación no se puede completar. [-$10]", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if ((Convert.ToDecimal(txtMontoPago.Text) - _montoPago) > 10)
                        {
                            DialogResult re1 = MessageBox.Show("Existe una diferencia por " + ((Convert.ToDecimal(txtMontoPago.Text) - _montoPago)).ToString("C2") + " que se aplicará a la cuenta de demasías.\r\n¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (re1 == DialogResult.OK)
                            {
                                _montoPago = Convert.ToDecimal(txtMontoPago.Text);
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            }
                        }
                        else
                        {
                            _montoPago = Convert.ToDecimal(txtMontoPago.Text);
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                       
                       

                  
                    }  
                    
                   
                }
                else
                {
                    if (string.IsNullOrEmpty(_formaPago))
                    {
                        MessageBox.Show("Debes seleccionar la forma de pago.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
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
