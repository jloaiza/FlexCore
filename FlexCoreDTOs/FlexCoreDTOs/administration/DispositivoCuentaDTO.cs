using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.administration
{
    public class DispositivoCuentaDTO
    {
        private int idDispositivoCuenta;
        private String idDispositivo;
        private bool activo;
        private int idCuenta;

        public DispositivoCuentaDTO(int idDispositivoCuenta, String idDispositivo, bool activo, int idCuenta)
        {
            this.idDispositivoCuenta = idDispositivoCuenta;
            this.idDispositivo = idDispositivo;
            this.activo = activo;
            this.idCuenta = idCuenta;
        }

        public int getIdDispositivoCuenta() { return this.idDispositivoCuenta; }
        public String getIdDispositivo() { return this.idDispositivo; }
        public bool getActivo() { return this.activo; }
        public int getIdCuenta() { return this.idCuenta; }
        public void setIdDispositivoCuenta(int idDispositivoCuenta) { this.idDispositivoCuenta = idDispositivoCuenta; }
        public void setIdDispositivo(String idDispositivo) { this.idDispositivo = idDispositivo; }
        public void setActivo(bool activo) { this.activo = activo; }
        public void setIdCuenta(int idCuenta) { this.idCuenta = idCuenta; }
    }
}
