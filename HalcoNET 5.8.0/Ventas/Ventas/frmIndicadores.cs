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

namespace Ventas.Ventas
{
    public partial class frmIndicadores : Form
    {
        public int RolUsuario;
        public int CodigoVendedor;
        public string Sucursal;
        public DateTime Fecha;
        public string Usuario;

        public decimal ObjetivoVenta = 0;
        public decimal VentaActual = 0;
        public decimal AcumuladovsCuota = 0;
        public decimal UtilidadObjetivoM = 0;
        public decimal UtilidadObjetivoT = 0;
        public decimal UtilidadObjetivoA = 0;
        public decimal UtilidadRealM = 0;
        public decimal UtilidadRealT = 0;
        public decimal UtilidadRealA = 0;

        public decimal ObjetivoHalcon = 0;
        public decimal VentaHalcon = 0;
        public decimal ObjetivovsCuota = 0;

        public decimal ObjetivoObjetivo = 0;
        public decimal VentaObjetivo = 0;
        public decimal ObjetivovsCuotaObjetivo = 0;

        public decimal Pronostico1 = 0;
        public decimal Pronostico2 = 0;
        public decimal Pronostico3 = 0;

        public decimal AcumvsCouta1 = 0;
        public decimal AcumvsCouta2 = 0;
        public decimal AcumvsCouta3 = 0;

        public decimal Pronostico1M = 0;
        public decimal Pronostico2M = 0;
        public decimal Pronostico3M = 0;

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        public frmIndicadores(int _rol, int _vendedor, string _sucursal, DateTime _fecha, string _usuario)
        {
            RolUsuario = _rol;
            CodigoVendedor = _vendedor;
            Sucursal = _sucursal;
            Fecha = _fecha;
            InitializeComponent();
            Usuario = _usuario;

            Color ForeCoror = btnPPC.ForeColor;
            Color BackColor = btnPPC.BackColor;
            h = new Thread(new ThreadStart(Hilo));
        }

        private void Indicadores_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            CheckForIllegalCrossThreadCalls = false;

           // dateTimePicker1.Value = (DateTime.Now.AddDays(-1));

            if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                CargarVendedores();
            }
            else
            {
                cbVendedores.Visible = false;
                lblVendedor.Visible = false;
                //button1.Visible = false;
                //button2.Visible = false;
                //dateTimePicker1.Visible = false;
                try
                {
                    ObjetivoVenta = 0;
                    VentaActual = 0;
                    AcumuladovsCuota = 0;
                    UtilidadObjetivoM = 0;
                    UtilidadObjetivoT = 0;
                    UtilidadRealM = 0;
                    UtilidadRealT = 0;

                    ObjetivoHalcon = 0;
                    VentaHalcon = 0;
                    ObjetivovsCuota = 0;

                    ObjetivoObjetivo = 0;
                    VentaObjetivo = 0;
                    ObjetivovsCuotaObjetivo = 0;

                    Pronostico1 = 0;
                    Pronostico2 = 0;
                    Pronostico3 = 0;
          
                    SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                    commandVendedor.CommandType = CommandType.StoredProcedure;
                    commandVendedor.Parameters.AddWithValue("@TipoConsulta", 2);
                    commandVendedor.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                    commandVendedor.Parameters.AddWithValue("@Fecha", Fecha);
                    commandVendedor.Parameters.AddWithValue("@Bono", 0);
                    commandVendedor.Parameters.AddWithValue("@From", string.Empty);
                    commandVendedor.Parameters.AddWithValue("@Mensaje", string.Empty);

                    DataTable tbl = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = commandVendedor;
                    da.SelectCommand.CommandTimeout = 0;
                    da.Fill(tbl);


                    var queryUM = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[0]"//Utilidad Mayoreo
                                  select item).ToList();
                    if (queryUM.Count != 0)
                        UtilidadRealM = Convert.ToDecimal(queryUM[0].ItemArray[2].ToString());

                    var queryUT = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[1]"//Utilidad Transporte
                                   select item).ToList();
                    if (queryUT.Count != 0)
                        UtilidadRealT = Convert.ToDecimal(queryUT[0].ItemArray[2].ToString());

                    var queryVA = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[2]"//Venta Actual
                                   select item).ToList();
                    if (queryVA.Count != 0)
                        VentaActual = Convert.ToDecimal(queryVA[0].ItemArray[2].ToString());

                    var queryOV = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[3]"//Objetivo Venta
                                   select item).ToList();
                    if (queryOV.Count != 0)
                        ObjetivoVenta = Convert.ToDecimal(queryOV[0].ItemArray[2].ToString());

                    var queryOH = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[4]"//Objetivo halcon
                                   select item).ToList();
                    if (queryOH.Count != 0)
                        ObjetivoHalcon = Convert.ToDecimal(queryOH[0].ItemArray[2].ToString());

                    var queryVH = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[5]"//Venta halcon
                                   select item).ToList();
                    if (queryVH.Count != 0)
                        VentaHalcon = Convert.ToDecimal(queryVH[0].ItemArray[2].ToString());

                    var queryOO = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[6]"
                                   select item).ToList();
                    if (queryOO.Count != 0)
                        ObjetivoObjetivo = Convert.ToDecimal(queryOO[0].ItemArray[2].ToString());

                    var queryVO = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[7]"
                                   select item).ToList();
                    if (queryVO.Count != 0)
                        VentaObjetivo = Convert.ToDecimal(queryVO[0].ItemArray[2].ToString());

                    var queryUOM = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[8]"
                                   select item).ToList();
                    if (queryUOM.Count != 0)
                        UtilidadObjetivoM = Convert.ToDecimal(queryUOM[0].ItemArray[2].ToString());

                    var queryUOT = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[9]"
                                   select item).ToList();
                    if (queryUOT.Count != 0)
                        UtilidadObjetivoT = Convert.ToDecimal(queryUOT[0].ItemArray[2].ToString());

                    var queryP1 = (from item in tbl.AsEnumerable()
                                    where item[1].ToString() == "[10]"
                                    select item).ToList();
                    if (queryP1.Count != 0)
                        Pronostico1 = Convert.ToDecimal(queryP1[0].ItemArray[2].ToString());

                    var queryP2 = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[11]"
                                   select item).ToList();
                    if (queryP2.Count != 0)
                        Pronostico2 = Convert.ToDecimal(queryP2[0].ItemArray[2].ToString());

                    var queryP3 = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[12]"
                                   select item).ToList();
                    if (queryP3.Count != 0)
                        Pronostico3 = Convert.ToDecimal(queryP3[0].ItemArray[2].ToString());

                    var queryP1M = (from item in tbl.AsEnumerable()
                                    where item[1].ToString() == "[13]"
                                    select item).ToList();
                    if (queryP1M.Count != 0)
                        Pronostico1M = Convert.ToDecimal(queryP1M[0].ItemArray[2].ToString());

                    var queryP2M = (from item in tbl.AsEnumerable()
                                   where item[1].ToString() == "[14]"
                                   select item).ToList();
                    if (queryP2M.Count != 0)
                        Pronostico2M = Convert.ToDecimal(queryP2M[0].ItemArray[2].ToString());

                    var queryP3M = (from item in tbl.AsEnumerable()
                                    where item[1].ToString() == "[15]"
                                    select item).ToList();
                    if (queryP3M.Count != 0)
                        Pronostico3M = Convert.ToDecimal(queryP3M[0].ItemArray[2].ToString());

                    if (ObjetivoVenta != 0)
                        AcumuladovsCuota = VentaActual / ObjetivoVenta;
                    if (ObjetivoHalcon != 0)
                        ObjetivovsCuota = VentaHalcon / ObjetivoHalcon;
                    if (ObjetivoObjetivo != 0)
                        ObjetivovsCuotaObjetivo = VentaObjetivo / ObjetivoObjetivo;

                    AcumvsCouta1 = VentaActual - ObjetivoVenta;
                    AcumvsCouta2 = VentaHalcon - ObjetivoHalcon;
                    AcumvsCouta3 = VentaObjetivo - ObjetivoObjetivo;

                    MostrarValores();
                   // btnEspecial.Enabled = true;
                    btnPPC.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ObjetivoVenta = 0;
                    VentaActual = 0;
                    AcumuladovsCuota = 0;
                    UtilidadObjetivoM = 0;
                    UtilidadObjetivoT = 0;
                    UtilidadRealM = 0;
                    UtilidadRealT = 0;

                    ObjetivoHalcon = 0;
                    VentaHalcon = 0;
                    ObjetivovsCuota = 0;

                    ObjetivoObjetivo = 0;
                    VentaObjetivo = 0;
                    ObjetivovsCuotaObjetivo = 0;

                    Pronostico1 = 0;
                    Pronostico2 = 0;
                    Pronostico3 = 0;
                    MostrarValores();
                }
                finally
                {
                    conection.Close();
                }
            }


        }
        /// <summary>
        /// Método que carga los Vendedores en el clbVendedor
        /// </summary>
        private void CargarVendedores()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
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

                cbVendedores.DataSource = table;
                cbVendedores.DisplayMember = "Nombre";
                cbVendedores.ValueMember = "Codigo";
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET .GerenteVentasSucursal)
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

                cbVendedores.DataSource = table;
                cbVendedores.DisplayMember = "Nombre";
                cbVendedores.ValueMember = "Codigo";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //btnEspecial.Enabled = false;
                btnPPC.Enabled = false;
                btnStockReciente.Enabled = false;
                btnArribo.Enabled = false;
                btnAclarar.Enabled = false;

                ObjetivoVenta = 0;
                VentaActual = 0;
                AcumuladovsCuota = 0;
                UtilidadObjetivoM = 0;
                UtilidadObjetivoT = 0;
                UtilidadObjetivoA = 0;
                UtilidadRealM = 0;
                UtilidadRealT = 0;
                UtilidadRealA = 0;

                ObjetivoHalcon = 0;
                VentaHalcon = 0;
                ObjetivovsCuota = 0;

                ObjetivoObjetivo = 0;
                VentaObjetivo = 0;
                ObjetivovsCuotaObjetivo = 0;

                Pronostico1 = 0;
                Pronostico2 = 0;
                Pronostico3 = 0;

                if ((int)ClasesSGUV.Login.Rol != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                    CodigoVendedor = (int)cbVendedores.SelectedValue;
                Fecha = dateTimePicker1.Value;
                SqlCommand commandVendedor = new SqlCommand("PJ_VariasScoreCard", conection);
                commandVendedor.CommandType = CommandType.StoredProcedure;
                commandVendedor.Parameters.AddWithValue("@TipoConsulta", 2);
                commandVendedor.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                commandVendedor.Parameters.AddWithValue("@Fecha", Fecha);
                commandVendedor.Parameters.AddWithValue("@Bono", 0);
                commandVendedor.Parameters.AddWithValue("@From", string.Empty);
                commandVendedor.Parameters.AddWithValue("@Mensaje", string.Empty);

                DataTable tbl = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = commandVendedor;
                da.SelectCommand.CommandTimeout = 0;
                da.Fill(tbl);

                var queryUM = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[0]"//Utilidad Mayoreo
                               select item).ToList();
                if (queryUM.Count != 0)
                    UtilidadRealM = Convert.ToDecimal(queryUM[0].ItemArray[2].ToString());

                var queryUA = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[16]"//Utilidad Mayoreo
                               select item).ToList();
                if (queryUA.Count != 0)
                    UtilidadRealA = Convert.ToDecimal(queryUA[0].ItemArray[2].ToString());

                var queryUT = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[1]"//Utilidad Transporte
                               select item).ToList();
                if (queryUT.Count != 0)
                    UtilidadRealT = Convert.ToDecimal(queryUT[0].ItemArray[2].ToString());

                var queryVA = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[2]"//Venta Actual
                               select item).ToList();
                if (queryVA.Count != 0)
                    VentaActual = Convert.ToDecimal(queryVA[0].ItemArray[2].ToString());

                var queryOV = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[3]"//Objetivo Venta
                               select item).ToList();
                if (queryOV.Count != 0)
                    ObjetivoVenta = Convert.ToDecimal(queryOV[0].ItemArray[2].ToString());

                var queryOH = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[4]"//Objetivo halcon
                               select item).ToList();
                if (queryOH.Count != 0)
                    ObjetivoHalcon = Convert.ToDecimal(queryOH[0].ItemArray[2].ToString());

                var queryVH = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[5]"//Venta halcon
                               select item).ToList();
                if (queryVH.Count != 0)
                    VentaHalcon = Convert.ToDecimal(queryVH[0].ItemArray[2].ToString());

                var queryOO = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[6]"
                               select item).ToList();
                if (queryOO.Count != 0)
                    ObjetivoObjetivo = Convert.ToDecimal(queryOO[0].ItemArray[2].ToString());

                var queryVO = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[7]"
                               select item).ToList();
                if (queryVO.Count != 0)
                    VentaObjetivo = Convert.ToDecimal(queryVO[0].ItemArray[2].ToString());

                var queryUOM = (from item in tbl.AsEnumerable()
                                where item[1].ToString() == "[8]"
                                select item).ToList();
                if (queryUOM.Count != 0)
                    UtilidadObjetivoM = Convert.ToDecimal(queryUOM[0].ItemArray[2].ToString());

                var queryUOA = (from item in tbl.AsEnumerable()
                                where item[1].ToString() == "[17]"
                                select item).ToList();
                if (queryUOA.Count != 0)
                    UtilidadObjetivoA = Convert.ToDecimal(queryUOA[0].ItemArray[2].ToString());

                var queryUOT = (from item in tbl.AsEnumerable()
                                where item[1].ToString() == "[9]"
                                select item).ToList();
                if (queryUOT.Count != 0)
                    UtilidadObjetivoT = Convert.ToDecimal(queryUOT[0].ItemArray[2].ToString());

                var queryP1 = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[10]"
                               select item).ToList();
                if (queryP1.Count != 0)
                    Pronostico1 = Convert.ToDecimal(queryP1[0].ItemArray[2].ToString());

                var queryP2 = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[11]"
                               select item).ToList();
                if (queryP2.Count != 0)
                    Pronostico2 = Convert.ToDecimal(queryP2[0].ItemArray[2].ToString());

                var queryP3 = (from item in tbl.AsEnumerable()
                               where item[1].ToString() == "[12]"
                               select item).ToList();
                if (queryP3.Count != 0)
                    Pronostico3 = Convert.ToDecimal(queryP3[0].ItemArray[2].ToString());

                var queryP1M = (from item in tbl.AsEnumerable()
                                where item[1].ToString() == "[13]"
                                select item).ToList();
                if (queryP1M.Count != 0)
                    Pronostico1M = Convert.ToDecimal(queryP1M[0].ItemArray[2].ToString());

                var queryP2M = (from item in tbl.AsEnumerable()
                                where item[1].ToString() == "[14]"
                                select item).ToList();
                if (queryP2M.Count != 0)
                    Pronostico2M = Convert.ToDecimal(queryP2M[0].ItemArray[2].ToString());

                var queryP3M = (from item in tbl.AsEnumerable()
                                where item[1].ToString() == "[15]"
                                select item).ToList();
                if (queryP3M.Count != 0)
                    Pronostico3M = Convert.ToDecimal(queryP3M[0].ItemArray[2].ToString());

                if (ObjetivoVenta != 0)
                    AcumuladovsCuota = VentaActual / ObjetivoVenta;
                if (ObjetivoHalcon != 0)
                    ObjetivovsCuota = VentaHalcon / ObjetivoHalcon;
                if (ObjetivoObjetivo != 0)
                    ObjetivovsCuotaObjetivo = VentaObjetivo / ObjetivoObjetivo;

                AcumvsCouta1 = VentaActual - ObjetivoVenta;
                AcumvsCouta2 = VentaHalcon - ObjetivoHalcon;
                AcumvsCouta3 = VentaObjetivo - ObjetivoObjetivo;

                MostrarValores();

                //btnEspecial.Enabled = true;
                btnPPC.Enabled = true;
                btnStockReciente.Enabled = true;
                btnArribo.Enabled = true;
                btnAclarar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ObjetivoVenta = 0;
                VentaActual = 0;
                AcumuladovsCuota = 0;
                UtilidadObjetivoM = 0;
                UtilidadObjetivoT = 0;
                UtilidadRealM = 0;
                UtilidadRealT = 0;

                ObjetivoHalcon = 0;
                VentaHalcon = 0;
                ObjetivovsCuota = 0;

                ObjetivoObjetivo = 0;
                VentaObjetivo = 0;
                ObjetivovsCuotaObjetivo = 0;

                Pronostico1 = 0;
                Pronostico2 = 0;
                Pronostico3 = 0;
                MostrarValores();
            }
            finally
            {
                conection.Close();
            }
        }

        #region METODOS
        public void formatoPronostico1()
        {
                    if (Convert.ToDecimal(Pronostico1) * 100 > 100)
                    {
                        txtPronostico1.BackColor = Color.FromArgb(0, 176, 80);//green
                        txtPronostico1.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(Pronostico1) * 100 >= 90
                        && Convert.ToDecimal(Pronostico1) * 100 <= 100)
                    {
                        txtPronostico1.BackColor = Color.FromArgb(255, 255, 0);//yellow
                        txtPronostico1.ForeColor = Color.Black;
                    }
                    else if (Convert.ToDecimal(Pronostico1) * 100 < 90)
                    {
                        txtPronostico1.BackColor = Color.FromArgb(255, 0, 0);//red
                        txtPronostico1.ForeColor = Color.White;
                    }
        
        }

        public void formatoPronostico2()
        {
            if (Convert.ToDecimal(Pronostico2) * 100 >= 100)
            {
                txtPronostico2.BackColor = Color.FromArgb(0, 176, 80);//green
                txtPronostico2.ForeColor = Color.Black;
            }
            else if ( Convert.ToDecimal(Pronostico2) * 100 < 100)
            {
                txtPronostico2.BackColor = Color.FromArgb(255, 0, 0);//yellow
                txtPronostico2.ForeColor = Color.White;
            }

        }

        public void formatoPronostico3()
        {
            if (Convert.ToDecimal(Pronostico3) * 100 >= 100)
            {
                txtPronostico3.BackColor = Color.FromArgb(0, 176, 80);//green
                txtPronostico3.ForeColor = Color.Black;
            }
            else if (Convert.ToDecimal(Pronostico3) * 100 < 100)
            {
                txtPronostico3.BackColor = Color.FromArgb(255, 0, 0);//yellow
                txtPronostico3.ForeColor = Color.White;
            }

        }

        public void acumuladoVSCuota()
        {
            if (Convert.ToDecimal(AcumuladovsCuota) * 100 > 100)
            {
                txtAcumvsCuota.BackColor = Color.FromArgb(0, 176, 80);//green
                txtAcumvsCuota.ForeColor = Color.Black;
            }
            else if (Convert.ToDecimal(AcumuladovsCuota) * 100 >= 90
                && Convert.ToDecimal(AcumuladovsCuota) * 100 <= 100)
            {
                txtAcumvsCuota.BackColor = Color.FromArgb(255, 255, 0);//yellow
                txtAcumvsCuota.ForeColor = Color.Black;
            }
            else if (Convert.ToDecimal(AcumuladovsCuota) * 100 < 90)
            {
                txtAcumvsCuota.BackColor = Color.FromArgb(255, 0, 0);//red
                txtAcumvsCuota.ForeColor = Color.White;
            }

        }

        public void colores()
        {
            formatoPronostico1();
            formatoPronostico2();
            formatoPronostico3();
            acumuladoVSCuota();
        }

        Thread h ;
        private void GetPendientes(int SlpCode)
        {
           
            try
            {
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_SeguimientoCompras", conn))
                    {
                        conn.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@Almacen", string.Empty);
                        command.Parameters.AddWithValue("@Tipo", string.Empty);
                        command.Parameters.AddWithValue("@Vendedor", SlpCode);

                        int pendientes = 0;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            pendientes = reader.GetInt32(0);
                        }

                        toolTip1.ToolTipTitle = pendientes.ToString();

                        
                        if (pendientes > 0)
                        {
                            btnPPC.Enabled = true;
                            if(h.ThreadState == ThreadState.Unstarted)
                                h.Start();
                            else if (h.ThreadState == ThreadState.Suspended)
                                h.Resume();
                        }
                        else
                        {
                            btnPPC.Enabled = false;
                            if (h.IsAlive)
                            {
                                h.Suspend();
                            }
                            btnPPC.BackColor = Color.FromName("Info");
                            btnPPC.ForeColor = Color.Black;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        int Contador = 0;
        public void Hilo()
        {
            
            while(Contador <= 2000)
            {
                if (btnPPC.BackColor == Color.FromName("Info"))
                {
                    btnPPC.BackColor = Color.Red;
                    btnPPC.ForeColor = Color.White;
                }
                else if (btnPPC.BackColor == Color.Red)
                {
                    btnPPC.BackColor = Color.FromName("Info");
                    btnPPC.ForeColor = Color.Black;
                }
                Contador ++;
                Thread.Sleep(500);
            }
        }

        public void MostrarValores()
        {
            this.Text = "Indicadores: " + cbVendedores.Text; 

            txtAcum1.Text = AcumvsCouta1.ToString("C0");
            txtAcum2.Text = AcumvsCouta2.ToString("C0");
            txtAcum3.Text = AcumvsCouta3.ToString("C0");

            txtObjVenta.Text = ObjetivoVenta.ToString("C0");
            txtVentaActual.Text = VentaActual.ToString("C0");
            txtAcumvsCuota.Text = AcumuladovsCuota.ToString("P2");

            txtPronostico1M.Text = Pronostico1M.ToString("C0");
            txtPronostico2M.Text = Pronostico2M.ToString("C0");
            txtPronostico3M.Text = Pronostico3M.ToString("C0");

            txtUtilidadObjetivo.Text = UtilidadObjetivoM.ToString("P1") + " - " + UtilidadObjetivoT.ToString("P1") + " - " + UtilidadObjetivoA.ToString("P1");
            txtUtilidadReal.Text = UtilidadRealM.ToString("P1") + " - " + UtilidadRealT.ToString("P1") + " - " + UtilidadRealA.ToString("P1");

            //if (UtilidadObjetivoM == 0 || UtilidadObjetivoT == 0)
            //{
            //    if (UtilidadObjetivoM > 0)
            //        txtUtilidadObjetivo.Text = UtilidadObjetivoM.ToString("P1");
            //    else
            //        txtUtilidadObjetivo.Text = UtilidadObjetivoT.ToString("P1");
            //}
            //else
            //{
            //    txtUtilidadObjetivo.Text = UtilidadObjetivoM.ToString("P1") + " -- " + UtilidadObjetivoT.ToString("P1");
            //}

            //if (UtilidadRealM == 0 || UtilidadRealT == 0)
            //{
            //    if (UtilidadRealM > 0)
            //        txtUtilidadReal.Text = UtilidadRealM.ToString("P1");
            //    else
            //        txtUtilidadReal.Text = UtilidadRealT.ToString("P1");
            //}
            //else
            //{
            //    txtUtilidadReal.Text = UtilidadRealM.ToString("P1") + " -- " + UtilidadRealT.ToString("P1");
            //}

            //if (UtilidadRealA == 0 || UtilidadRealT == 0)
            //{
            //    if (UtilidadRealM > 0)
            //        txtUtilidadReal.Text = UtilidadRealM.ToString("P1");
            //    else
            //        txtUtilidadReal.Text = UtilidadRealT.ToString("P1");
            //}
            //else
            //{
            //    txtUtilidadReal.Text = UtilidadRealM.ToString("P1") + " -- " + UtilidadRealT.ToString("P1");
            //}

            txtObjHalcon.Text = ObjetivoHalcon.ToString("C0");
            txtVentaHalcon.Text = VentaHalcon.ToString("C0");
            txtObvsCuotaHalcon.Text = ObjetivovsCuota.ToString("P1");

            txtObjetivoObjetivo.Text = ObjetivoObjetivo.ToString("C0");
            txtObjetivovsCuotaObjetivo.Text = ObjetivovsCuotaObjetivo.ToString("P1");
            txtVentaObjetivo.Text = VentaObjetivo.ToString("C0");

            txtPronostico1.Text = Pronostico1.ToString("P1");
            txtPronostico2.Text = Pronostico2.ToString("P1");
            txtPronostico3.Text = Pronostico3.ToString("P1");
            colores();

            this.GetPendientes(CodigoVendedor);
            
        }
        #endregion

        private void cbVendedores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnPPC_Click(object sender, EventArgs e)
        {
            Compras.SeguimientoComprasVendedores seg = new Compras.SeguimientoComprasVendedores(CodigoVendedor, "PPC y Compras especiales pendientes ");
            seg.MdiParent = this.MdiParent;
            seg.Show();
        }

        private void btnEspecial_Click(object sender, EventArgs e)
        {
            Compras.SeguimientoComprasVendedores seg = new Compras.SeguimientoComprasVendedores(CodigoVendedor, "Compra especial");
            seg.MdiParent = this.MdiParent;
            seg.Show();
        }

        private void Indicadores_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                h.Abort();
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Compras.frmNotificacionStock seg = new Compras.frmNotificacionStock(Usuario, CodigoVendedor);
            seg.MdiParent = this.MdiParent;
            seg.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Compras.frmArribos seg = new Compras.frmArribos(CodigoVendedor);
            seg.MdiParent = this.MdiParent;
            seg.Show();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            Cobranza.Indicadores.NCPorAclarar seg = new Cobranza.Indicadores.NCPorAclarar(CodigoVendedor, RolUsuario);
            seg.MdiParent = this.MdiParent;
            seg.Show();
        }

       
    }
}
