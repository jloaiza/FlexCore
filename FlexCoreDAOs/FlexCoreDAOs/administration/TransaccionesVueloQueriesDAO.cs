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
    public class TransaccionesVueloQueriesDAO
    {

        public void insertTransaccionVuelo(String descripcion, DateTime fechaHora, int idCuenta, int tipoTransaccion)
        {
            String query = "INSERT INTO TRANSACCIONES_VUELO (descripcion, fechaHora, idCuenta, tipoTransaccion)" +
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

        public List<TransaccionesVueloDTO> getDescripcion()
        {
            String query = "SELECT * FROM TRANSACCIONES_VUELO";
            List<TransaccionesVueloDTO> transacciones_vuelo = new List<TransaccionesVueloDTO>();
            /*DataTable transaccion_vuelo = new DataTable();
            transaccion_vuelo.Columns.Add("idTransaccion", typeof(int));
            transaccion_vuelo.Columns.Add("descripcion", typeof(String));
            transaccion_vuelo.Columns.Add("fechaHora", typeof(DateTime));
            transaccion_vuelo.Columns.Add("idCuenta", typeof(int));
            transaccion_vuelo.Columns.Add("tipoTransaccion", typeof(int));*/
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TransaccionesVueloDTO tmp = new TransaccionesVueloDTO((int)reader["idTransaccion"], 
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()), 
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
                /*transaccion_vuelo.Rows.Add(reader["idTransaccion"], reader["descripcion"],
                    reader["fechaHora"], reader["idCuenta"], reader["tipoTransaccion"]);*/
            }
            MySQLManager.cerrarConexion(connD);
            return transacciones_vuelo;
        }

        public List<TransaccionesVueloDTO> getDescripcion(int idTransaccion)
        {
            String query = "SELECT * FROM TRANSACCIONES_VUELO WHERE idTransaccion = @idTransaccion;";
            List<TransaccionesVueloDTO> transacciones_vuelo = new List<TransaccionesVueloDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TransaccionesVueloDTO tmp = new TransaccionesVueloDTO((int)reader["idTransaccion"],
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()),
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return transacciones_vuelo;
        }

        public List<TransaccionesVueloDTO> getDescripcion(String descripcion)
        {
            String query = "SELECT * FROM TRANSACCIONES_VUELO WHERE descripcion = @descripcion;";
            List<TransaccionesVueloDTO> transacciones_vuelo = new List<TransaccionesVueloDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TransaccionesVueloDTO tmp = new TransaccionesVueloDTO((int)reader["idTransaccion"],
                    reader["descripcion"].ToString(), DateTime.Parse(reader["fechaHora"].ToString()),
                    (int)reader["idCuenta"], (int)reader["tipoTransaccion"]);
                transacciones_vuelo.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return transacciones_vuelo;
        }

        public int getIdTransaccionVuelo(String descripcion)
        {
            String query = "SELECT * FROM TRANSACCIONES_VUELO WHERE descripcion = @descripcion;";
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

        public void updateTransaccionVuelo(int idTransaccion, String descripcion)
        {
            String query = "UPDATE TRANSACCIONES_VUELO SET descripcion = @descripcion WHERE idTransaccion = @idTransaccion;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteTransaccionVuelo(int idTransaccion)
        {
            String query = "DELETE FROM TRANSACCIONES_VUELO WHERE idTransaccion = @idTransaccion;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idTransaccion", idTransaccion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deleteTransaccionVuelo(String descripcion)
        {
            String query = "DELETE FROM TRANSACCIONES_VUELO WHERE descripcion = @descripcion;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@descripcion", descripcion);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }
    }
}
