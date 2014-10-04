using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    class PersonAddressDAO:GeneralDAO<PersonAddressDTO>
    {
        protected override string getFindCondition(PersonAddressDTO pAddress)
        {
            //Este método no es necesario para esta clase
            return null;
        }

        protected override void setFindParameters(MySqlCommand pCommand, PersonAddressDTO pAddress)
        {
            //Este método no es necesario para esta clase
        }

        public override void insert(PersonAddressDTO pAddress, MySqlCommand pCommand)
        {
            string tableName = "DIRECCION_PERSONA";
            string columns = "direccion, idPersona";
            string values = "@direccion, @idPersona";
            string query = getInsertQuery(tableName, columns, values);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@direccion", pAddress.getAddress());
            pCommand.Parameters.AddWithValue("@idPersona", pAddress.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void delete(PersonAddressDTO pAddress, MySqlCommand pCommand)
        {
            string tableName = "DIRECCION_PERSONA";
            string condition = "idPersona = @idPersona";
            string query = getDeleteQuery(tableName, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@idPersona", pAddress.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonAddressDTO pNewPhoto, PersonAddressDTO pPastPhoto, MySqlCommand pCommand)
        {
            string tableName = "DIRECCION_PERSONA";
            string values = "idPersona=@nuevoIdPersona, direccion=@direccion";
            string condition = "idPersona = @idPersonaAnterior";
            string query = getUpdateQuery(tableName, values, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevoIdPersona", pNewPhoto.getPersonID());
            pCommand.Parameters.AddWithValue("@direccion", pNewPhoto.getAddress());
            pCommand.Parameters.AddWithValue("@idPersonaAnterior", pPastPhoto.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonAddressDTO> search(PersonAddressDTO pAddress, MySqlCommand pCommand)
        {
            string selection = "*";
            string from = "DIRECCION_PERSONA";
            string condition = "idPersona = @idPersona";
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@idPersona", pAddress.getPersonID());

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonAddressDTO> list = new List<PersonAddressDTO>();

            while (reader.Read())
            {
                PersonAddressDTO photo = new PersonAddressDTO();
                photo.setPersonID((int)reader["idPersona"]);
                photo.setAddress(reader["direccion"].ToString());
                list.Add(photo);
            }
            return list;
        }
    }
}
