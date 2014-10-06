using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.Cuentas;
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
                CuentaAhorroAutomatico _cuentaAhorroAutomatico = new CuentaAhorroAutomatico(_numeroCuenta, pCuentaAhorroAutomatico.getDescripcion(), 0,
                    false, pCuentaAhorroAutomatico.getTipoMoneda(), pCuentaAhorroAutomatico.getFechaInicio(), pCuentaAhorroAutomatico.getTiempoAhorro(),
                    _fechaFinalizacion, pCuentaAhorroAutomatico.getFechaInicio(), _montoAhorro, pCuentaAhorroAutomatico.getMontoDeduccion(), pCuentaAhorroAutomatico.getProposito(), 
                    pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
                _cuentaAhorroAutomatico.setCliente(pCuentaAhorroAutomatico.getCliente());
                CuentaAhorroAutomaticoDAO.agregarCuentaAhorroAutomaticoBase(_cuentaAhorroAutomatico);
                pCuentaAhorroAutomatico.setNumeroCuenta(_numeroCuenta);
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
                    realizarAhorro(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion(), _montoAAhorrar, pCuentaAhorroAutomatico.getNumeroCuenta());
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            CuentaAhorroAutomaticoDAO.modificarEstado(pCuentaAhorroAutomatico.getNumeroCuenta(), false);
        }

        private static void realizarAhorro(string pNumeroCuentaOrigen, decimal pMontoAhorro, string pNumeroCuentaDestino)
        {
            CuentaAhorroVista _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pNumeroCuentaOrigen);
            if(_cuentaOrigen.getEstado() == false)
            {
                Console.WriteLine("La cuenta desde donde se hace la deduccion se encuentra desactivada");
                //GENERO EL ERROR A LA TABLA DE ERRORES.
            }
            else if(_cuentaOrigen.getSaldoFlotante() < pMontoAhorro)
            {
                Console.WriteLine("La cuenta desde donde se hace la deduccion se ha quedado sin fondos");
                //SE GENERA EL ERROR A LA TABLA DE ERRORES
            }
            else
            {
                CuentaAhorroVistaDAO.quitarDinero(pNumeroCuentaOrigen, pMontoAhorro, pNumeroCuentaDestino, Constantes.AHORROAUTOMATICO);
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
                    realizarAhorro(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion(), _montoAAhorrar, pCuentaAhorroAutomatico.getNumeroCuenta());
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            CuentaAhorroAutomaticoDAO.modificarEstado(pCuentaAhorroAutomatico.getNumeroCuenta(), false);
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
                    realizarAhorro(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion(), _montoAAhorrar, pCuentaAhorroAutomatico.getNumeroCuenta());
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            CuentaAhorroAutomaticoDAO.modificarEstado(pCuentaAhorroAutomatico.getNumeroCuenta(), false);
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
                    realizarAhorro(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion(), _montoAAhorrar, pCuentaAhorroAutomatico.getNumeroCuenta());
                    modificarUltimaFechaCobro(pCuentaAhorroAutomatico, _horaActualLimitada, _proporcionalidadDeCobro);
                }
                pCuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
                Thread.Sleep(SLEEP);
            }
            CuentaAhorroAutomaticoDAO.modificarEstado(pCuentaAhorroAutomatico.getNumeroCuenta(), false);
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
            CuentaAhorroAutomaticoDAO.modificarUltimaFechaCobro(pCuentaAhorroAutomatico.getNumeroCuenta(), pCuentaAhorroAutomatico.getUltimaFechaCobro());
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
        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoNombre(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, int pNumeroPagina, int pCantidadElementos)
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
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen.getNumeroCuenta(), pMonto, _cuentaDestino.getNumeroCuenta(), Constantes.AHORROAUTOMATICO);
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
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen.getNumeroCuenta(), pMonto, _cuentaDestino.getNumeroCuenta(), Constantes.AHORROVISTA);
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

        private static CuentaAhorroAutomaticoDTO cuentaAhorroAutomaticoADTO(CuentaAhorroAutomatico pCuentaAhorroAutomatico)
        {
            CuentaAhorroAutomaticoDTO _cuentaSalida = new CuentaAhorroAutomaticoDTO();
            _cuentaSalida.setDescripcion(pCuentaAhorroAutomatico.getDescripcion());
            _cuentaSalida.setEstado(pCuentaAhorroAutomatico.getEstado());
            _cuentaSalida.setFechaFinalizacion(pCuentaAhorroAutomatico.getFechaFinalizacion().Day, pCuentaAhorroAutomatico.getFechaFinalizacion().Month, 
                pCuentaAhorroAutomatico.getFechaFinalizacion().Year, pCuentaAhorroAutomatico.getFechaFinalizacion().Hour, pCuentaAhorroAutomatico.getFechaFinalizacion().Minute,
                pCuentaAhorroAutomatico.getFechaFinalizacion().Second);
            _cuentaSalida.setFechaInicio(pCuentaAhorroAutomatico.getFechaInicio().Day, pCuentaAhorroAutomatico.getFechaInicio().Month, pCuentaAhorroAutomatico.getFechaInicio().Year,
                pCuentaAhorroAutomatico.getFechaInicio().Hour, pCuentaAhorroAutomatico.getFechaInicio().Minute, pCuentaAhorroAutomatico.getFechaInicio().Second);
            _cuentaSalida.setUltimaFechaCobro(pCuentaAhorroAutomatico.getUltimaFechaCobro().Day, pCuentaAhorroAutomatico.getUltimaFechaCobro().Month, pCuentaAhorroAutomatico.getUltimaFechaCobro().Year,
                pCuentaAhorroAutomatico.getUltimaFechaCobro().Hour, pCuentaAhorroAutomatico.getUltimaFechaCobro().Minute, pCuentaAhorroAutomatico.getUltimaFechaCobro().Second);
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
            _cuentaSalida.setCliente(pCuentaAhorroAutomatico.getCliente());
            return _cuentaSalida;
        }
    }
}
