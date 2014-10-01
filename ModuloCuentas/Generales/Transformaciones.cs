using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloCuentas.Generales
{
    internal static class Transformaciones
    {
        public static int boolToInt(bool pEstado)
        {
            if(pEstado == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static bool intToBool(int pEstado)
        {
            if (pEstado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
