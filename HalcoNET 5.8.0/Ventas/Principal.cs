using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using ComponentFactory.Krypton;
using System.Drawing.Drawing2D;

namespace Ventas
{
    public partial class Principal : Form
    {
        //DataTable TblAlertasCobranza = new DataTable();

        #region PARÁMETROS

        private int ClaveEntidad;
        private string NombreUsuario;
        private string Contrasenha;
        private int Rol;
        private int Vendedor;
        private string Sucursal;
        private DataTable _Permisos = new DataTable();

        #endregion

        #region METODOS

        #endregion

        #region EVENTOS

        /// <summary
        /// Función que inicializa el Formulario
        /// </summary>
        /// <param name="claveEntidad">Clave Entidad del usuario que está logueado</param>
        /// <param name="nombreUsuario">Nombre del Usuario que está logueado en el sistema</param>
        /// <param name="contrasenha">Contraseña del usuario logueado</param>
        /// <param name="rol">Rol asignado para el usuario logueado</param>
        public Principal(int claveEntidad, string nombreUsuario, string contrasenha, int rol, int vendedor, string sucursal)
        {
            InitializeComponent();

            ClaveEntidad = claveEntidad;
            NombreUsuario = nombreUsuario;
            Contrasenha = contrasenha;
            Rol = rol;
            Vendedor = vendedor;
            Sucursal = sucursal;
        }

        public void EstablecerPermisos()
        {
            _Permisos.Clear();
            using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "select * from PJ_Permisos where Rol = @Rol";
                command.CommandType = CommandType.Text;
                command.Connection = con;
                command.Parameters.AddWithValue("@Rol", Rol);

                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                _Permisos.Load(dr);

                //stripVentas.Visible = (from item in _TblPermisos.AsEnumerable()
                //                       where item.Field<string>("Modulo") == "stripVentas"
                //                       select item).Count() > 0;
            }

        }

        /// <summary>
        /// Evento que ocurre al cargarse la página
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void Principal_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

              //  MessageBox.Show(Application.StartupPath);
                this.Text = "HalcoNET " + ClasesSGUV.Propiedades.Version;
                toolVersion.Text = "HalcoNET " + ClasesSGUV.Propiedades.Version;


                MdiClient ctlMDI;
                foreach (Control ctl in this.Controls)
                {
                    try
                    {
                        ctlMDI = (MdiClient)ctl;

                        ctlMDI.BackColor = Color.White;
                    }
                    catch (InvalidCastException)
                    {
                    }
                }

                this.WindowState = FormWindowState.Maximized;
                this.CargarPermisos();
                this.RecurseToolStripItems(menuStripPrincipal.Items);

                stripUptate.Visible = true;
                CheckForIllegalCrossThreadCalls = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                toolTipImage.SetToolTip(statusStripPrincipal, ClasesSGUV.Login.NombreUsuario);
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\HalcoNET\\img";
                if (System.IO.Directory.Exists(mdoc))
                {
                    string fromFile = string.Empty;
                    string[] images = System.IO.Directory.GetFiles(mdoc, "*.png");
                    foreach (var item in images)
                    {
                        fromFile = item;
                    }
                    tooStatusP.Image = Image.FromFile(fromFile);
                }
            }
            catch (Exception)
            {

            }
        }

        public void AlertasCobranza()
        {

        }

        public void CargarPermisos()
        {
            string queryString = "Select * from PJ_PermisosP where Rol = @Rol ";
            SqlCommand command = new SqlCommand(queryString);
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue("@Rol", Rol);
            //SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            try
            {
                using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    command.Connection = con;
                    con.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        _Permisos.Load(dr);
                    }
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                //con.Close();
            }
        }

        public void newForm(Form frm)
        {
            bool var = false;
            foreach (Form item in this.MdiChildren)
            {
                if (item.GetType() == frm.GetType())
                {
                    item.WindowState = FormWindowState.Normal;
                    var = true;
                    foreach (Form i in this.MdiChildren)
                    {
                        if (!(item.GetType() == frm.GetType()))
                        {
                            i.WindowState = FormWindowState.Minimized;
                        }
                    }
                }
            }
            if (!var)
            {
                frm.MdiParent = this;
                frm.Show();
                this.WindowState = FormWindowState.Maximized;
            }
        }

        public void RecurseToolStripItems(ToolStripItemCollection tsic)
        {
            foreach (ToolStripItem item in tsic)
            {
                if (item.Name.ToString() != string.Empty)
                {
                    item.Visible = false;
                    var query = from perm in _Permisos.AsEnumerable()
                                where perm.Field<string>("Modulo") == item.Name
                                select perm;
                    bool flag = query.Count() > 0;
                    item.Visible = flag;
                }
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem item2 = (ToolStripMenuItem)item;
                    RecurseToolStripItems(item2.DropDown.Items);
                }
            }
        }
        /// <summary>
        /// Muestra FACTURAS
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facturas formulario = new Facturas(ClasesSGUV.Login.Rol, ClasesSGUV.Login.Vendedor1, ClasesSGUV.Login.NombreUsuario, ClasesSGUV.Login.Sucursal);
            this.newForm(formulario);
        }

        /// <summary>
        /// Muestra COBRANZA
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void cobranzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //bool var = false;
            //    foreach(Form  item in this.MdiChildren)
            //    {
            //        if (item is Descargas)
            //        {
            //            item.WindowState = FormWindowState.Normal;
            //            var = true;
            //            foreach (Form i in this.MdiChildren)
            //            {
            //                if (!(i is Descargas))
            //                {
            //                    i.WindowState = FormWindowState.Minimized;
            //                }
            //            }
            //        }
            //    }
            //    if (!var)
            //    {
            //        //if (Rol == (int)Constantes.RolSistemaSGUV.Cobranza || Rol == (int)Constantes.RolSistemaSGUV.Direccion) 
            //        //{
            //            Descargas formulario2 = new Descargas();
            //            formulario2.MdiParent = this;
            //            formulario2.Show();
            //            this.WindowState = FormWindowState.Maximized;
            //       // }
            //    }
            Descargas formulario2 = new Descargas();
            this.newForm(formulario2);
        }

        /// <summary>
        /// Muestra ADMINISTRACIÓN
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void administraciónDeUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            Administración formario2 = new Administración();
            this.newForm(formario2);

        }

        /// <summary>
        /// Muestra COSTOS BASE
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void costosBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultarCostosBase formulario2 = new ConsultarCostosBase();
            this.newForm(formulario2);
        }

        /// <summary>
        /// Muestra REPORTE DE UTILIDAD
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void utilidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Desarrollo.frmReporte formulario = new Desarrollo.frmReporte(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void costosBaseOriginalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CostoBaseOriginal formulario = new CostoBaseOriginal();

            this.newForm(formulario);
        }

        private void consularCostosBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CosultaCostoBase formulario = new CosultaCostoBase(Rol, NombreUsuario);
            this.newForm(formulario);
        }

        private void cobranzaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cobranza1 formulario = new Cobranza1(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void utilidadXLineasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UtilidadLineas formulario = new UtilidadLineas(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void actualizarCostoBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActualizarCostoBase formulario = new ActualizarCostoBase();
            this.newForm(formulario);
        }

        private void cosultarDescuentosEspecialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsultarDescEsp formulario = new ConsultarDescEsp();
            this.newForm(formulario);
        }

        private void scoreCardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ScoreCard formulario = new ScoreCard(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void objetivosMensualesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjetivoMensual formulario = new ObjetivoMensual();
            this.newForm(formulario);
        }

        private void lineasMoradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.frmLineasMoradas formulario = new Ventas.ScoreCard.frmLineasMoradas(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);

        }

        private void bonoLineasMoradasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.frmBonoLineasMoradas formulario = new Ventas.ScoreCard.frmBonoLineasMoradas(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void indicadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas.frmIndicadores formulario = new Ventas.frmIndicadores(Rol, Vendedor, Sucursal, DateTime.Now, NombreUsuario);
            this.newForm(formulario);
        }

        private void lineaObjetivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.frmLineaObjetivo formulario = new Ventas.ScoreCard.frmLineaObjetivo(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void auditoriaDePresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presupuesto.frmPresupuesto formulario = new Presupuesto.frmPresupuesto(Rol, Sucursal, NombreUsuario);
            this.newForm(formulario);
        }

        private void reciboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Cobranza.Principal p = new Cobranza.Principal();
            //p.Show();
            bool var = false;
            foreach (Form item in this.MdiChildren)
            {
                if (item is Cobranza.Principal)
                {
                    item.WindowState = FormWindowState.Normal;
                    var = true;
                    foreach (Form i in this.MdiChildren)
                    {
                        if (!(i is Cobranza.Principal))
                        {
                            i.WindowState = FormWindowState.Minimized;
                        }
                    }
                }
            }
            if (!var)
            {
                string paramNombre = "";
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", conn))
                    {
                        conn.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 8);
                        command.Parameters.AddWithValue("@Vendedores", string.Empty);
                        command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                        command.Parameters.AddWithValue("@Sucursal", string.Empty);
                        command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                        command.Parameters.AddWithValue("@Cliente", "0");
                        command.Parameters.AddWithValue("@Factura", string.Empty);
                        command.CommandTimeout = 0;

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            paramNombre = reader.GetString(reader.GetOrdinal("Nombre"));
                        }
                    }
                }


                Cobranza.Principal formulario2 = new Cobranza.Principal(Sucursal, paramNombre, Rol, NombreUsuario);
                formulario2.MdiParent = this;
                formulario2.Show();
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void envarFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.Form1 formulario = new Cobranza.Form1(NombreUsuario, Rol);
            this.newForm(formulario);
        }
        
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.Cobranza1 formulario = new Cobranza.Cobranza1(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void antigüedadDeSaldosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.AntiguiedadSaldos formulario = new Cobranza.AntiguiedadSaldos(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void saldosNoIdentificadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Cobranza.frmSaldosNI formulario = new Cobranza.frmSaldosNI(NombreUsuario);
            this.newForm(formulario);
        }

        private void seguimientoaDeCompromisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string paramNombre = "";
            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                    command.Parameters.AddWithValue("@Cliente", "0");
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.CommandTimeout = 0;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        paramNombre = reader.GetString(reader.GetOrdinal("Nombre"));
                    }
                }
            }

            Cobranza.AntiguedadSaldos.SeguimientoCompromisos formulario = new Cobranza.AntiguedadSaldos.SeguimientoCompromisos(Sucursal, Rol, paramNombre, NombreUsuario);
            this.newForm(formulario);
            //formulario2.MdiParent = this;
            //formulario2.Show();
            //this.WindowState = FormWindowState.Maximized;
        }

        


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Principal_Load(sender, e);
        }

        private void Principal_Shown(object sender, EventArgs e)
        {

            tooStatusP.Text = "Usuario: " + ClasesSGUV.Login.Usuario;
            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas)
            {
                Ventas.frmIndicadores frm = new Ventas.frmIndicadores(Rol, Vendedor, Sucursal, DateTime.Now, NombreUsuario);
                frm.MdiParent = this;
                frm.Show();
                // frm.WindowState = FormWindowState.Maximized;
            }

            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                string paramNombre = "";
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", conn))
                    {
                        conn.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 8);
                        command.Parameters.AddWithValue("@Vendedores", string.Empty);
                        command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                        command.Parameters.AddWithValue("@Sucursal", string.Empty);
                        command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                        command.Parameters.AddWithValue("@Cliente", "0");
                        command.Parameters.AddWithValue("@Factura", string.Empty);
                        command.CommandTimeout = 0;

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            paramNombre = reader.GetString(reader.GetOrdinal("Nombre"));
                        }
                    }
                }
                Cobranza.Indicares.InidicadoresMnsuales indicadores = new Cobranza.Indicares.InidicadoresMnsuales(paramNombre, Sucursal, Rol, NombreUsuario);
                indicadores.MdiParent = this;
                indicadores.Show();
            }
            //COORREOS COBRANZA
            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador && NombreUsuario.Equals("Administrador"))
            {
                DateTime hoy = DateTime.Now;

                if (hoy.DayOfWeek == DayOfWeek.Monday)
                {
                    //System.Threading.Thread processMails = new System.Threading.Thread(ProcesoCorreos);
                    //processMails.Start();
                    Cobranza.Indicadores.CorreosNCPendientes correos = new Cobranza.Indicadores.CorreosNCPendientes();
                    correos.Visible = false;
                    correos.Show();
                }
                //toolSendMails.Visible = true;

            }
            else
            {
                //toolSendMails.Visible = false;
            }
        }

        public void ProcesoCorreos()
        {
            Cobranza.Indicadores.CorreosNCPendientes correos = new Cobranza.Indicadores.CorreosNCPendientes();
            correos.Visible = false;
            correos.Show();
        }


        private void stripIndicCob_Click_1(object sender, EventArgs e)
        {
            string paramNombre = "";
            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                    command.Parameters.AddWithValue("@Cliente", "0");
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.CommandTimeout = 0;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        paramNombre = reader.GetString(reader.GetOrdinal("Nombre"));
                    }
                }
            }
            Cobranza.Indicares.InidicadoresMnsuales formulario = new Cobranza.Indicares.InidicadoresMnsuales(paramNombre, Sucursal, Rol, NombreUsuario);
            this.newForm(formulario);
        }

        private void stripSolicitud_Click(object sender, EventArgs e)
        {
            //Compras.Solicitudes.frmPPC formulario = new Compras.Solicitudes.frmPPC(Vendedor);
            //this.newForm(formulario);

            Compras.Solicitudes.frmSolicitudProducto formulario = new Compras.Solicitudes.frmSolicitudProducto();
            this.newForm(formulario);

        }

        private void analisisDePagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.PagosClientes formulario = new Cobranza.PagosClientes();
            this.newForm(formulario);
        }

        private void tendenciaDeNominaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presupuesto.RepNomina formulario = new Presupuesto.RepNomina(0);
            this.newForm(formulario);
        }

        private void tendenciaDeGastoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presupuesto.Form1 formulario = new Presupuesto.Form1();
            this.newForm(formulario);
        }

        private void scoreCardGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.ScocreCardGlobal formulario = new Cobranza.ScocreCardGlobal(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void objetivosMensualesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Cobranza.Score.Objetivos formulario = new Cobranza.Score.Objetivos();
            this.newForm(formulario);
        }

        private void stripCobertura_Click(object sender, EventArgs e)
        {
            string paramNombre = "";
            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                    command.Parameters.AddWithValue("@Cliente", "0");
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.CommandTimeout = 0;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        paramNombre = reader.GetString(reader.GetOrdinal("Nombre"));
                    }
                }
            }

            Cobranza.GestionCobranza.CoberturaCartera formulario = new Cobranza.GestionCobranza.CoberturaCartera(Sucursal, NombreUsuario, Rol, paramNombre);
            this.newForm(formulario);

        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            var forms = this.MdiChildren;

            if (forms.Count() > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Otros procesos se estan ejecutando,\r\n ¿Desea cerrar HalcoNET? ", "Alerta", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {

                }
                else if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void auditoriaDeRecibosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string paramNombre = "";
            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                    command.Parameters.AddWithValue("@Cliente", "0");
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.CommandTimeout = 0;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        paramNombre = reader.GetString(reader.GetOrdinal("Nombre"));
                    }
                }
            }

            Cobranza.RecibosReparto.AuditoriaRecibos formulario = new Cobranza.RecibosReparto.AuditoriaRecibos(Sucursal, paramNombre, Rol, NombreUsuario);
            this.newForm(formulario);

        }

        private void toolEdoResultados_Click(object sender, EventArgs e)
        {
            Presupuesto.EdoResultados formulario = new Presupuesto.EdoResultados(Rol, Sucursal);
            this.newForm(formulario);
        }


        private void stripRanking_Click(object sender, EventArgs e)
        {
            
            Ventas.frmRankingLineas formulario = new Ventas.frmRankingLineas(Rol, Vendedor, Sucursal, NombreUsuario);
            this.newForm(formulario);
        }

        private void stripVentaCaida_Click(object sender, EventArgs e)
        {
            Ventas.frmVentaCaida formulario = new Ventas.frmVentaCaida(Rol, Vendedor, Sucursal, NombreUsuario);
            this.newForm(formulario);
        }

        private void stripFacturasContado_Click(object sender, EventArgs e)
        {
            string paramNombre = "";
            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                    command.Parameters.AddWithValue("@Cliente", "0");
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.CommandTimeout = 0;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        paramNombre = reader.GetString(reader.GetOrdinal("Nombre"));
                    }
                }
            }

            Cobranza.Contado.FacturasContado formulario = new Cobranza.Contado.FacturasContado(Rol, Sucursal, paramNombre);
            this.newForm(formulario);
        }

        private void stripRemisiones_Click(object sender, EventArgs e)
        {
            string paramNombre = "";
            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                    command.Parameters.AddWithValue("@Cliente", "0");
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.CommandTimeout = 0;

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        paramNombre = reader.GetString(reader.GetOrdinal("Nombre"));
                    }
                }
            }

            Cobranza.Contado.frmRemisiones formulario = new Cobranza.Contado.frmRemisiones(Rol, Sucursal, paramNombre);
            this.newForm(formulario);
        }

        private void toolCargaMov_Click(object sender, EventArgs e)
        {
            Cobranza.Pagos.frmCargarExcel mov = new Cobranza.Pagos.frmCargarExcel();
            mov.MdiParent = this;
            mov.Show();
        }

        private void toolIdMov_Click(object sender, EventArgs e)
        {
            Cobranza.Pagos.frmIdentificarMovs mov = new Cobranza.Pagos.frmIdentificarMovs();
            mov.MdiParent = this;
            mov.Show();
        }

        private void toolTemplatePagos_Click(object sender, EventArgs e)
        {
            Cobranza.Pagos.frmTempletePagos mov = new Cobranza.Pagos.frmTempletePagos();
            mov.MdiParent = this;
            mov.Show();
        }

        private void promocionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Catalogos.Promociones mov = new Catalogos.Promociones();
            mov.MdiParent = this;
            mov.Show();
        }

        private void carteraVencidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Cobranza.Catalogos.CatalogosCob mov = new Cobranza.Catalogos.CatalogosCob("CV");
                mov.MdiParent = this;
                mov.Show();
            }
            catch (Exception)
            {
            }
        }

        private void stripCatalogoDC_Click(object sender, EventArgs e)
        {
            try
            {
                Cobranza.Catalogos.CatalogosCob mov = new Cobranza.Catalogos.CatalogosCob("DC");
                mov.MdiParent = this;
                mov.Show();
            }
            catch (Exception) { }
        }

        private void templateNoIdentificadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.Pagos.frmTempleteNI mov = new Cobranza.Pagos.frmTempleteNI(false);
            mov.MdiParent = this;
            mov.Show();
        }

        private void templateDemasíasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.Pagos.frmTemplateDemasias mov = new Cobranza.Pagos.frmTemplateDemasias();
            mov.MdiParent = this;
            mov.Show();
        }

        private void templateNotasDeCréditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.Pagos.frmTemplateNC mov = new Cobranza.Pagos.frmTemplateNC();
            mov.MdiParent = this;
            mov.Show();
        }

        private void pagosProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stripScoreCC_Click(object sender, EventArgs e)
        {
            ScoreCardCC formulario = new ScoreCardCC(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void stripLogs_Click(object sender, EventArgs e)
        {
            Pagos.ReportesHalcoNET.Logs log = new Pagos.ReportesHalcoNET.Logs();
            log.MdiParent = this;
            log.Show();

        }

        private void bloquearPreciosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query1 = "Select ItemCode FROM OITM Where QryGroup50 = 'Y' OR QryGroup51 = 'Y'";

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            {
                using (SqlCommand command = new SqlCommand(query1, connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    DataTable table = new DataTable();
                    da.Fill(table);

                    if (table.Rows.Count > 0)
                    {
                        DialogResult dia = MessageBox.Show("Se bloquearán " + table.Rows.Count + " artículos.\r\n¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dia == System.Windows.Forms.DialogResult.Yes)
                        {
                            string query = "Update OITM Set QryGroup50 = 'N', QryGroup51 = 'N' Where QryGroup50 = 'Y' OR QryGroup51 = 'Y' AND ItemType <> 'F'";

                            using (SqlConnection connection1 = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                            {
                                using (SqlCommand command1 = new SqlCommand(query, connection1))
                                {
                                    connection1.Open();
                                    int x = command1.ExecuteNonQuery();

                                    if (x > 0)
                                    {
                                        MessageBox.Show("Artículos bloqueados...", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }
                }
            }


        }

        private void stripPEJFacturas_Click(object sender, EventArgs e)
        {
            Cobranza.DocsElectronicos formulario = new Cobranza.DocsElectronicos(NombreUsuario, Rol);
            this.newForm(formulario);
        }

        private void vendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalisisClientes.Inicio formulario = new AnalisisClientes.Inicio();
            formulario.MdiParent = this;
            formulario.Show();

            AnalisisClientes.Controles.Ranking re = new AnalisisClientes.Controles.Ranking();
            re.Dock = DockStyle.Fill;
            re.BringToFront();
            formulario.Controls.Add(re);
            formulario.WindowState = FormWindowState.Maximized;

            this.newForm(formulario);
        }

        private void gerenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void comisionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Presupuesto.Comisiones formulario = new Presupuesto.Comisiones();
            this.newForm(formulario);
        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Cobranza.FacturasPendientes formulario = new Cobranza.FacturasPendientes(Rol, Sucursal, NombreUsuario);
            this.newForm(formulario);
        }

        private void auditoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Cobranza.Corcho.RevFacturas formulario = new Cobranza.Corcho.RevFacturas();
            this.newForm(formulario);
        }

        private void stripCalculo_Click(object sender, EventArgs e)
        {
            Ventas.frmCalculoUtilidad formulario2 = new Ventas.frmCalculoUtilidad();
            formulario2.MdiParent = this;
            formulario2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                toolClock.Text = DateTime.Now.ToString("HH:mm:ss");
            }
            catch (Exception)
            {

            }
        }

        private void resumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalisisClientes.Inicio formulario = new AnalisisClientes.Inicio();
            formulario.MdiParent = this;
            formulario.Show();

            AnalisisClientes.Controles.PrincipalGerencia re = new AnalisisClientes.Controles.PrincipalGerencia();
            re.Dock = DockStyle.Fill;
            formulario.Controls.Add(re);
            formulario.WindowState = FormWindowState.Maximized;

            this.newForm(formulario);
        }

        private void seguimientoACompromisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnalisisClientes.Inicio formulario = new AnalisisClientes.Inicio();
            formulario.MdiParent = this;
            formulario.Show();

            AnalisisClientes.Controles.Gerencia.SeguimientoCompromisos re = new AnalisisClientes.Controles.Gerencia.SeguimientoCompromisos(Rol, Sucursal, Vendedor);
            re.Dock = DockStyle.Fill;
            formulario.Controls.Add(re);
            formulario.WindowState = FormWindowState.Maximized;

            this.newForm(formulario);
        }

        private void toolPagosProv_Click(object sender, EventArgs e)
        {
            Pagos.PagosProveedores pago = new Pagos.PagosProveedores(Rol);
            pago.MdiParent = this;
            pago.Show();
        }

        private void tooStatusP_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialog = openImage.ShowDialog();
                if (dialog == System.Windows.Forms.DialogResult.OK)
                {
                    tooStatusP = null;
                    toolTipImage = null;

                    string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\HalcoNET\\img";

                    System.IO.Directory.CreateDirectory(mdoc);

                    string[] files = System.IO.Directory.GetFiles(mdoc, "*.png");

                    foreach (var item in files)
                    {
                        System.IO.File.Delete(item);
                    }
                    Bitmap vBitmap = new Bitmap(32, 32);
                    Image img = Image.FromFile(openImage.FileName);
                    //creamos un graphics tomando como base el nuevo Bitmap
                    string extension = System.IO.Path.GetExtension(openImage.FileName);
                    using (Graphics vGraphics = Graphics.FromImage((Image)vBitmap))
                    {

                        //especificamos el tipo de transformación, se escoge esta para no perder calidad.

                        vGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        //Se dibuja la nueva imagen

                        vGraphics.DrawImage(img, 0, 0, 32, 32);
                        img = (Image)vBitmap;
                        img.Save(mdoc + "\\userlog.png");

                    }



                    //  System.IO.File.Copy(openImage.FileName, mdoc + "\\userlog" + extension, true);

                }
            }
            catch (Exception)
            {

            }
            try
            {
                string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\HalcoNET\\img";

                if (System.IO.Directory.Exists(mdoc))
                {
                    string fromFile = string.Empty;
                    string[] images = System.IO.Directory.GetFiles(mdoc, "*.png");
                    foreach (var item in images)
                    {
                        fromFile = item;
                    }

                    tooStatusP.Image = Image.FromFile(fromFile);
                }
            }
            catch (Exception)
            {

            }
        }

        private void toolTemplateProv_Click(object sender, EventArgs e)
        {
            Pagos.TemplatePagosProv formulario = new Pagos.TemplatePagosProv();
            this.newForm(formulario);
        }

        private void stripClasifClientes_Click(object sender, EventArgs e)
        {
            Cobranza.ClasificacionClientes formulario = new Cobranza.ClasificacionClientes();
            this.newForm(formulario);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            int wid = e.ToolTipSize.Width + 50;
            int hgt = e.ToolTipSize.Height;
            if (hgt < 50) hgt = 50;
            e.ToolTipSize = new Size(wid, hgt);
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            string fromFile = string.Empty;
            //string mdoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\HalcoNET\\img";
            //if (System.IO.Directory.Exists(mdoc))
            //{

            //    string[] images = System.IO.Directory.GetFiles(mdoc, "*.png");
            //    foreach (var item in images)
            //    {
            //        fromFile = item;
            //    }
            //    // tooStatusP.Image = Image.FromFile(fromFile);
            //}

            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                Rectangle rect = new Rectangle(50, 0, e.Bounds.Width - 50, e.Bounds.Height);
                e.Graphics.DrawString(e.ToolTipText, e.Font, Brushes.Green, rect, sf);
            }

            e.Graphics.DrawImage(tooStatusP.Image, 9, 9);
        }

        private void stripGarantiasCompras_Click(object sender, EventArgs e)
        {
            Garantia.Garantias_Cobranza formulario = new Garantia.Garantias_Cobranza();
            this.newForm(formulario);
        }

        private void stripVtaEfectiva_Click(object sender, EventArgs e)
        {
            Ventas.frmVentaEfectiva formulario = new Ventas.frmVentaEfectiva();
            this.newForm(formulario);
        }

        private void stripAnalisisVentas_Click(object sender, EventArgs e)
        {
            Ventas.frmAnalisisVenta formulario = new Ventas.frmAnalisisVenta();
            this.newForm(formulario);
        }

        private void stripAtradius_Click(object sender, EventArgs e)
        {
            Cobranza.Atradius.frmAtradius formulario = new Cobranza.Atradius.frmAtradius();
            this.newForm(formulario);
        }
        
        private void stripHistoricosAtradius_Click(object sender, EventArgs e)
        {
            Cobranza.AtradiusHistoricos formulario = new Cobranza.AtradiusHistoricos();
            this.newForm(formulario);
        }

        private void stripClientesAtradius_Click(object sender, EventArgs e)
        {
            Cobranza.ClientesAtradius formulario = new Cobranza.ClientesAtradius();
            this.newForm(formulario);
        }

        private void stripReqPagos_Click(object sender, EventArgs e)
        {
            Pagos.PagosCorte pa = new Pagos.PagosCorte();
            this.newForm(pa);
        }

        private void stripMEP_Click(object sender, EventArgs e)
        {
            Cobranza.Reporte_MEP_Atradius formulario = new Cobranza.Reporte_MEP_Atradius();
            this.newForm(formulario);
        }

        private void stripFacturasProrrogaPago_Click(object sender, EventArgs e)
        {
            Cobranza.PagosProrrogas formulario = new Cobranza.PagosProrrogas();
            this.newForm(formulario);
        } 
        
        #endregion

        private void stripEvaluacionPP_Click(object sender, EventArgs e)
        {
            Cobranza.ReportePP formulario = new Cobranza.ReportePP();
            this.newForm(formulario);
        }

        private void stripTractoZone_Click(object sender, EventArgs e)
        {
            Ventas.ScoreCard.LineasTractozone formulario = new Ventas.ScoreCard.LineasTractozone(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario);
        }

        private void stripIndicadoresSucursal_Click(object sender, EventArgs e)
        {
            Cobranza.Indicadores.IndicadoresSucursal formulario = new Cobranza.Indicadores.IndicadoresSucursal();
            this.newForm(formulario);
        }

        private void stripReporteBancos_Click(object sender, EventArgs e)
        {
            Cobranza.frmReporteBancos formulario = new Cobranza.frmReporteBancos();
            this.newForm(formulario);
        }

        private void stripRptMtto_Click(object sender, EventArgs e)
        {
            Cobranza.frmMttoBancos formulario = new Cobranza.frmMttoBancos();
            this.newForm(formulario);
        }

        private void stripNuevoRptCompras_Click(object sender, EventArgs e)
        {
            Compras.frmNuevoCompras formulario = new Compras.frmNuevoCompras();
            this.newForm(formulario);
        }

        private void presupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presupuesto.Presupuesto formulario = new Presupuesto.Presupuesto();
            this.newForm(formulario);
        }

        private void nominaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presupuesto.Nomina formulario = new Presupuesto.Nomina();
            this.newForm(formulario);
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras.Desarrollo.frmCompras formulario = new Compras.Desarrollo.frmCompras();
            this.newForm(formulario);
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.frmReporteCuentaBanco formulario = new Cobranza.frmReporteCuentaBanco();
            this.newForm(formulario);
        }

        private void surtimientoRacsaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras.Reparticion.frmReparticionTractozone formulario = new Compras.Reparticion.frmReparticionTractozone();
            this.newForm(formulario);
        }

        private void consignasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras.Desarrollo.frmConsignas formulario = new Compras.Desarrollo.frmConsignas();
            this.newForm(formulario);
        }

        private void reportesPendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sistemas.frmReportePendientesABM formulario = new Sistemas.frmReportePendientesABM();
            this.newForm(formulario);
        }

        private void facturasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Desarrollo.Facturas formulario2 = new Desarrollo.Facturas(Rol, Vendedor, NombreUsuario, Sucursal);
            //Facturas formulario2 = new Facturas(Rol, Vendedor, NombreUsuario, Sucursal);
            this.newForm(formulario2);
        }

        private void lineasObjToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLineas formulario = new frmLineas(ClasesSGUV.Login.Rol, ClasesSGUV.Login.Vendedor1, ClasesSGUV.Login.NombreUsuario, ClasesSGUV.Login.Sucursal);
            this.newForm(formulario);
        }

        private void tractoZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTractoZone formulario = new frmTractoZone(ClasesSGUV.Login.Rol, ClasesSGUV.Login.Vendedor1, ClasesSGUV.Login.NombreUsuario, ClasesSGUV.Login.Sucursal);
            this.newForm(formulario);
        }

        private void objetivosLineasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmObjetivos formulario = new frmObjetivos();
            this.newForm(formulario);
        }

        private void requisicionDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presupuesto.Requisicion.frmRequisicion formulario = new Presupuesto.Requisicion.frmRequisicion();
            this.newForm(formulario);
        }

        private void traspasosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras.Desarrollo.frmTraspasosImpo formulario = new Compras.Desarrollo.frmTraspasosImpo();
            this.newForm(formulario);
        }

        private void kPISToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            KPIS.KPS formulario = new KPIS.KPS();
            this.newForm(formulario);
        }

        private void scoreProgramasHalcónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KPIS.frmScoreCardProgsHalcon formulario = new KPIS.frmScoreCardProgsHalcon();
            this.newForm(formulario);
        }

        private void scoreConsignasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KPIS.frmScoreConsignas formulario = new KPIS.frmScoreConsignas();
            this.newForm(formulario);
        }

        private void scoreRegionalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KPIS.frmScoreRegionales formulario = new KPIS.frmScoreRegionales();
            this.newForm(formulario);
        }

        private void coutasKPISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KPIS.frmControlCuotasKPS formulario = new KPIS.frmControlCuotasKPS();
            this.newForm(formulario);
        }

        private void addendaElecktraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PEJ.Addenda formulario = new PEJ.Addenda();
            this.newForm(formulario);
        }

        private void prorrogasJurídicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.frmAtradiusJuridico formulario = new Cobranza.frmAtradiusJuridico();
            this.newForm(formulario);
        }

        private void capturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.Contado.frm_ReciboPago formulario = new Cobranza.Contado.frm_ReciboPago();
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void reporteJefasDeCobranzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.Corcho.frmCorteCaja formulario = new Cobranza.Corcho.frmCorteCaja(2, "Corte de caja - Reporte Jefas Cobranza");
            formulario.MdiParent = this;
            formulario.Show();            
        }

        private void reporteAuditoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.Corcho.frmCorteCaja formulario = new Cobranza.Corcho.frmCorteCaja(3, "Corte de caja - Reporte Auditoria");
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void analisisDeVetaspagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.ReportesVarios.frmHistorialVentasPagos formulario = new Cobranza.ReportesVarios.frmHistorialVentasPagos();
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void facturasPagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.frmFacturasBrincadas formulario = new Cobranza.frmFacturasBrincadas(2);
            formulario.Text = "Aplicación con salto en Facturas";
            formulario.MdiParent = this;
            formulario.Show();
        }

        private void estadoDeCuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.GestionCobranza.frmEdoCuenta formulario = new Cobranza.GestionCobranza.frmEdoCuenta();
             this.newForm(formulario);
        }

        private void analisisObsoletosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras.frmAnalisisObsLento formulario = new Compras.frmAnalisisObsLento();
            this.newForm(formulario);
        }

        private void impresiónDeReferenciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.frmDepositos formulario = new Cobranza.frmDepositos();
            this.newForm(formulario);
        }

        private void créditosEnDesusoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.ReportesVarios.frmCreditosDesuso formulario = new Cobranza.ReportesVarios.frmCreditosDesuso();
            this.newForm(formulario);
        }

        private void teplateNotasDeCréditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cobranza.frmTemplateNC formulario = new Cobranza.frmTemplateNC();
            this.newForm(formulario);
        }

        private void auditoriaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Cobranza.Pagos.frmAuditoria formulario = new Cobranza.Pagos.frmAuditoria();
            this.newForm(formulario);
        }

        private void auditoriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cobranza.GestionCobranza.frmAuditoriaEdoCta formulario = new Cobranza.GestionCobranza.frmAuditoriaEdoCta();
            this.newForm(formulario);
        }

        private void expedientesCobranzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExpedientesCobranza.CargarDocumentos formulario = new ExpedientesCobranza.CargarDocumentos();
            this.newForm(formulario);
        }

        private void financierosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presupuesto.frmPerdidaGanancia formulario = new Presupuesto.frmPerdidaGanancia();
            this.newForm(formulario);

        }

        private void utilidadPorLineasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Desarrollo.UtilidadLineas formulario = new Desarrollo.UtilidadLineas(ClasesSGUV.Login.Rol, ClasesSGUV.Login.Vendedor1, ClasesSGUV.Login.NombreUsuario, ClasesSGUV.Login.Sucursal);
            this.newForm(formulario);
        }

        private void compromisoDeComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compras.Form1 formulario = new Compras.Form1();
            this.newForm(formulario);
        }

        #region Compras
        private void stripCompras13_3_Click(object sender, EventArgs e)
        {
            Compras.Transferencias_Compras.Compras formulario = new Compras.Transferencias_Compras.Compras();
            this.newForm(formulario);
        }

        private void stripInventarios06_Click(object sender, EventArgs e)
        {
            Compras.Transferencias_Compras.Transferencias formulario = new Compras.Transferencias_Compras.Transferencias();
            this.newForm(formulario);
        }

        private void stripInventarios05_Click(object sender, EventArgs e)
        {
            Compras.frmReubicaciones formulario = new Compras.frmReubicaciones();
            this.newForm(formulario);
        }

        private void stripCompras10_Click(object sender, EventArgs e)
        {
            Compras.Reparticion.frmReparticionStock formulario = new Compras.Reparticion.frmReparticionStock();
            this.newForm(formulario);
        }

        private void stripCompras06_Click(object sender, EventArgs e)
        {

            Compras.frmDesabasto formulario = new Compras.frmDesabasto();
            this.newForm(formulario);
        }

        private void stripCompras12_Click(object sender, EventArgs e)
        {
            Compras.SeguimientoCompras formulario = new Compras.SeguimientoCompras();
            this.newForm(formulario);
        }

        private void stripInventarios02_Click(object sender, EventArgs e)
        {
            Compras.frmClasificacionABC formulario = new Compras.frmClasificacionABC();
            this.newForm(formulario);
        }

        private void stripInventarios04_Click(object sender, EventArgs e)
        {
            Compras.frmIdealesArticulosAlmacen formulario = new Compras.frmIdealesArticulosAlmacen();
            this.newForm(formulario);
        }

        private void stripCompras02_Click(object sender, EventArgs e)
        {
            Compras.frmArribos formulario = new Compras.frmArribos(Vendedor);
            this.newForm(formulario);
        }

        private void stripInventarios03_1_Click(object sender, EventArgs e)
        {
            Ventas.Garantias formulario = new Ventas.Garantias();
            this.newForm(formulario);
        }

        private void stripInventarios03_2_Click(object sender, EventArgs e)
        {
            Garantia.Garantias_Compras formulario = new Garantia.Garantias_Compras();
            this.newForm(formulario);
        }

        private void stripCompras07_Click(object sender, EventArgs e)
        {
            Compras.frmEntradasMercancia formulario = new Compras.frmEntradasMercancia();
            this.newForm(formulario);
        }

        private void stripCompras08_Click(object sender, EventArgs e)
        {
            Compras.frmHistorialVentas formulario = new Compras.frmHistorialVentas();
            this.newForm(formulario);
        }

        private void stripCompras13_1_Click(object sender, EventArgs e)
        {
            Compras.frmIdealxLinea formulario = new Compras.frmIdealxLinea();
            this.newForm(formulario);
        }

        private void stripCompras01_Click(object sender, EventArgs e)
        {
            Compras.frmAnalisisCompras formulario = new Compras.frmAnalisisCompras();
            this.newForm(formulario);
        }

        private void stripCompras09_Click(object sender, EventArgs e)
        {
            Compras.frmPE formulario = new Compras.frmPE();
            this.newForm(formulario);
        }
        
        private void registroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Pagos.frmCreditosProvedor formulario = new Pagos.frmCreditosProvedor();
            this.newForm(formulario);
        }
        
        private void stripCompras04_Click(object sender, EventArgs e)
        {
            Compras.Desarrollo.frmLineasCompromiso formulario = new Compras.Desarrollo.frmLineasCompromiso();
            this.newForm(formulario);
        }

        private void stripInventarios01_Click(object sender, EventArgs e)
        {
            Compras.frmAnalisisObsLento formulario = new Compras.frmAnalisisObsLento();
            this.newForm(formulario);
        }

        private void stripCompras13_4_2_Click(object sender, EventArgs e)
        {
            Pagos.frmCreditosProvedorReporte formulario = new Pagos.frmCreditosProvedorReporte();
            this.newForm(formulario);
        }

        private void stripCompras13_1_Click_1(object sender, EventArgs e)
        {
            Compras.frmAcumuladoCompraVenta formulario = new Compras.frmAcumuladoCompraVenta();
            this.newForm(formulario);
        }

        private void stripCompras13_2_Click(object sender, EventArgs e)
        {
            Compras.frmClasificacionABCMTY formulario = new Compras.frmClasificacionABCMTY();
            this.newForm(formulario);
        }
        
        private void stripCompras14_Click_1(object sender, EventArgs e)
        {
            Compras.IndicadorDesabasto.frmIndicadores formulario = new Compras.IndicadorDesabasto.frmIndicadores();
            this.newForm(formulario);
        }
        private void stripCompras03_Click(object sender, EventArgs e)
        {
            Compras.Desarrollo.frmCompras formulario = new Compras.Desarrollo.frmCompras();
            this.newForm(formulario);
        }
        private void stripCompras11_Click(object sender, EventArgs e)
        {
            Compras.Desarrollo.frmTraspasosImpo formulario = new Compras.Desarrollo.frmTraspasosImpo();
            this.newForm(formulario);
        }

        private void stripCompras15_Click(object sender, EventArgs e)
        {
            Compras.Desarrollo.frmCostoVtaSucursal formulario = new Compras.Desarrollo.frmCostoVtaSucursal();
            this.newForm(formulario);
        }
        #endregion

        #region RRHH
        private void controlDeVacanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RRHH.frmCtl_Vacantes formulario = new RRHH.frmCtl_Vacantes();
            this.newForm(formulario);
        }
        #endregion

        private void utilidadSucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Desarrollo.UtilidadSucursales formulario = new Desarrollo.UtilidadSucursales(ClasesSGUV.Login.Rol, ClasesSGUV.Login.Vendedor1, ClasesSGUV.Login.NombreUsuario, ClasesSGUV.Login.Sucursal);
            this.newForm(formulario);
        }

        private void resultadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RRHH.Resultados.frmResumen formulario = new  RRHH.Resultados.frmResumen();
            formulario.Captura = true;
            this.newForm(formulario);
        }

        private void encuestaClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEncuesta formulario = new frmEncuesta();
            this.newForm(formulario);
        }

        private void stripInventarios07_Click(object sender, EventArgs e)
        {
            Compras.frmTraspasosFactura formulario = new Compras.frmTraspasosFactura();
            this.newForm(formulario);

        }

        private void ventaPérdidaOImcompletaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Desarrollo.frmVentaPerdida formulario = new Desarrollo.frmVentaPerdida();
            this.newForm(formulario);

        }

        



    }
}
