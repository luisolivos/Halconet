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

namespace Presupuesto.EstadoResultados
{
    public partial class CargarDatos : Form
    {
        public CargarDatos()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private List<string> Sucursales;
        string[] array = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

        private void CargarMeses()
        {
            DataTable Meses = new DataTable();
            Meses.Columns.Add("Index", typeof(int));
            Meses.Columns.Add("Mes", typeof(string));
            

            for (int i = 0; i < 12; i++)
            {
                DataRow row = Meses.NewRow();
                row["Index"] = i;
                row["Mes"] = array[i];

                Meses.Rows.Add(row);
            }
            DataTable meses1 = Meses.Copy();
            cmbMes.DataSource = Meses;
            cmbMes.DisplayMember = "Mes";
            cmbMes.ValueMember = "Index";

            cbMes1.DataSource = meses1;
            cbMes1.DisplayMember = "Mes";
            cbMes1.ValueMember = "Index";
        }

        private void CargarDatos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            CargarMeses();
            txtAño.Text = DateTime.Now.Year.ToString();
            txtAño1.Text = DateTime.Now.Year.ToString();

            Sucursales = new List<string>();

            Sucursales.Add("PUEBLA");
            Sucursales.Add("MTY");
            Sucursales.Add("APIZACO");
            Sucursales.Add("CORDOBA");
            Sucursales.Add("TEPEACA");
            Sucursales.Add("EDOMEX");
            Sucursales.Add("GDL");
            Sucursales.Add("SALTILLO");
            Sucursales.Add("TODOS");
        }

        private void Espera()
        {
            System.Threading.Thread.Sleep(500);
        }

        private void Anima()
        {
            while(true)
            pictureBox1.Refresh();
        }


        private void btnPresentar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread a = new System.Threading.Thread(Anima);
            a.Start();
            toolMessage.BackColor = Color.FromName("Control");
            pictureBox1.Visible = true;
            btnPresentar.Enabled = false;
            int inicio = cbMes1.SelectedIndex + 1;
            int fin = cmbMes.SelectedIndex + 1;

            DateTime f_inicio = new DateTime(int.Parse(txtAño1.Text), inicio, 1);
            DateTime f_fin = new DateTime(int.Parse(txtAño.Text), fin, 1);

            if (f_inicio <= f_fin)
            {
                while (f_inicio <= f_fin)
                {

                    foreach (string branch in Sucursales)
                    {
                        toolMessage.Text = "Cargando: " + branch + " " + array[f_inicio.Month - 1] + " " + f_inicio.Year + "...";
                        System.Threading.Thread pausa1 = new System.Threading.Thread(Espera);
                        pausa1.Start();
                        pausa1.Join();


                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                        {
                            using (SqlCommand command = new SqlCommand("PJ_Edo_Resultados", connection))
                            {
                                connection.Open();
                                command.CommandTimeout = 0;
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@TipoConsulta", 2);
                                command.Parameters.AddWithValue("@Sucursal", branch);
                                command.Parameters.AddWithValue("@Mes", f_inicio.Month);
                                command.Parameters.AddWithValue("@Year", f_inicio.Year);

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    System.Threading.Thread pausa = new System.Threading.Thread(Espera);
                    pausa.Start();
                    pausa.Join();
                    f_inicio = f_inicio.AddMonths(1);
                }
                toolMessage.Text = "Listo!";
                toolMessage.BackColor = Color.Green;
                
            }
            else
            {
                toolMessage.Text = "Error en el rango de meses!";
                toolMessage.BackColor = Color.Red;
            }
            btnPresentar.Enabled = true;
            pictureBox1.Visible = false;
            a.Abort();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            (sender as PictureBox).Update();
        }
    }
}
