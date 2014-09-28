using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.Cuentas;

namespace ModuloCuentas.DTO
{
    public class CuentaAhorroVistaDTO
    {
        //RECORDAR QUE TAMBIEN VOY A RECIBIR UN CLIENTE DUEÑO DE LA CUENTA Y UNA LISTA DE CLIENTES DTO COMO BENEFICIARIOS.
        //HAY QUE MODIFICAR LO NECESARIO DONDE CORRESPONDE 

        private string _numeroCuenta;
        private string _descripcion;
        private decimal _saldo;
        private bool _estado;
        private int _tipoMoneda;
        private decimal _saldoFlotante;

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

        public decimal getSaldoFlotante()
        {
            return _saldoFlotante;
        }

        public void setSaldoFlotante(decimal pSaldoFlotante)
        {
            _saldoFlotante = pSaldoFlotante;
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
