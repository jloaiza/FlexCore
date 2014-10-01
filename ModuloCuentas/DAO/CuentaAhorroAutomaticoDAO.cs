using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.Cuentas;
using MySql.Data.MySqlClient;
using ConexionMySQLServer.ConexionMySql;
using ModuloCuentas.Generales;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroAutomaticoDAO
    {
        public static void agregarCuentaAhorroAutomaticoBase(CuentaAhorroAutomatico pCuentaAhorroAutomatico)
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
            _comandoMySQL.Parameters.AddWithValue("@idCuentaAhorro", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico.getNumeroCuenta()));
            _comandoMySQL.Parameters.AddWithValue("@idCuentaDeduccion", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion()));
            _comandoMySQL.Parameters.AddWithValue("@proposito", pCuentaAhorroAutomatico.getProposito());
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);

        }

        public static void modificarCuentaAhorroAutomaticoBase(CuentaAhorroAutomatico pCuentaAhorroAutomatico)
        {
            CuentaAhorroDAO.modificarCuentaAhorro(CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico.getNumeroCuenta()), pCuentaAhorroAutomatico);
            String _query = "UPDATE CUENTA_AHORRO_AUTOMATICO SET TIEMPOAHORRO = @tiempoAhorro, MONTOFINAL = @montoFinal, MONTODEDUCCION = @montoDeduccion, FECHAFINALIZACION = @fechaFinalizacion, IDCUENTADEDUCCION = @idCuentaDeduccion, PROPOSITO = @proposito, WHERE IDCUENTAAHORRO = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@tiempoAhorro", pCuentaAhorroAutomatico.getTiempoAhorro());
            _comandoMySQL.Parameters.AddWithValue("@montoFinal", pCuentaAhorroAutomatico.getMontoAhorro());
            _comandoMySQL.Parameters.AddWithValue("@montoDeduccion", pCuentaAhorroAutomatico.getMontoDeduccion());
            _comandoMySQL.Parameters.AddWithValue("@fechaFinalizacion", pCuentaAhorroAutomatico.getFechaFinalizacion());
            _comandoMySQL.Parameters.AddWithValue("@idCuentaDeduccion", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico.getNumeroCuentaDeduccion()));
            _comandoMySQL.Parameters.AddWithValue("@proposito", pCuentaAhorroAutomatico.getProposito());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroAutomatico.getNumeroCuenta()));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            PeriocidadAhorroDAO.modificarPeriodicidadAhorro(PeriocidadAhorroDAO.obtenerIdPeriodo(pCuentaAhorroAutomatico.getNumeroCuenta()), 
                pCuentaAhorroAutomatico.getMagnitudPeriodoAhorro(), pCuentaAhorroAutomatico.getTipoPeriodo());
        }

        public static void modificarUltimaFechaCobro(string pNumeroCuenta, DateTime pUltimaFechaCobro)
        {
            String _query = "UPDATE CUENTA_AHORRO_AUTOMATICO SET ULTIMAFECHACOBRO = @ultimaFechaCobro WHERE IDCUENTAAHORRO = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@ultimaFechaCobro", pUltimaFechaCobro);
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuenta));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void eliminarCuentaAhorroAutomaticoBase(string pNumeroCuenta)
        {
            int _idPeriodo = PeriocidadAhorroDAO.obtenerIdPeriodo(pNumeroCuenta);
            String _query = "DELETE FROM CUENTA_AHORRO_AUTOMATICO WHERE idCuentaAhorro = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuenta));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            PeriocidadAhorroDAO.eliminarPeriodicidadAhorro(_idPeriodo);
            CuentaAhorroDAO.eliminarCuentaAhorro(CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuenta));
        }

        public static CuentaAhorroAutomatico obtenerCuentaAhorroAutomaticoNumeroCuenta(string pNumeroCuenta)
        {
            CuentaAhorroAutomatico _cuentaSalida = null;
            String _query = "SELECT * FROM CUENTA_AHORRO_AUTOMATICO_V WHERE NUMCUENTA = @numCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pNumeroCuenta);
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
                string _numeroCuentaDeduccion = CuentaAhorroDAO.obtenerNumeroCuenta(Convert.ToInt32(_reader["idCuentaDeduccion"]));
                _cuentaSalida = new CuentaAhorroAutomatico(_numeroCuenta, _descripcion, _saldo, _estado, _tipoMoneda, _fechaInicio, _tiempoAhorro,
                    _fechaFinalizacion, _ultimaFechaCobro, _montoAhorro, _montoDeduccion, _proposito, _magnitudPeriodoAhorro, _tipoPeriodo, _numeroCuentaDeduccion);
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _cuentaSalida;
        }

        public static List<CuentaAhorroAutomatico> obtenerCuentaAhorroAutomaticoNombre(string pNombre)
        {
            //SE OBTIENE LA CUENTA DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroAutomatico obtenerCuentaAhorroAutomaticoCedula(string pCedula)
        {
            //SE OBTIENE LA CUENTA DADA LA CEDULA;
            return null;
        }

        public static CuentaAhorroAutomatico obtenerCuentaAhorroAutomaticoCIF(string pCIF)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static void quitarDinero(string pNumeroCuentaOrigen, decimal pMonto, string pNumeroCuentaDestino, int pTipoCuenta)
        {
            CuentaAhorroAutomatico _cuentaAhorroOrigen = obtenerCuentaAhorroAutomaticoNumeroCuenta(pNumeroCuentaOrigen);
            _cuentaAhorroOrigen.setSaldo(_cuentaAhorroOrigen.getSaldo() - pMonto);
            CuentaAhorroDAO.modificarSaldo(CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuentaOrigen), _cuentaAhorroOrigen.getSaldo());
            agregarDinero(pNumeroCuentaDestino, pMonto, pTipoCuenta);
        }

        public static void agregarDinero(string pNumeroCuenta, decimal pMonto, int pTipoCuenta)
        {
            if (pTipoCuenta == Constantes.AHORROAUTOMATICO)
            {
                agregarDineroAux(pNumeroCuenta, pMonto);
            }
            else if (pTipoCuenta == Constantes.AHORROVISTA)
            {
                CuentaAhorroVistaDAO.agregarDinero(pNumeroCuenta, pMonto, Constantes.AHORROVISTA);
            }
        }

        private static void agregarDineroAux(string pNumeroCuenta, decimal pMonto)
        {
            CuentaAhorroAutomatico _cuentaAhorroAutomatico = obtenerCuentaAhorroAutomaticoNumeroCuenta(pNumeroCuenta);
            _cuentaAhorroAutomatico.setSaldo(_cuentaAhorroAutomatico.getSaldo() + pMonto);
            CuentaAhorroDAO.modificarSaldo(CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuenta), _cuentaAhorroAutomatico.getSaldo());
        }

        public static void modificarEstado(string pNumeroCuenta, bool pEstado)
        {
            CuentaAhorroDAO.modificarEstadoCuentaAhorro(CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuenta), pEstado);
        }
    }
}
