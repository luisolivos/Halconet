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

namespace Cobranza.Score
{
    public partial class NuevoObjetiv : Form
    {
        Clases.Logs log;

        public NuevoObjetiv()
        {
            InitializeComponent();
        }
        DataTable JefasCobranza = new DataTable();
        private void CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);
            
            JefasCobranza = table.Copy();
            cbCobranza.DataSource = table;
            cbCobranza.DisplayMember = "Nombre";
            cbCobranza.ValueMember = "Codigo";
        }

        private void NuevoObjetiv_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            this.CargarJefesCobranza();
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
        }

        private void cbCobranza_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string Opcion = Convert.ToString(cbCobranza.SelectedValue);
            string var = "";
            switch (Opcion)
            {
                case "01": var = "Puebla"; break;
                case "02": var = "Monterrey"; break;
                case "03": var = "Apizaco"; break;
                case "05": var = "Cordoba"; break;
                case "06": var = "Tepeaca"; break;
                case "16": var = "EdoMex"; break;
                case "18": var = "Guadalajara"; break;
                default: var = ""; break;
            }
            txtSucursal.Text = var;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            string query = "INSERT INTO ObjMesCobranza Values(@Jefa, @Sucursal, @From, @To, @Objetivo)";
            try
            {
                string Jefa = cbCobranza.Text;
                string Sucursal = txtSucursal.Text;
                DateTime From = dtInicial.Value;
                DateTime To = dtFinal.Value;
                
                if (!string.IsNullOrEmpty(Jefa))
                {
                    if (To > From)
                    {
                        if (!string.IsNullOrEmpty(txtObjetivo.Text))
                        {
                            Decimal Objetivo = Convert.ToDecimal(txtObjetivo.Text);
                            using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                            {
                                using (SqlCommand command = new SqlCommand(query, con))
                                {
                                    command.CommandType = CommandType.Text;
                                    con.Open();
                                    command.Parameters.AddWithValue("@Jefa", Jefa);
                                    command.Parameters.AddWithValue("@Sucursal", Sucursal);
                                    command.Parameters.AddWithValue("@From", From);
                                    command.Parameters.AddWithValue("@To", To);
                                    command.Parameters.AddWithValue("@Objetivo", Objetivo);

                                    command.ExecuteNonQuery();

                                    MessageBox.Show("Registro exitoso.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dtFinal.Value = DateTime.Now;
                                    dtInicial.Value = DateTime.Now;
                                    txtObjetivo.Clear();
                                }
                            }

                        }
                        else
                        {
                            errorProvider1.SetError(txtObjetivo, "Ingrese un objetivo.");
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(dtFinal, "El rango de fechas no es valido.");
                    }
                }
                else
                {
                    errorProvider1.SetError(cbCobranza, "Elija una jefa de cobranza.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NuevoObjetiv_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void NuevoObjetiv_FormClosing(object sender, FormClosingEventArgs e)
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
