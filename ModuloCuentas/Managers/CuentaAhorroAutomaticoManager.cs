using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.DAO;
using ModuloCuentas.Generales;
using System.Threading;
using FlexCoreDTOs.cuentas;

namespace ModuloCuentas.Managers
{
    internal static class CuentaAhorroAutomaticoManager
    {
        public static int SLEEP = 1000;

        public static string agregarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                string _numeroCuenta = GeneradorCuentas.generarCuenta(Constantes.AHORROAUTOMATICO, pCuentaAhorroAutomatico.getTipoMoneda());
                DateTime _fechaFinalizacion = pCuentaAhorroAutomatico.getFechaInicio().AddMonths(pCuentaAhorroAutomatico.getTiempoAhorro());
                decimal _montoAhorro = calcularMontoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getMontoDeduccion());
                pCuentaAhorroAutomatico.setNumeroCuenta(_numeroCuenta);
                pCuentaAhorroAutomatico.setSaldo(0);
                pCuentaAhorroAutomatico.setEstado(false);
                pCuentaAhorroAutomatico.setFechaFinalizacion(_fechaFinalizacion.Day, _fechaFinalizacion.Month, _fechaFinalizacion.Year, _fechaFinalizacion.Hour, _fechaFinalizacion.Minute, _fechaFinalizacion.Second);
                pCuentaAhorroAutomatico.setMontoAhorro(_montoAhorro);
                pCuentaAhorroAutomatico.setUltimaFechaCobro(pCuentaAhorroAutomatico.getFechaInicio().Day, pCuentaAhorroAutomatico.getFechaInicio().Month, pCuentaAhorroAutomatico.getFechaInicio().Year, pCuentaAhorroAutomatico.getFechaInicio().Hour, pCuentaAhorroAutomatico.getFechaInicio().Minute, pCuentaAhorroAutomatico.getFechaInicio().Second);
                CuentaAhorroAutomaticoDAO.agregarCuentaAhorroAutomaticoBase(pCuentaAhorroAutomatico);
                Console.WriteLine(iniciarAhorro(pCuentaAhorroAutomatico));
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
                ThreadStart _delegado = new ThreadStart(() => esperarTiempoInicioAhorro(pCuentaAhorroAutomatico));
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
                return "Transacción completada con éxito";
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Ha ocurrido un error en la transacción";
            }
            
        }

        private static void esperarTiempoInicioAhorro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while(Tiempo.getHoraActual() < pCuentaAhorroAutomatico.getFechaInicio())
            {
                Thread.Sleep(SLEEP);
            }
            pCuentaAhorroAutomatico.setEstado(true);
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroAutomatico);
            iniciarAhorroAux(pCuentaAhorroAutomatico);
        }

        private static void iniciarAhorroAux(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.SEGUNDOS)
            {
                cobrarEnSegundos(pCuentaAhorroAutomatico);
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.MINUTOS)
            {
                cobrarEnMinutos(pCuentaAhorroAutomatico);
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.HORAS)
            {
                cobrarEnHoras(pCuentaAhorroAutomatico);
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.DIAS)
            {
                cobrarEnDias(pCuentaAhorroAutomatico);
            }
        }

        private static void cobrarEnSegundos(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddSeconds(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < Tiempo.getHoraActual())
                {
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalSeconds / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            pCuentaAhorroAutomatico.setEstado(false);
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroAutomatico);
        }

        private static void cobrarEnDias(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddDays(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < Tiempo.getHoraActual())
                {
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalDays / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            pCuentaAhorroAutomatico.setEstado(false);
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroAutomatico);
        }

        private static void cobrarEnMinutos(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddMinutes(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < Tiempo.getHoraActual())
                {
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalMinutes / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            pCuentaAhorroAutomatico.setEstado(false);
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroAutomatico);
        }

        private static void cobrarEnHoras(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            while (pCuentaAhorroAutomatico.getUltimaFechaCobro() < pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                if (pCuentaAhorroAutomatico.getUltimaFechaCobro().AddHours(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()) < Tiempo.getHoraActual())
                {
                    DateTime _horaActualLimitada = getHoraActualLimitada(pCuentaAhorroAutomatico);
                    TimeSpan _tiempoTranscurrido = _horaActualLimitada - pCuentaAhorroAutomatico.getUltimaFechaCobro();
                    int _proporcionalidadDeCobro = Convert.ToInt32(Math.Truncate(_tiempoTranscurrido.TotalHours / pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro()));
                    decimal _montoAAhorrar = _proporcionalidadDeCobro * pCuentaAhorroAutomatico.getMontoDeduccion();
                    CuentaAhorroVistaDTO _cuentaDeduccion = new CuentaAhorroVistaDTO();
                    _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                    realizarAhorro(_cuentaDeduccion, _montoAAhorrar, pCuentaAhorroAutomatico);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            pCuentaAhorroAutomatico.setEstado(false);
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroAutomatico);
        }

        private static void modificarUltimaFechaCobro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, DateTime pHoraActual, int pProporcionalidadDeCobro)
        {
            DateTime _ultimaFechaCobro = new DateTime();
            if(pHoraActual == pCuentaAhorroAutomatico.getFechaFinalizacion())
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getFechaFinalizacion();
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.SEGUNDOS)
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getUltimaFechaCobro().AddSeconds(pProporcionalidadDeCobro * pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.MINUTOS)
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getUltimaFechaCobro().AddMinutes(pProporcionalidadDeCobro * pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.HORAS)
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getUltimaFechaCobro().AddHours(pProporcionalidadDeCobro * pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            }
            else if (pCuentaAhorroAutomatico.getTipoPeriodo() == Constantes.DIAS)
            {
                _ultimaFechaCobro = pCuentaAhorroAutomatico.getUltimaFechaCobro().AddDays(pProporcionalidadDeCobro * pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
            }
            pCuentaAhorroAutomatico.setUltimaFechaCobro(_ultimaFechaCobro.Day, _ultimaFechaCobro.Month, _ultimaFechaCobro.Year, _ultimaFechaCobro.Hour, _ultimaFechaCobro.Minute, _ultimaFechaCobro.Second);
            CuentaAhorroAutomaticoDAO.modificarUltimaFechaCobro(pCuentaAhorroAutomatico, pCuentaAhorroAutomatico.getUltimaFechaCobro());
        }

        private static DateTime getHoraActualLimitada(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            if(pCuentaAhorroAutomatico.getFechaFinalizacion() < Tiempo.getHoraActual())
            {
                return pCuentaAhorroAutomatico.getFechaFinalizacion();
            }
            else
            {
                return Tiempo.getHoraActual();
            }
        }

        private static void realizarAhorro(CuentaAhorroVistaDTO pCuentaOrigen, decimal pMontoAhorro, CuentaAhorroAutomaticoDTO pCuentaDestino)
        {
            CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaOrigen);
            if (_cuentaOrigen.getEstado() == false)
            {
                Console.WriteLine("La cuenta desde donde se hace la deduccion se encuentra desactivada");
                //GENERO EL ERROR A LA TABLA DE ERRORES.
            }
            else if (_cuentaOrigen.getSaldoFlotante() < pMontoAhorro)
            {
                Console.WriteLine("La cuenta desde donde se hace la deduccion se ha quedado sin fondos");
                //SE GENERA EL ERROR A LA TABLA DE ERRORES
            }
            else
            {
                CuentaAhorroVistaDAO.quitarDinero(pCuentaOrigen, pMontoAhorro, pCuentaDestino, Constantes.AHORROAUTOMATICO);
            }
        }

        public static string eliminarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                CuentaAhorroAutomaticoDAO.eliminarCuentaAhorroAutomaticoBase(pCuentaAhorroAutomatico);
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
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoInterna = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                decimal _montoAhorro = calcularMontoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getMontoDeduccion());
                DateTime _fechaFinalizacion = _cuentaAhorroAutomaticoInterna.getFechaInicio().AddMonths(pCuentaAhorroAutomatico.getTiempoAhorro());
                _cuentaAhorroAutomaticoInterna.setMontoAhorro(_montoAhorro);
                _cuentaAhorroAutomaticoInterna.setFechaFinalizacion(_fechaFinalizacion.Day, _fechaFinalizacion.Month, _fechaFinalizacion.Year, _fechaFinalizacion.Hour, _fechaFinalizacion.Minute, _fechaFinalizacion.Second);
                _cuentaAhorroAutomaticoInterna.setDescripcion(pCuentaAhorroAutomatico.getDescripcion());
                _cuentaAhorroAutomaticoInterna.setTipoMoneda(pCuentaAhorroAutomatico.getTipoMoneda());
                _cuentaAhorroAutomaticoInterna.setTiempoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro());
                _cuentaAhorroAutomaticoInterna.setMontoDeduccion(pCuentaAhorroAutomatico.getMontoDeduccion());
                _cuentaAhorroAutomaticoInterna.setProposito(pCuentaAhorroAutomatico.getProposito());
                _cuentaAhorroAutomaticoInterna.setMagnitudPeriodoAhorro(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro());
                _cuentaAhorroAutomaticoInterna.setTipoPeriodo(pCuentaAhorroAutomatico.getTipoPeriodo());
                _cuentaAhorroAutomaticoInterna.setNumeroCuentaDeduccion(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                CuentaAhorroAutomaticoDAO.modificarCuentaAhorroAutomaticoBase(_cuentaAhorroAutomaticoInterna);
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

        //CAMBIAR A GETCEDULA
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

        //CAMBIAR A GET NOMBRE
        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoNombre(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, int pNumeroPagina, int pCantidadElementos)
        {
            try
            {
                return CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNombre(pCuentaAhorroAutomatico, pNumeroPagina, pCantidadElementos);
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
                return CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoCIF(pCuentaAhorroAutomatico);
            }
            catch
            {
                return null;
            }
        }

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaOrigen = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen);
                CuentaAhorroAutomaticoDTO _cuentaDestino = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino);
                if(_cuentaOrigen.getEstado() == true)
                {
                    return "La cuenta con la cual se desea pagar se encuentra actualmente en ahorro";
                }
                else if(_cuentaOrigen.getSaldo() < pMonto)
                {
                    return "Fondos insuficientes";
                }
                else if(_cuentaDestino.getEstado() == true)
                {
                    return "La cuenta a la cual se desea pagar se encuentra actualmente en ahorro";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO);
                    return "Transacción completada con éxito";
                }
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaOrigen = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen);
                CuentaAhorroVistaDTO _cuentaDestino = CuentaAhorroVistaManager.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino);
                if (_cuentaOrigen.getEstado() == true)
                {
                    return "La cuenta con la cual se desea pagar se encuentra actualmente en ahorro";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    return "Fondos insuficientes";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    return "La cuenta a la cual se desea pagar se encuentra actualmente desactivada";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA);
                    return "Transacción completada con éxito";
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
                _montoAhorro = Math.Truncate(((pTiempoAhorro) / (Tiempo.segundosAMeses(pMagnitudPeriodoAhorro)))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.MINUTOS)
            {
                _montoAhorro = Math.Truncate(((pTiempoAhorro) / (Tiempo.minutosAMeses(pMagnitudPeriodoAhorro)))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.HORAS)
            {
                _montoAhorro = Math.Truncate(((pTiempoAhorro) / (Tiempo.horasAMeses(pMagnitudPeriodoAhorro)))) * pMontoDeduccion;
            }
            else if (pTipoPeriodo == Constantes.DIAS)
            {
                _montoAhorro = Math.Truncate(((pTiempoAhorro) / (Tiempo.diasAMeses(pMagnitudPeriodoAhorro)))) * pMontoDeduccion;
            }
            return _montoAhorro;
        }
    }
}
