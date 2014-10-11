using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using FlexCoreDAOs.clients;
using MySql.Data;
using MySql.Data.MySqlClient;
using FlexCoreLogic.exceptions;
using ConexionMySQLServer.ConexionMySql;

namespace FlexCoreLogic.clients
{

    abstract class AbstractPersonLogic<DTO>
    {

        public abstract void insert(DTO pPerson, MySqlCommand pCommand);

        public abstract void delete(DTO pPerson, MySqlCommand pCommand);

        public abstract void update(DTO pPerson, MySqlCommand pCommand);

        public abstract List<DTO> search(DTO pPerson, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy);

        public abstract List<DTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy);

        public void addAddress(List<PersonAddressDTO> pAddresses, MySqlCommand pCommand){
            try
            {
                PersonAddressDAO dao = PersonAddressDAO.getInstance();
                foreach (PersonAddressDTO address in pAddresses)
                {
                    dao.insert(address, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new InsertException();
            }
        }

        public void addPhoto(PersonPhotoDTO pPhoto, MySqlCommand pCommand)
        {
            try
            {
                PersonPhotoDAO dao = PersonPhotoDAO.getInstance();
                PersonPhotoDTO result = dao.search(pPhoto, pCommand)[0];
                if (result != null)
                {
                    dao.update(pPhoto, pPhoto, pCommand);
                }
                else
                {
                    dao.insert(pPhoto, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }
        
        public void addPhone(List<PersonPhoneDTO> pPhones, MySqlCommand pCommand)
        {
            try
            {
                PersonPhoneDAO dao = PersonPhoneDAO.getInstance();
                foreach (PersonPhoneDTO phone in pPhones)
                {
                    dao.insert(phone, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

        public void addDoc(PersonDocumentDTO pDocument, MySqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO dao = PersonDocumentDAO.getInstance();
                PersonDocumentDTO result = dao.search(pDocument)[0];
                if (result != null)
                {
                    dao.update(pDocument, pDocument, pCommand);
                }
                else
                {
                    dao.insert(pDocument, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

    }
}
