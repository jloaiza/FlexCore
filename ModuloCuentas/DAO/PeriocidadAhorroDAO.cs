using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ConexionMySQLServer.ConexionMySql;

namespace ModuloCuentas.DAO
{
    internal class PeriocidadAhorroDAO
    {
        public static int agregarMagnitudPeriocidad(int pMagnitud, int pTipoPeriodo)
        {
            String _query = "INSERT INTO PERIODICIDAD_AHORRO(MAGNITUD, TIPO) VALUES(@magnitud, @tipo);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@magnitud", pMagnitud);
            _comandoMySQL.Parameters.AddWithValue("@tipo", pTipoPeriodo);
            _comandoMySQL.ExecuteNonQuery();
            int _lastId = Convert.ToInt32(_comandoMySQL.LastInsertedId);
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _lastId;
        }

        public static void eliminarPeriodicidadAhorro(int pIdPeriodicidad)
        {
            String _query = "DELETE FROM PERIODICIDAD_AHORRO WHERE idPeriodo = @idPeriodo;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idPeriodo", pIdPeriodicidad);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static int obtenerIdPeriodo(string pNumeroCuenta)
        {
            int _idPeriodo = 0;
            String _query = "SELECT * FROM CUENTA_AHORRO_AUTOMATICO_V WHERE numCuenta = @numCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pNumeroCuenta);
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            if(_reader.Read())
            {
                _idPeriodo = Convert.ToInt32(_reader["idPeriodo"]);
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _idPeriodo;
        }

        public static void modificarPeriodicidadAhorro(int pIdPeriodicidad, int pMagnitud, int pTipo)
        {
            String _query = "UPDATE PERIODICIDAD_AHORRO SET MAGNITUD = @magnitud, TIPO = @tipo WHERE IDPERIODO = @idPeriodicidad;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@magnitud", pMagnitud);
            _comandoMySQL.Parameters.AddWithValue("@tipo", pTipo);
            _comandoMySQL.Parameters.AddWithValue("@idPeriodicidad", pIdPeriodicidad);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }
    }
}
