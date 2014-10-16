using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FlexCoreDTOs.cuentas;
using MySql.Data.MySqlClient;
using ConexionMySQLServer.ConexionMySql;
using FlexCoreDAOs.cuentas;
using FlexCoreLogic.cuentas.Generales;

namespace FlexCoreLogic.cuentas.Managers
{
    internal static class CuentaAhorroVistaManager
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

        public static string agregarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                string _numeroCuenta = GeneradorCuentas.generarCuenta(Constantes.AHORROVISTA, pCuentaAhorroVista.getTipoMoneda(), _comandoMySQL);
                pCuentaAhorroVista.setNumeroCuenta(_numeroCuenta);
                pCuentaAhorroVista.setSaldo(0);
                pCuentaAhorroVista.setSaldoFlotante(0);
                CuentaAhorroVistaDAO.agregarCuentaAhorroVistaBase(pCuentaAhorroVista, _comandoMySQL);
                _comandoMySQL.Transaction.Commit();
                return "Transacción completada con éxito";
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

        public static string eliminarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.eliminarCuentaAhorroVistaBase(pCuentaAhorroVista, _comandoMySQL);
                _comandoMySQL.Transaction.Commit();
                return "Transacción completada con éxito";
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

        public static string modificarCuentaAhorroVista(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.modificarCuentaAhorroVistaBase(pCuentaAhorroVista, _comandoMySQL);
                _comandoMySQL.Transaction.Commit();
                return "Transacción completada con éxito";
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

        public static CuentaAhorroVistaDTO obtenerCuentaAhorroVistaNumeroCuenta(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDTO _cuentaSalida = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaNumeroCuenta(pCuentaAhorroVista, _comandoMySQL);
                _comandoMySQL.Transaction.Commit();
                return _cuentaSalida;
            }
            catch
            {
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaCedula(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                List<CuentaAhorroVistaDTO> _cuentaSalida = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaCedulaOCIF(pCuentaAhorroVista, _comandoMySQL);
                _comandoMySQL.Transaction.Commit();
                return _cuentaSalida;
            }
            catch
            {
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }

        public static List<CuentaAhorroVistaDTO> obtenerCuentaAhorroVistaCIF(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                List<CuentaAhorroVistaDTO> _cuentasSalida = CuentaAhorroVistaDAO.obtenerCuentaAhorroVistaCedulaOCIF(pCuentaAhorroVista, _comandoMySQL);
                _comandoMySQL.Transaction.Commit();
                return _cuentasSalida;
            }
            catch
            {
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                    return null;
                }
                catch
                {
                    return null;
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }

        public static int obtenerCuentaAhorroVistaID(CuentaAhorroVistaDTO pCuentaAhorroVista)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                int _id = CuentaAhorroDAO.obtenerCuentaAhorroID(pCuentaAhorroVista, _comandoMySQL);
                _comandoMySQL.Transaction.Commit();
                return _id;
            }
            catch
            {
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }

        public static string agregarDinero(CuentaAhorroVistaDTO pCuentaAhorroVista, decimal pMonto)
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.agregarDinero(pCuentaAhorroVista, pMonto, Constantes.AHORROVISTA, _comandoMySQL);
                _comandoMySQL.Transaction.Commit();
                return "Transacción completada con éxito";
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

        public static string realizarCierreCuentas()
        {
            try
            {
                ThreadStart _delegado = new ThreadStart(realizarCierreCuentasAux);
                Thread _hiloReplica = new Thread(_delegado);
                _hiloReplica.Start();
                return "Transacción completada con éxito";
            }
            catch
            {
                return "Ha ocurrido un error en la transacción";
            }
        }

        private static void realizarCierreCuentasAux()
        {
            MySqlCommand _comandoMySQL = obtenerConexionSQL();
            try
            {
                CuentaAhorroVistaDAO.iniciarCierre(_comandoMySQL);
                _comandoMySQL.Transaction.Commit();
            }
            catch
            {
                try
                {
                    _comandoMySQL.Transaction.Rollback();
                }
                catch
                {
                    return;
                }
            }
            finally
            {
                MySQLManager.cerrarConexion(_comandoMySQL.Connection);
            }
        }
    }
}
