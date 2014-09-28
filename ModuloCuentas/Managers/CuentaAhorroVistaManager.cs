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
                CuentaAhorroVistaDAO.agregarDinero(pCuentaAhorroVista.getNumeroCuenta(), pMonto);
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
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaActual.getNumeroCuenta(), pMonto);
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
