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
    public class HistoricoTransaccionalQueriesDAO
    {
        public void insertHistoricoTransaccional(String descripcion, DateTime fechaHora, int idCuenta, int tipoTransaccion)
        {
            String query = "INSERT INTO HISTORICO_TRANSACCIONAL (descripcion, fechaHora, idCuenta, tipoTransaccion)" +
                " VALUES (@descripcion, @fechaHora, @idCuenta, @tipoTransaccion);";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.Parameters.AddWithValue("@fechaHora", fechaHora);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.Parameters.AddWithValue("@tipoTransaccion", tipoTransaccion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public List<HistoricoTransaccionalDTO> getDescripcion()
        {
            String query = "SELECT * FROM HISTORICO_TRANSACCIONAL";
            List<HistoricoTransaccionalDTO> transacciones_vuelo = new List<HistoricoTransaccionalDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HistoricoTransaccionalDTO tmp = new HistoricoTransaccionalDTO((int)reader["idTransaccion"],
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()),
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return transacciones_vuelo;
        }

        public List<HistoricoTransaccionalDTO> getDescripcion(int idTransaccion)
        {
            String query = "SELECT * FROM HISTORICO_TRANSACCIONAL WHERE idTransaccion = @idTransaccion;";
            List<HistoricoTransaccionalDTO> transacciones_vuelo = new List<HistoricoTransaccionalDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HistoricoTransaccionalDTO tmp = new HistoricoTransaccionalDTO((int)reader["idTransaccion"],
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()),
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return transacciones_vuelo;
        }

        public List<HistoricoTransaccionalDTO> getDescripcion(String descripcion)
        {
            String query = "SELECT * FROM HISTORICO_TRANSACCIONAL WHERE descripcion = @descripcion;";
            List<HistoricoTransaccionalDTO> transacciones_vuelo = new List<HistoricoTransaccionalDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                HistoricoTransaccionalDTO tmp = new HistoricoTransaccionalDTO((int)reader["idTransaccion"],
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()),
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return transacciones_vuelo;
        }

        public int getIdHistoricoTransaccional(String descripcion)
        {
            String query = "SELECT * FROM HISTORICO_TRANSACCIONAL WHERE descripcion = @descripcion;";
            int transaccion_vuelo = -1;
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                transaccion_vuelo = (int)reader["idTransaccion"];
            }
            MySQLManager.cerrarConexion(connD);
            return transaccion_vuelo;
        }

        public void updateHistoricoTransaccional(int idTransaccion, String descripcion)
        {
            String query = "UPDATE HISTORICO_TRANSACCIONAL SET descripcion = @descripcion WHERE idTransaccion = @idTransaccion;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteHistoricoTransaccional(int idTransaccion)
        {
            String query = "DELETE FROM HISTORICO_TRANSACCIONAL WHERE idTransaccion = @idTransaccion;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteHistoricoTransaccional(String descripcion)
        {
            String query = "DELETE FROM HISTORICO_TRANSACCIONAL WHERE descripcion = @descripcion;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

    }
}
