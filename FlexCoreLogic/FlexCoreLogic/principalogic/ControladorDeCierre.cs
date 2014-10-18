using FlexCoreLogic.cuentas.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexCoreLogic.principalogic
{
    public static class ControladorDeCierre
    {
        static int _tiempoEspera = 1000;

        public static void iniciarControladorDeCierre()
        {
            ThreadStart _delegado = new ThreadStart(iniciarControladorDeCierreAux);
            Thread _hiloReplica = new Thread(_delegado);
            _hiloReplica.Start();
        }

        private static void iniciarControladorDeCierreAux()
        {
            while(true)
            {
                if(TiempoManager.obtenerCambioDeDia())
                {
                    iniciarCierre();
                    TiempoManager.apagarCambioDeDia();
                    Thread.Sleep(_tiempoEspera);
                }
                else
                {
                    Thread.Sleep(_tiempoEspera);
                }
            }
        }

        public static void iniciarCierre()
        {
            FacadeCuentas.realizarCierreCuentas();
        }
    }
}
