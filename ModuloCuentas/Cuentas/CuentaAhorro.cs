using FlexCoreDTOs.clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloCuentas.Cuentas
{
    internal class CuentaAhorro
    {
        private string _numeroCuenta;
        private string _descripcion;
        private decimal _saldo;
        private bool _estado;
        private int _tipoMoneda;
        private ClientDTO _cliente;

        public CuentaAhorro(string pNumeroCuenta, string pDescripcion, decimal pSaldo, bool pEstado, int pTipoMoneda)
        {
            _numeroCuenta = pNumeroCuenta;
            _descripcion = pDescripcion;
            _saldo = pSaldo;
            _estado = pEstado;
            _tipoMoneda = pTipoMoneda;
        }

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

        public ClientDTO getCliente()
        {
            return _cliente;
        }

        public void setCliente(ClientDTO pCliente)
        {
            _cliente = pCliente;
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
