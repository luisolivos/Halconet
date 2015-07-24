using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Ventas.AnalisisClientes.Controles
{
    public partial class form1 : UserControl
    {
        string Vendedor;
        string Linea;
        DataTable Datos = new DataTable();

        public enum Columnas1
        {
            Linea,
            Mes7,
            Mes6,
            Mes5,
            Mes4,
            Mes3,
            Mes2,
            Promedio,
            Mes1
        }

        public enum Columnas2
        {
            Cliente,
            Nombre,
            PromedioVenta,
            PromedioLinea,
            Porcentaje,
            Mes1,
            Boton
        }

        public form1(string _vendedor, string _linea, DataTable _Datos)
        {
            InitializeComponent();
            Vendedor = _vendedor;
            Datos = _Datos;
            Linea = _linea;
        }

        public void FormatoEncabezado(DataGridView dgv)
        {
            int act = DateTime.Now.Month;
            DateTimeFormatInfo fecha = CultureInfo.CurrentCulture.DateTimeFormat;

            string actual = fecha.GetMonthName(DateTime.Now.Month);

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.Columns[(int)Columnas1.Mes7].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas1.Mes6].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas1.Mes5].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas1.Mes4].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas1.Mes3].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas1.Mes2].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas1.Promedio].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas1.Mes1].DefaultCellStyle.Format = "C0";

            dgv.Columns[(int)Columnas1.Mes7].HeaderText = fecha.GetMonthName(DateTime.Now.Month - 6) + "\r\n" + DateTime.Now.AddMonths(-6).Year;
            dgv.Columns[(int)Columnas1.Mes6].HeaderText = fecha.GetMonthName(DateTime.Now.Month - 5) + "\r\n" + DateTime.Now.AddMonths(-5).Year;
            dgv.Columns[(int)Columnas1.Mes5].HeaderText = fecha.GetMonthName(DateTime.Now.Month - 4) + "\r\n" + DateTime.Now.AddMonths(-4).Year;
            dgv.Columns[(int)Columnas1.Mes4].HeaderText = fecha.GetMonthName(DateTime.Now.Month - 3) + "\r\n" + DateTime.Now.AddMonths(-3).Year;
            dgv.Columns[(int)Columnas1.Mes3].HeaderText = fecha.GetMonthName(DateTime.Now.Month - 2) + "\r\n" + DateTime.Now.AddMonths(-2).Year;
            dgv.Columns[(int)Columnas1.Mes2].HeaderText = fecha.GetMonthName(DateTime.Now.Month - 1) + "\r\n" + DateTime.Now.AddMonths(-1).Year;
            dgv.Columns[(int)Columnas1.Mes1].HeaderText = fecha.GetMonthName(DateTime.Now.Month) + "\r\n" + DateTime.Now.AddMonths(0).Year;

            dgv.Columns[(int)Columnas1.Mes7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Mes6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Mes5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Mes4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Mes3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Mes2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Mes1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
            dgv.Columns[(int)Columnas1.Promedio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void FormatoDetalle(DataGridView dgv)
        {

            DataGridViewButtonColumn buttonComent = new DataGridViewButtonColumn();
            {
                buttonComent.Name = "btnSeleccionar";
                buttonComent.HeaderText = "Seleccionar";
                buttonComent.Width = 100;
                buttonComent.UseColumnTextForButtonValue = true;
                buttonComent.FlatStyle = FlatStyle.Popup;
            }

            dgv.Columns.Add(buttonComent);

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.Columns[(int)Columnas2.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)Columnas2.Nombre].HeaderText = "Nombre";
            dgv.Columns[(int)Columnas2.PromedioVenta].HeaderText = "Promedio de\r\nventa";
            dgv.Columns[(int)Columnas2.PromedioLinea].HeaderText = "Promedio de\r\nventa (linea)";
            dgv.Columns[(int)Columnas2.Mes1].HeaderText = "Venta\r\nActual";

            dgv.Columns[(int)Columnas2.PromedioVenta].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas2.PromedioLinea].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas2.Porcentaje].DefaultCellStyle.Format = "P0";
            dgv.Columns[(int)Columnas2.Mes1].DefaultCellStyle.Format = "C0";

            dgv.Columns[(int)Columnas2.Nombre].Width = 350;
            dgv.Columns[(int)Columnas2.PromedioVenta].Width = 100;
            dgv.Columns[(int)Columnas2.PromedioLinea].Width = 100;
            dgv.Columns[(int)Columnas2.Porcentaje].Width = 100;
            dgv.Columns[(int)Columnas2.Mes1].Width = 100;
  
            dgv.Columns[(int)Columnas2.PromedioVenta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.PromedioLinea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.Mes1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void form1_Load(object sender, EventArgs e)
        {
            lblCliente.Text = "Linea: " + Linea;
            txtVendedor.Text = Vendedor;
            dgvDetalle.Columns.Clear();
            this.Focus();

            var query = from row in Datos.AsEnumerable()
                        where row.Field<string>("ItmsGrpNam") == Linea
                              && row.Field<string>("SlpName") == Vendedor
                        select row;

            if (query.Count() > 0)
            {
                DataTable encabezado = query.CopyToDataTable();

                var queryEncabezado = from item in encabezado.AsEnumerable()
                            group item by new
                            {
                                Linea = item.Field<string>("ItmsGrpNam")
                            } into grouped
                            select new
                            {
                                Linea = grouped.Key.Linea,
                                Mes7 = grouped.Sum(ix => ix.Field<decimal>("MES7")),
                                Mes6 = grouped.Sum(ix => ix.Field<decimal>("MES6")),
                                Mes5 = grouped.Sum(ix => ix.Field<decimal>("MES5")),
                                Mes4 = grouped.Sum(ix => ix.Field<decimal>("MES4")),
                                Mes3 = grouped.Sum(ix => ix.Field<decimal>("MES3")),
                                Mes2 = grouped.Sum(ix => ix.Field<decimal>("MES2")),
                                Promedio = grouped.Sum(ix => ix.Field<decimal>("Promedio")),
                                Mes1 = grouped.Sum(ix => ix.Field<decimal>("MES1"))
                            };

                dgvEncabezado.DataSource = Clases.ListConverter.ToDataTable(queryEncabezado.ToList());
                this.FormatoEncabezado(dgvEncabezado);

                var queryDetalle = from item in encabezado.AsEnumerable()
                                   select new
                                   {
                                       Cliente = item.Field<string>("CardCode"),
                                       Nombre = item.Field<string>("CardName"),
                                       PromedioVenta = Convert.ToDecimal(Datos.Compute("SUM(Promedio)", "CardCode='" + item.Field<string>("CardCode") + "'")),
                                       PromedioLinea = item.Field<decimal>("Promedio"),
                                       Porcentaje = Convert.ToDecimal(Datos.Compute("SUM(Promedio)", "CardCode='" + item.Field<string>("CardCode") + "'")) == 0 ? 0 :
                                                    item.Field<decimal>("Promedio") / Convert.ToDecimal(Datos.Compute("SUM(Promedio)", "CardCode='" + item.Field<string>("CardCode") + "'")),
                                       Mes1 = item.Field<decimal>("MES1")
                                   };

                dgvDetalle.DataSource = Clases.ListConverter.ToDataTable( queryDetalle.ToList());

                
                this.FormatoDetalle(dgvDetalle);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.Parent.Controls.Remove(this);
        }

        private void dgvDetalle_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = (sender as DataGridView).Rows[e.RowIndex].Cells["btnSeleccionar"] as DataGridViewButtonCell;
                Icon icoAtomico;



                icoAtomico = Properties.Resources.miniarrow_right_blue;
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left, e.CellBounds.Top);



                (sender as DataGridView).Rows[e.RowIndex].Height = icoAtomico.Height + 2;
                (sender as DataGridView).Columns[e.ColumnIndex].Width = icoAtomico.Width + 2;

                e.Handled = true;
            }
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((sender as DataGridView).Columns[e.ColumnIndex].Name == "btnSeleccionar")
                {
                    string Cliente = (sender as DataGridView).Rows[e.RowIndex].Cells["Cliente"].Value.ToString();
                    string Nombre = (sender as DataGridView).Rows[e.RowIndex].Cells["Nombre"].Value.ToString();
                    Clases.Contantes.Cliente = Cliente;
                    Clases.Contantes.Nombre = Nombre;

                    Controles.Pregunta1 p1 = new Pregunta1(Cliente, Nombre);
                    p1.Dock = DockStyle.Fill;

                    this.Parent.Controls.Add(p1);
                    p1.BringToFront();
                }
            }
            catch (Exception)
            {
            }
        }

        private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button1_Click(sender, e);
            }
        }
    }
}
