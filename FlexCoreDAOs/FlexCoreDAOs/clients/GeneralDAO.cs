using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;

namespace FlexCoreDAOs.clients
{
    public abstract class GeneralDAO<T>
    {
        protected string getInsertQuery(string pTableName, string pColumns, string pValues)
        {
            return String.Format("INSERT INTO {0} ({1}) VALUES ({2})", pTableName, pColumns, pValues);
        }

        protected string getDeleteQuery(string pTableName, string pCondition)
        {
            return String.Format("DELETE FROM {0} WHERE {1}", pTableName, pCondition);
        }

        protected string getUpdateQuery(string pTableName, string pValues, string pCondition)
        {
            return String.Format("UPDATE {0} SET {1} WHERE {2}", pTableName, pValues, pCondition);
        }

        protected string getSelectQuery(string pSelection, string pFrom, string pCondition)
        {
            return String.Format("SELECT {0} FROM {1} WHERE {2}", pSelection, pFrom, pCondition);
        }

        protected string getSelectQuery(string pSelection, string pFrom)
        {
            return String.Format("SELECT {0} FROM {1}", pSelection, pFrom);
        }

        protected MySqlCommand getCommand()
        {
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            return _conexionMySQLBase.CreateCommand();
        }

        protected string addCondition(string pBuffer, string pCondition)
        {
            if (pBuffer != "")
            {
                pBuffer += " AND ";
            }
            pBuffer += pCondition;
            return pBuffer;
        }

        protected string boolToSql(bool pBool)
        {
            return pBool ? "TRUE" : "FALSE";
        }

        protected bool sqlToBool(string pSQL)
        {
            if (pSQL.ToLower() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected string bytesToHex(Byte[] pBytes)
        {
            return BitConverter.ToString(pBytes).Replace("-", string.Empty);
        }

        public virtual void insert(T pDTO)
        {
            MySqlCommand command = getCommand();
            insert(pDTO, command);
            MySQLManager.cerrarConexion(command.Connection);
        }

        public virtual void delete(T pDTO)
        {
            MySqlCommand command = getCommand();
            delete(pDTO, command);
            MySQLManager.cerrarConexion(command.Connection);
        }

        public virtual void update(T pNewDTO, T pPastDTO)
        {
            MySqlCommand command = getCommand();
            update(pNewDTO, pPastDTO, command);
            MySQLManager.cerrarConexion(command.Connection);
        }

        public virtual List<T> search(T pDTO)
        {
            MySqlCommand command = getCommand();
            List<T> result = search(pDTO, command);
            MySQLManager.cerrarConexion(command.Connection);
            return result;
        }

        public virtual List<T> getAll()
        {
            MySqlCommand command = getCommand();
            List<T> result = getAll(command);
            MySQLManager.cerrarConexion(command.Connection);
            return result;
        }

        protected virtual string getFindCondition(T pDTO)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }
        protected virtual void setFindParameters(MySqlCommand pCommand, T pDTO)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public virtual void insert(T pDTO, MySqlCommand pCommand)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public virtual void delete(T pDTO, MySqlCommand pCommand)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public virtual void update(T pNewDTO, T pPastDTO, MySqlCommand pCommand)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public virtual List<T> search(T pDTO, MySqlCommand pCommand)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public virtual List<T> getAll(MySqlCommand pCommand)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }
    }
}
