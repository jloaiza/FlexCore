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

        public virtual void insert(DTO pPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                insert(pPerson, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public virtual void delete(DTO pPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                delete(pPerson, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public virtual void update(DTO pNewPerson, DTO pPastPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                update(pNewPerson, pPastPerson, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public virtual List<DTO> search(DTO pPerson, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                return search(pPerson, command, pPageNumber, pShowCount, pOrderBy);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public virtual void newPerson(DTO pPerson, List<PersonAddressDTO> pAddresses, List<PersonPhoneDTO> pPhones, List<PersonDocumentDTO> pDocuments, PersonPhotoDTO pPhoto)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            MySqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                newPerson(pPerson, command, pAddresses, pPhones, pDocuments, pPhoto);
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public virtual void newPerson(DTO pPerson, MySqlCommand pCommand, List<PersonAddressDTO> pAddresses=null, List<PersonPhoneDTO> pPhones=null, List<PersonDocumentDTO> pDocuments=null, PersonPhotoDTO pPhoto=null)
        {
            insert(pPerson, pCommand);
            if (pAddresses != null)
            {
                addAddress(pAddresses, pCommand);
            }
            if (pPhoto != null)
            {
                updatePhoto(pPhoto, pCommand);
            }
            if (pPhones != null)
            {
                addPhone(pPhones, pCommand);
            }
            if (pDocuments != null)
            {
                addDoc(pDocuments, pCommand);
            }
        }

        public abstract void insert(DTO pPerson, MySqlCommand pCommand);

        public abstract void delete(DTO pPerson, MySqlCommand pCommand);

        public abstract void update(DTO pNewPerson, DTO pPastPerson, MySqlCommand pCommand);

        public abstract List<DTO> search(DTO pPerson, MySqlCommand pCommand, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy);

        public abstract List<DTO> getAll(int pPageNumber=0, int pShowCount=0, params string[] pOrderBy);

        public bool exists(PersonDTO pPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                return exists(pPerson, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public bool exists(PersonDTO pPerson, MySqlCommand pCommand)
        {
            try
            {
                return PersonDAO.getInstance().search(pPerson, pCommand)[0] != null;
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        }

        //Address

        public void addAddress(List<PersonAddressDTO> pAddresses)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                addAddress(pAddresses, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

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

        public void deleteAddress(List<PersonAddressDTO> pAddresses)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                deleteAddress(pAddresses, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void deleteAddress(List<PersonAddressDTO> pAddresses, MySqlCommand pCommand)
        {
            try
            {
                PersonAddressDAO dao = PersonAddressDAO.getInstance();
                foreach (PersonAddressDTO address in pAddresses)
                {
                    dao.delete(address, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new DeleteException();
            }
        }

        public List<PersonAddressDTO> getAddress(PersonDTO pPerson)
        {
            PersonAddressDTO dummy = new PersonAddressDTO(pPerson.getPersonID());
            try
            {                
                return PersonAddressDAO.getInstance().search(dummy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
            
        }

        //Photo

        public void updatePhoto(PersonPhotoDTO pPhoto)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                updatePhoto(pPhoto, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void updatePhoto(PersonPhotoDTO pPhoto, MySqlCommand pCommand)
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

        public PersonPhotoDTO getPhoto(PersonDTO pPerson)
        {
            PersonPhotoDTO dummy = new PersonPhotoDTO(pPerson.getPersonID());
            try
            {
                return PersonPhotoDAO.getInstance().search(dummy)[0];
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
            
        }

        //Phone

        public void addPhone(List<PersonPhoneDTO> pPhones)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                addPhone(pPhones, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
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

        public void deletePhone(List<PersonPhoneDTO> pPhones)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                deletePhone(pPhones, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void deletePhone(List<PersonPhoneDTO> pPhones, MySqlCommand pCommand)
        {
            try
            {
                PersonPhoneDAO dao = PersonPhoneDAO.getInstance();
                foreach (PersonPhoneDTO phone in pPhones)
                {
                    dao.delete(phone, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

        public List<PersonPhoneDTO> getPhones(PersonDTO pPerson)
        {
            PersonPhoneDTO dummy = new PersonPhoneDTO(pPerson.getPersonID());
            try
            {
                return PersonPhoneDAO.getInstance().search(dummy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        }

        //Document

        public void addDoc(List<PersonDocumentDTO> pDocuments)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                addDoc(pDocuments, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void addDoc(List<PersonDocumentDTO> pDocuments, MySqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO dao = PersonDocumentDAO.getInstance();
                PersonDocumentDTO result;
                foreach (PersonDocumentDTO document in pDocuments)
                {
                    result = dao.search(document)[0];
                    if (result != null)
                    {
                        dao.update(document, document, pCommand);
                    }
                    else
                    {
                        dao.insert(document, pCommand);
                    }
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }        

        public void deleteDoc(List<PersonDocumentDTO> pDocuments)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                deleteDoc(pDocuments, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void deleteDoc(List<PersonDocumentDTO> pDocuments, MySqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO dao = PersonDocumentDAO.getInstance();
                foreach (PersonDocumentDTO document in pDocuments)
                {
                    dao.delete(document, pCommand);
                }
                
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

        public PersonDocumentDTO getCompleteDoc(PersonDocumentDTO pDocumment)
        {
            try
            {
                return PersonDocumentDAO.getInstance().search(pDocumment)[0];
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        }

        public List<PersonDocumentDTO> getPartialDoc(PersonDocumentDTO pDocument, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            return PersonDocumentDAO.getInstance().searchPartial(pDocument, pPageNumber, pShowCount, pOrderBy);
        }

    }
}
