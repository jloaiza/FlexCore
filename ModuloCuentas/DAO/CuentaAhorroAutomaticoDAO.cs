using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.Cuentas;
using ModuloCuentas.DTO;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroAutomaticoDAO
    {
        public static void agregarCuentaAhorroAutomaticoBase(CuentaAhorroAutomatico pCuenta)
        {
            //Se agrega la cuenta a la base;
        }

        public static void eliminarCuentaAhorroAutomaticoBase(string pNumeroCuenta)
        {
            //se elimina la cuenta;
        }

        public static void modificarCuentaAhorroAutomaticoBase(CuentaAhorroAutomatico pCuenta)
        {
            //se modifica la cuenta SIN MODIFICAR LOS SALDOS Y COSAS QUE NO DEBEN SER MODIFICADAS;
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoNumeroCuenta(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            //SE OBTIENE LA CUENTA DADO EL NUMERO DE CUENTA;
            return null;
        }

        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoNombre(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            //SE OBTIENE LA CUENTA DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCedula(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            //SE OBTIENE LA CUENTA DADA LA CEDULA;
            return null;
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCIF(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static void quitarDinero(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, decimal pMonto)
        {
            //QUITA DINERO A LA CUENTA
        }

        public static bool existeCuenta(string pNumeroCuenta)
        {
            //DEVUELLVE TRUE SI YA LA CUENTA EXISTE
            return true;
        }
    }
}
