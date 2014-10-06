using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    class JuridicalClientViewDAO:GeneralDAO<JuridicalClientVDTO>
    {

        private static readonly string CLIENT_ID = "idCliente";
        private static readonly string CIF = "CIF";
        private static readonly string ACTIVE = "activo";   
        private static readonly string NAME = "nombre";
        private static readonly string ID_CARD = "cedula";
        private static readonly string TYPE = "tipo";

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

        public override List<JuridicalClientVDTO> search(JuridicalClientVDTO pClient, MySqlCommand pCommand)
        {
            string selection = "*";
            string from = "CLIENTE_JURIDICO_V";
            string condition = getFindCondition(pClient);
            string query = getSelectQuery(selection, from, condition);

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

        public override List<JuridicalClientVDTO> getAll(MySqlCommand pCommand)
        {
            string query = "SELECT * FROM CLIENTE_JURIDICO_V";
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
