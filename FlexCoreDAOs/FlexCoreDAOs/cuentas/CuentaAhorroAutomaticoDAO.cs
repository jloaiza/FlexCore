using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ConexionMySQLServer.ConexionMySql;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;

namespace FlexCoreDAOs.cuentas
{
    public static class CuentaAhorroAutomaticoDAO
    {
        public static void agregarCuentaAhorroAutomaticoBase(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, MySqlCommand pComando)
        {
            CuentaAhorroDAO.agregarCuentaAhorro(pCuentaAhorroAutomatico, pComando);
            CuentaAhorroAutomaticoDTO _cuentaDeduccion = new CuentaAhorroAutomaticoDTO();
            _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
            int _lastId = PeriocidadAhorroDAO.agregarMagnitudPeriocidad(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pComando);
            int _ahorroId = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico, pComando);
            int _deduccionId = CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaDeduccion, pComando);
            String _query = "INSERT INTO CUENTA_AHORRO_AUTOMATICO(FECHAINICIO, FECHAFINALIZACION, ULTIMAFECHACOBRO, TIEMPOAHORRO, MONTODEDUCCION, MONTOFINAL, PERIODICIDAD, IDCUENTAAHORRO, IDCUENTADEDUCCION, PROPOSITO) VALUES(@fechaInicio, @fechaFinalizacion, @ultimaFechaCobro, @tiempoAhorro, @montoDeduccion, @montoFinal, @periodicidad, @idCuentaAhorro, @idCuentaDeduccion, @proposito);";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@fechaInicio", pCuentaAhorroAutomatico.getFechaInicio());
            pComando.Parameters.AddWithValue("@fechaFinalizacion", pCuentaAhorroAutomatico.getFechaFinalizacion());
            pComando.Parameters.AddWithValue("@ultimaFechaCobro", pCuentaAhorroAutomatico.getUltimaFechaCobro());
            pComando.Parameters.AddWithValue("@tiempoAhorro", pCuentaAhorroAutomatico.getTiempoAhorro());
            pComando.Parameters.AddWithValue("@montoDeduccion", pCuentaAhorroAutomatico.getMontoDeduccion());
            pComando.Parameters.AddWithValue("@montoFinal", pCuentaAhorroAutomatico.getMontoAhorro());
            pComando.Parameters.AddWithValue("@periodicidad", _lastId);
            pComando.Parameters.AddWithValue("@idCuentaAhorro", _ahorroId);
            pComando.Parameters.AddWithValue("@idCuentaDeduccion", _deduccionId);
            pComando.Parameters.AddWithValue("@proposito", pCuentaAhorroAutomatico.getProposito());
            pComando.ExecuteNonQuery();
        }

        public static void modificarCuentaAhorroAutomaticoBase(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, MySqlCommand pComando)
        {
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroAutomatico, pComando);
            CuentaAhorroAutomaticoDTO _cuentaDeduccion = new CuentaAhorroAutomaticoDTO();
            _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
            int _idCuentaDeduccion = CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaDeduccion, pComando);
            int _idCuentaAhorro = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico, pComando);
            String _query = "UPDATE CUENTA_AHORRO_AUTOMATICO SET TIEMPOAHORRO = @tiempoAhorro, MONTOFINAL = @montoFinal, MONTODEDUCCION = @montoDeduccion, FECHAFINALIZACION = @fechaFinalizacion, IDCUENTADEDUCCION = @idCuentaDeduccion, PROPOSITO = @proposito WHERE IDCUENTAAHORRO = @idCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@tiempoAhorro", pCuentaAhorroAutomatico.getTiempoAhorro());
            pComando.Parameters.AddWithValue("@montoFinal", pCuentaAhorroAutomatico.getMontoAhorro());
            pComando.Parameters.AddWithValue("@montoDeduccion", pCuentaAhorroAutomatico.getMontoDeduccion());
            pComando.Parameters.AddWithValue("@fechaFinalizacion", pCuentaAhorroAutomatico.getFechaFinalizacion());
            pComando.Parameters.AddWithValue("@idCuentaDeduccion", _idCuentaDeduccion);
            pComando.Parameters.AddWithValue("@proposito", pCuentaAhorroAutomatico.getProposito());
            pComando.Parameters.AddWithValue("@idCuenta", _idCuentaAhorro);
            pComando.ExecuteNonQuery();
            PeriocidadAhorroDAO.modificarPeriodicidadAhorro(PeriocidadAhorroDAO.obtenerIdPeriodo(pCuentaAhorroAutomatico.getNumeroCuenta(), pComando), 
                pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo(), pComando);
        }

        public static void modificarUltimaFechaCobro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, DateTime pUltimaFechaCobro, MySqlCommand pComando)
        {
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico, pComando);
            String _query = "UPDATE CUENTA_AHORRO_AUTOMATICO SET ULTIMAFECHACOBRO = @ultimaFechaCobro WHERE IDCUENTAAHORRO = @idCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@ultimaFechaCobro", pUltimaFechaCobro);
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
        }

        public static void eliminarCuentaAhorroAutomaticoBase(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, MySqlCommand pComando)
        {
            int _idPeriodo = PeriocidadAhorroDAO.obtenerIdPeriodo(pCuentaAhorroAutomatico.getNumeroCuenta(), pComando);
            int _idCuenta = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico, pComando);
            String _query = "DELETE FROM CUENTA_AHORRO_AUTOMATICO WHERE idCuentaAhorro = @idCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@idCuenta", _idCuenta);
            pComando.ExecuteNonQuery();
            PeriocidadAhorroDAO.eliminarPeriodicidadAhorro(_idPeriodo, pComando);
            CuentaAhorroDAO.eliminarCuentaAhorro(pCuentaAhorroAutomatico, pComando);
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoNumeroCuenta(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, MySqlCommand pComando)
        {
            CuentaAhorroAutomaticoDTO _cuentaSalida = null;
            int _idCuentaDeduccion = 0;
            String _query = "SELECT * FROM CUENTA_AHORRO_AUTOMATICO_V WHERE NUMCUENTA = @numCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@numCuenta", pCuentaAhorroAutomatico.getNumeroCuenta());
            MySqlDataReader _reader = pComando.ExecuteReader();
            if(_reader.Read())
            {
                string _numeroCuenta = _reader["numCuenta"].ToString();
                string _descripcion = _reader["descripcion"].ToString();
                decimal _saldo = Convert.ToDecimal(_reader["saldo"]);
                bool _estado = Transformaciones.intToBool(Convert.ToInt32(_reader["activa"]));
                int _tipoMoneda = Convert.ToInt32(_reader["idMoneda"]);
                DateTime _fechaInicio = Convert.ToDateTime(_reader["fechaInicio"]);
                int _tiempoAhorro = Convert.ToInt32(_reader["tiempoAhorro"]);
                DateTime _fechaFinalizacion = Convert.ToDateTime(_reader["fechaFinalizacion"]);
                DateTime _ultimaFechaCobro = Convert.ToDateTime(_reader["ultimaFechaCobro"]);
                decimal _montoAhorro = Convert.ToDecimal(_reader["montoFinal"]);
                decimal _montoDeduccion = Convert.ToDecimal(_reader["montoDeduccion"]);
                int _proposito = Convert.ToInt32(_reader["idProposito"]);
                int _magnitudPeriodoAhorro = Convert.ToInt32(_reader["periodicidad"]);
                int _tipoPeriodo = Convert.ToInt32(_reader["idTipoPeriodo"]);
                int _idCliente = Convert.ToInt32(_reader["idCliente"]);
                _idCuentaDeduccion = Convert.ToInt32(_reader["idCuentaDeduccion"]);
                ClientVDTO _cliente = new ClientVDTO();
                _cliente.setClientID(_idCliente);
                _cuentaSalida = new CuentaAhorroAutomaticoDTO(_numeroCuenta, _descripcion, _saldo, _estado, _tipoMoneda, _cliente,_fechaInicio, _tiempoAhorro,
                    _fechaFinalizacion, _ultimaFechaCobro, _montoAhorro, _montoDeduccion, _proposito, _magnitudPeriodoAhorro, _tipoPeriodo, "");
            }
            _reader.Close();
            _cuentaSalida.setNumeroCuentaDeduccion(CuentaAhorroDAO.obtenerNumeroCuenta(_idCuentaDeduccion, pComando));
            return _cuentaSalida;
        }

        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoNombre(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, int pNumeroPagina, int pCantidadElementos)
        {
            //SE OBTIENE LA CUENTA DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCedula(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            return null;
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCIF(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static void quitarDinero(CuentaAhorroDTO pCuentaOrigen, decimal pMonto, CuentaAhorroDTO pCuentaDestino, int pTipoCuenta, MySqlCommand pComando)
        {
            CuentaAhorroAutomaticoDTO _cuentaOrigenEntrada = new CuentaAhorroAutomaticoDTO();
            _cuentaOrigenEntrada.setNumeroCuenta(pCuentaOrigen.getNumeroCuenta());
            CuentaAhorroAutomaticoDTO _cuentaAhorroOrigen = obtenerCuentaAhorroAutomaticoNumeroCuenta(_cuentaOrigenEntrada, pComando);
            decimal _montoDeduccion = Transformaciones.convertirDinero(pMonto, _cuentaAhorroOrigen.getTipoMoneda(), CuentaAhorroDAO.obtenerCuentaAhorroMoneda(pCuentaDestino, pComando));
            _cuentaAhorroOrigen.setSaldo(_cuentaAhorroOrigen.getSaldo() - _montoDeduccion);
            CuentaAhorroDAO.modificarSaldo(_cuentaAhorroOrigen, _cuentaAhorroOrigen.getSaldo(), pComando);
            agregarDinero(pCuentaDestino, pMonto, pTipoCuenta, pComando);
        }

        public static void agregarDinero(CuentaAhorroDTO pCuentaAhorro, decimal pMonto, int pTipoCuenta, MySqlCommand pComando)
        {
            if (pTipoCuenta == Constantes.AHORROAUTOMATICO)
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomatico.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                agregarDineroAux(_cuentaAhorroAutomatico, pMonto, pComando);
            }
            else if (pTipoCuenta == Constantes.AHORROVISTA)
            {
                CuentaAhorroVistaDTO _cuentaAhorroVista = new CuentaAhorroVistaDTO();
                _cuentaAhorroVista.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                CuentaAhorroVistaDAO.agregarDinero(_cuentaAhorroVista, pMonto, Constantes.AHORROVISTA, pComando);
            }
        }

        private static void agregarDineroAux(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, decimal pMonto, MySqlCommand pComando)
        {
            CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico, pComando);
            _cuentaAhorroAutomatico.setSaldo(_cuentaAhorroAutomatico.getSaldo() + pMonto);
            CuentaAhorroDAO.modificarSaldo(_cuentaAhorroAutomatico, _cuentaAhorroAutomatico.getSaldo(), pComando);
        }
    }
}
