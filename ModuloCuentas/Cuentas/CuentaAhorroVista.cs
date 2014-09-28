using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloCuentas.Cuentas
{
    internal class CuentaAhorroVista : CuentaAhorro
    {
        private decimal _saldoFlotante;

        public CuentaAhorroVista(string pNumeroCuenta, string pDescripcion, decimal pSaldo, bool pEstado, int pTipoMoneda, decimal pSaldoFlotante) : 
            base(pNumeroCuenta, pDescripcion, pSaldo, pEstado, pTipoMoneda)
        {
            _saldoFlotante = pSaldoFlotante;
        }

        public decimal getSaldoFlotante()
        {
            return _saldoFlotante;
        }

        public void setSaldoFlotante(decimal pSaldoFlotante)
        {
            _saldoFlotante = pSaldoFlotante;
        }
    }
}
