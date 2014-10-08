using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloCuentas.Generales
{
    internal static class Tiempo
    {
        private static decimal _decimalPrimario = 1;

        public static decimal segundosAMeses(int pSegundos)
        {
            return pSegundos * (_decimalPrimario / 60) * (_decimalPrimario / 60) * (_decimalPrimario / 24) * (_decimalPrimario / 30);
        }

        public static decimal minutosAMeses(int pMinutos)
        {
            return pMinutos * (_decimalPrimario / 60) * (_decimalPrimario / 24) * (_decimalPrimario / 30);
        }

        public static decimal horasAMeses(int pHoras)
        {
            return pHoras * (_decimalPrimario / 24) * (_decimalPrimario / 30);
        }

        public static decimal diasAMeses(int pDias)
        {
            return pDias * (_decimalPrimario / 30);
        }

        public static DateTime getHoraActual()
        {
            DateTime _test = new DateTime();
            _test = DateTime.Now;
            return _test;
        }
    }
}
