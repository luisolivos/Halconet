using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RRHH.Resultados
{
    public partial class frmResumenPFD : Form
    {
        AcroPDFLib.AcroPDF pdf;

        

        public frmResumenPFD()
        {
           
            InitializeComponent();

            
        }

        private void frmResumen_Load(object sender, EventArgs e)
        {
            axAcroPDF1.LoadFile(@"c:\pp\i.pdf");
        }
    }
}
