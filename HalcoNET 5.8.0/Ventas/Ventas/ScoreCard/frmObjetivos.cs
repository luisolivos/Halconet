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
    public partial class frmObjetivos : Form
    {
        private string _stringQry = "Select Code, Vendedor, Objetivo from Tbl_OMLV Where Active = 'Y' AND ItmsGrpCod =  @Lineas";
        private SqlDataAdapter da = null;
        private SqlCommandBuilder buider = new SqlCommandBuilder();
        private BindingSource source = new BindingSource();
        private DataSet set = new DataSet();

        public frmObjetivos()
        {
            InitializeComponent();
        }

        private void CargarLineas()
        {
            DataTable tblLineas = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_RtpLineas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 3);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    command.CommandTimeout = 0;

                    da.Fill(tblLineas);

                }

            }

            cbLinea.DataSource = tblLineas;
            cbLinea.DisplayMember = "Nombre";
            cbLinea.ValueMember = "Codigo";
        }

        private void frmObjetivos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                this.CargarLineas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                da = null;
                buider = null;
                source = null;
                set = null;

                set = new DataSet();
                da = new SqlDataAdapter();
                buider = new SqlCommandBuilder();
                source = new BindingSource();

                dgvDatos.DataSource = null;

                SqlCommand command = new SqlCommand(_stringQry, new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.Parameters.AddWithValue("@Lineas", cbLinea.SelectedValue);

                da.SelectCommand = command;

                buider.DataAdapter = da;

                da.Fill(set);

                source.DataSource = set.Tables[0];

                dgvDatos.DataSource = source;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                da.Update(set);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
