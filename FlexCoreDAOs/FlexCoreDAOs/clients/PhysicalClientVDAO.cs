using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    public class PhysicalClientVDAO:GeneralDAO<PhysicalClientVDTO>
    {
        public static readonly string CLIENT_ID = "idCliente";
        public static readonly string CIF = "CIF";
        public static readonly string ACTIVE = "activo";
        public static readonly string NAME = "nombre";
        public static readonly string ID_CARD = "cedula";
        public static readonly string TYPE = "tipo";
        public static readonly string FIRST_LSTNM = "primerApellido";
        public static readonly string SECOND_LSTNM = "segundoApellido";

        private static object _syncLock = new object();
        private static PhysicalClientVDAO _instance;

        public static PhysicalClientVDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PhysicalClientVDAO();
                    }
                }
            }
            return _instance;
        }

        private PhysicalClientVDAO() { }

        protected override string getFindCondition(PhysicalClientVDTO pClient)
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

        protected override void setFindParameters(MySqlCommand pCommand, PhysicalClientVDTO pClient)
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

        public override List<PhysicalClientVDTO> search(PhysicalClientVDTO pClient, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            string selection = "*";
            string from = "CLIENTE_FISICO_V";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pClient);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PhysicalClientVDTO> list = new List<PhysicalClientVDTO>();

            while (reader.Read())
            {
                PhysicalClientVDTO client = new PhysicalClientVDTO();
                client.setClientID((int)reader[CLIENT_ID]);
                client.setCIF(reader[CIF].ToString());
                client.setActive(sqlToBool(reader[ACTIVE].ToString()));
                client.setName(reader[NAME].ToString());
                client.setIDCard(reader[ID_CARD].ToString());
                client.setPersonType(reader[TYPE].ToString());
                client.setFirstLastName(reader[FIRST_LSTNM].ToString());
                client.setSecondLastName(reader[SECOND_LSTNM].ToString());
                list.Add(client);
            }
            return list;
        }

        public override List<PhysicalClientVDTO> getAll(MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            string query = getSelectQuery("*", "CLIENTE_FISICO_V", pPageNumber, pShowCount, pOrderBy);
            pCommand.CommandText = query;
            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PhysicalClientVDTO> list = new List<PhysicalClientVDTO>();
            while (reader.Read())
            {
                PhysicalClientVDTO client = new PhysicalClientVDTO();
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
