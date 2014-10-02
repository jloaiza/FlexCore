using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace ModuloCuentas.DAO
{
    internal class CuentaBeneficiariosDAO
    {
        public static void agregarBeneficiario(int pIdCuenta, int pIdBeneficiario)
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

        public static void eliminarBeneficiario(int pIdCuenta)
        {
            String _query = "DELETE FROM CUENTA_BENEFICIARIOS WHERE idCuenta = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static List<PhysicalPersonDTO> obtenerListaBeneficiarios(int pIdCuenta)
        {
            List<PhysicalPersonDTO> _listaBeneficiarios = new List<PhysicalPersonDTO>();
            String _query = "SELECT * FROM CUENTA_BENEFICIARIOS WHERE IDCUENTA = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", pIdCuenta);
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            if (_reader.Read())
            {
                PhysicalPersonDTO _beneficiario = new PhysicalPersonDTO(Convert.ToInt32(_reader["idBeneficiario"]), "", "", "", "");
                _listaBeneficiarios.Add(_beneficiario);
            }
            return _listaBeneficiarios;
        }
    }
}
