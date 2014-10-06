using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.administration
{
    public class PagoInteresesDTO
    {
        private int idPago;
        private Decimal monto;
        private int idCuenta;
        private int idCierre;

        public PagoInteresesDTO(int idPago, Decimal monto, int idCuenta, int idCierre)
        {
            this.idPago = idPago;
            this.monto = monto;
            this.idCuenta = idCuenta;
            this.idCierre = idCierre;
        }

        public int getIdPago() { return this.idPago; }
        public Decimal getMonto() { return this.monto; }
        public int getIdCuenta() { return this.idCuenta; }
        public int getIdCierre() { return this.idCierre; }
        public void setIdPago(int idPago) { this.idPago = idPago; }
        public void setMonto(Decimal monto) { this.monto = monto; }
        public void setIdCuenta(int idCuenta) { this.idCuenta = idCuenta; }
        public void setIdCierre(int idCierre) { this.idCierre = idCierre; }
    }
}
