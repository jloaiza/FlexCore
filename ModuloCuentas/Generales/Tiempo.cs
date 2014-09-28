using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloCuentas.Generales
{
    internal static class Tiempo
    {
        public static decimal segundosAMeses(int pSegundos)
        {
            return pSegundos * (1 / 60) * (1 / 60) * (1 / 24) * (1 / 30);
        }

        public static decimal minutosAMeses(int pMinutos)
        {
            return pMinutos * (1 / 60) * (1 / 24) * (1 / 30);
        }

        public static decimal horasAMeses(int pHoras)
        {
            return pHoras * (1 / 24) * (1 / 30);
        }

        public static decimal diasAMeses(int pDias)
        {
            return pDias * (1 / 30);
        }

        public static DateTime getHoraActual()
        {
            DateTime _test = new DateTime();
            return _test;
        }
    }
}
