using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Cobranza
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
            //Application.Run(new Cobranza1(1, 0, "Administrador", "Puebla"));
           // Application.Run(new PrincipalCobranza());
           //Application.Run(new AntiguiedadSaldos(1, 0, "Administrador", "Puebla"));
            Application.Run(new ScocreCardGlobal(1, 0, "Administrador", "Puebla"));
           // Application.Run(new AntiguedadSaldos.SeguimientoCompromisos("Apizaco"));

        }
    }
}
