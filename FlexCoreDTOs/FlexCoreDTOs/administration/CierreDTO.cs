using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCore.administration
{
    public class CierreDTO
    {
        private int idCierre;
        private DateTime fechaHora;
        private bool estado;

        public CierreDTO(int idCierre, DateTime fechaHora, bool estado)
        {
            this.idCierre = idCierre;
            this.fechaHora = fechaHora;
            this.estado = estado;
        }

        public int getIdCierre() { return this.idCierre; }
        public DateTime getFechaHora() { return this.fechaHora; }
        public bool getEstado() { return this.estado; }
        public void setIdCierre(int idCierre) { this.idCierre = idCierre; }
        public void setFechaHora(DateTime fechaHora) { this.fechaHora = fechaHora; }
        public void setEstado(bool estado) { this.estado = estado; }
    }
}
