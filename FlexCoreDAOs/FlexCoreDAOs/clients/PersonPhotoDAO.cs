﻿using System;
using System.Collections.Generic;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreDTOs.clients;

namespace FlexCoreDAOs.clients
{
    public class PersonPhotoDAO:GeneralDAO<PersonPhotoDTO>
    {

        public static readonly string PHOTO = "foto";
        public static readonly string PERSON_ID = "idPersona";

        private static object _syncLock = new object();
        private static PersonPhotoDAO _instance;

        public static PersonPhotoDAO getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonPhotoDAO();
                    }
                }
            }
            return _instance;
        }

        private PersonPhotoDAO() { }

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

        public override List<PersonPhotoDTO> search(PersonPhotoDTO pPhoto, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            string selection = "*";
            string from = "FOTO_PERSONA";
            string condition = String.Format("{0}= @{0}", PERSON_ID);
            string query = getSelectQuery(selection, from, condition, pPageNumber, pShowCount, pOrderBy);

            pCommand.CommandText = query;
            pCommand.Parameters.AddWithValue("@"+PERSON_ID, pPhoto.getPersonID());

            MySqlDataReader reader = pCommand.ExecuteReader();
            List<PersonPhotoDTO> list = new List<PersonPhotoDTO>();

            while (reader.Read())
            {
                PersonPhotoDTO photo = new PersonPhotoDTO(pPhoto.getPersonID());
                photo.setHexBytes((byte[])reader[PHOTO]);
                list.Add(photo);
            }
            return list;
        }
    }
}
