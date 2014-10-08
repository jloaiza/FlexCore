using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.cuentas;

namespace FlexCoreLogic.cuentas.Generales
{
    internal static class GeneradorCuentas
    {
        private static string generarCuentaAux()
        {
            string _numeroCuenta = "";
            int _semilla = (int)DateTime.Now.Millisecond;
            Random _random = new Random(_semilla);
            for(int i = 0; i < 8; i++)
            {
                int _numero = _random.Next(0, 10);
                _numeroCuenta = _numeroCuenta + Convert.ToString(_numero);
                System.Threading.Thread.Sleep(1);
            }
            string _numeroCuentaAux = new string(_numeroCuenta.ToCharArray().OrderBy(s => (_random.Next(2) % 2) == 0).ToArray());
            return _numeroCuentaAux;
        }

        public static string generarCuenta(int pTipoCuenta, int pTipoMoneda, MySqlCommand pComando)
        {
            string _numeroCuenta = "";
            do
            {
                _numeroCuenta = generarCuentaAux();
                if (pTipoCuenta == Constantes.AHORROVISTA)
                {
                    if (pTipoMoneda == Constantes.COLONES)
                    {
                        _numeroCuenta = "9" + _numeroCuenta;
                    }
                    else if (pTipoMoneda == Constantes.DOLARES)
                    {
                        _numeroCuenta = "8" + _numeroCuenta;
                    }

                }
                else if (pTipoCuenta == Constantes.AHORROAUTOMATICO)
                {
                    if (pTipoMoneda == Constantes.COLONES)
                    {
                        _numeroCuenta = "7" + _numeroCuenta;
                    }
                    else if (pTipoMoneda == Constantes.DOLARES)
                    {
                        _numeroCuenta = "6" + _numeroCuenta;
                    }
                }
            } while (CuentaAhorroDAO.existeCuenta(_numeroCuenta, pComando));
            return _numeroCuenta;
        }
    }
}
