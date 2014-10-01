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
        public static int SLEEP = 10000;

        public static string agregarCuentaAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            try
            {
                string _numeroCuenta = GeneradorCuentas.generarCuenta(Constantes.AHORROAUTOMATICO, pCuentaAhorroAutomatico.getTipoMoneda());
                DateTime _fechaFinalizacion = pCuentaAhorroAutomatico.getFechaInicio().AddMonths(pCuentaAhorroAutomatico.getTiempoAhorro());
                decimal _montoAhorro = calcularMontoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getMontoDeduccion());
                CuentaAhorroAutomatico _cuentaAhorroAutomatico = new CuentaAhorroAutomatico(_numeroCuenta, pCuentaAhorroAutomatico.getDescripcion(), 0,
                    false, pCuentaAhorroAutomatico.getTipoMoneda(), pCuentaAhorroAutomatico.getFechaInicio(), pCuentaAhorroAutomatico.getTiempoAhorro(),
                    _fechaFinalizacion, pCuentaAhorroAutomatico.getFechaInicio(), _montoAhorro, pCuentaAhorroAutomatico.getMontoDeduccion(), pCuentaAhorroAutomatico.getProposito(), 
                    pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                CuentaAhorroAutomaticoDAO.agregarCuentaAhorroAutomaticoBase(_cuentaAhorroAutomatico);
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
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDTO = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Console.WriteLine("Hora de iniciar a cobrar");
                ThreadStart _delegado = new ThreadStart(() => esperarTiempoInicioAhorro(pCuentaAhorroAutomatico));
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
                return "Transacción completada con éxito";
            }
            catch
            {
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
            CuentaAhorroAutomaticoDAO.modificarEstado(pCuentaAhorroAutomatico.getNumeroCuenta(), true);
            iniciarAhorroAux(pCuentaAhorroAutomatico);
        }

        private static void iniciarAhorroAux(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoDTO = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);         
            if (_cuentaAhorroAutomaticoDTO.getTipoPeriodo() == Constantes.SEGUNDOS)
            {
                cobrarEnSegundos(_cuentaAhorroAutomaticoDTO);
            }
            else if (_cuentaAhorroAutomaticoDTO.getTipoPeriodo() == Constantes.MINUTOS)
            {
                cobrarEnMinutos(_cuentaAhorroAutomaticoDTO);
            }
            else if (_cuentaAhorroAutomaticoDTO.getTipoPeriodo() == Constantes.HORAS)
            {
                cobrarEnHoras(_cuentaAhorroAutomaticoDTO);
            }
            else if (_cuentaAhorroAutomaticoDTO.getTipoPeriodo() == Constantes.DIAS)
            {
                cobrarEnDias(_cuentaAhorroAutomaticoDTO);
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
                    CuentaAhorroAutomaticoDAO.agregarDinero(pCuentaAhorroAutomatico.getNumeroCuenta(), _montoAAhorrar, Constantes.AHORROAUTOMATICO);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
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
                    CuentaAhorroAutomaticoDAO.agregarDinero(pCuentaAhorroAutomatico.getNumeroCuenta(), _montoAAhorrar, Constantes.AHORROAUTOMATICO);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
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
                    CuentaAhorroAutomaticoDAO.agregarDinero(pCuentaAhorroAutomatico.getNumeroCuenta(), _montoAAhorrar, Constantes.AHORROAUTOMATICO);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
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
                    CuentaAhorroAutomaticoDAO.agregarDinero(pCuentaAhorroAutomatico.getNumeroCuenta(), _montoAAhorrar, Constantes.AHORROAUTOMATICO);
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
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
            modificarCuentaAhorroAutomatico(pCuentaAhorroAutomatico);
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
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomaticoInterna = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                decimal _montoAhorro = calcularMontoAhorro(pCuentaAhorroAutomatico.getTiempoAhorro(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getMontoDeduccion());
                DateTime _fechaFinalizacion = _cuentaAhorroAutomaticoInterna.getFechaInicio().AddMonths(pCuentaAhorroAutomatico.getTiempoAhorro());
                CuentaAhorroAutomatico _cuentaAhorroAutomatico = new CuentaAhorroAutomatico(pCuentaAhorroAutomatico.getNumeroCuenta(), pCuentaAhorroAutomatico.getDescripcion(),
                    0, false, pCuentaAhorroAutomatico.getTipoMoneda(), _cuentaAhorroAutomaticoInterna.getFechaInicio(), pCuentaAhorroAutomatico.getTiempoAhorro(), _fechaFinalizacion, _cuentaAhorroAutomaticoInterna.getUltimaFechaCobro(),
                    _montoAhorro, pCuentaAhorroAutomatico.getMontoDeduccion(), pCuentaAhorroAutomatico.getProposito(), pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
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

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaOrigen = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen);
                CuentaAhorroAutomaticoDTO _cuentaDestino = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino);
                if (_cuentaOrigen.getEstado() == false && _cuentaOrigen.getSaldo() >= pMonto && _cuentaDestino.getEstado() == false)
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen.getNumeroCuenta(), pMonto, _cuentaDestino.getNumeroCuenta(), Constantes.AHORROAUTOMATICO);
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

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaOrigen = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen);
                CuentaAhorroVistaDTO _cuentaDestino = CuentaAhorroVistaManager.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino);
                if (_cuentaOrigen.getEstado() == false && _cuentaOrigen.getSaldo() >= pMonto && _cuentaDestino.getEstado() == true)
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen.getNumeroCuenta(), pMonto, _cuentaDestino.getNumeroCuenta(), Constantes.AHORROVISTA);
                    return "Transacción completada con éxito";
                }
                else
                {
                    return "Fondos insuficientes o cuenta inactiva";
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

        private static CuentaAhorroAutomaticoDTO cuentaAhorroAutomaticoADTO(CuentaAhorroAutomatico pCuentaAhorroAutomatico)
        {
            CuentaAhorroAutomaticoDTO _cuentaSalida = new CuentaAhorroAutomaticoDTO();
            _cuentaSalida.setDescripcion(pCuentaAhorroAutomatico.getDescripcion());
            _cuentaSalida.setEstado(pCuentaAhorroAutomatico.getEstado());
            _cuentaSalida.setFechaFinalizacion(pCuentaAhorroAutomatico.getFechaFinalizacion().Day, pCuentaAhorroAutomatico.getFechaFinalizacion().Month, 
                pCuentaAhorroAutomatico.getFechaFinalizacion().Year, pCuentaAhorroAutomatico.getFechaFinalizacion().Hour, pCuentaAhorroAutomatico.getFechaFinalizacion().Day,
                pCuentaAhorroAutomatico.getFechaFinalizacion().Second);
            _cuentaSalida.setFechaInicio(pCuentaAhorroAutomatico.getFechaInicio().Day, pCuentaAhorroAutomatico.getFechaInicio().Month, pCuentaAhorroAutomatico.getFechaInicio().Year,
                pCuentaAhorroAutomatico.getFechaInicio().Hour, pCuentaAhorroAutomatico.getFechaInicio().Day, pCuentaAhorroAutomatico.getFechaInicio().Second);
            _cuentaSalida.setUltimaFechaCobro(pCuentaAhorroAutomatico.getUltimaFechaCobro().Day, pCuentaAhorroAutomatico.getUltimaFechaCobro().Month, pCuentaAhorroAutomatico.getUltimaFechaCobro().Year,
                pCuentaAhorroAutomatico.getUltimaFechaCobro().Day, pCuentaAhorroAutomatico.getUltimaFechaCobro().Hour, pCuentaAhorroAutomatico.getUltimaFechaCobro().Second);
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
