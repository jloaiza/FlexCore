using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuloCuentas.Cuentas;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using ModuloCuentas.Generales;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroVistaDAO
    {
        public static void agregarCuentaAhorroVistaBase(CuentaAhorroVista pCuentaAhorroVista)
        {
            CuentaAhorroDAO.agregarCuentaAhorro(pCuentaAhorroVista);
            String _query = "INSERT INTO CUENTA_AHORRO_VISTA(SALDOFLOTANTE, IDCUENTA) VALUES(@saldoFlotante, @idCuenta);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@saldoFlotante", pCuentaAhorroVista.getSaldoFlotante());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista.getNumeroCuenta()));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            //AQUI VA UN CICLO WHILE PARA LOS BENEFICIARIOS, PERO POR EL MOMENTO
            CuentaBeneficiariosDAO.agregarBeneficiario(CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista.getNumeroCuenta()).ToString(), "1");
        }

        public static void modificarCuentaAhorroVistaBase(CuentaAhorroVista pCuentaAhorroVista)
        {
            CuentaAhorroDAO.modificarCuentaAhorro(CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista.getNumeroCuenta()), pCuentaAhorroVista);
        }

        public static void eliminarCuentaAhorroVistaBase(string pNumeroCuenta)
        {
            CuentaBeneficiariosDAO.eliminarBeneficiario(CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuenta).ToString());
            String _query = "DELETE FROM CUENTA_AHORRO_VISTA WHERE idCuenta = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuenta));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            CuentaAhorroDAO.eliminarCuentaAhorro(CuentaAhorroDAO.obtenerCuentaAhorroID(pNumeroCuenta));
        }

        public static CuentaAhorroVista obtenerCuentaAhorroVistaNumeroCuenta(string pNumeroCuenta)
        {
            CuentaAhorroVista _cuentaSalida = null;
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V WHERE NUMCUENTA = @numCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pNumeroCuenta);
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            if(_reader.Read())
            {
                string _numeroCuenta = _reader["numCuenta"].ToString();
                string _descripcion = _reader["descripcion"].ToString();
                decimal _saldo = Convert.ToDecimal(_reader["saldo"]);
                bool _estado = Transformaciones.intToBool(Convert.ToInt32(_reader["activa"]));
                int _tipoMoneda = Convert.ToInt32(_reader["idMoneda"]);
                decimal _saldoFlotante = Convert.ToDecimal(_reader["saldoFlotante"]);
                _cuentaSalida = new CuentaAhorroVista(_numeroCuenta, _descripcion, _saldo, _estado, _tipoMoneda, _saldoFlotante);
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _cuentaSalida;
        }

        public static CuentaAhorroVista obtenerCuentaAhorroVistaCedula(string pCedula)
        {
            //SE OBTIENE LA CUENTA DADA LA CEDULA;
            return null;
        }

        public static List<CuentaAhorroVista> obtenerCuentaAhorroVistaNombre(string pNombre)
        {
            //SE OBTIENEN LAS CUENTAS DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroVista obtenerCuentaAhorroVistaCIF(string pCIF)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static void agregarDinero(string pNumeroCuenta, decimal pMonto, int pTipoCuenta)
        {
            if(pTipoCuenta == Constantes.AHORROVISTA)
            {
                agregarDineroAux(pNumeroCuenta, pMonto);
            }
            else if(pTipoCuenta == Constantes.AHORROAUTOMATICO)
            {
                CuentaAhorroAutomaticoDAO.agregarDinero(pNumeroCuenta, pMonto, Constantes.AHORROAUTOMATICO);
            }
        }

        private static void agregarDineroAux(string pNumeroCuenta, decimal pMonto)
        {
            CuentaAhorroVista _cuentaAhorroVista = obtenerCuentaAhorroVistaNumeroCuenta(pNumeroCuenta);
            _cuentaAhorroVista.setSaldoFlotante(_cuentaAhorroVista.getSaldoFlotante() + pMonto);
            string _query = "UPDATE CUENTA_AHORRO_VISTA SET SALDOFLOTANTE = @saldoFlotante WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@saldoFlotante", _cuentaAhorroVista.getSaldoFlotante());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", _cuentaAhorroVista.getNumeroCuenta());
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void quitarDinero(string pNumeroCuentaOrigen, decimal pMonto, string pNumeroCuentaDestino, int pTipoCuenta)
        {
            CuentaAhorroVista _cuentaAhorroOrigen = obtenerCuentaAhorroVistaNumeroCuenta(pNumeroCuentaOrigen);
            _cuentaAhorroOrigen.setSaldoFlotante(_cuentaAhorroOrigen.getSaldoFlotante() - pMonto);
            string _query = "UPDATE CUENTA_AHORRO_VISTA SET SALDOFLOTANTE = @saldoFlotante WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@saldoFlotante", _cuentaAhorroOrigen.getSaldoFlotante());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaAhorroOrigen.getNumeroCuenta()));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            agregarDinero(pNumeroCuentaDestino, pMonto, pTipoCuenta);
        }

        public static void iniciarCierre()
        {
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            while(_reader.Read())
            {
                CuentaAhorroDAO.modificarSaldo(Convert.ToInt32(_reader["idCuenta"]), Convert.ToDecimal(_reader["saldoFlotante"]));
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }
    }
}
