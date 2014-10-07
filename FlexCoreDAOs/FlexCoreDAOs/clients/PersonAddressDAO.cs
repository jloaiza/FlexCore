using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{   

    public class PersonAddressDAO:GeneralDAO<PersonAddressDTO>
    {

        public static readonly string ADDRESS = "direccion";
        public static readonly string PERSON_ID = "idCliente";

        private static object _syncLock = new object();
        private static PersonAddressDAO _instance;

        public static PersonAddressDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonAddressDAO();
                    }
                }
            }
            return _instance;
        }

        private PersonAddressDAO() { }

        public override void insert(PersonAddressDTO pAddress, MySqlCommand pCommand)
        {
            string tableName = "DIRECCION_PERSONA";
            string columns = String.Format("{0}, {1}", ADDRESS, PERSON_ID);
            string values = String.Format("@{0}, @{1}", ADDRESS, PERSON_ID);
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+ADDRESS, pAddress.getAddress());
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pAddress.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void delete(PersonAddressDTO pAddress, MySqlCommand pCommand)
        {
            string tableName = "DIRECCION_PERSONA";
            string condition = String.Format("{0}= @{0} AND {1}=@{1}", PERSON_ID, ADDRESS);
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pAddress.getPersonID());
            pCommand.Parameters.AddWithValue("@" + ADDRESS, pAddress.getAddress());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonAddressDTO pNewAddress, PersonAddressDTO pPastAddress, MySqlCommand pCommand)
        {
            string tableName = "DIRECCION_PERSONA";
            string values = String.Format("{0}=@nuevo{0}, {1}=@nuevo{1}", PERSON_ID, ADDRESS);
            string condition = String.Format("{0}= @{0}Anterior AND {1}=@{1}Anterior", PERSON_ID);
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo"+PERSON_ID, pNewAddress.getPersonID());
            pCommand.Parameters.AddWithValue("@nuevo"+ADDRESS, pNewAddress.getAddress());
            pCommand.Parameters.AddWithValue("@"+PERSON_ID+"Anterior", pPastAddress.getPersonID());
            pCommand.Parameters.AddWithValue("@"+ADDRESS+"Anterior", pPastAddress.getAddress());

            pCommand.ExecuteNonQuery();
        }

        public override List<PersonAddressDTO> search(PersonAddressDTO pAddress, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            string selection = "*";
            string from = "DIRECCION_PERSONA";
            string condition = String.Format("{0} = @{0}", PERSON_ID);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pAddress.getPersonID());

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonAddressDTO> list = new List<PersonAddressDTO>();

            while (reader.Read())
            {
                PersonAddressDTO Address = new PersonAddressDTO(pAddress.getPersonID());
                Address.setAddress(reader[ADDRESS].ToString());
                list.Add(Address);
            }
            return list;
        }
    }
}
