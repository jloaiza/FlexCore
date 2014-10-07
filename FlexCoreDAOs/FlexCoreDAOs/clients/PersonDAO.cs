using System;
using System.Collections.Generic;
using FlexCoreDTOs.clients;
using MySql.Data.MySqlClient;
using ConexionMySQLServer.ConexionMySql;

namespace FlexCoreDAOs.clients
{
    public class PersonDAO:GeneralDAO<PersonDTO>
    {
        public static readonly string JURIDICAL_PERSON = "Juridica";
        public static readonly string PHYSICAL_PERSON = "Fisica";

        public static readonly string PERSON_ID = "idPersona";
        public static readonly string NAME = "nombre";
        public static readonly string ID_CARD = "cedula";
        public static readonly string TYPE = "tipo";

        private static object _syncLock = new object();
        private static PersonDAO _instance = null;

        public static PersonDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonDAO();
                    }
                }
            }
            return _instance;
        }


        private PersonDAO() { }

        protected override string getFindCondition(PersonDTO pPerson)
        {
            string condition = "";
            if (pPerson.getPersonID() != DTOConstants.DEFAULT_INT_ID)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", PERSON_ID));
            }
            if (pPerson.getName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} LIKE @{0}", NAME));
            }
            if (pPerson.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", ID_CARD));
            }
            if (pPerson.getPersonType() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", TYPE));
            }
            return condition;
        }

        protected override void setFindParameters(MySqlCommand pCommand, PersonDTO pPerson)
        {
            if (pPerson.getPersonID() != DTOConstants.DEFAULT_INT_ID)
            {
                pCommand.Parameters.AddWithValue("@"+PERSON_ID, pPerson.getPersonID());
            }

            if (pPerson.getName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@"+NAME, pPerson.getName());
            }
            if (pPerson.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@"+ID_CARD, pPerson.getIDCard());
            }
            if (pPerson.getPersonType() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@"+TYPE, pPerson.getPersonType());
            }
        }

        public override void insert(PersonDTO pPerson, MySqlCommand pCommand)
        {
            string tableName = "PERSONA";
            string columns = String.Format("{0}, {1}, {2}", NAME, ID_CARD, TYPE);
            string values = String.Format("@{0}, @{1}, @{2}", NAME, ID_CARD, TYPE);
            string query = getInsertQuery(tableName, columns, values);
            
            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+NAME, pPerson.getName());
            pCommand.Parameters.AddWithValue("@"+ID_CARD, pPerson.getIDCard());
            pCommand.Parameters.AddWithValue("@"+TYPE, pPerson.getPersonType());
            pCommand.ExecuteNonQuery();
        }
        public override void delete(PersonDTO pPerson, MySqlCommand pCommand)
        {
            string tableName = "PERSONA";
            string condition = String.Format("{0} = @{0} OR {1}=@{1}", PERSON_ID, ID_CARD);
            string query = getDeleteQuery(tableName, condition);
            
            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pPerson.getPersonID());
            pCommand.Parameters.AddWithValue("@"+ID_CARD, pPerson.getIDCard());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonDTO pNewPerson, PersonDTO pPastPerson, MySqlCommand pCommand)
        {
            string tableName = "PERSONA";
            //Añadir trigger que valide el cambio de tipo
            string values = String.Format("{0}=@nuevo{0}, {1}=@nuevo{1}, {2}=@nuevo{2}, {3}=@nuevo{3}", PERSON_ID, NAME, ID_CARD, TYPE);
            string condition = String.Format("{0} = @{0}Anterior OR {1} = @{1}Anterior", PERSON_ID, ID_CARD);
            string query = getUpdateQuery(tableName, values, condition);
            
            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo"+PERSON_ID, pNewPerson.getPersonID());
            pCommand.Parameters.AddWithValue("@nuevo"+NAME, pNewPerson.getName());
            pCommand.Parameters.AddWithValue("@nuevo"+ID_CARD, pNewPerson.getIDCard());
            pCommand.Parameters.AddWithValue("@nuevo"+TYPE, pNewPerson.getPersonType());
            pCommand.Parameters.AddWithValue("@"+PERSON_ID+"Anterior", pPastPerson.getPersonID());
            pCommand.Parameters.AddWithValue("@"+ID_CARD+"Anterior", pPastPerson.getIDCard());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonDTO> search(PersonDTO pPerson, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            string selection = "*";
            string from = "PERSONA";
            string condition = getFindCondition(pPerson);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pPerson);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonDTO> list = new List<PersonDTO>();

            while (reader.Read())
            {
                PersonDTO person = new PersonDTO();
                person.setPersonID((int)reader[PERSON_ID]);
                person.setName(reader[NAME].ToString());
                person.setIDCard(reader[ID_CARD].ToString());
                person.setPersonType(reader[TYPE].ToString());
                list.Add(person);
            }
            return list;
        }

        public List<PersonDTO> searchJuridical(PersonDTO pPerson, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            string selection = "*";
            string from = "PERSONA_JURIDICA_V";
            string condition = getFindCondition(pPerson);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pPerson);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonDTO> list = new List<PersonDTO>();

            while (reader.Read())
            {
                PersonDTO person = new PersonDTO();
                person.setPersonID((int)reader[PERSON_ID]);
                person.setName(reader[NAME].ToString());
                person.setIDCard(reader[ID_CARD].ToString());
                person.setPersonType(reader[TYPE].ToString());
                list.Add(person);
            }
            return list;
        }

        public override List<PersonDTO> getAll(MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            string query = getSelectQuery("*", "PERSONA", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonDTO> list = new List<PersonDTO>();
            while (reader.Read())
            {
                PersonDTO person = new PersonDTO();
                person.setPersonID((int)reader[PERSON_ID]);
                person.setName(reader[NAME].ToString());
                person.setIDCard(reader[ID_CARD].ToString());
                person.setPersonType(reader[TYPE].ToString());
                list.Add(person);
            }
            return list;
        }

        public List<PersonDTO> getAllJuridical(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            MySqlCommand command = getCommand();
            List<PersonDTO> result = getAllJuridical(command, pPageNumber, pShowCount, pOrderBy);
            MySQLManager.cerrarConexion(command.Connection);
            return result;
        }

        public List<PersonDTO> getAllJuridical(MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            string query = getSelectQuery("*", "PERSONA_JURIDICA_V", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonDTO> list = new List<PersonDTO>();
            while (reader.Read())
            {
                PersonDTO person = new PersonDTO();
                person.setPersonID((int)reader[PERSON_ID]);
                person.setName(reader[NAME].ToString());
                person.setIDCard(reader[ID_CARD].ToString());
                person.setPersonType(reader[TYPE].ToString());
                list.Add(person);
            }
            return list;
        }

    }
}
