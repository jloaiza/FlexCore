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
            String _query = "INSERT INTO CUENTA_AHORRO(NUMCUENTA, DESCRIPCION, SALDO, ACTIVA, IDCLIENTE, TIPOMONEDA) VALUES(@numCuenta, @descripcion, @saldo, @activa, @idCliente, @tipoMoneda);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pCuentaAhorroVista.getNumeroCuenta());
            _comandoMySQL.Parameters.AddWithValue("@descripcion", pCuentaAhorroVista.getDescripcion());
            _comandoMySQL.Parameters.AddWithValue("@saldo", pCuentaAhorroVista.getSaldo());
            _comandoMySQL.Parameters.AddWithValue("@activa", Transformaciones.boolToInt(pCuentaAhorroVista.getEstado()));
            _comandoMySQL.Parameters.AddWithValue("@idCliente", "1");
            _comandoMySQL.Parameters.AddWithValue("@tipoMoneda", pCuentaAhorroVista.getTipoMoneda());
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void eliminarCuentaAhorro(string pIdCuenta)
        {
            String _query = "DELETE FROM CUENTA_AHORRO WHERE idCuenta = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void modificarCuentaAhorro(string pIdCuenta, CuentaAhorroVista pCuentaAhorroVista)
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

        public static void modificarSaldo(string pIdCuenta, decimal pSaldo)
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
    }
}
