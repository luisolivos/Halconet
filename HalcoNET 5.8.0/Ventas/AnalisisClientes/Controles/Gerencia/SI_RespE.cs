using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.AnalisisClientes.Controles.Gerencia
{
    public partial class SI_RespE : UserControl
    {
        string nombreVendedor;
        int Vendedor;
        string Respuesta;

        public enum Columnas
        {
            Cliente,
            Nombre,
            LimiteActual,
            LimeteSugerido
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Cliente].Width = 80;
            dgv.Columns[(int)Columnas.Nombre].Width = 250;
            dgv.Columns[(int)Columnas.LimiteActual].Width = 100;
            dgv.Columns[(int)Columnas.LimeteSugerido].Width = 100;

            dgv.Columns[(int)Columnas.LimiteActual].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.LimeteSugerido].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.LimiteActual].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.LimeteSugerido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public SI_RespE(int _vendedor, string _nombreVendedor, string _respuesta)
        {
            InitializeComponent();

            nombreVendedor = _nombreVendedor;
            Vendedor = _vendedor;

            lblRespuesta.Text = Respuesta;
            lblVendedor.Text = nombreVendedor;
        }

        public DataTable DataSource()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AnalisisVentas";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 10);
                    command.Parameters.AddWithValue("@Pregunta", Vendedor);
                    command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                    command.Parameters.AddWithValue("@Letra", string.Empty);
                    command.Parameters.AddWithValue("@Especificacion", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                    command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                    command.Parameters.AddWithValue("@Nombre", string.Empty);

                    command.CommandTimeout = 0;

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);

                    return table;
                }
            }
        }

        private void SI_RespE_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable table = this.DataSource();

                dgvCredit.DataSource = table;

                this.Formato(dgvCredit);
            }
            catch (Exception)
            {

            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.Parent.Controls.Remove(this);
        }

        private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                toolStripStatusLabel1_Click(sender, e);
            }
        }
    }
}
