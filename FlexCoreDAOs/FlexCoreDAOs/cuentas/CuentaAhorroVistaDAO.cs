using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;
using ModuloCuentas.Generales;
using FlexCoreDTOs.cuentas;

namespace ModuloCuentas.DAO
{
    internal static class CuentaAhorroVistaDAO
    {
        public static void agregarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista, MySqlCommand pComando)
        {
            CuentaAhorroDAO.agregarCuentaAhorro(pCuentaAhorroVista, pComando);
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, pComando);
            String _query = "INSERT INTO CUENTA_AHORRO_VISTA(SALDOFLOTANTE, IDCUENTA) VALUES(@saldoFlotante, @idCuenta);";
            pComando.Parameters.Clear();
            pComando.CommandText = _query;
            pComando.Parameters.AddWithValue("@saldoFlotante", pCuentaAhorroVista.getSaldoFlotante());
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
            CuentaBeneficiariosDAO.agregarBeneficiarios(pCuentaAhorroVista, pComando);
        }

        public static void modificarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista, MySqlCommand pComando)
        {
            CuentaAhorroDAO.modificarCuentaAhorro(pCuentaAhorroVista, pComando);
        }

        public static void eliminarCuentaAhorroVistaBase(CuentaAhorroVistaDTO pCuentaAhorroVista, MySqlCommand pComando)
        {
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, pComando);
            CuentaBeneficiariosDAO.eliminarBeneficiario(pCuentaAhorroVista, pComando);
            String _query = "DELETE FROM CUENTA_AHORRO_VISTA WHERE idCuenta = @idCuenta;";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
            CuentaAhorroDAO.eliminarCuentaAhorro(pCuentaAhorroVista, pComando);
        }

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaNumeroCuenta(CuentaAhorroVistaDTO pCuentaAhorroVista, MySqlCommand pComando)
        {
            CuentaAhorroVistaDTO _cuentaSalida = null;
            List<PhysicalPersonDTO> _listaBeneficiarios = CuentaBeneficiariosDAO.obtenerListaBeneficiarios(pCuentaAhorroVista, pComando);
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V WHERE NUMCUENTA = @numCuenta";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@numCuenta", pCuentaAhorroVista.getNumeroCuenta());
            MySqlDataReader _reader = pComando.ExecuteReader();
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
                _cuentaSalida = new CuentaAhorroVistaDTO(_numeroCuenta, _descripcion, _saldo, _estado, _tipoMoneda, _cliente, _saldoFlotante, _listaBeneficiarios);
            }
            _reader.Close();
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

        public static void agregarDinero(CuentaAhorroDTO pCuentaAhorro, decimal pMonto, int pTipoCuenta, MySqlCommand pComando)
        {
            if(pTipoCuenta == Constantes.AHORROVISTA)
            {
                CuentaAhorroVistaDTO _cuentaAhorroVista = new CuentaAhorroVistaDTO();
                _cuentaAhorroVista.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                agregarDineroAux(_cuentaAhorroVista, pMonto, pComando);
            }
            else if(pTipoCuenta == Constantes.AHORROAUTOMATICO)
            {
                CuentaAhorroAutomaticoDTO _cuentaAhorroAutomatico = new CuentaAhorroAutomaticoDTO();
                _cuentaAhorroAutomatico.setNumeroCuenta(pCuentaAhorro.getNumeroCuenta());
                CuentaAhorroAutomaticoDAO.agregarDinero(_cuentaAhorroAutomatico, pMonto, Constantes.AHORROAUTOMATICO, pComando);
            }
        }

        private static void agregarDineroAux(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto, MySqlCommand pComando)
        {
            CuentaAhorroVistaDTO _cuentaAhorroVista = obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista, pComando);
            _cuentaAhorroVista.setSaldoFlotante(_cuentaAhorroVista.getSaldoFlotante() + pMonto);
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaAhorroVista, pComando);
            string _query = "UPDATE CUENTA_AHORRO_VISTA SET SALDOFLOTANTE = @saldoFlotante WHERE IDCUENTA = @idCuenta";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@saldoFlotante", _cuentaAhorroVista.getSaldoFlotante());
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
        }

        public static void quitarDinero(CuentaAhorroDTO pCuentaOrigen, decimal pMonto, CuentaAhorroDTO pCuentaDestino, int pTipoCuenta, MySqlCommand pComando)
        {
            CuentaAhorroVistaDTO _cuentaOrigenEntrada = new CuentaAhorroVistaDTO();
            _cuentaOrigenEntrada.setNumeroCuenta(pCuentaOrigen.getNumeroCuenta());
            CuentaAhorroVistaDTO _cuentaAhorroOrigen = obtenerCuentaAhorroVistaNumeroCuenta(_cuentaOrigenEntrada, pComando);
            int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(_cuentaAhorroOrigen, pComando);
            decimal _montoDeduccion = Transformaciones.convertirDinero(pMonto, _cuentaAhorroOrigen.getTipoMoneda(), CuentaAhorroDAO.obtenerCuentaAhorroMoneda(pCuentaDestino, pComando));
            _cuentaAhorroOrigen.setSaldoFlotante(_cuentaAhorroOrigen.getSaldoFlotante() - _montoDeduccion);
            string _query = "UPDATE CUENTA_AHORRO_VISTA SET SALDOFLOTANTE = @saldoFlotante WHERE IDCUENTA = @idCuenta";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            pComando.Parameters.AddWithValue("@saldoFlotante", _cuentaAhorroOrigen.getSaldoFlotante());
            pComando.Parameters.AddWithValue("@idCuenta", _id);
            pComando.ExecuteNonQuery();
            agregarDinero(pCuentaDestino, pMonto, pTipoCuenta, pComando);
        }

        public static void iniciarCierre(MySqlCommand pComando)
        {
            CuentaAhorroDTO _cuentaAhorro = new CuentaAhorroDTO();
            List<Tuple<string, decimal>> _cuentas = new List<Tuple<string, decimal>>();
            String _query = "SELECT * FROM CUENTA_AHORRO_VISTA_V";
            pComando.CommandText = _query;
            pComando.Parameters.Clear();
            MySqlDataReader _reader = pComando.ExecuteReader();
            while(_reader.Read())
            {
                var _cuentaBase = new Tuple<string, decimal>(_reader["numCuenta"].ToString(), Convert.ToDecimal(_reader["saldoFlotante"]));
                _cuentas.Add(_cuentaBase);
            }
            _reader.Close();
            foreach(Tuple<string, decimal> Cuenta in _cuentas)
            {
                _cuentaAhorro.setNumeroCuenta(Cuenta.Item1);
                CuentaAhorroDAO.modificarSaldo(_cuentaAhorro, Cuenta.Item2, pComando);
            }
        }
    }
}
