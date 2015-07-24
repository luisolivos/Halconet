using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presupuesto.Clases
{
	public class Comun_Presupuesto
	{
        int _tipoConsulta;

        public int TipoConsulta
        {
            get { return _tipoConsulta; }
            set { _tipoConsulta = value; }
        }
        string _cuenta;

        public string Cuenta
        {
            get { return _cuenta; }
            set { _cuenta = value; }
        }
        string _nR;

        public string NR
        {
            get { return _nR; }
            set { _nR = value; }
        }
        string _proyecto;

        public string Proyecto
        {
            get { return _proyecto; }
            set { _proyecto = value; }
        }
        int _mes;

        public int Mes
        {
            get { return _mes; }
            set { _mes = value; }
        }
        int _año;

        public int Año
        {
            get { return _año; }
            set { _año = value; }
        }
        string _cvePres;

        public string CvePres
        {
            get { return _cvePres; }
            set { _cvePres = value; }
        }
        decimal _original;

        public decimal Original
        {
            get { return _original; }
            set { _original = value; }
        }
        decimal _presupuesto;

        public decimal Presupuesto
        {
            get { return _presupuesto; }
            set { _presupuesto = value; }
        }
        DateTime _creacion;

        public DateTime Creacion
        {
            get { return _creacion; }
            set { _creacion = value; }
        }
        DateTime _modificacion;

        public DateTime Modificacion
        {
            get { return _modificacion; }
            set { _modificacion = value; }
        }

        public Comun_Presupuesto()
        {
            _tipoConsulta = 0;
            _cuenta = string.Empty;
            _nR = string.Empty;
            _proyecto = string.Empty;
            _mes = 0;
            _año= 0;
            _cvePres = string.Empty;
            _original = decimal.Zero;
            _presupuesto = decimal.Zero;
            _creacion = DateTime.Now;
            _modificacion = DateTime.Now;
        }
	}
}
