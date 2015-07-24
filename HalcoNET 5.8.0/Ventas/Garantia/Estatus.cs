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
using System.IO;

namespace Ventas.Garantia
{
    public partial class Estatus : Form
    {
        string _estatus_String;
        string Articulo;
        int DocEntry;
        int LineNum;
        BinaryWriter bw;
        FileStream fs;

        bool _exist = false;

        byte[] bytes;

        string _Filename;
        string _Extension;
        string _File;

        public string Estatus_String
        {
            get { return _estatus_String; }
            set { _estatus_String = value; }
        }

        public Estatus(string status, string _articulo, int _docEntry, int _lineNum)
        {
            InitializeComponent();

            _estatus_String = status;
            Articulo = _articulo;
            DocEntry = _docEntry;
            LineNum = _lineNum;
        }

        public string DecodificarArchivo(string sBase64, string _fileName, string _extension, byte[] bytes1)
        {
            string sFileTemporal = _fileName + _extension;
            bytes = bytes1;

            fs = new FileStream(sFileTemporal, FileMode.Create);

            bw = new BinaryWriter(fs);
         
            try
            {
                bytes = Convert.FromBase64String(sBase64);
                //bw.Write(bytes);
                return sFileTemporal;
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al leer la imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return sFileTemporal = string.Empty;
            }
            finally
            {
                fs.Close();
               // bytes = null;
                bw = null;
                sBase64 = null;
            }
        }

        public string CodificarArchivo(string sNombreArchivo)
        {
            string sBase64 = "";
            // Declaramos fs para tener acceso al archivo residente en la maquina cliente.
            FileStream fs = new FileStream(sNombreArchivo, FileMode.Open);
            // Declaramos un Leector Binario para accesar a los datos del archivo pasarlos a un arreglo de bytes
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = new byte[(int)fs.Length];
            try
            {
                br.Read(bytes, 0, bytes.Length);
                // base64 es la cadena en donde se guarda el arreglo de bytes ya convertido
                sBase64 = Convert.ToBase64String(bytes);
                return sBase64;



            }

            catch
            {
                MessageBox.Show("Ocurri un error al cargar el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return null;
            }
            // Se cierran los archivos para liberar memoria.
            finally
            {
                fs.Close();
                fs = null;
                br = null;
                bytes = null;
            }
        }

        private void Estatus_Load(object sender, EventArgs e)
        {
            try
            {

                switch (_estatus_String)
                {
                    case "ALTA": rbAlta.Checked = true; break;
                    case "PROCESO": rbProceso.Checked = true; break;
                    case "APROBADA": rbAprobada.Checked = true; break;
                    case "RECHAZADA": rbRechazada.Checked = true; break;
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Garantias", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 10);
                        command.Parameters.AddWithValue("@ItemCode", string.Empty);
                        command.Parameters.AddWithValue("@CantidadGarantia", 0);
                        command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                        command.Parameters.AddWithValue("@Cndicion", string.Empty);
                        command.Parameters.AddWithValue("@DoceEntry", DocEntry);
                        command.Parameters.AddWithValue("@User", string.Empty);
                        command.Parameters.AddWithValue("@SlpCode", 0);
                        command.Parameters.AddWithValue("@LineNum", 0);
                        command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                        command.Parameters.AddWithValue("@Desde", DateTime.Now);
                        command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);

                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Vendedores", string.Empty);

                        connection.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        byte[] pepepep = null;
                        
                        if (reader.Read())
                        {
                            _Filename =  reader[0].ToString();
                            _File =  reader[2].ToString();//["U_DictamenFile"];
                            _Extension = reader[1].ToString();
                            txtDetalles.Text = reader[3].ToString();
                            _exist = true;
                        }

                        if (_File != null)
                        {
                            listView.Items.Add(this.DecodificarArchivo(_File, _Filename, _Extension, pepepep));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool _continuar = true;

            if (rbAlta.Checked)
                _estatus_String = "A";
            if (rbProceso.Checked)
                _estatus_String = "P";
            if (rbAprobada.Checked)
                _estatus_String = "B";
            if (rbRechazada.Checked)
                _estatus_String = "R";

            if ((_estatus_String == "B" || _estatus_String == "R") && (_File == "" || _File == null))
            {
                _continuar = false;
                MessageBox.Show("Debe adjuntar el Dictamen para poder cambiar a estatus 'Rechazado' o 'Aprobado'");
            }

            if (_exist && _continuar)
            {
                if (System.Windows.Forms.DialogResult.Yes == MessageBox.Show("Ya se adjunto un dictamen para esta Garantía, ¿Desea reemplazalo por uno nuevo? ", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            try
                            {
                                command.Connection = connection;
                                command.CommandText = "Update PJ_TblGarantias Set U_DictamenName = @Name, U_DictamenExt = @Ext, U_DictamenFile = @File Where DocEntry =  @DocEntry";
                                command.Parameters.AddWithValue("@Ext", _Extension);
                                command.Parameters.AddWithValue("@Name", _Filename);
                                command.Parameters.AddWithValue("@File", _File);
                                command.Parameters.AddWithValue("@DocEntry", DocEntry);

                                connection.Open();

                                command.ExecuteNonQuery();
                                //_cerar = true;
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
            }
            else if (!_exist && _continuar && (_estatus_String == "B" | _estatus_String == "R"))
            {
               
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        try
                        {
                            command.Connection = connection;
                            command.CommandText = "Update PJ_TblGarantias Set U_DictamenName = @Name, U_DictamenExt = @Ext, U_DictamenFile = @File Where DocEntry =  @DocEntry";
                            command.Parameters.AddWithValue("@Ext", _Extension);
                            command.Parameters.AddWithValue("@Name", _Filename);
                            command.Parameters.AddWithValue("@File", _File);
                            command.Parameters.AddWithValue("@DocEntry", DocEntry);

                            connection.Open();

                            command.ExecuteNonQuery();
                            //_cerar = true;
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }


            if (_continuar)
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_Garantias";
                        command.CommandTimeout = 0;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 7);
                        command.Parameters.AddWithValue("@ItemCode", Articulo);
                        command.Parameters.AddWithValue("@CantidadGarantia", 0);
                        command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                        command.Parameters.AddWithValue("@Cndicion", txtDetalles.Text);
                        command.Parameters.AddWithValue("@DoceEntry", DocEntry);
                        command.Parameters.AddWithValue("@User", string.Empty);
                        command.Parameters.AddWithValue("@SlpCode", 0);
                        command.Parameters.AddWithValue("@LineNum", LineNum);
                        command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                        command.Parameters.AddWithValue("@Desde", DateTime.Now);
                        command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                        command.Parameters.AddWithValue("@Cliente", _estatus_String);

                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@Vendedores", string.Empty);

                        connection.Open();
                        int pp = command.ExecuteNonQuery();
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Selecccionar Archivo";

            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                _Filename = dlg.FileName;
                listView.Items.Add(_Filename);

                _File = this.CodificarArchivo(_Filename);
                _Extension = System.IO.Path.GetExtension(_Filename);
                _Filename = System.IO.Path.GetFileNameWithoutExtension(_Filename);
                 
            }
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            string name = listView.SelectedItems[0].Text;

           


            if (!string.IsNullOrEmpty(name))
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                DialogResult reuslt = dialog.ShowDialog();

                if (reuslt == DialogResult.OK)
                {
                    try
                    {
                        fs = new FileStream(dialog.SelectedPath + "\\" + name, FileMode.Create);

                        bw = new BinaryWriter(fs);

                        bw.Write(bytes);
                    }
                    catch
                    {
                        MessageBox.Show("Ocurrió un error al leer la imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    finally
                    {
                        fs.Close();
                      //  bytes = null;
                        bw = null;
                    }
                }
            }
        }
    }
}
