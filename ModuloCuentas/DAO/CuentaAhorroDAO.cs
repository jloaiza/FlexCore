using ModuloCuentas.Cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using ModuloCuentas.Generales;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroDAO
    {
        public static void agregarCuentaAhorro(CuentaAhorroVista pCuentaAhorroVista)
        {
            String _query = "INSERT INTO CUENTA_AHORRO(NUMCUENTA, DESCRIPCION, SALDO, ACTIVA, IDCLIENTE, TIPOMONEDA, BLOQUEADA) VALUES(@numCuenta, @descripcion, @saldo, @activa, @idCliente, @tipoMoneda, @bloqueada);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pCuentaAhorroVista.getNumeroCuenta());
            _comandoMySQL.Parameters.AddWithValue("@descripcion", pCuentaAhorroVista.getDescripcion());
            _comandoMySQL.Parameters.AddWithValue("@saldo", pCuentaAhorroVista.getSaldo());
            _comandoMySQL.Parameters.AddWithValue("@activa", Transformaciones.boolToInt(pCuentaAhorroVista.getEstado()));
            _comandoMySQL.Parameters.AddWithValue("@idCliente", "1");
            _comandoMySQL.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorroVista.getTipoMoneda());
            _comandoMySQL.Parameters.AddWithValue("@bloqueada", "0");
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void agregarCuentaAhorro(CuentaAhorroAutomatico pCuentaAhorroAutomatico)
        {
            String _query = "INSERT INTO CUENTA_AHORRO(NUMCUENTA, DESCRIPCION, SALDO, ACTIVA, IDCLIENTE, TIPOMONEDA, BLOQUEADA) VALUES(@numCuenta, @descripcion, @saldo, @activa, @idCliente, @tipoMoneda, @bloqueada);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pCuentaAhorroAutomatico.getNumeroCuenta());
            _comandoMySQL.Parameters.AddWithValue("@descripcion", pCuentaAhorroAutomatico.getDescripcion());
            _comandoMySQL.Parameters.AddWithValue("@saldo", pCuentaAhorroAutomatico.getSaldo());
            _comandoMySQL.Parameters.AddWithValue("@activa", Transformaciones.boolToInt(pCuentaAhorroAutomatico.getEstado()));
            _comandoMySQL.Parameters.AddWithValue("@idCliente", "2");
            _comandoMySQL.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorroAutomatico.getTipoMoneda());
            _comandoMySQL.Parameters.AddWithValue("@bloqueada", "0");
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void eliminarCuentaAhorro(int pIdCuenta)
        {
            String _query = "DELETE FROM CUENTA_AHORRO WHERE idCuenta = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void modificarCuentaAhorro(int pIdCuenta, CuentaAhorroVista pCuentaAhorroVista)
        {
            String _query = "UPDATE CUENTA_AHORRO SET DESCRIPCION = @descripcion, TIPOMONEDA = @tipoMoneda, ACTIVA = @estado WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@descripcion", pCuentaAhorroVista.getDescripcion());
            _comandoMySQL.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorroVista.getTipoMoneda());
            _comandoMySQL.Parameters.AddWithValue("@estado", pCuentaAhorroVista.getEstado());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void modificarEstadoCuentaAhorro(int pIdCuenta, bool pEstado)
        {
            String _query = "UPDATE CUENTA_AHORRO SET ACTIVA = @estado WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@estado", pEstado);
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void modificarCuentaAhorro(int pIdCuenta, CuentaAhorroAutomatico pCuentaAhorroAutomatico)
        {
            String _query = "UPDATE CUENTA_AHORRO SET DESCRIPCION = @descripcion, TIPOMONEDA = @tipoMoneda WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@descripcion", pCuentaAhorroAutomatico.getDescripcion());
            _comandoMySQL.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorroAutomatico.getTipoMoneda());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void modificarSaldo(int pIdCuenta, decimal pSaldo)
        {
            String _query = "UPDATE CUENTA_AHORRO SET SALDO = @saldo WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@saldo", pSaldo);
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static bool existeCuenta(string pNumeroCuenta)
        {
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V WHERE NUMCUENTA = @numCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pNumeroCuenta);
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            _reader.Read();
            if(_reader.HasRows)
            {
                MySQLManager.cerrarConexion(_conexionMySQLBase);
                return true;
            }
            else
            {
                MySQLManager.cerrarConexion(_conexionMySQLBase);
                return false;
            }
        }

        public static int obtenerCuentaAhorroID(string pNumeroCuenta)
        {
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE NUMCUENTA = @numCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pNumeroCuenta);
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            _reader.Read();
            int _idCuenta = Convert.ToInt32(_reader["idCuenta"]);
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _idCuenta;
        }

        public static string obtenerNumeroCuenta(int pIdCuenta)
        {
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            _reader.Read();
            string _numeroCuenta = _reader["numCuenta"].ToString();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _numeroCuenta;
        }
    }
}
