using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Cobranza
{
    public partial class ReporteCrystal : Form
    {
        #region PARAMETROS
        DataSet Ds = new DataSet();
        string Ruta;
        #endregion


        bool er = false;
        decimal Folio;
        public ReporteCrystal(DataSet _ds, string _ruta, string _name)
        {
            InitializeComponent();
            Ruta = _ruta;
            Ds = _ds;
            this.Text = _name;
            er = false;
            textBox1.Visible = false;
            button2.Visible = false;
        }

        public ReporteCrystal()
        {
            InitializeComponent();
        }

        public ReporteCrystal(decimal folio)
        {
            Folio = folio;
            er = true;
            InitializeComponent();
            textBox1.Visible = false;
            button2.Visible = false;
        }

        #region EVENTOS

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!textBox1.Visible)
                {
                    this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                    if (er == false)
                    {
                        ReportDocument doc = new ReportDocument();
                        doc.Load(Ruta);
                        doc.SetDataSource(Ds);
                        crystalReportViewer1.ReportSource = doc;
                    }
                    else
                    {
                        ReportDocument docFacturasI;
                        Tables CrTables;
                        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                        ConnectionInfo crConnectionInfo = new ConnectionInfo();

                        docFacturasI = new ReportDocument();
                        docFacturasI.Load(@"\\192.168.2.100\HalcoNET\Crystal\PJ_ReciboCobro.rpt");
                        crConnectionInfo.ServerName = "192.168.2.100";
                        crConnectionInfo.DatabaseName = "SBO-DistPJ";
                        crConnectionInfo.UserID = "sa";
                        crConnectionInfo.Password = "SAP-PJ1";
                        CrTables = docFacturasI.Database.Tables;

                        foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                        {
                            crtableLogoninfo = CrTable.LogOnInfo;
                            crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                            CrTable.ApplyLogOnInfo(crtableLogoninfo);

                        }
                        docFacturasI.SetParameterValue("Folio", Folio);

                        crystalReportViewer1.ReportSource = docFacturasI;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            ReportDocument docFacturasI;
            Tables CrTables;
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();

            docFacturasI = new ReportDocument();
            docFacturasI.Load(@"\\192.168.2.100\HalcoNET\Crystal\PJ_ReciboCobro.rpt");
            crConnectionInfo.ServerName = "192.168.2.100";
            crConnectionInfo.DatabaseName = "SBO-DistPJ";
            crConnectionInfo.UserID = "sa";
            crConnectionInfo.Password = "SAP-PJ1";
            CrTables = docFacturasI.Database.Tables;

            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);

            }
            docFacturasI.SetParameterValue("Folio", textBox1.Text);

            crystalReportViewer1.ReportSource = docFacturasI;
        }
    }
}

