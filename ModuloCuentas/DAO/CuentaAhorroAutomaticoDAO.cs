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
        public static void agregarCuentaAhorroAutomaticoBase(CuentaAhorroAutomatico pCuentaAhorroAutomatico)
        {
            //Se agrega la cuenta a la base;
        }

        public static void modificarCuentaAhorroAutomaticoBase(CuentaAhorroAutomatico pCuentaAhorroAutomatico)
        {
            //se modifica la cuenta SIN MODIFICAR LOS SALDOS Y COSAS QUE NO DEBEN SER MODIFICADAS;
        }

        public static void eliminarCuentaAhorroAutomaticoBase(string pNumeroCuenta)
        {
            //se elimina la cuenta;
        }

        public static CuentaAhorroAutomatico obtenerCuentaAhorroAutomaticoNumeroCuenta(string pNumeroCuenta)
        {
            //SE OBTIENE LA CUENTA DADO EL NUMERO DE CUENTA;
            return null;
        }

        public static List<CuentaAhorroAutomatico> obtenerCuentaAhorroAutomaticoNombre(string pNombre)
        {
            //SE OBTIENE LA CUENTA DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroAutomatico obtenerCuentaAhorroAutomaticoCedula(string pCedula)
        {
            //SE OBTIENE LA CUENTA DADA LA CEDULA;
            return null;
        }

        public static CuentaAhorroAutomatico obtenerCuentaAhorroAutomaticoCIF(string pCIF)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static void quitarDinero(string pNumeroCuenta, decimal pMonto)
        {
            //QUITA DINERO A LA CUENTA
        }

        public static void agregarDinero(string pNumeroCuenta, decimal pMonto)
        {
            //se agrega dinero a la cuenta;
        }

        public static void modificarEstado(string pNumeroCuenta, bool pEstado)
        {
            //modifica el estado de una cuenta;
        }
    }
}
