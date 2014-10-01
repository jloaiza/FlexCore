using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.Cuentas
{
    public class CuentaAhorroDTO
    {
        private string _numeroCuenta;
        private string _descripcion;
        private int _tipoMoneda;
        private decimal _saldo;
        private bool _estado;

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

        public void setSaldo(decimal pSaldo)
        {
            _saldo = pSaldo;
        }

        public bool getEstado()
        {
            return _estado;
        }

        public int getTipoMoneda()
        {
            return _tipoMoneda;
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

    }
}
