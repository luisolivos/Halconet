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
    public partial class ModificarUsuario : Form
    {

        #region PARÁMETROS

        public SqlConnection conectionSGUV = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        public SqlConnection conectionPJ = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);        

        #endregion


        #region EVENTOS

        /// <summary>
        /// Función que inicializa el formulario
        /// </summary>
        /// <param name="claveEntidad">Clave Entidad</param>
        /// <param name="nombreUsuario">Nombre de Usuario</param>
        /// <param name="contraseña">Contraseña</param>
        /// <param name="rol">Rol</param>
        public ModificarUsuario(int claveEntidad, string nombreUsuario, string contraseña, int rol, int Vendedor)
        {
            InitializeComponent();

            //txtClave.Text = claveEntidad.ToString();
            //txtUsuario.Text = nombreUsuario;
            //txtContrasena.Text = contraseña;
            //switch (rol)
            //{
            //    case (int)ClasesSGUV.Propiedades.RolesHalcoNET.Vendedor:
            //        rdbVendedor.Checked = true;
            //        groupBoxVendedores.Visible = true;
            //        this.Height = 486;
            //        groupBox1.Height = 443;
            //        btnGuardar.Location = new Point(93, 361);
            //        break;
            //    case (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas:
            //        rdbGerente.Checked = true;
            //        groupBoxVendedores.Visible = false;
            //        break;
            //    case (int)C.Cobranza:
            //        rdbCobranza.Checked = true;
            //        groupBoxVendedores.Visible = false;
            //        break;
            //    case (int)Constantes.RolSistemaSGUV.Direccion:
            //        rdbDireccion.Checked = true;
            //        groupBoxVendedores.Visible = false;
            //        break;
            //}

            //CargarVendedores();
            //foreach (Control control in panel1.Controls)
            //{
            //    if (control is RadioButton)
            //    {
            //        RadioButton rdb = control as RadioButton;
            //        if (Convert.ToInt32(rdb.Name) == Vendedor)
            //        {
            //            rdb.Checked = true;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Evento que guarda las modificaciones en la BD
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            //try
            //{
            //     if (txtUsuario.Text.Length > 5 && txtUsuario.Text.Length < 19)
            //    {
            //        if (txtContrasena.Text.Length > 4 && txtContrasena.Text.Length < 19)
            //        {
            //            if (rdbVendedor.Checked == true || rdbGerente.Checked == true || rdbCobranza.Checked == true || rdbDireccion.Checked == true)
            //            {
            //                int vendedor = 0;
            //                foreach (Control control in panel1.Controls)
            //                {
            //                    if (control is RadioButton)
            //                    {
            //                        RadioButton rdb = control as RadioButton;
            //                        if (rdb.Checked == true)
            //                        {
            //                            vendedor = Convert.ToInt32(rdb.Name);
            //                        }
            //                    }
            //                }

            //                if (vendedor == 0 && rdbVendedor.Checked == true)
            //                {
            //                    MessageBox.Show("El campo 'Vendedor' es obligatorio cuando se elige el rol 'Vendedor'.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                }
            //                else
            //                {
            //                    if (rdbVendedor.Checked != true)
            //                    {
            //                        vendedor = 0;
            //                    }

            //                    SqlCommand command = new SqlCommand("SGUV_Usuarios", conectionSGUV);
            //                    command.CommandType = CommandType.StoredProcedure;
            //                    command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaSGUV.ConsultarSiExisteNombreUsuarioYClave);
            //                    command.Parameters.AddWithValue("@ClaveEntidad", txtClave.Text);
            //                    command.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
            //                    command.Parameters.AddWithValue("@Contrasenha", string.Empty);
            //                    command.Parameters.AddWithValue("@Rol", 0);
            //                    command.Parameters.AddWithValue("@Vendedor", 0);
            //                    SqlParameter ValidaUsuario = new SqlParameter("@ValidaUsuario", 0);
            //                    ValidaUsuario.Direction = ParameterDirection.Output;
            //                    command.Parameters.Add(ValidaUsuario);
            //                    conectionSGUV.Open();
            //                    command.ExecuteNonQuery();
            //                    int Validar = Convert.ToInt32(command.Parameters["@ValidaUsuario"].Value.ToString());

            //                    if (Validar == (int)Constantes.ResultadoVerficacionUsuario.NoExisteElUsuario)
            //                    {
            //                        int Rol = 0;
            //                        if (rdbVendedor.Checked)
            //                            Rol = (int)Constantes.RolSistemaSGUV.Vendedor;
            //                        if (rdbGerente.Checked)
            //                            Rol = (int)Constantes.RolSistemaSGUV.GerenteVentas;
            //                        if (rdbCobranza.Checked)
            //                            Rol = (int)Constantes.RolSistemaSGUV.Cobranza;
            //                        if (rdbDireccion.Checked)
            //                            Rol = (int)Constantes.RolSistemaSGUV.Direccion;

            //                        SqlCommand command2 = new SqlCommand("SGUV_Usuarios", conectionSGUV);
            //                        command2.CommandType = CommandType.StoredProcedure;
            //                        command2.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaSGUV.Modificar);
            //                        command2.Parameters.AddWithValue("@ClaveEntidad", txtClave.Text);
            //                        command2.Parameters.AddWithValue("@Usuario", txtUsuario.Text);
            //                        command2.Parameters.AddWithValue("@Contrasenha", txtContrasena.Text);
            //                        command2.Parameters.AddWithValue("@Rol", Rol);
            //                        command2.Parameters.AddWithValue("@Vendedor", vendedor);

            //                        command2.ExecuteNonQuery();
            //                        conectionSGUV.Close();

            //                        MessageBox.Show("El usuario ha sido modificado correctamente", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    }
            //                    else
            //                    {
            //                        conectionSGUV.Close();
            //                        MessageBox.Show("Ya existe en la Base de Datos un Usuario con el mismo Nombre. Cambie el campo 'Nombre de Usuario'.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("No se ha elegido ningun Rol.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("El campo 'Contraseña' debe contener entre 5 y 20 caracteres.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //    }
            //     else
            //     {
            //         MessageBox.Show("El campo 'Usuario' debe contener entre 6 y 20 caracteres.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        /// <summary>
        /// Muestra u oculta el groupbox con los vendedores
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void rdbVendedor_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbVendedor.Checked == true)
            {
                this.Height = 486;
                groupBox1.Height = 443;
                btnGuardar.Location = new Point(93, 361);
                groupBoxVendedores.Visible = true;
            }
            else if (rdbVendedor.Checked == false)
            {
                this.Height = 386;
                groupBox1.Height = 343;
                btnGuardar.Location = new Point(93, 261);
                groupBoxVendedores.Visible = false;
            }
        }

        #endregion      


        #region FUNCIONES

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

        private void ModificarUsuario_Load(object sender, EventArgs e)
        {

        }

    }
}
