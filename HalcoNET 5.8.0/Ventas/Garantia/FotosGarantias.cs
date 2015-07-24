using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.Garantia
{
    public partial class FotosGarantias : Form
    {
        string images;

        public string Images
        {
            get { return images; }
            set { images = value; }
        }

        string ItemCode;
        int LineNum;
        int DocEntry;

        public FotosGarantias(string img, string _itemCode, int lineNum, int _docEntry)
        {
            InitializeComponent();
            images = img;

            ItemCode = _itemCode;
            LineNum = lineNum;
            DocEntry = _docEntry;

        }

        public FotosGarantias()
        {
            InitializeComponent();
        }

        private void FotosGarantias_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            if (ItemCode != "")
            {
                string[] files = images.Split('\t');
                foreach (string item in files)
                {
                    if (!string.IsNullOrEmpty(item))
                        listView.Items.Add(item);
                }
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files|*.jpeg;*.jpg;*.png;|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            openFileDialog1.Multiselect = true;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string[] files = openFileDialog1.FileNames;
                foreach (string item in files)
                {
                    if (!string.IsNullOrEmpty(item))
                        listView.Items.Add(item);
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedListViewItemCollection list = listView.SelectedItems;

                foreach (ListViewItem item in list)
                {
                    listView.Items.Remove(item);
                }
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            images = string.Empty;
            foreach (ListViewItem item in listView.Items)
            {
                images += item.Text + "\t";
            }

            //if (ItemCode != "")
            //    images = "From Data Base";

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
