using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    class PersonPhotoDAO:GeneralDAO<PersonPhotoDTO>
    {
        protected override string getFindCondition(PersonPhotoDTO pPhoto)
        {
            //Este método no es necesario para esta clase
            return null;
        }

        protected override void setFindParameters(MySqlCommand pCommand, PersonPhotoDTO pPhoto)
        {
            //Este método no es necesario para esta clase
        }

        public override void insert(PersonPhotoDTO pPhoto, MySqlCommand pCommand)
        {
            string tableName = "FOTO_PERSONA";
            string columns = "fotografia, idPersona";
            string values = "@fotografia, @idPersona";
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@fotografia", pPhoto.getHexBytes());
            pCommand.Parameters.AddWithValue("@idPersona", pPhoto.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void delete(PersonPhotoDTO pPhoto, MySqlCommand pCommand)
        {
            string tableName = "FOTO_PERSONA";
            string condition = "idPersona = @idPersona";
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@idPersona", pPhoto.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonPhotoDTO pNewPhoto, PersonPhotoDTO pPastPhoto, MySqlCommand pCommand)
        {
            string tableName = "FOTO_PERSONA";
            string values = "idPersona=@nuevoIdPersona, fotografia=@fotografia";
            string condition = "idPersona = @idPersonaAnterior";
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevoIdPersona", pNewPhoto.getPersonID());
            pCommand.Parameters.AddWithValue("@fotografia", pNewPhoto.getHexBytes());
            pCommand.Parameters.AddWithValue("@idPersonaAnterior", pPastPhoto.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonPhotoDTO> search(PersonPhotoDTO pPhoto, MySqlCommand pCommand)
        {
            string selection = "*";
            string from = "FOTO_PERSONA";
            string condition = "idPersona = @idPersona";
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@idPersona", pPhoto.getPersonID());

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonPhotoDTO> list = new List<PersonPhotoDTO>();

            while (reader.Read())
            {
                PersonPhotoDTO photo = new PersonPhotoDTO();
                photo.setPersonID((int)reader["idPersona"]);
                photo.setHexBytes(reader["fotografia"].ToString());
                list.Add(photo);
            }
            return list;
        }
    }
}
