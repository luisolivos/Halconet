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
    public partial class BandejaMensajes : Form
    {
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

        public BandejaMensajes(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            RolUsuario = rolUsuario;
            NombreUsuario = nombreUsuario;
            CodigoVendedor = codigoVendedor;
            Sucursal = sucursal;
            InitializeComponent();
        }

        #region Eventos
        private void BandejaMensajes_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                commandVendedor.CommandType = CommandType.StoredProcedure;
                commandVendedor.Parameters.AddWithValue("@TipoConsulta", 4);
                commandVendedor.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                commandVendedor.Parameters.AddWithValue("@Fecha", DateTime.Now);
                commandVendedor.Parameters.AddWithValue("@Bono", 0);
                commandVendedor.Parameters.AddWithValue("@From", NombreUsuario);
                commandVendedor.Parameters.AddWithValue("@Mensaje", string.Empty);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = commandVendedor;
                adapter.SelectCommand.CommandTimeout = 0;

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;

                DataGridViewButtonColumn botonDetails = new DataGridViewButtonColumn();
                {
                    botonDetails.Name = "Ver";
                    botonDetails.HeaderText = "Ver";
                    botonDetails.Text = "Ver";
                    botonDetails.Width = 130;
                    botonDetails.UseColumnTextForButtonValue = true;
                }
                dataGridView1.Columns.Add(botonDetails);

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 50;
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
                {
                }
           
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == 0)
                    {
                        
                        string nombre = Convert.ToString(dataGridView1.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[1].Value);
                        LeerMensaje details = new LeerMensaje(nombre);
                        details.ShowDialog();
                        //BandejaMensajes_Load(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
