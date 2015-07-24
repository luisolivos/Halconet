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
    public partial class NCPorAclarar : Form
    {
        int SlpCode;
        int Rol;
        public enum Columnas
        {
            Factura, Fecha, Cliente, Nombre, PRecioSap, PrecioReal, Pagos, Dias, Diferencia
        }

        public enum ColumnasDetalle
        {
            Factura, Articulo, nombre, cantidad, PrecioSap, PrecioReal, PrecioCliente, Comentarios, LineNum, Docentry, CostoBase, Canal, Utilidad
        }

        public void Formato1(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Factura].Width = 100;
            dgv.Columns[(int)Columnas.Fecha].Width = 100;
            dgv.Columns[(int)Columnas.Cliente].Width = 100;
            dgv.Columns[(int)Columnas.Nombre].Width = 250;
            dgv.Columns[(int)Columnas.PRecioSap].Width = 100;
            dgv.Columns[(int)Columnas.PrecioReal].Width = 100;
            dgv.Columns[(int)Columnas.Pagos].Width = 100;
            dgv.Columns[(int)Columnas.Diferencia].Width = 100;
           // dgv.Columns[(int)Columnas.Dias].Visible = false;


            dgv.Columns[(int)Columnas.PRecioSap].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PrecioReal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Pagos].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Diferencia].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.PRecioSap].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PrecioReal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Pagos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public void Formato2(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasDetalle.Factura].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.Articulo].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.nombre].Width = 180;
            dgv.Columns[(int)ColumnasDetalle.cantidad].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.PrecioSap].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.PrecioReal].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.PrecioCliente].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.Comentarios].Width = 100;

            dgv.Columns[(int)ColumnasDetalle.Factura].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.Articulo].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.nombre].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.cantidad].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.PrecioSap].ReadOnly = true;
            dgv.Columns[(int)ColumnasDetalle.PrecioReal].ReadOnly = false;
            dgv.Columns[(int)ColumnasDetalle.PrecioReal].DefaultCellStyle.BackColor = Color.FromName("Silver");
            dgv.Columns[(int)ColumnasDetalle.PrecioCliente].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.Comentarios].ReadOnly = true;

            dgv.Columns[(int)ColumnasDetalle.cantidad].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasDetalle.PrecioSap].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.PrecioReal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.PrecioCliente].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasDetalle.cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasDetalle.cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.PrecioSap].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.PrecioReal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasDetalle.LineNum].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.Docentry].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.CostoBase].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.Canal].Visible = false;
            dgv.Columns[(int)ColumnasDetalle.Utilidad].Visible = false;

            /*
             , , , , */
        }

        public NCPorAclarar(int vendor, int _rol)
        {
            InitializeComponent();
            SlpCode = vendor;
            Rol = _rol;
        }

        public DataTable ProcedureSelect(int _tipo, string _cliente, int _docentry, decimal _precio, DateTime _fecha, 
                int _vendedor, int _factura, string _articulo, int _linenum, string _coments, string _name)
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

                        command.Parameters.AddWithValue("@TipoConsulta", _tipo);
                        command.Parameters.AddWithValue("@Cliente", _cliente);
                        command.Parameters.AddWithValue("@Docentry", _docentry);
                        command.Parameters.AddWithValue("@PrecioCliente", _precio);
                        command.Parameters.AddWithValue("@FechaEnvio", _fecha);
                        command.Parameters.AddWithValue("@Vendedor", _vendedor);
                        command.Parameters.AddWithValue("@Factura", _factura);

                        command.Parameters.AddWithValue("@Articulo", _articulo);
                        command.Parameters.AddWithValue("@Linenum", _linenum);
                        command.Parameters.AddWithValue("@Comentario", _coments);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.Fill(table);
                        table.TableName = _name;
                        return table;
                    }
                }
            }
            catch (Exception)
            {
                return new DataTable(_name);
            }
        }

        private void NCPorAclarar_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                DataSet data = new DataSet();
                BindingSource masterBindingSource = new BindingSource();
                BindingSource detailsBindingSource = new BindingSource();

                data.Tables.Add(this.ProcedureSelect(5, string.Empty, 0, decimal.Zero, DateTime.Now, SlpCode, 0, string.Empty, 0, string.Empty, "Facturas"));
                data.Tables["Facturas"].Columns.Add("Diferencia", typeof(decimal), "[Precio Real] - Pagos");

                data.Tables.Add(this.ProcedureSelect(4, string.Empty, 0, decimal.Zero, DateTime.Now, SlpCode, 0, string.Empty, 0, string.Empty, "Detalle"));
                data.Tables["Detalle"].Columns.Add("Utilidad", typeof(decimal), "IIF([Precio Real] = 0, 0, 1-([Costo Base]/[Precio Real]))");

                DataRelation relation = new DataRelation("FacturaDetalle", data.Tables["Facturas"].Columns["Factura"], data.Tables["Detalle"].Columns["Factura"]);

                data.Relations.Add(relation);

                masterBindingSource.DataSource = data;
                masterBindingSource.DataMember = "Facturas";
                detailsBindingSource.DataSource = masterBindingSource;
                detailsBindingSource.DataMember = "FacturaDetalle";
                gridFacturas.DataSource = masterBindingSource;
                gridDetalles.DataSource = detailsBindingSource;

                this.Formato1(gridFacturas);
                this.Formato2(gridDetalles);
            }
            catch (Exception )
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int Errors = 0;
                string result = string.Empty;
                foreach (DataGridViewRow item in gridDetalles.Rows)
                {
                    if (Rol != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                    {
                        if (item.Cells[(int)ColumnasDetalle.Canal].Value.ToString() == "T" && Convert.ToDecimal(item.Cells[(int)ColumnasDetalle.Utilidad].Value) < (decimal)0.16)
                        {
                            Errors++;
                            item.Cells[(int)ColumnasDetalle.PrecioReal].Style.BackColor = Color.Red;
                            item.Cells[(int)ColumnasDetalle.PrecioReal].Style.ForeColor = Color.White;
                        }
                        if (item.Cells[(int)ColumnasDetalle.Canal].Value.ToString() == "M" && Convert.ToDecimal(item.Cells[(int)ColumnasDetalle.Utilidad].Value) < (decimal)0.13)
                        {
                            Errors++;
                            item.Cells[(int)ColumnasDetalle.PrecioReal].Style.BackColor = Color.Red;
                            item.Cells[(int)ColumnasDetalle.PrecioReal].Style.ForeColor = Color.White;
                        }
                        if (item.Cells[(int)ColumnasDetalle.PrecioReal].Value.ToString() == "A" && Convert.ToDecimal(item.Cells[(int)ColumnasDetalle.Utilidad].Value) < (decimal)0.13)
                        {
                            Errors++;
                            item.Cells[(int)ColumnasDetalle.PrecioReal].Style.BackColor = Color.Red;
                            item.Cells[(int)ColumnasDetalle.PrecioReal].Style.ForeColor = Color.White;
                        }

                        // precios reales no mayores a precio sap
                        if (Convert.ToDecimal(item.Cells[(int)ColumnasDetalle.PrecioReal].Value) > Convert.ToDecimal(item.Cells[(int)ColumnasDetalle.PrecioSap].Value))
                        {
                            Errors++;
                            item.Cells[(int)ColumnasDetalle.PrecioReal].Style.BackColor = Color.Red;
                            item.Cells[(int)ColumnasDetalle.PrecioReal].Style.ForeColor = Color.White;
                        }
                    }
                }

                if (Errors == 0)
                {
                    foreach (DataGridViewRow item in gridDetalles.Rows)
                    {
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("PJ_NCPedientes", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@TipoConsulta", 7);
                                command.Parameters.AddWithValue("@Cliente", ClasesSGUV.Login.NombreUsuario);
                                command.Parameters.AddWithValue("@Docentry", Convert.ToInt32(item.Cells[(int)ColumnasDetalle.Docentry].Value));
                                command.Parameters.AddWithValue("@PrecioCliente", Convert.ToDecimal(item.Cells[(int)ColumnasDetalle.PrecioReal].Value));
                                command.Parameters.AddWithValue("@FechaEnvio", DateTime.Now);
                                command.Parameters.AddWithValue("@Vendedor", SlpCode);
                                command.Parameters.AddWithValue("@Factura", 0);
                                command.Parameters.AddWithValue("@Articulo", Convert.ToString(item.Cells[(int)ColumnasDetalle.Articulo].Value));
                                command.Parameters.AddWithValue("@LineNum", Convert.ToInt32(item.Cells[(int)ColumnasDetalle.LineNum].Value));
                                command.Parameters.AddWithValue("@Comentario", string.Empty);
                                connection.Open();

                               // command.ExecuteNonQuery();
                                SqlDataReader reader = command.ExecuteReader();
                                if (reader.Read())
                                    result += reader.GetString(0);

                                NCPorAclarar_Load(sender, e);
                            }
                        }

                    }
                    if (string.IsNullOrEmpty(result))
                        MessageBox.Show("La factura se actualizo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("El precio real ya se ha actualizado anteriormente, para realizar otra modificación comuniquese con el área de Crédito y Cobranza.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error: no se pudo actualizar la factura, \r\n\r\nCausas: \r\n1.- El Precio Real esta fuera del rango de utilidad permitida."
                                        +"\r\n2.- El Precio Real es mayor al precio SAP.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToInt32(item.Cells[(int)Columnas.Dias].Value) > 15)
                    {
                        item.Cells[(int)Columnas.Factura].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Factura].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
  
    }
}

