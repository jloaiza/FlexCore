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

        public static string agregarDineroCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            return CuentaAhorroVistaManager.agregarDinero(pCuentaAhorroVista, pMonto);
        }

        public static string realizarPagoODebitoCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            return CuentaAhorroVistaManager.realizarPagoODebito(pCuentaAhorroVistaOrigen, pMonto, pCuentaAhorroVistaDestino);
        }

        public static string realizarPagoODebitoCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            return CuentaAhorroVistaManager.realizarPagoODebito(pCuentaAhorroVistaOrigen, pMonto, pCuentaAhorroAutomaticoDestino);
        }

        public static string realizarCierreCuentas()
        {
            return CuentaAhorroVistaManager.realizarCierreCuentas();
        }

        public static string agregarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return CuentaAhorroAutomaticoManager.agregarCuentaAhorroAutomatico(pCuentaAhorroAutomatico);
        }

        public static string iniciarAhorroCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return CuentaAhorroAutomaticoManager.iniciarAhorro(pCuentaAhorroAutomatico);
        }

        public static string eliminarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return CuentaAhorroAutomaticoManager.eliminarCuentaAhorroAutomatico(pCuentaAhorroAutomatico);
        }

        public static string modificarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return CuentaAhorroAutomaticoManager.modificarCuentaAhorroAutomatico(pCuentaAhorroAutomatico);
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoNumeroCuenta(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return CuentaAhorroAutomaticoManager.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCedula(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return CuentaAhorroAutomaticoManager.obtenerCuentaAhorroAutomaticoCedula(pCuentaAhorroAutomatico);
        }

        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoNombre(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return CuentaAhorroAutomaticoManager.obtenerCuentaAhorroAutomaticoNombre(pCuentaAhorroAutomatico);
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCIF(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return CuentaAhorroAutomaticoManager.obtenerCuentaAhorroAutomaticoCIF(pCuentaAhorroAutomatico);
        }

        public static string realizarPagoODebitoCuentoAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            return CuentaAhorroAutomaticoManager.realizarPagoODebito(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroAutomaticoDestino);
        }

        public static string realizarPagoODebitoCuentoAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            return CuentaAhorroAutomaticoManager.realizarPagoODebito(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroVistaDestino);
        }
    }
}
