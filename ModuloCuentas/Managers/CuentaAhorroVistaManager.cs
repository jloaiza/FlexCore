using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.DTO;
using ModuloCuentas.Cuentas;
using ModuloCuentas.DAO;

namespace ModuloCuentas.Managers
{
    internal static class CuentaAhorroVistaManager
    {
        public static string agregarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                string _numeroCuenta = GeneradorCuentas.generarCuenta(Constantes.AHORROVISTA, pCuentaAhorroVista.getTipoMoneda());
                CuentaAhorroVista _cuentaAhorroVista = new CuentaAhorroVista(_numeroCuenta, pCuentaAhorroVista.getDescripcion(), 0, pCuentaAhorroVista.getEstado(),
                    pCuentaAhorroVista.getTipoMoneda(), 0);
                CuentaAhorroVistaDAO.agregarCuentaAhorroVistaBase(_cuentaAhorroVista);
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string eliminarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                CuentaAhorroVistaDAO.eliminarCuentaAhorroVistaBase(pCuentaAhorroVista.getNumeroCuenta());
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string modificarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                CuentaAhorroVista _cuentaAhorroVista = new CuentaAhorroVista(pCuentaAhorroVista.getNumeroCuenta(), pCuentaAhorroVista.getDescripcion(),
                0, pCuentaAhorroVista.getEstado(), pCuentaAhorroVista.getTipoMoneda(), 0);
                CuentaAhorroVistaDAO.modificarCuentaAhorroVistaBase(_cuentaAhorroVista);
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaNumeroCuenta(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                return CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista);
            }
            catch
            {
                return null;
            }
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaCedula(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                return CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaCedula(pCuentaAhorroVista);
            }
            catch
            {
                return null;
            }
        }

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaNombre(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                return CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNombre(pCuentaAhorroVista);
            }
            catch
            {
                return null;
            }
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaCIF(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                return CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaCIF(pCuentaAhorroVista);
            }
            catch
            {
                return null;
            }
        }

        public static int obtenerCuentaAhorroVistaID(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                return CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaID(pCuentaAhorroVista.getNumeroCuenta());
            }
            catch
            {
                return 0;
            }
        }

        public static string agregarDinero(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            try
            {
                CuentaAhorroVistaDAO.agregarDinero(pCuentaAhorroVista, pMonto);
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string realizarPagoODebito(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            try
            {
                CuentaAhorroVistaDTO _cuentaActual = obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista);
                if (_cuentaActual.getSaldoFlotante() >= pMonto)
                {
                    CuentaAhorroVistaDAO.quitarDinero(pCuentaAhorroVista, pMonto);
                    return "Transacción completada con éxito";
                }
                else
                {
                    return "Fondos insuficientes";
                }
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }
    }
}
