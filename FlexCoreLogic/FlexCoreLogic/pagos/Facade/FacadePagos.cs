using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.pagos.Managers;

namespace FlexCoreLogic.pagos.Facade
{
    public class FacadePagos
    {
        public static string realizarPagoODebitoCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            return PagosManager.realizarPagoODebito(pCuentaAhorroVistaOrigen, pMonto, pCuentaAhorroVistaDestino);
        }

        public static string realizarPagoODebitoCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            return PagosManager.realizarPagoODebito(pCuentaAhorroVistaOrigen, pMonto, pCuentaAhorroAutomaticoDestino);
        }

        public static string realizarPagoODebitoCuentoAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            return PagosManager.realizarPagoODebito(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroAutomaticoDestino);
        }

        public static string realizarPagoODebitoCuentoAhorroAutomatico(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            return PagosManager.realizarPagoODebito(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroVistaDestino);
        }
    }
}
