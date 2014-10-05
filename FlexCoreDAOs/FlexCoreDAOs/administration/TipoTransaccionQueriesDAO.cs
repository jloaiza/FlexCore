using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using ConexionMySQLServer.ConexionMySql;

namespace FlexCore1.DAO
{
    public class TipoTransaccionQueriesDAO
    {

        public void insertDescripcion(String desc)
        {
            String query = "INSERT INTO TIPO_TRANSACCION (descripcion) VALUES (@descripcion);";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", desc);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public List<DTO.TipoTransaccionDTO> getDescripcion()
        {
            String query = "SELECT * FROM TIPO_TRANSACCION";
            List<DTO.TipoTransaccionDTO> tipo_transaccion = new List<DTO.TipoTransaccionDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DTO.TipoTransaccionDTO tmp = new DTO.TipoTransaccionDTO((int)reader["idTipo"], reader["descripcion"].ToString());
                tipo_transaccion.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return tipo_transaccion;
        }

        public List<DTO.TipoTransaccionDTO> getDescripcion(int idTipo)
        {
            String query = "SELECT * FROM TIPO_TRANSACCION WHERE idTipo = @idTipo";
            List<DTO.TipoTransaccionDTO> tipo_transaccion = new List<DTO.TipoTransaccionDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTipo", idTipo);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DTO.TipoTransaccionDTO tmp = new DTO.TipoTransaccionDTO((int)reader["idTipo"], reader["descripcion"].ToString());
                tipo_transaccion.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return tipo_transaccion;
        }

        public List<DTO.TipoTransaccionDTO> getDescripcion(String descripcion)
        {
            String query = "SELECT * FROM TIPO_TRANSACCION WHERE descripcion = @descripcion";
            List<DTO.TipoTransaccionDTO> tipo_transaccion = new List<DTO.TipoTransaccionDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DTO.TipoTransaccionDTO tmp = new DTO.TipoTransaccionDTO((int)reader["idTipo"], reader["descripcion"].ToString());
                tipo_transaccion.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return tipo_transaccion;
        }

        public int getIdDescripcion(String descripcion)
        {
            String query = "SELECT * FROM TIPO_TRANSACCION WHERE descripcion = @descripcion";
            int idTipo = 0;
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                idTipo = (int)reader["idTipo"];
            }
            MySQLManager.cerrarConexion(connD);
            return idTipo;
        }

        public void updateDescripcion(int idTipo, String descripcion)
        {
            String query = "UPDATE TIPO_TRANSACCION SET descripcion = @descripcion WHERE idTipo = @idTipo;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTipo", idTipo);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteDescription(int idTipo)
        {
            String query = "DELETE FROM TIPO_TRANSACCION WHERE idTipo = @idTipo;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTipo", idTipo);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteDescription(String descripcion)
        {
            String query = "DELETE FROM TIPO_TRANSACCION WHERE descripcion = @descripcion;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }
    }
}
