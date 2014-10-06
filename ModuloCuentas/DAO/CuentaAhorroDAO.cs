using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using ModuloCuentas.Generales;
using FlexCoreDTOs.cuentas;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroDAO
    {
        public static void agregarCuentaAhorro(CuentaAhorroDTO pCuentaAhorro)
        {
            String _query = "INSERT INTO CUENTA_AHORRO(NUMCUENTA, DESCRIPCION, SALDO, ACTIVA, IDCLIENTE, TIPOMONEDA) VALUES(@numCuenta, @descripcion, @saldo, @activa, @idCliente, @tipoMoneda);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pCuentaAhorro.getNumeroCuenta());
            _comandoMySQL.Parameters.AddWithValue("@descripcion", pCuentaAhorro.getDescripcion());
            _comandoMySQL.Parameters.AddWithValue("@saldo", pCuentaAhorro.getSaldo());
            _comandoMySQL.Parameters.AddWithValue("@activa", Transformaciones.boolToInt(pCuentaAhorro.getEstado()));
            _comandoMySQL.Parameters.AddWithValue("@idCliente", pCuentaAhorro.getCliente().getClientID());
            _comandoMySQL.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorro.getTipoMoneda());
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }
        

        public static void eliminarCuentaAhorro(CuentaAhorroDTO pCuentaAhorro)
        {
            String _query = "DELETE FROM CUENTA_AHORRO WHERE idCuenta = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", obtenerCuentaAhorroID(pCuentaAhorro));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void modificarCuentaAhorro(CuentaAhorroDTO pCuentaAhorro)
        {
            String _query = "UPDATE CUENTA_AHORRO SET DESCRIPCION = @descripcion, TIPOMONEDA = @tipoMoneda, ACTIVA = @estado WHERE IDCUENTA = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@descripcion", pCuentaAhorro.getDescripcion());
            _comandoMySQL.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorro.getTipoMoneda());
            _comandoMySQL.Parameters.AddWithValue("@estado", pCuentaAhorro.getEstado());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", obtenerCuentaAhorroID(pCuentaAhorro));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void modificarSaldo(CuentaAhorroDTO pCuentaAhorro, decimal pSaldo)
        {
            String _query = "UPDATE CUENTA_AHORRO SET SALDO = @saldo WHERE IDCUENTA = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@saldo", pSaldo);
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", obtenerCuentaAhorroID(pCuentaAhorro));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static bool existeCuenta(string pNumeroCuenta)
        {
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V WHERE NUMCUENTA = @numCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pNumeroCuenta);
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            if(_reader.Read())
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

        public static int obtenerCuentaAhorroID(CuentaAhorroDTO pCuentaAhorro)
        {
            int _idCuenta = 0;
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE NUMCUENTA = @numCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pCuentaAhorro.getNumeroCuenta());
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            if(_reader.Read())
            {
                _idCuenta = Convert.ToInt32(_reader["idCuenta"]);
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _idCuenta;
        }

        public static int obtenerCuentaAhorroMoneda(CuentaAhorroDTO pCuentaAhorro)
        {
            int _moneda = 0;
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE NUMCUENTA = @numCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pCuentaAhorro.getNumeroCuenta());
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            if (_reader.Read())
            {
                _moneda = Convert.ToInt32(_reader["tipoMoneda"]);
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _moneda;
        }

        public static string obtenerNumeroCuenta(int pIdCuenta)
        {
            string _numeroCuenta = "";
            String _query = "SELECT * FROM CUENTA_AHORRO WHERE IDCUENTA = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            if(_reader.Read())
            {
                _numeroCuenta = _reader["numCuenta"].ToString();
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _numeroCuenta;
        }
    }
}
