using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.cuentas.Generales
{
    public  static class Transformaciones
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

        public static decimal convertirDinero(decimal pMonto, int pMonedaOrigen, int pMonedaDestino)
        {
            if(pMonedaOrigen == Constantes.COLONES && pMonedaDestino == Constantes.DOLARES)
            {
                //SE REGISTRA CAMBIO DE DOLARES A COLONES.
                return dolaresAColores(pMonto);
            }
            else if(pMonedaOrigen == Constantes.DOLARES && pMonedaDestino == Constantes.COLONES)
            {
                //SE REGISTRA CAMBIO DE COLONES A DOLARES
                return colonesADolares(pMonto);
            }
            else
            {
                return pMonto;
            }
        }

        private static decimal dolaresAColores(decimal pMonto)
        {
            return pMonto * Constantes.UNDOLARENCOLONES;
        }

        private static decimal colonesADolares(decimal pMonto)
        {
            return pMonto / Constantes.UNDOLARENCOLONES;
        }
    }
}
