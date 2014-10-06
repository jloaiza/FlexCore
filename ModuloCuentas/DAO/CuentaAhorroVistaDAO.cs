using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using ModuloCuentas.Generales;
using FlexCoreDTOs.clients;
using FlexCoreDTOs.cuentas;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroVistaDAO
    {
        public static void agregarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            CuentaAhorroDAO.agregarCuentaAhorro(pCuentaAhorroVista);
            String _query = "INSERT INTO CUENTA_AHORRO_VISTA(SALDOFLOTANTE, IDCUENTA) VALUES(@saldoFlotante, @idCuenta);";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@saldoFlotante", pCuentaAhorroVista.getSaldoFlotante());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            CuentaBeneficiariosDAO.agregarBeneficiarios(pCuentaAhorroVista);
        }

        public static void modificarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroVista);
        }

        public static void eliminarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            CuentaBeneficiariosDAO.eliminarBeneficiario(pCuentaAhorroVista);
            String _query = "DELETE FROM CUENTA_AHORRO_VISTA WHERE idCuenta = @idCuenta;";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            CuentaAhorroDAO.eliminarCuentaAhorro(pCuentaAhorroVista);
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaNumeroCuenta(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            CuentaAhorroVistaDTO _cuentaSalida = null;
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V WHERE NUMCUENTA = @numCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@numCuenta", pCuentaAhorroVista.getNumeroCuenta());
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            if(_reader.Read())
            {
                string _numeroCuenta = _reader["numCuenta"].ToString();
                string _descripcion = _reader["descripcion"].ToString();
                decimal _saldo = Convert.ToDecimal(_reader["saldo"]);
                bool _estado = Transformaciones.intToBool(Convert.ToInt32(_reader["activa"]));
                int _tipoMoneda = Convert.ToInt32(_reader["idMoneda"]);
                decimal _saldoFlotante = Convert.ToDecimal(_reader["saldoFlotante"]);
                int _idCliente = Convert.ToInt32(_reader["idCliente"]);
                ClientDTO _cliente = new ClientDTO(_idCliente, "");
                List<PhysicalPersonDTO> _listaBeneficiarios = CuentaBeneficiariosDAO.obtenerListaBeneficiarios(pCuentaAhorroVista);
                _cuentaSalida = new CuentaAhorroVistaDTO(_numeroCuenta, _descripcion, _saldo, _estado, _tipoMoneda, _cliente, _saldoFlotante, _listaBeneficiarios);
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            return _cuentaSalida;
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaCedula(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            //SE OBTIENE LA CUENTA DADA LA CEDULA;
            return null;
        }

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaNombre(CuentaAhorroVistaDTO pCuentaAhorroVista, int pNumeroPagina, int pCantidadElementos)
        {
            //SE OBTIENEN LAS CUENTAS DADO EL NOMBRE;
            return null;
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaCIF(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            //SE OBTIENE LA CUENTA DADO EL CIF;
            return null;
        }

        public static void agregarDinero(CuentaAhorroDTO pCuentaAhorro, decimal pMonto, int pTipoCuenta)
        {
            if(pTipoCuenta == Constantes.AHORROVISTA)
            {
                CuentaAhorroVistaDTO _cuentaAhorroVista = new CuentaAhorroVistaDTO();
                _cuentaAhorroVista.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                agregarDineroAux(_cuentaAhorroVista, pMonto);
            }
            else if(pTipoCuenta == Constantes.AHORROAUTOMATICO)
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomatico.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                CuentaAhorroAutomaticoDAO.agregarDinero(_cuentaAhorroAutomatico, pMonto, Constantes.AHORROAUTOMATICO);
            }
        }

        private static void agregarDineroAux(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            CuentaAhorroVistaDTO _cuentaAhorroVista = obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista);
            _cuentaAhorroVista.setSaldoFlotante(_cuentaAhorroVista.getSaldoFlotante() + pMonto);
            string _query = "UPDATE CUENTA_AHORRO_VISTA SET SALDOFLOTANTE = @saldoFlotante WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@saldoFlotante", _cuentaAhorroVista.getSaldoFlotante());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaAhorroVista));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }

        public static void quitarDinero(CuentaAhorroDTO pCuentaOrigen, decimal pMonto, CuentaAhorroDTO pCuentaDestino, int pTipoCuenta)
        {
            CuentaAhorroVistaDTO _cuentaOrigenEntrada = new CuentaAhorroVistaDTO();
            _cuentaOrigenEntrada.setNumeroCuenta(pCuentaOrigen.getNumeroCuenta());
            CuentaAhorroVistaDTO _cuentaAhorroOrigen = obtenerCuentaAhorroVistaNumeroCuenta(_cuentaOrigenEntrada);
            decimal _montoDeduccion = Transformaciones.convertirDinero(pMonto, _cuentaAhorroOrigen.getTipoMoneda(), CuentaAhorroDAO.obtenerCuentaAhorroMoneda(pCuentaDestino));
            _cuentaAhorroOrigen.setSaldoFlotante(_cuentaAhorroOrigen.getSaldoFlotante() - _montoDeduccion);
            string _query = "UPDATE CUENTA_AHORRO_VISTA SET SALDOFLOTANTE = @saldoFlotante WHERE IDCUENTA = @idCuenta";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            _comandoMySQL.Parameters.AddWithValue("@saldoFlotante", _cuentaAhorroOrigen.getSaldoFlotante());
            _comandoMySQL.Parameters.AddWithValue("@idCuenta", CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaAhorroOrigen));
            _comandoMySQL.ExecuteNonQuery();
            MySQLManager.cerrarConexion(_conexionMySQLBase);
            agregarDinero(pCuentaDestino, pMonto, pTipoCuenta);
        }

        public static void iniciarCierre()
        {
            CuentaAhorroDTO _cuentaAhorro = new CuentaAhorroDTO();
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V";
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            _comandoMySQL.CommandText = _query;
            MySqlDataReader _reader = _comandoMySQL.ExecuteReader();
            while(_reader.Read())
            {
                _cuentaAhorro.setNumeroCuenta(_reader["numCuenta"].ToString()); 
                CuentaAhorroDAO.modificarSaldo(_cuentaAhorro, Convert.ToDecimal(_reader["saldoFlotante"]));
            }
            MySQLManager.cerrarConexion(_conexionMySQLBase);
        }
    }
}
