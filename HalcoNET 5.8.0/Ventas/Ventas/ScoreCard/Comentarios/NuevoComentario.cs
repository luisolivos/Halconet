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
    public partial class NuevoComentario : Form
    {
        public NuevoComentario(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            RolUsuario = rolUsuario;
            NombreUsuario = nombreUsuario;
            CodigoVendedor = codigoVendedor;
            Sucursal = sucursal;
            InitializeComponent();
        }

        private void NuevoComentario_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToShortDateString();
            CargarVendedores();
        }

        #region PARÁMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        public string NombreUsuario;
        public string Vendedores;
        public string Sucursales;
        public string Cliente;
        public int RolUsuario;
        public string Canales;
        public int CodigoVendedor;
        public string Sucursal;

        public string Mensaje;

        /// <summary>
        /// Enumerador de los tipos de consulta del ScoreCard
        /// </summary>
        private enum TipoConsulta
        {
            DiasMes = 1,
            DiasTranscurridos = 2,
            Clientes = 3,
            Vendedores = 4,
            Objetivo,
            PresupuestoMensual
        }



        #endregion        

        #region METODOS 
        /// <summary>
        /// Método que carga los Vendedores en el clbVendedor
        /// </summary>
        private void CargarVendedores()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                cbVendedor.DataSource = table;
                cbVendedor.DisplayMember = "Nombre";
                cbVendedor.ValueMember = "Codigo";
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 11);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                cbVendedor.DataSource = table;
                cbVendedor.DisplayMember = "Nombre";
                cbVendedor.ValueMember = "Codigo";
            }
        }       
        #endregion 

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Mensaje = txtMensaje.Text.Trim();
                int vendedor = 0;
                vendedor = (int)cbVendedor.SelectedValue;
                conection.Open();
                SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                commandVendedor.CommandType = CommandType.StoredProcedure;
                commandVendedor.Parameters.AddWithValue("@TipoConsulta", 3);
                commandVendedor.Parameters.AddWithValue("@SlpCode", vendedor);
                commandVendedor.Parameters.AddWithValue("@Fecha", DateTime.Now);
                commandVendedor.Parameters.AddWithValue("@Bono", 0);
                commandVendedor.Parameters.AddWithValue("@From", NombreUsuario);
                commandVendedor.Parameters.AddWithValue("@Mensaje", Mensaje);

                commandVendedor.ExecuteNonQuery();
                MessageBox.Show("El mensaje fue enviado con exito!", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtMensaje.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally{
                conection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
