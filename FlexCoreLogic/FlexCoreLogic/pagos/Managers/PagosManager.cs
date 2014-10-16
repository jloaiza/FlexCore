﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using FlexCoreDAOs.cuentas;
using FlexCoreDAOs.administration;
using FlexCoreDTOs.cuentas;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreLogic.cuentas.Managers;
using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;


namespace FlexCoreLogic.pagos.Managers
{
    internal class PagosManager
    {
        private static MySqlCommand obtenerConexionSQL()
        {
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            MySqlCommand _comandoMySQL = _conexionMySQLBase.CreateCommand();
            MySqlTransaction _transaccion = _conexionMySQLBase.BeginTransaction();
            _comandoMySQL.Connection = _conexionMySQLBase;
            _comandoMySQL.Transaction = _transaccion;
            return _comandoMySQL;
        }

       
        private static bool verificarCliente(int pIdCliente)
        {
            ClientsFacade _facadeCliente = ClientsFacade.getInstance();
            ClientDTO _cliente = new ClientDTO();
            _cliente.setClientID(pIdCliente);
            return _facadeCliente.isClientActive(_cliente);
        }

        public static string realizarPagoODebito(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaOrigen, _comandoMySQL);
                CuentaAhorroVistaDTO _cuentaDestino = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino, _comandoMySQL);
                if (_cuentaOrigen.getSaldoFlotante() < pMonto)
                {
                    return "Fondos insuficientes";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    return "La cuenta con la cual se desea pagar se encuentra actualmente desactivada";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "El cliente que desea hacer el pago se encuentra inactivo.";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "El cliente al cual se desea hacer el pago se encuentra inactivo.";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    return "La cuenta a la cual se desea pagar se encuentra actualmente desactivada";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA, _comandoMySQL);
                    _comandoMySQL.Transaction.Commit();
                    return "Transacción completada con éxito";
                }
            }
            catch(Exception ex1)
            {
                Console.WriteLine(ex1.Message);
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transacción";
                }
                catch(Exception ex2)
                {
                    Console.WriteLine(ex2.Message);
                    return "Ha ocurrido un error en la transacción";
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }

        public static string realizarPagoODebito(CuentaAhorroVistaDTO pCuentaAhorroVistaOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDTO _cuentaOrigen = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaOrigen, _comandoMySQL);
                CuentaAhorroAutomaticoDTO _cuentaDestino = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino, _comandoMySQL);
                if (_cuentaOrigen.getSaldoFlotante() < pMonto)
                {
                    return "Fondos insuficientes";
                }
                else if (_cuentaOrigen.getEstado() == false)
                {
                    return "La cuenta con la cual se desea pagar se encuentra actualmente desactivada";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "El cliente que desea hacer el pago se encuentra inactivo.";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "El cliente al cual se desea hacer el pago se encuentra inactivo.";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    return "La cuenta a la cual se desea pagar se encuentra actualmente en ahorro";
                }
                else
                {
                    CuentaAhorroVistaDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO, _comandoMySQL);
                    _comandoMySQL.Transaction.Commit();
                    return "Transacción completada con éxito";
                }
            }
            catch
            {
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transacción";
                }
                catch
                {
                    return "Ha ocurrido un error en la transacción";
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoDestino)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaOrigen = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen, _comandoMySQL);
                CuentaAhorroAutomaticoDTO _cuentaDestino = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoDestino, _comandoMySQL);
                if (_cuentaOrigen.getEstado() == true)
                {
                    return "La cuenta con la cual se desea pagar se encuentra actualmente en ahorro";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    return "Fondos insuficientes";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "El cliente que desea hacer el pago se encuentra inactivo.";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "El cliente al cual se desea hacer el pago se encuentra inactivo.";
                }
                else if (_cuentaDestino.getEstado() == true)
                {
                    return "La cuenta a la cual se desea pagar se encuentra actualmente en ahorro";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(pCuentaAhorroAutomaticoOrigen, pMonto, pCuentaAhorroAutomaticoDestino, Constantes.AHORROAUTOMATICO, _comandoMySQL);
                    _comandoMySQL.Transaction.Commit();
                    return "Transacción completada con éxito";
                }
            }
            catch
            {
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transacción";
                }
                catch
                {
                    return "Ha ocurrido un error en la transacción";
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }

        public static string realizarPagoODebito(CuentaAhorroAutomaticoDTO pCuentaAhorroAutomaticoOrigen, decimal pMonto, CuentaAhorroVistaDTO pCuentaAhorroVistaDestino)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroAutomaticoDTO _cuentaOrigen = CuentaAhorroAutomaticoDAO.obtenerCuentaAhorroAutomaticoNumeroCuenta(pCuentaAhorroAutomaticoOrigen, _comandoMySQL);
                CuentaAhorroVistaDTO _cuentaDestino = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVistaDestino, _comandoMySQL);
                if (_cuentaOrigen.getEstado() == true)
                {
                    return "La cuenta con la cual se desea pagar se encuentra actualmente en ahorro";
                }
                else if (_cuentaOrigen.getSaldo() < pMonto)
                {
                    return "Fondos insuficientes";
                }
                else if (verificarCliente(_cuentaOrigen.getCliente().getClientID()) == false)
                {
                    return "El cliente que desea hacer el pago se encuentra inactivo.";
                }
                else if (verificarCliente(_cuentaDestino.getCliente().getClientID()) == false)
                {
                    return "El cliente al cual se desea hacer el pago se encuentra inactivo.";
                }
                else if (_cuentaDestino.getEstado() == false)
                {
                    return "La cuenta a la cual se desea pagar se encuentra actualmente desactivada";
                }
                else
                {
                    CuentaAhorroAutomaticoDAO.quitarDinero(_cuentaOrigen, pMonto, pCuentaAhorroVistaDestino, Constantes.AHORROVISTA, _comandoMySQL);
                    _comandoMySQL.Transaction.Commit();
                    return "Transacción completada con éxito";
                }
            }
            catch
            {
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                    return "Ha ocurrido un error en la transacción";
                }
                catch
                {
                    return "Ha ocurrido un error en la transacción";
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }
    }
}
