using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.administration
{
    public class ErrorDTO
    {
        private int idError;
        private String metodo;
        private int linea;
        private DateTime fechaHora;
        private String descripcion;

        public ErrorDTO(int idError, String metodo, int linea, DateTime fechaHora, String descripcion)
        {
            this.idError = idError;
            this.metodo = metodo;
            this.linea = linea;
            this.fechaHora = fechaHora;
            this.descripcion = descripcion;
        }

        public int getIdError() { return this.idError; }
        public String getMetodo() { return this.metodo; }
        public int getLinea() { return this.linea; }
        public DateTime getFechaHora() { return this.fechaHora; }
        public String getDescripcion() { return this.descripcion; }
        public void setIdError(int idError) { this.idError = idError; }
        public void setMetodo(String metodo) { this.metodo = metodo; }
        public void setLinea(int linea) { this.linea = linea; }
        public void setFechaHora(DateTime fechaHora) { this.fechaHora = fechaHora; }
        public void setDescripcion(String descripcion) { this.descripcion = descripcion; }
    }
}
