using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCore.DTO
{
    public class TipoTransaccionDTO
    {
        private int idTipo;
        private String descripcion;

        public TipoTransaccionDTO(int idTipo, String descripcion)
        {
            this.idTipo = idTipo;
            this.descripcion = descripcion;
        }
        public TipoTransaccionDTO()
        {
            this.idTipo = -1;
            this.descripcion = null;
        }

        public int getIdTipo(){ return this.idTipo;}
        public String getDescripcion(){return this.descripcion;}
        public void setIdTipo(int idTipo) { this.idTipo = idTipo; }
        public void setDescripcion(String descripcion) { this.descripcion = descripcion; }
    }
}
