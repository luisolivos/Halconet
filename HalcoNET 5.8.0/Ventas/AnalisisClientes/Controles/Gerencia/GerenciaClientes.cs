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

namespace Ventas.AnalisisClientes.Controles
{
    public partial class GerenciaClientes : UserControl
    {
        DataTable _Datos = new DataTable();
        int Vendedor;
        string Clasificacion;
        string Letra;
        string NombreVendedor;
        string ItmsGrpNam;

        public enum Columnas
        {
            Cliente, Nombre, PromedioVenta, PromedioLinea, Porcentaje, VentaAcutal, Comentarios, buton1, buton2
        }

        public void Formato(DataGridView dgv)
        {
            DataGridViewButtonColumn btnSi = new DataGridViewButtonColumn();
            {
                btnSi.Name = "Si";
                btnSi.HeaderText = "Si";
                btnSi.Width = 100;
                btnSi.UseColumnTextForButtonValue = true;
                btnSi.FlatStyle = FlatStyle.Popup;
            }

            dgv.Columns.Add(btnSi);

            DataGridViewButtonColumn btnNo = new DataGridViewButtonColumn();
            {
                btnNo.Name = "No";
                btnNo.HeaderText = "No";
                btnNo.Width = 100;
                btnNo.UseColumnTextForButtonValue = true;
                btnNo.FlatStyle = FlatStyle.Popup;
            }

            dgv.Columns.Add(btnNo);

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dgv.Columns[(int)Columnas.Cliente].Width = 80;
            dgv.Columns[(int)Columnas.Nombre].Width = 300;
            dgv.Columns[(int)Columnas.PromedioVenta].Width = 90;
            dgv.Columns[(int)Columnas.PromedioLinea].Width = 90;
            dgv.Columns[(int)Columnas.Porcentaje].Width = 90;
            dgv.Columns[(int)Columnas.VentaAcutal].Width = 90;
            dgv.Columns[(int)Columnas.Comentarios].Width = 90;

            dgv.Columns[(int)Columnas.Cliente].HeaderText = "Cliente";
            dgv.Columns[(int)Columnas.Nombre].HeaderText = "Nombre";
            dgv.Columns[(int)Columnas.PromedioVenta].HeaderText = "Promedio de\r\nventa";
            dgv.Columns[(int)Columnas.PromedioLinea].HeaderText = "Promedio de\r\nventa (linea)";
            dgv.Columns[(int)Columnas.VentaAcutal].HeaderText = "Venta\r\nActual";
            dgv.Columns[(int)Columnas.Comentarios].HeaderText = "Comentarios";

            dgv.Columns[(int)Columnas.PromedioVenta].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas.PromedioLinea].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)Columnas.Porcentaje].DefaultCellStyle.Format = "P0";
            dgv.Columns[(int)Columnas.VentaAcutal].DefaultCellStyle.Format = "C0";

            dgv.Columns[(int)Columnas.PromedioVenta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PromedioLinea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Porcentaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.VentaAcutal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public GerenciaClientes(int _vendedor, string _clasificacion, string _letra, string _respuesta, string _vendedorName)
        {
            InitializeComponent();
            Vendedor = _vendedor;
            Clasificacion = _clasificacion;
            Letra = _letra;
            NombreVendedor = _vendedorName;

            this.lblRespuesta.Text = _respuesta; 
        }

        public DataTable DataSource()
        {
            if (string.IsNullOrEmpty(ItmsGrpNam))
                ItmsGrpNam = string.Empty;
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AnalisisVentas";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 9);
                    command.Parameters.AddWithValue("@Pregunta", Vendedor);
                    command.Parameters.AddWithValue("@Clasificacion", Clasificacion);
                    command.Parameters.AddWithValue("@Letra", Letra);
                    command.Parameters.AddWithValue("@Especificacion", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                    command.Parameters.AddWithValue("@Articulo",  ItmsGrpNam);
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

        private void GerenciaClientes_Load(object sender, EventArgs e)
        {
            try
            {
                _Datos = this.DataSource();

                List<string> Lineas = new List<string>();

                Lineas = (from item in _Datos.AsEnumerable()
                          select item.Field<string>("ItmsGrpNam")).Distinct().ToList();
                Lineas.Insert(0, string.Empty);

                clbVendedor.DataSource = Lineas;

                lblVendedor.Text = NombreVendedor;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItmsGrpNam = clbVendedor.Text;

            try
            {
                _Datos = this.DataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                
                dgvDetalle.Columns.Clear();
                // this.Focus();
                string Linea = clbVendedor.Text;

                var query = from row in _Datos.AsEnumerable()
                            where row.Field<string>("ItmsGrpNam") == Linea
                                  && row.Field<string>("Existe") == "Y"
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

                    var queryDetalle = from item in encabezado.AsEnumerable()

                                       select new
                                       {
                                           Cliente = item.Field<string>("CardCode"),
                                           Nombre = item.Field<string>("CardName"),
                                           PromedioVenta = Convert.ToDecimal(_Datos.Compute("SUM(Promedio)", "CardCode='" + item.Field<string>("CardCode") + "'")),
                                           PromedioLinea = item.Field<decimal>("Promedio"),
                                           Porcentaje = Convert.ToDecimal(_Datos.Compute("SUM(Promedio)", "CardCode='" + item.Field<string>("CardCode") + "'")) == 0 ? 0 :
                                                        item.Field<decimal>("Promedio") / Convert.ToDecimal(_Datos.Compute("SUM(Promedio)", "CardCode='" + item.Field<string>("CardCode") + "'")),
                                           Mes1 = item.Field<decimal>("MES1"),
                                           Comentarios = item.Field<string>("Comentarios")
                                       };

                    dgvDetalle.DataSource = Clases.ListConverter.ToDataTable(queryDetalle.ToList());
                    this.Formato(dgvDetalle);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.Parent.Controls.Remove(this);
        }

        private void dgvDetalle_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex].Name == "Si" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = (sender as DataGridView).Rows[e.RowIndex].Cells["Si"] as DataGridViewButtonCell;
                Icon icoAtomico;



                icoAtomico = Properties.Resources.yes;
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 5, e.CellBounds.Top + 5);



                (sender as DataGridView).Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                (sender as DataGridView).Columns[e.ColumnIndex].Width = icoAtomico.Width + 10;

                e.Handled = true;
            }

            if (e.ColumnIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex].Name == "No" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = (sender as DataGridView).Rows[e.RowIndex].Cells["No"] as DataGridViewButtonCell;
                Icon icoAtomico;



                icoAtomico = Properties.Resources.error_do_not;
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 5, e.CellBounds.Top + 5);



                (sender as DataGridView).Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                (sender as DataGridView).Columns[e.ColumnIndex].Width = icoAtomico.Width + 10;

                e.Handled = true;
            }
        }

        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)Columnas.buton2)
                {
                    string cliente = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Cliente].Value.ToString();

                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.CommandText = "PJ_AnalisisVentas";

                            command.Connection = connection;
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", 12);
                            command.Parameters.AddWithValue("@Pregunta", 0);
                            command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                            command.Parameters.AddWithValue("@Letra", string.Empty);
                            command.Parameters.AddWithValue("@Especificacion", string.Empty);
                            command.Parameters.AddWithValue("@Linea", 0);
                            command.Parameters.AddWithValue("@Cliente", cliente);

                            command.Parameters.AddWithValue("@Articulo", ItmsGrpNam);
                            command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                            command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                            command.Parameters.AddWithValue("@Nombre", string.Empty);

                            command.CommandTimeout = 0;

                            connection.Open();

                            DialogResult result = MessageBox.Show("¿Esta seguro que el problema no tiene solución?\r\nSi elije 'Si' no podra visualizar nuevamante la información de este cliente.", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (result == DialogResult.Yes)
                            {
                                int rows = command.ExecuteNonQuery();

                                button1_Click(sender, e);
                            }
                        }
                    }
                }
                if (e.ColumnIndex == (int)Columnas.buton1)
                {
                    string cliente = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Cliente].Value.ToString();
                     string nombre = (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Nombre].Value.ToString();
                     decimal vtamensual =Convert.ToDecimal( (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.PromedioVenta].Value);
                     decimal vtalinea =Convert.ToDecimal( (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.PromedioLinea].Value);

                    Controles.Gerencia.SiSolucion f1 = new Gerencia.SiSolucion(cliente, nombre, vtamensual, vtalinea, clbVendedor.Text);
                    f1.Dock = DockStyle.Fill;
                    f1.Parent = this.Parent;
                    f1.BringToFront();
                    f1.Show();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
