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
    public class ConfiguracionesQueriesDAO
    {
        public void insertCierre(Decimal compraDolar, Decimal ventaDolar, DateTime fechaHoraSistema, 
            Decimal tasaInteresAhorro)
        {
            String query = "INSERT INTO CONFIGURACIONES (compraDolar, ventaDolar, fechaHoraSistema, tasaInteresAhorro)" +
                " VALUES (@compraDolar, @ventaDolar, @fechaHoraSistema, @tasaInteresAhorro);";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@compraDolar", compraDolar);
            command.Parameters.AddWithValue("@ventaDolar", ventaDolar);
            command.Parameters.AddWithValue("@fechaHoraSistema", fechaHoraSistema);
            command.Parameters.AddWithValue("@tasaInteresAhorro", tasaInteresAhorro);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public List<ConfiguracionesDTO> getConfiguracion()
        {
            String query = "SELECT * FROM CONFIGURACIONES";
            List<ConfiguracionesDTO> configuraciones = new List<ConfiguracionesDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ConfiguracionesDTO tmp = new ConfiguracionesDTO((Decimal)reader["compraDolar"],
                    (Decimal)reader["ventaDolar"], DateTime.Parse(reader["fechaHora"].ToString()),
                    (Decimal)reader["tasaInteresAhorro"]);
                configuraciones.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return configuraciones;
        }

        public void actualizarHoraBase(DateTime pHora)
        {
            String query = "UPDATE FROM CONFIGURACIONES SET FECHAHORASISTEMA = @horaSistema";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@horaSistema", pHora);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }

        public List<ConfiguracionesDTO> getConfiguracion(Decimal compraDolar, Decimal ventaDolar, 
            DateTime fechaHoraSistema, Decimal tasaInteresAhorro)
        {
            String query = "SELECT * FROM CONFIGURACIONES WHERE compraDolar = @compraDolar AND ventaDolar = @ventaDolar AND " +
                "fechaHoraSistema = @fechaHoraSistema AND tasaInteresAhorro = @tasaInteresAhorro";
            List<ConfiguracionesDTO> cierre = new List<ConfiguracionesDTO>();
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@compraDolar", compraDolar);
            command.Parameters.AddWithValue("@ventaDolar", ventaDolar);
            command.Parameters.AddWithValue("@fechaHoraSistema", fechaHoraSistema);
            command.Parameters.AddWithValue("@tasaInteresAhorro", tasaInteresAhorro);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ConfiguracionesDTO tmp = new ConfiguracionesDTO((Decimal)reader["compraDolar"],
                    (Decimal)reader["ventaDolar"], DateTime.Parse(reader["fechaHoraSistema"].ToString()),
                    (Decimal)reader["tasaInteresAhorro"]);
                cierre.Add(tmp);
            }
            MySQLManager.cerrarConexion(connD);
            return cierre;
        }

        public void deleteCierre(Decimal compraDolar, Decimal ventaDolar,
            DateTime fechaHoraSistema, Decimal tasaInteresAhorro)
        {
            String query = "DELETE FROM CONFIGURACIONES WHERE compraDolar = @compraDolar AND ventaDolar = @ventaDolar AND " +
                "fechaHoraSistema = @fechaHoraSistema AND tasaInteresAhorro = @tasaInteresAhorro;";
            MySqlConnection connD = MySQLManager.nuevaConexion();
            MySqlCommand command = connD.CreateCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@compraDolar", compraDolar);
            command.Parameters.AddWithValue("@ventaDolar", ventaDolar);
            command.Parameters.AddWithValue("@fechaHoraSistema", fechaHoraSistema);
            command.Parameters.AddWithValue("@tasaInteresAhorro", tasaInteresAhorro);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connD);
        }
    }
}
