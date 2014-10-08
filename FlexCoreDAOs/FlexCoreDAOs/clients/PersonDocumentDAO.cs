using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    public class PersonDocumentDAO:GeneralDAO<PersonDocumentDTO>
    {

        public static readonly string DOCUMENT = "documento";
        public static readonly string DOC_NAME = "nombreDocumento";
        public static readonly string DOC_DESCRIP = "descripcionDocumento";
        public static readonly string PERSON_ID = "idPersona";
        
        private static object _syncLock = new object();
        private static PersonDocumentDAO _instance;

        public static PersonDocumentDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonDocumentDAO();
                    }
                }
            }
            return _instance;
        }

        private PersonDocumentDAO() { }

        protected override string getFindCondition(PersonDocumentDTO pDocument)
        {
            
            string condition = "";
            if (pDocument.getPersonID() != DTOConstants.DEFAULT_INT_ID)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", PERSON_ID));
            }
            if (pDocument.getName() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} LIKE @{0}", DOC_NAME));
            }
            if (pDocument.getDescription() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} LIKE @{0}", DOC_DESCRIP));
            }
            
            return condition;
        }

        protected override void setFindParameters(MySqlCommand pCommand, PersonDocumentDTO pDocument)
        {
            if (pDocument.getPersonID() != DTOConstants.DEFAULT_INT_ID)
            {
                pCommand.Parameters.AddWithValue("@"+PERSON_ID, pDocument.getPersonID());
            }
            if (pDocument.getName() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@"+DOC_NAME, pDocument.getName());
            }
            if (pDocument.getDescription() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@"+DOC_DESCRIP, pDocument.getDescription());
            }
        }

        public override void insert(PersonDocumentDTO pDocument, MySqlCommand pCommand)
        {
            string tableName = "DOCUMENTO_PERSONA";
            string columns = String.Format("{0}, {1}, {2}, {3}", DOCUMENT, DOC_NAME, DOC_DESCRIP, PERSON_ID);
            string values = String.Format("@{0}, @{1}, @{2}, @{3}", DOCUMENT, DOC_NAME, DOC_DESCRIP, PERSON_ID);
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+DOCUMENT, pDocument.getFile());
            pCommand.Parameters.AddWithValue("@"+DOC_NAME, pDocument.getName());
            pCommand.Parameters.AddWithValue("@"+DOC_DESCRIP, pDocument.getDescription());
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pDocument.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void delete(PersonDocumentDTO pDocument, MySqlCommand pCommand)
        {
            string tableName = "DOCUMENTO_PERSONA";
            string condition = String.Format("{0}= @{0} AND {1}=@{1}", PERSON_ID, DOC_NAME);
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pDocument.getPersonID());
            pCommand.Parameters.AddWithValue("@"+DOC_NAME, pDocument.getName());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonDocumentDTO pNewDoc, PersonDocumentDTO pPastDoc, MySqlCommand pCommand)
        {
            string tableName = "DOCUMENTO_PERSONA";
            string values = String.Format("{0}=@nuevo{0}, {1}=@nuevo{1}, {2}=@nuevo{2}, {3}=@nuevo{3}", DOCUMENT, PERSON_ID, DOC_NAME, DOC_DESCRIP);
            string condition = String.Format("{0}= @{0}Anterior AND {1}=@{1}Anterior", PERSON_ID, DOC_NAME);
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo"+DOCUMENT, pNewDoc.getFile());
            pCommand.Parameters.AddWithValue("@nuevo"+PERSON_ID, pNewDoc.getPersonID());
            pCommand.Parameters.AddWithValue("@nuevo"+DOC_NAME, pNewDoc.getName());
            pCommand.Parameters.AddWithValue("@nuevo"+DOC_DESCRIP, pNewDoc.getDescription());
            pCommand.Parameters.AddWithValue("@"+PERSON_ID+"Anterior", pPastDoc.getPersonID());
            pCommand.Parameters.AddWithValue("@" + DOC_NAME + "Anterior", pPastDoc.getName());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonDocumentDTO> search(PersonDocumentDTO pDocument, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            string selection = "*";
            string from = "DOCUMENTO_PERSONA";
            string condition = getFindCondition(pDocument);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pDocument);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonDocumentDTO> list = new List<PersonDocumentDTO>();

            while (reader.Read())
            {
                PersonDocumentDTO client = new PersonDocumentDTO();
                client.setPersonID((int)reader[PERSON_ID]);
                client.setName(reader[DOC_NAME].ToString());
                client.setDocHexBytes((byte[])reader[DOCUMENT]);
                client.setDescription(reader[DOC_DESCRIP].ToString());
                list.Add(client);
            }
            return list;
        }
    }
}
