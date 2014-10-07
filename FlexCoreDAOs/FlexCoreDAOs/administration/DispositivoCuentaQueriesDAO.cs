using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using ConexionMySQLServer.ConexionMySql;
using FlexCoreDTOs.administration;

namespace FlexCoreDAOs.administration
{
    public class DispositivoCuentaQueriesDAO
    {
        public void insertDispositivoCuenta(String idDispositivo, bool activo, int idCuenta)
        {
            String query = "INSERT INTO DISPOSITIVO_CUENTA (idDispositivo, activo, idCuenta)" +
                " VALUES (@idDispositivo, @activo, @idCuenta);";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivo", idDispositivo);
            command.Parameters.AddWithValue("@activo", activo);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public List<DispositivoCuentaDTO> getDispositivoCuenta()
        {
            String query = "SELECT * FROM DISPOSITIVO_CUENTA";
            List<DispositivoCuentaDTO> dispositivoCuenta = new List<DispositivoCuentaDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DispositivoCuentaDTO tmp = new DispositivoCuentaDTO((int)reader["idDispositivoCuenta"],
                    reader["idDispositivo"].ToString(), (bool)reader["activo"], (int)reader["idCuenta"]);
                dispositivoCuenta.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return dispositivoCuenta;
        }

        public List<DispositivoCuentaDTO> getDispositivoCuenta(int idDispositivoCuenta)
        {
            String query = "SELECT * FROM DISPOSITIVO_CUENTA WHERE idDispositivoCuenta = @idDispositivoCuenta";
            List<DispositivoCuentaDTO> dispositivoCuenta = new List<DispositivoCuentaDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivoCuenta", idDispositivoCuenta);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DispositivoCuentaDTO tmp = new DispositivoCuentaDTO((int)reader["idDispositivoCuenta"],
                    reader["idDispositivo"].ToString(), (bool)reader["activo"], (int)reader["idCuenta"]);
                dispositivoCuenta.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return dispositivoCuenta;
        }

        public void updateDispositivoCuenta(int idDispositivoCuenta, String idDispositivo, bool activo, 
            int idCuenta)
        {
            String query = "UPDATE DISPOSITIVO_CUENTA SET idDispositivo = @idDispositivo, activo = @activo," +
                " idCuenta = @idCuenta WHERE idDispositivoCuenta = @idDispositivoCuenta;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivoCuenta", idDispositivoCuenta);
            command.Parameters.AddWithValue("@idDispositivov", idDispositivo);
            command.Parameters.AddWithValue("@activo", activo);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteDispositivoCuenta(int idDispositivoCuenta)
        {
            String query = "DELETE FROM DISPOSITIVO_CUENTA WHERE idDispositivoCuenta = @idDispositivoCuenta;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idDispositivoCuenta", idDispositivoCuenta);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }
    }
}
