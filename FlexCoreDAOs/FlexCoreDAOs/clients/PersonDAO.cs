using System.Collections.Generic;

using FlexCoreDTOs.clients;
using MySql.Data.MySqlClient;

namespace FlexCoreDAOs.clients
{
    public class PersonDAO:GeneralDAO<PersonDTO>
    {
        protected override string getFindCondition(PersonDTO pPerson)
        {
            //CAMBIAR POR DEFAULTS
            string condition = "";
            if (pPerson.getPersonID() != -1)
            {
                condition = addCondition(condition, "idPersona = @idPersona");
            }
            if (pPerson.getName() != "")
            {
                condition = addCondition(condition, "nombre LIKE %@nombre%");
            }
            if (pPerson.getIDCard() != "")
            {
                condition = addCondition(condition, "cedula = @cedula");
            }
            if (pPerson.getPersonType() != "")
            {
                condition = addCondition(condition, "tipo = @tipo");
            }
            return condition;
        }

        protected override void setFindParameters(MySqlCommand pCommand, PersonDTO pPerson)
        {
            if (pPerson.getPersonID() != -1)
            {
                pCommand.Parameters.AddWithValue("@idPersona", pPerson.getPersonID());
            }

            if (pPerson.getName() != "")
            {
                pCommand.Parameters.AddWithValue("@nombre", pPerson.getName());
            }
            if (pPerson.getIDCard() != "")
            {
                pCommand.Parameters.AddWithValue("@cedula", pPerson.getIDCard());
            }
            if (pPerson.getPersonType() != "")
            {
                pCommand.Parameters.AddWithValue("@tipo", pPerson.getPersonType());
            }
        }

        public override void insert(PersonDTO pPerson, MySqlCommand pCommand)
        {
            string tableName = "PERSONA";
            string columns = "nombre, cedula, tipo";
            string values = "@nombre, @cedula, @tipo";
            string query = getInsertQuery(tableName, columns, values);
            
            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nombre", pPerson.getName());
            pCommand.Parameters.AddWithValue("@cedula", pPerson.getIDCard());
            pCommand.Parameters.AddWithValue("@tipo", pPerson.getPersonType());
            pCommand.ExecuteNonQuery();
        }
        public override void delete(PersonDTO pPerson, MySqlCommand pCommand)
        {
            string tableName = "PERSONA";
            string condition = "idPersona = @idPersona";
            string query = getDeleteQuery(tableName, condition);
            
            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@idPersona", pPerson.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override void update(PersonDTO pNewPerson, PersonDTO pPastPerson, MySqlCommand pCommand)
        {
            string tableName = "PERSONA";
            //Añadir trigger que valide el cambio de tipo
            string values = "idPersona=@nuevoIdPersona, nombre=@nuevoNombre, cedula=@nuevaCedula, tipo=@nuevoTipo";
            string condition = "idPersona = @idPersonaAnterior";
            string query = getUpdateQuery(tableName, values, condition);
            
            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@nuevoIdPersona", pNewPerson.getPersonID());
            pCommand.Parameters.AddWithValue("@nuevoNombre", pNewPerson.getName());
            pCommand.Parameters.AddWithValue("@nuevaCedula", pNewPerson.getIDCard());
            pCommand.Parameters.AddWithValue("@nuevoTipo", pNewPerson.getPersonType());
            pCommand.Parameters.AddWithValue("@idPersonaAnterior", pPastPerson.getPersonID());
            pCommand.ExecuteNonQuery();
        }

        public override List<PersonDTO> search(PersonDTO pPerson, MySqlCommand pCommand)
        {
            string selection = "*";
            string from = "PERSONA";
            string condition = getFindCondition(pPerson);
            string query = getSelectQuery(selection, from, condition);

            pCommand.CommandText = query;
            setFindParameters(pCommand, pPerson);

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonDTO> list = new List<PersonDTO>();

            while (reader.Read())
            {
                PersonDTO person = new PersonDTO();
                person.setPersonID((int)reader["idPersona"]);
                person.setName(reader["nombre"].ToString());
                person.setIDCard(reader["cedula"].ToString());
                person.setPersonType(reader["tipo"].ToString());
                list.Add(person);
            }
            return list;
        }
    
    }
}
