using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace Ventas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Logueo frmLogueo = new Logueo();
                frmLogueo.ShowDialog();

                if (frmLogueo.DialogResult == DialogResult.OK)
                {
                    try
                    {

                        Application.Run(new Principal(frmLogueo.ClaveEntidad, frmLogueo.NombreUsuario, frmLogueo.Contrasenha, frmLogueo.Rol, frmLogueo.Vendedor, frmLogueo.Sucursal.Trim()));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error inesperado: " + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.TargetSite, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {

                    }
                }

                //Application.Run(new Cobranza.GestionCobranza.frmNoLlamadas()); 
                //Application.Run( new Cobranza.ReporteCrystal());

               // ClasesSGUV.Login.Rol = 1;

                //Application.Run(new Desarrollo.frmVentaPerdida());
                //Application.Run(new frmEncuesta());
                //Application.Run(new Compras.frmHistorialABC());
                //Application.Run(new RRHH.Resultados.frmResumen());
                //Application.Run(new Compras.Desarrollo.frmCostoVtaSucursal());
                //Application.Run(new Desarrollo.UtilidadLineas(1,0,"Administrador","PUEBLA"));
                //Application.Run(new Presupuesto.frmPerdidaGanancia());
                //Application.Run(new Compras.Desarrollo.frmCostoVtaSucursal());
                //Application.Run(new Cobranza.frmTemplateNC());
                //Application.Run(new Cobranza.ReportesVarios.frmCreditosDesuso());
                //ClasesSGUV.Login.Sucursal = "PUEBLA";
                //ClasesSGUV.Login.Rol = 1;
                //Application.Run(new Cobranza.Corcho.frmIngresos(1));
                //Application.Run(new Cobranza.frmDepositos());
                //Application.Run(new Compras.Solicitudes.frmSolicitudProducto()); 


                //Application.Run(new Compras.Solicitudes.frmSolicitudProducto());
                //Application.Run(new Cobranza.frmVarios(2));   

            }
            catch (ArgumentOutOfRangeException ex1)
            {
                MessageBox.Show("Error: " + ex1.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
 