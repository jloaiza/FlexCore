using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.DTO;
using ModuloCuentas.Cuentas;
using ModuloCuentas.DAO;
using ModuloCuentas.Generales;

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
                return CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
            }
            catch
            {
                return null;
            }
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCedula(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                return CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoCedula(pCuentaAhorroAutomatico);
            }
            catch
            {
                return null;
            }
        }

        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoNombre(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                return CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNombre(pCuentaAhorroAutomatico);
            }
            catch
            {
                return null;
            }
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCIF(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                return CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoCIF(pCuentaAhorroAutomatico);
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
                CuentaAhorroAutomaticoDTO _cuentaActual = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                if (Tiempo.getHoraActual() > _cuentaActual.getFechaFinalizacion() && _cuentaActual.getSaldo() >= pMonto)
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(pCuentaAhorroAutomatico, pMonto);
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
    }
}
