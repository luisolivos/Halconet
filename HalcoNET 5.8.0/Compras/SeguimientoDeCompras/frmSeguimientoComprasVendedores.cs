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

namespace Compras
{
    public partial class SeguimientoComprasVendedores : Form
    {
        public int SlpCode;
        public string Tipo;
        Clases.Logs log;

        public SeguimientoComprasVendedores(int _slpCode, string _tipo)
        {
            InitializeComponent();
            SlpCode = _slpCode;
            Tipo = _tipo;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            label1.Text = _tipo + " pendientes por vender: ";
        }

        private void SeguimientoComprasVendedores_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_SeguimientoCompras", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@Almacen", string.Empty);
                        command.Parameters.AddWithValue("@Tipo", string.Empty);
                        command.Parameters.AddWithValue("@Vendedor", SlpCode);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        gridClasificacion.DataSource = table;

                        label1.Text += table.Compute("SUM([Cantidad pendiente])", "").ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbVendedores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void SeguimientoComprasVendedores_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
               
            }
        }

        private void SeguimientoComprasVendedores_FormClosing(object sender, FormClosingEventArgs e)
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
