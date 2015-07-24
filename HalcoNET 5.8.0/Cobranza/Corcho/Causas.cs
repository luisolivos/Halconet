using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.Corcho
{
    public partial class Causas : Form
    {
        #region PARAMETROS

        DataTable table = new DataTable();
        int DocEntry;
        int DocNum;
        string CondicionPago;
        string Tipo;
        string Cliente;
        string path;
        Boolean isCorcho;
        string Banco= string.Empty;

        #endregion

        public Causas(int _docEntry, int _docNum, bool _complete, string condPago, string _tipo, string _cliente)
        {
            InitializeComponent();

            DocEntry = _docEntry;
            DocNum = _docNum;
            isCorcho = _complete;
            if (_complete)
            {
                btnDeshacer.Visible = false;
                lblTitulo.Visible = true;
                txtComentarios.Visible = true;
                cbCancelada.Visible = true;
            }
            else
            {
                btnDeshacer.Visible = true;
                lblTitulo.Visible = true;
                txtComentarios.Visible = true;
                txtComentarios.ReadOnly = true;
                cbCancelada.Visible = false;
            }
            CondicionPago = condPago;
            Tipo = _tipo;
            Cliente = _cliente;
        }

        #region METODOS

        public string GetEstatus()
        {
            string _estatus = string.Empty;

            if (CondicionPago == "Crédito")
            {
                if (cbOriginal.Checked && cbBlanca.Checked)
                    _estatus = "Completa";
                else
                    _estatus = "Incompleta";
            }
            if (CondicionPago == "Contado")
            {
                if (cbBlanca.Checked & (cbEfectivo.Checked | cbTransferencia.Checked | cbCheque.Checked | cbTCTD.Checked))
                    _estatus = "Completa";
                else
                    _estatus = "Incompleta";
            }
            if (CondicionPago == "Cheque protegido")
            {
                if (cbBlanca.Checked & cbCheque.Checked)
                    _estatus = "Completa";
                else
                    _estatus = "Incompleta";
            }
            if (CondicionPago == "Cheque posfechado")
            {
                if (cbOriginal.Checked & cbBlanca.Checked & cbCheque.Checked)
                    _estatus = "Completa";
                else
                    _estatus = "Incompleta";
            }

            return _estatus;
        }

        #endregion

        #region EVENTOS

        private void Causas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                if (CondicionPago == "Crédito")
                {
                    cbOriginal.Enabled = true;
                    cbBlanca.Enabled = true;

                    cbEfectivo.Enabled = true;
                    cbTransferencia.Enabled = true;
                    cbCheque.Enabled = true;
                }
                if (CondicionPago == "Contado")
                {
                    cbOriginal.Enabled = false;
                    cbBlanca.Enabled = true;

                    cbEfectivo.Enabled = true;
                    cbTransferencia.Enabled = true;
                    cbCheque.Enabled = true;
                }
                if (CondicionPago == "Cheque protegido")
                {
                    cbOriginal.Enabled = false;
                    cbBlanca.Enabled = true;

                    cbEfectivo.Enabled = false;
                    cbTransferencia.Enabled = false;
                    cbCheque.Enabled = true;
                }
                if (CondicionPago == "Cheque posfechado")
                {
                    cbOriginal.Enabled = true;
                    cbBlanca.Enabled = true;

                    cbEfectivo.Enabled = false;
                    cbTransferencia.Enabled = false;
                    cbCheque.Enabled = true;
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "sp_Corcho";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        command.Parameters.AddWithValue("@DocEntry", 0);
                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Responsable", string.Empty);
                        command.Parameters.AddWithValue("@chkb1", false);
                        command.Parameters.AddWithValue("@chkb2", false);
                        command.Parameters.AddWithValue("@chkb3", false);
                        command.Parameters.AddWithValue("@chkb4", false);
                        command.Parameters.AddWithValue("@chkb5", false);
                        command.Parameters.AddWithValue("@chkb6", false);
                        command.Parameters.AddWithValue("@chkb7", false);
                        command.Parameters.AddWithValue("@chkb8", false);
                        command.Parameters.AddWithValue("@chkb9", false);
                        command.Parameters.AddWithValue("@chkb10", false);
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Completar", string.Empty);
                        command.Parameters.AddWithValue("@Tipo", Tipo);
                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Descuentos", string.Empty);
                        command.Parameters.AddWithValue("@TC_TD", string.Empty);
                        command.Parameters.AddWithValue("@Recibido", decimal.Zero);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;
                        da.Fill(table);
                        int x = 15;
                        int y = 0;
                        foreach (DataRow item in table.Rows)
                        {
                            CheckBox chb = new CheckBox();
                            chb.Name = item.Field<string>("Nombre");
                            chb.Text = item.Field<string>("Descripcion");
                            chb.Visible = item.Field<bool>("Visible");
                            chb.Location = new Point(x, y);
                            chb.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                            chb.Width = panel1.Width - 20;
                            chb.Font = new System.Drawing.Font("Calibri", 9.75f, FontStyle.Regular);
                            panel1.Controls.Add(chb);

                            y = y + chb.Height + 3;
                        }
                    }
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "sp_Corcho";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        command.Parameters.AddWithValue("@DocEntry", DocEntry);
                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Responsable", string.Empty);
                        command.Parameters.AddWithValue("@chkb1", false);
                        command.Parameters.AddWithValue("@chkb2", false);
                        command.Parameters.AddWithValue("@chkb3", false);
                        command.Parameters.AddWithValue("@chkb4", false);
                        command.Parameters.AddWithValue("@chkb5", false);
                        command.Parameters.AddWithValue("@chkb6", false);
                        command.Parameters.AddWithValue("@chkb7", false);
                        command.Parameters.AddWithValue("@chkb8", false);
                        command.Parameters.AddWithValue("@chkb9", false);
                        command.Parameters.AddWithValue("@chkb10", false);
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Completar", string.Empty);
                        command.Parameters.AddWithValue("@Tipo", Tipo);
                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Descuentos", string.Empty);
                        command.Parameters.AddWithValue("@TC_TD", string.Empty);
                        command.Parameters.AddWithValue("@Recibido", decimal.Zero);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;
                        DataTable resp = new DataTable();

                        da.Fill(resp);

                        foreach (DataRow item in resp.Rows)
                        {
                            foreach (Control cb in panel1.Controls)
                            {
                                if (cb is CheckBox)
                                {
                                    if (item.Field<string>("U_Completar").Equals("0,0,0,0,0,0"))
                                    {
                                        cbCompleto.Checked = true;
                                    }
                                    else
                                    {
                                        cbCompleto.Checked = false;
                                    }

                                    if (cb.Name == "ck1")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk1");
                                    if (cb.Name == "ck2")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk2");
                                    if (cb.Name == "ck3")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk3");
                                    if (cb.Name == "ck4")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk4");
                                    if (cb.Name == "ck5")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk5");
                                    if (cb.Name == "ck6")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk6");
                                    if (cb.Name == "ck7")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk7");
                                    if (cb.Name == "ck8")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk8");
                                    if (cb.Name == "ck9")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk9");
                                    if (cb.Name == "ck10")
                                        (cb as CheckBox).Checked = item.Field<bool>("U_Chk10");
                                }
                            }
                            cbOtros.Checked = item.Field<bool>("U_Chk11");
                            txtEspecifique.Visible = cbOtros.Checked;

                            string[] docs = item.Field<string>("U_Completar").Split(new char[] { ',' });

                            cbOriginal.Checked = docs[0] == "1" ? true : false;
                            cbBlanca.Checked = docs[1] == "1" ? true : false;
                            cbCheque.Checked = docs[2] == "1" ? true : false;
                            cbEfectivo.Checked = docs[3] == "1" ? true : false;
                            cbTransferencia.Checked = docs[4] == "1" ? true : false;
                            cbTCTD.Checked = docs[5] == "1" ? true : false;

                            txtComentarios.Text = item.Field<string>("U_Responsable");

                            txtEspecifique.Text = item.Field<string>("U_Commts");
                            txtDescuentos.Text = item.Field<string>("U_Dscuentos");
                        }
                    }
                }

                path = System.Configuration.ConfigurationSettings.AppSettings["rutaDocumentos"].ToString() +Cliente;

                string[] files = System.IO.Directory.GetFiles(path, DocEntry + "-" + DocNum + ".pdf");
                
                lvFiles.Items.Clear();
                foreach (string item in files)
                {
                    lvFiles.Items.Add(item, 1);
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
                string _estatus = this.GetEstatus();
                //int faltantes = 0;
                string docFaltante = string.Empty;

                if (cbOriginal.Checked)
                {
                    //faltantes++;
                    docFaltante += "1,";
                }
                else
                    docFaltante += "0,";
                if (cbBlanca.Checked)
                {
                    //faltantes++;
                    docFaltante += "1,";
                }
                else
                    docFaltante += "0,";
                if (cbCheque.Checked)
                {
                    //faltantes++;
                    docFaltante += "1,";
                }
                else
                    docFaltante += "0,";
                if (cbEfectivo.Checked)
                {
                    //faltantes++;
                    docFaltante += "1,";
                }
                else
                    docFaltante += "0,";
                if (cbTransferencia.Checked)
                {
                    //faltantes++;
                    docFaltante += "1,";
                }
                else
                    docFaltante += "0,";
                if (cbTCTD.Checked)
                {
                    //faltantes++;
                    docFaltante += "1";
                }
                else
                    docFaltante += "0";


                bool[] Chks = new bool[10];
                int index = 0;
                int causas = 0;

                foreach (Control item in panel1.Controls)
                {
                    if (item is CheckBox && item.Name != "cbCancelada")
                    {
                        Chks[index] = (item as CheckBox).Checked;
                        if (_estatus == "Completa")
                            Chks[index] = false;
                        index++;

                        if ((item as CheckBox).Checked)
                            causas++;
                    }
                }

                causas += cbOtros.Checked ? 1 : 0;

                if (CondicionPago == "Crédito" ) ///|| CondicionPago == "Cheque posfechado"
                {
                    if (cbBlanca.Checked && (cbEfectivo.Checked || cbCheque.Checked || cbTransferencia.Checked || cbTCTD.Checked))
                        _estatus = "Completa";
                }

                if (Chks[9]) _estatus = "Refacturación";
                if (cbCancelada.Checked) {_estatus = "Cancelada"; docFaltante = "0,0,0,0,0,0";} 

                //if (Banco == null) { Banco = string.Empty; cbTCTD.Checked = false; } 
                //else { cbTCTD.Checked = true; }
                
                if (!cbTCTD.Checked) Banco = string.Empty;

                if (_estatus == "Completa" || (_estatus != "Completa" && causas != 0) || _estatus == "Refacturación" || _estatus == "Cancelada")
                {
                    if (isCorcho && !string.IsNullOrEmpty(txtComentarios.Text) || !isCorcho)
                    {
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand())
                            {
                                if (lvFiles.Items.Count == 0 && _estatus == "Completa" && CondicionPago != "Contado")
                                    _estatus = "Falta escanear";

                                command.CommandText = "sp_Corcho";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Connection = connection;

                                command.Parameters.AddWithValue("@TipoConsulta", 3);
                                command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                                command.Parameters.AddWithValue("@DocEntry", DocEntry);
                                command.Parameters.AddWithValue("@DocNum", DocNum);
                                command.Parameters.AddWithValue("@Responsable", txtComentarios.Text);
                                command.Parameters.AddWithValue("@chkb1", Chks[0]);
                                command.Parameters.AddWithValue("@chkb2", Chks[1]);
                                command.Parameters.AddWithValue("@chkb3", Chks[2]);
                                command.Parameters.AddWithValue("@chkb4", Chks[3]);
                                command.Parameters.AddWithValue("@chkb5", Chks[4]);
                                command.Parameters.AddWithValue("@chkb6", Chks[5]);
                                command.Parameters.AddWithValue("@chkb7", Chks[6]);
                                command.Parameters.AddWithValue("@chkb8", Chks[7]);
                                command.Parameters.AddWithValue("@chkb9", Chks[8]);
                                command.Parameters.AddWithValue("@chkb10", Chks[9]);
                                command.Parameters.AddWithValue("@chkb11", cbOtros.Checked);
                                command.Parameters.AddWithValue("@Commnts", txtEspecifique.Text);
                                command.Parameters.AddWithValue("@Sucursales", string.Empty);
                                command.Parameters.AddWithValue("@Completar", docFaltante);
                                command.Parameters.AddWithValue("@Tipo", Tipo);
                                command.Parameters.AddWithValue("@Estatus", _estatus);
                                command.Parameters.AddWithValue("@Descuentos", txtDescuentos.Text);
                                command.Parameters.AddWithValue("@TC_TD", Banco);
                                command.Parameters.AddWithValue("@Recibido", decimal.Zero);

                                connection.Open();

                                int row = command.ExecuteNonQuery();

                                if (row > 0)
                                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El campo [Comentarios] no puede estar vacio.\r\nEspecifique el motivo por el cual se quita la factura del corcho.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar al menos una causa y/o Forma de pago.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult resp = MessageBox.Show("¿Desea eliminar la validación de la factura " + DocNum + "?", "HalcoNET", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (resp == DialogResult.OK)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.CommandText = "sp_Corcho";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Connection = connection;

                            command.Parameters.AddWithValue("@TipoConsulta", 3);
                            command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                            command.Parameters.AddWithValue("@DocEntry", DocEntry);
                            command.Parameters.AddWithValue("@DocNum", DocNum);
                            command.Parameters.AddWithValue("@Responsable", txtComentarios.Text);
                            command.Parameters.AddWithValue("@chkb1", false);
                            command.Parameters.AddWithValue("@chkb2", false);
                            command.Parameters.AddWithValue("@chkb3", false);
                            command.Parameters.AddWithValue("@chkb4", false);
                            command.Parameters.AddWithValue("@chkb5", false);
                            command.Parameters.AddWithValue("@chkb6", false);
                            command.Parameters.AddWithValue("@chkb7", false);
                            command.Parameters.AddWithValue("@chkb8", false);
                            command.Parameters.AddWithValue("@chkb9", false);
                            command.Parameters.AddWithValue("@chkb10", false);
                            command.Parameters.AddWithValue("@Sucursales", string.Empty);
                            command.Parameters.AddWithValue("@Completar", "0,0,0,0,0,0");
                            command.Parameters.AddWithValue("@Tipo", Tipo);
                            command.Parameters.AddWithValue("@Estatus", "N");
                            command.Parameters.AddWithValue("@Descuentos", string.Empty);
                            command.Parameters.AddWithValue("@TC_TD", string.Empty);
                            command.Parameters.AddWithValue("@Recibido", decimal.Zero);

                            connection.Open();

                            int row = command.ExecuteNonQuery();

                            if (row > 0)
                                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                ClasesSGUV.Scanner device = new ClasesSGUV.Scanner();

                List<System.Drawing.Image> image = device.Scan();
                ClasesSGUV.ConvertToPDF pdf = new ClasesSGUV.ConvertToPDF();

                if (device.ExistScanner)
                {
                    System.IO.Directory.CreateDirectory(path);

                    pdf.convertPDF(path + "\\" + DocEntry + "-" + DocNum + ".pdf", image);


                }
                else
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.FileName = "";
                    ofd.Title = "No se detectaron dispositivos, seleccione el archivo.";
                    string filePath = "";
                    if (DialogResult.OK == ofd.ShowDialog(this))
                    {
                        filePath = ofd.FileName;

                        System.IO.Directory.CreateDirectory(path);



                        System.IO.File.Copy(filePath, path + "\\" + DocEntry + "-" + DocNum + ".pdf", true);
                    }
                }


                string[] files = System.IO.Directory.GetFiles(path, DocEntry + "-" + DocNum + ".pdf");
                lvFiles.Items.Clear();
                foreach (string item in files)
                {
                    lvFiles.Items.Add(item, 1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void lvFiles_Click(object sender, EventArgs e)
        {
            ListViewItem item = (sender as ListView).SelectedItems[0];

            System.Diagnostics.Process.Start(item.Text);
        }
       
        private void cbTCTD_Click(object sender, EventArgs e)
        {
            TCTD dialogTCTD = new TCTD();
            DialogResult result  = dialogTCTD.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Banco = dialogTCTD.TC_TC;
            }
        }
        #endregion

        private void cbOtros_Click(object sender, EventArgs e)
        {
            txtEspecifique.Visible = true;
            label3.Visible = true;

            txtEspecifique.ReadOnly = !cbOtros.Checked;
            if (!cbOtros.Checked)
                txtEspecifique.Clear();
        }

    }
}
