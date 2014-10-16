using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;

namespace FlexCoreDAOs.clients
{
    public abstract class GeneralDAO<T>
    {

        private int getRowOffset(int pPageNumber, int pShowCount)
        {
            return pShowCount * (pPageNumber - 1);
        }

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

        protected string getSelectQuery(string pSelection, string pFrom, string pCondition, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            if (pPageNumber != 0)
            {
                string ordBuffer = "";
                foreach (String ord in pOrderBy)
                {
                    ordBuffer = addCondition(ordBuffer, ord);
                }
                int offset = getRowOffset(pPageNumber, pShowCount);
                return String.Format("SELECT {0} FROM {1} WHERE {2} ORDER BY {3} DESC LIMIT {4} OFFSET {5}", pSelection, pFrom, pCondition, ordBuffer, pShowCount, offset);
            }
            else
            {
                return String.Format("SELECT {0} FROM {1} WHERE {2}", pSelection, pFrom, pCondition);
            }
            
        }

        protected string getSelectQuery(string pSelection, string pFrom, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            string ordBuffer = "";
            foreach (String ord in pOrderBy)
            {
                ordBuffer = addCondition(ordBuffer, ord);
            }
            int offset = getRowOffset(pPageNumber, pShowCount);
            return String.Format("SELECT {0} FROM {1} ORDER BY {2} DESC LIMIT {3} OFFSET {4}", pSelection, pFrom, ordBuffer, pShowCount, offset);
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

        protected int boolToSql(bool pBool)
        {
            return pBool ? 1 : 0;
        }

        protected bool sqlToBool(string pValue)
        {
            if (pValue == "True")
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

        public virtual List<T> search(T pDTO, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            MySqlCommand command = getCommand();
            List<T> result = search(pDTO, command, pPageNumber, pShowCount, pOrderBy);
            MySQLManager.cerrarConexion(command.Connection);
            return result;
        }

        public virtual List<T> search(T pDTO)
        {
            MySqlCommand command = getCommand();
            List<T> result = search(pDTO, command);
            MySQLManager.cerrarConexion(command.Connection);
            return result;
        }

        public virtual List<T> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            MySqlCommand command = getCommand();
            List<T> result = getAll(command, pPageNumber, pShowCount, pOrderBy);
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

        public virtual List<T> search(T pDTO, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }

        public virtual List<T> getAll(MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            // Not developed yet.
            throw new NotImplementedException();
        }
    }
}
