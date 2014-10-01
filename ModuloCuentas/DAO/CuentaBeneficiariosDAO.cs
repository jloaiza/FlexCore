using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;

namespace ModuloCuentas.DAO
{
    internal class CuentaBeneficiariosDAO
    {
        public static void agregarBeneficiario(string pIdCuenta, string pIdBeneficiario)
        {
            String _query = "INSERT INTO CUENTA_BENEFICIARIOS(IDCUENTA, IDBENEFICIARIO) VALUES(@idCuenta, @idBeneficiario);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.Parameters.AddWithValue("@idBeneficiario", pIdBeneficiario);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void eliminarBeneficiario(string pIdCuenta)
        {
            String _query = "DELETE FROM CUENTA_BENEFICIARIOS WHERE idCuenta = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }
    }
}
