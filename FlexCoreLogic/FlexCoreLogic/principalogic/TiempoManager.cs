using FlexCoreDAOs.administration;
using FlexCoreDTOs.administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlexCoreLogic.principalogic
{
    public class TiempoManager
    {
        static bool _relojIniciado;
        static bool _cambioDeDia;
        static int _tiempoEspera = 1000;
        static DateTime _horaActual;

        public static void iniciarReloj()
        {
            _horaActual = DateTime.Now;
            _relojIniciado = true;
            _cambioDeDia = false;
            ThreadStart _delegado = new ThreadStart(iniciarRelojAux);
            Thread _hiloReplica = new Thread(_delegado);
            _hiloReplica.Start();
        }

        private static void iniciarRelojAux()
        {
            while(true)
            {
                if(_relojIniciado == true)
                {
                    Thread.Sleep(_tiempoEspera);
                    _horaActual = _horaActual.AddSeconds(1);
                    detectarCambioDeDia();
                    modificarHoraBase();
                }
                else
                {
                    Thread.Sleep(_tiempoEspera);
                }
            }
        }

        private static void detectarCambioDeDia()
        {
            ConfiguracionesQueriesDAO _configuraciones = new ConfiguracionesQueriesDAO();
            List<ConfiguracionesDTO> _listaConfiguraciones = new List<ConfiguracionesDTO>();
            if (_horaActual.Day > _listaConfiguraciones[0].getFechaHoraActual().Day)
            {
                encenderCambioDeDia();
            }
        }

        private static void modificarHoraBase()
        {
            //ACTUALIZAR HORA EN LA BASE
        }

        public static void pausarReloj()
        {
            _relojIniciado = false;
        }

        public static void reanudarReloj()
        {
            _relojIniciado = true;
        }

        public static void agregarSegundos(int pSegundos)
        {
            _horaActual = _horaActual.AddSeconds(pSegundos);
        }

        public static void agregarMinutos(int pMinutos)
        {
            _horaActual = _horaActual.AddMinutes(pMinutos);
        }

        public static void agregarHoras(int pHoras)
        {
            _horaActual = _horaActual.AddHours(pHoras);
        }

        public static void agregarMeses(int pMeses)
        {
            _horaActual = _horaActual.AddMonths(pMeses);
        }

        public static void agregarAnos(int pAnos)
        {
            _horaActual = _horaActual.AddYears(pAnos);
        }

        public static DateTime obtenerHoraActual()
        {
            return _horaActual;
        }

        public static void encenderCambioDeDia()
        {
            _cambioDeDia = true;
        }

        public static void apagarCambioDeDia()
        {
            _cambioDeDia = false;
        }

        public static bool obtenerCambioDeDia()
        {
            return _cambioDeDia;
        }
    }
}
