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
    class PagoInteresesQueriesDAO
    {
        public void insertPagoIntereses(Decimal monto, int idCuenta, int idCierre)
        {
            String query = "INSERT INTO PAGO_INTERESES (monto, idCuenta, idCierre)" +
                " VALUES (@monto, @idCuenta, @idCierre);";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@monto", monto);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.Parameters.AddWithValue("@idCierre", idCierre);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public List<DTO.PagoInteresesDTO> getPagoIntereses()
        {
            String query = "SELECT * FROM PAGO_INTERESES";
            List<DTO.PagoInteresesDTO> pagoIntereses = new List<DTO.PagoInteresesDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DTO.PagoInteresesDTO tmp = new DTO.PagoInteresesDTO((int)reader["idPago"],
                    (Decimal)reader["monto"], (int)reader["idCuenta"], (int)reader["idCierre"]);
                pagoIntereses.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return pagoIntereses;
        }

        public List<DTO.PagoInteresesDTO> getPagoIntereses(int idPago)
        {
            String query = "SELECT * FROM PAGO_INTERESES WHERE idPago = @idPago";
            List<DTO.PagoInteresesDTO> pagoIntereses = new List<DTO.PagoInteresesDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idPago", idPago);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                DTO.PagoInteresesDTO tmp = new DTO.PagoInteresesDTO((int)reader["idPago"],
                    (Decimal)reader["monto"], (int)reader["idCuenta"], (int)reader["idCierre"]);
                pagoIntereses.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return pagoIntereses;
        }

        public void updatePagoIntereses(int idPago, Decimal monto, int idCuenta, int idCierre)
        {
            String query = "UPDATE PAGO_INTERESES SET monto = @monto, idCuenta = @idCuenta, idCierre = @idCierre"+
                " WHERE idPago = @idPago;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idPago", idPago);
            command.Parameters.AddWithValue("@monto", monto);
            command.Parameters.AddWithValue("@idCuenta", idCuenta);
            command.Parameters.AddWithValue("@idCierre", idCierre);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public void deletePagoIntereses(int idPago)
        {
            String query = "DELETE FROM PAGO_INTERESES WHERE idPago = @idPago;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idPago", idPago);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }
    }
}
