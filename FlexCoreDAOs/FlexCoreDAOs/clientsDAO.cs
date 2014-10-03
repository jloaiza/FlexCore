using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConexionMySQLServer.ConexionMySql;
using FlexCoreDTOs.clients;
using MySql.Data.MySqlClient;
using System.Data;

namespace FlexCoreDAOs
{

    public class ClientsDAO
    {

        private string getInsertQuery(string pTableName, string pColumns, string pValues)
        {
            return String.Format("INSERT INTO {0} ({1}) VALUES ({2})", pTableName, pColumns, pValues);
        }

        private string getDeleteQuery(string pTableName, string pCondition)
        {
            return String.Format("DELETE FROM {0} WHERE {1}", pTableName, pCondition);
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

        private MySqlCommand getCommand()
        {
            MySqlConnection _conexionMySQLBase = MySQLManager.nuevaConexion();
            return _conexionMySQLBase.CreateCommand();
        }

        //---------------------------------------------------

        public void newPerson(PersonDTO pPerson)
        {
            string tableName = "PERSONA";
            string columns = "nombre, cedula, tipo";
            string values = "@nombre, @cedula, @tipo";
            string query = getInsertQuery(tableName, columns, values);
            MySqlCommand command = getCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@nombre", pPerson.getName());
            command.Parameters.AddWithValue("@cedula", pPerson.getIDCard());
            command.Parameters.AddWithValue("@tipo", pPerson.getPersonType());
            command.ExecuteNonQuery();

            MySQLManager.cerrarConexion(command.Connection);
        }

        public void deletePerson(PersonDTO pPerson)
        {
            string tableName = "PERSONA";
            string condition = "idPersona = @idPersona";
            string query = getDeleteQuery(tableName, condition);
            MySqlCommand command = getCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@idPersona", pPerson.getPersonID());
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(command.Connection);
        }

        public void updatePerson(PersonDTO pNewPerson, PersonDTO pOldPerson)
        {
            string tableName = "PERSONA";
            //Añadir trigger que valide el cambio de tipo
            string values = "idPersona=@nuevoIdPersona, nombre=@nuevoNombre, cedula=@nuevaCedula, tipo=@nuevoTipo";
            string condition = "idPersona = @idPersonaAnterior";
            string query = getUpdateQuery(tableName, values, condition);
            MySqlCommand command = getCommand();
            command.CommandText = query;
            command.Parameters.AddWithValue("@nuevoIdPersona", pNewPerson.getPersonID());
            command.Parameters.AddWithValue("@nuevoNombre", pNewPerson.getName());
            command.Parameters.AddWithValue("@nuevaCedula", pNewPerson.getIDCard());
            command.Parameters.AddWithValue("@nuevoTipo", pNewPerson.getPersonType());
            command.Parameters.AddWithValue("@idPersonaAnterior", pOldPerson.getPersonID());
            command.ExecuteNonQuery();
            MySQLManager.cerrarConexion(command.Connection);
        }

        private string addCondition(string pBuffer, string pCondition)
        {
            if (pBuffer != "")
            {
                pBuffer += " AND ";
            }
            pBuffer += pCondition;
            return pBuffer;
        }

        private string getFindPersonCondition(PersonDTO pPerson)
        {
            //CAMBIAR POR DEFAULTS
            string condition = "";
            if (pPerson.getPersonID() != -1)
                Console.WriteLine("person id");
                condition = addCondition(condition, "idPersona = @idPersona");
            if (pPerson.getName() != "")
                Console.WriteLine("person name");
                condition = addCondition(condition, "nombre = @nombre");
            if (pPerson.getIDCard() != "")
                Console.WriteLine("person card");
                condition = addCondition(condition, "cedula = @cedula");
            if (pPerson.getPersonType() != "")
                Console.WriteLine("person type");
                condition = addCondition(condition, "tipo = @tipo");
            return condition;
        }

        private void setCommandFindClientParameters(MySqlCommand pCommand, PersonDTO pPerson)
        {
            if (pPerson.getPersonID() != -1)
                pCommand.Parameters.AddWithValue("@idPersona", pPerson.getPersonID());
            if (pPerson.getName() != "")
                pCommand.Parameters.AddWithValue("@nombre", pPerson.getName());
            if (pPerson.getIDCard() != "")
                pCommand.Parameters.AddWithValue("@cedula", pPerson.getIDCard());
            if (pPerson.getPersonType() != "")
                pCommand.Parameters.AddWithValue("@tipo", pPerson.getPersonType());
        }

        public List<PersonDTO> findPerson(PersonDTO pPerson)
        {
            string selection = "*";
            string from = "PERSONA";
            string condition = getFindPersonCondition(pPerson);
            string query = getSelectQuery(selection, from, condition);
            //query = "SELECT * FROM PERSONA WHERE nombre = 'Joseph' AND cedula = @cedula";
            MySqlCommand command = getCommand();
            command.CommandText = query;
            setCommandFindClientParameters(command, pPerson);
            Console.WriteLine("query {0} {1}", command.CommandText, command.Parameters.Count);

            MySqlDataReader reader = command.ExecuteReader();
            List<PersonDTO> list = new List<PersonDTO>();

            while (reader.Read())
            {
                PersonDTO person = new PersonDTO();
                person.setPersonID((int)reader["idPersona"]);
                person.setName(reader["nombre"].ToString());
                person.setIDCard(reader["cedula"].ToString());
                person.setPersonType(reader["tipo"].ToString());
                list.Add(person);
            }
            MySQLManager.cerrarConexion(command.Connection);
            return list;
        }

        public void newPhysicalPerson(PhysicalPersonDTO pPerson)
        {
            string tableName = "PERSONA_FISICA";
            string columns = "primerApellido, segundoApellido, idPersona";
            string values = String.Format("{0}, {1}, {2}",
                pPerson.getFirstLastName(), pPerson.getSecondLastName(), pPerson.getPersonID());
            

        }

        public void deletePhysicalPerson(PhysicalPersonDTO pPerson)
        {
            string tableName = "PERSONA_FISICA";
            string condition = "idPersona = " + pPerson.getPersonID();
            

        }

        public void updatePhysicalPerson(PhysicalPersonDTO pNewPerson, PhysicalPersonDTO pOldPerson)
        {
            string tableName = "PERSONA_FISICA";
            string values = String.Format("primerApellido");
        }



    }
}
