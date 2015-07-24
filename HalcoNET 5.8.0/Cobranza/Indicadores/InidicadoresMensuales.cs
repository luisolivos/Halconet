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
using System.Threading;

namespace Cobranza.Indicares
{
    public partial class InidicadoresMnsuales : Form
    {
        private string JefaCobranza;
        private string Sucursal;
        private int Rol;
        public decimal CarteraAM;
        public decimal CarteraAP;
        public decimal CarteraHM;
        public decimal CarteraHP;
        public DateTime Fecha;
        public string Usuario;
        Clases.Logs log;

        DataTable _TblJefas = new DataTable();

        public InidicadoresMnsuales(string _jefa, string _sucursal, int _rol, string _usuario)
        {
            Sucursal = this.GetSucursalName(_sucursal);
            JefaCobranza = _jefa;
            Rol = _rol;
            Usuario = _usuario;
            InitializeComponent();

            Color ForeCoror = btnCompromisos.ForeColor;
            Color BackColor = btnCompromisos.BackColor;
            h = new Thread(new ThreadStart(Hilo));

            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }


        private void CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            _TblJefas = table.Copy();
            cbCobranza.DataSource = table;
            cbCobranza.DisplayMember = "Nombre";
            cbCobranza.ValueMember = "Codigo";
        }

        private string GetSucursal(string _memo)
        {
            switch (_memo)
            {
                case "01": return "PUEBLA";
                case "02": return "MONTERREY";
                case "03": return "APIZACO";
                case "05": return "CORDOBA";
                case "06": return "TEPEACA";
                case "16": return "EDOMEX";
                case "18": return "GUADALAJARA";
                case "23": return "SALTILLO";
                default: return "";

            }
        }

        private string GetSucursalName(string _suc)
        {
            switch (_suc)
            {
                case "PUEBLA": return "PUEBLA";
                case "CORDOBA": return "CORDOBA";
                case "EDOMEX": return "EDOMEX";
                case "GDL": return "GUADALAJARA";
                case "MTY": return "MONTERREY";
                case "MTYT": return "MONTERREY";
                case "MTYM": return "MONTERREY";
                case "TEPEACA": return "TEPEACA";
                case "APIZACO": return "APIZACO";
                case "SALTILLO": return "SALTILLO";
                default: return "";

            }
        }

        Thread h;
        private void GetIndicadores(string _jefa, DateTime _fi, DateTime _ff, string _sucursal)
        {
            try
            {
                this.Esperar();
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_IndicadoresCobranza", conn))
                    {
                        conn.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Sucursales", _sucursal);
                        command.Parameters.AddWithValue("@JefaCobranza", _jefa);
                        command.Parameters.AddWithValue("@FechaInicial", _fi);
                        command.Parameters.AddWithValue("@FechaFinal", _ff);
                        command.Parameters.AddWithValue("@Sucursal", string.Empty);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);

                        decimal objetivo = 0;
                        decimal cobranza = 0;

                        var qryObjetivo = (from iObj in table.AsEnumerable()
                                           where iObj.Field<string>("Indicador") == "[1]"
                                           select iObj).ToList();
                        if (qryObjetivo.Count() > 0)
                        {
                            objetivo = Convert.ToDecimal(qryObjetivo[0].ItemArray[1]);
                            txtObjetivo.Text = Convert.ToDecimal(qryObjetivo[0].ItemArray[1]).ToString(qryObjetivo[0].ItemArray[2].ToString());
                        }

                        var qryCobranza = (from item in table.AsEnumerable()
                                           where item.Field<string>("Indicador") == "[2]"
                                           select item).ToList();
                        if (qryCobranza.Count() > 0)
                        {
                            cobranza = Convert.ToDecimal(qryCobranza[0].ItemArray[1]);
                            txtCobranza.Text = Convert.ToDecimal(qryCobranza[0].ItemArray[1]).ToString(qryCobranza[0].ItemArray[2].ToString());
                        }

                        txtCobranzavsObjetivoM.Text = (cobranza - objetivo).ToString("C2");
                        txtCobranzavsObjetivoM.ForeColor = this.Colores(txtCobranzavsObjetivoM, cobranza - objetivo, true, img1Up, img1Down);

                        txtCobranzavsObjetivoP.ForeColor = this.Colores(txtCobranzavsObjetivoP, cobranza - objetivo, true, img2Up, img2Down);
                        if (objetivo != 0)
                        {
                            txtCobranzavsObjetivoP.Text = (cobranza / objetivo).ToString("P2");
                        }
                        else
                            txtCobranzavsObjetivoP.Text = "0%";

                        var qryPronosticoM = (from item in table.AsEnumerable()
                                              where item.Field<string>("Indicador") == "[3]"
                                              select item).ToList();
                        if (qryPronosticoM.Count() > 0)
                        {
                            txtPronosticoM.Text = Convert.ToDecimal(qryPronosticoM[0].ItemArray[1]).ToString(qryPronosticoM[0].ItemArray[2].ToString());
                        }

                        var qryPronosticoP = (from item in table.AsEnumerable()
                                              where item.Field<string>("Indicador") == "[4]"
                                              select item).ToList();
                        if (qryPronosticoP.Count() > 0)
                        {
                            txtPronosticoP.Text = Convert.ToDecimal(qryPronosticoP[0].ItemArray[1]).ToString(qryPronosticoP[0].ItemArray[2].ToString());
                        }
                        
                        //cartera vencida mes anterior
                        var qryCarteraVM = (from item in table.AsEnumerable()
                                           where item.Field<string>("Indicador") == "[11]"
                                           select item).ToList();
                        if (qryCarteraVM.Count() > 0)
                        {
                            CarteraAM = Convert.ToDecimal(qryCarteraVM[0].ItemArray[1]);
                            txtCarteraAnteriorM.Text = Convert.ToDecimal(qryCarteraVM[0].ItemArray[1]).ToString(qryCarteraVM[0].ItemArray[2].ToString());
                        }

                        var qryCarteraVP = (from item in table.AsEnumerable()
                                           where item.Field<string>("Indicador") == "[12]"
                                           select item).ToList();
                        if (qryCarteraVP.Count() > 0)
                        {
                            CarteraAP = Convert.ToDecimal(qryCarteraVP[0].ItemArray[1]);
                            txtCarteraAnteriorP.Text = Convert.ToDecimal(qryCarteraVP[0].ItemArray[1]).ToString(qryCarteraVP[0].ItemArray[2].ToString());
                        }
                        //cartera vencida actual
                        var qryCarteraM = (from item in table.AsEnumerable()
                                           where item.Field<string>("Indicador") == "[5]"
                                           select item).ToList();
                        if (qryCarteraM.Count() > 0)
                        {
                            CarteraHM = Convert.ToDecimal(qryCarteraM[0].ItemArray[1]);
                            txtCarteraM.Text = Convert.ToDecimal(qryCarteraM[0].ItemArray[1]).ToString(qryCarteraM[0].ItemArray[2].ToString());
                        }

                        var qryCarteraP = (from item in table.AsEnumerable()
                                           where item.Field<string>("Indicador") == "[6]"
                                           select item).ToList();
                        if (qryCarteraP.Count() > 0)
                        {
                            CarteraHP = Convert.ToDecimal(qryCarteraP[0].ItemArray[1]);
                            txtCarteraP.Text = Convert.ToDecimal(qryCarteraP[0].ItemArray[1]).ToString(qryCarteraP[0].ItemArray[2].ToString());
                        }

                        txtDif2.Text = (CarteraHP - CarteraAP).ToString("P2");
                        

                        txtDif1.Text = (CarteraHM - CarteraAM).ToString("C2");
                        //txtDif2.Text = 
                        //-----------------------------------------------------------
                        var qryDiasCartera = (from item in table.AsEnumerable()
                                              where item.Field<string>("Indicador") == "[7]"
                                              select item).ToList();
                        decimal DiasCartera = 0;
                        if (qryDiasCartera.Count() > 0)
                        {
                            DiasCartera = Convert.ToDecimal(qryDiasCartera[0].ItemArray[1]);
                            txtDiasCartera.Text = DiasCartera.ToString(qryDiasCartera[0].ItemArray[2].ToString());
                        }

                        var qryNCPendientes = (from item in table.AsEnumerable()
                                               where item.Field<string>("Indicador") == "[8]"
                                               select item).ToList();
                        decimal factsPen = 0;
                        if (qryNCPendientes.Count() > 0)
                        {
                            factsPen = Convert.ToDecimal(qryNCPendientes[0].ItemArray[1]);
                            txtNotasSaldo.Text = Convert.ToDecimal(qryNCPendientes[0].ItemArray[1]).ToString(qryNCPendientes[0].ItemArray[2].ToString());
                        }

                        var qryFacturasMes = (from item in table.AsEnumerable()
                                              where item.Field<string>("Indicador") == "[9]"
                                              select item).ToList();
                        if (qryFacturasMes.Count() > 0)
                        {
                            decimal facts = Convert.ToDecimal(qryFacturasMes[0].ItemArray[1]);
                            if (facts > 0)
                            {
                                tct.Text = (factsPen / Convert.ToDecimal(qryFacturasMes[0].ItemArray[1])).ToString(qryFacturasMes[0].ItemArray[2].ToString());
                            }
                        }

                        ////--------
                        var qryCV = (from item in table.AsEnumerable()
                                              where item.Field<string>("Indicador") == "[13]"
                                              select item).ToList();
                        decimal OCV =0;
                        if (qryCV.Count() > 0)
                        {
                            OCV = Convert.ToDecimal(qryCV[0].ItemArray[1]);
                            txtObjCartera.Text = OCV.ToString(qryCV[0].ItemArray[2].ToString());
                        }

                        txtDiferenciaCV.Text = (CarteraHP - OCV ).ToString("P2");

                        var qryDC = (from item in table.AsEnumerable()
                                     where item.Field<string>("Indicador") == "[14]"
                                     select item).ToList();
                        decimal ODC = 0;
                        if (qryDC.Count() > 0)
                        {
                            ODC = Convert.ToDecimal(qryDC[0].ItemArray[1]);
                            txtObjDC.Text = ODC.ToString(qryDC[0].ItemArray[2].ToString());

                            
                        }


                        txtDifDC.Text = (DiasCartera - ODC).ToString("N0");
                        txtDifDC.ForeColor = this.Colores(txtDifDC, DiasCartera - ODC, false, img5Up, img5Down);
                        txtDiferenciaCV.Text = (CarteraHP - OCV).ToString("P2");
                        txtDiferenciaCV.ForeColor = this.Colores(txtDiferenciaCV, CarteraHP - OCV, false, img4Up, img4Down);
                                           ////////-------
                        var qryCompromisos = (from item in table.AsEnumerable()
                                              where item.Field<string>("Indicador") == "[10]"
                                              select item).ToList();

                        var qryEC = (from item in table.AsEnumerable()
                                     where item.Field<string>("Indicador") == "[15]"
                                     select item).ToList();
                        decimal OEC = 0;
                        if (qryEC.Count() > 0)
                        {
                            OEC = Convert.ToDecimal(qryEC[0].ItemArray[1]);
                            txtEdoCta.Text = OEC.ToString(qryEC[0].ItemArray[2].ToString());


                        }

                        if (qryCompromisos.Count() > 0)
                        {
                            decimal facts = Convert.ToDecimal(qryCompromisos[0].ItemArray[1]);

                            toolTip1.SetToolTip(btnCompromisos, facts.ToString("N0") + " Compromisos vencen hoy!");
                            if (facts > 0)
                            {
                                btnCompromisos.Enabled = true;
                                if (h.ThreadState == ThreadState.Unstarted)
                                    h.Start();
                                else if (h.ThreadState == ThreadState.Suspended)
                                    h.Resume();
                            }
                            else
                            {
                                btnCompromisos.Enabled = false;
                                if (h.IsAlive)
                                {
                                    h.Suspend();
                                }
                                btnCompromisos.BackColor = Color.FromName("Info");
                                btnCompromisos.ForeColor = Color.Black;
                            }
                        }


                    }
                }
                btnDetalle.Enabled = true;
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Continuar();
            }
        }

        int Contador = 0;
        public void Hilo()
        {

            while (Contador <= 2000)
            {
                if (btnCompromisos.BackColor == Color.FromName("Info"))
                {
                    btnCompromisos.BackColor = Color.Red;
                    btnCompromisos.ForeColor = Color.White;
                }
                else if (btnCompromisos.BackColor == Color.Red)
                {
                    btnCompromisos.BackColor = Color.FromName("Info");
                    btnCompromisos.ForeColor = Color.Black;
                }
                Contador++;
                Thread.Sleep(500);
            }
        }

        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
        private void Esperar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }

        /// <summary>
        /// Método que carga los meses en el cblMes
        /// </summary>
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
            cmbMes.DataSource = Meses;
            cmbMes.DisplayMember = "Mes";
            cmbMes.ValueMember = "Index";

        }

        public void ClearControls()
        {
            //txtObjetivo.Clear();
            //txtCobranzavsObjetivoM.Clear();
            //txtCobranzavsObjetivoP.Clear();
            //txtPronosticoP.Clear();
            //txtPronosticoM.Clear();
            //txtCobranza.Clear();

            foreach (Control item in this.Controls)
            {
                if (item is GroupBox)
                {
                    foreach (Control  item1 in item.Controls)
                    {
                        if (item1 is TextBox)
                        {
                            TextBox txt = item1 as TextBox;
                            txt.Clear();
                        }
                    }
                }
            }


        }

        public System.Drawing.Color Colores(TextBox txt, decimal valor, bool mayor, PictureBox picUp, PictureBox picDown)
        {
            if (mayor)
            {
                if (valor < 0)
                {
                    picDown.Visible = true;
                    picUp.Visible = false;
                    return Color.Red;
                }
                else
                {
                    picDown.Visible = false;
                    picUp.Visible = true;
                    return Color.Black;
                }
            }
            else
            {
                if (valor < 0)
                {
                    picDown.Visible = false;
                    picUp.Visible = true;
                    return Color.Black;
                }
                else
                {
                    picDown.Visible = true;
                    picUp.Visible = false;
                    return Color.Red;
                }
            }
        }

        private void InidicadoresMnsuales_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                DateTime fechatemp = DateTime.Today;
                DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);
                DateTime fecha2 = new DateTime(fechatemp.Year, fechatemp.AddMonths(1).Month, 1).AddDays(-1);

                Fecha = fecha1;

                btnDetalle.Enabled = false;
                btnAlertasPR.Enabled = false;
                btnCompromisos.Enabled = false;

                dtInicial.Value = fecha1;
                dtFinal.Value = fecha2;

                this.CargarJefesCobranza();
                this.CargarMeses();

                txtAño.Text = DateTime.Now.Year.ToString();
                cmbMes.SelectedIndex = DateTime.Now.Month - 1;

                if (!string.IsNullOrEmpty(JefaCobranza))
                    this.GetIndicadores(JefaCobranza, fecha1, DateTime.Now, Sucursal);

                if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
                {
                    lblVendedor.Visible = false;
                    cbCobranza.Visible = false;
                }

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ClearControls();
            btnDetalle.Enabled = false;
            btnAlertasPR.Enabled = false;
            btnCompromisos.Enabled = false;

            DateTime _fi = dtInicial.Value;
            DateTime _ff = dtFinal.Value;
            //btnCarteraVencida.Enabled = false;
            
            string querySucursal = "";
            _fi = new DateTime(Convert.ToInt32(txtAño.Text), cmbMes.SelectedIndex+1, 1);
            Fecha = _fi;
            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                JefaCobranza = JefaCobranza;
                Sucursal = Sucursal;
            }
            else
            {
                JefaCobranza = cbCobranza.Text;

                querySucursal = (from item in _TblJefas.AsEnumerable()
                            where item.Field<string>("Nombre") == JefaCobranza
                            select item.Field<string>("Codigo")).FirstOrDefault();

                Sucursal = this.GetSucursal(querySucursal);
            }
            //cbCobranza.SelectedValue.ToString();
            if(dtFinal.Value>DateTime.Now)
            {
                _ff = DateTime.Now;
            }

            this.GetIndicadores(JefaCobranza, _fi, _ff, Sucursal);

            btnDetalle.Enabled = true;
            btnAlertasPR.Enabled = true;
            btnCompromisos.Enabled = true;
            
        }

        private void btnCompromisos_Click(object sender, EventArgs e)
        {
            Cobranza.GestionCobranza.AlertasCompromisos alerta = new GestionCobranza.AlertasCompromisos(JefaCobranza, "Compromisos que vencen hoy", Usuario);
            alerta.ShowDialog();
        }

        private void InidicadoresMnsuales_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }

        }

        private void InidicadoresMnsuales_SizeChanged(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    this.WindowState = FormWindowState.Normal;
            //}
        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Indicadores.DetalleNCPendientes form = new Indicadores.DetalleNCPendientes(Sucursal, JefaCobranza, Fecha, Usuario);
            form.MdiParent = this.MdiParent;
            form.Show();
        }

        private void InidicadoresMnsuales_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Indicadores.AlertasPR form = new Indicadores.AlertasPR(Sucursal, JefaCobranza);
            form.MdiParent = this.MdiParent;
            form.Show();
        }


    }
}
