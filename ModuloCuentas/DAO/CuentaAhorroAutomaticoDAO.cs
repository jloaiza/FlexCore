using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ConexionMySQLServer.ConexionMySql;
using ModuloCuentas.Generales;
using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroAutomaticoDAO
    {
        public static void agregarCuentaAhorroAutomaticoBase(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            CuentaAhorroDAO.agregarCuentaAhorro(pCuentaAhorroAutomatico);
            int _lastId = PeriocidadAhorroDAO.agregarMagnitudPeriocidad(pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo());
            String _query = "INSERT INTO CUENTA_AHORRO_AUTOMATICO(FECHAINICIO, FECHAFINALIZACION, ULTIMAFECHACOBRO, TIEMPOAHORRO, MONTODEDUCCION, MONTOFINAL, PERIODICIDAD, IDCUENTAAHORRO, IDCUENTADEDUCCION, PROPOSITO) VALUES(@fechaInicio, @fechaFinalizacion, @ultimaFechaCobro, @tiempoAhorro, @montoDeduccion, @montoFinal, @periodicidad, @idCuentaAhorro, @idCuentaDeduccion, @proposito);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@fechaInicio", pCuentaAhorroAutomatico.getFechaInicio());
            _comandoMySQL.Parameters.AddWithValue("@fechaFinalizacion", pCuentaAhorroAutomatico.getFechaFinalizacion());
            _comandoMySQL.Parameters.AddWithValue("@ultimaFechaCobro", pCuentaAhorroAutomatico.getUltimaFechaCobro());
            _comandoMySQL.Parameters.AddWithValue("@tiempoAhorro", pCuentaAhorroAutomatico.getTiempoAhorro());
            _comandoMySQL.Parameters.AddWithValue("@montoDeduccion", pCuentaAhorroAutomatico.getMontoDeduccion());
            _comandoMySQL.Parameters.AddWithValue("@montoFinal", pCuentaAhorroAutomatico.getMontoAhorro());
            _comandoMySQL.Parameters.AddWithValue("@periodicidad", _lastId);
            _comandoMySQL.Parameters.AddWithValue("@idCuentaAhorro", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico));
            CuentaAhorroAutomaticoDTO _cuentaDeduccion = new CuentaAhorroAutomaticoDTO();
            _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
            _comandoMySQL.Parameters.AddWithValue("@idCuentaDeduccion", CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaDeduccion));
            _comandoMySQL.Parameters.AddWithValue("@proposito", pCuentaAhorroAutomatico.getProposito());
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);

        }

        public static void modificarCuentaAhorroAutomaticoBase(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroAutomatico);
            String _query = "UPDATE CUENTA_AHORRO_AUTOMATICO SET TIEMPOAHORRO = @tiempoAhorro, MONTOFINAL = @montoFinal, MONTODEDUCCION = @montoDeduccion, FECHAFINALIZACION = @fechaFinalizacion, IDCUENTADEDUCCION = @idCuentaDeduccion, PROPOSITO = @proposito, WHERE IDCUENTAAHORRO = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@tiempoAhorro", pCuentaAhorroAutomatico.getTiempoAhorro());
            _comandoMySQL.Parameters.AddWithValue("@montoFinal", pCuentaAhorroAutomatico.getMontoAhorro());
            _comandoMySQL.Parameters.AddWithValue("@montoDeduccion", pCuentaAhorroAutomatico.getMontoDeduccion());
            _comandoMySQL.Parameters.AddWithValue("@fechaFinalizacion", pCuentaAhorroAutomatico.getFechaFinalizacion());
            CuentaAhorroAutomaticoDTO _cuentaDeduccion = new CuentaAhorroAutomaticoDTO();
            _cuentaDeduccion.setNumeroCuenta(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion());
            _comandoMySQL.Parameters.AddWithValue("@idCuentaDeduccion", CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaDeduccion));
            _comandoMySQL.Parameters.AddWithValue("@proposito", pCuentaAhorroAutomatico.getProposito());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            PeriocidadAhorroDAO.modificarPeriodicidadAhorro(PeriocidadAhorroDAO.obtenerIdPeriodo(pCuentaAhorroAutomatico.getNumeroCuenta()), 
                pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo());
        }

        public static void modificarUltimaFechaCobro(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, DateTime pUltimaFechaCobro)
        {
            String _query = "UPDATE CUENTA_AHORRO_AUTOMATICO SET ULTIMAFECHACOBRO = @ultimaFechaCobro WHERE IDCUENTAAHORRO = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@ultimaFechaCobro", pUltimaFechaCobro);
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void eliminarCuentaAhorroAutomaticoBase(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            int _idPeriodo = PeriocidadAhorroDAO.obtenerIdPeriodo(pCuentaAhorroAutomatico.getNumeroCuenta());
            String _query = "DELETE FROM CUENTA_AHORRO_AUTOMATICO WHERE idCuentaAhorro = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            PeriocidadAhorroDAO.eliminarPeriodicidadAhorro(_idPeriodo);
            CuentaAhorroDAO.eliminarCuentaAhorro(pCuentaAhorroAutomatico);
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoNumeroCuenta(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            CuentaAhorroAutomaticoDTO _cuentaSalida = null;
            String _query = "SELECT * FROM CUENTA_AHORRO_AUTOMATICO_V WHERE NUMCUENTA = @numCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pCuentaAhorroAutomatico.getNumeroCuenta());
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
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
                string _numeroCuentaDeduccion = CuentaAhorroDAO.obtenerNumeroCuenta(Convert.ToInt32(_reader["idCuentaDeduccion"]));
                ClientDTO _cliente = new ClientDTO(_idCliente, "");
                _cuentaSalida = new CuentaAhorroAutomaticoDTO(_numeroCuenta, _descripcion, _saldo, _estado, _tipoMoneda, _cliente,_fechaInicio, _tiempoAhorro,
                    _fechaFinalizacion, _ultimaFechaCobro, _montoAhorro, _montoDeduccion, _proposito, _magnitudPeriodoAhorro, _tipoPeriodo, _numeroCuentaDeduccion);
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _cuentaSalida;
        }

        public static List<CuentaAhorroAutomaticoDTO> obtenerCuentaAhorroAutomaticoNombre(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, int pNumeroPagina, int pCantidadElementos)
        {
            //SE OBTIENE LA CUENTA DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCedula(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            //SE OBTIENE LA CUENTA DADA LA CEDULA;
            return null;
        }

        public static CuentaAhorroAutomaticoDTO obtenerCuentaAhorroAutomaticoCIF(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static void quitarDinero(CuentaAhorroDTO pCuentaOrigen, decimal pMonto, CuentaAhorroDTO pCuentaDestino, int pTipoCuenta)
        {
            CuentaAhorroAutomaticoDTO _cuentaOrigenEntrada = new CuentaAhorroAutomaticoDTO();
            _cuentaOrigenEntrada.setNumeroCuenta(pCuentaOrigen.getNumeroCuenta());
            CuentaAhorroAutomaticoDTO _cuentaAhorroOrigen = obtenerCuentaAhorroAutomaticoNumeroCuenta(_cuentaOrigenEntrada);
            decimal _montoDeduccion = Transformaciones.convertirDinero(pMonto, _cuentaAhorroOrigen.getTipoMoneda(), CuentaAhorroDAO.obtenerCuentaAhorroMoneda(pCuentaDestino));
            _cuentaAhorroOrigen.setSaldo(_cuentaAhorroOrigen.getSaldo() - _montoDeduccion);
            CuentaAhorroDAO.modificarSaldo(_cuentaAhorroOrigen, _cuentaAhorroOrigen.getSaldo());
            agregarDinero(pCuentaDestino, pMonto, pTipoCuenta);
        }

        public static void agregarDinero(CuentaAhorroDTO pCuentaAhorro, decimal pMonto, int pTipoCuenta)
        {
            if (pTipoCuenta == Constantes.AHORROAUTOMATICO)
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomatico.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                agregarDineroAux(_cuentaAhorroAutomatico, pMonto);
            }
            else if (pTipoCuenta == Constantes.AHORROVISTA)
            {
                CuentaAhorroVistaDTO _cuentaAhorroVista = new CuentaAhorroVistaDTO();
                _cuentaAhorroVista.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                CuentaAhorroVistaDAO.agregarDinero(_cuentaAhorroVista, pMonto, Constantes.AHORROVISTA);
            }
        }

        private static void agregarDineroAux(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomatico, decimal pMonto)
        {
            CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomatico);
            _cuentaAhorroAutomatico.setSaldo(_cuentaAhorroAutomatico.getSaldo() + pMonto);
            CuentaAhorroDAO.modificarSaldo(_cuentaAhorroAutomatico, _cuentaAhorroAutomatico.getSaldo());
        }
    }
}
