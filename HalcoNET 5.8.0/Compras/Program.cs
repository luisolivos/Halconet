using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Compras
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Desabasto());
            //Application.Run(new Transferencias_Compras.Transferencias_Compras());
            //Application.Run(new HistorialVentas());
            //Application.Run(new Reubicaciones());
            //Application.Run(new IdealxLinea());
           // Application.Run(new Remate_devolucion());
            Application.Run(new frmAnalisisCompras());
        }
    }
}
