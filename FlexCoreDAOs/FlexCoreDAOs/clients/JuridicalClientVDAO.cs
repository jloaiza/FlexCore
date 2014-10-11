using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    public class JuridicalClientVDAO:GeneralDAO<JuridicalClientVDTO>
    {

        public static readonly string CLIENT_ID = "idCliente";
        public static readonly string CIF = "CIF";
        public static readonly string ACTIVE = "activo";
        public static readonly string NAME = "nombre";
        public static readonly string ID_CARD = "cedula";
        public static readonly string TYPE = "tipo";

        private static object _syncLock = new object();
        private static JuridicalClientVDAO _instance;

        public static JuridicalClientVDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new JuridicalClientVDAO();
                    }
                }
            }
            return _instance;
        }

        private JuridicalClientVDAO() { }

        protected override string getFindCondition(JuridicalClientVDTO pClient)
        {
            string condition = "";
            if (pClient.getClientID() != DTOConstants.DEFAULT_INT_ID)
            {
                condition = addCondition(condition, String.Format("{0}=@{0}", CLIENT_ID));
            }
            if (pClient.getCIF() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}=@{0}", CIF));
            }
            if (pClient.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                condition = addCondition(condition, String.Format("{0}=@{0}", CIF));
            }
            return condition;
        }

        protected override void setFindParameters(MySqlCommand pCommand, JuridicalClientVDTO pClient)
        {
            if (pClient.getClientID() != DTOConstants.DEFAULT_INT_ID)
            {
                pCommand.Parameters.AddWithValue("@" + CLIENT_ID, pClient.getClientID());
            }

            if (pClient.getCIF() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + CIF, pClient.getCIF());
            }
            if (pClient.getIDCard() != DTOConstants.DEFAULT_STRING)
            {
                pCommand.Parameters.AddWithValue("@" + ID_CARD, pClient.getIDCard());
            }
        }

        public override List<JuridicalClientVDTO> search(JuridicalClientVDTO pClient, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string selection = "*";
            string from = "CLIENTE_JURIDICO_V";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pClient);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<JuridicalClientVDTO> list = new List<JuridicalClientVDTO>();

            while (reader.Read())
            {
                JuridicalClientVDTO client = new JuridicalClientVDTO();
                client.setClientID((int)reader[CLIENT_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                client.setName(reader[NAME].ToString());
                client.setIDCard(reader[ID_CARD].ToString());
                client.setPersonType(reader[TYPE].ToString());
                list.Add(client);
            }
            return list;
        }

        public override List<JuridicalClientVDTO> getAll(MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            pCommand.Parameters.Clear();
            string query = getSelectQuery("*", "CLIENTE_JURIDICO_V", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            MySqlDataReader reader = pCommand.ExecuteReader();
            List<JuridicalClientVDTO> list = new List<JuridicalClientVDTO>();
            while (reader.Read())
            {
                JuridicalClientVDTO client = new JuridicalClientVDTO();
                client.setClientID((int)reader[CLIENT_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                client.setName(reader[NAME].ToString());
                client.setIDCard(reader[ID_CARD].ToString());
                client.setPersonType(reader[TYPE].ToString());
                list.Add(client);
            }
            return list;
        }
    }
}
