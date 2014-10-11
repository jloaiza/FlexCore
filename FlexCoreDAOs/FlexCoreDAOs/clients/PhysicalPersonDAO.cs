using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    public class PhysicalPersonDAO:GeneralDAO<PhysicalPersonDTO>
    {
        public static readonly string PERSON_ID = "idPersona";
        public static readonly string NAME = "nombre";
        public static readonly string ID_CARD = "cedula";
        public static readonly string TYPE = "tipo";
        public static readonly string FIRST_LSTNM = "primerApellido";
        public static readonly string SECOND_LSTNM = "segundoApellido";

        private static object _syncLock = new object();
        private static PhysicalPersonDAO _instance;

        public static PhysicalPersonDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PhysicalPersonDAO();
                    }
                }
            }
            return _instance;
        }

        private PhysicalPersonDAO() { }

        protected override string getFindCondition(PhysicalPersonDTO pPerson)
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
            if (pPerson.getFirstLastName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", FIRST_LSTNM));
            }
            if (pPerson.getSecondLastName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", SECOND_LSTNM));
            }
            return condition;
        }

        protected override void setFindParameters(MySqlCommand pCommand, PhysicalPersonDTO pPerson)
        {
            if (pPerson.getPersonID() != DTOConstants.DEFAULT_INT_ID)
            {
                pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPerson.getPersonID());
            }

            if (pPerson.getName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + NAME, pPerson.getName());
            }
            if (pPerson.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + ID_CARD, pPerson.getIDCard());
            }
            if (pPerson.getFirstLastName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + FIRST_LSTNM, pPerson.getFirstLastName());
            }
            if (pPerson.getSecondLastName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + SECOND_LSTNM, pPerson.getSecondLastName());
            }
        }

        public override void insert(PhysicalPersonDTO pPerson, MySqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "PERSONA_FISICA";
            string columns = String.Format("{0}, {1}, {2}", PERSON_ID, FIRST_LSTNM, SECOND_LSTNM);
            string values = String.Format("@{0}, @{1}, @{2}", PERSON_ID, FIRST_LSTNM, SECOND_LSTNM);
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPerson.getName());
            pCommand.Parameters.AddWithValue("@" + FIRST_LSTNM, pPerson.getIDCard());
            pCommand.Parameters.AddWithValue("@" + SECOND_LSTNM, pPerson.getPersonType());
            pCommand.ExecuteNonQuery();
        }
        public override void delete(PhysicalPersonDTO pPerson, MySqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "PERSONA_FISICA";
            string condition = String.Format("{0} = @{0}", PERSON_ID);
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPerson.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PhysicalPersonDTO pNewPerson, PhysicalPersonDTO pPastPerson, MySqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "PERSONA_FISICA";
            string values = String.Format("{0}=@nuevo{0}, {1}=@nuevo{1}", FIRST_LSTNM, SECOND_LSTNM);
            string condition = String.Format("{0} = @{0}Anterior", PERSON_ID);
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo" + FIRST_LSTNM, pNewPerson.getFirstLastName());
            pCommand.Parameters.AddWithValue("@nuevo" + SECOND_LSTNM, pNewPerson.getSecondLastName());
            pCommand.Parameters.AddWithValue("@" + PERSON_ID + "Anterior", pPastPerson.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override List<PhysicalPersonDTO> search(PhysicalPersonDTO pPerson, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string selection = "*";
            string from = "PERSONA_FISICA_V";
            string condition = getFindCondition(pPerson);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pPerson);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PhysicalPersonDTO> list = new List<PhysicalPersonDTO>();

            while (reader.Read())
            {
                PhysicalPersonDTO person = new PhysicalPersonDTO();
                person.setPersonID((int)reader[PERSON_ID]);
                person.setName(reader[NAME].ToString());
                person.setIDCard(reader[ID_CARD].ToString());
                person.setPersonType(reader[TYPE].ToString());
                person.setFirstLastName(reader[FIRST_LSTNM].ToString());
                person.setSecondLastName(reader[SECOND_LSTNM].ToString());
                list.Add(person);
            }
            return list;
        }

        public override List<PhysicalPersonDTO> getAll(MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("*", "PERSONA_FISICA_V", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PhysicalPersonDTO> list = new List<PhysicalPersonDTO>();
            while (reader.Read())
            {
                PhysicalPersonDTO person = new PhysicalPersonDTO();
                person.setPersonID((int)reader[PERSON_ID]);
                person.setName(reader[NAME].ToString());
                person.setIDCard(reader[ID_CARD].ToString());
                person.setPersonType(reader[TYPE].ToString());
                person.setFirstLastName(reader[FIRST_LSTNM].ToString());
                person.setSecondLastName(reader[SECOND_LSTNM].ToString());
                list.Add(person);
            }
            return list;
        }
    }
}
