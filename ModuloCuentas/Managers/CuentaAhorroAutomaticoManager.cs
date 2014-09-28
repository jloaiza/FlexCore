using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.DTO;
using ModuloCuentas.Cuentas;
using ModuloCuentas.DAO;
using ModuloCuentas.Generales;
using System.Threading;

namespace ModuloCuentas.Managers
{
    internal static class CuentaAhorroAutomaticoManager
    {
        public static string agregarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                string _numeroCuenta = GeneradorCuentas.generarCuenta(Constantes.AHORROAUTOMATICO, pCuentaAhorroAutomatico.getTipoMoneda());
                DateTime _fechaFinalizacion = pCuentaAhorroAutomatico.getFechaInicio().AddMonths(pCuentaAhorroAutomatico.getTiempoAhorro());
                decimal _montoAhorro = calcularMontoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getMontoDeduccion());
                CuentaAhorroAutomatico _cuentaAhorroAutomatico = new CuentaAhorroAutomatico(_numeroCuenta, pCuentaAhorroAutomatico.getDescripcion(), 0,
                    pCuentaAhorroAutomatico.getEstado(), pCuentaAhorroAutomatico.getTipoMoneda(), pCuentaAhorroAutomatico.getFechaInicio(), pCuentaAhorroAutomatico.getTiempoAhorro(),
                    _fechaFinalizacion, _montoAhorro, pCuentaAhorroAutomatico.getMontoDeduccion(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(),
                    pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                CuentaAhorroAutomaticoDAO.agregarCuentaAhorroAutomaticoBase(_cuentaAhorroAutomatico);
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string iniciarAhorro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDTO = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                if (_cuentaAhorroAutomaticoDTO.getEstado() == true)
                {
                    ThreadStart _delegado = new ThreadStart(() => iniciarAhorroAux(pCuentaAhorroAutomatico));
                    Thread _hiloReplica = new Thread(_delegado);
                    _hiloReplica.Start();
                    return "Transacción completada con éxito";
                }
                else
                {
                    return "Debe activar la cuenta antes de iniciar el ahorro";
                }
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
            
        }

        private static void iniciarAhorroAux(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {

        }

        public static string eliminarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                CuentaAhorroAutomaticoDAO.eliminarCuentaAhorroAutomaticoBase(pCuentaAhorroAutomatico.getNumeroCuenta());
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string modificarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                decimal _montoAhorro = calcularMontoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getMontoDeduccion());
                DateTime _fechaFinalizacion = pCuentaAhorroAutomatico.getFechaInicio().AddMonths(pCuentaAhorroAutomatico.getTiempoAhorro());
                CuentaAhorroAutomatico _cuentaAhorroAutomatico = new CuentaAhorroAutomatico(pCuentaAhorroAutomatico.getNumeroCuenta(), pCuentaAhorroAutomatico.getDescripcion(),
                    0, pCuentaAhorroAutomatico.getEstado(), pCuentaAhorroAutomatico.getTipoMoneda(), pCuentaAhorroAutomatico.getFechaInicio(), pCuentaAhorroAutomatico.getTiempoAhorro(), _fechaFinalizacion, _montoAhorro,
                    pCuentaAhorroAutomatico.getMontoDeduccion(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                CuentaAhorroAutomaticoDAO.modificarCuentaAhorroAutomaticoBase(_cuentaAhorroAutomatico);
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoNumeroCuenta(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                CuentaAhorroAutomatico _cuentaAhorroAutomatico =  CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuenta());
                return cuentaAhorroAutomaticoADTO(_cuentaAhorroAutomatico);
            }
            catch
            {
                return null;
            }
        }

        //CAMBIAR A GETCEDULA
        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCedula(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                CuentaAhorroAutomatico _cuentaAhorroAutomatico = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoCedula(pCuentaAhorroAutomatico.getNumeroCuenta());
                return cuentaAhorroAutomaticoADTO(_cuentaAhorroAutomatico);
            }
            catch
            {
                return null;
            }
        }

        //CAMBIAR A GET NOMBRE
        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoNombre(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                List<CuentaAhorroAutomatico> _cuentaAhorroAutomatico = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNombre(pCuentaAhorroAutomatico.getNumeroCuenta());
                List<CuentaAhorroAutomaticoDTO> _cuentasSalida = new List<CuentaAhorroAutomaticoDTO>();
                foreach (CuentaAhorroAutomatico cuentas in _cuentaAhorroAutomatico)
                {
                    _cuentasSalida.Add(cuentaAhorroAutomaticoADTO(cuentas));
                }
                return _cuentasSalida;
            }
            catch
            {
                return null;
            }
        }

        //CAMBIAR A GET CIF
        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCIF(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                CuentaAhorroAutomatico _cuentaAhorroAutomatico = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoCIF(pCuentaAhorroAutomatico.getNumeroCuenta());
                return cuentaAhorroAutomaticoADTO(_cuentaAhorroAutomatico);
            }
            catch
            {
                return null;
            }
        }

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, decimal pMonto)
        {
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaActualDTO = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                if (Tiempo.getHoraActual() > _cuentaActualDTO.getFechaFinalizacion() && _cuentaActualDTO.getSaldo() >= pMonto)
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaActualDTO.getNumeroCuenta(), pMonto);
                    return "Transacción completada con éxito";
                }
                else
                {
                    return "Fondos insuficientes o cuenta actualmente en ahorro";
                }
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static bool agregarDinero(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            try
            {
                CuentaAhorroAutomaticoDAO.agregarDinero(pCuentaAhorroVista.getNumeroCuenta(), pMonto);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static decimal calcularMontoAhorro(int pTiempoAhorro, int pMagnitudPeriodoAhorro, int pTipoPeriodo, decimal pMontoDeduccion)
        {
            decimal _montoAhorro = 0;
            if (pTipoPeriodo == Constantes.SEGUNDOS)
            {
                _montoAhorro = ((pTiempoAhorro) / (Tiempo.segundosAMeses(pMagnitudPeriodoAhorro))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.MINUTOS)
            {
                _montoAhorro = ((pTiempoAhorro) / (Tiempo.minutosAMeses(pMagnitudPeriodoAhorro))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.HORAS)
            {
                _montoAhorro = ((pTiempoAhorro) / (Tiempo.horasAMeses(pMagnitudPeriodoAhorro))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.DIAS)
            {
                _montoAhorro = ((pTiempoAhorro) / (Tiempo.diasAMeses(pMagnitudPeriodoAhorro))) * pMontoDeduccion;
            }
            return _montoAhorro;
        }

        private static CuentaAhorroAutomaticoDTO cuentaAhorroAutomaticoADTO(CuentaAhorroAutomatico pCuentaAhorroAutomatico)
        {
            CuentaAhorroAutomaticoDTO _cuentaSalida = new CuentaAhorroAutomaticoDTO();
            _cuentaSalida.setDescripcion(pCuentaAhorroAutomatico.getDescripcion());
            _cuentaSalida.setEstado(pCuentaAhorroAutomatico.getEstado());
            _cuentaSalida.setFechaFinalizacion(pCuentaAhorroAutomatico.getFechaFinalizacion().Day, pCuentaAhorroAutomatico.getFechaFinalizacion().Month, pCuentaAhorroAutomatico.getFechaFinalizacion().Year);
            _cuentaSalida.setFechaInicio(pCuentaAhorroAutomatico.getFechaInicio().Day, pCuentaAhorroAutomatico.getFechaInicio().Month, pCuentaAhorroAutomatico.getFechaInicio().Year);
            _cuentaSalida.setMagnitudPeriodoAhorro(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            _cuentaSalida.setMontoAhorro(pCuentaAhorroAutomatico.getMontoAhorro());
            _cuentaSalida.setMontoDeduccion(pCuentaAhorroAutomatico.getMontoDeduccion());
            _cuentaSalida.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuenta());
            _cuentaSalida.setNumeroCuentaDeduccion(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
            _cuentaSalida.setProposito(pCuentaAhorroAutomatico.getProposito());
            _cuentaSalida.setSaldo(pCuentaAhorroAutomatico.getSaldo());
            _cuentaSalida.setTiempoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro());
            _cuentaSalida.setTipoMoneda(pCuentaAhorroAutomatico.getTipoMoneda());
            _cuentaSalida.setTipoPeriodo(pCuentaAhorroAutomatico.getTipoPeriodo());
            return _cuentaSalida;
        }
    }
}
