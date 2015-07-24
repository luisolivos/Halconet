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

namespace Presupuesto
{
    public partial class ComentariosGlobal : Form
    {
        private string Cuenta;
        private string NombreCuenta;
        private string FechaInicial;
        private string FechaFinal;
        private string Sucursal;
        public bool Actualizar = false;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        private int RolUsuario;

        #region EVENTOS
        public ComentariosGlobal(string cuenta, string nombrecuenta, string fechainicial, string fechafinal, int _rol)
        {
            Cuenta = cuenta;
            NombreCuenta = nombrecuenta;
            FechaInicial = fechainicial;
            FechaFinal = fechafinal;
            RolUsuario = _rol;
            InitializeComponent();
        }

        private void ComentariosGlobal_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.Text = NombreCuenta;
            try
            {
                SqlCommand command = new SqlCommand("PJ_PresupuestoCuenta", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 5);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Cuenta", Cuenta);
                command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                command.Parameters.AddWithValue("@Comentario", string.Empty);
                command.Parameters.AddWithValue("@Rol", RolUsuario);
                command.CommandTimeout = 0;

                DataTable tble = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(tble);

                dataGridView1.DataSource = tble;
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].Width = 290;

                DataGridViewTextBoxColumn col = (DataGridViewTextBoxColumn)dataGridView1.Columns[1];
                col.MaxInputLength = 400;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { 
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conection.Open();
                foreach (DataGridViewRow  item in dataGridView1.Rows)
                {
                    Actualizar = true;
                    string Comentario = Convert.ToString(item.Cells[1].Value);//////<-------ERROR
                    Sucursal = Convert.ToString(item.Cells[0].Value);

                    if (!String.IsNullOrEmpty(Comentario.Trim()))
                    {
                        SqlCommand command = new SqlCommand("PJ_PresupuestoCuenta", conection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Sucursal", Sucursal);
                        command.Parameters.AddWithValue("@Cuenta", Cuenta);
                        command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                        command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                        command.Parameters.AddWithValue("@Comentario", Comentario.Replace((char)(13), ' ').Trim());
                        command.Parameters.AddWithValue("@Rol", RolUsuario);
                        command.CommandTimeout = 0;
     
                        command.ExecuteNonQuery();
 
                    }
                    else
                        Actualizar = false;
                }
                MessageBox.Show("Comentarios almacenados.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception )
            {
            }
            finally
            {
                conection.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {

                this.Close();
            }
            catch (Exception )
            {
            }
            finally
            {

            }
        }
        #endregion 
    }
}
