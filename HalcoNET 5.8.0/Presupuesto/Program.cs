using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Presupuesto
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
           //88 Application.Run (new EstadoResultados());
            Application.Run(new frmPresupuesto(1, "Puebla", "Administrador"));
           //*/
           // Application.Run(new Ventas.ScoreCard.BonoLineasMoradas(1, 0, "Administrador", ""));
            //Application.Run(new ObjetivoMensual());
        }
    }
}
