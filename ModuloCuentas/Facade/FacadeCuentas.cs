using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.DTO;
using ModuloCuentas.Managers;

namespace ModuloCuentas.Facade
{
    public static class FacadeCuentas
    {
        public static string agregarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            return CuentaAhorroVistaManager.agregarCuentaAhorroVista(pCuentaAhorroVista);
        }

        public static string eliminarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            return CuentaAhorroVistaManager.eliminarCuentaAhorroVista(pCuentaAhorroVista);
        }

        public static string modificarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            return CuentaAhorroVistaManager.modificarCuentaAhorroVista(pCuentaAhorroVista);
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaNumeroCuenta(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            return CuentaAhorroVistaManager.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista);
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaCedula(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            return CuentaAhorroVistaManager.obtenerCuentaAhorroVistaCedula(pCuentaAhorroVista);
        }

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaNombre(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            return CuentaAhorroVistaManager.obtenerCuentaAhorroVistaNombre(pCuentaAhorroVista);
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaCIF(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            return CuentaAhorroVistaManager.obtenerCuentaAhorroVistaCIF(pCuentaAhorroVista);
        }

        public static string agregarDinero(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            return CuentaAhorroVistaManager.agregarDinero(pCuentaAhorroVista, pMonto);
        }

        public static string realizarPagoODebito(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            return CuentaAhorroVistaManager.realizarPagoODebito(pCuentaAhorroVista, pMonto);
        }
    }
}
