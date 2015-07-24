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


namespace Ventas.Ventas.ScoreCard.Comentarios
{
    public partial class LeerMensaje : Form
    {
        public String Codigo;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);


        public LeerMensaje(string _codigoMensaje)
        {
            Codigo = _codigoMensaje;
            InitializeComponent();
        }

        private void LeerMensaje_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                commandVendedor.CommandType = CommandType.StoredProcedure;
                commandVendedor.Parameters.AddWithValue("@TipoConsulta", 5);
                commandVendedor.Parameters.AddWithValue("@SlpCode", string.Empty);
                commandVendedor.Parameters.AddWithValue("@Fecha", DateTime.Now);
                commandVendedor.Parameters.AddWithValue("@Bono", 0);
                commandVendedor.Parameters.AddWithValue("@From", string.Empty);
                commandVendedor.Parameters.AddWithValue("@Mensaje", Codigo);
                commandVendedor.CommandTimeout = 0;
                conection.Open();
                SqlDataReader reader = commandVendedor.ExecuteReader();

                while (reader.Read())
                {
                    txtPara.Text = reader.GetString(1);
                    txtDe.Text = reader.GetString(2);
                    txtFecha.Text = reader.GetDateTime(3).ToShortDateString();
                    txtMensaje.Text = reader.GetString(4);
                }

                SqlCommand command = new SqlCommand("PJ_VariasScoreCard", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 6);
                command.Parameters.AddWithValue("@SlpCode", string.Empty);
                command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                command.Parameters.AddWithValue("@Bono", 0);
                command.Parameters.AddWithValue("@From", string.Empty);
                command.Parameters.AddWithValue("@Mensaje", Codigo);
                command.CommandTimeout = 0;

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " +ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
