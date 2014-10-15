using FlexCoreDTOs.clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.cuentas
{
    public class CuentaAhorroVistaDTO : CuentaAhorroDTO
    {
        private List<PhysicalPersonDTO> _listaBeneficiarios;
        private decimal _saldoFlotante;

        public CuentaAhorroVistaDTO() { }

        public CuentaAhorroVistaDTO(string pNumeroCuenta, string pDecripcion, decimal pSaldo, bool pEstado, int pTipoMoneda, ClientVDTO pCliente, decimal pSaldoFlotante,
            List<PhysicalPersonDTO> pListaBeneficiarios) : base(pNumeroCuenta, pDecripcion, pSaldo, pEstado, pTipoMoneda, pCliente)
        {
            _saldoFlotante = pSaldoFlotante;
            _listaBeneficiarios = pListaBeneficiarios;
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
