using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;

namespace ModuloCuentas.Cuentas
{
    internal class CuentaAhorroVista : CuentaAhorro
    {
        private decimal _saldoFlotante;
        private List<PhysicalPersonDTO> _listaBeneficiarios;

        public CuentaAhorroVista(string pNumeroCuenta, string pDescripcion, decimal pSaldo, bool pEstado, int pTipoMoneda, decimal pSaldoFlotante) : 
            base(pNumeroCuenta, pDescripcion, pSaldo, pEstado, pTipoMoneda)
        {
            _saldoFlotante = pSaldoFlotante;
        }

        public decimal getSaldoFlotante()
        {
            return _saldoFlotante;
        }

        public List<PhysicalPersonDTO> getListaBeneficiarios()
        {
            return _listaBeneficiarios;
        }

        public void setListaBeneficiarios(List<PhysicalPersonDTO> pListaBeneficiarios)
        {
            _listaBeneficiarios = pListaBeneficiarios;
        }

        public void setSaldoFlotante(decimal pSaldoFlotante)
        {
            _saldoFlotante = pSaldoFlotante;
        }
    }
}
