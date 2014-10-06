using FlexCoreDTOs.clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.cuentas
{
    public class CuentaAhorroAutomaticoDTO : CuentaAhorroDTO
    {
        private DateTime _fechaInicio;
        private DateTime _fechaFinalizacion;
        private DateTime _ultimaFechaCobro;
        private string _numeroCuentaDeduccion;
        private decimal _montoAhorro;
        private decimal _montoDeduccion;
        private int _tiempoAhorro;
        private int _proposito;
        private int _magnitudPeriodoAhorro;
        private int _tipoPeriodo;

        public CuentaAhorroAutomaticoDTO() { }

        public CuentaAhorroAutomaticoDTO(string pNumeroCuenta, string pDecripcion, decimal pSaldo, bool pEstado, int pTipoMoneda, ClientDTO pCliente, DateTime pFechaInicio,
            int pTiempoAhorro, DateTime pFechaFinalizacion, DateTime pUltimaFechaCobro, decimal pMontoAhorro, decimal pMontoDeduccion, int pProposito, int pMagnitudPeriodoAhorro,
            int pTipoPeriodo, string pNumeroCuentaDeduccion) : base(pNumeroCuenta, pDecripcion, pSaldo, pEstado, pTipoMoneda, pCliente)
        {
            _fechaInicio = pFechaInicio;
            _tiempoAhorro = pTiempoAhorro;
            _fechaFinalizacion = pFechaFinalizacion;
            _ultimaFechaCobro = pUltimaFechaCobro;
            _montoAhorro = pMontoAhorro;
            _montoDeduccion = pMontoDeduccion;
            _proposito = pProposito;
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

        public int getProposito()
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

        public void setFechaInicio(int pDia, int pMes, int pAño, int pHora, int pMinuto, int pSegundo)
        {
            _fechaInicio = new DateTime(pAño, pMes, pDia, pHora, pMinuto, pSegundo);
        }

        public void setUltimaFechaCobro(int pDia, int pMes, int pAño, int pHora, int pMinuto, int pSegundo)
        {
            _ultimaFechaCobro = new DateTime(pAño, pMes, pDia, pHora, pMinuto, pSegundo);
        }


        public void setTiempoAhorro(int pTiempoAhorro)
        {
            _tiempoAhorro = pTiempoAhorro;
        }

        public void setFechaFinalizacion(int pDia, int pMes, int pAño, int pHora, int pMinuto, int pSegundo)
        {
            _fechaFinalizacion = new DateTime(pAño, pMes, pDia, pHora, pMinuto, pSegundo);
        }

        public void setMontoAhorro(decimal pMontoAhorro)
        {
            _montoAhorro = pMontoAhorro;
        }

        public void setMontoDeduccion(decimal pMontoDeduccion)
        {
            _montoDeduccion = pMontoDeduccion;
        }

        public void setProposito(int pProposito)
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
