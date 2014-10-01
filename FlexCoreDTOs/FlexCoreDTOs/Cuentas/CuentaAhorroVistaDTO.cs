using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.Cuentas
{
    public class CuentaAhorroVistaDTO : CuentaAhorroDTO
    {
        //RECORDAR QUE TAMBIEN VOY A RECIBIR UN CLIENTE DUEÑO DE LA CUENTA Y UNA LISTA DE CLIENTES DTO COMO BENEFICIARIOS.
        //HAY QUE MODIFICAR LO NECESARIO DONDE CORRESPONDE 

        private decimal _saldoFlotante;
       
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
