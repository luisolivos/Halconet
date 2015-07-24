using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.AnalisisClientes.Controles
{
    public partial class Pregunta3AD : UserControl
    {
        DataTable Precios = new DataTable();
        DataTable Articul = new DataTable();
        DataTable _Respuestas = new DataTable();

        public enum Columnas
        {
            Codigo,
            PrecioPJ,
            PrecioCompetencia,
            NombreCompetencia
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.NombreCompetencia].Width = 250;

            dgv.Columns[(int)Columnas.PrecioPJ].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PrecioCompetencia].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.PrecioPJ].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PrecioCompetencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public Pregunta3AD(DataTable _respuestas)
        {
            InitializeComponent();
            Precios.Columns.Add("Codigo", typeof(string));
            Precios.Columns.Add("Precio PJ", typeof(decimal));
            Precios.Columns.Add("Precio competencia", typeof(decimal));
            Precios.Columns.Add("Nombre competencia", typeof(string));

            _Respuestas = _respuestas;
        }

        public void ListaArticulos()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AnalisisVentas";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 4);
                    command.Parameters.AddWithValue("@Pregunta", 0);
                    command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                    command.Parameters.AddWithValue("@Letra", string.Empty);
                    command.Parameters.AddWithValue("@Especificacion", string.Empty);
                    command.Parameters.AddWithValue("@Linea", Clases.Contantes.Linea);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                    command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                    command.Parameters.AddWithValue("@Nombre", string.Empty);

                    command.CommandTimeout = 0;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(Articul);

                }
            }
        }

        private void Finalizar()
        {
            foreach (DataRow item in _Respuestas.Rows)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_AnalisisVentas";

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Pregunta", item.Field<Int32>("U_Pregunta"));
                        command.Parameters.AddWithValue("@Clasificacion", item.Field<string>("U_clasificacion"));
                        command.Parameters.AddWithValue("@Letra", item.Field<string>("U_Respuesta"));
                        command.Parameters.AddWithValue("@Especificacion", item.Field<string>("U_Especificacion"));
                        command.Parameters.AddWithValue("@Linea", item.Field<string>("U_Linea"));
                        command.Parameters.AddWithValue("@Cliente", item.Field<string>("U_Cliente"));

                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                        command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                        command.Parameters.AddWithValue("@Nombre", string.Empty);

                        command.CommandTimeout = 0;

                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.Parent.Controls.Remove(this);
        }

        private void Pregunta3AD_Load(object sender, EventArgs e)
        {
            
            this.ListaArticulos();

            lblCliente.Text ="Cliente: " + Clases.Contantes.Nombre + "\r\nLinea: " + Clases.Contantes.Linea;

            dgvItems.DataSource = Precios;
            this.Formato(dgvItems);

            this.Focus();
        }

        private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnFinalizar.Enabled)
                if (e.KeyCode == Keys.Escape)
                {
                    toolStripStatusLabel1_Click(sender, e);
                }
        }

        private void dgvRanking_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Articulos = (from item in Articul.AsEnumerable()
                                          select item.Field<string>("ItemCode").ToLower()).ToList();

                if (Articulos.Contains(txtArticulo.Text.ToLower()))
                {
                    if (!string.IsNullOrEmpty(txtPrecioPJ.Text))
                    {
                        if (!string.IsNullOrEmpty(txtPrecioCompetencia.Text))
                        {
                            if (!string.IsNullOrEmpty(txtNombreComp.Text))
                            {
                                try
                                {
                                    decimal precioPJ = Convert.ToDecimal(txtPrecioPJ.Text);
                                    decimal precioCompetencia = Convert.ToDecimal(txtPrecioCompetencia.Text);
                                    string articulo = (from item in Articul.AsEnumerable()
                                                       where item.Field<string>("ItemCode").ToLower().Contains(txtArticulo.Text.ToLower())
                                                       select item.Field<string>("ItemCode")).FirstOrDefault();

                                    string nombre = txtNombreComp.Text;

                                    DataRow row = Precios.NewRow();
                                    row["Codigo"] = articulo;
                                    row["Precio PJ"] = precioPJ;
                                    row["Precio competencia"] = precioCompetencia;
                                    row["Nombre competencia"] = nombre;

                                    Precios.Rows.Add(row);

                                    txtArticulo.Clear();
                                    txtPrecioPJ.Clear();
                                    txtNombreComp.Clear();
                                    txtPrecioCompetencia.Clear();

                                    txtArticulo.Focus();
                                }
                                catch (Exception ex)
                                {
                                   MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                MessageBox.Show("El campo [Nombre competencia] es obligatorio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El campo [Precio Competencia] es obligatorio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo [Precio PJ] es obligatorio", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("El artículo " + txtArticulo.Text + " no existe, o no pertenece a la línea " + Clases.Contantes.Linea +".", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvRanking_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            List<string> lineas2items = new List<string>();
            lineas2items.Add("ACURR");
            lineas2items.Add("BALAT");
            lineas2items.Add("CHEVRON");
            lineas2items.Add("MINCE");

            int itemsxlinea = 0;

            if (lineas2items.Contains(Clases.Contantes.Linea))
            {
                itemsxlinea = 2;
            }
            else
            {
                itemsxlinea = 5;
            }

            if (dgvItems.Rows.Count >= itemsxlinea)
            {
                //if (!string.IsNullOrEmpty(txtConsumo.Text))
                //{
                    try
                    {
                        this.Finalizar();

                       //Guardar articulos
                        foreach (DataRow  item in (dgvItems.DataSource as DataTable).Rows)
                        {
                            using (SqlConnection connection = new SqlConnection())
                            {
                                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                                using (SqlCommand command = new SqlCommand())
                                {
                                    command.CommandText = "PJ_AnalisisVentas";

                                    command.Connection = connection;
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@TipoConsulta", 5);
                                    command.Parameters.AddWithValue("@Pregunta", 0);
                                    command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                                    command.Parameters.AddWithValue("@Letra", string.Empty);
                                    command.Parameters.AddWithValue("@Especificacion", string.Empty);
                                    command.Parameters.AddWithValue("@Linea", Clases.Contantes.Linea);
                                    command.Parameters.AddWithValue("@Cliente", Clases.Contantes.Cliente);

                                    command.Parameters.AddWithValue("@Articulo", item.Field<string>("Codigo"));
                                    command.Parameters.AddWithValue("@PrecioPJ", item.Field<decimal>("Precio PJ"));
                                    command.Parameters.AddWithValue("@PrecioComp", item.Field<decimal>("Precio competencia"));
                                    command.Parameters.AddWithValue("@Nombre", item.Field<string>("Nombre competencia"));

                                    connection.Open();

                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        //Guardar consumo
                        //using (SqlConnection connection = new SqlConnection())
                        //{
                        //    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                        //    using (SqlCommand command = new SqlCommand())
                        //    {
                        //        command.CommandText = "PJ_AnalisisVentas";

                        //        command.Connection = connection;
                        //        command.CommandType = CommandType.StoredProcedure;
                        //        command.Parameters.AddWithValue("@TipoConsulta", 6);
                        //        command.Parameters.AddWithValue("@Pregunta", 0);
                        //        command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                        //        command.Parameters.AddWithValue("@Letra", string.Empty);
                        //        command.Parameters.AddWithValue("@Especificacion", string.Empty);
                        //        command.Parameters.AddWithValue("@Linea", Clases.Contantes.Linea);
                        //        command.Parameters.AddWithValue("@Cliente", Clases.Contantes.Cliente);

                        //        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        //        command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                        //        command.Parameters.AddWithValue("@PrecioComp", Convert.ToDecimal(txtConsumo.Text));
                        //        command.Parameters.AddWithValue("@Nombre", string.Empty);

                        //        connection.Open();

                        //        command.ExecuteNonQuery();
                        //    }
                        //}

                        MessageBox.Show("Registro exitoso.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        toolHome.Visible = true;
                        toolBack.Visible = false;
                        btnFinalizar.Enabled= false;
                    }
                    catch (Exception ex)
                    {
                       MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                //}
                //else
                //{
                //    MessageBox.Show("El campo consumo mensual es obligatorio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            else
            {
                MessageBox.Show("Para la línea "+ Clases.Contantes.Linea + " debes ingresar al menos " + itemsxlinea + " artículos.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lblHome_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Parent.Controls)
            {
                if (!(item is Controles.Ranking))
                {
                    try
                    {
                        item.Visible = false;
                    }
                    catch (Exception)
                    {
                    }
                }

            }
        }
    }
}
