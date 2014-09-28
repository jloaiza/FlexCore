using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloCuentas.DTO
{
    public class CuentaAhorroAutomaticoDTO
    {
        private string _numeroCuenta;
        private string _descripcion;
        private decimal _saldo;
        private bool _estado;
        private int _tipoMoneda;
        private DateTime _fechaInicio;
        private int _tiempoAhorro;
        private DateTime _fechaFinalizacion;
        private decimal _montoAhorro;
        private decimal _montoDeduccion;
        private string _proposito;
        private int _magnitudPeriodoAhorro;
        private int _tipoPeriodo;
        private string _numeroCuentaDeduccion;

        public string getNumeroCuenta()
        {
            return _numeroCuenta;
        }

        public string getDescripcion()
        {
            return _descripcion;
        }

        public decimal getSaldo()
        {
            return _saldo;
        }

        public bool getEstado()
        {
            return _estado;
        }

        public int getTipoMoneda()
        {
            return _tipoMoneda;
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

        public void setFechaInicio(int pDia, int pMes, int pAño)
        {
            _fechaInicio = new DateTime(pAño, pMes, pDia);
        }

        public void setTiempoAhorro(int pTiempoAhorro)
        {
            _tiempoAhorro = pTiempoAhorro;
        }

        public void setFechaFinalizacion(int pDia, int pMes, int pAño)
        {
            _fechaFinalizacion = new DateTime(pAño, pMes, pDia);
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

        public void setNumeroCuenta(string pNumeroCuenta)
        {
            _numeroCuenta = pNumeroCuenta;
        }

        public void setDescripcion(string pDescripcion)
        {
            _descripcion = pDescripcion;
        }

        public void setEstado(bool pEstado)
        {
            _estado = pEstado;
        }

        public void setTipoMoneda(int pTipoMoneda)
        {
            _tipoMoneda = pTipoMoneda;
        }

        public void setSaldo(decimal pSaldo)
        {
            _saldo = pSaldo;
        }
    }
}
