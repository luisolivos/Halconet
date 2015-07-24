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
using System.Drawing.Printing;

using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Cobranza.GestionCobranza
{
    public partial class AlertasCompromisos : Form
    {
        private string Jefas;
        private string Usuario;
        private Clases.Logs log;

        private enum Columnas
        {
            Folio, 
            Tipo,
            CLiente,
            Nombre,
            FechaVto,
            Compometido,
            Pagos,
            Efectividad,
            Estatus
        }

        public AlertasCompromisos(string jefa, string _title, string usuario)
        {
            InitializeComponent();
            Jefas = jefa; 
            lblTitle.Text = _title;
            Usuario = usuario;
            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }

        private void Formato()
        {
            gridFacturas.Columns[(int)Columnas.Folio].Width = 85;
            gridFacturas.Columns[(int)Columnas.Tipo].Width = 100;
            gridFacturas.Columns[(int)Columnas.CLiente].Width = 80;
            gridFacturas.Columns[(int)Columnas.Nombre].Width = 180;
            gridFacturas.Columns[(int)Columnas.FechaVto].Width = 90;
            gridFacturas.Columns[(int)Columnas.Compometido].Width = 90;
            gridFacturas.Columns[(int)Columnas.Pagos].Width = 90;
            gridFacturas.Columns[(int)Columnas.Efectividad].Width = 80;
            gridFacturas.Columns[(int)Columnas.Estatus].Width = 80;

            gridFacturas.Columns[(int)Columnas.Compometido].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)Columnas.Pagos].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)Columnas.Efectividad].DefaultCellStyle.Format = "P2";

            gridFacturas.Columns[(int)Columnas.Compometido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)Columnas.Pagos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)Columnas.Efectividad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
   
        }

        private void CargarCompromisos(string _jefas)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 7);
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                        command.Parameters.AddWithValue("@Monto", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);
                        command.Parameters.AddWithValue("@Factura", 0);
                        command.Parameters.AddWithValue("@Otro", _jefas);
                        command.Parameters.AddWithValue("@NumCompromiso", string.Empty);
                        command.Parameters.AddWithValue("@Comprometido", 0);
                        command.Parameters.AddWithValue("@Tipo", "");

                        SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                        parameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(table);

                        gridFacturas.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: "+ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AlertasCompromisos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            try
            {
                string[] files = System.IO.Directory.GetFiles("PDF\\", "*.pdf");

                foreach (string item in files)
                {
                    System.IO.File.Delete(item);
                }
            }
            catch (Exception)
            {
            }

            this.CargarCompromisos(Jefas);
            this.Formato();
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow item in gridFacturas.Rows)
            {
                if (Convert.ToString(item.Cells[(int)Columnas.Estatus].Value) == "No cumplido")
                {
                    item.Cells[(int)Columnas.Estatus].Style.BackColor = Color.Red;
                    item.Cells[(int)Columnas.Estatus].Style.ForeColor = Color.White;
                }
                else
                {
                    item.Cells[(int)Columnas.Estatus].Style.BackColor = Color.White;
                    item.Cells[(int)Columnas.Estatus].Style.ForeColor = Color.Black;
                }
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Clases.CrearPDF pdf = new Clases.CrearPDF();

                if (pdf.ToPDF(gridFacturas, Jefas))
                {
                    string nombre = pdf.Nombre;
                    PdfReader reader = new PdfReader(nombre);
                    string nombreCopia = Path.GetTempFileName() + ".pdf";
                    PdfStamper stamper = new PdfStamper(reader, new FileStream(nombreCopia, FileMode.Create));
                    AcroFields fields = stamper.AcroFields;
                    stamper.JavaScript = "this.print(true);\r";
                    stamper.FormFlattening = true;
                    stamper.Close();
                    reader.Close();

                    System.Diagnostics.Process.Start(nombreCopia);
                }
                else
                {
                    MessageBox.Show("Error al generar el archivo. \r\n", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el archivo. \r\n" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
            }

        }

        private void AlertasCompromisos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles("PDF\\", "*.pdf");

                foreach (string item in files)
                {
                    System.IO.File.Delete(item);
                }

                log.Fin();
            }
            catch (Exception)
            {
            }
        }

        private void AlertasCompromisos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }
    }
}
