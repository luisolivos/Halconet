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
using System.IO;
using System.Collections;

namespace Ventas
{
    public partial class Logueo : Form
    {
        /// <summary>
        /// Función que inicializa el formulario
        /// </summary>
        public Logueo()
        {
            InitializeComponent();
        }
        #region PARÁMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);

        public int ClaveEntidad;
        public string NombreUsuario;
        public string Contrasenha;
        public int Rol;
        public int Vendedor;
        public string Sucursal;

        #endregion

        #region EVENTOS
        ///<sumary>
        ///Evento desencadenado al presionar la tecla enter estando en el txt Usuario
        /// </sumary>
        private void txtUsuario_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                //if (!String.IsNullOrEmpty(txtUsuario.Text) && !String.IsNullOrEmpty(txtContrasena.Text))
                    btnIngresar_Click(sender, e);
                //else if (String.IsNullOrEmpty(txtContrasena.Text))
                  //  txtContrasena.Focus();
            }

        }

        ///<sumary>
        ///Evento desencadenado al presionar la tecla enter estando en el txt Password
        /// </sumary>
        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                //if (!String.IsNullOrEmpty(txtUsuario.Text) && !String.IsNullOrEmpty(txtContrasena.Text))
                    btnIngresar_Click(sender, e);
                //else if (String.IsNullOrEmpty(txtUsuario.Text))
                //  txtUsuario.Focus();
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer clik sobre el btnIngresar
        /// Valida si el usuario y la contraseña son correctos y permite el acceso al sistema
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsuario.Text != string.Empty && txtContrasena.Text != string.Empty)
                {
                    NombreUsuario = txtUsuario.Text;
                    Contrasenha = txtContrasena.Text;

                    SqlCommand command = new SqlCommand("SGUV_Usuarios", conection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaSGUV.Verificar);
                    command.Parameters.AddWithValue("@ClaveEntidad", 0);
                    command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                    command.Parameters.AddWithValue("@Contrasenha", Contrasenha);
                    command.Parameters.AddWithValue("@Rol", 0);
                    command.Parameters.AddWithValue("@Vendedor", 0);
                    SqlParameter ValidaUsuario = new SqlParameter("@ValidaUsuario", 0);
                    ValidaUsuario.Direction = ParameterDirection.Output;
                    command.Parameters.Add(ValidaUsuario);
                    conection.Open();
                    command.ExecuteNonQuery();
                    int Validar = Convert.ToInt32(command.Parameters["@ValidaUsuario"].Value.ToString());

                    LimpiarCampos();
                    if (Validar == (int)Constantes.ResultadoVerficacionUsuario.SiExisteElUsuario)
                    {
                        SqlCommand command2 = new SqlCommand("SGUV_Usuarios", conection);
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.TipoConsultaSGUV.ConsultarPorUsuario);
                        command2.Parameters.AddWithValue("@ClaveEntidad", 0);
                        command2.Parameters.AddWithValue("@Usuario", NombreUsuario);
                        command2.Parameters.AddWithValue("@Contrasenha", Contrasenha);
                        command2.Parameters.AddWithValue("@Rol", 0);
                        command2.Parameters.AddWithValue("@Vendedor", 0);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command2;
                        adapter.Fill(table);

                        foreach (DataRow row in table.Rows)
                        {
                            ClaveEntidad = row.Field<int>("ClaveEntidad");
                            NombreUsuario = row.Field<string>("NombreUsuario");
                            Contrasenha = row.Field<string>("Contrasenha");
                            Rol = row.Field<int>("NuevoRol");
                            Vendedor = row.Field<int>("SlpCode");
                            Sucursal = row.Field<string>("Sucursal");
                            

                            ClasesSGUV.Login.Id_Usuario = row.Field<int>("ClaveEntidad");
                            ClasesSGUV.Login.Rol = Rol;
                            ClasesSGUV.Login.NombreUsuario = NombreUsuario;
                            ClasesSGUV.Login.Sucursal = Sucursal;
                            ClasesSGUV.Login.Vendedor1 = Vendedor;
                            ClasesSGUV.Login.Edit = row.Field<string>("Edit");
                            ClasesSGUV.Login.Usuario = row.Field<string>("Nombre");
                            //if (checkBox1.Checked)
                            //{
                            //    System.IO.StreamWriter file = new System.IO.StreamWriter("log.txt", false);
                            //    file.WriteLine("[Usuario]\t" + NombreUsuario);
                            //    file.WriteLine("[Password]\t" + Contrasenha);
                            //    file.Close();
                            //}
                            //else
                            //{
                            //    System.IO.StreamWriter file = new System.IO.StreamWriter("log.txt", false);
                            //    file.WriteLine("");
                            //    file.Close();
                            //}

                        }
                        //notifyIcon1.Visible = true;
                        //notifyIcon1.Text = "HalcoNET";
                        //notifyIcon1.ShowBalloonTip(2000);
                        conection.Close();
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        if (Validar == (int)Constantes.ResultadoVerficacionUsuario.NoExisteElUsuario)
                        {
                            conection.Close();
                            MessageBox.Show("Nombre de Usuario o contraseña incorrectas. Vuelva a intentarlo.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            LimpiarCampos();
                            txtUsuario.Focus();
                            this.DialogResult = DialogResult.None;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Necesita ingresar el Nombre del Usuario y la Contraseña", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                }
            }
            catch (Exception ex)
            {
                LimpiarCampos();
                txtUsuario.Focus();
                conection.Close();
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
            }
        }

        #endregion


        #region FUNCIONES

        /// <summary>
        /// Método que limpia los campos usados
        /// </summary>
        public void LimpiarCampos()
        {
            txtUsuario.Text = string.Empty;
            txtContrasena.Text = string.Empty;
        }

        #endregion   

        private void Logueo_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.Text = "HalcoNET " + ClasesSGUV.Propiedades.Version;
            label3.Text = ClasesSGUV.Propiedades.Version;
            //try
            //{
            //    StreamReader objReader = new StreamReader("log.txt");
            //    string sLine = "";
            //    ArrayList arrText = new ArrayList();

            //    while (sLine != null)
            //    {
            //        sLine = objReader.ReadLine();
            //        if (sLine != null)
            //            arrText.Add(sLine);
            //    }
            //    objReader.Close();

            //    string[] Datos = new string[10];
            //    int x = 0;
            //    foreach (string sOutput in arrText)
            //    {
            //        string[] a = sOutput.Split('\t');
            //        Datos[x] = a[1];
            //        x++;
            //    }
            //    if (Datos.Count() > 0)
            //    {
            //        txtUsuario.Text = Datos[0];
            //        txtContrasena.Text = Datos[1];
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }

       

        
    }   
}
