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

namespace Ventas
{
    public partial class AgregarUsuario : Form
    {

        #region PARÁMETROS

        public SqlConnection conectionSGUV = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        public SqlConnection conectionPJ = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        #endregion


        #region EVENTOS

        public AgregarUsuario()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento load
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void AgregarUsuario_Load(object sender, EventArgs e)
        {
            groupBoxVendedores.Visible = false;
            CargarVendedores();
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnAgregar
        /// Agrega un nuevo usuario en la BD
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsuario.Text.Length > 5 && txtUsuario.Text.Length < 19)
                {
                    if (txtContrasena.Text.Length > 4 && txtContrasena.Text.Length < 19)
                    {
                        if ((cmbRol.SelectedIndex + 1) != 0)
                        {
                            int vendedor = 0;


                            foreach (Control item in panel1.Controls)
                            {
                                if (item is RadioButton)
                                {
                                    RadioButton r = item as RadioButton;
                                    if(r.Checked)
                                        vendedor = int.Parse(r.Name);
                                }
                            }

                            if (vendedor == 0 && (cmbRol.SelectedIndex + 1) == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                            {
                                MessageBox.Show("El campo 'Vendedor' es obligatorio cuando se elige el rol 'Vendedor'.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                SqlCommand command = new SqlCommand("SGUV_Usuarios", conectionSGUV);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaSGUV.ConsultarSiExisteNombreUsuario);
                                command.Parameters.AddWithValue("@ClaveEntidad", 0);
                                command.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                                command.Parameters.AddWithValue("@Contrasenha", string.Empty);
                                command.Parameters.AddWithValue("@Rol", 0);
                                command.Parameters.AddWithValue("@Vendedor", 0);
                                SqlParameter ValidaUsuario = new SqlParameter("@ValidaUsuario", 0);
                                ValidaUsuario.Direction = ParameterDirection.Output;
                                command.Parameters.Add(ValidaUsuario);
                                conectionSGUV.Open();
                                command.ExecuteNonQuery();
                                int Validar = Convert.ToInt32(command.Parameters["@ValidaUsuario"].Value.ToString());

                                if (Validar == (int)Constantes.ResultadoVerficacionUsuario.NoExisteElUsuario)
                                {
                                    int rol = 0;

                                    rol = cmbRol.SelectedIndex + 1;

                                    SqlCommand command2 = new SqlCommand("SGUV_Usuarios", conectionSGUV);
                                    command2.CommandType = CommandType.StoredProcedure;
                                    command2.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaSGUV.Agregar);
                                    command2.Parameters.AddWithValue("@ClaveEntidad", 0);
                                    command2.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
                                    command2.Parameters.AddWithValue("@Contrasenha", txtContrasena.Text);
                                    command2.Parameters.AddWithValue("@Rol", rol);
                                    command2.Parameters.AddWithValue("@Vendedor", vendedor);

                                    command2.ExecuteNonQuery();
                                    conectionSGUV.Close();
                                    LimpiarCampos();

                                    MessageBox.Show("El usuario ha sido agregado correctamente", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    conectionSGUV.Close();
                                    MessageBox.Show("Ya existe en la Base de Datos un Usuario con el mismo Nombre. Cambie el campo 'Nombre de Usuario'.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se ha elegido ningun Rol.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo 'Contraseña' debe contener entre 5 y 20 caracteres.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("El campo 'Usuario' debe contener entre 6 y 20 caracteres.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        #endregion


        #region FUNCIONES

        /// <summary>
        /// Función que limpia los campos usados
        /// </summary>
        private void LimpiarCampos()
        {
            txtUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;
            //rdbVendedor.Checked = false;
            //rdbGerente.Checked = false;
            //rdbCobranza.Checked = false;
            //rdbDireccion.Checked = false;
        }

        /// <summary>
        /// Método que carga los vendedores en el groupbox
        /// </summary>
        public void CargarVendedores()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conectionPJ);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            int i = 0;
            foreach (DataRow row in table.Rows)
            {
                RadioButton rdb = new RadioButton();
                rdb.Text = row.Field<string>("Nombre");
                rdb.Name = row.Field<Int32>("Codigo").ToString();
                rdb.Width = 200;
                panel1.Controls.Add(rdb);
                rdb.Location = new Point(10, 0 + i * 22);
                i++;
            }     
        }

        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbRol.SelectedIndex + 1) == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal || (cmbRol.SelectedIndex + 1) == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                this.Height = 472;
                groupBox1.Height = 428;
                btnAgregar.Location = new Point(95, 360);
                groupBoxVendedores.Visible = true;
            }
            else //if (rdbVendedor.Checked == false)
            {
                this.Height = 372;
                groupBox1.Height = 328;
                btnAgregar.Location = new Point(95, 260);
                groupBoxVendedores.Visible = false;
            }
        }

    }
}
