using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.DAO;
using System.Threading;
using FlexCoreDTOs.cuentas;
using ModuloCuentas.Generales;

namespace ModuloCuentas.Managers
{
    internal static class CuentaAhorroVistaManager
    {
        public static string agregarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            try
            {
                string _numeroCuenta = GeneradorCuentas.generarCuenta(Constantes.AHORROVISTA, pCuentaAhorroVista.getTipoMoneda());
                pCuentaAhorroVista.setNumeroCuenta(_numeroCuenta);
                pCuentaAhorroVista.setSaldo(0);
                pCuentaAhorroVista.setSaldoFlotante(0);
                CuentaAhorroVistaDAO.agregarCuentaAhorroVistaBase(pCuentaAhorroVista);
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
                CuentaAhorroVistaDAO.eliminarCuentaAhorroVistaBase(pCuentaAhorroVista);
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
                CuentaAhorroVistaDAO.modificarCuentaAhorroVistaBase(pCuentaAhorroVista);
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

        //PASAR A GET CEDULA
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

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaNombre(CuentaAhorroVistaDTO pCuentaAhorroVista, int pNumeroPagina, int pCantidadElementos)
        {
            try
            {
                return CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNombre(pCuentaAhorroVista, pNumeroPagina, pCantidadElementos);
            }
            catch
            {
                return null;
            }
        }

        //PASAR A GET CIF 
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
                return CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista);
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
                CuentaAhorroVistaDAO.agregarDinero(pCuentaAhorroVista, pMonto, Constantes.AHORROVISTA);
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string realizarPagoODebito(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            try
            {
                CuentaAhorroVistaDTO _cuentaOrigen = obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaOrigen);
                CuentaAhorroVistaDTO _cuentaDestino = obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino);
                if(_cuentaOrigen.getSaldoFlotante() < pMonto)
                {
                    return "Fondos insuficientes";
                }
                else if(_cuentaOrigen.getEstado() == false)
                {
                    return "La cuenta con la cual se desea pagar se encuentra actualmente desactivada";
                }
                else if(_cuentaDestino.getEstado() == false)
                {
                    return "La cuenta a la cual se desea pagar se encuentra actualmente desactivada";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA);
                    return "Transacción completada con éxito";
                }
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string realizarPagoODebito(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            try
            {
                CuentaAhorroVistaDTO _cuentaOrigen = obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaOrigen);
                CuentaAhorroAutomaticoDTO _cuentaDestino = CuentaAhorroAutomaticoManager.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino);
                if (_cuentaOrigen.getSaldoFlotante() < pMonto)
                {
                    return "Fondos insuficientes";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    return "La cuenta con la cual se desea pagar se encuentra actualmente desactivada";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    return "La cuenta a la cual se desea pagar se encuentra actualmente en ahorro";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO);
                    return "Transacción completada con éxito";
                }
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string realizarCierreCuentas()
        {
            try
            {
                ThreadStart _delegado = new ThreadStart(realizarCierreCuentasAux);
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        private static void realizarCierreCuentasAux()
        {
            CuentaAhorroVistaDAO.iniciarCierre();
        }
    }
}
