using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using FlexCoreDTOs.clients;
using MySql.Data.MySqlClient;
using System.Data;

namespace FlexCoreDAOs.clients
{
    class ClientsDAO
    {

        private void executeNonQuery(string pQuery)
        {
            MySqlConnection connection = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand(pQuery, connection);
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(connection);
        }

        private DataTable executeReadQuery(string pQuery)
        {
            MySqlConnection connection = MySQLManager.nuevaConexion();
            MySqlDataAdapter adapter = new MySqlDataAdapter(pQuery, connection);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            DataTable table = dataSet.Tables[0];
            MySQLManager.cerrarConexion(connection);
            return table;
        }

        private string getInsertQuery(string pTableName, string pColumns, string pValues)
        {
            return String.Format("INSERT INTO {0} ({1}) VALUES ({2})", pTableName, pColumns, pValues);
        }

        private string getDeleteQuery(string pTableName, string pCondition)
        {
            return String.Format("DELTE FROM {0} WHERE {1}", pTableName, pCondition);
        }

        private string getUpdateQuery(string pTableName, string pValues, string pCondition)
        {
            return String.Format("UPDATE {0} SET {1} WHERE {2}", pTableName, pValues, pCondition);
        }

        private string getSelectQuery(string pSelection, string pFrom, string pCondition)
        {
            return String.Format("SELECT {0} FROM {1} WHERE {2}", pSelection, pFrom, pCondition);
        }

        private string getSelectQuery(string pSelection, string pFrom)
        {
            return String.Format("SELECT {0} FROM {1}", pSelection, pFrom);
        }

        private DataTable executeReadQuery(string pSelection, string pFrom, string pCondition)
        {
            
            string query;
            if (pCondition == "")
                query = getSelectQuery(pSelection, pFrom);
            else
                query = getSelectQuery(pSelection, pFrom, pCondition);
            return executeReadQuery(query);
        }

        private void executeUpdateQuery(string pTableName, string pValues, string pCondition)
        {
            string query = getUpdateQuery(pTableName, pValues, pCondition);
            executeNonQuery(query);
        }

        private void exectuteDeleteQuery(string pTableName, string pCondition)
        {
            string query = getDeleteQuery(pTableName, pCondition);
            executeNonQuery(query);
        }

        private void executeInsertQuery(string pTableName, string pColumns, string pValues)
        {
            string query = getInsertQuery(pTableName, pColumns, pValues);
            executeNonQuery(query);
        }

        public void newPerson(PersonDTO pPerson)
        {
            string tableName = "PERSONA";
            string columns = "idPersona, nombre, cedula, tipo";
            string values = String.Format("{0}, {1}, {2}, {3}", 
                pPerson.getPersonID(), pPerson.getName(), pPerson.getIDCard(), pPerson.getPersonType());
            executeInsertQuery(tableName, columns, values);
        }

        public void deletePerson(PersonDTO pPerson)
        {
            string tableName = "PERSONA";
            string condition = "idPersona =" + pPerson.getPersonID();
            exectuteDeleteQuery(tableName, condition);
        }

        public void updatePerson(PersonDTO pPerson)
        {
            string tableName = "PERSONA";
            string values = String.Format("nombre={0}, cedula={1}, tipo={2}", 
                pPerson.getName(), pPerson.getIDCard(), pPerson.GetType());
            string condition = "idPersona =" + pPerson.getPersonID();
            executeUpdateQuery(tableName, values, condition);
        }

        public List<PersonDTO> findPerson(PersonDTO pPerson)
        {
            string selection = "*";
            string from = "PERSONA";
            string condition = "idPersona = " + pPerson.getPersonID();
            DataTable dataTable = executeReadQuery(selection, from, condition);
            List<PersonDTO> list = new List<PersonDTO>();
            foreach (DataRow row in dataTable.Rows)
            {
                PersonDTO person = new PersonDTO(pPerson.getPersonID());
                person.setName(row["nombre"]);
                person.setIDCard(row["cedula"]);
                person.setPersonType(row["tipo"]);
                list.Add(person);
            }
            return list;
        }

    }
}
