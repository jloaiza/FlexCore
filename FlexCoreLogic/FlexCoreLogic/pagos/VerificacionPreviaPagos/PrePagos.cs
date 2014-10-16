using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.cuentas;
using FlexCoreDAOs.administration;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreLogic.cuentas.Managers;

namespace FlexCoreLogic.pagos.VerificacionPreviaPagos
{
    public class PrePagos
    {
        /*
         * Banderas de los requerimientos antes de hacer un pago
         */
        private Boolean _sePuedePagar;
        private Boolean _clientesOK;
        private Boolean _cuentasOrigenOK;
        private Boolean _cuentasDestinoOK;
        private Boolean _DispositivoOK;

        /*
         * Contructor, se asignan valores falsos a las banderas
         */
        public PrePagos() {
            this._sePuedePagar = false;
            this._clientesOK = false;
            this._cuentasOrigenOK = false;
            this._cuentasDestinoOK = false;
            this._DispositivoOK = false;
        }

        /*
         * Este metodo, recibe los parametros necesarios para realizar un pago, pCuentaOrigen, se refiere a la 
         * cuenta del usuario que esta realizando un pago, pCuenta Destino a la cuenta donde se va a hacer el 
         * deposito, y el IdOrigen, se refiere al id del dispositivo con el que se conecta el usuario para
         * realizar el pago.
         * Como es un pago, todo pago tiene un monto, pMonto, es el monto del pago.
         */
        public string iniciarPago(string pCuentaOrigen, string pCuentaDestino, string pIdOrigen, int pMonto)
        {

            FlexCoreDTOs.cuentas.CuentaAhorroVistaDTO nCuentaVista_O = new CuentaAhorroVistaDTO();
            FlexCoreDTOs.cuentas.CuentaAhorroVistaDTO nCuentaVista_D = new CuentaAhorroVistaDTO();
            FlexCoreDTOs.cuentas.CuentaAhorroAutomaticoDTO nCuentaAA_O = new CuentaAhorroAutomaticoDTO();
            FlexCoreDTOs.cuentas.CuentaAhorroAutomaticoDTO nCuentaAA_D = new CuentaAhorroAutomaticoDTO();
            int tCuentaOrigen = this.identificarCuentas(pCuentaOrigen);//verifica el tipo de cuenta
            int tCuentaDestino = this.identificarCuentas(pCuentaDestino);//verifica el tipo de cuenta
            this.verificarDispositivos(pCuentaOrigen, pIdOrigen);
            decimal Monto = (decimal)pMonto;
            if (_clientesOK && _cuentasOrigenOK && _DispositivoOK) {//si se activan las 3 banderas, se puede hacer un pago
                if(tCuentaOrigen == 0 && tCuentaDestino==0){
                    nCuentaAA_O.setNumeroCuenta(pCuentaOrigen);
                    nCuentaAA_D.setNumeroCuenta(pCuentaDestino);
                    FlexCoreLogic.pagos.Facade.FacadePagos.realizarPagoODebitoCuentoAhorroAutomatico(nCuentaAA_O,Monto,nCuentaAA_D);
                }
                else if(tCuentaOrigen == 0 && tCuentaDestino==1){
                    nCuentaAA_O.setNumeroCuenta(pCuentaOrigen);
                    nCuentaVista_D.setNumeroCuenta(pCuentaDestino);
                    FlexCoreLogic.pagos.Facade.FacadePagos.realizarPagoODebitoCuentoAhorroAutomatico(nCuentaAA_O,Monto,nCuentaVista_D);
                }
                else if(tCuentaOrigen == 1 && tCuentaDestino==0){
                    nCuentaVista_O.setNumeroCuenta(pCuentaOrigen);
                    nCuentaAA_D.setNumeroCuenta(pCuentaDestino);
                    FlexCoreLogic.pagos.Facade.FacadePagos.realizarPagoODebitoCuentaAhorroVista(nCuentaVista_O, Monto, nCuentaAA_D);
                }
                else if (tCuentaOrigen == 1 && tCuentaDestino == 1) {
                    nCuentaVista_O.setNumeroCuenta(pCuentaOrigen);
                    nCuentaVista_D.setNumeroCuenta(pCuentaDestino);
                    FlexCoreLogic.pagos.Facade.FacadePagos.realizarPagoODebitoCuentaAhorroVista(nCuentaVista_O,Monto,nCuentaVista_D);
                }
            }
            return "";
        }

        /*
         * Verifica su un dispositivo, esta activo, si esta asociado a una cuenta,  y si existe. 
         */
        public string verificarDispositivos(string pCuentaOrigen, string pIdOrigen)
        {
            String resultado = "Dispositivo";
            CuentaAhorroVistaDTO cuenta = new CuentaAhorroVistaDTO();
            cuenta.setNumeroCuenta(pCuentaOrigen);
            int idCuenta = FlexCoreLogic.cuentas.Facade.FacadeCuentas.obtenerCuentaAhorroVistaID(cuenta);

            List<int> estadoCuentaDisp;
            try
            {
                FlexCoreDAOs.administration.DispositivoCuentaQueriesDAO DispC = new DispositivoCuentaQueriesDAO();
                estadoCuentaDisp = DispC.checkDispositivoCuenta(pCuentaOrigen, idCuenta);
                if (estadoCuentaDisp.Count == 1)
                {
                    if (estadoCuentaDisp[0] == ConstantesDAO.DISPOSITIVONOEXISTE)
                    {
                        resultado += " NO EXISTE!";
                    }
                }
                else if (estadoCuentaDisp.Count == 2)
                {
                    if (estadoCuentaDisp.Contains(ConstantesDAO.DISPOSITIVOEXISTE)
                        && estadoCuentaDisp.Contains(ConstantesDAO.DISPOSITIVOCUENTANOENLAZADOS))
                    {
                        resultado += " EXISTE, Y NO ESTA ENLAZADO CON CUENTAS";
                    }
                }
                else if (estadoCuentaDisp.Count == 3)
                {
                    if (estadoCuentaDisp.Contains(ConstantesDAO.DISPOSITIVOEXISTE)
                        && estadoCuentaDisp.Contains(ConstantesDAO.DISPOSITIVOCUENTAENLAZADOS)
                        && estadoCuentaDisp.Contains(ConstantesDAO.DISPOSITIVOACTIVO))
                    {
                        this._DispositivoOK = true;
                        this._cuentasOrigenOK = true;
                        resultado += " EXISTE, ESTA ENLAZADO, Y ACTIVO"; //incluir en constantes
                    }
                }
                else if (estadoCuentaDisp.Count == 3)
                {
                    if (estadoCuentaDisp.Contains(ConstantesDAO.DISPOSITIVOEXISTE)
                        && estadoCuentaDisp.Contains(ConstantesDAO.DISPOSITIVOCUENTAENLAZADOS)
                        && estadoCuentaDisp.Contains(ConstantesDAO.DISPOSITIVOINACTIVO))
                    {
                        resultado += " EXISTE, ESTA ENLAZADO, NO ACTIVO";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return resultado;
        }

        /*
         * Identifica el tipo de cuenta,  si es una cuenta de Ahorro vista,
         * o si es una cuenta de ahorra automatico
         */
        public int identificarCuentas(string pCuenta) {
            int resultado=-1;
            switch(pCuenta.ElementAt(0)){
                case '6':
                    resultado = 0;
                    break;
                case '7':
                    resultado = 0;
                    break;
                case '8':
                    resultado = 1;
                    break;
                case '9':
                    resultado = 1;
                    break;
            }
            return 0;
        }

        public void registrarPago() {
            
        
        }
    }
}
