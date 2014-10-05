using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    class PersonPhotoDAO:GeneralDAO<PersonPhotoDTO>
    {

        private static readonly string PHOTO = "foto";
        private static readonly string PERSON_ID = "idPersona";

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
            string columns = String.Format("{0}, {1}", PHOTO, PERSON_ID);
            string values = String.Format("@{0}, @{1}", PHOTO, PERSON_ID);
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+PHOTO, pPhoto.getHexBytes());
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pPhoto.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void delete(PersonPhotoDTO pPhoto, MySqlCommand pCommand)
        {
            string tableName = "FOTO_PERSONA";
            string condition = String.Format("{0} = @{0}", PERSON_ID);
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pPhoto.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonPhotoDTO pNewPhoto, PersonPhotoDTO pPastPhoto, MySqlCommand pCommand)
        {
            string tableName = "FOTO_PERSONA";
            string values = String.Format("{0}=@nuevo{0}, {1}=@nuevo{1}", PERSON_ID, PHOTO);
            string condition = String.Format("{0} = @{0}Anterior", PERSON_ID);
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevo"+PERSON_ID, pNewPhoto.getPersonID());
            pCommand.Parameters.AddWithValue("@nuevo"+PHOTO, pNewPhoto.getHexBytes());
            pCommand.Parameters.AddWithValue("@"+PERSON_ID+"Anterior", pPastPhoto.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonPhotoDTO> search(PersonPhotoDTO pPhoto, MySqlCommand pCommand)
        {
            string selection = "*";
            string from = "FOTO_PERSONA";
            string condition = String.Format("{0}= @{0}", PERSON_ID);
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pPhoto.getPersonID());

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonPhotoDTO> list = new List<PersonPhotoDTO>();

            while (reader.Read())
            {
                PersonPhotoDTO photo = new PersonPhotoDTO(pPhoto.getPersonID());
                photo.setHexBytes(reader[PHOTO].ToString());
                list.Add(photo);
            }
            return list;
        }
    }
}
