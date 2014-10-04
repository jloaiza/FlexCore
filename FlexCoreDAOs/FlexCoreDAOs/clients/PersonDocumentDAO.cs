using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    class PersonDocumentDAO:GeneralDAO<PersonDocumentDTO>
    {
        protected override string getFindCondition(PersonDocumentDTO pDocument)
        {
            //CAMBIAR POR DEFAULTS
            string condition = "";
            if (pDocument.getPersonID() != -1)
            {
                condition = addCondition(condition, "idCliente = @idCliente");
            }
            if (pDocument.getName() != "")
            {
                condition = addCondition(condition, "nombreDocumento LIKE %@nombreDocumento%");
            }
            if (pDocument.getDescription() != "")
            {
                condition = addCondition(condition, "descripcionDocumento LIKE %@descripcionDocumento%");
            }
            
            return condition;
        }

        protected override void setFindParameters(MySqlCommand pCommand, PersonDocumentDTO pDocument)
        {
            if (pDocument.getPersonID() != -1)
            {
                pCommand.Parameters.AddWithValue("@idCliente", pDocument.getPersonID());
            }
            if (pDocument.getName() != "")
            {
                pCommand.Parameters.AddWithValue("@nombreDocumento", pDocument.getName());
            }
            if (pDocument.getDescription() != "")
            {
                pCommand.Parameters.AddWithValue("@descripcionDocumento", pDocument.getDescription());
            }
        }

        public override void insert(PersonDocumentDTO pDocument, MySqlCommand pCommand)
        {
            string tableName = "DOCUMENTO_PERSONA";
            string columns = "documento, nombreDocumento, descripcionDocumento, idPersona";
            string values = "@documento, @nombreDocumento, @descripcionDocumento, @idPersona";
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@documento", pDocument.getDocHexBytes());
            pCommand.Parameters.AddWithValue("@nombreDocumento", pDocument.getName());
            pCommand.Parameters.AddWithValue("@descripcionDocumento", pDocument.getDescription());
            pCommand.Parameters.AddWithValue("@idPersona", pDocument.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void delete(PersonDocumentDTO pDocument, MySqlCommand pCommand)
        {
            string tableName = "DOCUMENTO_PERSONA";
            string condition = "idCliente = @idCliente";
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@idCliente", pDocument.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonDocumentDTO pNewDoc, PersonDocumentDTO pPastDoc, MySqlCommand pCommand)
        {
            string tableName = "DOCUMENTO_PERSONA";
            string values = "idCliente=@nuevoIdCliente, CIF=@nuevoCIF, activo=@nuevoActivo";
            string condition = "idCliente = @idClienteAnterior";
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevoIdCliente", pNewDoc.getPersonID());
            pCommand.Parameters.AddWithValue("@nuevoCIF", pNewDoc.getCIF());
            pCommand.Parameters.AddWithValue("@nuevoActivo", boolToSql(pNewDoc.isActive()));
            pCommand.Parameters.AddWithValue("@idClienteAnterior", pPastDoc.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonDocumentDTO> search(PersonDocumentDTO pDocument, MySqlCommand pCommand)
        {
            string selection = "*";
            string from = "DOCUMENTO_PERSONA";
            string condition = getFindCondition(pDocument);
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pDocument);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonDocumentDTO> list = new List<PersonDocumentDTO>();

            while (reader.Read())
            {
                PersonDocumentDTO client = new PersonDocumentDTO();
                client.setClientID((int)reader["idCliente"]);
                client.setCIF(reader["CIF"].ToString());
                client.setActive(sqlToBool(reader["cedula"].ToString()));
                list.Add(client);
            }
            return list;
        }
    }
}
