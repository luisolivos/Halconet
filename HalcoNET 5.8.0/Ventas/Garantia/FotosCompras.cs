using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Drawing.Drawing2D;

namespace Ventas.Garantia
{
    public partial class FotosCompras : Form
    {
        string ItemCode;
        int LineNum;
        int DocEntry;
        DataTable Datos = new DataTable();
        Image picture;
        string Name;

        public FotosCompras(string _itemCode, int _lineNum, int _docEntry)
        {
            InitializeComponent();
            ItemCode = _itemCode;
            LineNum = _lineNum;
            DocEntry = _docEntry;
        }


        private void FotosCompras_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_Garantias";
                    command.CommandTimeout = 0;
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 5);
                    command.Parameters.AddWithValue("@ItemCode", ItemCode);
                    command.Parameters.AddWithValue("@CantidadGarantia", 0);
                    command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                    command.Parameters.AddWithValue("@Cndicion", string.Empty);
                    command.Parameters.AddWithValue("@DoceEntry", DocEntry);
                    command.Parameters.AddWithValue("@User", string.Empty);
                    command.Parameters.AddWithValue("@SlpCode", 0);
                    command.Parameters.AddWithValue("@LineNum", LineNum);
                    command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                    command.Parameters.AddWithValue("@Desde", DateTime.Now);
                    command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(Datos);
                    
                    foreach (DataRow item in Datos.Rows)
                    {
                        listView.Items.Add(item.Field<string>("U_ItemCode") + " " + item.Field<Int32>("Code"));

         
                    }
                }
            }
        }

        private void listView_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem item = (sender as ListView).SelectedItems[0];

                var query = (from pic in Datos.AsEnumerable()
                             where pic.Field<string>("U_ItemCode") + " " + pic.Field<Int32>("Code") == item.Text
                             select pic);

                if (query.Count() > 0)
                {
                    DataTable result = query.CopyToDataTable();

                    foreach (DataRow pic in result.Rows)
                    {
                        byte[] imageBuffer = (byte[])pic["U_Fotografia"];
                        Name = pic["U_ItemCode"] + " " + pic.Field<Int32>("Code");

                        System.IO.MemoryStream ms = new System.IO.MemoryStream(imageBuffer);

                        picture = Image.FromStream(ms);
                        pictureBox1.Image = picture;
                    }
                }
            }
            catch (Exception)
            {
            }
           // System.Diagnostics.Process.Start(item.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                   saveFileDialog.FileName = Name;
                DialogResult dialog = saveFileDialog.ShowDialog();

                if (dialog == DialogResult.OK)
                {
                   
                    string path = saveFileDialog.FileName;
                    picture.Save(path , ImageFormat.Jpeg);
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
