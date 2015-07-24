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


using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Cobranza
{
    public partial class frmDepositos : Form
    {
        DataTable Datos = new DataTable();
        Clases.Logs log;

        public frmDepositos()
        {
            InitializeComponent();
        }

        private void nuevoToolStripButton_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Cliente", typeof(string));
            tabla.Columns.Add("Nombre", typeof(string));
            tabla.Columns.Add("Referencia", typeof(string));
            tabla.Columns.Add("Monto", typeof(decimal));

            dataGridView1.DataSource = tabla;

            toolStatus.Text = "Total: " + decimal.Zero.ToString("C2");
        }

        private void frmDepositos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

            DataTable tabla = new DataTable();
            tabla.Columns.Add("Cliente", typeof(string));
            tabla.Columns.Add("Nombre", typeof(string));
            tabla.Columns.Add("Referencia", typeof(string));
            tabla.Columns.Add("Monto", typeof(decimal));

            dataGridView1.DataSource = tabla;

            dataGridView1.Columns[3].ValueType = typeof(decimal);

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("select CardCode, CardName, ISNULL(U_RefPago, '') U_RefPago from [SBO-DistPJ].dbo.OCRD where CardType = 'C' AND frozenFor = 'N'", connection))
                {
                    command.CommandTimeout = 0;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(Datos);
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((sender as DataGridView).CurrentCell.ColumnIndex == 0)
            {
                var source = new AutoCompleteStringCollection();


                string[] stringArray = Array.ConvertAll<DataRow, String>(Datos.Select(), delegate(DataRow row) { return (String)row["CardCode"]; });

                source.AddRange(stringArray);

                TextBox prodCode = e.Control as TextBox;
                if (prodCode != null)
                {
                    prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    prodCode.AutoCompleteCustomSource = source;
                    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((sender as DataGridView).CurrentCell.ColumnIndex == 0)
                {
                    string item = (sender as DataGridView).CurrentCell.Value.ToString();

                    var qry = (from itemvar in Datos.AsEnumerable()
                               where itemvar.Field<string>("CardCode").ToLower().Equals(item.ToLower())
                               select itemvar).CopyToDataTable();

                    if (qry.Rows.Count > 0)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = (sender as DataGridView).CurrentRow;

                        row.Cells[1].Value = qry.Rows[0].Field<string>("CardName");
                        row.Cells[2].Value = qry.Rows[0].Field<string>("U_RefPago");
                       


                    }
                }
                //if ((sender as DataGridView).CurrentCell.ColumnIndex == 3)
                //{//Dim resul As Double = dataGridView1.Rows.Cast(Of DataGridViewRow)().Sum(Function(x) Convert.ToDouble(x.Cells("Total").Value))
                    toolStatus.Text = "Total: " + ((sender as DataGridView).Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells[3].Value))).ToString("C2");
                //}
            }
            catch (Exception) { }
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[0].Value = string.Empty;
                e.Row.Cells[1].Value = string.Empty;
                e.Row.Cells[2].Value = string.Empty;
                e.Row.Cells[3].Value = decimal.Zero;
            }
            catch (Exception)
            {

            }
        }

        private void imprimirToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cobranza.Clases.CrearPDF pdf = new Clases.CrearPDF();
                pdf.ToPDFDepositos(dataGridView1.DataSource as DataTable, string.Empty);

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
            catch (Exception)
            {
                MessageBox.Show("Error", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void frmDepositos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void frmDepositos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }
    }
}
