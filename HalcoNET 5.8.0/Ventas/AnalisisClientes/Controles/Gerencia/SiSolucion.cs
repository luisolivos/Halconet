using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.AnalisisClientes.Controles.Gerencia
{
    public partial class SiSolucion : UserControl
    {
        String Cliente;
        string Nombre;
        decimal VtaMensual;
        decimal VtaLinea;
        string Linea;

        public SiSolucion(string _cliente, string _nombre, decimal _vtamensual, decimal _vtalinea, string _linea)
        {
            InitializeComponent();
            Cliente = _cliente;
            Nombre = _nombre;
            VtaMensual = _vtamensual;
            VtaLinea = _vtalinea;
            Linea = _linea;
        }

        private void SiSolucion_Load(object sender, EventArgs e)
        {
            lblName.Text = "Línea: " + Linea;
            txtCliente.Text = Cliente;
            txtNombre.Text = Nombre;
            txtVtaMensual.Text = VtaMensual.ToString("C2");
            txtVtaLinea.Text = VtaLinea.ToString("C2");
        }

        private void kryptonTextBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal compromiso = Convert.ToDecimal(txtCompromiso.Text);

                txtAumento.Text = (1 - (VtaLinea / compromiso)).ToString("P2");
            }
            catch (Exception)
            {
                
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCompromisoPJ.Text))
                {
                    if (!string.IsNullOrEmpty(txtProblema.Text))
                    {
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("PJ_AnalisisVentas", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.CommandTimeout = 0;
                                ///////////////////////////////////
                                command.Parameters.AddWithValue("@TipoConsulta", 6);
                                command.Parameters.AddWithValue("@Pregunta", 0);
                                command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                                command.Parameters.AddWithValue("@Letra", string.Empty);
                                command.Parameters.AddWithValue("@Especificacion", txtProblema.Text);
                                command.Parameters.AddWithValue("@Linea", Linea);
                                command.Parameters.AddWithValue("@Cliente", Cliente);
                                command.Parameters.AddWithValue("@Articulo", string.Empty);
                                command.Parameters.AddWithValue("@PrecioPJ", Convert.ToDecimal(txtComsumoLinea.Text));
                                command.Parameters.AddWithValue("@PrecioComp",Convert.ToDecimal(txtCompromiso.Text));
                                command.Parameters.AddWithValue("@Nombre", txtCompromisoPJ.Text);

                                connection.Open();

                                int x = command.ExecuteNonQuery();
                                if (x > 0)
                                {
                                    MessageBox.Show("Registro Exitoso.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    btnFinalizar.Enabled = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo [Problema por arreglar] no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El campo [Compromiso PJ] no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.Parent.Controls.Remove(this);
        }

        private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                toolStripStatusLabel1_Click(sender, e);
            }
        }
    }
}
