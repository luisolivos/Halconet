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
    public partial class SI_RespA : UserControl
    {
        string nombreVendedor;
        int Vendedor;

        public enum Columnas
        {
            Cliente, Nombre, Consumo, Linea, Articulo, PrecioPJ, Competencia,
            PrecioCompetecia, Diferencia, UtilidadPJ, UtilidadComp
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Cliente].Width = 80;
            dgv.Columns[(int)Columnas.Nombre].Width = 150;
            dgv.Columns[(int)Columnas.Consumo].Visible = false;
            dgv.Columns[(int)Columnas.Linea].Width = 80;
            dgv.Columns[(int)Columnas.Articulo].Width = 90;
            dgv.Columns[(int)Columnas.PrecioPJ].Width = 80;
            dgv.Columns[(int)Columnas.Competencia].Width = 90;
            dgv.Columns[(int)Columnas.PrecioCompetecia].Width = 80;
            dgv.Columns[(int)Columnas.Diferencia].Width = 80;
            dgv.Columns[(int)Columnas.UtilidadPJ].Width = 80;
            dgv.Columns[(int)Columnas.UtilidadComp].Width = 80;


            dgv.Columns[(int)Columnas.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)Columnas.Nombre].HeaderText = "Nombre";
            dgv.Columns[(int)Columnas.Linea].HeaderText = "Línea";
            dgv.Columns[(int)Columnas.Articulo].HeaderText = "Artículo";
            dgv.Columns[(int)Columnas.PrecioPJ].HeaderText = "Precio PJ";
            dgv.Columns[(int)Columnas.Competencia].HeaderText = "Nombre\r\nCompetencia";
            dgv.Columns[(int)Columnas.PrecioCompetecia].HeaderText = "Precio\r\nCompetencia";
            dgv.Columns[(int)Columnas.Diferencia].HeaderText = "Diferencia\r\n(%)";
            dgv.Columns[(int)Columnas.UtilidadPJ].HeaderText = "Utilidad\r\nPJ";
            dgv.Columns[(int)Columnas.UtilidadComp].HeaderText = "Utilidad\r\nCompetencia";


            dgv.Columns[(int)Columnas.PrecioPJ].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PrecioCompetecia].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Diferencia].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)Columnas.UtilidadPJ].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)Columnas.UtilidadComp].DefaultCellStyle.Format = "P2";

            dgv.Columns[(int)Columnas.PrecioPJ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PrecioCompetecia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.UtilidadPJ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.UtilidadComp].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public SI_RespA(int _vendedor, string _nombre)
        {
            InitializeComponent();

            Vendedor = _vendedor;
            nombreVendedor = _nombre;

            lblVendedor.Text = _nombre;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.Parent.Controls.Remove(this);
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
                    command.Parameters.AddWithValue("@TipoConsulta", 11);
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

        private void SI_RespA_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable table = this.DataSource();

                dgvItems.DataSource = table;

                this.Formato(dgvItems);

            }
            catch (Exception)
            {
                
            }
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
