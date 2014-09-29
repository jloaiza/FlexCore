using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloCuentas.Cuentas
{
    internal class CuentaAhorroAutomatico : CuentaAhorro
    {
        private DateTime _fechaInicio;
        private int _tiempoAhorro;
        private DateTime _fechaFinalizacion;
        private decimal _montoAhorro;
        private decimal _montoDeduccion;
        private string _proposito;
        private int _magnitudPeriodoAhorro;
        private int _tipoPeriodo;
        private string _numeroCuentaDeduccion;
        private DateTime _ultimaFechaCobro;

        public CuentaAhorroAutomatico(string pNumeroCuenta, string pDescripcion, decimal pSaldo, bool pEstado, int pTipoMoneda, DateTime pFechaInicio, int pTiempoAhorro,
            DateTime pFechaFinalizacion, DateTime pUltimaFechaCobro, decimal pMontoAhorro, decimal pMontoDeduccion, int pMagnitudPeriodoAhorro, int pTipoPeriodo, string pNumeroCuentaDeduccion) : 
            base(pNumeroCuenta, pDescripcion, pSaldo, pEstado, pTipoMoneda)
        {
            _fechaInicio = pFechaInicio;
            _tiempoAhorro = pTiempoAhorro;
            _fechaFinalizacion = pFechaFinalizacion;
            _montoAhorro = pMontoAhorro;
            _ultimaFechaCobro = pUltimaFechaCobro;
            _montoDeduccion = pMontoDeduccion;
            _magnitudPeriodoAhorro = pMagnitudPeriodoAhorro;
            _tipoPeriodo = pTipoPeriodo;
            _numeroCuentaDeduccion = pNumeroCuentaDeduccion;
        }

        public DateTime getFechaInicio()
        {
            return _fechaInicio;
        }

        public int getTiempoAhorro()
        {
            return _tiempoAhorro;
        }

        public DateTime getFechaFinalizacion()
        {
            return _fechaFinalizacion;
        }

        public DateTime getUltimaFechaCobro()
        {
            return _ultimaFechaCobro;
        }

        public decimal getMontoAhorro()
        {
            return _montoAhorro;
        }

        public decimal getMontoDeduccion()
        {
            return _montoDeduccion;
        }

        public string getProposito()
        {
            return _proposito;
        }

        public int getMagnitudPeriodoAhorro()
        {
            return _magnitudPeriodoAhorro;
        }

        public string getNumeroCuentaDeduccion()
        {
            return _numeroCuentaDeduccion;
        }

        public int getTipoPeriodo()
        {
            return _tipoPeriodo;
        }

        public void setTipoPeriodo(int pTipoPeriodo)
        {
            _tipoPeriodo = pTipoPeriodo;
        }

        public void setFechaInicio(DateTime pFechaInicio)
        {
            _fechaInicio = pFechaInicio;
        }

        public void setUltimaFechaCobro(DateTime pUltimaFechaCobro)
        {
            _ultimaFechaCobro = pUltimaFechaCobro;
        }

        public void setTiempoAhorro(int pTiempoAhorro)
        {
            _tiempoAhorro = pTiempoAhorro;
        }

        public void setFechaFinalizacion(DateTime pFechaFinalizacion)
        {
            _fechaFinalizacion = pFechaFinalizacion;
        }

        public void setMontoAhorro(decimal pMontoAhorro) 
        {
            _montoAhorro = pMontoAhorro;
        }

        public void setMontoDeduccion(decimal pMontoDeduccion)
        {
            _montoDeduccion = pMontoDeduccion;
        }

        public void setProposito(string pProposito)
        {
            _proposito = pProposito;
        }

        public void setMagnitudPeriodoAhorro(int pMagnitudPeriodoAhorro)
        {
            _magnitudPeriodoAhorro = pMagnitudPeriodoAhorro;
        }

        public void setNumeroCuentaDeduccion(string pNumeroCuentaDeduccion)
        {
            _numeroCuentaDeduccion = pNumeroCuentaDeduccion;
        }
    }
}
