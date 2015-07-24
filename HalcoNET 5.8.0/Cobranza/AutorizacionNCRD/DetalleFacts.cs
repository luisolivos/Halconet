using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.AutorizacionNCRD
{
    public partial class DetalleFacts : Form
    {
        public int Factura;
        public enum Columnas
        {
           Factura, Articulo, Nombre, Cantidd, PrecioSap,PrecioReal,PRecioPromocion
        }

        public void Formato()
        {
            gridDetalles.Columns[(int)Columnas.Factura].Width = 90;
            gridDetalles.Columns[(int)Columnas.Articulo].Width = 90;
            gridDetalles.Columns[(int)Columnas.Nombre].Width = 90;
            gridDetalles.Columns[(int)Columnas.Cantidd].Width = 90;
            gridDetalles.Columns[(int)Columnas.PrecioSap].Width = 90;
            gridDetalles.Columns[(int)Columnas.PrecioReal].Width = 90;
            gridDetalles.Columns[(int)Columnas.PRecioPromocion].Width = 90;

            gridDetalles.Columns[(int)Columnas.Cantidd].DefaultCellStyle.Format = "N0";
            gridDetalles.Columns[(int)Columnas.PrecioSap].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)Columnas.PrecioReal].DefaultCellStyle.Format = "C2";
            gridDetalles.Columns[(int)Columnas.PRecioPromocion].DefaultCellStyle.Format = "C2";

            gridDetalles.Columns[(int)Columnas.Cantidd].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridDetalles.Columns[(int)Columnas.PrecioSap].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDetalles.Columns[(int)Columnas.PrecioReal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDetalles.Columns[(int)Columnas.PRecioPromocion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        public DetalleFacts(int _factura)
        {
            Factura = _factura;

            InitializeComponent();
        }

        private void DetalleFacts_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                lblTitle.Text = "Factura: " + Factura;
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_NCPedientes", conn))
                    {
                        conn.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 6);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);
                        command.Parameters.AddWithValue("@Docentry", 0);
                        command.Parameters.AddWithValue("@PrecioCliente", decimal.Zero);
                        command.Parameters.AddWithValue("@FechaEnvio", DateTime.Now);
                        command.Parameters.AddWithValue("@Vendedor", 0);
                        command.Parameters.AddWithValue("@Factura", Factura);

                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@Linenum", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(table);

                        gridDetalles.DataSource = table;

                        this.Formato();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
