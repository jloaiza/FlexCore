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
    public class CierreQueriesDAO
    {
        public void insertCierre(DateTime fechaHora, bool estado)
        {
            String query = "INSERT INTO CIERRE (fechaHora, estado)" +
                " VALUES (@fechaHora, @estado);";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@estado", estado);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public List<CierreDTO> getCierre()
        {
            String query = "SELECT * FROM CIERRE";
            List<CierreDTO> cierre = new List<CierreDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CierreDTO tmp = new CierreDTO((int)reader["idCierre"],
                    DateTime.Parse(reader["fechaHora"].ToString()), (bool)reader["estado"]);
                cierre.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return cierre;
        }

        public List<CierreDTO> getCierre(int idCierre)
        {
            String query = "SELECT * FROM CIERRE WHERE idCierre = @idCierre";
            List<CierreDTO> cierre = new List<CierreDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idCierre", idCierre);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                CierreDTO tmp = new CierreDTO((int)reader["idCierre"],
                    DateTime.Parse(reader["fechaHora"].ToString()), (bool)reader["estado"]);
                cierre.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return cierre;
        }

        public void updateCierre(int idCierre, DateTime fechaHora, bool estado)
        {
            String query = "UPDATE CIERRE SET fechaHora = @fechaHora, estado = @estado WHERE idCierre = @idCierre;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@estado", estado);
            command.Parameters.AddWithValue("@idCierre", idCierre);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteCierre(int idCierre)
        {
            String query = "DELETE FROM CIERRE WHERE idCierre = @idCierre;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idCierre", idCierre);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }
    }
}
