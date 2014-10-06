using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;

namespace ModuloCuentas.DAO
{
    internal class CuentaBeneficiariosDAO
    {
        public static void agregarBeneficiarios(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista);
            foreach (PhysicalPersonDTO beneficiario in pCuentaAhorroVista.getListaBeneficiarios())
            {
                String _query = "INSERT INTO CUENTA_BENEFICIARIOS(IDCUENTA, IDBENEFICIARIO) VALUES(@idCuenta, @idBeneficiario);";
                MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
                MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
                _comandoMySQL.CommandText = _query;
                _comandoMySQL.Parameters.AddWithValue("@idCuenta", _id);
                _comandoMySQL.Parameters.AddWithValue("@idBeneficiario", beneficiario.getPersonID());
                _comandoMySQL.ExecuteNonQuery();
                MySQLManager.cerrarConexion(_conexionMySQLBase);
            }
        }

        public static void eliminarBeneficiario(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            String _query = "DELETE FROM CUENTA_BENEFICIARIOS WHERE idCuenta = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static List<PhysicalPersonDTO> obtenerListaBeneficiarios(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            List<PhysicalPersonDTO> _listaBeneficiarios = new List<PhysicalPersonDTO>();
            String _query = "SELECT * FROM CUENTA_BENEFICIARIOS WHERE IDCUENTA = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista));
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
