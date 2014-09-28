using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.Cuentas;
using ModuloCuentas.DTO;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroVistaDAO
    {
        public static void agregarCuentaAhorroVistaBase(CuentaAhorroVista pCuenta)
        {
            //Se agrega la cuenta a la base;
        }

        public static void eliminarCuentaAhorroVistaBase(string pNumeroCuenta)
        {
            //se elimina la cuenta;
        }

        public static void modificarCuentaAhorroVistaBase(CuentaAhorroVista pCuenta)
        {
            //se modifica la cuenta SIN MODIFICAR LOS SALDOS;
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaNumeroCuenta(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            //SE OBTIENE LA CUENTA DADO EL NUMERO DE CUENTA;
            return null;
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaCedula(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            //SE OBTIENE LA CUENTA DADA LA CEDULA;
            return null;
        }

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaNombre(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            //SE OBTIENEN LAS CUENTAS DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaCIF(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static int obtenerCuentaAhorroVistaID(string pNumeroCuenta)
        {
            //SE OBTIENE EL ID DE CUENTA DADO UN NUMERO DE CUENTA;
            return 0;
        }

        public static void agregarDinero(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
             //AGREGO SALDO A LA CUENTA;
        }

        public static void quitarDinero(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
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
