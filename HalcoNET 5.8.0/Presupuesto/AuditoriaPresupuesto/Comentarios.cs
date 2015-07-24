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
    public partial class Comentarios : Form
    {
        private string Cuenta;
        private string NombreCuenta;
        private string FechaInicial;
        private string FechaFinal;
        private string Sucursal;
        public bool Actualizar = false;
        private int RolUsuario;
        private string Comentario;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        private bool Editar;
       
        public Comentarios(string cuenta, string nombrecuenta, string sucursal, string fechainicial, string fechafinal, int _rol, string _comentario, bool _editar)
        {
            Cuenta = cuenta;
            NombreCuenta = nombrecuenta;
            FechaInicial = fechainicial;
            FechaFinal = fechafinal;
            Sucursal = sucursal;
            RolUsuario = _rol;
            Comentario = _comentario;
            Editar = _editar;
            InitializeComponent();
        }


        #region EVENTOS
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Actualizar = true;
                string Comentario = textBox1.Text;
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
                conection.Open();
                
                if (!String.IsNullOrEmpty(Comentario))
                    command.ExecuteNonQuery();
                else
                    Actualizar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conection.Close();
            }
            this.Close();
        }

        private void Comentarios_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            tabPage1.Text = Sucursal;
            tabPage2.Text = "Global";

            this.Text = NombreCuenta;
            if (Sucursal.Equals("TODAS"))
            {
                label2.Visible = true;
                btnGuardar.Visible = false;
                btnCancelar.Location = new Point(btnGuardar.Location.X, btnGuardar.Location.Y);
                btnCancelar.Text = "Aceptar";
                textBox1.ReadOnly = true;
                tabControl1.TabPages.Remove(tabPage2);
            }

            textBox1.Text = Comentario;
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if(Editar)
                if (Sucursal == "TODAS")
                {
                    ComentariosGlobal frmComentario = new ComentariosGlobal(Cuenta, NombreCuenta, FechaInicial, FechaFinal, RolUsuario);
                    frmComentario.ShowDialog();
                    frmComentario.WindowState = FormWindowState.Normal;

                    if (frmComentario.Actualizar)
                        Comentarios_Load(sender, e);
                }
        }
        #endregion
    }
}
