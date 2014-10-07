using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    public class PersonPhoneDAO:GeneralDAO<PersonPhoneDTO>
    {

        public static readonly string PHONE = "telefono";
        public static readonly string PERSON_ID = "idPersona";

        private static object _syncLock = new object();
        private static PersonPhoneDAO _instance;

        public static PersonPhoneDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonPhoneDAO();
                    }
                }
            }
            return _instance;
        }

        private PersonPhoneDAO() { }

        public override void insert(PersonPhoneDTO pPhone, MySqlCommand pCommand)
        {
            string tableName = "TELEFONO_PERSONA";
            string columns = String.Format("{0}, {1}", PERSON_ID, PHONE);
            string values = String.Format("@{0}, @{1}", PERSON_ID, PHONE);
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPhone.getPersonID());
            pCommand.Parameters.AddWithValue("@" + PHONE, pPhone.getPhone());
            pCommand.ExecuteNonQuery();
        }
        public override void delete(PersonPhoneDTO pPhone, MySqlCommand pCommand)
        {
            string tableName = "TELEFONO_PERSONA";
            string condition = String.Format("{0} = @{0} AND {1}=@{1}", PERSON_ID, PHONE);
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pPhone.getPersonID());
            pCommand.Parameters.AddWithValue("@" + PHONE, pPhone.getPhone());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonPhoneDTO pNewPhone, PersonPhoneDTO pPastPhone, MySqlCommand pCommand)
        {
            string tableName = "TELEFONO_PERSONA";
            string values = String.Format("{0}=@nuevo{0}, {1}=@nuevo{1}", PERSON_ID, PHONE);
            string condition = String.Format("{0} = @{0}Anterior AND {1} = @{1}Anterior", PERSON_ID, PHONE);
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo" + PERSON_ID, pNewPhone.getPersonID());
            pCommand.Parameters.AddWithValue("@nuevo" + PHONE, pNewPhone.getPhone());
            pCommand.Parameters.AddWithValue("@" + PERSON_ID + "Anterior", pPastPhone.getPersonID());
            pCommand.Parameters.AddWithValue("@" + PHONE + "Anterior", pPastPhone.getPhone());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonPhoneDTO> search(PersonPhoneDTO pPhone, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            string selection = "*";
            string from = "TELEFONO_PERSONA";
            string condition = getFindCondition(pPhone);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pPhone);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonPhoneDTO> list = new List<PersonPhoneDTO>();

            while (reader.Read())
            {
                PersonPhoneDTO phone = new PersonPhoneDTO(pPhone.getPersonID());
                phone.setPhone(reader[PHONE].ToString());
                list.Add(phone);
            }
            return list;
        }
    }
}
