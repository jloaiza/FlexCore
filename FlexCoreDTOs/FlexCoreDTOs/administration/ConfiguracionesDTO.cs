using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCore1.DTO
{
    public class ConfiguracionesDTO
    {
        private DateTime fechaHoraSistema;
        private Decimal compraDolar;
        private Decimal ventaDolar;
        private Decimal tasaInteresAhorro;

        public ConfiguracionesDTO(Decimal compraDolar, Decimal ventaDolar, DateTime fechaHoraSistema,
            Decimal tasaInteresAhorro)
        {
            this.compraDolar = compraDolar;
            this.ventaDolar = ventaDolar;
            this.fechaHoraSistema = fechaHoraSistema;
            this.tasaInteresAhorro = tasaInteresAhorro;
        }

        public DateTime getFechaHoraActual() { return this.fechaHoraSistema; }
        public Decimal getCompraDolar() { return this.compraDolar; }
        public Decimal getVentaDolar() { return this.ventaDolar; }
        public Decimal getTasaInteresAhorro() { return this.tasaInteresAhorro; }
    }
}
