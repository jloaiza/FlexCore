using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using ConexionMySQLServer.ConexionMySql;

namespace FlexCoreDAOs.administration
{
    class ErrorQueriesDAO
    {
        public void insertError(String metodo, int linea, DateTime fechaHora, 
            String descripcion)
        {
            String query = "INSERT INTO ERROR (metodo, linea, fechaHora, descripcion)" +
                " VALUES (@metodo, @linea, @fechaHora, @descripcion);";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@metodo", metodo);
            command.Parameters.AddWithValue("@linea", linea);
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public List<DTO.ErrorDTO> getError()
        {
            String query = "SELECT * FROM ERROR";
            List<DTO.ErrorDTO> error = new List<DTO.ErrorDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DTO.ErrorDTO tmp = new DTO.ErrorDTO((int)reader["idError"],
                    reader["metodo"].ToString(),  (int)reader["linea"], (DateTime)reader["fechaHora"], 
                    reader["idCierre"].ToString());
                error.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return error;
        }

        public List<DTO.ErrorDTO> getError(int idError)
        {
            String query = "SELECT * FROM ERROR WHERE idError = @idError";
            List<DTO.ErrorDTO> error = new List<DTO.ErrorDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idError", idError);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DTO.ErrorDTO tmp = new DTO.ErrorDTO((int)reader["idError"],
                    reader["metodo"].ToString(), (int)reader["linea"], (DateTime)reader["fechaHora"],
                    reader["idCierre"].ToString());
                error.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return error;
        }

        public void updateError(int idError, String metodo, int linea, DateTime fechaHora,
            String descripcion)
        {
            String query = "UPDATE ERROR SET metodo = @metodo, linea = @linea, fechaHora = @fechaHora," +
                "descripcion = @descripcion WHERE idError = @idError;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idError", idError);
            command.Parameters.AddWithValue("@metodo", metodo);
            command.Parameters.AddWithValue("@linea", linea);
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteError(int idError)
        {
            String query = "DELETE FROM ERROR WHERE idError = @idError;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idError", idError);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }
    }
}
