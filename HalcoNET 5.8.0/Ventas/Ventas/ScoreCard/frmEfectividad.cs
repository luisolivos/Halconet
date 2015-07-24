using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.Ventas.ScoreCard
{
    public partial class frmEfectividad : Form
    {
        public DataTable TablaDetalle;
      //  public DataTable Tabla;
        public frmEfectividad(DataTable tbl, DataTable tbl2, string vendor)
        {
            TablaDetalle = tbl;
        //    Tabla = tbl2;
            this.Text += vendor;
            InitializeComponent();
        }
      
        private void Efectividad_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                DataTable tblTotal = new DataTable();
                tblTotal.Columns.Add("Vendedor", typeof(string));
                tblTotal.Columns.Add("Clientes", typeof(int));
                tblTotal.Columns.Add("Efectivo", typeof(int));
                tblTotal.Columns.Add("Inefectivo", typeof(int));
               
                tblTotal.Columns.Add("Efectividad", typeof(decimal), "Efectivo / Clientes");
                tblTotal.Columns.Add("Obj. Efectividad", typeof(decimal));
                tblTotal.Columns.Add("Obj Ctes Efectivos", typeof(decimal));
                tblTotal.Columns.Add("Venta CERO", typeof(int));

                var filterEfectivos = (from item in TablaDetalle.AsEnumerable()
                                            where item.Field<decimal>("Pronostico fin de mes(%)") >= (decimal)0.75
                                                && item.Field<string>("U_Efectividad") == "Y"
                                            select item) ;

                var filterVentaCero = (from item in TablaDetalle.AsEnumerable()
                                       where item.Field<decimal>("Pronostico fin de mes(%)") <= 0
                                          && item.Field<string>("U_Efectividad") == "Y"
                                       select item);
                var filterInfectivos = (from item in TablaDetalle.AsEnumerable()
                                        where item.Field<decimal>("Pronostico fin de mes(%)") < (decimal)0.75
                                        //  && item.Field<decimal>("Pronostico fin de mes(%)") > 0
                                          && item.Field<string>("U_Efectividad") == "Y"
                                        select item);
                DataRow r = tblTotal.NewRow();
                r["Vendedor"] = "TODOS";
                r["Clientes"] = filterEfectivos.Count() + filterInfectivos.Count();
                r["Efectivo"] = filterEfectivos.Count();
                r["Inefectivo"] = filterInfectivos.Count();
                r["Venta CERO"] = filterVentaCero.Count();

                r["Obj. Efectividad"] = Convert.ToDecimal(1);
                r["Obj Ctes Efectivos"] = Convert.ToDecimal(filterEfectivos.Count() + filterInfectivos.Count());

                tblTotal.Rows.Add(r);

                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = tblTotal;

                List<string> Vendedores = new List<string>();
                Vendedores = (from vendor in TablaDetalle.AsEnumerable()
                              select vendor.Field<string>("Vendedor")).Distinct().ToList();

                DataTable tblVendor = new DataTable();
                tblVendor.Columns.Add("Vendedor", typeof(string));
                tblVendor.Columns.Add("Clientes", typeof(int));
                tblVendor.Columns.Add("Efectivo", typeof(int));
                tblVendor.Columns.Add("Inefectivo", typeof(int));
                
                tblVendor.Columns.Add("Efectividad", typeof(decimal), "Efectivo / Clientes");
                tblVendor.Columns.Add("Obj. Efectividad", typeof(decimal));
                tblVendor.Columns.Add("Obj Ctes Efectivos", typeof(decimal));
                tblVendor.Columns.Add("Venta CERO", typeof(int));

                foreach (string v in Vendedores)
                {
                    var filterEfectivosV = (from item in TablaDetalle.AsEnumerable()
                                            where item.Field<decimal>("Pronostico fin de mes(%)") >= (decimal)0.75
                                                && item.Field<string>("U_Efectividad") == "Y"
                                                && item.Field<string>("Vendedor") == v
                                            select item) ;

                    var filterVentaCeroV = (from item in TablaDetalle.AsEnumerable()
                                            where item.Field<decimal>("Pronostico fin de mes(%)") <= 0
                                                && item.Field<string>("U_Efectividad") == "Y"
                                                && item.Field<string>("Vendedor") == v
                                            select item);

                    var filterInfectivosV = (from item in TablaDetalle.AsEnumerable()
                                             where item.Field<decimal>("Pronostico fin de mes(%)") < (decimal)0.75
                                               // && item.Field<decimal>("Pronostico fin de mes(%)") > 0
                                                && item.Field<string>("U_Efectividad") == "Y"
                                                && item.Field<string>("Vendedor") == v
                                             select item);

                    DataRow rv = tblVendor.NewRow();
                    rv["Vendedor"] = v;
                    rv["Clientes"] = filterEfectivosV.Count() + filterInfectivosV.Count();
                    rv["Efectivo"] = filterEfectivosV.Count();
                    rv["Inefectivo"] = filterInfectivosV.Count();
                    rv["Venta CERO"] = filterVentaCeroV.Count();

                    rv["Obj. Efectividad"] = Convert.ToDecimal(1);
                    rv["Obj Ctes Efectivos"] = Convert.ToDecimal(filterEfectivosV.Count()  + filterInfectivosV.Count());

                    tblVendor.Rows.Add(rv);
                }

                dataGridView2.DataSource = tblVendor;

                FormatoGrid(dataGridView1);
                FormatoGrid(dataGridView2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message);
            }
        }

        public void FormatoGrid(DataGridView dgv)
        {
            dgv.Columns["Vendedor"].HeaderCell.Style.BackColor = Color.FromArgb(31, 73, 125);
            dgv.Columns["Clientes"].HeaderCell.Style.BackColor = Color.FromArgb(31, 73, 125);
            dgv.Columns["Efectivo"].HeaderCell.Style.BackColor = Color.FromArgb(0, 176, 80);
            dgv.Columns["Inefectivo"].HeaderCell.Style.BackColor = Color.FromArgb(255, 255, 0);
            dgv.Columns["Venta CERO"].HeaderCell.Style.BackColor = Color.FromArgb(255, 0, 0);
            dgv.Columns["Efectividad"].HeaderCell.Style.BackColor = Color.FromArgb(31, 73, 125);
            dgv.Columns["Obj. Efectividad"].HeaderCell.Style.BackColor = Color.FromArgb(31, 73, 125);
            dgv.Columns["Obj Ctes Efectivos"].HeaderCell.Style.BackColor = Color.FromArgb(31, 73, 125);
           // dgv.Columns["Venta CERO"].Visible = false;
            dgv.Columns["Vendedor"].HeaderCell.Style.ForeColor = Color.White;
            dgv.Columns["Clientes"].HeaderCell.Style.ForeColor = Color.White;
            dgv.Columns["Efectivo"].HeaderCell.Style.BackColor = Color.FromArgb(0, 176, 80);
            dgv.Columns["Inefectivo"].HeaderCell.Style.BackColor = Color.FromArgb(255, 255, 0);
            dgv.Columns["Venta CERO"].HeaderCell.Style.ForeColor = Color.White;
            dgv.Columns["Efectividad"].HeaderCell.Style.ForeColor = Color.White;
            dgv.Columns["Obj. Efectividad"].HeaderCell.Style.ForeColor = Color.White;
            dgv.Columns["Obj Ctes Efectivos"].HeaderCell.Style.ForeColor = Color.White;

            dgv.Columns["Obj. Efectividad"].DefaultCellStyle.Format = "P2";
            dgv.Columns["Efectividad"].DefaultCellStyle.Format = "P2";
            dgv.Columns["Obj Ctes Efectivos"].DefaultCellStyle.Format = "N0";
            dgv.Columns["Venta CERO"].DefaultCellStyle.Format = "N0";
        }

        private void cbVendedores_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
