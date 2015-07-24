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

namespace Ventas.AnalisisClientes.Controles
{
    public partial class Pregunta3E : UserControl
    {
        DataTable _Respuestas = new DataTable();

        public Pregunta3E(DataTable _respuestas)
        {
            InitializeComponent();
            _Respuestas = _respuestas;
        }

        private void Finalizar()
        {
            foreach (DataRow item in _Respuestas.Rows)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_AnalisisVentas";

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Pregunta", item.Field<Int32>("U_Pregunta"));
                        command.Parameters.AddWithValue("@Clasificacion", item.Field<string>("U_clasificacion"));
                        command.Parameters.AddWithValue("@Letra", item.Field<string>("U_Respuesta"));
                        command.Parameters.AddWithValue("@Especificacion", item.Field<string>("U_Especificacion"));
                        command.Parameters.AddWithValue("@Linea", item.Field<string>("U_Linea"));
                        command.Parameters.AddWithValue("@Cliente", item.Field<string>("U_Cliente"));

                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                        command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                        command.Parameters.AddWithValue("@Nombre", string.Empty);

                        command.CommandTimeout = 0;

                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.Parent.Controls.Remove(this);
        }

        private void Pregunta3E_Load(object sender, EventArgs e)
        {
            lblCliente.Text = "Cliente: " + Clases.Contantes.Nombre;

            txtCliente.Text = Clases.Contantes.Cliente;
            txtNombre.Text = Clases.Contantes.Nombre;

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            {
                using (SqlCommand command = new SqlCommand("select Top 1 CreditLine From OCRD Where CardCode = @Cliente", connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Cliente", Clases.Contantes.Cliente);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);

                    decimal limiteactual = (from item in table.AsEnumerable()
                                            select item.Field<decimal>("CreditLine")).FirstOrDefault();

                    txtLimiteActual.Text = limiteactual.ToString("C2");
                }
            }

            this.Focus();
        }

        private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnFinalizar.Enabled)
                if (e.KeyCode == Keys.Escape)
                {
                    toolStripStatusLabel1_Click(sender, e);
                }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            //Guardar consumo
            if(txtLimiteRequerido.Text != string.Empty)
            {
                this.Finalizar();

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_AnalisisVentas";

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 7);
                        command.Parameters.AddWithValue("@Pregunta", 0);
                        command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                        command.Parameters.AddWithValue("@Letra", string.Empty);
                        command.Parameters.AddWithValue("@Especificacion", string.Empty);
                        command.Parameters.AddWithValue("@Linea", Clases.Contantes.Linea);
                        command.Parameters.AddWithValue("@Cliente", Clases.Contantes.Cliente);

                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                        command.Parameters.AddWithValue("@PrecioComp", Convert.ToDecimal(txtLimiteRequerido.Text));
                        command.Parameters.AddWithValue("@Nombre", string.Empty);

                        connection.Open();

                        command.ExecuteNonQuery();

                        MessageBox.Show("Registro exitoso.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolHome.Visible = true;
                        toolBack.Visible = false;
                        btnFinalizar.Enabled = false;
                    }
                }
            }
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Parent.Controls)
            {
                if (!(item is Controles.Ranking))
                {
                    try
                    {
                        item.Visible = false;
                    }
                    catch (Exception)
                    {
                    }
                }

            }
        }
    }
}
