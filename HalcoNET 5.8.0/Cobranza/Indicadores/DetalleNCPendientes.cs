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

using System.IO;
using System.Collections;
using System.Reflection;

namespace Cobranza.Indicadores
{
    public partial class DetalleNCPendientes : Form
    {
        public string Sucursal;
        public string Jefa;
        public DateTime Fecha;
        private int Factura;
        private int Vendedor;
        private string Cliente;
        DataTable factuas = new DataTable();
        DataTable detalles = new DataTable();
        DataSet data = null;
        BindingSource masterBindingSource = null;
        BindingSource detailsBindingSource = null;

        public enum Columnas
        {
            DocEntry, Factura, FechaFactura, FechaVto, Cliente, NombreCliente, SlpCode, Vendedor, TotalFactura, Saldo, DiasTrans, Asignada, Enviar
        }

        public enum ColumnasDetalle
        {
            DocEntry,
            LinenNum,
            Articulo,
            Nombre,
            Cantidad,
            PrecioSAP,
            PrecioReal,
            PrecioCliente
        }
        Clases.Logs log;

        public DetalleNCPendientes(string _sucursal, string _jefa, DateTime _fecha, string Usuario)
        {
            InitializeComponent();

            Sucursal = _sucursal;
            Jefa = _jefa;
            Fecha = _fecha;
            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }

        private void Formato()
        {
            dgvFacturas.Columns[(int)Columnas.DocEntry].Visible = false;
            dgvFacturas.Columns[(int)Columnas.SlpCode].Visible = false;

            dgvFacturas.Columns[(int)Columnas.Cliente].Width = 80;
            dgvFacturas.Columns[(int)Columnas.NombreCliente].Width = 200;

            dgvFacturas.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgvFacturas.Columns[(int)Columnas.TotalFactura].DefaultCellStyle.Format = "C2";
            dgvFacturas.Columns[(int)Columnas.DiasTrans].DefaultCellStyle.Format = "N0";

            dgvFacturas.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvFacturas.Columns[(int)Columnas.TotalFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvFacturas.Columns[(int)Columnas.DiasTrans].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvFacturas.Columns[(int)Columnas.Factura].ReadOnly = true;

            dgvFacturas.Columns[(int)Columnas.Factura].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.FechaFactura].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.FechaVto].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.NombreCliente].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.Vendedor].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.TotalFactura].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.Saldo].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.DiasTrans].ReadOnly = true;
            dgvFacturas.Columns[(int)Columnas.Asignada].ReadOnly = false;
        }

        private void FormatoDetalle()
        {
            dgvDetalle.Columns[(int)ColumnasDetalle.DocEntry].Visible = false;
            dgvDetalle.Columns[(int)ColumnasDetalle.LinenNum].Visible = false;

            dgvDetalle.Columns[(int)ColumnasDetalle.Nombre].Width = 150;

            dgvDetalle.Columns[(int)ColumnasDetalle.PrecioCliente].Visible = false;
            dgvDetalle.Columns[(int)ColumnasDetalle.PrecioReal].DefaultCellStyle.Format = "C2";
            dgvDetalle.Columns[(int)ColumnasDetalle.PrecioSAP].DefaultCellStyle.Format = "C2";
            dgvDetalle.Columns[(int)ColumnasDetalle.Cantidad].DefaultCellStyle.Format = "N0";

            dgvDetalle.Columns[(int)ColumnasDetalle.PrecioCliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalle.Columns[(int)ColumnasDetalle.PrecioReal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalle.Columns[(int)ColumnasDetalle.PrecioSAP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDetalle.Columns[(int)ColumnasDetalle.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvDetalle.Columns[(int)ColumnasDetalle.LinenNum].ReadOnly = true;
            dgvDetalle.Columns[(int)ColumnasDetalle.Articulo].ReadOnly = true;
            dgvDetalle.Columns[(int)ColumnasDetalle.Nombre].ReadOnly = true;
            dgvDetalle.Columns[(int)ColumnasDetalle.Cantidad].ReadOnly = true;
            dgvDetalle.Columns[(int)ColumnasDetalle.PrecioSAP].ReadOnly = true;
            dgvDetalle.Columns[(int)ColumnasDetalle.PrecioReal].ReadOnly = true;

        }

        private string MailVendedor(int _slpCode)
        {
            using (SqlConnection connection  = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    connection.Open();
                    command.CommandText = "Select U_Correo from OSLP Where SlpCode = @Vendedor";
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@Vendedor", _slpCode);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                    else
                        return string.Empty;
                }
            }
        }

        private string MailJefa(string _jefa)
        {
            string _mail = string.Empty;
            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_IndicadoresCobranza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    _mail = (from item in table.AsEnumerable()
                             where item.Field<string>("JefaCobranza") == _jefa
                             select item.Field<string>("Correo")).FirstOrDefault();
                }
            }
            return _mail;
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


        private void Insert(string _cliente, int _docentry, int _linenum, string _articulo, decimal _precio, int vendedor, string _comentario)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_NCPedientes", conn))
                    {
                        conn.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Cliente", _cliente);
                        command.Parameters.AddWithValue("@Docentry", _docentry);
                        command.Parameters.AddWithValue("@PrecioCliente", _precio);
                        command.Parameters.AddWithValue("@FechaEnvio", DateTime.Now);
                        command.Parameters.AddWithValue("@Vendedor", vendedor);
                        command.Parameters.AddWithValue("@Factura", Factura);

                        command.Parameters.AddWithValue("@Articulo", _articulo);
                        command.Parameters.AddWithValue("@Linenum", _linenum);
                        command.Parameters.AddWithValue("@Comentario", _comentario);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DetalleNCPendientes_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            data = null;
            masterBindingSource = null;
            detailsBindingSource = null;
            dgvFacturas.DataSource = null;
            dgvDetalle.DataSource = null;

            data = new DataSet();
            masterBindingSource = new BindingSource();
            detailsBindingSource = new BindingSource();

            factuas.Clear();
            detalles.Clear();

            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_IndicadoresCobranza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 4);
                    command.Parameters.AddWithValue("@Sucursales", Sucursal);
                    command.Parameters.AddWithValue("@JefaCobranza", Jefa);
                    command.Parameters.AddWithValue("@FechaInicial", Fecha);
                    command.Parameters.AddWithValue("@FechaFinal",  DateTime.Now);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(factuas);

                    
                }
            }

            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_IndicadoresCobranza", conn))
                {
                    conn.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 5);
                    command.Parameters.AddWithValue("@Sucursales", Sucursal);
                    command.Parameters.AddWithValue("@JefaCobranza", Jefa);
                    command.Parameters.AddWithValue("@FechaInicial", Fecha);
                    command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(detalles);

                }
            }

            
            factuas.TableName = "TablaFactura";
            detalles.TableName = "TablaDetalles";

            data.Tables.Add(factuas.Copy());
            data.Tables.Add(detalles.Copy());

            DataRelation relation = new DataRelation("FacturaDetalle", data.Tables["TablaFactura"].Columns["DocEntry"], data.Tables["TablaDetalles"].Columns["DocEntry"], false);
            data.Relations.Add(relation);

            masterBindingSource.DataSource = data;
            masterBindingSource.DataMember = "TablaFactura";
            detailsBindingSource.DataSource = masterBindingSource;
            detailsBindingSource.DataMember = "FacturaDetalle";

            dgvFacturas.DataSource = masterBindingSource;
            dgvDetalle.DataSource = detailsBindingSource;

            if (dgvFacturas.Columns.Count > 0)
                this.Formato();
            if (dgvDetalle.Columns.Count > 0)
                this.FormatoDetalle();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToInt32(item.Cells[(int)Columnas.DiasTrans].Value) > 20)
                    {
                        item.Cells[(int)Columnas.DiasTrans].Style.ForeColor = Color.White;
                        item.Cells[(int)Columnas.DiasTrans].Style.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();

                int facturas = Convert.ToInt32(data.Tables["TablaFactura"].Compute("COUNT(DocEntry)", "Asignar = 1"));

                if (facturas > 0)
                {
                    foreach (DataRow fact in data.Tables["TablaFactura"].Rows)
                    {
                        if (fact.Field<bool>("Asignar") == true)
                        {
                            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                            {
                                using (SqlCommand command = new SqlCommand("PJ_NCPedientes", connection))
                                {
                                    connection.Open();
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.CommandTimeout = 0;
                                    Factura = fact.Field<Int32>("Factura");
                                    Cliente = fact.Field<string>("Cliente");
                                    Vendedor = fact.Field<Int32>("SlpCode");
                                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                                    command.Parameters.AddWithValue("@Docentry", 0);
                                    command.Parameters.AddWithValue("@PrecioCliente", decimal.Zero);
                                    command.Parameters.AddWithValue("@FechaEnvio", DateTime.Now);
                                    command.Parameters.AddWithValue("@Vendedor", string.Empty);
                                    command.Parameters.AddWithValue("@Factura", Factura);
                                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                                    command.Parameters.AddWithValue("@Linenum", 0);
                                    command.Parameters.AddWithValue("@Comentario", string.Empty);

                                    decimal pago = Convert.ToDecimal(command.ExecuteScalar());
                                    DataTable table = dgvFacturas.DataSource as DataTable;

                                    //decimal pagoCliente = Convert.ToDecimal((from item in data.Tables["TablaDetalles"].AsEnumerable()
                                    //                                         where item.Field<Int32>("DocEntry") == fact.Field<Int32>("DocEntry")
                                    //                                         select item.Field<decimal>("Precio cliente") * item.Field<decimal>("Cantidad")).Sum());

                                    //if (pagoCliente != 0)
                                    //{

                                    //    if ((pago - pagoCliente) <= 1 && (pago - pagoCliente) >= -1)
                                    //    {

                                    foreach (DataRow item in data.Tables["TablaDetalles"].Rows)
                                    {
                                        if (item.Field<Int32>("DocEntry") == fact.Field<Int32>("DocEntry"))
                                        {
                                            this.Insert(Cliente, item.Field<Int32>("DocEntry"), item.Field<Int32>("LineNum"), item.Field<string>("Artículo"), decimal.Zero, Vendedor, string.Empty);
                                        }
                                        // this.DetalleNCPendientes_Load(sender, e);

                                    }
                                    MessageBox.Show("La factura " + Factura + " se envío correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    }
                                    //    else
                                    //    {
                                    //        MessageBox.Show("Los precios del cliente no corresponden a lo que pago.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //    }
                                    //}
                                    //else if (pagoCliente == 0)
                                    //{
                                    //    DialogResult dialogResult = MessageBox.Show("Los precios del cliente en la Factura " + Factura + " estan cero, se enviará al vendedor indicando que el cliente no proporciono información", "Alerta", MessageBoxButtons.YesNo);
                                    //    if (dialogResult == DialogResult.Yes)
                                    //    {
                                    //        foreach (DataRow item in data.Tables["TablaDetalles"].Rows)
                                    //        {
                                    //            this.Insert(Cliente, item.Field<Int32>("DocEntry"), item.Field<Int32>("LineNum"), item.Field<string>("Artículo"), item.Field<decimal>("Precio cliente"), Vendedor, "Prueba");
                                    //            //this.DetalleNCPendientes_Load(sender, e);

                                    //        }
                                    //        MessageBox.Show("La factura " + Factura + " se envío correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    }
                                    //    else if (dialogResult == DialogResult.No)
                                    //    {

                                    //    }
                                    //}

                                }
                            }
                        }

                    }
                    this.DetalleNCPendientes_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar al menos una factura.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //bool enviados = false;
                //string _vendedores = string.Empty;

                //var ListaVendedores = (from item in (dataGridView1.DataSource as DataTable).AsEnumerable()
                //                       select new
                //                       {
                //                           SlpCode = item.Field<int>("SlpCode"),
                //                           Vendedor = item.Field<string>("Vendedor")
                //                       }).Distinct().ToList();

                //foreach (var vendedor in ListaVendedores)
                //{
                //    DataTable t = (dataGridView1.DataSource as DataTable);

                //    var facturas = from item in t.AsEnumerable()
                //                   where item.Field<string>("Vendedor") == vendedor.Vendedor.ToString()
                //                         && item.Field<bool>("Enviar") == true
                //                   orderby item.Field<DateTime>("Fecha factura")
                //                   select item;

                //    if (facturas.Count() > 0)
                //    {
                //        Clases.CrearPDF pdf = new Clases.CrearPDF();
                //        pdf.ToPDF(facturas.CopyToDataTable(), vendedor.Vendedor.ToString(), Jefa);

                //        string file = "PDF\\" + pdf.Nombre;
                //        string destinatario = this.MailVendedor(vendedor.SlpCode);
                //        string cc = this.MailJefa(Jefa);

                //        SendMail mail = new SendMail();

                //        enviados = mail.Enviar(file, destinatario, vendedor.Vendedor, cc, Jefa);
                //        _vendedores += vendedor.Vendedor + "\r\n";
                //        if (!enviados)
                //        {
                //            MessageBox.Show("Información adicional\r\n" + "Archivo: " + file + "\r\nVendedor: " + destinatario, "Error al enviar correo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            return;
                //        }
                //    }
                //}

                //MessageBox.Show("Se envió correo exitosamente a: \r\n" + _vendedores, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void DetalleNCPendientes_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void DetalleNCPendientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            if (exp.Exportar(dgvFacturas, true))
            {
                MessageBox.Show("El archivo se creo con exito", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{


            //    Factura = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Factura].Value);
            //    Cliente = Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Cliente].Value);
            //    Vendedor = Convert.ToInt32((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.SlpCode].Value);

            //    using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            //    {
            //        using (SqlCommand command = new SqlCommand("PJ_NCPedientes", conn))
            //        {
            //            conn.Open();
            //            command.CommandType = CommandType.StoredProcedure;
            //            command.CommandTimeout = 0;

            //            command.Parameters.AddWithValue("@TipoConsulta", 1);
            //            command.Parameters.AddWithValue("@Cliente", string.Empty);
            //            command.Parameters.AddWithValue("@Docentry", 0);
            //            command.Parameters.AddWithValue("@PrecioCliente", decimal.Zero);
            //            command.Parameters.AddWithValue("@FechaEnvio", DateTime.Now);
            //            command.Parameters.AddWithValue("@Vendedor", string.Empty);
            //            command.Parameters.AddWithValue("@Factura", Factura);

            //            command.Parameters.AddWithValue("@Articulo", string.Empty);
            //            command.Parameters.AddWithValue("@Linenum", 0);
            //            command.Parameters.AddWithValue("@Comentario", string.Empty);

            //            DataTable table = new DataTable();
            //            SqlDataAdapter adapter = new SqlDataAdapter();
            //            adapter.SelectCommand = command;
            //            adapter.Fill(table);

            //            dataGridView2.DataSource = table;

            //            if (dataGridView2.Columns.Count > 0)
            //                this.FormatoDetalle();
            //        }
            //    }

            //    if (e.ColumnIndex == ((int)Columnas.Enviar))
            //    {
            //        MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }
    }
}
