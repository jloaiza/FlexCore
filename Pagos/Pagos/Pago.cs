using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;
using FlexCoreDAOs.administration;
using FlexCoreDAOs.clients;





namespace Pagos
{
    class Pago
    {
       //datos de entrada para realizar un pago
        private String _cuentaOrigen;
        private String _cuentaDestino;
        private String _clienteOrigen;
        private String _clienteDestino;
        private String _idOrigen;
        private String _idDestino;
        private String _Monto;

        //Bandera de salida para indicar que un pago se realizo con exito
        private String _PagoExitoso;


        public Pago(String pidOrigen, String pCuentaDestino, String pMonto)
        {
            this._idOrigen = pidOrigen;
            this._idDestino =  null;
            this._Monto = pMonto;
            this._cuentaOrigen = null;
            this._cuentaDestino = pCuentaDestino;
            this._clienteOrigen = null;
            this._cuentaDestino = null;
            this._PagoExitoso = null;
        }




        //UTILIZAR LOS DAO******

        /*
         * es un metodo que describe los procesos que se dan para llevar a cabo las transacciones de 
         * un pago realizado.
         * Los procesos son de verificación de cuentas, persona,  id de los dispositivos involucrados.
         */
        public void iniciarCicloPagos(){
            this.verificarDispositivos();


        
        }

        /*
         * Se encarga de verificar con el id del dispositivo que este está activo.
         */
        public void verificarDispositivos() {
            //asignamos la cuenta del cliente, o del establecimiento donde un usuario realizo una compra
            FlexCoreDTOs.cuentas.CuentaAhorroVistaDTO cuentaCliente = new CuentaAhorroVistaDTO();
            cuentaCliente.setNumeroCuenta(this._cuentaDestino);
            //asignamos la cuenta del usuario que reliza la compra. 
            FlexCoreDTOs.cuentas.CuentaAhorroVistaDTO cuentaUsuario = new CuentaAhorroVistaDTO();

            

        }
        
        /*
         * 
         */
     
        public void realizarTransaccion() {
            
        
        }
       
        /*
         * 
         */
        public String devolverEstadoPago(){



            return null;
        }
    }
}
