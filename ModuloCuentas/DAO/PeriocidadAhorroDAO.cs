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
        public static int agregarMagnitudPeriocidad(int pMagnitud, int pTipoPeriodo, MySqlCommand pComando)
        {
            String _query = "INSERT INTO PERIODICIDAD_AHORRO(MAGNITUD, TIPO) VALUES(@magnitud, @tipo);";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@magnitud", pMagnitud);
            pComando.Parameters.AddWithValue("@tipo", pTipoPeriodo);
            pComando.ExecuteNonQuery();
            int _lastId = Convert.ToInt32(pComando.LastInsertedId);
            return _lastId;
        }

        public static void eliminarPeriodicidadAhorro(int pIdPeriodicidad, MySqlCommand pComando)
        {
            String _query = "DELETE FROM PERIODICIDAD_AHORRO WHERE idPeriodo = @idPeriodo;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@idPeriodo", pIdPeriodicidad);
            pComando.ExecuteNonQuery();
        }

        public static int obtenerIdPeriodo(string pNumeroCuenta, MySqlCommand pComando)
        {
            int _idPeriodo = 0;
            String _query = "SELECT * FROM CUENTA_AHORRO_AUTOMATICO_V WHERE numCuenta = @numCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@numCuenta", pNumeroCuenta);
            MySqlDataReader _reader = pComando.ExecuteReader();
            if(_reader.Read())
            {
                _idPeriodo = Convert.ToInt32(_reader["idPeriodo"]);
            }
            _reader.Close();
            return _idPeriodo;
        }

        public static void modificarPeriodicidadAhorro(int pIdPeriodicidad, int pMagnitud, int pTipo, MySqlCommand pComando)
        {
            String _query = "UPDATE PERIODICIDAD_AHORRO SET MAGNITUD = @magnitud, TIPO = @tipo WHERE IDPERIODO = @idPeriodicidad;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@magnitud", pMagnitud);
            pComando.Parameters.AddWithValue("@tipo", pTipo);
            pComando.Parameters.AddWithValue("@idPeriodicidad", pIdPeriodicidad);
            pComando.ExecuteNonQuery();
        }
    }
}
