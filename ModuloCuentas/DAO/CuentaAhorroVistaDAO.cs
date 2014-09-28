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
        public static void agregarCuentaAhorroVistaBase(CuentaAhorroVista pCuentaAhorrroVista)
        {
            //Se agrega la cuenta a la base;
        }

        public static void modificarCuentaAhorroVistaBase(CuentaAhorroVista pCuentaAhorroVista)
        {
            //se modifica la cuenta SIN MODIFICAR LOS SALDOS;
        }

        public static void eliminarCuentaAhorroVistaBase(string pNumeroCuenta)
        {
            //se elimina la cuenta;
        }

        public static CuentaAhorroVista obtenerCuentaAhorroVistaNumeroCuenta(string pNumeroCuenta)
        {
            //SE OBTIENE LA CUENTA DADO EL NUMERO DE CUENTA;
            return null;
        }

        public static CuentaAhorroVista obtenerCuentaAhorroVistaCedula(string pCedula)
        {
            //SE OBTIENE LA CUENTA DADA LA CEDULA;
            return null;
        }

        public static List<CuentaAhorroVista> obtenerCuentaAhorroVistaNombre(string pNombre)
        {
            //SE OBTIENEN LAS CUENTAS DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroVista obtenerCuentaAhorroVistaCIF(string pCIF)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static int obtenerCuentaAhorroVistaID(string pNumeroCuenta)
        {
            //SE OBTIENE EL ID DE CUENTA DADO UN NUMERO DE CUENTA;
            return 0;
        }

        public static void agregarDinero(string pNumeroCuenta, decimal pMonto)
        {
             //AGREGO SALDO A LA CUENTA;
        }

        public static void quitarDinero(string pNumeroCuenta, decimal pMonto)
        {
            //QUITA DINERO A LA CUENTA
        }
    }
}
