using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.Pagos
{
    public partial class frmMovimientosNoI : Form
    {
        public frmMovimientosNoI()
        {
            InitializeComponent();
        }

        private void MovimientosNoI_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }
    }
}
