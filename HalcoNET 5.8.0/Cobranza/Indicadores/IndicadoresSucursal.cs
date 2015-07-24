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

namespace Cobranza.Indicadores
{
    public partial class IndicadoresSucursal : Form
    {
        string Sucursal;

        public IndicadoresSucursal()
        {
            InitializeComponent();
        }

        private void CargarMeses()
        {
            DataTable Meses = new DataTable();
            Meses.Columns.Add("Index", typeof(int));
            Meses.Columns.Add("Mes", typeof(string));
            string[] array = new string[12] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            for (int i = 0; i < 12; i++)
            {
                DataRow row = Meses.NewRow();
                row["Index"] = i;
                row["Mes"] = array[i];

                Meses.Rows.Add(row);
            }
            cbMes.DataSource = Meses;
            cbMes.DisplayMember = "Mes";
            cbMes.ValueMember = "Index";

        }

        private void CargarSucursales()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    cbSucursal.DataSource = tbl;
                    cbSucursal.ValueMember = "Codigo";
                    cbSucursal.DisplayMember = "Nombre";
                }
            }
        }

        public void Colores(TextBox txt, decimal valor, bool mayor, PictureBox picUp, PictureBox picDown)
        {
            txt.ReadOnly = false;
            if (mayor)
            {
                if (valor < 1)
                {
                    picDown.Visible = true;
                    picUp.Visible = false;
                    txt.ForeColor = Color.Red;
                }
                else
                {
                    picDown.Visible = false;
                    picUp.Visible = true;
                    txt.ForeColor = Color.Black;
                }
            }
            else
            {
                if (valor < 0)
                {
                    picDown.Visible = false;
                    picUp.Visible = true;
                    txt.ForeColor = Color.Black;
                }
                else
                {
                    picDown.Visible = true;
                    picUp.Visible = false;
                    txt.ForeColor = Color.Red;
                }
            }

            txt.ReadOnly = true;
        }

        //INDICADORES EN TEXTBOX
        private void GetIndicador(DataTable tbl, string _indicator, TextBox txt)
        {
            var qry = (from iObj in tbl.AsEnumerable()
                       where iObj.Field<string>("Indicador") == _indicator
                       select iObj).ToList();
            if (qry.Count() > 0)
            {
                txt.AccessibleDescription = "0";
                txt.AccessibleDescription = qry[0].ItemArray[1].ToString();

                txt.Text = Convert.ToDecimal(qry[0].ItemArray[1]).ToString(qry[0].ItemArray[2].ToString());
            }

        }

        //INDICADORES FACTS A MAS DE 90 DIAS
        private void GetIndicador(DataTable tbl, string _indicator, Button button, string format)
        {
            var qry = (from iObj in tbl.AsEnumerable()
                       where iObj.Field<string>("Indicador") == _indicator
                       select iObj).ToList();

            if (qry.Count() > 0)
            {
                string mensaje = String.Format(format, Convert.ToDecimal(qry[0].ItemArray[1]).ToString(qry[0].ItemArray[2].ToString()));
                toolTip1.SetToolTip(button, mensaje);
            }

            // toolTip1.SetToolTip(btnCompromisos, facts.ToString("N0") + " Compromisos vencen hoy!");
        }

        //INDICADORES PP
        private void GetIndicador(DataTable tbl, Button button)
        {
            try
            {
                var qry1 = (from iObj in tbl.AsEnumerable()
                            where iObj.Field<string>("Indicador") == "[15]"
                            select iObj).ToList();
                var qry2 = (from iObj2 in tbl.AsEnumerable()
                            where iObj2.Field<string>("Indicador") == "[16]"
                            select iObj2).ToList();
                var qry3 = (from iObj3 in tbl.AsEnumerable()
                            where iObj3.Field<string>("Indicador") == "[17]"
                            select iObj3).ToList();

                if (qry1.Count() > 0)
                {
                    string mensaje = String.Format("Monto Total: {0} \r\nMonto aplicado dentro de plazo: {1}\r\nMonto descontado fuera de plazo: {2}",
                        Convert.ToDecimal(qry1[0].ItemArray[1]).ToString(qry1[0].ItemArray[2].ToString()),
                        Convert.ToDecimal(qry2[0].ItemArray[1]).ToString(qry2[0].ItemArray[2].ToString()),
                        Convert.ToDecimal(qry3[0].ItemArray[1]).ToString(qry3[0].ItemArray[2].ToString()));
                    toolTip1.SetToolTip(button, mensaje);
                }
            }
            catch (Exception)
            { }
        }

        private void IndicadoresSucursal_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                this.CargarMeses();
                this.CargarSucursales();

                txtAño.Text = DateTime.Now.Year.ToString();
                cbMes.SelectedIndex = DateTime.Now.Month - 1;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Sucursal = cbSucursal.SelectedValue.ToString();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Fecha", new DateTime(Int32.Parse(txtAño.Text), cbMes.SelectedIndex + 1, 1).AddMonths(1).AddDays(-1));

                        DataTable tbl = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);


                        this.GetIndicador(tbl, "[1]", txtObjetivo);
                        this.GetIndicador(tbl, "[2]", txtCobranza);
                        this.GetIndicador(tbl, "[3]", txtCobranzavsObjetivoM);
                        this.GetIndicador(tbl, "[4]", txtCobranzavsObjetivoP);
                        this.GetIndicador(tbl, "[5]", txtPronosticoM);
                        this.GetIndicador(tbl, "[6]", txtPronosticoP);

                        this.Colores(txtCobranzavsObjetivoP, Convert.ToDecimal(txtCobranzavsObjetivoP.AccessibleDescription), true, img1Up, img1Down);
                        this.Colores(txtCobranzavsObjetivoP, Convert.ToDecimal(txtCobranzavsObjetivoP.AccessibleDescription), true, img2Up, img2Down);
                        //////////////////////////////////////////////
                        this.GetIndicador(tbl, "[7]", txtCarteraM);
                        this.GetIndicador(tbl, "[8]", txtCarteraP);
                        this.GetIndicador(tbl, "[9]", txtObjCartera);
                        this.GetIndicador(tbl, "[10]", txtDiferenciaCV);
                        this.Colores(txtCobranzavsObjetivoP, Convert.ToDecimal(txtDiferenciaCV.AccessibleDescription), false, img4Up, img4Down);
                        //////////////////////////////////////////////
                        this.GetIndicador(tbl, "[11]", txtObjDC);
                        this.GetIndicador(tbl, "[12]", txtDiasCartera);
                        this.GetIndicador(tbl, "[13]", txtDifDC);
                        this.Colores(txtCobranzavsObjetivoP, Convert.ToDecimal(txtDifDC.AccessibleDescription), false, img5Up, img5Down);

                        this.GetIndicador(tbl, "[14]", btnFacts90, "Facturas a mas de 90 días: {0}");
                        this.GetIndicador(tbl, btnPP);
                        // dataGridView1.DataSource = tbl;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPP_Click(object sender, EventArgs e)
        {
            Indicadores.IndicadorPP frm = new IndicadorPP(cbSucursal.SelectedValue.ToString(), (cbMes.SelectedIndex).ToString(), txtAño.Text);
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void btnFacts90_Click(object sender, EventArgs e)
        {
            Indicadores.IndicadorDias frm = new IndicadorDias(cbSucursal.SelectedValue.ToString());
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Indicadores.IndicadorReferencias frm = new IndicadorReferencias(cbSucursal.SelectedValue.ToString());
            frm.MdiParent = this.MdiParent;
            frm.Show();
        }
    }
}
