using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    public class ClientDAO:GeneralDAO<ClientDTO>
    {

        public static readonly string PERSON_ID = "idCliente";
        public static readonly string CIF = "CIF";
        public static readonly string ACTIVE = "activo";
        
        private static object _syncLock = new object();
        private static ClientDAO _instance;

        public static ClientDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientDAO();
                    }
                }
            }
            return _instance;
        }

        private ClientDAO() { }
        protected override string getFindCondition(ClientDTO pClient)
        {
            string condition = "";
            if (pClient.getClientID() != DTOConstants.DEFAULT_INT_ID)
            {
                condition = addCondition(condition, String.Format("{0}= @{0}", PERSON_ID));
            }
            if (pClient.getCIF() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0} LIKE @{0}", CIF));
            }
            return condition;
        }

        protected override void setFindParameters(MySqlCommand pCommand, ClientDTO pClient)
        {
            if (pClient.getClientID() != DTOConstants.DEFAULT_INT_ID)
            {
                pCommand.Parameters.AddWithValue("@" + PERSON_ID, pClient.getClientID());
            }

            if (pClient.getCIF() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + CIF, pClient.getCIF());
            }
        }

        public override void insert(ClientDTO pClient, MySqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "CLIENTE";
            string columns = String.Format("{0}, {1}, {2}", PERSON_ID, CIF, ACTIVE);
            string values = String.Format("@{0}, @{1}, @{2}", PERSON_ID, CIF, ACTIVE);
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pClient.getClientID());
            pCommand.Parameters.AddWithValue("@" + CIF, pClient.getCIF());
            pCommand.Parameters.AddWithValue("@" + ACTIVE, boolToSql(pClient.isActive()));
            pCommand.ExecuteNonQuery();
        }
        public override void delete(ClientDTO pClient, MySqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "CLIENTE";
            string condition = String.Format("{0} = @{0} OR {1}=@{1}", PERSON_ID, CIF);
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pClient.getClientID());
            pCommand.Parameters.AddWithValue("@" + CIF, pClient.getCIF());
            pCommand.ExecuteNonQuery();
        }

        public override void update(ClientDTO pNewClient, ClientDTO pPastClient, MySqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "CLIENTE";
            string values = String.Format("{0}=@nuevo{0}, {1}=@nuevo{1}", CIF, ACTIVE);
            string condition = String.Format("{0} = @{0}Anterior OR {1} = @{1}Anterior", PERSON_ID, CIF);
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo" + CIF, pNewClient.getCIF());
            pCommand.Parameters.AddWithValue("@nuevo" + ACTIVE, boolToSql(pNewClient.isActive()));
            pCommand.Parameters.AddWithValue("@" + PERSON_ID + "Anterior", pPastClient.getClientID());
            pCommand.Parameters.AddWithValue("@" + CIF + "Anterior", pPastClient.getCIF());
            pCommand.ExecuteNonQuery();
        }

        public override List<ClientDTO> search(ClientDTO pClient, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string selection = "*";
            string from = "CLIENTE";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pClient);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<ClientDTO> list = new List<ClientDTO>();

            while (reader.Read())
            {
                ClientDTO client = new ClientDTO();
                client.setClientID((int)reader[PERSON_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                list.Add(client);
            }
            return list;
        }

        public override List<ClientDTO> getAll(MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("*", "CLIENTE", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            MySqlDataReader reader = pCommand.ExecuteReader();
            List<ClientDTO> list = new List<ClientDTO>();
            while (reader.Read())
            {
                ClientDTO client = new ClientDTO();
                client.setClientID((int)reader[PERSON_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                list.Add(client);
            }
            return list;
        }

        public List<ClientDTO> getAllByActive(MySqlCommand pCommand, int pPageNumber, int pShowCount, bool pActive, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("*", "CLIENTE", "activo="+(pActive?"1":"0") ,pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            MySqlDataReader reader = pCommand.ExecuteReader();
            List<ClientDTO> list = new List<ClientDTO>();
            while (reader.Read())
            {
                ClientDTO client = new ClientDTO();
                client.setClientID((int)reader[PERSON_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                list.Add(client);
            }
            return list;
        }

        public void setActive(ClientDTO pClient, MySqlCommand pCommand)
        {
            pCommand.Parameters.Clear();
            string tableName = "CLIENTE";
            string values = String.Format("{1}=@nuevo{1}", ACTIVE);
            string condition = String.Format("{0} = @{0} OR {1} = @{1}", PERSON_ID, CIF);
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo" + ACTIVE, boolToSql(pClient.isActive()));
            pCommand.Parameters.AddWithValue("@" + PERSON_ID, pClient.getClientID());
            pCommand.Parameters.AddWithValue("@" + CIF, pClient.getCIF());
            pCommand.ExecuteNonQuery();
        }



    }
}
