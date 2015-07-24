using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClasesSGUV
{
    public static class Propiedades
    {
        public static Icon IconHalcoNET
        {
            get { return Properties.Resources.PJ; }
        }

        public static string conectionSGUV
        {
            get { return @"Data Source=192.168.2.100;Initial Catalog=SGUV;user id = sa; password = SAP-PJ1"; }
        }

        public static string conectionPEJ
        {
            get { return @"Data Source=192.168.2.100;Initial Catalog=SBOCPEJ;user id = sa; password = SAP-PJ1"; }
        }

        public static string conectionPJ
        {
            get { return @"Data Source=192.168.2.100;Initial Catalog=SBO-DistPJ;user id = sa; password = SAP-PJ1 ; MultipleActiveResultSets=True"; }
        }

        public static string conectionPJPrueba
        {
            get { return @"Data Source=192.168.2.100;Initial Catalog=SBO-Pruebas2014;user id = sa; password = SAP-PJ1 ; MultipleActiveResultSets=True"; }
        }

        public static string conectionPJ_Pres
        {
            get { return @"Data Source=192.168.2.227;Initial Catalog=PJ_Pres;user id = sa; password = SAP-PJ1"; }
        }

        public static string conectionRH
        {
            get { return @"Data Source=192.168.2.100;Initial Catalog=PJ_RH;user id = sa; password = SAP-PJ1"; }
        }


        
        public static string Version
        {
            get { return @"v 5.8.2"; }

        }

        public enum RolesHalcoNET
        {
            Administrador = 1,
            GerenteVentas = 2,
            GerenteVentasSucursal = 3,
            Ventas = 4,
            Mostrador = 5,
            GerenteFinanzas = 6,
            GerenteCobranza = 7,
            JefasCobranza = 8,
            Caja = 9,
            GerenteDescargas = 10,
            GerentePagos = 11,
            GerenteGastos = 12,
            GerenteCompras = 13,
            Zulma = 14,
            Kluum = 15,
            PresupuestoReq = 16,
            Flotilla = 17,
            RecursosHumanos = 18,
            Contabilidad = 19
        }

        public static string pathDigitalizacion = @"\\192.168.2.98\Digitalización\";



    }
}
