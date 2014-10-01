using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.Cuentas;
using ModuloCuentas.DAO;
using System.Threading;
using FlexCoreDTOs.Cuentas;

namespace ModuloCuentas.Managers
{
    internal static class CuentaAhorroVistaManager
    {
        public static string agregarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            //AQUI TIENE QUE METER LOS BENEFICIARIOS Y LOS CLIENTES
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
                CuentaAhorroVista _cuentaAhorroVista =  CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista.getNumeroCuenta());
                return cuentaAhorroVistaADTO(_cuentaAhorroVista);
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
                CuentaAhorroVista _cuentaAhorroVista = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaCedula(pCuentaAhorroVista.getNumeroCuenta());
                return cuentaAhorroVistaADTO(_cuentaAhorroVista);
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
                List<CuentaAhorroVista> _cuentasAhorroVista = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNombre(pCuentaAhorroVista.getNumeroCuenta());
                List<CuentaAhorroVistaDTO> _cuentasSalida = new List<CuentaAhorroVistaDTO>();
                foreach (CuentaAhorroVista cuentas in _cuentasAhorroVista)
                {
                    _cuentasSalida.Add(cuentaAhorroVistaADTO(cuentas));
                }
                return _cuentasSalida;
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
                CuentaAhorroVista _cuentaAhorroVista = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaCIF(pCuentaAhorroVista.getNumeroCuenta());
                return cuentaAhorroVistaADTO(_cuentaAhorroVista);
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
                return CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista.getNumeroCuenta());
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
                CuentaAhorroVistaDAO.agregarDinero(pCuentaAhorroVista.getNumeroCuenta(), pMonto, Constantes.AHORROVISTA);
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
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen.getNumeroCuenta(), pMonto, pCuentaAhorroVistaDestino.getNumeroCuenta(), Constantes.AHORROVISTA);
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
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen.getNumeroCuenta(), pMonto, pCuentaAhorroAutomaticoDestino.getNumeroCuenta(), Constantes.AHORROAUTOMATICO);
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

        private static CuentaAhorroVistaDTO cuentaAhorroVistaADTO(CuentaAhorroVista pCuentaAhorroVista)
        {
            CuentaAhorroVistaDTO _cuentaSalida = new CuentaAhorroVistaDTO();
            _cuentaSalida.setDescripcion(pCuentaAhorroVista.getDescripcion());
            _cuentaSalida.setEstado(pCuentaAhorroVista.getEstado());
            _cuentaSalida.setNumeroCuenta(pCuentaAhorroVista.getNumeroCuenta());
            _cuentaSalida.setSaldo(pCuentaAhorroVista.getSaldo());
            _cuentaSalida.setSaldoFlotante(pCuentaAhorroVista.getSaldoFlotante());
            _cuentaSalida.setTipoMoneda(pCuentaAhorroVista.getTipoMoneda());
            return _cuentaSalida;
        }
    }
}
