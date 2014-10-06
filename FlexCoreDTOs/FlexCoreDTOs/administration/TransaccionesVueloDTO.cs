﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.administration
{
    public class TransaccionesVueloDTO
    {
        private int idTransaccion;
        private String descripcion;
        private DateTime fechaHora;
        private int idCuenta;
        private int tipoTransaccion;

        public TransaccionesVueloDTO(int idTransaccion, String descripcion, DateTime fechaHora, int idCuenta, 
            int tipoTransaccion)
        {
            this.idTransaccion = idTransaccion;
            this.descripcion = descripcion;
            this.fechaHora = fechaHora;
            this.idCuenta = idCuenta;
            this.tipoTransaccion = tipoTransaccion;
        }

        public TransaccionesVueloDTO()
        {
            this.idTransaccion = -1;
            this.descripcion = null;
            this.fechaHora = new DateTime(0,0,0,0,0,0);
            this.idCuenta = -1;
            this.tipoTransaccion = -1;
        }

        public int getIdTransaccion() { return this.idTransaccion; }
        public String getDescripcion() { return this.descripcion; }
        public DateTime getFechaHora() { return this.fechaHora; }
        public int getIdCuenta() { return this.idCuenta; }
        public int getTipoTransaccion() { return this.tipoTransaccion; }
        public void setIdTransaccion(int idTransaccion) { this.idTransaccion = idTransaccion; }
        public void setDescripcion(String descripcion) { this.descripcion = descripcion; }
        public void setFechaHora(DateTime fechaHora) { this.fechaHora = fechaHora; }
        public void setIdCuenta(int idCuenta) { this.idCuenta = idCuenta; }
        public void setTipoTransaccion(int tipoTransaccion) { this.tipoTransaccion = tipoTransaccion; }

    }
}
